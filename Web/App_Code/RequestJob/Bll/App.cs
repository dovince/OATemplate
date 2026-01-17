using Newtonsoft.Json;
using RequestJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZWL.Common;
using ViewModels;

/// <summary>
/// System 的摘要说明
/// </summary>
namespace RequestJob
{
    public class App : Base, IRequestJob
    {
        public App()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult LatestVersion(HttpRequest Request)
        {
            try
            {
                AppVersionInfoOutput output = null;
                var kModel = new ZWL.BLL.ERPKeyValue();
                kModel = kModel.GetModel("Category='LatestVersion' and Key1='App'");
                if (kModel != null && !kModel.Value1.IsNullOrEmpty())
                {
                    var alist = JsonConvert.DeserializeObject<List<AppVersionInfoOutput>>(kModel.Value1);
                    var item = alist.FirstOrDefault(x => x.appname == "app");
                    if (item != null)
                    {
                        output = item;
                    }
                }

                return JsonResult(true, "", output);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
    }
}