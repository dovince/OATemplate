using System;
using System.Web.UI;

public partial class NWorkFlow_NWorkFlowFrame : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
        }
    }
}
