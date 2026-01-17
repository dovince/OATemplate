

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Windows.Forms;
using ZWL.BLL;
using ZWL.DBUtility;

public partial class RequireCreate_ERPCaiDanAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            this.HiddenField_UserName.Value = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            this.HiddenField_Department.Value = ZWL.Common.PublicMethod.GetSessionValue("Department");
            //设置上传的附件为空
            this.HiddenField_WenJianList.Value = "";
            //绑定工作名称
            this.txtWorkName.Text = ZWL.Common.PublicMethod.GetSessionValue("UserName") + "--菜单信息(" + DateTime.Now.ToShortDateString() + ")";

            this.txtDJtime.Text = DateTime.Now.ToShortDateString();

            //绑定编号和名称
            if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
            {
                var erpcaidan = new ZWL.BLL.ERPCaiDan();
                erpcaidan.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
                setDefault(erpcaidan);
            }
        }
    }
    
    /// <summary>
    /// 表单的提交部分
    /// </summary>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //在提交表单的时候重新获取编号
        DateTime defaultime = DateTime.Now;
        //ZWL.Common.PublicMethod.GetDefaultTime(out defaultime);

        ZWL.BLL.ERPCaiDan erpcaidan = new ZWL.BLL.ERPCaiDan();
        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            erpcaidan.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
        }

        erpcaidan.WorkName = this.txtWorkName.Text;
        if (string.IsNullOrEmpty(this.txtDJtime.Text))
        {
            erpcaidan.DengJiTime = defaultime;//以此为默认时间
        }
        else
        {
            DateTime djtime = DateTime.Parse(this.txtDJtime.Text);
            erpcaidan.DengJiTime = DateTime.Parse(djtime.ToShortDateString());
        }

        //展示日期起

        if (string.IsNullOrEmpty(this.txtZhanShiRiQiQi.Text))
        {
            erpcaidan.ZhanShiRiQiQi = defaultime;//以此为默认时间
        }
        else
        {
            DateTime zhanshiriqiqi = DateTime.Parse(this.txtZhanShiRiQiQi.Text);
            erpcaidan.ZhanShiRiQiQi = DateTime.Parse(zhanshiriqiqi.ToShortDateString());
        }
            
        //展示日期止

        if (string.IsNullOrEmpty(this.txtZhanShiRiQiZhi.Text))
        {
            erpcaidan.ZhanShiRiQiZhi = defaultime;//以此为默认时间
        }
        else
        {
            DateTime zhanshiriqizhi = DateTime.Parse(this.txtZhanShiRiQiZhi.Text);
            erpcaidan.ZhanShiRiQiZhi = DateTime.Parse(zhanshiriqizhi.ToShortDateString());
        }
            
        //菜单图片
//erpcaidan.CaiDanTuPian = UploadFiles.Result;
erpcaidan.CaiDanTuPian = this.HiddenField_WenJianList.Value;
        

        //修改时间
        erpcaidan.ModifyTime = defaultime;//以此为默认时间
            
        //用户名
erpcaidan.UserName = this.HiddenField_UserName.Value;

        //部门
erpcaidan.BuMen = this.HiddenField_Department.Value;

        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            erpcaidan.Update();//更新菜单管理信息

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改了菜单管理信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            ZWL.Common.MessageBox.ShowAndRedirect(this, "菜单管理信息变更成功！", "ERPCaiDanManager.aspx");
        }
        else
        {
            //新增
            var mainid = erpcaidan.Add();

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加新工作信息(" + this.txtWorkName.Text + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            ZWL.Common.MessageBox.ShowAndRedirect(this, "审批工作添加成功！", "ERPCaiDanManager.aspx");
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
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {

                Response.Write("<script>window.open('../FlexPaperFlash/SWFShow.aspx?f=" + this.CheckBoxList1.SelectedItem.Value + "');</script>");
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

    public void setDefault(ZWL.BLL.ERPCaiDan erpcaidan)
    {

        //展示日期起

        this.txtZhanShiRiQiQi.Text = erpcaidan.ZhanShiRiQiQi != null ? erpcaidan.ZhanShiRiQiQi.ToString("yyyy-MM-dd") : "";
            
        //展示日期止

        this.txtZhanShiRiQiZhi.Text = erpcaidan.ZhanShiRiQiZhi != null ? erpcaidan.ZhanShiRiQiZhi.ToString("yyyy-MM-dd") : "";

        //菜单图片
        this.HiddenField_WenJianList.Value = erpcaidan.CaiDanTuPian;
        ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, this.HiddenField_WenJianList.Value);
        //UploadFiles.Init(erpcaidan.CaiDanTuPian);

        //修改时间

        //this.txtModifyTime.Text = erpcaidan.ModifyTime != null ? erpcaidan.ModifyTime.ToString("yyyy-MM-dd") : "";

        //用户名

        this.HiddenField_UserName.Value = erpcaidan.UserName;

        //部门

        this.HiddenField_Department.Value = erpcaidan.BuMen;

    }

//子表
    
}

