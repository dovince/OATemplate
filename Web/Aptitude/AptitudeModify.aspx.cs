using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Aptitude_AptitudeModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            var id = int.Parse(Request.QueryString.Get("ID"));
            LoadUI(id);
        }
    }

    /// <summary>
    /// 表单的提交部分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnModify_Click(object sender, ImageClickEventArgs e)
    {
        var msg = "";
        if (!ValidateInput(ref msg))
        {
            MessageBox.Show(this, msg);
        }
        else
        {
            try
            {
                var nWorkToDoModel = new ZWL.BLL.ERPNWorkToDo();
                var aptWorkModel = new ZWL.BLL.AptitudeWork();
                var aptWorkDtlModel = new ZWL.BLL.AptitudeWorkDetail();
                var erpNFormModel = new ZWL.BLL.ERPNForm();
                var dt = DateTime.Now;
                var userName = PublicMethod.GetSessionValue("UserName");
                var department = PublicMethod.GetSessionValue("Department");
                var workName = userName + "--资质使用申请审批表(" + dt.ToString("yyyy/MM/dd") + ")";
                var emId = 0;
                int.TryParse(Request.QueryString.Get("ID"), out emId);
                aptWorkModel.GetModel(emId);
                aptWorkModel.ID = emId;
                var strAptitudeFileList = this.HiddenField_AptitudeFiles.Value;

                nWorkToDoModel.GetModel(int.Parse(aptWorkModel.NWorkID));
                nWorkToDoModel.ID = int.Parse(aptWorkModel.NWorkID);
                erpNFormModel.GetModel(nWorkToDoModel.FormID.Value);
                
                aptWorkModel.CreatedDate = dt;
                aptWorkModel.Department = txt使用单位.Text;
                aptWorkModel.StartDate = DateTime.Parse(txtQJSJStart.Text);
                aptWorkModel.EndDate = DateTime.Parse(txtQJSJEnd.Text);
                aptWorkModel.Comment = this.txtComment.Text;
                aptWorkModel.OtherAptitude = txtOtherLicense.Text;
                aptWorkModel.UsingRange = txt使用范围.Text;
                aptWorkModel.ProjectName = txt项目名称.Text;
                aptWorkModel.Operator = userName;
                aptWorkModel.WorkName = workName;

                var result = aptWorkModel.Update();

                nWorkToDoModel.TimeStr = dt;
                //替换控件中的值到表单中
                nWorkToDoModel.FormContent = GetFormHtml(nWorkToDoModel.FormID.Value);
                nWorkToDoModel.FuJianList = HiddenField_WenJianList.Value;
                var lateTime = DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + nWorkToDoModel.JieDianID);
                nWorkToDoModel.LateTime = DateTime.Now.AddHours(double.Parse(lateTime));
                nWorkToDoModel.BeiYong1 = workName;

                nWorkToDoModel.Update();

                var existedList = aptWorkDtlModel.GetModelList(" RefID = " + aptWorkModel.ID);
                var arrList = strAptitudeFileList.Split(';').ToList();
                foreach (var item in arrList)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    if (existedList.Any(r => r.AptFileStateID==int.Parse(item))) continue;
                    else
                    {
                        var m = new ZWL.BLL.AptitudeWorkDetail
                        {
                            AptFileStateID = int.Parse(item),
                            AptWorkID = emId,
                        }.Add();
                    }

                }

                foreach (var item in existedList)
                {
                    if (!arrList.Contains(item.AptFileStateID.ToString()))
                    {
                        item.Delete(item.ID);
                    }
                }

                if (result)
                {
                    MessageBox.ShowAndRedirect(this, "修改表单成功！", "AptitudeList.aspx");
                }
                else
                {
                    MessageBox.Show(this, "修改表单出错，请重试！");
                }
            }
            catch
            {
                MessageBox.Show(this, "修改表单出错，请重试！");
            }
        }
    }

    protected void btnaddsomething_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.HiddenField_xmqqbh.Value))
        {
            ZWL.BLL.ERPXMQQDJ XMQQXXDJ = new ZWL.BLL.ERPXMQQDJ();
            XMQQXXDJ.GetModel(this.HiddenField_xmqqbh.Value);
            this.txt项目名称.Text = XMQQXXDJ.XMName;
            this.lb资质证照.Text = this.HiddenField_html.Value;
        }
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
    private void LoadUI(int id)
    {
        var m = new ZWL.BLL.AptitudeWork();
        m.GetModel(id);

        var t = new ZWL.BLL.ERPNWorkToDo();
        t.GetModel(int.Parse(m.NWorkID));

        var formContent = t.FormContent;

        var strsplit = "<!--split-->";
        var aptitudeFileList = "";

        if (!string.IsNullOrEmpty(formContent) && formContent.Contains(strsplit))
        {
            var splitContent = Regex.Split(formContent, strsplit, RegexOptions.IgnoreCase);
            aptitudeFileList = splitContent[1];
        }
        this.lb资质证照.Text = aptitudeFileList;
        this.txt项目名称.Text = m.ProjectName;
        this.txtOtherLicense.Text = m.OtherAptitude;
        this.txt使用范围.Text = m.UsingRange;
        this.txtComment.Text = m.Comment;
        this.txtQJSJStart.Text = m.StartDate.Value.ToString("yyyy-MM-dd");
        this.txtQJSJEnd.Text = m.EndDate.Value.ToString("yyyy-MM-dd");
        this.txt申请人.Text = PublicMethod.GetSessionValue("UserName");
        this.txt使用单位.Text = PublicMethod.GetSessionValue("Department");
        this.txt申请时间.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //设置上传的附件为空
        this.HiddenField_WenJianList.Value = t.FuJianList;
        this.HiddenField_html.Value = "";
        PublicMethod.BindDDL(CheckBoxList1, t.FuJianList);
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
            foreach (var item in HiddenField_AptitudeFiles.Value.Split(';'))
            {
                if (string.IsNullOrEmpty(item) || item.Contains("原件") || item.Contains("复印件")) continue;
                var count = DbHelperSQL.GetSHSLInt1(string.Format(@"select COUNT(1)  FROM [Aptitude] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID
  where t.StateNow='正在办理' and a.[是否归还]='否' and a.[资质证照名称] like '%{0}%'", item));
                if (count > 0)
                {
                    msg = "资质证照[" + item + "]尚未归还,请确认归还后再申请！";
                    return false;
                }
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
        var workName = userName + "--资质使用申请审批表(" + dt.ToString("yyyy/MM/dd") + ")";


        var aptlist = HiddenField_html.Value;
        if (!string.IsNullOrEmpty(aptlist) && aptlist.Contains("<SPAN style=\"COLOR: black\"></SPAN>"))
        {
            var splitContent = Regex.Split(aptlist, "<SPAN style=\"COLOR: black\"></SPAN>", RegexOptions.IgnoreCase);
            aptlist = splitContent[1];
        }

        //插入当前用户的印章
        //绑定所有印章
        var strImgPath = DbHelperSQL.GetSHSL("select ImgPath from ERPYinZhang where YinZhangLeiBie='私人印章' and UserName='"
                                            + PublicMethod.GetSessionValue("UserName") + "' order by YinZhangLeiBie desc");
        strImgPath = "../UploadFile/" + strImgPath;

        result = formModel.ContentStr;
        result = result.Replace("用户自定义控件-项目名称", workName)
                       .Replace("用户自定义控件-资质证照名称", aptlist)
                       .Replace("用户自定义控件-其他证照", txtOtherLicense.Text)
                       .Replace("用户自定义控件-使用范围", txt使用范围.Text)
                       .Replace("用户自定义控件-从年", dt.Year.ToString())
                       .Replace("用户自定义控件-至年", dt.Year.ToString())
                       .Replace("用户自定义控件-从月", dt.Month.ToString().PadLeft(2, '0'))
                       .Replace("用户自定义控件-至月", dt.Month.ToString().PadLeft(2, '0'))
                       .Replace("用户自定义控件-从日", dt.Day.ToString().PadLeft(2, '0'))
                       .Replace("用户自定义控件-至日", dt.Day.ToString().PadLeft(2, '0'))
                       .Replace("用户自定义控件-单位名称", txt使用单位.Text)
                       .Replace("用户自定义控件-经办人签字", strImgPath)
                       .Replace("用户自定义控件-经办人日期", dt.ToString("yyyy/MM/dd"))
                       ;

        return result;
    }
}