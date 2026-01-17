using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_CostDetailModify : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();

            string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
            string strbumen = PublicMethod.GetSessionValue("Department");
            //绑定项目编号
            PublicMethod.CheckSession();
            decimal costsum = PublicMethod.GetDecimal(Request.QueryString["sum"]);
            txt合计.Text = String.Format("{0:###,###,##0.00}", costsum);
            ZWL.BLL.ERPCostDetail costdetail = new ZWL.BLL.ERPCostDetail();
            costdetail.GetModel(Id);
            ZWL.BLL.ERPProjectCost projectcost = new ZWL.BLL.ERPProjectCost();
            if (costdetail.ParentId.HasValue)
                projectcost.GetModel(costdetail.ParentId.Value);
            this.txtXMName.Text = projectcost.XMName;
            this.txtXMID.Text = costdetail.XMBH;
            hdfXMID.Value = costdetail.XMBH;

            if (costdetail != null)
            {
                txt住房补贴.Text = costdetail.住房补贴.ToString();
                txt材料费.Text = costdetail.材料费.ToString();
                txt工程出包费.Text = costdetail.工程出包费.ToString();
                this.txt工程出包费.Text = costdetail.工程出包费.ToString();
                this.TextBox_zkjcf.Text = costdetail.钻探出包费.ToString();
                this.TextBox_zkjc.Text = costdetail.钻孔进尺.ToString();
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
                txt印刷费.Text = costdetail.印刷费.ToString();
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
                txt期间.Text = costdetail.期间.ToString();
                txt录入日期.Text = costdetail.beiyong2.ToString();
                txt摘要.Text = costdetail.beiyong1.ToString();
            }

            ZWL.BLL.ERPXMJBXX xmjbxx = new ZWL.BLL.ERPXMJBXX();
            xmjbxx.GetModel(projectcost.XMBH);
            if (xmjbxx.XMBH != null)
            {
                this.txt承接部门.Text = xmjbxx.XMBM.ToString();
                this.txt专业类别.Text = xmjbxx.ZYLB.ToString();
                this.txt合同金额.Text = String.Format("{0:###,###,##0.00}", xmjbxx.XMJF == null ? 0.00M : xmjbxx.XMJF);
                string strcjbm = this.txt承接部门.Text;
                this.txt上季度合同登记总额.Text = String.Format("{0:###,###,##0.00}", rethtsum(strcjbm));
            }
            else
            {
                this.txt承接部门.Text = projectcost.XMBM.ToString();
                this.txt专业类别.Text = projectcost.ZYLB.ToString();
                this.txt合同金额.Text = String.Format("{0:###,###,##0.00}", projectcost.XMJF == null ? 0.00M : projectcost.XMJF);
                this.txt上季度合同登记总额.Text = String.Format("{0:###,###,##0.00}", "0");
            }


        }
    }

    #region 上季度合同总额模块
    public static decimal rethtsum(string strbm)
    {
        var array2d = retseasondata();
        var num = retseasonnum(DateTime.Now.Month);
        string stridlist = "";
        //该项目的承接部门上个季度的项目经费总和
        stridlist = PublicMethod.GetNWorkToDoIDList("54");
        DataSet seasoncount = ZWL.DBUtility.DbHelperSQL.GetDataSet("select sum(xm.XMJF) 上季度项目经费总额 from ERPXMJBXX xm where  xm.XMBH in (" + stridlist + ") and xm.XMBM='" + strbm + "' and xm.DJTime between '" + array2d[num, 0] + "' and '" + array2d[num, 1] + "'");
        return seasoncount.Tables[0].Rows[0]["上季度项目经费总额"] == seasoncount.Tables[0].Rows[0]["上季度项目经费总额"] ? 0.00M : decimal.Parse(seasoncount.Tables[0].Rows[0]["上季度项目经费总额"].ToString());
    }
    public static string[,] retseasondata()
    {
        int hyear = DateTime.Now.Year;
        int hmon = DateTime.Now.Month;
        string[,] array2d = new string[4, 2];
        array2d[0, 0] = hyear - 1 + "-10-1 00:00:00";
        array2d[0, 1] = hyear - 1 + "-12-31 23:59:59";

        array2d[1, 0] = hyear + "-1-1 00:00:00";
        array2d[1, 1] = hyear + "-3-31 23:59:59";

        array2d[2, 0] = hyear + "-4-1 00:00:00";
        array2d[2, 1] = hyear + "-6-30 23:59:59";

        array2d[3, 0] = hyear + "-7-1 00:00:00";
        array2d[3, 1] = hyear + "-9-30 23:59:59";

        return array2d;

    }
    public static int retseasonnum(int mon)
    {
        int num = 0;
        if (mon >= 1 && mon <= 3)
        {
            num = 0;
        }
        else if (mon >= 4 && mon <= 6)
        {
            num = 1;
        }
        else if (mon >= 7 && mon <= 9)
        {
            num = 2;
        }
        else if (mon >= 10 && mon <= 12)
        {
            num = 3;
        }
        return num;
    }
    #endregion
    /// <summary>
    /// 提交按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        var msg = string.Empty;
        if (!ValidateInput(ref msg))
        {
            MessageBox.Show(this, msg);
        }
        else
        {
            ZWL.BLL.ERPCostDetail costdetail = new ZWL.BLL.ERPCostDetail();
            costdetail.GetModel(Id);
            if (costdetail != null)
            {
                var shots = EditShot(costdetail);
                costdetail = setcostdetail(costdetail, this.txtXMID.Text, this);
                costdetail.ID = Id;
                costdetail.Update();
                EditLog(shots, costdetail);
                ZWL.BLL.ERPProjectCost projectcost = new ZWL.BLL.ERPProjectCost();
                DataSet dsid = projectcost.GetList("XMBH='" + costdetail.XMBH + "'");
                MessageBox.ShowAndRedirect(this, "项目支出信息修改成功！", "CostDetailInfo.aspx?ID=" + dsid.Tables[0].Rows[0]["ID"].ToString());
            }
        }
    }

    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var amount = decimal.Parse("0.00");
        var xmbh = txtXMID.Text;
        ZWL.BLL.ERPCostDetail costdetail = new ZWL.BLL.ERPCostDetail();
        costdetail.GetModel(Id);
        ZWL.BLL.ERPProjectCost projectcost = new ZWL.BLL.ERPProjectCost();
        if (costdetail.ParentId.HasValue)
            projectcost.GetModel(costdetail.ParentId.Value);
        var proj = new ZWL.BLL.ERPXMJBXX();
        proj = proj.GetModelByXMBH(projectcost.XMBH);
        if (proj != null)
        {
            var kv = new ZWL.BLL.ERPKeyValue();
            var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
            foreach (var item in list)
            {
                var op = this.FindControl("txt" + item.Key3);
                if (op == null) continue;
                result = ValidateCostIsRight(item.Key3, proj.ZYLB, ref msg);
                if (!result)
                {
                    break;
                }
            }
        }
        return result;
    }
    public bool ValidateCostIsRight(string cname, string zylb, ref string msg)
    {
        var result = true;
        var val = ((TextBox)this.FindControl("txt" + cname)).Text;
        var bugetModel = new ZWL.BLL.ERPBudgetDetail();
        bugetModel.GetModel(Id);
        if (bugetModel != null)
        {
            var percent = DbHelperSQL.GetSHSL(string.Format(@"select Value1 from [ERPKeyValue] where Category='BudgetPercContrl'
                            and Key3 ='{0}'--费用科目
                            and Key2 = (select Value1 from [ERPKeyValue] where Category='BudgetType'
                            and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", cname, zylb));

            if (!string.IsNullOrEmpty(percent))
            {
                var cval = PublicMethod.GetModelPropertyValueByName(bugetModel, cname);
                if (!string.IsNullOrEmpty(cval))
                {
                    var compVal = PublicMethod.GetDecimal(cval);
                    var currentVal = PublicMethod.GetDecimal(val);
                    if (currentVal > compVal)
                    {
                        msg = string.Format(@"当前输入的[{0}]的值[{1}],超出其预算[{2}],请检查后重试.", cname, currentVal, compVal, (currentVal - compVal));
                        result = false;
                    }
                }
            }
        }
        return result;
    }

    public ZWL.BLL.ERPCostDetail setcostdetail(ZWL.BLL.ERPCostDetail costdetail, string xmid, Financial_CostDetailModify thisfp)
    {
        ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
        pcmodel.GetModel(costdetail.ParentId.Value);
        DataSet ds = new DataSet();
        costdetail.ParentId = pcmodel.ID;
        costdetail.XMBH = pcmodel.XMBH;
        costdetail.HTBH = pcmodel.HTBH;
        costdetail.beiyong1 = thisfp.txt摘要.Text;
        costdetail.beiyong2 = thisfp.txt录入日期.Text;
        costdetail.工资及津贴 = inputcondition(thisfp.txt工资及津贴.Text);
        costdetail.节日补贴 = inputcondition(thisfp.txt节日补贴.Text);
        costdetail.养老统筹 = inputcondition(thisfp.txt养老统筹.Text);
        costdetail.福利费 = inputcondition(thisfp.txt福利费.Text);
        costdetail.劳动保护费 = inputcondition(thisfp.txt劳动保护费.Text);
        costdetail.住房公积金 = inputcondition(thisfp.txt住房公积金.Text);
        costdetail.住房补贴 = inputcondition(thisfp.txt住房补贴.Text);
        costdetail.材料费 = inputcondition(thisfp.txt材料费.Text);
        costdetail.工程出包费 = inputcondition(thisfp.txt工程出包费.Text);
        costdetail.钻探出包费 = inputcondition(thisfp.TextBox_zkjcf.Text);
        costdetail.钻孔进尺 = inputcondition(thisfp.TextBox_zkjc.Text);
        costdetail.固定资产 = inputcondition(thisfp.txt固定资产.Text);
        costdetail.办公费 = inputcondition(thisfp.txt办公费.Text);
        costdetail.差旅费 = inputcondition(thisfp.txt差旅费.Text);
        costdetail.水电费 = inputcondition(thisfp.txt水电费.Text);
        costdetail.物业管理费 = inputcondition(thisfp.txt物业管理费.Text);
        costdetail.交通运输费用 = inputcondition(thisfp.txt交通运输费用.Text);
        costdetail.邮电费用 = inputcondition(thisfp.txt邮电费用.Text);
        costdetail.维修费用 = inputcondition(thisfp.txt维修费用.Text);
        costdetail.会议费 = inputcondition(thisfp.txt会议费.Text);
        costdetail.培训费 = inputcondition(thisfp.txt培训费.Text);
        costdetail.业务招待费 = inputcondition(thisfp.txt业务招待费.Text);
        costdetail.劳务费 = inputcondition(thisfp.txt劳务费.Text);
        costdetail.租赁费 = inputcondition(thisfp.txt租赁费.Text);
        costdetail.税金及附加 = inputcondition(thisfp.txt税金及附加.Text);
        costdetail.安全生产费用 = inputcondition(thisfp.txt安全生产费用.Text);
        costdetail.工会经费 = inputcondition(thisfp.txt工会经费.Text);
        costdetail.印刷费 = inputcondition(thisfp.txt印刷费.Text);
        costdetail.其它费用 = inputcondition(thisfp.txt其他费用.Text);


        costdetail.工资及津贴_备注 = thisfp.txt工资及津贴_备注.Text;
        costdetail.节日补贴_备注 = thisfp.txt节日补贴_备注.Text;
        costdetail.养老统筹_备注 = thisfp.txt养老统筹_备注.Text;
        costdetail.福利费_备注 = thisfp.txt福利费_备注.Text;
        costdetail.劳动保护费_备注 = thisfp.txt劳动保护费_备注.Text;
        costdetail.住房公积金_备注 = thisfp.txt住房公积金_备注.Text;
        costdetail.住房补贴_备注 = thisfp.txt住房补贴_备注.Text;
        costdetail.材料费_备注 = thisfp.txt材料费_备注.Text;
        costdetail.工程出包费_备注 = thisfp.txt工程出包费_备注.Text;
        costdetail.固定资产_备注 = thisfp.txt固定资产_备注.Text;
        costdetail.办公费_备注 = thisfp.txt办公费_备注.Text;
        costdetail.差旅费_备注 = thisfp.txt差旅费_备注.Text;
        costdetail.水电费_备注 = thisfp.txt水电费_备注.Text;
        costdetail.物业管理费_备注 = thisfp.txt物业管理费_备注.Text;
        costdetail.交通运输费用_备注 = thisfp.txt交通运输费用_备注.Text;
        costdetail.邮电费用_备注 = thisfp.txt邮电费用_备注.Text;
        costdetail.维修费用_备注 = thisfp.txt维修费用_备注.Text;
        costdetail.会议费_备注 = thisfp.txt会议费_备注.Text;
        costdetail.培训费_备注 = thisfp.txt培训费_备注.Text;
        costdetail.业务招待费_备注 = thisfp.txt业务招待费_备注.Text;
        costdetail.劳务费_备注 = thisfp.txt劳务费_备注.Text;
        costdetail.租赁费_备注 = thisfp.txt租赁费_备注.Text;
        costdetail.税金及附加_备注 = thisfp.txt税金及附加_备注.Text;
        costdetail.安全生产费用_备注 = thisfp.txt安全生产费用_备注.Text;
        costdetail.工会经费_备注 = thisfp.txt工会经费_备注.Text;
        costdetail.印刷费_备注 = thisfp.txt印刷费_备注.Text;
        costdetail.其它费用_备注 = thisfp.txt其他费用_备注.Text;
        costdetail.期间 = thisfp.txt期间.Text;
        return costdetail;
    }
    public decimal inputcondition(string str)
    {
        decimal dec = str == "" ? 0 : decimal.Parse(str);
        return dec;
    }
    public double CaculateMoney(double money)
    {
        double dres = 0.0;
        if (txt合同金额.Text != "" && txt上季度合同登记总额.Text != "")
        {
            double current = money;
            double htje = double.Parse(txt合同金额.Text);
            double zonge = double.Parse(txt上季度合同登记总额.Text);
            dres = current * htje / zonge;
        }
        return dres;

    }
    public void CalculateCost(string cname)
    {
        if (txt合同金额.Text != "" && txt上季度合同登记总额.Text != "")
        {
            var optb = this.FindControl("txt" + cname + "0");
            if (optb != null)
            {
                var current = ((TextBox)optb).Text;
                if (!string.IsNullOrEmpty(current))
                {
                    var currentVal = double.Parse(current);
                    var htje = double.Parse(txt合同金额.Text);
                    var zonge = double.Parse(txt上季度合同登记总额.Text);
                    var dres = currentVal * htje / zonge;
                    var op = this.FindControl("txt" + cname);
                    if (op != null)
                    {
                        var xmModel = new ZWL.BLL.ERPXMJBXX();
                        xmModel = xmModel.GetModelByXMBH(txtXMID.Text);
                        ((TextBox)op).Text = String.Format("{0:###,###,##0.00}", dres);
                        var msg = string.Empty;
                        var result = ValidateCostIsRight(cname, xmModel.ZYLB, ref msg);
                        if (!result)
                            MessageBox.Show(this, msg);
                    }
                }
            }

        }
    }
    protected void btn工资及津贴_Click(object sender, EventArgs e)
    {
        CalculateCost("工资及津贴");
    }
    protected void btn节日补贴_Click(object sender, EventArgs e)
    {
        CalculateCost("节日补贴");
    }
    protected void btn养老统筹_Click(object sender, EventArgs e)
    {
        CalculateCost("养老统筹");
    }
    protected void btn福利费_Click(object sender, EventArgs e)
    {
        CalculateCost("福利费");
    }
    protected void btn劳动保护费_Click(object sender, EventArgs e)
    {
        CalculateCost("劳动保护费");
    }
    protected void btn住房公积金_Click(object sender, EventArgs e)
    {
        CalculateCost("住房公积金");
    }
    protected void btn住房补贴_Click(object sender, EventArgs e)
    {
        CalculateCost("住房补贴");
    }
    protected void btn固定资产_Click(object sender, EventArgs e)
    {
        CalculateCost("固定资产");
    }
    protected void btn租赁费_Click(object sender, EventArgs e)
    {
        CalculateCost("租赁费");
    }
    protected void Button钻孔进尺费用_Click(object sender, EventArgs e)
    {
        double dzkjc = Double.Parse(TextBox_zkjc.Text);
        double ddj = Double.Parse(TextBox_dj.Text);
        this.TextBox_zkjcf.Text = (dzkjc * ddj).ToString();//钻探出包费
    }
}