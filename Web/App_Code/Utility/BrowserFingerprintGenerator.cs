using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// BrowserFingerprintGenerator 的摘要说明
/// </summary>
public class BrowserFingerprintGenerator
{
    private HttpRequest _request;
    public HttpRequest Request
    {
        get
        {
            if (_request == null)
            {
                _request = HttpContext.Current.Request;
            }
            return _request;
        }
    }
    public BrowserFingerprintGenerator()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public void Init(HttpRequest request)
    {
        _request = request;
    }
    // 生成浏览器指纹的公共方法
    public string GenerateFingerprint(Dictionary<string, string> clientProperties)
    {
        // 1. 收集浏览器属性
        var fingerprintData = CollectBrowserAttributes(clientProperties);

        // 2. 规范化数据并生成字符串
        string fingerprintString = NormalizeAndConcat(fingerprintData);

        // 3. 生成哈希指纹
        return ComputeSha256Hash(fingerprintString);
    }

    // 收集浏览器属性（包含服务端和客户端属性）
    private Dictionary<string, string> CollectBrowserAttributes(Dictionary<string, string> clientProps)
    {
        var attributes = new Dictionary<string, string>();

        // 服务端可获取的属性
        attributes.Add("UserAgent", Request.UserAgent ?? "");
        attributes.Add("AcceptLanguage", Request.Headers["Accept-Language"] ?? "");
        attributes.Add("AcceptEncoding", Request.Headers["Accept-Encoding"] ?? "");

        // 需要客户端传递的属性（示例）
        attributes.Add("IPAddress", GetRealIp());

        return attributes;
    }

    // 数据规范化和拼接
    public string NormalizeAndConcat(Dictionary<string, string> data)
    {
        // 对键进行排序以确保一致性
        var keys = new List<string>(data.Keys);
        keys.Sort();

        StringBuilder sb = new StringBuilder();
        foreach (var key in keys)
        {
            // 规范化值：转换为小写并移除多余空格
            string value = data[key].Trim().ToLower();
            sb.AppendFormat("{0}={1};", key, value);
        }
        return sb.ToString();
    }

    // 计算 SHA256 哈希（更安全的替代方案）
    private string ComputeSha256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
    public string GetRealIp()
    {
        string text = null;
        try
        {
            text = ((Request.ServerVariables["HTTP_VIA"] != null) ? Request.ServerVariables["HTTP_X_FORWARDED_FOR"]: Request.UserHostAddress);
            bool flag = text.IndexOf("192.168") == 0 || text.IndexOf("127.0.0.1") == 0 || text.IndexOf("::1") == 0;
            if (flag)
            {
                text = GetInternalIp();
            }
        }
        catch (Exception var_3_8B)
        {
            throw;
        }
        return text;
    }
    private string GetInternalIp()
    {
        string result = "?";
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addressList = hostEntry.AddressList;
        for (int i = 0; i < addressList.Length; i++)
        {
            IPAddress iPAddress = addressList[i];
            bool flag = iPAddress.AddressFamily.ToString() == "InterNetwork" && iPAddress.ToString().IndexOf("192.168") == 0;
            if (flag)
            {
                result = iPAddress.ToString();
                break;
            }
        }
        return result;
    }
}