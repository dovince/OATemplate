using FSDZ.Logger;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZWL.BLL;
using ZWL.Common;
using ZWL.DBUtility;

public class BasePage : Page
{
    #region MyRegion
    private int _id;
    private int _formId;
    private int _workFlowId;
    private int _currentNodeId;
    private int _orginID;
    private string _action = string.Empty;
    protected string DefaultPageSize { get { return "20"; } }
    protected string UserName { get { return PublicMethod.GetUserName(); } }
    protected int UserID { get { var uModel = new ZWL.BLL.ERPUser(); return uModel.GetModel("UserName='" + UserName + "'").ID; } }
    protected string Department { get { return PublicMethod.GetDepartment(); } }
    protected string JiaoSe { get { return PublicMethod.GetJiaoSe(); } }
    protected string QuanXian { get { return PublicMethod.GetQuanXian(); } }
    protected int PostBackCount { get { return PublicMethod.GetInto(ViewState["PostBackCount"]); } }
    protected DateTime DefaultTime { get { var defaultime = new DateTime(); PublicMethod.GetDefaultTime(out defaultime); return defaultime; } }
    protected bool IsDebug
    {
        get
        {
            return Util.IsDebug;
        }
    }
    protected string UrlReferrer { get { return ViewState["UrlReferrer"] != null ? ViewState["UrlReferrer"].ToString() : Request.RawUrl; } set { ViewState["UrlReferrer"] = value; } }
    protected string ReturnUrl { get { return ViewState["returnUrl"] != null ? ViewState["returnUrl"].ToString() : ""; } }
    protected virtual string UrlGoBack
    {
        get
        {
            if (Request.UrlReferrer == null && Request.RawUrl == UrlReferrer)
            {
                return "javascript:window.opener=null;window.close();";
            }
            return "javascript:window.location.href='" + PublicMethod.GetRelativePath(UrlReferrer) + "'";
        }
        set
        {
            UrlReferrer = value;
        }
    }
    protected virtual string PiLiangSet
    {
        get; set;
    }
    protected string RawUrl { get { return Request.RawUrl; } }
    protected DateTime Timestamp { get { return Context.Timestamp; } }
    protected virtual string QuanxianValue
    {
        get
        {
            var _qxValue = string.Empty;
            var page = CurrentPageName + ".aspx";
            if (CurrentPageClass != CurrentPageName)
            {
                page = CurrentPageClass + "/" + page;
            }
            var tr = new ZWL.BLL.ERPTreeList();
            var list = tr.GetListModel(string.Format("NavigateUrlStr like '%{0}%'", page));
            if (list.Any())
            {
                var selectedItem = list.FirstOrDefault();
                _qxValue = selectedItem.ValueStr;
            }
            return _qxValue;
        }
    }
    protected int Id
    {
        get
        {
            var id = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(id))
                return _id;
            else
            {
                if (id.ToString().PadLeft(2, '0').Substring(0, 2) == "JM")
                {
                    return PublicMethod.GetInt(PublicMethod.GetDecryptByParam(id));
                }
                return PublicMethod.GetInt(id);
            }
        }
        set
        {
            _id = value;
        }
    }
    protected virtual int FormId
    {
        get
        {
            if (_formId <= 0)
            {
                var formId = Get("FormID");
                if (string.IsNullOrEmpty(formId))
                    return _formId;
                else
                    return PublicMethod.GetInt(formId);
            }
            else
            {
                return _formId;
            }

        }
        set
        {
            _formId = value;
        }
    }
    protected virtual int WorkFlowId
    {
        get
        {
            if (_workFlowId <= 0)
            {
                var workFlowId = Get("WorkFlowID");
                if (string.IsNullOrEmpty(workFlowId))
                    return _workFlowId;
                else
                    return PublicMethod.GetInt(workFlowId);
            }
            else
            {
                return _workFlowId;
            }
        }
        set
        {
            _workFlowId = value;
        }
    }
    protected int CurrentNodeId
    {
        get
        {
            var currentNodeId = Request.QueryString["CurrentNodeId"];
            if (string.IsNullOrEmpty(currentNodeId))
                return _currentNodeId;
            else
                return PublicMethod.GetInt(currentNodeId);
        }
        set
        {
            _currentNodeId = value;
        }
    }
    protected int OrginID
    {
        get
        {
            var OrginID = Request.QueryString["OrginID"];
            if (string.IsNullOrEmpty(OrginID))
                return _orginID;
            else
                return PublicMethod.GetInt(OrginID);
        }
        set
        {
            _orginID = value;
        }
    }
    protected string Action
    {
        get
        {
            var action = PublicMethod.GetQueryString("Action");
            if (string.IsNullOrEmpty(action))
                return _action;
            else
                return action;
        }
        set
        {
            _action = value;
        }
    }

    protected string CurrentPageName
    {
        get
        {
            return this.Page.GetType().BaseType.Name.Split('_').ToList().LastOrDefault();
        }
    }
    protected string CurrentPageClass
    {
        get
        {
            return this.Page.GetType().BaseType.Name.Split('_').ToList().FirstOrDefault();
        }
    }
    protected string CurrentSessionId
    {
        get
        {
            var sessionId = string.Empty;
            var sid = ViewState[CurrentSessionKey];
            if (sid != null)
            {
                sessionId = sid.ToString();
            }
            return sessionId;
        }
    }

    protected string CurrentSessionKey
    {
        get
        {
            return "SessionId_" + CurrentPageName;
        }
    }
    protected string UrlReferBasePath
    {
        get
        {
            var result = "BusinessManage";
            var refer = Request.UrlReferrer;
            if (refer != null && refer.Segments.Length > 2)
            {
                var selected = "";
                var list = refer.Segments;
                for (int i = 0; i < list.Length; i++)
                {
                    var item = list[i];
                    if (item.ToLower().Contains("aspx"))
                    {
                        selected = list[i - 1];
                        break;
                    }
                }
                return string.IsNullOrEmpty(selected) ? result : selected.TrimEnd('/');
            }
            else
                return result;
        }
    }
    protected string BaseUrl
    {
        get
        {
            return Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath)) + Request.ApplicationPath;
        }
    }
    protected string EncryptKey
    {
        get { return ConfigurationManager.AppSettings.Get("EncryptKey"); }
    }
    //protected string RenShiFormIds { get { return PublicMethod.RenShiFormIds; } }
    //protected string JingYingFormIds { get { return PublicMethod.JingYingFormIds; } }
    //protected string XiangMuFormIds { get { return PublicMethod.XiangMuFormIds; } }
    //protected string GongWenFormIds { get { return PublicMethod.GongWenFormIds; } }
    //protected List<int> RenShiFormIdsList { get { return new ZWL.BLL.ERPNForm().GetFormIdListModelByAllSql(RenShiFormIds); } }
    //protected List<int> JingYingFormIdsList { get { return new ZWL.BLL.ERPNForm().GetFormIdListModelByAllSql(JingYingFormIds); } }
    //protected List<int> XiangMuFormIdsList { get { return new ZWL.BLL.ERPNForm().GetFormIdListModelByAllSql(XiangMuFormIds); } }
    //protected List<int> GongWenFormIdsList { get { return new ZWL.BLL.ERPNForm().GetFormIdListModelByAllSql(GongWenFormIds); } }
    #endregion

    protected string Get(string name)
    {
        var r = string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            var t = Request[name];
            if (t != null)
                r = t.ToString();
        }
        return r;
    }
    protected override void OnPreLoad(EventArgs e)
    {
        if (IsPostBack)
        {
            foreach (var key in Request.Form.AllKeys)
            {
                if (string.IsNullOrEmpty(key)) continue;
                var t = this.FindControl(key);
                if (t != null)
                {
                    switch (t.ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            var txt = (System.Web.UI.WebControls.TextBox)this.FindControl(key);
                            var text = txt.Text;
                            txt.Text = text.Trim();
                            break;
                        case "System.Web.UI.WebControls.HiddenField":
                            var hidden = (System.Web.UI.WebControls.HiddenField)this.FindControl(key);
                            var val = hidden.Value;
                            hidden.Value = val.Trim();
                            break;
                    }
                }
            }
            ViewState["PostBackCount"] = PublicMethod.GetInto(ViewState["PostBackCount"]) + 1;
        }
        else
        {
            ViewState["PostBackCount"] = 0;
            if (Request.UrlReferrer != null)
            {
                ViewState["UrlReferrer"] = Request.UrlReferrer.PathAndQuery;
            }
            // Find the hidden field control by ID
            if (!CurrentPageName.IsNullOrEmpty())
            {
                // 根据页面名称生成Session变量的键
                Session[CurrentSessionKey] = Guid.NewGuid().ToString();
                ViewState[CurrentSessionKey] = Session[CurrentSessionKey].ToString();
            }
            var returnUrl = PublicMethod.GetQueryString("returnUrl");
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewState["returnUrl"] = returnUrl;
            }

            var token = Get("token");
            if (!string.IsNullOrEmpty(token))
            {
                var decodeToken = GetDecryptToken(token);
                var cusername = GetDecryptTokenUsername(token);
                var checkToken = string.Empty;
                var uname = string.Empty;
                var upwd = string.Empty;
                var uModel = new ZWL.BLL.ERPUser();
                uModel = uModel.GetModel(string.Format("UserName='{0}'", cusername));
                if (uModel != null)
                {
                    uname = uModel.UserName;
                    upwd = uModel.UserPwd;
                }
                checkToken = GetDecryptToken(GetEncryptToken(uname, PublicMethod.GetMd5(upwd)));
                if (decodeToken == checkToken)
                {
                    var loginUrl = "~/Login_New.aspx";
                    var SqlSTr = "select * from ERPUser where UserName='" + cusername + "'";
                    var MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
                    if (ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                    {
                        System.Web.HttpContext.Current.Session["UserID"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                        System.Web.HttpContext.Current.Session["UserName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                        System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                        System.Web.HttpContext.Current.Session["JiaoSe"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                        System.Web.HttpContext.Current.Session["Department"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                        System.Web.HttpContext.Current.Session["TrueName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                        System.Web.HttpContext.Current.Session["ZhiWei"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                        System.Web.HttpContext.Current.Session["QuanXian"] = ZWL.DBUtility.DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");
                        //写登陆日志
                        WriteLog("用户登陆系统");
                        //写入Cookies
                        ZWL.Common.PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString());
                        ZWL.Common.PublicMethod.WriteCookie("AdminName", "DTcms", System.Web.HttpContext.Current.Session["UserName"].ToString());
                        ZWL.Common.PublicMethod.WriteCookie("AdminPwd", "DTcms", System.Web.HttpContext.Current.Session["Password"].ToString());
                    }
                    else
                        Response.Redirect(loginUrl);
                    if (!PublicMethod.CheckPower(QuanxianValue))
                    {
                        Response.Redirect(loginUrl);
                    }
                }
            }
        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        //var link = new HtmlLink();
        //link.Href = "../CSS/default/easyui.css";
        //link.Attributes.Add("rel", "stylesheet");
        //link.Attributes.Add("type", "text/css");
        //this.Header.Controls.Add(link);
    }
    protected string GetEncryptToken()
    {
        var username = PublicMethod.GetSessionValue("UserName");
        var pwd = PublicMethod.GetSessionValue("Password");
        var result = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(username + "," + PublicMethod.GetMd5(username + "," + pwd + "," + EncryptKey));
        return result;
    }
    protected string GetEncryptToken(string username, string pwd)
    {
        var result = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(username + "," + PublicMethod.GetMd5(username + "," + pwd + "," + EncryptKey));
        return result;
    }
    protected string GetDecryptToken()
    {
        var token = Request["token"];
        return GetDecryptToken(token);
    }
    protected string GetDecryptToken(string token)
    {
        var result = string.Empty;
        var decodeToken = ZWL.Common.DEncrypt.DESEncrypt.Decrypt(token);
        if (!string.IsNullOrEmpty(decodeToken) && decodeToken.Contains(","))
        {
            var list = decodeToken.Split(',');
            var encryptCode = list[1];
            result = encryptCode;
        }
        return result;
    }
    protected string GetDecryptTokenUsername(string token)
    {
        var result = string.Empty;
        var decodeToken = ZWL.Common.DEncrypt.DESEncrypt.Decrypt(token);
        if (!string.IsNullOrEmpty(decodeToken) && decodeToken.Contains(","))
        {
            var list = decodeToken.Split(',');
            var username = list[0];
            result = username;
        }
        return result;
    }
    protected bool WriteLog(string doSomething)
    {
        //写系统日志
        return new ZWL.BLL.ERPRiZhi
        {
            UserName = UserName,
            DoSomething = doSomething,
            IpStr = HttpContext.Current.Request.UserHostAddress.ToString(),
            TimeStr = Timestamp
        }.Add() > 0;
    }
    protected virtual bool CanEditModel(int workId, ref string msg)
    {
        var result = true;
        var work = new ZWL.BLL.ERPNWorkToDo();
        work.GetModel(workId);
        if (!JiaoSe.Contains("超级管理员"))
        {
            if (work.UserName != UserName)
            {
                msg = "只有登记人的本人才能编辑工作申请.";
                return false;
            }
            else
            {
                if (work.JieDianID.HasValue)
                {
                    var node = new ZWL.BLL.ERPNWorkFlowNode();
                    node.GetModel(work.JieDianID.Value);
                    var nextNodes = new List<int>();
                    var nodeList = new ZWL.BLL.ERPNWorkFlowNode().GetListModel("WorkFlowID=" + node.WorkFlowID);
                    if (nodeList.Any())
                    {
                        var firstNode = nodeList.FirstOrDefault(r => r.NodeAddr == "开始");
                        foreach (var item in firstNode.NextNode.Split(','))
                        {
                            var nextNode = PublicMethod.GetInt(item);
                            if (nextNode > 0)
                                nextNodes.Add(nextNode);
                        }
                    }
                    var sort = PublicMethod.GetInt(node.NodeSerils);
                    var list = new List<string> { "已被驳回", "不通过" };
                    if (work.StateNow == "正在办理" && nextNodes.Contains(sort) || list.Contains(work.StateNow))
                    {
                        return result;
                    }
                    else
                    {
                        msg = "只有未开始审批的或者已被驳回的工作才可以继续编辑，请检查工作状态．";
                        return false;
                    }
                }
            }
        }
        return result;
    }

    protected virtual bool CanDeleteModel(int workId, ref string msg)
    {
        var result = true;
        var work = new ZWL.BLL.ERPNWorkToDo();
        work.GetModel(workId);
        if (!JiaoSe.Contains("超级管理员"))
        {
            if (work.UserName != UserName)
            {
                msg = "只有登记人的本人才能删除工作申请.";
                return false;
            }
            else
            {
                if (work.JieDianID.HasValue)
                {
                    var node = new ZWL.BLL.ERPNWorkFlowNode();
                    node.GetModel(work.JieDianID.Value);
                    var nextNodes = new List<int>();
                    var nodeList = new ZWL.BLL.ERPNWorkFlowNode().GetListModel("WorkFlowID=" + node.WorkFlowID);
                    if (nodeList.Any())
                    {
                        var firstNode = nodeList.FirstOrDefault(r => r.NodeAddr == "开始");
                        foreach (var item in firstNode.NextNode.Split(','))
                        {
                            var nextNode = PublicMethod.GetInt(item);
                            if (nextNode > 0)
                                nextNodes.Add(nextNode);
                        }
                    }
                    var sort = PublicMethod.GetInt(node.NodeSerils);
                    var list = new List<string> { "已被驳回", "不通过" };
                    if (work.StateNow == "正在办理" && nextNodes.Contains(sort) || list.Contains(work.StateNow))
                    {
                        return result;
                    }
                    else
                    {
                        msg = "只有未开始审批的工作才可以删除，请检查工作状态．";
                        return false;
                    }
                }
            }
        }
        return result;
    }
    protected virtual bool CanAlterModel(int workId, ref string msg)
    {
        var result = true;
        var work = new ZWL.BLL.ERPNWorkToDo();
        work.GetModel(workId);
        if (!JiaoSe.Contains("超级管理员"))
        {
            if (work.UserName != UserName)
            {
                msg = "只有登记人的本人才能变更工作申请.";
                return false;
            }
            else
            {
                if (work.StateNow != "正常结束")
                {
                    msg = "此审批工作状态为[" + work.StateNow + "],只有状态为[正常结束]的工作才可变更．";
                    return false;
                }
            }

        }
        return result;
    }
    protected virtual bool ValidateDelete(string IDlist, ref string msg)
    {
        var result = true;
        if (!string.IsNullOrEmpty(IDlist))
        {
            foreach (var item in IDlist.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var p = PublicMethod.GetInt(item);
                if (!CanDeleteModel(p, ref msg))
                {
                    return false;
                }
            }
        }

        return result;
    }
    protected void BindSecondNode(DropDownList ddl, int? workFlowId)
    {
        //绑定下一节点
        string[] NextStrList = DbHelperSQL.GetSHSL("select NextNode from ERPNWorkFlowNode where WorkFlowID=" + workFlowId + " and NodeAddr='开始'").Split(',');
        for (int i = 0; i < NextStrList.Length; i++)
        {
            ListItem MyItem = new ListItem();
            MyItem.Value = DbHelperSQL.GetSHSL("select ID from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + workFlowId);
            MyItem.Text = "节点序号：" + NextStrList[i].ToString() + "--节点名称：" + DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + workFlowId);
            if (MyItem.Value.ToString().Length > 0)
            {
                ddl.Items.Add(MyItem);
            }
        }
    }
    protected void BindSelectedNode(DropDownList ddl, int? node)
    {
        BindSelectedNode(ddl, new List<int?> { node });
    }
    protected void BindSelectedNode(DropDownList ddl, IList<int?> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var selected = nodes[i];
            var item = Conv<ZWL.BLL.ERPNWorkFlowNode>.GetModel("select * from ERPNWorkFlowNode where ID='" + selected + "' ");
            var MyItem = new ListItem();
            MyItem.Value = selected.ToString();
            MyItem.Text = "节点序号：" + item.NodeSerils + "--节点名称：" + item.NodeName;
            if (MyItem.Value.ToString().Length > 0)
            {
                ddl.Items.Add(MyItem);
            }
        }
    }
    protected virtual void BindNextNode(DropDownList ddl, int? currentNodeId)
    {
        BindNextNodeForce(ddl, currentNodeId);
    }
    protected virtual void BindNextNodeInProgress(DropDownList ddl, int workId)
    {
        var tModel = new ZWL.BLL.ERPNWorkToDo();
        tModel.GetModel(workId);
        var nModel = new ZWL.BLL.ERPNWorkFlowNode();
        nModel.GetModel(tModel.JieDianID.Value);
        if (nModel.PSType == "全部通过可向下流转")
        {
            var isCompleted = true;
            var list = tModel.ShenPiUserList.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                var item = list[i];
                if (!(tModel.OKUserList + "," + UserName).Contains("," + item))
                {
                    isCompleted = false;
                    break;
                }
            }
            if (isCompleted)
            {
                BindNextNodeForce(ddl, tModel.JieDianID);
            }
            else
            {
                BindSelectedNode(ddl, tModel.JieDianID);
            }
        }
        else
        {
            BindNextNodeForce(ddl, tModel.JieDianID.Value);
        }
    }
    public void BindNextNodeForce(DropDownList ddl, int? currentNodeId)
    {
        var list = GetNextNode(currentNodeId);
        if (list.Any())
        {
            foreach (var item in list)
            {
                if (item.ID > 0 || (string.IsNullOrEmpty(item.NextNode) && list.Count > 1))
                {
                    var MyItem = new ListItem();
                    MyItem.Value = item.ID.ToString();
                    MyItem.Text = "节点序号：" + item.NodeSerils + "--节点名称：" + item.NodeName;
                    if (MyItem.Value.ToString().Length > 0)
                    {
                        ddl.Items.Add(MyItem);
                    }
                }
                else
                {
                    var MyItem = new ListItem() { Value = item.ID.ToString(), Text = item.NodeAddr };
                    if (MyItem.Text == "0") MyItem.Text = "结束";
                    ddl.Items.Add(MyItem);
                }
            }
            try
            {
                ///////////根据条件获取下一审批节点信息                    
                var XJieDianIDStr = CheckConditionStart(ddl.SelectedValue.ToString()).ToString();
                if (XJieDianIDStr == "0")
                {
                    var MyItem = new ListItem() { Value = XJieDianIDStr, Text = "结束" };
                    ddl.Items.Add(MyItem);
                }
                ddl.SelectedValue = XJieDianIDStr;
            }
            catch
            {
            }
        }
    }
    public int CheckConditionStart(string DefaultNodeID)
    {
        ZWL.BLL.ERPNWorkToDo MyModel = new ZWL.BLL.ERPNWorkToDo();
        MyModel.GetModel(Id);

        ZWL.Common.ParseHtml parse = new ZWL.Common.ParseHtml();

        if (MyModel.FormContent != "")
        {
            parse.GetAttListFormHTMLall(MyModel.FormContent);
        }

        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where ID=" + MyModel.JieDianID.ToString()).Split('|');
        for (int i = 0; i < TiaoJianList.Length; i++)
        {
            if (TiaoJianList[i].Trim().Length > 0)
            {
                //string NextIDStr = CheckTiaoJian(TiaoJianList[i].ToString());
                var NextIDStr = "0";
                var TiaoJianStr = TiaoJianList[i].ToString();

                //条件格式如：ABCD_请假天数→大于→10→3        
                string ZiDuanStrEN = TiaoJianStr.Split('_')[0]; //字段名称EN 如：ABCD        
                string ZiDuanStrCN = TiaoJianStr.Split('→')[0].Split('_')[1]; //字段名称CN 如：请假天数        
                string BiJiaoStr = TiaoJianStr.Split('→')[1]; //条件比较  如：大于
                string ZhiStr = TiaoJianStr.Split('→')[2];//比较的值，如： 10
                string JieDianXuHaoStr = TiaoJianStr.Split('→')[3];//跳转的节点序号，如： 3

                string NowValue = "";
                try
                {
                    if (ZiDuanStrEN == "用户角色")
                    {
                        NowValue = DbHelperSQL.GetSHSL("Select top 1 Jiaose from ERPUser Where UserName='" + MyModel.UserName + "'"); ;
                    }
                    else
                    {
                        NowValue = parse.getValue(ZiDuanStrCN).ToString();
                    }
                }
                catch
                { }

                try
                {
                    if (ZhiStr == "审批人用户名")
                    {
                        ZhiStr = UserName;
                    }
                }
                catch
                { }

                if (BiaoJiaoTwoStr(NowValue, ZhiStr, BiJiaoStr) == true)
                {
                    NextIDStr = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 ID from ERPNWorkFlowNode where NodeSerils='" + JieDianXuHaoStr + "' and WorkFlowID=" + MyModel.WorkFlowID.ToString());
                    return PublicMethod.GetInt(NextIDStr);
                }
                else
                {
                    NextIDStr = "0";
                }
                if (NextIDStr != "0")
                {
                    return int.Parse(NextIDStr);
                }
            }
        }
        return int.Parse(DefaultNodeID);
    }
    /// <summary>
    /// 比较两个字符串，返回结果是否正确
    /// </summary>
    /// <param name="Str1"></param>
    /// <param name="Str2"></param>
    /// <param name="BiJiaoTiaoJian"></param>
    /// <param name="LeiXing"></param>
    /// <returns></returns>
    protected bool BiaoJiaoTwoStr(string Str1, string Str2, string BiJiaoTiaoJian)
    {
        try
        {
            double A1 = double.Parse(Str1);
            double A2 = double.Parse(Str2); //大于  大于等于   小于  小于等于   等于   不等于  包含  不包含
            if (BiJiaoTiaoJian == "大于" && A1 > A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "大于等于" && A1 >= A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "小于" && A1 < A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "小于等于" && A1 <= A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "等于" && A1 == A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不等于" && A1 != A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            if (BiJiaoTiaoJian == "等于" && Str1 == Str2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不等于" && Str1 != Str2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
    protected List<ZWL.BLL.ERPNWorkFlowNode> GetNextNode(int? currentNodeId)
    {
        var result = new List<ZWL.BLL.ERPNWorkFlowNode>();
        var node = new ZWL.BLL.ERPNWorkFlowNode();
        node.GetModel(currentNodeId.Value);
        if (!string.IsNullOrEmpty(node.NextNode))
        {
            var list = node.NextNode.Split(',').ToList();
            if (list.Any(r => !string.IsNullOrEmpty(r)))
            {
                var workFlowId = node.WorkFlowID.Value;
                foreach (var item in list)
                {
                    var no = new ZWL.BLL.ERPNWorkFlowNode();
                    no.GetModel("WorkFlowID=" + workFlowId + " and NodeSerils='" + item + "'");
                    result.Add(no);
                }
            }
        }
        else
        {
            if (node.ID > 0)
            {
                var list = node.NextNode.Split(',').ToList();
                if (node.NodeAddr.Contains("结束") && !list.Any(r => !string.IsNullOrEmpty(r)))
                {
                    node.NodeName = node.NodeAddr;
                    node.ID = 0;
                    result.Add(node);
                }
            }
            else
            {
                node.NodeName = "结束";
                node.ID = 0;
                result.Add(node);
            }
        }
        return result;
    }
    protected ZWL.BLL.ERPNWorkFlowNode GetLastNode(int? workflowid)
    {
        if (workflowid.HasValue)
        {
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            node.GetModel(string.Format("WorkFlowID={0} and NodeAddr='结束'", workflowid.Value));
            return node;
        }
        return null;
    }
    protected void SetNodeInfoAndSet(int workFlowId, TextBox txtSPUser, TextBox txtPingShen, TextBox txtMoShi, DropDownList ddlJieDian, ImageButton delFile, ImageButton readFile, ImageButton editFile)
    {
        try
        {
            //根据选择的节点，绑定人员等信息。
            var MyJieDian = new ZWL.BLL.ERPNWorkFlowNode();
            MyJieDian.GetModel(PublicMethod.GetInt(ddlJieDian.SelectedItem.Value));
            txtPingShen.Text = MyJieDian.PSType;
            txtMoShi.Text = MyJieDian.SPType;
            //根据审批模式设置页面
            txtSPUser.Text = GetShenPiUserList(MyJieDian.SPType, MyJieDian.SPDefaultList);

            //当前开始节点是否有查看、编辑、删除按钮？当前按钮属性
            var NowNodeID = DbHelperSQL.GetSHSLInt("select ID from ERPNWorkFlowNode where WorkFlowID=" + workFlowId + " and NodeAddr='开始'");
            var MyJieDianNow = new ZWL.BLL.ERPNWorkFlowNode();
            MyJieDianNow.GetModel(PublicMethod.GetInt(NowNodeID));
            if (MyJieDianNow.IFCanDel == "否")
            {
                delFile.Visible = false;
            }
            if (MyJieDianNow.IFCanView == "否")
            {
                readFile.Visible = false;
            }
            if (MyJieDianNow.IFCanEdit == "否")
            {
                editFile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.ShowAndRedirect(this, "该流程下一节点未定义完整，请配置完整！" + ex.Message, "./NWorkFlow/NWorkToDoSelect.aspx");
        }
    }
    protected string GetLimitDataSqlWhere(string valueStr)
    {
        return PublicMethod.GetLimitDataSqlWhere(valueStr);

    }
    protected string GetLimitDataSqlWhere(int formid, string valueStr)
    {
        return PublicMethod.GetLimitDataSqlWhere(formid, valueStr, string.Empty);
    }
    protected string GetLimitDataSqlWhere(int formid, string valueStr, string colName)
    {
        return PublicMethod.GetLimitDataSqlWhere(formid, valueStr, colName);
    }
    protected ZWL.BLL.ERPNWorkToDo GetWorkToDoModel(int? workid)
    {
        var oWork = new ZWL.BLL.ERPNWorkToDo();
        if (workid.HasValue)
            oWork.GetModel(workid.Value);
        return oWork;
    }
    protected ZWL.BLL.ERPNWorkFlowNode GetSecondNode(int? workFlowId)
    {
        ZWL.BLL.ERPNWorkFlowNode node = null;
        if (workFlowId.HasValue)
        {
            var no = new ZWL.BLL.ERPNWorkFlowNode();
            no.GetModel("WorkFlowID=" + workFlowId + " and NodeAddr='开始'");
            if (!string.IsNullOrEmpty(no.NodeAddr))
            {
                var nextNode = no.NextNode;
                if (nextNode.Contains(","))
                {
                    nextNode = nextNode.Split(',').ToList().FirstOrDefault();
                }
                no.GetModel("WorkFlowID=" + workFlowId + " and NodeSerils='" + nextNode + "'");
                if (!string.IsNullOrEmpty(no.NodeAddr))
                    node = no;
            }
        }

        return node;
    }
    protected ZWL.BLL.ERPNWorkFlowNode GetSecondWorkFlowNode(int id)
    {
        var model = new ZWL.BLL.ERPNWorkFlowNode();
        var list = model.GetListModel("WorkFlowID=" + id);
        if (list.Any(r => r.NodeSerils == "2"))
        {
            return list.FirstOrDefault(r => r.NodeSerils == "2");
        }

        return null;
    }

    protected string GetShenPiUserList(string SPStr, string DefultStr)
    {
        return GetShenPiUserList(0, SPStr, DefultStr);
    }

    /// <summary>
    /// 根据审批模式字符串设置页面显示
    /// </summary>
    /// <param name="SPStr"></param>
    protected string GetShenPiUserList(int workid, string SPStr, string DefultStr)
    {
        var username = string.Empty;
        var Model = new ZWL.BLL.ERPNWorkToDo();
        if (workid > 0)
        {
            Model.GetModel(workid);
        }
        else
        {
            if (UserName == "蔡晓帆" && UserName == DefultStr)
                username = "谢廷忠";
            else
                Model.UserName = UserName;
        }
        if (SPStr == "审批时自由指定")
        {
            username = "";
        }
        else if (SPStr == "从默认审批人中选择")
        {
            if (workid <= 0 && UserName == "蔡晓帆" && UserName == DefultStr)
                username = "谢廷忠";
            else
                username = DefultStr;
        }
        else if (SPStr == "从默认审批人中再选择")
        {
            username = DefultStr;
        }
        else if (SPStr == "从默认审批部门中选择")
        {
            var sql = "select UserName from ERPUser where IfLogin<>'否' ";
            var SqlWhere = "";
            string[] DefultList = DefultStr.Split(',');
            for (int i = 0; i < DefultList.Length; i++)
            {
                if (SqlWhere.Trim().Length > 0)
                {
                    SqlWhere = SqlWhere + " or  " + " ','+Department+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
                else
                {
                    SqlWhere = " ','+Department+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
            }
            if (!string.IsNullOrEmpty(SqlWhere))
            {
                sql += string.Format(" and ({0})", SqlWhere);
            }

            username = DbHelperSQL.GetStringList(sql).Replace("|", ",");
        }
        else if (SPStr == "从默认审批角色中选择")
        {
            var sql = "select UserName from ERPUser where IfLogin<>'否' ";
            string SqlWhere = "";
            string[] DefultList = DefultStr.Split(',');
            for (int i = 0; i < DefultList.Length; i++)
            {
                if (SqlWhere.Trim().Length > 0)
                {
                    SqlWhere = SqlWhere + " or  " + " ','+JiaoSe+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
                else
                {
                    SqlWhere = " ','+JiaoSe+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
            }
            if (!string.IsNullOrEmpty(SqlWhere))
            {
                sql += string.Format(" and ({0})", SqlWhere);
            }
            username = DbHelperSQL.GetStringList(sql).Replace("|", ",");
        }
        else if (SPStr == "自动选择流程发起人")
        {
            username = Model.UserName;
        }
        else if (SPStr == "自动选择本部门主管")
        {
            username = DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName=(select top 1 Department from ERPUser where IfLogin<>'否' and UserName='" + Model.UserName + "')");
        }
        else if (SPStr == "自动选择本部门班子成员")
        {
            var bmodel = new ZWL.BLL.ERPBuMen();
            var list = bmodel.GetListModel("BuMenName in (select Department from ERPUser where UserName='{0}')".FormatWith(Model.UserName));
            if (list != null && list.Any())
            {
                var select = list.FirstOrDefault();
                username = select.LeadingGroup;
                if (!username.Contains(select.ChargeMan))
                    username = select.ChargeMan + "," + username;
            }
        }
        else if (SPStr == "自动选择上级部门主管")
        {
            username = DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where ID=(select top 1 DirID from ERPBuMen where BuMenName=(select top 1 Department from ERPUser where IfLogin<>'否' and UserName='" + Model.UserName + "'))");
        }
        else if (SPStr == "自动选择局分管领导")
        {
            //各分管领导如下，按人事科提供的资料
            var strbu = Department;
            var user = new ZWL.BLL.ERPUser();
            var userlist = user.GetListModel("UserName='" + Model.UserName + "'");
            if (userlist.Any())
            {
                strbu = userlist.FirstOrDefault().Department;
            }
            var bmodel = new ZWL.BLL.ERPBuMen();
            var list = bmodel.GetListModel("BuMenName='{0}'".FormatWith(strbu));
            if (list.Any())
            {
                if (!list.FirstOrDefault().SuperiorMan.IsNullOrEmpty())
                    username = list.FirstOrDefault().SuperiorMan;
            }
            if (username.IsNullOrEmpty())
                username = "谢廷忠";
        }

        return PublicMethod.WorkWeiTuoUserList(username);
    }

    protected bool SendEmail(string content, string title, string fujian, string toUser, string beiyong1, int? formId, int? workFlowId)
    {
        return SendEmail(content, title, fujian, toUser, beiyong1, formId, workFlowId, 0);
    }
    protected bool SendEmail(string content, string title, ZWL.BLL.ERPNWorkToDo m)
    {
        if (m != null)
            return SendEmail(title, title, m.FuJianList, m.ShenPiUserList, m.BeiYong1, m.FormID, m.WorkFlowID, m.ID);
        return false;
    }

    protected bool SendEmail(string content, string title, string fujian, string toUser, string beiyong1, int? formId, int? workFlowId, int worktodoid)
    {
        return Util.SendEmail(content, title, fujian, toUser, beiyong1, formId, workFlowId, worktodoid);
    }
    protected string GetWorkFlowName(int workflowid)
    {
        return DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=" + workflowid);
    }
    protected string GetWorkName(int workflowid)
    {
        return UserName + "--" + GetWorkFlowName(workflowid) + "(" + Timestamp.ToShortDateString() + ")";
    }
    protected string GetSubHetongNo(string htid)
    {
        var result = string.Empty;
        var ht = new ZWL.BLL.ERPHeTong();
        var list = ht.GetListModel("HTID like '" + htid + "%'");
        var serialNo = string.Empty;
        var i = list.Count;
        while (i >= 0)
        {
            serialNo = i.ToString().PadLeft(2, '0');
            var tempNo = htid + "_" + serialNo;
            var htModel = ht.GetModelByWhere("HTID='" + tempNo + "'");
            if (htModel == null)
            {
                result = tempNo;
                break;
            }
            i++;
        }

        return result;
    }
    protected bool WriteWorkLog(int workid, string name, string operation, string action)
    {
        var result = true;
        var work = new ZWL.BLL.ERPNWorkToDo();
        work.GetModel(workid);
        var shenpiyijian = PublicMethod.GetLastShenPiyijian(work.ShenPiYiJian);
        var recordId = work.JieDianID.Value;
        if (name == "WorkFlow" && operation == ZWL.BLL.Opration.Submit.ToString())
        {
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            node.GetModel("WorkFlowID=" + work.WorkFlowID + " and NodeAddr='开始'");
            if (node.ID > 0)
                recordId = node.ID;
        }
        var model = new ZWL.BLL.ERPNWorkToDoLog()
        {
            Name = name,
            UniqueID = PublicMethod.GenerateGuid(),
            ParentID = workid,
            RecordID = recordId,
            Operation = operation,
            Action = action,
            StateNow = work.StateNow,
            ShenPiUserList = work.ShenPiUserList,
            OKUserList = work.OKUserList,
            TimeStamp = Timestamp,
            UserName = UserName,
            Description = shenpiyijian == null ? "" : shenpiyijian.Comment
        };
        result = model.Add() > 0;
        return result;
    }
    protected bool WriteWorkFlowLog(int workid, string operation, string state)
    {
        return WriteWorkLog(workid, "WorkFlow", operation, state);
    }
    protected List<ProcessingViewModel> GetProcessingList(int id)
    {
        var source = new List<ProcessingViewModel>();
        var workLog = new ZWL.BLL.ERPNWorkToDoLog();
        var list = workLog.GetModelList("Name='WorkFlow' and ParentID=" + id);
        if (list.Any())
        {
            var i = 1;
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            foreach (var item in list.OrderByDescending(r => r.TimeStamp))
            {
                node.GetModel(PublicMethod.GetInt(item.RecordID));
                source.Add(new ProcessingViewModel
                {
                    ID = i,
                    NodeName = node.NodeName,
                    Operation = item.Operation,
                    Action = item.Action,
                    Description = item.Description,
                    Timestamp = TimeParser.GetFormatTimeString(item.TimeStamp),
                    UserName = item.UserName,
                });
                i++;
            }
        }
        else
        {
            var work = new ZWL.BLL.ERPNWorkToDo();
            work.GetModel(id);
            var yList = PublicMethod.GetShenPiYiJianList(work.ShenPiYiJian);
            yList.Insert(yList.Count, new ShenPiyijian { ID = yList.Count + 1, UserName = work.UserName, TimeStamp = TimeParser.GetFormatTimeString(work.TimeStr) });
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            var i = 1;
            foreach (var item in yList.OrderByDescending(r => TimeParser.GetFormatTimeString(r.TimeStamp)))
            {
                var action = "";
                if (i == 1)
                    action = GetAction(work.StateNow);
                else if (i != yList.Count)
                    action = "Agree";
                node.GetModel(item.NodeID);
                source.Add(new ProcessingViewModel
                {
                    ID = i,
                    NodeName = item.NodeID > 0 ? node.NodeName : item.NodeName,
                    Operation = i == yList.Count ? "Submit" : "Approved",
                    Action = action,
                    Description = item.Comment,
                    Timestamp = item.TimeStamp,
                    UserName = item.UserName,
                });
                i++;
            }
        }
        return source;
    }
    protected string GetProcessingHtml(int id)
    {
        var html = string.Empty;
        var list = GetProcessingList(id);
        var htmlFormat = @"<tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{4}</td>
                                <td style='{6}'>{2}</td>
                                <td>{5}</td>
                                <td style='width:35%'>
                                    <div align='left' style='max-width: 98%;text-overflow: ellipsis;overflow-x: hidden;overflow-y: scroll; max-height: 60px;' title='{3}'>{3}</div>
                                </td>
                            </tr>";
        var i = list.Count;
        foreach (var item in list)
        {
            var op = EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation);
            switch (op)
            {
                case ZWL.BLL.Opration.Submit:
                    html += string.Format(htmlFormat, i, item.NodeName,
                        EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation)),
                        item.Description, item.UserName, item.Timestamp, "");
                    break;
                case ZWL.BLL.Opration.Approved:
                    if (!string.IsNullOrEmpty(item.Action))
                    {
                        var color = "";
                        var st = EnumHelper.ToEnum<ZWL.BLL.Action>(item.Action);
                        switch (st)
                        {
                            case ZWL.BLL.Action.Agree:
                                color = "color:green";
                                break;
                            case ZWL.BLL.Action.Return:
                                color = "color:blue";
                                break;
                            case ZWL.BLL.Action.Reject:
                                color = "color:red";
                                break;
                        }
                        html += string.Format(htmlFormat, i, item.NodeName,
                EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation))
                + EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Action>(item.Action)),
                item.Description, item.UserName, TimeParser.GetFormatTimeString(item.Timestamp), color);
                    }
                    break;
                default:

                    break;
            }
            i--;
        }
        return html;
    }
    private string GetAction(string stateNow)
    {
        var result = string.Empty;
        if (stateNow == "正在办理"
            || stateNow == "正常结束")
            result = "Agree";
        if (stateNow == "已被驳回")
            result = "Return";
        if (stateNow == "不通过")
            result = "Reject";
        return result;
    }
    protected DateTime GetLateTime(int? nodeid)
    {
        var hours = DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + nodeid);
        return Timestamp.AddHours(double.Parse(hours));
    }
    protected int GetFirstNodeID(int workflowid)
    {
        return DbHelperSQL.GetSHSLInt1("select ID from ERPNWorkFlowNode where WorkFlowID=" + workflowid + " and NodeAddr='开始'");
    }
    protected bool CheCkIfOk(string TongGuoStr, string ShenPiList, string TiaoJianStr)
    {
        if (TiaoJianStr == "一人通过可向下流转")
        {
            return true;
        }
        else
        {
            //判断审批人列表是否全部在通过人列表中
            var ShenPiArry = ShenPiList.Split(',');
            foreach (var item in ShenPiArry)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (!PublicMethod.StrIFIn("," + item + ",", "," + TongGuoStr + ","))
                {
                    //检测到任何一个审批人不在已经通过列表中，则返回false
                    return false;
                }
            }
            return true;
        }
    }
    protected virtual string GetFilledFormContent(int? formId, object infoModel)
    {
        //替换原有表单content中的值
        var fModel = new ZWL.BLL.ERPNForm();
        if (formId.HasValue)
            fModel.GetModel(formId.Value);
        var parser = new ZWL.Common.ParseHtml(fModel.ContentStr);
        var dtype = infoModel.GetType();
        foreach (PropertyInfo onePro in dtype.GetProperties())
        {
            if ((DescriptionAttribute)Attribute.GetCustomAttribute(onePro, typeof(DescriptionAttribute)) == null) //判断是否 描述内容有值
                continue;
            var nameCN = ((DescriptionAttribute)Attribute.GetCustomAttribute(onePro, typeof(DescriptionAttribute))).Description;//获取到描述内的数据
                                                                                                                                //Get value for the column
            var val = string.Empty;
            var dval = onePro.GetValue(infoModel, null);
            if (dval != null) val = dval.ToString();
            if (onePro.PropertyType.Name.StartsWith("String"))
            {

            }
            else if (onePro.PropertyType.FullName.Contains("Decimal") || onePro.PropertyType.FullName.Contains("Double") || onePro.PropertyType.FullName.Contains("Float"))
            {
                val = PublicMethod.FormatMoney(PublicMethod.GetDecimal(val.ToString()));
            }
            else if (onePro.PropertyType.FullName.Contains("DateTime"))
            {
                val = TimeParser.GetFormatDateString(val.ToString());
            }
            else if (onePro.PropertyType.FullName.Contains("Int"))
            {
                val = PublicMethod.GetInto(val).ToString();
            }
            else
            {

            }

            parser.SetInputValue(nameCN, val);
        }
        parser.SetInputValue("申请日期", TimeParser.GetFormatDateString(DateTime.Now));
        return parser.GetOuterHtml();
    }
    protected virtual string GetDescriptionNameByKey(object infoModel, string keyname)
    {
        //替换原有表单content中的值
        var nameCN = string.Empty;
        var dtype = infoModel.GetType();
        foreach (PropertyInfo onePro in dtype.GetProperties())
        {
            if (onePro.Name != keyname) continue;
            if ((DescriptionAttribute)Attribute.GetCustomAttribute(onePro, typeof(DescriptionAttribute)) == null) //判断是否 描述内容有值
                continue;
            nameCN = ((DescriptionAttribute)Attribute.GetCustomAttribute(onePro, typeof(DescriptionAttribute))).Description;//获取到描述内的数据
                                                                                                                            //Get value for the column
            break;
        }
        return nameCN;
    }
    protected string GetEditContentHtml(int workid, string strnewFormContent)
    {
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workid);
        var strxmformcontent = Model.FormContent;
        var strsplit = "<!--ApprovedProcedure-->";
        //如果空表单和项目基本信息表单都不为空时
        if (!string.IsNullOrEmpty(strxmformcontent) && !string.IsNullOrEmpty(strnewFormContent)
            && strnewFormContent.Contains(strsplit) && strxmformcontent.Contains(strsplit))
        {
            //首先截断表单，拼接新表单(用<!--ApprovedProcedure-->分割)用正则表达式分割
            string[] strsplitempty = Regex.Split(strnewFormContent, strsplit, RegexOptions.IgnoreCase);
            string[] strsplithtform = Regex.Split(strxmformcontent, strsplit, RegexOptions.IgnoreCase);
            strnewFormContent = strsplitempty[0].ToString() + strsplit + strsplithtform[1].ToString();
        }
        return strnewFormContent;
    }
    public void AsyncShow(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alertMessage('" + msg + "');", true);
    }
    public void AsyncShowAndRedirect(string msg, string url)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessageAndRedirect", "alertMessageAndRedirect('" + msg + "','" + url + "');", true);
    }
    public void AsyncShowAndForceRedirect(string msg, string url)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "AsyncShowAndForceRedirect", "AsyncShowAndForceRedirect('" + msg + "','" + url + "');", true);
    }
    public void AsyncShowAndParentRedirect(string msg, string url)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessageAndParentRedirect", "alertMessageAndParentRedirect('" + msg + "','" + url + "');", true);
    }
    public void AsyncShowAndBacktolist(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessageAndRedirect", "alertMessage('" + msg + "');gobacktolist();", true);
    }
    protected virtual void DataBindToGridview()
    {

    }
    protected virtual void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        var HdfPageSum = (HiddenField)this.FindControl("HdfPageSum");
        var GVData = (GridView)this.FindControl("GVData");
        var GoPage = (TextBox)this.FindControl("GoPage");
        var TxtPageSize = (TextBox)this.FindControl("TxtPageSize");
        var pageCount = PublicMethod.GetInt(HdfPageSum.Value);
        try
        {
            if (GoPage.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "页码不可以为空！");
            }
            else if (GoPage.Text.Trim().ToString() == "0" || PublicMethod.GetInt(GoPage.Text.Trim().ToString()) > pageCount)
            {
                MessageBox.Show(this, "页码不是一个有效值！");
            }
            else if (GoPage.Text.Trim() != "")
            {
                var PageI = PublicMethod.GetInt(GoPage.Text.Trim()) - 1;
                if (PageI >= 0 && PageI < pageCount)
                {
                    GVData.PageIndex = PageI;
                }
            }

            if (TxtPageSize.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "每页显示行数不可以为空！");
            }
            else if (TxtPageSize.Text.Trim().ToString() == "0")
            {
                MessageBox.Show(this, "每页显示行数不是一个有效值！");
            }
            else if (TxtPageSize.Text.Trim() != "")
            {
                try
                {
                    int MyPageSize = PublicMethod.GetInt(TxtPageSize.Text.ToString().Trim());
                    GVData.PageSize = MyPageSize;
                }
                catch
                {
                    MessageBox.Show(this, "每页显示行数不是一个有效值！");
                }
            }

            DataBindToGridview();
        }
        catch
        {
            DataBindToGridview();
            MessageBox.Show(this, "请输入有效数字！");
        }
    }
    protected virtual void PagerButtonClick(object sender, ImageClickEventArgs e)
    {
        //获得Button的参数值
        var HdfPageSum = (HiddenField)this.FindControl("HdfPageSum");
        var GVData = (GridView)this.FindControl("GVData");
        var GoPage = (TextBox)this.FindControl("GoPage");
        var TxtPageSize = (TextBox)this.FindControl("TxtPageSize");
        var LabCurrentPage = (Label)this.FindControl("LabCurrentPage");
        var pageCount = PublicMethod.GetInt(HdfPageSum.Value);
        var arg = ((ImageButton)sender).CommandName.ToString();
        var currentPage = PublicMethod.GetInt(LabCurrentPage.Text) - 1;
        switch (arg)
        {
            case ("Next"):
                if (currentPage < (pageCount - 1))
                    GVData.PageIndex = currentPage + 1;
                break;
            case ("Pre"):
                if (currentPage > 0)
                    GVData.PageIndex = currentPage - 1;
                break;
            case ("Last"):
                try
                {
                    GVData.PageIndex = (pageCount - 1);
                }
                catch
                {
                    GVData.PageIndex = 0;
                }

                break;
            default:
                //本页值
                GVData.PageIndex = 0;
                break;
        }
        DataBindToGridview();
    }
    protected List<T> GetSubmitDataList<T>(string preName)
    {
        var result = new List<T>();
        var length = PublicMethod.GetInto(Request["rowCount"]);
        if (length > 0)
        {
            var type = typeof(T);
            for (int i = 0; i < length; i++)
            {
                var t = (T)Activator.CreateInstance(typeof(T));
                var properties = type.GetProperties();
                foreach (var item in properties)
                {
                    var name = preName + "_" + item.Name + "_" + (i + 1);
                    object value = null;
                    if (Request.Form.AllKeys.Contains(name))
                    {
                        if (item.PropertyType == typeof(string))
                        {
                            value = Request[name];
                        }
                        else
                        {
                            try
                            {
                                value = Convert.ChangeType(Request[name], System.Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
                            }
                            catch { continue; }
                        }
                        item.SetValue(t, value, null);
                    }
                }
                result.Add(t);
            }
        }
        return result;
    }
    protected virtual string GetDataLimitExtendSqlWhereForChargeMan(int formid)
    {
        return GetDataLimitExtendSqlWhereForChargeMan(formid, string.Empty);
    }
    protected virtual string GetDataLimitExtendSqlWhereForChargeMan(int formid, string colName)
    {
        return PublicMethod.GetDataLimitExtendSqlWhereForChargeMan(formid, colName);
    }
    protected virtual string GetDataLimitExtendSqlWhereForPersonal(int formid)
    {
        return GetDataLimitExtendSqlWhereForPersonal(formid, string.Empty);
    }
    protected virtual string GetDataLimitExtendSqlWhereForPersonal(int formid, string colName)
    {
        return PublicMethod.GetDataLimitExtendSqlWhereForPersonal(formid, colName);
    }
    protected virtual string GetDataLimitExtendSqlWhere(int formid)
    {
        return PublicMethod.GetDataLimitExtendSqlWhere(formid);
    }

    protected virtual string CombineDataLimitExtendSqlWhere(string colName, string sqlWhere1)
    {
        return CombineDataLimitExtendSqlWhere(colName, colName, sqlWhere1);
    }
    protected virtual string CombineDataLimitExtendSqlWhere(string colName1, string colName2, string sqlWhere1)
    {
        var sqlWhere2 = GetDataLimitExtendSqlWhere(colName2);
        return string.Format(" ({0} {1} {2} {3})", colName1, sqlWhere1, (string.IsNullOrEmpty(sqlWhere2) ? string.Empty : "or"), sqlWhere2);
    }
    /// <summary>
    /// GetDataLimitExtendSqlWhere
    /// </summary>
    /// <param name="colName">字段名</param>
    /// <param name="dataType">UserName,Department</param>
    /// <returns></returns>
    protected virtual string GetDataLimitExtendSqlWhere()
    {
        return GetDataLimitExtendSqlWhere(string.Empty);
    }
    /// <summary>
    /// GetDataLimitExtendSqlWhere
    /// </summary>
    /// <param name="colName">字段名</param>
    /// <param name="dataType">UserName,Department</param>
    /// <returns></returns>
    protected virtual string GetDataLimitExtendSqlWhere(string colName)
    {
        return PublicMethod.GetDataLimitExtendSqlWhere(colName);
    }
    protected string AddLog(object entity)
    {
        var rid = "0";
        try
        {
            rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
        }
        catch { }
        return AddLog(entity, PublicMethod.GetInto(rid), "", Timestamp, UserName);
    }
    protected string AddLog(object entity, int id)
    {
        return AddLog(entity, id, "", Timestamp, UserName);
    }
    protected string AddLog(object entity, string guid, FlowOperation operation = FlowOperation.Add)
    {
        var rid = "0";
        try
        {
            rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
        }
        catch { }
        return AddLog(entity, PublicMethod.GetInto(rid), guid, Timestamp, UserName, operation);
    }
    protected string AddLog(object entity, int id, string guid, DateTime timeStamp, string userName, FlowOperation operation = FlowOperation.Add)
    {
        try
        {
            if (string.IsNullOrEmpty(guid))
                guid = Guid.NewGuid().ToString();
            var list = new List<ZWL.BLL.Flow>();
            var table = entity.GetType().Name;
            var rid = "0";
            try
            {
                rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
            }
            catch { }
            foreach (var pro in entity.GetType().GetProperties())
            {
                try
                {
                    switch (pro.PropertyType.Namespace.ToString().ToLower())
                    {
                        case "system":
                            var newval = string.Empty;
                            if (pro.GetValue(entity, null) != null)
                                newval = pro.GetValue(entity, null).ToString();
                            var flow = new ZWL.BLL.Flow()
                            {
                                DataTable = table,
                                CreatedTime = timeStamp,
                                TKey = pro.Name,
                                NewValue = newval,
                                Operation = (int)operation,
                                UserName = userName,
                                LotID = guid,
                                ParentID = id.ToString(),
                                RecordID = rid,
                            };
                            list.Add(flow);
                            break;
                        default:

                            break;
                    }
                }
                catch { }
            }
            if (list.Any())
            {
                foreach (var item in list)
                {
                    if (item != null)
                        item.Add();
                }
            }
        }
        catch (Exception e)
        {
            Logger.Log(e);
        }
        return guid;
    }

    protected IList<ObjectShot> EditShot(object entity)
    {
        var result = new List<ObjectShot>();
        var name = entity.GetType().Name;
        var rid = 0;
        try
        {
            rid = int.Parse(entity.GetType().GetProperty("ID").GetValue(entity, null).ToString());
        }
        catch { }
        var shot = new ObjectShot()
        {
            Name = name,
            ID = rid
        };
        foreach (var pro in entity.GetType().GetProperties())
        {
            try
            {
                switch (pro.PropertyType.Namespace.ToString().ToLower())
                {
                    case "system":
                        var v = "";
                        var va = pro.GetValue(entity, null);
                        if (va != null)
                            v = va.ToString();
                        shot.KeyValue.Add(pro.Name, v);
                        break;
                    default:

                        break;
                }
            }
            catch { }
        }
        result.Add(shot);
        return result;
    }

    protected string EditLog(IList<ObjectShot> shots, object entity)
    {
        var rid = "0";
        try
        {
            rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
        }
        catch { }
        return EditLog(shots, entity, PublicMethod.GetInto(rid), "", Timestamp, UserName);
    }
    protected string EditLog(IList<ObjectShot> shots, object entity, int id)
    {
        return EditLog(shots, entity, id, "", Timestamp, UserName);
    }
    protected string EditLog(IList<ObjectShot> shots, object entity, int id, string guid, DateTime timeStamp, string userName)
    {
        if (string.IsNullOrEmpty(guid))
            guid = Guid.NewGuid().ToString();
        try
        {
            var list = new List<Flow>();
            var table = entity.GetType().Name;
            var rid = "0";
            try
            {
                rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
            }
            catch { }
            var shot = shots.FirstOrDefault(r => r.Name == table && r.ID == int.Parse(rid));
            foreach (var pro in entity.GetType().GetProperties())
            {
                try
                {
                    switch (pro.PropertyType.Namespace.ToString().ToLower())
                    {
                        case "system":
                            var va = pro.GetValue(entity, null);
                            var v = "";
                            if (va != null)
                                v = va.ToString();

                            if (shot != null && shot.KeyValue[pro.Name] != v)
                            {
                                var flow = new Flow()
                                {
                                    DataTable = table,
                                    CreatedTime = timeStamp,
                                    TKey = pro.Name,
                                    NewValue = v,
                                    OldValue = shot.KeyValue[pro.Name],
                                    Operation = (int)FlowOperation.Edit,
                                    UserName = userName,
                                    LotID = guid,
                                    ParentID = id.ToString(),
                                    RecordID = rid,
                                };
                                list.Add(flow);
                            }
                            else if (shot == null)
                            {
                                var flow = new Flow()
                                {
                                    DataTable = table,
                                    CreatedTime = timeStamp,
                                    TKey = pro.Name,
                                    NewValue = v,
                                    Operation = (int)FlowOperation.Edit,
                                    UserName = userName,
                                    LotID = guid,
                                    ParentID = id.ToString(),
                                    RecordID = rid,
                                };
                                list.Add(flow);
                            }
                            break;
                        default:

                            break;
                    }
                }
                catch { }
            }
            if (list.Any())
            {
                foreach (var item in list)
                {
                    if (item != null)
                        item.Add();
                }
            }
        }
        catch (Exception e)
        {
            Logger.Log(e);
        }
        return guid;
    }
    protected string DelLog(object entity)
    {
        var rid = "0";
        try
        {
            rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
        }
        catch { }
        return AddLog(entity, PublicMethod.GetInto(rid), "", Timestamp, UserName, FlowOperation.Delete);
    }
    protected string DelLog(object entity, int id)
    {
        return AddLog(entity, id, "", Timestamp, UserName, FlowOperation.Delete);
    }
    protected void AddLogAsyn(object entity, int id)
    {
        AddLogAsyn(entity, id, "");
    }
    protected void AddLogAsyn(object entity, int id, string guid)
    {
        try
        {
            var thread = new Thread(new ParameterizedThreadStart(AddLogThread));
            thread.Start(new LogParameter { Entity = entity, ID = id, Guid = guid, Timestamp = Timestamp, UserName = UserName });
        }
        catch { }
    }
    private void AddLogThread(object obj)
    {
        var model = obj as LogParameter;
        AddLog(model.Entity, model.ID, model.Guid, model.Timestamp, model.UserName);
    }
    protected void EditLogAsyn(IList<ObjectShot> shots, object entity, int id, string guid)
    {
        try
        {
            var thread = new Thread(new ParameterizedThreadStart(EditLogThread));
            var args = new LogParameter
            {
                Shots = shots,
                Entity = entity,
                ID = id,
                Guid = guid,
                Timestamp = Timestamp,
                UserName = UserName
            };
            thread.Start(args);
        }
        catch { }
    }
    private void EditLogThread(object obj)
    {
        var model = obj as LogParameter;
        EditLog(model.Shots, model.Entity, model.ID, model.Guid, model.Timestamp, model.UserName);
    }
    public class LogParameter
    {
        public IList<ObjectShot> Shots { get; set; }
        public object Entity { get; set; }
        public int ID { get; set; }
        public string Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
    }

    public class ObjectShot
    {
        public ObjectShot()
        {
            KeyValue = new Dictionary<string, string>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> KeyValue { get; set; }
    }

    public class UserLog
    {
        public UserLog()
        {
            LogShots = new List<LogShot>();
        }
        public string UserName { get; set; }
        public DateTime LogTime { get; set; }
        public FlowOperation Operation { get; set; }
        public IList<LogShot> LogShots { get; set; }

        public class LogShot
        {
            public string Key { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
        }
    }
}