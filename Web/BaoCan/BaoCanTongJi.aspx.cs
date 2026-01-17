using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using ZWL.Common;

public partial class HR_BaoCanTongJi : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            DropDownListYear.SelectedValue = DateTime.Now.Year.ToString();
            DropDownListMonth.SelectedValue = DateTime.Now.Month.ToString();
            //TimeStr_Start.Text = DateTime.Now.ToString("yyyy-MM-01");
            //TimeStr_End.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            DataBindToGridview();
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
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        PublicMethod.GridViewRowDataBound(e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HyperLink hlwork = (HyperLink)e.Row.FindControl("HyperLink1");
        }
    }
    protected void BtnSerch_Click(object sender, ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
        DataBindToGridview();
    }
    protected override void DataBindToGridview()
    {
        var sqlWhere = GetSqlWhere();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        sqlWhere = PublicMethod.GetSqlKeywordAnd(sqlWhere) + sqlWhere;

        var timelimit = "";
        timelimit += PublicMethod.GetSqlKeywordAnd(timelimit) + " (RecordDate >= '" + DropDownListYear.SelectedItem.Text + "-" + DropDownListMonth.SelectedItem.Text + "-01" + "') ";
        timelimit += PublicMethod.GetSqlKeywordAnd(timelimit) + " (RecordDate < '" + DateTime.Parse(DropDownListYear.SelectedItem.Text + "-" + DropDownListMonth.SelectedItem.Text + "-01").AddMonths(1) + "')";
        var bcrqlimit = timelimit.Replace("RecordDate", "BCRQ");

        var sql = @"select d.*,e.*,ERPUser.Department from (
                    select *,
                    (select count(0) c from FanTangJiuCanRecord d join [Flow] f on f.LotID=d.LotID where {0} and d.Name=t.Name and CanShi='早餐' and [KaoQinRecord] != '-' and Operation=1 ) ZaoCan,
                    (select count(0) c from FanTangJiuCanRecord d join [Flow] f on f.LotID=d.LotID where {0} and d.Name=t.Name and CanShi='午餐' and [KaoQinRecord] != '-' and Operation=1 ) WuCan
                    from (
                    select Number,Dept,Name from FanTangJiuCanRecord where {0} GROUP BY Number,Dept,Name
                    ) t 
                    ) d 
                    full join (
                   select *,
                   (select count(0) c from ERPBaoCan d where  {2} and d.UserName=t.UserName and ShiJianDian='早餐' and IsCancel='否' ) ZaoCan1,
                   (select count(0) c from ERPBaoCan d where  {2} and d.UserName=t.UserName and ShiJianDian='午餐' and IsCancel='否' ) WuCan1
                   from (
                   select BuMen,UserName from ERPBaoCan where  {2} GROUP BY BuMen,UserName
                   ) t 
                   ) e
                   on d.Name = e.UserName
                   join ERPUser on(ERPUser.[UserName] = e.[UserName] or ERPUser.[UserName] = d.[Name])
                   where 1=1 {1} ".FormatWith(timelimit, sqlWhere, bcrqlimit);
        //var sql = SQLFormat.FormatWith(Get("LotID"), sqlWhere);
        //throw new Exception(sql);
        var pager = new Pager(sql, currPage, pageSize, "Number");
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
        Labelrowcount.Text = Convert.ToString(pager == null ? ds.Tables[0].Rows.Count : pager.Rows);
        var pageSum = pager == null ? GVData.PageCount : pager.TotalPage;
        LabPageSum.Text = pageSum.ToString();
        HdfPageSum.Value = pageSum.ToString();
        LabCurrentPage.Text = currPage.ToString();
        GoPage.Text = currPage.ToString();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //string formid = "0";
        //for (int i = 0; i < GVData.Rows.Count; i++)
        //{
        //    Label LabVis = (Label)GVData.Rows[i].FindControl("LabVisible");
        //    formid = formid + "," + LabVis.Text.ToString();
        //}
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("WorkName", "工作名称");
        //MyTable.Add("FormID", "所用表单");
        //MyTable.Add("WorkFlowID", "所用工作流程");
        //MyTable.Add("UserName", "发起人");
        //MyTable.Add("TimeStr", "发起时间");
        //MyTable.Add("FuJianList", "附件文件");
        //MyTable.Add("JieDianID", "当前所在节点");
        //MyTable.Add("JieDianName", "当前节点名称");
        //MyTable.Add("ShenPiUserList", "当前审批用户");
        //MyTable.Add("OKUserList", "当前已审批通过的用户");
        //MyTable.Add("StateNow", "当前状态");
        //MyTable.Add("LateTime", "超时时间");
        //DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select  WorkName,FormID,WorkFlowID,UserName,TimeStr,FuJianList,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime  from ERPNWorkToDo where ID in (" + formid + ") order by ID desc"), MyTable, "Excel报表");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string nworkid = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPNWorkToDo where StateNow='已被驳回' and ID in (" + nworkid + ")") == -1)
        {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else
        {
            if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPDZZLCGJieYue where NWorkToDoID in (" + nworkid + ")") == -1)
            {
                Response.Write("<script>alert('删除选中记录时发生错误！请重试！');</script>");
            }
            else
            {
                Response.Write("<script>alert('删除选中记录成功！');</script>");

                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除地质资料（成果）借阅信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
        }
    }
    protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("ERPDZZLCGJieYueAdd.aspx?FormID=" + formid + "&WorkFlowID=" + workFlowID);
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');

        string strShenPiYiJian = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ShenPiYiJian from ERPNWorkToDo where ID=" + CheckStrArray[0].ToString());
        string strStateNow = ZWL.DBUtility.DbHelperSQL.GetSHSL("select StateNow from ERPNWorkToDo where ID=" + CheckStrArray[0].ToString());
        //驳回的项目可以修改
        if (!strStateNow.Equals("已被驳回"))
        {
            if (!string.IsNullOrEmpty(strShenPiYiJian))
            {
                Response.Write("<script language='javascript'>alert('该地质资料（成果）借阅已经通过审核不能修改！');</script>");
                return;
            }
        }
        string strURL = "ERPDZZLCGJieYueAdd.aspx?Nwid=" + CheckStrArray[0].ToString();
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

    private string GetSqlWhere()
    {
        var strWhere = "";
        //if (txtNumber.Text != "")
        //{
        //    strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" Number like '%{0}%'", txtNumber.Text);
        //}
        if (txtXingMing.Text != "")
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" (d.Name like '%{0}%' or e.UserName like '%{0}%') ", txtXingMing.Text);
        }

        if (txtDept.Text != "")
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" Department like '%{0}%' ", txtDept.Text);
        }
        return strWhere;
    }
}