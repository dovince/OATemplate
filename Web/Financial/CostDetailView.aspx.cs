using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Financial_CostDetailView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            string strJiaoSe = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");
            string strbumen = ZWL.Common.PublicMethod.GetSessionValue("Department");
            //绑定项目编号
            ZWL.BLL.ERPCostDetail costdetail = new ZWL.BLL.ERPCostDetail();
            costdetail.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            if (costdetail != null)
            {
                txt住房补贴.Text = costdetail.住房补贴.ToString();
                txt材料费.Text = costdetail.材料费.ToString();
                txt工程出包费.Text = costdetail.工程出包费.ToString();
                txt固定资产.Text = costdetail.固定资产.ToString();
                txt办公费.Text = costdetail.办公费.ToString();
                txt差旅费.Text = costdetail.差旅费.ToString();
                txt水电费.Text = costdetail.水电费.ToString();
                txt物业管理费.Text = costdetail.物业管理费.ToString();
                txt交通运输费用.Text = costdetail.交通运输费用.ToString();
                txt邮电费用.Text = costdetail.邮电费用.ToString();
                txt维修费用.Text = costdetail.维修费用.ToString();
                txt会议费.Text = costdetail.会议费.ToString();
                txt培训费.Text = costdetail.培训费.ToString();
                txt业务招待费.Text = costdetail.业务招待费.ToString();
                txt劳务费.Text = costdetail.劳务费.ToString();
                txt租赁费.Text = costdetail.租赁费.ToString();
                txt税金及附加.Text = costdetail.税金及附加.ToString();
                txt安全生产费用.Text = costdetail.安全生产费用.ToString();
                txt工会经费.Text = costdetail.工会经费.ToString();
                txt其他费用.Text = costdetail.其它费用.ToString();
                txt工资及津贴_备注.Text = costdetail.工资及津贴_备注.ToString();
                txt节日补贴_备注.Text = costdetail.节日补贴_备注.ToString();
                txt养老统筹_备注.Text = costdetail.养老统筹_备注.ToString();
                txt福利费_备注.Text = costdetail.福利费_备注.ToString();
                txt劳动保护费_备注.Text = costdetail.劳动保护费_备注.ToString();
                txt住房公积金_备注.Text = costdetail.住房公积金_备注.ToString();
                txt住房补贴_备注.Text = costdetail.住房补贴_备注.ToString();
                txt工资及津贴.Text = costdetail.工资及津贴.ToString();
                txt材料费_备注.Text = costdetail.材料费_备注.ToString();
                txt工程出包费_备注.Text = costdetail.工程出包费_备注.ToString();
                txt固定资产_备注.Text = costdetail.固定资产_备注.ToString();
                txt办公费_备注.Text = costdetail.办公费_备注.ToString();
                txt差旅费_备注.Text = costdetail.差旅费_备注.ToString();
                txt水电费_备注.Text = costdetail.水电费_备注.ToString();
                txt物业管理费_备注.Text = costdetail.物业管理费_备注.ToString();
                txt交通运输费用_备注.Text = costdetail.交通运输费用_备注.ToString();
                txt邮电费用_备注.Text = costdetail.邮电费用_备注.ToString();
                txt维修费用_备注.Text = costdetail.维修费用_备注.ToString();
                txt节日补贴.Text = costdetail.节日补贴.ToString();
                txt会议费_备注.Text = costdetail.会议费_备注.ToString();
                txt培训费_备注.Text = costdetail.培训费_备注.ToString();
                txt业务招待费_备注.Text = costdetail.业务招待费_备注.ToString();
                txt劳务费_备注.Text = costdetail.劳务费_备注.ToString();
                txt租赁费_备注.Text = costdetail.租赁费_备注.ToString();
                txt税金及附加_备注.Text = costdetail.税金及附加_备注.ToString();
                txt安全生产费用_备注.Text = costdetail.安全生产费用_备注.ToString();
                txt工会经费_备注.Text = costdetail.工会经费_备注.ToString();
                txt其他费用_备注.Text = costdetail.其它费用_备注.ToString();
                txt养老统筹.Text = costdetail.养老统筹.ToString();
                txt福利费.Text = costdetail.福利费.ToString();
                txt劳动保护费.Text = costdetail.劳动保护费.ToString();
                txt住房公积金.Text = costdetail.住房公积金.ToString();
                txt期数.Text = costdetail.期间.ToString();
                txtXMBH.Text = costdetail.XMBH.ToString();
                //txt摘要.Text = costdetail.beiyong1.ToString();
            }
            
        }
    }
    protected void ImageButton_goback_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }
}