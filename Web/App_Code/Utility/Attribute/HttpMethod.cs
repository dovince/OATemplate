using System;
using System.Web;
using ZWL.Common;

/// <summary>
/// HttpMethod 的摘要说明
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

public class HttpMethodAttribute : Attribute
{
    private HttpVerb _action = HttpVerb.GET;
    // 这里可以添加更多属性，比如角色等  
    public HttpVerb Action
    {
        get
        {
            return _action;
        }
        set
        {
            _action = value;
        }
    }

    // 构造函数可以是空的，或者根据需要添加参数  
    public HttpMethodAttribute()
    {
        // 初始化代码（如果需要）  
    }

    // 可以添加一个方法来判断用户是否通过身份验证  
    // 但这通常是在反射调用时由外部逻辑决定的  
    public bool IsAuthorized(HttpContext context)
    {
        // 实现身份验证逻辑  
        return context.Request.HttpMethod == _action.ToString();
    }
}