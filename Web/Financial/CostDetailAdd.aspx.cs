using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_CostDetailAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();

            string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
            string strbumen = PublicMethod.GetSessionValue("Department");
            //绑定项目编号
            var xModel = new ZWL.BLL.ERPProjectCost();
            xModel.GetModel(Id);
            ZWL.BLL.ERPXMJBXX xmjbxx = new ZWL.BLL.ERPXMJBXX();
            xmjbxx.GetModel(xModel.XMBH);

            this.txtXMBH.Text = xModel.XMBH;
            this.txtHTBH.Text = xModel.HTBH;
            hdfXMID.Value = txtXMBH.Text;
            hdfHTBH.Value = txtHTBH.Text;
            this.txtXMName.Text = xModel.XMName;

            var cModel = new ZWL.BLL.ERPCostDetail();
            var clist = cModel.GetListModelByParentId(Id);
            var qishu = clist != null && clist.Any() ? clist.Count + 1 : 0;

            this.txt期数1.Text = qishu.ToString();

            this.txt录入日期.Text = DateTime.Now.ToShortDateString();
            this.txt承接部门.Text = xmjbxx.XMBM;
            this.txt专业类别.Text = xmjbxx.ZYLB;
            this.txt合同金额.Text = String.Format("{0:###,###,##0.00}", DataHelper.GetProjectCostJieSuanAmt(Id));
            string strcjbm = this.txt承接部门.Text;
            this.txt上季度合同登记总额.Text = String.Format("{0:###,###,##0.00}", rethtsum(strcjbm));


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
            var costdetail = new ZWL.BLL.ERPCostDetail() { ParentId = Id };
            costdetail = new ZWL.BLL.ERPCostDetail();
            var gccbf = inputcondition(this.txt工程出包费.Text);
            if (gccbf > 0)
            {
                if (this.txt工程出包费_备注.Text == "")
                {
                    MessageBox.Show(this, "请输入工程出包费的用途到备注内");
                    return;
                }
                else
                {
                    this.txt摘要.Text += " " + this.txt工程出包费_备注.Text;
                }
            }
            costdetail = setcostdetail(costdetail, txtXMBH.Text, this);
            costdetail.ParentId = Id;
            costdetail.ID = costdetail.Add();
            AddLog(costdetail);
            MessageBox.ShowAndRedirect(this, "项目支出信息添加成功！", "CostDetailInfo.aspx?ID=" + Id);
        }
    }

    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var amount = decimal.Parse("0.00");
        var xmbh = txtXMBH.Text;
        var htbh = txtHTBH.Text;
        var proCost = new ZWL.BLL.ERPProjectCost();
        proCost.GetModel(Id);
        var proj = new ZWL.BLL.ERPXMJBXX();
        proj = proj.GetModelByXMBH(proCost.XMBH);
        if (proj != null)
        {
            var kv = new ZWL.BLL.ERPKeyValue();
            var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
            //获取top 1 只为获取各分类
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
        bugetModel = bugetModel.GetModel(string.Format(@"ParentId={0} 
                        and Version = (select Max(Version) from ERPBudgetDetail where ParentId={0})", Id));
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
    public ZWL.BLL.ERPCostDetail setcostdetail(ZWL.BLL.ERPCostDetail costdetail, string xmid, Financial_CostDetailAdd thisfp)
    {
        ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
        //DataSet ds = new DataSet();
        //ds = pcmodel.GetList("XMBH='" + txtXMBH.Text + "' and HTBH='" + txtHTBH.Text + "'");
        Pojo.SetField<ZWL.BLL.ERPCostDetail, Financial_CostDetailAdd>(costdetail, thisfp);
        costdetail.XMBH = txtXMBH.Text;
        //costdetail.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
        costdetail.HTBH = txtHTBH.Text;
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
        costdetail.印刷费 = inputcondition(thisfp.txt印刷费.Text);//佛山局OA ,财务管理那块预算共需要增加三个：水电费、会议费、印刷费。支出增加一个印刷费
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
        costdetail.期间 = thisfp.txt期数1.Text;
        return costdetail;
    }
    //protected void DropDownListHT_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
    //    DataSet ds = new DataSet();
    //    ds = pcmodel.GetList("XMBH='" + this.txtXMBH.Text + "'");
    //    this.txtXMName.Text = ds.Tables[0].Rows[0]["XMName"].ToString();
    //}
    public decimal inputcondition(string str)
    {
        decimal dec = str == "" ? 0 : decimal.Parse(str);
        return dec;
    }
    protected void txt_TextChanged(object sender, EventArgs e)
    {

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
                        ((TextBox)op).Text = String.Format("{0:###,###,##0.00}", dres);
                        var msg = string.Empty;
                        var xmModel = new ZWL.BLL.ERPXMJBXX();
                        xmModel = xmModel.GetModelByXMBH(txtXMBH.Text);
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


    protected void btn办公费_Click(object sender, EventArgs e)
    {
        CalculateCost("办公费");
    }
    protected void btn水电费_Click(object sender, EventArgs e)
    {
        CalculateCost("水电费");
    }
    protected void btn交通运输费用_Click(object sender, EventArgs e)
    {
        CalculateCost("交通运输费用");
    }
    protected void btn维修费用_Click(object sender, EventArgs e)
    {
        CalculateCost("维修费用");
    }
    protected void btn培训费_Click(object sender, EventArgs e)
    {
        CalculateCost("培训费");
    }
    protected void btn劳务费_Click(object sender, EventArgs e)
    {
        CalculateCost("劳务费");
    }
    protected void btn税金及附加_Click(object sender, EventArgs e)
    {
        CalculateCost("税金及附加");
    }
    protected void btn工会经费_Click(object sender, EventArgs e)
    {
        CalculateCost("工会经费");
    }
    protected void btn材料费_Click(object sender, EventArgs e)
    {
        CalculateCost("材料费");
    }
    protected void btn差旅费_Click(object sender, EventArgs e)
    {
        CalculateCost("差旅费");
    }
    protected void btn物业管理费_Click(object sender, EventArgs e)
    {
        CalculateCost("物业管理费");
    }
    protected void btn邮电费用_Click(object sender, EventArgs e)
    {
        CalculateCost("邮电费用");
    }
    protected void btn会议费_Click(object sender, EventArgs e)
    {
        CalculateCost("会议费");
    }
    protected void btn业务招待费_Click(object sender, EventArgs e)
    {
        CalculateCost("业务招待费");
    }
    protected void btn安全生产费用_Click(object sender, EventArgs e)
    {
        CalculateCost("安全生产费用");
    }
    protected void btn印刷费_Click(object sender, EventArgs e)
    {
        CalculateCost("印刷费");
    }
    protected void btn其他费用_Click(object sender, EventArgs e)
    {
        CalculateCost("其它费用");
    }
    protected void btn工程出包费_Click(object sender, EventArgs e)
    {
        CalculateCost("工程出包费");
    }
    protected void Button钻孔进尺费用_Click(object sender, EventArgs e)
    {
        double dzkjc = Double.Parse(TextBox_zkjc.Text);
        double ddj = Double.Parse(TextBox_dj.Text);
        this.TextBox_zkjcf.Text = (dzkjc * ddj).ToString();//钻探出包费
    }
}