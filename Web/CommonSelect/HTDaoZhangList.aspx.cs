using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;
using System.Linq;

public partial class CommonSelect_HTDaoZhangList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var nworkid = PublicMethod.GetInto(PublicMethod.DecryptParam(Get("ID")));
            var info = new ZWL.BLL.ERPHeTong();
            info.GetModelByWorkId(nworkid);
            if (info.ID > 0)
            {
                var sql = @"select ROW_NUMBER() over(order by NWorkToDoID DESC) ID,* from (select NWorkToDoID,'开票' Type, a.HTBH,KaiPiaoJE,(select top 1 SQTime from ERPHeTongShouKuan b where b.NWorkToDoID=a.NWorkToDoID) SQTime,a.DaoZhangJE, a.DaoZhangTime from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH='{0}'
UNION
select 0 NWorkToDoID,'预收' Type,a.HTBH,0 KaiPiaoJE,ReceivedTime SQTime,a.Amount DaoZhangJE,ReceivedTime DaoZhangTime from ERPHeTongYuShouKuan a where Flag<>0 and ConnectID is null 
and NWorkID in (select NWorkToDoID	from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and 	HTID='{0}')) t ORDER BY NWorkToDoID DESC";
                var dt = DbHelperSQL.GetDataTable(sql.FormatWith(info.HTID));
                if (dt != null && dt.Rows.Count > 0)
                {
                    var rownum = 1;
                    var glist = dt.AsEnumerable().Where(r => r.Field<Int64>("ID") > 0).GroupBy(r => r.Field<int>("NWorkToDoID"));
                    for (int i = 0; i < glist.Count(); i++)
                    {
                        var gitem = glist.ElementAt(i);
                        for (int j = 0; j < gitem.Count(); j++)
                        {
                            var item = gitem.ElementAt(j);
                            item["ID"] = rownum;
                        }
                        rownum++;
                    }
                    var slist = dt.AsEnumerable().Where(r => r.Field<Int64>("ID") == 0);
                    if (slist != null && slist.Any())
                    {
                        for (int i = 0; i < slist.Count(); i++)
                        {
                            var item = slist.ElementAt(i);
                            item["ID"] = rownum;
                            rownum++;
                        }
                    }
                    GVData.DataSource = dt;
                    GVData.DataBind();
                }
            }
        }
    }

    protected void GVData_PreRender(object sender, EventArgs e)
    {
        for (int rowIndex = GVData.Rows.Count - 2; rowIndex >= 0; rowIndex += -1)
        {
            GridViewRow row = GVData.Rows[rowIndex];
            GridViewRow previousRow = GVData.Rows[rowIndex + 1];
            var data = (DataTable)GVData.DataSource;
            var item = data.Rows[row.DataItemIndex];
            var id = PublicMethod.GetInto(item["ID"]);
            var nworkid = PublicMethod.GetInto(item["NWorkToDoID"]);
            var pitem = data.Rows[previousRow.DataItemIndex];
            var pid = PublicMethod.GetInto(pitem["ID"]);
            var pnworkid = PublicMethod.GetInto(pitem["NWorkToDoID"]);
            if (nworkid == pnworkid)
            {
                row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                previousRow.Cells[0].Visible = false;
                row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 : previousRow.Cells[1].RowSpan + 1;
                previousRow.Cells[1].Visible = false;
                row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan < 2 ? 2 : previousRow.Cells[2].RowSpan + 1;
                previousRow.Cells[2].Visible = false;
                row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 : previousRow.Cells[3].RowSpan + 1;
                previousRow.Cells[3].Visible = false;
            }
        }
    }

    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var row = e.Row;
            var data = ((DataRowView)row.DataItem).DataView.Table;
            var item = ((DataRowView)row.DataItem).Row;
            var id = PublicMethod.GetInto(item["ID"]);
            var nworkid = PublicMethod.GetInto(item["NWorkToDoID"]);
            if (nworkid > 0 && row.Cells[2].Visible)
            {
                var list = data.AsEnumerable().Where(r => r.Field<int>("NWorkToDoID") == nworkid);
                var listx = list.Where(r => r.Field<Int64>("ID") != id);
                if (listx != null && listx.Any())
                {
                    row.Cells[2].RowSpan = list.Count();
                    for (int j = 0; j < listx.Count(); j++)
                    {
                        GVData.Rows[row.RowIndex + j + 1].Cells[2].Visible = false;
                    }
                }
            }
        }
    }
}