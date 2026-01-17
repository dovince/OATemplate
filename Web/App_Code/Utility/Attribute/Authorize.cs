using System;
using System.Web;
using ZWL.Common;

/// <summary>
/// Authorize 的摘要说明
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

public class AuthorizeAttribute : Attribute
{
    private AuthorizeType _type = AuthorizeType.Token;
    // 这里可以添加更多属性，比如角色等  
    public AuthorizeType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }

    // 构造函数可以是空的，或者根据需要添加参数  
    public AuthorizeAttribute()
    {
        // 初始化代码（如果需要）  
    }

    // 可以添加一个方法来判断用户是否通过身份验证  
    // 但这通常是在反射调用时由外部逻辑决定的  
    public bool IsAuthorized(HttpContext context)
    {
        // 实现身份验证逻辑  
        switch (_type)
        {
            case AuthorizeType.Token:
                return CheckToken(context);
        }
        return true; // 或者基于某种条件的false  
    }
    private bool CheckToken(HttpContext context)
    {
        var deviceId = context.Request.Headers["DeviceId"].ToString();
        if (string.IsNullOrEmpty(deviceId))
        {
            return false;
        }
        var token = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        var info = new ZWL.BLL.Token();
        info.GetModel(token);
        if (info.ID <= 0)
        {
            return false;
        }
        if (info.DeviceId != deviceId)
        {
            return false;
        }
        if (info.EnabledMark.HasValue && info.EnabledMark.Value == 1)
        {
            return false;
        }
        return true;
    }
}