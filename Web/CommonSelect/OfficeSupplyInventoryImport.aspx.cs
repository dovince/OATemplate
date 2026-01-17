using FSDZ.Logger;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using ZWL.Common;

public partial class CommonSelect_OfficeSupplyInventoryImport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblFormTitle.Text = "办公用品入库";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg) && ImportItemRecord(ref msg))
        {
            var sb = new StringBuilder();
            sb.AppendLine(MessageBox.CheckEasyUI);
            sb.AppendLine(MessageBox.ShowText);
            sb.AppendFormat("alert('{0}');CCC();window.parent.frameElement.src = window.parent.frameElement.src;", "操作成功！");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptKey", sb.ToString(), true);
        }
        else
        {
            MessageBox.Show(this, msg);
        }

    }
    private bool ImportItemRecord(ref string msg)
    {
        var result = true;
        try
        {
            var list = UploadFiles.Result.Split('|');
            var select = list.FirstOrDefault(r => !r.IsNullOrEmpty());
            if (select != null && !select.IsNullOrEmpty())
            {

                var lotid = Guid.NewGuid().ToString();
                var sModel = new ZWL.BLL.ERPSaveFileName();
                sModel = sModel.GetModelByNowName(select);
                var flow = new ZWL.BLL.Flow()
                {
                    DataTable = "ERPOfficeSupplyItem",
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
                var sourcefile = Path.Combine(PublicMethod.UploadFileFolderTruePath, select);
                var dt = ExcelUtility.FileToDataTable(sourcefile, 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    InsertToDB(dt);
                }
            }
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
            var headers = new string[] { "序号", "入库日期", "物品名称", "规格型号", "数量", "单位", "单价", "总价", "采购来源", "采购人", "备注" };
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
                var hssfsheet = hssfworkbook.GetSheetAt(0);
                var hssfrow = hssfsheet.GetRow(1);
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
                        msg = "第二行须为表头，且须包含列【{0}】".FormatWith(headers);
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
    private bool InsertToDB(DataTable dt)
    {
        var result = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            var supplier = UserName;
            var fsupplier = dt.AsEnumerable().Where(x => !x.Field<string>("物品名称").IsNullOrEmpty());
            if (fsupplier != null && fsupplier.Any())
            {
                supplier = string.Join(",", fsupplier.Select(x => x.Field<string>("采购来源")));
            }
            var info = new ZWL.BLL.ERPOfficeSupplyStockIn
            {
                Name = supplier,
                CreatorTime = Timestamp,
                CreatorUser = UserName,
            };
            info.ID = info.Add();
            AddLog(info);
            var typeModel = new ZWL.BLL.ERPOfficeSupplyType();
            var typeList = typeModel.GetModelList("(DeleteMark is null or DeleteMark<>1)");
            if (typeList != null && typeList.Any(x => x.Category == "未分类"))
            {
                typeModel = typeList.FirstOrDefault();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = dt.Rows[i];
                var itemname = item["物品名称"].ToString();
                if (itemname.IsNullOrEmpty()) continue;

                var indate = item["入库日期"].ToString();
                var spec = item["规格型号"].ToString();
                var qty = item["数量"].ToString();
                var unit = item["单位"].ToString();
                var price = item["单价"].ToString();
                var total = item["总价"].ToString();
                var source = item["采购来源"].ToString();
                var cgr = item["采购人"].ToString();
                var dec = item["备注"].ToString();
                var itemModel = new ZWL.BLL.ERPOfficeSupplyItem();
                var checkItem = Conv<ZWL.BLL.ERPOfficeSupplyItem>.GetListBySQLWhere("ItemName='{0}' and Spec='{1}' and Unit='{2}' ".FormatWith(itemname, spec, unit));
                if (checkItem != null && checkItem.Any())
                {
                    if (checkItem.Any(x => !x.DeleteMark.HasValue || x.DeleteMark.Value != 1))
                    {
                        itemModel = checkItem.FirstOrDefault(x => !x.DeleteMark.HasValue || x.DeleteMark.Value != 1);
                        var shots = EditShot(itemModel);
                        itemModel.CategoryID = typeModel.ID;
                        itemModel.Update();
                        EditLog(shots, itemModel);
                    }
                    else
                    {
                        itemModel = checkItem.FirstOrDefault(x => x.DeleteMark.HasValue && x.DeleteMark.Value == 1);
                        var shots = EditShot(itemModel);
                        itemModel.DeleteMark = null;
                        itemModel.Update();
                        EditLog(shots, itemModel);
                    }
                }
                if (itemModel.ID <= 0)
                {
                    itemModel = new ZWL.BLL.ERPOfficeSupplyItem()
                    {
                        CategoryID = typeModel.ID,
                        ItemName = itemname,
                        Spec = spec,
                        Unit = unit,
                        UnitPrice = price,
                        Description = dec,
                        CreatorTime = Timestamp,
                        CreatorUser = UserName,
                    };
                    itemModel.ID = itemModel.Add();
                    AddLog(itemModel);
                }

                var itemInfo = new ZWL.BLL.ERPOfficeSupplyStockInDetail()
                {
                    StockInID = info.ID,
                    ItemID = itemModel.ID,
                    Name = itemname,
                    Spec = spec,
                    Unit = unit,
                    Quantity = PublicMethod.GetInt(qty),
                    Price = PublicMethod.GetDecimal(price),
                    Total = PublicMethod.GetDecimal(total),
                    Supplier = source,
                    StockInTime = TimeParser.GetFormatDate(indate),
                    StockInUser = cgr,
                    CreatorTime = Timestamp,
                    CreatorUser = UserName,
                };
                itemInfo.ID = itemInfo.Add();
                AddLog(itemInfo);

                var invModel = new ZWL.BLL.ERPOfficeSupplyInventory();
                var invList = Conv<ZWL.BLL.ERPOfficeSupplyInventory>.GetListBySQLWhere("(ItemID={0} or (ItemName='{1}' and Spec='{2}' and Unit='{3}'))".FormatWith(itemModel.ID, itemname, spec, unit));
                if (invList != null && invList.Any())
                {
                    if (invList.Any(x => !x.DeleteMark.HasValue || x.DeleteMark.Value != 1))
                    {
                        invModel = invList.FirstOrDefault(x => !x.DeleteMark.HasValue || x.DeleteMark.Value != 1);
                        var shots = EditShot(invModel);
                        invModel.Quantity += PublicMethod.GetInt(qty);
                        invModel.Update();
                        EditLog(shots, invModel);
                    }
                    else
                    {
                        invModel = invList.FirstOrDefault(x => x.DeleteMark.HasValue && x.DeleteMark.Value == 1);
                        var shots = EditShot(invModel);
                        invModel.DeleteMark = null;
                        invModel.LockedInQuantity = 0;
                        invModel.LockedOutQuantity = 0;
                        invModel.Quantity = PublicMethod.GetInt(qty);
                        invModel.Update();
                        EditLog(shots, invModel);
                    }
                }

                if (invModel.ID <= 0)
                {
                    invModel = new ZWL.BLL.ERPOfficeSupplyInventory()
                    {
                        ItemID = itemModel.ID,
                        Name = itemname,
                        Spec = spec,
                        Unit = unit,
                        Quantity = PublicMethod.GetInt(qty),
                        CreatorTime = Timestamp,
                        CreatorUser = UserName,
                    };
                    invModel.ID = invModel.Add();
                    AddLog(invModel);
                }
            }
            result = true;
        }
        return result;
    }
    #region MyRegion

    #endregion
}