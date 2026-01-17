using System;

namespace ZWL.Common
{
    public static class TimeParser
    {
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        public static DateTime? GetFormatTime(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return DateTime.Parse(dt.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else return dt;
        }

        public static DateTime? GetFormatDate(DateTime? dt)
        {
            if (dt.HasValue)
            {
                var tempDate = DateTime.MinValue;
                var result = DateTime.TryParse(dt.Value.ToString(), out tempDate);
                if (result)
                {
                    return DateTime.Parse(dt.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    return tempDate;
                }
            }
            else return dt;
        }
        public static DateTime? GetFormatDate(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                var tempDate = DateTime.MinValue;
                var result = DateTime.TryParse(dt, out tempDate);
                if (result)
                    return GetFormatDate(tempDate);
                else
                    return tempDate;
            }
            else return null;
        }
        public static string GetFormatDateString(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("yyyy-MM-dd");
            }
            else return string.Empty;
        }
        public static string GetFormatDateString(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                var date = new DateTime();
                var t = DateTime.TryParse(dt, out date);
                if (t)
                {
                    return GetFormatDateString(date);
                }
                return string.Empty;
            }
            else return string.Empty;
        }
        public static string GetFormatTimeString(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else return string.Empty;
        }
        public static string GetFormatTimeString(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                return GetFormatTimeString(DateTime.Parse(dt));
            }
            else return string.Empty;
        }
    }
}
