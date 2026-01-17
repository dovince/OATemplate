using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using ZWL.DBUtility;

namespace ZWL.Common
{
    public class JWTUtil
    {
        public static string GenerateJwt(string userId)
        {
            var dt = HttpContext.Current.Timestamp;
            var exp = dt.AddDays(1).Date;
            return GenerateJwt(userId, dt, exp);
        }
        public static string GenerateJwt(string userId, DateTime createdTime, DateTime expiration)
        {
            // 创建Payload  
            var payload = new
            {
                iss = userId,
                iat = createdTime.Ticks,
                exp = ToUnixTimestamp(expiration) // 过期时间戳（秒）  
            };
            return GenerateJwt(payload);
        }
        public static string GenerateJwt(object payload)
        {
            // 创建Header  
            var header = new
            {
                alg = "HS256", // 算法  
                typ = "JWT"   // 类型  
            };

            // 将Header和Payload序列化为JSON字符串，并进行Base64Url编码  
            string encodedHeader = Base64UrlEncode(JsonSerialize(header));
            string encodedPayload = Base64UrlEncode(JsonSerialize(payload));

            // 拼接Header和Payload，用于签名  
            string signingInput = encodedHeader + "." + encodedPayload;

            // 使用HMAC SHA-256生成Signature  
            string signature = HmacSha256(signingInput, PublicMethod.TokenCipherKey);

            // 拼接Header、Payload和Signature，形成完整的JWT  
            return encodedHeader + "." + encodedPayload + "." + signature;
        }

        public static bool ValidateToken(string jwtToken)
        {
            jwtToken = jwtToken.Replace("Bearer ", "");
            string[] parts = jwtToken.Split('.');
            if (parts.Length != 3)
            {
                return false; // JWT格式不正确  
            }

            string encodedHeader = parts[0];
            string encodedPayload = parts[1];
            string signature = parts[2];
            //var t = Base64UrlDecode(encodedHeader);
            //var l = Base64UrlDecode(encodedPayload);
            // 重新计算签名  
            string signingInput = encodedHeader + "." + encodedPayload;
            string expectedSignature = HmacSha256(signingInput, PublicMethod.CipherKey);

            // 验证签名  
            return expectedSignature == signature;
        }
        public static bool ValidateDevice(string jwtToken, string deviceId)
        {
            var result = false;
            jwtToken = jwtToken.Replace("Bearer ", "");
            var item = DbHelperSQL.GetDataRow("select top 1 * from Token where TokenValue='{0}' ".FormatWith(jwtToken));
            if (item != null && !deviceId.IsNullOrEmpty())
            {
                result = item["DeviceId"].ToString() == deviceId;
            }
            return result;
        }

        // 辅助方法：将对象序列化为JSON字符串（这里需要你自己实现或使用Json.NET等库）  
        public static string JsonSerialize(object obj)
        {
            // 注意：这里应该使用Json.NET或其他JSON库来序列化对象  
            // 但为了简化，我们假设已经有一个方法可以将对象转换为JSON字符串  
            // 在实际应用中，你需要自己实现或引入一个JSON库  
            return PublicMethod.ToJson(obj);
        }

        // 辅助方法：Base64Url编码  
        public static string Base64UrlEncode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var encodedData = Convert.ToBase64String(inputBytes);
            // 将Base64的"+"和"/"替换为"-"和"_"，并移除末尾的"="（如果有的话）  
            return encodedData.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }// Base64 URL 解码  
        public static string Base64UrlDecode(string input)
        {
            var inputBytes = Convert.FromBase64String(input
                .Replace('-', '+') // '-' 替换回 '+'  
                .Replace('_', '/') // '_' 替换回 '/'  
                .PadRight(input.Length + (4 - input.Length % 4) % 4, '=')); // 添加缺失的 '='  
            return Encoding.UTF8.GetString(inputBytes);
        }

        // 辅助方法：HMAC SHA-256签名  
        public static string HmacSha256(string input, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hash).Replace('+', '-').Replace('/', '_').TrimEnd('='); // 通常不需要再次进行Base64Url编码，因为JWT签名部分允许使用标准的Base64编码  
            }
        }
        // 辅助方法：将DateTime转换为Unix时间戳（秒）  
        public static long ToUnixTimestamp(DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.Subtract(new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds);
        }
        public static DateTime TimeStampToDateTime(long timeStamp)
        {
            return (new DateTime(1970, 1, 1).ToLocalTime()).AddSeconds(timeStamp);
        }
    }
}
