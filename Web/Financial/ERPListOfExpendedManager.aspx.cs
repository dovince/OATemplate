using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class Financial_ERPListOfExpendedManager : System.Web.UI.Page
{
    public string formid = "106";
    public string workFlowID = "105";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();
            //设定按钮权限
            ImageButton1.Visible = ZWL.Common.PublicMethod.StrIFIn("|X001A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton5.Visible = ZWL.Common.PublicMethod.StrIFIn("|X001M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = ZWL.Common.PublicMethod.StrIFIn("|X001D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|X001E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
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
        switch (arg)
        {
            case ("Next"):
                if (this.GVData.PageIndex < (GVData.PageCount - 1))
                    GVData.PageIndex++;
                break;
            case ("Pre"):
                if (GVData.PageIndex > 0)
                    GVData.PageIndex--;
                break;
            case ("Last"):
                try
                {
                    GVData.PageIndex = (GVData.PageCount - 1);
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
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlwork = (HyperLink)e.Row.FindControl("HyperLink1");
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
    }
    public void DataBindToGridview()
    {
        var ds = new DataSet();
        var strjiaose = PublicMethod.GetSessionValue("JiaoSe");
        var currPage = ((int)GVData.PageIndex + 1);
        var pageSize = int.Parse(TxtPageSize.Text);
        Pager pager = null;
        var sqlWhere = GetWhere();
        var table = @"select *  FROM (select *  FROM (select h.*,d.StateNow,d.ShenPiUserList,d.FormID,d.WorkFlowID,
                                d.JieDianName,d.JieDianID,u.UserName,u.Department, d.TimeStr,d.LateTime,d.OKUserList,d.BeiYong1,d.BeiYong2 from (SELECT ID
								,WorkName,Amount,Department BM,Username DJR,CreatedTime,NWorkToDoID,State  FROM ERPListOfExpended) h
                                join ERPNWorkToDo d
                                on h.NWorkToDoID=d.ID
                                join ERPUser u 
                                on d.UserName = u.UserName
                                ) t) t ";
        if (sqlWhere.Trim() != "")
        {
            table += " where " + sqlWhere;
        }
        pager = new Pager(table, currPage, pageSize);
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
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string formid = "0";
        for (int i = 0; i < GVData.Rows.Count; i++)
        {
            Label LabVis = (Label)GVData.Rows[i].FindControl("LabVisible");
            formid = formid + "," + LabVis.Text.ToString();
        }
        Hashtable MyTable = new Hashtable();
        MyTable.Add("WorkName", "工作名称");
        MyTable.Add("FormID", "所用表单");
        MyTable.Add("WorkFlowID", "所用工作流程");
        MyTable.Add("UserName", "发起人");
        MyTable.Add("TimeStr", "发起时间");
        MyTable.Add("FuJianList", "附件文件");
        MyTable.Add("JieDianID", "当前所在节点");
        MyTable.Add("JieDianName", "当前节点名称");
        MyTable.Add("ShenPiUserList", "当前审批用户");
        MyTable.Add("OKUserList", "当前已审批通过的用户");
        MyTable.Add("StateNow", "当前状态");
        MyTable.Add("LateTime", "超时时间");
        ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select  WorkName,FormID,WorkFlowID,UserName,TimeStr,FuJianList,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime  from ERPNWorkToDo where ID in (" + formid + ") order by ID desc"), MyTable, "Excel报表");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string nworkid = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPNWorkToDo where StateNow='已被驳回' and ID in (" + nworkid + ")") == -1)
        {
            ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
        }
        else
        {
            if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPListOfExpended where NWorkToDoID in (" + nworkid + ")") == -1)
            {
                ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重试！");
            }
            else
            {
                ZWL.Common.MessageBox.Show(this, "删除选中记录成功！");

                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除费用成本报销信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ERPListOfExpendedAdd.aspx?FormID=" + formid + "&WorkFlowID=" + workFlowID);
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');

        string strShenPiYiJian = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ShenPiYiJian from ERPNWorkToDo where ID=" + CheckStrArray[0].ToString());
        string strStateNow = ZWL.DBUtility.DbHelperSQL.GetSHSL("select StateNow from ERPNWorkToDo where ID=" + CheckStrArray[0].ToString());
        //驳回的项目可以修改
        if (!strStateNow.Equals("已被驳回"))
        {
            if (!string.IsNullOrEmpty(strShenPiYiJian))
            {
                ZWL.Common.MessageBox.Show(this, "该费用成本报销已经通过审核不能修改！");
                return;
            }
        }
        string strURL = "ERPListOfExpendedAdd.aspx?Nwid=" + CheckStrArray[0].ToString();
        Response.Redirect(strURL);
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

    private string GetWhere()
    {
        var strWhere = " FormID in (" + formid + ") ";
        var strdepartment = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");

        if (strdepartment.Contains("单位领导") || strdepartment.Contains("总工程师办公室") || strdepartment.Contains("经营管理科"))
        {
        }
        else if (strdepartment.Contains("部门负责人"))
        {
            string strbumen = ZWL.Common.PublicMethod.GetSessionValue("Department");
            string namelist = "'" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'";
            DataTable namedt = new DataTable();
            namedt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select UserName from ERPUser where Department='" + strbumen + "'");
            for (int i = 0; i < namedt.Rows.Count; i++)
            {
                namelist += ",'" + namedt.Rows[i]["UserName"].ToString() + "'";
            }
            strWhere += " and UserName in (" + namelist + ") ";
        }
        else
        {
            strWhere += " and UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' ";
        }
        if (WorkName.Text != "")
        {
            strWhere += string.Format(" and WorkName like '%{0}%'", WorkName.Text);
        }
        if (Amount.Text != "")
        {
            strWhere += string.Format(" and Amount like '%{0}%'", Amount.Text);
        }
        if (Department.Text != "")
        {
            strWhere += string.Format(" and Department like '%{0}%'", Department.Text);
        }
        if (Username.Text != "")
        {
            strWhere += string.Format(" and Username like '%{0}%'", Username.Text);
        }
        if (Department.Text != "")
        {
            strWhere += string.Format(" and Department like '%{0}%'", Department.Text);
        }
        if (StateNow.SelectedValue != "")
        {
            strWhere += string.Format(" and StateNow='{0}'", StateNow.SelectedValue);
        }
        if (!string.IsNullOrEmpty(TimeStr_Start.Text))
        {
            strWhere += " and TimeStr >= '" + DateTime.Parse(TimeStr_Start.Text).Date + "'";
        }
        if (!string.IsNullOrEmpty(TimeStr_End.Text))
        {
            strWhere += " and TimeStr < '" + DateTime.Parse(TimeStr_End.Text).Date.AddDays(1) + "'";
        }
        return strWhere;
    }
}