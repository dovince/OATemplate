using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PinYinHelper 的摘要说明
/// </summary>
public class PinYinHelper
{
    public static string ConvertToPinYin(string source)
    {
        //return "";
        return NPinyin.Pinyin.GetPinyin(source);
    }

    public static string ConvertToFirstPinYin(string source)
    {
        return source.Aggregate("", (current, c) => current + ConvertToPinYin(c.ToString())[0]);
    }

    /// <summary>
    /// 转拼音并首字母大写
    /// </summary>
    public static string ConvertToPinYinFirstUpper(string source)
    {
        var temp = ConvertToPinYin(source);
        var tempsplit = temp.Split(' ');
        return tempsplit.Where(str => !string.IsNullOrEmpty(str)).Aggregate("", (current, str) => current + (str.Substring(0, 1).ToUpper() + str.Substring(1)));
    }
}