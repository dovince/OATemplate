using System;
using System.IO;

namespace ZWL.Common
{
    public static class AsposeHelper
    {
        /// <summary>
        /// 将Word转换为Pdf 
        /// </summary>
        /// <param name="strFilePath">文件地址</param>
        /// <param name="savepath">保存地址</param>
        /// <returns></returns>
        public static bool Word2Pdf(string strFilePath, string savepath)
        {

            try
            {

                Aspose.Words.Document doc = new Aspose.Words.Document(strFilePath);
                //将Word保存到指定路径下 savepath：保存路径
                doc.Save(savepath, Aspose.Words.SaveFormat.Pdf);
                //将Word保存至数据流中 再通过Response显示在页面中
                //MemoryStream memStream = new MemoryStream();
                //doc.Save(memStream, Aspose.Words.SaveFormat.Pdf);
                //byte[] bt = memStream.ToArray();
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(bt, 0, bt.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 将Word转换为Png 
        /// </summary>
        /// <param name="filepath">文件地址</param>
        /// <param name="pageIndex">要转换的页</param>
        /// <returns></returns>
        //public static void Word2Png(string filepath, int pageIndex)
        //{
        //    MemoryStream memStream = new MemoryStream();
        //    Aspose.Words.Document doc = new Aspose.Words.Document(filepath);
        //    PageInfo pageInfo = Document.GetPageInfo(pageIndex);
        //    float scale = 100 / 100.0f;
        //    const int Resolution = 96;
        //    Size imgSize = pageInfo.GetSizeInPixels(scale, Resolution);
        //    using (Bitmap img = new Bitmap(imgSize.Width, imgSize.Height))
        //    {
        //        img.SetResolution(Resolution, Resolution);
        //        using (Graphics gfx = Graphics.FromImage(img))
        //        {
        //            gfx.Clear(Color.White);
        //            Document.RenderToScale(pageIndex, gfx, 0, 0, scale);
        //            img.Save(memStream, ImageFormat.Png);
        //        }
        //    }
        //}
        //// Send the bitmap data to the output stream.
        //Response.ContentType = "image/png";
        //    byte[] imageData = memStream.ToArray();
        //Response.OutputStream.Write(imageData, 0, imageData.Length);
        //}

        /// <summary>
        /// 将Word转换为Pdf 
        /// </summary>
        /// <param name="strFilePath">文件地址</param>
        /// <param name="savepath">保存地址</param>
        /// <returns></returns>
        public static bool Excel2Pdf(string fileName, string savepath)
        {
            try
            {
                Aspose.Cells.Workbook excel = new Aspose.Cells.Workbook(fileName);
                //将ppt保存到指定路径下 savepath：保存路径
                excel.Save(savepath, Aspose.Cells.SaveFormat.Pdf);
                //将ppt保存至数据流中 再通过Response显示在页面中
                //MemoryStream memStream = new MemoryStream();
                //excel.Save(memStream, Aspose.Cells.SaveFormat.Pdf);
                //byte[] bt = memStream.ToArray();
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(bt, 0, bt.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 转换ppt文件在线预览
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="savepath">保存路径</param>
        /// <returns></returns>
        public static bool ppt2Pdf(string fileName, string savepath)
        {
            try
            {
                Aspose.Slides.Presentation ppt = new Aspose.Slides.Presentation(fileName);
                //将ppt保存到指定路径下 savepath：保存路径
                ppt.Save(savepath, Aspose.Slides.Export.SaveFormat.Pdf);
                //将ppt保存至数据流中 再通过Response显示在页面中
                //MemoryStream memStream = new MemoryStream();
                //ppt.Save(memStream, Aspose.Slides.Export.SaveFormat.Pdf);
                //byte[] bt = memStream.ToArray();
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(bt, 0, bt.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
