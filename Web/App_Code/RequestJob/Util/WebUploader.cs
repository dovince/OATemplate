using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZWL.Common;

namespace RequestJob
{
    /// <summary>
    /// WebUploader 的摘要说明
    /// </summary>
    public class WebUploader : Base, IRequestJob
    {

        public JsonResult FileUpload(HttpRequest Request)
        {
            try
            {
                if(HttpContext.Current.Session["UserName"] == null)
                {
                    return null;
                }
                if(HttpContext.Current.Session["UserName"] == "" || HttpContext.Current.Session["UserName"] == "NoLogin")
                {
                    return null;
                }
                var fileInput = HttpContext.Current.Request.Files["fileInput"];
                var fileName = fileInput.FileName;
                if (fileName.Contains("\\"))
                {
                    fileName = fileName.Split('\\').ToList().LastOrDefault();
                }

                if (PublicMethod.IfOkFile(fileName))
                {
                    var FileNameStr = PublicMethod.UploadFileIntoDir(fileInput, DateTime.Now.Ticks.ToString() + Path.GetExtension(fileName));
                    var fileSize = PublicMethod.GetByteUnit(fileInput.ContentLength);
                    var sf = new FileViewModel() { NowName = FileNameStr, OldName = fileName, FileSize = fileSize };
                    return JsonResult(true, "上传成功", sf);
                }
                else
                {
                    return JsonResult(false, "不允许上传此类型文件！");
                }
            }
            catch
            {
                return JsonResult(false, "上传失败");
            }
        }
        public JsonResult UploadFile(HttpRequest Request)
        {
            var fileInput = HttpContext.Current.Request.Files["fileInput"];
            if (fileInput.ContentLength > 0)
            {
                var dt = Request.RequestContext.HttpContext.Timestamp;
                var guid = Guid.NewGuid().ToString();
                var fileSize = PublicMethod.GetByteUnit(fileInput.ContentLength);
                var fileName = fileInput.FileName;
                if (fileName.Contains("\\"))
                {
                    fileName = fileName.Split('\\').ToList().LastOrDefault();
                }
                var nowName = PublicMethod.UploadFileIntoDir(fileInput, dt.Ticks.ToString() + Path.GetExtension(fileInput.FileName));
                var filePath = HttpContext.Current.Server.MapPath("~/Html/UploadFileEditRow.html");
                var htmlStr = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                var downUrl = string.Format("../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/{0}&n={1}", nowName, fileName);
                var readUrl = string.Format("../FlexPaperFlash/SWFShow.aspx?f={0}&n={1}", nowName, fileName);
                var list = new List<object>();
                var o = new
                {
                    nowName = nowName,
                    oldName = fileName,
                    showHtml = string.Format(htmlStr, fileName, fileSize, readUrl, downUrl, nowName)
                };
                list.Add(o);
                var smodel = new ZWL.BLL.ERPSaveFileName();
                smodel = smodel.GetModelByNowName(nowName);
                new ZWL.BLL.Flow()
                {
                    DataTable = "ERPSaveFileName",
                    CreatedTime = dt,
                    TKey = "NowName",
                    NewValue = nowName,
                    Operation = 1,
                    UserName = PublicMethod.GetUserName(),
                    LotID = guid,
                    ParentID = smodel.ID.ToString(),
                    RecordID = smodel.ID.ToString(),
                }.Add();
                new ZWL.BLL.Flow()
                {
                    DataTable = "ERPSaveFileName",
                    CreatedTime = dt,
                    TKey = "OldName",
                    NewValue = fileName,
                    Operation = 1,
                    UserName = PublicMethod.GetUserName(),
                    LotID = guid,
                    ParentID = smodel.ID.ToString(),
                    RecordID = smodel.ID.ToString(),
                }.Add();
                return JsonResult(true, "上传成功", list);
            }
            return JsonResult(false, "上传失败", "");
        }
        private class FileViewModel : ZWL.BLL.ERPSaveFileName
        {
            public string FileSize { get; set; }
        }
    }
}