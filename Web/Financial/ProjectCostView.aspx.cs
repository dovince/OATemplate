using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Financial_ProjectCostView : System.Web.UI.Page
{
    public string PiLiangSet = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            string strid = Request.QueryString["ID"].ToString();
            decimal costsum = decimal.Parse(Request.QueryString["sum"].ToString());
            this.txt成本支出合计.Text = String.Format("{0:###,###,##0.00}", costsum);
            ZWL.BLL.ERPProjectCost MyModel = new ZWL.BLL.ERPProjectCost();
            MyModel.GetModel(int.Parse(strid));
            this.txtXMName.Text = MyModel.XMName;
            this.txtXMID.Text = MyModel.XMBH;

            this.txtZYLB.Text = MyModel.ZYLB;
            //绑定时将几个时间设置为当前时间，修改时也方便改。
            this.txtXMstarttime.Text = MyModel.XMBeginTime.ToString("yyyy-MM-dd");
            this.txtXMendtime.Text = MyModel.XMEndTime.ToString("yyyy-MM-dd");

            this.txtXMState.Text = MyModel.XMState;
            this.txtHTBH.Text = MyModel.HTBH;
            this.txtXMJingFei.Text = String.Format("{0:###,###,##0.00}", Double.Parse(MyModel.HTJE.ToString()));
            this.txtXMbumen.Text = MyModel.XMBM;
            this.txtXMFZR.Text = MyModel.XMFZR;

            txt结算金额.Text = String.Format("{0:###,###,##0.00}", Double.Parse(MyModel.JSJE.ToString()));
            
            PiLiangSet += "document.getElementById(\"infoFrame\").src='CostDetailInfo.aspx?type=8475&ID="+MyModel.ID+"';";
            PiLiangSet += "document.getElementById(\"budgetFrame\").src='BudgetDetailInfo.aspx?ShowPage=false&type=8475&ID=" + MyModel.ID+"';";
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }
}