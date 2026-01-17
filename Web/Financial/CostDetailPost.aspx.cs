using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_CostDetailPost : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            InitInput();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                //Remove the Selected Row data and reset row number  
                dt.Rows.Remove(dt.Rows[rowID]);
                ResetRowID(dt);
            }
            else
            {
                MessageBox.Show(this, "无法全部删除,至少保留一条!");
            }

            //Store the current data in ViewState for future reference  
            ViewState["CurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            GVData.DataSource = dt;
            GVData.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetPreviousData();
    }

    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["RowNumber"] = rowNumber;
                rowNumber++;
            }
        }
    }
    private void InitInput()
    {
        DJBM.Text = Department;
        DJR.Text = UserName;
        if (Action == "Edit")
            LoadEditInputData();
        else if (Action == "View")
            LoadEditInputData();
        else
        {
            SetInitialRow();
        }
        MessageBox.Show(this, "勘察院的项目请切换至院OA系统操作。");
    }
    private void LoadEditInputData()
    {
        var infoModel = new ZWL.BLL.ERPCostDetailPost();
        infoModel = infoModel.GetModel<ZWL.BLL.ERPCostDetailPost>(Id);
        if (infoModel != null)
        {
            ExcurtGetData.forData(infoModel, this);
            ExcurtGetData.formatDate();
            DJBM.Text = infoModel.DJBM;
            DJR.Text = infoModel.DJR;
            var subDetail = infoModel.SubItems;
            foreach (var item in subDetail)
            {
                var pcost = new ZWL.BLL.ERPProjectCost();
                pcost.GetModel(item.RecordId);
                item.XMName = pcost.XMName;
                item.HTBH = !pcost.HTBH.IsNullOrEmpty() ? pcost.HTBH : pcost.XMBH;
                var dsql = @"select w.* from ERPCostDetailPostItemsWorkload w join ERPCostDetailPostItems i 
                            on i.ID=w.ParentId  where i.RecordId ={0} and w.ParentId={1}".FormatWith(item.RecordId, item.ID);
                var details = Conv<ZWL.BLL.ERPCostDetailPostItemsWorkload>.GetList(dsql);
                item.Workload = String.Join(",", details.Select(e => e.ID));
            }
            var dt = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(subDetail);
            dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
            ResetRowID(dt);
            ViewState["CurrentTable"] = dt;
            GVData.DataSource = dt;
            GVData.DataBind();
            SetPreviousData();
        }
    }
    public string GetSqlWhere(string notInIds)
    {
        var sqlWhere = string.Empty;

        if (!string.IsNullOrEmpty(notInIds))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " NWorkID not in (" + notInIds + ")";
        }
        //var limitSql = GetLimitDataSqlWhere(QuanxianValue);

        //sqlWhere += PublicMethod.GetSqlAndByWhere(sqlWhere, limitSql) + limitSql;

        return sqlWhere;
    }
    /// <summary>
    /// 表单的提交部分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        GridViewToData();
        if (ValidateInputEx(ref msg))
        {
            var infoModel = new ZWL.BLL.ERPCostDetailPost();
            if (Action == "Edit")
            {
                infoModel.GetModel(Id);
                var shots = EditShot(infoModel);
                ExcurtGetData.toData<ZWL.BLL.ERPCostDetailPost>(infoModel, this);
                var dt = (DataTable)ViewState["CurrentTable"];
                var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
                var subitems = infoModel.SubItems;
                foreach (var item in subitems)
                {
                    if (!list.Any(r => r.ID == item.ID))
                    {
                        var tshots = EditShot(item);
                        item.DeleteMark = 1;
                        item.DeleteTime = Timestamp;
                        item.DeleteUser = UserName;
                        item.Update();
                        EditLog(tshots, item);
                    }
                }
                foreach (var item in list.Where(r => r.ID == 0))
                {
                    if (item.RecordId <= 0) continue;
                    item.ParentId = infoModel.ID;
                    item.ID = item.Add();
                    AddLog(item, infoModel.ID);
                    SaveItemWorkload(item);
                }
                foreach (var item in list.Where(r => r.ID > 0))
                {
                    if (item.RecordId <= 0) continue;
                    var titem = new ZWL.BLL.ERPCostDetailPostItems();
                    titem.GetModel(item.ID);
                    var tshots = EditShot(titem);
                    item.ParentId = infoModel.ID;
                    item.Update();
                    EditLog(tshots, item);
                    SaveItemWorkload(item);
                }
                infoModel.State = "暂存";
                infoModel.Update();
                EditLog(shots, infoModel);

                SaveCostDetailSum(infoModel.ID);
                WriteLog("修改成本报销({0})".FormatWith(infoModel.ID));
            }
            else
            {
                infoModel = new ZWL.BLL.ERPCostDetailPost()
                {
                    LotNo = GenerateNewNumber(),
                    DJBM = Department,
                    DJR = UserName,
                    DJTime = Timestamp,
                    Comment = Comment.Text,
                    State = "暂存"
                };
                infoModel.ID = infoModel.Add();
                AddLog(infoModel, infoModel.ID);

                var dt = (DataTable)ViewState["CurrentTable"];
                var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
                foreach (var item in list)
                {
                    if (item.RecordId <= 0) continue;
                    item.ParentId = infoModel.ID;
                    item.ID = item.Add();
                    AddLog(item, infoModel.ID);
                    SaveItemWorkload(item);
                }

                SaveCostDetailSum(infoModel.ID);
                WriteLog("添加成本报销({0})".FormatWith(infoModel.ID));

            }
            MessageBox.ShowAndRedirect(this, "操作成功！", "CostDetailPostList.aspx");
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool SaveItemWorkload(ZWL.BLL.ERPCostDetailPostItems info)
    {
        var result = false;
        if (info != null)
        {
            if (info.Item != "工程出包费")
                return false;
            if (info.Workload.IsNullOrEmpty())
                return false;
            foreach (var item in info.Workload.Split(','))
            {
                var sitem = new ZWL.BLL.ERPCostDetailPostItemsWorkload();
                sitem = sitem.GetModel<ZWL.BLL.ERPCostDetailPostItemsWorkload>(PublicMethod.GetInto(item));
                if (sitem != null)
                {
                    var shots = EditShot(sitem);
                    sitem.ParentId = info.ID;
                    sitem.Update();
                    EditLog(shots, sitem);
                }
            }
            result = true;
        }
        return result;
    }
    private bool SaveCostDetailSum(int pid)
    {
        var result = false;
        var oldsql = "select * from ERPCostDetailPostSum where ParentId={0}".FormatWith(pid);
        var oldList = Conv<ZWL.BLL.ERPCostDetailPostSum>.GetList(oldsql);
        if (oldList != null && oldList.Any())
        {
            for (int i = 0; i < oldList.Count; i++)
            {
                var item = oldList.ElementAt(i);
                item.Delete(item.ID);
            }
        }
        var recordId = 0;
        var dlist = Conv<ZWL.BLL.ERPCostDetailPostItems>.GetListBySQLWhere("ParentId={0} and DeleteMark is null".FormatWith(pid));
        if(dlist!=null&& dlist.Any())
        {
            recordId = dlist.FirstOrDefault().RecordId;
        }
        DataRow budgetRow = getbuget(recordId);
        DataTable costDetails = getCostDetail(recordId);

        if (budgetRow == null)
        {
            // Handle case where no budget is found
            return result;
        }

        // 1. 计算各项成本支出合计
        Dictionary<string, decimal> totalCosts = new Dictionary<string, decimal>();
        foreach (var field in expenseFields)
        {
            totalCosts[field] = 0m;
        }

        foreach (DataRow row in costDetails.Rows)
        {
            foreach (var field in expenseFields)
            {
                if (row[field] != DBNull.Value)
                {
                    totalCosts[field] += PublicMethod.GetDecimal(row[field]);
                }
            }
        }

        // 2. 构建最终的 DataTable 结构
        DataTable reportTable = new DataTable();
        reportTable.Columns.Add("类别", typeof(string));

        // 存储需要保留的列名（过滤掉同时为0的列）
        List<string> columnsToShow = new List<string>();

        foreach (var field in expenseFields)
        {
            decimal budgetValue = PublicMethod.GetDecimal(budgetRow[field] == DBNull.Value ? 0m : budgetRow[field]);
            decimal costValue = totalCosts[field];

            // 3.3 过滤 支出合计和预算 同时为0的无效的字段
            if (budgetValue != 0m || costValue != 0m)
            {
                reportTable.Columns.Add(field, typeof(string)); // 使用string类型以便格式化输出 (e.g. "6,869,048.70")
                columnsToShow.Add(field);
            }
        }

        // 3.2 增加“合计”列
        reportTable.Columns.Add("合计", typeof(string));

        // 3. 填充数据行
        // 行1: 支出合计
        DataRow rowTotalCost = reportTable.NewRow();
        rowTotalCost["类别"] = "支出合计";
        decimal grandTotalCost = 0m;
        foreach (var field in columnsToShow)
        {
            decimal value = totalCosts[field];
            rowTotalCost[field] = PublicMethod.GetDecimal(value); // 格式化为货币/数字格式
            grandTotalCost += value;
        }
        rowTotalCost["合计"] = PublicMethod.GetDecimal(grandTotalCost); // 合计列可能不需要小数位，根据您的示例

        // 行2: 预算
        DataRow rowBudget = reportTable.NewRow();
        rowBudget["类别"] = "预算";
        decimal grandTotalBudget = 0m;
        foreach (var field in columnsToShow)
        {
            decimal value = PublicMethod.GetDecimal(budgetRow[field] == DBNull.Value ? 0m : budgetRow[field]);
            rowBudget[field] = PublicMethod.GetDecimal(value); // 您的示例中预算没有小数位
            grandTotalBudget += value;
        }
        rowBudget["合计"] = PublicMethod.GetDecimal(grandTotalBudget);

        // 行3: 支出比例(%)
        DataRow rowPercentage = reportTable.NewRow();
        rowPercentage["类别"] = "支出比例(%)";
        foreach (var field in columnsToShow)
        {
            decimal cost = totalCosts[field];
            decimal budget = PublicMethod.GetDecimal(budgetRow[field] == DBNull.Value ? 0m : budgetRow[field]);

            if (budget > 0)
            {
                decimal percentage = (cost / budget) * 100m;
                rowPercentage[field] = PublicMethod.GetDecimal(percentage);
            }
            else
            {
                rowPercentage[field] = 0; // 避免除以零
            }
        }

        // 计算总比例
        if (grandTotalBudget > 0)
        {
            decimal grandPercentage = (grandTotalCost / grandTotalBudget) * 100m;
            rowPercentage["合计"] = PublicMethod.GetDecimal(grandPercentage);
        }
        else
        {
            rowPercentage["合计"] = 0;
        }

        reportTable.Rows.Add(rowTotalCost);
        reportTable.Rows.Add(rowBudget);
        reportTable.Rows.Add(rowPercentage);
        for (int i = 0; i < reportTable.Rows.Count; i++)
        {
            var item = reportTable.Rows[i];
            var info = DataTableHelper.CreateItem<ZWL.BLL.ERPCostDetailPostSum>(item);
            info.ParentId = pid;
            info.Sorting = i + 1;
            info.Total = PublicMethod.GetDecimal(item["合计"]);
            info.ItemName = item["类别"].ToString();
            info.ID = info.Add();
        }

        return true;
    }
    private static readonly List<string> expenseFields = new List<string> { "工资及津贴", "节日补贴", "养老统筹", "福利费", "劳动保护费", "住房公积金", "住房补贴", "材料费", "工程出包费", "固定资产", "办公费", "差旅费", "水电费", "物业管理费", "交通运输费用", "邮电费用", "维修费用", "会议费", "培训费", "业务招待费", "劳务费", "租赁费", "安全生产费用", "税金及附加", "工会经费", "印刷费", "其它费用" };
    private string GenerateNewNumber()
    {
        return PublicMethod.ShortenMD5() + DateTime.Now.ToString("yyyyMMddHHmmss");
    }
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = ((DataRowView)e.Row.DataItem).Row;
            var itemName = item["Item"].ToString();
            var workload = item["Workload"].ToString();
            if (!itemName.IsNullOrEmpty() && itemName == "工程出包费")
            {
                var btnWorkload = (LinkButton)e.Row.FindControl("btnWorkload");
                btnWorkload.Visible = true;
                btnWorkload.Attributes.Add("href", "javascript:void(0);");
                btnWorkload.Attributes.Add("onclick", "focusEx(this);");
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            var lblamt = (Label)e.Row.FindControl("lblAmount");
            var dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                decimal amt = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = dt.Rows[i];
                    if (!item["SubmitAmt"].ToString().IsNullOrEmpty())
                    {
                        amt += PublicMethod.GetDecimal(item["SubmitAmt"].ToString());
                    }
                }
                lblamt.Text = PublicMethod.FormatMoney(amt);
            }
        }
    }
    #region  分页方法
    protected override void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        base.ButtonGo_Click(sender, e);
    }
    protected override void PagerButtonClick(object sender, ImageClickEventArgs e)
    {
        base.PagerButtonClick(sender, e);
    }
    #endregion

    protected void GVData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dt = (DataTable)ViewState["CurrentTable"];
            var lb = (LinkButton)e.Row.FindControl("btnDel");
            if (lb != null)
            {
                //if (dt.Rows.Count > 1)
                //{
                //    if (e.Row.RowIndex == dt.Rows.Count - 1)
                //    {
                //        lb.Visible = false;
                //    }
                //}
                //else
                //{
                //    lb.Visible = false;
                //}
            }
        }
    }

    private void SetInitialRow()
    {
        var sql = "select top 0 * from ERPCostDetailPostItems";
        var dt = DbHelperSQL.GetDataTable(sql);
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("XMName", typeof(string)));
        dt.Columns.Add(new DataColumn("HTBH", typeof(string)));
        dt.Columns.Add(new DataColumn("Workload", typeof(string)));
        var dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Amount"] = 0.00;
        dr["SettleAmt"] = 0.00;
        dr["ReceivedAmt"] = 0.00;
        dr["BudgetAmt"] = 0.00;
        dr["CostedAmt"] = 0.00;
        dr["TotalAmt"] = 0.00;
        dr["AccuAmt"] = 0.00;
        dr["ItemScale"] = 0.00;
        dr["CostScale"] = 0.00;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        GVData.DataSource = dt;
        GVData.DataBind();

        //After binding the gridview, we can then extract and fill the DropDownList with Data   
        var ddlItem = (DropDownList)GVData.Rows[0].FindControl("Item");
        FillDropDownList(ddlItem);
    }
    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {
                GridViewToData();
                DataRow drCurrentRow = null;
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["Amount"] = 0.00;
                drCurrentRow["SettleAmt"] = 0.00;
                drCurrentRow["ReceivedAmt"] = 0.00;
                drCurrentRow["BudgetAmt"] = 0.00;
                drCurrentRow["CostedAmt"] = 0.00;
                drCurrentRow["TotalAmt"] = 0.00;
                drCurrentRow["AccuAmt"] = 0.00;
                drCurrentRow["ItemScale"] = 0.00;
                drCurrentRow["CostScale"] = 0.00;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   

                ViewState["CurrentTable"] = dtCurrentTable;
                //Rebind the Grid with the current data to reflect changes   
                GVData.DataSource = dtCurrentTable;
                GVData.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");

        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }

    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var idhd = (HiddenField)GVData.Rows[i].FindControl("ID");
                    var parentIdhd = (HiddenField)GVData.Rows[i].FindControl("ParentId");
                    var recordIdhd = (HiddenField)GVData.Rows[i].FindControl("RecordId");
                    var workloadhd = (HiddenField)GVData.Rows[i].FindControl("Workload");
                    var xmnametxt = (TextBox)GVData.Rows[i].FindControl("XMName");
                    var desctxt = (TextBox)GVData.Rows[i].FindControl("Description");
                    var htbhtxt = (TextBox)GVData.Rows[i].FindControl("HTBH");
                    var amttxt = (TextBox)GVData.Rows[i].FindControl("Amount");
                    var setamttxt = (TextBox)GVData.Rows[i].FindControl("SettleAmt");
                    var rectxt = (TextBox)GVData.Rows[i].FindControl("ReceivedAmt");
                    var budtxt = (TextBox)GVData.Rows[i].FindControl("BudgetAmt");
                    var costtxt = (TextBox)GVData.Rows[i].FindControl("CostedAmt");
                    var subtxt = (TextBox)GVData.Rows[i].FindControl("SubmitAmt");
                    var accutxt = (TextBox)GVData.Rows[i].FindControl("AccuAmt");
                    var itemscaletxt = (HiddenField)GVData.Rows[i].FindControl("ItemScale");
                    var costscaletxt = (TextBox)GVData.Rows[i].FindControl("CostScale");

                    var ddlItem = (DropDownList)GVData.Rows[i].FindControl("Item");

                    //Fill the DropDownList with Data   
                    FillDropDownList(ddlItem);

                    if (i < dt.Rows.Count)
                    {

                        //Assign the value from DataTable to the TextBox   
                        idhd.Value = dt.Rows[i]["ID"].ToString();
                        parentIdhd.Value = dt.Rows[i]["ParentId"].ToString();
                        recordIdhd.Value = dt.Rows[i]["RecordId"].ToString();
                        workloadhd.Value = dt.Rows[i]["Workload"].ToString();
                        xmnametxt.Text = dt.Rows[i]["XMName"].ToString();
                        desctxt.Text = dt.Rows[i]["Description"].ToString();
                        htbhtxt.Text = dt.Rows[i]["HTBH"].ToString();
                        amttxt.Text = dt.Rows[i]["Amount"].ToString();
                        setamttxt.Text = dt.Rows[i]["SettleAmt"].ToString();
                        rectxt.Text = dt.Rows[i]["ReceivedAmt"].ToString();
                        budtxt.Text = dt.Rows[i]["BudgetAmt"].ToString();
                        costtxt.Text = dt.Rows[i]["CostedAmt"].ToString();
                        subtxt.Text = dt.Rows[i]["SubmitAmt"].ToString();
                        itemscaletxt.Value = dt.Rows[i]["ItemScale"].ToString();
                        costscaletxt.Text = dt.Rows[i]["CostScale"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddlItem.ClearSelection();
                        var selectedItem = ddlItem.Items.FindByValue(dt.Rows[i]["Item"].ToString());
                        if (selectedItem != null)
                            selectedItem.Selected = true;

                    }

                    rowIndex++;
                }
            }
        }
    }
    private static List<string> CostDetailItems
    {
        get
        {
            var colsql = @"select name from syscolumns 
                    where id=(select max(id) from sysobjects where xtype='u' and name='ERPCostDetail')
                    and PATINDEX('%[a-z]%', LOWER(name)) <= 0 and PATINDEX('%_备注', LOWER(name)) <= 0
                    and name not in ('期间','钻探出包费','钻孔进尺')";
            return DbHelperSQL.GetSingleCulumnList<string>(colsql);
        }
    }
    private void FillDropDownList(DropDownList ddl)
    {
        foreach (var item in CostDetailItems)
        {
            var val = item;
            var text = item;
            ddl.Items.Add(new ListItem(text, val));
        }
    }

    protected void Item_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnChange_Click(sender, e);
    }

    protected void SubmitAmt_TextChanged(object sender, EventArgs e)
    {
        btnChange_Click(sender, e);
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        GridViewToData();
        if (Action != "View")
            FillItemScale();
        var msg = string.Empty;
        if (ValidateInput(ref msg))
        {
            var dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //Rebind the Grid with the current data to reflect changes   
            GVData.DataSource = dtCurrentTable;
            GVData.DataBind();
            SetPreviousData();
        }
        else
        {
            var sb = new StringBuilder();
            sb.AppendLine(MessageBox.CheckEasyUI);
            sb.AppendLine(MessageBox.ShowText);
            sb.AppendLine(string.Format("showText('{0}');", msg));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptKey", sb.ToString(), true);
        }
    }
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        if (Action == "View") return result;
        var dt = (DataTable)ViewState["CurrentTable"];
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
        if (list != null && list.Any())
        {
            var glist = list.Where(e => e.RecordId > 0 && !e.Item.IsNullOrEmpty())
                .GroupBy(e => new { e.RecordId, e.Item });
            foreach (var item in glist)
            {
                if (item.Count() > 1)
                {
                    var l = new ZWL.BLL.ERPProjectCost();
                    l.GetModel(item.Key.RecordId);
                    msg = "项目[{0}]、支出类别[{1}]重复输入，请检查或合并后再提交。".FormatWith(l.XMName, item.Key.Item);
                    return false;
                }
            }
            var notCheckBudgetList = new List<string> { "固定资产" };
            foreach (var item in list.Where(e => e.RecordId > 0 && !e.Item.IsNullOrEmpty()))
            {
                if (item.BudgetAmt <= 0 && !notCheckBudgetList.Contains(item.Item))
                {
                    msg = "项目[{0}]、支出类别[{1}]的预算未设置，请设置后再提交。".FormatWith(item.XMName, item.Item);
                    return false;
                }
                if (item.ItemScale > 100)
                {
                    msg = "项目[{0}]、支出类别[{1}]的超过预算，请检查后再提交。".FormatWith(item.XMName, item.Item);
                    return false;
                }
            }
            var checkItems = new List<string> { "工程出包费", "劳务费" };
            var blist = list.Where(e => e.RecordId > 0 && !e.Item.IsNullOrEmpty() && e.SubmitAmt > 0 && checkItems.Contains(e.Item))
                .GroupBy(e => new { e.RecordId });
            if (blist != null && blist.Any())
            {
                var kModel = new ZWL.BLL.ERPKeyValue();
                var klist = kModel.GetModelList("Category='XMQLCYSYFJZRQ'");//各部门应收应付截止日期 baseDate
                var zxzjXMList = new List<string>();
                var zxzjXM = kModel.GetModel("Category='ZXZJXMList'");//专项资金项目
                if (zxzjXM != null)
                {
                    var zxzjs = zxzjXM.Value1.Split(',');
                    foreach (var item in zxzjs)
                    {
                        if (!item.IsNullOrEmpty() && !zxzjXMList.Contains(item))
                            zxzjXMList.Add(item);
                    }
                }
                foreach (var item in blist)
                {
                    var checkedFlag = false;
                    var l = new ZWL.BLL.ERPProjectCost();
                    l.GetModel(item.Key.RecordId);
                    if (zxzjXMList.Contains(l.XMBH))//专项资金项目已全部收款，可以直接报销，无需验证是否预算
                    {
                        checkedFlag = true;
                    }
                    else
                    {
                        var xModel = new ZWL.BLL.ERPXMJBXX();
                        xModel = xModel.GetModelByXMBH(l.XMBH);
                        if (xModel != null)
                        {
                            var selectedItem = klist.FirstOrDefault(r => r.Key1 == xModel.XMBM);
                            if (selectedItem != null)
                            {
                                var tempdt = TimeParser.GetFormatDate(selectedItem.Value1);
                                if (tempdt.HasValue && xModel.DJTime > tempdt.Value)//在baseDate之后登记的项目
                                {
                                    checkedFlag = true;
                                }
                            }
                        }
                    }
                    if (!checkedFlag)
                    {
                        var checkyfsql = @"SELECT x.XMBH, NWorkID, x.XMName,YFFY,XMJF,SFLYEXM FROM ERPXMJBXX x join ERPNWorkToDo d on x.NWorkID=d.ID join XMYFKXX f on x.XMName=f.XMName
                                    where d.StateNow in ('正在办理','正常结束') and x.XMBH='{0}'
                                    UNION
                                    SELECT XMBH,0 NWorkID,XMName,YFJE YFFY,0 XMJF,'' SFLYEXM  FROM ERPXMJBXXExtend WHERE XMBH='{0}'".FormatWith(l.XMBH);
                        var checkyf = DbHelperSQL.GetDataTable(checkyfsql);
                        if (checkyf == null || checkyf.Rows.Count <= 0)
                        {
                            msg = "未设置此项目的应付费用，请到‘项目全流程’添加此项目的应收应付，或联系经营组。（工程出包费,劳务费需提交应付费用）。项目名称：{0}".FormatWith(l.XMName);
                            return false;
                        }
                        else
                        {
                            var firstitem = checkyf.Rows[0];
                            if (firstitem["SFLYEXM"].ToString() == "是")
                            {

                            }
                            else
                            {
                                var yffy = firstitem["YFFY"].ToString();
                                if (yffy.IsNullOrEmpty() || yffy == "0" || yffy == "/")
                                {
                                    msg = "未设置此项目的应付费用，请到‘项目全流程’添加此项目的应收应付，或联系经营组。（工程出包费,劳务费需提交应付费用）。项目名称：{0}".FormatWith(l.XMName);
                                    return false;
                                }
                            }
                        }
                        if (item.Any(r => r.Item == "工程出包费"))
                        {
                            var selectItem = item.FirstOrDefault(r => r.Item == "工程出包费");
                            if (selectItem != null)
                            {
                                var projectCost = new ZWL.BLL.ERPProjectCost();
                                projectCost.GetModel(selectItem.RecordId);
                                var toCheckReceivedAmt = true;
                                var checkKModel = new ZWL.BLL.ERPKeyValue();
                                checkKModel = checkKModel.GetModel("Category='ExceptToCheckReceivedAmtForCostDetailPost'");
                                if (checkKModel != null && !checkKModel.Value1.IsNullOrEmpty())
                                {
                                    var exceptXMBHlist = checkKModel.Value1.Split(',').ToList();
                                    if (exceptXMBHlist.Contains(projectCost.XMBH))
                                    {
                                        toCheckReceivedAmt = false;
                                    }
                                }
                                if (toCheckReceivedAmt)
                                {
                                    if (selectItem.ReceivedAmt <= 0)
                                    {
                                        msg = "此项目尚未收到甲方账款，无法报销工程出包费。";
                                        return false;
                                    }
                                    var settlesql = @"SELECT  ISNULL(sum(KaiPiaoJE), 0)  KaiPiaoFS  from ERPHeTongShouKuan h JOIN ERPNWorkToDo d ON
                                                        h.NWorkToDoID=d.ID
                                                        where StateNow not in ('已被驳回','不通过')
                                                        and JieDianName in ('财务科出纳','结束')
                                                        and HTBH in (
                                                        SELECT HTID from ERPHeTong h JOIN ERPNWorkToDo d ON
                                                        h.NWorkToDoID=d.ID 
                                                        where StateNow not in ('已被驳回','不通过')
                                                        and HTLB='收款' AND XMID='{0}'
                                                        )
                                                        ".FormatWith(projectCost.XMBH);
                                    var settleAmt = PublicMethod.GetDecimal(DbHelperSQL.GetSingle(settlesql));
                                    if (settleAmt <= 0)
                                    {
                                        msg = "此项目的合同未曾开票，无法报销工程出包费。(如有疑问请联系经营科)";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var clist = list.Where(x => x.RecordId > 0 && !x.Item.IsNullOrEmpty() && x.SubmitAmt > 0 && x.Item == "工程出包费");
            if (clist != null && clist.Any())//工程出包费  是否 填写工作量
            {
                foreach (var item in clist)
                {
                    if (item.Workload.IsNullOrEmpty())
                    {
                        msg = "请正确填写[工程出包费]的工作量。";
                        return false;
                    }
                }
            }
        }
        else
        {
            msg = "请输入选择需报销的项目/合同，并填写必要信息。";
            return false;
        }
        return result;
    }
    private bool ValidateInputEx(ref string msg)
    {
        var result = ValidateInput(ref msg);
        if (!result)
        {
            return false;
        }
        var dt = (DataTable)ViewState["CurrentTable"];
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
        if (list != null && list.Any())
        {
            var glist = list.GroupBy(x => x.ParentId);
            if (glist != null && glist.Count() > 1)
            {
                msg = "暂不支持多个项目合并到一张单，每张单只支持同一个项目的成本报销。";
                return false;
            }
            foreach (var item in list)
            {
                if (item.RecordId == 0) continue;
                var singleBudget = GetItemBudget(item.RecordId, item.Item);
                var singlecostsum = GetItemCostSum(item.RecordId, item.Item);
                if (singleBudget > 0 && (singlecostsum + item.SubmitAmt) > singleBudget)
                {
                    msg = "项目[{0}]、支出类型[{1}]累计支出[{2}]元，超出预算[{3}]，请重新填写。"
                        .FormatWith(item.XMName, item.Item, PublicMethod.FormatMoney(singlecostsum + item.SubmitAmt), PublicMethod.FormatMoney(singleBudget));
                    return false;
                }
                if (item.Description.IsNullOrEmpty())
                {
                    msg = "请填写项目[{0}]、支出类型[{1}]的支出明细，不能为空。"
                        .FormatWith(item.XMName, item.Item);
                    return false;
                }
            }
        }
        return result;
    }
    private decimal GetItemBudget(int recordid, string item)
    {
        decimal singleBudget = 0;
        var bsql = @"select top 1 ISNULL({0}, 0) from ERPBudgetDetail b join ERPProjectCost c 
                        on b.ParentId=c.ID where c.ID={1} ORDER BY Version DESC".FormatWith(item, recordid);
        var budget = DbHelperSQL.GetSingle(bsql);
        if (budget != null)
        {
            singleBudget = PublicMethod.GetDecimal(budget.ToString());
        }
        return singleBudget;
    }
    private decimal GetItemCostSum(int recordid, string item)
    {
        decimal singlecostsum = 0;
        var csql = @"select  ISNULL(sum({1}), 0) from ERPCostDetail d join ERPProjectCost c 
                        on d.ParentId=c.ID and c.ID={0}".FormatWith(recordid, item);
        var scostsums = DbHelperSQL.GetSingle(csql);
        if (scostsums != null)
        {
            singlecostsum = PublicMethod.GetDecimal(scostsums.ToString());
        }
        return singlecostsum;
    }
    private decimal GetProjectCostSum(int recordid)
    {
        decimal costsum = 0;
        var pModel = new ZWL.BLL.ERPProjectCost();
        pModel.GetModel(recordid);
        var dModel = new ZWL.BLL.ERPCostDetail();
        var list = dModel.GetListModelByParentId(pModel.ID);
        if (list != null && list.Any())
        {
            foreach (var item in list)
            {
                foreach (var sitem in CostDetailItems)
                {
                    if (sitem == "钻探出包费" || sitem == "钻孔进尺") continue;
                    costsum += PublicMethod.GetDecimal(PublicMethod.GetModelPropertyValueByName(item, sitem));
                }
            }
        }
        return costsum;
    }
    private void GridViewToData()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                //extract the TextBox values   

                var idhd = (HiddenField)GVData.Rows[i].FindControl("ID");
                var parentIdhd = (HiddenField)GVData.Rows[i].FindControl("ParentId");
                var recordIdhd = (HiddenField)GVData.Rows[i].FindControl("RecordId");
                var workloadhd = (HiddenField)GVData.Rows[i].FindControl("Workload");
                var xmnametxt = (TextBox)GVData.Rows[i].FindControl("XMName");
                var desctxt = (TextBox)GVData.Rows[i].FindControl("Description");
                var htbhtxt = (TextBox)GVData.Rows[i].FindControl("HTBH");
                var amttxt = (TextBox)GVData.Rows[i].FindControl("Amount");
                var setamttxt = (TextBox)GVData.Rows[i].FindControl("SettleAmt");
                var rectxt = (TextBox)GVData.Rows[i].FindControl("ReceivedAmt");
                var budtxt = (TextBox)GVData.Rows[i].FindControl("BudgetAmt");
                var costtxt = (TextBox)GVData.Rows[i].FindControl("CostedAmt");
                var subtxt = (TextBox)GVData.Rows[i].FindControl("SubmitAmt");
                var totaltxt = (TextBox)GVData.Rows[i].FindControl("TotalAmt");
                var itemscaletxt = (HiddenField)GVData.Rows[i].FindControl("ItemScale");
                var costscaletxt = (TextBox)GVData.Rows[i].FindControl("CostScale");

                dtCurrentTable.Rows[i]["ID"] = PublicMethod.GetInto(idhd.Value);
                dtCurrentTable.Rows[i]["ParentId"] = PublicMethod.GetInto(parentIdhd.Value);
                dtCurrentTable.Rows[i]["RecordId"] = PublicMethod.GetInto(recordIdhd.Value);
                dtCurrentTable.Rows[i]["Workload"] = workloadhd.Value;
                dtCurrentTable.Rows[i]["XMName"] = xmnametxt.Text;
                dtCurrentTable.Rows[i]["Description"] = desctxt.Text;
                dtCurrentTable.Rows[i]["HTBH"] = htbhtxt.Text;
                dtCurrentTable.Rows[i]["Amount"] = PublicMethod.GetDecimal(amttxt.Text);
                dtCurrentTable.Rows[i]["SettleAmt"] = PublicMethod.GetDecimal(setamttxt.Text);
                dtCurrentTable.Rows[i]["ReceivedAmt"] = PublicMethod.GetDecimal(rectxt.Text);
                dtCurrentTable.Rows[i]["BudgetAmt"] = PublicMethod.GetDecimal(budtxt.Text);
                dtCurrentTable.Rows[i]["CostedAmt"] = PublicMethod.GetDecimal(costtxt.Text);
                if (PublicMethod.GetDecimal(subtxt.Text) > 0)
                    dtCurrentTable.Rows[i]["SubmitAmt"] = PublicMethod.GetDecimal(subtxt.Text);
                else
                    dtCurrentTable.Rows[i]["SubmitAmt"] = DBNull.Value;

                dtCurrentTable.Rows[i]["TotalAmt"] = PublicMethod.GetDecimal(totaltxt.Text);
                dtCurrentTable.Rows[i]["ItemScale"] = PublicMethod.GetDecimal(itemscaletxt.Value);
                dtCurrentTable.Rows[i]["CostScale"] = PublicMethod.GetDecimal(costscaletxt.Text);
                //extract the DropDownList Selected Items   

                var ddlItem = (DropDownList)GVData.Rows[i].FindControl("Item");

                // Update the DataRow with the DDL Selected Items   
                dtCurrentTable.Rows[i]["Item"] = ddlItem.SelectedValue;
            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }
    private void FillItemScale()
    {
        var dt = (DataTable)ViewState["CurrentTable"];
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
        if (list != null && list.Any())
        {
            decimal rowLeijiAmt = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = dt.Rows[i];
                var recordid = PublicMethod.GetInto(item["RecordId"]);
                var itemname = item["Item"].ToString();
                var subamt = PublicMethod.GetDecimal(item["SubmitAmt"]);
                var amt = PublicMethod.GetDecimal(item["Amount"]);
                var setamt = PublicMethod.GetDecimal(item["SettleAmt"]);
                var recAmt = PublicMethod.GetDecimal(item["ReceivedAmt"]);
                if (recordid == 0) continue;
                item["TotalAmt"] = 0;
                item["ItemScale"] = 0;
                item["CostScale"] = 0;
                item["BudgetAmt"] = 0;
                item["CostedAmt"] = 0;
                decimal costsum = GetProjectCostSum(recordid);//项目总支出
                decimal scostsum = GetItemCostSum(recordid, itemname);//单项支出合计
                decimal ccostsum = 0;//当前输入支出合计
                decimal costbudget = GetItemBudget(recordid, itemname);//单项预算
                for (int j = 0; j < list.Count; j++)
                {
                    var element = list.ElementAt(j);
                    if (element.RecordId == recordid && j <= i)
                    {
                        ccostsum += element.SubmitAmt;
                    }
                }
                rowLeijiAmt += ccostsum;
                //var clist = list.Where(e => e.RecordId == recordid);
                //if (clist != null && clist.Any())
                //{
                //    ccostsum = clist.Sum(e => e.SubmitAmt);
                //}
                if (PublicMethod.GetDecimal(item["TotalAmt"]) != costsum)
                {
                    item["TotalAmt"] = costsum;
                }
                if ((scostsum + subamt) > 0 && costbudget > 0)
                    item["ItemScale"] = PublicMethod.GetDecimal((scostsum + subamt) * 100 / costbudget);
                if ((costsum + rowLeijiAmt) > 0 && recAmt > 0)
                {
                    item["CostScale"] = PublicMethod.GetDecimal((costsum + rowLeijiAmt) * 100 / recAmt);
                }
                if (costbudget > 0)
                    item["BudgetAmt"] = costbudget;
                if (scostsum > 0)
                    item["CostedAmt"] = scostsum;
            }
            ViewState["CurrentTable"] = dt;
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

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        GridViewToData();
        var itemIndex = ((GridViewRow)((Control)sender).DataItemContainer).DataItemIndex;
        var dt = (DataTable)ViewState["CurrentTable"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == itemIndex)
            {
                var row = dt.Rows[i];
                dt.Rows.Add(row.ItemArray);
                break;
            }
        }
        ResetRowID(dt);
        ViewState["CurrentTable"] = dt;
        GVData.DataSource = dt;
        GVData.DataBind();
        SetPreviousData();
    }

    public static DataRow getbuget(int parentId)
    {
        return getbuget(parentId, "");
    }
    public static DataRow getbuget(int parentId, string version)
    {
        var versql = version.IsNullOrEmpty() ? "" : " and Version={0} ".FormatWith(version);
        var sqlWhere = @"select top 1 *,0 税金及附加 from ERPBudgetDetail where ParentId={0} {1} order by Version desc ".FormatWith(parentId, versql);
        var dr = DbHelperSQL.GetDataRow(sqlWhere);

        return dr;
    }
    private static DataTable getCostDetail(int parentId)
    {
        var sqlWhere = "select * from ERPCostDetail where ParentId={0}".FormatWith(parentId);
        var dr = DbHelperSQL.GetDataTable(sqlWhere);

        return dr;
    }
}

