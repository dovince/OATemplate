using System;
using System.Security.Policy;
using System.Text;
namespace ZWL.Common
{
    /// <summary>
    /// 显示消息提示对话框。
    /// 李瑞宁
    /// 2014.4
    /// </summary>
    public class MessageBox
    {
        private MessageBox()
        {
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowText);
            sb.AppendLine(string.Format("showText('{0}');", msg));
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowTimeout(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowText);
            sb.AppendLine(string.Format("showText('{0}');", msg));
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowAndRedirectTimeout(System.Web.UI.Page page, string msg, string url)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndRedirect);
            sb.AppendLine(string.Format("showTextAndRedirect('{0}','{1}');", msg, url));
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }

        /// <summary>
        /// 显示消息提示对话框，同时取消加载中事件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowAndUnBlock(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowAndGobacktolist(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){gobacktolist();});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowAndReload(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){window.frameElement.src = window.frameElement.src;});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowAndDoSomething(System.Web.UI.Page page, string msg, string scriptblock)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){" + scriptblock + "});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            ShowAndRedirectTimeout(page, msg, url);
        }
        public static void ShowAndForceRedirect(System.Web.UI.Page page, string msg, string url)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){window.location.href='" + url + "'});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }
        public static void ShowAndBacktolist(System.Web.UI.Page page, string msg)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){gobacktolist();});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndParentRedirect(System.Web.UI.Page page, string msg, string url)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CheckEasyUI);
            sb.AppendLine(ShowTextAndDelFunc);
            sb.Append("showTextAndDelFunc('" + msg + "',function(){parent.window.location.href='" + url + "'});");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString(), true);
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script, true);

        }

        /// <summary>
		/// 显示成功消息提示对话框，并进行页面跳转(NewMoa)
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="url">跳转的目标URL</param>
		public static void ShowSuccessAndRedirect_NewMoa(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'>");
            Builder.AppendFormat("TipBox.SucessAndChangeHrefTipBox('{0}','{1}');", msg, url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "moa_success", Builder.ToString());
        }

        /// <summary>
		/// 显示错误消息提示对话框，并进行页面跳转(NewMoa)
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="url">跳转的目标URL</param>
		public static void ShowErrorAndRedirect_NewMoa(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'>");
            Builder.AppendFormat("TipBox.ErrorAndChangeHrefTipBox('{0}','{1}');", msg, url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "moa_error", Builder.ToString());
        }

        /// <summary>
		/// 显示错误消息提示对话框(NewMoa)
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="url">跳转的目标URL</param>
		public static void ShowError_NewMoa(System.Web.UI.Page page, string msg)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'>");
            Builder.AppendFormat("TipBox.ErrorTipBox('{0}');", msg);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "moa_error", Builder.ToString());
        }

        /// <summary>
		/// 显示消息提示对话框，并进行页面跳转(NewMoa)
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="url">跳转的目标URL</param>
		public static void ShowTipAndRedirect_NewMoa(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'>");
            Builder.AppendFormat("TipBox.AlertAndChangeHrefTipBox('{0}','{1}');", msg, url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "moa_alert", Builder.ToString());
        }

        public static string CheckEasyUI
        {
            get
            {
                return @"function checkEasyUI() {
                            var headerHTML = null;
                            for (var i = 0; i < document.childNodes.length; i++) {
                                if (headerHTML == null && document.childNodes[i].tagName == 'HTML') {
                                    for (var j = 0; j < document.childNodes[i].childNodes.length; j++) {
                                        if (document.childNodes[i].childNodes[j].tagName == 'HEAD') {
                                            headerHTML = document.childNodes[i].childNodes[j].innerHTML;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (headerHTML == null) {
                                headerHTML = document.documentElement.innerHTML;
                            }
							if (headerHTML.indexOf('easyui.css') > 0 && headerHTML.indexOf('jquery.easyui') > 0 
                            && (headerHTML.indexOf('jquery-') > 0 || headerHTML.indexOf('jquery.min.js') > 0|| headerHTML.indexOf('jquery.js') > 0)
                            && headerHTML.indexOf('common.js') > 0
                            && typeof(messageShowTimeout)!='undefined'&& typeof(messageShowTimeout)=='function') {
								return true;
							}
							else {
								return false;
							}
						}";
            }
        }
        public static string ShowText
        {
            get
            {
                return @"function showText(msg) {
                            if (checkEasyUI()) {
                                messageShowTimeout({ msg: msg })
                            }
                            else {
                                alert(msg);
                            }
                        }";
            }
        }
        public static string ShowTextAndRedirect
        {
            get
            {
                return @"function showTextAndRedirect(msg, url) {
                            if (checkEasyUI()) {
                                messageShowTimeout({ msg: msg, url: url })
                            }
                            else {
                                alert(msg);
                                window.location.href = url;
                            }
                        }";
            }
        }
        public static string ShowTextAndDelFunc
        {
            get
            {
                return @"function showTextAndDelFunc(msg,func) {
                            if (checkEasyUI()) {
                                messageShowTimeout({ msg: msg, closeFuncEx: func })
                            }
                            else {
                                alert(msg);
                            }
                        }";
            }
        }
    }
}
