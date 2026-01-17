<%@ WebHandler Language="C#" Class="ProjectHandler" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ZWL.Common;

public class ProjectHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Function = Convert.ToString(context.Request.Params["funName"]).Trim();
        switch (Function)
        {
            case "GetProjectList":
                GetProjectList(context);
                break;
            case "GetDDLSheng":
                GetDDLXian(context);
                break;
            case "GetDDLXian":
                GetDDLXian(context);
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void GetProjectList(HttpContext context)
    {
        string query = context.Request.Params["q"];
        if (query.Contains(",") && query.Length > 1)
        {
            query = query.Substring(1, query.Length - 1);
        }
        string strsql = "select top 20 ID as value,ProjectName as text from PROJECT where ProjectName like '%" + query + "%'";
        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            var reault = DataTableHelper.ConvertTo<Combobox>(dt);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string myJson = jss.Serialize(reault);
            context.Response.Write(myJson);
        }
        else
        {
            strsql = "select top 20 ID as value,ProjectName as text from PROJECT ";
            ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(strsql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                List<Combobox> reault = DataTableHelper.ConvertTo<Combobox>(dt);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string myJson = jss.Serialize(reault);
                context.Response.Write(myJson);
            }
        }
    }

    private void GetDDLXian(HttpContext context)
    {
        string DDLShi = context.Request.Params["PN"];
        string re = string.Empty;
        List<Combobox> reault = new List<Combobox>();

        string shiareaid = ZWL.DBUtility.DbHelperSQL.GetSHSL("SELECT AreaID FROM ERPArea WHERE AreaName = '" + DDLShi + "'");
        if (DDLShi == "initProvince")
        {
            shiareaid = "0";
        }
        if (!string.IsNullOrEmpty(shiareaid))
        {
            string sql = "SELECT AreaName FROM ERPArea WHERE ParentID = " + shiareaid;
            DataTable table = ZWL.DBUtility.DbHelperSQL.GetDataTable(sql);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    Combobox cb = new Combobox();
                    cb.text = dr["AreaName"].ToString();
                    reault.Add(cb);
                }
            }
        }
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string myJson = jss.Serialize(reault);
        context.Response.Write(myJson);
    }

}

public class Combobox
{
    public string value { get; set; }

    public string text { get; set; }
}