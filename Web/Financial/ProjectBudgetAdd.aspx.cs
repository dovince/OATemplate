using System;
using System.Data;
using System.Web.UI;
using ZWL.Common;
using System.Web.UI.WebControls;
using ZWL.DBUtility;
using System.Linq;
using System.Collections.Generic;

public partial class Financial_ProjectBudgetAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            if (Request.QueryString["ID"] != null)
            {
                var infoModel = new ZWL.BLL.ERPProjectCost();
                infoModel.GetModel(Id);
                if (infoModel.ID > 0)
                {
                    var ht = new ZWL.BLL.ERPHeTong();
                    if (!string.IsNullOrEmpty(infoModel.HTBH))
                        ht.GetstrModel(infoModel.HTBH);

                    txtXMName.Text = infoModel.XMName;
                    txtXMID.Text = infoModel.XMBH;
                    txtHTID.Text = infoModel.HTBH;
                    txtZYTYPE.Text = infoModel.ZYLB;
                    txtDepartment.Text = infoModel.XMBM;
                    txtBizpat.Text = ht.JYFS;

                    var contractAmt = !string.IsNullOrEmpty(infoModel.HTBH) ?
                        infoModel.HTJE: ht.HTJE;

                    txtContractAmt.Text = PublicMethod.FormatMoney(contractAmt);

                    var version = Request.QueryString.Get("Version");
                    var buget = new ZWL.BLL.ERPBudgetDetail();
                    buget = getbuget(infoModel.ID, version);
                    if (buget != null)
                    {
                        txt材料费.Text = buget.材料费.ToString();
                        txt工程出包费.Text = buget.工程出包费.ToString();
                        txt办公费.Text = buget.办公费.ToString();
                        txt差旅费.Text = buget.差旅费.ToString();
                        txt交通运输费用.Text = buget.交通运输费用.ToString();
                        txt邮电费用.Text = buget.邮电费用.ToString();
                        txt维修费用.Text = buget.维修费用.ToString();
                        txt劳务费.Text = buget.劳务费.ToString();
                        txt租赁费.Text = buget.租赁费.ToString();
                        txt安全生产费用.Text = buget.安全生产费用.ToString();
                        txt其它费用.Text = buget.其它费用.ToString();
                        txt工资及津贴.Text = buget.工资及津贴.ToString();
                        txt水电费.Text = buget.水电费.ToString();
                        txt会议费.Text = buget.会议费.ToString();
                        txt印刷费.Text = buget.印刷费.ToString();
                        txtComment.Text = buget.Comment;

                        var kv = new ZWL.BLL.ERPKeyValue();
                        var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
                        foreach (var item in list)
                        {
                            var lbl = this.FindControl("lbl" + item.Key3);
                            var ctl = this.FindControl("ctl" + item.Key3);
                            if (lbl != null)
                            {
                                var op = ((Label)lbl);
                                if (PublicMethod.GetDecimal(txtContractAmt.Text) == 0)
                                {
                                    op.Text = "0%";
                                }
                                else
                                {
                                    var val = string.Format("{0:N}", (PublicMethod.GetDecimal(PublicMethod.GetModelPropertyValueByName(buget, item.Key3)) / PublicMethod.GetDecimal(txtContractAmt.Text)) * 100) + "%";
                                    op.Text = string.Format(op.Text, val);
                                }
                            }
                            if (ctl != null)
                            {
                                var perc = DbHelperSQL.GetSHSL(string.Format(@"select Value1 from [ERPKeyValue] where Category='BudgetPercContrl'
                                and Key3 ='{0}'--费用科目
                                and Key2 = (select Value1 from [ERPKeyValue] where Category='BudgetType'  and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", item.Key3, infoModel.ZYLB));

                                var op = ((Label)ctl);
                                if (!string.IsNullOrEmpty(perc))
                                {
                                    op.Text = string.Format(op.Text, perc);
                                }
                                else
                                {
                                    op.Visible = false;
                                }
                            }
                        }


                    }
                    else
                    {
                        //还没有预算，显示预算比例
                        var kv = new ZWL.BLL.ERPKeyValue();
                        var list = kv.GetModelList(@"Category='BudgetPercContrl' and Key2 = (
                        select top 1 Key2 from [ERPKeyValue]  where Category='BudgetPercContrl' group by Key2)");
                        foreach (var item in list)
                        {
                            var lbl = this.FindControl("lbl" + item.Key3);
                            var ctl = this.FindControl("ctl" + item.Key3);
                            if (ctl != null)
                            {
                                var perc = DbHelperSQL.GetSHSL(string.Format(@"select Value1 from [ERPKeyValue] where Category='BudgetPercContrl'
                                and Key3 ='{0}'--费用科目
                                and Key2 = (select Value1 from [ERPKeyValue] where Category='BudgetType'  and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", item.Key3, infoModel.ZYLB));

                                var op = ((Label)ctl);
                                if (!string.IsNullOrEmpty(perc))
                                {
                                    op.Text = string.Format(op.Text, perc);
                                }
                                else
                                {
                                    op.Visible = false;
                                }
                            }
                            if (lbl != null)
                            {
                                var op = ((Label)lbl);
                                op.Text = string.Format(op.Text, "0%");
                            }
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
            var MyModel = new ZWL.BLL.ERPProjectCost();
            MyModel.GetModel(Id);
            var xmbh = MyModel.XMBH;
            var htbh = MyModel.HTBH;
            //var proj = new ZWL.BLL.ERPXMJBXX();
            //proj = proj.GetModelBySqlWhere("XMBH='" + xmbh + "'");
            //if (proj != null)
            //{
            //    htbh = proj.HTBH;
            //}
            var bdModel = new ZWL.BLL.ERPBudgetDetail();
            var bdList = bdModel.GetListModelByParentId(MyModel.ID);
            var version = bdList != null && bdList.Any() ? bdList.Count() : 0;
            var buget = new ZWL.BLL.ERPBudgetDetail
            {
                ParentId = MyModel.ID,
                Version = version + 1,
                XMBH = xmbh,
                HTBH = htbh.IsNullOrEmpty() ? "" : htbh,
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
                水电费 = PublicMethod.GetDecimal(txt水电费.Text),
                会议费 = PublicMethod.GetDecimal(txt会议费.Text),
                印刷费 = PublicMethod.GetDecimal(txt印刷费.Text),
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
    /// <summary>    
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var htbh = string.Empty;
        var pcmodel = new ZWL.BLL.ERPProjectCost();
        pcmodel.GetModel(Id);
        var amount = pcmodel.HTJE;
        if (pcmodel != null)
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
                            and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", item.Key3, pcmodel.ZYLB));

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
            var checkList = new List<string>() { "罗鑫", "admin" };
            if (!checkList.Contains(PublicMethod.GetUserName()))
            {
                var budget = new ZWL.BLL.ERPBudgetDetail();
                var blist = budget.GetListModelByParentId(Id);
                if (blist != null && blist.Count > 0)
                {
                    if (blist.Count >= 3)
                    {
                        msg = "最大调整次数为3，当前项目剩余调整次数为0.";
                        return false;
                    }
                }
            }
        }
        return result;
    }
    //public static ZWL.BLL.ERPBudgetDetail getbuget(string xmid, string htbh)
    //{
    //    return getbuget(xmid, "");
    //}
    public static ZWL.BLL.ERPBudgetDetail getbuget(int proId, string version)
    {
        var bugetModel = new ZWL.BLL.ERPBudgetDetail();
        var ds = new DataSet();
        var sqlWhere = "ParentId=" + proId;
        if (!string.IsNullOrEmpty(version))
        {
            sqlWhere += " and Version=" + version;
        }
        else
        {
            var list = bugetModel.GetListModel(sqlWhere);
            if (list != null && list.Any())
            {
                version = list.Max(r => r.Version).ToString();
                sqlWhere += " and Version=" + version;
            }
        }
        ds = bugetModel.GetList(sqlWhere);
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
    //public ZWL.BLL.ERPBudgetDetail setbuget(ZWL.BLL.ERPBudgetDetail buget, string xmid, Financial_ProjectBudgetAdd thisfp)
    //{
    //    var pcmodel = new ZWL.BLL.ERPProjectCost();
    //    pcmodel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
    //    buget.XMBH = ((System.Web.UI.WebControls.TextBox)thisfp.FindControl("txtXMID")).Text;
    //    buget.HTBH = pcmodel.HTBH;
    //    buget.工资及津贴 = inputcondition(thisfp.txt工资及津贴.Text);
    //    buget.工程出包费 = inputcondition(thisfp.txt工程出包费.Text);
    //    buget.材料费 = inputcondition(thisfp.txt材料费.Text);
    //    buget.办公费 = inputcondition(thisfp.txt办公费.Text);
    //    buget.差旅费 = inputcondition(thisfp.txt差旅费.Text);
    //    buget.交通运输费用 = inputcondition(thisfp.txt交通运输费用.Text);
    //    buget.维修费用 = inputcondition(thisfp.txt维修费用.Text);
    //    buget.劳务费 = inputcondition(thisfp.txt劳务费.Text);
    //    buget.租赁费 = inputcondition(thisfp.txt租赁费.Text);
    //    buget.安全生产费用 = inputcondition(thisfp.txt安全生产费用.Text);
    //    buget.邮电费用 = inputcondition(thisfp.txt邮电费用.Text);
    //    buget.其它费用 = inputcondition(thisfp.txt其它费用.Text);
    //    return buget;
    //}
}