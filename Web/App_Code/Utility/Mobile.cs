using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using ZWL.Common;

/// <summary>
/// Mobile 的摘要说明
/// </summary>
public class Mobile
{
    private static String product = ConfigurationManager.AppSettings["product"].ToString();//（短信产品名固定，无需修改）
    private static String domain = ConfigurationManager.AppSettings["domain"].ToString();//短信API产品域名（接口地址固定，无需修改）
    private static String accessKeyId = ConfigurationManager.AppSettings["accessKeyId"].ToString();//你的accessKeyId，参考本文档步骤2
    private static String accessKeySecret = ConfigurationManager.AppSettings["accessKeySecret"].ToString();//你的accessKeySecret，参考本文档步骤2

    private static String SignName = ConfigurationManager.AppSettings["SignName"].ToString();

    public Mobile()
    {
    }

    /// <summary>
    /// 发送单条信息
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="smsMsg"></param>
    /// <returns></returns>
    public static string SendOneMSM(string phone, string smsMsg, string TemplateCode)
    {
        if ("xxxx" == accessKeyId)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, "还没配置短信", "", "", DateTime.Now, new Nullable<DateTime>());
            return "OK";
        }
        IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
        // SingleSendSmsRequest request = new SingleSendSmsRequest();
        //初始化ascClient,暂时不支持多region（请勿修改）

        DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
        IAcsClient acsClient = new DefaultAcsClient(profile);
        SendSmsRequest request = new SendSmsRequest();
        try
        {
            //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
            request.PhoneNumbers = phone;
            //必填:短信签名-可在短信控制台中找到
            request.SignName = SignName;
            //必填:短信模板-可在短信控制台中找到
            request.TemplateCode = TemplateCode;
            //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
            // request.TemplateParam = "{\"name\":\"123\",\"store\":\"12222\"}";
            request.TemplateParam = smsMsg;
            //请求失败这里会抛ClientException异常
            SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
            WriteDataInDB(phone, smsMsg, TemplateCode, sendSmsResponse.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            //ZWL.Common.PublicMethod.WriteTextLog("phoneNum：" + phone + " ,msgs：" + smsMsg.ToString() + " ,templateCode=" + TemplateCode + " ,sendstate=" + sendSmsResponse.Message + "\r\n");
            return sendSmsResponse.Message;
        }
        catch (ServerException e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
        catch (ClientException e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
        catch (Exception e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
    }

    /// <summary>
    /// 发送单条信息，可以获取短信回执(有延迟的，为了获取短信回执)
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="smsMsg"></param>
    /// <returns></returns>
    public static string SendOneMSM_HasDelay(string phone, string smsMsg, string TemplateCode)
    {
        IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
        // SingleSendSmsRequest request = new SingleSendSmsRequest();
        //初始化ascClient,暂时不支持多region（请勿修改）

        DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
        IAcsClient acsClient = new DefaultAcsClient(profile);
        SendSmsRequest request = new SendSmsRequest();
        try
        {
            //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
            request.PhoneNumbers = phone;
            //必填:短信签名-可在短信控制台中找到
            request.SignName = SignName;
            //必填:短信模板-可在短信控制台中找到
            request.TemplateCode = TemplateCode;
            //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
            request.TemplateParam = smsMsg;
            //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
            //request.OutId = "yourOutId";
            //请求失败这里会抛ClientException异常
            SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
            if (sendSmsResponse.Code != null && sendSmsResponse.Code == "OK")
            {
                QuerySendDetailsResponse queryReponse = querySendDetails(sendSmsResponse.BizId, phone);
                //由于网络问题，获取短信回执会有点
                while (true)
                {
                    if (!queryReponse.SmsSendDetailDTOs[0].SendStatus.ToString().Equals("1"))
                    {
                        break;
                    }
                    else
                    {
                        queryReponse = querySendDetails(sendSmsResponse.BizId, phone);
                    }
                }
                //记录短信发送状态
                foreach (QuerySendDetailsResponse.QuerySendDetails_SmsSendDetailDTO smsSendDetailDTO in queryReponse.SmsSendDetailDTOs)
                {
                    WriteDataInDB(smsSendDetailDTO.PhoneNum, smsSendDetailDTO.Content, smsSendDetailDTO.TemplateCode, sendSmsResponse.Message + "|" + smsSendDetailDTO.SendStatus.ToString(),
                        smsSendDetailDTO.ErrCode, smsSendDetailDTO.OutId,
                        !string.IsNullOrEmpty(smsSendDetailDTO.SendDate) ? Convert.ToDateTime(smsSendDetailDTO.SendDate) : new Nullable<DateTime>(),
                        !string.IsNullOrEmpty(smsSendDetailDTO.ReceiveDate) ? Convert.ToDateTime(smsSendDetailDTO.ReceiveDate) : new Nullable<DateTime>());
                    //Console.WriteLine("Content=" + smsSendDetailDTO.Content);
                    //Console.WriteLine("ErrCode=" + smsSendDetailDTO.ErrCode);
                    //Console.WriteLine("OutId=" + smsSendDetailDTO.OutId);
                    //Console.WriteLine("PhoneNum=" + smsSendDetailDTO.PhoneNum);
                    //Console.WriteLine("ReceiveDate=" + smsSendDetailDTO.ReceiveDate);
                    //Console.WriteLine("SendDate=" + smsSendDetailDTO.SendDate);
                    //Console.WriteLine("SendStatus=" + smsSendDetailDTO.SendStatus);
                    //Console.WriteLine("Template=" + smsSendDetailDTO.TemplateCode);
                }
                if (queryReponse.Code != "OK")
                {
                    return queryReponse.Message;
                }
                else
                {
                    return "OK";
                }
            }
            else
            {
                WriteDataInDB(phone, smsMsg, TemplateCode, sendSmsResponse.Message, "", "", DateTime.Now, new Nullable<DateTime>());
                //ZWL.Common.PublicMethod.WriteTextLog("phoneNum：" + phone + " ,msgs：" + smsMsg.ToString() + " ,templateCode=" + TemplateCode + " ,sendstate=" + sendSmsResponse.Message + "\r\n");
            }
            return sendSmsResponse.Message;
        }
        catch (ServerException e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
        catch (ClientException e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
        catch (Exception e)
        {
            WriteDataInDB(phone, smsMsg, TemplateCode, e.Message, "", "", DateTime.Now, new Nullable<DateTime>());
            return e.Message;
        }
    }

    /// <summary>
    /// 批量发送短信
    /// </summary>
    /// <param name="phoneList"></param>
    /// <param name="smsMsgListstr"></param>
    /// <returns></returns>
    //public static string sendSms(string phoneList, string smsMsgList, string TemplateCode)
    //{
    //    IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
    //    DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
    //    IAcsClient acsClient = new DefaultAcsClient(profile);
    //    SendBatchSmsRequest request = new SendBatchSmsRequest();
    //    SendBatchSmsResponse response = null;
    //    try
    //    {
    //        //必填:待发送手机号。支持JSON格式的批量调用，批量上限为100个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
    //        //request.PhoneNumberJson = "[\"13246324501\",\"13620306130\"]";
    //        request.PhoneNumberJson = phoneList;
    //        //必填:短信签名-支持不同的号码发送不同的短信签名
    //        request.SignNameJson = SignName;
    //        //必填:短信模板-可在短信控制台中找到
    //        request.TemplateCode = TemplateCode;
    //        //友情提示:如果JSON中需要带换行符,请参照标准的JSON协议对换行符的要求,比如短信内容中包含\r\n的情况在JSON中需要表示成\\r\\n,否则会导致JSON在服务端解析失败
    //        //request.TemplateParamJson = "[{\"mtname\": \"Ben\", \"store\": \"工作1\" },{ \"name\": \"Tony\",\"store\": \"工作2\" }]";
    //        request.TemplateParamJson = smsMsgList;
    //        //请求失败这里会抛ClientException异常
    //        response = acsClient.GetAcsResponse(request);
    //        return response.Message;
    //    }
    //    catch (ServerException e)
    //    {
    //        return e.ErrorCode;
    //    }
    //    catch (ClientException e)
    //    {
    //        return "ErrorCode=" + e.ErrorCode + " Messages=" + e.Message;
    //    }

    //}


    public static SendBatchSmsResponse sendSms(string phone, string smsMsg, string TemplateCode)
    {
        IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
        DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);

        IAcsClient acsClient = new DefaultAcsClient(profile);
        SendBatchSmsRequest request = new SendBatchSmsRequest();
        //request.Protocol = ProtocolType.HTTPS;
        //request.TimeoutInMilliSeconds = 1;

        SendBatchSmsResponse response = null;
        try
        {

            //必填:待发送手机号。支持JSON格式的批量调用，批量上限为100个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
            request.PhoneNumberJson = phone;
            //必填:短信签名-支持不同的号码发送不同的短信签名
            request.SignNameJson = SignName;
            //必填:短信模板-可在短信控制台中找到
            request.TemplateCode = TemplateCode;
            //必填:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
            //友情提示:如果JSON中需要带换行符,请参照标准的JSON协议对换行符的要求,比如短信内容中包含\r\n的情况在JSON中需要表示成\\r\\n,否则会导致JSON在服务端解析失败
            request.TemplateParamJson = smsMsg;
            //可选-上行短信扩展码(扩展码字段控制在7位或以下，无特殊需求用户请忽略此字段)
            //request.SmsUpExtendCodeJson = "[\"90997\",\"90998\"]";

            //请求失败这里会抛ClientException异常
            response = acsClient.GetAcsResponse(request);

        }
        catch (ServerException e)
        {
            //Console.Write(e.ErrorCode);
        }
        catch (ClientException e)
        {
            //Console.Write(e.ErrorCode);
            //Console.Write(e.Message);
        }
        return response;
    }

    public static QuerySendDetailsResponse querySendDetails(string bizId, string phonenum)
    {
        //初始化acsClient,暂不支持region化
        IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
        DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
        IAcsClient acsClient = new DefaultAcsClient(profile);
        //组装请求对象
        QuerySendDetailsRequest request = new QuerySendDetailsRequest();
        //必填-号码
        request.PhoneNumber = phonenum;
        //可选-流水号
        request.BizId = bizId;
        //必填-发送日期 支持30天内记录查询，格式yyyyMMdd       
        request.SendDate = DateTime.Now.ToString("yyyyMMdd");
        //必填-页大小
        request.PageSize = 10;
        //必填-当前页码从1开始计数
        request.CurrentPage = 1;

        QuerySendDetailsResponse querySendDetailsResponse = null;
        try
        {
            querySendDetailsResponse = acsClient.GetAcsResponse(request);
        }
        catch (ServerException e)
        {
            Console.WriteLine(e.ErrorCode);
        }
        catch (ClientException e)
        {
            Console.WriteLine(e.ErrorCode);
        }
        return querySendDetailsResponse;
    }

    public static string LoginCheck(string ToUserList, string code)
    {
        string resultMsg = "";
        string phoneNum = ZWL.DBUtility.DbHelperSQL.GetSHSL("select JiaTingDianHua from ERPUser where UserName in('" + ToUserList.Replace(",", "','") + "')");
        string templateCode = ConfigurationManager.AppSettings["TemplateCode_YanZhangCode"].ToString();
        if (!string.IsNullOrEmpty(phoneNum))
        {
            JObject msgs = new JObject();
            msgs.Add("code", code);
            resultMsg = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
        }
        else
        {
            resultMsg = "接收方的手机号码为空！";
        }
        return resultMsg;
    }

    /// <summary>
    /// 内部工作通知短信
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="ContentStr"></param>
    /// <param name="flag"></param>
    public static string SendSMS(string FaSongUser, string ToUserList, string ContentStr)
    {
        string resultMsg = "";
        JObject msgs = new JObject();
        string templateCode = "";
        string name = "";
        string workname = "";
        string time = "";
        string regstr = "[^()]*";
        Regex reg = new Regex(regstr);
        var mat = reg.Matches(ContentStr);
        for (int i = 0; i < mat.Count; i++)
        {
            var item = mat[i].ToString();
            if (!string.IsNullOrEmpty(item))
            {
                if (item.Contains("--"))
                {
                    name = item.Split('-')[0];
                    workname = item.Split('-')[2];
                }
                else if (!item.Contains("工作"))
                {
                    try
                    {
                        time = !string.IsNullOrEmpty(item) ? Convert.ToDateTime(item).ToString("yyyy-MM-dd") : "";
                    }
                    catch
                    {
                        time = "";
                    }
                }
            }
        }
        if (ContentStr.Contains("您有新的工作需要办理"))
        {
            templateCode = ConfigurationManager.AppSettings["TemplateCode_BanLi"].ToString();
            workname = string.IsNullOrEmpty(workname) ? ContentStr.Replace("您有新的工作需要办理！", "") : workname;
            msgs.Add("name", name);
            msgs.Add("time", time);
            msgs.Add("workname", workname);
        }
        else
        {
            templateCode = ConfigurationManager.AppSettings["TemplateCode_WorkState"].ToString();
            if (ContentStr.Contains("您的工作已经正常结束"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您的工作已经正常结束！", "") : workname;
                msgs.Add("state", "正常结束");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("您的工作已经被强制结束"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您的工作已经被强制结束！", "") : workname;
                msgs.Add("state", "被强制结束");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("您的工作已经被驳回"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您的工作已经被驳回！", "") : workname;
                msgs.Add("state", "被驳回");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("您的工作没有通过审批"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您的工作没有通过审批！", "") : workname;
                msgs.Add("state", "不通过");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("您有工作") && ContentStr.Contains("通过") && ContentStr.Contains("审核进入下一节点"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_WorkPassCode"].ToString();
                var list = GetMessagePairs(ContentStr);
                if (list.Count >= 3)
                {
                    msgs.Add("workname", workname);
                    msgs.Add("username", list[2]);
                    msgs.Add("jiedian", list[3]);
                }
            }
            //先不发送超时短信
            //else if (ContentStr.Contains("您有工作未办理，已超时请及时处理"))
            //{
            //    workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您有工作未办理，已超时请及时处理！", "") : workname;
            //    msgs.Add("state", "超时请处理");
            //    msgs.Add("workname", workname);
            //    msgs.Add("time", time);
            //}
            else if (ContentStr.Contains("HeTongDaoZhangTemplate"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_HeTongDaoZhangCode"].ToString();
                var data = ContentStr.Replace("HeTongDaoZhangTemplate_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("xmkgaqtxaqz"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_XMKGAQTXSmsCode"].ToString();
                var data = ContentStr.Replace("xmkgaqtxaqz_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("申请使用车辆"))//{0}_{1}申请使用车辆,人数{2},自驾({3}),时间{4}至{5},目的地{6}
            {
                //${username}申请使用车辆,人数${usernum},自驾(${selfdriving}),时间${starttime}至${endtime},目的地${address}
                templateCode = ConfigurationManager.AppSettings["TemplateCode_CheLiangShiYong"].ToString();
                var list = ContentStr.Split(',');
                var list1 = list[3].ToString().Split('至');
                msgs.Add("username", list[0].ToString().Replace("申请使用车辆", ""));
                msgs.Add("usernum", list[1].ToString().Replace("人数", ""));
                msgs.Add("selfdriving", list[2].ToString().Replace("自驾(", "").TrimEnd(')'));
                msgs.Add("starttime", list1[0].ToString().Replace("时间", ""));
                msgs.Add("endtime", list1[1].ToString());
                msgs.Add("address", list[4].ToString().Replace("目的地", ""));
            }
            else if (ContentStr.Contains("您有新的文件需要接收"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您有新的文件需要接收！", "") : workname;
                msgs.Add("state", "开始，需要接受文件");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("您有文件快要超时"))
            {
                workname = string.IsNullOrEmpty(workname) ? ContentStr = ContentStr.Replace("您有文件快要超时", "") : workname;
                msgs.Add("state", "快要超时，需要接收文件");
                msgs.Add("workname", workname);
                msgs.Add("time", time);
            }
            else if (ContentStr.Contains("TemplateCode_LaoWuHeTongDaoQiNotice"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_LaoWuHeTongDaoQiNotice"].ToString();
                var data = ContentStr.Replace("TemplateCode_LaoWuHeTongDaoQiNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("TemplateCode_BanCanYongCanYiChangNotice"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_BanCanYongCanYiChangNotice"].ToString();
                var data = ContentStr.Replace("TemplateCode_BanCanYongCanYiChangNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("TemplateCode_ChuChaiYongCanYiChangNotice"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_ChuChaiYongCanYiChangNotice"].ToString();
                var data = ContentStr.Replace("TemplateCode_ChuChaiYongCanYiChangNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("TemplateCode_BanLiTuiXiuNotice"))
            {
                templateCode = ConfigurationManager.AppSettings["TemplateCode_BanLiTuiXiuNotice"].ToString();
                var data = ContentStr.Replace("TemplateCode_BanLiTuiXiuNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("BirthdayNotice"))
            {
                //亲爱的${name}, 在这个特别的日子，祝您生日快乐，一生平安！
                templateCode = ConfigurationManager.AppSettings["TemplateCode_BirthdayNotice"].ToString();
                var data = ContentStr.Replace("BirthdayNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("ProjectReportRequiredNotice"))
            {
                //您有${worknum}个项目[${projectno}]超过登记日期30天尚未提交设计审查、安全报告！请及时补充审批流程。
                //您有${worknum}个${workname}超过${days}天${flowstate}！请及时${handlecontent}。(详细:${details})  20230220
                templateCode = ConfigurationManager.AppSettings["TemplateCode_ProjectReportRequiredNotice"].ToString();
                var data = ContentStr.Replace("ProjectReportRequiredNotice_", "").FromBase64String();
                msgs = JObject.Parse(data);
            }
            else if (ContentStr.Contains("如需在饭堂就餐，请尽快登录OA完成报餐"))
            {
                //亲爱的${name}, 在这个特别的日子，祝您生日快乐，一生平安！
                templateCode = ConfigurationManager.AppSettings["TemplateCode_BanCanNotice"].ToString();
                var username = ContentStr.Replace("您好，如需在饭堂就餐，请尽快登录OA完成报餐", "");
                msgs.Add("name", username);
            }
            else if (ContentStr.Contains("发现当天已经报餐，请尽快在OA取消当天的报餐。"))
            {
                //亲爱的${name}, 在这个特别的日子，祝您生日快乐，一生平安！
                templateCode = ConfigurationManager.AppSettings["TemplateCode_BanCanNeedCancelNotice"].ToString();
                var username = ContentStr.Replace("发现当天已经报餐，请尽快在OA取消当天的报餐。", "");
                msgs.Add("name", username);
            }
            else
            {
                //系统通知：您的同事${ fromuser} 发送了一条短信，工作名称如下：${ workname}。
                templateCode = ConfigurationManager.AppSettings["TemplateCode_InMsg"].ToString();
                msgs.Add("fromuser", ZWL.Common.PublicMethod.GetUserName());
                msgs.Add("workname", ContentStr);
            }
        }

        //根据用户名列表获取手机号码 admin,test,zwl,test123
        var toUsers = string.Empty;
        //发送手机信息   经营管理科所有人员  除了人事管理模块，其余都工作流程都不需要短信提醒  2022.10.9
        var ulist = ZWL.Common.Conv<ZWL.BLL.ERPUser>.GetListBySQLWhere("Department ='经营管理科'");
        var users = ToUserList.Split(',');
        foreach (var item in users)
        {
            if (!ulist.Any(r => r.UserName.Contains(item)) ||
                ulist.Any(e => e.UserName.Contains(item)) && ContentStr.Contains("祝您生日快乐，一生平安"))//
                toUsers += item + ",";
        }
        toUsers = toUsers.TrimEnd(',');
        var bianliangchangdulist = new List<string> { "xmkgaqtxaqz_" };
        var MyDT = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,JiaTingDianHua from ERPUser where UserName in('" + toUsers.Replace(",", "','") + "')");
        for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
        {
            string username = MyDT.Tables[0].Rows[i]["UserName"].ToString();
            string phoneNum = MyDT.Tables[0].Rows[i]["JiaTingDianHua"].ToString();
            if (msgs.Count > 0 && !string.IsNullOrEmpty(phoneNum))
            {
                foreach (var sitem in msgs)
                {
                    if(!bianliangchangdulist.Any(item => ContentStr.StartsWith(item)))
                    {
                        if (msgs[sitem.Key].ToString().Length > 35)
                        {
                            msgs[sitem.Key] = msgs[sitem.Key].ToString().Substring(0, 34) + "…";
                        }
                    }
                }
                resultMsg = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
            else
            {
                if (string.IsNullOrEmpty(phoneNum))
                {
                    resultMsg = "接收方的手机号码为空！";
                }
                if (msgs.Count <= 0)
                {
                    resultMsg = "请检查模板内容与模板参数是否匹配！";
                }
            }
        }
        return resultMsg;
    }



    /// <summary>
    /// 发送内部短信
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="ContentStr"></param>
    /// <returns></returns>
    public static string SendSMS_InMsg(string FaSongUser, string ToUserList, string ContentStr)
    {
        string resultMsg = "";
        string nullphone = "";
        string templateCode = ConfigurationManager.AppSettings["TemplateCode_InMsg"].ToString();
        //根据用户名列表获取手机号码 admin,test,zwl,test123
        DataSet MyDT = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,JiaTingDianHua from ERPUser where UserName in('" + ToUserList.Replace(",", "','") + "')");
        for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
        {
            string username = MyDT.Tables[0].Rows[i]["UserName"].ToString();
            string phoneNum = MyDT.Tables[0].Rows[i]["JiaTingDianHua"].ToString();
            if (!string.IsNullOrEmpty(phoneNum))
            {
                JObject msgs = new JObject();
                msgs.Add("fromuser", FaSongUser);
                msgs.Add("msg", ContentStr);
                resultMsg = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
            else
            {
                nullphone = string.IsNullOrEmpty(nullphone) ? username : "," + username;
            }
        }
        if (!string.IsNullOrEmpty(nullphone))
        {
            resultMsg = resultMsg + "以下用户没有手机号码而没有发送：" + nullphone;
        }
        if (string.IsNullOrEmpty(resultMsg))
        {
            resultMsg = "系统没有以下用户的手机号码，请通知管理员！（" + ToUserList + "）";
        }
        return resultMsg;
    }

    /// <summary>
    /// 发送外部短信，直接是手机号码列表(没有用户名字)
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="ContentStr"></param>
    /// <returns></returns>
    public static string SendSMS_OutMsg(string FaSongUser, string ToUserList, string ContentStr)
    {
        string info = "";
        string[] sr = ToUserList.Split(',');
        if (ContentStr.Length > 20)
        {
            ContentStr = ContentStr.Substring(0, 16) + "...";
        }
        for (int i = 0; i < sr.Length; i++)
        {
            if (!string.IsNullOrEmpty(sr[i]))
            {
                string phoneNum = sr[i];
                string templateCode = ConfigurationManager.AppSettings["TemplateCode_OutMsg"].ToString(); ;
                JObject msgs = new JObject();
                msgs.Add("fromuser", FaSongUser);
                msgs.Add("msg", ContentStr);
                info = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
        }
        return info;
    }

    /// <summary>
    /// 发送系统通知
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="ContentStr"></param>
    /// <returns></returns>
    public static string SendSMS_SystemMsg(string FaSongUser, string ToUserList, string ContentStr)
    {
        string info = "";
        string[] sr = ToUserList.Split(',');
        if (ContentStr.Length > 20)
        {
            ContentStr = ContentStr.Substring(0, 16) + "...";
        }
        for (int i = 0; i < sr.Length; i++)
        {
            if (!string.IsNullOrEmpty(sr[i]))
            {
                string phoneNum = sr[i];
                string templateCode = ConfigurationManager.AppSettings["TemplateCode_Msg"].ToString(); ;
                JObject msgs = new JObject();
                msgs.Add("msg", ContentStr);
                info = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
            else
            {
                info = "接收方的手机号码为空！";
            }
        }
        return info;
    }

    /// <summary>
    /// 发送会议通知
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="Meet_Name"></param>
    /// <param name="Meet_Loaction"></param>
    /// <param name="Meet_Time"></param>
    /// <param name="Meet_Content"></param>
    /// <param name="Meet_Attend"></param>
    /// <param name="Meet_AdArrive"></param>
    /// <param name="Meet_Alert"></param>
    /// <returns></returns>
    public static string SendSMS_MeetingMsg(string FaSongUser, string ToUserList, string Meet_Name, string Meet_Loaction, string Meet_Time, string Meet_Content, string Meet_Attend, string Meet_AdArrive, string Meet_Alert)
    {
        string resultMsg = "";
        string nullphone = "";
        //根据用户名列表获取手机号码 admin,test,zwl,test123
        DataSet MyDT = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,JiaTingDianHua from ERPUser where UserName in('" + ToUserList.Replace(",", "','") + "')");
        for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
        {
            string username = MyDT.Tables[0].Rows[i]["UserName"].ToString();
            string phoneNum = MyDT.Tables[0].Rows[i]["JiaTingDianHua"].ToString();
            if (!string.IsNullOrEmpty(phoneNum))
            {
                string templateCode = ConfigurationManager.AppSettings["TemplateCode_MeetingMsg"].ToString();
                JObject msgs = new JObject();
                msgs.Add("name", Meet_Name);
                msgs.Add("location", Meet_Loaction);
                msgs.Add("time", Meet_Time);
                msgs.Add("content", Meet_Content);
                msgs.Add("attendperson", Meet_Attend);
                msgs.Add("arrivetime", Meet_AdArrive);
                msgs.Add("BZ", Meet_Alert);
                //resultMsg = SendOneMSM_HasDelay(phoneNum, msgs.ToString(), templateCode);
                resultMsg = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
            else
            {
                nullphone = string.IsNullOrEmpty(nullphone) ? username : "," + username;
            }
        }
        if (!string.IsNullOrEmpty(nullphone))
        {
            resultMsg = resultMsg + "以下用户没有手机号码而没有发送：" + nullphone;
        }
        if (string.IsNullOrEmpty(resultMsg))
        {
            resultMsg = "系统没有以下用户的手机号码，请通知管理员！（" + ToUserList + "）";
        }
        return resultMsg;
    }


    /// <summary>
    /// 发送会议通知
    /// </summary>
    /// <param name="FaSongUser"></param>
    /// <param name="ToUserList"></param>
    /// <param name="Meet_Name"></param>
    /// <param name="Meet_Loaction"></param>
    /// <param name="Meet_Time"></param>
    /// <param name="Meet_Content"></param>
    /// <param name="Meet_Attend"></param>
    /// <param name="Meet_AdArrive"></param>
    /// <param name="Meet_Alert"></param>
    /// <returns></returns>
    public static string SendSMS_TongGuoMsg(string FaSongUser, string ToUserList, string workname, string username, string jiedian)
    {
        string resultMsg = "";
        string nullphone = "";
        //根据用户名列表获取手机号码 admin,test,zwl,test123
        DataSet MyDT = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,JiaTingDianHua from ERPUser where UserName in('" + ToUserList.Replace(",", "','") + "')");
        for (int i = 0; i < MyDT.Tables[0].Rows.Count; i++)
        {
            string phoneNum = MyDT.Tables[0].Rows[i]["JiaTingDianHua"].ToString();
            if (!string.IsNullOrEmpty(phoneNum))
            {
                string templateCode = ConfigurationManager.AppSettings["TemplateCode_TongGuo"].ToString();
                JObject msgs = new JObject();
                msgs.Add("workname", workname);
                msgs.Add("username", username);
                msgs.Add("jiedian", jiedian);
                //resultMsg = SendOneMSM_HasDelay(phoneNum, msgs.ToString(), templateCode);
                resultMsg = SendOneMSM(phoneNum, msgs.ToString(), templateCode);
            }
            else
            {
                nullphone = string.IsNullOrEmpty(nullphone) ? username : "," + username;
            }
        }
        if (!string.IsNullOrEmpty(nullphone))
        {
            resultMsg = resultMsg + "以下用户没有手机号码而没有发送：" + nullphone;
        }
        if (string.IsNullOrEmpty(resultMsg))
        {
            resultMsg = "系统没有以下用户的手机号码，请通知管理员！（" + ToUserList + "）";
        }
        return resultMsg;
    }

    /// <summary>
    /// 记录短信的发送情况
    /// </summary>
    /// <param name="ReceiveName"></param>
    /// <param name="PhoneNum"></param>
    /// <param name="MsgContent"></param>
    /// <param name="Template"></param>
    /// <param name="SendStatus"></param>
    /// <param name="ErrCode"></param>
    /// <param name="OutId"></param>
    /// <param name="SendDate"></param>
    /// <param name="ReceiveDate"></param>
    private static void WriteDataInDB(string PhoneNum, string MsgContent, string Template, string SendStatus, string ErrCode, string OutId, DateTime? SendDate, DateTime? ReceiveDate)
    {
        var aliyunMsgLog = new ZWL.BLL.AliyunMsgLog();
        var sendName = string.Empty;
        if (System.Web.HttpContext.Current == null)
        {
            sendName = "系统通知";
        }
        else
        {
            var sessionName = System.Web.HttpContext.Current.Session["UserName"];
            if (sessionName == null)
            {
                sendName = "未知";
            }
            else
                sendName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        }
        aliyunMsgLog.SendName = sendName;
        if (!aliyunMsgLog.SendName.Equals("NoLogin"))
        {
            aliyunMsgLog.ReceiveName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select TrueName from ERPUser where JiaTingDianHua ='" + PhoneNum + "'");
            aliyunMsgLog.PhoneNum = PhoneNum;
            aliyunMsgLog.MsgContent = MsgContent;
            aliyunMsgLog.Template = Template;
            aliyunMsgLog.SendStatus = SendStatus;
            aliyunMsgLog.ErrCode = ErrCode;
            aliyunMsgLog.OutId = OutId;
            aliyunMsgLog.SendDate = SendDate;
            aliyunMsgLog.ReceiveDate = ReceiveDate;
            aliyunMsgLog.Add();
        }
    }
    private static Dictionary<int, string> GetMessagePairs(string message)
    {
        var result = new Dictionary<int, string>();
        if (!string.IsNullOrEmpty(message))
        {
            var source = message.Split('[');
            for (int i = 0; i < source.Length; i++)
            {
                var item = source[i];
                if (!item.Contains("]")) continue;
                result.Add(result.Count + 1, item.Substring(0, item.IndexOf("]")));
            }
        }
        return result;
    }
    public static Dictionary<int, string> GetMessagePairsSmallFlag(string message)
    {
        var result = new Dictionary<int, string>();
        if (!string.IsNullOrEmpty(message))
        {
            var source = message.Split('(');
            for (int i = 0; i < source.Length; i++)
            {
                var item = source[i];
                if (!item.Contains(")")) continue;
                result.Add(result.Count + 1, item.Substring(0, item.IndexOf(")")));
            }
        }
        return result;
    }
    private static Dictionary<int, string> GetMessagePairsSmallZhCnFlag(string message)
    {
        var result = new Dictionary<int, string>();
        if (!string.IsNullOrEmpty(message))
        {
            var splitContent = Regex.Split(message, "！合同（", RegexOptions.IgnoreCase);
            var firstContent = splitContent[1];
            var secondContent = Regex.Split(firstContent, "）于 ", RegexOptions.IgnoreCase);
            result.Add(1, secondContent[0]);
        }
        return result;
    }
}