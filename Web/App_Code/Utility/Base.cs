using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base 的摘要说明
/// </summary>
public class Base
{
    private string _baseToken = string.Empty;
    public string BaseToken
    {
        get
        {
            if (this._baseToken == string.Empty)
            {
                _baseToken = System.Configuration.ConfigurationManager.AppSettings.Get("BaseToken");
            }
            return this._baseToken;
        }
    }
    public Base()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected JsonResult JsonResult(bool code, object data)
    {
        return new JsonResult(code, string.Empty, data);
    }
    protected JsonResult JsonResult(bool code, string msg)
    {
        return new JsonResult(code, msg, null);
    }
    protected JsonResult JsonResult(bool code, string msg, object data)
    {
        return new JsonResult(code, msg, data);
    }
}