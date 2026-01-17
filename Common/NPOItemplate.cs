using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
namespace ZWL.Common
{
    public class NPOItemplate
    {

        /// <summary>
        /// 填充字典里面的字段 如 #key#  
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="Dic"></param>
        private static void _InitTmpFromDictionary(ISheet sheet, Dictionary<string, string> Dic)
        {
            Func<Match, string> initprams = (m =>
            {
                var res = string.Empty;
                if (m.Value.Contains("#{"))
                {
                    res = m.Value.Replace("#{", "#").Replace("}#", "#");
                }
                else
                {
                    var key = m.Value.TrimStart('#').TrimEnd('#');
                    res = Dic.ContainsKey(key) ? Dic[key] : "";
                }
                return res;
            });

            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) { continue; }
                for (int j = 0; j < row.LastCellNum + 1; j++)
                {
                    var cellobj = row.GetCell(j);
                    if (cellobj != null)
                    {
                        var cellvalue = cellobj.ToString();
                        if (!string.IsNullOrEmpty(cellvalue) && cellvalue.Contains("#"))
                        {
                            cellvalue = Regex.Replace(cellvalue, @"#[a-zA-Z_\{\}0-9]+?#", new MatchEvaluator(initprams), System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if (!string.IsNullOrEmpty(cellvalue) && cellvalue.Contains("base64") && cellvalue.Contains("data:image"))
                            {
                                string prebase64 = cellvalue.Split(',')[0];
                                string base64String = cellvalue.Split(',')[1];
                                byte[] imageBytes = Convert.FromBase64String(base64String);
                                int pictureIndex = sheet.Workbook.AddPicture(imageBytes, GetPictureTypeFromExtension("." + ExtractImageType(prebase64)));

                                IDrawing drawing = sheet.CreateDrawingPatriarch();
                                IClientAnchor anchor = new XSSFClientAnchor(0, 0, 0, 0, cellobj.ColumnIndex, cellobj.RowIndex - 1, cellobj.ColumnIndex + 1, cellobj.RowIndex);
                                IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                                //picture.Resize(110,60);
                            }
                            else
                            {
                                cellobj.SetCellValue(cellvalue);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// [tmp]模版插入数据
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="detailtable">数据datatable</param>
        /// <param name="rowIndex">模版的行的index</param>
        private static void _InitTmpDetailFromDataTable(ISheet sheet, DataTable detailtable, ref int rowIndex)
        {
            if (detailtable.Rows.Count > 0)
            {
                var tmpindex = rowIndex;//记录模版的index，追加完成后删除
                var itemcount = 0;
                ICell datacell;
                IRow tmprow = sheet.GetRow(rowIndex);
                #region 委托
                DataRow rowfield = detailtable.Rows[0];
                Func<Match, string> initprams = (m =>
                {
                    var res = string.Empty;
                    if (m.Value.Contains("{"))
                    {
                        res = m.Value.Replace("{", "").Replace("}", "");
                    }
                    else
                    {
                        var key = m.Value.TrimStart('#').TrimEnd('#');
                        res = detailtable.Columns.Contains(key) && key != "" ? rowfield[key].ToString() : "";
                    }
                    return res;
                });
                #endregion

                foreach (DataRow row in detailtable.Rows)
                {
                    itemcount++;
                    rowIndex++;

                    //这里有个bug 移动区域有合并列的时候 有可能报“索引超出范围。必须为非负值并小于集合大小”错误
                    sheet.ShiftRows(rowIndex, //开始行
                        sheet.LastRowNum, //结束行
                        1, //插入行总数
                        true,        //是否复制行高
                        false        //是否重置行高
                        );
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    #region 新追加的行 追加列 并填充数据
                    rowfield = row;
                    foreach (ICell col in tmprow)
                    {
                        datacell = dataRow.CreateCell(col.ColumnIndex);
                        datacell.CellStyle = col.CellStyle;
                        var field = col.ToString();
                        var cellvalue = col.ToString().Replace("[tmp]", "");
                        if (!string.IsNullOrEmpty(cellvalue) && cellvalue.Contains("#"))
                        {
                            cellvalue = Regex.Replace(cellvalue, @"#[a-zA-Z_\{\}]+?#", new MatchEvaluator(initprams), System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if (!string.IsNullOrEmpty(cellvalue) && cellvalue.Contains("base64") && cellvalue.Contains("data:image"))
                            {
                                var prebase64 = cellvalue.Split(',')[0];
                                string base64String = cellvalue.Split(',')[1];
                                byte[] imageBytes = Convert.FromBase64String(base64String);
                                int pictureIndex = sheet.Workbook.AddPicture(imageBytes, GetPictureTypeFromExtension("." + ExtractImageType(prebase64)));

                                IDrawing drawing = sheet.CreateDrawingPatriarch();
                                IClientAnchor anchor = new XSSFClientAnchor(0, 0, 0, 0, datacell.ColumnIndex, datacell.RowIndex - 1, datacell.ColumnIndex + 1, datacell.RowIndex);
                                IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                                //picture.Resize(110,60);
                            }
                            else
                            {
                                datacell.SetCellValue(cellvalue);
                            }
                        }
                    }
                    #endregion
                }

                //清除模版行
                sheet.ShiftRows(tmpindex + 1, //开始行
                       sheet.LastRowNum, //结束行
                       -1
                       );
            }
        }

        /// <summary>
        /// 主要方法
        /// </summary>
        /// <param name="TemplateServerPath"></param>
        /// <param name="Dic"></param>
        /// <param name="detailtable"></param>
        /// <returns></returns>

        public static IWorkbook GenerateIWorkbook(string TemplateServerPath, Dictionary<string, string> Dic, DataTable detailtable)
        {

            IWorkbook hssfworkbook = null;
            string fileExt = "";
            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream(TemplateServerPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileExt = Path.GetExtension(TemplateServerPath).ToLower();
                if (fileExt == ".xlsx")
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                else if (fileExt == ".xls")
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                file.Close();
            }
            if (hssfworkbook != null)
            {
                ISheet sheet = hssfworkbook.GetSheetAt(0);
                _InitTmpFromDictionary(sheet, Dic);
                var rowIndex = -1;
                for (int i = 0; i < sheet.LastRowNum + 1; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) { continue; }
                    var cellobj = row.GetCell(0);
                    if (cellobj != null && cellobj.ToString().Contains("[tmp]"))
                    {
                        rowIndex = i;
                        break;
                    }
                }
                _InitTmpDetailFromDataTable(sheet, detailtable, ref rowIndex);
            }
            return hssfworkbook;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static MemoryStream IworkToMemoryStream(IWorkbook workbook)
        {
            //hssfworkbook.Write(new FileStream(outpath+"out"+fileExt, FileMode.Create, FileAccess.Write,FileShare.ReadWrite));
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            return ms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="outpath"></param>
        public static void IworkSave(IWorkbook workbook, string outpath)
        {
            var filename = Path.GetFileName(outpath);
            if (!Directory.Exists(Path.GetDirectoryName(outpath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outpath));
            }
            workbook.Write(new FileStream(outpath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
        }// 假设的基于文件后缀名决定行为的方法  
        public static PictureType GetPictureTypeFromExtension(string ext)
        {
            // 注意：这里我们实际上并不根据后缀名来决定 PictureType，  
            // 因为 PictureType 不直接关联到文件类型。  
            // 但为了演示，我们返回一个默认值。  
            var result = PictureType.PNG;
            if (!ext.IsNullOrEmpty())
            {
                switch (ext)
                {
                    case ".jpeg":
                    case ".jpg":
                        result = PictureType.JPEG;
                        break;
                    case ".png":
                        result = PictureType.PNG;
                        break;
                    case ".gif":
                        result = PictureType.GIF;
                        break;
                    case ".bmp":
                        result = PictureType.BMP;
                        break;
                }
            }
            return result;
        }

        /// <summary>  
        /// 从给定的base64前缀字符串中提取图片类型。  
        /// </summary>  
        /// <param name="base64Prefix">包含base64前缀的字符串，如 "data:image/jpeg;base64,"</param>  
        /// <returns>提取出的图片类型（如jpeg、png），如果未找到则返回null。</returns>  
        public static string ExtractImageType(string base64Prefix)
        {
            var result = "";
            if (!string.IsNullOrEmpty(base64Prefix))
            {
                Regex RegexPattern = new Regex(@"data:image/(\w+);base64,", RegexOptions.IgnoreCase);

                Match match = RegexPattern.Match(base64Prefix);

                if (match.Success)
                {
                    // 返回捕获组中的第一个（也是唯一一个）匹配项，即图片类型  
                    return match.Groups[1].Value;
                }

            }

            // 如果没有找到匹配项，则返回null  
            return result;
        }
    }
}

