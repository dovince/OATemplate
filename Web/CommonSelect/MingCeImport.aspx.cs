using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class CommonSelect_MingCeImport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ImportInfo info = null;
            var type = Get("Type");
            if (type != null)
            {
                info = ImportList.FirstOrDefault(x => x.Type == type);
                if (info != null)
                {
                    lblFormTitle.Text = info.Title;
                    TemplateExcelFilePath.NavigateUrl = info.TemplateUrl;
                    TemplateExcelFilePath.Text = info.TemplateText;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg)&& ImportMingCe(ref msg))
        {
            var sb = new StringBuilder();
            sb.AppendLine(MessageBox.CheckEasyUI);
            sb.AppendLine(MessageBox.ShowText);
            sb.AppendFormat("alert('{0}');CCC();window.parent.frameElement.src = window.parent.frameElement.src;", "导入成功！");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptKey", sb.ToString(), true);
        }
        else
        {
            MessageBox.Show(this, msg);
        }

    }
    private bool ImportMingCe(ref string msg)
    {
        var result = true;
        try
        {
            var type = Get("Type");
            var info = ImportList.FirstOrDefault(x => x.Type == type);
            var files = UploadFiles.Result.Split('|').ToList();
            var select = files.FirstOrDefault(r => !r.IsNullOrEmpty());
            if (select != null)
            {
                var sourcefile = Path.Combine(PublicMethod.UploadFileFolderTruePath, select);
                var dt = ExcelUtility.FileToDataTable(sourcefile);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var syscols = Util.GetSysColumnInfos(info.TableName);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        var item = dt.Columns[i];
                        var citem = syscols.FirstOrDefault(r => r.Desc == item.ColumnName);
                        if (citem != null)
                        {
                            dt.Columns[i].ColumnName = citem.Name;
                        }
                    }

                    if (type == "ZB")
                        InsertToZaiBian(dt);
                    else if (type == "HT")
                        InsertToHeTongZhi(dt);
                    else if (type == "PQ")
                        InsertToLaoWu(dt);
                    else if (type == "LT")
                        InsertToLiTui(dt);
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
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
        }
        var type = Get("Type");
        if (type.IsNullOrEmpty())
        {
            msg = "非法访问。";
            return false;
        }
        else
        {
            var item = ImportList.FirstOrDefault(r => r.Type == type);
            if (item != null)
            {
                var count = DbHelperSQL.GetSHSLInt1("select count(1) from {0} where (DeleteMark is null or DeleteMark='')".FormatWith(item.TableName));
                if (count > 0 && UserName != "admin")
                {
                    msg = "系统只支持导入一次模板数据，如需再次导入请删除所有已导入数据，也可以点击添加按钮逐条添加。";
                    return false;
                }
            }
        }

        return result;
    }
    private bool InsertToZaiBian(DataTable dt)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeZaiBianZhiGong>(dt);
            foreach (var item in list)
            {
                if (item.XingMing.IsNullOrEmpty()) continue;
                item.Number = Guid.NewGuid().ToString();
                item.CreatorTime = Timestamp;
                item.CreatorUser = UserName;
                item.ID = item.Add();
                item.SortCode = item.ID;
                item.Update();
            }
            result = true;
        }
        return result;
    }
    private bool InsertToHeTongZhi(DataTable dt)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeHeTongZhiZhiGong>(dt);
            foreach (var item in list)
            {
                if (item.XingMing.IsNullOrEmpty()) continue;
                item.Number = Guid.NewGuid().ToString();
                item.CreatorTime = Timestamp;
                item.CreatorUser = UserName;
                item.ID = item.Add();
                item.SortCode = item.ID;
                item.Update();
            }
            result = true;
        }
        return result;
    }
    private bool InsertToLaoWu(DataTable dt)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeLaoWuPaiQian>(dt);
            foreach (var item in list)
            {
                if (item.XingMing.IsNullOrEmpty()) continue;
                item.Number = Guid.NewGuid().ToString();
                item.CreatorTime = Timestamp;
                item.CreatorUser = UserName;
                item.ID = item.Add();
                item.SortCode = item.ID;
                item.Update();
            }
            result = true;
        }
        return result;
    }
    private bool InsertToLiTui(DataTable dt)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var list = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeLiTuiXiu>(dt);
            foreach (var item in list)
            {
                if (item.XingMing.IsNullOrEmpty()) continue;
                item.Number = Guid.NewGuid().ToString();
                item.CreatorTime = Timestamp;
                item.CreatorUser = UserName;
                item.ID = item.Add();
                item.SortCode = item.ID;
                item.Update();
            }
            result = true;
        }
        return result;
    }
    #region MyRegion
    protected static List<ImportInfo> ImportList =
    new List<ImportInfo>
    {
            new ImportInfo{ Type="ZB",Title="在编职工名册导入",TemplateUrl="../ReportFile/职工名册导入模板_在编职工.xls",TemplateText="在编职工名册导入模板.xls",TableName="ERPMingCeZaiBianZhiGong" },
            new ImportInfo{ Type="HT",Title="合同制职工名册导入",TemplateUrl="../ReportFile/职工名册导入模板_合同制职工.xls",TemplateText="合同制职工名册导入模板.xls",TableName="ERPMingCeHeTongZhiZhiGong" },
            new ImportInfo{ Type="PQ",Title="劳务派遣人员名册导入",TemplateUrl="../ReportFile/职工名册导入模板_劳务派遣人员.xls",TemplateText="劳务派遣人员名册导入模板.xls",TableName="ERPMingCeLaoWuPaiQian" },
            new ImportInfo{ Type="LT",Title="离退休职工名册导入",TemplateUrl="../ReportFile/职工名册导入模板_离退休职工.xls",TemplateText="离退休职工名册导入模板.xls",TableName="ERPMingCeLiTuiXiu" },
    };
    protected class ImportInfo
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string TemplateUrl { get; set; }
        public string TemplateText { get; set; }
        public string TableName { get; set; }
    }
    #endregion
}