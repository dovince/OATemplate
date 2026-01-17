using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ZWL.Common;
using ZWL.DBUtility;

namespace RequestJob
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class Collections : Base, IRequestJob
    {

        public JsonResult GetAptitudeInfo(HttpRequest Request)
        {
            ZWL.BLL.AptitudeFile result = null;
            var name = Request["name"];
            var sql = string.Format(@"select top 1 a.*  FROM [Aptitude] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID
  where t.StateNow='正在办理' and a.[是否归还]='否' and a.[资质证照名称] like '%{0}%' order by id desc", HttpUtility.UrlDecode(name));
            var dt = DbHelperSQL.GetDataTable(sql);
            var list = DataTableHelper.ConvertTo<ZWL.BLL.AptitudeFile>(dt);
            if (list != null)
            {
                var model = list.FirstOrDefault();
                if (model != null)
                {
                    result = model;
                }
            }
            return JsonResult(true, "", result);
        }

        public JsonResult GetMoreOADongTai(HttpRequest Request)
        {
            List<ZWL.BLL.ERPGongGao> result = null;
            var count = Request["count"];
            var sql1 = string.Format(@"select top 3 * from ERPGongGao where TypeStr='2' and 
                            id not in (select top {0} id from ERPGongGao 
                            where TypeStr='2' order by id desc) order by id desc"
                        , HttpUtility.UrlDecode(count));

            var dt1 = DbHelperSQL.GetDataTable(sql1);
            var list1 = DataTableHelper.ConvertTo<ZWL.BLL.ERPGongGao>(dt1);
            if (list1 != null)
            {
                if (list1 != null && list1.Count > 0)
                {
                    result = list1;
                }
            }
            return JsonResult(true, "", result);
        }

        public JsonResult GetPhotoNews(HttpRequest Request)
        {
            List<ZWL.BLL.ERPPhotoNews> ERPPhotoNewsFileList = new List<ZWL.BLL.ERPPhotoNews>();
            ERPPhotoNewsFileList = GetModelList("");
            return JsonResult(true, "", ERPPhotoNewsFileList);
        }

        public JsonResult GetOfficeSupplyRow(HttpRequest Request)
        {
            var rowIndex = Request["rowIndex"];
            var filePath = HttpContext.Current.Server.MapPath("~/Html/OfficeSupplyRow.html");
            var htmlStr = System.IO.File.ReadAllText(filePath, System.Text.Encoding.UTF8);

            var unitList = DbHelperSQL.GetDataTable("select distinct [Unit] from [ERPOfficeSupplyDetail] where Unit is not null and Unit <>''");
            var replaceUnit = string.Empty;
            if (unitList != null && unitList.Rows.Count > 0)
            {
                var i = 0;
                var defaultItem = string.Empty;
                replaceUnit += string.Format(@"<select class='selectOfficeUint' id='selectUint_{0}' name='selectUint_{0}' style='width:auto;display:inline'>", rowIndex);
                foreach (DataRow item in unitList.Rows)
                {
                    replaceUnit += string.Format(@"<option value='{0}'>{0}</option>", item["Unit"].ToString());
                    if (i == 0)
                    {
                        defaultItem = item["Unit"].ToString();
                    }
                    i++;
                }
                replaceUnit += string.Format(@"<option value='其他'>其他</option></select>
                            <input id='Unit_{0}' name='Unit_{0}' class='selectOfficeUintValue' style='width:40px;display:none' value='{1}' />", rowIndex, defaultItem);

                htmlStr = htmlStr.Replace("{Replace-Display}", "inline");
            }
            else
            {
                replaceUnit += string.Format(@"<input id='Unit_{0}' name='Unit_{0}' style='width:40px;' class='selectOfficeUintValue' />", rowIndex);
                htmlStr = htmlStr.Replace("{Replace-Display}", "none");
            }
            htmlStr = htmlStr.Replace("{Replace-Index}", rowIndex)
                             .Replace("{Replace-Unit}", replaceUnit);

            var typeList = DbHelperSQL.GetDataTable("select distinct [ReservedField2] from [ERPOfficeSupplyDetail] where ReservedField2 is not null and ReservedField2 <>''");
            var replaceType = "";
            if (typeList != null && typeList.Rows.Count > 0)
            {
                var i = 0;
                var defaultItem = string.Empty;
                replaceType += string.Format(@"<select class='selectOfficeUint' id='ReservedField2_{0}' name='ReservedField2_{0}' style='width:auto;display:inline'>", rowIndex);
                foreach (DataRow item in typeList.Rows)
                {
                    replaceType += string.Format(@"<option value='{0}'>{0}</option>", item["ReservedField2"].ToString());
                    if (i == 0)
                    {
                        defaultItem = item["ReservedField2"].ToString();
                    }
                    i++;
                }
            }
            htmlStr = htmlStr.Replace("{Replace-ProductType}", replaceType);

            return JsonResult(true, "", htmlStr);
        }

        public JsonResult OfficeSupplyItemSpecify(HttpRequest Request)
        {
            var page = PublicMethod.GetInto(Request["page"]);
            var rows = PublicMethod.GetInto(Request["rows"]);
            var frules = Request["filterRules"];
            var sqlWhere = " (EnabledMark is null or EnabledMark<>0) and (DeleteMark is null or DeleteMark<>1)";
            var flist = new List<FilterRule>();
            if (!frules.IsNullOrEmpty())
            {
                var serializer = new JavaScriptSerializer();
                flist = serializer.Deserialize<List<FilterRule>>(frules);
                var conditions = new List<string>();
                foreach (var rule in flist)
                {
                    switch (rule.op)
                    {
                        case "contains":
                            conditions.Add(@"{0} LIKE '%{1}%'".FormatWith(rule.field, rule.value));
                            break;
                            // 添加其他操作符处理
                            // case "equals":
                            // case "starts with":
                            // case "ends with":
                            // etc.
                    }
                }

                if (conditions.Count > 0)
                {
                    sqlWhere += " AND " + string.Join(" AND ", conditions);
                }
            }
            var item = new ZWL.BLL.ERPOfficeSupplyItem();
            var list = new List<ZWL.BLL.ERPOfficeSupplyItem>();
            var pager = item.GetListAndPaging(sqlWhere, page, rows, "ItemName asc");
            if (pager.ExecuteToDataTable())
            {
                var dt = (DataTable)pager.Result;
                if (dt != null && dt.Rows.Count > 0)
                    list = DataTableHelper.ConvertTo_1<ZWL.BLL.ERPOfficeSupplyItem>(dt);
            }
            //var result = "{\"total\":\"28\",\"rows\":[{\"itemid\":\"EST-1\",\"productid\":\"FI-SW-01\",\"listprice\":\"16.50\",\"unitcost\":\"10.00\",\"status\":\"P\",\"attr1\":\"Large\"},{\"itemid\":\"EST-10\",\"productid\":\"K9-DL-01\",\"listprice\":\"18.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Spotted Adult Female\"},{\"itemid\":\"EST-11\",\"productid\":\"RP-SN-01\",\"listprice\":\"18.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Venomless\"},{\"itemid\":\"EST-12\",\"productid\":\"RP-SN-01\",\"listprice\":\"18.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Rattleless\"},{\"itemid\":\"EST-13\",\"productid\":\"RP-LI-02\",\"listprice\":\"18.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Green Adult\"},{\"itemid\":\"EST-14\",\"productid\":\"FL-DSH-01\",\"listprice\":\"58.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Tailless\"},{\"itemid\":\"EST-15\",\"productid\":\"FL-DSH-01\",\"listprice\":\"23.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"With tail\"},{\"itemid\":\"EST-16\",\"productid\":\"FL-DLH-02\",\"listprice\":\"93.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Adult Female\"},{\"itemid\":\"EST-17\",\"productid\":\"FL-DLH-02\",\"listprice\":\"93.50\",\"unitcost\":\"12.00\",\"status\":\"P\",\"attr1\":\"Adult Male\"},{\"itemid\":\"EST-18\",\"productid\":\"AV-CB-01\",\"listprice\":\"193.50\",\"unitcost\":\"92.00\",\"status\":\"P\",\"attr1\":\"Adult Male\"}]}";

            return JsonResult(true, "", list);
        }
        public JsonResult SetChuChaiNoReceived(HttpRequest Request)
        {
            var result = false;
            var msg = "操作成功";
            var id = PublicMethod.GetInto(Request["ID"]);
            var received = Request["Received"];
            var comment = Request["Comment"];
            if (received.IsNullOrEmpty()) received = "是";
            var fmodel = new ZWL.BLL.FanTangJiuCanRecordReport();
            fmodel.GetModel(id);
            if (fmodel.ID > 0)
            {
                fmodel.Received = received;
                fmodel.Comment = comment;
                result = fmodel.Update();
                if (!result)
                {
                    msg = "操作失败";
                }
            }
            else
                msg = "请求失败";
            return JsonResult(true, msg, result.ToString().ToLower());
        }
        #region Private Functions
        private class FilterRule
        {
            public string field { get; set; }
            public string op { get; set; }
            public string value { get; set; }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        private List<ZWL.BLL.ERPPhotoNews> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP 5 [ID],[PicName],[PicDescribe],[ImgPath],[PicHref],[UploadDate] ");
            strSql.Append(" FROM ERPPhotoNews ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by UploadDate desc ");
            var ds = ZWL.DBUtility.DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return ZWL.Common.DataTableHelper.ConvertTo<ZWL.BLL.ERPPhotoNews>(dt);
            }
            return new List<ZWL.BLL.ERPPhotoNews>();
        }
        #endregion
    }

}