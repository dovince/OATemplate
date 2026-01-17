using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ZWL.Common;
using ZWL.DBUtility;

public partial class WebApi_HR : System.Web.UI.Page
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
                case "ccsq":
                    result = CCSQ();
                    break;
				case "qingjiasq":
                    result = QingJiaSQ();
                    break;
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        Response.Write(result);
    }

    protected string CCSQ()
    {
        string Department = Request.Form["CCbumen"];
        string FormID = Request.Form["FormID"];
        string WorkFlowID = Request.Form["WorkFlowID"];
        string UserName = Request.Form["CCUserName"].ToString();
        ZWL.BLL.ERPNWorkToDo Model = new ZWL.BLL.ERPNWorkToDo();
        Model.WorkName = UserName + "--" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=" + WorkFlowID) + "(" + DateTime.Now.ToShortDateString() + ")";
        DateTime defaultime = new DateTime();
        ZWL.Common.PublicMethod.GetDefaultTime(out defaultime);
        Model.FormID = int.Parse(FormID);
        Model.WorkFlowID = int.Parse(WorkFlowID);
        Model.UserName = UserName;
        Model.TimeStr = DateTime.Now;

        string strFormContent = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ContentStr from ERPNForm where ID=" + FormID);
        strFormContent = strFormContent.Replace("宏控件-用户部门", Department);
        strFormContent = strFormContent.Replace("宏控件-用户姓名", UserName);
        strFormContent = strFormContent.Replace("宏控件-用户角色", Request.Form["JiaoSe"]);
        strFormContent = strFormContent.Replace("宏控件-用户职位", Request.Form["ZhiWei"]);
        strFormContent = strFormContent.Replace("宏控件-当前日期", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("宏控件-部门主管", ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + Department + "'"));

        //替换原有表单content中的值
        strFormContent = strFormContent.Replace("用户自定义控件-填表日期", DateTime.Now.ToString("yyyy/MM/dd"));
        strFormContent = strFormContent.Replace("用户自定义控件-所属部门", Department);
        strFormContent = strFormContent.Replace("用户自定义控件-申请人", UserName);
        strFormContent = strFormContent.Replace("用户自定义控件-出差地点", Request.Form["CCDd"]);
        strFormContent = strFormContent.Replace("用户自定义控件-同行人员", "无");
        strFormContent = strFormContent.Replace("用户自定义控件-出差事由", Request.Form["CCSY"]);

        strFormContent = strFormContent.Replace("用户自定义控件-交通工具", Request.Form["JTGJ"]);
        strFormContent = strFormContent.Replace("用户自定义控件-出差时间起", Request.Form["CCStarttime"]);
        strFormContent = strFormContent.Replace("用户自定义控件-出差时间止", Request.Form["CCEndtime"]);
        strFormContent = strFormContent.Replace("用户自定义控件-备注", Request.Form["CCBZ"]);



        Model.FormContent = strFormContent;//将表单内容写回到数据库中



        string FuJianList = string.Empty;
        for (var i = 0; i < Request.Files.Count; i++)
        {
            HttpPostedFile File = Request.Files[i];
            if (File != null)
            {
                FuJianList += UploadFileIntoDir(File) + "|";

            }
        }
        if (!string.IsNullOrEmpty(FuJianList))
        {
            Model.FuJianList = FuJianList.Substring(0, FuJianList.Length - 1);
        }
        Model.ShenPiYiJian = "";
        string CCXYJD = Request.Form["CCXYJD"];
        try
        {
            if (1 == 1)
            {
                //条件未找到或者没有匹配项时，采用选择好的节点
                Model.JieDianID = int.Parse(CCXYJD);
                try
                {
                    ///////////根据条件获取下一审批节点信息                    
                    Model.JieDianID = CheckCondition(CCXYJD);
                }
                catch
                {
                    Model.JieDianID = int.Parse(CCXYJD);
                }
            }
            else
            {
                Model.JieDianID = int.Parse(CCXYJD);
            }
            Model.JieDianName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString());
            Model.StateNow = "正在办理";
        }
        catch
        {
            Model.JieDianName = "结束";
            Model.StateNow = "强制结束";
        }
        Model.ShenPiUserList = ZWL.Common.PublicMethod.WorkWeiTuoUserList(Request.Form["CCSPR"]);
        Model.OKUserList = "默认";
        Model.LateTime = DateTime.Now.AddHours(double.Parse(ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString())));

        Model.BeiYong1 = Department + "--" + Department;
        Model.BeiYong2 = "";
        var workid = Model.Add();
        Model.ID = workid;

        ZWL.BLL.ERPChuChai ChuChai = new ZWL.BLL.ERPChuChai();
        ChuChai.SQR = UserName;
        ChuChai.BM = Department;
        ChuChai.TBTime = DateTime.Now;
        ChuChai.ChuChaiShiYou = Request.Form["CCSY"];

        ChuChai.ChuChaiStart = DateTime.Parse(DateTime.Parse(Request.Form["CCStarttime"]).ToShortDateString());
        ChuChai.ChuChaiEnd = DateTime.Parse(DateTime.Parse(Request.Form["CCEndtime"]).ToShortDateString());

        ChuChai.ChuChaiDiDian = Request.Form["CCDd"];
        ChuChai.TongXingRenYuan = "无";
        ChuChai.JiaoTongGJ = Request.Form["JTGJ"];
        ChuChai.BZ = Request.Form["CCBZ"];
        //出差一旦提交，该状态为正在办理，如果该工作对应的工作流被终止或驳回，那么要更改出差的状态为不通过
        ChuChai.CCState = "正在办理";
        ChuChai.BSState = "未报销";

        ChuChai.NWorkID = DbHelperSQL.GetMaxID("ID", "ERPNWorkToDo");//当前提交的工作id
        ChuChai.Add();

        bool sms = Request.Form["SMS"].ToString().Equals("true") == true ? true : false;
        bool mob = Request.Form["MOB"].ToString().Equals("true") == true ? true : false;
        if (Model.StateNow == "正在办理")
        {
            //发送短信
            SendMessage1(sms, mob, "您有新的工作需要办理！(" + Model.WorkName + ")", ZWL.Common.PublicMethod.WorkWeiTuoUserList(Request.Form["CCSPR"]), int.Parse(Request.Form["FormID"].ToString()), int.Parse(Request.Form["WorkFlowID"].ToString()),Model.ID, Model.BeiYong1);
            //SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + strWorkName + ")", ZWL.Common.PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()));
        }
        else
        {
            SendMessage(sms, mob, "您的工作已经被强制结束！(" + Model.WorkName + ")", UserName);
        }


        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加新工作信息(" + Model.WorkName + ")[手机版]";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        return Success;
    }
	
	protected string QingJiaSQ()
	{
	    var WorkFlowID = "68";
	    var FormID = "50";
        var txtQJDateNum = Request["txtQJDateNum"];
        var DropDownListQJLX = Request["DropDownListQJLX"];
        var NXJ = Request["NXJ"];
        var Label_QJR = Request["Label_QJR"];
        var txtQJSJStart = Request["txtQJSJStart"];
        var txtQJSJEnd = Request["txtQJSJEnd"];
        var txtQJYY = Request["txtQJYY"];
        var txtBZ = Request["txtBZ"];
        var DropDownList3 = Request["DropDownList3"];
        var TextBox5 = Request["TextBox5"];

        bool sms = Request.Form["SMS"].ToString().Equals("true") == true ? true : false;
        bool mob = Request.Form["MOB"].ToString().Equals("true") == true ? true : false;

        if (txtQJDateNum == "" || txtQJDateNum == "0")
        {
            return "请选择请假时间！";
        }

        if (DropDownListQJLX == "补休假")//更新年休假信息
        {
            double temp = double.Parse(txtQJDateNum);
            string dep = ZWL.Common.PublicMethod.GetSessionValue("Department");
            string deplist = "中心领导,总工程师办公室,经营管理科,人事科,财务科,安全生产科,基地管理科,工会,离退休人员管理科,资料室,监察科（与纪委、审计科合署）,办公室,党委办公室,开发部";
            var bxts = Getbxts();
            temp += bxts;
            if (temp > 3 && deplist.Contains(dep))
            {
                return "每年最多只能请三天补休假，请重新选择。";
            }
        }

        //从隐藏文本框中获取初始化时计算好的剩余年休假
        float nsynxj = 0;
        float.TryParse(NXJ, out nsynxj);
        //判断年休假剩余天数是否为0
        string strshiyongnxj = "0";//使用
        string strshengyunxj = nsynxj.ToString();//剩余

        //计算请假时间在选择请假截止日期时计算好放在txttxtQJDateNum中
        float nqjdatenum = 0;
        float.TryParse(txtQJDateNum, out nqjdatenum);
        if (DropDownListQJLX == "年休假")//更新年休假信息
        {
            #region 旧版代码
            //if (nqjdatenum > nsynxj || Label_kynxj.Text == "0")
            //{
            //    Response.Write("<script>alert('年休假剩余天数不足！请重新输入时间！');</script>");
            //    return;
            //}
            //else
            //{
            //    strshiyongnxj = nqjdatenum.ToString();
            //    strshengyunxj = (nsynxj - nqjdatenum).ToString();
            //}
            #endregion
            //如果年休假不为空
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            string strusername = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            amodel.GetModel(strusername);//根据用户名获取对象信息
            var res = amodel.addnxj(nqjdatenum);
            if (res == "OK")
            {
                strshiyongnxj = nqjdatenum.ToString();
            }
            else
            {
                return res;
            }
            //将年休假的代码归到model里面
            //double nowremain = amodel.NowRemainDays;
            //double lastremain = amodel.LastRemainDays;
            //double lastky = lastremain - amodel.LastUsedDays;
            //double dallky = nowremain + lastky;
            //if (nqjdatenum <= dallky)
            //{
            //    if (nqjdatenum < lastky)//去年剩下的够用了
            //    {
            //        //去年的还有剩余，今年的不变
            //        amodel.LastUsedDays += nqjdatenum;
            //        amodel.LastFreezonDay += nqjdatenum;//冻结年休假

            //    }
            //    else//去年剩的还不够
            //    {
            //        amodel.LastUsedDays = amodel.LastRemainDays;//用完去年的

            //        double ntemp = nqjdatenum - lastky;//还差几天
            //        //amodel.NowUsedDays += ntemp;//今年已用的
            //        amodel.FreezonDay += ntemp;//冻结年休假
            //        amodel.LastFreezonDay += lastky;
            //        amodel.NowRemainDays -= ntemp;//从今年剩余的扣除
            //        amodel.NowUsedDays = amodel.NowDays - amodel.NowRemainDays;//算出今年已用年休假
            //    }
            //    amodel.Update();
            //    strshiyongnxj = nqjdatenum.ToString();
            //}
            //else
            //{
            //    return "可用年休假剩余天数不足！请重新输入请假时间！";
            //}


        }

        ZWL.BLL.ERPNWorkToDo Model = new ZWL.BLL.ERPNWorkToDo();
        Model.WorkName = ZWL.Common.PublicMethod.GetSessionValue("UserName") + "--" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=" + WorkFlowID) + "(" + DateTime.Now.ToShortDateString() + ")"; ;
        DateTime defaultime = new DateTime();
        ZWL.Common.PublicMethod.GetDefaultTime(out defaultime);
        Model.FormID = int.Parse(Request.QueryString["FormID"].ToString());
        Model.WorkFlowID = int.Parse(Request.QueryString["WorkFlowID"].ToString());
        Model.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.TimeStr = DateTime.Now;

        //替换控件中的值到表单中
        var strFormContent = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ContentStr from ERPNForm where ID=" + FormID); ;
        //替换原有表单content中的值
        strFormContent = strFormContent.Replace("用户自定义控件-姓名", Label_QJR);
        strFormContent = strFormContent.Replace("用户自定义控件-填表时间", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("用户自定义控件-所属部门", ZWL.Common.PublicMethod.GetSessionValue("Department"));
        strFormContent = strFormContent.Replace("用户自定义控件-请假类型", DropDownListQJLX);
        strFormContent = strFormContent.Replace("用户自定义控件-请假日期起", txtQJSJStart);
        strFormContent = strFormContent.Replace("用户自定义控件-请假日期止", txtQJSJEnd);
        strFormContent = strFormContent.Replace("用户自定义控件-请假原因", txtQJYY);
        //用户实际请假日期
        strFormContent = strFormContent.Replace("用户自定义控件-实际请假日期起", "");
        strFormContent = strFormContent.Replace("用户自定义控件-实际请假日期止", "");
        //计算请假天数          
        strFormContent = strFormContent.Replace("用户自定义控件-请假天数", txtQJDateNum);
        strFormContent = strFormContent.Replace("用户自定义控件-使用年休假", strshiyongnxj);
        //strFormContent = strFormContent.Replace("用户自定义控件-剩余年休假", strshengyunxj);
        strFormContent = strFormContent.Replace("用户自定义控件-备注", txtBZ);
        Model.FormContent = strFormContent;//将表单内容写回到数据库中

        //app的方法
        string FuJianList = string.Empty;
        for (var i = 0; i < Request.Files.Count; i++)
        {
            HttpPostedFile File = Request.Files[i];
            if (File != null)
            {
                FuJianList += UploadFileIntoDir(File) + "|";

            }
        }
        if (!string.IsNullOrEmpty(FuJianList))
        {
            Model.FuJianList = FuJianList.Substring(0, FuJianList.Length - 1);
        }
        /////////////////////////////////////////
        
        Model.ShenPiYiJian = "";
	    var CheckBox1 = true;
        try
        {
            if (CheckBox1 == true)
            {
                //条件未找到或者没有匹配项时，采用选择好的节点
                Model.JieDianID = int.Parse(DropDownList3);
                try
                {
                    ///////////根据条件获取下一审批节点信息                    
                    Model.JieDianID = CheckCondition(DropDownList3);
                }
                catch
                {
                    Model.JieDianID = int.Parse(DropDownList3);
                }
            }
            else
            {
                Model.JieDianID = int.Parse(DropDownList3);
            }
            Model.JieDianName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString());
            Model.StateNow = "正在办理";
        }
        catch
        {
            Model.JieDianName = "结束";
            Model.StateNow = "强制结束";
        }
        Model.ShenPiUserList = ZWL.Common.PublicMethod.WorkWeiTuoUserList(TextBox5.Trim());
        Model.OKUserList = "默认";
        Model.LateTime = DateTime.Now.AddHours(double.Parse(ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString())));

        Model.BeiYong1 = txtQJSJStart + "--" + txtQJSJEnd;
        Model.BeiYong2 = "";
        Model.ID = Model.Add();

        ZWL.BLL.ERPQingJia QingJia = new ZWL.BLL.ERPQingJia();
        QingJia.QJR = Label_QJR;
        QingJia.BM = ZWL.Common.PublicMethod.GetSessionValue("Department");
        //请假一旦提交，该请假状态为正在办理，如果该请假对应的工作流被终止或驳回，那么要更改请假的状态为不通过，计算可用年休假时不计算这条记录用的年休假
        QingJia.QJState = "正在办理";
        //设置角色
        string strJiaoSe = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");
        if (strJiaoSe.Contains("一般工作人员（人事管理）"))
        {
            strJiaoSe = "一般工作人员（人事管理）";
        }
        else if (strJiaoSe.Contains("机关科室职工（人事管理）"))
        {
            strJiaoSe = "机关科室职工（人事管理）";
        }
        else if (strJiaoSe.Contains("部门领导（人事管理）"))
        {
            strJiaoSe = "部门领导（人事管理）";
        }
        else if (strJiaoSe.Contains("单位领导"))
        {
            strJiaoSe = "单位领导";
        }
        else
        {
            strJiaoSe = "一般工作人员（人事管理）";
        }
        QingJia.JiaoSe = strJiaoSe;
        QingJia.TBTime = DateTime.Now;

        QingJia.QJSJStart = DateTime.Parse(DateTime.Parse(txtQJSJStart).ToShortDateString());
        QingJia.QJSJEnd = DateTime.Parse(DateTime.Parse(txtQJSJEnd).ToShortDateString());

        QingJia.QJTS = float.Parse(txtQJDateNum);
        QingJia.ShiYongNXJ = float.Parse(strshiyongnxj);
        QingJia.QJLX = DropDownListQJLX;

        QingJia.QJYY = txtQJYY;
        QingJia.NWorkID = DbHelperSQL.GetMaxID("ID", "ERPNWorkToDo");//当前提交的工作id
        QingJia.BZ = txtBZ;
        QingJia.Add();
        if (QingJia.QJLX == "年休假")
        {
            ZWL.BLL.AnnualLeave.UpdateLastLog(QingJia.NWorkID);
        }

        if (Model.StateNow == "正在办理")
        {
            //发送短信
            SendMessage1(sms, mob, "您有新的工作需要办理！(" + Model.WorkName + ")", ZWL.Common.PublicMethod.WorkWeiTuoUserList(Request.Form["CCSPR"]), int.Parse(FormID.ToString()), int.Parse(WorkFlowID.ToString()),Model.ID, Model.BeiYong1);
        }
        else
        {
            SendMessage(sms, mob, "您的工作已经被强制结束！(" + Model.WorkName + ")", PublicMethod.GetSessionValue("UserName"));
        }

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加新工作信息(" + Model.WorkName + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();

        return Success;
    }
	
	private double Getbxts()
    {
        double bxts = 0;
        string temp = DbHelperSQL.GetSHSL("SELECT isnull(sum([QJTS]), 0) AS sumTS FROM [ERPQingJia] where QJR='" +
                                   PublicMethod.GetSessionValue("UserName") + "' AND [QJLX]='补休假'" +
                                   " AND [QJSJStart] > '" + DateTime.Now.ToString("yyyy-01-01") + "' AND [QJSJStart] < '" + DateTime.Now.AddYears(1).ToString("yyyy-01-01") + "'");
        bxts = Convert.ToDouble(temp);
        return bxts;
    }
	
    public static void SendMessage(bool SmsChk, bool MailChk, string ContentStr, string ToUserList)
    {

        if (SmsChk == true)
        {
            //发送手机信息
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            if (MailChk == true)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.Add();
            }
        }
    }

    public static void SendMessage1(bool SmsChk, bool MailChk, string ContentStr, string ToUserList, int FormID, int WorkFlowID,int workid, string BeiYong1)
    {

        if (SmsChk == true)
        {
            //发送手机信息
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            if (MailChk == true)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.FormID = FormID;
                MyMail.WorkFlowID = WorkFlowID;
                MyMail.BeiYong1 = BeiYong1;
                MyMail.WorkToDoID = workid;
                MyMail.Add();
            }
        }
    }

    private string UploadFileIntoDir(HttpPostedFile File)
    {
        string DirName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(File.FileName);
        File.SaveAs(Server.MapPath("~/UploadFile/") + DirName);
        //将原文件名与现在文件名写入ERPSaveFileName表中
        string NowName = DirName;
        string OldName = File.FileName;
        string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
        ZWL.DBUtility.DbHelperSQL.ExecuteSQL(SqlTempStr);
        return DirName;
    }

    public int CheckCondition(string DefaultNodeID)
    {
        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString() + " and NodeAddr='开始'").Split('|');
        for (int i = 0; i < TiaoJianList.Length; i++)
        {
            if (TiaoJianList[i].Trim().Length > 0)
            {
                string NextIDStr = CheckTiaoJian(TiaoJianList[i].ToString());
                if (NextIDStr != "0")
                {
                    return int.Parse(NextIDStr);
                }
            }
        }
        return int.Parse(DefaultNodeID);
    }

    protected string CheckTiaoJian(string TiaoJianStr)
    {
        //条件格式如：ABCD_请假天数→大于→10→3        
        string ZiDuanStrEN = TiaoJianStr.Split('_')[0]; //字段名称EN 如：ABCD        
        string ZiDuanStrCN = TiaoJianStr.Split('_')[1]; //字段名称CN 如：请假天数        
        string BiJiaoStr = TiaoJianStr.Split('→')[1]; //条件比较  如：大于
        string ZhiStr = TiaoJianStr.Split('→')[2];//比较的值，如： 10
        string JieDianXuHaoStr = TiaoJianStr.Split('→')[3];//跳转的节点序号，如： 3

        string NowValue = "";
        try
        {
            NowValue = Request.Form[ZiDuanStrEN].ToString();
        }
        catch
        { }

        if (BiaoJiaoTwoStr(NowValue, ZhiStr, BiJiaoStr) == true)
        {
            return ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 ID from ERPNWorkFlowNode where NodeSerils='" + JieDianXuHaoStr + "' and WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString());
        }
        else
        {
            return "0";
        }
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

}