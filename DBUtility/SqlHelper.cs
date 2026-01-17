using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace ZWL.DBUtility
{
    public sealed class SqlHelper
    {
        #region Constructures & Private Utility Methods
        private SqlHelper() { }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
        private static void attachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                    p.Value = DBNull.Value;

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        private static void prepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
                connection.Open();

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
                command.Transaction = transaction;

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
                attachParameters(command, commandParameters);
        }
        #endregion

        #region GetConnectionString
        /// <summary>
        /// 获取读写操作的数据库连接字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConnectionString_RW(Type type)
        {
            string connectionStr = string.Empty;
            if (type != null)
            {
                string ns = type.Namespace;
                if (!string.IsNullOrEmpty(ns))
                {
                    string key = string.Empty;
                    switch (ns)
                    {
                        case "Com.EnjoyCodes.SqlHelper":
                        default: key = "MSSQLConnectionString"; break;
                    }
                    connectionStr = GetConnectionString(key);
                }
            }
            return connectionStr;
        }

        public static string GetConnectionString(string key) { return ConfigurationManager.AppSettings[key]; }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        { return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null); }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            int result = 0;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                try
                {
                    result = ExecuteNonQuery(cn, commandType, commandText, commandParameters);
                    cn.Close();
                    cn.Dispose();
                }
                catch (Exception ex)
                {
                    cn.Close();
                    cn.Dispose();
                    throw ex;
                }
            }

            return result;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            prepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //finally, execute the command.
            int retval = cmd.ExecuteNonQuery();

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return retval;
        }
        #endregion

        #region ExecuteDataSet
        public static DataSet ExecuteDataSet(string connectionString, string commandText)
        {
            return ExecuteDataSet(connectionString,CommandType.Text, commandText);
        }
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        { return ExecuteDataSet(connectionString, commandType, commandText, null); }

        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataSet result = null;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                try
                {
                    result = ExecuteDataSet(cn, commandType, commandText, commandParameters);
                    cn.Close();
                    cn.Dispose();
                }
                catch (Exception ex)
                {
                    cn.Close();
                    cn.Dispose();
                    throw ex;
                }
            }

            return result;
        }
        public static DataTable ExecuteDataTable(string connectionString, string commandText)
        {
            return ExecuteDataTable(connectionString, CommandType.Text, commandText, null);
        }
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataTable(connectionString, commandType, commandText, null);
        }
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable result = null;
            var ds = ExecuteDataSet(connectionString, commandType, commandText, commandParameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = ds.Tables[0];
            }
            return result;
        }
        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            prepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the SqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        { return ExecuteScalar(connectionString, commandType, commandText, null); }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object result = null;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                try
                {
                    result = ExecuteScalar(cn, commandType, commandText, commandParameters);
                    cn.Close();
                    cn.Dispose();
                }
                catch (Exception ex)
                {
                    cn.Close();
                    cn.Dispose();
                    throw ex;
                }
            }
            return result;
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            prepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }

        public static bool IsExists(string connectionString, string commandText)
        { return Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, commandText, null)) != 0; }

        public static bool IsExists(string connectionString, string commandText, params SqlParameter[] commandParameters)
        { return Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, commandText, commandParameters)) != 0; }
        #endregion
    }

    public class SqlHelper<T>
    {
        #region Members & Utility Methods
        /// <summary>
        /// C#类型与SQLServer类型对照字典
        /// </summary>
        public static Dictionary<Type, SqlDbType> SqlDbTypes = new Dictionary<Type, SqlDbType>() {
            {typeof(long),SqlDbType.BigInt},
            {typeof(int),SqlDbType.Int},
            {typeof(short),SqlDbType.SmallInt},
            {typeof(byte),SqlDbType.TinyInt},
            {typeof(decimal),SqlDbType.Decimal},
            {typeof(double),SqlDbType.Float},
            {typeof(float),SqlDbType.Real},
            {typeof(bool),SqlDbType.Bit},
            {typeof(string),SqlDbType.NVarChar},
            {typeof(char),SqlDbType.Char},
            {typeof(DateTime),SqlDbType.DateTime},
            {typeof(TimeSpan),SqlDbType.Timestamp},
            {typeof(Guid),SqlDbType.UniqueIdentifier},
            {typeof(Enum),SqlDbType.Int}
        };

        private static void fill(T obj, IDataReader dr, string columnPrefix, PropertyInfo[] properties)
        {
            foreach (var item in properties)
                try
                {
                    var v = dr[columnPrefix + item.Name];
                    //if (v != null) PropertyAccessor.Set(obj, item.Name, v);
                    if (v != null) item.SetValue(obj, convertObject(v, item.PropertyType), null);
                }
                catch { }
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns></returns>
        private static object convertObject(object obj, Type type)
        {
            if (type == null) return obj;
            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return type.IsValueType ? Activator.CreateInstance(type) : null;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType()))
            {
                // 如果待转换对象的类型与目标类型兼容，则无需转换
                return obj;
            }
            else if ((underlyingType ?? type).IsEnum)
            {
                // 如果待转换的对象的基类型为枚举

                if (underlyingType != null && string.IsNullOrEmpty(obj.ToString()))
                {
                    // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
                    return null;
                }
                else
                    return Enum.Parse(underlyingType ?? type, obj.ToString());
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type))
            {
                // 如果目标类型的基类型实现了IConvertible，则直接转换
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType()))
                    return converter.ConvertFrom(obj);

                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    object o = constructor.Invoke(null);
                    PropertyInfo[] propertys = type.GetProperties();
                    Type oldType = obj.GetType();
                    foreach (PropertyInfo property in propertys)
                    {
                        PropertyInfo p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                            property.SetValue(o, convertObject(p.GetValue(obj, null), property.PropertyType), null);
                    }
                    return o;
                }
            }
            return obj;
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type type) { return type.IsValueType ? Activator.CreateInstance(type) : null; }
        public static int CreateTable(string connectionString, string modelTableName, string modelPrimaryKey, string columnPrefix)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat("CREATE TABLE [{0}](", modelTableName);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var item in properties)
            {
                try
                {
                    if (!item.PropertyType.IsSealed) continue;

                    sqlStr.AppendFormat("[{0}{1}] ", columnPrefix, item.Name);
                    if (item.PropertyType == typeof(string))
                        sqlStr.AppendFormat("{0}(max)", SqlDbTypes[item.PropertyType]);
                    else if (item.PropertyType.IsEnum)
                        sqlStr.AppendFormat("{0}", SqlDbTypes[typeof(Enum)]);
                    else
                        sqlStr.AppendFormat("{0}", SqlDbTypes[item.PropertyType]);

                    if (item.Name.ToLower() == modelPrimaryKey.ToLower())
                    {
                        sqlStr.Append(" PRIMARY KEY ");
                        if (item.PropertyType == typeof(Int64) || item.PropertyType == typeof(Int32) || item.PropertyType == typeof(Int16))
                            sqlStr.Append("IDENTITY");
                    }
                    sqlStr.Append(",");
                }
                catch { }
            }

            sqlStr.Append(")");

            return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlStr.ToString());
        }
        #endregion
    }
}