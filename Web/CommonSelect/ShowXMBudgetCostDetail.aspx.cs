using System;
using System.Data;
using ZWL.Common;

public partial class CommonSelect_ShowXMBudgetCostDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindToGridview();
        }
    }
    protected override void DataBindToGridview()
    {
        var sqlWhere = GetSqlWhere();
        var ds = new DataSet();
        var currPage = GVData.PageIndex + 1;
        var pageSize = PublicMethod.GetInt(TxtPageSize.Text);
        var sql = MainSQL;
        if (!sqlWhere.IsNullOrEmpty())
            sql += PublicMethod.GetSqlKeywordAnd(sqlWhere) + sqlWhere;
        var pager = new Pager(sql, currPage, pageSize,"XMBH Desc");
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
    public string GetSqlWhere()
    {
        var sqlWhere = string.Empty;
        if (!string.IsNullOrEmpty(SearchKeyWord.Text))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + string.Format(" (c.HTBH like '%{0}%' or x.XMName like '%{0}%' or c.XMBH like '%{0}%')", SearchKeyWord.Text);
        }
        if (!string.IsNullOrEmpty(StartDate.Text))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJTime >= '" + DateTime.Parse(StartDate.Text).Date + "'";
        }

        if (!string.IsNullOrEmpty(EndDate.Text))
        {
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " DJTime < '" + DateTime.Parse(EndDate.Text).Date.AddDays(1) + "'";
        }
        var limitSql = GetLimitDataSqlWhere("X001");

        sqlWhere += PublicMethod.GetSqlAndByWhere(sqlWhere, limitSql) + limitSql;

        return sqlWhere;
    }

    protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        GVData.PageIndex = 0;
        DataBindToGridview();
    }
    private string MainSQL
    {
        get
        {
            return @"select c.XMBH,c.HTBH,t.*,(Budget-Cost) BalAmt,x.XMName,c.XMBM,UserName from 
            (

                                select COALESCE(a.ParentId,b.ParentId) ParentId,COALESCE(a.Item,b.Item) Item,ISNULL(a.Budget, 0) Budget,ISNULL(b.Cost, 0) Cost from (

                                select * from (
											            select * from ERPBudgetDetail where ID in(
											            SELECT max(ID) ID FROM ERPBudgetDetail GROUP BY ParentId
                                )) as sg  --源数据
                                unpivot(Budget for Item in (工资及津贴,工程出包费,材料费,租赁费,劳务费,安全生产费用,办公费,维修费用,交通运输费用,差旅费,邮电费用,其它费用,水电费,会议费,印刷费)) as cjd --转换后的结果数据
                                --where XMBH='X20230522' 
                                ) a
                                FULL JOIN
                                (

                                select ParentId,Item,sum(Cost) Cost
                                from (
                                select ParentId,Item,Cost from ERPCostDetail as sg --源数据
                                unpivot(Cost for Item in (工资及津贴,工程出包费,材料费,租赁费,劳务费,安全生产费用,办公费,维修费用,交通运输费用,差旅费,邮电费用,其它费用,水电费,会议费,印刷费)) as cjd --转换后的结果数据
                                --where XMBH='X20230522' 
                                ) t
                                GROUP BY ParentId,Item

                                ) b on a.ParentId=b.ParentId and a.Item=b.Item
										
										
										
            ) t 
										
            LEFT JOIN ERPProjectCost c on t.ParentId=c.ID 
            LEFT JOIN ERPXMJBXX x on c.XMBH=x.XMBH join ERPNWorkToDo d on x.NWorkID=d.ID
             where (Budget>0 or Cost>0) and Budget<>Cost";
        }
    }
}