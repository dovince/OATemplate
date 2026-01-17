using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Aptitude_AptitudeFileRecord : System.Web.UI.Page
{
    private DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限
            //ImageButton2.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";
            PublicMethod.BindDepartmentDDL(ddlDepartment, "select [Department] from [AptitudeWork] group by [Department]", "全部");
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");

        var MyTable = new Hashtable();
        MyTable.Add("使用资质名称", "使用资质名称");
        MyTable.Add("证书使用时间", "开始时间");
        MyTable.Add("所用项目名称", "项目名称");
        MyTable.Add("申请人", "申请人");
        MyTable.Add("使用部门", "使用部门");
        MyTable.Add("是否归还", "是否归还");
        MyTable.Add("结束时间", "结束时间");
        var sql = @"select [使用资质名称]
      ,[开始时间]
      ,[结束时间]
      ,[项目名称]
      ,[使用部门]
      ,[申请人]
      ,[是否归还] FROM (
  SELECT E.*,T.StateNow FROM [AptitudeFile] E JOIN ERPNWorkToDo T 
  ON E.NWorkID = T.ID

 ) B ";
        if (!string.IsNullOrEmpty(IDlist))
        {
            sql += " where id in (" + IDlist + ")";
        }

        var ds = DbHelperSQL.GetDataSet(sql);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataToExcel.GridViewToExcel(ds, MyTable, "Excel报表");
        }
        else
        {
            MessageBox.Show(this, "导出数据为空！");
        }
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

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    #region  分页方法
    protected void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (GoPage.Text.Trim().ToString() == "")
            {
                ZWL.Common.MessageBox.Show(this, "页码不可以为空！");
            }
            else if (GoPage.Text.Trim().ToString() == "0" || ZWL.Common.PublicMethod.GetInt(GoPage.Text.Trim().ToString()) > ZWL.Common.PublicMethod.GetInt(LabPageSum.Text))
            {
                ZWL.Common.MessageBox.Show(this, "页码不是一个有效值！");
            }
            else if (GoPage.Text.Trim() != "")
            {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if (PageI >= 0 && PageI < (ZWL.Common.PublicMethod.GetInt(LabPageSum.Text)))
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
        var currentPage = int.Parse(LabCurrentPage.Text);
        switch (arg)
        {
            case ("Next"):
                if (currentPage < pageCount)
                    GVData.PageIndex = currentPage + 1;
                break;
            case ("Pre"):
                if (currentPage > 0)
                    GVData.PageIndex = currentPage - 1;
                break;
            case ("Last"):
                try
                {
                    GVData.PageIndex = pageCount;
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

    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        PublicMethod.GridViewRowDataBound(e);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dt = ds.Tables[0];
                var fileIdLbl = (Label)e.Row.FindControl("LabVisible");
                var fileTypeLbl = (Label)e.Row.FindControl("LabState");
                var fileStateLbl = (Label)e.Row.FindControl("lblAptState");
                var fileId = fileIdLbl.Text;
                var fileState = fileTypeLbl.Text;
                var type = 0;
                var state = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ID"].ToString() == fileId)
                    {
                        type = int.Parse(dt.Rows[i]["Type"].ToString());
                        state = int.Parse(dt.Rows[i]["State"].ToString());
                        var returnDate = dt.Rows[i]["ReturnDate"].ToString();
                        if (state == 0 && string.IsNullOrEmpty(returnDate)) state = 2;
                        break;
                    }
                }
                if (type != 0)
                {
                    switch (type)
                    {
                        case (int)ZWL.Common.AptitudeType.Original:
                            fileTypeLbl.Text = "正本原件";
                            break;
                        case (int)ZWL.Common.AptitudeType.Carbon:
                            fileTypeLbl.Text = "副本原件";
                            break;
                        case (int)ZWL.Common.AptitudeType.OriginalCopy:
                            fileTypeLbl.Text = "正本复印件";
                            break;
                        case (int)ZWL.Common.AptitudeType.CarbonCopy:
                            fileTypeLbl.Text = "副本复印件";
                            break;
                    }
                }
                if (state == 0)
                {
                    fileStateLbl.Text = "是";
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Green;
                }
                else if (state == 2)
                {
                    fileStateLbl.Text = "";
                }
                else
                {
                    fileStateLbl.Text = "否";
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    public void DataBindToGridview()
    {
        var MyModel = new ZWL.BLL.AptitudeWorkDetail();
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        ds = new DataSet();
        var currPage = GVData.PageIndex == 0 ? 1 : GVData.PageIndex;
        var pageSize = int.Parse(TxtPageSize.Text);
        Pager pager = null;
        var strwhere = "";
        strwhere = GetSqlWhere();

        pager = MyModel.GetPagingList(strwhere, currPage, pageSize);

        if (pager != null)
        {
            if (pager.ExecuteToDataSet())
            {
                ds = (DataSet)pager.Result;
            }
        }
        var pageSum = pager == null ? GVData.PageCount : pager.TotalPage;
        currPage = pager == null ? 0 : pager.CurrentPage;

        GVData.DataSource = ds;
        GVData.PageIndex = currPage;
        GVData.PageSize = pageSize;
        GVData.DataBind();
        LabPageSum.Text = pageSum.ToString();
        HdfPageSum.Value = pageSum.ToString();
        LabCurrentPage.Text = currPage.ToString();
        GoPage.Text = currPage.ToString();
    }

    private string GetSqlWhere()
    {
        var result = " 1 = 1 ";
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        var userName = PublicMethod.GetUserName();

        if (!string.IsNullOrEmpty(txtAptitudeName.Text.Trim()))
        {
            result += " and [AptitudeName] " + " like '%" + txtAptitudeName.Text + "%'";
        }

        if (!string.IsNullOrEmpty(ddlAptitudeType.SelectedValue) && ddlDepartment.SelectedValue != "全部")
        {
            if (ddlDepartment.SelectedValue == "2")
            {
                result += " and [Type] " + " in (3,4)";
            }
            else
            {
                result += " and [Type] " + "  in (1,2)";
            }
        }

        if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
        {
            result += " and [No] " + " like '%" + txtNo.Text + "%'";
        }
        if (!string.IsNullOrEmpty(txtProjectNo.Text.Trim()))
        {
            result += " and [ProjectNo] " + " like '%" + txtProjectNo.Text + "%'";
        }
        if (!string.IsNullOrEmpty(txtProjectName.Text.Trim()))
        {
            result += " and [ProjectName] " + " like '%" + txtProjectName.Text + "%'";
        }
        if (!string.IsNullOrEmpty(TextBox_qjr.Text.Trim()))
        {
            result += " and [Operator] " + " like '%" + TextBox_qjr.Text + "%'";
        }
        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue) && ddlDepartment.SelectedValue != "全部")
        {
            result += " and w.[Department] " + " like '%" + ddlDepartment.SelectedValue + "%'";
        }
        if (!string.IsNullOrEmpty(ddlReturn.SelectedValue) && ddlReturn.SelectedValue != "全部")
        {
            result += " and d.[State] " + " like '%" + ddlReturn.SelectedValue + "%'";
        }

        if (!string.IsNullOrEmpty(txb_Start.Text.Trim()))
        {
            result += " and [CreatedDate] >='" + txb_Start.Text + "'";
        }
        if (!string.IsNullOrEmpty(txb_End.Text.Trim()))
        {
            result += " and [CreatedDate] <='" + DateTime.Parse(txb_End.Text).AddDays(1) + "'";
        }
        return result;
    }
}