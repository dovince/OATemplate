using System;

using System.Collections.Generic;

using System.Text.RegularExpressions;

using System.Web;

using System.Web.SessionState;

namespace ZWL.Common
{
    public class XSSFilter : IHttpModule
    {

        public void Dispose()
        {

            //try
            //{

            //    this.Dispose();

            //}

            //catch (Exception)
            //{

            //    throw;

            //}

        }

        public void Init(HttpApplication context)
        {

            context.BeginRequest += new EventHandler(context_BeginRequest);//为BeginRequest事件注册方法 \

        }

        private void context_BeginRequest(object sender, EventArgs e)
        {

            HttpApplication application = (HttpApplication)sender;//得到当前应用程序对象

            HttpContext context = application.Context; //得到当前请求的上下文

            string regexsString = @"\'|#|--|chr(34)|chr(0)|([\s\b+()]+(select|update|insert|delete|declare|dbcc|alter|drop|create|backup|else|open|close|confirm|script|use|begin|retun|exists)[\s\b+]*)";

            string url = HttpContext.Current.Request.RawUrl.ToString();

            bool IsValid = true;

            Regex regex = new Regex(regexsString, RegexOptions.IgnoreCase);

            string decodeUrl = HttpUtility.UrlDecode(url);
            if (!(url + decodeUrl).Contains("SmsShow"))
            {
                if (regex.IsMatch(url) || regex.IsMatch(decodeUrl))
                {

                    IsValid = false;

                }
            }
            var formSource = HttpContext.Current.Request.Form;
            foreach (var item in HttpContext.Current.Request.Form.AllKeys)
            {
                var val = formSource.Get(item);
                if (regex.IsMatch(val))
                {
                    IsValid = false;
                    if (!IsValid) break;

                }

            }

            if (!IsValid)
            {

                HttpServerUtility page = context.Server;

                string error = "";//= "发生异常页: " + page.UrlEncode(url) + "<br>";

                error += "异常信息: 非法的请求<br>";

                context.Application["error"] = error;

                page.Transfer("../Errorrequest.aspx");

            }

        }

    }
}
