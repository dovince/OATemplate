using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class SelectControl_ManageHTState : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            var xModel = new ZWL.BLL.ERPXMJBXX();
            xModel.GetModelByWorkId(Id);
            XMBH.Text = xModel.XMBH;
            XMName.Text = xModel.XMName;
            HTState.SelectedValue = xModel.HTState;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //var todoModel = new ZWL.BLL.ERPNWorkToDo();
        //todoModel.GetModel(Id);
        var xModel = new ZWL.BLL.ERPXMJBXX();
        xModel.GetModelByWorkId(Id);
        xModel.HTState = HTState.SelectedValue;
        xModel.Update();
        //MessageBox.Show(this, "成功");
        var sb = new StringBuilder();
        sb.AppendFormat("<script language='javascript' defer>alert('成功将合同状态修改为[{0}].');", xModel.HTState);
        sb.Append(@"if (window.parent.length > 0){ parent.location.reload();window.parent.$('#win_select').window('close');}else{window.close();}");
        sb.Append("</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "message", sb.ToString());


    }
}
