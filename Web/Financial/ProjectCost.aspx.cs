using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZWL.BLL;
using ZWL.Common;
using ZWL.DBUtility;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class Financial_ProjectCost : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            InitSuperQueryControl();
            this.TextBox_Start.Text = TimeParser.GetFormatDateString(DateTime.Now.AddYears(-5));
            this.TextBox_End.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            DataBindToGridview();

            //设定按钮权限            
            ImageButton4.Visible = PublicMethod.StrIFIn("|cost001A|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton5.Visible = PublicMethod.StrIFIn("|cost001M|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton3.Visible = PublicMethod.StrIFIn("|cost001D|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = PublicMethod.StrIFIn("|cost001E|", PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";

            var array2d = retseasondata();
            var num = retseasonnum(DateTime.Now.Month);
        }
    }
    protected override void DataBindToGridview()
    {
        //隐藏列的控制
        if (GVData.Columns[14].Visible == false)
        {
            GVData.Columns[14].Visible = true;
        }
        //隐藏列2的控制
        if (GVData.Columns[16].Visible == false)
        {
            GVData.Columns[16].Visible = true;

        }

        var ds = new DataSet();
        var strJiaoSe = PublicMethod.GetJiaoSe();
        var MyModel = new ZWL.BLL.ERPProjectCost();
        var currentPage = ((int)GVData.PageIndex + 1);
        var pageSize = Convert.ToInt32(this.TxtPageSize.Text);
        var totalPage = 0;
        var strwhere = GetQueryString();
        var pager = MyModel.GetListAndPagingFinancial(strwhere, currentPage, pageSize, "ID,XMBH desc");
        if (pager.ExecuteToDataSet())
        {
            ds = (DataSet)pager.Result;
            if (pager.Rows > 0)
            {
                var sqlwhere = @"select t.*,h.XMID from (						
						select HTBH,sum(DaoZhangJE) s,sum(KaiPiaoJE) k
															from ERPHeTongDaoZhang h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh
															) t LEFT JOIN ERPHeTong h on t.HTBH=h.HTID
															where t.HTBH in (
															select HTID from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过')
															and XMID in (
															
															select XMBH from (
															{0}
															
																	) t GROUP BY XMBH
																)
															)".FormatWith(pager.ResultSQL);
                var XMAmtdt = DbHelperSQL.GetDataTable(sqlwhere);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var item = ds.Tables[0].Rows[i];
                        var xmbh = item["XMBH"].ToString();
                        var htbh = item["HTBH"].ToString();
                        if (htbh.IsNullOrEmpty() && XMAmtdt != null && XMAmtdt.Rows.Count > 0)
                        {
                            var flist = XMAmtdt.AsEnumerable().Where(r => r.Field<string>("XMID") == xmbh);
                            if (flist != null && flist.Any())
                            {
                                item["开票金额"] = flist.Sum(r => r.Field<decimal>("k"));
                                item["收款金额"] = flist.Sum(r => r.Field<decimal>("s"));
                            }
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                }
            }
            totalPage = pager.TotalPage;
            GVData.DataSource = ds;
            GVData.DataBind();
        }

        var sql = MyModel.GetFinancialSql(strwhere).ToString();
        sql = string.Format("select isnull(sum(HTJE),0) HTJE,isnull(sum(HTJE2),0) HTJE2,isnull(sum(结算金额),0) 结算金额,isnull(sum(开票金额),0) 开票金额,isnull(sum(收款金额),0) 收款金额,isnull(sum(CostSums),0) CostSums from ({0}) as newtable", sql);
        var dt = DbHelperSQL.GetDataTable(sql);
        if (dt != null)
        {
            LabelXMJEHJ.Text = Convert.ToDouble((dt.Rows[0]["HTJE"] == null || dt.Rows[0]["HTJE"] is System.DBNull) ? "0" : dt.Rows[0]["HTJE"]).ToString("###,###,##0.00");
            LabelHTJEHJ.Text = Convert.ToDouble((dt.Rows[0]["HTJE2"] == null || dt.Rows[0]["HTJE2"] is System.DBNull) ? "0" : dt.Rows[0]["HTJE2"]).ToString("###,###,##0.00");
            LabelJSJEHJ.Text = Convert.ToDouble((dt.Rows[0]["结算金额"] == null || dt.Rows[0]["结算金额"] is System.DBNull) ? "0" : dt.Rows[0]["结算金额"]).ToString("###,###,##0.00");
            LabelKPJEHJ.Text = Convert.ToDouble((dt.Rows[0]["开票金额"] == null || dt.Rows[0]["开票金额"] is System.DBNull) ? "0" : dt.Rows[0]["开票金额"]).ToString("###,###,##0.00");
            LabelSKJEHJ.Text = Convert.ToDouble((dt.Rows[0]["收款金额"] == null || dt.Rows[0]["收款金额"] is System.DBNull) ? "0" : dt.Rows[0]["收款金额"]).ToString("###,###,##0.00");
            LabelCBZCHJ.Text = Convert.ToDouble((dt.Rows[0]["CostSums"] == null || dt.Rows[0]["CostSums"] is System.DBNull) ? "0" : dt.Rows[0]["CostSums"]).ToString("###,###,##0.00");
        }
        Labelrowcount.Text = Convert.ToString(pager == null ? ds.Tables[0].Rows.Count : pager.Rows);
        var pageSum = Convert.ToString(totalPage == 0 ? GVData.PageCount : totalPage);
        LabPageSum.Text = pageSum;
        LabCurrentPage.Text = currentPage.ToString();
        GVData.PageIndex = currentPage;
        LabPageSum.Text = pageSum;
        HdfPageSum.Value = pageSum;
        this.GoPage.Text = currentPage.ToString();
        if (strJiaoSe == "超级管理员" || strJiaoSe.Contains("核心人物"))
        {
            GVData.Columns[13].Visible = true;
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
            if (GVData.Columns[15].Visible == false)
            {
                GVData.Columns[15].Visible = true;

            }
            if (GVData.Columns[17].Visible == false)
            {
                GVData.Columns[17].Visible = true;

            }
            var xmbhLink = (HyperLink)e.Row.FindControl("HyperLink_XMBH");
            var linkJieSuan = (HyperLink)e.Row.FindControl("linkJieSuan");
            var xmNameLink = (HyperLink)e.Row.FindControl("XMNameLink");
            var linkNo = (HyperLink)e.Row.FindControl("linkNo");
            var htbhLink = (HyperLink)e.Row.FindControl("linkHTBH");
            var checkSelect = (CheckBox)e.Row.FindControl("CheckSelect");
            var xmbh = item["XMBH"].ToString();
            var htbh = item["HTBH"].ToString();
            var infoModel = Conv<ZWL.BLL.ERPProjectCost>.GetModel("select top 1 * from ERPProjectCost where XMBH='{0}' and HTBH='{1}'".FormatWith(xmbh, htbh));
            var alertmsg = "javarscript:alert('{0}');".FormatWith("无数据或未登记。");
            //判断是否为旧项，旧项目的项目编号为“C”开头
            if (xmbh.Contains("C"))
            {
                e.Row.Cells[7].Text = String.Format("{0:###,###,##0.00}", infoModel.XMJF);
                e.Row.Cells[8].Text = String.Format("{0:###,###,##0.00}", infoModel.HTJE);
                e.Row.Cells[9].Text = String.Format("{0:###,###,##0.00}", infoModel.JSJE);

                e.Row.Cells[16].Text = String.Format("{0:#0.00%}", (Double.Parse(item["CostSums"].ToString()) / (Double.Parse(item["CostSums"].ToString()))));
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[4].ToolTip = e.Row.Cells[4].Text;
                if ((Double.Parse(item["CostSums"].ToString())) / (Double.Parse(item["CostSums"].ToString())) > 1)
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.Gainsboro;
            }
            //支出比例计算，应付金额优先
            else if (Double.Parse(item["CostMoneySUM"].ToString()) > 0)
            {
                //e.Row.Cells[12].Text = String.Format("{0:###,###,##0.00}", ((Double.Parse(e.Row.Cells[13].Text) - Double.Parse(e.Row.Cells[16].Text))));//应付余额
                e.Row.Cells[16].Text = String.Format("{0:#0.00%}", (Double.Parse(item["CostSums"].ToString())) / (Double.Parse(item["CostMoneySUM"].ToString())));
                if ((Double.Parse(item["CostSums"].ToString())) / (Double.Parse(item["CostMoneySUM"].ToString())) > 1)
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.Red;
            }
            //结算金额次之
            else if (Double.Parse(item["结算金额"].ToString()) > 0)
            {
                e.Row.Cells[16].Text = String.Format("{0:#0.00%}", (Double.Parse(item["CostSums"].ToString())) / (Double.Parse(item["结算金额"].ToString())));
                //e.Row.Cells[12].Text = "-无余额-";
                if ((Double.Parse(item["CostSums"].ToString())) / (Double.Parse(item["结算金额"].ToString())) > 1)
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.Red;

            }
            //如果结算金额也没有，就按照合同金额来计算

            else if (e.Row.Cells[8].Text != "&nbsp;")
            {
                if (Double.Parse(e.Row.Cells[8].Text) > 0)
                    e.Row.Cells[16].Text = String.Format("{0:#0.00%}", ((Double.Parse(item["CostSums"].ToString()) / Double.Parse(e.Row.Cells[8].Text))));
                //e.Row.Cells[12].Text = "-无余额-";
                if ((Double.Parse(item["CostSums"].ToString())) / (Double.Parse(e.Row.Cells[8].Text)) > 1)
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.Red;
            }
            //如果合同金额也没有，就按照项目金额来计算

            else if (e.Row.Cells[7].Text != "&nbsp;")
            {
                if (Double.Parse(e.Row.Cells[7].Text) > 0)
                    e.Row.Cells[16].Text = String.Format("{0:#0.00%}", ((Double.Parse(item["CostSums"].ToString()) / Double.Parse(e.Row.Cells[7].Text))));
                //e.Row.Cells[12].Text = "-无余额-";
                if ((Double.Parse(item["CostSums"].ToString())) / (Double.Parse(e.Row.Cells[7].Text)) > 1)
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.Red;
            }
            if (!xmbh.StartsWith("C"))
            {
                var xmjf = PublicMethod.GetDecimal(item["XMJF"].ToString());
                var htje = PublicMethod.GetDecimal(item["HTJE"].ToString());
                if (xmjf <= 0)
                {
                    var xm = new ZWL.BLL.ERPXMJBXX();
                    xm = xm.GetModelByXMBH(xmbh);
                    e.Row.Cells[7].Text = PublicMethod.FormatMoney(xm.XMJF);
                }
                if (htje <= 0 && !htbh.IsNullOrEmpty())
                {
                    var ht = new ZWL.BLL.ERPHeTong();
                    ht = ht.GetModelByNo(htbh);
                    e.Row.Cells[8].Text = PublicMethod.FormatMoney(ht.HTJE);
                }
            }
            if (linkJieSuan != null)
            {
                if (!htbh.IsNullOrEmpty())
                {
                    var htnworkid = DbHelperSQL.GetSHSLInt1("select top 1 NWorkToDoID from ERPHeTong where HTID='{0}'".FormatWith(htbh));
                    linkJieSuan.Attributes.Add("onclick", "javarscript:addTab_parent('{0}','{1}');".FormatWith("../BusinessManage/HTJieSuanList.aspx?ID={0}".FormatWith(htnworkid), "合同结算信息({0})".FormatWith(htbh)));
                }
                else
                {
                    linkJieSuan.Attributes.Add("onclick", "javarscript:alert('{0}');".FormatWith("该项目未关联或尚未签订合同，无相关信息。"));
                }
            }
            if (!xmbh.IsNullOrEmpty() && ZXZJXMList.Contains(xmbh))
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Attributes.Add("title", "财政专项资金项目");
            }
            if (xmNameLink != null)
            {
                xmNameLink.ToolTip = "查看成本核算详细信息({0})".FormatWith(item["XMName"]);
                xmNameLink.Attributes.Add("onclick", "javarscript:addTab_parent('{0}','{1}');".FormatWith("../Financial/ProjectCostView.aspx?ID={0}&sum={1}".FormatWith(item["ID"], item["CostSums"]), "成本核算详细信息({0})".FormatWith(htbh)));
            }
            if (linkNo != null)
            {
                linkNo.ToolTip = "查看成本核算详细信息({0})".FormatWith(item["XMName"]);
                linkNo.Attributes.Add("onclick", "javarscript:addTab_parent('{0}','{1}');".FormatWith("../Financial/ProjectCostView.aspx?ID={0}&sum={1}".FormatWith(item["ID"], item["CostSums"]), "成本核算详细信息({0})".FormatWith(htbh)));
            }
            if (xmbhLink != null)
            {
                xmbhLink.Text = xmbh;
                xmbhLink.Attributes.Add("onclick", "javarscript:addTab_parent('{0}','{1}');".FormatWith("../ProjectManage/ProjectFrame.aspx?type=XMCB&XMID={0}".FormatWith(xmbh), "项目详细信息({0})".FormatWith(htbh)));
            }
            if (htbhLink != null)
            {
                var msg = alertmsg;
                if (!htbh.IsNullOrEmpty())
                    msg = "javarscript:addTab_parent('{0}','{1}');".FormatWith("../BusinessManage/HeTongFrame.aspx?type=XMCB&HTID={0}".FormatWith(htbh), "合同详细信息({0})".FormatWith(htbh));
                htbhLink.Attributes.Add("onclick", msg);
            }
            if (checkSelect != null)
            {
                checkSelect.Attributes.Add("data-id", item["ID"].ToString());
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            GVData.Columns[15].Visible = false;
            GVData.Columns[17].Visible = false;
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
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
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string IDlist = PublicMethod.CheckCbxL(this.GVData, "CheckSelect", "LabVisible");

        DataSet ds = new DataSet();
        var dmodel = Pojo.GetModelList<ZWL.BLL.ERPCostDetail>("select cd.* from ERPProjectCost pc,ERPCostDetail cd where pc.ID=cd.ParentId and pc.id in (" + IDlist + ")");
        if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete cd from ERPProjectCost pc,ERPCostDetail cd where pc.ID=cd.ParentId and pc.id in (" + IDlist + ")") == -1)
        {
            ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
        }
        else
        {
            if (ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPProjectCost where ID in (" + IDlist + ")") == -1)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dmodel[i].Add();
                }
                ZWL.Common.MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
            }
            else
            {
                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除请审批工作管理信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
        }
    }
    /// <summary>
    /// 根据界面上的条件导出财务数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string strwhere = "";
        strwhere = GetQueryString();
        ZWL.BLL.ERPProjectCost MyModel = new ZWL.BLL.ERPProjectCost();
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        string strUserName = PublicMethod.GetSessionValue("UserName");
        if (strbumen == "中心领导" || strJiaoSe.Contains("超级管理员") || strbumen == "财务科" || strUserName == "罗鑫")
        {
            this.ImageButton3.Visible = true;//显示删除按钮
            //显示所有记录，按照填表日期排序，最新的排在前面
            //if (strwhere != "")
            //{
            //    strwhere += " and XMBH like '%%' ";
            //}
            //else
            //{
            //    strwhere = " XMBH like '%%' ";
            //}
        }
        else if (strJiaoSe.Contains("部门负责人"))
        {
            if (strwhere != "")
            {
                strwhere += " and XMBM='" + strbumen + "' ";
            }
            else
            {
                strwhere = " XMBM='" + strbumen + "' ";
            }

        }
        else
        {
            if (strwhere != "")
            {
                strwhere += " and XMFZR='" + PublicMethod.GetSessionValue("UserName") + "' ";
            }
            else
            {
                strwhere = " XMFZR='" + PublicMethod.GetSessionValue("UserName") + "' ";
            }
        }
        Hashtable HasColList = new Hashtable();
        HasColList.Add("ID", "编号");
        HasColList.Add("XMName", "项目名称");
        HasColList.Add("XMBH", "项目编号");
        HasColList.Add("DJTime", "登记时间");
        HasColList.Add("HTBH", "合同编号");
        HasColList.Add("XMState", "项目状态");
        HasColList.Add("ZYLB", "专业类别");
        HasColList.Add("XMBM", "承接部门");
        HasColList.Add("XMFZR", "项目负责人");
        HasColList.Add("HTJE", "项目金额");
        HasColList.Add("XMBeginTime", "开始时间");
        HasColList.Add("XMEndTime", "结束时间");
        HasColList.Add("结算金额", "结算金额");
        HasColList.Add("CostSums", "支出合计");
        HasColList.Add("收款金额", "收款金额");
        HasColList.Add("开票金额", "开票金额");
        HasColList.Add("HTJE2", "合同金额");
        HasColList.Add("BudgetSum", "项目预算");
        //HasColList.Add("Comment", "备注");
        //HasColList.Add("Comment", "备注");
        //HasColList.Add("Comment", "备注");
        var strsql = MyModel.GetListAndPagingFinancial(strwhere);
        DataSet ds = new DataSet();
        ds = ZWL.DBUtility.DbHelperSQL.Query(strsql);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                var sqlwhere = @"select t.*,h.XMID from (						
						select HTBH,sum(DaoZhangJE) s,sum(KaiPiaoJE) k
															from ERPHeTongDaoZhang h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh
															) t LEFT JOIN ERPHeTong h on t.HTBH=h.HTID
															where t.HTBH in (
															select HTID from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过')
															and XMID in (
															
															select XMBH from (
															{0}
															
																	) t GROUP BY XMBH
																)
															)".FormatWith(strsql);
                var XMAmtdt = DbHelperSQL.GetDataTable(sqlwhere);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var item = ds.Tables[0].Rows[i];
                        var xmbh = item["XMBH"].ToString();
                        var htbh = item["HTBH"].ToString();
                        if (htbh.IsNullOrEmpty() && XMAmtdt != null && XMAmtdt.Rows.Count > 0)
                        {
                            var flist = XMAmtdt.AsEnumerable().Where(r => r.Field<string>("XMID") == xmbh);
                            if (flist != null && flist.Any())
                            {
                                item["开票金额"] = flist.Sum(r => r.Field<decimal>("k"));
                                item["收款金额"] = flist.Sum(r => r.Field<decimal>("s"));
                            }
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                }
            }

            string datestring = DateTime.Now.ToString("yyyyMMddHHmmsss");
            string destfile = System.Web.HttpContext.Current.Server.MapPath("../") + "ReportFile\\" + datestring + ".xls";
            DataToExcel.DSToExcel(ds, destfile, HasColList);
            if (destfile != "")
            {
                string strFilePath = destfile;
                if (System.IO.File.Exists(strFilePath))
                {
                    //PublicMethod.WirteLog("已经生成了统计结果文件开始导出"+strFilePath);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.AddHeader("Content-Disposition", "inline;filename=" + Server.UrlEncode(datestring + ".xls"));
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
    //修改
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("ProjectCostModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    /// <summary>
    /// 返回季度日期所属二维数组
    /// </summary>
    public static string[,] retseasondata()
    {
        int hyear = DateTime.Now.Year;
        int hmon = DateTime.Now.Month;
        string[,] array2d = new string[4, 2];
        array2d[0, 0] = hyear - 1 + "-10-1";
        array2d[0, 1] = hyear - 1 + "-12-31";

        array2d[1, 0] = hyear + "-1-1";
        array2d[1, 1] = hyear + "-3-31";

        array2d[2, 0] = hyear + "-4-1";
        array2d[2, 1] = hyear + "-6-30";

        array2d[3, 0] = hyear + "-7-1";
        array2d[3, 1] = hyear + "-9-30";

        return array2d;

    }
    /// <summary>
    /// 根据当前月份获取上一个季度
    /// </summary>
    public static int retseasonnum(int mon)
    {
        int num = 0;
        if (mon >= 1 && mon <= 3)
        {
            num = 0;
        }
        else if (mon >= 4 && mon <= 6)
        {
            num = 1;
        }
        else if (mon >= 7 && mon <= 9)
        {
            num = 2;
        }
        else if (mon >= 10 && mon <= 12)
        {
            num = 3;
        }
        return num;
    }
    /// <summary>
    /// 根据界面上控件中的值组合查询语句
    /// </summary>
    /// <returns></returns>
    public string GetQueryString()
    {
        string strwhere = "";
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        string strUserName = PublicMethod.GetSessionValue("UserName");
        if (strbumen == "中心领导" || strJiaoSe.Contains("超级管理员") || PublicMethod.CheckPower(QuanxianValue + "W") || strbumen == "财务科" || new List<string> { "黄政涛", "何广", "罗鑫" }.Contains(strUserName) || strJiaoSe.Contains("监察科"))
        {
            this.ImageButton3.Visible = true;//显示删除按钮
            //显示所有记录，按照填表日期排序，最新的排在前面
            if (strwhere != "")
            {
                strwhere += " and pc.XMBH like '%%' ";
            }
            else
            {
                strwhere = " pc.XMBH like '%%' ";
            }
        }
        else if (strJiaoSe.Contains("部门负责人") || PublicMethod.CheckPower(QuanxianValue + "V"))
        {
            var bmname = "pc.XMBM";
            var bumensql = "='" + strbumen + "'";
            var sqlWhere = CombineDataLimitExtendSqlWhere(bmname, bumensql);
            strwhere += PublicMethod.GetSqlKeywordAnd(strwhere) + sqlWhere;
        }
        else
        {
            var sqlWhere = CombineDataLimitExtendSqlWhere("pc.XMFZR", "pc.XMBM", "='" + UserName + "'");
            strwhere += PublicMethod.GetSqlKeywordAnd(strwhere) + sqlWhere;
        }
        if (this.TextBox_xmname.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " pc.XMName like '%" + this.TextBox_xmname.Text + "%'";
        }
        if (DropDownList_cjbm.SelectedItem.Text != "全部")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " pc.XMBM like '%" + DropDownList_cjbm.SelectedItem.Text + "%'";
        }
        if (DropDownList_xmsubjecttype.SelectedItem.Text != "全部")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " pc.ZYLB = '" + DropDownList_xmsubjecttype.SelectedItem.Text + "'";
        }
        if (this.txtXMBH.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " " + DropDownListBH.SelectedValue + " like '%" + this.txtXMBH.Text + "%' ";
        }
        if (this.txtXMBH.Text != "" || this.TextBox_xmname.Text != "")
        {
            //按照编号和名称查的时候不考虑时间
        }
        else if (TextBox_Start.Text != "" && TextBox_End.Text != "")
        {
            if (strwhere != "")
            {
                strwhere += " and ";
            }
            strwhere += " pc.DJTime >= '" + TextBox_Start.Text + "' and pc.DJTime < '" + TimeParser.GetFormatDateString(DateTime.Parse(TextBox_End.Text).AddDays(1)) + "'";
        }

        return strwhere;
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
    /// <summary>
    /// 该项目成本预算的添加、修改和删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnYS_Click(object sender, EventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("BudgetDetailInfo.aspx?ID=" + CheckStrArray[0].ToString());
    }

    public void InitSuperQueryControl()
    {
        //部门
        string strsql = "SELECT distinct XMBM FROM ERPXMJBXX";
        DataTable cjbmtable = ZWL.DBUtility.DbHelperSQL.GetDataTable(strsql);
        this.DropDownList_cjbm.DataSource = cjbmtable.DefaultView;
        this.DropDownList_cjbm.DataValueField = cjbmtable.Columns[0].ColumnName;
        this.DropDownList_cjbm.DataTextField = cjbmtable.Columns[0].ColumnName;
        this.DropDownList_cjbm.DataBind();
        this.DropDownList_cjbm.Items.Add("全部");
        this.DropDownList_cjbm.Text = "全部";
        //专业类别
        strsql = "SELECT distinct ZYLB FROM ERPProjectCost";
        DataTable subjecttable1 = ZWL.DBUtility.DbHelperSQL.GetDataTable(strsql);
        this.DropDownList_xmsubjecttype.DataSource = subjecttable1.DefaultView;
        this.DropDownList_xmsubjecttype.DataTextField = subjecttable1.Columns[0].ColumnName;
        this.DropDownList_xmsubjecttype.DataValueField = subjecttable1.Columns[0].ColumnName;
        this.DropDownList_xmsubjecttype.DataBind();
        this.DropDownList_xmsubjecttype.Items.Add("全部");
        this.DropDownList_xmsubjecttype.Text = "全部";
    }
    protected void DropDownList_cjbm_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    protected void DropDownList_xmsubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    /// <summary>
    /// 导入成本支出明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CostDetailin_Click(object sender, EventArgs e)
    {
        if (this.FileUpload5.FileName != "" && this.FileUpload5.FileName.IndexOf(".xls") > 0)
        {
            //读取EXCEL文件
            Excel.Application excelApp;
            Excel._Workbook workBook;
            Excel._Worksheet worksheet;
            excelApp = new Excel.Application();
            string FileNameStr = PublicMethod.UploadFileIntoDir1(this.FileUpload5, System.IO.Path.GetFileNameWithoutExtension(FileUpload5.PostedFile.FileName) + DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName));
            string strexcelfilepath = Server.MapPath("~/UploadFile/" + FileNameStr);
            if (System.IO.File.Exists(strexcelfilepath))
            {
                workBook = excelApp.Workbooks.Add(strexcelfilepath);
                worksheet = (Excel.Worksheet)workBook.Worksheets[1];
                //worksheet.get_Range(excelApp.Cells[1, 1], excelApp.Cells[50, 50]).Select();
                int colnum = worksheet.UsedRange.Columns.Count;
                int rownum = worksheet.UsedRange.Rows.Count;
                Hashtable hashrow = new Hashtable();
                Excel.Range ranger = worksheet.UsedRange.get_Range("B1", "F1");
                Excel.Range ranger2 = worksheet.UsedRange.get_Range("C4", "F4");
                object[,] itemr = (object[,])ranger.Value2;
                string type = itemr[1, 1].ToString();
                object[,] itemr2 = (object[,])ranger2.Value2;
                string type2 = itemr2[1, 1] == null ? "" : itemr2[1, 1].ToString();
                type = type.Replace("费用成本分配统计表", "");

                int naddnum = 0;
                for (int i = 6; i <= rownum; i++)
                {
                    ZWL.BLL.ERPUser user = new ZWL.BLL.ERPUser();
                    if (worksheet.get_Range(worksheet.Cells[i, 3], worksheet.Cells[i, 3]).Value2 != null)
                    {
                        if (worksheet.get_Range(worksheet.Cells[i, 3], worksheet.Cells[i, 3]).Value2.ToString() != "部门负责人：" && worksheet.get_Range(worksheet.Cells[i, 3], worksheet.Cells[i, 3]).Value2.ToString() != "(1)")
                        {
                            string xmbh = worksheet.get_Range(worksheet.Cells[i, 3], worksheet.Cells[i, 3]).Value2.ToString();
                            string cost = worksheet.get_Range(worksheet.Cells[i, 6], worksheet.Cells[i, 6]).Value2.ToString();

                            ZWL.DBUtility.DbHelperSQL.ExecuteSQL("INSERT INTO ERPCostDetail (XMBH,beiyong1,beiyong2," + type + ") VALUES('" + xmbh + "','" + type2 + "','" + DateTime.Now.ToShortDateString() + "','" + cost + "')");
                            naddnum++;
                        }
                    }
                }

                //关闭文档对象
                workBook.Close(false, strexcelfilepath, false);
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

                string strmessage = "成功导入" + (naddnum) + "条合同数据！";
                MessageBox.Show(this, strmessage);
                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = strmessage;
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();


            }

        }
    }

    /// <summary>
    /// 按查询条件导入项目成本数据
    /// </summary>
    protected void btnProjectcostin_Click(object sender, EventArgs e)
    {

        string strwhere = "";
        strwhere = GetQueryString();
        strwhere = strwhere.Replace("pc.XMBH", "XMBH");
        strwhere = strwhere.Replace("pc.HTBH", "HTBH");
        ZWL.BLL.ERPXMJBXX[] xmxx = null;
        ZWL.BLL.ERPHeTong ht = new ERPHeTong();
        if (strwhere == "")
        {
            xmxx = Pojo.GetModelList<ZWL.BLL.ERPXMJBXX>("select ID from ERPXMJBXX");
        }
        else
        {
            var tsqlwhere = strwhere.Replace("pc.", "");
            xmxx = Pojo.GetModelList<ZWL.BLL.ERPXMJBXX>("select ID from ERPXMJBXX where " + tsqlwhere);
        }
        if (xmxx == null)
            return;
        for (int i = 0; i < xmxx.Length; i++)
        {
            var ds = ht.GetList(" [XMID]='" + xmxx[i].XMBH + "' and HTLB='收款' ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ZWL.BLL.ERPProjectCost pc = new ZWL.BLL.ERPProjectCost();
                    if (DbHelperSQL.GetSHSLInt1("select count(1) from ERPProjectCost where [XMBH]='" + xmxx[i].XMBH + "' and [HTBH]='" + dr["HTID"].ToString() + "'") > 0)
                    {
                        continue;
                    }
                    pc.XMBH = xmxx[i].XMBH;
                    //pc.HTBH = xmxx[i].HTBH;
                    pc.HTBH = dr["HTID"].ToString();
                    pc.XMName = xmxx[i].XMName;
                    pc.XMFZR = xmxx[i].XMFZR;
                    pc.XMBM = xmxx[i].XMBM;
                    pc.XMState = xmxx[i].XMState;
                    pc.ZYLB = xmxx[i].ZYLB;
                    pc.DJTime = xmxx[i].DJTime;
                    pc.XMBeginTime = xmxx[i].XMBeginTime;
                    pc.XMEndTime = xmxx[i].XMEndTime;
                    //pc.HTJE = decimal.Parse(xmxx[i].XMJF.ToString());
                    pc.HTJE = decimal.Parse(dr["HTJE"].ToString());
                    var list = pc.GetList("XMBH='" + pc.XMBH + "' and HTBH='" + pc.HTBH + "'");
                    if (list == null || list.Tables.Count == 0 || list.Tables[0].Rows.Count <= 0)
                    {
                        pc.Add();
                    }
                }
            }
            else
            {
                ZWL.BLL.ERPProjectCost pc = new ZWL.BLL.ERPProjectCost();
                pc.XMBH = xmxx[i].XMBH;
                pc.HTBH = "";
                var ds2 = ht.GetList(" [HTID]='" + xmxx[i].HTBH + "' and HTLB='收款' ");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    pc.HTBH = xmxx[i].HTBH;
                }
                pc.XMName = xmxx[i].XMName;
                pc.XMFZR = xmxx[i].XMFZR;
                pc.XMBM = xmxx[i].XMBM;
                pc.XMState = xmxx[i].XMState;
                pc.ZYLB = xmxx[i].ZYLB;
                pc.DJTime = xmxx[i].DJTime;
                pc.XMBeginTime = xmxx[i].XMBeginTime;
                pc.XMEndTime = xmxx[i].XMEndTime;
                pc.HTJE = decimal.Parse(xmxx[i].XMJF.ToString());
                var list = pc.GetList("XMBH='" + pc.XMBH + "' and HTBH='" + pc.HTBH + "'");
                if (list == null || list.Tables.Count == 0 || list.Tables[0].Rows.Count <= 0)
                {
                    pc.Add();
                }
            }
        }
        txtXMBH.Text = "";
        MessageBox.Show(this, "导入项目成本管理项目信息成功。");
        DataBindToGridview();
    }

    protected string getStringShort(object str, int length)
    {
        var result = str.ToString();
        return PublicMethod.ShortenText(result, length);
    }
    private static List<string> ZXZJXMList
    {
        get
        {
            var result = new List<string>();
            var kModel = new ZWL.BLL.ERPKeyValue();
            var list = kModel.GetModelList("Category='ZXZJXMList'");
            if (list != null && list.Any())
            {
                var item = list.FirstOrDefault();
                foreach (var item2 in item.Value1.Split(','))
                {
                    if (item2.IsNullOrEmpty()) continue;
                    result.Add(item2);
                }
            }
            return result;
        }
    }

    protected void GVData_PreRender(object sender, EventArgs e)
    {
        GVData.RowSpan(new { ColumnIndex = 3, ColumnControlID = "HyperLink_XMBH", Columns = "2,3" });
    }

    protected void btnLotFiled_Click(object sender, ImageClickEventArgs e)
    {
        CombinedProjectCost();
    }
    private void CombinedProjectCost()
    {
        var sqlWhere = @"select * from ERPProjectCost
                        where XMBH in (
                            select XMBH from ERPProjectCost
                            where XMBH in (
                            select XMBH from (
                            select XMBH,count(XMBH) c from ERPProjectCost GROUP BY XMBH
                            ) t where t.c=2
                            ) and (HTBH is null or HTBH='')
                        ) ";
        var list = Conv<ZWL.BLL.ERPProjectCost>.GetList(sqlWhere);
        if (list != null && list.Count > 0)
        {
            var glist = list.GroupBy(x => new { x.XMBH });
            foreach (var item in glist)
            {
                var fitem = item.FirstOrDefault(x => x.HTBH.IsNullOrEmpty());
                var sitem = item.LastOrDefault(x => !x.HTBH.IsNullOrEmpty());
                if (fitem != null && sitem != null)
                {
                    var budgets = Conv<ZWL.BLL.ERPBudgetDetail>.GetList("select * from ERPBudgetDetail where XMBH='{0}' and (HTBH is null or HTBH='')".FormatWith(item.Key.XMBH));
                    var costs = Conv<ZWL.BLL.ERPCostDetail>.GetList("select * from ERPCostDetail where XMBH='{0}' and (HTBH is null or HTBH='')".FormatWith(item.Key.XMBH));
                    foreach (var xitem in budgets)
                    {
                        var shots = EditShot(xitem);
                        xitem.HTBH = sitem.HTBH;
                        xitem.Update();
                        DbHelperSQL.ExecuteSql("update ERPBudgetDetail set HTBH='{0}' where ID={1}".FormatWith(sitem.HTBH, xitem.ID));
                        EditLog(shots, xitem);
                    }
                    foreach (var xitem in costs)
                    {
                        var shots = EditShot(xitem);
                        xitem.HTBH = sitem.HTBH;
                        xitem.Update();
                        DbHelperSQL.ExecuteSql("update ERPCostDetail set HTBH='{0}' where ID={1}".FormatWith(sitem.HTBH, xitem.ID));
                        EditLog(shots, xitem);
                    }
                    fitem.Delete(fitem.ID);
                    DelLog(fitem);
                }
            }
        }
    }
}