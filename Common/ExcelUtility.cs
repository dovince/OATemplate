using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ZWL.Common
{

    public class ExcelUtility
    {
        public static Stream RenderDataTableToExcel(DataTable SourceTable, Hashtable nameList)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);


            int colIndex = 0;
            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
            {
                if (nameList != null && nameList.Count > 0)
                {
                    if (!nameList.ContainsKey(column.ColumnName)) continue;
                }
                colIndex++;
                string name = column.ColumnName.Trim();
                object namestr = (object)name;
                IDictionaryEnumerator Enum = nameList.GetEnumerator();
                while (Enum.MoveNext())
                {
                    if (Enum.Key.ToString().Trim() == name)
                    {
                        namestr = Enum.Value;
                    }
                }
                headerRow.CreateCell(column.Ordinal).SetCellValue(namestr.ToString());
            }

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    if (column.ColumnName == "ID")//解决导出索引不正确的问题
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(rowIndex);
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static Stream RenderDataTableToExcel(DataTable SourceTable, Dictionary<string, string> nameList, string reportTitle, string fileext = "xls")
        {
            if (fileext.IsNullOrEmpty())
                fileext = "xls";
            IWorkbook workbook;
            if (fileext.Equals("xls"))
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = new XSSFWorkbook();
            }
            var sheets = workbook.NumberOfSheets;
            for (int i = 0; i < sheets; i++)
            {
                workbook.RemoveSheetAt(i);
            }
            ISheet sheet = null;
            IRow titleRow = null;
            if (reportTitle.IsNullOrEmpty())
            {
                sheet = workbook.CreateSheet();
            }
            else
            {
                sheet = workbook.CreateSheet(reportTitle);//根据索引或名称创建工作表对象

                titleRow = sheet.CreateRow(0);
                titleRow.CreateCell(3).SetCellValue(reportTitle);
            }
            

            if (SourceTable != null && SourceTable.Rows.Count > 0)
            {
                IRow headerRow = null;
                if (reportTitle.IsNullOrEmpty())
                {
                    headerRow = sheet.CreateRow(0);
                }
                else
                {
                    headerRow = sheet.CreateRow(2);
                }
                if (nameList.Any())
                {
                    var i = 0;
                    foreach (var item in nameList)
                    {
                        headerRow.CreateCell(i).SetCellValue(item.Value);
                        i++;
                    }
                }
                else
                {
                    var i = 0;
                    foreach (DataColumn item in SourceTable.Columns)
                    {
                        headerRow.CreateCell(i).SetCellValue(item.ColumnName);
                        i++;
                    }
                }
                var dataIndex = 1;
                if (!reportTitle.IsNullOrEmpty())
                {
                    dataIndex = 3;
                }
                for (int i = 0; i < SourceTable.Rows.Count; i++)
                {
                    var item = SourceTable.Rows[i];
                    IRow row = sheet.CreateRow(dataIndex + i);
                    if (nameList.Any())
                    {
                        for (int j = 0; j < nameList.Count; j++)
                        {
                            var colitem = nameList.ElementAt(j);
                            if (colitem.Key.ToLower() == "row" && !SourceTable.Columns.Contains("row"))
                            {
                                ICell cell = row.CreateCell(j);
                                cell.SetCellValue(i + 1);
                            }
                            else
                            {
                                for (int k = 0; k < SourceTable.Columns.Count; k++)
                                {
                                    var col = SourceTable.Columns[k];
                                    if (colitem.Key.Equals(col.ColumnName))
                                    {
                                        ICell cell = row.CreateCell(j);
                                        if (col.DataType == Type.GetType("System.DateTime") && item[col.ColumnName] != null)
                                        {
                                            if (item[col.ColumnName].ToString() != "")
                                            {
                                                var str = (Convert.ToDateTime(item[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                                                cell.SetCellValue(str);
                                            }
                                        }
                                        else
                                        {
                                            var value = item[col.ColumnName].ToString();
                                            if (PublicMethod.IsMoney(value))
                                                cell.SetCellValue(PublicMethod.GetDouble(value));
                                            else
                                                cell.SetCellValue(value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < SourceTable.Columns.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            var value = SourceTable.Rows[i][j].ToString();
                            if (PublicMethod.IsMoney(value))
                                cell.SetCellValue(PublicMethod.GetDouble(value));
                            else
                                cell.SetCellValue(value);
                        }
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);

            sheet = null;
            workbook = null;

            return ms;
        }
        public static Stream AppendSheetToExcel(Stream ExcelFileStream, DataTable SourceTable, Dictionary<string, string> nameList, string reportTitle, string fileext = "xls")
        {
            if (fileext.IsNullOrEmpty())
                fileext = "xls";
            IWorkbook workbook;
            if (fileext.Equals("xls"))
            {
                workbook = new HSSFWorkbook(ExcelFileStream);
            }
            else
            {
                workbook = new XSSFWorkbook(ExcelFileStream);
            }

            ISheet sheet = null;
            IRow titleRow = null;
            if (reportTitle.IsNullOrEmpty())
            {
                sheet = workbook.CreateSheet();
            }
            else
            {
                sheet = workbook.CreateSheet(reportTitle);//根据索引或名称创建工作表对象

                titleRow = sheet.CreateRow(0);
                titleRow.CreateCell(3).SetCellValue(reportTitle);
            }

            if (SourceTable != null && SourceTable.Rows.Count > 0)
            {
                IRow headerRow = null;
                if (reportTitle.IsNullOrEmpty())
                {
                    headerRow = sheet.CreateRow(0);
                }
                else
                {
                    headerRow = sheet.CreateRow(2);
                }
                if (nameList.Any())
                {
                    var i = 0;
                    foreach (var item in nameList)
                    {
                        headerRow.CreateCell(i).SetCellValue(item.Value);
                        i++;
                    }
                }
                else
                {
                    var i = 0;
                    foreach (DataColumn item in SourceTable.Columns)
                    {
                        headerRow.CreateCell(i).SetCellValue(item.ColumnName);
                        i++;
                    }
                }
                var dataIndex = 1;
                if (!reportTitle.IsNullOrEmpty())
                {
                    dataIndex = 3;
                }
                for (int i = 0; i < SourceTable.Rows.Count; i++)
                {
                    var item = SourceTable.Rows[i];
                    IRow row = sheet.CreateRow(dataIndex + i);
                    if (nameList.Any())
                    {
                        for (int j = 0; j < nameList.Count; j++)
                        {
                            var colitem = nameList.ElementAt(j);
                            if (colitem.Key.ToLower() == "row" && !SourceTable.Columns.Contains("row"))
                            {
                                ICell cell = row.CreateCell(j);
                                cell.SetCellValue(i + 1);
                            }
                            else
                            {
                                for (int k = 0; k < SourceTable.Columns.Count; k++)
                                {
                                    var col = SourceTable.Columns[k];
                                    if (colitem.Key.Equals(col.ColumnName))
                                    {
                                        ICell cell = row.CreateCell(j);
                                        if (col.DataType == Type.GetType("System.DateTime") && item[col.ColumnName] != null)
                                        {
                                            if (item[col.ColumnName].ToString() != "")
                                            {
                                                var str = (Convert.ToDateTime(item[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                                                cell.SetCellValue(str);
                                            }
                                        }
                                        else
                                        {
                                            var value = item[col.ColumnName].ToString();
                                            if (PublicMethod.IsMoney(value))
                                                cell.SetCellValue(PublicMethod.GetDouble(value));
                                            else
                                                cell.SetCellValue(value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < SourceTable.Columns.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            var value = SourceTable.Rows[i][j].ToString();
                            if (PublicMethod.IsMoney(value))
                                cell.SetCellValue(PublicMethod.GetDouble(value));
                            else
                                cell.SetCellValue(value);
                        }
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);

            sheet = null;
            workbook = null;

            return ms;
        }

        public static void RenderDataTableToExcel(DataTable SourceTable, string FileName, Hashtable nameList)
        {
            MemoryStream ms = RenderDataTableToExcel(SourceTable, nameList) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
        }
        public static string RenderDataTableToExcel1(DataTable SourceTable, string FileName, Hashtable nameList)
        {
            MemoryStream ms = RenderDataTableToExcel(SourceTable, nameList) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
            return "";
        }
        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        public static void RenderDataTableToExcel(DataTable SourceTable, string FileName)
        {
            MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
        }
        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            NPOI.SS.UserModel.ISheet sheet = workbook.GetSheet(SheetName);

            DataTable table = new DataTable();

            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
        public static DataTable FileToDataTable(string fileName)
        {
            return FileToDataTable(fileName, 0);
        }

        public static DataTable FileToDataTable(string fileName, int headerIndex = 0)
        {
            DataTable dt = new DataTable();
            string extendName = Path.GetExtension(fileName);//获取文件的后缀名
            switch (extendName.ToLower())
            {
                case ".xls":
                    dt = XlsToDataTable(fileName, headerIndex);
                    break;
                case ".xlsx":
                    dt = XlsxToDataTable(fileName, headerIndex);
                    break;
                default:
                    break;
            }
            return dt;
        }
        private static DataTable XlsToDataTable(string fileName)
        {
            return XlsToDataTable(fileName, 0);
        }
        private static DataTable XlsToDataTable(string fileName, int headerIndex = 0)
        {
            DataTable dataTable = new DataTable();
            Stream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                var hssfworkbook = new HSSFWorkbook(stream);
                HSSFSheet hssfsheet = (HSSFSheet)hssfworkbook.GetSheetAt(hssfworkbook.ActiveSheetIndex);
                HSSFRow hssfrow = (HSSFRow)hssfsheet.GetRow(headerIndex);
                int lastCellNum = (int)hssfrow.LastCellNum;
                for (int i = (int)hssfrow.FirstCellNum; i < lastCellNum; i++)
                {
                    DataColumn column = new DataColumn(hssfrow.GetCell(i).StringCellValue);
                    dataTable.Columns.Add(column);
                }
                dataTable.TableName = hssfsheet.SheetName;
                int lastRowNum = hssfsheet.LastRowNum;
                //列名后,从TABLE第二行开始进行填充数据
                for (int i = headerIndex + 1; i < hssfsheet.LastRowNum; i++)//
                {
                    var hssfrow2 = (HSSFRow)hssfsheet.GetRow(i);
                    if (hssfrow2 == null || hssfrow.Cells.Count <= 0 || hssfrow2.FirstCellNum < 0) continue;
                    DataRow dataRow = dataTable.NewRow();
                    for (int j = (int)hssfrow2.FirstCellNum; j < lastCellNum; j++)//
                    {
                        var cell = hssfrow2.GetCell(j);
                        dataRow[j] = cell;//
                        if (cell != null && cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                        {
                            dataRow[j] = cell.DateCellValue.ToString();
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("xls to DataTable: \n" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return dataTable;
        }
        private static DataTable XlsxToDataTable(string fileName)
        {
            return XlsxToDataTable(fileName, 0);
        }
        private static DataTable XlsxToDataTable(string fileName, int headerIndex = 0)
        {
            DataTable dataTable = new DataTable();
            Stream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                var hssfworkbook = new XSSFWorkbook(stream);
                var hssfsheet = (XSSFSheet)hssfworkbook.GetSheetAt(hssfworkbook.ActiveSheetIndex);
                var hssfrow = (XSSFRow)hssfsheet.GetRow(headerIndex);
                int lastCellNum = (int)hssfrow.LastCellNum;
                for (int i = (int)hssfrow.FirstCellNum; i < lastCellNum; i++)
                {
                    DataColumn column = new DataColumn(hssfrow.GetCell(i).StringCellValue);
                    dataTable.Columns.Add(column);
                }
                dataTable.TableName = hssfsheet.SheetName;
                int lastRowNum = hssfsheet.LastRowNum;
                //列名后,从TABLE第二行开始进行填充数据
                for (int i = headerIndex + 1; i < hssfsheet.LastRowNum; i++)//
                {
                    var hssfrow2 = (XSSFRow)hssfsheet.GetRow(i);
                    if (hssfrow2 == null || hssfrow.Cells.Count <= 0 || hssfrow2.FirstCellNum < 0) continue;
                    DataRow dataRow = dataTable.NewRow();
                    for (int j = (int)hssfrow2.FirstCellNum; j < lastCellNum; j++)//
                    {
                        var cell = hssfrow2.GetCell(j);
                        dataRow[j] = cell;//
                        if (cell != null && cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                        {
                            dataRow[j] = cell.DateCellValue.ToString();
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("xlsx to DataTable: \n" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return dataTable;
        }
        public static void SetActiveSheet(string fileName, int sheetIndex = 0)
        {
            try
            {
                var file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                var extendName = Path.GetExtension(fileName);//获取文件的后缀名
                IWorkbook wbook = null;
                switch (extendName.ToLower())
                {
                    case ".xls":
                        wbook = new HSSFWorkbook(file);
                        break;
                    case ".xlsx":
                        wbook = new XSSFWorkbook(file);
                        break;
                    default:
                        break;
                }
                if (wbook != null)
                {
                    wbook.SetActiveSheet(sheetIndex);
                    using (FileStream filess = File.OpenWrite(fileName))
                    {
                        wbook.Write(filess);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable ExcelToDataTable(string fileName, string sheetName = "", int headerIndex = 0, int dataRowIndex = 1)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            FileStream fs;
            IWorkbook workbook = null;
            int cellCount = 0;
            int rowCount = 0;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                if (fileName.EndsWith(".xlsx")) // 2007 版本
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.EndsWith(".xls")) // 2003 版本
                {
                    workbook = new HSSFWorkbook(fs);
                }

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    IRow headerRow = sheet.GetRow(headerIndex);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; ++i)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        data.Columns.Add(column);
                    }

                    startRow = dataRowIndex;

                    rowCount = sheet.LastRowNum;

                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);

                        if (row == null)
                        {
                            continue;
                        }
                        if (row.FirstCellNum < 0) break;

                        DataRow dataRow = data.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }

                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}
