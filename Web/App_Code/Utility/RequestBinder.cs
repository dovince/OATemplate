using Aspose.Words;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

/// <summary>
/// RequestBinder 的摘要说明
/// </summary>
namespace Request.Binder
{
    public interface IRequestBinder
    {
        object Converter(RequestBinderContext binderContext);
    }
    public class RequestBinderContext
    {
        private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;
        private CultureInfo _instanceCulture;
        public CultureInfo Culture
        {
            get
            {
                if (_instanceCulture == null)
                {
                    _instanceCulture = _staticCulture;
                }
                return _instanceCulture;
            }
            set
            {
                _instanceCulture = value;
            }
        }
        public string ParameterName { get; set; }
        public Type PModelType { get; set; }
        public Type ModelType { get; set; }
        public int ParameterIndex { get; set; }
        public string ParameterRawValue { get; set; }
        public object ParameterDefaultValue { get; set; }
        private HttpContextBase httpContextBase;
        public HttpContextBase HttpContextBase
        {
            get
            {
                if (httpContextBase == null)
                {
                    if (HttpContext.Current == null)
                        throw new ArgumentNullException("HttpContextBase", "HttpContext.Current is null");
                    return new HttpContextWrapper(HttpContext.Current);
                }
                return httpContextBase;
            }
            set
            {
                httpContextBase = value;
            }
        }
    }
    internal class RequestBinderHelpers
    {
        public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> matchesInterface = t => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType;
            return (matchesInterface(queryType)) ? queryType : queryType.GetInterfaces().FirstOrDefault(matchesInterface);
        }
        public static object GetTypeDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
        /// <summary>
        /// Simple model = int, string, etc.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(Type type)
        {
            return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
        }
        public static object ChangeType(Type type, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(type);
            return tc.ConvertFrom(value);
        }

        private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.IsInstanceOfType(value))
            {
                return value;
            }

            // if this is a user-input value but the user didn't type anything, return no value
            string valueAsString = value as string;
            if (valueAsString != null && valueAsString.Trim().Length == 0)
            {
                return null;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
            bool canConvertFrom = converter.CanConvertFrom(value.GetType());
            if (!canConvertFrom)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }
            if (!(canConvertFrom || converter.CanConvertTo(destinationType)))
            {
                throw new InvalidOperationException("converter Exception");
            }

            try
            {
                object convertedValue = (canConvertFrom) ?
                     converter.ConvertFrom(null /* context */, culture, value) :
                     converter.ConvertTo(null /* context */, culture, value, destinationType);
                return convertedValue;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public static object ConvertTo(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.IsInstanceOfType(value))
            {
                return value;
            }

            // array conversion results in four cases, as below
            Array valueAsArray = value as Array;
            if (destinationType.IsArray)
            {
                Type destinationElementType = destinationType.GetElementType();
                if (valueAsArray != null)
                {
                    // case 1: both destination + source type are arrays, so convert each element
                    IList converted = Array.CreateInstance(destinationElementType, valueAsArray.Length);
                    for (int i = 0; i < valueAsArray.Length; i++)
                    {
                        converted[i] = ConvertSimpleType(culture, valueAsArray.GetValue(i), destinationElementType);
                    }
                    return converted;
                }
                else
                {
                    // case 2: destination type is array but source is single element, so wrap element in array + convert
                    object element = ConvertSimpleType(culture, value, destinationElementType);
                    IList converted = Array.CreateInstance(destinationElementType, 1);
                    converted[0] = element;
                    return converted;
                }
            }
            else if (valueAsArray != null)
            {
                // case 3: destination type is single element but source is array, so extract first element + convert
                if (valueAsArray.Length > 0)
                {
                    value = valueAsArray.GetValue(0);
                    return ConvertSimpleType(culture, value, destinationType);
                }
                else
                {
                    // case 3(a): source is empty array, so can't perform conversion
                    return null;
                }
            }
            // case 4: both destination + source type are single elements, so convert
            return ConvertSimpleType(culture, value, destinationType);
        }
    }
    public static class RequestBinder
    {
        private static readonly Dictionary<string, RuntimeTypeHandle>
          registeredConverters = new Dictionary<string, RuntimeTypeHandle>();
        private static readonly Dictionary<string, IRequestBinder>
          instantiatedConverters = new Dictionary<string, IRequestBinder>();
        private static readonly Dictionary<string, object>
           defaultValues = new Dictionary<string, object>();
        static RequestBinder()
        {
            RegistConvert(typeof(Boolean), typeof(BooleanRequestBinder));
        }
        public static T UpdateModel<T>() where T : class
        {
            return UpdateModel<T>(string.Empty);
        }
        public static T UpdateModel<T>(string parameterName)
        {
            return (T)UpdateModel(new RequestBinderContext
            {
                ModelType = typeof(T),
                ParameterName = parameterName
            });
        }
        private static object UpdateModel(RequestBinderContext binderContext)
        {
            if (RequestBinderHelpers.IsSimpleType(binderContext.ModelType))
            {
                if (string.IsNullOrEmpty(binderContext.ParameterName))
                    throw new ArgumentNullException("parameterName");
                return BindSimpleModel(binderContext);
            }
            var requestValues = GetRequestValues(binderContext);
            if (requestValues != null && requestValues.Length > 0)
            {
                return BindSimpleModel(binderContext);
            }
            return BindComplexModel(binderContext);
        }
        private static NameValueCollection GetCleanRequest(NameValueCollection original)
        {
            NameValueCollection result = new NameValueCollection();
            if (original != null && original.AllKeys != null && original.AllKeys.Length > 0)
            {
                original.AllKeys.ToList().ForEach(okey =>
                {
                    var cleanKey = okey.Split("$".ToArray(), StringSplitOptions.RemoveEmptyEntries).Last();
                    var values = original.GetValues(okey);
                    values.ToList().ForEach(v =>
                    {
                        result.Add(cleanKey, v);
                    });
                });
            }
            return result;
        }
        private static string[] GetRequestValues(RequestBinderContext binderContext)
        {
            string[] result = new string[] { };
            result = GetCleanRequest(binderContext.HttpContextBase.Request.Form).GetValues(binderContext.ParameterName);
            if (result == null || result.Length == 0)
            {
                result = GetCleanRequest(binderContext.HttpContextBase.Request.QueryString).GetValues(binderContext.ParameterName);
            }
            return result;
        }
        private static NameValueCollection GetRequestCollection(RequestBinderContext binderContext)
        {
            var result = GetCleanRequest(binderContext.HttpContextBase.Request.Form);
            if (result == null || result.Count == 0)
            {
                result = GetCleanRequest(binderContext.HttpContextBase.Request.QueryString);
            }
            return result;
        }
        private static string GetRequestValue(RequestBinderContext binderContext)
        {
            var temp = GetRequestValues(binderContext);
            if (temp != null && temp.Length > 0)
            {
                return temp.ElementAtOrDefault(binderContext.ParameterIndex);
            }
            return null;
        }
        private static object GetTypeDefaultValue(Type type)
        {
            object defaultValue;
            if (!defaultValues.TryGetValue(type.FullName, out defaultValue))
            {
                return RequestBinderHelpers.GetTypeDefaultValue(type);
            }
            return defaultValue;
        }
        private static object BindSimpleModel(RequestBinderContext binderContext)
        {
            object requestRawValue = GetRequestValues(binderContext);
            if (binderContext.ModelType.IsInstanceOfType(requestRawValue))
            {
                return requestRawValue;
            }
            if (binderContext.ModelType != typeof(string))
            {
                if (binderContext.ModelType.IsArray)
                {
                    Type elementType = binderContext.ModelType.GetElementType();
                    if (!RequestBinderHelpers.IsSimpleType(elementType) && string.IsNullOrEmpty(binderContext.ParameterName))
                    {
                        throw new ArgumentNullException("parameterName", "简单类型必须指定ParameterName");
                    }
                    Type listType = typeof(List<>).MakeGenericType(elementType);
                    IList collection = CreateModel(listType) as IList;

                    binderContext.ParameterDefaultValue = GetTypeDefaultValue(binderContext.ModelType);
                    var values = GetRequestValues(binderContext);
                    if (values == null || values.Length == 0)
                    {
                        return binderContext.ParameterDefaultValue;
                    }
                    Array.ForEach(values, v =>
                    {
                        binderContext.ParameterDefaultValue = GetTypeDefaultValue(elementType);
                        binderContext.ModelType = elementType;
                        var temp = UpdateModel(binderContext);
                        binderContext.ParameterIndex++;
                        collection.Add(temp);
                    });
                    binderContext.ParameterIndex = 0;//reset parameterIndex

                    Array array = Array.CreateInstance(elementType, collection.Count);
                    collection.CopyTo(array, 0);
                    return array;
                }
                Type enumerableType = RequestBinderHelpers.ExtractGenericInterface(binderContext.ModelType, typeof(IEnumerable<>));
                if (enumerableType != null)
                {
                    Type elementType = enumerableType.GetGenericArguments()[0];
                    if (!RequestBinderHelpers.IsSimpleType(elementType) && string.IsNullOrEmpty(binderContext.ParameterName))
                    {
                        throw new ArgumentNullException("parameterName", "简单类型必须指定ParameterName");
                    }
                    Type collectionType = typeof(ICollection<>).MakeGenericType(elementType);
                    IList collection = CreateModel(collectionType) as IList;

                    binderContext.ParameterDefaultValue = GetTypeDefaultValue(binderContext.ModelType);
                    var values = GetRequestValues(binderContext);
                    if (values == null || values.Length == 0)
                    {
                        return binderContext.ParameterDefaultValue;
                    }
                    Array.ForEach(values, v =>
                    {
                        binderContext.ParameterDefaultValue = GetTypeDefaultValue(elementType);
                        binderContext.ModelType = elementType;
                        var temp = UpdateModel(binderContext);
                        binderContext.ParameterIndex++;
                        collection.Add(temp);
                    });
                    binderContext.ParameterIndex = 0;//reset parameterIndex
                    return collection;
                }
            }
            binderContext.ParameterDefaultValue = GetTypeDefaultValue(binderContext.ModelType);
            binderContext.ParameterRawValue = GetRequestValue(binderContext);
            if (RequestBinder.ContainsConvert(binderContext.ModelType))
            {
                return GetConverter(binderContext.ModelType).Converter(binderContext);
            }
            return RequestBinderBase.GeneralBind(binderContext);
        }
        private static object BindComplexModel(RequestBinderContext binderContext)
        {

            if (binderContext.ModelType.IsArray)
            {
                return null;//TODO
            }
            // special-case IDictionary<,> and ICollection<>
            Type dictionaryType = RequestBinderHelpers.ExtractGenericInterface(binderContext.ModelType, typeof(IDictionary<,>));
            if (dictionaryType != null)
            {
                return null;//TODO
            }
            Type enumerableType = RequestBinderHelpers.ExtractGenericInterface(binderContext.ModelType, typeof(IEnumerable<>));
            if (enumerableType != null)
            {
                return BindCollectionModel(binderContext);//TODO
            }
            // otherwise, just update the properties on the complex type
            binderContext.ParameterDefaultValue = GetTypeDefaultValue(binderContext.ModelType);
            return BindComplexElementalModel(binderContext);

        }
        internal static object BindComplexElementalModel(RequestBinderContext binderContext)
        {

            if (binderContext.ParameterName.Length > 0 &&
                ((binderContext.PModelType == null && binderContext.ParameterName != binderContext.ModelType.Name) ||
                (binderContext.PModelType != null && !RequestBinderHelpers.IsSimpleType(binderContext.PModelType) && binderContext.PModelType.GetGenericArguments()[0].Name != binderContext.ModelType.Name)))//防止嵌套循环
            {
                return null;
            }
            var memberBindings = binderContext.ModelType.GetProperties()
                                     .Select(p => Expression.Bind(p, Expression.Call(null,
                                                                     typeof(RequestBinder).GetMethod("UpdateModel", new[] { typeof(string) })
                                                                                          .MakeGenericMethod(p.PropertyType),
                                                                     Expression.Constant(binderContext.ParameterName.Length > 0 ? binderContext.ParameterName + "." + p.Name : p.Name))));
            var body = Expression.MemberInit(Expression.New(binderContext.ModelType), memberBindings.ToArray());
            var func = Expression.Lambda<Func<object>>(body, new ParameterExpression[0]);
            return (func.Compile())();
        }
        private static object BindCollectionModel(RequestBinderContext binderContext)
        {
            Type elementType = binderContext.ModelType.GetGenericArguments()[0];
            List<object> list = GetListModel(binderContext);
            object model = CreateModel(binderContext.ModelType);
            ReplaceHelper.ReplaceCollection(elementType, model, list);
            return model;
        }
        private static List<object> GetListModel(RequestBinderContext binderContext)
        {
            List<object> list = new List<object>();
            var valueProvider = GetRequestCollection(binderContext);
            var prefix = binderContext.ParameterName;
            if (!string.IsNullOrEmpty(prefix) && valueProvider.AllKeys.Any(e => e.StartsWith(prefix)))
            {
                //var result = valueProvider.Get(prefix);
                //if (null != result)
                //{
                //    IEnumerable enumerable = result.ConvertTo(modelType) as IEnumerable;
                //    foreach (var value in enumerable)
                //    {
                //        list.Add(value);
                //    }
                //}
            }
            var modelType = binderContext.ModelType;
            var elementType = modelType.GetGenericArguments()[0];
            var context = new RequestBinderContext { HttpContextBase = binderContext.HttpContextBase, PModelType = modelType, ModelType = elementType, ParameterName = prefix };
            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);
            foreach (var index in indexes)
            {
                string indexPrefix = prefix + "[" + index + "]";
                if (!valueProvider.AllKeys.Any(e => e.StartsWith(indexPrefix)) && numericIndex)
                {
                    break;
                }
                context.ParameterName = indexPrefix;
                list.Add(UpdateModel(context));
            }
            return list;
        }
        private static IEnumerable<string> GetIndexes(string prefix, NameValueCollection valueProvider, out bool numericIndex)
        {
            var key = string.IsNullOrEmpty(prefix) ? "index" : prefix + "." + "index";
            var result = valueProvider.AllKeys.Any(e => e == key);
            if (result)
            {
                var val = valueProvider.Get(key);
                string[] indexes = val.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (null != indexes)
                {
                    numericIndex = false;
                    return indexes;
                }
            }
            numericIndex = true;
            return GetZeroBasedIndexes();
        }
        private static IEnumerable<string> GetZeroBasedIndexes()
        {
            int iteratorVariable = 0;
            while (true)
            {
                yield return iteratorVariable.ToString();
                iteratorVariable++;
            }
        }
        internal static object CreateModel(Type modelType)
        {
            Type typeToCreate = modelType;

            // we can understand some collection interfaces, e.g. IList<>, IDictionary<,>
            if (modelType.IsGenericType)
            {
                Type genericTypeDefinition = modelType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    typeToCreate = typeof(Dictionary<,>).MakeGenericType(modelType.GetGenericArguments());
                }
                else if (genericTypeDefinition == typeof(IEnumerable<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IList<>))
                {
                    typeToCreate = typeof(List<>).MakeGenericType(modelType.GetGenericArguments());
                }
            }

            // fallback to the type's default constructor
            return Activator.CreateInstance(typeToCreate);
        }
        /// <summary>
        /// 注册类型默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        public static void RegistDefaultValue<T>(T defaultValue)
        {
            string typeFullName = typeof(T).FullName;
            if (defaultValues.ContainsKey(typeFullName))
            {
                defaultValues.Remove(typeFullName);
            }
            defaultValues.Add(typeFullName, defaultValue);
        }
        /// <summary>
        /// 注册类型转换器
        /// </summary>
        /// <param name="binderForType"></param>
        /// <param name="requestBinderType"></param>
        public static void RegistConvert(Type binderForType, Type requestBinderType)
        {
            Type interfaceType = requestBinderType.GetInterface("IRequestBinder");
            if (interfaceType != null)
            {
                var fullName = binderForType.FullName;
                if (registeredConverters.ContainsKey(fullName))
                    registeredConverters.Remove(fullName);
                registeredConverters.Add(fullName, requestBinderType.TypeHandle);
            }
        }

        private static bool ContainsConvert(Type type)
        {
            return registeredConverters.ContainsKey(type.FullName);
        }

        public static IRequestBinder GetConverter(Type type)
        {
            var fullName = type.FullName;
            if (!registeredConverters.ContainsKey(fullName))
                throw new Exception(
                  "No RequestBinder found for Type: " + fullName);
            if (instantiatedConverters.ContainsKey(fullName))
                return instantiatedConverters[fullName];
            else
            {
                var typeHandle = registeredConverters[fullName];
                IRequestBinder converter =
                  (IRequestBinder)Activator.CreateInstance(
                  Type.GetTypeFromHandle(typeHandle));
                instantiatedConverters.Add(fullName, converter);
                return converter;
            }
        }
    }

    public abstract class RequestBinderBase : IRequestBinder
    {
        public static object GeneralBind(RequestBinderContext binderContext)
        {
            try
            {
                if (string.IsNullOrEmpty(binderContext.ParameterRawValue))
                {
                    return binderContext.ParameterDefaultValue;
                }
                return RequestBinderHelpers.ChangeType(binderContext.ModelType, binderContext.ParameterRawValue);
            }
            catch
            {
                return binderContext.ParameterDefaultValue;
            }
        }
        #region IRequestBinder Members
        public abstract object Converter(RequestBinderContext binderContext);
        #endregion
    }



    public sealed class BooleanRequestBinder : RequestBinderBase
    {
        public override object Converter(RequestBinderContext binderContext)
        {
            if (!String.IsNullOrEmpty(binderContext.ParameterRawValue))
            {
                switch (binderContext.ParameterRawValue.Trim())
                {
                    case "False":
                    case "false":
                    case "0":
                    case "off":
                    case "":
                        return false;
                    case "True":
                    case "true":
                    case "1":
                    case "on":
                        return true;
                    default:
                        return false;
                }
            }
            else
                return binderContext.ParameterDefaultValue;
        }
    }


    internal static class ReplaceHelper
    {
        private static MethodInfo replaceCollectionMethod = typeof(ReplaceHelper).GetMethod("ReplaceCollectionImpl", BindingFlags.Static | BindingFlags.NonPublic);

        public static void ReplaceCollection(Type elementType, object model, object list)
        {
            replaceCollectionMethod.MakeGenericMethod(new Type[] { elementType }).Invoke(null, new object[] { model, list });
        }
        private static void ReplaceCollectionImpl<T>(ICollection<T> model, IEnumerable list)
        {
            model.Clear();
            if (list != null)
            {
                foreach (object obj2 in list)
                {
                    T item = (obj2 is T) ? ((T)obj2) : default(T);
                    model.Add(item);
                }
            }
        }

        private static MethodInfo replaceDictionaryMethod = typeof(ReplaceHelper).GetMethod("ReplaceDictionaryImpl", BindingFlags.Static | BindingFlags.NonPublic);

        public static void ReplaceDictionary(Type keyType, Type valueType, object dictionary, object newContents)
        {
            replaceDictionaryMethod.MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(null, new object[] { dictionary, newContents });
        }

        private static void ReplaceDictionaryImpl<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<object, object>> newContents)
        {
            dictionary.Clear();
            foreach (KeyValuePair<object, object> pair in newContents)
            {
                TKey key = (TKey)pair.Key;
                TValue local2 = (TValue)((pair.Value is TValue)
                    ? pair.Value : default(TValue));
                dictionary[key] = local2;
            }
        }
    }
}