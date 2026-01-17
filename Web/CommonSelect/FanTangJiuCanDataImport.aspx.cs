using Aspose.Slides.Effects;
using FSDZ.Logger;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.BLL;
using ZWL.Common;
using ZWL.DBUtility;

public partial class CommonSelect_FanTangJiuCanDataImport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblFormTitle.Text = "饭堂用餐数据导入";
            //TemplateExcelFilePath.NavigateUrl = "";
            //TemplateExcelFilePath.Text = "";//饭堂就餐数据导入
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg)&& ImportJiuCanRecord(ref msg))
        {
            SchedulerJob.SchedulerAgent.SendJob(new SchedulerJob.CustomJobs.ChuChaiQiJianYongCan());
            var sb = new StringBuilder();
            sb.AppendLine(MessageBox.CheckEasyUI);
            sb.AppendLine(MessageBox.ShowText);
            sb.AppendFormat("alert('{0}');CCC();window.parent.frameElement.src = window.parent.frameElement.src;", "操作成功！(后台正在继续导入数据，预计2-5分钟完成，查看需手动刷新)");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptKey", sb.ToString(), true);
        }
        else
        {
            MessageBox.Show(this, msg);
        }

    }
    private bool ImportJiuCanRecord(ref string msg)
    {
        var result = true;
        try
        {
            var list = UploadFiles.Result.Split('|');
            var select = list.FirstOrDefault(r => !r.IsNullOrEmpty());
            var lotid = Guid.NewGuid().ToString();
            var sModel = new ZWL.BLL.ERPSaveFileName();
            sModel = sModel.GetModelByNowName(select);
            var flow = new ZWL.BLL.Flow()
            {
                DataTable = "FanTangJiuCanRecord",
                CreatedTime = Timestamp,
                TKey = "LotID",
                NewValue = select,
                OldValue = sModel != null ? sModel.OldName : select,
                Operation = (int)ZWL.BLL.FlowOperation.Add,
                UserName = UserName,
                LotID = lotid,
                ParentID = lotid,
                RecordID = lotid,
            };
            flow.ID = flow.Add();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            FSDZ.Logger.Logger.Log(ex);
            result = false;
        }
        return result;
    }
    private bool Validate(ref string msg)
    {
        var result = true;
        if (UploadFiles.Result.IsNullOrEmpty())
        {
            msg = "请上传附件。";
            return false;
        }
        else
        {
            var list = UploadFiles.Result.Split('|');
            if (!list.Any(r => !r.IsNullOrEmpty()))
            {
                msg = "请上传附件。";
                return false;
            }
            foreach (var item in list)
            {
                if (item.IsNullOrEmpty()) continue;
                var extendName = Path.GetExtension(item);//获取文件的后缀名
                if (extendName != ".xls" && extendName != ".xlsx")
                {
                    msg = "上传附件格式有误，只支持xls和xlsx后缀的文件。";
                    return false;
                }
            }
            var select = list.FirstOrDefault(r => !r.IsNullOrEmpty());
            var filepath = Path.Combine(PublicMethod.UploadFileFolderTruePath, select);
            var headers = new string[] { "序号", "编号", "姓名", "部门", "职务", "性别", "日期", "星期", "时间段", "考勤记录" };
            Stream stream = null;
            try
            {
                stream = File.OpenRead(filepath);
                IWorkbook hssfworkbook = null;
                var extendName = Path.GetExtension(select);//获取文件的后缀名
                switch (extendName.ToLower())
                {
                    case ".xls":
                        hssfworkbook = new HSSFWorkbook(stream);
                        break;
                    case ".xlsx":
                        hssfworkbook = new XSSFWorkbook(stream);
                        break;
                    default:
                        break;
                }
                if (hssfworkbook == null)
                {
                    msg = "上传附件格式有误，只支持xls和xlsx后缀的文件。";
                    return false;
                }
                hssfworkbook.SetActiveSheet(0);
                var hssfsheet = hssfworkbook.GetSheetAt(hssfworkbook.ActiveSheetIndex);
                var hssfrow = hssfsheet.GetRow(2);
                int lastCellNum = (int)hssfrow.LastCellNum;
                for (int i = 0; i < headers.Length; i++)
                {
                    var headText = headers[i];
                    var cellText = "";
                    if (hssfrow.GetCell(i) != null)
                    {
                        cellText = hssfrow.GetCell(i).StringCellValue;
                    }
                    if (headText != cellText)
                    {
                        msg = "第三行须为表头，且须包含列【{0}】".FormatWith(headers);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
        return result;
    }
    private bool InsertToDB(DataTable dt, string nowname)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var lotid = Guid.NewGuid().ToString();
            var list = DataTableHelper.ConvertTo<ZWL.BLL.FanTangJiuCanRecord>(dt);
            //foreach (var item in list)
            //{
            //    if (item.Name.IsNullOrEmpty()) continue;
            //    item.LotID = lotid;
            //    item.CanShi = item.ShiJianDuan.Substring(0, item.ShiJianDuan.IndexOf("餐") + 1);
            //    item.ID = item.Add();
            //}
            var sModel = new ZWL.BLL.ERPSaveFileName();
            sModel = sModel.GetModelByNowName(nowname);
            var flow = new ZWL.BLL.Flow()
            {
                DataTable = "FanTangJiuCanRecord",
                CreatedTime = Timestamp,
                TKey = "LotID",
                NewValue = nowname,
                OldValue = sModel != null ? sModel.OldName : nowname,
                Operation = (int)ZWL.BLL.FlowOperation.Add,
                UserName = UserName,
                LotID = lotid,
                ParentID = lotid,
                RecordID = lotid,
            };
            flow.ID = flow.Add();
            result = true;
        }
        return result;
    }
    #region MyRegion

    #endregion
}