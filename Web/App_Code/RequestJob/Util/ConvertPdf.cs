using PdfiumViewer;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using ZWL.Common;

namespace RequestJob
{
    /// <summary>
    /// ConvertPdf 的摘要说明
    /// </summary>
    public class ConvertPdf : Base, IRequestJob
    {

        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        ///// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        ///// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>       
        ///// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        ///// <param name="imageFormat">设置所需图片格式</param>       
        public JsonResult GetPDF2OneImage(HttpRequest Request)
        {
            try
            {
                var result = string.Empty;
                var PDFFilePath = ConfigurationManager.AppSettings["PDFFilePath"];
                var SWFFilePath = ConfigurationManager.AppSettings["SWFFilePath"];
                //格式 
                var imageFormat = ImageFormat.Png;
                var filename = Request["filename"];
                var imgIndex = PublicMethod.GetInto(Request["idx"]);
                result = filename + (imgIndex + 1) + "." + imageFormat.ToString();
                var pdfInputPath = Path.Combine(Request.MapPath(PDFFilePath), PublicMethod.MakeValidFileName1(Path.GetFileNameWithoutExtension(filename)) + ".pdf");
                var pdfOutputPath = Path.Combine(Request.MapPath(SWFFilePath), Path.GetFileNameWithoutExtension(filename));
                if (!Directory.Exists(pdfOutputPath))
                {
                    Directory.CreateDirectory(pdfOutputPath);
                }
                var imageOutputRet = Path.Combine(pdfOutputPath, result);
                if (!FileIsExists(imageOutputRet))
                {
                    var dpi = 48 * (int)Definition.Two;
                    var length = 0;
                    using (var document = PdfDocument.Load(pdfInputPath))
                    {
                        length = document.PageCount;
                        if (imgIndex >= 0 && imgIndex < length)
                        {
                            using (var image = document.Render(imgIndex, (int)document.PageSizes[imgIndex].Width, (int)document.PageSizes[imgIndex].Height, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
                            {
                                image.Save(imageOutputRet, imageFormat);
                            }
                        }
                    }
                }
                return JsonResult(true, "", new { Name = result, Index = imgIndex });
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }

        //图片的清晰度，数字越大越清晰
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }

        //判断文件是否存在
        private bool FileIsExists(string path)
        {
            if (System.IO.File.Exists(path))
                return true;
            else
                return false;
        }
    }
}