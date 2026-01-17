using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.BLL;
using ZWL.Common;
using ZWL.DBUtility;
using System.Linq;

public partial class Aptitude_AptitudeAdd : System.Web.UI.Page
{
    public string PiLiangSet = "";//批量设置字段的可写、保密属性
    public string strFormContent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            this.txt申请人.Text = PublicMethod.GetSessionValue("UserName");
            this.txt申请时间.Text = DateTime.Now.ToShortDateString();
            this.txt使用单位.Text = PublicMethod.GetSessionValue("Department");
            this.txt申请时间.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtQJSJStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //设置上传的附件为空
            this.HiddenField_WenJianList.Value = "";
            this.HiddenField_html.Value = "";
            this.lb资质证照.Text = Util.GetAptitudeFileList();

            //绑定下一节点
            string[] NextStrList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select NextNode from ERPNWorkFlowNode where WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString() + " and NodeAddr='开始'").Split(',');
            for (int i = 0; i < NextStrList.Length; i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Value = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ID from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString());
                MyItem.Text = "节点序号：" + NextStrList[i].ToString() + "--节点名称：" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString());
                if (MyItem.Value.ToString().Length > 0)
                {
                    this.DropDownList3.Items.Add(MyItem);
                }
            }
            //初始化 

            SetNodeInfoAndSet();

        }
        else
        {
            this.lb资质证照.Text = HiddenField_html.Value;
        }
    }
    /// 重载加载表单内容部分，获取宏控件的内容，写到页面的对应控件中。
    /// </summary>
    /// <param name="FormName"></param>
    protected void GetFormContent1(string FormID)
    {
        strFormContent = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ContentStr from ERPNForm where ID=" + FormID);
        strFormContent = strFormContent.Replace("宏控件-用户部门", PublicMethod.GetSessionValue("Department"));
        strFormContent = strFormContent.Replace("宏控件-用户姓名", PublicMethod.GetSessionValue("UserName"));
        strFormContent = strFormContent.Replace("宏控件-用户角色", PublicMethod.GetSessionValue("JiaoSe"));
        strFormContent = strFormContent.Replace("宏控件-用户职位", PublicMethod.GetSessionValue("ZhiWei"));
        strFormContent = strFormContent.Replace("宏控件-当前日期", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("宏控件-部门主管", ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + PublicMethod.GetSessionValue("Department") + "'"));
    }

    /// <summary>
    /// 设置下一节点具体属性、当前判断权限、可写、保密等
    /// </summary>
    public void SetNodeInfoAndSet()
    {
        try
        {
            //根据选择的节点，绑定人员等信息。
            ZWL.BLL.ERPNWorkFlowNode MyJieDian = new ZWL.BLL.ERPNWorkFlowNode();
            MyJieDian.GetModel(int.Parse(this.DropDownList3.SelectedItem.Value.ToString()));
            this.TextBox1.Text = MyJieDian.PSType;
            this.TextBox2.Text = MyJieDian.SPType;
            //根据审批模式设置页面
            SetPageFromPSStr(MyJieDian.SPType, MyJieDian.SPDefaultList);

            //当前开始节点是否有查看、编辑、删除按钮？当前按钮属性
            string NowNodeID = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select ID from ERPNWorkFlowNode where WorkFlowID=" + Request.QueryString["WorkFlowID"].ToString() + " and NodeAddr='开始'");
            ZWL.BLL.ERPNWorkFlowNode MyJieDianNow = new ZWL.BLL.ERPNWorkFlowNode();
            MyJieDianNow.GetModel(int.Parse(NowNodeID));
            if (MyJieDianNow.IFCanDel == "否")
            {
                this.ImageButton3.Visible = false;
            }
            if (MyJieDianNow.IFCanView == "否")
            {
                this.ImageButton5.Visible = false;
            }
            if (MyJieDianNow.IFCanEdit == "否")
            {
                this.ImageButton6.Visible = false;
            }

            //获取当前表单对应的工作数据列
            string[] FormItemList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ItemsList from ERPNForm where ID=" + Request.QueryString["FormID"].ToString()).Split('|');
            //获取当前节点的可写权限、保密权限
            string CanWriteStr = MyJieDianNow.CanWriteSet;
            string SecretStr = MyJieDianNow.SecretSet;

            for (int ItemNum = 0; ItemNum < FormItemList.Length; ItemNum++)
            {
                if (FormItemList[ItemNum].ToString().Trim().Length > 0)
                {
                    if (PublicMethod.StrIFIn(FormItemList[ItemNum].ToString(), CanWriteStr) == false)//不属于可写字段
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").disabled=true;";//readOnly
                    }
                    else
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").disabled=false;";//readOnly
                    }
                    if (PublicMethod.StrIFIn(FormItemList[ItemNum].ToString(), SecretStr) == true)//属于保密字段
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").style.visibility=\"hidden\";";
                    }
                    else
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").style.visibility=\"visible\";";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            MessageBox.ShowAndRedirect(this, "该流程下一节点未定义完整，请配置完整！" + ex.Message, "NWorkToDoSelect.aspx");
        }
    }
    /// <summary>
    /// 根据审批模式字符串设置页面显示
    /// </summary>
    /// <param name="SPStr"></param>
    public void SetPageFromPSStr(string SPStr, string DefultStr)
    {
        if (SPStr == "审批时自由指定")
        {
            this.TextBox5.Text = "";
        }
        else if (SPStr == "从默认审批人中选择")
        {
            this.TextBox5.Text = DefultStr;
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

            TextBox5.Text = DbHelperSQL.GetStringList(sql).Replace("|", ",");
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
            TextBox5.Text = DbHelperSQL.GetStringList(sql).Replace("|", ",");
        }
        else if (SPStr == "自动选择流程发起人")
        {
            this.TextBox5.Text = PublicMethod.GetSessionValue("UserName");
        }
        else if (SPStr == "自动选择本部门主管")
        {
            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + PublicMethod.GetSessionValue("Department") + "'");
        }
        else if (SPStr == "自动选择上级部门主管")
        {
            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where ID=(select top 1 DirID from ERPBuMen where BuMenName='" + PublicMethod.GetSessionValue("Department") + "')");
        }
    }

    /// <summary>
    /// 检测条件，然后返回下一字段，否则返回默认节点ID
    /// </summary>
    /// <returns></returns>
    public int CheckCondition(string DefaultNodeID, int workFlowId)
    {
        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where WorkFlowID=" + workFlowId + " and NodeAddr='开始'").Split('|');
        for (int i = 0; i < TiaoJianList.Length; i++)
        {
            if (TiaoJianList[i].Trim().Length > 0)
            {
                string NextIDStr = CheckTiaoJian(TiaoJianList[i].ToString(), workFlowId);
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
    protected string CheckTiaoJian(string TiaoJianStr, int workFlowId)
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
            return ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 ID from ERPNWorkFlowNode where NodeSerils='" + JieDianXuHaoStr + "' and WorkFlowID=" + workFlowId);
        }
        else
        {
            return "0";
        }
    }

    private bool ValidateInput(ref string msg)
    {
        var result = true;

        if (string.IsNullOrEmpty(txtQJSJStart.Text) || string.IsNullOrEmpty(txtQJSJEnd.Text))
        {
            msg = "请选择资质使用期限！";
            return false;
        }
        else
        {
            var start = DateTime.Parse(txtQJSJStart.Text);
            var end = DateTime.Parse(txtQJSJEnd.Text);
            if (start > end)
            {
                msg = "使用期限的的开始日期要小于结束日期！";
                return false;
            }
        }


        if (string.IsNullOrEmpty(this.txt项目名称.Text))
        {
            msg = "请输入项目名称！";
            return false;
        }

        if (string.IsNullOrEmpty(this.txt使用范围.Text))
        {
            msg = "请输入资质使用范围！";
            return false;
        }

        if (string.IsNullOrEmpty(HiddenField_AptitudeFiles.Value))
        {
            msg = "请勾选至少一个资质证照！";
            return false;
        }
        else
        {
            var aptSource = GetApitudeFilesList(HiddenField_AptitudeFiles.Value);
            var aptid = 0;
            var validRe = ValidateApitudeFiles(aptSource, out aptid);
            if (!validRe)
            {
                var aptModel = new ZWL.BLL.AptitudeFile();
                aptModel.GetModel(aptid);
                msg = "资质证照[" + aptModel.AptitudeName + "]的勾选不正确,请在正本/副本和原件/复印件两个组合至少各选择一项!";
                return false;
            }

            var firstList = new List<int> { 1, 2 };
            var groupSource = aptSource.GroupBy(r => r.AptFileID);
            foreach (var item in groupSource)
            {
                var currentSource = aptSource.Where(r => r.AptFileID == item.Key);
                if (currentSource.Any(r => r.Type == (int)ZWL.Common.AptitudeType.OriginalCopy))
                {
                    foreach (var it in currentSource.Where(r => firstList.Contains(r.Type)))
                    {
                        //if (it.State == (int)ZWL.Common.AptitudeState.Using)
                        //{
                        //    var aptModel = new ZWL.BLL.AptitudeFile();
                        //    aptModel.GetModel(it.AptFileID);
                        //    msg = "资质证照[" + aptModel.AptitudeName + "]的[" + Common.GetTitleTextByAptType(it.Type) + "]尚未归还,请确认归还后再申请！";
                        //    return false;
                        //}
                    }
                }
            }
        }

        //if (!string.IsNullOrEmpty(HiddenField_AptitudeFiles.Value))
        //{
        //    foreach (var item in HiddenField_AptitudeFiles.Value.Split(';'))
        //    {
        //        if (string.IsNullOrEmpty(item)) continue;

        //    }
        //}

        return result;
    }
    private bool ValidateApitudeFiles(List<AptitudeFileState> source, out int aptid)
    {
        var result = true;
        if (source.Any())
        {
            var firstList = new List<int> { (int)ZWL.Common.AptitudeType.Original, (int)ZWL.Common.AptitudeType.Carbon };
            var secondList = new List<int> { (int)ZWL.Common.AptitudeType.OriginalCopy, (int)ZWL.Common.AptitudeType.CarbonCopy };
            var s = source.GroupBy(r => r.AptFileID).Select(t => new { AptID = t.Key, SubList = t });
            foreach (var item in s)
            {
                var t = item.SubList.Any(r => firstList.Contains(r.Type));
                var t1 = item.SubList.Any(r => secondList.Contains(r.Type));
                if (!(t && t1))
                {
                    aptid = item.AptID;
                    return false;
                }
            }
        }
        aptid = 0;
        return result;
    }
    private List<AptitudeFileState> GetApitudeFilesList(string files)
    {
        var result = new List<AptitudeFileState>();
        if (!string.IsNullOrEmpty(files))
        {
            foreach (var item in files.Split(';'))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var aptFileId = 0;
                var aptType = 0;
                int.TryParse(item.Split('_')[0], out aptFileId);
                int.TryParse(item.Split('_')[1], out aptType);
                var aptex = new ZWL.BLL.AptitudeFileState();
                var aptitudeFileStateList = aptex.GetListModel(" AptFileID = " + aptFileId + " and Type =" + aptType);
                if (aptitudeFileStateList.Any())
                    result.AddRange(aptitudeFileStateList);
            }
        }

        return result;
    }
    private string GetFormHtml(int formId)
    {
        var result = string.Empty;
        var dt = DateTime.Now;
        var formModel = new ZWL.BLL.ERPNForm();

        formModel.GetModel(formId);
        var userName = PublicMethod.GetUserName();


        var aptlist = HiddenField_html.Value;
        if (!string.IsNullOrEmpty(aptlist) && aptlist.Contains("<SPAN style=\"COLOR: black\"></SPAN>"))
        {
            var splitContent = Regex.Split(aptlist, "<SPAN style=\"COLOR: black\"></SPAN>", RegexOptions.IgnoreCase);
            HiddenField_html.Value = splitContent[1];
        }

        //插入当前用户的印章
        //绑定所有印章
        var strImgPath = DbHelperSQL.GetSHSL("select ImgPath from ERPYinZhang where YinZhangLeiBie='私人印章' and UserName='"
                                            + userName + "' order by YinZhangLeiBie desc");
        strImgPath = "../UploadFile/" + strImgPath;

        var shiYongFanWeiStart = DateTime.Parse(txtQJSJStart.Text);
        var shiYongFanWeiEnd = DateTime.Parse(txtQJSJEnd.Text);

        result = formModel.ContentStr;
        result = result.Replace("用户自定义控件-项目名称", txt项目名称.Text)
                       .Replace("用户自定义控件-项目编号", txtProjectNo.Text)
                       .Replace("用户自定义控件-资质证照名称", HiddenField_html.Value)
                       .Replace("用户自定义控件-其他证照", txtOtherLicense.Text)
                       .Replace("用户自定义控件-使用范围", txt使用范围.Text)
                       .Replace("用户自定义控件-从年月日", shiYongFanWeiStart.Year.ToString() + "年" + shiYongFanWeiStart.Month.ToString().PadLeft(2, '0') + "月" + shiYongFanWeiStart.Day.ToString().PadLeft(2, '0') + "日")
                       .Replace("用户自定义控件-至年月日", shiYongFanWeiEnd.Year.ToString() + "年" + shiYongFanWeiEnd.Month.ToString().PadLeft(2, '0') + "月" + shiYongFanWeiEnd.Day.ToString().PadLeft(2, '0') + "日")
                       .Replace("用户自定义控件-单位名称", txt使用单位.Text)
                       .Replace("用户自定义控件-经办人签字", strImgPath)
                       .Replace("用户自定义控件-经办人日期", dt.ToString("yyyy/MM/dd"))
                       ;

        return result;
    }
    /// <summary>
    /// 表单的提交部分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        var msg = "";
        if (!ValidateInput(ref msg))
        {
            this.lb资质证照.Text = HiddenField_html.Value;
            MessageBox.Show(this, msg);
        }
        else
        {
            var dt = DateTime.Now;
            var userName = PublicMethod.GetUserName();
            var department = PublicMethod.GetDepartment();
            var no = "A" + dt.Year.ToString() + new PublicMethod().GetWaterCodeByTableName("AptitudeWork");
            var workName = userName + "--资质使用申请审批表(" + dt.ToString("yyyy/MM/dd") + ")";

            var formID = int.Parse(Request.QueryString["FormID"].ToString());
            var workFlowID = int.Parse(Request.QueryString["WorkFlowID"].ToString());
            var aptSource = GetApitudeFilesList(HiddenField_AptitudeFiles.Value);

            var model = new ZWL.BLL.AptitudeWork
            {
                No = no,
                WorkName = workName,
                ProjectNo = txtProjectNo.Text,
                ProjectName = txt项目名称.Text,
                OtherAptitude = txtOtherLicense.Text,
                StartDate = DateTime.Parse(txtQJSJStart.Text),
                EndDate = DateTime.Parse(txtQJSJEnd.Text),
                UsingRange = txt使用范围.Text,
                CreatedDate = dt,
                Department = department,
                Operator = userName,
                Comment = txtComment.Text,
            };

            var aptId = model.Add();

            var nodeId = 0;
            var nodeName = string.Empty;
            var stateNow = "";
            try
            {
                nodeId = GetNodeId(workFlowID);
                nodeName = GetNodeName(nodeId);
                stateNow = "正在办理";
            }
            catch
            {
                nodeName = "结束";
                stateNow = "强制结束";
            }
            var lateTime = DateTime.Now.AddHours(double.Parse(DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + nodeId)));
            var toDoModel = new ZWL.BLL.ERPNWorkToDo
            {
                WorkName = workName,
                FormID = formID,
                WorkFlowID = workFlowID,
                UserName = userName,
                TimeStr = dt,
                FormContent = GetFormHtml(formID),
                FuJianList = this.HiddenField_WenJianList.Value,
                JieDianID = nodeId,
                JieDianName = nodeName,
                ShenPiUserList = PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()),
                OKUserList = "默认",
                StateNow = stateNow,
                BeiYong1 = txtProjectNo.Text + (string.IsNullOrEmpty(txtProjectNo.Text) ? "" : "--") + txt项目名称.Text,
                LateTime = lateTime,
            };

            var todoID = toDoModel.Add();

            if (todoID > 0)
            {
                var mm = new ZWL.BLL.AptitudeWork();
                mm.GetModel(aptId);
                mm.ID = aptId;
                mm.NWorkID = todoID.ToString();
                mm.Update();
            }

            if (aptSource.Any())
            {
                var firstList = new List<int> { (int)ZWL.Common.AptitudeType.Original, (int)ZWL.Common.AptitudeType.Carbon };
                var secondList = new List<int> { (int)ZWL.Common.AptitudeType.OriginalCopy, (int)ZWL.Common.AptitudeType.CarbonCopy };

                var list = aptSource.Where(r => firstList.Contains(r.Type));
                var source = aptSource.Where(r => secondList.Contains(r.Type));
                foreach (var item in source)
                {
                    switch (item.Type)
                    {
                        case 3:
                            foreach (var sub in list)
                            {
                                if (sub.AptFileID == item.AptFileID)
                                {
                                    var aptFileStateModel = new ZWL.BLL.AptitudeFileState();
                                    var aptFileStateList = aptFileStateModel.GetListModel("AptFileID=" + sub.AptFileID + " and Type=" + sub.Type);
                                    if (aptFileStateList.Any())
                                    {
                                        var selectedAptFileState = aptFileStateList.FirstOrDefault();
                                        var m = new ZWL.BLL.AptitudeWorkDetail
                                        {
                                            AptFileStateID = selectedAptFileState.ID,
                                            AptWorkID = aptId,
                                        }.Add();
                                    }
                                }
                            }
                            break;
                        case 4:
                            foreach (var sub in list)
                            {
                                if (sub.AptFileID == item.AptFileID)
                                {
                                    var aptFileStateModel = new ZWL.BLL.AptitudeFileState();
                                    var aptFileStateList = aptFileStateModel.GetListModel("AptFileID=" + sub.AptFileID + " and Type=" + (sub.Type + 2));
                                    if (aptFileStateList.Any())
                                    {
                                        var selectedAptFileState = aptFileStateList.FirstOrDefault();
                                        var m = new ZWL.BLL.AptitudeWorkDetail
                                        {
                                            AptFileStateID = selectedAptFileState.ID,
                                            AptWorkID = aptId,
                                        }.Add();
                                    }
                                }
                            }
                            break;
                    }
                }

            }

            if (toDoModel.StateNow == "正在办理")
            {
                //发送短信
                SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + workName + ")", PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()), formID, workFlowID, workName, todoID);
                //SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + strWorkName + ")", PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()));
            }
            else
            {
                SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您的工作已经被强制结束！(" + workName + ")", userName);
            }



            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加新工作信息(" + workName + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            MessageBox.ShowAndRedirect(this, "资质使用申请提交成功！", "AptitudeList.aspx?FormID=89&WorkFlowID=80");
        }

    }
    private string GetNodeName(int nodeId)
    {
        var result = DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + nodeId);

        return result;
    }

    private int GetNodeId(int workFlowId)
    {
        var nodeId = 0;
        if (CheckBox1.Checked == true)
        {
            //条件未找到或者没有匹配项时，采用选择好的节点
            nodeId = int.Parse(this.DropDownList3.SelectedValue.ToString());
            try
            {
                ///////////根据条件获取下一审批节点信息                    
                nodeId = CheckCondition(this.DropDownList3.SelectedValue.ToString(), workFlowId);
            }
            catch
            {
                nodeId = int.Parse(this.DropDownList3.SelectedValue.ToString());
            }
        }
        else
        {
            nodeId = int.Parse(this.DropDownList3.SelectedValue.ToString());
        }
        return nodeId;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string FileNameStr = PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
        if (this.HiddenField_WenJianList.Value.Trim() == "")
        {
            this.HiddenField_WenJianList.Value = FileNameStr;
        }
        else
        {
            this.HiddenField_WenJianList.Value += "|" + FileNameStr;
        }
        PublicMethod.BindDDL(this.CheckBoxList1, this.HiddenField_WenJianList.Value);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected == true)
                {
                    this.HiddenField_WenJianList.Value = this.HiddenField_WenJianList.Value.Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|");
                }
            }
            PublicMethod.BindDDL(this.CheckBoxList1, this.HiddenField_WenJianList.Value);
        }
        catch
        { }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetNodeInfoAndSet();
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../FlexPaperFlash/SWFShow.aspx?f=" + this.CheckBoxList1.SelectedItem.Value + "&n=" + CheckBoxList1.SelectedItem.Text + "');</script>");
            }
        }
        catch
        { }
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../DsoFramer/EditFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Value + "');</script>");
            }
        }
        catch
        { }
    }
    protected void btnaddsomething_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.HiddenField_xmqqbh.Value))
        {
            var model = new ZWL.BLL.ERPXMJBXX();
            model.GetModel(this.HiddenField_xmqqbh.Value);
            this.txt项目名称.Text = model.XMName;
            this.txtProjectNo.Text = model.XMBH;
            this.lb资质证照.Text = this.HiddenField_html.Value;
        }
    }
}