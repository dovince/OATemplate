using Aspose.Words.Markup;
using EditableControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_CostDetailPostItemsWorkload : BasePage
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
                if (gvRow.RowIndex < dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number  
                    var row = dt.Rows[rowID];
                    if (PublicMethod.GetInto(row["ID"]) > 0)
                    {
                        var info = new ZWL.BLL.ERPCostDetailPostItemsWorkload();
                        info.GetModel(PublicMethod.GetInto(row["ID"]));
                        if (info.ID > 0)
                        {
                            info.Delete(info.ID);
                            DelLog(info);
                        }
                    }
                    dt.Rows.Remove(dt.Rows[rowID]);
                    ResetRowID(dt);
                }
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
        if (Action == "Edit" || Action == "View")
            LoadEditInputData();
        else
            SetInitialRow();
    }
    private void LoadEditInputData()
    {
        SetInitialRow();
        var submit = Get("Submit");
        if (!submit.IsNullOrEmpty())
        {
            var dt = (DataTable)ViewState["CurrentTable"];
            dt.Rows.Clear();
            var sourcedt = DbHelperSQL.GetDataTable("select * from ERPCostDetailPostItemsWorkload where ID in ({0})".FormatWith(submit));
            foreach (DataRow item in sourcedt.Rows)
            {
                dt.Rows.Add(item.ItemArray);
            }
            ResetRowID(dt);
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
            var dt = (DataTable)ViewState["CurrentTable"];
            var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItemsWorkload>(dt);
            var result = list.Where(r => r.ItemId > 0 && r.Quantity > 0 && r.Price > 0);
            foreach (var item in result)
            {
                if (item.ID <= 0)
                {
                    var workload = new ZWL.BLL.ERPCostDetailPostItemsWorkload
                    {
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Supplier = item.Supplier,
                        Unit = item.Unit,
                        CalcPercent = item.CalcPercent,
                        Amount = item.Amount,
                    };
                    workload.ID = workload.Add();
                    item.ID = workload.ID;
                    AddLog(workload);
                }
                else
                {
                    var workload = new ZWL.BLL.ERPCostDetailPostItemsWorkload();
                    workload.GetModel(item.ID);
                    var shots = EditShot(workload);
                    item.Update();
                    EditLog(shots, item);
                }
            }
            PiLiangSet = JsonHelper.Convert2Json(new { Amount = result.Sum(r => r.Amount), Workload = result.Select(r => r.ID) });
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private string GenerateNewNumber()
    {
        return PublicMethod.ShortenMD5() + DateTime.Now.ToString("yyyyMMddHHmmss");
    }
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
                    amt += PublicMethod.GetDecimal(item["Amount"].ToString());
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
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    private void SetInitialRow()
    {
        var dt = new DataTable();
        var sql = "select top 0 * from ERPCostDetailPostItemsWorkload";
        var dataTable = DbHelperSQL.GetDataTable(sql);
        foreach (DataColumn item in dataTable.Columns)
        {
            dt.Columns.Add(new DataColumn(item.ColumnName, typeof(string)));
        }
        dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType", typeof(string)));
        var dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["CalcPercent"] = 100;
        dr["Amount"] = 0.00;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        GVData.DataSource = dt;
        GVData.DataBind();

        //After binding the gridview, we can then extract and fill the DropDownList with Data   
        var ddlItemType = (DropDownList)GVData.Rows[0].Cells[1].FindControl("ItemType");
        var ddlItemname = (DropDownList)GVData.Rows[0].Cells[1].FindControl("ItemId");
        var ddlItemunit = (EditableDropDownList)GVData.Rows[0].Cells[1].FindControl("Unit");
        FillDropDownItemType(ddlItemType);
        FillDropDownItemName(ddlItemname, ddlItemType.SelectedValue);
        var selected = DetailItems.FirstOrDefault(e => e.Key1 == ddlItemType.SelectedValue && e.Key2 == ddlItemname.SelectedItem.Text);
        if (selected != null && !selected.Value1.IsNullOrEmpty())
        {
            FillDropDownItems(ddlItemunit, selected.Value1);
        }
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
                drCurrentRow["CalcPercent"] = 100;
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
                    var grow = GVData.Rows[i];
                    var idhd = (HiddenField)grow.Cells[1].FindControl("ID");
                    var parentIdhd = (HiddenField)grow.Cells[1].FindControl("ParentId");
                    var itemtypeddl = (DropDownList)grow.Cells[1].FindControl("ItemType");
                    var itemidddl = (DropDownList)grow.Cells[2].FindControl("ItemId");
                    var unitddl = (EditableDropDownList)grow.Cells[3].FindControl("Unit");
                    var qtytxt = (TextBox)grow.Cells[4].FindControl("Quantity");
                    var pritxt = (TextBox)grow.Cells[5].FindControl("Price");
                    var perctxt = (TextBox)grow.Cells[5].FindControl("CalcPercent");
                    var amttxt = (TextBox)grow.Cells[6].FindControl("Amount");
                    var suptxt = (TextBox)grow.Cells[7].FindControl("Supplier");

                    //Fill the DropDownList with Data
                    FillDropDownItemType(itemtypeddl);
                    //FillDropDownItemName(ddlItemname, ddlItemType.SelectedValue);

                    if (i < dt.Rows.Count)
                    {
                        var item = dt.Rows[i];
                        //Assign the value from DataTable to the TextBox   
                        idhd.Value = item["ID"].ToString();
                        parentIdhd.Value = item["ParentId"].ToString();
                        //itemtypeddl.Value = item["ItemId"].ToString();
                        qtytxt.Text = item["Quantity"].ToString();
                        pritxt.Text = item["Price"].ToString();
                        perctxt.Text = item["CalcPercent"].ToString().IsNullOrEmpty() ? "100" : item["CalcPercent"].ToString();
                        amttxt.Text = PublicMethod.FormatMoney(GetDecimal(item["Amount"]));
                        suptxt.Text = item["Supplier"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        itemtypeddl.ClearSelection();
                        var selectedItem = DetailItems.FirstOrDefault(r => r.Key1 == item["ItemType"].ToString());
                        if (!item["ItemId"].ToString().IsNullOrEmpty() && PublicMethod.GetInto(item["ItemId"].ToString()) > 0)
                            selectedItem = DetailItems.FirstOrDefault(e => e.ID == PublicMethod.GetInto(item["ItemId"]));
                        if (selectedItem == null) InitBindDropdownList(itemtypeddl, itemidddl, unitddl);
                        else
                            BindDropdownList(itemtypeddl, itemidddl, unitddl, selectedItem.Key1, selectedItem.ID.ToString(), selectedItem.Value1);

                    }

                    rowIndex++;
                }
            }
        }
    }
    private void SetItemTypeChangeData(object sender)
    {
        var csender = (DropDownList)sender;
        var selectedIndex = ((GridViewRow)csender.Parent.DataItemContainer).DataItemIndex;
        if (ViewState["CurrentTable"] != null)
        {
            var dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var grow = GVData.Rows[i];
                    var idhd = (HiddenField)grow.Cells[1].FindControl("ID");
                    var parentIdhd = (HiddenField)grow.Cells[1].FindControl("ParentId");
                    var itemtypeddl = (DropDownList)grow.Cells[1].FindControl("ItemType");
                    var itemidddl = (DropDownList)grow.Cells[2].FindControl("ItemId");
                    var unitddl = (EditableDropDownList)grow.Cells[3].FindControl("Unit");
                    var qtytxt = (TextBox)grow.Cells[4].FindControl("Quantity");
                    var pritxt = (TextBox)grow.Cells[5].FindControl("Price");
                    var amttxt = (TextBox)grow.Cells[6].FindControl("Amount");
                    var suptxt = (TextBox)grow.Cells[7].FindControl("Supplier");

                    //Fill the DropDownList with Data
                    FillDropDownItemType(itemtypeddl);
                    //FillDropDownItemName(ddlItemname, ddlItemType.SelectedValue);

                    if (i < dt.Rows.Count)
                    {
                        var item = dt.Rows[i];
                        //Assign the value from DataTable to the TextBox   
                        idhd.Value = item["ID"].ToString();
                        parentIdhd.Value = item["ParentId"].ToString();
                        //itemtypeddl.Value = item["ItemId"].ToString();
                        qtytxt.Text = item["Quantity"].ToString();
                        pritxt.Text = item["Price"].ToString();
                        amttxt.Text = PublicMethod.FormatMoney(PublicMethod.GetDecimal(item["Amount"]));
                        suptxt.Text = item["Supplier"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        itemtypeddl.ClearSelection();
                        var selectedItem = DetailItems.FirstOrDefault(r => r.Key1 == item["ItemType"].ToString());
                        if (!item["ItemId"].ToString().IsNullOrEmpty() && PublicMethod.GetInto(item["ItemId"].ToString()) > 0)
                            selectedItem = DetailItems.FirstOrDefault(e => e.ID == PublicMethod.GetInto(item["ItemId"]));
                        if (selectedItem == null) InitBindDropdownList(itemtypeddl, itemidddl, unitddl);
                        else
                            BindDropdownList(itemtypeddl, itemidddl, unitddl, selectedItem.Key1, selectedItem.ID.ToString(), selectedItem.Value1);

                    }

                    //rowIndex++;
                }
            }
        }
    }
    private static void InitBindDropdownList(DropDownList ddltype, DropDownList ddlname, EditableDropDownList ddlunit)
    {
        FillDropDownItemType(ddltype);
        FillDropDownItemName(ddlname, ddltype.SelectedValue);
        var selected = DetailItems.FirstOrDefault(e => e.Key1 == ddltype.SelectedValue && e.Key2 == ddlname.SelectedItem.Text);
        var selunit = selected != null ? selected.Value1 : "";
        FillDropDownItems(ddlunit, selunit);
    }
    private static void BindDropdownList(DropDownList ddltype, DropDownList ddlname, EditableDropDownList ddlunit, string type, string name, string unit)
    {
        FillDropDownItemType(ddltype, type);
        FillDropDownItemName(ddlname, ddltype.SelectedValue, name);
        FillDropDownItems(ddlunit, unit, ddlunit.Text);
    }

    protected void Item_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewToData((DropDownList)sender);
        var msg = string.Empty;
        if (ValidateInput(ref msg))
        {
            // FillItemScale();
            var dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //Rebind the Grid with the current data to reflect changes   
            GVData.DataSource = dtCurrentTable;
            GVData.DataBind();
            SetPreviousData();
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }

    protected void ItemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewToData();
        var selectedddl = (DropDownList)sender;
        var dataItemIndex = ((GridViewRow)selectedddl.Parent.DataItemContainer).DataItemIndex;
        var ddlItemunit = (EditableDropDownList)GVData.Rows[dataItemIndex].Cells[1].FindControl("Unit");
        var selecteditem = DetailItems.FirstOrDefault(r => r.ID == PublicMethod.GetInto(selectedddl.SelectedValue));
        var selunit = selecteditem != null ? selecteditem.Value1 : "";
        ddlItemunit.Text = "";
        ddlItemunit.Items.Clear();
        FillDropDownItems(ddlItemunit, selunit);
    }

    protected void SubmitAmt_TextChanged(object sender, EventArgs e)
    {
        //btnChange_Click(sender, e);
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        GridViewToData();
        var msg = string.Empty;
        if (ValidateInput(ref msg))
        {
            // FillItemScale();
            var dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //Rebind the Grid with the current data to reflect changes   
            GVData.DataSource = dtCurrentTable;
            GVData.DataBind();
            SetPreviousData();
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var dt = (DataTable)ViewState["CurrentTable"];
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItemsWorkload>(dt);
        if (list != null && list.Any())
        {

        }
        else
        {
            msg = "请填写必要信息。";
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
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItemsWorkload>(dt);
        if (list != null && list.Any())
        {
            for (int i = 0; i < list.Count; i++)
            {
                var e = list.ElementAt(i);
                if (e.ItemId > 0 && e.Quantity > 0 && e.Price > 0)
                {
                    if (e.Unit.IsNullOrEmpty())
                    {
                        msg = "请填写第{0}行的单位。".FormatWith(i + 1);
                        return false;
                    }
                    if (e.Supplier.IsNullOrEmpty())
                    {
                        msg = "请填写第{0}行的分包单位名称。".FormatWith(i + 1);
                        return false;
                    }
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
        return costsum;
    }
    private void FillItemScale()
    {
        var dt = (DataTable)ViewState["CurrentTable"];
        var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(dt);
        if (list != null && list.Any())
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = dt.Rows[i];
                var recordid = PublicMethod.GetInto(item["RecordId"]);
                var itemname = item["Item"].ToString();
                var subamt = PublicMethod.GetDecimal(item["SubmitAmt"]);
                var setamt = PublicMethod.GetDecimal(item["SettleAmt"]);
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
                var clist = list.Where(e => e.RecordId == recordid);
                if (clist != null && clist.Any())
                {
                    ccostsum = clist.Sum(e => e.SubmitAmt);
                }
                item["TotalAmt"] = costsum + ccostsum;
                if ((scostsum + subamt) > 0 && costbudget > 0)
                    item["ItemScale"] = PublicMethod.GetDecimal((scostsum + subamt) * 100 / costbudget);
                if ((costsum + ccostsum) > 0 && setamt > 0)
                    item["CostScale"] = PublicMethod.GetDecimal((costsum + ccostsum) * 100 / setamt);
                if (costbudget > 0)
                    item["BudgetAmt"] = costbudget;
                if (scostsum > 0)
                    item["CostedAmt"] = scostsum;
            }
            ViewState["CurrentTable"] = dt;
        }
    }
    #region private method
    private void GridViewToData(DropDownList sender = null)
    {
        if (ViewState["CurrentTable"] != null)
        {
            var selectedIndex = -1;
            if (sender != null)
                selectedIndex = ((GridViewRow)sender.Parent.DataItemContainer).DataItemIndex;
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                //extract the TextBox values   
                var item = dtCurrentTable.Rows[i];
                var grow = GVData.Rows[i];
                var idhd = (HiddenField)grow.Cells[1].FindControl("ID");
                var parentIdhd = (HiddenField)grow.Cells[1].FindControl("ParentId");
                var itemtypeddl = (DropDownList)grow.Cells[1].FindControl("ItemType");
                if (selectedIndex == i && sender != null) itemtypeddl = sender;
                var itemidddl = (DropDownList)grow.Cells[2].FindControl("ItemId");
                var unitddl = (EditableDropDownList)grow.Cells[3].FindControl("Unit");
                var qtytxt = (TextBox)grow.Cells[4].FindControl("Quantity");
                var pritxt = (TextBox)grow.Cells[5].FindControl("Price");
                var perctxt = (TextBox)grow.Cells[5].FindControl("CalcPercent");
                var amttxt = (TextBox)grow.Cells[6].FindControl("Amount");
                var suptxt = (TextBox)grow.Cells[7].FindControl("Supplier");

                item["ID"] = PublicMethod.GetInto(idhd.Value);
                item["ParentId"] = PublicMethod.GetInto(parentIdhd.Value);
                item["ItemType"] = itemtypeddl.Text;
                if (selectedIndex == i && sender != null)
                {
                    var selecteditem = DetailItems.FirstOrDefault(e => e.Key1 == itemtypeddl.Text);
                    if (selecteditem != null)
                    {
                        item["ItemId"] = selecteditem.ID;
                        item["Unit"] = selecteditem.Value1.Split(',')[0];
                    }
                }
                else
                {
                    item["ItemId"] = itemidddl.SelectedValue;
                    item["Unit"] = unitddl.Text;
                }
                item["CalcPercent"] = perctxt.Text;
                item["Supplier"] = suptxt.Text;
                var quantity = GetDecimal(qtytxt.Text);
                var price = GetDecimal(pritxt.Text);
                var cperc = GetDecimal(perctxt.Text) / 100;
                if (quantity > 0)
                    item["Quantity"] = qtytxt.Text;
                if (price > 0)
                    item["Price"] = pritxt.Text;
                decimal amount = quantity > 0 && price > 0 ? quantity * price * (cperc <= 0 ? 1 : cperc) : 0;
                if (amount > 0)
                    item["Amount"] = amount;
                //extract the DropDownList Selected Items   


                //Update the DataRow with the DDL Selected Items   

            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }
    private void GridViewToDataForItemChanged()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                //extract the TextBox values   
                var item = dtCurrentTable.Rows[i];
                var grow = GVData.Rows[i];
                var idhd = (HiddenField)grow.Cells[1].FindControl("ID");
                var parentIdhd = (HiddenField)grow.Cells[1].FindControl("ParentId");
                var itemtypeddl = (DropDownList)grow.Cells[1].FindControl("ItemType");
                var itemidddl = (DropDownList)grow.Cells[2].FindControl("ItemId");
                var unitddl = (EditableDropDownList)grow.Cells[3].FindControl("Unit");
                var qtytxt = (TextBox)grow.Cells[4].FindControl("Quantity");
                var pritxt = (TextBox)grow.Cells[5].FindControl("Price");
                var amttxt = (TextBox)grow.Cells[6].FindControl("Amount");
                var suptxt = (TextBox)grow.Cells[7].FindControl("Supplier");

                item["ID"] = PublicMethod.GetInto(idhd.Value);
                item["ParentId"] = PublicMethod.GetInto(parentIdhd.Value);
                item["ItemType"] = itemtypeddl.Text;
                item["ItemId"] = itemidddl.SelectedValue;
                item["Unit"] = unitddl.Text;
                item["Quantity"] = PublicMethod.GetInto(qtytxt.Text);
                item["Price"] = PublicMethod.GetDecimal(pritxt.Text);
                item["Amount"] = PublicMethod.GetDecimal(amttxt.Text);
                item["Supplier"] = suptxt.Text;
                //extract the DropDownList Selected Items   


                //Update the DataRow with the DDL Selected Items   

            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }
    private static List<ZWL.BLL.ERPKeyValue> DetailItems
    {
        get
        {
            return Conv<ZWL.BLL.ERPKeyValue>.GetListBySQLWhere("Category='XiangMuFenBaoFeiYongBaoXiaoFenLei'");
        }
    }
    private static void FillDropDownItemType(DropDownList ddl, string selected = "")
    {
        ddl.Items.Clear();
        foreach (var item in DetailItems.GroupBy(e => e.Key1))
        {
            var sitem = new ListItem(item.Key, item.Key);
            sitem.Selected = selected.IsNullOrEmpty() ? false : selected == item.Key;
            ddl.Items.Add(sitem);
        }
    }
    private static void FillDropDownItemName(DropDownList ddl, string itemtype, string selected = "")
    {
        ddl.Items.Clear();
        foreach (var item in DetailItems.Where(e => e.Key1 == itemtype))
        {
            var sitem = new ListItem(item.Key2, item.ID.ToString());
            sitem.Selected = selected.IsNullOrEmpty() ? false : selected == item.ID.ToString();
            ddl.Items.Add(sitem);
        }
    }
    private static void FillDropDownItems(DropDownList ddl, Dictionary<string, string> list, string selected = "")
    {
        ddl.Items.Clear();
        if (list != null && list.Count > 0)
        {
            foreach (var item in list)
            {
                var sitem = new ListItem(item.Key, item.Value);
                sitem.Selected = selected.IsNullOrEmpty() ? false : selected == item.Value;
                ddl.Items.Add(sitem);
            }
        }
    }
    private static void FillDropDownItems(DropDownList ddl, string sourceWithSplit, string selected = "")
    {
        if (!sourceWithSplit.IsNullOrEmpty())
        {
            ddl.Items.Clear();
            foreach (var item in sourceWithSplit.Split(','))
            {
                var sitem = new ListItem(item, item);
                sitem.Selected = selected.IsNullOrEmpty() ? false : selected == item;
                ddl.Items.Add(sitem);
            }
            if (selected.IsNullOrEmpty())
                ddl.SelectedIndex = 0;
        }
    }
    private static decimal GetDecimal(object dec)
    {
        var result = decimal.Parse("0.0000");
        if (dec != null && !string.IsNullOrEmpty(dec.ToString()))
        {
            if (decimal.TryParse(dec.ToString().Replace(",", ""), out result))
            {
                result = decimal.Parse(string.Format("{0:N4}", result));
            }
        }
        return result;
    }
    #endregion
}

