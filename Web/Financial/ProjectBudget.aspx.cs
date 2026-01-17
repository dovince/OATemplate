using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Financial_ProjectBudget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            string strid = Request.QueryString["ID"].ToString();
            ZWL.BLL.ERPProjectCost MyModel = new ZWL.BLL.ERPProjectCost();
            MyModel.GetModel(int.Parse(strid));
            this.txtXMName.Text = MyModel.XMName;
            this.txtXMID.Text = MyModel.XMBH;
            ZWL.BLL.ERPBudget buget = new ZWL.BLL.ERPBudget();
            buget = getbuget(MyModel.XMBH);
            if (buget != null)
            {

                txt住房补贴.Text = buget.住房补贴.ToString();
                txt材料费.Text = buget.材料费.ToString();
                txt工程出包费.Text = buget.工程出包费.ToString();
                txt固定资产.Text = buget.固定资产.ToString();
                txt办公费.Text = buget.办公费.ToString();
                txt差旅费.Text = buget.差旅费.ToString();
                txt水电费.Text = buget.水电费.ToString();
                txt物业管理费.Text = buget.物业管理费.ToString();
                txt交通运输费用.Text = buget.交通运输费用.ToString();
                txt邮电费用.Text = buget.邮电费用.ToString();
                txt维修费用.Text = buget.维修费用.ToString();
                txt会议费.Text = buget.会议费.ToString();
                txt培训费.Text = buget.培训费.ToString();
                txt业务招待费.Text = buget.业务招待费.ToString();
                txt劳务费.Text = buget.劳务费.ToString();
                txt租赁费.Text = buget.租赁费.ToString();
                txt税金及附加.Text = buget.税金及附加.ToString();
                txt安全生产费用.Text = buget.安全生产费用.ToString();
                txt工会经费.Text = buget.工会经费.ToString();
                txt其他费用.Text = buget.其它费用.ToString();
                txt工资及津贴_备注.Text = buget.工资及津贴_备注.ToString();
                txt节日补贴_备注.Text = buget.节日补贴_备注.ToString();
                txt养老统筹_备注.Text = buget.养老统筹_备注.ToString();
                txt福利费_备注.Text = buget.福利费_备注.ToString();
                txt劳动保护费_备注.Text = buget.劳动保护费_备注.ToString();
                txt住房公积金_备注.Text = buget.住房公积金_备注.ToString();
                txt住房补贴_备注.Text = buget.住房补贴_备注.ToString();
                txt工资及津贴.Text = buget.工资及津贴.ToString();
                txt材料费_备注.Text = buget.材料费_备注.ToString();
                txt工程出包费_备注.Text = buget.工程出包费_备注.ToString();
                txt固定资产_备注.Text = buget.固定资产_备注.ToString();
                txt办公费_备注.Text = buget.办公费_备注.ToString();
                txt差旅费_备注.Text = buget.差旅费_备注.ToString();
                txt水电费_备注.Text = buget.水电费_备注.ToString();
                txt物业管理费_备注.Text = buget.物业管理费_备注.ToString();
                txt交通运输费用_备注.Text = buget.交通运输费用_备注.ToString();
                txt邮电费用_备注.Text = buget.邮电费用_备注.ToString();
                txt维修费用_备注.Text = buget.维修费用_备注.ToString();
                txt节日补贴.Text = buget.节日补贴.ToString();
                txt会议费_备注.Text = buget.会议费_备注.ToString();
                txt培训费_备注.Text = buget.培训费_备注.ToString();
                txt业务招待费_备注.Text = buget.业务招待费_备注.ToString();
                txt劳务费_备注.Text = buget.劳务费_备注.ToString();
                txt租赁费_备注.Text = buget.租赁费_备注.ToString();
                txt税金及附加_备注.Text = buget.税金及附加_备注.ToString();
                txt安全生产费用_备注.Text = buget.安全生产费用_备注.ToString();
                txt工会经费_备注.Text = buget.工会经费_备注.ToString();
                txt其他费用_备注.Text = buget.其它费用_备注.ToString();
                txt养老统筹.Text = buget.养老统筹.ToString();
                txt福利费.Text = buget.福利费.ToString();
                txt劳动保护费.Text = buget.劳动保护费.ToString();
                txt住房公积金.Text = buget.住房公积金.ToString();
            }


        }
    }
    /// <summary>
    /// 提交按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        ZWL.BLL.ERPBudget buget = new ZWL.BLL.ERPBudget();
        buget = getbuget(this.txtXMID.Text);
        if (buget != null)
        {
            buget = setbuget(buget, this.txtXMID.Text, this);
            buget.Update();
            ZWL.Common.MessageBox.ShowAndRedirect(this, "项目成本预算修改成功！", "ProjectCost.aspx");
        }
        else
        {
            buget = new ZWL.BLL.ERPBudget();
            buget = setbuget(buget, this.txtXMID.Text, this);
            buget.Add();
            ZWL.Common.MessageBox.ShowAndRedirect(this, "项目成本预算添加成功！", "ProjectCost.aspx");
        }
    }

    public static ZWL.BLL.ERPBudget getbuget(string xmid)
    {
        ZWL.BLL.ERPBudget bugetModel = new ZWL.BLL.ERPBudget();
        DataSet ds = new DataSet();
        ds = bugetModel.GetList("XMBH='" + xmid + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            bugetModel.GetModel(int.Parse(ds.Tables[0].Rows[0]["ID"].ToString()));
            return bugetModel;
        }
        else
        {
            return null;
        }

    }
    public decimal inputcondition(string str)
    {
        decimal dec = str == "" ? 0 : decimal.Parse(str);
        return dec;
    }
    public ZWL.BLL.ERPBudget setbuget(ZWL.BLL.ERPBudget buget, string xmid, Financial_ProjectBudget thisfp)
    {
        ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
        pcmodel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
        buget.XMBH = thisfp.txtXMID.Text;
        buget.HTBH = pcmodel.HTBH;
        buget.工资及津贴 = inputcondition(thisfp.txt工资及津贴.Text);
        buget.节日补贴 = inputcondition(thisfp.txt节日补贴.Text);
        buget.养老统筹 = inputcondition(thisfp.txt养老统筹.Text);
        buget.福利费 = inputcondition(thisfp.txt福利费.Text);
        buget.劳动保护费 = inputcondition(thisfp.txt劳动保护费.Text);
        buget.住房公积金 = inputcondition(thisfp.txt住房公积金.Text);
        buget.住房补贴 = inputcondition(thisfp.txt住房补贴.Text);
        buget.材料费 = inputcondition(thisfp.txt材料费.Text);
        buget.工程出包费 = inputcondition(thisfp.txt工程出包费.Text);
        buget.固定资产 = inputcondition(thisfp.txt固定资产.Text);
        buget.办公费 = inputcondition(thisfp.txt办公费.Text);
        buget.差旅费 = inputcondition(thisfp.txt差旅费.Text);
        buget.水电费 = inputcondition(thisfp.txt水电费.Text);
        buget.物业管理费 = inputcondition(thisfp.txt物业管理费.Text);
        buget.交通运输费用 = inputcondition(thisfp.txt交通运输费用.Text);
        buget.邮电费用 = inputcondition(thisfp.txt邮电费用.Text);
        buget.维修费用 = inputcondition(thisfp.txt维修费用.Text);
        buget.会议费 = inputcondition(thisfp.txt会议费.Text);
        buget.培训费 = inputcondition(thisfp.txt培训费.Text);
        buget.业务招待费 = inputcondition(thisfp.txt业务招待费.Text);
        buget.劳务费 = inputcondition(thisfp.txt劳务费.Text);
        buget.租赁费 = inputcondition(thisfp.txt租赁费.Text);
        buget.税金及附加 = inputcondition(thisfp.txt税金及附加.Text);
        buget.安全生产费用 = inputcondition(thisfp.txt安全生产费用.Text);
        buget.工会经费 = inputcondition(thisfp.txt工会经费.Text);
        buget.其它费用 = inputcondition(thisfp.txt其他费用.Text);


        buget.工资及津贴_备注 = thisfp.txt工资及津贴_备注.Text;
        buget.节日补贴_备注 = thisfp.txt节日补贴_备注.Text;
        buget.养老统筹_备注 = thisfp.txt养老统筹_备注.Text;
        buget.福利费_备注 = thisfp.txt福利费_备注.Text;
        buget.劳动保护费_备注 = thisfp.txt劳动保护费_备注.Text;
        buget.住房公积金_备注 = thisfp.txt住房公积金_备注.Text;
        buget.住房补贴_备注 = thisfp.txt住房补贴_备注.Text;
        buget.材料费_备注 = thisfp.txt材料费_备注.Text;
        buget.工程出包费_备注 = thisfp.txt工程出包费_备注.Text;
        buget.固定资产_备注 = thisfp.txt固定资产_备注.Text;
        buget.办公费_备注 = thisfp.txt办公费_备注.Text;
        buget.差旅费_备注 = thisfp.txt差旅费_备注.Text;
        buget.水电费_备注 = thisfp.txt水电费_备注.Text;
        buget.物业管理费_备注 = thisfp.txt物业管理费_备注.Text;
        buget.交通运输费用_备注 = thisfp.txt交通运输费用_备注.Text;
        buget.邮电费用_备注 = thisfp.txt邮电费用_备注.Text;
        buget.维修费用_备注 = thisfp.txt维修费用_备注.Text;
        buget.会议费_备注 = thisfp.txt会议费_备注.Text;
        buget.培训费_备注 = thisfp.txt培训费_备注.Text;
        buget.业务招待费_备注 = thisfp.txt业务招待费_备注.Text;
        buget.劳务费_备注 = thisfp.txt劳务费_备注.Text;
        buget.租赁费_备注 = thisfp.txt租赁费_备注.Text;
        buget.税金及附加_备注 = thisfp.txt税金及附加_备注.Text;
        buget.安全生产费用_备注 = thisfp.txt安全生产费用_备注.Text;
        buget.工会经费_备注 = thisfp.txt工会经费_备注.Text;
        buget.其它费用_备注 = thisfp.txt其他费用_备注.Text;
        return buget;
    }
}