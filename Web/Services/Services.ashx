<%@ WebHandler Language="C#" Class="Services" %>
using System.Web;
using System.Web.SessionState;
using ZWL.Common;
using Newtonsoft.Json;

public class Services : Base, IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        var result = PublicMethod.ToJson(this.Accept(context));
        context.Response.ContentType = ContentType(context);
        context.Response.Write(result);
    }
    private string ContentType(HttpContext context)
    {
        var result = "text/plain";
        var accept = context.Request.Headers["Accept"];
        if (!accept.IsNullOrEmpty() && accept.IndexOf(",") > -1)
        {
            result = accept.Substring(0, accept.IndexOf(","));
            if (accept.Contains("application/x-ms-application"))
            {
                result = "text/html";
            }
        }
        return result;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}