

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.DBUtility;

public partial class RequireCreate_ERPBaoCanAddManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            BCRQ_Start.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BCRQ_End.Text = Convert.ToDateTime(BCRQ_Start.Text).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            DataBindToGridview("");
            //设定按钮权限
            //ImageButton1.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPBaoCanA|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton5.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPBaoCanM|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton3.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPBaoCanD|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPBaoCanE|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));


        }
    }

    public void AddBaoCanList()
    {
        //报餐日期
        //DateTime bcrqs = DateTime.Parse(this.BCRQ_Start.Text);
        DateTime bcrqs = DateTime.Now.AddDays(1);
        DateTime bcrqe = DateTime.Parse(this.BCRQ_End.Text);

        var workdays = ZWL.Common.PublicMethod.GetWorkDays(bcrqs, bcrqe);
        foreach (var workday in workdays)
        {
            //替换控件中的值到表单中
            ZWL.BLL.ERPBaoCan erpbaocan = new ZWL.BLL.ERPBaoCan();
            erpbaocan.WorkName = "";
            erpbaocan.DengJiTime = DateTime.Now;//以此为默认时间
                                                //是否取消
            erpbaocan.IsCancel = "否";
            //取消日期
            erpbaocan.CancelTime = DateTime.Now;//以此为默认时间
                                                //用户名
            erpbaocan.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            //部门
            erpbaocan.BuMen = ZWL.Common.PublicMethod.GetSessionValue("Department");

            {
                var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐'");
                if (num == 0)
                {
                    erpbaocan.ShiJianDian = "早餐";
                    erpbaocan.BCRQ = workday;
                    //新增
                    erpbaocan.Add();
                }
            }

            {
                var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐'");
                if (num == 0)
                {
                    erpbaocan.ShiJianDian = "午餐";
                    erpbaocan.BCRQ = workday;
                    //新增
                    erpbaocan.Add();
                }
            }
        }
    }
    #region  分页方法
    protected void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (GoPage.Text.Trim().ToString() == "")
            {
                Response.Write("<script language='javascript'>alert('页码不可以为空!');</script>");
            }
            else if (GoPage.Text.Trim().ToString() == "0" || Convert.ToInt32(GoPage.Text.Trim().ToString()) > GVData.PageCount)
            {
                Response.Write("<script language='javascript'>alert('页码不是一个有效值!');</script>");
            }
            else if (GoPage.Text.Trim() != "")
            {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if (PageI >= 0 && PageI < (GVData.PageCount))
                {
                    GVData.PageIndex = PageI;
                }
            }

            if (TxtPageSize.Text.Trim().ToString() == "")
            {
                Response.Write("<script language='javascript'>alert('每页显示行数不可以为空!');</script>");
            }
            else if (TxtPageSize.Text.Trim().ToString() == "0")
            {
                Response.Write("<script language='javascript'>alert('每页显示行数不是一个有效值!');</script>");
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
                    Response.Write("<script language='javascript'>alert('每页显示行数不是一个有效值!');</script>");
                }
            }

            DataBindToGridview("");
        }
        catch
        {
            DataBindToGridview("");
            Response.Write("<script language='javascript'>alert('请输入有效数字！');</script>");
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
        DataBindToGridview("");
    }
    #endregion
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HyperLink hlwork = (HyperLink)e.Row.FindControl("HyperLink1");
        //}
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        //保存上一次查询结果
        string JJ = "0";
        for (int i = 0; i < this.GVData.Rows.Count; i++)
        {
            Label LabV = (Label)GVData.Rows[i].FindControl("LabVisible");
            JJ = JJ + "," + LabV.Text.Trim();
        }
        DataBindToGridview(JJ);
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview("");
    }
    public void DataBindToGridview(string nworkid)
    {
        //AddBaoCanList();

        ZWL.BLL.ERPNWorkToDo MyModel = new ZWL.BLL.ERPNWorkToDo();
        //string JJ = formid.ToString();

        var sql = GetWhere();

        DataSet ds = GetListNotInManager(sql, "ERPBaoCan", " BCRQ asc ");

        GVData.DataSource = ds;
        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }
    public DataSet GetListNotInManager(string strWhere, string tablename, string orderby = "")
    {
        var strSql = new StringBuilder();
        strSql.Append("select [BCRQ],[UserName],[BuMen] ");
        strSql.AppendFormat(@" FROM {0} ", tablename);
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        strSql.Append(" group by [BCRQ],[UserName],[BuMen] ");
        if (orderby == "")
        {
            strSql.Append(" order by ID desc ");
        }
        else
        {
            strSql.Append(" order by " + orderby + " ");
        }
        return DbHelperSQL.Query(strSql.ToString());
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        var MyTable = new System.Collections.Generic.Dictionary<string, string>();
     
        MyTable.Add("BCRQ", "报餐日期"); 
        MyTable.Add("IsCancel", "是否取消"); 
        MyTable.Add("CancelTime", "取消日期"); 
        MyTable.Add("UserName", "用户名"); 
        MyTable.Add("BuMen", "部门");  
        MyTable.Add("DengJiTime", "登记时间");
        
        Hashtable hashTable = new Hashtable();
        foreach (var key in MyTable.Keys)
        {
            hashTable.Add(key, MyTable[key]);
        }

        var sql = GetWhere();
        var MyModel = new ZWL.BLL.ERPNWorkToDo();
        var ds = MyModel.GetListInManager(sql, "ERPBaoCan");
        ZWL.Common.DataToExcel.GridViewToExcel(ds, hashTable, "报餐管理Excel报表");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string nworkid = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPNWorkToDo where StateNow='已被驳回' and ID in (" + nworkid + ")") == -1)
        {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else
        {
            if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPBaoCan where NWorkToDoID in (" + nworkid + ")") == -1)
            {
                Response.Write("<script>alert('删除选中记录时发生错误！请重试！');</script>");
            }
            else
            {
                Response.Write("<script>alert('删除选中记录成功！');</script>");

                DataBindToGridview("");
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除报餐管理信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ERPBaoCanAdd.aspx");
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
                Response.Write("<script language='javascript'>alert('该报餐管理已经通过审核不能修改！');</script>");
                return;
            }
        }
        string strURL = "ERPBaoCanAdd.aspx?Nwid=" + CheckStrArray[0].ToString();
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

    protected void GVData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var commandName = e.CommandName.ToString();
        var id = e.CommandArgument.ToString();
        if (commandName == "quxiao")
        {
            //午餐取消截止时间为当天上午9点,早餐取消截止时间为昨天晚上12点
            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '是',[CancelTime] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID='" + id + "' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-9).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
        }
        if (commandName == "baocan")
        {
            //午餐取消截止时间为当天上午9点,早餐取消截止时间为昨天晚上12点
            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '否',[CancelTime] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID='" + id + "' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-9).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
        }
        
        DataBindToGridview("");
        return;
    }

    public bool CanCancel(object id)
    {
        var result = ZWL.DBUtility.DbHelperSQL.GetSHSLInt1("select count(ID) from [ERPBaoCan] WHERE ID='" + id + "' and IsCancel='否' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-9).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
        return result == 1;
    }

    public bool CanBaoCan(object id)
    {
        var result = ZWL.DBUtility.DbHelperSQL.GetSHSLInt1("select count(ID) from [ERPBaoCan] WHERE ID='" + id + "' and IsCancel='是' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-9).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
        return result == 1;
    }

    Dictionary<DateTime, string> dtList = new Dictionary<DateTime, string>();
    public string GetCaiDanTuPian(DateTime dt)
    {
        if (dtList.Keys.Contains(dt))
        {
            return dtList[dt];
        }
        else
        {
            //获取菜单图片
            var caidan = DbHelperSQL.GetSHSL("SELECT TOP 1 CaiDanTuPian FROM [ERPCaiDan] WHERE [ZhanShiRiQiQi]<='" + dt.ToString("yyyy-MM-dd") + "' AND [ZhanShiRiQiZhi]>='" + dt.ToString("yyyy-MM-dd") + "' ");
            dtList.Add(dt, caidan.Replace("|", ""));
            return caidan.Replace("|", "");
        }
    }

    public string GetBaoCanQingKuang(DateTime workday, string username)
    {
        var res = "";
        var IsCancel = DbHelperSQL.GetSHSL("SELECT TOP 1 IsCancel FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + username + "' AND [ShiJianDian]='早餐'");
        if (IsCancel == "否")
        {
            res += "<span style='color:blue;'>早餐√</span>";
        }
        else
        {
            res += "<span style='color:red;'>早餐X</span>";
        }

        IsCancel = DbHelperSQL.GetSHSL("SELECT TOP 1 IsCancel FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + username + "' AND [ShiJianDian]='午餐'");
        if (IsCancel == "否")
        {
            res += "<span style='color:blue;'> 午餐√</span>";
        }
        else
        {
            res += "<span style='color:red;'> 午餐X</span>";
        }

        return res;
    }

    private string GetWhere()
    {
        var strWhere = " 1=1 ";
        var strdepartment = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");

        if (strdepartment.Contains("超级管理员"))
        {
        }
        //else if (strdepartment.Contains("部门负责人"))
        //{
        //    string strbumen = ZWL.Common.PublicMethod.GetSessionValue("Department");
        //    string namelist = "'" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'";
        //    DataTable namedt = new DataTable();
        //    namedt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select UserName from ERPUser where Department='" + strbumen + "'");
        //    for (int i = 0; i < namedt.Rows.Count; i++)
        //    {
        //        namelist += ",'" + namedt.Rows[i]["UserName"].ToString() + "'";
        //    }
        //    strWhere += " and UserName in (" + namelist + ") ";
        //}
        else
        {
            strWhere += " and UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' ";
        }
        if (!string.IsNullOrEmpty(BCRQ_Start.Text))
        {
            strWhere += " and BCRQ >= '" + DateTime.Parse(BCRQ_Start.Text).Date + "'";
        }
        if (!string.IsNullOrEmpty(BCRQ_End.Text))
        {
            strWhere += " and BCRQ < '" + DateTime.Parse(BCRQ_End.Text).Date.AddDays(1) + "'";
        }
      //  if (IsCancel.SelectedItem.Text != "")
      //  {
      //      strWhere += string.Format(" and IsCancel like '%{0}%'", IsCancel.Text);
      //  }
      //if (UserName.Text != "")
      //  {
      //      strWhere += string.Format(" and UserName like '%{0}%'", UserName.Text);
      //  }
      //if (BuMen.Text != "")
      //  {
      //      strWhere += string.Format(" and BuMen like '%{0}%'", BuMen.Text);
      //  }

      //  if (!string.IsNullOrEmpty(TimeStr_Start.Text))
      //  {
      //      strWhere += " and DengJiTime >= '" + DateTime.Parse(TimeStr_Start.Text).Date + "'";
      //  }
      //  if (!string.IsNullOrEmpty(TimeStr_End.Text))
      //  {
      //      strWhere += " and DengJiTime < '" + DateTime.Parse(TimeStr_End.Text).Date.AddDays(1) + "'";
      //  }
        return strWhere;
    }
}