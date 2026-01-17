using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ZWL.Common
{
    public class EnumHelper
    {
        public static T ToEnum<T>(string strEnum)
        {
            T t = (T)Enum.Parse(typeof(T), strEnum);
            return t;
        }
        public static T ToEnum<T>(int valEnum)
        {
            T t = (T)Enum.Parse(typeof(T), valEnum.ToString());
            return t;
        }
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return en.ToString();
        }
    }
    public enum AptitudeType
    {
        [Description("正本")]
        Original = 1,
        [Description("副本")]
        Carbon = 2,
        [Description("正本复印件")]
        OriginalCopy = 3,
        [Description("副本复印件")]
        CarbonCopy = 4,
    }
    public enum FileType
    {
        OriginalCopy = 1,
        PhotoCopy = 2,
    }
    public enum AptitudeState
    {
        Returned = 0,
        Using = 1,
    }
    public enum Active
    {
        Inactive = 0,
        Active = 1,
    }
}
