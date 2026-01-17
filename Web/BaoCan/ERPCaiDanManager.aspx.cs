

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequireCreate_ERPCaiDanManager : System.Web.UI.Page
{
    //public string formid = ""自己填FormId";
    //public string workFlowID = ""自己填WorkFlowId";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview("");
            //设定按钮权限
            ImageButton1.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPCaiDanA|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton5.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPCaiDanM|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPCaiDanD|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|ERPCaiDanE|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlwork = (HyperLink)e.Row.FindControl("HyperLink1");
        }
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
        ZWL.BLL.ERPNWorkToDo MyModel = new ZWL.BLL.ERPNWorkToDo();
        //string JJ = formid.ToString();
        DataSet ds = new DataSet();
        var sql = GetWhere();

        ds = MyModel.GetListNotInManager(sql, "ERPCaiDan");
        GVData.DataSource = ds;
        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        var MyTable = new System.Collections.Generic.Dictionary<string, string>();
     
        MyTable.Add("ZhanShiRiQiQi", "展示日期起"); 
        MyTable.Add("ZhanShiRiQiZhi", "展示日期止"); 
        MyTable.Add("CaiDanTuPian", "菜单图片"); 
        MyTable.Add("ModifyTime", "修改时间"); 
        MyTable.Add("UserName", "用户名"); 
        MyTable.Add("BuMen", "部门");  
        MyTable.Add("WorkName", "工作名称");
        MyTable.Add("DengJiTime", "发起时间");
        
        Hashtable hashTable = new Hashtable();
        foreach (var key in MyTable.Keys)
        {
            hashTable.Add(key, MyTable[key]);
        }

        var sql = GetWhere();
        var MyModel = new ZWL.BLL.ERPNWorkToDo();
        var ds = MyModel.GetListInManager(sql, "ERPCaiDan");
        ZWL.Common.DataToExcel.GridViewToExcel(ds, hashTable, "菜单管理Excel报表");
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
            if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPCaiDan where ID in (" + nworkid + ")") == -1)
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
                MyRiZhi.DoSomething = "用户删除菜单管理信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ERPCaiDanAdd.aspx");
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
                Response.Write("<script language='javascript'>alert('该菜单管理已经通过审核不能修改！');</script>");
                return;
            }
        }
        string strURL = "ERPCaiDanAdd.aspx?Nwid=" + CheckStrArray[0].ToString();
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

    public string GetCaiDanName(string item)
    {
        try
        {
            var model = new ZWL.BLL.ERPSaveFileName();
            model = model.GetModelByNowName(item.Replace("|", ""));
            return model.OldName;
        }
        catch
        {
            return "";
        }
    }

    private string GetWhere()
    {
        var strWhere = " 1=1 ";
        var strdepartment = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");

        if (strdepartment.Contains("单位领导") || strdepartment.Contains("超级管理员") || strdepartment.Contains("办公室"))
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
        if (!string.IsNullOrEmpty(ZhanShiRiQiQi_Start.Text))
        {
            strWhere += " and ZhanShiRiQiQi >= '" + DateTime.Parse(ZhanShiRiQiQi_Start.Text).Date + "'";
        }
        if (!string.IsNullOrEmpty(ZhanShiRiQiQi_End.Text))
        {
            strWhere += " and ZhanShiRiQiQi < '" + DateTime.Parse(ZhanShiRiQiQi_End.Text).Date.AddDays(1) + "'";
        }
      if (UserName.Text != "")
        {
            strWhere += string.Format(" and UserName like '%{0}%'", UserName.Text);
        }
      if (BuMen.Text != "")
        {
            strWhere += string.Format(" and BuMen like '%{0}%'", BuMen.Text);
        }
  
        if (!string.IsNullOrEmpty(TimeStr_Start.Text))
        {
            strWhere += " and DengJiTime >= '" + DateTime.Parse(TimeStr_Start.Text).Date + "'";
        }
        if (!string.IsNullOrEmpty(TimeStr_End.Text))
        {
            strWhere += " and DengJiTime < '" + DateTime.Parse(TimeStr_End.Text).Date.AddDays(1) + "'";
        }
        return strWhere;
    }
}