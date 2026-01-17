using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace ZWL.Common
{
    /// <summary>
    /// ControlExtension 的摘要说明
    /// </summary>
    public class ControlExtension
    {
        public ControlExtension()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static string EmptyText = "没有记录";

        /// <summary>
        /// 防止PostBack后Gridview不能显示
        /// </summary>
        /// <param name="gridview"></param>
        private static void ResetGridView(GridView gridview)
        {
            //如果数据为空则重新构造Gridview
            if (gridview.Rows.Count == 1 && gridview.Rows[0].Cells[0].Text == EmptyText)
            {
                int columnCount = gridview.Columns.Count;
                InitGridView(gridview, columnCount);
            }
        }

        private static void InitGridView(GridView gridview, int columnCount)
        {
            gridview.Rows[0].Cells.Clear();
            gridview.Rows[0].Cells.Add(new TableCell());
            gridview.Rows[0].Cells[0].ColumnSpan = columnCount;
            gridview.Rows[0].Cells[0].Text = EmptyText;
            gridview.Rows[0].Cells[0].Style.Add("text-align", "center");
        }

        /// <summary>
        /// 绑定数据到GridView，当表格数据为空时显示表头
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="table"></param>
        public static void GridViewBind(GridView gridview, DataTable table)
        {
            //记录为空重新构造Gridview
            if (table.Rows.Count == 0)
            {
                table = table.Clone();
                table.Rows.Add(table.NewRow());
                gridview.DataSource = table;
                gridview.DataBind();
                int columnCount = gridview.Columns.Count;
                InitGridView(gridview, columnCount);
            }
            else
            {
                //数据不为空直接绑定
                gridview.DataSource = table;
                gridview.DataBind();
            }

            //重新绑定取消选择
            gridview.SelectedIndex = -1;
        }

        public static void GridViewBind<T>(GridView gridview, List<T> list)
        {
            //记录为空重新构造Gridview
            if (list.Count == 0)
            {
                DataTable table = new DataTable();
                DataRow dr = table.NewRow();
                Type type = typeof(T);
                foreach (PropertyInfo p in type.GetProperties())
                {

                    DataColumn dc = new DataColumn(p.Name);
                    table.Columns.Add(dc);
                }
                table.Rows.Add(table.NewRow());
                gridview.DataSource = table;
                gridview.DataBind();
                int columnCount = gridview.Columns.Count;
                InitGridView(gridview, columnCount);
            }
            else
            {
                //数据不为空直接绑定
                gridview.DataSource = list;
                gridview.DataBind();
            }

            //重新绑定取消选择
            gridview.SelectedIndex = -1;
        }

    }
}
