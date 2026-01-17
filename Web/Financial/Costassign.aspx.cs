using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZWL.Common;
using System.Reflection;

public partial class Financial_Costassign : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            this.TextBox_Start.Text = DateTime.Now.AddMonths(-1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            this.TextBox_End.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            PublicMethod.BindDepartmentDDL(DropDownListBM, "select BuMenName from ERPBuMen where DirID in (130,131) order by BuMenName", Department);
            DataBindToGridview();

            //设定按钮权限            
            ImageButton2.Visible = ZWL.Common.PublicMethod.StrIFIn("|cost003E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));

            this.HiddenField_query.Value = "true";

            this.TextBox_Start.Text = DateTime.Now.AddMonths(-1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            this.TextBox_End.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }
    }
    public void DataBindToGridview()
    {
        string strJiaoSe = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = ZWL.Common.PublicMethod.GetSessionValue("Department");
        string strUserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");

        if (strbumen == "中心领导" || strJiaoSe == "超级管理员" || strJiaoSe.Contains("监察科") || strbumen == "财务科" || strUserName == "罗鑫")
        {

        }
        else
        {
            btnout.Enabled = false;
            btnout.Visible = false;
            DropDownListBM.Enabled = false;
            DropDownListBM.SelectedValue = strbumen;
        }


        this.lbtittle.Text = DropDownListKM.SelectedValue + "成本费用分配表";
        lbbzbm.Text = "报账部门名称：" + this.DropDownListBM.SelectedValue;
        lbrq.Text = DateZH(DateTime.Now.ToShortDateString());
        ZWL.BLL.ERPProjectCost MyModel = new ZWL.BLL.ERPProjectCost();

        DataSet ds = new DataSet();
        var currentPage = (int)GVData.PageIndex;
        if ((int)GVData.PageIndex == 0)
        {
            currentPage = ((int)GVData.PageIndex + 1);
        }

        var pageSize = Convert.ToInt32(this.TxtPageSize.Text);
        var totalPage = 0;
        string strwhere = "";
        strwhere = GetQueryString();

        //else if (strJiaoSe.Contains("部门负责人"))
        //{
        //    if (strwhere != "")
        //    {
        //        strwhere += " and pc.XMBM='" + strbumen + "' ";
        //    }
        //    else
        //    {
        //        strwhere = " pc.XMBM='" + strbumen + "' ";
        //    }

        //}
        //else
        //{
        //    if (strwhere != "")
        //    {
        //        strwhere += " and pc.XMFZR='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' ";
        //    }
        //    else
        //    {
        //        strwhere = " pc.XMFZR='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' ";
        //    }
        //}

        var pager = MyModel.GetListAndPagingCostassign(strwhere, currentPage, pageSize, this.DropDownListKM.SelectedValue);
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
        Response.Redirect("ProjectCostAdd.aspx");
    }
    /// <summary>
    /// 导出到EXCEL表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
        DataSet ds = (DataSet)ViewState["dataset"];
        if (DropDownListBM.SelectedValue == "")
        {
            //ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select xm.HTJE 合同金额,pc.XMName 项目名称 ,cd.XMBH 项目编号,(case when xm.XMJF=0 or xm.XMJF is null then 1 else Convert(decimal(18,2),cd." + this.DropDownListKM.SelectedValue + "/xm.XMJF) end) 费用分配率,cd." + this.DropDownListKM.SelectedValue + " 费用分配额 from ERPCostDetail cd,ERPProjectCost pc,ERPXMJBXX xm  where cd.XMBH=pc.XMBH and cd.XMBH =xm.XMBH and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "' and " + this.DropDownListKM.SelectedValue + ">0");
            ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select pc.HTJE 合同金额,pc.XMName 项目名称 ,cd.XMBH 项目编号,(case when pc.HTJE=0 or pc.HTJE is null then 1 else Convert(decimal(18,2),cd." + this.DropDownListKM.SelectedValue + "/pc.HTJE) end) 费用分配率,cd." + this.DropDownListKM.SelectedValue + " 费用分配额 from ERPCostDetail cd,ERPProjectCost pc where cd.XMBH=pc.XMBH and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "' and " + this.DropDownListKM.SelectedValue + ">0");
        }
        else
        {
            //ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select xm.XMJF 合同金额,pc.XMName 项目名称 ,cd.XMBH 项目编号,(case when xm.XMJF=0 or xm.XMJF is null then 1 else Convert(decimal(18,2),cd." + this.DropDownListKM.SelectedValue + "/xm.XMJF) end) 费用分配率,cd." + this.DropDownListKM.SelectedValue + " 费用分配额 from ERPCostDetail cd,ERPProjectCost pc,ERPXMJBXX xm  where cd.XMBH=pc.XMBH and cd.XMBH =xm.XMBH and pc.XMBM='" + DropDownListBM.SelectedValue + "' and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "' and " + this.DropDownListKM.SelectedValue + ">0");
            ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select pc.HTJE 合同金额,pc.XMName 项目名称 ,cd.XMBH 项目编号,(case when pc.HTJE=0 or pc.HTJE is null then 1 else Convert(decimal(18,2),cd." + this.DropDownListKM.SelectedValue + "/pc.HTJE) end) 费用分配率,cd." + this.DropDownListKM.SelectedValue + " 费用分配额 from ERPCostDetail cd,ERPProjectCost pc where cd.XMBH=pc.XMBH and pc.XMBM='" + DropDownListBM.SelectedValue + "' and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "' and " + this.DropDownListKM.SelectedValue + ">0");
        }


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Hashtable MyTable = new Hashtable();
            DataTable dt = ds.Tables[0];


            string sourcefile = System.Web.HttpContext.Current.Server.MapPath("../") + "SetupFile\\成本费用分配表.xls";
            string datestring = DateTime.Now.ToString("yyyyMMddHHmmsss");
            string destfilefullname = "成本费用分配表" + datestring + ".xls";
            string destfile = System.Web.HttpContext.Current.Server.MapPath("../") + "ReportFile\\成本费用分配表" + datestring + ".xls";

            Costassignoutput(ds.Tables[0], this.lbtittle.Text, lbbzbm.Text, this.lbrq.Text, sourcefile, destfile);
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                if (!p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (destfile != "")
            {
                //System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + destfilefullname, false);
                //用来解决导出统计结果文件时出现的404问题，20160810
                string strFilePath = System.Web.HttpContext.Current.Server.MapPath("../") + "ReportFile/" + destfilefullname;
                if (System.IO.File.Exists(strFilePath))
                {
                    //ZWL.Common.PublicMethod.WirteLog("已经生成了统计结果文件开始导出"+strFilePath);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.AddHeader("Content-Disposition", "inline;filename=" + Server.UrlEncode(destfilefullname));
                    Response.WriteFile(strFilePath);
                    Response.Flush();
                    Response.Close();
                }
            }
        }
        else
        {
            Response.Write("<script>alert('导出数据为空！');</script>");
        }

    }
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Data.DataSet currentds = (System.Data.DataSet)ViewState["dataset"];
            System.Data.DataTable currentdt = currentds.Tables[0];
            string indexstring1 = "合同金额";
            string indexstring2 = "费用分配额";
            e.Row.Cells[0].Text = "-";

            e.Row.BackColor = System.Drawing.Color.Gainsboro;
            e.Row.Cells[1].Text = "合计";
            e.Row.Cells[1].Font.Bold = true;

            e.Row.Cells[3].Text = GetFieldCount(currentds, indexstring1);
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].Text = GetFieldCount(currentds, indexstring2);
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[4].Text = String.Format("{0:###,###,##0.00}", decimal.Parse(e.Row.Cells[3].Text) != 0 ? Double.Parse(((decimal.Parse(e.Row.Cells[5].Text) / decimal.Parse(e.Row.Cells[3].Text)).ToString())) : 1);
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

        }
    }
    public string GetFieldCount(System.Data.DataSet ds, string strfieldname)
    {

        decimal dcount = 0.00M;
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal dvalue = 0.00M;
                decimal.TryParse(ds.Tables[0].Rows[i][strfieldname].ToString(), out dvalue);
                dcount += dvalue;
            }
        }
        return String.Format("{0:###,###,##0.00}", Double.Parse(dcount.ToString()));
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
            strwhere += " xm.XMName like '%" + this.TextBox_xmname.Text + "%'";
        }
        if (this.DropDownListBM.SelectedValue != "")
        {
            var strUserName = PublicMethod.GetUserName();
            var strbumen = this.DropDownListBM.SelectedValue;

            var bmname = "xm.XMBM";
            var bumensql = "='" + strbumen + "'";
            var sqlWhere = CombineDataLimitExtendSqlWhere(bmname, bumensql);
            strwhere += PublicMethod.GetSqlKeywordAnd(strwhere) + sqlWhere;
        }

        if (TextBox_Start.Text != "" && TextBox_End.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "'";
            }
            else
            {
                strwhere += "cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "'";
            }
        }
        return strwhere;
    }
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
    //日期转化函数
    public static string DateZH(string bgdate)
    {
        try
        {
            DateTime ymd = DateTime.Parse(bgdate);
            DateTime time = DateTime.Parse(ymd.ToShortDateString());
            string cndate = time.Year + "年" + time.Month + "月" + time.Day + "日";
            return cndate;
        }
        catch
        {
            return bgdate;
        }

    }
    protected void DropDownListKM_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    protected void DropDownListBM_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    private void Costassignoutput(System.Data.DataTable dt, string title, string bunmen, string strtimespan, string sourcefile, string destfile)
    {
        Excel.Application excelApp;
        Excel._Workbook workBook;
        Excel._Worksheet worksheet;
        excelApp = new Excel.Application();
        workBook = excelApp.Workbooks.Add(sourcefile);
        worksheet = (Excel.Worksheet)workBook.Worksheets[1];
        worksheet.get_Range(excelApp.Cells[1, 1], excelApp.Cells[50, 50]).Select();
        int colnum = worksheet.UsedRange.Columns.Count;
        int rownum = worksheet.UsedRange.Rows.Count;
        char mb = (char)(65 + colnum);
        Hashtable hashrow = new Hashtable();
        Excel.Range ranger = worksheet.get_Range("A5", "F5");
        object[,] itemr = (object[,])ranger.Value2;

        for (int i = 1; i <= itemr.GetLength(1); i++)
        {
            if (itemr[1, i] != null)
            {
                string strtext = itemr[1, i].ToString().Replace(" ", "");//删除行名中间的空格
                strtext = strtext.Trim();
                hashrow.Add(strtext, i);//第一行
            }
        }
        if (!string.IsNullOrEmpty(title))
        {
            worksheet.Cells[1, 1] = title; //该单元格在EXCEL中为[A1],Cells[行,列]
        }
        if (!string.IsNullOrEmpty(bunmen))
        {
            worksheet.Cells[2, 1] = bunmen; //该单元格在EXCEL中为[A1],Cells[行,列]
        }
        //String strTitle = itemr[1, 1].ToString();
        if (!string.IsNullOrEmpty(strtimespan))
        {
            worksheet.Cells[2, 3] = strtimespan; //该单元格在EXCEL中为[A1],Cells[行,列]
        }

        worksheet.Name = "成本费用分配表";
        ArrayList bumenlist = new ArrayList();//部门列表

        //根据部门来写excel，一个部门一个部门的写入到excel中
        int ntemprowindex = 6;
        Excel.Range objCopy = worksheet.get_Range("A6", "F6");
        Excel.Range objCopyxm = worksheet.get_Range("A7", "F7");
        Excel.Range objCopysum = worksheet.get_Range("A8", "F8");

        string xmname = "";
        double sumHTmoney = 0;//合同金额
        double HTmoney = 0;//合同金额
        double sumassignmoney = 0;//费用分配总额
        double rowcounts = new double();
        double xmrowcounts = new double();//用于装载单个项目各个分项的合计

        int flag = 0;//用于设置分项目标记
        double xmHTmoney = 0;//项目合同金额
        double xmassignmoney = 0;//费用分配总额

        rowcounts = 0;
        xmrowcounts = 0;

        //筛选的行集合的循环
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            //从第4行开始插入
            objCopy.Select();
            objCopy.Copy(Type.Missing);
            Excel.Range range1 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
            range1.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);

            for (int c = 0; c < dt.Columns.Count; c++)
            {

                //遍历列，获取当前的列名称
                string strcolname = dt.Columns[c].ToString();
                int colindex = 0;
                if (hashrow.ContainsKey(strcolname))
                {
                    colindex = int.Parse(hashrow[strcolname].ToString());
                }
                if (colindex > 0)
                {
                    if (strcolname != null && !string.IsNullOrEmpty(strcolname.ToString()))
                    {
                        string strxmname = dt.Rows[j][strcolname].ToString();//获取当前行列的值
                        if (hashrow.ContainsKey("序号"))
                        {
                            worksheet.Cells[ntemprowindex, 1] = j + 1;
                        }
                        if (strcolname.Equals("项目名称"))
                        {
                            if (strxmname == xmname)
                            {
                                flag = 1;
                            }
                            else if (flag == 1)
                            {
                                flag = 2;
                                objCopyxm.Select();
                                objCopyxm.Copy(Type.Missing);
                                Excel.Range range2 = (Excel.Range)worksheet.Rows[ntemprowindex - 1, Type.Missing];
                                range1.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);

                                worksheet.Cells[ntemprowindex, 6] = xmrowcounts;
                                worksheet.Cells[ntemprowindex, 5] = xmrowcounts / double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());

                                worksheet.Cells[ntemprowindex, 4] = worksheet.Cells[ntemprowindex - 1, 4];
                                sumHTmoney += double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());
                                ntemprowindex++;

                            }
                            else
                            {
                                flag = 0;
                            }
                            xmname = strxmname;

                        }
                        if (strcolname.Equals("合同金额"))
                        {
                            if (flag == 1)
                                xmHTmoney = double.Parse(strxmname);

                            HTmoney = double.Parse(strxmname);
                            //sumHTmoney += double.Parse(strxmname);
                        }
                        if (strcolname.Equals("费用分配额"))
                        {

                            if (hashrow.ContainsKey(strcolname))
                            {
                                if (flag == 1 || flag == 0)
                                {
                                    double num = 0;
                                    double.TryParse(strxmname, out num);

                                    if (rowcounts == -1)
                                    { rowcounts = 0; }
                                    rowcounts += num;

                                    if (xmrowcounts == -1)
                                    { xmrowcounts = 0; }
                                    xmrowcounts += num;

                                }
                                else if (flag == 2)
                                {
                                    double num = 0;
                                    double.TryParse(strxmname, out num);

                                    if (rowcounts == -1)
                                    { rowcounts = 0; }
                                    rowcounts += num;


                                    if (xmrowcounts == -1)
                                    { xmrowcounts = 0; }
                                    xmrowcounts = 0;
                                    xmrowcounts += num;

                                }
                            }


                        }
                        worksheet.Cells[ntemprowindex, colindex] = strxmname;
                    }
                }
            }
            ntemprowindex++;
        }
        //最后项目合计处理
        flag = 2;
        objCopyxm.Select();
        objCopyxm.Copy(Type.Missing);
        Excel.Range range4 = (Excel.Range)worksheet.Rows[ntemprowindex - 1, Type.Missing];
        Excel.Range range3 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        range3.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
        if (xmrowcounts != -1)
        {
            worksheet.Cells[ntemprowindex, 6] = xmrowcounts;
            worksheet.Cells[ntemprowindex, 5] = xmrowcounts / double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());
        }

        worksheet.Cells[ntemprowindex, 4] = worksheet.Cells[ntemprowindex - 1, 4];
        sumHTmoney += double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());

        Excel.Range ranges = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        ntemprowindex++;
        objCopysum.Select();
        objCopysum.Copy(Type.Missing);
        ranges.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);

        if (rowcounts != -1)
        {
            worksheet.Cells[ntemprowindex, 6] = rowcounts;
            worksheet.Cells[ntemprowindex, 5] = rowcounts / sumHTmoney;
        }

        worksheet.Cells[ntemprowindex, 4] = sumHTmoney;
        //    worksheet.Cells[ntemprowindex, 4] = sumHTmoney;//合计合同金额
        //worksheet.Cells[ntemprowindex, 6] = sumassignmoney;//合计费用分配额
        //worksheet.Cells[ntemprowindex, 5] = String.Format("{0:###,###,##0.00}", (Double)(sumassignmoney / sumHTmoney));//合计费用分配率

        worksheet.Cells[3, 3] = rowcounts;//合计合同金额
        worksheet.Cells[3, 6] = String.Format("{0:###,###,##0.00}", (Double)(rowcounts / sumHTmoney));//合计费用分配率

        ntemprowindex++;

        //删除最后一行用来做模板的合计行
        Excel.Range rangelast = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        Excel.Range rangelast1 = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        rangelast.Delete(Type.Missing);
        rangelast1.Delete(Type.Missing);
        Excel.Range rangelast2 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        Excel.Range rangelast3 = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        rangelast2.Delete(Type.Missing);
        rangelast3.Delete(Type.Missing);

        workBook.SaveAs(destfile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        workBook.Close(null, null, null);
        excelApp.Quit();
        if (worksheet != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            worksheet = null;
        }
        if (workBook != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            workBook = null;
        }
        if (excelApp != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelApp = null;
        }
        GC.Collect();//垃圾回收
    }

    private void MainCostassignoutput(System.Data.DataTable dt, string bm, string strtimespan, string sourcefile, string destfile)
    {
        Excel.Application excelApp;
        Excel._Workbook workBook;
        Excel._Worksheet worksheet;
        excelApp = new Excel.Application();
        workBook = excelApp.Workbooks.Add(sourcefile);
        worksheet = (Excel.Worksheet)workBook.Worksheets[1];
        worksheet.get_Range(excelApp.Cells[1, 1], excelApp.Cells[50, 50]).Select();
        int colnum = worksheet.UsedRange.Columns.Count;
        int rownum = worksheet.UsedRange.Rows.Count;
        char mb = (char)(65 + colnum);
        Hashtable hashrow = new Hashtable();
        Excel.Range ranger = worksheet.get_Range("A5", "BD5");
        object[,] itemr = (object[,])ranger.Value2;

        for (int i = 1; i <= itemr.GetLength(1); i++)
        {
            if (itemr[1, i] != null)
            {
                string strtext = itemr[1, i].ToString().Replace(" ", "");//删除行名中间的空格
                strtext = strtext.Trim();
                hashrow.Add(strtext, i);//第一行
            }
        }
        if (!string.IsNullOrEmpty(bm))
        {
            worksheet.Cells[2, 1] = bm; //该单元格在EXCEL中为[A1],Cells[行,列]
        }

        //String strTitle = itemr[1, 1].ToString();
        if (!string.IsNullOrEmpty(strtimespan))
        {
            worksheet.Cells[2, 3] = strtimespan; //该单元格在EXCEL中为[A1],Cells[行,列]
        }

        worksheet.Name = "成本费用分配表";
        ArrayList bumenlist = new ArrayList();//部门列表

        //根据部门来写excel，一个部门一个部门的写入到excel中
        int ntemprowindex = 6;
        Excel.Range objCopy = worksheet.get_Range("A6", "BD6");
        Excel.Range objCopyxm = worksheet.get_Range("A7", "BD7");
        Excel.Range objCopysum = worksheet.get_Range("A8", "BD8");

        string xmname = "";
        double sumHTmoney = 0;//合同金额
        double HTmoney = 0;//合同金额
        double sumassignmoney = 0;//费用分配总额
        double[] rowcounts = new double[70];
        double[] xmrowcounts = new double[70];//用于装载单个项目各个分项的合计

        int flag = 0;//用于设置分项目标记
        double xmHTmoney = 0;//项目合同金额
        double xmassignmoney = 0;//费用分配总额

        for (int s = 0; s < 70; s++)
        {
            rowcounts[s] = -1;
        }
        for (int s = 0; s < 70; s++)
        {
            xmrowcounts[s] = -1;
        }
        //筛选的行集合的循环
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            //从第4行开始插入
            objCopy.Select();
            objCopy.Copy(Type.Missing);
            Excel.Range range1 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
            range1.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);

            for (int c = 0; c < dt.Columns.Count; c++)
            {

                //遍历列，获取当前的列名称
                string strcolname = dt.Columns[c].ToString();
                int colindex = 0;
                if (hashrow.ContainsKey(strcolname))
                {
                    colindex = int.Parse(hashrow[strcolname].ToString());
                }
                if (colindex > 0)
                {
                    if (strcolname != null && !string.IsNullOrEmpty(strcolname.ToString()))
                    {
                        string strxmname = dt.Rows[j][strcolname].ToString();//获取当前行列的值
                        if (hashrow.ContainsKey("序号"))
                        {
                            worksheet.Cells[ntemprowindex, 1] = j + 1;
                        }
                        if (strcolname.Equals("项目名称"))
                        {
                            if (strxmname == xmname)
                            {
                                flag = 1;
                            }
                            else if (flag == 1)
                            {
                                flag = 2;
                                objCopyxm.Select();
                                objCopyxm.Copy(Type.Missing);
                                Excel.Range range2 = (Excel.Range)worksheet.Rows[ntemprowindex - 1, Type.Missing];
                                range1.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                                for (int s = 4; s < 70; s++)
                                {
                                    if (xmrowcounts[s] != -1)
                                    {
                                        worksheet.Cells[ntemprowindex, s] = xmrowcounts[s];
                                        worksheet.Cells[ntemprowindex, s + 1] = xmrowcounts[s] / double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());
                                    }
                                }
                                worksheet.Cells[ntemprowindex, 4] = worksheet.Cells[ntemprowindex - 1, 4];
                                sumHTmoney += double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());
                                ntemprowindex++;

                            }
                            else
                            {
                                flag = 0;
                            }
                            xmname = strxmname;

                        }
                        if (strcolname.Equals("合同金额"))
                        {
                            if (flag == 1)
                                xmHTmoney = double.Parse(strxmname);

                            HTmoney = double.Parse(strxmname);
                            //sumHTmoney += double.Parse(strxmname);
                        }
                        if (strxmname.Trim() != "")
                        {

                            if (hashrow.ContainsKey(strcolname))
                            {
                                if (flag == 1 || flag == 0)
                                {
                                    double num = 0;
                                    double.TryParse(strxmname, out num);

                                    if (rowcounts[colindex] == -1)
                                    { rowcounts[colindex] = 0; }
                                    rowcounts[colindex] += num;

                                    if (xmrowcounts[colindex] == -1)
                                    { xmrowcounts[colindex] = 0; }
                                    xmrowcounts[colindex] += num;

                                }
                                else if (flag == 2)
                                {
                                    double num = 0;
                                    double.TryParse(strxmname, out num);

                                    if (rowcounts[colindex] == -1)
                                    { rowcounts[colindex] = 0; }
                                    rowcounts[colindex] += num;


                                    if (xmrowcounts[colindex] == -1)
                                    { xmrowcounts[colindex] = 0; }
                                    xmrowcounts[colindex] = 0;
                                    xmrowcounts[colindex] += num;

                                }
                            }


                        }
                        worksheet.Cells[ntemprowindex, colindex] = strxmname;
                        if (colindex > 3)
                        {
                            worksheet.Cells[ntemprowindex, colindex + 1] = double.Parse(strxmname) / HTmoney;
                            sumassignmoney += double.Parse(strxmname);
                        }
                    }
                }
            }
            ntemprowindex++;
        }
        //最后项目合计处理
        flag = 2;
        objCopyxm.Select();
        objCopyxm.Copy(Type.Missing);
        Excel.Range range4 = (Excel.Range)worksheet.Rows[ntemprowindex - 1, Type.Missing];
        Excel.Range range3 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        range3.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
        for (int s = 4; s < 70; s++)
        {
            if (xmrowcounts[s] != -1)
            {
                worksheet.Cells[ntemprowindex, s] = xmrowcounts[s];
                worksheet.Cells[ntemprowindex, s + 1] = xmrowcounts[s] / double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());
            }
        }
        worksheet.Cells[ntemprowindex, 4] = worksheet.Cells[ntemprowindex - 1, 4];
        sumHTmoney += double.Parse(worksheet.get_Range(worksheet.Cells[ntemprowindex - 1, 4], worksheet.Cells[ntemprowindex - 1, 4]).Value2.ToString());

        Excel.Range ranges = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        ntemprowindex++;
        objCopysum.Select();
        objCopysum.Copy(Type.Missing);
        ranges.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
        for (int s = 4; s < 70; s++)
        {
            if (rowcounts[s] != -1)
            {
                worksheet.Cells[ntemprowindex, s] = rowcounts[s];
                worksheet.Cells[ntemprowindex, s + 1] = rowcounts[s] / sumHTmoney;
            }
        }
        worksheet.Cells[ntemprowindex, 4] = sumHTmoney;
        //    worksheet.Cells[ntemprowindex, 4] = sumHTmoney;//合计合同金额
        //worksheet.Cells[ntemprowindex, 6] = sumassignmoney;//合计费用分配额
        //worksheet.Cells[ntemprowindex, 5] = String.Format("{0:###,###,##0.00}", (Double)(sumassignmoney / sumHTmoney));//合计费用分配率
        sumassignmoney = 0;
        for (int s = 5; s < 70; s++)
        {
            if (rowcounts[s] != -1)
            {
                sumassignmoney += rowcounts[s];
            }
        }
        worksheet.Cells[3, 3] = sumassignmoney;//合计合同金额
        worksheet.Cells[3, 6] = String.Format("{0:###,###,##0.00}", (Double)(sumassignmoney / sumHTmoney));//合计费用分配率

        ntemprowindex++;

        //删除最后一行用来做模板的合计行
        Excel.Range rangelast = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        Excel.Range rangelast1 = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        rangelast.Delete(Type.Missing);
        rangelast1.Delete(Type.Missing);
        Excel.Range rangelast2 = (Excel.Range)worksheet.Rows[ntemprowindex, Type.Missing];
        Excel.Range rangelast3 = (Excel.Range)worksheet.Rows[ntemprowindex + 1, Type.Missing];
        rangelast2.Delete(Type.Missing);
        rangelast3.Delete(Type.Missing);

        workBook.SaveAs(destfile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        workBook.Close(null, null, null);
        excelApp.Quit();
        if (worksheet != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            worksheet = null;
        }
        if (workBook != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            workBook = null;
        }
        if (excelApp != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelApp = null;
        }
        GC.Collect();//垃圾回收
    }
    protected void btnout_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select xm.XMJF 合同金额,pc.XMName 项目名称 ,cd.XMBH 项目编号,* from ERPCostDetail cd,ERPProjectCost pc,ERPXMJBXX xm  where cd.XMBH=pc.XMBH and cd.XMBH =xm.XMBH and cd.beiyong2 >= '" + TextBox_Start.Text + "' and cd.beiyong2 <= '" + TextBox_End.Text + "' and xm.XMBM='" + DropDownListBM.SelectedValue + "' ");


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Hashtable MyTable = new Hashtable();
            DataTable dt = ds.Tables[0];

            string sourcefile = System.Web.HttpContext.Current.Server.MapPath("../") + "SetupFile\\成本费用分配总表.xls";
            string datestring = DateTime.Now.ToString("yyyyMMddHHmmsss");
            string destfilefullname = "成本费用分配总表" + datestring + ".xls";
            string destfile = System.Web.HttpContext.Current.Server.MapPath("../") + "ReportFile\\成本费用分配总表" + datestring + ".xls";

            MainCostassignoutput(ds.Tables[0], DropDownListBM.SelectedValue, this.lbrq.Text, sourcefile, destfile);
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                if (!p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Session["CheckOver_DFFN"] = destfilefullname;//添加session用于检测是否导出完成
            if (destfile != "")
            {
                //System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + destfilefullname, false);
                //用来解决导出统计结果文件时出现的404问题，20160810
                string strFilePath = System.Web.HttpContext.Current.Server.MapPath("../") + "ReportFile/" + destfilefullname;
                if (System.IO.File.Exists(strFilePath))
                {
                    //ZWL.Common.PublicMethod.WirteLog("已经生成了统计结果文件开始导出"+strFilePath);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.AddHeader("Content-Disposition", "inline;filename=" + Server.UrlEncode(destfilefullname));
                    Response.WriteFile(strFilePath);
                    Response.Flush();
                    Response.Close();
                }
            }
        }
        else
        {
            Response.Write("<script>alert('导出数据为空！');</script>");
        }

    }
}