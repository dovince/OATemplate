using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class NWorkFlow_DoWorkNew : BasePage
{
    public string JieDianName = string.Empty;
    public string EditModelUrl = string.Empty;
    public string MessageHtml = string.Empty;
    public bool IsYiqing = false;//疫情期间省外出差 特殊标记
    public List<ZWL.BLL.ERPSaveFileName> Files = new List<ZWL.BLL.ERPSaveFileName>();
    protected void Page_Load(object sender, EventArgs e)
    {
        PublicMethod.CheckSession();
        if (!IsPostBack)
        {
            var msg = "";
            if (ValidateLoad(ref msg))
            {
                LoadInput();
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
            msg = "登记人已删除此表单，无需审批。";
            return false;
        }
        return true;
    }
    private void LoadInput()
    {
        var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var sql = string.Format(@"select * from (select ContentStr from ERPShenPi where ContentStr<>'' 
                        and (UserName='admin' or  UserName='{0}') group by ContentStr) t order by ContentStr desc", UserName);
        DbHelperSQL.BindDropDownList2(sql, this.commonUsages, "ContentStr", "ContentStr");
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workId);

        WorkFlowId = Model.WorkFlowID.Value;
        FormId = Model.FormID.Value;
        CurrentNodeId = Model.JieDianID.Value;
        JieDianName = Model.JieDianName;
        MD5.Value = Model.MD5();
        var formModel = new ZWL.BLL.ERPNForm();
        formModel.GetModel(FormId);
        var workflowModel = new ZWL.BLL.ERPNWorkFlow();
        workflowModel.GetModel(WorkFlowId);
        //解析表单的数据
        Hidden_form.Value = GetBaseInfoBodyHtml(Model.FormContent);
        //lblProcessLog.Text = WorkLogHelper.GetProcessingHtml(Model.ID, false);
        fdFlowDescription.InnerText = workflowModel.WorkFlowName + (workflowModel.BackInfo != ""
            && workflowModel.BackInfo != workflowModel.WorkFlowName ? "(" + workflowModel.BackInfo + ")" : "");
        SelectedNextNode.Items.Clear();
        BindNextNode(SelectedNextNode, Model.JieDianID);
        LoadNextNodeInput(workId, PublicMethod.GetInt(SelectedNextNode.SelectedValue));
        LoadNextNodeExtend(workId, PublicMethod.GetInt(SelectedNextNode.SelectedValue));
        InitCurrentNodeSignInput(workId);
        InitInput(Model);
        InitMessage(Model);
        InitFiles(Model);
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
        if (model.WorkName.Contains("盖章") && model.JieDianName.Contains("经营"))
        {
            var kModel = new ZWL.BLL.ERPKeyValue();
            kModel = kModel.GetModel("Category='GaiZhangXMBlackList'");
            if (kModel != null)
            {
                var list = kModel.Value1.Split(',');
                if (list.Any())
                {
                    var gModel = Conv<ZWL.BLL.ERPGaizhang>.GetModel("select * from ERPGaizhang where NWorkID=" + model.ID);
                    if (gModel != null && gModel.Type == "项目编号")
                    {
                        if (list.Contains(gModel.code))
                        {
                            MessageHtml = "注意：此项目已暂被列入黑名单，请联系经营科负责人确认！";
                        }
                    }
                }
            }
        }
    }
    private void InitInput(ZWL.BLL.ERPNWorkToDo model)
    {
        var win = "location"; //window
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
                url = "../BusinessManage/BusinessFormChangeSP.aspx?ID=" + model.ID;
            }
        }
        else if (PublicMethod.XiangMuFormIDsint.Contains(model.FormID.Value))
        {
            if (model.FormID.ToString() == "54")
            {
                url = "../ProjectManage/XiangMu.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
                if (model.JieDianName == "部门负责人")
                {
                    ShenPiUserIcon.Visible = false;
                    ShenPiUser.Enabled = false;
                }
            }
            else if (model.FormID.ToString() == "108")
            {
                url = "../ProjectManage/ERPGKProjectGWGLForm.aspx?Action=Edit&ID=" + PublicMethod.EncryptParam(model.ID);
            }
            else
            {
                url = "../ProjectManage/ProjectFormChangeSP.aspx?ID=" + model.ID;
            }
        }
        else if (PublicMethod.GongWenFormIDsint.Contains(model.FormID.Value))
        {
            if (model.FormID.ToString() == "49")
            {
                url = "../GongWen/FaWenSPModify.aspx?FromDoWork=1&FormID=49&WorkFlowID=43&ID=" + model.ID;
            }
            else
            {
                url = "../BusinessManage/BusinessFormChangeSP.aspx?ID=" + model.ID;
            }
        }
        else if (PublicMethod.HRFormIDsint.Contains(model.FormID.Value))
        {
            if (model.FormID == 78)
            {
                url = "../HR/ChuChai.aspx?Action=Edit&ID=" + model.ID;
            }
            else if (model.FormID == 50)
            {
                //url = "../HR/QingJia.aspx?Action=Edit&ID=" + model.ID;
                url = "../BusinessManage/BusinessFormChangeSP.aspx?ID=" + model.ID;
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
    private string GetNodeFeatureShenpiUser(int workid, string shenpiuserlist = "")
    {
        var result = string.Empty;
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workid);

        string strkpfs = "", strskzh = "";

        var list = new List<int> { 60, 39, 43 };
        var kModel = new ZWL.BLL.ERPKeyValue();
        var xmuModel = kModel.GetModel("Category='XMUtilityReviewerForm'");
        if (Model.FormID == 46 && Model.JieDianName == "部门负责人审核")//合同收款审批
        {
            var parse = new ParseHtml();
            if (Model.FormContent != "")
            {
                parse.GetAttListFormHTMLall(Model.FormContent);
                strkpfs = parse.getValue("开票方式");
                strskzh = parse.getValue("收款账户");
            }
            if (!string.IsNullOrEmpty(strkpfs))
            {
                if (!string.IsNullOrEmpty(strskzh))
                {
                    switch (strskzh)
                    {
                        case "广东省佛山地质局":
                        case "广东省地质局佛山地质调查中心":
                            result = "郑晓波";
                            break;
                        case "广东佛山地质工程勘察院":
                            result = "马苏玲";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        else if (Model.FormID == 109)
        {
            var gModel = new ZWL.BLL.ERPGKProjectGWGL();
            gModel.GetModelByNWorkId(PublicMethod.GetInt(Model.BeiYong2));
            if (Model.JieDianName == "生产经营单位主要负责人签名")
            {
                result = gModel.XiangMuFuZeRen;
            }
            else if (Model.JieDianName == "项目负责人签名")
            {
                result = gModel.GongDiAnQuanYuan;
            }
            else if (Model.JieDianName == "工地安全员签名")
            {
                result = gModel.DiZhiBianLuYuan;
            }
        }
        else if (Model.FormID == 114)//项目成果报告-勘察子项目
        {
            var nextNodeList = GetNextNode(Model.JieDianID);
            var nextNode = nextNodeList.FirstOrDefault();
            if (nextNode != null)
            {
                if (nextNode.NodeName == "编写人修改")
                {
                    var parser = new ZWL.Common.ParseHtml();
                    parser.GetAttListFormHTMLinput(Model.FormContent);
                    var bxr = parser.getValue("编写人");
                    if (!bxr.IsNullOrEmpty())
                    {
                        result = bxr;
                    }
                }
                else
                {
                    var xmModel = new ZWL.BLL.ERPXMJBXX();
                    xmModel.GetModelByXMBH(Model.Number);
                    var nextNodeName = nextNode.NodeName;
                    if (nextNode.NodeName == "复核审核人")
                    {
                        nextNodeName = "审核人审核成果报告";
                    }
                    var kanItem = kModel.GetModel("Category='XMCGUtilityReviewer' and Key1='{0}' and Key2='{1}' and Key3='{2}'".FormatWith(xmModel.XMBM, Model.BeiYong2, nextNodeName));
                    if (kanItem != null && !kanItem.Value1.IsNullOrEmpty())
                    {
                        result = kanItem.Value1;
                    }
                }
            }
        }
        else if (list.Contains(Model.FormID.Value) && Model.JieDianName.Contains("部门负责人"))
        {
            decimal amount = 0;
            if (Model.FormID == 39)
            {
                var tmodel = new ZWL.BLL.ERPTouBiao();
                tmodel = tmodel.GetModelByWorkId(Model.ID);
                if (tmodel != null) amount = tmodel.TBBJ;
            }
            else if (Model.FormID == 43)
            {
                var hmodel = new ZWL.BLL.ERPHeTong();
                hmodel.GetModelByWorkId(Model.ID);
                if (hmodel.ID > 0) amount = hmodel.HTJE;
            }
            else if (Model.FormID == 60)
            {
                var qmodel = new ZWL.BLL.ERPXMQQDJ();
                qmodel = qmodel.GetModelByWorkId(Model.ID);
                if (qmodel != null) amount = PublicMethod.GetDecimal(qmodel.YJXMZJ);
            }
            if (amount >= 30 * 10000 && !shenpiuserlist.IsNullOrEmpty())
            {
                result = shenpiuserlist.Split(',')[0];
                var kvModel = new ZWL.BLL.ERPKeyValue();
                kvModel = kvModel.GetModel("Category='JingYingKeHeTongApprovalExceedsAgents'");
                if (kvModel != null && !kvModel.Value1.IsNullOrEmpty())
                    result = kvModel.Value1;
            }
        }
        else if (Model.FormID == 113 && Model.JieDianName.Contains("部门班子成员"))
        {
            decimal amount = 0;
            var pmodel = new ZWL.BLL.ERPXMCJJCPG();
            pmodel = pmodel.GetModelByNWorkId<ZWL.BLL.ERPXMCJJCPG>(Model.ID);
            if (pmodel != null) amount = pmodel.Amount;
            if (amount > 30 * 10000 && !shenpiuserlist.IsNullOrEmpty())
            {
                result = shenpiuserlist.Split(',')[0];
                var kvModel = new ZWL.BLL.ERPKeyValue();
                kvModel = kvModel.GetModel("Category='JingYingKeHeTongApprovalExceedsAgents'");
                if (kvModel != null && !kvModel.Value1.IsNullOrEmpty())
                    result = kvModel.Value1;
            }
        }
        else if (xmuModel != null && xmuModel.ID > 0 && xmuModel.Value1.Split(',').Select<string, int>(x => Convert.ToInt32(x)).ToList().Contains(Model.FormID.Value))
        {
            var tshenpiuserlist = Util.GetXMUtilityReviewer(workid);
            if (!tshenpiuserlist.IsNullOrEmpty() || (Model.FormID == 71 && !Model.JieDianName.Contains("局（院）审定人审定成果报告")))
                result = tshenpiuserlist;
            else
                result = shenpiuserlist;
        }
        return result;
    }
    protected override void BindNextNode(DropDownList ddl, int? currentNodeId)
    {
        var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        base.BindNextNodeInProgress(ddl, workId);
        var node = new ZWL.BLL.ERPNWorkFlowNode();
        node.GetModel(currentNodeId.Value);
        if (node.NodeAddr != "结束")
        {
            var Model = new ZWL.BLL.ERPNWorkToDo();
            Model.GetModel(workId);
            AutoNextNodeExtend(Model);
        }
    }
    private void LoadNextNodeExtend(int workId, int selectedNodeId)
    {
        var model = new ZWL.BLL.ERPNWorkToDo();
        model.GetModel(workId);
        if (model.FormID == 71)
        {
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            node.GetModel(selectedNodeId);
            if (node.NodeName != null && node.NodeName.Contains("局（院）审定人审定成果报告"))
            {
                CHKMOB.Checked = false;
            }
        }
    }
    private void InitCurrentNodeSignInput(int workId)
    {
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workId);
        var signimgid = string.Empty;
        Util.FormInputCanWriteSet(Model.ID, ref signimgid);
        Hidden_SignInput.Value = signimgid;
    }
    protected void btnSavePass_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (ValidatePass(ref msg))
        {
            try
            {
                var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
                var workToDo = new ZWL.BLL.ERPNWorkToDo();
                workToDo.GetModel(workid);

                var FileNameStr = PublicMethod.UploadFileIntoDir(ShenPiFuJian, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ShenPiFuJian.PostedFile.FileName));
                var fileLink = string.IsNullOrEmpty(FileNameStr) ? "" : "<br>审批附件：<a href=../UploadFile/" + FileNameStr + ">[右键下载]</a>";
                var PiShiStr = "<font color=#0000FF>" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">" +
                    fdUsageContent.Text + "</div>" + fileLink + "<hr>";

                workToDo.FormContent = Hidden_form.Value;
                workToDo.OKUserList += "," + UserName;
                workToDo.ShenPiYiJian = PiShiStr + workToDo.ShenPiYiJian;
                workToDo.Update();
                WorkLogHelper.WriteWorkFlowLog(workToDo.ID, ZWL.BLL.Opration.Approved.ToString(), ZWL.BLL.Action.Agree.ToString(), Hidden_SignImg.Value);
                workToDo.GetModel(workid);
                if (CheckIFOk(workid))
                {
                    DoSomething(workToDo);
                    BanLiWork();
                }

                SetEmailReaded(workToDo);
                WriteLog("用户办理工作(" + workToDo.WorkName + ")");
                //修改审批后返回的页面，根据郑晓波的要求，返回到查看信息的页面方便直接打印收款表（2015.12.2-sj）
                if (workToDo.FormID == 46 && UserName == "马苏玲")
                {
                    MessageBox.ShowAndRedirect(this, "工作办理成功！", "../NWorkFlow/NWorkToDoViewNew.aspx?ID=" + workid);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "工作办理成功!", UrlReferrer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "网络错误!" + ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    protected bool CheckIFOk(int workid)
    {
        var workToDo = new ZWL.BLL.ERPNWorkToDo();
        workToDo.GetModel(workid);
        var nModel = workToDo.CurrentNode();
        return CheCkIfOk(workToDo.OKUserList, workToDo.ShenPiUserList, nModel.PSType);
    }

    protected void btnSaveEnd_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (ValidatePass(ref msg))
        {
            try
            {
                var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
                var workToDo = new ZWL.BLL.ERPNWorkToDo();
                workToDo.GetModel(workid);

                var FileNameStr = PublicMethod.UploadFileIntoDir(ShenPiFuJian, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ShenPiFuJian.PostedFile.FileName));
                var fileLink = string.IsNullOrEmpty(FileNameStr) ? "" : "<br>审批附件：<a href=../UploadFile/" + FileNameStr + ">[右键下载]</a>";
                var PiShiStr = "<font color=#0000FF>" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">" +
                    fdUsageContent.Text + "</div>" + fileLink + "<hr>";

                DbHelperSQL.ExecuteSQL("update ERPNWorkToDo set FuJianList='" + FileNameStr + "',ShenPiYiJian='" + PiShiStr + "'+ShenPiYiJian,StateNow='正常结束',ShenPiUserList='工作已办结',JieDianName='结束' where ID=" + workid);

                ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
                Mymodel.GetModel(workid);
                Mymodel.ID = workid;
                Mymodel.FormContent = this.Hidden_form.Value;

                //发邮件通知发文拟稿人

                var Content = "您的工作已经通过审批！(" + workToDo.WorkName + ")";
                Util.SendEmail(Content, Content, workToDo);

                Mymodel.UpdateBD();

                WorkLogHelper.WriteWorkFlowLog(Mymodel.ID, ZWL.BLL.Opration.Approved.ToString(), ZWL.BLL.Action.Agree.ToString(), Hidden_SignImg.Value);

                //写系统日志
                WriteLog("用户审批工作信息(" + workToDo.WorkName + ")");
                MessageBox.ShowAndRedirect(this, "工作办理成功!", UrlReferrer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "网络错误!" + ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (ValidateNotPass(ref msg))
        {
            var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
            var WorkToDo = new ZWL.BLL.ERPNWorkToDo();
            WorkToDo.GetModel(workid);

            var FileNameStr = PublicMethod.UploadFileIntoDir(ShenPiFuJian, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ShenPiFuJian.PostedFile.FileName));
            var fileLink = string.IsNullOrEmpty(FileNameStr) ? "" : "<br>审批附件：<a href=../UploadFile/" + FileNameStr + ">[右键下载]</a>";
            var PiShiStr = "<font color=#0000FF>" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">" +
                fdUsageContent.Text + "</div>" + fileLink + "<hr>";

            WorkToDo.ShenPiYiJian = PiShiStr + WorkToDo.ShenPiYiJian;
            WorkToDo.StateNow = "已被驳回";
            WorkToDo.Update();
            SetReturnTodoExtend(WorkToDo);
            WorkLogHelper.WriteWorkFlowLog(WorkToDo.ID, ZWL.BLL.Opration.Approved.ToString(), ZWL.BLL.Action.Return.ToString(), Hidden_SignImg.Value);

            SetEmailReaded(WorkToDo);

            //发邮件通知发文拟稿人
            var title = "您的工作已经被驳回！(" + WorkToDo.WorkName + ")";
            SendEmail(title, title, WorkToDo.FuJianList, WorkToDo.UserName,
                WorkToDo.BeiYong1, WorkToDo.FormID, WorkToDo.WorkFlowID, WorkToDo.ID);

            //发送驳回短信
            Mobile.SendSMS("系统消息", WorkToDo.UserName, title);

            //写系统日志
            WriteLog("用户审批工作信息(" + WorkToDo.WorkName + ")");

            MessageBox.ShowAndRedirect(this, "工作办理成功!", UrlReferrer);
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    //驳回后的工作处理
    private void SetReturnTodoExtend(ZWL.BLL.ERPNWorkToDo WorkToDo)
    {
        if (WorkToDo != null && WorkToDo.FormID > 0)
        {
            if (WorkToDo.FormID == 60)//项目前期信息登记流程
            {
                var model = new ZWL.BLL.ERPNWorkToDo();
                model.GetModel(WorkToDo.ID);
                var node = new ZWL.BLL.ERPNWorkFlowNode();
                var nodes = node.GetListModel("WorkFlowID=" + WorkToDo.WorkFlowID);
                var firstItem = nodes.FirstOrDefault(r => r.NodeAddr == "开始");
                var nextNode = new ZWL.BLL.ERPNWorkFlowNode();
                nextNode = nodes.FirstOrDefault(r => r.NodeSerils == firstItem.NextNode);
                model.JieDianID = nextNode.ID;
                model.JieDianName = nextNode.NodeName;
                model.ShenPiUserList = GetShenPiUserList(WorkToDo.ID, nextNode.SPType, nextNode.SPDefaultList);
                model.Update();
            }
            else if (WorkToDo.FormID == 47)
            {
                var detail = new ZWL.BLL.ERPHeTongJieYueDetail();
                var list = detail.GetListModelByWorkId(Id);
                foreach (var item in list)
                {
                    var hModel = item.CurrentHeTong;
                    hModel.HTJYState = "可借阅";
                    hModel.Update();
                    item.LendDate = null;
                    item.Update();
                }
            }
            else if (WorkToDo.FormID == 90)//办公用品需求计划
            {
                var officeModel = new ZWL.BLL.ERPOfficeSupply().GetModelBySqlWhere(" NWorkID ='" + WorkToDo.ID + "'");
                var shots = EditShot(officeModel);
                officeModel.State = "0";
                officeModel.Update();
                EditLog(shots, officeModel);
                var subItems = officeModel.SubItems.Where(x => !x.DeleteMark.HasValue || x.DeleteMark != 1);
                foreach (var item in subItems)
                {
                    var tshots = EditShot(item);
                    item.State = "0";
                    item.Update();
                    EditLog(tshots, item);
                }
            }
            else if (WorkToDo.FormID == 116)//办公用品批量采购
            {
                var officeModel = new ZWL.BLL.ERPOfficeSupply().GetModelBySqlWhere(" NWorkID ='" + WorkToDo.ID + "'");
                var shots = EditShot(officeModel);
                officeModel.State = "0";
                officeModel.Update();
                EditLog(shots, officeModel);
                var subItems = officeModel.SubItems.Where(x => !x.DeleteMark.HasValue || x.DeleteMark != 1);
                foreach (var item in subItems)
                {
                    var tshots = EditShot(item);
                    item.State = "0";
                    item.Update();
                    EditLog(tshots, item);

                    var citem = new ZWL.BLL.ERPOfficeSupplyDetail();
                    citem.GetModel(PublicMethod.GetInt(item.ReservedField3));
                    var cshots = EditShot(citem);
                    citem.State = "1";
                    citem.Update();
                    EditLog(cshots, citem);
                }
            }
            else if (WorkToDo.FormID == 117)//办公用品领用
            {
                var officeModel = new ZWL.BLL.ERPOfficeSupply().GetModelBySqlWhere(" NWorkID ='" + WorkToDo.ID + "'");
                var shots = EditShot(officeModel);
                officeModel.State = "1";
                officeModel.Update();
                EditLog(shots, officeModel);
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (ValidateNotPass(ref msg))
        {
            var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
            var WorkToDo = new ZWL.BLL.ERPNWorkToDo();
            WorkToDo.GetModel(workid);

            var FileNameStr = PublicMethod.UploadFileIntoDir(ShenPiFuJian, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ShenPiFuJian.PostedFile.FileName));
            var fileLink = string.IsNullOrEmpty(FileNameStr) ? "" : "<br>审批附件：<a href=../UploadFile/" + FileNameStr + ">[右键下载]</a>";
            var PiShiStr = "<font color=#0000FF>" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">" +
                fdUsageContent.Text + "</div>" + fileLink + "<hr>";

            WorkToDo.ShenPiYiJian = PiShiStr + WorkToDo.ShenPiYiJian;
            WorkToDo.StateNow = "不通过";
            WorkToDo.ShenPiUserList = "工作已办结";
            WorkToDo.JieDianName = "结束";
            //WorkToDo.FormContent = WorkToDo.FormContent;
            WorkToDo.Update();

            WorkLogHelper.WriteWorkFlowLog(WorkToDo.ID, ZWL.BLL.Opration.Approved.ToString(), ZWL.BLL.Action.Reject.ToString(), Hidden_SignImg.Value);

            SetEmailReaded(WorkToDo);

            SetReturnTodoExtend(WorkToDo);
            //发邮件通知发文拟稿人
            var title = "您的工作没有通过审批！(" + WorkToDo.WorkName + ")";
            SendEmail(title, title, WorkToDo.FuJianList, WorkToDo.UserName,
                WorkToDo.BeiYong1, WorkToDo.FormID, WorkToDo.WorkFlowID, WorkToDo.ID);

            //发送驳回短信
            Mobile.SendSMS("系统消息", WorkToDo.UserName, title);

            //写系统日志
            WriteLog("用户审批工作信息(" + WorkToDo.WorkName + ")");

            MessageBox.ShowAndRedirect(this, "工作办理成功!", UrlReferrer);
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    protected bool ValidateNotPass(ref string msg)
    {
        if (string.IsNullOrEmpty(fdUsageContent.Text))
        {
            msg = "请在审批意见处说明理由.";
            return false;
        }
        if (!CheckJieDianIsOver(ref msg))
        {
            return false;
        }
        return true;
    }
    protected bool ValidatePass(ref string msg)
    {
        var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var workToDo = new ZWL.BLL.ERPNWorkToDo();
        workToDo.GetModel(workid);

        #region 公文验证
        if (workToDo.FormID == 90 && workToDo.JieDianName == "办公室确认金额")
        {
            //var erpOfficeSupplySummary = new ZWL.BLL.ERPOfficeSupplySummary();
            //erpOfficeSupplySummary = erpOfficeSupplySummary.GetModelBySqlWhere(string.Format(" NWorkID='{0}' ", workToDo.ID));
            //var detail = new ZWL.BLL.ERPOfficeSupplyDetail();
            //var dtlist = detail.GetListBySqlWhere(string.Format(" ID in (SELECT [SupplyDetailID] FROM [ERPOfficeSupplySummaryDetail] where [SupplySummaryID]='{0}') order by ID desc", erpOfficeSupplySummary.ID));
            //var realprice = Request["Text813640769"];
            //var name = Request["Text1147953348"];
            //var oneprice = Request["Text813640768"];
            //var num = Request["Text813640767"];
            //var realpricesplit = realprice.Split(',');
            //var onepricesplit = oneprice.Split(',');
            //var numsplit = num.Split(',');
            //var namesplit = name.Split(',');

            //if (dtlist.Count != realpricesplit.Count())
            //{
            //    msg = "数量有误，请重新确认！";
            //    return false;
            //}
            //for (int i = 0; i < dtlist.Count; i++)
            //{
            //    double test = 0;
            //    var dt = dtlist[i];
            //    var rp = realpricesplit[i];
            //    var ns = namesplit[i];
            //    if (!double.TryParse(rp, out test))
            //    {
            //        msg = "金额有误，实际金额请输入数字！";
            //        return false;
            //    }
            //    if (ns != dt.Name)
            //    {
            //        msg = string.Format("名称有误，[{0}]与[{1}]不相符！", dt.Name, ns);
            //        return false;
            //    }
            //}
        }
        #endregion

        #region 经营验证
        if (workToDo.FormID == 43 && workToDo.JieDianName == "合同归档" && workToDo.WorkName.Contains("合同签订评审"))
        {
            //合同签订评审表单的经营管理科审核节点，点击保存并通过后提取合同信息保存到对应数据库表中
            ParseHtml parse = new ParseHtml();
            if (workToDo.FormContent != "")
            {
                parse.GetAttListFormHTMLall(Hidden_form.Value);
                var strhtzt = parse.getValue("合同状态");
                var strhtgd = parse.getValue("合同归档");
                var gdtime = parse.getValue("归档日期");
                if (strhtzt != "中止")
                {
                    if (strhtgd.EndsWith("未归档"))
                    {
                        msg = "合同未归档，不能通过合同归档节点！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(gdtime))
                    {
                        msg = "合同归档时间必选，否则不能通过合同归档节点！";
                        return false;
                    }
                }
            }
        }
        //if ((workToDo.FormID == 43 || workToDo.FormID == 39) && workToDo.JieDianName == "经营科负责人审核")
        //{
        //    if (workToDo.FormID == 43)
        //    {
        //        var hmodel = new ZWL.BLL.ERPHeTong();
        //        hmodel.GetModelByWorkId(workid);
        //        if (hmodel.HTLB == "收款")
        //        {
        //            if (!CheckXMCJJCPGPassed(workid, ref msg))
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (!CheckXMCJJCPGPassed(workid, ref msg))
        //        {
        //            return false;
        //        }
        //    }

        //}
        #endregion

        #region 项目验证
        var currentNode = workToDo.CurrentNode();
        if (workToDo.FormID == 71 && currentNode != null && currentNode.NodeAddr.Contains("结束"))
        {
            //var result = ValidateXMChengguoInput(workToDo.FormContent, ref msg);
            //if (!result)
            //{
            //return false;
            //}
        }
        if (workToDo.FormID == 56 && workToDo.WorkFlowID == 51 && currentNode.ID == 179)
        {
            if (ShenPiUser.Text == UserName)//设计审查时，在质量审核员审核这个节点增加验证，当前节点时审查，下一节点的修改不能再选择自己
            {
                msg = "审查人与修改人不能为同一人！";
                return false;
            }
        }
        #endregion

        if (!CheckJieDianIsOver(ref msg))
        {
            return false;
        }
        var cnode = workToDo.CurrentNode();
        if (cnode.ID > 0 && cnode.PSType == "全部通过可向下流转")
        {
            if (MD5.Value != workToDo.MD5())
            {
                msg = "当前表单处于多人协同处理状态，系统发现此表单有新的审批意见，请刷新后再审核。";
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// 根据不同的表单处理不同的事项
    /// </summary>
    /// <param name="workToDo"></param>
    public void DoSomething(ZWL.BLL.ERPNWorkToDo workToDo)
    {
        var formId = workToDo.FormID.Value;
        if (PublicMethod.GongWenFormIDsint.Contains(formId))
        {
            GongWenHander(workToDo);
        }
        else if (PublicMethod.JingYingFormIDsint.Contains(formId))
        {
            JingYingHander(workToDo);
        }
        else if (PublicMethod.XiangMuFormIDsint.Contains(formId))
        {
            XMHander(workToDo);
        }
        else if (PublicMethod.HRFormIDsint.Contains(formId))
        {
            HRHander(workToDo);
        }
    }

    /// <summary>
    /// 根据Model找到对应的邮件，并设置为已读
    /// </summary>
    public void SetEmailReaded(ZWL.BLL.ERPNWorkToDo Model)
    {
        //根据Model找到对应的邮件，并设置为已读
        string strworkname = Model.WorkName;
        DateTime emiletime = (DateTime)Model.TimeStr;
        string stremileid = DbHelperSQL.GetSHSL("select ID FROM ERPLanEmail where EmailTitle like '%" + strworkname + "%' and ToUser='" + UserName + "' and datediff(minute,TimeStr,'" + emiletime + "') < 1");
        if (stremileid != "")
        {
            ZWL.BLL.ERPLanEmail MyLanEmail = new ZWL.BLL.ERPLanEmail();
            MyLanEmail.GetModel(PublicMethod.GetInt(stremileid));
            //设置为已读
            if (MyLanEmail.ToUser.Trim() == UserName.Trim())
            {
                if (MyLanEmail.EmailState == "未读")
                {
                    DbHelperSQL.ExecuteSQL("update ERPLanEmail set EmailState='已读' where ID=" + stremileid.Trim());
                }
            }
        }
    }

    protected void SelectedNextNode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        LoadNextNodeInput(workid, PublicMethod.GetInt(SelectedNextNode.SelectedItem.Value.ToString()));
    }
    private void LoadNextNodeInput(int workid, int nodeid)
    {
        try
        {
            //根据选择的节点，绑定人员等信息。
            if (nodeid == 0)
            {
                PingshenMoshi.Text = "默认";
                ShenpiMoshi.Text = "默认";
                ShenPiUser.Text = "默认";
            }
            else
            {
                var tModel = new ZWL.BLL.ERPNWorkToDo();
                tModel.GetModel(workid);
                var nModel = new ZWL.BLL.ERPNWorkFlowNode();
                nModel.GetModel(tModel.JieDianID.Value);
                var shenpiuser = string.Empty;
                if (nModel.PSType == "全部通过可向下流转")
                {
                    if (!string.IsNullOrEmpty(tModel.ShenPiUserList))
                        foreach (var item in tModel.ShenPiUserList.Split(','))
                        {
                            if (!tModel.OKUserList.Contains(item) && item != UserName)
                            {
                                shenpiuser += (string.IsNullOrEmpty(shenpiuser) ? "" : ",") + item;
                            }
                        }
                    if (shenpiuser.IsNullOrEmpty() && tModel.JieDianID.Value != nodeid)
                    {
                        nModel.GetModel(nodeid);
                        shenpiuser = GetShenPiUserList(workid, nModel.SPType, nModel.SPDefaultList);
                        var fuser = GetNodeFeatureShenpiUser(workid, shenpiuser);
                        if (!string.IsNullOrEmpty(fuser))
                            shenpiuser = PublicMethod.WorkWeiTuoUserList(fuser);
                    }
                }
                else
                {
                    nModel.GetModel(nodeid);
                    shenpiuser = GetShenPiUserList(workid, nModel.SPType, nModel.SPDefaultList);
                    var fuser = GetNodeFeatureShenpiUser(workid, shenpiuser);
                    if (!string.IsNullOrEmpty(fuser))
                        shenpiuser = PublicMethod.WorkWeiTuoUserList(fuser);
                }
                ShenPiUser.Text = shenpiuser;
                PingshenMoshi.Text = nModel.PSType;
                ShenpiMoshi.Text = nModel.SPType;
            }
        }
        catch
        { }
    }

    private string GetBaseInfoBodyHtml(string formContent)
    {
        var result = string.Empty;
        var parse = new ParseHtml();
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

    #region 工作处理部分
    /// <summary>
    /// 正常结束的节点处理事件
    /// </summary>
    public void DoEnd()
    {
        var msg = string.Empty;
        if (!CheckJieDianIsOver(ref msg)) { MessageBox.Show(this, msg); }
        var WorkToDoID = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        ZWL.BLL.ERPNWorkToDo workToDo = new ZWL.BLL.ERPNWorkToDo();
        workToDo.GetModel(WorkToDoID);
        workToDo.FormContent = Hidden_form.Value;
        workToDo.StateNow = "正常结束";
        workToDo.ShenPiUserList = "工作已办结";
        workToDo.JieDianName = "结束";
        workToDo.Update();

        //职工请假审批表  
        if (workToDo.FormID == 50)
        {
            //修改当前请假的状态为正常结束
            DbHelperSQL.ExecuteSQL("update ERPQingJia set QJState='正常结束' where NWorkID=" + workToDo.ID);
        }
        //外出活动审批表
        if (workToDo.FormID == 77)
        {
            //修改当前外出活动的状态为正常结束
            DbHelperSQL.ExecuteSQL("update ERPWaiChuHuoDong set WCState='正常结束' where NWorkID=" + workToDo.ID);
        }
        //出差审批表
        if (workToDo.FormID == 78)
        {
            //修改当前出差的状态为正常结束
            DbHelperSQL.ExecuteSQL("update ERPChuChai set CCState='正常结束' where NWorkID=" + workToDo.ID);
        }
        //考勤补签表
        if (workToDo.FormID == 51)
        {
            //修改当前考勤补签的状态为正常结束
            DbHelperSQL.ExecuteSQL("update ERPBuQian set BQState='正常结束' where NWorkID=" + workToDo.ID);
        }

        //发邮件通知发文拟稿人
        var content = "您的工作已经通过审批！(" + workToDo.WorkName + ")";
        SendEmail(content, content, workToDo.FuJianList, workToDo.UserName, workToDo.BeiYong1, workToDo.FormID, workToDo.WorkFlowID, workToDo.ID);
    }

    /// <summary>
    /// 检测节点是否已经完成(防止同时审核)
    /// </summary>
    /// <param name="Mymodel"></param>
    /// <returns></returns>
    private bool CheckJieDianIsOver(ref string msg)
    {
        var WorkToDoID = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var Mymodel = new ZWL.BLL.ERPNWorkToDo();
        Mymodel.GetModel(WorkToDoID);
        if (Mymodel.ID > 0)
        {
            if (!Mymodel.ShenPiUserList.Contains(UserName))
            {
                msg = string.Format("当前审批已流转到[{0}],审批人为[{1}],请刷新待办工作列表核对.", Mymodel.JieDianName, Mymodel.ShenPiUserList);
                return false;
            }
        }
        else
        {
            msg = "登记人已删除此表单，无需审批。";
            return false;
        }
        return true;
    }
    #endregion

    #region 公文处理方法
    /// <summary>
    /// 公文处理方法
    /// </summary>
    /// <param name="workToDo"></param>
    public void GongWenHander(ZWL.BLL.ERPNWorkToDo workToDo)
    {
        //收文办文，最后通过后UPDATE批办人、承办人（sj--2015.12.30）
        if (workToDo.FormID == 48 && workToDo.JieDianName == "分管领导意见")
        {
            ParseHtml parse = new ParseHtml();
            if (workToDo.FormContent != "")
            {
                parse.GetAttListFormHTMLall(workToDo.FormContent);
                //提交发文字号
                ZWL.BLL.ERPShouWenBanWen MyShouWen = new ZWL.BLL.ERPShouWenBanWen();
                MyShouWen.NWorkGetModel(workToDo.ID);
                MyShouWen.PBR = parse.getValue("批办人");
                MyShouWen.CBR = parse.getValue("承办人");
                MyShouWen.Update();
            }
        }
        //发文登记，最后通过后给主送、抄送单位发传阅文件（sj--2015.12.23）
        //else if (workToDo.FormID == 49 && workToDo.JieDianName == "办公室校核")
        //{
        //    ParseHtml parse = new ParseHtml();
        //    if (workToDo.FormContent != "")
        //    {
        //        parse.GetAttListFormHTMLall(workToDo.FormContent);
        //        //提交发文字号
        //        ZWL.BLL.ERPFaWenDJ MyFaWenDJ = new ZWL.BLL.ERPFaWenDJ();
        //        MyFaWenDJ.NWorkGetModel(workToDo.ID);
        //        MyFaWenDJ.FaWenZH = parse.getValue("发文字号");

        //        MyFaWenDJ.ChuanYueYiJian = "";
        //        MyFaWenDJ.ContenStr = workToDo.FormContent;

        //        //发文传送用户名列表
        //        string strToUserList = "";
        //        strToUserList = MyFaWenDJ.ZhuSongDW;
        //        if (MyFaWenDJ.ChaoSong != "")
        //        {
        //            strToUserList += "," + MyFaWenDJ.ChaoSong;
        //        }


        //        MyFaWenDJ.ToUser = strToUserList;

        //        MyFaWenDJ.YiJieShouRen = "";
        //        MyFaWenDJ.ChuanYueHouIDList1 = "";
        //        MyFaWenDJ.QianShouHouIDList = "0";

        //        MyFaWenDJ.Update();
        //    }
        //}
        //发文登记，最后通过后给主送、抄送单位发传阅文件（sj--2015.12.23）
        else if (workToDo.FormID == 49 && (workToDo.JieDianName == "办公室分发" || workToDo.JieDianName == "办公室校核"))
        {
            ParseHtml parse = new ParseHtml();
            if (workToDo.FormContent != "")
            {
                parse.GetAttListFormHTMLall(workToDo.FormContent);
                //提交发文字号
                ZWL.BLL.ERPFaWenDJ MyFaWenDJ = new ZWL.BLL.ERPFaWenDJ();
                MyFaWenDJ.NWorkGetModel(workToDo.ID);
                MyFaWenDJ.FaWenZH = parse.getValue("发文字号");
                MyFaWenDJ.ZhuSongDW = parse.getValue("主送单位");
                MyFaWenDJ.ChaoSong = parse.getValue("抄送");

                MyFaWenDJ.Update();
            }
        }
        else if (workToDo.FormID == 90 && workToDo.JieDianName == "办公室确认金额")
        {
            //var erpOfficeSupplySummary = new ZWL.BLL.ERPOfficeSupplySummary();
            //erpOfficeSupplySummary = erpOfficeSupplySummary.GetModelBySqlWhere(string.Format(" NWorkID='{0}' ", workToDo.ID));
            //var detail = new ZWL.BLL.ERPOfficeSupplyDetail();
            //var dtlist = detail.GetListBySqlWhere(string.Format(" ID in (SELECT [SupplyDetailID] FROM [ERPOfficeSupplySummaryDetail] where [SupplySummaryID]='{0}') order by ID desc", erpOfficeSupplySummary.ID));
            //var realprice = Request["Text813640769"];
            //var name = Request["Text1147953348"];
            //var oneprice = Request["Text813640768"];
            //var num = Request["Text813640767"];
            //var realpricesplit = realprice.Split(',');
            //var onepricesplit = oneprice.Split(',');
            //var numsplit = num.Split(',');
            //var namesplit = name.Split(',');
            //for (int i = 0; i < dtlist.Count; i++)
            //{
            //    double test = 0;
            //    var dt = dtlist[i];
            //    var rp = realpricesplit[i];
            //    var ns = namesplit[i];
            //    dt.Total = (decimal)test;
            //    dt.Update();
            //}
        }
        else if (workToDo.FormID == 100)//会议室申请
        {
            string strmessage = "";
            string strtouserlist = "";
            if (workToDo.JieDianName == "办公室主任")
            {
                //状态改为已审批，向办公室科员发短信
                var erpmeeting = new ZWL.BLL.ERPMeeting();
                erpmeeting.GetNWorkModel(workToDo.ID);
                erpmeeting.State = "已审批";
                erpmeeting.Update();
                strmessage = "部门：" + erpmeeting.ShenQingBuMen + " ,申请人： "
                    + erpmeeting.ShenQingRen + " , 会议名称：" + erpmeeting.MeetingTitle
                    + @"  ，会议开始时间：" + erpmeeting.HuiYiStartTime + " ，会议结束时间" + erpmeeting.HuiYiEndTime
                    + @" ，会议地点：" + erpmeeting.HuiYiDiDian
                    + @" 已经通过办公室主任审核，请做好会议室的准备工作！";
                strtouserlist = DbHelperSQL.GetSHSL("select top 1 SPDefaultList from ERPNWorkFlowNode where ID=" + 368);
                //更新审批状态
                string strFormContent = workToDo.FormContent;
                strFormContent = strFormContent.Replace("待批准", "已批准");
                workToDo.FormContent = strFormContent;
                workToDo.Update();
            }
            else if (workToDo.JieDianName == "办公室科员")
            {
                var erpmeeting = new ZWL.BLL.ERPMeeting();
                erpmeeting.GetNWorkModel(workToDo.ID);
                //向申请人发送手机短信，请准时参会
                strtouserlist = erpmeeting.ShenQingRen;
                strmessage = "您于：" + erpmeeting.DengJiTime + "  申请的会议：" + erpmeeting.MeetingTitle
                    + @"  ，会议开始时间：" + erpmeeting.HuiYiStartTime + " ，会议结束时间" + erpmeeting.HuiYiEndTime
                    + @" ，会议地点：" + erpmeeting.HuiYiDiDian
                    + @" 已经通过审核，请及时参会！";
            }
            if (strmessage != "" && strtouserlist != "")
            {
                //发送手机信息
                Mobile.SendSMS("系统消息", strtouserlist, strmessage);
            }

        }
    }
    #endregion

    #region 经营处理方法
    /// <summary>
    /// 经营处理方法
    /// </summary>
    /// <param name="workToDo"></param>
    public void JingYingHander(ZWL.BLL.ERPNWorkToDo workToDo)
    {
        DateTime defaultime = new DateTime();
        PublicMethod.GetDefaultTime(out defaultime);
        //合同签订评审的合同归档节点
        if (workToDo.FormID == 43 && workToDo.JieDianName == "合同归档" && workToDo.WorkName.Contains("合同签订评审"))
        {
            //合同签订评审表单的经营管理科审核节点，点击保存并通过后提取合同信息保存到对应数据库表中
            ParseHtml parse = new ParseHtml();
            if (workToDo.FormContent != "")
            {
                parse.GetAttListFormHTMLall(workToDo.FormContent);
                string strhtzt = parse.getValue("合同状态");
                string strhtgd = parse.getValue("合同归档");
                string gdtime = parse.getValue("归档日期");
                string strhtid = parse.getValue("合同编号");
                string strzbtzs = parse.getValue("中标通知书");
                string strhgzs = parse.getValue("合格证书");
                ZWL.BLL.ERPHeTong hetong = new ZWL.BLL.ERPHeTong(strhtid);
                hetong.HTZT = strhtzt;
                hetong.HTGD = strhtgd;
                hetong.ZBTZS = strzbtzs;
                hetong.HGZS = strhgzs;
                if (strhtgd.EndsWith("未归档"))
                {

                }
                else if (strhtgd.Equals("已归档"))//如果该合同文本已归档，那么这个合同的借阅状态是可借出的，更改借阅状态为未借出
                {
                    hetong.HTJYState = "可借阅";
                    if (!string.IsNullOrEmpty(gdtime))
                        hetong.GDTime = gdtime == "" ? defaultime : DateTime.Parse(gdtime);
                }
                hetong.UpdateBD(strhtid);//更新数据库表中对应的记录，记录了该合同是否已经归档，如果已经归档那么可以借阅
            }
        }
        //合同签订评审经营科通过后给局长发邮件提醒
        else if (workToDo.FormID == 43 && workToDo.JieDianName == "经营科负责人审核")
        {
            int m_formid = PublicMethod.GetInt(workToDo.FormID.ToString());
            int m_workflowid = PublicMethod.GetInt(workToDo.WorkFlowID.ToString());
            string beiyong1 = workToDo.BeiYong1;
            SendMainAndSms.SendMessage2("新的合同签订评审工作已通过经营科审核！", PublicMethod.WorkWeiTuoUserList("蔡晓帆"), m_formid, m_workflowid, beiyong1);

        }

        else if (workToDo.FormID == 46)
        {
            //收款状态修改
            if (workToDo.JieDianName == "财务科出纳")
            {
                int m_formid = PublicMethod.GetInt(workToDo.FormID.ToString());
                int m_workflowid = PublicMethod.GetInt(workToDo.WorkFlowID.ToString());
                string beiyong1 = workToDo.BeiYong1;
                DbHelperSQL.ExecuteSQL("update ERPHeTongShouKuan set SKZT='已到账' where NWorkToDoID=" + workToDo.ID);
                //新增对第二工程处的收款发邮件的功能20160505lrn
                DateTime jiezhitime = new DateTime();
                DateTime.TryParse("2016/4/1", out jiezhitime);
                TimeSpan span = new TimeSpan();
                ParseHtml parse = new ParseHtml();
                parse.GetAttListFormHTMLall(workToDo.FormContent);
                DateTime SQTime = new DateTime();
                string strSQTime = parse.getValue("申请日期");
                DateTime.TryParse(strSQTime, out SQTime);
                string strBM = parse.getValue("部门");
                string strBMJBR = parse.getValue("部门经办人");
                span = SQTime - jiezhitime;
                if (strBM == "第二工程处")
                {
                    string strtouserlist = "张涛,马威,陈湛,陈敏妍,丁就荣";
                    if (strBMJBR == "陈城利")
                    {
                        if (span.Days > 0)//陈城利的项目只发申请日期在2016年4月1日之后的
                        {
                            SendMainAndSms.SendMessage2("新的合同收款审批工作已通过开票审核！", strtouserlist, m_formid, m_workflowid, beiyong1);
                        }
                    }
                    else
                    {
                        SendMainAndSms.SendMessage2("新的合同收款审批工作已通过开票审核！", strtouserlist, m_formid, m_workflowid, beiyong1);
                    }

                }
            }
            //合同收款审批开票审核通过后给邝珊珊发邮件提醒，并且存入ERPHeTongShouKuan表
            else if (workToDo.JieDianName == "开票审核" || workToDo.JieDianName == "经营科负责人审核")
            {
                ParseHtml parse = new ParseHtml();
                ZWL.BLL.ERPHeTongShouKuan HTshoukuan = new ZWL.BLL.ERPHeTongShouKuan();
                HTshoukuan.GetModelByNWorkId(workToDo.ID);
                if (workToDo.FormContent != "")
                {
                    parse.GetAttListFormHTMLall(workToDo.FormContent);
                    HTshoukuan.HTBH = parse.getValue("合同编号");
                    HTshoukuan.HTName = parse.getValue("合同名称");
                    HTshoukuan.BM = parse.getValue("部门");
                    ZWL.BLL.ERPHeTong HeTong = new ZWL.BLL.ERPHeTong(HTshoukuan.HTBH);
                    HTshoukuan.ZYLB = HeTong.ZYLB;
                    HTshoukuan.HTJE = HeTong.HTJE;
                    //HTshoukuan.GetSTRModel(HeTong.HTID);
                    if (DbHelperSQL.GetSHSL("select HTJE from ERPHeTongShouKuan where HTBH='" + HTshoukuan.HTBH + "'") != "")
                    {
                        decimal a = decimal.Parse(DbHelperSQL.GetSHSL("select sum(KaiPiaoJE) from ERPHeTongShouKuan where HTBH='" + HTshoukuan.HTBH + "'"));
                        HTshoukuan.ShengYuJE = HTshoukuan.HTJE - a - decimal.Parse(parse.getValue("开票金额"));
                    }
                    else
                    {
                        HTshoukuan.ShengYuJE = HTshoukuan.HTJE - decimal.Parse(parse.getValue("开票金额"));
                    }

                    HTshoukuan.KaiPiaoJE = decimal.Parse(parse.getValue("开票金额"));
                    HTshoukuan.SQTime = DateTime.Parse(parse.getValue("申请日期"));
                    HTshoukuan.KaiPiaoFS = parse.getValue("开票方式");
                    HTshoukuan.SKZT = "未到账";
                    HTshoukuan.FKDW = parse.getValue("付款单位");
                    HTshoukuan.NSRnum = parse.getValue("付款方纳税人识别号");
                    HTshoukuan.DZ = parse.getValue("付款方地址");
                    HTshoukuan.KaiHuHang = parse.getValue("付款方开户行及账号");
                    HTshoukuan.NWorkToDoID = workToDo.ID;
                    if (HTshoukuan.ID > 0)
                        HTshoukuan.Update(HTshoukuan.ID);
                    else
                        HTshoukuan.Add();
                }

                int m_formid = PublicMethod.GetInt(workToDo.FormID.ToString());
                int m_workflowid = PublicMethod.GetInt(workToDo.WorkFlowID.ToString());
                string beiyong1 = workToDo.BeiYong1;
                SendMainAndSms.SendMessage2("新的合同收款审批工作已通过开票审核！", PublicMethod.WorkWeiTuoUserList("邝珊珊"), m_formid, m_workflowid, beiyong1);
            }

        }
        else if (workToDo.FormID == 86 && workToDo.JieDianID == 294)//经营管理科  第二个节点 借出资质
        {
            var aptWork = new ZWL.BLL.AptitudeWork();
            var source = aptWork.GetModelList(" NWorkID = " + workToDo.ID);
            if (source.Any()) aptWork = source.FirstOrDefault();
            if (aptWork.ID != 0)
            {
                var firstList = new List<int> { 1, 2 };
                var aptWorkDtl = new ZWL.BLL.AptitudeWorkDetail();
                var aptWorkDtlList = aptWorkDtl.GetModelList("[AptWorkID]=" + aptWork.ID);
                if (aptWorkDtlList.Any())
                {
                    foreach (var item in aptWorkDtlList)
                    {
                        var aptState = new ZWL.BLL.AptitudeFileState();
                        aptState.GetModel(item.AptFileStateID);

                        if (!firstList.Contains(aptState.Type)) continue;

                        //if (aptState.State == (int)AptitudeState.Using)
                        //{
                        //    var aptModel = new ZWL.BLL.AptitudeFile();
                        //    aptModel.GetModel(aptState.AptFileID);
                        //    msg = "资质证照[" + aptModel.AptitudeName + "]的[" + Common.GetTitleTextByAptType(aptState.Type) + "]尚未归还,请确认归还后再申请！";
                        //    return false;
                        //}
                    }
                }
            }
        }
        else if (workToDo.FormID == 45 && workToDo.WorkName.Contains("合同结算"))//合同结算表单和流程
        {
            if (workToDo.JieDianName == "经营科审核")
            {
                ParseHtml parse = new ParseHtml();
                if (workToDo.FormContent != "")
                {
                    parse.GetAttListFormHTMLinput(workToDo.FormContent);
                    string jsje = parse.getValue("结算金额");
                    if (jsje != "")
                    {
                        DbHelperSQL.ExecuteSQL(
                            string.Format("UPDATE [ERPHTJieSuan] SET [JSJE] = '{0}' WHERE [NWorkToDoID] = '{1}'",
                                jsje, workToDo.ID));
                    }
                }
            }
        }
    }
    #endregion

    #region 项目处理方法
    /// <summary>
    /// 项目管理处理方法
    /// </summary>
    /// <param name="todoModel"></param>
    public void XMHander(ZWL.BLL.ERPNWorkToDo todoModel)
    {
        if (todoModel.FormID == 73 && todoModel.JieDianID == 204)// 资料归档表流程  第二个节点 资料室
        {
            //把资料归档的数据记录到数据
            var parse = new ParseHtml();
            var textareaParse = new ParseHtml();
            var guidang = new ZWL.BLL.ERPXMZLGuiDang();
            string strContent = todoModel.FormContent;
            if (!string.IsNullOrEmpty(strContent))
            {
                parse.GetAttListFormHTMLall(strContent);
                textareaParse.GetAttListFormHTMLtextarea(strContent);
                var dah = parse.getValue("归档号");
                guidang.XMName = textareaParse.getValue("项目名称");
                guidang.DAH = dah;
                guidang.XMBH = parse.getValue("项目编号");
                guidang.ReportBH = parse.getValue("报告编写");
                guidang.JBR = parse.getValue("经办人");
                guidang.XMFZR = parse.getValue("项目负责");
                guidang.WorkDate = parse.getValue("工作日期");
                guidang.DJTime = DateTime.Now;
                guidang.GDDate = new Nullable<DateTime>();
                guidang.NWorkID = todoModel.ID;
                if (guidang.Add() > 0)
                {
                    var rowcount = GetXMGuiDangRowCount(strContent);
                    for (int i = 1; i <= rowcount; i++)
                    {
                        if (!string.IsNullOrEmpty(parse.getValue("档案名称" + i)) || !string.IsNullOrEmpty(textareaParse.getValue("档案名称" + i)))
                        {
                            var detail = new ZWL.BLL.ERPXMZLGuiDangDetail();
                            detail.DAH = dah;
                            detail.XuHao = i;
                            detail.DAName = (string.IsNullOrEmpty(parse.getValue("档案名称" + i)) ? textareaParse.getValue("档案名称" + i) : parse.getValue("档案名称" + i));
                            detail.DanWei = parse.getValue("单位" + i);
                            detail.Count = parse.getValue("数量" + i);
                            detail.ZTXS = parse.getValue("载体形式" + i);
                            detail.BZ = parse.getValue("备注" + i);
                            detail.NWorkID = todoModel.ID;
                            if (detail.Add() <= 0)
                            {
                                MessageBox.Show(this, "资料归档的子表数据添加失败！");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "资料归档主表数据添加失败！");
                }
            }
        }
        else if (todoModel.FormID == 115 && todoModel.JieDianName.Contains("资料室"))
        {
            var parse = new ParseHtml();
            parse.GetAttListFormHTMLall(todoModel.FormContent);
            var dah = parse.getValue("归档号");
            var guidang = new ZWL.BLL.ERPXMZLGuiDang();
            guidang.GetModelByNWorkID(todoModel.ID);
            guidang.DAH = dah;
            guidang.Update();
            var guidangdetail = new ZWL.BLL.ERPXMZLGuiDangDetail();
            var list = guidangdetail.GetModelList("NWorkID=" + todoModel.ID);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    item.DAH = dah;
                    item.Update();
                }
            }
        }
        else if (todoModel.FormID == 56 && todoModel.JieDianName == "总工办核定员核定")
        {
            //自动发起盖章流程
            AutoCreateYinZhang(todoModel.ID, 81, 73, "项目设计审查");
        }
        else if (todoModel.FormID == 75 && todoModel.JieDianName == "总工办核定员核定")
        {
            //自动发起盖章流程
            AutoCreateYinZhang(todoModel.ID, 81, 73, "项目设计审查");
        }
    }

    /// <summary>
    /// 自动发起盖章流程
    /// </summary>
    /// <param name="Model">传入的是当前的工作</param>
    /// <summary>
    /// 更新项目成果信息
    /// </summary>
    /// <param name="Model">传入的是当前的工作</param>
    public void UpdateXMResult(string strFormC)
    {
        //int nrows = 0;
        if (strFormC != null || strFormC != "")
        {
            //项目成果审核表
            //点击保存并通过后提取钻孔信息保存到对应数据库表中
            ParseHtml parse = new ParseHtml();
            parse.GetAttListFormHTMLall(strFormC);
            string m_XMID = parse.getValue("项目编号");
            #region new ZWL.BLL.ERPXMJBXX(m_XMID);
            ZWL.BLL.ERPXMJBXX ModelXM = new ZWL.BLL.ERPXMJBXX(m_XMID);//获取当前项目基本信息(数据库表中的记录)

            string strnamevalue = ModelXM.BeiYong1;
            if (strnamevalue == "" || strnamevalue == null)
            {
                strnamevalue = parse.getValue("项目编号") + "@" + parse.getValue("项目名称");
            }
            int nXMCG = DbHelperSQL.GetDataRowsCount("SELECT * FROM ERPNWorkToDo Where FormID=71 and StateNow='正常结束' and BeiYong1='" + strnamevalue + "'");

            //只对钻孔数和进尺进行累加操作，其余信息存储到成果审核表中。
            if (parse.getValue("钻孔数") != "")
            {
                if (nXMCG == 0)
                {
                    ModelXM.ZKS = PublicMethod.GetInt(parse.getValue("钻孔数"));
                }
                else
                {
                    int nZKS = DbHelperSQL.GetSHSLInt1("SELECT isnull(ZKS,0) FROM ERPXMJBXX Where XMBH='" + m_XMID + "'");
                    ModelXM.ZKS = PublicMethod.GetInt(parse.getValue("钻孔数")) + nZKS;
                }

            }
            if (parse.getValue("钻孔进尺") != "")
            {
                if (nXMCG == 0)
                {
                    ModelXM.ZKJC = PublicMethod.GetFloat(parse.getValue("钻孔进尺"));
                }
                else
                {
                    string strZKJC = DbHelperSQL.GetSHSLInt("SELECT ZKJC FROM ERPXMJBXX Where XMBH='" + m_XMID + "'");
                    ModelXM.ZKJC = PublicMethod.GetFloat(parse.getValue("钻孔进尺")) + PublicMethod.GetFloat(strZKJC);
                }
            }
            //如果是第一个成果审核表
            if (nXMCG == 0)
            {
                ModelXM.PGDJ = parse.getValue("评估等级");
                ModelXM.KCDJ = parse.getValue("勘察等级");
                ModelXM.BXCLDJ = parse.getValue("变形测量等级");
                if (parse.getValue("评估面积") != "")
                {
                    ModelXM.PGMJ = PublicMethod.GetFloat(parse.getValue("评估面积"));
                }
                if (parse.getValue("调查面积") != "")
                {
                    ModelXM.DCMJ = PublicMethod.GetFloat(parse.getValue("调查面积"));
                }
                if (parse.getValue("探槽") != "")
                {
                    ModelXM.TC = PublicMethod.GetFloat(parse.getValue("探槽"));
                }
                if (parse.getValue("坑探") != "")
                {
                    ModelXM.KT = PublicMethod.GetFloat(parse.getValue("坑探"));
                }
                if (parse.getValue("钻探") != "")
                {
                    ModelXM.ZT = PublicMethod.GetFloat(parse.getValue("钻探"));
                }
            }
            //如果多个成果审核，将报告都存到项目基本信息中，方便定位到项目。
            if (parse.getValue("报告名称") != "")
            {
                if (ModelXM.XMReport != "")
                {
                    ModelXM.XMReport += "@";//多个报告，用@分割
                }
                ModelXM.XMReport += parse.getValue("报告名称") + ";";
            }
            //nrows = ModelXM.UpdateXMCGSHB(m_XMID);
            //ViewState["updatestate"] = "yes";
            ModelXM.UpdateXMCGSHB(m_XMID);
            #endregion
            #region ModelXMCG = new ZWL.BLL.ERPXMChengGuo()

            //当前的项目成果审核表通过审定人审核，所以在这里将成果审核信息存到成果审核数据库表中。
            ZWL.BLL.ERPXMChengGuo ModelXMCG = new ZWL.BLL.ERPXMChengGuo();//保存当前项目成果信息
            if (parse.getValue("钻孔数") != "")
            {
                ModelXMCG.ZKS = PublicMethod.GetInt(parse.getValue("钻孔数"));
            }
            if (parse.getValue("钻孔进尺") != "")
            {
                ModelXMCG.ZKJC = PublicMethod.GetFloat(parse.getValue("钻孔进尺"));
            }
            ModelXMCG.PGDJ = parse.getValue("评估等级");
            ModelXMCG.KCDJ = parse.getValue("勘察等级");
            ModelXMCG.BXCLDJ = parse.getValue("变形测量等级");
            if (parse.getValue("评估面积") != "")
            {
                ModelXMCG.PGMJ = PublicMethod.GetFloat(parse.getValue("评估面积"));
            }
            if (parse.getValue("调查面积") != "")
            {
                ModelXMCG.DCMJ = PublicMethod.GetFloat(parse.getValue("调查面积"));
            }
            if (parse.getValue("探槽") != "")
            {
                ModelXMCG.TC = PublicMethod.GetFloat(parse.getValue("探槽"));
            }
            if (parse.getValue("坑探") != "")
            {
                ModelXMCG.KT = PublicMethod.GetFloat(parse.getValue("坑探"));
            }
            if (parse.getValue("钻探") != "")
            {
                ModelXMCG.ZT = PublicMethod.GetFloat(parse.getValue("钻探"));
            }
            if (parse.getValue("报告名称") != "")
            {
                ModelXMCG.BGMC = parse.getValue("报告名称");
            }
            //测量相关字段
            if (parse.getValue("工程测量长度") != "")
            {
                ModelXMCG.GCCLlength = PublicMethod.GetFloat(parse.getValue("工程测量长度"));
            }
            if (parse.getValue("工程测量面积") != "")
            {
                ModelXMCG.GCCLarea = PublicMethod.GetFloat(parse.getValue("工程测量面积"));
            }
            if (parse.getValue("基坑监测面积") != "")
            {
                ModelXMCG.JKJCarea = PublicMethod.GetFloat(parse.getValue("基坑监测面积"));
            }
            if (parse.getValue("基坑监测深度") != "")
            {
                ModelXMCG.JKJCdepth = PublicMethod.GetFloat(parse.getValue("基坑监测深度"));
            }
            if (parse.getValue("基坑设计周长") != "")
            {
                ModelXMCG.JKSJlength = PublicMethod.GetFloat(parse.getValue("基坑设计周长"));
            }
            if (parse.getValue("基坑设计深度") != "")
            {
                ModelXMCG.JKSJdepth = PublicMethod.GetFloat(parse.getValue("基坑设计深度"));
            }
            if (parse.getValue("基坑设计面积") != "")
            {
                ModelXMCG.JKSJarea = PublicMethod.GetFloat(parse.getValue("基坑设计面积"));
            }
            if (parse.getValue("管线探测") != "")
            {
                ModelXMCG.GXTC = PublicMethod.GetFloat(parse.getValue("管线探测"));
            }
            if (parse.getValue("土壤氡检测") != "")
            {
                ModelXMCG.TRDJC = PublicMethod.GetFloat(parse.getValue("土壤氡检测"));
            }

            ModelXMCG.ShTime = DateTime.Now;//将当前时间记为审核时间
            ModelXMCG.XMBH = m_XMID;
            ModelXMCG.XMName = parse.getValue("项目名称");
            ModelXMCG.NWorkToDoID = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
            ModelXMCG.BeiYong1 = m_XMID + "@" + parse.getValue("项目名称");
            ModelXMCG.Add();
            #endregion

            #region 随之光速发起盖章流程

            ZWL.BLL.ERPNWorkToDo Model = new ZWL.BLL.ERPNWorkToDo();
            Model.WorkName = ModelXM.XMFZR.ToString() + "--" + DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=73") + "(" + DateTime.Now.ToShortDateString() + ")";
            DateTime defaultime = new DateTime();
            PublicMethod.GetDefaultTime(out defaultime);
            Model.FormID = 81;
            Model.WorkFlowID = 73;
            Model.UserName = ModelXM.XMFZR.ToString();
            Model.TimeStr = DateTime.Now;


            //替换控件中的值到表单中
            string strFormContent = GetFormContent1("81");
            //对拟稿日期和收文日期进行处理（中文格式转化）
            string cntxtTime = DateZH(DateTime.Now.ToShortDateString());


            //替换原有表单content中的值

            strFormContent = strFormContent.Replace("用户自定义控件-时间", cntxtTime);
            strFormContent = strFormContent.Replace("用户自定义控件-申请部门", ModelXM.XMBM.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-经办人", ModelXM.XMFZR.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-项目编号", ModelXM.XMBH.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-项目名称", ModelXM.XMName.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称1", ModelXMCG.BGMC == null ? "" : ModelXMCG.BGMC.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-份数1", "1");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称2", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数2", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称3", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数3", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称4", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数4", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称5", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数5", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称6", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数6", "");


            Model.FormContent = strFormContent;//将表单内容写回到数据库中

            Model.ShenPiYiJian = "";
            Model.JieDianID = 274;

            Model.JieDianName = DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString());
            Model.StateNow = "正在办理";

            var shenpiusers = "";
            var ulist = Conv<ZWL.BLL.ERPUser>.GetList("SELECT * from ERPUser where Department='经营管理科' and IfLogin='是' ORDER BY DisplayID");
            if (ulist != null && ulist.Any())
            {
                shenpiusers = string.Join(",", ulist.Select(x => x.UserName));
            }
            Model.ShenPiUserList = shenpiusers;
            Model.OKUserList = "默认";
            Model.LateTime = DateTime.Now.AddHours(double.Parse(DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString())));
            Model.BeiYong1 = ModelXM.XMName.ToString() + "印章使用登记" + "--" + cntxtTime;
            Model.BeiYong2 = "";

            var workid = Model.Add();

            ZWL.BLL.ERPGaizhang Gaizhang = new ZWL.BLL.ERPGaizhang();
            Gaizhang.Department = ModelXM.XMBM.ToString();
            Gaizhang.UserName = ModelXM.XMFZR.ToString();
            Gaizhang.Type = "项目编号";
            Gaizhang.code = ModelXM.XMBH.ToString();
            Gaizhang.WorkType = "印章使用登记表";
            Gaizhang.NWorkID = workid;
            Gaizhang.state = "GZ-" + (DbHelperSQL.GetMaxID("ID", "ERPGaizhang") + 1).ToString();
            Gaizhang.beiyong1 = ModelXM.XMName.ToString() + "-" + ModelXM.XMBM.ToString() + "-印章使用登记";
            Gaizhang.beiyong2 = DateZH(DateTime.Now.ToShortDateString());
            Gaizhang.beiyong3 = DateTime.Parse(DateTime.Now.ToShortTimeString());
            Gaizhang.beiyong4 = "成果报告";
            Gaizhang.FSDW = "";
            Gaizhang.Add();

            CheckBox cb1 = new CheckBox();
            CheckBox cb2 = new CheckBox();
            cb1.Checked = true;
            cb2.Checked = false;
            if (Model.StateNow == "正在办理")
            {
                //发送短信
                SendMainAndSms.SendMessage1(cb1, cb2, "您有新的工作需要办理！(" + Model.WorkName + ")", "罗鑫", 81, 73, Model.BeiYong1);
                SendMainAndSms.SendMessage1(cb1, cb2, "项目成功审核结束，已自动生成盖章流程！(" + Model.WorkName + ")", ModelXM.XMFZR.ToString(), 81, 73, Model.BeiYong1);
                //SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + strWorkName + ")", PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()));
            }
            else
            {
                SendMainAndSms.SendMessage(cb1, cb2, "您的工作已经被强制结束！(" + Model.WorkName + ")", ModelXM.XMFZR.ToString());
            }


            //写系统日志
            WriteLog("用户添加新工作信息(" + Model.WorkName + ")");
            #endregion

        }
        //return nrows;
    }

    public void AutoCreateYinZhang(int todoid, int formid, int workflowid, string gaizhanglx)
    {
        //int nrows = 0;
        var wModel = new ZWL.BLL.ERPNWorkToDo();
        wModel.GetModel(todoid);
        if (!string.IsNullOrEmpty(wModel.Number))
        {
            var ModelXM = new ZWL.BLL.ERPXMJBXX(wModel.Number);//获取当前项目基本信息(数据库表中的记录)
            #region 随之光速发起盖章流程
            var Model = new ZWL.BLL.ERPNWorkToDo();
            Model.WorkName = ModelXM.XMFZR.ToString() + "--" + GetWorkFlowName(workflowid) + "(" + Timestamp.ToShortDateString() + ")";
            if (gaizhanglx == "项目设计审查")
            {
                Model.WorkName = ModelXM.XMFZR.ToString() + "--" + "勘查纲要/项目工作大纲/项目设计审查盖章" + "(" + Timestamp.ToShortDateString() + ")";
            }
            if (gaizhanglx == "测量报告")
            {
                Model.WorkName = ModelXM.XMFZR.ToString() + "--" + "测量报告审核表盖章" + "(" + Timestamp.ToShortDateString() + ")";
            }
            DateTime defaultime = new DateTime();
            PublicMethod.GetDefaultTime(out defaultime);
            Model.FormID = formid;
            Model.WorkFlowID = workflowid;
            Model.UserName = ModelXM.XMFZR.ToString();
            Model.TimeStr = DateTime.Now;
            //替换控件中的值到表单中
            var fModel = new ZWL.BLL.ERPNForm();
            fModel.GetModel(formid);
            var strFormContent = fModel.ContentStr;
            //对拟稿日期和收文日期进行处理（中文格式转化）
            var cntxtTime = DateZH(DateTime.Now.ToShortDateString());
            var filename = wModel.CurrentForm().FormName.Replace("审核表", "");
            if (!filename.Contains("项目")) filename = "项目" + filename;
            if (!filename.Contains("盖章")) filename = filename + "盖章";
            //替换原有表单content中的值
            strFormContent = strFormContent.Replace("用户自定义控件-时间", cntxtTime);
            strFormContent = strFormContent.Replace("用户自定义控件-申请部门", ModelXM.XMBM.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-经办人", ModelXM.XMFZR.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-项目编号", ModelXM.XMBH.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-项目名称", ModelXM.XMName.ToString());
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称1", filename);
            strFormContent = strFormContent.Replace("用户自定义控件-份数1", "1");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称2", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数2", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称3", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数3", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称4", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数4", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称5", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数5", "");
            strFormContent = strFormContent.Replace("用户自定义控件-资料名称6", "");
            strFormContent = strFormContent.Replace("用户自定义控件-份数6", "");
            Model.FormContent = strFormContent;//将表单内容写回到数据库中
            Model.ShenPiYiJian = "";
            var secNode = GetSecondNode(Model.WorkFlowID);
            if (secNode != null)
            {
                Model.JieDianID = secNode.ID;
                Model.JieDianName = secNode.NodeName;
                Model.ShenPiUserList = GetShenPiUserList(secNode.SPType, secNode.SPDefaultList);
            }
            Model.StateNow = "正在办理";
            Model.OKUserList = "默认";
            Model.LateTime = GetLateTime(Model.JieDianID);
            Model.BeiYong1 = ModelXM.XMName.ToString() + "印章使用登记" + "--" + cntxtTime;
            Model.BeiYong2 = "";

            var workid = Model.Add();

            ZWL.BLL.ERPGaizhang Gaizhang = new ZWL.BLL.ERPGaizhang();
            Gaizhang.Department = ModelXM.XMBM.ToString();
            Gaizhang.UserName = ModelXM.XMFZR.ToString();
            Gaizhang.Type = "项目编号";
            Gaizhang.code = ModelXM.XMBH.ToString();
            Gaizhang.WorkType = "印章使用登记表";
            Gaizhang.NWorkID = workid;
            Gaizhang.state = "GZ-" + (DbHelperSQL.GetMaxID("ID", "ERPGaizhang") + 1).ToString();
            Gaizhang.beiyong1 = ModelXM.XMName.ToString() + "-" + ModelXM.XMBM.ToString() + "-印章使用登记";
            Gaizhang.beiyong2 = DateZH(DateTime.Now.ToShortDateString());
            Gaizhang.beiyong3 = DateTime.Parse(DateTime.Now.ToShortTimeString());
            Gaizhang.beiyong4 = gaizhanglx;
            Gaizhang.FSDW = "";
            Gaizhang.Add();

            CheckBox cb1 = new CheckBox();
            CheckBox cb2 = new CheckBox();
            cb1.Checked = true;
            cb2.Checked = false;
            if (Model.StateNow == "正在办理")
            {
                //发送短信
                SendMainAndSms.SendMessage(cb1, cb2, "您有新的工作需要办理！(" + Model.WorkName + ")", "罗鑫", formid, workflowid, Model.BeiYong1, workid);
                SendMainAndSms.SendMessage(cb1, cb2, fModel.FormName + "成功审核结束，已自动生成盖章流程！(" + Model.WorkName + ")", ModelXM.XMFZR.ToString(), formid, workflowid, Model.BeiYong1, workid);
            }
            else
            {
                SendMainAndSms.SendMessage(cb1, cb2, "您的工作已经被强制结束！(" + Model.WorkName + ")", ModelXM.XMFZR.ToString());
            }


            //写系统日志
            WriteLog("用户添加新工作信息(" + Model.WorkName + ")");
            #endregion

        }
        //return nrows;
    }

    protected string GetFormContent1(string FormID)
    {
        string strFormContent;
        strFormContent = DbHelperSQL.GetSHSL("select top 1 ContentStr from ERPNForm where ID=" + FormID);
        strFormContent = strFormContent.Replace("宏控件-用户部门", PublicMethod.GetSessionValue("Department"));
        strFormContent = strFormContent.Replace("宏控件-用户姓名", UserName);
        strFormContent = strFormContent.Replace("宏控件-用户角色", PublicMethod.GetSessionValue("JiaoSe"));
        strFormContent = strFormContent.Replace("宏控件-用户职位", PublicMethod.GetSessionValue("ZhiWei"));
        strFormContent = strFormContent.Replace("宏控件-当前日期", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("宏控件-部门主管", DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + PublicMethod.GetSessionValue("Department") + "'"));
        return strFormContent;
    }

    public static string DateZH(string bgdate)
    {
        try
        {
            DateTime ymd = DateTime.Parse(bgdate);
            DateTime time = DateTime.Parse(ymd.ToShortDateString());
            string cndate = time.Year + "年" + time.Month + "月" + time.Day + "日";
            return cndate;
        }
        catch
        {
            return bgdate;
        }
    }

    #endregion

    #region 人事处理方法
    /// <summary>
    /// 人事处理方法
    /// </summary>
    /// <param name="workToDo"></param>
    public void HRHander(ZWL.BLL.ERPNWorkToDo workToDo)
    {
        //职工请假审批表  
        if (workToDo.FormID == 50)
        {
            //针对请假审批判断是否存在下一节点，没有则提前结束流程
            ZWL.BLL.ERPQingJia QJModel = new ZWL.BLL.ERPQingJia();
            QJModel.GetNWorkModel(workToDo.ID);
            float QingJiaNum = QJModel.QJTS;//请假天数
            string strJiaoSe = QJModel.JiaoSe;
            string strBM = QJModel.BM;
            var bm = Conv<ZWL.BLL.ERPBuMen>.GetModel("select * from ERPBuMen where BuMenName='{0}' and BuMenName not in('中心领导')".FormatWith(strBM));
            if (bm != null && bm.ChargeMan == QJModel.QJR)
            {
                strJiaoSe = "部门领导（人事管理）";
            }
            string strzgzt = DbHelperSQL.GetSHSL("select ZaiGang  FROM ERPUser where UserName='" + QJModel.QJR + "'");
            //废弃 根据请假天数，判断审批流程
            //废弃 除各部门、各科室负责人以外的工作人员请假由所在部门负责人及所在部门分管领导批准，超过 3 天且在 5天以内的，由人事分管领导批准;5 天以上的，由局长批准
            //20251011现在请假不用区分天数，
            //组长——部门负责人——人事——分管领导——中心负责人 
            //副组长或组员——组长——人事——分管领导
            //非在编只需要部门负责人审批
            if (workToDo.JieDianName == "结束")
            {
                DoEnd();
                return;
            }
            if (workToDo.JieDianName == "部门主管审批")
            {
                switch (strJiaoSe)
                {
                    case "一般工作人员（人事管理）":
                        if (strzgzt != "在编")
                        {
                            Qingjiaconfirm(workToDo.ID);
                            DoEnd();
                            //BindEndNode(workToDo.WorkFlowID);
                        }
                        break;
                }
            }
            else if (workToDo.JieDianName == "中心分管领导审批")//部门领导请假5天以内，都需要局分管领导审批
            {
                if (QJModel.QJR == "蔡晓帆")
                {
                    Qingjiaconfirm(workToDo.ID);
                    DoEnd();
                    return;
                }

                if (!strJiaoSe.Contains("单位领导") && (bm != null && bm.ChargeMan != QJModel.QJR || !strJiaoSe.Contains("部门领导")))
                {
                    Qingjiaconfirm(workToDo.ID);
                    DoEnd();
                }
            }
            else if (workToDo.JieDianName == "中心负责人审批")
            {
                Qingjiaconfirm(workToDo.ID);
                DoEnd();
            }
            //if (QingJiaNum <= 2)
            //{
            //    if (workToDo.JieDianName == "部门主管审批")//一般工作人员请假2天以内，需要部门主管审批
            //    {
            //        switch (strJiaoSe)
            //        {
            //            case "一般工作人员（人事管理）":
            //                if (strzgzt != "在编")
            //                {
            //                    Qingjiaconfirm(workToDo.ID);
            //                    DoEnd();
            //                    //BindEndNode(workToDo.WorkFlowID);
            //                }
            //                break;
            //        }
            //    }
            //    else if (workToDo.JieDianName == "人事科审批")//机关科室职工请假2天以内，需要人事科审批
            //    {
            //        //switch (strJiaoSe)
            //        //{
            //        //    case "机关科室职工（人事管理）":
            //        //        if (workToDo.UserName != "卢文瑜")
            //        //        {
            //        //            BindEndNode(workToDo.WorkFlowID);
            //        //        }
            //        //        break;
            //        //    case "一般工作人员（人事管理）":
            //        //        BindEndNode(workToDo.WorkFlowID);
            //        //        break;
            //        //    default:
            //        //        break;
            //        //}
            //    }
            //    else if (workToDo.JieDianName == "中心分管领导审批")//部门领导请假5天以内，都需要局分管领导审批
            //    {
            //        if (QJModel.QJR == "蔡晓帆")
            //        {
            //            Qingjiaconfirm(workToDo.ID);
            //            DoEnd();
            //            //BindEndNode(workToDo.WorkFlowID);
            //            return;
            //        }

            //        if (!strJiaoSe.Contains("单位领导") && (bm != null && bm.ChargeMan != QJModel.QJR || !strJiaoSe.Contains("部门领导")))
            //        {
            //            Qingjiaconfirm(workToDo.ID);
            //            DoEnd();
            //            //BindEndNode(workToDo.WorkFlowID);
            //        }
            //    }
            //    else if (workToDo.JieDianName == "中心负责人审批")//部门领导请假5天以内，都需要局分管领导审批
            //    {
            //        Qingjiaconfirm(workToDo.ID);
            //        DoEnd();
            //        //BindEndNode(workToDo.WorkFlowID);
            //    }
            //}
            //else if (QingJiaNum <= 5 && QingJiaNum > 2)//所有人请假2至5天，都需要局分管领导审批
            //{
            //    if (strJiaoSe == "单位领导")
            //    {
            //        if (workToDo.JieDianName == "人事科审批")
            //        {

            //        }
            //        else if (workToDo.JieDianName == "中心分管领导审批")
            //        {
            //            if (QJModel.QJR == "蔡晓帆")
            //            {
            //                Qingjiaconfirm(workToDo.ID);
            //                DoEnd();
            //                //BindEndNode(workToDo.WorkFlowID);
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else if (workToDo.JieDianName == "人事分管领导")
            //        {

            //        }
            //        else if (workToDo.JieDianName == "中心负责人审批")
            //        {
            //            Qingjiaconfirm(workToDo.ID);
            //            DoEnd();
            //            //BindEndNode(workToDo.WorkFlowID);
            //        }
            //    }
            //    else
            //    {
            //        if (workToDo.JieDianName == "部门主管审批")
            //        {
            //            if (strzgzt != "在编")//合同制员工只需部门负责人审批，不需要到人事科
            //            {
            //                Qingjiaconfirm(workToDo.ID);
            //                DoEnd();
            //                //BindEndNode(workToDo.WorkFlowID);
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else if (workToDo.JieDianName == "人事科审批")
            //        {

            //        }
            //        else if (workToDo.JieDianName == "中心分管领导审批")
            //        {

            //        }
            //        else if (workToDo.JieDianName == "人事分管领导")
            //        {
            //            if (!strJiaoSe.Contains("单位领导") && (bm != null && bm.ChargeMan != QJModel.QJR || !strJiaoSe.Contains("部门领导")))
            //            {
            //                Qingjiaconfirm(workToDo.ID);
            //                DoEnd();
            //                //BindEndNode(workToDo.WorkFlowID);
            //            }
            //        }
            //        else if (workToDo.JieDianName == "中心负责人审批")
            //        {

            //        }
            //    }

            //}
            //else if (QingJiaNum > 5)
            //{
            //    if (strzgzt != "在编")//合同制员工只需部门负责人审批，不需要到人事科
            //    {
            //        Qingjiaconfirm(workToDo.ID);
            //        DoEnd();
            //        //BindEndNode(workToDo.WorkFlowID);
            //    }
            //    else
            //    {

            //    }
            //}

        }
        else if (workToDo.FormID == 78)
        {
            //针对出差审批判断交通工具是否飞机，不是则提前结束流程
            ZWL.BLL.ERPChuChai CCModel = new ZWL.BLL.ERPChuChai();
            CCModel.GetNWorkModel(workToDo.ID);
            if (IsYiqing)
            {
                if (!CCModel.ChuChaiDiDian.Contains("广东省"))
                {
                    if (workToDo.JieDianName == "领导审批")
                    {
                        if (workToDo.ShenPiUserList.ToLower().Contains("蔡晓帆"))
                        {
                            string strImgPath = DbHelperSQL.GetSHSL("select ImgPath from ERPYinZhang where YinZhangLeiBie='私人印章' and UserName='" + UserName + "' order by YinZhangLeiBie desc");
                            strImgPath = "../UploadFile/" + strImgPath;
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("../images/Button/InsertYinZhang.gif", strImgPath);
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Text546511503", "value=\"同意\" name=Text546511503");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Text546511503\"", "value=\"同意\" name=\"Text546511503\"");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Date919230742", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=Date919230742");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Date919230742\"", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=\"Date919230742\"");
                            DoEnd();
                        }
                    }
                }
                else
                {
                    DoEnd();
                }
            }
            else
            {
                if (CCModel.SQR == "韩梅")
                {
                    if (workToDo.JieDianName == "领导审批")
                    {

                    }
                    else
                    {
                        if (CCModel.JiaoTongGJ == "飞机")
                        {
                            if (workToDo.JieDianName == "领导审批")
                            {
                                if (workToDo.ShenPiUserList.ToLower().Contains("蔡晓帆"))
                                {
                                    string strImgPath = DbHelperSQL.GetSHSL("select ImgPath from ERPYinZhang where YinZhangLeiBie='私人印章' and UserName='" + UserName + "' order by YinZhangLeiBie desc");
                                    strImgPath = "../UploadFile/" + strImgPath;
                                    this.Hidden_form.Value = this.Hidden_form.Value.Replace("../images/Button/InsertYinZhang.gif", strImgPath);
                                    this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Text546511503", "value=\"同意\" name=Text546511503");
                                    this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Text546511503\"", "value=\"同意\" name=\"Text546511503\"");
                                    this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Date919230742", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=Date919230742");
                                    this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Date919230742\"", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=\"Date919230742\"");
                                    DoEnd();
                                }
                                else if (workToDo.JieDianName == "单位负责人审批")
                                {
                                    DoEnd();
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            DoEnd();
                        }
                    }
                }
                else if (CCModel.JiaoTongGJ == "飞机")
                {
                    if (workToDo.JieDianName == "领导审批")
                    {
                        if (workToDo.ShenPiUserList.ToLower().Contains("蔡晓帆"))
                        {
                            string strImgPath = DbHelperSQL.GetSHSL("select ImgPath from ERPYinZhang where YinZhangLeiBie='私人印章' and UserName='" + UserName + "' order by YinZhangLeiBie desc");
                            strImgPath = "../UploadFile/" + strImgPath;
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("../images/Button/InsertYinZhang.gif", strImgPath);
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Text546511503", "value=\"同意\" name=Text546511503");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Text546511503\"", "value=\"同意\" name=\"Text546511503\"");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=Date919230742", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=Date919230742");
                            this.Hidden_form.Value = this.Hidden_form.Value.Replace("name=\"Date919230742\"", "value=\"" + DateTime.Now.ToShortDateString() + "\" name=\"Date919230742\"");
                            DoEnd();
                        }
                        else
                        {

                        }
                    }
                    else if (workToDo.JieDianName == "单位负责人审批")
                    {
                        DoEnd();
                    }
                }
                else
                {
                    DoEnd();
                }
            }
        }
        else if (workToDo.FormID == 112)
        {
            if (workToDo.JieDianName == "分管领导意见")
            {
                var parser = new ZWL.Common.ParseHtml();
                parser.GetAttListFormHTMLinput(workToDo.FormContent);
                var outType = parser.getValue("外出类型");
                if (outType == "出市")
                {
                    DoEnd();
                }
            }
        }
        else if (workToDo.FormID == 51)
        {
            DoEnd();
        }
        else if (workToDo.FormID == 77)
        {
            //针对外出活动申请判断是否存在下一节点，没有则提前结束流程
            ZWL.BLL.ERPWaiChuHuoDong WCModel = new ZWL.BLL.ERPWaiChuHuoDong();
            WCModel.GetNWorkModel(workToDo.ID);

            if (workToDo.JieDianName == "局长意见")
            {
                DoEnd();
            }
            else
            {

            }
        }
        else
        {
        }
    }

    public void Qingjiaconfirm(int workid)
    {
        ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
        var qingjia = Pojo.GetModelList<ZWL.BLL.ERPQingJia>("select * from ERPQingJia where NWorkID=" + workid);
        if (qingjia[0].ShiYongNXJ > 0)
        {
            amodel.GetModel(qingjia[0].QJR);
            amodel.nxjconfirm(qingjia[0].ShiYongNXJ);
            //将年休假的代码归到model里面
            //if (qingjia[0].ShiYongNXJ > amodel.LastFreezonDay)//如果使用年休假大于去年冻结年休假，就需要分段扣掉
            //{
            //    double temp = qingjia[0].ShiYongNXJ - amodel.LastFreezonDay;//算出还需要在今年冻结年休假扣掉的天数
            //    amodel.LastFreezonDay -= amodel.LastFreezonDay;//把去年冻结的年休假全部扣掉
            //    amodel.FreezonDay -= temp;//扣掉今年冻结
            //}
            //else//如果使用年休假小于或等于去年冻结的，只要在去年冻结扣掉即可
            //{
            //    amodel.LastFreezonDay -= qingjia[0].ShiYongNXJ;//扣掉扣掉扣掉
            //}
            //amodel.Update();
        }
    }
    #endregion

    #region nextnode

    private void BindEndNode(int? workflowid)
    {
        var lastNode = GetLastNode(workflowid);
        if (lastNode != null)
        {
            SelectedNextNode.Items.Clear();
            BindNextNodeForce(SelectedNextNode, lastNode.ID);
        }
    }

    private void AutoNextNodeExtend(ZWL.BLL.ERPNWorkToDo todoModel)
    {
        var nWorkId = todoModel.ID;
        var formContent = todoModel.FormContent;
        var cFormId = todoModel.FormID;
        var jieDianName = todoModel.JieDianName;
        var workFlowId = todoModel.WorkFlowID;
        //职工请假审批表  
        #region 职工请假审批表
        if (cFormId == 50)
        {
            //针对请假审批判断是否存在下一节点，没有则提前结束流程
            var QJModel = new ZWL.BLL.ERPQingJia();
            QJModel.GetNWorkModel(todoModel.ID);
            float QingJiaNum = QJModel.QJTS;//请假天数
            string strJiaoSe = QJModel.JiaoSe;
            string strBM = QJModel.BM;
            var bm = Conv<ZWL.BLL.ERPBuMen>.GetModel("select * from ERPBuMen where BuMenName='{0}' and BuMenName not in('中心领导')".FormatWith(strBM));
            if (strJiaoSe == "部门领导（人事管理）")
            {
                if (bm != null && bm.ChargeMan != QJModel.QJR)
                {
                    strJiaoSe = "一般工作人员（人事管理）";
                }
            }
            string strzgzt = DbHelperSQL.GetSHSL("select ZaiGang  FROM ERPUser where UserName='" + QJModel.QJR + "'");
            //根据请假天数，判断审批流程
            // 除各部门、各科室负责人以外的工作人员请假由所在部门负责人及所在部门分管领导批准，超过 3 天且在 5天以内的，由人事分管领导批准;5 天以上的，由局长批准
            //20251011现在请假不用区分天数，
            //组长——部门负责人——人事——分管领导——中心负责人 
            //副组长或组员——组长——人事——分管领导
            //非在编只需要部门负责人审批
            if (jieDianName == "部门主管审批")
            {
                switch (strJiaoSe)
                {
                    case "一般工作人员（人事管理）":
                        if (strzgzt != "在编")
                        {
                            BindEndNode(workFlowId);
                        }
                        break;
                }
            }
            else if (jieDianName == "中心分管领导审批")//部门领导请假5天以内，都需要局分管领导审批
            {
                if (QJModel.QJR == "蔡晓帆")
                {
                    BindEndNode(workFlowId);
                    return;
                }

                if (!strJiaoSe.Contains("单位领导") && (bm != null && bm.ChargeMan != QJModel.QJR || !strJiaoSe.Contains("部门领导")))
                {
                    BindEndNode(workFlowId);
                    return;
                }
            }
            else if (jieDianName == "中心负责人审批")
            {
                BindEndNode(workFlowId);
            }
            //if (QingJiaNum <= 2)
            //{
            //    if (jieDianName == "部门主管审批")//一般工作人员请假2天以内，需要部门主管审批
            //    {
            //        switch (strJiaoSe)
            //        {
            //            case "一般工作人员（人事管理）":
            //                if (strzgzt != "在编")
            //                {
            //                    BindEndNode(workFlowId);
            //                    //Qingjiaconfirm(todoModel.ID);
            //                    //DoEnd();
            //                }
            //                break;
            //        }
            //    }
            //    else if (jieDianName == "人事科审批")//机关科室职工请假2天以内，需要人事科审批
            //    {

            //    }
            //    else if (jieDianName == "中心分管领导审批")//部门领导请假5天以内，都需要局分管领导审批
            //    {
            //        if (QJModel.QJR == "蔡晓帆")
            //        {
            //            BindEndNode(workFlowId);
            //            return;
            //        }
            //        switch (strJiaoSe)
            //        {
            //            //部门领导请假都需要局长审批
            //            case "部门领导（人事管理）":
            //                break;
            //            default:
            //                BindEndNode(workFlowId);
            //                break;
            //        }
            //    }
            //    else if (jieDianName == "中心负责人审批")//部门领导请假5天以内，都需要局分管领导审批
            //    {
            //        BindEndNode(workFlowId);
            //    }
            //}
            //else if (QingJiaNum <= 5 && QingJiaNum > 2)//所有人请假2至5天，都需要局分管领导审批
            //{
            //    if (strJiaoSe == "单位领导")
            //    {
            //        if (jieDianName == "人事科审批")
            //        {

            //        }
            //        else if (jieDianName == "中心分管领导审批")
            //        {
            //            if (QJModel.QJR == "蔡晓帆")
            //            {
            //                BindEndNode(workFlowId);
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else if (jieDianName == "人事分管领导")
            //        {

            //        }
            //        else if (jieDianName == "中心负责人审批")
            //        {
            //            BindEndNode(workFlowId);
            //        }
            //    }
            //    else
            //    {
            //        if (jieDianName == "部门主管审批")
            //        {
            //            if (strzgzt != "在编")//合同制员工只需部门负责人审批，不需要到人事科
            //            {
            //                BindEndNode(workFlowId);
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else if (jieDianName == "人事科审批")
            //        {

            //        }
            //        else if (jieDianName == "中心分管领导审批")
            //        {

            //        }
            //        else if (jieDianName == "人事分管领导")
            //        {
            //            if (!strJiaoSe.Contains("单位领导") && !strJiaoSe.Contains("部门领导（人事管理）"))
            //            {
            //                BindEndNode(workFlowId);
            //            }
            //        }
            //        else if (jieDianName == "中心负责人审批审批")
            //        {

            //        }
            //    }

            //}
            //else if (QingJiaNum > 5)
            //{
            //    if (strzgzt != "在编")//合同制员工只需部门负责人审批，不需要到人事科
            //    {
            //        BindEndNode(workFlowId);
            //    }
            //    else
            //    {

            //    }
            //}

        }
        else if (cFormId == 78)
        {
            //针对出差审批判断交通工具是否飞机，不是则提前结束流程
            var CCModel = new ZWL.BLL.ERPChuChai();
            if (todoModel.WorkName.Contains("出差变更审批流程"))
            {
                CCModel.GetNWorkModel(Convert.ToInt32(todoModel.BeiYong2));
            }
            else
            {
                CCModel.GetNWorkModel(todoModel.ID);
            }
            var user = new ZWL.BLL.ERPUser();
            var usermodel = user.GetModel(" UserName ='" + CCModel.SQR + "'");
            var zaibian = usermodel.ZaiGang;
            if (IsYiqing)
            {
                if (!CCModel.ChuChaiDiDian.Contains("广东省"))
                {
                    if (jieDianName == "领导审批")
                    {
                        if (todoModel.ShenPiUserList.ToLower().Contains("蔡晓帆"))
                        {
                            BindEndNode(workFlowId);
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    BindEndNode(workFlowId);
                }
            }
            else
            {
                if (CCModel.SQR == "韩梅" && todoModel.JieDianName == "领导审批")
                {
                    this.SelectedNextNode.SelectedIndex = 1;
                }
                else if (CCModel.SQR == "韩梅" && todoModel.JieDianName == "分管领导审批")
                {
                    //20250714 朱月 在编需要人事科签名
                }
                else if (CCModel.JiaoTongGJ == "飞机")
                {
                    if (jieDianName == "领导审批")
                    {
                        if (todoModel.ShenPiUserList.ToLower().Contains("蔡晓帆"))
                        {
                            BindEndNode(workFlowId);
                        }
                        else
                        {

                        }
                    }
                    else if (jieDianName == "乘坐飞机单位负责人审批")
                    {
                        BindEndNode(workFlowId);
                    }
                }
                else if (zaibian == "在编" && todoModel.JieDianName == "领导审批")
                {
                    //20250714 朱月 在编需要人事科签名
                    this.SelectedNextNode.SelectedIndex = 2;
                }
                else
                {
                    BindEndNode(workFlowId);
                }
            }
        }
        else if (cFormId == 51)
        {
            BindEndNode(workFlowId);
        }
        else if (cFormId == 77)
        {
            //针对外出活动申请判断是否存在下一节点，没有则提前结束流程
            ZWL.BLL.ERPWaiChuHuoDong WCModel = new ZWL.BLL.ERPWaiChuHuoDong();
            WCModel.GetNWorkModel(todoModel.ID);

            if (jieDianName == "中心负责人意见")
            {
                BindEndNode(workFlowId);
            }
            else
            {

            }
        }
        else if (cFormId == 112)
        {
            if (jieDianName == "分管领导意见")
            {
                var parser = new ZWL.Common.ParseHtml();
                parser.GetAttListFormHTMLinput(formContent);
                var outType = parser.getValue("外出类型");
                if (outType == "出市")
                {
                    BindEndNode(workFlowId);
                }
            }
        }
        else
        {

        }
        #endregion

        //如果是投标审批、单位分管领导审批、审批人是李锋，资质单位是勘察院，直接保存并结束该工作流程--20160827lrn
        #region 经营管理
        var fsdzjComp = "广东省佛山地质局";
        if (cFormId == 39)
        {
            if (!jieDianName.Contains("部门负责人"))
            {
                var tmodel = new ZWL.BLL.ERPTouBiao();
                tmodel = tmodel.GetModelByWorkId(nWorkId);
                var tbbj = tmodel.TBBJ;
                if (jieDianName.Contains("经营科"))
                {
                    if (tbbj < 50 * 10000)
                    {
                        BindEndNode(workFlowId);
                    }
                }
                else if (jieDianName.Contains("分管领导"))
                {
                    if (tbbj >= 50 * 10000 && (tbbj < 300 * 10000 || tbbj >= 300 * 10000))
                        BindEndNode(workFlowId);
                }
            }
            if (todoModel.StateNow == "正常结束")
            {
                BindEndNode(workFlowId);
            }
        }

        if (cFormId == 43 && jieDianName == "经营科负责人审核")
        {
            this.CheckAutoNextNode.Text += " (【工程勘查和测绘项目合同金额在50万元以上、施工项目合同金额100万元以上、重大地质勘查项目合同等请选择分管领导审批，否则请选择合同归档】)";
            this.CheckAutoNextNode.ForeColor = System.Drawing.Color.Red;
            this.CheckAutoNextNode.Font.Bold = true;
        }
        string strkpfs = "", strskzh = "";

        if (cFormId == 46 && jieDianName == "部门负责人审核")
        {
            this.CheckAutoNextNode.Text += " (【请选择开票审核】)";
            this.CheckAutoNextNode.ForeColor = System.Drawing.Color.Red;
            this.CheckAutoNextNode.Font.Bold = true;
            ParseHtml parse = new ParseHtml();
            if (formContent != "")
            {
                parse.GetAttListFormHTMLall(formContent);
                strkpfs = parse.getValue("开票方式");
                strskzh = parse.getValue("收款账户");
            }
        }
        //合同签订评审
        var stryfdw = string.Empty;
        if (cFormId == 43)
        {
            if (!jieDianName.Contains("部门负责人"))
            {
                var hmodel = new ZWL.BLL.ERPHeTong();
                hmodel.GetModelByWorkId(nWorkId);
                var htje = hmodel.HTJE;
                var CDDW = hmodel.HTLB == "收款" ? hmodel.YFDW : hmodel.JFDW;
                stryfdw = CDDW;
                if (jieDianName.Contains("经营科"))
                {
                    if (htje < 50 * 10000)
                    {
                        var lastnode = GetLastNode(workFlowId);
                        if (lastnode != null)
                        {
                            var selectedItem = SelectedNextNode.Items.FindByValue(lastnode.ID.ToString());
                            if (selectedItem != null)
                                this.SelectedNextNode.SelectedValue = selectedItem.Value;
                        }
                    }
                    else
                    {
                        if (CDDW == fsdzjComp || CDDW == "广东省地质局佛山地质调查中心")
                        {
                            //if (tbbj >= 50 * 10000 && tbbj < 300 * 10000)
                            //{
                            //}
                            this.SelectedNextNode.SelectedIndex = SelectedNextNode.Items.Count - 1;//the last node
                        }
                        else
                        {
                            //var bm = new ZWL.BLL.ERPBuMen();
                            //bm.GetModel(131);
                            //var kcyfzr = bm.ChargeMan;
                            //if (tbbj >= 50 * 10000 && tbbj < 300 * 10000)
                            this.SelectedNextNode.SelectedIndex = 1;
                        }
                    }
                }
                else if (jieDianName.Contains("分管领导"))
                {
                    if (htje >= 50 * 10000 && (htje < 300 * 10000 || htje >= 300 * 10000))
                    {
                        var lastnode = GetLastNode(workFlowId);
                        if (lastnode != null)
                        {
                            var selectedItem = SelectedNextNode.Items.FindByValue(lastnode.ID.ToString());
                            if (selectedItem != null)
                                this.SelectedNextNode.SelectedValue = selectedItem.Value;
                        }
                    }
                }
                //else if (jieDianName.Contains("院长"))
                //{
                //    if ((htje >= 50 * 10000 && htje < 300 * 10000))
                //    {
                //        var lastnode = GetLastNode(workFlowId);
                //        if (lastnode != null)
                //        {
                //            var selectedItem = SelectedNextNode.Items.FindByValue(lastnode.ID.ToString());
                //            if (selectedItem != null)
                //                this.SelectedNextNode.SelectedValue = selectedItem.Value;
                //        }
                //    }
                //    else
                //    {
                //        var selectedItem = SelectedNextNode.Items.FindByValue("416".ToString());
                //        if (selectedItem != null)
                //            this.SelectedNextNode.SelectedValue = selectedItem.Value;
                //    }
                //}
            }
            if (todoModel.StateNow == "正常结束")
            {
                BindEndNode(workFlowId);
            }
        }
        if (cFormId == 113)
        {
            decimal amount = 0;
            var pmodel = new ZWL.BLL.ERPXMCJJCPG();
            pmodel = pmodel.GetModelByNWorkId<ZWL.BLL.ERPXMCJJCPG>(todoModel.ID);
            if (pmodel != null) amount = pmodel.Amount;
            if (amount >= 50 * 10000)
            {
                if ((amount < 300 * 10000 || amount >= 300 * 10000) && jieDianName == "中心分管领导")
                {
                    BindEndNode(workFlowId);
                }
            }
            else
            {
                if (jieDianName == "经营管理科")
                {
                    BindEndNode(workFlowId);
                }
            }
        }
        //判断 当前节点信息（全部通过可向下流转、一人通过可向下流转）  该工作的通过人列表信息、审批人列表信息
        string SPMoShi = DbHelperSQL.GetSHSL("select top 1 PSType from ERPNWorkFlowNode where ID=" + todoModel.JieDianID.ToString());
        if (Util.CheCkIfOk(todoModel.OKUserList, todoModel.ShenPiUserList, SPMoShi))
        {
            //绑定下一个节点
            try
            {
                //绑定下一节点  正常状态
                //绑定下一节点
                //根据开票方式选择性的绑定下一节点
                if (cFormId == 46 && jieDianName == "部门负责人审核")//合同收款审批
                {

                    if (!string.IsNullOrEmpty(strkpfs))
                    {
                        this.SelectedNextNode.SelectedIndex = 0;
                    }
                }
                if (cFormId == 43 && jieDianName == "部门负责人审核")//合同收款审批
                {
                    var list = Util.GetContractWithoutProject();
                    var ht = new ZWL.BLL.ERPHeTong();
                    ht = ht.GetModel("NWorkToDoID='" + todoModel.ID + "'");
                    if (list.Any(r => r.Value1.Contains(ht.ZYLB)))//是否为购销,租赁合同
                    {
                        if (SelectedNextNode.Items.Count > 1)
                        {
                            SelectedNextNode.Items.RemoveAt(0);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(stryfdw))
                        {
                            if (stryfdw.Equals("广东省佛山地质局") || stryfdw.Equals("广东省地质局佛山地质调查中心"))
                            {
                                //广东省佛山地质局，下一节点为总工办人审核
                                this.SelectedNextNode.SelectedIndex = 0;
                            }
                            else
                            {
                                this.SelectedNextNode.SelectedIndex = 1;
                            }
                        }
                    }
                }
            }
            catch
            {
                //清除根据条件决定下一节点选项
                this.CheckAutoNextNode.Checked = false;
                this.CheckAutoNextNode.Enabled = false;


                DataSet MyDS = DbHelperSQL.GetDataSet("select ID,NodeSerils,NodeName from ERPNWorkFlowNode where WorkFlowID=" + todoModel.WorkFlowID.ToString());
                for (int jjj = 0; jjj < MyDS.Tables[0].Rows.Count; jjj++)
                {
                    ListItem MyItem = new ListItem();
                    MyItem.Value = MyDS.Tables[0].Rows[jjj]["ID"].ToString();
                    MyItem.Text = "节点序号：" + MyDS.Tables[0].Rows[jjj]["NodeSerils"].ToString() + "--节点名称：" + MyDS.Tables[0].Rows[jjj]["NodeName"].ToString();
                    if (MyItem.Value.ToString().Length > 0)
                    {
                        this.SelectedNextNode.Items.Add(MyItem);
                    }
                }
            }
        }
        #endregion

        #region 项目管理
        if (cFormId == 71)
        {
            if (todoModel.JieDianName.EndsWith("审定人审定成果报告"))
            {
                var xModel = new ZWL.BLL.ERPXMJBXX();
                xModel = xModel.GetModelByXMBH(todoModel.Number);
                if (xModel != null && xModel.ZYLB != "岩土工程勘察")
                {
                    BindEndNode(todoModel.WorkFlowID);
                }
            }
        }
        #endregion
    }
    #endregion


    #region MyRegion

    public void BanLiWork()
    {
        var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var XMyModel = new ZWL.BLL.ERPNWorkToDo();
        XMyModel.GetModel(workid);

        //初始化
        int m_formid = PublicMethod.GetInt(XMyModel.FormID.ToString());
        int m_workflowid = PublicMethod.GetInt(XMyModel.WorkFlowID.ToString());
        string beiyong1 = XMyModel.BeiYong1;
        string XJieDianIDStr = "0";
        string XJieDianNameStr = "";
        string XShenPiRenListStr = "";
        string XTongGuoRenListStr = "";
        string XStateNowStr = "正在办理";

        string JingBanRenStr = "";//条件判断时读取经办人
        try
        {
            if (CheckAutoNextNode.Checked)
            {
                //条件未找到或者没有匹配项时，采用选择好的节点
                XJieDianIDStr = this.SelectedNextNode.SelectedValue.ToString();
                if (PublicMethod.GetInt(XJieDianIDStr) == 0 || SelectedNextNode.SelectedItem.Text.Contains("结束"))
                {
                    XStateNowStr = "正常结束";
                    XJieDianIDStr = XMyModel.JieDianID.ToString();
                    GetDetailsData();//写入明细表
                    XJieDianNameStr = "结束";

                    string formidlist = "62,108,109,71,56,75";
                    if (formidlist.Contains(m_formid.ToString()))
                    {
                        string xmbh = beiyong1.Split('@')[0];
                        string xmname = beiyong1.Split('@')[1];
                        //流程结束的时候，变更项目子流程的状态
                        ZWL.BLL.SubWorkFlowState subwork = new ZWL.BLL.SubWorkFlowState();
                        subwork.GetModelByBH(xmbh);//项目基本信息审核流程，用来在待办工作，指定节点时判断子流程状态。
                        switch (m_formid.ToString())
                        {
                            case ("62"):
                                subwork.KGAQ62 = 1;
                                break;
                            case ("108"):
                                subwork.GWHD108 = 1;
                                break;
                            case ("109"):
                                subwork.ZRGZS109 = 1;
                                break;
                            case ("71"):
                                subwork.CG71 = 1;
                                break;
                            case ("56"):
                                subwork.SJSC56 = 1;
                                break;
                            case ("75"):
                                subwork.SJSC75 = 1;
                                break;
                            default:
                                break;
                        }
                        subwork.Update();
                    }

                }
                else
                {
                    try
                    {
                        XJieDianIDStr = this.SelectedNextNode.SelectedValue.ToString();
                    }
                    catch
                    {
                        ///////////根据条件获取下一审批节点信息                    
                        XJieDianIDStr = CheckCondition(XMyModel.FormContent, m_workflowid, XMyModel.JieDianID.Value).ToString();
                    }
                }
            }
            else
            {
                XJieDianIDStr = this.SelectedNextNode.SelectedValue.ToString();
            }
            if (XJieDianNameStr != "结束")
                XJieDianNameStr = DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + XJieDianIDStr);
        }
        catch
        {
            if (DbHelperSQL.GetSHSL("select NodeAddr from ERPNWorkFlowNode where ID=" + XMyModel.JieDianID.ToString()) == "结束")
            {
                XStateNowStr = "正常结束";
                XJieDianIDStr = XMyModel.JieDianID.ToString();
                GetDetailsData();//写入明细表
            }
            else
            {
                XStateNowStr = "强制结束";
                XJieDianIDStr = XMyModel.JieDianID.ToString();
                GetDetailsData();//写入明细表
            }
            XJieDianNameStr = "结束";
        }
        if (JingBanRenStr == "")
        {
            XShenPiRenListStr = PublicMethod.WorkWeiTuoUserList(ShenPiUser.Text.Replace("，", ",").Replace("、", ","));
        }
        else
        {
            XShenPiRenListStr = PublicMethod.WorkWeiTuoUserList(JingBanRenStr);
        }
        if (XShenPiRenListStr.Trim().Length <= 0 || XShenPiRenListStr == "默认")
        {
            XShenPiRenListStr = "工作已办结";
        }
        XTongGuoRenListStr = "默认";
        if (XMyModel.FormID == 46)
        {
            if (XMyModel.JieDianName == "开票审核" || XMyModel.JieDianName == "经营科负责人审核")
            {
                XStateNowStr = "正在办理，已开票";
            }
        }
        //执行update语句
        var lastTime = DateTime.Now.AddHours(double.Parse(DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + XJieDianIDStr))).ToString();
        DbHelperSQL.ExecuteSQL("update ERPNWorkToDo set LateTime='" + lastTime + "',JieDianID=" + XJieDianIDStr
            + ",JieDianName='" + XJieDianNameStr + "',ShenPiUserList='" + XShenPiRenListStr.Replace("，", ",") + "',OKUserList='"
            + XTongGuoRenListStr + "',StateNow='" + XStateNowStr + "' where ID=" + workid);
        XMyModel.GetModel(workid);
        //合同收款财务科出纳节点郑晓波不想接收短信
        if (XMyModel.FormID == 46 && this.ShenPiUser.Text.Trim() == "郑晓波" && XJieDianNameStr == "财务科出纳")
        {
            CHKMOB.Checked = false;
        }
        //经营科审批的时候罗鑫说经营科不需要接收到短信
        if (this.ShenPiUser.Text.Trim().Contains("罗鑫") && (XJieDianNameStr.Contains("经营科") || XJieDianNameStr.Contains("经管科")))
        {
            CHKMOB.Checked = false;
        }

        if (XStateNowStr.Contains("正在办理"))
        {
            //经营发送进度短信
            Mobile.SendSMS("系统消息", XMyModel.UserName, string.Format("您有工作[{0}]通过[{1}]审核进入下一节点[{2}]！", XMyModel.WorkName, UserName, XJieDianNameStr));
            //发送短信
            SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + XMyModel.WorkName + ")", PublicMethod.WorkWeiTuoUserList(this.ShenPiUser.Text.Trim()), m_formid, m_workflowid, beiyong1, XMyModel.ID);
        }
        else if (XStateNowStr == "正常结束")
        {
            //发短信给发起人
            CHKMOB.Checked = true;
            //车辆使用申请管理 工作结束的时候，发送短信通知申请人//只需要给申请人发送通知20231106，by mark
            if (m_formid == 99 && XJieDianNameStr == "结束")
            {
                string ContentStr = "您的用车申请已通过审批！（" + XMyModel.WorkName + "）";
                //发送手机信息
                Mobile.SendSMS("系统消息", PublicMethod.WorkWeiTuoUserList(XMyModel.UserName), ContentStr);
            }
            else
            {
                SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您的工作已经正常结束！(" + XMyModel.WorkName + ")", PublicMethod.WorkWeiTuoUserList(XMyModel.UserName), m_formid, m_workflowid, beiyong1, XMyModel.ID);
            }
        }
        else
        {
            //发短信给发起人
            CHKMOB.Checked = true;
            //SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您的工作已经被强制结束！(" + XMyModel.WorkName + ")", XMyModel.UserName);
            SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您的工作已经被强制结束！(" + XMyModel.WorkName + ")", PublicMethod.WorkWeiTuoUserList(XMyModel.UserName), m_formid, m_workflowid, beiyong1, XMyModel.ID);

        }
        DoExtendSomething(XMyModel);
        //写系统日志
        WriteLog("用户办理工作(" + XMyModel.WorkName + ")");
    }
    private void DoExtendSomething(ZWL.BLL.ERPNWorkToDo todoModel)
    {
        if (todoModel == null)
        {
            return;
        }
        if (todoModel.FormID == 86)
        {
            var stateNow = todoModel.StateNow;
            var sql = string.Format("select * from [ERPNWorkFlowNode] where WorkFlowID={0} and NextNode =(select NodeSerils from [ERPNWorkFlowNode] where WorkFlowID={0} and NodeAddr='结束')", todoModel.WorkFlowID);
            var workflowNodeId = DbHelperSQL.GetSHSLInt1(sql);
            if (stateNow == "正常结束")
            {
                var model = new ZWL.BLL.AptitudeWork().GetModel(" [NWorkID] = " + todoModel.ID);
                if (model != null)
                {
                    var parse = new ParseHtml();
                    string strzzdw = "";
                    if (todoModel.FormContent != "")
                    {
                        parse.GetAttListFormHTMLall(todoModel.FormContent);
                        strzzdw = parse.getValue("是否归还资质");
                    }

                    model.State = strzzdw == "是" ? ((int)AptitudeState.Returned) : ((int)AptitudeState.Using);
                    if (strzzdw == "是")
                    {
                        model.ReturnDate = DateTime.Now;
                        model.Update();

                        var workDetail = new ZWL.BLL.AptitudeWorkDetail();
                        var workDetailList = workDetail.GetModelList(" [AptWorkID] = " + model.ID);

                        foreach (var item in workDetailList)
                        {
                            item.State = (int)AptitudeState.Returned;
                            item.Update();

                            var workFileState = new ZWL.BLL.AptitudeFileState();
                            workFileState.GetModel(item.AptFileStateID);
                            workFileState.ID = item.AptFileStateID;

                            workFileState.State = (int)AptitudeState.Returned;
                            workFileState.Update();
                        }

                    }

                }
            }
            else if (todoModel.JieDianID.HasValue && todoModel.JieDianID == workflowNodeId)
            {
                var model = new ZWL.BLL.AptitudeWork().GetModel(" [NWorkID] = " + todoModel.ID);
                if (model != null)
                {
                    model.State = (int)AptitudeState.Using;
                    model.Update();

                    var workDetail = new ZWL.BLL.AptitudeWorkDetail();
                    var workDetailList = workDetail.GetModelList(" [AptWorkID] = " + model.ID);

                    foreach (var item in workDetailList)
                    {
                        item.State = (int)AptitudeState.Using;
                        item.Update();

                        var workFileState = new ZWL.BLL.AptitudeFileState();
                        workFileState.GetModel(item.AptFileStateID);
                        workFileState.ID = item.AptFileStateID;

                        workFileState.State = (int)AptitudeState.Using;
                        workFileState.Update();
                    }

                }
            }
        }
        if (todoModel.StateNow.Contains("正常结束"))
        {
            if (todoModel.FormID == 95)
            {
                AutoCreateYinZhang(todoModel.ID, 81, 73, "测量报告");
            }
            else if (todoModel.FormID == 47)
            {
                var detail = new ZWL.BLL.ERPHeTongJieYueDetail();
                var list = detail.GetListModelByWorkId(Id);
                foreach (var item in list)
                {
                    var hModel = item.CurrentHeTong;
                    hModel.HTJYState = "已借出";
                    hModel.Update();
                    item.LendDate = DateTime.Now;
                    item.Update();
                }
            }
            else if (todoModel.FormID == 44)
            {
                var ver = new ZWL.BLL.ERPHeTongVersion();
                ver.GetModelByWorkId(todoModel.ID);
                if (ver.ID > 0)
                {
                    var ht = new ZWL.BLL.ERPHeTong();
                    ht.GetModel(ver.RecordID);
                    if (ht != null)
                    {
                        var id = ht.ID;
                        var nworkid = ht.NWorkToDoID;
                        var htzt = ht.HTZT;
                        ht = Util.ConverToTEntity<ZWL.BLL.ERPHeTong>(ver);
                        ht.ID = id;
                        ht.XMID = ver.XMID;
                        ht.XMName = ver.XMName;
                        ht.NWorkToDoID = nworkid;
                        ht.HTZT = htzt;
                        ht.Update();

                        var dWork = new ZWL.BLL.ERPNWorkToDo();
                        dWork.GetModel(nworkid);
                        dWork.FuJianList = ver.CurrentWorkToDo.FuJianList;
                        dWork.BeiYong1 = ver.HTID + "@" + ver.HTName;
                        dWork.BeiYong2 = ver.Adress;

                        var parser = new ZWL.Common.ParseHtml();
                        parser.GetAttListFormHTMLtextarea(ver.CurrentWorkToDo.FormContent);
                        var remark = parser.getValue("其他说明");

                        var baseContent = GetFilledFormContent(dWork.FormID, ht);

                        var content = GetHeTongContentHtml(baseContent, ht);
                        var newHtml = Regex.Split(content, "<!--split-->", RegexOptions.IgnoreCase);

                        var oldHtml = Regex.Split(dWork.FormContent, "<!--split-->", RegexOptions.IgnoreCase);

                        var resultHtml = newHtml.FirstOrDefault() + "<!--split-->" + oldHtml.LastOrDefault();

                        parser = new ZWL.Common.ParseHtml(resultHtml);
                        parser.SetInputValue("其他说明", remark);

                        dWork.FormContent = parser.GetOuterHtml();
                        dWork.Update();

                    }
                }
            }
            else if (todoModel.FormID == 71 || todoModel.FormID == 114)
            {
                var xModel = new ZWL.BLL.ERPXMJBXX();
                xModel = xModel.GetModelByXMBH(todoModel.Number);
                if (xModel != null && xModel.ID > 0)
                {
                    //保存项目成果
                    UpdateXMResult(todoModel.FormContent);
                    xModel.SHState = "已审核";
                    xModel.SHTime = Timestamp;
                    xModel.Update();
                    //更新合同里的项目状态
                    DbHelperSQL.ExecuteSQL("update [ERPHeTong] set XMZT='已完成' where XMID='" + todoModel.Number + "'");
                }
            }
            else if (todoModel.FormID == 78)
            {
                if (todoModel.WorkName.Contains("出差变更审批流程"))
                {
                    return;
                }
                //审批完成后将出差情况同步到用餐明细
                var chuModel = new ZWL.BLL.ERPChuChai();
                chuModel.GetNWorkModel(todoModel.ID);
                var cc = chuModel.SQR;
                if (!string.IsNullOrEmpty(chuModel.TongXingRenYuan) && chuModel.TongXingRenYuan != "无")
                {
                    cc += "," + Util.ReplaceSymbolsWithComma(chuModel.TongXingRenYuan);
                }
                var reportSql = @"SELECT * from FanTangJiuCanRecordReport where LotID in (
                                    SELECT LotID from Flow where DataTable='FanTangJiuCanRecord' and Operation=1 
                                    ) and (RecordDate>='{0}' and RecordDate<='{1}') and Name in ({2})".FormatWith(chuModel.ChuChaiStart.Date, chuModel.ChuChaiEnd.Date, PublicMethod.GetSqlInWhere(cc));
                var reportList = Conv<ZWL.BLL.FanTangJiuCanRecordReport>.GetList(reportSql);
                if (reportList != null && reportList.Any())
                {
                    foreach (var item in reportList)
                    {
                        if (CheckCanEditReceived(item.RecordDate.Value))
                        {
                            if (item.IsChuChai != "是")
                                item.IsChuChai = "是";
                            item.ChuChaiDiDian = (item.ChuChaiDiDian.IsNullOrEmpty() ? "" : ",") + chuModel.ChuChaiDiDian;
                            item.ChuChaiNWorkID = (item.ChuChaiNWorkID.IsNullOrEmpty() ? "" : ",") + chuModel.NWorkID;
                            if ((item.ZaoCan != "-" || item.WuCan != "-") && item.Received != "否")
                            {
                                item.Received = "是";
                            }
                            item.Update();
                        }
                    }
                }
            }
        }
        ExcuteMoreWork(todoModel);
    }

    private void ExcuteMoreWork(ZWL.BLL.ERPNWorkToDo todoModel)///公文
    {
        if (todoModel != null)
        {
            //初始化
            int m_formid = PublicMethod.GetInt(todoModel.FormID.ToString());
            int m_workflowid = PublicMethod.GetInt(todoModel.WorkFlowID.ToString());

            if (m_formid == 90 && todoModel.JieDianName == "结束")
            {
                var officeModel = new ZWL.BLL.ERPOfficeSupply().GetModelBySqlWhere(" NWorkID ='" + todoModel.ID + "'");
                var shots = EditShot(officeModel);
                officeModel.State = "1";
                officeModel.Update();
                EditLog(shots, officeModel);
                //var subItems = officeModel.SubItems.Where(x => !x.DeleteMark.HasValue || x.DeleteMark != 1);
                //foreach (var item in subItems)
                //{
                //    var tshots = EditShot(item);
                //    item.State = "2";
                //    item.Update();
                //    EditLog(tshots, item);
                //}
            }

            if (m_formid == 116 && todoModel.JieDianName == "结束")
            {
                var officeModel = new ZWL.BLL.ERPOfficeSupply().GetModelBySqlWhere(" NWorkID ='" + todoModel.ID + "'");
                var shots = EditShot(officeModel);
                officeModel.State = "1";
                officeModel.Update();
                EditLog(shots, officeModel);
                var subItems = officeModel.SubItems.Where(x => !x.DeleteMark.HasValue || x.DeleteMark != 1);
                foreach (var item in subItems)
                {
                    var tshots = EditShot(item);
                    item.State = "1";
                    item.Update();
                    EditLog(tshots, item);
                }
            }
            if (m_formid == 117 && todoModel.JieDianName == "结束")
            {
                var outsql = "select * from ERPOfficeSupplyStockOut where NWorkID ='{0}'".FormatWith(todoModel.ID);
                var officeModel = Conv<ZWL.BLL.ERPOfficeSupplyStockOut>.GetModel(outsql);
                if (officeModel != null)
                {
                    var itemsql = @"select * from ERPOfficeSupplyStockOutDetail where StockOutID={0} and (DeleteMark is null or DeleteMark<>1)".FormatWith(officeModel.ID);
                    var outItems = Conv<ZWL.BLL.ERPOfficeSupplyStockOutDetail>.GetList(itemsql);
                    if (outItems != null && outItems.Any())
                    {
                        foreach (var item in outItems)
                        {
                            var invMdoel = new ZWL.BLL.ERPOfficeSupplyInventory();
                            invMdoel.GetModel(item.ItemID.Value);
                            if (invMdoel.ID > 0)
                            {
                                var invShots = EditShot(invMdoel);
                                invMdoel.Quantity -= item.Quantity;
                                invMdoel.LockedOutQuantity -= item.Quantity;
                                invMdoel.Update();
                                EditLog(invShots, invMdoel);
                            }
                        }
                    }
                }
            }
            if (m_formid == 99 && m_workflowid == 92 && todoModel.JieDianName == "结束")
            {
                var model = new ZWL.BLL.ERPCarUseSP();
                model.GetNWorkModel(todoModel.ID);
                //车辆使用审批单完成的时候
                //var shortmessage = string.Format("{0}_{1}申请使用车辆,人数{2},自驾({3}),时间{4}至{5},目的地{6}", model.SQBM, model.SQR, model.SYRS, model.SFZJ, Convert.ToDateTime(model.SYTime).ToString("yyyy-MM-dd HH:mm"), Convert.ToDateTime(model.SYTime2).ToString("yyyy-MM-dd HH:mm"), model.Adress);
                var shortmessage = string.Format("{0}_{1}申请使用车辆,人数{2},自驾({3}),时间{4}至{5},目的地{6}", model.SQBM, model.SQR, model.SYRS, (string.IsNullOrEmpty(model.SFZJ) ? "否" : model.SFZJ), Convert.ToDateTime(model.SYTime).ToString("yyyy-MM-dd HH:mm"), Convert.ToDateTime(model.SYTime2).ToString("yyyy-MM-dd HH:mm"), model.Adress);
                Mobile.SendSMS("系统消息", "李向军", shortmessage);
            }
        }
    }
    private bool CheckCanEditReceived(DateTime recordDate)
    {
        DateTime today = DateTime.Today;
        int currentMonth = today.Month;
        int currentYear = today.Year;
        int currentDay = today.Day;

        //2024年8月5日前(即当前时间至8月5日)可对6月到7月数据责敌，之每月1-5
        //日对前一个月数据更改，其他时间段的数据不能
        var startDate = new DateTime(2024, 8, 5);
        if (today <= startDate)
        {
            // 2024年8月特殊处理  
            // 如果需要编辑的是6月或7月的数据，则返回true  
            return (recordDate.Year == startDate.Year) && (recordDate.Month == 6 || recordDate.Month == 7);
        }
        else
        {
            // 检查是否是每月的1至5日  
            if (currentDay >= 1 && currentDay <= 5)
            {
                // 获取传入日期的月份和年份  
                int recordMonth = recordDate.Month;
                int recordYear = recordDate.Year;

                // 检查传入的日期是否是上个月的  
                DateTime lastMonth = today.AddMonths(-1);
                int lastMonthYear = lastMonth.Year;
                int lastMonthMonth = lastMonth.Month;

                // 如果传入的日期是上个月的，则返回true  
                return recordYear == lastMonthYear && recordMonth == lastMonthMonth;
            }
        }
        return false; // 默认不开放编辑权限  
    }
    /// <summary>
    /// 将表单中个数据列的数据写入明细记录表中，便于后期统计
    /// </summary>
    public void GetDetailsData()
    {
        var workid = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var MyModel = new ZWL.BLL.ERPNWorkToDo();
        MyModel.GetModel(workid);
        var parser = new ZWL.Common.ParseHtml(MyModel.FormContent);
        //获取当前表单对应的工作数据列
        string[] FormItemList = DbHelperSQL.GetSHSL("select top 1 ItemsList from ERPNForm where ID=" + MyModel.FormID.ToString()).Split('|');

        for (int i = 0; i < FormItemList.Length; i++)
        {
            if (FormItemList[i].Trim().Length > 0)
            {
                try
                {
                    var Model = new ZWL.BLL.ERPNWorkDetails();
                    Model.WorkID = workid;
                    Model.ItemsNameCN = FormItemList[i].Split('_')[1];
                    Model.ItemsNameEn = FormItemList[i].Split('_')[0];
                    var requestValue = Request.Form[Model.ItemsNameEn];
                    if (requestValue == null)
                    {
                        requestValue = parser.getValue(Model.ItemsNameCN);
                    }
                    Model.ItemsValue = requestValue;

                    Model.Add();
                }
                catch { }
            }
        }
    }
    public int CheckCondition(string formContent, int workflowid, int nodeid)
    {
        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where ID=" + nodeid.ToString()).Split('|');
        for (int i = 0; i < TiaoJianList.Length; i++)
        {
            if (TiaoJianList[i].Trim().Length > 0)
            {
                string NextIDStr = CheckTiaoJian(formContent, workflowid, TiaoJianList[i].ToString());
                if (NextIDStr != "0")
                {
                    return PublicMethod.GetInt(NextIDStr);
                }
            }
        }
        return 0;
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
            else if (BiJiaoTiaoJian == "包含" && PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (PublicMethod.StrIFIn(Str2, Str1))
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
            else if (BiJiaoTiaoJian == "包含" && PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (PublicMethod.StrIFIn(Str2, Str1))
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

    /// <summary>
    /// 判断条件，返回下一节点ID
    /// </summary>
    /// <param name="FormCcontent"></param>
    /// <param name="TiaoJianStr"></param>
    /// <param name="WorkFlowIDStr"></param>
    /// <returns></returns>
    protected string CheckTiaoJian(string formContent, int workflowid, string TiaoJianStr)
    {
        //条件格式如：ABCD_请假天数→大于→10→3        
        string ZiDuanStrEN = TiaoJianStr.Split('_')[0]; //字段名称EN 如：ABCD        
        string ZiDuanStrCN = TiaoJianStr.Split('→')[0].Split('_')[1]; //字段名称CN 如：请假天数        
        string BiJiaoStr = TiaoJianStr.Split('→')[1]; //条件比较  如：大于
        string ZhiStr = TiaoJianStr.Split('→')[2];//比较的值，如： 10
        string JieDianXuHaoStr = TiaoJianStr.Split('→')[3];//跳转的节点序号，如： 3

        string NowValue = "";
        try
        {
            var parser = new ZWL.Common.ParseHtml();
            parser.GetAttListFormHTMLall(formContent);
            NowValue = parser.getValue(ZiDuanStrCN).ToString();
        }
        catch
        { }

        if (BiaoJiaoTwoStr(NowValue, ZhiStr, BiJiaoStr) == true)
        {
            return DbHelperSQL.GetSHSLInt("select top 1 ID from ERPNWorkFlowNode where NodeSerils='" + JieDianXuHaoStr + "' and WorkFlowID=" + workflowid.ToString());
        }
        else
        {
            return "0";
        }
    }


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

    public static bool ValidateXMChengguoInput(string formcontent, ref string msg)
    {
        var result = true;
        if (!string.IsNullOrEmpty(formcontent))
        {
            var parse = new ParseHtml();
            parse.GetAttListFormHTMLall(formcontent);
            //只对钻孔数和进尺进行累加操作，其余信息存储到成果审核表中。
            var mFormat = "[{0}]的输入格式不正确.请修改正确后再提交.";
            var alt = "钻孔数";
            var tResult = parse.getValue(alt);
            if (tResult != "")
            {
                var t = 0;
                result = int.TryParse(tResult, out t);
                if (!result)
                {
                    msg = string.Format(mFormat, alt);
                    return false;
                }
            }
            alt = "土壤氡检测";
            tResult = parse.getValue(alt);
            if (tResult != "")
            {
                var t = 0;
                result = int.TryParse(tResult, out t);
                if (!result)
                {
                    msg = string.Format(mFormat, alt);
                    return false;
                }
            }
            alt = "钻孔进尺";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "评估面积";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "调查面积";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "探槽";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "坑探";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "钻探";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "工程测量长度";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "工程测量面积";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "基坑监测面积";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "基坑监测深度";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "基坑设计周长";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "基坑设计深度";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "基坑设计面积";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "管线探测";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
            alt = "土壤氡检测";
            tResult = parse.getValue(alt);
            result = ValidateFloatLegal(tResult);
            if (!result)
            {
                msg = string.Format(mFormat, alt);
                return false;
            }
        }
        return result;
    }
    public static bool ValidateFloatLegal(string inputtext)
    {
        var result = true;
        if (inputtext != "")
        {
            var t = float.Parse("0.00");
            result = float.TryParse(inputtext, out t);
            if (!result)
            {
                return false;
            }
        }
        return result;
    }

    public string GetHeTongContentHtml(string strFormContent, ZWL.BLL.ERPHeTong infoModel)
    {
        //替换原有表单content中的值
        strFormContent = strFormContent.Replace("宏控件-当前日期", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("用户自定义控件-合同编号", infoModel.HTID);
        strFormContent = strFormContent.Replace("用户自定义控件-合同名称", infoModel.HTName);
        strFormContent = strFormContent.Replace("用户自定义控件-合同类别", infoModel.HTLB);
        strFormContent = strFormContent.Replace("用户自定义控件-专业类别", infoModel.ZYLB);
        strFormContent = strFormContent.Replace("用户自定义控件-行业类别", infoModel.HYLB);
        strFormContent = strFormContent.Replace("用户自定义控件-经营方式", infoModel.JYFS);
        strFormContent = strFormContent.Replace("用户自定义控件-甲方单位", infoModel.JFDW);
        strFormContent = strFormContent.Replace("用户自定义控件-甲方负责人", infoModel.JFFZR);
        strFormContent = strFormContent.Replace("用户自定义控件-乙方单位", infoModel.YFDW);
        strFormContent = strFormContent.Replace("用户自定义控件-乙方负责人", infoModel.YFFZR);
        strFormContent = strFormContent.Replace("用户自定义控件-丙方单位", infoModel.BFDW);
        strFormContent = strFormContent.Replace("用户自定义控件-丙方负责人", infoModel.BFFZR);

        strFormContent = strFormContent.Replace("用户自定义控件-合作单位1", infoModel.HZDW1);
        strFormContent = strFormContent.Replace("用户自定义控件-合作负责人1", infoModel.HZFZR1);
        strFormContent = strFormContent.Replace("用户自定义控件-合作单位2", infoModel.HZDW2);
        strFormContent = strFormContent.Replace("用户自定义控件-合作负责人2", infoModel.HZFZR2);
        strFormContent = strFormContent.Replace("用户自定义控件-合同金额", PublicMethod.FormatMoney(infoModel.HTJE));
        strFormContent = strFormContent.Replace("用户自定义控件-计价方式", infoModel.JJFS);
        strFormContent = strFormContent.Replace("用户自定义控件-项目地址", infoModel.Adress);
        strFormContent = strFormContent.Replace("用户自定义控件-合同形式", infoModel.HTXS);
        if (infoModel.QDTime.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同签订日期", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同签订日期", TimeParser.GetFormatDateString(infoModel.QDTime));
        }
        if (infoModel.KSTime.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同开始时间", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同开始时间", TimeParser.GetFormatDateString(infoModel.KSTime));
        }

        if (infoModel.JZTime.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同截止时间", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-合同截止时间", TimeParser.GetFormatDateString(infoModel.JZTime));
        }

        strFormContent = strFormContent.Replace("用户自定义控件-项目编号", infoModel.XMID);
        strFormContent = strFormContent.Replace("用户自定义控件-项目名称", infoModel.XMName);
        strFormContent = strFormContent.Replace("用户自定义控件-经费来源", infoModel.JFLY);
        strFormContent = strFormContent.Replace("用户自定义控件-签订份数", infoModel.QDFS.ToString());
        //收付款计划-金额
        strFormContent = strFormContent.Replace("用户自定义控件-计划金额1", infoModel.JEJH1);
        strFormContent = strFormContent.Replace("用户自定义控件-计划金额2", infoModel.JEJH2);
        strFormContent = strFormContent.Replace("用户自定义控件-计划金额3", infoModel.JEJH3);
        strFormContent = strFormContent.Replace("用户自定义控件-计划金额4", infoModel.JEJH4);
        strFormContent = strFormContent.Replace("用户自定义控件-计划金额5", infoModel.JEJH5);
        //收付款日期
        if (infoModel.RQJH1.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期1", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期1", TimeParser.GetFormatDateString(infoModel.RQJH1));
        }
        if (infoModel.RQJH2.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期2", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期2", TimeParser.GetFormatDateString(infoModel.RQJH2));
        }
        if (infoModel.RQJH3.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期3", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期3", TimeParser.GetFormatDateString(infoModel.RQJH3));
        }
        if (infoModel.RQJH4.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期4", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期4", TimeParser.GetFormatDateString(infoModel.RQJH4));
        }
        if (infoModel.RQJH5.Equals(DefaultTime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期5", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-计划日期5", TimeParser.GetFormatDateString(infoModel.RQJH5));
        }

        //收付款计划-备注
        strFormContent = strFormContent.Replace("用户自定义控件-计划备注1", infoModel.BZJH1);
        strFormContent = strFormContent.Replace("用户自定义控件-计划备注2", infoModel.BZJH2);
        strFormContent = strFormContent.Replace("用户自定义控件-计划备注3", infoModel.BZJH3);
        strFormContent = strFormContent.Replace("用户自定义控件-计划备注4", infoModel.BZJH4);
        strFormContent = strFormContent.Replace("用户自定义控件-计划备注5", infoModel.BZJH5);

        strFormContent = strFormContent.Replace("用户自定义控件-承接部门", infoModel.CJBM);
        strFormContent = strFormContent.Replace("用户自定义控件-项目承接人", infoModel.XMCJR);
        strFormContent = strFormContent.Replace("用户自定义控件-经办人", infoModel.JBR);

        //var cFlag = false;
        //if (infoModel.HTLB == "收款" && infoModel.YFDW == "广东佛山地质工程勘察院")//勘察院的合同处理
        //{
        //    if (infoModel.ZYLB.Contains("施工"))
        //    {
        //        if (infoModel.HTJE > 100 * 10000)
        //        {
        //            cFlag = true;
        //        }
        //    }
        //    else
        //    {
        //        if (infoModel.HTJE > 30 * 10000)
        //        {
        //            cFlag = true;
        //        }
        //    }
        //}
        //if (cFlag)
        //{
        //    strFormContent = strFormContent.Replace("分管领导", "院长审批");
        //}

        return strFormContent;
    }
    private int GetXMGuiDangRowCount(string html)
    {
        var count = 0;
        if (!html.IsNullOrEmpty())
        {
            var parser = new ZWL.Common.ParseHtml();
            var ds = parser.GetDataSetFormHTML(html);
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            if (dt != null)
            {
                var dtwhere = dt.AsEnumerable().Where(x => !x.Field<string>("Name").IsNullOrEmpty() && x.Field<string>("Name").Contains("档案名称"));
                if (dtwhere != null && dtwhere.Any())
                {
                    for (int i = 0; i < dtwhere.Count(); i++)
                    {
                        var item = dtwhere.ElementAt(i);
                        var name = item["Name"].ToString();
                        var val = item["Value"].ToString();
                        var no = PublicMethod.GetInt(PublicMethod.GetNumeric(name));
                        if (no > count && !val.IsNullOrEmpty())
                            count = no;
                    }
                }
            }
        }
        return count;
    }
    public string CheckXMCJJCPG(ZWL.BLL.ERPNWorkToDo model)
    {
        string result = "";
        var formatStr = @"<div style='color: red;font-size: 20px;text-align: left;font-weight: bold;'>
                          <font>注意，提交到下一步前，请检查该项目的【项目承接决策评估登记】是否已通过评审。</font>
                        </div>
                        <div>
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
    private bool CheckXMCJJCPGPassed(int nworkid, ref string msg)
    {
        var model = new ZWL.BLL.ERPNWorkToDo();
        model.GetModel(nworkid);
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
            if (list == null || !list.Any())
            {
                msg = "未关联【项目承接决策评估登记】。";
                return false;
            }
            if (list != null && !list.Any(e => e.CurrentWorkToDo.StateNow == "正常结束"))
            {
                msg = "【项目承接决策评估登记】正在办理中。";
                return false;
            }
        }
        return true;
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

    protected void btnForward_Click(object sender, EventArgs e)
    {

    }
}
