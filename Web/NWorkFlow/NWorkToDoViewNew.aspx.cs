using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using ZWL.Common;
using System.Linq;
using System.Collections.Generic;
using ZWL.DBUtility;

public partial class NWorkFlow_NWorkToDoViewNew : BasePage
{
    public string FormTitle = string.Empty;
    public string BaseInfoBody = string.Empty;
    public string ProcessLog = string.Empty;
    public string MessageHtml = string.Empty;
    public string EditModelUrl = string.Empty;
    public string UrlReferBasePath = "NWorkFlow";
    public List<ZWL.BLL.ERPSaveFileName> Files = new List<ZWL.BLL.ERPSaveFileName>();
    public List<ZWL.BLL.ERPSaveFileName> ZWFiles = new List<ZWL.BLL.ERPSaveFileName>();
    protected void Page_Load(object sender, EventArgs e)
    {
        PublicMethod.CheckSession();
        if (!IsPostBack)
        {
            var msg = "";
            if (ValidateLoad(ref msg))
            {
                var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
                if (workId > 0)
                {
                    var Model = new ZWL.BLL.ERPNWorkToDo();
                    Model.GetModel(workId);
                    WorkFlowId = Model.WorkFlowID.Value;
                    FormId = Model.FormID.Value;
                    CurrentNodeId = Model.JieDianID.Value;
                    InitInput(Model);
                    InitEditModel(Model);
                    InitMessage(Model);
                    InitFiles(Model);
                }
            }
            else
            {
                MessageBox.ShowAndBacktolist(this, msg);
            }
        }
    }
    private bool ValidateLoad(ref string msg)
    {
        var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workId);
        if (Model.ID <= 0)
        {
            msg = "表单已删除，无法查看，请确认。";
            return false;
        }
        return true;
    }
    private void InitFiles(ZWL.BLL.ERPNWorkToDo model)
    {
        if (model != null && !string.IsNullOrEmpty(model.FuJianList))
        {
            foreach (var item in model.FuJianList.Split('|'))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var sfile = new ZWL.BLL.ERPSaveFileName();
                sfile = sfile.GetModelByNowName(item);
                if (sfile != null)
                {
                    Files.Add(sfile);
                }
            }
        }
        if (model != null && model.FormID == 49 && !string.IsNullOrEmpty(model.BeiYong2))
        {
            foreach (var item in model.BeiYong2.Split('|'))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var sfile = new ZWL.BLL.ERPSaveFileName();
                sfile = sfile.GetModelByNowName(item);
                if (sfile != null)
                {
                    ZWFiles.Add(sfile);
                }
            }
        }
    }
    private void InitInput(ZWL.BLL.ERPNWorkToDo model)
    {
        if (model != null && model.ID > 0)
        {
            if (PublicMethod.GongWenFormIDsint.Contains(model.FormID.Value))
            {
                UrlReferBasePath = "GongWen";
            }
            else if (PublicMethod.JingYingFormIDsint.Contains(model.FormID.Value))
            {
                UrlReferBasePath = "BusinessManage";
            }
            else if (PublicMethod.XiangMuFormIDsint.Contains(model.FormID.Value))
            {
                UrlReferBasePath = "ProjectManage";
            }
            else if (PublicMethod.HRFormIDsint.Contains(model.FormID.Value))
            {
                UrlReferBasePath = "HR";
            }
            if (model.WorkName.Contains("出差变更审批流程"))
            {
                Response.Redirect("../" + UrlReferBasePath + "/NWorkToDoView.aspx?ID=" + model.ID);
            }
        }
    }
    private void InitMessage(ZWL.BLL.ERPNWorkToDo model)
    {
        if (model.FormID == 60)
        {
            var xmn = DbHelperSQL.GetSHSL("select XMName from ERPXMQQDJ where nworkid = '" + model.ID + "'");
            if (!string.IsNullOrEmpty(xmn))
            {
                MessageHtml = CheckQQSim(xmn);
            }
        }
        if (model.FormID == 56 && model.WorkFlowID == 51 && model.JieDianID == 179)//设计审查时，在质量审核员审核这个节点增加提示信息
        {
            MessageHtml = "注意：审查人与修改人不能为同一人！";
        }
        if (model.FormID == 43 && model.FormContent.Contains("收款") || model.FormID == 39)
        {
            var text = CheckXMCJJCPG(model);
            if (!string.IsNullOrEmpty(text))
            {
                MessageHtml = text;
            }
        }
    }
    private void InitEditModel(ZWL.BLL.ERPNWorkToDo model)
    {
        var win = "window";
        var url = "";
        if (PublicMethod.JingYingFormIDsint.Contains(model.FormID.Value))
        {
            if (model.FormID.ToString() == "60")
            {
                url = "../BusinessManage/XMQQDJ.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else if (model.FormID.ToString() == "39")
            {
                url = "../BusinessManage/TouBiaoProject.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else if (model.FormID.ToString() == "43")
            {
                url = "../BusinessManage/HeTong.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else
            {
                win = "location";
                url = "../BusinessManage/BusinessFormChangeSP.aspx?ID=" + model.ID;
            }
        }
        else if (PublicMethod.XiangMuFormIDsint.Contains(model.FormID.Value))
        {
            if (model.FormID.ToString() == "54")
            {
                url = "../ProjectManage/XiangMu.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else if (model.FormID.ToString() == "108")
            {
                url = "../ProjectManage/ERPGKProjectGWGLForm.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else
            {
                win = "location";
                url = "../ProjectManage/ProjectFormChangeSP.aspx?ID=" + model.ID;
            }
        }
        if (url != "")
        {
            var o = new
            {
                win = win,
                url = url
            };
            EditModelUrl = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(o);// "{'win':'" + win + "','url':'" + url + "'}";
        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("PrintWork.aspx?ID=" + Request.QueryString["ID"].ToString());
    }
    private string GetBaseInfoBodyHtml(string formContent)
    {
        var result = string.Empty;
        var parse = new ZWL.Common.ParseHtml();
        var ds = parse.GetDataSetFormHTML(formContent);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            var list = ds.Tables[0].Rows;
            var first = new Queue();
            var second = new Queue();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var name = item["Name"].ToString();
                var val = item["Value"].ToString();
                var cols = item["Cols"].ToString();
                if (cols == "1")
                    second.Enqueue(item);
                if (cols == "2")
                    first.Enqueue(item);
                if (first.Count == 2)
                {
                    var f = (DataRow)first.Dequeue();
                    var s = (DataRow)first.Dequeue();
                    result += string.Format(TowColsFormat, f["Name"], f["Value"], s["Name"], s["Value"]);
                    if (second.Count > 0)
                    {
                        for (int j = 0; j < second.Count; j++)
                        {
                            var d = (DataRow)second.Dequeue();
                            result += string.Format(OneColsFormat, d["Name"], d["Value"]);
                        }
                    }
                }
                if (first.Count == 1 && (i == list.Count - 1))
                {
                    var d = (DataRow)first.Dequeue();
                    result += string.Format(OneColsFormat, d["Name"], d["Value"]);
                }
                if (second.Count > 0)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        var d = (DataRow)second.Dequeue();
                        result += string.Format(OneColsFormat, d["Name"], d["Value"]);
                    }
                }
            }
        }
        return result;
    }
    #region MyRegion
    public string CheckQQSim(string xmName)
    {
        string result = "";
        var formatStr = @"<div style='color: red;font-size: 20px;text-align: center;font-weight: bold;'>
                          <font>注意，该项目已有相似项目的存在，请在签名前确认项目是否重复</font>
                        </div>
                        <div>
                          <p>相似项目：</p>
                          {0}
                        </div>";
        var linkformat = @"<p><a target='{6}' href='{5}'>{0}，相似项目名：{1}；登记部门：{2}；登记人：{3}; 相似度：{4};</a></p>";
        var items = "";
        var dt1 = DbHelperSQL.GetDataTable("select nworkid,XMName,DJBM,DJR from [ERPXMQQDJ] where XMName like '%" + xmName + "%' and XMName <>'" + xmName + "' order by nworkid desc");
        var i = 1;
        foreach (DataRow dr in dt1.Rows)
        {
            var target = "_self";
            var href = "javascript:void(0);";
            if (Department == "经营管理科")
            {
                target = "_blank";
                href = "../NWorkFlow/NWorkToDoViewNew.aspx?ID=" + PublicMethod.EncryptParam(dr["nworkid"]);
            }
            items += string.Format(linkformat, i, dr["XMName"], dr["DJBM"], dr["DJR"], PublicMethod.GetSimilWidth(xmName, dr["XMName"].ToString()).ToString("f2"), href, target);
            i++;
        }
        var dt2 = DbHelperSQL.GetDataTable("select nworkid,XMName,DJBM,DJR from [ERPXMQQDJ] where XMName not like '%" + xmName + "%' and XMName <>'" + xmName + "' order by nworkid desc");
        foreach (DataRow dr in dt2.Rows)
        {
            var des = dr["XMName"].ToString();
            xmName = xmName.Replace("有限公司", "").Replace("勘察", "").Replace("设计", "").Replace("（", "").Replace("、", "").Replace("）", "").Replace("市", "").Replace("区", "");
            des = des.Replace("有限公司", "").Replace("勘察", "").Replace("设计", "").Replace("（", "").Replace("、", "").Replace("）", "").Replace("市", "").Replace("区", "");
            var temt = PublicMethod.GetSimilWidth(xmName, des);
            if (temt > 0.8)//
            {
                var target = "_self";
                var href = "javascript:void(0);";
                if (Department == "经营管理科")
                {
                    target = "_blank";
                    href = "../NWorkFlow/NWorkToDoViewNew.aspx?ID=" + PublicMethod.EncryptParam(dr["nworkid"]);
                }
                items += string.Format(linkformat, i, dr["XMName"], dr["DJBM"], dr["DJR"], temt.ToString("f2"), href, target);
                i++;
            }
        }
        if (!string.IsNullOrEmpty(items))
        {
            result = string.Format(formatStr, items);
        }
        return result;
    }

    public string CheckXMCJJCPG(ZWL.BLL.ERPNWorkToDo model)
    {
        string result = "";
        var formatStr = @"<div>
                          <p>{0}</p>
                        </div>
                        {1}
                        ";
        var text = "无【项目承接决策评估登记】";
        var table = "";
        var nworkidcol = "NWorkID";
        var rModel = new ZWL.BLL.ERPXMCJJCPG();
        if (model.FormID == 39)
        {
            table = "ERPTouBiao";
        }
        if (model.FormID == 43)
        {
            table = "ERPHeTong";
            nworkidcol = "NWorkToDoID";
        }
        if (!table.IsNullOrEmpty())
        {
            var sqlWhere = @"ID in (
                            select LV1 from BaseTableRelativeColumn where LTable='ERPXMCJJCPG' and LC1='ID' and RTable='{0}' and RC1='ID'
                            and RV1 in (select ID from {0} where {1}={2})
                            )".FormatWith(table, nworkidcol, model.ID);
            var list = Conv<ZWL.BLL.ERPXMCJJCPG>.GetListBySQLWhere(sqlWhere);
            if (list != null && list.Any(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过"))
            {
                rModel = list.Where(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过").FirstOrDefault();
            }
            else
            {
                var relativeNumber = 0;
                if (model.FormID == 39)
                {
                    var sqlWhere1 = @"ID in (
                            select LV1 from BaseTableRelativeColumn where LTable='ERPXMCJJCPG' and LC1='ID' and RTable='ERPXMQQDJ' and RC1='ID'
                            and RV1 in (select ID from ERPXMQQDJ where XMBH IN (select XMQQBH from {0} where {1}={2}))
                            )		".FormatWith(table, nworkidcol, model.ID);
                    list = Conv<ZWL.BLL.ERPXMCJJCPG>.GetListBySQLWhere(sqlWhere1);
                    if (list != null && list.Any(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过"))
                    {
                        rModel = list.Where(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过").FirstOrDefault();
                        var tmodel = new ZWL.BLL.ERPTouBiao();
                        tmodel = tmodel.GetModelByWorkId(model.ID);
                        if (tmodel != null)
                        {
                            relativeNumber = tmodel.ID;
                        }
                    }
                }
                if (model.FormID == 43)
                {
                    var sqlWhere1 = @"ID in (
                            select LV1 from BaseTableRelativeColumn where LTable='ERPXMCJJCPG' and LC1='ID' and RTable='ERPXMQQDJ' and RC1='ID'
                            and RV1 in (select h.ID from {0} h JOIN ERPXMJBXX x on h.XMID=x.XMBH JOIN ERPXMQQDJ q on x.XMQQBH=q.XMBH where {1}={2})
                            )".FormatWith(table, nworkidcol, model.ID);
                    list = Conv<ZWL.BLL.ERPXMCJJCPG>.GetListBySQLWhere(sqlWhere1);
                    if (list != null && list.Any(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过"))
                    {
                        rModel = list.Where(e => e.CurrentWorkToDo.StateNow != "已被驳回" && e.CurrentWorkToDo.StateNow != "不通过").FirstOrDefault(); var hmodel = new ZWL.BLL.ERPHeTong();
                        hmodel.GetModelByWorkId(model.ID);
                        if (hmodel != null)
                        {
                            relativeNumber = hmodel.ID;
                        }
                    }
                }
                if (rModel.ID <= 0)
                {
                    if (model.FormID == 39)
                    {
                        var tmodel = new ZWL.BLL.ERPTouBiao();
                        tmodel = tmodel.GetModelByWorkId(model.ID);
                        if (tmodel != null)
                        {
                            relativeNumber = tmodel.ID;
                            var sqlWhere1 = @"select p.* from ERPXMCJJCPG p JOIN ERPNWorkToDo d on p.NWorkID=d.ID where StateNow in ('正在办理','正常结束') and EXISTS (select TBXMBH from ERPTouBiao h where 
                        TBXMMC like (case when right(p.XMName, 2) = '项目' then left(p.XMName, len(p.XMName) - 2) else p.XMName end)+'%' and TBXMMC='{0}') ".FormatWith(tmodel.TBXMMC);
                            var plist = Conv<ZWL.BLL.ERPXMCJJCPG>.GetList(sqlWhere1);
                            if (plist != null && plist.Any())
                            {
                                rModel = plist.FirstOrDefault();
                            }
                        }
                    }
                    if (model.FormID == 43)
                    {
                        var hmodel = new ZWL.BLL.ERPHeTong();
                        hmodel.GetModelByWorkId(model.ID);
                        if (hmodel != null)
                        {
                            relativeNumber = hmodel.ID;
                            var sqlWhere1 = @"select p.* from ERPXMCJJCPG p JOIN ERPNWorkToDo d on p.NWorkID=d.ID where StateNow in ('正在办理','正常结束') and EXISTS (select HTID from ERPHeTong h where 
                        HTName like (case when right(p.XMName, 2) = '项目' then left(p.XMName, len(p.XMName) - 2) else p.XMName end)+'%' and HTName='{0}')".FormatWith(hmodel.HTName);
                            var plist = Conv<ZWL.BLL.ERPXMCJJCPG>.GetList(sqlWhere1);
                            if (plist != null && plist.Any())
                            {
                                rModel = plist.FirstOrDefault();
                            }
                        }
                    }
                }
                if (rModel.ID > 0)
                {
                    var checkpgSQL = @"select * from BaseTableRelativeColumn where LTable='ERPXMCJJCPG' and LC1='ID' and LV1='{0}' and RTable='{1}' and RC1='ID' and RV1='{2}'".FormatWith(rModel.ID, table, relativeNumber);
                    var plist = Conv<ZWL.BLL.BaseTableRelativeColumn>.GetList(checkpgSQL);
                    if (plist == null && !plist.Any())
                    {
                        new ZWL.BLL.BaseTableRelativeColumn()
                        {
                            LTable = "ERPXMCJJCPG",
                            LC1 = "ID",
                            LV1 = rModel.ID.ToString(),
                            RTable = table,
                            RC1 = "ID",
                            RV1 = relativeNumber.ToString(),
                        }.Add();
                    }
                }
            }
            if (rModel.ID > 0)
            {
                var linkformat = @"<a onclick={1}javascript:showxmpgpop('{0}');{1} href={1}javascript:void(0);{1} style={1}color: blue;text-decoration: underline;{1}>{2}</a>".FormatWith(PublicMethod.EncryptParam(rModel.NWorkID), "\"", rModel.XMBH);
                text = "项目承接决策评估登记:{2},流程状态[{0}],审批[{1}]。".FormatWith(rModel.CurrentWorkToDo.StateNow, (rModel.CurrentWorkToDo.StateNow == "正常结束" ? "已完成" : "未完成"), linkformat);
            }
        }
        var js = @"<script type='text/javascript'>
                            function showxmpgpop(workid){
                                var options = {
                                    title: '查看工作',
                                    url: '../NWorkFlow/NWorkToDoViewNew.aspx?ID=' + workid + '&_=' + Math.random(),
                                    width: 800,
                                    height: 500
                                }
                                showPopwindow('', options);
                            }
                        </script>";
        result = string.Format(formatStr, text, js);
        return result;
    }
    #endregion
    private readonly string TowColsFormat = @"<tr>
                                    <td class='td_normal_title' width='15%'>{0}</td>
                                    <td width='35%'><div id='_xform_docSubject' _xform_type='text'>{1}</div></td>
                                    <td class='td_normal_title' width='15%'>{2}</td>
                                    <td width='35%'><div id='_xform_fdDeptId' _xform_type='address'>{3}</div></td>
                                </tr>";
    private readonly string OneColsFormat = @"<tr>
                                                        <td class='td_normal_title' width='15%'>{0}</td>
                                                        <td colspan='5' width='83.0%'>
                                                            <div id='_xform_fdRiskContent' _xform_type='textarea'>{1}</div>
                                                        </td>
                                                    </tr>";
}
