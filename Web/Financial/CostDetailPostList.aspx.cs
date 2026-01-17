using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Financial_CostDetailPostList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            DataBindToGridview();
            PublicMethod.BindDepartmentDDL(txtZYLB, "select State from ERPCostDetailPost group by State");
            //设定按钮权限
            ImageButton1.Visible = PublicMethod.StrIFIn("|CostDetailPostListA|", PublicMethod.GetSessionValue("QuanXian"));
            //ImageButton3.Visible = PublicMethod.StrIFIn("|ht006D|", PublicMethod.GetSessionValue("QuanXian"));
            ImageButton2.Visible = PublicMethod.StrIFIn("|CostDetailPostListE|", PublicMethod.GetSessionValue("QuanXian"));
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
            var id = PublicMethod.GetInto(item["ID"]);
            var btnPrint = (LinkButton)e.Row.FindControl("btnPrint");
            var linkLot = (HyperLink)e.Row.FindControl("linkLot");
            if (btnPrint != null)
            {
                btnPrint.Attributes.Add("href", "javascript:void(0);");
                btnPrint.Attributes.Add("onclick", "confirmPostPrint({0},'{1}');".FormatWith(item["ID"], (new List<string> { "已提交", "已完成" }.Contains(item["State"].ToString()) ? "" : "确认提交数据并打印吗？")));
            }
            if (linkLot != null)
            {
                linkLot.Attributes.Add("onclick", "addTab_parent('{0}','{1}');".FormatWith("../Financial/CostDetailPost.aspx?Action=View&ID=" + PublicMethod.EncryptParam(item["ID"]), "成本报销详细信息(" + item["LotNo"] + ")"));
            }
        }

    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
        DataBindToGridview();
    }
    protected override void DataBindToGridview()
    {
        var sqlWhere = GetSqlWhere();
        var MyModel = new ZWL.BLL.ERPCostDetailPost();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        var msql = @"select *,(select sum(SubmitAmt) from ERPCostDetailPostItems d where (DeleteMark is null or DeleteMark=0) and d.ParentId=p.ID ) TotalAmt 
                    ,(select STUFF((select DISTINCT ',' + Description from ERPCostDetailPostItems d where (DeleteMark is null or DeleteMark=0) and d.ParentId=p.ID  FOR XML PATH('')),1,1,'')) Description
                    from ERPCostDetailPost p where (DeleteMark is null or DeleteMark=0) ";
        if (!sqlWhere.IsNullOrEmpty())
            msql += PublicMethod.GetSqlKeywordAnd(sqlWhere) + sqlWhere;
        var pager = new Pager(msql, currPage, pageSize, "ID desc");
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
    private string GetSqlWhere()
    {
        var sqlWhere = string.Empty;
        ///////////////////补写查询条件//////////////////////
        var htbh = txtHTBH.Text;
        if (!string.IsNullOrEmpty(htbh))
        {
            var sql = string.Format(@" exists (
                                            select * from ERPCostDetailPostItems d join ERPProjectCost c on d.RecordId=c.ID 
                                            where d.ParentId=p.ID and (HTBH like '%{0}%' or XMBH like '%{0}%'))", htbh);
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + sql;
        }
        var htname = txtHTName.Text;
        if (!string.IsNullOrEmpty(htname))
        {
            var sql = string.Format(@" exists (
                                            select * from ERPCostDetailPostItems d join ERPProjectCost c on d.RecordId=c.ID 
                                            where d.ParentId=p.ID and XMName like '%{0}%')", htname);
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + sql;
        }
        var jyly = txtZYLB.Text;
        if (!string.IsNullOrEmpty(jyly) && jyly != "全部")
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " State like '%" + jyly + "%'";
        }
        var jyr = txtUsername.Text;
        if (!string.IsNullOrEmpty(jyr))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJR like '%" + jyr + "%'";
        }
        var jybm = txtDept.Text;
        if (!string.IsNullOrEmpty(jybm))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJBM like '%" + jybm + "%'";
        }
        var datestart = txtDateStart.Text;
        if (!string.IsNullOrEmpty(datestart))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJTime >='" + datestart + "'";
        }
        var dateend = txtDateEnd.Text;
        if (!string.IsNullOrEmpty(dateend))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJTime <='" + dateend + "'";
        }
        var limitSql = GetLimitDataSqlWhere(QuanxianValue);

        sqlWhere += PublicMethod.GetSqlAndByWhere(sqlWhere, limitSql) + limitSql;
        sqlWhere = sqlWhere.Replace("UserName in", "DJR in").Replace("UserName=", "DJR=");
        return sqlWhere;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CostDetailPost.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        var IDList = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        var dic = new Dictionary<string, string>();
        dic.Add("LotNo", "批号");
        dic.Add("TotalAmt", "支出合计");
        dic.Add("Description", "支出明细");
        var list = PublicMethod.GetBoundFieldAndHeaderTextListByGrid(GVData, dic);

        var sqlWhere = GetSqlWhere();
        if (!string.IsNullOrEmpty(IDList))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " ID in (" + IDList.TrimStart(',') + ")";
        }
        var MyModel = new ZWL.BLL.ERPCostDetailPost();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        var msql = @"select *,(select sum(SubmitAmt) from ERPCostDetailPostItems d where (DeleteMark is null or DeleteMark=0) and d.ParentId=p.ID ) TotalAmt 
                    ,(select STUFF((select DISTINCT ',' + Description from ERPCostDetailPostItems d where (DeleteMark is null or DeleteMark=0) and d.ParentId=p.ID  FOR XML PATH('')),1,1,'')) Description
                    from ERPCostDetailPost p where (DeleteMark is null or DeleteMark=0) ";
        if (!sqlWhere.IsNullOrEmpty())
            msql += PublicMethod.GetSqlKeywordAnd(sqlWhere) + sqlWhere;

        var pager = new Pager(msql, currPage, pageSize, "ID desc");

        DataToExcel.GridViewToExcelOrderByNameList(this, DbHelperSQL.GetDataSet(pager.SQL), list, "成本报销Excel报表");

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        var msg = "";
        if (ValidateDelete(ref msg))
        {
            var IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
            foreach (var item in IDlist.Split(','))
            {
                var id = PublicMethod.GetInt(item);
                var jModel = new ZWL.BLL.ERPHeTongJieYue();
                jModel = jModel.GetModelByWorkId(id);
                if (jModel != null)
                {
                    var dModel = jModel.CurrentWorkToDo;
                    var tModels = jModel.JieYueItems;
                    foreach (var titem in tModels)
                    {
                        var hModel = titem.CurrentHeTong;
                        hModel.HTJYState = "可借阅";
                        hModel.Update();
                        titem.Delete(titem.ID);
                    }
                    dModel.Delete(dModel.ID);
                    jModel.Delete(jModel.ID);
                }
            }
            WriteLog("用户删除合借阅工作管理信息[" + IDlist + "]");
            DataBindToGridview();
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool ValidateDelete(ref string msg)
    {
        var result = true;
        var IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if (string.IsNullOrEmpty(IDlist))
        {
            msg = "请选择需要删除的借阅流程。";
            return false;
        }
        foreach (var item in IDlist.Split(','))
        {
            if (!CanDeleteModel(PublicMethod.GetInt(item), ref msg))
            {
                return false;
            }
        }
        return result;
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
        if (commandName == "modify")
        {
            var result = CanModify(id);
            if (!result)
            {
                MessageBox.ShowAndReload(this, "此报销单已提交无法修改，请联系经营科或系统管理员！");
            }
            else
            {
                string strURL = string.Format("CostDetailPost.aspx?Action=Edit&ID={0}", id);
                Response.Redirect(strURL);
            }
        }
        else if (commandName == "del")
        {
            var result = CanDelete(id);
            if (!result)
            {
                MessageBox.ShowAndReload(this, "申请已提交无法修改，请联系经营科或系统管理员！");
            }
            else
            {
                var info = new ZWL.BLL.ERPCostDetailPost();
                info.GetModel(PublicMethod.GetInt(id));
                if (info.ID > 0)
                {
                    var list = info.SubItems;
                    foreach (var titem in list)
                    {
                        var shots = EditShot(titem);
                        titem.DeleteMark = 1;
                        titem.DeleteTime = Timestamp;
                        titem.DeleteUser = UserName;
                        titem.Update();
                        EditLog(shots, titem);
                    }
                    var ishots = EditShot(info);
                    info.DeleteMark = 1;
                    info.DeleteTime = Timestamp;
                    info.DeleteUser = UserName;
                    info.Update();
                    EditLog(ishots, info);
                }
                MessageBox.ShowAndReload(this, "删除成功！");
                WriteLog("用户删除(CostDetailPostList)信息(" + id + ")");
            }
        }
        else if (commandName == "revert")
        {
            var result = CanRevert(id);
            if (!result)
            {
                MessageBox.ShowAndReload(this, "此报销单未提交无需撤回，请确认提交！");
            }
            var info = new ZWL.BLL.ERPCostDetailPost();
            info.GetModel(PublicMethod.GetInt(id));
            if (info.ID > 0)
            {
                var list = info.SubItems;
                foreach (var item in list)
                {
                    if (!item.RelativeId.HasValue) continue;
                    var sInfo = new ZWL.BLL.ERPCostDetail();
                    sInfo.GetModel(item.RelativeId.Value);
                    if (sInfo.ID > 0)
                    {
                        sInfo.Delete(sInfo.ID);
                        DelLog(sInfo);
                        var rshots = EditShot(item);
                        item.RelativeId = null;
                        item.Update();
                        EditLog(rshots, item);

                    }
                }
                var ishots = EditShot(info);
                info.State = "已撤回";
                info.Update();
                EditLog(ishots, info);
                MessageBox.ShowAndReload(this, "操作成功！");
            }
        }
        else if (commandName == "submit")
        {
            var result = CanSubmit(id);
            if (!result)
            {
                MessageBox.ShowAndReload(this, "此报销单已提交，无需重复！");
            }
            result = SubmitData(PublicMethod.GetInt(id));
            MessageBox.ShowAndReload(this, "操作成功！");
        }
        else if (commandName == "sign")
        {
            var result = CanSign(id);
            if (!result)
            {
                MessageBox.ShowAndReload(this, "此报销单已提交，无需重复！");
            }
            result = SignData(PublicMethod.GetInt(id));
            MessageBox.ShowAndReload(this, "操作成功！");
        }
        else if (commandName == "print")
        {
            #region print
            /*
                var result = CanPrint(id);
                if (!result)
                {
                    MessageBox.ShowAndReload(this, "此报销单未达打印要求，请确认！");
                }
                var info = new ZWL.BLL.ERPCostDetailPost();
                info.GetModel(PublicMethod.GetInt(id));
                if (info.ID > 0)
                {
                    if (info.State != "已提交")
                    {
                        SubmitData(PublicMethod.GetInt(id));
                    }
                    info.GetModel(PublicMethod.GetInt(id));
                    var tfilename = "ProjectCostDetailSubmitReport_{0}.html".FormatWith(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    var tempfilepath = Path.Combine(Server.MapPath("~/ReportFile"), "项目费用成本报销统计表.doc");
                    var tagetfilepath = Path.Combine(Path.Combine(Server.MapPath("~/UploadFile"), "DocumentPreview"), tfilename);

                    var dt = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPost>(new List<ZWL.BLL.ERPCostDetailPost> { info });
                    var dic = DataTableToDicList(dt).FirstOrDefault();
                    var selecteddic = dic.FirstOrDefault(r => r.Key == "DJR");
                    dic.Remove(selecteddic.Key);
                    var aspose = new AsposeWordHelper();
                    aspose.OpenTempelte(tempfilepath);
                    aspose.ExecuteField(dic.Keys.ToArray(), dic.Values.ToArray());
                    var yinfo = new ZWL.BLL.ERPYinZhang();
                    var ylist = yinfo.GetListModel("YinZhangLeiBie='私人印章' and UserName='{0}'".FormatWith(selecteddic.Value));
                    var val = string.Empty;
                    if (ylist != null && ylist.Any())
                    {
                        val = BitmapImageToByteArray(Path.Combine(Server.MapPath("~/UploadFile"), ylist.FirstOrDefault().ImgPath));
                    }
                    aspose.AddImageByStream(selecteddic.Key, new MemoryStream(Convert.FromBase64String(val)), 55, 30);

                    var dataTable = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(info.SubItems);
                    var itemsdt = dataTable.Clone();
                    var list = info.SubItems.GroupBy(r => r.RecordId);
                    foreach (var xitem in list)
                    {
                        for (int i = 0; i < xitem.Count(); i++)
                        {
                            var item = xitem.ElementAt(i);
                            var subdt = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(new List<ZWL.BLL.ERPCostDetailPostItems> { item });
                            if (i > 0)
                            {
                                var row = subdt.Rows[0];
                                row["XMName"] = DBNull.Value;
                                row["HTBH"] = DBNull.Value;
                                row["SettleAmt"] = DBNull.Value;
                                row["ReceivedAmt"] = DBNull.Value;
                                row["CostScale"] = DBNull.Value;
                            }
                            itemsdt.Rows.Add(subdt.Rows[0].ItemArray);
                        }
                    }
                    itemsdt.AcceptChanges();
                    itemsdt.TableName = "childrenDataTable2";
                    itemsdt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
                    ResetRowID(itemsdt);
                    aspose.WriteTable(itemsdt);
                    aspose.Save(tagetfilepath, Aspose.Words.SaveFormat.HtmlFixed);
                    aspose.Save(tagetfilepath.Substring(0, tagetfilepath.LastIndexOf('.') + 1) + Aspose.Words.SaveFormat.Pdf.ToString(), Aspose.Words.SaveFormat.Pdf);
                    var url = "{0}/CommonSelect/PrintHelper.aspx?filename={1}".FormatWith(BaseUrl, HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(tfilename)));
                    var dosomething = "addTab_parent('{0}','{1}');window.frameElement.src = window.frameElement.src;"
                        .FormatWith(url, PublicMethod.ShortenText(Path.GetFileNameWithoutExtension(tfilename), 10));
                    MessageBox.ResponseScript(this, dosomething);
                }
                */
            #endregion
        }
        else if (commandName == "detail")
        {
            var item = new ZWL.BLL.ERPCostDetailPost();
            item.GetModel(PublicMethod.GetInt(id));
            if (item.ID > 0)
            {
                var btn = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                // 获取子GridView
                GridView childGridView = (GridView)row.FindControl("GVDetail");
                UpdatePanel UpdatePanel1 = (UpdatePanel)row.FindControl("UpdatePanel1");

                // 根据需要从数据源获取子GridView的数据
                // DataTable childData = YourDataAccessMethod();

                // 绑定子GridView的数据
                var childData = item.SubItems;
                childGridView.DataSource = childData;
                childGridView.DataBind();

                // 根据实际情况更新UpdatePanel
                UpdatePanel1.Update();
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        var nworkid = PublicMethod.GetInt(((LinkButton)sender).CommandName);
        var hnworkid = PublicMethod.GetInt(((LinkButton)sender).CommandArgument);
        var sql = string.Format("select top 1 * from ERPHeTongJieYueDetail where HTID={0} and NWorkID={1}", hnworkid, nworkid);
        var selectedItem = Conv<ZWL.BLL.ERPHeTongJieYueDetail>.GetModel(sql);
        if (selectedItem != null)
        {
            var hModel = selectedItem.CurrentHeTong;
            hModel.HTJYState = "可借阅";
            hModel.Update();
            selectedItem.BackDate = DateTime.Now;
            selectedItem.BackConfirmUserID = UserID;
            selectedItem.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshTriggerRow('" + ((LinkButton)sender).ClientID + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "<script language='javascript' defer>alert('系统出错，请刷新重试！');</script>", true);
        }
    }

    protected void GVDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (ZWL.BLL.ERPCostDetailPostItems)e.Row.DataItem;
            var lblOrder = (Label)e.Row.FindControl("lblOrder");
            if (lblOrder != null)
                lblOrder.Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected bool CanModify(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0)
        {
            var list = new List<string> { "暂存", "已撤回" };
            if (PublicMethod.StrIFIn("|CostDetailPostListM|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool CanDelete(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0)
        {
            var list = new List<string> { "暂存", "已撤回" };
            if (PublicMethod.StrIFIn("|CostDetailPostListD|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool CanRevert(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0)
        {
            var list = new List<string> { "已提交", "已完成" };
            if (PublicMethod.StrIFIn("|CostDetailPostListR|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool CanSubmit(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0)
        {
            var list = new List<string> { "暂存", "已撤回" };
            if (PublicMethod.StrIFIn("|CostDetailPostListS|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool CanSign(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0 && info.State != "已完成")
        {
            var list = new List<string> { "已提交" };
            if (PublicMethod.StrIFIn("|CostDetailPostListC|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool CanPrint(object id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInto(id));
        if (info.ID > 0)
        {
            var list = new List<string> { "暂存", "已撤回", "已提交", "已完成" };
            if (PublicMethod.StrIFIn("|CostDetailPostListP|", QuanXian) && list.Contains(info.State))
            {
                result = true;
            }
        }
        return result;
    }
    protected bool SubmitData(int id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInt(id));
        if (info.ID > 0)
        {
            var list = info.SubItems;
            foreach (var item in list)
            {
                var pcost = new ZWL.BLL.ERPProjectCost();
                pcost.GetModel(item.RecordId);
                var maxqijian = 0;
                var dlist = new ZWL.BLL.ERPCostDetail().GetListModelByParentId(pcost.ID);
                if (dlist != null && dlist.Any())
                {
                    foreach (var ditem in dlist)
                    {
                        var tnum = PublicMethod.GetInto(ditem.期间);
                        maxqijian = tnum > maxqijian ? tnum : maxqijian;
                    }
                }
                var dInfo = new ZWL.BLL.ERPCostDetail()
                {
                    XMBH = pcost.XMBH,
                    HTBH = pcost.HTBH,
                    期间 = (maxqijian + 1).ToString(),
                    beiyong1 = item.Description,
                    beiyong2 = TimeParser.GetFormatTimeString(info.DJTime),
                };
                dInfo = (ZWL.BLL.ERPCostDetail)PublicMethod.SetModelPropertyValueByName(dInfo, item.Item, item.SubmitAmt);
                dInfo.ParentId = pcost.ID;
                dInfo.ID = dInfo.Add();
                AddLog(dInfo);
                var dshots = EditShot(item);
                item.RelativeId = dInfo.ID;
                item.Update();
                EditLog(dshots, item);
            }
            var ishots = EditShot(info);
            info.State = "已提交";
            result = info.Update();
            EditLog(ishots, info);
        }
        return result;
    }
    protected bool SignData(int id)
    {
        var result = false;
        var info = new ZWL.BLL.ERPCostDetailPost();
        info.GetModel(PublicMethod.GetInt(id));
        if (info.ID > 0)
        {
            var ishots = EditShot(info);
            info.State = "已完成";
            info.SignUser = UserName;
            info.SignTime = Timestamp;
            result = info.Update();
            EditLog(ishots, info);
        }
        return result;
    }

    /// <summary>
    /// DataTable转DicList.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private List<Dictionary<string, object>> DataTableToDicList(DataTable dt)
    {
        var result = new List<Dictionary<string, object>>();
        if (dt.AsEnumerable().Count() > 0)
            return dt.AsEnumerable().Select(
                    row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column])).ToList();
        else
        {
            var dic = new Dictionary<string, object>();
            foreach (DataColumn item in dt.Columns)
            {
                dic.Add(item.ColumnName, DBNull.Value);
            }
            result.Add(dic);
        }
        return result;
    }

    /// <summary>
    /// 动态表单时间格式转换.
    /// </summary>
    /// <param name="diclist"></param>
    /// <returns></returns>
    private List<Dictionary<string, object>> DateConver(List<Dictionary<string, object>> diclist)
    {
        foreach (var item in diclist)
        {
            foreach (var dic in item.Keys)
            {
                if (item[dic] is DateTime)
                {
                    item[dic] = item[dic].ToString() + " ";
                }
            }
        }

        return diclist;
    }
    /// <summary>
    /// 根据图片的路径解析成图片资源
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string BitmapImageToByteArray(string filePath)
    {
        var pic = "";
        try
        {
            byte[] byteArray = null;
            if (File.Exists(filePath))
                byteArray = File.ReadAllBytes(filePath);
            pic = Convert.ToBase64String(byteArray);
        }
        catch { }
        return pic;
    }
    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["RowNumber"] = rowNumber;
                rowNumber++;
            }
        }
    }
}
