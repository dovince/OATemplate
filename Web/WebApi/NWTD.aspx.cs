using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ZWL.DBUtility;

public partial class WebApi_NWTD : System.Web.UI.Page
{
    //正确的时候返回的字符串
    private const string Success = "ok";
    private const string Fail = "error";
    protected void Page_Load(object sender, EventArgs e)
    {
        var method = Request["api_method"].ToLower();
        var result = "Not Define " + method;
        try
        {
            switch (method)
            {
                case "getnowworkflow":
                    result = GetNowWorkFlow();
                    break;
				case "getnowworkflowcount":
                    result = GetNowWorkFlowCount();
                    break;
                case "get":
                    result = Get();
                    break;
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        Response.Write(result);
    }

    protected string GetNowWorkFlow()
    {
        var type = Request["type"];
        var id = Request["id"];
		var search = Request["search"];
        if (string.IsNullOrEmpty(type))
        {
            return Fail;
        }

        ZWL.BLL.ERPNWorkToDo MyModel = new ZWL.BLL.ERPNWorkToDo();
        var IDList = "";
        if (type == "JYGL")
        {
            IDList = ZWL.Common.PublicMethod.JingYingFormIDs;
        }
        else if (type == "RSGL")
        {
            IDList = ZWL.Common.PublicMethod.HRFormIDList;
        }
        else if (type == "XMGL")//限制只出现项目管理的单据
        {
            IDList = ZWL.Common.PublicMethod.XiangMuFormIDs;
        }
		
        //每次只取100条数据
		var oneloadnum = 50;
        DataSet ds = null;
		var otherWhere = " and WorkName like '%" + search + "%' ";
        if (string.IsNullOrEmpty(id))
        {
            ds = MyModel.GetListWithTop(
                    "  StateNow='正在办理' and ','+ShenPiUserList+',' like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' and ','+OKUserList+',' not like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' " + otherWhere + " and FormID in(" + IDList +
                    ") order by ID desc", oneloadnum);
        }
        else
        {
            ds = MyModel.GetListWithTop(
                    "  StateNow='正在办理' and ','+ShenPiUserList+',' like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' and ','+OKUserList+',' not like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' " + otherWhere + " and FormID in(" + IDList + ") and ID < " + id + " order by ID desc", oneloadnum);
        }
        if (ds.Tables.Count > 0)
        {
            var result = JsonConvert.SerializeObject(ds);
            return Success + "|" + result;
        }
        else
        {
            return Fail;
        }
    }

    protected string GetNowWorkFlowCount()
    {
        var type = Request["type"];
		var search = Request["search"];
        if (string.IsNullOrEmpty(type))
        {
            return Fail;
        }

        var IDList = "";
        if (type == "JYGL")
        {
            IDList = ZWL.Common.PublicMethod.JingYingFormIDs;
        }
        else if (type == "RSGL")
        {
            IDList = ZWL.Common.PublicMethod.HRFormIDList;
        }
        else if (type == "XMGL")//限制只出现项目管理的单据
        {
            IDList = ZWL.Common.PublicMethod.XiangMuFormIDs;
        }
		
		var otherWhere = " and WorkName like '%" + search + "%' ";
		var count = DbHelperSQL.GetSHSL("Select count(id) from ERPNWorkToDo where StateNow='正在办理' and ','+ShenPiUserList+',' like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' and ','+OKUserList+',' not like '%," +
                    ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' " + otherWhere + " and FormID in(" + IDList +
                    ")");
					
        if (!string.IsNullOrEmpty(count))
        {
            return Success + "|" + count;
        }
        else
        {
            return Fail;
        }
    }

    protected string Get()
    {
        var id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            return Fail;
        }

        ZWL.BLL.ERPNWorkToDo MyModel = new ZWL.BLL.ERPNWorkToDo();
        MyModel.GetModel(Convert.ToInt32(id));
        if (MyModel.ID > 0)
        {
            var result = JsonConvert.SerializeObject(MyModel);
            return Success + "|" + result;
        }
        else
        {
            return Fail;
        }
    }

    protected string Send()
    {
        var touser = Request["touser"];
        var title = Request["title"];
        var contant = Request["contant"];
        var res = ZWL.Common.PublicMethod.SendAjpush(touser, "", contant);
        return res;
    }
}