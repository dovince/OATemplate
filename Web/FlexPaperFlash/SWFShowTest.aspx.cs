using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using ZWL.Common;
using System.Diagnostics;

public partial class FlexPaperFlash_SWFShow : System.Web.UI.Page
{
    public string SwfFile = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                initJavascript();

                string PDFFilePath = System.Configuration.ConfigurationManager.AppSettings["PDFFilePath"];
                string SWFFilePath = System.Configuration.ConfigurationManager.AppSettings["SWFFilePath"];

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(PDFFilePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(PDFFilePath));
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(SWFFilePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(SWFFilePath));
                }

                //根据文件类型转换
                string file = Request["filepath"].ToLower();
                string filename = Request["filename"].ToLower().Split('.')[0];
                string sourcePath = HttpContext.Current.Server.MapPath(file);
                string targetPath = HttpContext.Current.Server.MapPath(PDFFilePath + filename + ".pdf");
                if (!File.Exists(targetPath))
                {
                    if (file.Contains(".doc"))
                    {
                        ConvertWord2Pdf(sourcePath, targetPath);
                    }
                    else if (file.Contains(".xls"))
                    {
                        ConvertExcel2Pdf(sourcePath, targetPath);
                    }
                    else if (file.Contains(".ppt"))
                    {
                        ConvertPowerPoint2Pdf(sourcePath, targetPath);
                    }
                    else
                    {
                        //把pdf复制过去
                        File.Copy(sourcePath, HttpContext.Current.Server.MapPath(PDFFilePath + filename + ".pdf"));
                    }
                }

                string filePath = HttpContext.Current.Server.MapPath(PDFFilePath + filename + ".pdf");
                string saveSWFPath = HttpContext.Current.Server.MapPath(SWFFilePath + filename + ".swf");

                if (!File.Exists(saveSWFPath))
                {
                    //把pdf转换为swf
                    string cmdStr = HttpContext.Current.Server.MapPath("SWFTools/pdf2swf.exe");

                    string args = "  -t " + filePath +
                        //" -s languagedir=" + HttpContext.Current.Server.MapPath("xpdf/xpdf-chinese-simplified") +
                        //" -s languagedir=E:\\xpdf\\xpdf-chinese-simplified" +
                        " -o " + saveSWFPath +
                        " -s flashversion=9 "; //解决PDF文件只有一页，生成的swf不能播放的问题
                    ExecutCmd(cmdStr, args);
                }

                SwfFile = "SWFFiles/" + filename + ".swf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                HttpContext.Current.Response.Write("<script language='javascript' defer>");
                HttpContext.Current.Response.Write("$('#loader_container').fadeOut();");
                HttpContext.Current.Response.Write("</script>");
                HttpContext.Current.Response.Flush();
            }
        }
    }

    /// <summary>
    /// 页面加载中效果
    /// </summary>
    public static void initJavascript()
    {
        HttpContext.Current.Response.Write(" <script language=JavaScript type=text/javascript>");
        HttpContext.Current.Response.Write("var t_id = setInterval(animate,20);");
        HttpContext.Current.Response.Write("var pos=0;var dir=2;var len=0;");
        HttpContext.Current.Response.Write("function animate(){");
        HttpContext.Current.Response.Write("var elem = document.getElementById('progress');");
        HttpContext.Current.Response.Write("if(elem != null) {");
        HttpContext.Current.Response.Write("if (pos==0) len += dir;");
        HttpContext.Current.Response.Write("if (len>32 || pos>79) pos += dir;");
        HttpContext.Current.Response.Write("if (pos>79) len -= dir;");
        HttpContext.Current.Response.Write(" if (pos>79 && len==0) pos=0;");
        HttpContext.Current.Response.Write("elem.style.left = pos;");
        HttpContext.Current.Response.Write("elem.style.width = len;");
        HttpContext.Current.Response.Write("}}");
        HttpContext.Current.Response.Write("function remove_loading() {");
        HttpContext.Current.Response.Write(" this.clearInterval(t_id);");
        HttpContext.Current.Response.Write("var targelem = document.getElementById('loader_container');");
        HttpContext.Current.Response.Write("targelem.style.display='none';");
        HttpContext.Current.Response.Write("targelem.style.visibility='hidden';");
        HttpContext.Current.Response.Write("}");
        HttpContext.Current.Response.Write("</script>");
        HttpContext.Current.Response.Write("<style>");
        HttpContext.Current.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
        HttpContext.Current.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:130px; border:1px solid #5a667b; text-align:left; z-index:2;}");
        HttpContext.Current.Response.Write("#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
        HttpContext.Current.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:113px; font-size:1px;}");
        HttpContext.Current.Response.Write("</style>");
        HttpContext.Current.Response.Write("<div id=loader_container>");
        HttpContext.Current.Response.Write("<div id=loader>");
        HttpContext.Current.Response.Write("<div align=center>页面正在加载中 ...</div>");
        HttpContext.Current.Response.Write("<div id=loader_bg><div id=progress> </div></div>");
        HttpContext.Current.Response.Write("</div></div>");
        HttpContext.Current.Response.Flush();
    }

    /// <summary> 
    /// 将word文档转换成PDF格式 
    /// </summary> 
    /// <param name="sourcePath"></param> 
    /// <param name="targetPath"></param> 
    /// <returns></returns> 
    public static bool ConvertWord2Pdf(string sourcePath, string targetPath)
    {
        bool result;
        Microsoft.Office.Interop.Word.WdExportFormat exportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
        object paramMissing = Type.Missing;
        Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
        Microsoft.Office.Interop.Word.Document wordDocument = null;
        try
        {
            object paramSourceDocPath = sourcePath;
            string paramExportFilePath = targetPath;
            Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = exportFormat;
            Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor =
                    Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
            Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
            int paramStartPage = 0;
            int paramEndPage = 0;
            Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
            Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks =
                    Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;

            wordDocument = wordApplication.Documents.Open(
                    ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing);
            if (wordDocument != null)
                wordDocument.ExportAsFixedFormat(paramExportFilePath,
                        paramExportFormat, false,
                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                        paramEndPage, paramExportItem, true,
                        true, paramCreateBookmarks, true,
                        true, false,
                        ref paramMissing);
            result = true;
        }
        catch (Exception ex) {
            throw ex;
        }
        finally
        {
            if (wordDocument != null)
            {
                wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                wordDocument = null;
            }
            if (wordApplication != null)
            {
                wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                wordApplication = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return result;
    }

    /// <summary> 
    /// 将excel文档转换成PDF格式 
    /// </summary> 
    /// <param name="sourcePath"></param> 
    /// <param name="targetPath"></param> 
    /// <returns></returns> 
    public static bool ConvertExcel2Pdf(string sourcePath, string targetPath)
    {
        bool result;
        object missing = Type.Missing;
        Microsoft.Office.Interop.Excel.XlFixedFormatType targetType = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
        Excel.Application application = null;
        Excel.Workbook workBook = null;
        try
        {
            application = new Excel.Application();
            object target = targetPath;
            workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing);
            new Workbook().ExportAsFixedFormat(targetType, target, Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
            result = true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            if (workBook != null)
            {
                workBook.Close(true, missing, missing);
                workBook = null;
            }
            if (application != null)
            {
                application.Quit();
                application = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return result;
    }

    /// <summary> 
    /// 将ppt文档转换成PDF格式 
    /// </summary> 
    /// <param name="sourcePath"></param> 
    /// <param name="targetPath"></param> 
    /// <returns></returns> 
    public static bool ConvertPowerPoint2Pdf(string sourcePath, string targetPath)
    {
        bool result;
        Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType targetFileType = Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
        Microsoft.Office.Interop.PowerPoint.Application application = null;
        Microsoft.Office.Interop.PowerPoint.Presentation persentation = null;
        try
        {
            application = new Microsoft.Office.Interop.PowerPoint.Application();
            persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
            persentation.SaveAs(targetPath, targetFileType, MsoTriState.msoTrue);
            result = true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            if (persentation != null)
            {
                persentation.Close();
                persentation = null;
            }
            if (application != null)
            {
                application.Quit();
                application = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return result;
    }

    /// <summary>
    /// PDF转SWF
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="args"></param>
    public static void ExecutCmd(string cmd, string args)
    {
        try
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.PriorityClass = ProcessPriorityClass.Normal;
                p.WaitForExit();
                p.Close();
                p.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
