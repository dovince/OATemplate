using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZWL.Common;

public partial class Financial_CostDetailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限            
            ImageButton4.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost002A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton5.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost002M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost002D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost002E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";
        }
    }
    public void DataBindToGridview()
    {
        ZWL.BLL.ERPCostDetail MyModel = new ZWL.BLL.ERPCostDetail();
        string strJiaoSe = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = ZWL.Common.PublicMethod.GetSessionValue("Department");
        DataSet ds = new DataSet();
        var currentPage = ((int)GVData.PageIndex + 1);
        var pageSize = Convert.ToInt32(this.TxtPageSize.Text);
        var totalPage = 0;


        string strwhere = "";
        strwhere = GetQueryString();
        if (strbumen == "中心领导" || strJiaoSe == "超级管理员" || strbumen == "财务科")
        {
            this.ImageButton3.Visible = true;//显示删除按钮
            //显示所有记录，按照填表日期排序，最新的排在前面
            if (strwhere != "")
            {

                strwhere += " and XMBH like '%%' ";
            }
            else
            {
                strwhere = " XMBH like '%%' ";
            }
        }
 
        var pager = MyModel.GetListAndPaging(strwhere, currentPage, pageSize);
        if (pager.ExecuteToDataSet())
        {
            ds = (DataSet)pager.Result;
            totalPage = pager.TotalPage;
            GVData.DataSource = ds;
            ViewState["dataset"] = ds;
            GVData.DataBind();
        }



        var pageSum = Convert.ToString(totalPage == 0 ? GVData.PageCount : totalPage);
        LabPageSum.Text = pageSum;
        LabCurrentPage.Text = currentPage.ToString();
        GVData.PageIndex = currentPage;
        LabPageSum.Text = pageSum;
        HdfPageSum.Value = pageSum;
        this.GoPage.Text = currentPage.ToString();
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
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }

    /// <summary>
    /// 根据界面上控件中的值组合查询语句
    /// </summary>
    /// <returns></returns>
    public string GetQueryString()
    {
        string strwhere = "";
        if (this.TextBox_xmname.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " beiyong1 like '%" + this.TextBox_xmname.Text + "%'";
        }
        if (this.TextBox_bm.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " beiyong2 like '%" + this.TextBox_bm.Text + "%'";
        }
        if (this.TextBox_xmbh.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " XMBH like '%" + this.TextBox_xmbh.Text + "%'";
        }
        //if (DropDownListZFFS.SelectedItem.Text != "")
        //{
        //    if (strwhere != "")
        //    {
        //        strwhere += " and ";
        //    }
        //    strwhere += " ZFFS = '" + DropDownListZFFS.SelectedItem.Text + "'";
        //}
        //if (TextBox_Start.Text != "" && TextBox_End.Text != "")
        //{
        //    if (strwhere != "")
        //    {
        //        strwhere += " and FKDate >= '" + TextBox_Start.Text + "' and FKDate <= '" + TextBox_End.Text + "'";
        //    }
        //    else
        //    {
        //        strwhere += "FKDate >= '" + TextBox_Start.Text + "' and FKDate <= '" + TextBox_End.Text + "'";
        //    }

        //}
        return strwhere;
    }
      protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
    }
    /// <summary>
    /// 添加项目财务管理记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CostDetailAdd.aspx");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPCostDetail where ID in (" + IDlist + ")") == -1)
        {
            ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
        }
        else
        {
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
        DataSet ds = (DataSet)ViewState["dataset"];
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Hashtable MyTable = new Hashtable();
            MyTable.Add("ID", "序号");
            MyTable.Add("XMName", "项目名称");
            MyTable.Add("XMBH", "项目编号");
            MyTable.Add("ZYLB", "专业类别");
            MyTable.Add("XMBM", "实施部门");
            MyTable.Add("XMFZR", "项目负责人");
            MyTable.Add("HTJE", "合同金额");
            MyTable.Add("JSJE", "结算金额");
            MyTable.Add("CostMoneySUM", "成本支出合计");
            //MyTable.Add("JBR", "经办人");
            //MyTable.Add("FKDate", "付款日期");
            //MyTable.Add("BZ", "备注");
            //MyTable.Add("FKState", "支付状态");
            ZWL.Common.DataToExcel.GridViewToExcel(ds, MyTable, "Excel报表");
        }
        else
        {
            Response.Write("<script>alert('导出数据为空！');</script>");
        }

    }
    //修改
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
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
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("ProjectBudget.aspx?ID=" + CheckStrArray[0].ToString());
    }
}
