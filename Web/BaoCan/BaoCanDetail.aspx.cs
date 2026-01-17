using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class HR_BaoCanDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            TimeStr_Start.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btnReport.Visible = PublicMethod.StrIFIn("|ERPBaoCanMXE|", PublicMethod.GetSessionValue("QuanXian"));
            btnShare.Visible = PublicMethod.StrIFIn("|ERPBaoCanMXS|", PublicMethod.GetSessionValue("QuanXian"));
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
            var item = ((DataRowView)e.Row.DataItem).Row;
            var lblKQJL = (Label)e.Row.FindControl("lblKQJL");
            var linkSFCC = (HyperLink)e.Row.FindControl("linkSFCC");
            if (lblKQJL != null)
            {
                var kqjl = item["KaoQinRecord"].ToString();
                if (!kqjl.IsNullOrEmpty() && kqjl != "-")
                {
                    lblKQJL.Text = kqjl.Split(' ')[0];
                }
            }
            if (linkSFCC != null)
            {
                var date = DateTime.MinValue;
                DateTime.TryParse(item["RecordDate"].ToString(), out date);
                if (date != DateTime.MinValue)
                {
                    var text = "否";
                    var link = "javascript:void(0);";
                    var csql = @"select top 1 d.ID from ERPChuChai c join ERPNWorkToDo d on c.NWorkID=d.ID 
				where StateNow='正常结束' and BSState='已报销' and (c.SQR='{1}' or TongXingRenYuan like '%{1}%')
				and '{0}'>=c.ChuChaiStart and '{0}'<=c.ChuChaiEnd".FormatWith(date.Date, item["Name"].ToString());
                    var cresult = DbHelperSQL.GetSHSLInt1(csql);
                    if (cresult > 0)
                    {
                        text = "<b>是</b>";
                        link = "../NWorkFlow/NWorkToDoViewNew.aspx?ID={0}".FormatWith(PublicMethod.EncryptParam(cresult));
                    }
                    else
                    {
                        linkSFCC.Enabled = false;
                    }
                    linkSFCC.Text = text;
                    linkSFCC.NavigateUrl = link;
                }
            }
        }
    }
    protected void BtnSerch_Click(object sender, ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
        DataBindToGridview();
    }
    protected string getSql(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append(@"select b.[ID],b.[LotID],[Number],[Name],a.[UserName],[ZhiWu],b.[Sex],[RecordDate],[XingQi],[ShiJianDuan],[CanShi],[ShiJianDian],[KaoQinRecord],[Memo],[IsChuChai],[BCRQ],[IsCancel],ISNULL(u.[Department], REPLACE(b.Dept, '地质局/', '')) Department,'' as sfbc,'' as sfyc,ISNULL(u.[UserName], b.Name) as xm,'' as rq,'' as xq,'' as ycsd,'' iserror,m.BackInfo,m.DirID,u.DisplayID
                        from FanTangJiuCanRecord b
                        join [Flow] f on f.LotID=b.LotID and Operation=1 and [KaoQinRecord] != '-'
                        FULL OUTER JOIN (select * from ERPBaoCan where IsCancel='否') a ON a.UserName = b.Name and a.BCRQ = b.[RecordDate] and a.ShiJianDian = b.CanShi
                        left join ERPUser u on(u.[UserName] = a.[UserName] or u.[UserName] = b.[Name])
                        LEFT JOIN ERPBuMen m on u.Department=m.BuMenName
                        where 1=1 ");

        if (strWhere.Trim() != "")
        {
            strSql.Append(" and " + strWhere);
        }
        //throw new Exception(strSql.ToString());
        return strSql.ToString();
    }

    public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
    {
        return new Pager(getSql(strWhere), cPage, pSize, orderby);
    }

    protected override void DataBindToGridview()
    {
        var sqlWhere = GetSqlWhere();
        if (sqlWhere.Contains("UserName="))
        {
            sqlWhere = sqlWhere.Replace("UserName=", "a.UserName=");
        }
        var MyModel = new ZWL.BLL.FanTangJiuCanRecord();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        var pager = GetListAndPaging(sqlWhere, currPage, pageSize, " (case when BCRQ is null then RecordDate else BCRQ end) asc,cast(ISNULL(DirID, 999) as int),cast(ISNULL(BackInfo, 999) as int),DisplayID ");
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
        Hashtable MyTable = new Hashtable();
        MyTable.Add("Department", "部门");
        MyTable.Add("xm", "姓名");
        MyTable.Add("rq", "日期");
        MyTable.Add("xq", "星期");
        MyTable.Add("ycsd", "用餐时段");
        MyTable.Add("sfbc", "是否报餐");
        MyTable.Add("sfyc", "是否用餐");
        MyTable.Add("iserror", "是否异常");

        var sqlWhere = GetSqlWhere();
        if (sqlWhere.Contains("UserName="))
        {
            sqlWhere = sqlWhere.Replace("UserName=", "a.UserName=");
        }
        var MyModel = new ZWL.BLL.FanTangJiuCanRecord();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        var pager = GetListAndPaging(sqlWhere, currPage, pageSize, " (case when BCRQ is null then RecordDate else BCRQ end) asc,cast(ISNULL(DirID, 999) as int),cast(ISNULL(BackInfo, 999) as int),DisplayID ");
        if (pager != null)
        {
            if (pager.ExecuteToDataTableWithOutPaging())
            {
                var dt = (DataTable)pager.Result;
                ds.Tables.Add(((DataTable)pager.Result).Copy());
            }
        }
        if (ds.Tables.Count > 0)
        {
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["rq"] = Convert.ToDateTime(!string.IsNullOrEmpty(dr["RecordDate"].ToString()) ? dr["RecordDate"] : dr["BCRQ"]).ToString("yyyy-MM-dd");
                dr["xq"] = GetDayOfWeek(Convert.ToDateTime(dr["rq"]));
                dr["ycsd"] = !string.IsNullOrEmpty(dr["CanShi"].ToString()) ? dr["CanShi"] : dr["ShiJianDian"];
                dr["sfbc"] = GetBaoCanState2(dr["UserName"].ToString(), dr["Name"].ToString());
                dr["sfyc"] = !string.IsNullOrEmpty(dr["Name"].ToString()) ? "是" : "否";
                dr["iserror"] = GetIsError2(dr["UserName"].ToString(), dr["Name"].ToString());
            }
        }
        DataToExcel.GridViewToExcel(ds, MyTable, "报餐及用餐明细表");
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
    public string GetDayOfWeek(DateTime dt)
    {
        string[] DayWeekArray = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        return DayWeekArray[Convert.ToInt32(dt.DayOfWeek.ToString("d"))].ToString();
    }

    public string GetBaoCanState(string username, string name)
    {
        var result = GetBaoCanState2(username, name);
        var text = "否";
        var color = "black";
        if (text != result)
        {
            text = result;
            color = "blue";
        }
        return "<span style='color:{0};'>{1}</span>".FormatWith(color, text);
    }

    public string GetIsError(string username, string name)
    {
        var result = GetIsError2(username, name);
        var text = "否";
        var color = "black";
        if (text != result)
        {
            text = result;
            color = "red";
        }
        return "<span style='color:{0};'>{1}</span>".FormatWith(color,text);
    }
    public string GetIsError2(string username, string name)
    {
        if (ExceptUsers.Contains("'" + name + "'"))
        {
            return "否";
        }
        else
        {
            if (ExceptUsers.Contains("'" + username + "'"))
            {
                return "否";
            }
            return username != name ? "是" : "否";
        }
    }

    public string GetBaoCanState2(string username, string name)
    {
        if (ExceptUsers.Contains("'" + name + "'"))
        {
            return "是";
        }
        else
        {
            if (ExceptUsers.Contains("'" + username + "'"))
            {
                return "否";
            }
            return !string.IsNullOrEmpty(username) ? "是" : "否";
        }
    }
    private string ExceptUsers
    {
        get
        {
            return "'蔡晓帆','谢廷忠'";
        }
    }
    /// <summary>
    /// 刷新页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
        Response.Redirect(Request.Url.ToString());
    }

    private string GetSqlWhere()
    {
        var strWhere = "";
        //var lotid = Get("LotID");
        //if (lotid != null)
        //{
        //    strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" LotID='{0}'", lotid);
        //}
        //if (BianHao.Text != "")
        //{
        //    strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" Number like '%{0}%'", BianHao.Text);
        //}
        if (Name.Text != "")
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" (Name like '%{0}%' or a.UserName like '%{0}%')", Name.Text);
        }

        if (Dept.Text != "")
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" (Department like '%{0}%') ", Dept.Text);
            //strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + string.Format(" (Dept like '%{0}%' or BuMen like '%{0}%')", Dept.Text);
        }
        if (!string.IsNullOrEmpty(TimeStr_Start.Text))
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + " (RecordDate >= '" + DateTime.Parse(TimeStr_Start.Text).Date + "' or BCRQ >= '" + DateTime.Parse(TimeStr_Start.Text).Date + "') ";
        }
        if (!string.IsNullOrEmpty(TimeStr_End.Text))
        {
            strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + " (RecordDate < '" + DateTime.Parse(TimeStr_End.Text).Date.AddDays(1) + "' or BCRQ < '" + DateTime.Parse(TimeStr_End.Text).Date.AddDays(1) + "')";
        }
        if (DataType.Text != "全部")
        {
            if (DataType.Text == "异常")
            {
                strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + " (a.UserName is null or Name is null) and (a.UserName not in ('蔡晓帆','谢廷忠') or Name not in ('蔡晓帆','谢廷忠')) ";
            }
            else if (DataType.Text == "正常")
            {
                strWhere += PublicMethod.GetSqlKeywordAnd(strWhere) + " a.UserName is not null and Name is not null";
            }
        }
        var limitSql = GetLimitDataSqlWhere("ERPBaoCanMX");

        strWhere += PublicMethod.GetSqlAndByWhere(strWhere, limitSql) + limitSql;
        return strWhere;
    }
}