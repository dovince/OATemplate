using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class Aptitude_AptitudeManage : System.Web.UI.Page
{
    private DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
            DataBindToGridview();
            PublicMethod.BindDepartmentDDL(ddlDepartment, "select [Department] from [AptitudeFile] group by [Department]");
            //设定按钮权限
            //ImageButton2.Visible = PublicMethod.StrIFIn("|ZZSQ|", PublicMethod.GetSessionValue("QuanXian"));
            this.HiddenField_query.Value = "true";
        }
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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AptitudeFileAdd.aspx");
    }

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string CheckStr = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');

        var id = 0;
        int.TryParse(CheckStrArray[0], out id);
        Response.Redirect("AptitudeFileModify.aspx?ID=" + id);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        var IDlist = PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        var msg = "";
        if (ValidateDelete(IDlist, ref msg))
        {
            if (DeleteAptitude(IDlist))
            {
                DataBindToGridview();
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户删除资质使用申请审批表管理信息";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
            }
            else
            {
                MessageBox.Show(this, "删除选中记录时发生错误！请重新登陆后重试！");
            }
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool ValidateDelete(string IDlist, ref string msg)
    {
        var result = true;
        if (!string.IsNullOrEmpty(IDlist))
        {
            foreach (var item in IDlist.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;

                var id = 0;
                int.TryParse(item, out id);
                var m = new ZWL.BLL.AptitudeWork();
                m.GetModel(id);

                var toDo = new ZWL.BLL.ERPNWorkToDo();
                toDo.GetModel(int.Parse(m.NWorkID));

                //驳回的项目可以修改
                if (!toDo.StateNow.Equals("已被驳回"))
                {
                    var nodes = new ZWL.BLL.ERPNWorkFlowNode();
                    var ds = nodes.GetList(" workflowID=" + toDo.WorkFlowID);
                    var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPNWorkFlowNode>(ds.Tables[0]);
                    var secondNode = list.FirstOrDefault(r => r.NodeSerils == "2");
                    if (toDo.JieDianID.Value != secondNode.ID)
                    {
                        MessageBox.Show(this, "该申请单已经通过" + secondNode.NodeName + "审核,不能删除！");
                        return false;
                    }
                }
            }
        }

        return result;
    }

    private bool DeleteAptitude(string IDlist)
    {
        var result = true;
        if (!string.IsNullOrEmpty(IDlist))
        {
            foreach (var item in IDlist.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var m = new ZWL.BLL.AptitudeWork();
                m.GetModel(int.Parse(item));
                result = DbHelperSQL.ExecuteSQL("delete [AptitudeWork] where id = " + item) > 0;
                DbHelperSQL.ExecuteSQL("delete [AptitudeWorkDetail] where [AptWorkID] = " + item);
                DbHelperSQL.ExecuteSQL("delete [ERPNWorkToDo] where id = " + m.NWorkID);
            }
        }

        return result;
    }
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        DataBindToGridview();
    }
    #region  分页方法
    protected void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (GoPage.Text.Trim().ToString() == "")
            {
                ZWL.Common.MessageBox.Show(this, "页码不可以为空！");
            }
            else if (GoPage.Text.Trim().ToString() == "0" || ZWL.Common.PublicMethod.GetInt(GoPage.Text.Trim().ToString()) > ZWL.Common.PublicMethod.GetInt(LabPageSum.Text))
            {
                ZWL.Common.MessageBox.Show(this, "页码不是一个有效值！");
            }
            else if (GoPage.Text.Trim() != "")
            {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if (PageI >= 0 && PageI < (ZWL.Common.PublicMethod.GetInt(LabPageSum.Text)))
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
        var currentPage = int.Parse(LabCurrentPage.Text);
        switch (arg)
        {
            case ("Next"):
                if (currentPage < pageCount)
                    GVData.PageIndex = currentPage + 1;
                break;
            case ("Pre"):
                if (currentPage > 0)
                    GVData.PageIndex = currentPage - 1;
                break;
            case ("Last"):
                try
                {
                    GVData.PageIndex = pageCount;
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var lblId = ((Label)e.Row.FindControl("LabVisible")).Text;
                DataRow selectedItem = null;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var id = item["ID"].ToString();
                    if (id != lblId) continue;
                    else
                    {
                        selectedItem = item;
                        break;
                    }

                }
                if (selectedItem != null)
                {
                    var lblActive = ((Label)e.Row.FindControl("lblIsActive"));
                    var active = selectedItem["IsActive"].ToString();
                    if (active == "1")
                    {
                        lblActive.Text = "启用";
                        lblActive.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblActive.Text = "关闭";
                        lblActive.ForeColor = System.Drawing.Color.Red;
                    }
                        
                }
            }
        }

    }

    public void DataBindToGridview()
    {
        var MyModel = new ZWL.BLL.AptitudeFile();
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        ds = new DataSet();
        var currPage = GVData.PageIndex == 0 ? 1 : GVData.PageIndex;
        var pageSize = int.Parse(TxtPageSize.Text);
        Pager pager = null;
        var strwhere = "";
        strwhere = GetSqlWhere();

        pager = MyModel.GetPagingList(strwhere, currPage, pageSize);

        if (pager != null)
        {
            if (pager.ExecuteToDataSet())
            {
                ds = (DataSet)pager.Result;
            }
        }
        var pageSum = pager == null ? GVData.PageCount : pager.TotalPage;
        currPage = pager == null ? 0 : pager.CurrentPage;

        GVData.DataSource = ds;
        GVData.PageIndex = currPage;
        GVData.PageSize = pageSize;
        GVData.DataBind();
        LabPageSum.Text = pageSum.ToString();
        HdfPageSum.Value = pageSum.ToString();
        LabCurrentPage.Text = currPage.ToString();
        GoPage.Text = currPage.ToString();
    }

    private string GetSqlWhere()
    {
        var result = " 1 = 1 ";
        string strJiaoSe = PublicMethod.GetSessionValue("JiaoSe");
        string strbumen = PublicMethod.GetSessionValue("Department");
        var userName = PublicMethod.GetUserName();

        if (!string.IsNullOrEmpty(txtAptitudeName.Text.Trim()))
        {
            result += " and [AptitudeName] " + " like '%" + txtAptitudeName.Text + "%'";
        }

        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue) && ddlDepartment.SelectedValue != "全部")
        {
            result += " and [Department] " + " like '%" + ddlDepartment.SelectedValue + "%'";
        }
        if (!string.IsNullOrEmpty(ddlActive.SelectedValue) && ddlActive.SelectedValue != "全部")
        {
            result += " and [IsActive] " + " like '%" + ddlActive.SelectedValue + "%'";
        }
        return result;
    }
}