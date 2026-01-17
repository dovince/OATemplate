using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ZWL.Common;
using ZWL.DBUtility;

public partial class WebApi_Message : System.Web.UI.Page
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
                case "get":
                    result = Get();
                    break;
                case "getlast":
                    result = GetLast();
                    break;
                case "send":
                    result = Send();
                    break;
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        Response.Write(result);
    }

    protected string Get()
    {
        var fromuser = Request["fromuser"];
        if (string.IsNullOrEmpty(fromuser))
        {
            return Fail;
        }
        ZWL.BLL.ERPLanEmail MyLanEmail = new ZWL.BLL.ERPLanEmail();
        //获取后立刻设置为已读
        DbHelperSQL.ExecuteSQL("UPDATE ERPLanEmail Set EmailState='已读' where FromUser ='" + fromuser + "' and ToUser='" +
                               ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and EmailState='未读'");
        var ds = MyLanEmail.GetList("FromUser ='" + fromuser + "' and ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and (EmailState='未读' or EmailState='已读') order by ID desc");
        if (ds.Tables.Count > 0)
        {
            var dt = ds.Tables[0];
            var result = JsonConvert.SerializeObject(ds);
            return Success + "|" + result;
        }
        else
        {
            return Fail;
        }
    }

    protected string GetLast()
    {
        ZWL.BLL.ERPLanEmail MyLanEmail = new ZWL.BLL.ERPLanEmail();
        var strSql =
            string.Format(@"select *,isnull(COUNTFromUser,0) as noreadcount from
                    (select *,(SELECT COUNT(FromUser) FROM [ERPLanEmail] where EmailState='未读' and FromUser=t.FromUser AND ToUser='{0}' group by FromUser) as COUNTFromUser from
                (SELECT *
                  FROM [dbo].[ERPLanEmail]
                where ID in (SELECT Max([ID]) FROM [ERPLanEmail] where ToUser='{0}' AND (EmailState='未读' or EmailState='已读') group by [FromUser])) t) v order by ID desc", ZWL.Common.PublicMethod.GetSessionValue("UserName"));
        //var ds = MyLanEmail.GetList(" ID in (SELECT Max([ID]) FROM [ERPLanEmail] where ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and (EmailState='未读' or EmailState='已读') group by [FromUser]) order by ID desc");
        var ds = DbHelperSQL.Query(strSql.ToString());

        if (ds.Tables.Count > 0)
        {
            var dt = ds.Tables[0];
            var result = JsonConvert.SerializeObject(ds);
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