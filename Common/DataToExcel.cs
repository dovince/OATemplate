using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using System.Web.UI;

namespace ZWL.Common
{
    /// <summary>
    /// 操作EXCEL导出数据报表的类
    /// 李瑞宁
    /// 2014.4
    /// </summary>
    public class DataToExcel
    {
        public DataToExcel()
        {
        }

        #region 操作EXCEL的一个类(需要Excel.dll支持)

        private int titleColorindex = 15;
        /// <summary>
        /// 标题背景色
        /// </summary>
        public int TitleColorIndex
        {
            set { titleColorindex = value; }
            get { return titleColorindex; }
        }

        private DateTime beforeTime;			//Excel启动之前时间
        private DateTime afterTime;				//Excel启动之后时间

        #region 创建一个Excel示例
        /// <summary>
        /// 创建一个Excel示例
        /// </summary>
        public void CreateExcel()
        {
            Excel.Application excel = new Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Cells[1, 1] = "第1行第1列";
            excel.Cells[1, 2] = "第1行第2列";
            excel.Cells[2, 1] = "第2行第1列";
            excel.Cells[2, 2] = "第2行第2列";
            excel.Cells[3, 1] = "第3行第1列";
            excel.Cells[3, 2] = "第3行第2列";

            //保存
            excel.ActiveWorkbook.SaveAs("./tt.xls", XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            //打开显示
            excel.Visible = true;
            //			excel.Quit();
            //			excel=null;            
            //			GC.Collect();//垃圾回收
        }
        #endregion

        #region 将DataTable的数据导出显示为报表
        /// <summary>
        /// 将DataTable的数据导出显示为报表
        /// </summary>
        /// <param name="dt">要导出的数据</param>
        /// <param name="strTitle">导出报表的标题</param>
        /// <param name="FilePath">保存文件的路径</param>
        /// <returns></returns>
        public string OutputExcel(System.Data.DataTable dt, string strTitle, string FilePath)
        {
            beforeTime = DateTime.Now;

            Excel.Application excel;
            Excel._Workbook xBk;
            Excel._Worksheet xSt;

            int rowIndex = 4;
            int colIndex = 1;

            excel = new Excel.ApplicationClass();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel._Worksheet)xBk.ActiveSheet;

            //取得列标题			
            foreach (DataColumn col in dt.Columns)
            {
                colIndex++;
                excel.Cells[4, colIndex] = col.ColumnName;

                //设置标题格式为居中对齐
                xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Font.Bold = true;
                xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Select();
                xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Interior.ColorIndex = titleColorindex;//19;//设置为浅黄色，共计有56种
            }


            //取得表格中的数据			
            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;
                colIndex = 1;
                foreach (DataColumn col in dt.Columns)
                {
                    colIndex++;
                    if (col.DataType == System.Type.GetType("System.DateTime"))
                    {
                        excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                        xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
                    }
                    else
                        if (col.DataType == System.Type.GetType("System.String"))
                    {
                        excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
                        xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
                    }
                    else
                    {
                        excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
            }

            //加载一个合计行			
            int rowSum = rowIndex + 1;
            int colSum = 2;
            excel.Cells[rowSum, 2] = "合计";
            xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //设置选中的部分的颜色			
            xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Select();
            //xSt.get_Range(excel.Cells[rowSum,colSum],excel.Cells[rowSum,colIndex]).Interior.ColorIndex =Assistant.GetConfigInt("ColorIndex");// 1;//设置为浅黄色，共计有56种

            //取得整个报表的标题			
            excel.Cells[2, 2] = strTitle;

            //设置整个报表的标题格式			
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Bold = true;
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Size = 22;

            //设置报表表格为最适应宽度			
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Select();
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Columns.AutoFit();

            //设置整个报表的标题为跨列居中			
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).Select();
            xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenterAcrossSelection;

            //绘制边框			
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, 2]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;//设置左边线加粗
            xSt.get_Range(excel.Cells[4, 2], excel.Cells[4, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;//设置上边线加粗
            xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;//设置右边线加粗
            xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;//设置下边线加粗



            afterTime = DateTime.Now;

            //显示效果			
            //excel.Visible=true;			
            //excel.Sheets[0] = "sss";

            ClearFile(FilePath);
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
            excel.ActiveWorkbook.SaveAs(FilePath + filename, Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

            //wkbNew.SaveAs strBookName;
            //excel.Save(strExcelFileName);

            #region  结束Excel进程

            //需要对Excel的DCOM对象进行配置:dcomcnfg


            //excel.Quit();
            //excel=null;            

            xBk.Close(null, null, null);
            excel.Workbooks.Close();
            excel.Quit();


            //注意：这里用到的所有Excel对象都要执行这个操作，否则结束不了Excel进程
            //			if(rng != null)
            //			{
            //				System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
            //				rng = null;
            //			}
            //			if(tb != null)
            //			{
            //				System.Runtime.InteropServices.Marshal.ReleaseComObject(tb);
            //				tb = null;
            //			}
            if (xSt != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
                xSt = null;
            }
            if (xBk != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
                xBk = null;
            }
            if (excel != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                excel = null;
            }
            GC.Collect();//垃圾回收
            #endregion

            return filename;

        }
        #endregion

        #region Kill Excel进程

        /// <summary>
        /// 结束Excel进程
        /// </summary>
        public void KillExcelProcess()
        {
            Process[] myProcesses;
            DateTime startTime;
            myProcesses = Process.GetProcessesByName("Excel");

            //得不到Excel进程ID，暂时只能判断进程启动时间
            foreach (Process myProcess in myProcesses)
            {
                startTime = myProcess.StartTime;
                if (startTime > beforeTime && startTime < afterTime)
                {
                    myProcess.Kill();
                }
            }
        }
        #endregion

        public static void DSToExcel(DataSet MyData, string filename, Hashtable nameList)
        {
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    ZWL.Common.ExcelUtility.RenderDataTableToExcel(MyData.Tables[0], filename, nameList);
                }
            }
            catch (Exception ex)
            {
                string strex = ex.ToString();
            }
            //if (filename != "")
            //{
            //    System.Web.HttpContext.Current.Response.Redirect(filename, true);
            //}
        }

        public static void DSToExcel(DataSet MyData)
        {
            string datestring = DateTime.Now.ToString("yyyyMMddHHmmsss");
            string filename = HostingEnvironment.MapPath("~/ReportFile/") + datestring + ".xls";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    ZWL.Common.ExcelUtility.RenderDataTableToExcel(MyData.Tables[0], filename);
                }
            }
            catch (Exception ex)
            {
                string strex = ex.ToString();
            }
            if (datestring != "")
            {
                System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + datestring + ".xls", true);
            }
        }
        #endregion

        #region 将DataTable的数据导出显示为报表(不使用Excel对象，使用COM.Excel)

        #region 使用示例
        public static void GridViewToExcel(DataSet MyData, Hashtable nameList, string ReportTitle)
        {
            string FilePath = HostingEnvironment.MapPath("~/ReportFile/");
            //利用excel对象
            DataToExcel dte = new DataToExcel();
            string filename = "";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    filename = dte.DataExcel(MyData.Tables[0], ReportTitle, FilePath, nameList);
                }
            }
            catch
            { }
            if (filename != "")
            {
                System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + filename, true);
            }
        }
        public static void GridViewToExcel(System.Data.DataTable MyData, Hashtable nameList, string ReportTitle)
        {
            string FilePath = HostingEnvironment.MapPath("~/ReportFile/");
            //利用excel对象
            DataToExcel dte = new DataToExcel();
            string filename = "";
            try
            {
                if (MyData.Rows.Count > 0)
                {
                    filename = dte.DataExcel(MyData, ReportTitle, FilePath, nameList);
                }
            }
            catch
            { }
            if (filename != "")
            {
                System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + filename, true);
            }
        }
        public static string GridViewToExcel1(DataSet MyData, Hashtable nameList, string ReportTitle)
        {
            string FilePath = HostingEnvironment.MapPath("~/ReportFile/");
            //利用excel对象
            DataToExcel dte = new DataToExcel();
            string filename = "";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    filename = dte.DataExcel(MyData.Tables[0], ReportTitle, FilePath, nameList);
                }
            }
            catch
            { }
            //if (filename != "")
            //{
            //    System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + filename, false);
            //}
            return filename;
        }
        #endregion
        /// <summary>
        /// 将DataTable的数据导出显示为报表(不使用Excel对象)
        /// </summary>
        /// <param name="dt">数据DataTable</param>
        /// <param name="strTitle">标题</param>
        /// <param name="FilePath">生成文件的路径</param>
        /// <param name="nameList"></param>
        /// <returns></returns>
        public string DataExcel(System.Data.DataTable dt, string strTitle, string FilePath, Hashtable nameList)
        {
            COM.Excel.cExcelFile excel = new COM.Excel.cExcelFile();
            ClearFile(FilePath);
            string filename = strTitle + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
            excel.CreateFile(FilePath + filename);
            excel.PrintGridLines = false;

            COM.Excel.cExcelFile.MarginTypes mt1 = COM.Excel.cExcelFile.MarginTypes.xlsTopMargin;
            COM.Excel.cExcelFile.MarginTypes mt2 = COM.Excel.cExcelFile.MarginTypes.xlsLeftMargin;
            COM.Excel.cExcelFile.MarginTypes mt3 = COM.Excel.cExcelFile.MarginTypes.xlsRightMargin;
            COM.Excel.cExcelFile.MarginTypes mt4 = COM.Excel.cExcelFile.MarginTypes.xlsBottomMargin;

            double height = 1.5;
            excel.SetMargin(ref mt1, ref height);
            excel.SetMargin(ref mt2, ref height);
            excel.SetMargin(ref mt3, ref height);
            excel.SetMargin(ref mt4, ref height);

            COM.Excel.cExcelFile.FontFormatting ff = COM.Excel.cExcelFile.FontFormatting.xlsNoFormat;
            string font = "宋体";
            short fontsize = 9;
            excel.SetFont(ref font, ref fontsize, ref ff);

            byte b1 = 1,
                b2 = 12;
            short s3 = 12;
            excel.SetColumnWidth(ref b1, ref b2, ref s3);

            string header = "页眉";
            string footer = "页脚";
            excel.SetHeader(ref header);
            excel.SetFooter(ref footer);


            COM.Excel.cExcelFile.ValueTypes vt = COM.Excel.cExcelFile.ValueTypes.xlsText;
            COM.Excel.cExcelFile.CellFont cf = COM.Excel.cExcelFile.CellFont.xlsFont0;
            COM.Excel.cExcelFile.CellAlignment ca = COM.Excel.cExcelFile.CellAlignment.xlsCentreAlign;
            COM.Excel.cExcelFile.CellHiddenLocked chl = COM.Excel.cExcelFile.CellHiddenLocked.xlsNormal;

            // 报表标题
            int cellformat = 1;
            //			int rowindex = 1,colindex = 3;					
            //			object title = (object)strTitle;
            //			excel.WriteValue(ref vt, ref cf, ref ca, ref chl,ref rowindex,ref colindex,ref title,ref cellformat);

            int rowIndex = 1;//起始行
            int colIndex = 0;



            //取得列标题				
            foreach (DataColumn colhead in dt.Columns)
            {
                if (nameList != null && nameList.Count > 0)
                {
                    if (!nameList.ContainsKey(colhead.ColumnName)) continue;
                }

                colIndex++;
                string name = colhead.ColumnName.Trim();
                object namestr = (object)name;
                IDictionaryEnumerator Enum = nameList.GetEnumerator();
                while (Enum.MoveNext())
                {
                    if (!dt.Columns.Contains(Enum.Key.ToString().Trim())) continue;

                    if (Enum.Key.ToString().Trim() == name)
                    {
                        namestr = Enum.Value;
                    }
                }
                excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref namestr, ref cellformat);
            }

            //取得表格中的数据			
            foreach (DataRow row in dt.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    if (nameList != null && nameList.Count > 0)
                    {
                        if (!nameList.ContainsKey(col.ColumnName)) continue;
                    }

                    colIndex++;
                    if (col.DataType == System.Type.GetType("System.DateTime") && row[col.ColumnName] != null)
                    {
                        if (row[col.ColumnName].ToString() != "")
                        {
                            object str = (object)(Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd"); ;
                            excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
                        }

                    }
                    else
                    {
                        object str = (object)row[col.ColumnName].ToString();
                        excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
                    }
                }
            }
            int ret = excel.CloseFile();

            //			if(ret!=0)
            //			{
            //				//MessageBox.Show(this,"Error!");
            //			}
            //			else
            //			{
            //				//MessageBox.Show(this,"请打开文件c:\\test.xls!");
            //			}
            return filename;

        }

        public static void GridViewToExcelOrderByNameList(DataSet MyData, Dictionary<string, string> nameList, string ReportTitle)
        {
            string FilePath = HostingEnvironment.MapPath("~/ReportFile/");
            string filename = "";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    filename = DataExcelOrderByNameList(MyData.Tables[0], ReportTitle, FilePath, nameList);
                }
            }
            catch
            { }
            if (filename != "")
            {
                System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + filename, true);
            }
        }
        public static string GridViewToExcelOrderByNameListReturnFileName(DataSet MyData, Dictionary<string, string> nameList, string ReportTitle, string formatext = ".xls")
        {
            var filename = "";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    var FilePath = Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview");
                    filename = DataExcelOrderByNameList(MyData.Tables[0], ReportTitle, FilePath, nameList, formatext);
                }
            }
            catch
            { }
            return filename;
        }
        /// <summary>
        /// 导出到excel
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="MyData"></param>
        /// <param name="fieldTextList">Dictionary<DataField, HeaderText></param>
        /// <param name="ReportTitle"></param>
        public static void GridViewToExcelOrderByNameList(Page _page, DataSet MyData, Dictionary<string, string> fieldTextList, string ReportTitle)
        {
            string FilePath = HostingEnvironment.MapPath("~/ReportFile/");
            string filename = "";
            try
            {
                if (MyData.Tables[0].Rows.Count > 0)
                {
                    filename = DataExcelOrderByNameList(MyData.Tables[0], ReportTitle, FilePath, fieldTextList);
                }
                else
                {
                    ZWL.Common.MessageBox.Show(_page, "数据为空，无法导出！");
                }
            }
            catch
            { }
            if (filename != "")
            {
                System.Web.HttpContext.Current.Response.Redirect("../ReportFile/" + filename, true);
            }
        }

        /// <summary>
        /// 将DataTable的数据导出显示为报表(不使用Excel对象)
        /// </summary>
        /// <param name="dt">数据DataTable</param>
        /// <param name="strTitle">标题</param>
        /// <param name="FilePath">生成文件的路径</param>
        /// <param name="nameList"></param>
        /// <returns></returns>
        public static string DataExcelOrderByNameList(System.Data.DataTable dt, string strTitle, string FilePath, Dictionary<string, string> nameList, string formatext = ".xls")
        {
            var filename = strTitle + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + formatext;
            var filepath = Path.Combine(FilePath, filename);
            var ms = ExcelUtility.RenderDataTableToExcel(dt, nameList, strTitle, formatext.TrimStart('.')) as MemoryStream;
            var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
            return filename;
        }




        #endregion

        #region  清理过时的Excel文件

        private void ClearFile(string FilePath)
        {
            //String[] Files = System.IO.Directory.GetFiles(FilePath);
            //if (Files.Length > 10)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        try
            //        {
            //            System.IO.File.Delete(Files[i]);
            //        }
            //        catch
            //        {
            //        }

            //    }
            //}
        }
        #endregion

    }
}
