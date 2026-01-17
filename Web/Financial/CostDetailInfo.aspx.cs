using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZWL.Common;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Financial_CostDetailInfo : BasePage
{
    public bool IsDataNull = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ZWL.Common.PublicMethod.CheckSession();
            ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
            pcmodel.GetModel(Id);
            this.TextBox_xmbh.Text = pcmodel.XMBH;
            this.TextBox_htbh.Text = pcmodel.HTBH;
            Label_htje.Text = pcmodel.HTJE.ToString("###,###,##0.00");
            //判断该项目是否通过开工安全流程的审核
            string strStateNow = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select StateNow from ERPNWorkToDo where BeiYong1 like'%" + pcmodel.XMBH + "%' and FormID =62");
            if (strStateNow == "正常结束")
            {
                Label_KGAQ.Text = "已经通过开工安全审核";
            }
            else
            {
                Label_KGAQ.Text = "未通过开工安全审核";
            }
            string strzkjcsql = "SELECT sum([ZKJC]) as 钻孔进尺总数  FROM [ERPXMChengGuo] where XMBH = '" + pcmodel.XMBH + "'";
            double dzkjc = 0.0;
            string strzkjc = ZWL.DBUtility.DbHelperSQL.GetSHSLInt(strzkjcsql);
            if (strzkjc != "")
            {
                dzkjc = double.Parse(strzkjc);
                this.Label_zkjc.Text = dzkjc.ToString();
            }
            this.TextBox_xmname.Text = pcmodel.XMName;
            if (Get("report") == "xls")
            {
                ImageButton2_Click(null, null);
            }
            else
            {
                DataBindToGridview();
            }
            //设定按钮权限            
            ImageButton4.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost001A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton5.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost001M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost001D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost001E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";
        }
    }

    protected override void DataBindToGridview()
    {
        var pager = new Pager("", (int)GVData.PageIndex + 1, PublicMethod.GetInt(TxtPageSize.Text));
        var result = GetDataList(pager);
        if (!IsDataNull)
        {
            var ds = (DataSet)result.Result;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVData.DataSource = ds;
                ViewState["dataset"] = ds;
                GVData.DataBind();

                datalist.DataSource = ds.Tables[0];
                datalist.DataBind();
            }
        }

        var pageSum = result.TotalPage == 0 ? GVData.PageCount : result.TotalPage;
        LabPageSum.Text = pageSum.ToString();
        LabCurrentPage.Text = result.CurrentPage.ToString();
        this.GoPage.Text = result.CurrentPage.ToString();
        GVData.PageIndex = result.CurrentPage;
        LabPageSum.Text = pageSum.ToString();
        HdfPageSum.Value = pageSum.ToString();
    }
    private Pager GetDataList(Pager pr)
    {
        var MyModel = new ZWL.BLL.ERPProjectCost();
        MyModel.GetModel(Id);
        var ds = new DataSet();
        var costdetil = new ZWL.BLL.ERPCostDetail();
        var currentPage = pr.CurrentPage;
        var pageSize = pr.PageSize;

        var strwhere = " ParentId=" + MyModel.ID;
        var pager = costdetil.GetListAndPaging(strwhere, currentPage, pageSize);
        if (pager.ExecuteToDataSet())
        {
            ds = (DataSet)pager.Result;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var buget = new ZWL.BLL.ERPBudgetDetail();
                var xmbh = MyModel.XMBH;
                var htbh = MyModel.HTBH;
                buget = getbuget(MyModel.ID);//获取该项目的最后一次预算
                var proj = new ZWL.BLL.ERPXMJBXX();
                var amoney = MyModel.JSJE;//项目经费或合同金额。新标准：首先检查 结算金额，无则以合同金额，其次是项目金额
                if (!xmbh.Contains("C"))
                {
                    //从ERPHTJIESUAN取出的结算金额
                    var jssum = ZWL.DBUtility.DbHelperSQL.GetDataSet("SELECT top 1 JSJE js,beiyong1 from ERPHTJieSuan where beiyong1='{0}' and HTBH='{1}' ORDER BY ID desc ".FormatWith(xmbh,htbh));

                    if (jssum != null && jssum.Tables.Count > 0 && jssum.Tables[0].Rows.Count > 0)
                    {
                        amoney = PublicMethod.GetDecimal(jssum.Tables[0].Rows[0]["js"].ToString());
                    }
                }
                if (amoney <= 0)
                {
                    amoney = MyModel.HTJE;
                    if (amoney <= 0)
                    {
                        amoney = MyModel.XMJF;
                    }
                }
                //如果有支出数据
                DataRow costsumrow = ds.Tables[0].NewRow();//支出合计
                DataRow budgetrow = ds.Tables[0].NewRow();//预算
                DataRow costpercentrow = ds.Tables[0].NewRow();//支出比例
                costsumrow["beiyong1"] = "支出合计";
                budgetrow["beiyong1"] = "预算";
                costpercentrow["beiyong1"] = "支出比例(%)";


                decimal dcount = 0.00M;
                decimal bcount = 0.00M;
                for (int i = 3; i < ds.Tables[0].Columns.Count; i++)
                {
                    string tempcolname = ds.Tables[0].Columns[i].ColumnName;

                    decimal singlebuget = 0.0M;

                    if (ds.Tables[0].Columns[i].DataType.FullName == "System.DateTime" || ds.Tables[0].Columns[i].ColumnName == "beiyong1" || ds.Tables[0].Columns[i].ColumnName == "sum" || ds.Tables[0].Columns[i].ColumnName == "row" || ds.Tables[0].Columns[i].ColumnName == "期间" || ds.Tables[0].Columns[i].ColumnName == "ParentId")
                    {
                        continue;
                    }
                    if (buget != null)
                    {
                        singlebuget = PublicMethod.GetDecimal(PublicMethod.GetModelPropertyValueByName(buget, tempcolname));//该项的预算金额
                        budgetrow[i] = singlebuget;
                        bcount += singlebuget;
                        string singlesum = "0";
                        if (tempcolname != "钻探出包费" && tempcolname != "钻孔进尺")
                        {
                            singlesum = GetFieldCount(ds, tempcolname);//单项累计支出
                        }
                        decimal dtarget = 0;
                        if (PublicMethod.GetDecimal(singlesum) > 0)
                        {
                            dcount += PublicMethod.GetDecimal(singlesum);
                            costsumrow[i] = singlesum;
                            if (singlebuget > 0)//预算不为0
                            {
                                dtarget = PublicMethod.GetDecimal(singlesum) / singlebuget;//单项支出累计除单项预算，该项支出比例
                                costpercentrow[i] = Math.Round(dtarget * 100, 2);
                            }

                        }
                    }
                    else
                    {
                        //预算为空，计算支出总额
                        string singlesum = GetFieldCount(ds, tempcolname);//单项累计支出

                        if (PublicMethod.GetDecimal(singlesum) > 0)
                        {
                            if (tempcolname != "钻探出包费" && tempcolname != "钻孔进尺")
                            {
                                dcount += PublicMethod.GetDecimal(singlesum);

                            }
                            costsumrow[i] = singlesum;
                        }
                    }

                }
                costsumrow["sum"] = dcount;
                budgetrow["sum"] = bcount;
                ds.Tables[0].Rows.Add(costsumrow);
                if (buget != null)
                {
                    ds.Tables[0].Rows.Add(budgetrow);
                }
                if (amoney > 0)
                {
                    decimal dtarget = dcount / amoney;//单项支出累计除单项预算，该项支出比例
                    costpercentrow["sum"] = Math.Round(dtarget * 100, 2);
                }
                ds.Tables[0].Rows.Add(costpercentrow);

                pager.Result = ds;
            }
            else
            {
                IsDataNull = true;
            }
        }

        return pager;
    }
    #region  分页方法
    protected void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        var pageCount = int.Parse(HdfPageSum.Value);
        try
        {
            if (GoPage.Text.Trim().ToString() == "")
            {
                ZWL.Common.MessageBox.Show(this, "页码不可以为空！");
            }
            else if (GoPage.Text.Trim().ToString() == "0" || Convert.ToInt32(GoPage.Text.Trim().ToString()) > pageCount)
            {
                ZWL.Common.MessageBox.Show(this, "页码不是一个有效值！");
            }
            else if (GoPage.Text.Trim() != "")
            {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if (PageI >= 0 && PageI < pageCount)
                {
                    GVData.PageIndex = PageI;
                }
            }

            if (TxtPageSize.Text.Trim().ToString() == "")
            {
                ZWL.Common.MessageBox.Show(this, "每页显示行数不可以为空！");
            }
            else if (TxtPageSize.Text.Trim().ToString() == "0")
            {
                ZWL.Common.MessageBox.Show(this, "每页显示行数不是一个有效值！");
            }
            else if (TxtPageSize.Text.Trim() != "")
            {
                try
                {
                    int MyPageSize = int.Parse(TxtPageSize.Text.ToString().Trim());
                    this.GVData.PageSize = MyPageSize;
                }
                catch
                {
                    ZWL.Common.MessageBox.Show(this, "每页显示行数不是一个有效值！");
                }
            }

            DataBindToGridview();
        }
        catch
        {
            DataBindToGridview();
            ZWL.Common.MessageBox.Show(this, "请输入有效数字！");
        }
    }
    protected void PagerButtonClick(object sender, ImageClickEventArgs e)
    {
        //获得Button的参数值
        string arg = ((ImageButton)sender).CommandName.ToString();
        var pageCount = int.Parse(HdfPageSum.Value);
        var currentPage = int.Parse(LabCurrentPage.Text) - 1;
        switch (arg)
        {
            case ("Next"):
                if (currentPage < (pageCount - 1))
                    GVData.PageIndex = currentPage + 1;
                break;
            case ("Pre"):
                if (currentPage > 0)
                    GVData.PageIndex = currentPage - 1;
                break;
            case ("Last"):
                try
                {
                    GVData.PageIndex = (pageCount - 1);
                }
                catch
                {
                    GVData.PageIndex = 0;
                }

                break;
            default:
                //本页值
                GVData.PageIndex = 0;
                break;
        }
        DataBindToGridview();
    }
    #endregion
    public static ZWL.BLL.ERPBudgetDetail getbuget(int parentId)
    {
        return getbuget(parentId, "");
    }
    public static ZWL.BLL.ERPBudgetDetail getbuget(int parentId, string version)
    {
        var bugetModel = new ZWL.BLL.ERPBudgetDetail();
        var ds = new DataSet();
        var sqlWhere = "ParentId=" + parentId;
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
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
        string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
        //增加合计信息
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataSet currentds = (System.Data.DataSet)ViewState["dataset"];
            if (type == "8475")
            {
                e.Row.Cells[1].Text = currentds.Tables[0].Rows[e.Row.RowIndex]["期间"].ToString();
            }
            e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;
            if (e.Row.Cells[3].Text == "支出比例(%)" || e.Row.Cells[3].Text == "预算" || e.Row.Cells[3].Text == "支出合计")
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[3].Font.Bold = true;
                for (int i = 4; i < 29; i++)
                {
                    e.Row.Cells[i].Font.Bold = true;
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
            }
            if (e.Row.Cells[3].Text == "支出比例(%)")//tooltip显示控制比例
            {
                var MyModel = new ZWL.BLL.ERPProjectCost();
                MyModel.GetModel(Id);
                var proj = new ZWL.BLL.ERPXMJBXX();
                proj = proj.GetModelByXMBH(MyModel.XMBH);
                if (proj != null && MyModel != null)
                {
                    for (int i = 3; i < 29; i++)
                    {
                        string tempcolname = currentds.Tables[0].Columns[i].ColumnName;
                        //该项的控制比例
                        var decimalcontrol = decimal.Parse("0.00");
                        var percontrol = ZWL.DBUtility.DbHelperSQL.GetSHSL(string.Format(@"select Value1 from [ERPKeyValue] where Category='BudgetPercContrl'
                        and Key3 ='{0}'--费用科目
                        and Key2 = (select Value1 from [ERPKeyValue] where Category='BudgetType'
                        and Key2 = (select Key2 from [ERPKeyValue] where Category='BudgetGroup' and Value1 ='{1}'))", tempcolname, MyModel.ZYLB));
                        if (!string.IsNullOrEmpty(percontrol))
                        {
                            if (percontrol.Contains("%"))
                            {
                                decimalcontrol = PublicMethod.GetDecimal(percontrol.TrimEnd('%')) / 100;
                                e.Row.Cells[i + 2].ToolTip = "控制比例[" + percontrol + "]";
                            }
                        }
                        else
                        {
                            percontrol = "0.00";
                        }

                    }
                }
            }
        }
    }
    public string GetFieldCount(System.Data.DataSet ds, string strfieldname)
    {

        decimal dcount = 0.00M;
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal dvalue = 0.00M;
                decimal.TryParse(ds.Tables[0].Rows[i][strfieldname].ToString(), out dvalue);
                dcount += dvalue;
            }
        }
        return String.Format("{0:###,###,##0.00}", Double.Parse(dcount.ToString()));
    }
    /// <summary>
    /// 添加项目财务管理记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CostDetailAdd.aspx?ID=" + PublicMethod.EncryptParam(Id));
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        //string IDlist = ZWL.Common.PublicMethod.CheckCbxL(this.GVData, "CheckSelect", "LabVisible");
        string IDlist = CheckCbx(this.datalist, "CheckSelect", "CostDetailID");
        var detailModel = new ZWL.BLL.ERPCostDetail();
        var dlist = detailModel.GetListModel("ID in (" + IDlist + ")");
        //var dmodel = ZWL.Common.Pojo.GetModelList<ZWL.BLL.ERPCostDetail>("select * from ERPCostDetail where id in (" + IDlist + ")");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPCostDetail where ID in (" + IDlist + ")") == -1)
        {
            ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
        }
        else
        {
            foreach (var item in dlist)
            {
                DelLog(item);
            }
            //decimal sum=0.00M;
            //foreach(var d in dmodel){
            //    sum+=d.工资及津贴;
            //}
            //ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
            //pcmodel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            //pcmodel.CostMoneySUM = pcmodel.CostMoneySUM - sum;
            //pcmodel.Update();
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除请审批工作管理信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        var pager = new Pager("", 1, 300);
        var result = GetDataList(pager);
        var ds = (DataSet)result.Result;
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Dictionary<string, string> MyTable = new Dictionary<string, string>();
            MyTable.Add("row", "序号");
            MyTable.Add("XMName", "项目名称");
            MyTable.Add("XMBH", "项目编号");
            MyTable.Add("HTBH", "合同编号");

            MyTable.Add("期间", "支出期数");
            MyTable.Add("beiyong2", "录入日期");
            MyTable.Add("beiyong1", "摘要");
            MyTable.Add("sum", "合计");

            MyTable.Add("工资及津贴", "工资及津贴");
            MyTable.Add("节日补贴", "节日补贴");
            MyTable.Add("养老统筹", "养老统筹");
            MyTable.Add("福利费", "福利费");
            MyTable.Add("劳动保护费", "劳动保护费");
            MyTable.Add("住房公积金", "住房公积金");
            MyTable.Add("住房补贴", "住房补贴");
            MyTable.Add("材料费", "材料费");
            MyTable.Add("工程出包费", "工程出包费");
            MyTable.Add("固定资产", "固定资产");
            MyTable.Add("办公费", "办公费");
            MyTable.Add("差旅费", "差旅费");
            MyTable.Add("水电费", "水电费");
            MyTable.Add("物业管理费", "物业管理费");
            MyTable.Add("交通运输费用", "交通运输费用");
            MyTable.Add("邮电费用", "邮电费用");
            MyTable.Add("维修费用", "维修费用");
            MyTable.Add("会议费", "会议费");
            MyTable.Add("培训费", "培训费");
            MyTable.Add("业务招待费", "业务招待费");
            MyTable.Add("劳务费", "劳务费");
            MyTable.Add("租赁费", "租赁费");
            MyTable.Add("税金及附加", "税金及附加");
            MyTable.Add("安全生产费用", "安全生产费用");
            MyTable.Add("工会经费", "工会经费");
            MyTable.Add("其它费用", "其它费用");


            var MyModel = new ZWL.BLL.ERPProjectCost();
            MyModel.GetModel(Id);

            //MyTable.Add("JBR", "经办人");
            //MyTable.Add("FKDate", "付款日期");
            //MyTable.Add("BZ", "备注");
            //MyTable.Add("FKState", "支付状态");
            ZWL.Common.DataToExcel.GridViewToExcelOrderByNameList(ds, MyTable, this.MakeValidFileName1(MyModel.XMBH + "_" + MyModel.XMName.Replace("\\", "")));
        }
        else
        {
            Response.Write("<script>alert('导出数据为空！');</script>");
        }

    }
    //修改
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = CheckCbx(this.datalist, "CheckSelect", "LabVisible");

        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("CostDetailModify.aspx?ID=" + CheckStrArray[0].ToString());
    }

    /// <summary>
    /// 根据界面上控件中的值组合查询语句
    /// </summary>
    /// <returns></returns>

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    /// <summary>
    /// 刷新页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    /// <summary>
    /// 该项目成本预算的添加、修改和删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnYS_Click(object sender, EventArgs e)
    {
        string CheckStr = CheckCbx(this.datalist, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("ProjectBudget.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void ImageButton_goback_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }


    //判断GridView里面被选中的ID
    public static string CheckCbx(Repeater GVData, string CheckBoxName, string LabID)
    {
        string str = "";
        for (int i = 0; i < GVData.Items.Count; i++)
        {
            RepeaterItem row = GVData.Items[i];
            CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
            Label LabVis = (Label)row.FindControl(LabID);
            if (Chk.Checked == true)
            {
                if (str == "")
                {
                    str = LabVis.Text.ToString();
                }
                else
                {
                    str = str + "," + LabVis.Text.ToString();
                }
            }
        }
        return str;
    }
    private string MakeValidFileName1(string hexData)
    {
        // 修改后的正则表达式，将 '-' 放在了范围的开始或结束，并对 '-' 进行了转义
        return Regex.Replace(hexData, "[\\[\\]^\\-*_×――(^)|'$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”\\t]", "_");
    }
}