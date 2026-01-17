using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class Aptitude_AptitudeFileAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        var msg = string.Empty;
        if (ValidateInput(ref msg))
        {
            var aptFile = new ZWL.BLL.AptitudeFile()
            {
                AptitudeName = txtAptitudeName.Text,
                Department = txtDepartment.Text,
                IsActive = int.Parse(rblActive.SelectedValue),
            };
            var aptId = aptFile.Add();
            for (int i = 1; i < 5; i++)
            {
                new ZWL.BLL.AptitudeFileState
                {
                    AptFileID = aptId,
                    Type = i,
                    State = 0,
                }.Add();
            }

            if (aptId > 0)
            {
                MessageBox.ShowAndRedirect(this,"资质添加成功!","AptitudeManage.aspx");
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