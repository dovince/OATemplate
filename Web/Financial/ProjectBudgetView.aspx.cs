using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_ProjectBudgetView : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            if (Request.QueryString["ID"] != null)
            {
                var MyModel = new ZWL.BLL.ERPBudgetDetail();
                MyModel.GetModel(Id);
                if (MyModel != null)
                {
                    var proj = new ZWL.BLL.ERPProjectCost();
                    proj.GetModel(MyModel.ParentId.Value);

                    var ht = new ZWL.BLL.ERPHeTong();
                    if (!string.IsNullOrEmpty(MyModel.HTBH))
                        ht.GetstrModel(MyModel.HTBH);

                    var contractAmt = !string.IsNullOrEmpty(MyModel.HTBH) ?
                        proj.HTJE : ht.HTJE;

                    txtContractAmt.Text = PublicMethod.FormatMoney(contractAmt);

                    txtXMName.Text = proj.XMName;
                    txtXMID.Text = MyModel.XMBH;
                    txtHTID.Text = MyModel.HTBH;
                    txtZYTYPE.Text = proj.ZYLB;
                    txtDepartment.Text = proj.XMBM;
                    txtBizpat.Text = ht.JYFS;

                    if (MyModel != null)
                    {
                        txt材料费.Text = MyModel.材料费.ToString();
                        txt工程出包费.Text = MyModel.工程出包费.ToString();
                        txt办公费.Text = MyModel.办公费.ToString();
                        txt差旅费.Text = MyModel.差旅费.ToString();
                        txt交通运输费用.Text = MyModel.交通运输费用.ToString();
                        txt邮电费用.Text = MyModel.邮电费用.ToString();
                        txt维修费用.Text = MyModel.维修费用.ToString();
                        txt劳务费.Text = MyModel.劳务费.ToString();
                        txt租赁费.Text = MyModel.租赁费.ToString();
                        txt安全生产费用.Text = MyModel.安全生产费用.ToString();
                        txt其它费用.Text = MyModel.其它费用.ToString();
                        txt工资及津贴.Text = MyModel.工资及津贴.ToString();
                        txtComment.Text = MyModel.Comment;
                    }

                    if (contractAmt > 0)
                    {
                        var kv = new ZWL.BLL.ERPKeyValue();
                        var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
                        foreach (var item in list)
                        {
                            var result = PublicMethod.GetDecimal(PublicMethod.GetModelPropertyValueByName(MyModel, item.Key3)) / contractAmt;
                            ((System.Web.UI.WebControls.Label)this.FindControl("lbl" + item.Key3)).Text = Math.Round(result, 2) * 100 + "%";
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 提交按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        var msg = string.Empty;
        if (!ValidateInput(ref msg))
        {
            MessageBox.Show(this, msg);
        }
        else
        {
            var xmbh = Request.QueryString["XMBH"].ToString();
            var htbh = string.Empty;
            var proj = new ZWL.BLL.ERPXMJBXX();
            proj = proj.GetModelBySqlWhere("XMBH='" + xmbh + "'");
            if (proj != null)
            {
                htbh = proj.HTBH;
            }
            var version = DbHelperSQL.GetSHSLInt1(string.Format(@"select COUNT(1) from ERPBudgetDetail where XMBH='{0}'", xmbh));
            var buget = new ZWL.BLL.ERPBudgetDetail
            {
                Version = version + 1,
                XMBH = xmbh,
                HTBH = htbh,
                工资及津贴 = PublicMethod.GetDecimal(txt工资及津贴.Text),
                工程出包费 = PublicMethod.GetDecimal(txt工程出包费.Text),
                材料费 = PublicMethod.GetDecimal(txt材料费.Text),
                租赁费 = PublicMethod.GetDecimal(txt租赁费.Text),
                劳务费 = PublicMethod.GetDecimal(txt劳务费.Text),
                安全生产费用 = PublicMethod.GetDecimal(txt安全生产费用.Text),
                办公费 = PublicMethod.GetDecimal(txt办公费.Text),
                维修费用 = PublicMethod.GetDecimal(txt维修费用.Text),
                交通运输费用 = PublicMethod.GetDecimal(txt交通运输费用.Text),
                差旅费 = PublicMethod.GetDecimal(txt差旅费.Text),
                邮电费用 = PublicMethod.GetDecimal(txt邮电费用.Text),
                其它费用 = PublicMethod.GetDecimal(txt其它费用.Text),
                CreatedTime = DateTime.Now,
                Comment = txtComment.Text,
            };
            var result = buget.Add();
            if (result > 0)
            {
                MessageBox.ShowAndRedirect(this, string.Format("项目[{0}]成本预算{1}成功！", xmbh, version == 0 ? "添加" : "调整"), "BudgetDetailInfo.aspx?ID=" + Request.QueryString.Get("ID"));
            }
            else
            {
                MessageBox.Show(this, "项目成本预算" + (version == 0 ? "添加" : "调整") + "失败！请重试.");
            }
        }
    }
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var amount = decimal.Parse("0.00");
        var budModel = new ZWL.BLL.ERPBudgetDetail();
        budModel.GetModel(Id);
        var proModel = new ZWL.BLL.ERPProjectCost();
        proModel.GetModel(budModel.ParentId.Value);
        if (proModel != null)
            amount = DataHelper.GetProjectCostJieSuanAmt(proModel.ID);
        var htbh = string.Empty;
        var xmbh = txtXMID.Text;
        var proj = new ZWL.BLL.ERPXMJBXX();
        proj = proj.GetModelByXMBH(proModel.XMBH);
        if (proj != null)
        {
            var kv = new ZWL.BLL.ERPKeyValue();
            var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
            foreach (var item in list)
            {
                var op = this.FindControl("txt" + item.Key3);
                if (op == null) continue;
                var val = PublicMethod.GetDecimal(((TextBox)op).Text);

                var perc = DbHelperSQL.GetSHSL(string.Format(@"select Value1 from [ERPKeyValue] where Category='BudgetPercContrl'
                            and Key3 ='{0}'--费用科目
                            and Key2 = (select Value1 from [ERPKeyValue] where Category='BudgetType'
                            and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", item.Key3, proj.ZYLB));

                if (!string.IsNullOrEmpty(perc))
                {
                    if (perc.Contains("%"))
                    {
                        if (amount > 0)
                        {
                            var comp = PublicMethod.GetDecimal(perc.TrimEnd('%')) / 100;
                            var target = val / amount;
                            if (target > comp)
                            {
                                msg = string.Format(@"当前输入的[{0}]的值[{3}],其预算占比例为[{1}],超出预算比例控制[{2}],请检查后重试.", item.Key3, PublicMethod.FormatDecimalString(target * 100) + "%", perc, val);
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
    public decimal inputcondition(string str)
    {
        decimal dec = str == "" ? 0 : decimal.Parse(str);
        return dec;
    }
}