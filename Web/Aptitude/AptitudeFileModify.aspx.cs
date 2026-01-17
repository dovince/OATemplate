using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class Aptitude_AptitudeFileModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            var id = 0;
            int.TryParse(Request.QueryString.Get("ID"),out id);
            HiddenField_ID.Value = id.ToString();
            var apt = new ZWL.BLL.AptitudeFile();
            apt.GetModel(id);
            if (apt.ID > 0)
            {
                HiddenField_ID.Value = id.ToString();
                txtDepartment.Text = apt.Department;
                txtAptitudeName.Text = apt.AptitudeName;
                rblActive.SelectedValue = apt.IsActive.ToString();
            }
            
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        var msg = string.Empty;
        if (ValidateInput(ref msg))
        {
            var id = int.Parse(HiddenField_ID.Value);
            var aptFile = new ZWL.BLL.AptitudeFile();
            aptFile.GetModel(id);
            if (aptFile.ID > 0)
            {
                aptFile.AptitudeName = txtAptitudeName.Text;
                aptFile.Department = txtDepartment.Text;
                aptFile.IsActive = int.Parse(rblActive.SelectedValue);
            }
            var result = aptFile.Update();
            
            if (result)
            {
                MessageBox.ShowAndRedirect(this, "资质修改成功!", "AptitudeManage.aspx");
            }
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        if (string.IsNullOrEmpty(txtDepartment.Text))
        {
            msg = "请输入资质单位!";
            return false;
        }
        if (string.IsNullOrEmpty(txtAptitudeName.Text))
        {
            msg = "请输入资质名称!";
            return false;
        }

        return result;
    }
}