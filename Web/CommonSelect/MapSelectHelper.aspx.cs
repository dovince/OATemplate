using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonSelect_MapSelectHelper : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var address = Get("address");
            if (!string.IsNullOrEmpty(address))
            {
                txtSearch.Value = address;
            }
        }
    }
}