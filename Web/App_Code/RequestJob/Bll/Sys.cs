using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RequestJob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ViewModels;
using ZWL.Common;

/// <summary>
/// System 的摘要说明
/// </summary>
namespace RequestJob
{
    public class Sys : Base, IRequestJob
    {
        public Sys()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public JsonResult LatestVersion(HttpRequest Request)
        {
            try
            {
                var username = string.Empty;
                var dept = string.Empty;
                var canjiagongzuotime = string.Empty;
                var gongling = string.Empty;
                var name = HttpUtility.UrlDecode(Request["username"]);
                AppVersionInfoOutput output = null;
                var kModel = new ZWL.BLL.ERPKeyValue();
                var klist = kModel.GetModelList("Category='LatestVersion' and Key1='App'");
                if (klist != null && klist.Any())
                {
                    kModel = klist[0];
                }
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
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult SendSmsCode(HttpRequest Request)
        {
            var msg = "";
            var input = Util.MapRequestToModel<SmsCodeInput>(Request);
            if (ValidateSendSmsCode(Request, ref msg))
            {
                try
                {
                    var vlist = Conv<ZWL.BLL.ValidateCode>.GetListBySQLWhere("PhoneNumber='{0}' and ExpiresTime>GETDATE() ".FormatWith(input.mobile));
                    if (vlist != null && vlist.Any())
                    {
                        //将旧code过期
                        foreach (var v in vlist)
                        {
                            v.ExpiresTime = v.ExpiresTime.Value.AddSeconds(-DateTime.Now.Subtract(v.ExpiresTime.Value).TotalSeconds);
                            v.Update();
                        }
                    }
                    var code = Util.GenerateRandomCode(6);
                    var umodel = new ZWL.BLL.ERPUser();
                    var ulist = umodel.GetListModel("JiaTingDianHua='{0}'".FormatWith(input.mobile));
                    if (ulist != null && ulist.Any())
                    {
                        umodel = ulist[0];
                        var msgs = new JObject();
                        msgs.Add("code", code);
                        Mobile.SendOneMSM(input.mobile, msgs.ToString(), ConfigurationManager.AppSettings["TemplateCode_LoginSmsCode"].ToString());
                    }
                    var vmodel = new ZWL.BLL.ValidateCode();
                    vmodel.PhoneNumber = input.mobile;
                    vmodel.Code = code;
                    vmodel.Time = Request.RequestContext.HttpContext.Timestamp;
                    vmodel.UserName = umodel.UserName;
                    vmodel.ExpiresTime = vmodel.Time.Value.AddMinutes(3);
                    vmodel.IsUsed = 0;
                    vmodel.Item = "Login";
                    vmodel.ID = vmodel.Add();

                    return JsonResult(true, "", code);
                }
                catch (Exception e)
                {
                    return JsonResult(false, e.Message);
                }
            }
            else
            {
                return JsonResult(false, msg);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult ValidateSmsCode(HttpRequest Request)
        {
            var msg = "";
            var input = Util.MapRequestToModel<SmsCodeInput>(Request);
            if (ValidatePostSmsCode(Request, ref msg))
            {
                try
                {
                    return JsonResult(true, "", input.code);
                }
                catch (Exception e)
                {
                    return JsonResult(false, e.Message);
                }
            }
            else
            {
                return JsonResult(false, msg);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult GetAccessToken(HttpRequest Request)
        {
            var msg = "";
            var input = Util.MapRequestToModel<AccessTokenInput>(Request);
            if (ValidateAccessTokenInput(Request, ref msg))
            {
                try
                {
                    //ZWL.BLL.ApiClients apiClients = null;
                    var sql = @"select * from ApiClients where Status=1 and ClientId='{0}' and ClientSecret='{1}' order by ID desc".FormatWith(input.ClientId, input.ClientSecret);
                    var infolist = Conv<ZWL.BLL.ApiClients>.GetList(sql);
                    //if (apiClients == null)
                    //{
                    //    var temp = Conv<ZWL.BLL.ApiClients>.GetModel("select top 1 * from ApiClients where ClientId='{0}'".FormatWith(input.ClientId));
                    //    var info = new ZWL.BLL.ApiClients
                    //    {
                    //        ClientId = input.ClientId,
                    //        ClientSecret = PublicMethod.GetNewGuid().ToLower(),
                    //        SystemName = temp.SystemName,
                    //        Status = 1,
                    //        CreatedAt = DateTime.Now,
                    //    };
                    //    info.ID = info.Add();
                    //    return JsonResult(true, "刷新ClientSecret", new { Type = "RefreshClientSecret", Info = info.ClientSecret });
                    //}
                    if (infolist != null && infolist.Any())
                    {
                        var tokenValue = "";
                        var sqlWhere = @"select top 1 * from Token where GETDATE()<ExpiresTime and EnabledMark=1 and UserName='{0}' and Type='ApiClient' order by ID desc ".FormatWith(input.ClientId);
                        var temp = Conv<ZWL.BLL.Token>.GetModel(sqlWhere);
                        if (temp != null)
                        {
                            tokenValue = temp.TokenValue;
                        }
                        else
                        {
                            var exp = DateTime.Now.AddYears(10);
                            var token = JWTUtil.GenerateJwt(input.ClientId, DateTime.Now, exp);
                            var tmodel = new ZWL.BLL.Token()
                            {
                                CreatedTime = DateTime.Now,
                                EnabledMark = 1,
                                UserName = input.ClientId,
                                TokenValue = token,
                                ExpiresTime = exp,
                                DeviceId = Request.UserAgent,
                                Type = "ApiClient"
                            };
                            tmodel.ID = tmodel.Add();
                            var logModel = new ZWL.BLL.TokenLogs()
                            {
                                TokenID = tmodel.ID,
                                AccessedTime = DateTime.Now,
                                Action = "Refresh",
                                IPAddress = Request.UserHostAddress,
                            };
                            logModel.ID = logModel.Add();
                        }
                        return JsonResult(true, "获取Token成功。", new { Type = "GenerateToken", Info = tokenValue.Replace("bearer ", "") });
                    }
                    return JsonResult(false, "初始化参数ERROR");
                }
                catch (Exception e)
                {
                    return JsonResult(false, e.Message);
                }
            }
            else
            {
                return JsonResult(false, msg);
            }
        }

        #region Private Method
        private bool ValidateSendSmsCode(HttpRequest Request, ref string msg)
        {
            try
            {
                var input = Util.MapRequestToModel<SmsCodeInput>(Request);
                if (input.mobile.IsNullOrEmpty())
                {
                    msg = "手机号码不能为空";
                    return false;
                }
                else
                {
                    if (Util.IdentifyCarrier(input.mobile) == Carrier.Unknown)
                    {
                        msg = "手机号码格式不正确";
                        return false;
                    }
                    if (!IsFrequentRequestSmsCode(Request, input.mobile, ref msg))
                    {
                        return false;
                    }
                    var umodel = new ZWL.BLL.ERPUser();
                    var vlist = umodel.GetListModel("JiaTingDianHua='{0}'".FormatWith(input.mobile));
                    if (vlist == null || !vlist.Any())
                    {
                        msg = "手机号码不存在";
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }
            return true;
        }
        private bool IsFrequentRequestSmsCode(HttpRequest Request, string phoneNumber, ref string msg)
        {
            // 检查 60 秒内是否有重复请求
            var timestamp = Request.RequestContext.HttpContext.Timestamp;
            var vlist = Conv<ZWL.BLL.ValidateCode>.GetList("select top 10 * from ValidateCode where PhoneNumber='{0}' and [Item]='Login' order by ID desc ".FormatWith(phoneNumber));
            if (vlist != null && vlist.Any())
            {
                var templist = vlist.Where(x => x.Time >= timestamp.AddMinutes(-10));
                if (templist != null && templist.Count() > 5)
                {
                    msg = "操作过于频繁，过一会再试。";
                    return false;
                }
                var lastitem = vlist.FirstOrDefault();
                if (lastitem.Time.HasValue && timestamp.Subtract(lastitem.Time.Value).TotalMinutes <= 1)
                {
                    msg = "操作过于频繁，过一会再试。";
                    return false;
                }
            }

            return true;
        }
        private bool ValidatePostSmsCode(HttpRequest Request, ref string msg)
        {
            try
            {
                var input = Util.MapRequestToModel<SmsCodeInput>(Request);
                if (input.mobile.IsNullOrEmpty())
                {
                    msg = "手机号码不能为空";
                    return false;
                }
                else
                {
                    if (Util.IdentifyCarrier(input.mobile) == Carrier.Unknown)
                    {
                        msg = "手机号码格式不正确";
                        return false;
                    }
                    var umodel = new ZWL.BLL.ERPUser();
                    var ulist = umodel.GetListModel("JiaTingDianHua='{0}'".FormatWith(input.mobile));
                    if (ulist == null || !ulist.Any())
                    {
                        msg = "手机号码不存在";
                        return false;
                    }
                    var vlist = Conv<ZWL.BLL.ValidateCode>.GetListBySQLWhere("PhoneNumber='{0}' and Code='{1}' and IsUsed=0 and [Item]='Login' ".FormatWith(input.mobile, input.code));
                    if (vlist == null || !vlist.Any())
                    {
                        msg = "手机号码与短信验证码不匹配";
                        return false;
                    }
                    else
                    {
                        var item = vlist[0];
                        if (!item.ExpiresTime.HasValue || item.ExpiresTime.HasValue && item.ExpiresTime.Value < DateTime.Now)
                        {
                            msg = "验证码已失效";
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }
            return true;
        }
        private bool ValidateAccessTokenInput(HttpRequest Request, ref string msg)
        {
            try
            {
                var input = Util.MapRequestToModel<AccessTokenInput>(Request);
                if (input.ClientId.IsNullOrEmpty() || input.ClientSecret.IsNullOrEmpty())
                {
                    msg = "参数初始化Error";
                    return false;
                }
                var sql = @"select top 1 * from ApiClients where ClientId='{0}' and ClientSecret='{1}' order by ID desc ".FormatWith(input.ClientId, input.ClientSecret);
                var info = Conv<ZWL.BLL.ApiClients>.GetModel(sql);
                if (info == null)
                {
                    msg = "参数初始化Error";
                    return false;
                }
                if (info.Status == 0)
                {
                    msg = "参数初始化Error";
                    return false;
                }
                //var sqlWhere = @"select top 1 * from Token where GETDATE()<ExpiresTime and EnabledMark=1 and UserName='{0}' and Type='APIClient' order by ID desc ".FormatWith(input.ClientId);
                //var temp = Conv<ZWL.BLL.Token>.GetModel(sqlWhere);
                //if (temp == null)
                //{
                //    msg = "无有效token。";
                //    return false;
                //}
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }
            return true;
        }
        #endregion
        #region ViewModel
        private class SmsCodeInput
        {
            private string _mobile = "";
            private string _code = "";
            public string mobile
            {
                get
                {
                    if (_mobile != null)
                    {
                        _mobile = _mobile.Trim().Replace(" ", "");
                    }
                    return _mobile;
                }
                set { _mobile = value; }
            }
            public string code
            {
                get
                {
                    if (_code != null)
                    {
                        _code = _code.Trim().Replace(" ", "");
                    }
                    return _code;
                }
                set { _code = value; }
            }
        }
        private class AccessTokenInput
        {
            private string _clientId = "";
            private string _clientSecret = "";
            public string ClientId
            {
                get
                {
                    if (_clientId != null)
                    {
                        _clientId = _clientId.Trim().Replace(" ", "");
                    }
                    return _clientId;
                }
                set { _clientId = value; }
            }
            public string ClientSecret
            {
                get
                {
                    if (_clientSecret != null)
                    {
                        _clientSecret = _clientSecret.Trim().Replace(" ", "");
                    }
                    return _clientSecret;
                }
                set { _clientSecret = value; }
            }
        }
        #endregion
    }
}