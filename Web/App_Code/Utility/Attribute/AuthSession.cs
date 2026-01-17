using System;
using System.Web;

/// <summary>
/// AuthSession 的摘要说明
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

public class AuthSessionAttribute : Attribute
{
    // 构造函数可以是空的，或者根据需要添加参数  
    public AuthSessionAttribute()
    {
        // 初始化代码（如果需要）  
    }

    // 可以添加一个方法来判断用户是否通过身份验证  
    // 但这通常是在反射调用时由外部逻辑决定的  
    public bool IsAuthorized(HttpContext context)
    {
        var result = false;
        // 实现身份验证逻辑  
        var session = context.Session;
        if (session != null)
        {
            result = session["UserName"] != null && session["Password"] != null;
        }
        return result;
    }
}