using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;
using System.Linq;

public partial class Aptitude_AptitudeList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限            
            ImageButton1.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton5.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton2.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";
            PublicMethod.BindDepartmentDDL(ddlDepartment, "select [Department] from [AptitudeWork] group by [Department]", "全部");
        }
    }
    public void DataBindToGridview()
    {
        var MyModel = new ZWL.BLL.AptitudeWork();
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        var ds = new DataSet();
        var currPage = ((int)GVData.PageIndex + 1);
        var pageSize = int.Parse(TxtPageSize.Text);
        Pager pager = null;
        string strwhere = GetSqlWhere();

        pager = MyModel.GetPagingList(strwhere, currPage, pageSize);

        if (pager != null)
        {
            if (pager.ExecuteToDataSet())
            {
                ds = (DataSet)pager.Result;
            }
        }
        GVData.DataSource = ds;
        GVData.DataBind();
        GVData.PageIndex = currPage;
        var pageSum = pager == null ? GVData.PageCount : pager.TotalPage;
        LabPageSum.Text = pageSum.ToString();
        HdfPageSum.Value = pageSum.ToString();
        LabCurrentPage.Text = currPage.ToString();
        this.GoPage.Text = currPage.ToString();
    }

    private string GetSqlWhere()
    {
        var result = " 1=1 ";
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        var userName = PublicMethod.GetUserName();
        if (strbumen == "中心领导" || strJiaoSe == Util.SuperAdmin || strbumen == "经营管理科")
        {
        }
        var wid = Get("wid");
        if (!wid.IsNullOrEmpty())
        {
            result += " and ID in ({0})".FormatWith(wid);
        }
        else if (strJiaoSe.Contains("部门负责人"))
        {
            result += " and [Department]='" + strbumen + "'";
        }
        else
        {
            result += " and [Operator]='" + userName + "'";
        }

        if (!string.IsNullOrEmpty(TextBox_qjr.Text.Trim()))
        {
            result += " and [Operator] " + " like '%" + TextBox_qjr.Text + "%'";
        }
        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue) && ddlDepartment.SelectedValue != "全部")
        {
            result += " and [Department] " + " like '%" + ddlDepartment.SelectedValue + "%'";
        }
        if (ddlReturn.SelectedValue != "全部")
        {
            result += " and [State] " + " like '%" + ddlReturn.SelectedValue + "%'";
        }

        if (!string.IsNullOrEmpty(txb_Start.Text.Trim()))
        {
            result += " and [StartDate] >='" + txb_Start.Text + "'";
        }
        if (!string.IsNullOrEmpty(txb_End.Text.Trim()))
        {
            result += " and [EndDate] <='" + DateTime.Parse(txb_End.Text).AddDays(1) + "'";
        }
        return result;
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
        var arg = ((ImageButton)sender).CommandName.ToString();
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
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        PublicMethod.GridViewRowDataBound(e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = ((DataRowView)e.Row.DataItem).Row;
            var lblAptState = ((Label)e.Row.FindControl("lblAptState"));
            var lblAptID = ((Label)e.Row.FindControl("LabVisible")).Text;
            var aptState = 0;
            if (item["ID"].ToString() == lblAptID)
            {
                aptState = int.Parse(item["State"].ToString());
                var returnDate = item["ReturnDate"].ToString();
                if (aptState == 0 && string.IsNullOrEmpty(returnDate)) aptState = 2;
            }

            if (aptState == 0)
            {
                lblAptState.Text = "是";
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Green;
            }
            else if (aptState == 2)
            {
                lblAptState.Text = "";
            }
            else
            {
                lblAptState.Text = "否";
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AptitudeAdd.aspx?FormID=86&WorkFlowID=77");
    }
    private bool DeleteAptitude(string IDlist)
    {
        var result = true;
        if (!string.IsNullOrEmpty(IDlist))
        {
            foreach (var item in IDlist.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var m = new ZWL.BLL.AptitudeWork();
                m.GetModel(int.Parse(item));
                result = DbHelperSQL.ExecuteSQL("delete [AptitudeWork] where id = " + item) > 0;
                DbHelperSQL.ExecuteSQL("delete [AptitudeWorkDetail] where [AptWorkID] = " + item);
                DbHelperSQL.ExecuteSQL("delete [ERPNWorkToDo] where id = " + m.NWorkID);
            }
        }

        return result;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        var IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        var msg = "";
        if (ValidateDelete(IDlist, ref msg))
        {
            if (DeleteAptitude(IDlist))
            {
                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除资质使用申请审批表管理信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
            else
            {
                MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
            }
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }

    private bool ValidateDelete(string IDlist, ref string msg)
    {
        var result = true;
        if (!string.IsNullOrEmpty(IDlist))
        {
            foreach (var item in IDlist.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;

                var id = 0;
                int.TryParse(item, out id);
                var m = new ZWL.BLL.AptitudeWork();
                m.GetModel(id);

                var toDo = new ZWL.BLL.ERPNWorkToDo();
                toDo.GetModel(int.Parse(m.NWorkID));

                //驳回的项目可以修改
                if (!toDo.StateNow.Equals("已被驳回"))
                {
                    var nodes = new ZWL.BLL.ERPNWorkFlowNode();
                    var ds = nodes.GetList(" workflowID=" + toDo.WorkFlowID);
                    var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPNWorkFlowNode>(ds.Tables[0]);
                    var secondNode = list.FirstOrDefault(r => r.NodeSerils == "2");
                    if (toDo.JieDianID.Value != secondNode.ID)
                    {
                        MessageBox.Show(this, "该申请单已经通过" + secondNode.NodeName + "审核,不能删除！");
                        return false;
                    }
                }
            }
        }

        return result;
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        var sqlWhere = GetSqlWhere();
        Hashtable MyTable = new Hashtable();
        MyTable.Add("ProjectName", "项目名称");
        MyTable.Add("Operator", "申请人");
        MyTable.Add("Department", "使用部门");
        MyTable.Add("CreatedDate", "申请时间");
        MyTable.Add("JieDianName", "节点名称");
        MyTable.Add("ShenPiUserList", "审批用户");
        MyTable.Add("OKUserList", "已审批用户");
        MyTable.Add("StateNow", "当前状态");
        MyTable.Add("State", "是否归还");
        MyTable.Add("StartDate", "开始时间");
        MyTable.Add("EndDate", "结束时间");
        MyTable.Add("OtherAptitude", "其他证照");
        MyTable.Add("UsingRange", "使用范围");
        MyTable.Add("Bided", "是否中标");
        MyTable.Add("ReturnDate", "归还日期");
        MyTable.Add("Comment", "备注");
        var sql = @"SELECT [ProjectNo]
      ,[ProjectName]
      ,[Operator]
      ,[Department]
      ,[CreatedDate]
      ,[State]
      ,[StartDate]
      ,[EndDate]
      ,[OtherAptitude]
      ,[UsingRange]
      ,[Bided]
      ,[BidDeadline]
      ,[ContractDate]
      ,[CompletionDate]
      ,[ReturnDate]
      ,[ContractNo]
      ,[Comment]
      ,JieDianName
      ,ShenPiUserList
      ,OKUserList
	  ,StateNow FROM (
  SELECT E.*,T.JieDianName,T.ShenPiUserList,T.OKUserList,T.StateNow FROM [Aptitude] E JOIN ERPNWorkToDo T 
  ON E.NWorkID = T.ID and T.StateNow = '正在办理'

 ) B " + " where " + sqlWhere;

        if (!string.IsNullOrEmpty(IDlist))
        {
            sql += " and id in (" + IDlist + ")";
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

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');

        var id = 0;
        int.TryParse(CheckStrArray[0], out id);
        var m = new ZWL.BLL.AptitudeWork();
        m.GetModel(id);

        var toDo = new ZWL.BLL.ERPNWorkToDo();
        toDo.GetModel(int.Parse(m.NWorkID));

        //驳回的项目可以修改
        if (!toDo.StateNow.Equals("已被驳回"))
        {
            if (!toDo.JieDianName.Equals("使用部门负责人"))
            {
                ZWL.Common.MessageBox.Show(this, "该申请单已经通过使用部门负责人审核！");
                return;
            }
        }
        Response.Redirect("AptitudeModify.aspx?ID=" + id);
    }
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        GVData.PageIndex = 0;
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
}