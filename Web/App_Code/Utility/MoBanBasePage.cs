using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using ZWL.Common;
using System.Xml;
using ZWL.BLL;
using ZWL.DBUtility;

public class MoBanBasePage : BasePage
{
    public ERPProjectRequire G_ERPProjectRequire;
    public List<ERPProjectRequire> G_ChildRequire;
    public DataTable ERPProjectRequireFileDt;
    public DataTable ERPProjectRequireFileDtNoHidden;
    public DataTable ERPProjectRequireWFNDt;
    public DataTable ERPProjectRequireWFNDtWithStart;
    public ZWL.BLL.ERPNForm ERPProjectRequireNForm;
    public List<ZWL.BLL.ERPNWorkFlowNode> ERPProjectRequireNWorkFlowNode;
    public string extString = "";
    public string G_Selectxmbh = ""; //是否需要项目编号选择
    public string G_Selectmainhtbh = ""; //是否需要项目编号选择

    protected override void OnPreInit(EventArgs e)
    {
        G_ChildRequire = new List<ERPProjectRequire>();

        if (!string.IsNullOrEmpty(Request.QueryString["EXT"]))
        {
            extString = Request.QueryString["EXT"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            //进行绑定数据
            ERPProjectRequire erpProjectRequire = new ERPProjectRequire();
            erpProjectRequire.GetModel(Convert.ToInt32(Request.QueryString["ID"].ToString()));
            G_ERPProjectRequire = erpProjectRequire;
            if (erpProjectRequire.ID > 0)
            {
                if (string.IsNullOrEmpty(G_ERPProjectRequire.FormId))
                {
                    G_ERPProjectRequire.FormId = "\"自己填FormId";
                }
                if (string.IsNullOrEmpty(G_ERPProjectRequire.WorkFlowId))
                {
                    G_ERPProjectRequire.WorkFlowId = "\"自己填WorkFlowId";
                }
                var erpProjectRequireField = new ZWL.BLL.ERPProjectRequireField();
                var ds = erpProjectRequireField.GetList(string.Format("ParentId='{0}'", erpProjectRequire.ID));
                var dt = ds.Tables[0];
                ERPProjectRequireFileDt = dt;

                var dsnh = erpProjectRequireField.GetList(string.Format("ParentId='{0}' and IsHidden <>'1'", erpProjectRequire.ID));
                var dtnh = dsnh.Tables[0];
                ERPProjectRequireFileDtNoHidden = dtnh;

                var erpProjectRequireWfn = new ZWL.BLL.ERPProjectRequireWFN();
                var wfds = erpProjectRequireWfn.GetList(string.Format("ParentId='{0}' and NodeAddr <>'开始' ", erpProjectRequire.ID));
                var wfdt = wfds.Tables[0];
                ERPProjectRequireWFNDt = wfdt;

                var wfdswsds = erpProjectRequireWfn.GetList(string.Format("ParentId='{0}'", erpProjectRequire.ID));
                var wfdswsdt = wfdswsds.Tables[0];
                ERPProjectRequireWFNDtWithStart = wfdswsdt;

                try
                {
                    if (!string.IsNullOrEmpty(erpProjectRequire.FormId))
                    {
                        ERPProjectRequireNForm = new ZWL.BLL.ERPNForm(Convert.ToInt32(erpProjectRequire.FormId));
                    }
                }
                catch
                {

                }

                try
                {
                    if (!string.IsNullOrEmpty(erpProjectRequire.WorkFlowId))
                    {
                        var model = new ZWL.BLL.ERPNWorkFlowNode();
                        ERPProjectRequireNWorkFlowNode = model.GetListModel(" WorkFlowID='" + erpProjectRequire.WorkFlowId + "' ");
                    }
                }
                catch
                {

                }

                //子表
                var childdt = erpProjectRequireField.GetList("ChildRequire='" + G_ERPProjectRequire.TableName + "'").Tables[0];
                foreach (DataRow row in childdt.Rows)
                {
                    var tempmodel = new ERPProjectRequire(Convert.ToInt32(row["ParentId"]));
                    if (tempmodel.ID > 0)
                    {
                        G_ChildRequire.Add(tempmodel);
                    }
                }
            }
            if (ERPProjectRequireFileDt != null && ERPProjectRequireFileDt.Rows.Count > 0)
                foreach (DataRow row in ERPProjectRequireFileDt.Rows)
                {
                    var funs = row["Functions"].ToString();
                    //项目选择
                    if (funs.Contains("项目选择"))
                    {
                        G_Selectxmbh = row["NameEN"].ToString();
                    }
                    //主合同选择
                    if (funs.Contains("主合同选择"))
                    {
                        G_Selectmainhtbh = row["NameEN"].ToString();
                    }
                }
        }

        base.OnPreInit(e);
    }
}