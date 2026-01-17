using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;
using System.Data;

public partial class Financial_ERPListOfExpendedAdd : BasePage
{
    public string FormID = "106";
    public string WorkFlowID = "105";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //绑定下一节点
            string[] NextStrList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select NextNode from ERPNWorkFlowNode where WorkFlowID=" + WorkFlowID.ToString() + " and NodeAddr='开始'").Split(',');
            for (int i = 0; i < NextStrList.Length; i++)
            {
                ListItem MyItem = new ListItem();
                MyItem.Value = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ID from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + WorkFlowID.ToString());//根据序号和workflowID获得节点ID
                MyItem.Text = "节点序号：" + NextStrList[i].ToString() + "--节点名称：" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where NodeSerils='" + NextStrList[i].ToString() + "' and WorkFlowID=" + WorkFlowID.ToString());
                if (MyItem.Value.ToString().Length > 0)
                {
                    this.DropDownList3.Items.Add(MyItem);
                }
            }
            txtDepartment.Text = Department;
            txtUsername.Text = UserName;
            SetNodeInfoAndSet();
            //绑定编号和名称
            if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
            {
                ZWL.BLL.ERPNWorkToDo Model = new ZWL.BLL.ERPNWorkToDo();
                Model.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
                TextBox5.Text = Model.ShenPiUserList;

                var erplistofexpended = new ZWL.BLL.ERPListOfExpended();
                erplistofexpended.GetNWorkModel(int.Parse(Request.QueryString["Nwid"].ToString()));
                setDefault(erplistofexpended);
            }
        }
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
            string NowNodeID = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select ID from ERPNWorkFlowNode where WorkFlowID=" + WorkFlowID.ToString() + " and NodeAddr='开始'");
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
            string[] FormItemList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ItemsList from ERPNForm where ID=" + FormID.ToString()).Split('|');
            //获取当前节点的可写权限、保密权限
            string CanWriteStr = MyJieDianNow.CanWriteSet;
            string SecretStr = MyJieDianNow.SecretSet;

            for (int ItemNum = 0; ItemNum < FormItemList.Length; ItemNum++)
            {
                if (FormItemList[ItemNum].ToString().Trim().Length > 0)
                {
                    if (ZWL.Common.PublicMethod.StrIFIn(FormItemList[ItemNum].ToString(), CanWriteStr) == false)//不属于可写字段
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").disabled=true;";//readOnly
                    }
                    else
                    {
                        PiLiangSet = PiLiangSet + "document.getElementById(\"" + FormItemList[ItemNum].ToString().Split('_')[0] + "\").disabled=false;";//readOnly
                    }
                    if (ZWL.Common.PublicMethod.StrIFIn(FormItemList[ItemNum].ToString(), SecretStr) == true)//属于保密字段
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
            ZWL.Common.MessageBox.ShowAndRedirect(this, "该流程下一节点未定义完整，请配置完整！" + ex.Message, "NWorkToDoSelect.aspx");
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
            string SqlWhere = "";
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

            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetStringList("select UserName from ERPUser where " + SqlWhere).Replace("|", ",");
        }
        else if (SPStr == "从默认审批角色中选择")
        {
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

            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetStringList("select UserName from ERPUser where " + SqlWhere).Replace("|", ",");
        }
        else if (SPStr == "自动选择流程发起人")
        {
            this.TextBox5.Text = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        }
        else if (SPStr == "自动选择本部门主管")
        {
            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + ZWL.Common.PublicMethod.GetSessionValue("Department") + "'");
        }
        else if (SPStr == "自动选择上级部门主管")
        {
            this.TextBox5.Text = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where ID=(select top 1 DirID from ERPBuMen where BuMenName='" + ZWL.Common.PublicMethod.GetSessionValue("Department") + "')");
        }
    }

    /// <summary>
    /// 重载加载表单内容部分，获取宏控件的内容，写到页面的对应控件中。
    /// </summary>
    /// <param name="FormName"></param>
    protected string GetFormContent1(string FormID)
    {
        var strFormContent = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ContentStr from ERPNForm where ID=" + FormID);
        strFormContent = strFormContent.Replace("宏控件-用户部门", ZWL.Common.PublicMethod.GetSessionValue("Department"));
        strFormContent = strFormContent.Replace("宏控件-用户姓名", ZWL.Common.PublicMethod.GetSessionValue("UserName"));
        strFormContent = strFormContent.Replace("宏控件-用户角色", ZWL.Common.PublicMethod.GetSessionValue("JiaoSe"));
        strFormContent = strFormContent.Replace("宏控件-用户职位", ZWL.Common.PublicMethod.GetSessionValue("ZhiWei"));
        strFormContent = strFormContent.Replace("宏控件-当前日期", DateTime.Now.ToShortDateString());
        strFormContent = strFormContent.Replace("宏控件-部门主管", ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + ZWL.Common.PublicMethod.GetSessionValue("Department") + "'"));
        var qianmingimg = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 ImgPath from ERPYinZhang where UserName='"+ ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'");
        strFormContent = strFormContent.Replace("宏控件-制表人签名", qianmingimg);
        return strFormContent;
    }
    /// <summary>
    /// 检测条件，然后返回下一字段，否则返回默认节点ID
    /// </summary>
    /// <returns></returns>
    public int CheckCondition(string DefaultNodeID)
    {
        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where WorkFlowID=" + WorkFlowID.ToString() + " and NodeAddr='开始'").Split('|');
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

    /// <summary>
    /// 判断条件，返回下一节点ID
    /// </summary>
    /// <param name="TiaoJianStr"></param>
    /// <returns></returns>
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

        if (ZWL.Common.PublicMethod.BiaoJiaoTwoStr(NowValue, ZhiStr, BiJiaoStr) == true)
        {
            return ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 ID from ERPNWorkFlowNode where NodeSerils='" + JieDianXuHaoStr + "' and WorkFlowID=" + WorkFlowID.ToString());
        }
        else
        {
            return "0";
        }
    }
    /// <summary>
    /// 表单的提交部分
    /// </summary>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //在提交表单的时候重新获取编号
        ZWL.BLL.ERPNWorkToDo Model = new ZWL.BLL.ERPNWorkToDo();
        Model.WorkName = ZWL.Common.PublicMethod.GetSessionValue("UserName") + "--" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=" + WorkFlowID.ToString()) + "(" + DateTime.Now.ToShortDateString() + ")";
        DateTime defaultime = new DateTime();
        ZWL.Common.PublicMethod.GetDefaultTime(out defaultime);
        Model.FormID = int.Parse(FormID.ToString());
        Model.WorkFlowID = int.Parse(WorkFlowID.ToString());
        Model.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.TimeStr = DateTime.Now;


        //替换控件中的值到表单中
        var strFormContent = GetFormContent1(Model.FormID.ToString());
        ZWL.BLL.ERPListOfExpended erplistofexpended = new ZWL.BLL.ERPListOfExpended();
        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            erplistofexpended.GetNWorkModel(int.Parse(Request.QueryString["Nwid"].ToString()));
        }

        erplistofexpended.WorkName = Model.WorkName;
        erplistofexpended.CreatedTime = DateTime.Now;//以此为默认时间
        //合计
        erplistofexpended.Amount = PublicMethod.GetDecimal(this.txtAmount.Text);

        //部门
        erplistofexpended.Department = this.txtDepartment.Text;

        //登记人
        erplistofexpended.Username = this.txtUsername.Text;


        //替换原有表单content中的值

        strFormContent = strFormContent.Replace("用户自定义控件-名称", erplistofexpended.WorkName);

        strFormContent = strFormContent.Replace("用户自定义控件-合计", erplistofexpended.Amount.ToString());

        strFormContent = strFormContent.Replace("用户自定义控件-部门", erplistofexpended.Department);

        strFormContent = strFormContent.Replace("用户自定义控件-登记人", erplistofexpended.Username);
        //判断是否有子表


        var ERPListOfExpendedDetailList = GetERPListOfExpendedDetailList();
        var ERPListOfExpendedDetailListform = MakeERPListOfExpendedDetailListForm(ERPListOfExpendedDetailList);
        strFormContent = strFormContent.Replace("<!--ERPListOfExpendedDetailList-->", ERPListOfExpendedDetailListform);



        Model.FormContent = strFormContent;//将表单内容写回到数据库中
        Model.FuJianList = this.HiddenField_WenJianList.Value;
        Model.ShenPiYiJian = "";
        try
        {
            if (CheckBox1.Checked == true)
            {
                //条件未找到或者没有匹配项时，采用选择好的节点
                Model.JieDianID = int.Parse(this.DropDownList3.SelectedValue.ToString());
                try
                {
                    ///////////根据条件获取下一审批节点信息                    
                    Model.JieDianID = CheckCondition(this.DropDownList3.SelectedValue.ToString());
                }
                catch
                {
                    Model.JieDianID = int.Parse(this.DropDownList3.SelectedValue.ToString());
                }
            }
            else
            {
                Model.JieDianID = int.Parse(this.DropDownList3.SelectedValue.ToString());
            }
            Model.JieDianName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select NodeName from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString());
            Model.StateNow = "正在办理";
        }
        catch
        {
            Model.JieDianName = "结束";
            Model.StateNow = "强制结束";
        }
        Model.ShenPiUserList = ZWL.Common.PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim());
        Model.OKUserList = "默认";
        Model.LateTime = DateTime.Now.AddHours(double.Parse(ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + Model.JieDianID.ToString())));
        Model.BeiYong1 = "";
        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            //修改
            Model.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
            string strxmformcontent = Model.FormContent;
            string strnewFormContent = strFormContent;
            string strsplit = "<!--split-->";
            //如果空表单和投标表单都不为空时
            if (!string.IsNullOrEmpty(strxmformcontent) && !string.IsNullOrEmpty(strFormContent) && strnewFormContent.Contains(strsplit) && strxmformcontent.Contains(strsplit))
            {
                //首先截断表单，拼接新表单(用<!--split-->分割)用正则表达式分割
                string[] strsplitempty = Regex.Split(strnewFormContent, strsplit, RegexOptions.IgnoreCase);
                string[] strsplithtform = Regex.Split(strxmformcontent, strsplit, RegexOptions.IgnoreCase);
                strnewFormContent = strsplitempty[0].ToString() + strsplit + strsplithtform[1].ToString();
            }
            else
            {
                ZWL.Common.MessageBox.Show(this, "费用成本报销信息变更失败！当前项目不支持变更操作。");
            }
            if (!string.IsNullOrEmpty(strnewFormContent))
            {
                //如果新表单不为空,更新
                Model.FuJianList = this.HiddenField_WenJianList.Value;
                Model.FormContent = strnewFormContent;
                Model.Update();
                erplistofexpended.Update();//更新费用成本报销信息

                //先删除子表再添加进去


                DbHelperSQL.ExecuteSQL(string.Format("Delete from ERPListOfExpendedDetail where RefID='{0}'", erplistofexpended.ID));
                foreach (var erplistofexpendeddetail in ERPListOfExpendedDetailList)
                {
                    erplistofexpendeddetail.RefID = erplistofexpended.ID;
                    erplistofexpendeddetail.Add();
                }




                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户修改了费用成本报销信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();

                ZWL.Common.MessageBox.ShowAndRedirect(this, "费用成本报销信息变更成功！", "ERPListOfExpendedManager.aspx");

                //如果该工作是被驳回的工作，向审批人发送修改信息
                if (Model.StateNow == "已被驳回" && Model.ShenPiUserList != "工作已办结")
                {
                    if (Model.ShenPiUserList.Contains(","))//如果是多人审批，给每个人发邮件
                    {
                        for (int i = 0; i < Model.ShenPiUserList.Split(',').Length; i++)
                        {
                            if (!string.IsNullOrEmpty(Model.ShenPiUserList.Split(',')[i]))
                            {
                                //发邮件通知审批人重新审批
                                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                                MyMail.EmailContent = ZWL.Common.PublicMethod.GetSessionValue("UserName") + "的工作：" + Model.BeiYong1 + " 已经修改，请在【已办工作】中重新审批!";
                                MyMail.EmailState = "未读";
                                MyMail.EmailTitle = "您驳回的工作：(" + Model.BeiYong1 + ")已经修改，请在【已办工作】中重新审批！";
                                MyMail.FromUser = "系统消息";
                                MyMail.FuJian = "";
                                MyMail.TimeStr = DateTime.Now;
                                MyMail.ToUser = Model.ShenPiUserList.Split(',')[i];
                                MyMail.FormID = (int)Model.FormID;
                                MyMail.WorkFlowID = (int)Model.WorkFlowID;
                                MyMail.BeiYong1 = Model.BeiYong1;
                                MyMail.Add();
                            }

                        }
                    }
                    else
                    {
                        //发邮件通知审批人重新审批
                        ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                        MyMail.EmailContent = ZWL.Common.PublicMethod.GetSessionValue("UserName") + "的工作：" + Model.BeiYong1 + " 已经修改，请在【已办工作】中重新审批!";
                        MyMail.EmailState = "未读";
                        MyMail.EmailTitle = "您驳回的工作：(" + Model.BeiYong1 + ")已经修改，请在【已办工作】中重新审批！";
                        MyMail.FromUser = "系统消息";
                        MyMail.FuJian = "";
                        MyMail.TimeStr = DateTime.Now;
                        MyMail.ToUser = Model.ShenPiUserList;
                        MyMail.FormID = (int)Model.FormID;
                        MyMail.WorkFlowID = (int)Model.WorkFlowID;
                        MyMail.BeiYong1 = Model.BeiYong1;
                        MyMail.Add();
                    }
                }
            }
            else
            {
                ZWL.Common.MessageBox.Show(this, "费用成本报销信息变更失败！");
            }
        }
        else
        {
            //新增
            var nwtdid = Model.Add();
            erplistofexpended.NWorkToDoID = nwtdid;
            var mainid = erplistofexpended.Add();

            foreach (var erplistofexpendeddetail in ERPListOfExpendedDetailList)
            {
                erplistofexpendeddetail.RefID = mainid;
                erplistofexpendeddetail.Add();
            }

            if (Model.StateNow == "正在办理")
            {
                //发送短信
                SendMainAndSms.SendMessage1(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + Model.WorkName + ")", ZWL.Common.PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()), int.Parse(FormID.ToString()), int.Parse(WorkFlowID.ToString()), Model.BeiYong1);
                //SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您有新的工作需要办理！(" + strWorkName + ")", ZWL.Common.PublicMethod.WorkWeiTuoUserList(this.TextBox5.Text.Trim()));
            }
            else
            {
                SendMainAndSms.SendMessage(CHKSMS, CHKMOB, "您的工作已经被强制结束！(" + Model.WorkName + ")", ZWL.Common.PublicMethod.GetSessionValue("UserName"));
            }


            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加新工作信息(" + Model.WorkName + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            ZWL.Common.MessageBox.ShowAndRedirect(this, "审批工作添加成功！", "ERPListOfExpendedManager.aspx");
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
        if (this.HiddenField_WenJianList.Value.Trim() == "")
        {
            this.HiddenField_WenJianList.Value = FileNameStr;
        }
        else
        {
            //ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList") + "|" + FileNameStr);
            this.HiddenField_WenJianList.Value += "|" + FileNameStr;
        }
        ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, this.HiddenField_WenJianList.Value);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected == true)
                {
                    //ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|"));
                    this.HiddenField_WenJianList.Value = this.HiddenField_WenJianList.Value.Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|");
                }
            }
            ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, this.HiddenField_WenJianList.Value);
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

    public void setDefault(ZWL.BLL.ERPListOfExpended erplistofexpended)
    {
        //合计

        this.txtAmount.Text = erplistofexpended.Amount.ToString();

        //部门

        this.txtDepartment.Text = erplistofexpended.Department;

        //登记人

        this.txtUsername.Text = erplistofexpended.Username;

        var dtl = new ZWL.BLL.ERPListOfExpendedDetail();
        var ds = dtl.GetList("MainID='" + erplistofexpended.ID + "'");
        if (ds.Tables.Count > 0)
        {
            var dt = ds.Tables[0];
            var cols = dt.Columns;
            var inithtml = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                inithtml += "ERPListOfExpendedDetail_AddRow();";
                var dr = dt.Rows[i];
                foreach (DataColumn col in cols)
                {
                    inithtml += "$('#ERPListOfExpendedDetail_" + col.ColumnName + "_" + (i + 1) + "').val('" + dr[col.ColumnName] + "');";
                }
            }
            rowCount.Value = dt.Rows.Count.ToString();
            HiddenField_InitHtml.Value = inithtml;
        }

    }

    //子表

    private string MakeERPListOfExpendedDetailListForm(List<ZWL.BLL.ERPListOfExpendedDetail> erplistofexpendeddetailList)
    {
        string HTMLModel =
            @"<tr id='ZLBodyTr' style='height: 30px;'>
                                <td style='text-align: center; background-color: #ffffff;'>
                                    {0}</td>
                
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_XMName_{0}' name='ERPListOfExpendedDetail_XMName_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{1}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_ZCLB_{0}' name='ERPListOfExpendedDetail_ZCLB_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{2}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_Summary_{0}' name='ERPListOfExpendedDetail_Summary_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{3}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_XHBH_{0}' name='ERPListOfExpendedDetail_XHBH_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{4}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_Amount_{0}' name='ERPListOfExpendedDetail_Amount_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{5}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_Budget_{0}' name='ERPListOfExpendedDetail_Budget_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{6}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_CostedAmt_{0}' name='ERPListOfExpendedDetail_CostedAmt_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{7}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_CostingAmt_{0}' name='ERPListOfExpendedDetail_CostingAmt_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{8}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_CostingPercent_{0}' name='ERPListOfExpendedDetail_CostingPercent_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{9}' /></td>
                        
                            <td style='text-align: center; background-color: #ffffff;'>
                                    <input id='ERPListOfExpendedDetail_CostedPercent_{0}' name='ERPListOfExpendedDetail_CostedPercent_{0}' type='text' style='border:0; text-align:center; width: 100px;' value='{10}' /></td>
                        
             </tr>";
        var bodyhtml = "";
        for (int i = 0; i < erplistofexpendeddetailList.Count; i++)
        {
            var dtl = erplistofexpendeddetailList[i];
            bodyhtml += string.Format(HTMLModel, i + 1
            , dtl.XMName, dtl.ZCLB, dtl.Summary, dtl.XHBH, dtl.Amount, dtl.Budget, dtl.CostedAmt, dtl.CostingAmt, dtl.CostingPercent, dtl.CostedPercent);
        }

        var tablehtml = string.Format(@"<tr><td colspan='4'><table style='width: 100%' border='1' cellpadding='0' cellspacing='0'>
                            <thead><tr><td class='TitleStyle' colspan='12' style='text-align: center; font-size:18px;'><strong>费用成本报销详细</strong></td>
                                </tr>
                                <tr>
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style1'>序号</td>
                                    
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>项目名称</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>支出类别</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>摘要</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>项目编号</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>结算金额</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>预算金额</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>已支付</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>报销费用金额</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>单项支出比例</td>
                        
                                    <td style='text-align: center; background-color: #ffffff;' class='auto-style2'>项目所有支出比例</td>
                        
                                </tr>
                            </thead>{0}
                        </table></td></tr>", bodyhtml);
        return tablehtml;
    }

    private List<ZWL.BLL.ERPListOfExpendedDetail> GetERPListOfExpendedDetailList()
    {
        var list = new List<ZWL.BLL.ERPListOfExpendedDetail>();
        var form = Request.Form;
        var rowCount = form.Get("rowCount");
        if (!string.IsNullOrEmpty(rowCount))
        {
            var length = int.Parse(rowCount);
            for (int i = 1; i <= length; i++)
            {
                if (form.AllKeys.Contains("ERPListOfExpendedDetail_" + "XMName" + "_" + i) && !string.IsNullOrEmpty(form.Get("ERPListOfExpendedDetail_" + "Summary" + "_" + i)))
                {
                    var erplistofexpended = new ZWL.BLL.ERPListOfExpendedDetail();

                    erplistofexpended.XMName = form.Get("ERPListOfExpendedDetail_XMName_" + i);
                    erplistofexpended.ZCLB = form.Get("ERPListOfExpendedDetail_ZCLB_" + i);
                    erplistofexpended.Summary = form.Get("ERPListOfExpendedDetail_Summary_" + i);
                    erplistofexpended.XHBH = form.Get("ERPListOfExpendedDetail_XHBH_" + i);
                    erplistofexpended.Amount = form.Get("ERPListOfExpendedDetail_Amount_" + i);
                    erplistofexpended.Budget = form.Get("ERPListOfExpendedDetail_Budget_" + i);
                    erplistofexpended.CostedAmt = form.Get("ERPListOfExpendedDetail_CostedAmt_" + i);
                    erplistofexpended.CostingAmt = form.Get("ERPListOfExpendedDetail_CostingAmt_" + i);
                    erplistofexpended.CostingPercent = form.Get("ERPListOfExpendedDetail_CostingPercent_" + i);
                    erplistofexpended.CostedPercent = form.Get("ERPListOfExpendedDetail_CostedPercent_" + i);


                    list.Add(erplistofexpended);
                }
            }
        }
        return list;
    }
}

