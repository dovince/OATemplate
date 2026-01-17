using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class Financial_BudgetDetailInfo : BasePage
{
    private bool _showPage = true;
    public bool ShowPage
    {
        get
        {
            var showp = Request.QueryString.Get("ShowPage");
            if (!string.IsNullOrEmpty(showp) && showp == "false")
            {
                _showPage = false;
            }
            return _showPage;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            var pcmodel = new ZWL.BLL.ERPProjectCost();
            pcmodel.GetModel(Id);
            TextBox_xmbh.Text = pcmodel.XMBH;
            TextBox_xmname.Text = pcmodel.XMName;
            this.TextBox_htbh.Text = pcmodel.HTBH;
            DataBindToGridview();
            //设定按钮权限            
            ImageButton4.Visible = PublicMethod.StrIFIn("|cost001A|", PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton5.Visible = PublicMethod.StrIFIn("|cost001M|", PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton3.Visible = PublicMethod.StrIFIn("|cost001D|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = PublicMethod.StrIFIn("|cost001E|", PublicMethod.GetSessionValue("QuanXian"));
            HiddenField_query.Value = "true";
            var budget = new ZWL.BLL.ERPBudgetDetail();
            var list = budget.GetListModelByParentId(pcmodel.ID);
            if (list != null && list.Count > 0)
            {
                lblLeftAdjustTimes.Visible = true;
                var times = 0;
                if ((3 - list.Count) >= 0)
                    times = 3 - list.Count;
                lblLeftAdjustTimes.Text = string.Format("剩余调整次数：{0}次", times);
                ImageButton4.ImageUrl = "../images/Button/BtnAdjust.jpg";
            }
        }
    }

    public void DataBindToGridview()
    {
        var MyModel = new ZWL.BLL.ERPProjectCost();
        MyModel.GetModel(Id);
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        DataSet ds = new DataSet();
        var currentPage = ((int)GVData.PageIndex + 1);
        var pageSize = Convert.ToInt32(TxtPageSize.Text);
        var totalPage = 0;

        var costdetil = new ZWL.BLL.ERPBudgetDetail();

        string strwhere = " ParentId=" + MyModel.ID;
        var pager = costdetil.GetListAndPaging(strwhere, currentPage, pageSize);
        if (pager.ExecuteToDataSet())
        {
            ds = (DataSet)pager.Result;
            totalPage = pager.TotalPage;
            GVData.DataSource = ds;
            ViewState["dataset"] = ds;
            GVData.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                FileUpload.Visible = false;
                ImageBtnImport.Visible = false;
            }
        }

        var pageSum = Convert.ToString(totalPage == 0 ? GVData.PageCount : totalPage);
        LabPageSum.Text = pageSum;
        LabCurrentPage.Text = currentPage.ToString();
        GVData.PageIndex = currentPage;
        LabPageSum.Text = pageSum;
        HdfPageSum.Value = pageSum;
        GoPage.Text = currentPage.ToString();
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
                    GVData.PageSize = MyPageSize;
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
        PublicMethod.GridViewRowDataBound(e);
        string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
        //增加合计信息
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;
        }
    }
    public string GetFieldCount(DataSet ds, string strfieldname)
    {

        decimal dcount = 0.00M;
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal dvalue = 0.00M;
                var columns = ds.Tables[0].Columns;
                if (columns.Contains(strfieldname))
                    decimal.TryParse(ds.Tables[0].Rows[i][strfieldname].ToString(), out dvalue);
                dcount += dvalue;
            }
        }
        return String.Format("{0:###,###,##0.00}", Double.Parse(dcount.ToString()));
    }
    /// <summary>
    /// 添加项目财务管理记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        var msg = "";
        if (ValidateInput(ref msg))
        {
            ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
            pcmodel.GetModel(Id);
            string strxmbh = pcmodel.XMBH;
            var url = "ProjectBudgetAdd.aspx?XMBH=" + strxmbh + "&HTBH=" + pcmodel.HTBH + "&ID=" + Id;
            string CheckStr = PublicMethod.CheckCbx(GVData, "CheckSelect", "LabVisible");
            string[] CheckStrArray = CheckStr.Split(',');
            if (CheckStrArray != null && CheckStrArray.Length > 0)
            {
                url += "&Version=" + CheckStrArray[0].ToString();
            }
            Response.Redirect(url);
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool ValidateInput(ref string msg)
    {
        var result = true;
        var pcmodel = new ZWL.BLL.ERPProjectCost();
        pcmodel.GetModel(Id);

        var budget = new ZWL.BLL.ERPBudgetDetail();
        var list = budget.GetListModel("ParentId" + pcmodel.ID);
        if (list != null && list.Count > 0)
        {
            var checkList = new List<string>() { "罗鑫","admin" };
            if (list.Count >= 3 && !checkList.Contains(PublicMethod.GetUserName()))
            {
                msg = "最大调整次数为3，当前项目剩余调整次数为0.";
                return false;
            }
        }
        return result;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string IDlist = PublicMethod.CheckCbxL(GVData, "CheckSelect", "LabVisible");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPBudgetDetail where ID in (" + IDlist + ")") == -1)
        {
            ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
        }
        else
        {
            //decimal sum=0.00M;
            //foreach(var d in dmodel){
            //    sum+=d.工资及津贴;
            //}
            //ZWL.BLL.ERPProjectCost pcmodel = new ZWL.BLL.ERPProjectCost();
            //pcmodel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            //pcmodel.CostMoneySUM = pcmodel.CostMoneySUM - sum;
            //pcmodel.Update();
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
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
            MyTable.Add("Version", "调整次数");
            MyTable.Add("CreatedTime", "录入日期");
            MyTable.Add("Comment", "摘要");
            MyTable.Add("sum", "合计");
            MyTable.Add("工资及津贴", "工资及津贴");
            MyTable.Add("工程出包费", "工程出包费");
            MyTable.Add("材料费", "材料费");
            MyTable.Add("租赁费", "租赁费");
            MyTable.Add("劳务费", "劳务费");
            MyTable.Add("安全生产费用", "安全生产费用");
            MyTable.Add("办公费", "办公费");
            MyTable.Add("维修费用", "维修费用");
            MyTable.Add("交通运输费用", "交通运输费用");
            MyTable.Add("差旅费", "差旅费");
            MyTable.Add("邮电费用", "邮电费用");
            MyTable.Add("其它费用", "其它费用");
            DataToExcel.GridViewToExcel(ds, MyTable, TextBox_xmbh.Text + "Excel报表");
        }
        else
        {
            Response.Write("<script>alert('导出数据为空！');</script>");
        }

    }
    //修改
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(GVData, "CheckSelect", "LabVisible");
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
        string CheckStr = PublicMethod.CheckCbx(GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("ProjectBudget.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void ImageButton_goback_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }

    /// <summary>
    /// 添加项目财务管理记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnImport_Click(object sender, ImageClickEventArgs e)
    {
        if (this.FileUpload.FileName != "" && this.FileUpload.FileName.IndexOf(".xls") > 0 || this.FileUpload.FileName != "" && this.FileUpload.FileName.IndexOf(".xlsx") > 0)
        {
            string result = "";
            //读取EXCEL文件
            string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir1(this.FileUpload, System.IO.Path.GetFileNameWithoutExtension(FileUpload.PostedFile.FileName) + DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload.PostedFile.FileName));
            string strexcelfilepath = Path.Combine(ZWL.Common.PublicMethod.UploadFileFolderTruePath, FileNameStr);
            try
            {
                if (System.IO.File.Exists(strexcelfilepath))
                {
                    var file = new FileStream(strexcelfilepath, FileMode.Open, FileAccess.Read);
                    var wbook = WorkbookFactory.Create(file);
                    //var wbook = new HSSFWorkbook(file);
                    ISheet sheet = null;
                    var sheetnum = wbook.NumberOfSheets;
                    for (var i = 0; i < sheetnum; i++)
                    {
                        if (!wbook.IsSheetHidden(i))
                        {
                            sheet = wbook.GetSheetAt(i);
                            break;
                        }
                    }
                    if (sheet == null)
                    {
                        throw new Exception("导入的Excel中所有sheet都被隐藏了！");
                    }

                    var rownum = sheet.LastRowNum;
                    if (rownum > 1)
                    {
                        var titlerow = sheet.GetRow(1);
                        if (titlerow != null)
                        {
                            var titlecell = titlerow.GetCell(0);
                            if (titlecell == null)
                            {
                                throw new Exception("导入的Excel格式不正确,标题位置有误！");
                            }

                            var costdetil = new ZWL.BLL.ERPBudgetDetail() { ParentId = Id };
                            var htrow = sheet.GetRow(3);
                            if (htrow != null)
                            {
                                var htbhcell = htrow.GetCell(7);
                                if (htbhcell != null)
                                {
                                    costdetil.HTBH = htbhcell.StringCellValue;
                                }
                                else
                                {
                                    throw new Exception("导入的Excel格式不正确,合同编号位置有误！");
                                }
                            }
                            if (TextBox_htbh.Text != costdetil.HTBH)
                            {
                                throw new Exception("导入的Excel中合同编号为【" + costdetil.HTBH + "】与该项目的合同编号【" + TextBox_htbh.Text + "】不一致！");
                            }

                            var xmrow = sheet.GetRow(4);
                            if (xmrow != null)
                            {
                                var xmbhcell = xmrow.GetCell(2);
                                if (xmbhcell != null)
                                {
                                    costdetil.XMBH = xmbhcell.StringCellValue;
                                }
                                else
                                {
                                    throw new Exception("导入的Excel格式不正确！");
                                }
                            }
                            if (TextBox_xmbh.Text != costdetil.XMBH)
                            {
                                throw new Exception("导入的Excel中项目编号与该项目编号不一致！" + costdetil.XMBH);
                            }
                            //costdetil.XMBH = TextBox_xmbh.Text;

                            for (var i = 4; i < 7; i++)
                            {
                                var rows = sheet.GetRowEnumerator();
                                var currentIndex = 0;
                                var addflag = false;
                                while (rows.MoveNext())
                                {
                                    var row = (IRow)rows.Current;
                                    if (row == null || currentIndex == 0)
                                    {
                                        currentIndex++;
                                        continue;
                                    }
                                    var fykmcol = row.GetCell(1);
                                    if (fykmcol != null)
                                    {
                                        var fykm = "";
                                        try
                                        {
                                            fykm = fykmcol.StringCellValue;
                                        }
                                        catch
                                        {

                                        }

                                        var fycol = row.GetCell(i);
                                        if (fycol != null)
                                        {
                                            decimal fy = 0;
                                            try
                                            {
                                                fy = Convert.ToDecimal(fycol.NumericCellValue);
                                            }
                                            catch
                                            {
                                            }
                                            var fykmflag = true;
                                            switch (fykm)
                                            {
                                                case "工资及津贴":
                                                    costdetil.工资及津贴 = fy;
                                                    break;
                                                case "工程出包费":
                                                    costdetil.工程出包费 = fy;
                                                    break;
                                                case "材料费":
                                                    costdetil.材料费 = fy;
                                                    break;
                                                case "租赁费":
                                                    costdetil.租赁费 = fy;
                                                    break;
                                                case "劳务费":
                                                    costdetil.劳务费 = fy;
                                                    break;
                                                case "安全生产费用":
                                                    costdetil.安全生产费用 = fy;
                                                    break;
                                                case "办公费":
                                                    costdetil.办公费 = fy;
                                                    break;
                                                case "维修费用":
                                                    costdetil.维修费用 = fy;
                                                    break;
                                                case "交通运输费用":
                                                    costdetil.交通运输费用 = fy;
                                                    break;
                                                case "差旅费":
                                                    costdetil.差旅费 = fy;
                                                    break;
                                                case "邮电费用":
                                                    costdetil.邮电费用 = fy;
                                                    break;
                                                case "水电费":
                                                    costdetil.水电费 = fy;
                                                    break;
                                                case "印刷费":
                                                    costdetil.印刷费 = fy;
                                                    break;
                                                case "会议费":
                                                    costdetil.会议费 = fy;
                                                    break;
                                                case "其它费用":
                                                    costdetil.其它费用 = fy;
                                                    break;
                                                default:
                                                    fykmflag = false;
                                                    break;
                                            }
                                            if (fy > 0 && fykmflag)//只要有一条数据大于0就代表可以插入了
                                            {
                                                addflag = true;
                                            }
                                        }
                                    }
                                }
                                if (addflag)
                                {
                                    costdetil.CreatedTime = DateTime.Now;
                                    costdetil.Version = i - 4;
                                    costdetil.Add();
                                }
                            }

                            ZWL.Common.MessageBox.Show(this, "导入成功！");
                        }
                        else
                        {
                            throw new Exception("导入的Excel格式不正确,标题位置为空！");
                        }
                    }
                }

                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户导入项目预算";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
            catch (Exception ex)
            {
                ZWL.Common.MessageBox.Show(this, ex.Message + "导入失败，请联系管理员！");
            }
        }
    }

    public decimal ConvertToDecimal(double str)
    {
        try
        {
            return Convert.ToDecimal(str);
        }
        catch
        {
            return 0;
        }
    }

    protected void DownLoadMB_Click(object sender, EventArgs e)
    {
        Response.Redirect("../UploadFile/项目费用预算表模板（20221213）.xls");
    }
}