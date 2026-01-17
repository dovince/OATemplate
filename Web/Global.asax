<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="ZWL.Common" %>
<%@ Import Namespace="Request.Binder" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        //系统启动时激活ASPose.Slides,相当于破解
        ZWL.Common.ModifyInMemory.ActivateMemoryPatching();
        RequestBinder.RegistDefaultValue<int>(0);
        RequestBinder.RegistDefaultValue<DateTime>(DateTime.Parse("1900/01/01"));
        SchedulerJob.SchedulerAgent.Start();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码
        SchedulerJob.SchedulerAgent.Stop();
    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
        Exception objErr = Server.GetLastError().GetBaseException();

        #region 写日志
        try
        {
            var logpath = Server.MapPath("~/ErrorLog");
            if (!Directory.Exists(logpath))
            {
                Directory.CreateDirectory(logpath);
            }
            var logfilepath = logpath + "/" + DateTime.Now.ToString("yyyy");
            if (!Directory.Exists(logfilepath))
            {
                Directory.CreateDirectory(logfilepath);
            }
            var logfile = logfilepath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string sError2 =
                "========================" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "========================\r\n" +
                "异常页面：" + HttpContext.Current.Request.Url.ToString() + "\r\n" +
                "异常信息：" + objErr.Message + "\r\n" +
                "异常方法：" + objErr.Source + "\r\n" +
                "异常类名：" + objErr.TargetSite + "\r\n" +
                "异常堆栈：" + objErr.StackTrace.ToString() + "\r\n\r\n";

            File.AppendAllText(logfile, sError2);
        }
        catch (Exception)
        {
        }
        #endregion
    }

    //void Session_Start(object sender, EventArgs e) 
    //{
    //    // 在新会话启动时运行的代码

    //}

    //void Session_End(object sender, EventArgs e) 
    //{
    //    // 在会话结束时运行的代码。 
    //    // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
    //    // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
    //    // 或 SQLServer，则不引发该事件。

    //}
    void Application_BeginRequest(object sender, EventArgs e)
    {
        //WriteLog();
        ProcessIp();
        if (Request.RawUrl.Contains(".axd"))
        {
            return;
        }
        if (ConfigurationManager.AppSettings.AllKeys.Contains("GetParamFilter"))
        {
            var getfilter = ConfigurationManager.AppSettings["GetParamFilter"].ToString();
            var arr = getfilter.Split('|');
            if (getfilter != "")
            {
                //处理get
                foreach (string i in Request.QueryString)
                {
                    if (i == null || i.StartsWith("__"))
                        continue;
                    if (!ProcessSqlStr(i, Request.QueryString[i], arr, "get"))
                    {
                        //跳到公安局
                        Response.Redirect("http://gdga.gd.gov.cn/");
                        Response.End();
                    }
                }
            }
        }
        if (ConfigurationManager.AppSettings.AllKeys.Contains("PostParamFilter"))
        {
            var postfilter = ConfigurationManager.AppSettings["PostParamFilter"].ToString();
            var arr = postfilter.Split('|');
            if (postfilter != "")
            {
                //处理post
                foreach (string i in Request.Form)
                {
                    if (i == null || i.StartsWith("__"))
                        continue;
                    if (!ProcessSqlStr(i, Request.Form[i], arr, "post"))
                    {
                        //跳到公安局
                        Response.Redirect("http://gdga.gd.gov.cn/");
                        Response.End();
                    }
                }
            }
        }

    }
    private void WriteLog()
    {
        var username = "";
        if (!Request.Url.ToString().Contains("SetUserActive.aspx"))
        {
            if (Context.Session != null && Context.Session["UserName"] != null)
            {
                username = Context.Session["UserName"].ToString();
            }
            if (string.IsNullOrEmpty(username))
            {
                //检查Cookies
                username = PublicMethod.GetCookie("AdminName", "DTcms"); //解密用户名
            }
            var sthing = new
            {
                Type = 1,
                Url = Request.Url,
                UserAgent = ParseUserAgent(),
                Params = Request.Params.Count > 0 ? PublicMethod.GetCutStr(PublicMethod.UrlDecode(Request.Params.ToString()), 100) : "",
                Form = Request.Form.Count > 0 ? PublicMethod.GetCutStr(PublicMethod.UrlDecode(Request.Form.ToString()), 100) : "",
            };
            new ZWL.BLL.ERPRiZhi
            {
                UserName = username,
                DoSomething = PublicMethod.ToJson(sthing),
                IpStr = Request.UserHostAddress.ToString(),
                TimeStr = Context.Timestamp
            }.Add();
        }

    }

    private void ProcessIp()
    {
        var ip = Request.UserHostAddress.ToString();
        var sql = "SELECT top 1 [IpStr] FROM [ERPRiZhi] where " +
                "([DoSomething] like '%中出现了敏感字符%') " +
                "and [TimeStr] > '2024-08-25' " +
                "and IpStr='" + ip + "'";

        var heiip = ZWL.DBUtility.DbHelperSQL.GetSHSL(sql);
        if (heiip == ip)
        {
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetCookie("DTRememberName");
            MyRiZhi.DoSomething = "ip出现过攻击现象，" + Request.RawUrl;
            MyRiZhi.IpStr = ip;
            MyRiZhi.Add();

            var limittime = Convert.ToDateTime("2024-08-26 00:00");
            if (DateTime.Now > limittime)
            {
                //跳到公安局
                Response.Redirect("http://gdga.gd.gov.cn/");
                Response.End();
            }
        }
    }

    /**/
    /// <summary>
    /// 分析用户请求是否正常
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value">传入用户提交数据</param>
    /// <param name="form"></param>
    /// <returns>返回是否含有SQL注入式攻击代码 </returns>
    private bool ProcessSqlStr(string key, string value, string[] anySqlStr, string type)
    {
        //string SqlStr = "exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare";
        //string SqlStr = "exec |insert |select |delete |update |truncate |char |declare | and |waitfor | union | when ";
        //string[] anySqlStr = { "exec ", "insert ", "select ", "delete ", "update ", "truncate ", "char ", "declare ", " and ", "waitfor ", " union ", " when " };
        try
        {
            if (value != "")
            {
                //string[] anySqlStr = SqlStr.Split('|');
                foreach (string ss in anySqlStr)
                {
                    //if (type.Equals("post") && (key.Equals("HiddenField_ZSMajor_Html") || key.Equals("HiddenField_PTMajor_Html")))
                    //{
                    //    continue;
                    //}
                    if (value.ToLower().IndexOf(ss, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (value.ToLower().IndexOf("用户自定义控件", StringComparison.OrdinalIgnoreCase) <= 0 && value.ToLower().IndexOf("table", StringComparison.OrdinalIgnoreCase) <= 0)
                        {
                            //写系统日志
                            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetCookie("DTRememberName"); ;
                            MyRiZhi.DoSomething = string.Format("请求参数{0}中出现了敏感字符{1}！method:{4},value:{2},url:{3}", key, ss, value, Request.RawUrl, type);
                            MyRiZhi.IpStr = Request.UserHostAddress.ToString();
                            MyRiZhi.Add();

                            Response.Write(string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":发生内部错误，请与管理员联系！<a href='javascript:history.back();'>返回</a>", key, ss, value));
                            //Response.Write(string.Format("警告,你的请求参数{0}中出现了敏感字符{1}！如有误报请与管理员联系！{2}<a href='javascript:history.back();'>返回</a>", key, ss, value));
                            return false;
                        }
                        //Response.Write(string.Format("警告,您的请求中出现了敏感字符{0}！如有误报请与管理员联系！<a href='javascript:history.back();'>返回</a>", ss));
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Response.Write("出错" + e.Message);
            //return false;
            return true;
        }
        return true;
    }
    private string ParseUserAgent()
    {
        string userAgent = Request.UserAgent;

        // 示例：解析浏览器和操作系统  
        // 浏览器名称和版本  
        string browserName = "Unknown Browser";
        string browserVersion = "Unknown Version";
        string os = "Unknown OS";

        // 简单的浏览器检测  
        // 使用正则表达式来检测浏览器和版本  
        Match browserMatch = Regex.Match(userAgent, @"(?<browser>Edge|MSIE|Trident|Firefox|Chrome|Safari|Opera)[\s/]?(?<version>[\d.]+)?");
        if (browserMatch.Success)
        {
            browserName = browserMatch.Groups["browser"].Value;
            browserVersion = browserMatch.Groups["version"].Success ? browserMatch.Groups["version"].Value : "Unknown Version";
        }

        // 简单的操作系统检测  
        if (userAgent.Contains("Windows NT 10.0"))
        {
            os = "Windows 10";
        }
        else if (userAgent.Contains("Windows NT 6.3"))
        {
            os = "Windows 8.1";
        }
        else if (userAgent.Contains("Windows NT 6.2"))
        {
            os = "Windows 8";
        }
        else if (userAgent.Contains("Windows NT 6.1"))
        {
            os = "Windows 7";
        }
        else if (userAgent.Contains("Windows NT 6.0"))
        {
            os = "Windows Vista";
        }
        else if (userAgent.Contains("Windows NT 5.1"))
        {
            os = "Windows XP";
        }
        else if (userAgent.Contains("Mac OS X"))
        {
            os = "Mac OS X";
        }
        else if (userAgent.Contains("Linux"))
        {
            os = "Linux";
        }

        // 输出结果  
        return @"{0} {1} {2}".FormatWith(os, browserName, browserVersion);
    }
</script>
