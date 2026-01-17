using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization; // 需要引用 System.Web.Extensions

namespace ZWL.Common
{
    public static class JwtTokenHelper
    {
        /// <summary>
        /// 从 JWT Token 中获取过期时间
        /// </summary>
        /// <param name="jwtToken">JWT Token 字符串</param>
        /// <returns>过期时间（UTC），如果无法解析则返回 null</returns>
        public static DateTime? GetTokenExpirationTime(string jwtToken)
        {
            try
            {
                // 检查 Token 格式是否有效（至少包含两个点）
                string[] parts = jwtToken.Split('.');
                if (parts.Length != 3)
                {
                    return null;
                }

                // 获取并解析 Payload 部分
                string payloadBase64Url = parts[1];
                string payloadJson = Base64UrlDecode(payloadBase64Url);
                Dictionary<string, object> payload = DeserializeJson(payloadJson);

                // 获取过期时间声明（exp 字段）
                if (payload.TryGetValue("exp", out object expValue))
                {
                    // 转换为 Unix 时间戳并计算 UTC 时间
                    if (long.TryParse(expValue.ToString(), out long unixTimestamp))
                    {
                        return UnixTimeStampToDateTime(unixTimestamp);
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 从 JWT Token 中获取所有声明
        /// </summary>
        /// <param name="jwtToken">JWT Token 字符串</param>
        /// <returns>声明字典，如果无法解析则返回空字典</returns>
        public static Dictionary<string, object> GetAllClaims(string jwtToken)
        {
            try
            {
                string[] parts = jwtToken.Split('.');
                if (parts.Length != 3)
                {
                    return new Dictionary<string, object>();
                }

                string payloadBase64Url = parts[1];
                string payloadJson = Base64UrlDecode(payloadBase64Url);
                return DeserializeJson(payloadJson);
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// 验证 JWT Token 格式是否有效
        /// </summary>
        /// <param name="jwtToken">JWT Token 字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsValidTokenFormat(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return false;
            }

            string[] parts = jwtToken.Split('.');
            return parts.Length == 3 &&
                   !string.IsNullOrEmpty(parts[0]) &&
                   !string.IsNullOrEmpty(parts[1]) &&
                   !string.IsNullOrEmpty(parts[2]);
        }

        #region 辅助方法

        /// <summary>
        /// 解码 Base64Url 字符串
        /// </summary>
        private static string Base64UrlDecode(string input)
        {
            string output = input;
            // 62nd char of encoding
            output = output.Replace('-', '+');
            // 63rd char of encoding
            output = output.Replace('_', '/');
            // Pad with trailing '='s
            switch (output.Length % 4)
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new ArgumentException("Illegal base64url string!");
            }
            // Standard base64 decoder
            byte[] converted = Convert.FromBase64String(output);
            return Encoding.UTF8.GetString(converted);
        }

        /// <summary>
        /// 简单的 JSON 反序列化
        /// </summary>
        private static Dictionary<string, object> DeserializeJson(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, object>>(json);
        }

        /// <summary>
        /// 将 Unix 时间戳转换为 DateTime（UTC）
        /// </summary>
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix 时间戳是从 1970-01-01T00:00:00Z 开始的秒数
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long ticks = unixTimeStamp * 10000;
            return dateTime.AddTicks(ticks);
        }

        #endregion
    }
}