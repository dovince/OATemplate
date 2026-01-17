using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace ZWL.Common
{
    public class RequestHelper
    {
        #region post 请求
        // 新增带 Cookie 的重载（CookieContainer 为可选参数，放在最后）
        public static string HttpPost(string url, object data, Dictionary<string, string> headerDic = null,
            string contentType = "application/x-www-form-urlencoded;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return Request(url, data, "POST", headerDic, contentType, cookieContainer);
        }

        public static T HttpPost<T>(string url, object data, Dictionary<string, string> headerDic = null,
            string contentType = "application/x-www-form-urlencoded;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return JsonConvert.DeserializeObject<T>(HttpPost(url, data, headerDic, contentType, cookieContainer));
        }
        #endregion

        #region put 请求
        // 新增带 Cookie 的重载
        public static string HttpPut(string url, object data, Dictionary<string, string> headerDic = null,
            string contentType = "application/x-www-form-urlencoded;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return Request(url, data, "PUT", headerDic, contentType, cookieContainer);
        }

        public static T HttpPut<T>(string url, object data, Dictionary<string, string> headerDic = null,
            string contentType = "application/x-www-form-urlencoded;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return JsonConvert.DeserializeObject<T>(HttpPut(url, data, headerDic, contentType, cookieContainer));
        }
        #endregion

        #region get 请求
        // 新增带 Cookie 的重载
        public static T HttpGet<T>(string url, Dictionary<string, string> urlParameters = null, Dictionary<string, string> headerDic = null,
            string contentType = "application/json;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return JsonConvert.DeserializeObject<T>(HttpGet(url, urlParameters, headerDic, contentType, cookieContainer));
        }

        public static string HttpGet(string url, Dictionary<string, string> urlParameters = null, Dictionary<string, string> headerDic = null,
            string contentType = "application/json;charset=UTF-8", CookieContainer cookieContainer = null)
        {
            return Request(BuildQuery(url, urlParameters), null, "Get", headerDic, contentType, cookieContainer);
        }
        #endregion

        #region 辅助方法  
        // 核心 Request 方法增加 CookieContainer 参数（可选）
        public static string Request(string url, object data, string method, Dictionary<string, string> headerDic,
            string contentType, CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2
                }

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method.ToUpper();
                request.ContentType = contentType;
                request.Accept = "application/json";
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = true;
                request.ServicePoint.Expect100Continue = false;

                // 绑定 Cookie 容器（如果传入则使用，否则自动创建临时容器，不影响现有逻辑）
                request.CookieContainer = cookieContainer ?? new CookieContainer();

                AddHeaderInfo(request, headerDic);

                if (data != null)
                {
                    string jsonString;

                    if (contentType.Contains("application/json"))
                    {
                        jsonString = data is string ? (string)data : JsonConvert.SerializeObject(data);
                    }
                    else
                    {
                        jsonString = ToFormUrlEncodedString(data);
                    }

                    byte[] byteData = Encoding.UTF8.GetBytes(jsonString);
                    request.ContentLength = byteData.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(byteData, 0, byteData.Length);
                    }
                }

                using (response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();
                        throw new Exception("服务器返回错误：" + error, ex);
                    }
                }
                throw;
            }
            finally
            {
                request?.Abort();
                response?.Close();
            }
        }

        /// <summary>
        /// 添加请求头信息
        /// </summary>
        public static void AddHeaderInfo(HttpWebRequest request, Dictionary<string, string> headerDic)
        {
            if (headerDic != null)
            {
                foreach (var item in headerDic)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// 组装请求参数
        /// </summary>
        public static string BuildQuery(string url, IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder(url);
            if (parameters != null && parameters.Count > 0)
            {
                postData.Append("?");
                bool hasParam = false;
                foreach (var item in parameters)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(item.Key);
                    postData.Append("=");
                    postData.Append(HttpUtility.UrlEncode(item.Value, Encoding.UTF8));
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        public static string GetIP()
        {
            string result = HttpContext.Current?.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current?.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current?.Request.UserHostAddress;
            }
            return string.IsNullOrEmpty(result) ? "0.0.0.0" : result;
        }

        /// <summary>
        /// 将对象转换为 x-www-form-urlencoded 格式字符串
        /// </summary>
        private static string ToFormUrlEncodedString(object data)
        {
            var sb = new StringBuilder();
            var properties = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                string name = prop.Name;
                object value = prop.GetValue(data, null);

                if (sb.Length > 0)
                    sb.Append("&");

                sb.Append(Uri.EscapeDataString(name));
                if (value != null)
                {
                    sb.Append("=");
                    sb.Append(Uri.EscapeDataString(value.ToString()));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 请求 API 并返回图片的字节数组（支持 Cookie）
        /// </summary>
        public static byte[] GetImage(string apiUrl, Dictionary<string, string> headerDic = null,
            CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                if (apiUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2
                }

                request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.Accept = "image/*";
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = true;
                request.ServicePoint.Expect100Continue = false;
                request.CookieContainer = cookieContainer ?? new CookieContainer(); // 支持 Cookie

                AddHeaderInfo(request, headerDic);

                using (response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return ms.ToArray();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();
                        throw new Exception("服务器返回错误：" + error, ex);
                    }
                }
                throw;
            }
            finally
            {
                request?.Abort();
                response?.Close();
            }
        }

        /// <summary>
        /// 请求 API 并返回验证码图片的 Base64 编码数据（支持 Cookie）
        /// </summary>
        public static string GetImageBase64(string apiUrl, Dictionary<string, string> headerDic = null,
            CookieContainer cookieContainer = null)
        {
            try
            {
                byte[] imageBytes = GetImage(apiUrl, headerDic, cookieContainer);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取验证码图片的 Base64 数据时出错: {ex.Message}", ex);
            }
        }
        #endregion
    }
}