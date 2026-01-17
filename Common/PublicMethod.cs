using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Xml;
using ZWL.DBUtility;

namespace ZWL.Common
{
    /// <summary>
    /// PublicMethod 的摘要说明
    /// </summary>
    public class PublicMethod
    {

        /// <summary>
        /// 人事管理的表单
        /// </summary>
        public static string HRFormTypeStr = "2";
        public static string GWFormTypeStr = "1,20,21";
        public static string JJFormTypeStr = "11,12,13,19";
        public static string XMFormTypeStr = "14,15,16,17,18";
        public static string HRFormIDList = string.Format("select ID from ERPNForm where TypeID in ({0})", HRFormTypeStr);
        public static string GongWenFormIDs = string.Format("select ID from ERPNForm where TypeID in({0})", GWFormTypeStr);//公文formid
        public static string JingYingFormIDs = string.Format("select ID from ERPNForm where TypeID in({0})", JJFormTypeStr);//经营formid
        public static string XiangMuFormIDs = string.Format("select ID from ERPNForm where TypeID in({0})", XMFormTypeStr);//项目formid
        public static List<int> HRFormIDsint = DbHelperSQL.GetSingleCulumnList<int>(HRFormIDList);
        public static List<int> GongWenFormIDsint = DbHelperSQL.GetSingleCulumnList<int>(GongWenFormIDs);
        public static List<int> JingYingFormIDsint = DbHelperSQL.GetSingleCulumnList<int>(JingYingFormIDs);
        public static List<int> XiangMuFormIDsint = DbHelperSQL.GetSingleCulumnList<int>(XiangMuFormIDs);
        private static string _noNeedKaiGong = "实验测试,矿山地质环境保护与治理恢复方案编制,地灾防治规划,规划编制,地学信息,地质科技";//不用开工安全的专业
        private static string _noNeedSheJi = "实验测试,水资源储量核实,规划编制,地学信息,地质科技";//不用开工安全的专业
        private static string _noNeedGangWeiZeRen = _noNeedKaiGong + "," + _noNeedSheJi;//不用岗位责任及其告知书的专业
        public static List<string> NoNeedKaiGong
        {
            get
            {
                return GetSplitCollection(_noNeedKaiGong);
            }
        }
        public static List<string> NoNeedSheJiShenCha
        {
            get
            {
                return GetSplitCollection(_noNeedSheJi);
            }
        }
        public static List<string> NoNeedGangWeiZeRen
        {
            get
            {
                return GetSplitCollection(_noNeedGangWeiZeRen);
            }
        }
        public static List<string> AllowedPicType
        {
            get
            {
                var list = new List<string> { ".jpg", ".gif", ".bmp", ".png", ".jpeg" };
                return list;
            }
        }
        public static List<string> AllowedOfficeType
        {
            get
            {
                var list = new List<string> { ".doc", ".xls", ".ppt", ".docx", ".xlsx", ".pptx" };
                return list;
            }
        }
        public static List<string> AllowedFileType
        {
            get
            {
                var list = new List<string> { ".txt", ".html", ".htm", ".pdf" };
                list.AddRange(AllowedPicType);
                list.AddRange(AllowedOfficeType);
                return list;
            }
        }
        public static string UploadFileFolderTruePath
        {
            get { return HostingEnvironment.MapPath("~/UploadFile/"); }
        }
        public static string BaseToken
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("BaseToken");
            }
        }
        public static string CipherKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("CipherKey");
            }
        }
        public static string TokenCipherKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("TokenCipherKey");
            }
        }
        public PublicMethod()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// GetSplitCollection
        /// </summary>
        /// <param name="inputStr">包含英文逗号，的字符串参数</param>
        /// <returns></returns>
        public static List<string> GetSplitCollection(string inputStr)
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(inputStr))
            {
                foreach (var item in inputStr.Split(','))
                {
                    if (!list.Contains(item))
                        list.Add(item);
                }
            }
            return list;
        }
        public static void LogError(Exception ex)
        {
            using (var w = new System.IO.StreamWriter(Path.Combine(UploadFileFolderTruePath, "Errors.txt"), true))
            {
                w.WriteLine(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 将树形菜单的各个节点对应的权限加载进入指定的CheckBoxList中
        /// </summary>
        /// <param name="MYCHK"></param>
        public static void AddItmesInCheCKList(CheckBoxList MYCHK)
        {
            DataSet MYDT = DbHelperSQL.GetDataSet("select * from ERPTreeList where NavigateUrlStr!='' and NavigateUrlStr is not null order by PaiXuStr asc,ParentID asc,ID asc");
            for (int i = 0; i < MYDT.Tables[0].Rows.Count; i++)
            {
                string[] CheckItems = new string[8] { "", "", "", "", "", "", "", "" };
                CheckItems[0] = MYDT.Tables[0].Rows[i]["ValueStr"].ToString() + "_" + DbHelperSQL.GetSHSL("select top 1 TextStr from ERPTreeList where ID=" + MYDT.Tables[0].Rows[i]["ParentID"].ToString()) + "--" + MYDT.Tables[0].Rows[i]["TextStr"].ToString() + "--查看";

                string[] QuanXianList = MYDT.Tables[0].Rows[i]["QuanXianList"].ToString().Split('|');
                for (int j = 0; j < QuanXianList.Length; j++)
                {
                    CheckItems[j + 1] = QuanXianList[j];
                }

                //将前面设置好的CheckItems数组加入到CheckBoxList中
                for (int k = 0; k < CheckItems.Length; k++)
                {
                    if (CheckItems[k].Trim().Length <= 0)
                    {
                        ListItem ItemsTemp = new ListItem("", "");
                        MYCHK.Items.Add(ItemsTemp);
                    }
                    else
                    {
                        if (k == 0)
                        {
                            ListItem ItemsTemp = new ListItem(CheckItems[k].Split('_')[1], CheckItems[k].Split('_')[0]);
                            MYCHK.Items.Add(ItemsTemp);
                        }
                        else
                        {
                            ListItem ItemsTemp = new ListItem(CheckItems[k].Split('_')[1], MYDT.Tables[0].Rows[i]["ValueStr"].ToString() + CheckItems[k].Split('_')[0]);
                            MYCHK.Items.Add(ItemsTemp);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 根据自增数生成64进制id
        /// @returns 64进制id字符串
        /// </summary>
        /// <returns></returns>
        public static string GeneratorNodeCode(int nodeSerils)
        {
            var quotient = new DateTime(2020, 08, 01).Ticks / 1000000000;
            if (nodeSerils.ToString().Length < 7)
            {
                quotient += PublicMethod.GetInto(nodeSerils.ToString().PadRight(7, '0')) + nodeSerils;
            }
            else
                quotient += nodeSerils;
            var chars = "0123456789ABCDEFGHIGKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz";
            var charArr = chars.ToCharArray();
            var radix = chars.Length;
            var res = new ArrayList();
            do
            {
                var mod = quotient % radix;
                quotient = (quotient - mod) / radix;
                res.Add(charArr[mod]);
            } while (quotient > 0);
            return string.Join("", res.ToArray());
        }
        /// <summary>
        /// 去掉字符串中的数字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RemoveNumber(string key)
        {
            if (!key.IsNullOrEmpty())
                return Regex.Replace(key, @"\d", "");
            return key;
        }

        /// <summary>
        /// 统一设置默认时间
        /// </summary>
        /// <param name="defaultime"></param>
        public static void GetDefaultTime(out DateTime defaultime)
        {
            defaultime = DateTime.Parse("2000-01-01");
        }
        public static DateTime GetDefaultTime()
        {
            var date = new DateTime();
            GetDefaultTime(out date);
            return date;
        }
        public static string GetSqlKeywordAnd(string sql)
        {
            return string.IsNullOrEmpty(sql) ? "" : " and ";
        }
        public static string GetSqlAndByWhere(string sql, string lmtsql)
        {
            return !string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(lmtsql) ? " and " : "";
        }
        public static string GetSqlInWhere(string inStr)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(inStr))
            {
                var sb = new StringBuilder();
                var list = inStr.Split(',');
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    sb.AppendFormat(@"'{0}',", item);
                }
                result = sb.ToString().TrimEnd(',');
            }
            return result;
        }
        /// <summary>
        /// 根据专业名称返回专业代码
        /// </summary>
        /// <param name="strzhuanye">专业大类</param>
        /// <returns></returns>
        public string GetSubjectCode(string strzhuanye)
        {
            string strcode = "";
            switch (strzhuanye)
            {
                case "地质勘查":
                    strcode += "DK";
                    break;
                case "环境地质":
                    strcode += "HJ";
                    break;
                case "工程地质":
                    strcode += "GC";
                    break;
                case "其它":
                    strcode += "QT";
                    break;
                default:
                    break;
            }
            return strcode;
        }
        public ArrayList GetGridViewColumns(string strtablename)
        {
            ArrayList columns = new ArrayList();
            string columstring = "";
            switch (strtablename)
            {
                //case "ERPXMQQDJ":
                //    columstring = "select XMBH as 信息编号,XMName as 项目名称, JYFS as 经营方式,XMAdress as 项目地址, WTDWName as 委托单位名称," +
                //    "WTFS as 委托方式,YJTBTime as 预计投标时间,WTTBDW as 委托投标单位,ZYType as 专业类别,HYType as 行业类别,HZDWName as 合作单位名称," +
                //    "DJBM as 登记部门,DJR as 登记人,DJSJ as 登记时间,YJXMZJ as 预计项目总价,XMZJLY as 项目资金来源,XMBeginTime as 预计项目开始时间," +
                //    "XMEndTime as 预计项目结束时间,HZDWLXR as 合作单位联系人,HZDWLXDH as 合作单位联系电话,GXTime as 更新时间,WTDWLXR as 委托单位联系人,WTDWLXDH as 委托单位联系电话,YJXMDJ as 预计项目单价," +
                //    "WORKNAME as 工作名称 from ERPXMQQDJ";
                //    break;
                //case "ERPTouBiao":
                //    columstring = "select ID as 序号,TBXMBH as 投标项目编号,DengJiTime as 登记时间,TBXMMC as 投标项目名称, TBFS as 投标方式,ZYLB as 专业类别,HYLB as 行业类别," +
                //    "YZDWName as 业主单位,ZBDLName as 招标代理单位,WZ as 网址,ZZDWName as 资质单位,LHDWName as 联合投标单位,TBTime as 投标时间," +
                //    "TBBM as 投标部门,JYFS as 经营方式,SQWTR as 授权委托人,TBManager as 投标负责人,KBTime as 开标时间,XMQQBH as 项目前期编号,ZBQK as 投标情况," +
                //    "WorkName as 工作名称 from ERPTouBiao";
                //    break;
                case "项目前期信息台账":
                    columstring = "信息编号,项目名称,项目地址,预计项目总价,预计项目单价,委托单位,委托方式,专业类别,行业类别,合作单位,登记部门,登记人,登记时间,更新时间,工作编号";
                    break;
                case "投标台账":
                    columstring = "投标项目编号,投标项目名称,投标方式,专业类别,行业类别,业主单位,资质单位,联合投标单位,投标部门,投标负责人,登记时间,投标时间,投标情况";
                    break;
                case "投标保证金台账":
                    columstring = "投标项目编号,投标项目名称,投标部门,投标保证金,招标单位,开户行,银行账号,支付方式,截止时间,退还时间,保证金来源,投标经办人";
                    break;
                case "收款合同台账":
                    columstring = "合同编号,合同名称,专业大类,专业类别,行业类别,经营方式,甲方单位,乙方单位,合同金额,开票合计,未开票金额,累计到账金额,本期到账金额,未到账金额,合同签订日期,承接部门,工作编号,合同状态,合同归档,合同归档时间,合同借阅状态,中标通知书,合格证书,项目地址";
                    break;
                case "付款合同台账":
                    columstring = "合同编号,合同名称,专业类别,经营方式,甲方单位,乙方单位,合同金额,合同签订日期,合同状态,合同借阅状态,承接部门,经办人";
                    break;
                case "合同收款明细":
                    columstring = "合同编号,合同名称,承接部门,专业类别,合同金额,开票金额,累计到账金额,本期到账金额,开票方式,申请时间,到账时间,付款单位,收款状态";
                    break;
                case "项目台账":
                    columstring = "项目编号,项目名称,合同编号,项目地址,项目经费,委托单位名称,专业大类,专业类别,行业类别,项目实施部门,项目负责人,项目状态,钻孔数,登记时间,项目开始时间,项目结束时间,审核状态,报告名称,合同状态,row";
                    break;
                case "项目归档台账":
                    string JiaoSe = ZWL.Common.PublicMethod.GetSessionValue("JiaoSe");
                    if (JiaoSe.Contains("资料室") || JiaoSe.Contains("超级管理员"))
                    {
                        columstring = "档案号,报告编号,项目编号,项目名称,项目负责人,经办人,工作日期,给号日期,归档人,归档日期,操作";
                    }
                    else
                    {
                        columstring = "档案号,报告编号,项目编号,项目名称,项目负责人,经办人,工作日期,给号日期,归档人,归档日期";
                    }
                    break;
                //case "ERPXMJBXX":
                //    columstring = "select ID as 序号,XMBH as 项目编号,XMQQBH as 项目前期编号, HTBH as 合同编号,XMName as 项目名称, XMState as 项目状态,XMAdress as 项目地址," +
                //    "WTDWName as 委托单位名称,WTDWLXR as 委托单位联系人,WTDWLXDH as 委托单位联系电话,HZDWName as 合作单位名称,HZDWLXR as 合作单位联系人,HZDWLXDH as 合作单位联系电话," +
                //    "WTFS as 委托方式,ZYLB as 专业类别,HYLB as 行业类别,XMZJLY as 项目资金来源,XMJF as 项目经费,XMBeginTime as 项目开始时间," +
                //    "XMEndTime as 项目结束时间,XMBM as 项目实施部门,XMFZR as 项目负责人,DJTime as 登记时间,SHState as 审核状态,ZYLBMain as 专业大类," +
                //    "ZKS as 钻孔数,ZKJC as 钻孔进尺,PGDJ as 评估等级,KCDJ as 勘察等级,BXCLDJ as 变形测量等级,PGMJ as 评估面积,DCMJ as 调查面积,TC as 探槽,KT as 坑探,ZT as 钻探,XMReport as 报告名称," +
                //    "WorkName as 工作名称 from ERPXMJBXX";
                //    break;
                //case "ERPHeTongShouKuan":
                //    columstring = "select ID as 序号,HTBH as 合同编号,HTName as 合同名称, BM as 部门,ZYLB as 专业类别, HTJE as 合同金额,KaiPiaoJE as 开票金额," +
                //    "ShengYuJE as 剩余金额,SQTime as 申请时间,KaiPiaoFS as 开票方式,FKDW as 付款单位,NWorkToDoID as 工作编号,SKZT as 收款状态 from ERPHeTongShouKuan";
                //    break;
                //case "ERPHeTongDaoZhang":
                //    columstring = "select ID as 序号,HTBH as 合同编号,HTName as 合同名称,BM as 部门,BMJBR as 部门经办人,SKZH as 收款账户,KaiPiaoFS as 开票方式,FKDW as 付款单位, HTJE as 合同金额,KaiPiaoJE as 开票金额," +
                //    "DaoZhangJE as 到账金额,DaoZhangTime as 到账时间,ZYLB as 专业类别,NWorkToDoID as 工作编号 from ERPHeTongDaoZhang";
                //    break;
                //case "ERPTBBaoZhengJin":
                //    columstring = "select ID as 序号,TBXMBH as 投标项目编号,TBXMMC as 投标项目名称, TBBM as 投标部门,TBBZJ as 投标保证金, ZBDWMC as 招标单位,KaiHuHang as 开户行," +
                //    "YHZH as 银行账号,ZFFS as 支付方式,JZTime as 截止时间,THTime as 退还时间,BZJLY as 保证金来源,TBJBR as 投标经办人 from ERPTBBaoZhengJin";
                //    break;
                case "XMBH":
                    columstring = "项目编号,项目名称";
                    break;
                case "XMQQBH":
                    columstring = "信息编号,项目名称";
                    break;
                case "TBXMBH":
                    columstring = "投标项目编号,投标项目名称";
                    break;
                case "XMQQCheck":
                    columstring = "信息编号,项目名称,专业类别,登记部门,登记时间";
                    break;
                case "XMJBXXCheck":
                    columstring = "项目编号,项目名称";
                    break;
                case "HTBH":
                    columstring = "合同编号,合同名称";
                    break;
                case "AUTOPROJECT":
                    columstring = "项目编号,项目名称";
                    break;
                case "ERPXMCJJCPG":
                    columstring = "项目编号,项目名称,专业类别,项目金额";
                    break;
                case "CGMC":
                    columstring = "项目编号,项目名称,报告名称";
                    break;
                case "XMFBCompanyName":
                    columstring = "企业名称";
                    break;
                case "SubcontractTeam":
                    columstring = "分包队伍名称,统一信用代码,考核结果,推荐部门";
                    break;
                case "OfficeSupplyType":
                    columstring = "分类名称,描述";
                    break;
            }
            if (columstring != "")
            {
                string[] strlist = columstring.Split(',');
                for (int i = 0; i < strlist.Length; i++)
                {
                    columns.Add(strlist[i]);
                }
            }
            return columns;

        }
        public string getSQLTable(string strtablename)
        {
            string selectSQL = "";
            switch (strtablename)
            {
                case "ERPXMQQDJ":
                    selectSQL = @"select DJSJ as ID,q.ID RID, XMBH as 信息编号,XMName as 项目名称, JYFS as 经营方式,XMAdress as 项目地址, WTDWName as 委托单位名称," +
                    "WTFS as 委托方式,YJTBTime as 预计投标时间,WTTBDW as 委托投标单位,ZYType as 专业类别,HYType as 行业类别,HZDWName as 合作单位名称," +
                    "DJBM as 登记部门,DJR as 登记人,DJSJ as 登记时间,YJXMZJ as 预计项目总价,XMZJLY as 项目资金来源,XMBeginTime as 预计项目开始时间," +
                    "XMEndTime as 预计项目结束时间,HZDWLXR as 合作单位联系人,HZDWLXDH as 合作单位联系电话,GXTime as 更新时间,WTDWLXR as 委托单位联系人,WTDWLXDH as 委托单位联系电话,YJXMDJ as 预计项目单价," +
                    "q.WORKNAME as 工作名称,d.JieDianName 节点名称,d.StateNow 工作状态,d.ID NWorkToDoID from ERPXMQQDJ q join ERPNWorkToDo d on q.nworkid = d.ID";
                    break;
                case "ERPTouBiao":
                    selectSQL = "select ID,ID as 序号,TBXMBH as 投标项目编号,DengJiTime as 登记时间,TBXMMC as 投标项目名称, TBFS as 投标方式,ZYLB as 专业类别,HYLB as 行业类别," +
                    "YZDWName as 业主单位,ZBDLName as 招标代理单位,WZ as 网址,ZZDWName as 资质单位,LHDWName as 联合投标单位,TBTime as 投标时间,ISNULL(TBBJ,0.00) as 投标报价," +
                    "TBBM as 投标部门,JYFS as 经营方式,SQWTR as 授权委托人,TBManager as 投标负责人,KBTime as 开标时间,XMQQBH as 项目前期编号,ZBQK as 投标情况," +
                    "WorkName as 工作名称,NWorkID as 工作编号 from ERPTouBiao";
                    break;
                case "ERPHeTong":
                    selectSQL = @"select h.ID,h.ID as 序号,HTID as 合同编号,HTName as 合同名称, HTLB as 合同类别,HTTYPE as 专业大类,ZYLB as 专业类别,HYLB as 行业类别,JYFS as 经营方式,JFDW as 甲方单位,
                    JFFZR as 甲方负责人,YFDW as 乙方单位,YFFZR as 乙方负责人,BFDW as 丙方单位,BFFZR as 丙方负责人,HZDW1 as 合作单位1,HZFZR1 as 合作负责人1,HZDW2 as 合作单位2,HZFZR2 as 合作负责人2,HTJE as 合同金额,
                    ISNULL((select ISNULL(sum(KaiPiaoJE), 0) from ERPHeTongShouKuan a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID),0) as 开票合计,
										(HTJE-ISNULL((select ISNULL(sum(KaiPiaoJE), 0) from ERPHeTongShouKuan a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID),0)) as 未开票金额,
                    ISNULL((select ISNULL(sum(DaoZhangJE), 0) from (
                    select DaoZhangJE from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID
                    UNION ALL
                    select Amount DaoZhangJE from ERPHeTongYuShouKuan y where Flag<>0 and ConnectID is null and HTBH=h.HTID
			                    ) t),0) as 累计到账金额,
                    ISNULL((
					select ISNULL(sum(DaoZhangJE), 0) from (
					select DaoZhangJE,DaoZhangTime from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID
					UNION ALL
					select Amount DaoZhangJE,ReceivedTime DaoZhangTime from ERPHeTongYuShouKuan y where Flag<>0 and ConnectID is null and HTBH=h.HTID
								) t),0) as 本期到账金额,
                    (HTJE-ISNULL((select ISNULL(sum(DaoZhangJE), 0) from (
                    select DaoZhangJE from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID
                    UNION ALL
                    select Amount DaoZhangJE from ERPHeTongYuShouKuan y where Flag<>0 and ConnectID is null and HTBH=h.HTID
			                    ) t),0)) as 未到账金额,
                    (select top 1 DaoZhangTime from (
                    select STUFF((select ',' + CONVERT(varchar(100), a.DaoZhangTime, 23) from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=h.HTID FOR XML PATH('')),1,1,'') DaoZhangTime
                    UNION
                    select STUFF((select ',' + CONVERT(varchar(100), ReceivedTime, 23) from ERPHeTongYuShouKuan where Flag<>0 and ConnectID is null and NWorkID=h.NWorkToDoID FOR XML PATH('')),1,1,'') DaoZhangTime
                    ) t where t.DaoZhangTime is not null) as 到账时间,
                    JJFS as 计价方式,TimeStr 合同登记日期,QDTime as 合同签订日期,KSTime as 合同开始时间,JZTime as 合同截止时间,XMID as 项目编号,XMName as 项目名称,JFLY as 经费来源,QDFS as 签订份数,CJBM as 承接部门,XMCJR as 项目承接人,
                    JBR as 经办人,HTGD as 合同归档,GDTime as 合同归档时间,HTZT as 合同状态,ZBTZS as 中标通知书,HGZS as 合格证书,JEJH1 as 计划应收金额1,RQJH1 as 计划时间1,BZJH1 as 计划备注1,JEJH2 as 计划应收金额2,
                    RQJH2 as 计划时间2,BZJH2 as 计划备注2,JEJH3 as 计划应收金额3,RQJH3 as 计划时间3,BZJH3 as 计划备注3,JEJH4 as 计划应收金额4,RQJH4 as 计划时间4,BZJH4 as 计划备注4,JEJH5 as 计划应收金额5,RQJH5 as 计划时间5,
                    BZJH5 as 计划备注5,HTJYState as 合同借阅状态,HTZYNR as 合同主要内容,XMZT as 项目状态,NWorkToDoID as 工作编号,Adress as 项目地址,d.WorkName as 工作名称,
                    JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID";
                    break;
                case "ERPXMJBXX":
                    selectSQL = "select ID,ID as 序号,XMBH as 项目编号,XMQQBH as 项目前期编号, HTBH as 合同编号,XMName as 项目名称, XMState as 项目状态,XMAdress as 项目地址," +
                    "WTDWName as 委托单位名称,WTDWLXR as 委托单位联系人,WTDWLXDH as 委托单位联系电话,HZDWName as 合作单位名称,HZDWLXR as 合作单位联系人,HZDWLXDH as 合作单位联系电话," +
                    "WTFS as 委托方式,ZYLB as 专业类别,HYLB as 行业类别,XMZJLY as 项目资金来源,XMJF as 项目经费,XMBeginTime as 项目开始时间," +
                    "XMEndTime as 项目结束时间,XMBM as 项目实施部门,XMFZR as 项目负责人,DJTime as 登记时间,SHState as 审核状态,ZYLBMain as 专业大类," +
                    "ZKS as 钻孔数,ZKJC as 钻孔进尺,PGDJ as 评估等级,KCDJ as 勘察等级,BXCLDJ as 变形测量等级,PGMJ as 评估面积,DCMJ as 调查面积,TC as 探槽,KT as 坑探,ZT as 钻探,XMReport as 报告名称,HTState as 合同状态," +
                    "WorkName as 工作名称,NWorkID from ERPXMJBXX";
                    break;
                case "ERPHeTongShouKuan":
                    selectSQL = "select ID,ID as 序号,HTBH as 合同编号,HTName as 合同名称, BM as 部门,ZYLB as 专业类别, HTJE as 合同金额,KaiPiaoJE as 开票金额," +
                    "ShengYuJE as 剩余金额,SQTime as 申请时间,KaiPiaoFS as 开票方式,(select SUM(DaoZhangJE) from ERPHeTongDaoZhang where NWorkToDoID=ERPHeTongShouKuan.NWorkToDoID and (NWorkToDoID<>0 or HTBH=ERPHeTongShouKuan.HTBH or HTName=ERPHeTongShouKuan.HTName)) as 到账金额,FKDW as 付款单位,NWorkToDoID as 工作编号,SKZT as 收款状态,NSRnum as 纳税人识别码,DZ as 地址和电话,KaiHuHang as 开户行及账号 from ERPHeTongShouKuan";
                    break;
                case "ERPHeTongDaoZhang":
                    selectSQL = "select ID as 序号,HTBH as 合同编号,HTName as 合同名称,BM as 部门,BMJBR as 部门经办人,SKZH as 收款账户,KaiPiaoFS as 开票方式,FKDW as 付款单位, HTJE as 合同金额,KaiPiaoJE as 开票金额," +
                    "DaoZhangJE as 到账金额,DaoZhangTime as 到账时间,ZYLB as 专业类别,NWorkToDoID as 工作编号 from ERPHeTongDaoZhang";
                    break;
                case "ERPKaoQin":
                    selectSQL = "SELECT ID,ID as 序号, UserName as 姓名 , GuiDingTime1 as 上午上班时间, DengJiTime1 as 上午签到时间 , GuiDingTime2 as 上午下班时间, DengJiTime2 as 上午签退时间，" +
                   "GuiDingTime3 as 下午上班时间，DengJiTime3 as 下午签到时间，GuiDingTime4 as 下午下班时间, DengJiTime as 下午签退时间 form ERPKaoQin";
                    break;
                case "ERPTBBaoZhengJin":
                    selectSQL = "select ID,ID as 序号,TBXMBH as 投标项目编号,TBXMMC as 投标项目名称, TBBM as 投标部门,TBBZJ as 投标保证金, ZBDWMC as 招标单位,KaiHuHang as 开户行," +
                    "YHZH as 银行账号,ZFFS as 支付方式,JZTime as 截止时间,THTime as 退还时间,BZJLY as 保证金来源,TBJBR as 投标经办人 from ERPTBBaoZhengJin";
                    break;
                case "ERPXMZLGuiDang":
                    selectSQL = "select a.ID,a.NWorkID,a.DAH as 档案号,a.ReportBH as 报告编号,a.XMBH as 项目编号, ISNULL(a.XMName, b.XMName) as 项目名称,a.XMFZR AS 项目负责人,a.JBR as 经办人 ,a.DJTime as 给号日期, a.WorkDate as 工作日期,a.GDR 归档人,a.GDDate as 归档日期,b.XMBeginTime,b.XMEndTime,'' as 操作 from ERPXMZLGuiDang a LEFT JOIN ERPXMJBXX b on a.XMBH=b.XMBH";
                    break;
                case "ERPXMNWorkFlowFormat":
                    selectSQL = @"select * from (
                select d.*,x.XMBH Number,x.XMName Name,x.ZYLB,x.XMBM
                      ,(case when ZYLB in ('实验测试','矿山地质环境保护与治理恢复方案编制','地灾防治规划','规划编制','地学信息','地质科技','水资源储量核实') then 1 else GWHD108 end) GWHD108
                      ,(case when ZYLB in ('实验测试','矿山地质环境保护与治理恢复方案编制','地灾防治规划','规划编制','地学信息','地质科技','水资源储量核实') then 1 else ZRGZS109 end) ZRGZS109
                      ,(case when ZYLB in ('实验测试','水资源储量核实','规划编制','地学信息','地质科技') then 1 else (case when SJSC56>0 or SJSC75>0 then 1 else 0 end) end) SJSC
                      ,(case when ZYLB in ('实验测试','矿山地质环境保护与治理恢复方案编制','地灾防治规划','规划编制','地学信息','地质科技') then 1 else KGAQ62 end) KGAQ62
	                   from ERPXMJBXX x join ERPNWorkToDo d on x.NWorkID=d.ID
                join SubWorkFlowState s on x.NWorkID=s.NWorkID
                where JieDianName='总工办核实' and StateNow='正在办理' and ','+ShenPiUserList+',' like '%,{0},%' and ','+OKUserList+',' not like '%,{0},%'
                {1}
                ) t where (SJSC*KGAQ62)=0";
                    break;
                case "ERPXMCJJCPG":
                    selectSQL = @"select h.ID, H.ID as 序号,XMBH as 项目编号,XMName as 项目名称,DJBM as 部门,DJR as 经办人,WTDWName as 招标单位,Amount as 项目金额,ZYLB as 专业类别,NWorkID,
                    FormID,WorkFlowID,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow from ERPXMCJJCPG h join ERPNWorkToDo d on h.NWorkID=d.ID";
                    break;
                case "OfficeSupplyType":
                    selectSQL = @"SELECT ID,Category 分类名称,Description 描述,SortCode,EnabledMark,DeleteMark from ERPOfficeSupplyType";
                    break;
            }
            return selectSQL;
        }
        /// <summary>
        /// 传入专业类别的子类，获取大类名称，用来根据专业大类构建流程树
        /// </summary>
        /// <param name="strsubname"></param>
        public string GetMainSubjectName(string strsubname)
        {
            string strmain = "";
            string filepath = HttpContext.Current.Server.MapPath("../App_Data/GeoSubjectInfo.xml");
            ArrayList paramlist = new ArrayList();
            if (System.IO.File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                if (xmlDoc != null)
                {
                    XmlElement xmlElem = xmlDoc.DocumentElement;
                    if (xmlElem != null)
                    {
                        XmlNodeList xmlnodelist = xmlElem.GetElementsByTagName("type");
                        foreach (XmlNode node in xmlnodelist)
                        {
                            if (node.InnerText == strsubname)
                            {
                                return node.ParentNode.Name;
                            }
                        }
                    }
                }
            }
            return strmain;
        }

        public static ArrayList Getzhuanyetype(string strname)
        {
            string filepath = HttpContext.Current.Server.MapPath("../App_Data/GeoSubjectInfo.xml");
            ArrayList paramlist = new ArrayList();
            if (System.IO.File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                if (xmlDoc != null)
                {
                    XmlElement xmlElem = xmlDoc.DocumentElement;
                    if (xmlElem != null)
                    {
                        XmlNodeList xmlnodelist = xmlElem.GetElementsByTagName("合同签订评审");
                        if (xmlnodelist.Count > 0)
                        {
                            foreach (XmlNode node in xmlnodelist[0].ChildNodes)
                            {
                                if (node.Name == strname)
                                {
                                    foreach (XmlNode node1 in node.ChildNodes)
                                    {
                                        paramlist.Add(node1.InnerText);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return paramlist;
        }
        public string GetWaterCodeByTableName(string strtablename)
        {
            return GetWaterCodeByTableName(strtablename, DateTime.Now.Year);
        }
        /// <summary>
        /// 根据传入的表名获取对应的流水账号4位20141231
        /// </summary>
        /// <param name="strtablename"></param>
        /// <returns></returns>
        public string GetWaterCodeByTableName(string strtablename, int currentYear)
        {
            string strwatercode = "";
            string strbianhaoname = "";
            int nlength = 0;
            string strSQL = " SELECT ";
            switch (strtablename)
            {
                case "ERPXMQQDJ"://项目前期信息登记
                    strbianhaoname = "XMBH";
                    nlength = 18;
                    break;
                case "ERPTouBiao"://投标项目审批
                    strbianhaoname = "TBXMBH";
                    nlength = 9;
                    break;
                case "ERPXMJBXX"://项目基本信息
                    strbianhaoname = "XMBH";
                    nlength = 18;
                    break;
                case "ERPHeTong"://合同
                    strbianhaoname = "HTID";
                    nlength = 9;
                    break;
                case "AptitudeWork"://资质管理
                    strbianhaoname = "No";
                    nlength = 18;
                    break;
                case "ERPOfficeSupply"://办公用品需要计划
                    strbianhaoname = "No";
                    nlength = 18;
                    break;
                case "ERPOfficeSupplySummary"://办公用品需要计划汇总
                    strbianhaoname = "No";
                    nlength = 18;
                    break;
                case "ERPInnerInviteBidRequest"://内部招标请示
                    strbianhaoname = "No";
                    nlength = 9;
                    break;
                case "ERPInnerInviteBidReport"://内部招标报告
                    strbianhaoname = "No";
                    nlength = 9;
                    break;
                case "ERPGKProjectGWGL"://工程勘察项目野外施工岗位管理
                    strbianhaoname = "Number";
                    nlength = 9;
                    break;
                case "ERPHeTongJieYue"://合同借阅
                    strbianhaoname = "HTJYID";
                    nlength = 9;
                    break;
                case "ERPXMCJJCPG"://项目承接决策评估
                    strbianhaoname = "XMBH";
                    nlength = 9;
                    break;
                default:
                    break;
            }
            strSQL += strbianhaoname;
            strSQL += " FROM " + strtablename;
            strSQL += " where DATALENGTH(" + strbianhaoname + ")=" + nlength;//计算新的9位的编号（原来是10位）
            string stryear = currentYear.ToString();
            DataTable dt = DbHelperSQL.GetDataTable(strSQL);
            //遍历取到当年中最大的流水账号
            int nmaxwatercode = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strbh = dt.Rows[i][0].ToString();
                    if (!string.IsNullOrEmpty(strbh) && strbh.Length == 9 && strbh.Contains(stryear))
                    {
                        int ncode = int.Parse(strbh.Substring(5, 4));
                        if (ncode >= nmaxwatercode)
                        {
                            nmaxwatercode = ncode;
                        }
                    }
                }
            }
            strwatercode = (nmaxwatercode + 1).ToString();
            if (strwatercode.Length < 4)//不足4位
            {
                if (strwatercode.Length == 1)
                {
                    strwatercode = "000" + strwatercode;
                }
                if (strwatercode.Length == 2)
                {
                    strwatercode = "00" + strwatercode;
                }
                if (strwatercode.Length == 3)
                {
                    strwatercode = "0" + strwatercode;
                }
            }
            return strwatercode;
        }
        /// <summary>
        /// 写日志文件（D:\\log.txt）
        /// </summary>
        /// <param name="strMsg"></param>
        public static void WirteLog(string strMsg)
        {
            DateTime dt = DateTime.Now;
            strMsg += "---------" + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString() + ":" + dt.Millisecond.ToString() + "\r\n";
            System.IO.FileStream fs = new System.IO.FileStream("d:\\log.txt", System.IO.FileMode.Append);
            byte[] bstr = new System.Text.UTF8Encoding(true).GetBytes(strMsg);
            fs.Write(bstr, 0, bstr.Length);
            fs.Close();
        }
        /// <summary>
        /// 将传入的用户名列表，按照工作委托的要求，自动置换成受委托后的用户名字符串，然后返回
        /// </summary>
        /// <param name="UserList"></param>
        /// <returns></returns>
        public static string WorkWeiTuoUserList(string UserList)
        {
            string ReturnList = "";
            string[] UserArray = UserList.Split(',');
            for (int i = 0; i < UserArray.Length; i++)
            {
                if (UserArray[i].ToString().Trim().Length > 0)
                {
                    string WeiTuoUser = DbHelperSQL.GetSHSL("select top 1 ToUser from ERPNWorkFlowWT where FromUser='" + UserArray[i].ToString() + "'");
                    if (WeiTuoUser.Trim().Length > 0)
                    {
                        if (ReturnList.Trim().Length > 0)
                        {
                            if (StrIFIn("," + WeiTuoUser + ",", "," + ReturnList + ",") == false)
                            {
                                ReturnList = ReturnList + "," + WeiTuoUser;
                            }
                        }
                        else
                        {
                            ReturnList = WeiTuoUser;
                        }
                    }
                    else
                    {
                        if (ReturnList.Trim().Length > 0)
                        {
                            if (StrIFIn("," + UserArray[i].ToString() + ",", "," + ReturnList + ",") == false)
                            {
                                ReturnList = ReturnList + "," + UserArray[i].ToString();
                            }
                        }
                        else
                        {
                            ReturnList = UserArray[i].ToString();
                        }
                    }
                }
            }
            return ReturnList;
        }
        //修改web.config节点的值
        public static void EditAppValue(string KeyNameStr, string SetValueStr)
        {
            //修改web.config
            XmlDocument xDoc = new XmlDocument();
            try
            {
                //打开web.config
                xDoc.Load(System.Web.HttpContext.Current.Request.MapPath("../Web.config"));
                //string key;
                XmlNode app;
                app = xDoc.SelectSingleNode("/configuration/appSettings/add[@key='" + KeyNameStr + "']");
                app.Attributes["value"].Value = SetValueStr;
                //关闭
                xDoc.Save(System.Web.HttpContext.Current.Request.MapPath("../web.config"));
                System.Web.HttpContext.Current.Response.Write("<script>alert('配置数据修改完成！');</script>");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                xDoc = null;
            }
        }
        //得到文件列表
        public static string GetWenJian(string WenJianList, string DirStr)
        {
            if (!string.IsNullOrEmpty(WenJianList))
            {
                string[] MyRange = WenJianList.Split('|');
                string MyReturn = string.Empty;
                for (int i = 0; i < MyRange.Length; i++)
                {
                    if (MyRange[i].ToString().Trim().Length > 0)
                    {
                        if (MyReturn.Trim().Length > 0)
                        {
                            if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                            {
                                MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                            }
                            else
                            {
                                string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                                if (OldNameStr.Trim().Length <= 0)
                                {
                                    OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                                }
                                MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='../DsoFramer/DownLoadFile.aspx?f=" + DirStr + MyRange[i].ToString() + "&n=" + OldNameStr + "'>" + OldNameStr + "</a>&nbsp;<a href='../FlexPaperFlash/SWFShow.aspx?f=" + MyRange[i].ToString() + "&n=" + OldNameStr + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                            }
                        }
                        else
                        {
                            if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                            {
                                MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                            }
                            else
                            {
                                string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                                if (OldNameStr.Trim().Length <= 0)
                                {
                                    OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                                }
                                MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='../DsoFramer/DownLoadFile.aspx?f=" + DirStr + MyRange[i].ToString() + "&n=" + OldNameStr + "'>" + OldNameStr + "</a>&nbsp;<a href='../FlexPaperFlash/SWFShow.aspx?f=" + MyRange[i].ToString() + "&n=" + OldNameStr + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                            }
                        }
                    }
                }
                if (MyReturn.ToString().Trim().Length <= 0)
                {
                    MyReturn = MyReturn + "无文件！";
                }
                return MyReturn;
            }
            return "无文件！";
        }



        /// <summary>
        /// 获取文件链接，同时加上阅读、编辑
        /// </summary>
        /// <param name="WenJianList"></param>
        /// <param name="DirStr"></param>
        /// <returns></returns>
        public static string GetWenJian2(string WenJianList, string DirStr)
        {
            if (!string.IsNullOrEmpty(WenJianList))
            {
                string[] MyRange = WenJianList.Split('|');
                string MyReturn = string.Empty;
                for (int i = 0; i < MyRange.Length; i++)
                {
                    if (MyRange[i].ToString().Trim().Length > 0)
                    {
                        if (MyReturn.Trim().Length > 0)
                        {
                            if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                            {
                                MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                            }
                            else
                            {
                                string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                                if (OldNameStr.Trim().Length <= 0)
                                {
                                    OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                                }
                                MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../FlexPaperFlash/SWFShow.aspx?f=" + MyRange[i].ToString() + "&n=" + OldNameStr + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>&nbsp;<a href='../DsoFramer/EditFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/EditFile.gif /></a>";
                            }
                        }
                        else
                        {
                            if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                            {
                                MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                            }
                            else
                            {
                                string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                                if (OldNameStr.Trim().Length <= 0)
                                {
                                    OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                                }
                                MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../FlexPaperFlash/SWFShow.aspx?f=" + MyRange[i].ToString() + "&n=" + OldNameStr + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>&nbsp;<a href='../DsoFramer/EditFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/EditFile.gif /></a>";
                            }
                        }
                    }
                }
                if (MyReturn.ToString().Trim().Length <= 0)
                {
                    MyReturn = MyReturn + "无文件！";

                }
                return MyReturn;
            }
            return "无文件！";
        }


        /// <summary>
        /// 可支持在线预览或者下载
        /// </summary>
        /// <param name="WenJianList"></param>
        /// <param name="DirStr"></param>
        /// <returns></returns>
        public static string GetWenJian_OnlineView(string WenJianList, string DirStr)
        {
            if (!string.IsNullOrEmpty(WenJianList))
            {
                string[] MyRange = WenJianList.Split('|');
                string MyReturn = string.Empty;
                for (int i = 0; i < MyRange.Length; i++)
                {
                    string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                    if (OldNameStr.Trim().Length <= 0)
                    {
                        OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                    }
                    MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='../FlexPaperFlash/SWFShow.aspx?f=" + DirStr + MyRange[i].ToString() + "&n=" + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;";
                }
                if (MyReturn.ToString().Trim().Length <= 0)
                {
                    MyReturn = MyReturn + "无文件！";
                }
                return MyReturn;
            }
            return "无文件！";
        }

        /// <summary>
        /// 显示附件（手机版本）
        /// </summary>
        /// <param name="WenJianList"></param>
        /// <param name="DirStr"></param>
        /// <returns></returns>
        public static DataSet GetWenJian_PhoneVersion(string WenJianList, string DirStr)
        {
            DataSet ds = new DataSet();
            DataTable Dtt = new DataTable();
            Dtt.TableName = "Pages";
            ds.Tables.Add(Dtt);
            ds.Tables[0].Columns.Add("ID");
            ds.Tables[0].Columns.Add("FilePath");

            if (!string.IsNullOrEmpty(WenJianList))
            {
                string[] MyRange = WenJianList.Split('|');
                string MyReturn = string.Empty;
                for (int i = 0; i < MyRange.Length; i++)
                {
                    var item = MyRange[i];
                    if (string.IsNullOrEmpty(item)) continue;
                    var OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + item.Replace("MailAttachments/", "") + "'");
                    var ID = DbHelperSQL.GetSHSL("select ID from ERPSaveFileName where NowName='" + item.Replace("MailAttachments/", "") + "'");
                    if (OldNameStr.Trim().Length <= 0)
                    {
                        OldNameStr = item.Replace("MailAttachments/", "");
                    }
                    MyReturn = string.Format("<a class='FileViewLink' href='javascript:void(0)' data-url='{0}' download='{1}'>{1}</a>", "../" + DirStr + item, OldNameStr);
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["ID"] = EncryptParam(ID);
                    dr["FilePath"] = MyReturn;
                    ds.Tables[0].Rows.Add(dr);
                }
            }
            return ds;
        }

        //将ListItem1中的选中项加入ListItem2中，或者从ListItem2中减去选中项,CanShu1代表是添加，或者去除，CanShu2代表是全部选中项
        public static string GetListStr(ListBox List1, ListBox List2, string CanShu1, string CanShu2)
        {
            if (CanShu1 == "添加")
            {
                if (CanShu2 == "全部")
                {
                    //全部添加
                    for (int i = 0; i < List1.Items.Count; i++)
                    {
                        if (List2.Items.IndexOf(List1.Items[i]) < 0)
                        {
                            List2.Items.Add(List1.Items[i]);
                        }
                    }
                }
                else
                {
                    //部分添加
                    for (int i = 0; i < List1.Items.Count; i++)
                    {
                        if (List1.Items[i].Selected == true)
                        {
                            if (List2.Items.IndexOf(List1.Items[i]) < 0)
                            {
                                List2.Items.Add(List1.Items[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (CanShu2 == "全部")
                {
                    //全部去除
                    List2.Items.Clear();
                }
                else
                {
                    //部分去除
                    for (int i = 0; i < List2.Items.Count; i++)
                    {
                        if (List2.Items[i].Selected == true)
                        {
                            List2.Items.Remove(List2.Items[i]);
                            i = i - 1;
                        }
                    }
                }
            }
            //返回选中项的构建字符串
            string ReturnStr = string.Empty;
            for (int j = 0; j < List2.Items.Count; j++)
            {
                if (ReturnStr.Trim().Length > 0)
                {
                    ReturnStr = ReturnStr + "," + List2.Items[j].Value.Trim();
                }
                else
                {
                    ReturnStr = List2.Items[j].Value.Trim();
                }
            }
            return ReturnStr;
        }
        //从checkBoxList里面读取选中的值
        public static string GetStringFromCheckList(CheckBoxList MyChk)
        {
            string ReturnStr = string.Empty;
            for (int i = 0; i < MyChk.Items.Count; i++)
            {
                if (MyChk.Items[i].Selected == true)
                {
                    ReturnStr = ReturnStr + "|" + MyChk.Items[i].Value.ToString() + "|";
                }
            }
            return ReturnStr;
        }
        //从checkBoxList里面读中字符串中有的值
        public static void GetCheckList(CheckBoxList MyChk, string PerStr)
        {
            for (int i = 0; i < MyChk.Items.Count; i++)
            {
                if (StrIFIn("|" + MyChk.Items[i].Value.ToString() + "|", PerStr) == true)
                {
                    MyChk.Items[i].Selected = true;
                }
                else
                {
                    MyChk.Items[i].Selected = false;
                }
            }
        }
        //绑定字符串分隔开的到CheckBoxList
        public static void BindDDL(CheckBoxList MyDDL, string FenGeStr)
        {
            MyDDL.Items.Clear();
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    string OldNameStr = DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");

                    ListItem MyListItem = new ListItem();

                    //待办工作里显示附件原本的名称（sj-2014.1.15）
                    //MyListItem.Text = MyRange[i].ToString().Replace("MailAttachments/", "");
                    MyListItem.Text = OldNameStr;

                    MyListItem.Value = MyRange[i].ToString();
                    MyListItem.Selected = true;
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }
        //绑定字符串分隔开的到dropdownlist
        public static void BindDDLForEmPty(DropDownList MyDDL, string FenGeStr)
        {
            ListItem MyListItem1 = new ListItem();
            MyListItem1.Text = "";
            MyListItem1.Value = "";
            MyDDL.Items.Add(MyListItem1);
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = MyRange[i].ToString();
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }
        public static void BindDDL(DropDownList MyDDL, string FenGeStr)
        {
            MyDDL.Items.Clear();
            ListItem MyListItem1 = new ListItem();
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = MyRange[i].ToString();
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }
        public static void BindDBDDL(DropDownList MyDDL, string category)
        {
            MyDDL.Items.Clear();
            var sql = string.Format(@"select * from ERPKeyValue where [Category] ='{0}'", category);
            var source = DbHelperSQL.GetDataTable(sql);
            if (source != null)
            {
                for (int i = 0; i < source.Rows.Count; i++)
                {
                    var MyListItem = new ListItem();
                    MyListItem.Text = source.Rows[i]["Key1"].ToString();
                    MyListItem.Value = source.Rows[i]["Value1"].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }


        public static void BindDepartmentDDL(DropDownList MyDDL)
        {
            var sql = "select * from ERPBuMen where DirID>0 order by id";
            BindDepartmentDDL(MyDDL, sql);
        }
        public static void BindDepartmentDDL(DropDownList MyDDL, string sqlWhere)
        {
            BindDepartmentDDL(MyDDL, sqlWhere, "全部");
        }
        public static void BindDepartmentDDL(DropDownList MyDDL, string sqlWhere, string selected = "全部")
        {
            MyDDL.Items.Clear();
            var MyListItem1 = new ListItem()
            {
                Text = "全部",
                Value = "全部",
                Selected = selected == "全部",
            };
            MyDDL.Items.Add(MyListItem1);
            var dt = DbHelperSQL.GetDataTable(sqlWhere);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(item[0].ToString()))
                    {
                        ListItem MyListItem = new ListItem();
                        MyListItem.Text = item[0].ToString();
                        MyListItem.Value = item[0].ToString();
                        MyListItem.Selected = selected.ToString() == item[0].ToString();
                        MyDDL.Items.Add(MyListItem);
                    }
                }
            }
        }
        //在RowDataBound事件时使用
        public static void GridViewRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E4F4FF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
                e.Row.Attributes.Add("onclick", "if(self!=top){parent.parent.SimpleCheckBox(this);}else{}");
                e.Row.Attributes.Add("ondblclick", "if(self!=top){parent.parent.MuliCheckBox(this);}else{}");

            }
        }
        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));


            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

                pwd = pwd + s[i].ToString("X").PadLeft(2, '0');

            }
            return pwd;
        }
        //从grd的第rowIndex行colIndex列单元格以下count行合并
        public static void GVDataRowSpan(GridView grd, int rowIndex, int colIndex, int count)
        {
            grd.Rows[rowIndex].Cells[colIndex].RowSpan = count;
            for (var i = rowIndex + 1; i < rowIndex + count; i++)
                grd.Rows[i].Cells[colIndex].Visible = false;
        }
        //判断GridView里面被选中的ID
        public static string CheckCbx(GridView GVData, string CheckBoxName, string LabID)
        {
            return CheckCbx(GVData, CheckBoxName, LabID, true);
        }
        //判断GridView里面被选中的ID
        public static string NotCheckCbx(GridView GVData, string CheckBoxName, string LabID)
        {
            return CheckCbx(GVData, CheckBoxName, LabID, false);
        }
        public static string CheckCbx(GridView GVData, string CheckBoxName, string LabID, bool check)
        {
            string str = "";
            for (int i = 0; i < GVData.Rows.Count; i++)
            {
                GridViewRow row = GVData.Rows[i];
                CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
                Label LabVis = (Label)row.FindControl(LabID);
                if (Chk.Checked == check)
                {
                    if (str == "")
                    {
                        str = LabVis.Text.ToString();
                    }
                    else
                    {
                        str = str + "," + LabVis.Text.ToString();
                    }
                }
            }
            return str;
        }
        //判断GridView里面被选中的ID&
        public static string CheckCbxL(GridView GVData, string CheckBoxName, string LabID)
        {
            string str = "";
            for (int i = 0; i < GVData.Rows.Count; i++)
            {
                GridViewRow row = GVData.Rows[i];
                CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
                Label LabVis = (Label)row.FindControl(LabID);
                if (Chk.Checked == true)
                {
                    if (str == "")
                    {
                        if (LabVis.Text.ToString().IndexOf("&") != -1)
                        {
                            str = LabVis.Text.ToString().Remove(LabVis.Text.ToString().IndexOf("&"));
                        }
                        else
                        {
                            str = LabVis.Text.ToString();
                        }
                    }
                    else
                    {
                        if (LabVis.Text.ToString().IndexOf("&") != -1)
                        {
                            str = str + "," + LabVis.Text.ToString().Remove(LabVis.Text.ToString().IndexOf("&"));
                        }
                        else
                        {
                            str = str + "," + LabVis.Text.ToString();
                        }
                    }
                }
            }
            return str;
        }
        //判断Str1是否是在Str2这个长的字符串中
        public static bool StrIFIn(string Str1, string Str2)
        {
            if (string.IsNullOrEmpty(Str1))
                Str1 = string.Empty;
            if (string.IsNullOrEmpty(Str2))
                Str2 = string.Empty;
            if (Str2.IndexOf(Str1) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //判断Str1是否是在Str2的字符串中(会自动判断字符长度)
        public static bool StrIFInLongStr(string Str1, string Str2)
        {
            if (Str2.Length > Str1.Length)
            {
                if (Str2.IndexOf(Str1) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (Str1.IndexOf(Str2) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        //将长字符串取前面250个，然后返回
        public static string LongToShortStr(string LongStr, int StrNum)
        {
            try
            {
                return LongStr.Substring(0, StrNum);
            }
            catch
            {
                return LongStr;
            }
        }
        //提取Html中的文字信息
        public static string StripHTML(string strHtml)
        {
            string[] aryReg = { @"<script[^>]*?>.*?</script>", @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);", @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n" };
            string[] aryRep = { "", "", "", "\"", "&", "<", ">", " ", "\xa1", "\xa2", "\xa3", "\xa9", "", "\r\n", "" };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }

        //判断文件是否在允许的范围内
        public static bool IfOkFile(string DirName)
        {
            bool ReturnIF = true;
            try
            {
                int PointPosition = DirName.LastIndexOf(".");
                string FileExd = DirName.Substring(PointPosition, DirName.Length - PointPosition).Replace(".", "");
                string JKL = DbHelperSQL.GetSHSL("select FileType from ERPSystemSetting where FileType like '%|" + FileExd + "|%'");
                if (JKL.Length < 1)
                {
                    ReturnIF = false;
                }
            }
            catch
            {
                ReturnIF = false;
            }
            return ReturnIF;
        }
        //上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="MyFile">上传控件</param>
        /// <param name="DirName">文件扩展名</param>
        /// <returns></returns>
        public static string UploadFileIntoDir(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    MyFile.SaveAs(Path.Combine(UploadFileFolderTruePath, DirName));
                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    DbHelperSQL.ExecuteSQL(SqlTempStr);
                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public static string UploadFileIntoDir2(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    try
                    {
                        MyFile.SaveAs(Path.Combine(UploadFileFolderTruePath, DirName));
                        //将原文件名与现在文件名写入ERPSaveFileName表中
                        string NowName = DirName;
                        string OldName = MyFile.FileName;
                        string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                        DbHelperSQL.ExecuteSQL(SqlTempStr);
                        return DirName;
                    }
                    catch (Exception e)
                    {
                        return ReturnStr;
                    }
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                return "";
            }
        }
        //上传文件
        public static string UploadFileIntoDir(System.Web.HttpPostedFile MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.ContentLength > 0)
                {
                    MyFile.SaveAs(Path.Combine(UploadFileFolderTruePath, DirName));
                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    if (OldName.Contains("\\"))
                    {
                        var list = OldName.Split('\\');
                        OldName = list[list.Length - 1].ToString();
                    }
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    DbHelperSQL.ExecuteSQL(SqlTempStr);
                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
        //上传文件
        public static string UploadFileIntoDir_Phone(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    MyFile.SaveAs(Path.Combine(System.Web.HttpContext.Current.Request.MapPath("../../UploadFile/"), DirName));
                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    DbHelperSQL.ExecuteSQL(SqlTempStr);
                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 上传考勤机的后台数据库
        /// </summary>
        /// <param name="MyFile"></param>
        /// <param name="DirName"></param>
        /// <returns></returns>
        public static string UploadAttMDBIntoDir(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    MyFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath("../App_Data/") + DirName);
                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
        //上传文件，保存的地址略微不同
        public static string UploadFileIntoDir1(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    //上传的后台程序放在根目录下面，保存的地址略微不同
                    MyFile.SaveAs(Path.Combine(UploadFileFolderTruePath, DirName));
                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    DbHelperSQL.ExecuteSQL(SqlTempStr);
                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
        //判断Session是否有效
        public static void CheckSession()
        {
            //测试的时候注释掉
            try
            {
                if (System.Web.HttpContext.Current.Session["UserName"] == null)
                {
                    //检查Cookies
                    string adminname = GetCookie("AdminName", "DTcms"); //解密用户名
                    string adminpwd = GetCookie("AdminPwd", "DTcms");
                    var token = System.Web.HttpContext.Current.Request["token"];
                    if ((adminname.IsNullOrEmpty() || adminpwd.IsNullOrEmpty()) && !token.IsNullOrEmpty())
                    {
                        token = token.Replace("Bearer ", "");
                        var SqlSTr = "select top 1 * from Token where TokenValue='" + token + "'";
                        var MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
                        if (MyDataRow != null && DateTime.Parse(MyDataRow["ExpiresTime"].ToString()) > DateTime.Now && PublicMethod.GetInto(MyDataRow["EnabledMark"]) <= 0)
                        {
                            adminname = MyDataRow["UserName"].ToString();
                            var usql = "select * from ERPUser where UserName='" + adminname + "'";
                            var uDataRow = DbHelperSQL.GetDataRow(usql);
                            if (uDataRow != null)
                            {
                                adminname = uDataRow["UserName"].ToString();
                                adminpwd = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(uDataRow, "UserPwd"));
                            }
                        }
                    }
                    else if ((adminname.IsNullOrEmpty() || adminpwd.IsNullOrEmpty()) && HttpContext.Current.Session != null && HttpContext.Current.Session["UserName"] != null)
                    {
                        adminname = HttpContext.Current.Session["UserName"].ToString();
                        adminpwd = HttpContext.Current.Session["Password"].ToString();
                    }
                    if (adminname != "" && adminpwd != "")
                    {
                        string SqlSTr = "select * from ERPUser where UserName='" + adminname + "'";
                        DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
                        if (MyDataRow == null)
                        {
                            ReLogin();
                            //System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Login.aspx'</script>");
                        }
                        else
                        {
                            var pwd = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                            if (adminpwd == pwd)
                            {
                                if (ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                                {
                                    System.Web.HttpContext.Current.Session["UserID"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                                    System.Web.HttpContext.Current.Session["UserName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                                    System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                                    System.Web.HttpContext.Current.Session["JiaoSe"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                                    System.Web.HttpContext.Current.Session["Department"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                                    System.Web.HttpContext.Current.Session["TrueName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                                    System.Web.HttpContext.Current.Session["ZhiWei"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                                    System.Web.HttpContext.Current.Session["QuanXian"] = DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");

                                    PublicMethod.WriteCookie("AdminName", "DTcms", System.Web.HttpContext.Current.Session["UserName"].ToString());
                                    PublicMethod.WriteCookie("AdminPwd", "DTcms", System.Web.HttpContext.Current.Session["Password"].ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        ReLogin();
                    }
                }
                else
                {
                    //ReLogin();
                }
            }
            catch
            {
                ReLogin();
                //System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Login.aspx'</script>");
            }
        }
        //获得Session中的值
        public static string GetSessionValue(string SessionKey)
        {
            //测试时候使用，不掉线
            try
            {
                CheckSession();
                return System.Web.HttpContext.Current.Session[SessionKey].ToString();
            }
            catch
            {
                ReLogin();
                //System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Login.aspx'</script>");
                return "NoLogin";
            }
        }
        public static void ReLogin()
        {
            //添加是否在手机版里面登陆的情况，如果是的话就跳到moa那里，不是的话就用原来的逻辑
            var url = System.Web.HttpContext.Current.Request.RawUrl;
            var idx = url.IndexOf("/moa", StringComparison.OrdinalIgnoreCase);
            var new_idx = url.ToLower().IndexOf("/newmoa", StringComparison.OrdinalIgnoreCase);
            if (new_idx >= 0)
            {
                var rurl = "../NewMoa/Default.aspx?a=loginExpired";
                var seg = HttpContext.Current.Request.Url.Segments;
                if (seg != null && seg.Length > 4)
                {
                    rurl = "../Default.aspx?a=loginExpired";
                }
                System.Web.HttpContext.Current.Response.Write("<script>location='{0}'</script>".FormatWith(rurl));
            }
            else
            {
                if (idx >= 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>top.location='" + url.Substring(0, idx) + "/Moa" + "'</script>");
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<script>top.location='../Login.aspx'</script>");
                }
            }

        }
        //设置Session中的值
        public static void SetSessionValue(string SessionKey, string ValueStr)
        {
            System.Web.HttpContext.Current.Session[SessionKey] = ValueStr;
        }
        //根据投票的选项和的分数，生成界面的Table
        public static string GetVoteTable(string ContentStr, string ScoreStr, string IDStr, bool IFTouGuo)
        {
            string StartStr = "<table>";
            string EndStr = "</table>";
            string MidStr = string.Empty;
            string[] ContentList = ContentStr.Split('|');
            string[] ScoreList = ScoreStr.Split('|');

            //总票数
            double TotalPiao = 0;
            for (int j = 0; j < ScoreList.Length; j++)
            {
                double MaxTouPiao = double.Parse(ScoreList[j]);
                if (MaxTouPiao > TotalPiao)
                {
                    TotalPiao = MaxTouPiao;
                }
            }
            if (TotalPiao == 0)
            {
                TotalPiao = 1;
            }

            for (int i = 0; i < ContentList.Length; i++)
            {
                double PicWidth = (double.Parse(ScoreList[i]) / TotalPiao) * 250;
                if (IFTouGuo == true)
                {
                    MidStr = MidStr + "<tr><td><img src=\"../images/ShiWuSmall.jpg\" /></td><td>" + (i + 1).ToString() + "：" + ContentList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td>得票：<img src=\"../images/vote_bg.gif\" height=10 width=" + PicWidth.ToString() + "  />&nbsp;&nbsp;" + ScoreList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td></td></tr>";
                }
                else
                {
                    MidStr = MidStr + "<tr><td><img src=\"../images/ShiWuSmall.jpg\" /></td><td>" + (i + 1).ToString() + "：" + ContentList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td>得票：<img src=\"../images/vote_bg.gif\" height=10 width=" + PicWidth.ToString() + "  />&nbsp;&nbsp;" + ScoreList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td><a href=VoteYiPiao.aspx?TouPiaoTextID=" + i.ToString() + "&ID=" + IDStr + "><img border=\"0\" src=\"../images/Button/vote.gif\" /></a></td></tr>";
                }
            }
            return StartStr + MidStr + EndStr;
        }
        //判断是否已经存在该项 列名称，表名称，去除的ID名称
        public static bool IFExists(string LieName, string TableName, int ExceptID, string TextStr)
        {
            bool ReturnIF = false;
            try
            {
                string JKL = DbHelperSQL.GetSHSLInt("select count(*) from " + TableName + " where " + LieName + "='" + TextStr + "' and ID !=" + ExceptID.ToString());
                if (int.Parse(JKL) < 1)
                {
                    ReturnIF = true;
                }
            }
            catch
            {
                ReturnIF = true;
            }
            return ReturnIF;
        }

        /// <summary>
        /// 排除“已被驳回”、“不通过”的NWorkToDo表ID列表，然后返回
        /// </summary>
        /// <param name="NWorkToDoIDList"></param>
        /// <returns></returns>
        public static string GetNWorkToDoIDList(string FormID)
        {
            return GetNWorkToDoIDList(FormID, true);
        }

        /// <summary>
        /// 排除“已被驳回”、“不通过”的NWorkToDo表ID列表，然后返回
        /// </summary>
        /// <param name="NWorkToDoIDList"></param>
        /// <returns></returns>
        public static string GetNWorkToDoIDList(string FormID, bool flag)
        {
            var ReturnStr = "";
            //增加合同统计筛选功能，只提取合同归档以后的合同信息进行统计
            if (FormID == "43" && flag == true)
            {
                //拼接旧的合同编号进返回的ID列表（sj2015.4.15）
                ReturnStr = @"select LEFT(BeiYong1,CHARINDEX('@',BeiYong1)-1) BeiYong1 from ERPNWorkToDo where JieDianID=132 and FormID=43
                            ";
            }
            else
            {
                ReturnStr = "select LEFT(BeiYong1,CHARINDEX('@',BeiYong1)-1) BeiYong1 from ERPNWorkToDo where StateNow <> '已被驳回' and StateNow <> '不通过' and FormID=" + FormID;
            }
            return ReturnStr;
        }

        public static string GetConferenceLeveTextByValue(string val)
        {
            return (string)DbHelperSQL.GetSingle(string.Format(@"select Key1 from [ERPKeyValue] where Category='ConferenceLeve' and Value1='{0}'", val));
        }
        public static string GetTrainTextByValue(string val)
        {
            return (string)DbHelperSQL.GetSingle(string.Format(@"select Key1 from [ERPKeyValue] where Category='TrainMode' and Value1='{0}'", val));
        }
        public static string GetNoticeTextByValue(string val)
        {
            return (string)DbHelperSQL.GetSingle(string.Format(@"select Key1 from [ERPKeyValue] where Category='NoticeTypeStr' and Value1='{0}'", val));
        }
        public static DataTable GetKeyValueListByCategory(string cat)
        {
            var sql = string.Format(@"select * from [ERPKeyValue] where Category='{0}'", cat);
            return DbHelperSQL.GetDataTable(sql);
        }

        #region 读取或写入cookie
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            strValue = BeforeWriteCookie(strName, strValue);
            strName = BeforeWriteCookieKey(strName, strValue);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            strValue = BeforeWriteCookie(strName, strValue);
            strName = BeforeWriteCookieKey(strName, strValue);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            strValue = BeforeWriteCookie(strName, strValue);
            strName = BeforeWriteCookieKey(strName, strValue);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            strValue = BeforeWriteCookie(strName, strValue);
            strName = BeforeWriteCookieKey(strName, strValue);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            strName = AfterGetCookieKey(strName);
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UrlDecode(AfterGetCookie(strName, HttpContext.Current.Request.Cookies[strName].Value.ToString()));
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            strName = AfterGetCookieKey(strName);
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return UrlDecode(AfterGetCookie(strName, HttpContext.Current.Request.Cookies[strName][key].ToString()));
            return "";
        }
        #endregion

        #region URL处理
        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        public static string GetSysTitle()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SYSTitle"];
        }
        public static string GetUserName()
        {
            return GetSessionValue("UserName");
        }

        public static int GetUserNameID()
        {
            return DbHelperSQL.GetSHSLInt1("select ID from ERPUser where UserName='" + GetSessionValue("UserName") + "'");
        }

        public static void GridViewRowDataFormat(GridViewRowEventArgs e, string rowType, int ColIndex, DataControlRowType dataControlRowType)
        {
            if (ColIndex <= 0)//小于0就不执行
                return;
            if (e.Row.RowType == dataControlRowType)
            {
                switch (rowType)
                {
                    case "Price":
                        try
                        {
                            e.Row.Cells[ColIndex].Text = String.Format("{0:###,###,##0.00}", Double.Parse(e.Row.Cells[ColIndex].Text));
                        }
                        catch { }
                        break;
                    default:
                        break;
                }
            }
        }

        public static int GetDataTableColumnsIndex(string colName, System.Data.DataTable dt)
        {
            var i = 0;
            for (; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == colName)
                    return i;
            }
            return -1;
        }
        public static string GetRelativePath(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var pre = "..";
                url = url.TrimStart('/');
                if (!url.StartsWith(pre))
                {
                    url = "/" + url.TrimStart('/');
                }
            }

            return url;
        }
        public static string GetDepartment()
        {
            return GetSessionValue("Department");
        }

        public static string GetJiaoSe()
        {
            return GetSessionValue("JiaoSe");
        }
        public static string GetQuanXian()
        {
            return GetSessionValue("QuanXian");
        }
        public static string GetQueryString(string name)
        {
            return HttpContext.Current.Request.QueryString.Get(name);
        }
        public static string GenerateNumber(string pre, string serial)
        {
            var result = string.Empty;
            if (string.IsNullOrEmpty(pre))
            {
                pre = GetRandomChar() + GetRandomChar();
            }
            if (string.IsNullOrEmpty(serial))
            {
                serial = GetRandomNumber(3);
            }
            var dateNow = DateTime.Now;
            result = pre + dateNow.ToString("yyyyMMdd") + serial;

            return result;
        }
        public static string GetRandomChar()
        {
            var ra = new Random();
            var result = (char)ra.Next(65, 90);//从随机数直接转换成字母
            return result.ToString();
        }
        public static string GetRandomNumber(int length)
        {
            var rd = new Random();
            string str = "";
            while (str.Length < length)
            {
                int temp = rd.Next(0, 10);
                if (!str.Contains(temp + ""))
                {
                    str += temp;
                }
            }
            return str;
        }

        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        }

        public static decimal GetDecimal(object dec)
        {
            var result = decimal.Parse("0.00");
            if (dec != null && !string.IsNullOrEmpty(dec.ToString()))
            {
                if (decimal.TryParse(dec.ToString().Replace(",", ""), out result))
                {
                    result = decimal.Parse(string.Format("{0:N}", result));
                }
            }
            return result;
        }

        public static double GetDouble(object dec)
        {
            var result = double.Parse("0.00");
            if (dec != null && !string.IsNullOrEmpty(dec.ToString()))
            {
                if (double.TryParse(dec.ToString().Replace(",", ""), out result))
                {
                    result = double.Parse(String.Format("{0:F}", result));
                }
            }
            return result;
        }

        public static float GetFloat(string dec)
        {
            var result = float.Parse("0.00");
            if (!string.IsNullOrEmpty(dec))
            {
                if (float.TryParse(dec, out result))
                {
                    result = float.Parse(string.Format("{0:N}", result));
                }
            }
            return result;
        }
        public static decimal FormatDecimal(decimal? d)
        {
            var result = decimal.Parse("0.00");
            if (d.HasValue) return GetDecimal(d.Value.ToString());
            return result;
        }
        public static string FormatDecimalString(decimal? d)
        {
            var result = decimal.Parse("0.00").ToString();
            if (d.HasValue) return GetDecimal(d.Value.ToString()).ToString();
            return result;
        }
        public static string FormatMoney(decimal? dec)
        {
            var result = "0.00";
            if (dec.HasValue)
            {
                result = String.Format("{0:###,###,##0.00}", dec.Value);
            }
            return result;
        }
        public static string FormatMoney(double? dec)
        {
            var result = "0.00";
            if (dec.HasValue)
            {
                result = String.Format("{0:###,###,##0.00}", dec.Value);
            }
            return result;
        }
        public static bool IsMoney(string input)
        {
            string pattern = @"^\-{0,1}[0-9]{0,}\.{0,1}[0-9]{1,}$";
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        public static int GetInt(string dec)
        {
            return GetInto(dec);
        }
        public static int GetInt(int? it)
        {
            if (it.HasValue)
            {
                return it.Value;
            }
            return 0;
        }
        public static int GetInto(object dec)
        {
            var result = 0;
            if (dec != null && !string.IsNullOrEmpty(dec.ToString()))
            {
                int.TryParse(dec.ToString(), out result);
                return result;
            }
            return result;
        }

        /// <summary>
        /// 判断是否是数字还是自负串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsContainNumber(string str)
        {
            return IsNumeric(GetNumeric(str));
        }
        /// <summary>
        /// 判断是否是数字还是自负串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNumeric(string str)
        {
            if (str == null) str = "";
            return Regex.Replace(str, @"[^0-9]+", "");
        }
        /// <summary>
        /// 判断是否是数字还是自负串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static string SetLaborEmpFilePath(FileUpload file)
        {
            var result = string.Empty;

            var fileName = file.FileName;
            var guid = PublicMethod.GetNewGuid();
            var newName = guid + "_" + fileName;
            var rootPath = "/UploadFile/LaborEmp";
            var phyRootPath = Path.Combine(UploadFileFolderTruePath, "LaborEmp");// HttpContext.Current.Server.MapPath("~" + rootPath);

            if (!Directory.Exists(phyRootPath))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(phyRootPath);
            }
            if (file.FileBytes.Length > 0)
            {
                var filePath = Path.Combine(phyRootPath, newName);
                file.SaveAs(filePath);
                result = rootPath + "/" + newName;
            }
            return result;
        }
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return false;
            }
            var list = new List<string>() { "1", "123", "123456", "fsdzj123", "fsdzj888", "fsdzj123456", "fsdzj888888" };
            if (list.Contains(password))
            {
                return false;
            }

            bool hasLetter = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c))
                    hasLetter = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else
                    hasSymbol = true;

                // 如果密码包含字符，并且还包含数字或符号，则提前返回true  
                if ((hasLetter && hasDigit) || (hasLetter && hasSymbol) || (hasDigit && hasSymbol))
                    return true;
            }

            // 如果遍历完没有满足两个类型，返回false  
            return false;
        }
        public static string GetByteUnit(decimal ContentLength)
        {
            string byteUnit = null;
            if (ContentLength < 1024)//上传文件小于1K
            {
                byteUnit = PublicMethod.FormatMoney(ContentLength) + "B";
            }
            else if (ContentLength >= 1024 && ContentLength < 1048576)//上传文件大于等于1K小于1M
            {
                byteUnit = PublicMethod.FormatMoney(ContentLength / 1024) + "KB";
            }
            else if (ContentLength >= 1048576)//上传文件大于1M小于2MB
            {
                byteUnit = PublicMethod.FormatMoney(ContentLength / 1024 / 1024) + "MB";
            }
            else
            {
                byteUnit = null;
            }
            return byteUnit;
        }
        public static string GetFileSize(string nowName)
        {
            var filePath = Path.Combine(UploadFileFolderTruePath, nowName);
            return GetByteUnit(FileSize(filePath));
        }
        //也是利用递归的思想,只不过是通过File类的Exits方法来判断

        //所给路径中所对应的是否为文件

        public static long FileSize(string filePath)
        {
            long temp = 0;

            //判断当前路径所指向的是否为文件
            if (File.Exists(filePath) == false)
            {
                if (Directory.Exists(filePath))
                {
                    string[] str1 = Directory.GetFileSystemEntries(filePath);
                    foreach (string s1 in str1)
                    {
                        temp += FileSize(s1);
                    }
                }
            }
            else
            {

                //定义一个FileInfo对象,使之与filePath所指向的文件向关联,

                //以获取其大小
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            return temp;
        }

        public static T ConvertToModel<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0 || dt.Rows[0] == null) return default(T);

            var nModel = Activator.CreateInstance<T>();
            var type = nModel.GetType();

            var dr = dt.Rows[0];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var name = dt.Columns[i].ColumnName;
                var pinfo = type.GetProperty(name);
                if (pinfo != null && dr[i] != DBNull.Value)
                {
                    pinfo.SetValue(nModel, dr[i], null);
                }
            }
            return nModel;
        }
        public static string GetModelPropertyValueByName(object model, string name)
        {
            var result = string.Empty;
            var type = model.GetType();
            var pros = type.GetProperties();
            foreach (var item in pros)
            {
                var pinfo = type.GetProperty(name);
                if (pinfo != null)
                {
                    result = pinfo.GetValue(model, null).ToString();
                    break;
                }
            }
            return result;
        }
        public static object SetModelPropertyValueByName(object model, string name, object val)
        {
            var type = model.GetType();
            var pinfo = type.GetProperty(name);
            if (pinfo != null)
            {
                pinfo.SetValue(model, DataTableHelper.ChangeType(val, pinfo.PropertyType), null);
            }
            return model;
        }
        public static PropertyInfo GetPropertyInfoByName(PropertyInfo[] source, string name)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (name.ToLower() == source[i].Name.ToLower())
                {
                    return source[i];
                }
            }

            return null;
        }
        public static bool IsNullOrEmpty(string s)
        {
            return string.IsNullOrEmpty(s);
        }

        #region 验证身份证是否有效 
        /**//// <summary> 
            /// 验证身份证是否有效 
            /// </summary> 
            /// <param name="Id"></param> 
            /// <returns></returns> 
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }
        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证 
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证 
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证 
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证 
            }
            return true;//符合GB11643-1999标准 
        }
        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证 
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证 
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证 
            }
            return true;//符合15位身份证标准 
        }
        #endregion

        //判断Session是否有效
        public static bool SetUserActive()
        {
            //测试的时候注释掉
            try
            {
                if (System.Web.HttpContext.Current.Session["UserName"] == null)
                {
                    //检查Cookies
                    string adminname = GetCookie("AdminName", "DTcms"); //解密用户名
                    string adminpwd = GetCookie("AdminPwd", "DTcms");
                    if (adminname != "" && adminpwd != "")
                    {
                        string SqlSTr = "select * from ERPUser where UserName='" + adminname + "'";
                        DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
                        if (MyDataRow == null)
                        {
                            return false;
                        }
                        else
                        {
                            var pwd = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                            if (adminpwd == pwd)
                            {
                                if (ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                                {
                                    System.Web.HttpContext.Current.Session["UserID"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                                    System.Web.HttpContext.Current.Session["UserName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                                    System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                                    System.Web.HttpContext.Current.Session["JiaoSe"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                                    System.Web.HttpContext.Current.Session["Department"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                                    System.Web.HttpContext.Current.Session["TrueName"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                                    System.Web.HttpContext.Current.Session["ZhiWei"] = ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                                    System.Web.HttpContext.Current.Session["QuanXian"] = DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + ZWL.Common.DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            if (System.Web.HttpContext.Current.Session["UserName"] != null)
            {
                DbHelperSQL.ExecuteSQL("UPDATE [ERPUser] SET [ActiveTime] = getdate() WHERE [UserName] = '" + HttpContext.Current.Session["UserName"] + "'");
            }
            return true;
        }

        //判断是否為int如果不是直接報錯
        public static int CheckInt(string strint)
        {
            int test = 0;
            if (int.TryParse(strint, out test))
            {
                return test;
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("参数错误!<a href='javascript:history.back();'>返回</a>");
                System.Web.HttpContext.Current.Response.End();
                return 0;
            }
        }
        public static string CheckSql(string Str)
        {
            bool errflag = false;
            string SqlStr = "exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare|waitfor";
            string ReturnValue = Str;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            errflag = true;
                        }
                    }
                }
            }
            catch
            {
                errflag = true;
            }
            if (errflag)
            {
                System.Web.HttpContext.Current.Response.Write("参数错误!<a href='javascript:history.back();'>返回</a>");
                System.Web.HttpContext.Current.Response.End();
                return "";
            }
            return ReturnValue;
        }

        public static string BeforeWriteCookie(string strName, string strValue)
        {
            if (strValue == "")
                return "";
            if (strName == "DTRememberName" || strName == "AdminName")
            {
                //如果是用戶名的話进行加密
                return ZWL.Common.DEncrypt.DESEncrypt.Encrypt(strValue);
            }
            return strValue;
        }

        public static string BeforeWriteCookieKey(string strName, string strValue)
        {
            if (strName == "")
                return "";
            if (strName == "DTRememberName" || strName == "AdminName" || strName == "AdminPwd")
            {
                //如果是用戶名的話进行加密
                return ZWL.Common.DEncrypt.DESEncrypt.Encrypt(strName);
            }
            return strName;
        }

        public static string AfterGetCookie(string strName, string strValue)
        {
            if (strValue == "")
                return "";
            if (strName == AfterGetCookieKey("DTRememberName") || strName == AfterGetCookieKey("AdminName"))
            {
                //如果是用戶名的話进行加密
                return ZWL.Common.DEncrypt.DESEncrypt.Decrypt(strValue);
            }
            return strValue;
        }

        public static string AfterGetCookieKey(string strName)
        {
            if (strName == "")
                return "";
            if (strName == "DTRememberName" || strName == "AdminName" || strName == "AdminPwd")
            {
                //如果是用戶名的話进行加密
                return ZWL.Common.DEncrypt.DESEncrypt.Encrypt(strName);
            }
            return strName;
        }

        public static bool IsZhCn(string input)
        {
            return Regex.Match(input, @"[\u4e00-\u9fa5]+").Success;
        }
        public static bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }

        public static bool GetLeaveDays(string start, string end, string leaveType, ref int days, ref string msg)
        {
            var result = true;
            if (start == "")
            {
                msg = "请先选择开始时间！";
                return false;
            }
            if (end == "")
            {
                msg = "请选择结束时间！";
                return false;
            }
            DateTime dt1 = DateTime.Parse(start);
            DateTime dt2 = DateTime.Parse(end);

            TimeSpan ts1 = dt2.Subtract(dt1);//TimeSpan得到时间间隔
            int countday = ts1.Days;//获取两个日期间的总天数
            if (countday < 0)
            {
                msg = "请假截止时间要在开始时间之后！请重新选择时间！";
                return false;
            }
            int weekday = 0;//工作日
            if (leaveType == "事假" || leaveType == "年休假" || leaveType == "婚假" || leaveType == "丧假" || leaveType == "补休假")
            {
                weekday = GetWorkDays(dt1, dt2).Count;
                //工作日
            }
            else
            {
                weekday = countday + 1;//总天数
            }
            if (leaveType == "补休假")//更新年休假信息
            {
                string dep = ZWL.Common.PublicMethod.GetSessionValue("Department");
                string deplist = "中心领导,总工程师办公室,经营管理科,人事科,财务科,安全生产科,基地管理科,工会,离退休人员管理科,资料室,监察科（与纪委、审计科合署）,办公室,党委办公室,开发部";
                if (weekday > 3 && deplist.Contains(dep))
                {
                    msg = "每年最多只能请三天补休假，请重新选择。";
                    return false;
                }
            }
            days = weekday;
            return result;
        }
        public static bool IsWorkDay(DateTime date)
        {
            var list = GetWorkDays(date, date);
            if (!list.Any()) return false;
            return true;
        }
        public static List<DateTime> GetWorkDays(DateTime sdate, DateTime edate)
        {
            var list = new List<DateTime>();
            if (sdate <= edate)
            {
                var year1 = sdate.Year;
                var year2 = edate.Year;
                var ts1 = edate.Subtract(sdate);//TimeSpan得到时间间隔
                var countday = ts1.Days;//获取两个日期间的总天数
                var sql = "select * from ERPHoliday where (Year(StartTime) between {0} and {1} or Year(EndTime) between {0} and {1}) ORDER BY StartTime".FormatWith(year1, year2);
                var dt = DbHelperSQL.GetDataTable(sql);
                for (int i = 0; i <= countday; i++)
                {
                    var flag = 0;
                    var tempdt = sdate.Date.AddDays(i);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        var item = dt.Rows[j];
                        var sDate = TimeParser.GetFormatDate(item["StartTime"].ToString());
                        var eDate = TimeParser.GetFormatDate(item["EndTime"].ToString());
                        if (tempdt >= sDate && tempdt <= eDate)
                        {
                            flag = 1;
                            break;
                        }
                        else
                        {
                            var workDate = item["SwitchWorkTime"].ToString();
                            if (!string.IsNullOrEmpty(workDate))
                            {
                                foreach (var date in workDate.Split(','))
                                {
                                    var d = DateTime.MinValue;
                                    DateTime.TryParse(date, out d);
                                    if (tempdt.Date == d.Date)
                                    {
                                        flag = 2;
                                        break;
                                    }
                                }
                                if (flag == 2) break;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        if (tempdt.DayOfWeek != DayOfWeek.Saturday && tempdt.DayOfWeek != DayOfWeek.Sunday)
                        {
                            list.Add(tempdt);
                        }
                    }
                    else if (flag == 2) list.Add(tempdt);
                }
            }
            return list;
        }

        //对url中的参数进行加密
        public static string EncryptParam(object str)
        {
            if (str == null)
                return "";
            try
            {
                return "JM" + DEncrypt.DESEncrypt.Encrypt(str.ToString());
            }
            catch (CryptographicException ex)
            {
                System.Web.HttpContext.Current.Response.Write("EP参数错误!<a href='javascript:history.back();'>返回</a>");
                System.Web.HttpContext.Current.Response.End();
                return "";
            }
        }

        //对url中的参数进行解密
        public static string DecryptParam(object str)
        {
            if (str == null)
                return "";
            try
            {
                if (str.ToString().Substring(0, 2) == "JM")
                {
                    return DEncrypt.DESEncrypt.Decrypt(str.ToString().Substring(2));
                }
                else
                {
                    return str.ToString();
                }
            }
            catch (CryptographicException ex)
            {
                System.Web.HttpContext.Current.Response.Write("DP参数错误!<a href='javascript:history.back();'>返回</a>");
                System.Web.HttpContext.Current.Response.End();
                return "";
            }
        }

        //对url中的参数进行解密
        public static string GetDecryptParam(string param)
        {
            return !string.IsNullOrEmpty(Get("ID")) ? DecryptParam(Get("ID")) : "0";
        }

        //对url中的参数进行解密
        public static string GetDecryptByParam(string param)
        {
            return !string.IsNullOrEmpty(param) ? DecryptParam(param) : "0";
        }
        /// <summary>
        /// 比较两个字符串，返回结果是否正确
        /// </summary>
        public static bool BiaoJiaoTwoStr(string Str1, string Str2, string BiJiaoTiaoJian)
        {
            try
            {
                double A1 = double.Parse(Str1);
                double A2 = double.Parse(Str2); //大于  大于等于   小于  小于等于   等于   不等于  包含  不包含
                if (BiJiaoTiaoJian == "大于" && A1 > A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "大于等于" && A1 >= A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "小于" && A1 < A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "小于等于" && A1 <= A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "等于" && A1 == A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "不等于" && A1 != A2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "不包含")
                {
                    if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                if (BiJiaoTiaoJian == "等于" && Str1 == Str2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "不等于" && Str1 != Str2)
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return true;
                }
                else if (BiJiaoTiaoJian == "不包含")
                {
                    if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //对url中的参数进行解密
        public static string SendAjpush(string tag, string title, string content)
        {
            try
            {
                content = content.Replace('！', '。');
                string result = string.Empty;
                string a = ConfigurationManager.AppSettings["ajpushkey"];
                //string a = "b48ef08087113002d2279f33:bd0a6a62226e8cc4652a4412";
                byte[] b = System.Text.Encoding.Default.GetBytes(a);
                //转成 Base64 形式的 System.String  {\"alias\":[\"chenj\"]}
                a = Convert.ToBase64String(b);
                string body = "{\"platform\":[\"android\"],\"audience\":{\"tag\":[\"" + tag + "\"]},\"notification\":{\"android\":{\"title\":\"" + title + "\",\"style\":1,\"alert\":\"" + content + "\",\"extras\":{\"id\":\"11\",\"actiontype\":\"1\"}}}}";
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.jpush.cn/v3/push");
                request.Method = "POST";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + a);

                byte[] buffer = encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            catch
            {
                return "nocatcherror";
            }
        }

        /// <summary>
        /// 得到字符的字节数（中文-2字节，英文数字-1字节）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="cutnum">超过cutnum就截取字符（中文字数为单位）</param>
        /// <returns></returns>
        public static string GetCutStr(string str, int cutnum)
        {
            int strbtyesLength = 0;
            string result = str;
            string tmp = str;
            if (!string.IsNullOrEmpty(str))
            {
                tmp = System.Text.RegularExpressions.Regex.Replace(str, @"[^\w]|_", "");//替换所有的符号
                //string eng = System.Text.RegularExpressions.Regex.Split(str, @"^[a-zA-Z]")[0];
                string eng = getMatchValue(tmp, @"[a-zA-Z]");
                string ch = getMatchValue(tmp, @"[\u4e00-\u9fa5]");
                string num = getMatchValue(tmp, @"\d");
                if (!string.IsNullOrEmpty(ch))
                {
                    byte[] chbytes = System.Text.Encoding.Default.GetBytes(ch);
                    strbtyesLength += chbytes.Length;
                }
                if (!string.IsNullOrEmpty(num))
                {
                    byte[] numbytes = System.Text.Encoding.Default.GetBytes(num);
                    strbtyesLength += numbytes.Length;
                }
                if (!string.IsNullOrEmpty(eng))
                {
                    byte[] engbytes = System.Text.Encoding.Default.GetBytes(eng);
                    strbtyesLength += engbytes.Length;
                }
            }
            if (strbtyesLength > (cutnum * 2))
            {
                result = str.Substring(0, cutnum) + "...";
            }
            return result;
        }

        /// <summary>
        /// 获取正则表达式的值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regstr"></param>
        /// <returns></returns>
        public static string getMatchValue(string str, string regstr)
        {
            string result = "";
            Regex reg = new Regex(regstr);
            var mat = reg.Matches(str);
            foreach (Match item in mat)
            {
                result += item.Value;
            }
            return result;
        }
        #region 获取 本周、本月、本季度、本年 的开始时间或结束时间
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeStartByType(string TimeType, DateTime now)
        {
            now = TimeParser.GetFormatDate(now).Value;
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(-(int)now.DayOfWeek + 1);
                case "Month":
                    return now.AddDays(-now.Day + 1);
                case "Season":
                    var time = now.AddMonths(0 - ((now.Month - 1) % 3));
                    return time.AddDays(-time.Day + 1);
                case "Year":
                    return now.AddDays(-now.DayOfYear + 1);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime? GetTimeEndByType(string TimeType, DateTime now)
        {
            now = TimeParser.GetFormatDate(now).Value;
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(7 - (int)now.DayOfWeek);
                case "Month":
                    return now.AddMonths(1).AddDays(-now.AddMonths(1).Day + 1).AddDays(-1);
                case "Season":
                    var time = now.AddMonths((3 - ((now.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case "Year":
                    var time2 = now.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return null;
            }
        }
        public static string GetFormatInt(int d)
        {
            var r = string.Empty;
            if (d > 0)
                r = d.ToString();
            return r;
        }
        #endregion

        public static Dictionary<string, string> GetBoundFieldAndHeaderTextListByGrid(GridView gv, Dictionary<string, string> exFilelds)
        {
            var list = new Dictionary<string, string>();
            foreach (DataControlField item in gv.Columns)
            {
                var val = string.Empty;
                if (string.IsNullOrEmpty(item.HeaderText)) continue;
                if (item is BoundField)
                {
                    val = ((BoundField)item).DataField;
                }
                if (item is TemplateField)
                {
                    foreach (var filed in exFilelds)
                    {
                        if (filed.Value != item.HeaderText) continue;
                        else
                        {
                            val = filed.Key;
                            break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(val) && !list.ContainsKey(val))
                    list.Add(val, item.HeaderText);
            }
            return list;
        }
        public Bitmap GetImageFromBase64(string base64string)
        {
            byte[] b = Convert.FromBase64String(base64string);
            MemoryStream ms = new MemoryStream(b);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }
        public static string GetBase64FromImage(string imagefile)
        {
            string strbaser64 = "";
            try
            {
                Bitmap bmp = new Bitmap(imagefile);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                throw new Exception("转换失败!");
            }
            return strbaser64;
        }
        // 生成一个6位数的密码，包含大写字母、小写字母和数字  
        public static string GenerateStrongPassword(int length = 6)
        {
            if (length < 4)
                throw new ArgumentException("Password length must be at least 4 characters.");

            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "~!@#$%^&*()_+";

            Random random = new Random();

            // Ensure at least one character from each set is included
            char[] password = new char[length];
            password[0] = upperCase[random.Next(upperCase.Length)];
            password[1] = lowerCase[random.Next(lowerCase.Length)];
            password[2] = digits[random.Next(digits.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            // Fill the rest with random characters from all sets
            string allChars = upperCase + lowerCase + digits + specialChars;
            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            // Shuffle the password to mix the guaranteed characters with the rest
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
        /// <summary>  
        /// Base64加密，采用utf8编码方式加密  
        /// </summary>  
        /// <param name="source">待加密的明文</param>  
        /// <returns>加密后的字符串</returns>  
        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }
        /// <summary>  
        /// Base64加密  
        /// </summary>  
        /// <param name="encodeType">加密采用的编码方式</param>  
        /// <param name="source">待加密的明文</param>  
        /// <returns></returns>  
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>  
        /// Base64解密，采用utf8编码方式解密  
        /// </summary>  
        /// <param name="result">待解密的密文</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Base64Decode(string result)
        {
            return Base64Decode(Encoding.UTF8, result);
        }

        /// <summary>  
        /// Base64解密  
        /// </summary>  
        /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>  
        /// <param name="result">待解密的密文</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        public static string GetBanLiLoction(string FormID)
        {
            string result = string.Empty;
            string key = string.Empty;
            if (PublicMethod.GongWenFormIDs.Contains(FormID))
            {
                key = "综合办公";
            }
            else if (PublicMethod.JingYingFormIDs.Contains(FormID))
            {
                key = "经营管理";
            }
            else if (PublicMethod.XiangMuFormIDs.Contains(FormID))
            {
                key = "项目管理";
            }
            else if (PublicMethod.HRFormIDList.Contains(FormID))
            {
                key = "人事管理";
            }
            result = " [" + key + "-已办工作] ";
            return result;
        }
        public static List<string> GetValueBySplitStr(string str, string s, string e)
        {
            var result = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                var rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                var r = rg.Matches(str);
                foreach (var item in r)
                {
                    result.Add(item.ToString());
                }
            }

            return result;
        }
        /// <summary>
        /// 把两个字符中间的字符提取出来
        /// </summary>
        /// <param name="str">字符串</param>
        public static List<ShenPiyijian> GetShenPiYiJianList(string html)
        {
            var list = new List<ShenPiyijian>();
            if (string.IsNullOrEmpty(html)) return list;
            var namevaluelist = new Hashtable();
            var htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(html);
            var items = htmldoc.DocumentNode.SelectNodes("//font");
            if (items != null && items.Count > 0)
            {
                var source = PublicMethod.GetValueBySplitStr(html, "<BR>", "<hr>");
                var i = 1;
                foreach (var item in items)
                {
                    var nodeid = item.GetAttributeValue("data-node", 0);
                    var text = item.InnerText;
                    var p = Regex.Split(text, "&nbsp;&nbsp;", RegexOptions.IgnoreCase);
                    if (nodeid > 0)
                    {
                        if (p.Length > 3)
                        {
                            list.Add(new ShenPiyijian { ID = i, NodeID = nodeid, NodeName = p[0], UserName = p[1], TimeStamp = p[2], Comment = source[i - 1] });
                            i++;
                        }
                    }
                    else
                    {
                        list.Add(new ShenPiyijian { ID = i, UserName = p[0], TimeStamp = p[1], Comment = source[i - 1] });
                        i++;
                    }
                }
            }

            return list;
        }
        public static ShenPiyijian GetLastShenPiyijian(string html)
        {
            var list = GetShenPiYiJianList(html);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public static string GenerateGuid()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray).ToString().ToUpper().Replace("-", "");
        }
        public static string GetLimitDataSqlWhere(int formid, string valueStr)
        {
            return GetLimitDataSqlWhere(formid, valueStr, string.Empty);
        }
        public static string GetLimitDataSqlWhere(int formid, string valueStr, string colName)
        {
            var sqlWhere = string.Empty;
            var sqlWhere1 = string.Empty;
            var usql = GetLimitDataSqlWhere(valueStr);
            if (PublicMethod.CheckPower(valueStr + "W") || PublicMethod.GetJiaoSe().Contains("管理员"))
            {

            }
            else if (PublicMethod.CheckPower(valueStr + "V"))
            {
                sqlWhere1 = GetDataLimitExtendSqlWhereForChargeMan(formid, colName);
            }
            else
            {
                sqlWhere1 = GetDataLimitExtendSqlWhereForPersonal(formid, colName);
            }
            var resultsql = string.Empty;
            var checkFlag = !string.IsNullOrEmpty(sqlWhere1);
            if (checkFlag)
                resultsql = sqlWhere1;
            else
                resultsql = usql;

            if (!string.IsNullOrEmpty(resultsql))
            {
                sqlWhere += string.Format(" ({0}) ", resultsql);
            }
            return sqlWhere;
        }
        public static string GetLimitDataSqlWhere(string valueStr)
        {
            var sqlWhere = string.Empty;
            if (PublicMethod.CheckPower(valueStr + "W") || PublicMethod.GetJiaoSe().Contains("管理员"))
            {

            }
            else if (PublicMethod.CheckPower(valueStr + "V"))
            {
                sqlWhere += " UserName in (select UserName from ERPUser where Department='" + PublicMethod.GetDepartment() + "')";
            }
            else
            {
                sqlWhere += " UserName='" + PublicMethod.GetUserName() + "'";
            }
            return sqlWhere;

        }
        public static string GetDataLimitExtendSqlWhereForChargeMan(int formid)
        {
            return GetDataLimitExtendSqlWhereForChargeMan(formid, string.Empty);
        }
        public static string GetDataLimitExtendSqlWhereForChargeMan(int formid, string colName)
        {
            var result = string.Empty;
            var sqlWhere1 = GetDataLimitExtendSqlWhere(formid);
            if (string.IsNullOrEmpty(colName)) colName = "ID";
            var sqlWhere = string.Empty;
            var ksql = "select top 1 * from ERPKeyValue where Category='TableDepartmentWorkToDoRelationShip' and Value1<>'' and Key2='" + formid + "'";
            var item = DbHelperSQL.GetDataRow(ksql);
            if (item != null)
            {
                var tSql = string.Empty;
                if (!string.IsNullOrEmpty(item["Value3"].ToString()))
                {
                    tSql = item["Value3"].ToString();
                }
                else
                {
                    tSql = string.Format("select {0} from {1}", item["Value2"].ToString(), item["Key1"].ToString());
                }
                if (!string.IsNullOrEmpty(item["Value1"].ToString()))
                {
                    var sqlWhere2 = string.Format(" in ('{0}')", PublicMethod.GetDepartment());
                    sqlWhere = string.Format("{0} where {1} {2} ", tSql, item["Value1"].ToString(), sqlWhere2);
                }
            }
            var checkFlag = !string.IsNullOrEmpty(sqlWhere1);
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                result = string.Format(" ({2} in ({0}) {1} )", sqlWhere, (checkFlag ? string.Format(" or {1} in ({0})", sqlWhere1, colName) : ""), colName);
            }
            return result;
        }
        public static string GetDataLimitExtendSqlWhereForPersonal(int formid)
        {
            return GetDataLimitExtendSqlWhereForPersonal(formid, "");
        }
        public static string GetDataLimitExtendSqlWhereForPersonal(int formid, string colName)
        {
            var result = string.Empty;
            var tSql = @"select ID from ERPNWorkToDo where UserName in ('{0}')";
            var sqlWhere = string.Format(tSql, PublicMethod.GetUserName());
            var sqlWhere1 = GetDataLimitExtendSqlWhere(formid);
            if (string.IsNullOrEmpty(colName)) colName = "ID";
            var checkFlag = !string.IsNullOrEmpty(sqlWhere1);
            result = string.Format(" ({2} in ({0}) {1} )", sqlWhere, (checkFlag ? string.Format(" or {1} in ({0})", sqlWhere1, colName) : ""), colName);
            return result;
        }
        public static string GetDataLimitExtendSqlWhere(int formid)
        {
            var result = string.Empty;
            var ksql = "select top 1 * from ERPKeyValue where Category='TableDepartmentWorkToDoRelationShip' and Value1<>'' and Key2='" + formid + "'";
            var item = DbHelperSQL.GetDataRow(ksql);
            if (item != null)
            {
                var tSql = string.Empty;
                if (!string.IsNullOrEmpty(item["Value3"].ToString()))
                {
                    tSql = item["Value3"].ToString();
                }
                else
                {
                    tSql = string.Format("select {0} from {1}", item["Value2"].ToString(), item["Key1"].ToString());
                }
                var sqlWhere = GetDataLimitExtendSqlWhere();
                if (!string.IsNullOrEmpty(item["Value1"].ToString()) && !string.IsNullOrEmpty(sqlWhere))
                {
                    tSql = string.Format("{0} where {1} {2} ", tSql, item["Value1"].ToString(), sqlWhere);
                    result = tSql;
                }
            }
            return result;
        }

        public static string CombineDataLimitExtendSqlWhere(string colName, string sqlWhere1)
        {
            return CombineDataLimitExtendSqlWhere(colName, colName, sqlWhere1);
        }
        public static string CombineDataLimitExtendSqlWhere(string colName1, string colName2, string sqlWhere1)
        {
            var sqlWhere2 = GetDataLimitExtendSqlWhere(colName2);
            return string.Format(" ({0} {1} {2} {3})", colName1, sqlWhere1, (string.IsNullOrEmpty(sqlWhere2) ? string.Empty : "or"), sqlWhere2);
        }
        /// <summary>
        /// GetDataLimitExtendSqlWhere
        /// </summary>
        /// <param name="colName">字段名</param>
        /// <param name="dataType">UserName,Department</param>
        /// <returns></returns>
        public static string GetDataLimitExtendSqlWhere()
        {
            return GetDataLimitExtendSqlWhere(string.Empty);
        }
        /// <summary>
        /// GetDataLimitExtendSqlWhere
        /// </summary>
        /// <param name="colName">字段名</param>
        /// <param name="dataType">UserName,Department</param>
        /// <returns></returns>
        public static string GetDataLimitExtendSqlWhere(string colName)
        {
            var result = string.Empty;
            var sqlWhere = "select * from ERPKeyValue where Category='DataLimitExtend'";
            var dt = DbHelperSQL.GetDataTable(sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                var username = GetUserName();
                var dept = GetDepartment();
                var sb = new StringBuilder();
                var itemFormat = @"'{0}',";
                foreach (DataRow item in dt.Rows)
                {
                    switch (item["Key1"].ToString())
                    {
                        case "UserName":
                            var ulist = new List<string>();
                            foreach (var it in item["Value1"].ToString().Split(','))
                            {
                                ulist.Add(it);
                            }
                            if (ulist.Contains(username))
                            {
                                foreach (var it in item["Value2"].ToString().Split(','))
                                {
                                    sb.AppendFormat(itemFormat, it);
                                }
                            }
                            break;
                        case "Department":
                            var dlist = new List<string>();
                            foreach (var it in item["Value1"].ToString().Split(','))
                            {
                                dlist.Add(it);
                            }
                            if (dlist.Contains(dept))
                            {
                                foreach (var it in item["Value2"].ToString().Split(','))
                                {
                                    sb.AppendFormat(itemFormat, it);
                                }
                            }
                            break;
                        case "SQLUser":
                            var sqlQuery = item["Value1"].ToString();
                            var checkFlag = DbHelperSQL.GetSHSLInt1(string.Format(sqlQuery, username)) > 0;
                            if (checkFlag)
                            {
                                foreach (var it in item["Value2"].ToString().Split(','))
                                {
                                    sb.AppendFormat(itemFormat, it);
                                }
                            }
                            break;
                    }
                }
                if (sb.Length > 0)
                {
                    result = string.Format(" {0} in ({1}) ", colName, sb.ToString().TrimEnd(','));
                }
            }
            return result;
        }

        public static bool CheckPower(string powername)
        {
            return StrIFIn("|" + powername + "|", GetSessionValue("QuanXian"));
        }
        public static bool CheckFileAllowed(string fileName)
        {
            var list = new List<string> { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            var ext = Path.GetExtension(fileName);
            return list.Contains(ext);
        }
        public static string Get(string name)
        {
            var r = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                var t = System.Web.HttpContext.Current.Request[name];
                if (t != null)
                    r = t.ToString();
            }
            return r;
        }
        public static int FindRowIndex(DataTable dt, string col, string stext)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][col].ToString() == stext)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings.Get(name);
        }
        public static string GetRemoteResultByURL(string url)
        {
            var result = string.Empty;
            try
            {
                var rsb = WebRequest.Create(url);
                WebResponse p = null;
                p = rsb.GetResponse();
                using (var myreader = new StreamReader(p.GetResponseStream()))
                {
                    result = myreader.ReadToEnd();
                }
            }
            catch (Exception e) { }
            return result;
        }
        public static bool IsLetter(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }
        public static string ToJson(object model)
        {
            var result = string.Empty;
            if (model != null)
            {
                var js = new JavaScriptSerializer();
                result = js.Serialize(model);
            }
            return result;
        }
        public static string ToEscapeSql(string str)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                var list = new List<string> { "[", "]" };
                foreach (var item in list)
                {
                    if (str.Contains(item))
                        str = str.Replace(item, "%");
                }
                result = str;
            }
            return result;
        }
        public static string GetSplitInSQL(string str, string split = "|")
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                var list = str.Split(split.ToCharArray());
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    sb.AppendFormat("'{0}',", item);
                }
            }
            return sb.ToString().TrimEnd(',');
        }
        public static string BaiduMapKey
        {
            get
            {
                return GetAppSetting("BaiduMapKey");
            }
        }
        public static string BaiduGeocodingUrl
        {
            get
            {
                return "https://api.map.baidu.com/geocoding/v3/?output=json&ak=" + BaiduMapKey + "&address=";
            }
        }
        public static string BaiduReverseGeocodingUrl
        {
            get
            {
                return "https://api.map.baidu.com/reverse_geocoding/v3/?output=json&coordtype=wgs84ll&ak=" + BaiduMapKey + "&location=";
            }
        }
        public static string BaiduLocationUrl
        {
            get
            {
                return "https://api.map.baidu.com/location/ip?coor=bd09ll&ak=" + BaiduMapKey + "&ip=";
            }
        }
        public static bool CanModifyNwtd(string id)
        {
            var result = DbHelperSQL.Exists("select * from ERPNWorkToDo where (StateNow='已被驳回' or DATALENGTH(ShenPiYiJian)=0 or ShenPiYiJian is null ) and ID in (" + id + ")");
            return result;
        }
        public static bool CanDeleteNwtd(string id)
        {
            var result = DbHelperSQL.Exists("select * from ERPNWorkToDo where (StateNow='已被驳回' or DATALENGTH(ShenPiYiJian)=0 or ShenPiYiJian is null ) and ID in (" + id + ")");
            return result;
        }
        public static bool CanAddGKXMNwtd(string id)
        {
            var sql = string.Format(@"select sum(t.c) from (
                                        select COUNT(StateNow) c from ERPNWorkToDo where StateNow='正常结束' and ID={0}
                                        union all
                                        select COUNT(StateNow) c from ERPNWorkToDo where StateNow='正常结束' and FormID = 109 and BeiYong2={0}
                                        ) t", id);
            var result = DbHelperSQL.GetSHSLInt1(sql) >= 2;
            return result;
        }
        public static double GetSimilWidth(string source, string str)
        {
            decimal Kq = 2;
            decimal Kr = 1;
            decimal Ks = 1;

            char[] ss = source.ToCharArray();
            char[] st = str.ToCharArray();

            double q = ss.Intersect(st).Count();
            double t = ss.Intersect(ss).Count();

            return q / t;
        }
        public static decimal GetYoYGrowthRate(decimal curAmt, decimal lastAmt)
        {
            var result = 0M;
            if (lastAmt != 0)
            {
                result = Math.Round((curAmt - lastAmt) * 100 / lastAmt, 2);
            }
            else
            {
                if (curAmt > 0) result = 100M;
            }
            return result;
        }

        /// <summary>
        /// 传入对象A，对象B，返回B与A相同名称且不同的值
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Dictionary<string, List<object>> GetDiffMapper<A, B>(A oldEntity, B NewEntity)
        {
            Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();
            try
            {
                Type TypeOld = oldEntity.GetType();
                Type TypeNew = NewEntity.GetType();
                foreach (PropertyInfo OldEp in TypeOld.GetProperties())
                {
                    foreach (PropertyInfo NewEp in TypeNew.GetProperties())
                    {
                        if (OldEp.Name == NewEp.Name)
                        {
                            object newvalue = NewEp.GetValue(NewEntity, null) == null ? "" : NewEp.GetValue(NewEntity, null);
                            object oldvalue = OldEp.GetValue(oldEntity, null) == null ? "" : OldEp.GetValue(oldEntity, null);

                            if (!oldvalue.ToString().Equals(newvalue.ToString()))
                            {
                                //处理时间的格式
                                if (NewEp.PropertyType.FullName.ToLower().Contains("datetime"))
                                {
                                    if (!string.IsNullOrEmpty(newvalue.ToString()))
                                    {
                                        newvalue = Convert.ToDateTime(newvalue).ToString("yyyy-MM-dd");
                                    }
                                    if (!string.IsNullOrEmpty(oldvalue.ToString()))
                                    {
                                        oldvalue = Convert.ToDateTime(oldvalue).ToString("yyyy-MM-dd");
                                    }
                                }
                                List<object> tmpList = new List<object>();
                                tmpList.Add(oldvalue);
                                tmpList.Add(newvalue);
                                result.Add(NewEp.Name, tmpList);
                            }
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 判断DataSet是否为空
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool IsDataSetNullOrEmpty(DataSet ds)
        {
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static T GetPrivateField<T>(object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }

        public static T GetPrivateProperty<T>(object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            return (T)field.GetValue(instance, null);
        }
        /// <summary>
        /// 去除特殊字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MakeValidFileName(string text)
        {
            StringBuilder str = new StringBuilder();
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            foreach (var c in text)
            {
                if (!invalidFileNameChars.Contains(c))
                {
                    str.Append(c);
                }
            }

            return str.ToString();
        }
        public static string MakeValidFileName1(string hexData)
        {
            //下文中的‘\\’表示转义
            return Regex.Replace(hexData, "[ \\[ \\] \\^ \\-_*×――(^)|'$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]", "_");
        }
        public static string ShortenMD5()
        {
            return GetShortUrl(Guid.NewGuid().ToString());
        }
        public static string ShortenText(string input, int length)
        {
            return ShortenText(input, length, true);
        }
        public static string ShortenText(string input, int length, bool addEllipsis)
        {
            var text = input;
            if (!string.IsNullOrEmpty(text) && input.Length > length)
            {
                var midlen = length % 2 == 0 ? length / 2 : (length + 1) / 2;
                var ltext = text.Substring(0, midlen);
                var rtext = text.Substring(text.Length - midlen, midlen);
                var ellipsis = addEllipsis ? "..." : string.Empty;
                text = ltext + ellipsis + rtext;
            }
            return text;
        }
        public static string GetShortUrl(string url)
        {
            //可以自定义生成MD5加密字符传前的混合KEY
            string key = DateTime.Now.ToString();
            //要使用生成URL的字符
            string[] chars = new string[]{
             "a","b","c","d","e","f","g","h",
             "i","j","k","l","m","n","o","p",
             "q","r","s","t","u","v","w","x",
             "y","z","0","1","2","3","4","5",
             "6","7","8","9","A","B","C","D",
             "E","F","G","H","I","J","K","L",
             "M","N","O","P","Q","R","S","T",
             "U","V","W","X","Y","Z"
              };

            //对传入网址进行MD5加密
            string hex = GetMD5Hash(key + url);

            string[] resUrl = new string[4];
            for (int i = 0; i < 4; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                string outChars = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }
                //把字符串存入对应索引的输出数组
                resUrl[i] = outChars;
            }
            return resUrl[new Random().Next(0, 3)];
        }
        /// 取得MD5加密串
        /// </summary>
        /// <param name="input">源明文字符串</param>
        /// <returns>密文字符串</returns>
        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ResolvedAddress(string input, string key)
        {
            var result = string.Empty;
            var list = ResolvedAddress(input);
            if (list != null && list.Count > 0)
            {
                result = list[key].Value;
            }
            return result;
        }
        /// <summary>
        ///Console.WriteLine("省份：" + match.Groups["province"].Value);
        ///Console.WriteLine("城市：" + match.Groups["city"].Value);
        ///Console.WriteLine("省份/城市：" + match.Groups["province_city"].Value);
        ///Console.WriteLine("县区：" + match.Groups["district"].Value);
        ///Console.WriteLine("乡镇：" + match.Groups["town"].Value);
        ///Console.WriteLine("村庄：" + match.Groups["village"].Value);
        ///Console.WriteLine("详细地址：" + match.Groups["detail"].Value);
        /// </summary>
        /// <param name="input">输入地址字符串</param>
        /// <returns></returns>

        public static GroupCollection ResolvedAddress(string input)
        {
            //定义正则表达式
            var pattern = @"^((?<province>[^省]+省|[^市]+市|[^自治区]+自治区|[^特别行政区]+特别行政区)(?<city>[^市]+市|[^地区]+地区|[^盟]+盟|[^州]+州)?(?<district>[^县区市旗]+县|[^县区市旗]+区|[^县区市旗]+市|[^县区市旗]+旗))(?<town>[^乡镇]+乡|[^乡镇]+镇)?(?<village>[^村]+村)?(?<detail>.*)$|(?<province_city>北京市|天津市|上海市|重庆市|香港特别行政区|澳门特别行政区|台湾省|北京军区|南京军区|沈阳军区|兰州军区|成都军区|广州军区)(?<detail>.*)$";
            //创建正则对象
            var regex = new Regex(pattern);
            //匹配地址字符串
            if (input == null) input = "";
            var match = regex.Match(input);
            //输出匹配结果
            if (match.Success)
            {
                return match.Groups;
            }
            return match.Groups;
        }
        public static bool IsPasswordValid(string password)
        {
            // 检查密码长度是否大于等于 6
            if (password.IsNullOrEmpty() || password.Length < 6)
            {
                return false;
            }
            var list = new List<string>() { "1", "123", "123456", "fsdzj123", "fsdzj888", "fsdzj123456", "fsdzj888888" };
            if (list.Contains(password))
            {
                return false;
            }
            // 检查是否包含字母
            bool hasLetter = Regex.IsMatch(password, @"[a-zA-Z]");
            // 检查是否包含数字
            bool hasDigit = Regex.IsMatch(password, @"\d");
            // 检查是否包含特殊字符
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?:{ }|<>]");

            // 统计符合条件的类型数量
            int count = 0;
            if (hasLetter) count++;
            if (hasDigit) count++;
            if (hasSpecialChar) count++;

            // 至少包含两种类型
            return count >= 2;
        }
    }
    public static class GridViewExtensions
    {
        /// <summary>
        ///  GridView行合并
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="field">合并参数(匿名类型)
        /// ColumnIndex:要合并行的索引 (以0开始,必须指定)
        /// ColumnControlID(可选):如果该行为模板行则必须指定 
        /// PropertyName:根据ID属性 默认值为Text
        /// Colums:(string类型)表示额外的行合并方式和ColumnIndex一样(多个使用逗号隔开,如Colums="5,6,7,8")
        /// 例:
        /// 合并第一行(第一行为模板行),绑定的一个Label名称为lblName  根据Text属性值合并  第6行方式和第一行相同
        /// new {ColumnIndex=0,ColumnControlID="lblName",PropertyName="Text",Columns="5"}
        /// </param>
        public static GridView RowSpan(this GridView gridView, object field)
        {
            Dictionary<string, string> rowDictionary = ObjectLoadDictionary(field);
            int columnIndex = int.Parse(rowDictionary["ColumnIndex"]);
            string columnName = rowDictionary["ColumnControlID"];
            string propertyName = rowDictionary["PropertyName"];
            string columns = rowDictionary["Columns"];
            for (var i = 0; i < gridView.Rows.Count; i++)
            {

                int rowSpanCount = 1;
                for (int j = i + 1; j < gridView.Rows.Count; j++)
                {
                    //绑定行合并处理
                    if (string.IsNullOrEmpty(columnName))
                    {
                        //比较2行的值是否相同
                        if (gridView.Rows[i].Cells[columnIndex].Text == gridView.Rows[j].Cells[columnIndex].Text)
                        {
                            //合并行的数量+1
                            rowSpanCount++;
                            //隐藏相同的行
                            gridView.Rows[j].Cells[columnIndex].Visible = false;
                            if (!string.IsNullOrEmpty(columns))
                            {
                                columns.Split(',').ToList<string>().ForEach(c => gridView.Rows[j].Cells[int.Parse(c)].Visible = false);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        //模板行的合并处理
                        if (GetPropertyValue(gridView.Rows[i].Cells[columnIndex].FindControl(columnName), propertyName).ToString() == GetPropertyValue(gridView.Rows[j].Cells[columnIndex].FindControl(columnName), propertyName).ToString())
                        {
                            rowSpanCount++;
                            //隐藏相同的行
                            gridView.Rows[j].Cells[columnIndex].Visible = false;
                            if (!string.IsNullOrEmpty(columns))
                            {

                                columns.Split(',').ToList<string>().ForEach(c => gridView.Rows[j].Cells[int.Parse(c)].Visible = false);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (rowSpanCount > 1)
                {
                    //行合并
                    gridView.Rows[i].Cells[columnIndex].RowSpan = rowSpanCount;
                    //判断是否有额外的行需要合并
                    if (!string.IsNullOrEmpty(columns))
                    {
                        //额外的行合并
                        columns.Split(',').ToList<string>().ForEach(c => gridView.Rows[i].Cells[int.Parse(c)].RowSpan = rowSpanCount);
                    }
                    i = i + rowSpanCount - 1;
                }


            }
            return gridView;
        }

        private static Dictionary<string, string> ObjectLoadDictionary(object fields)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            PropertyInfo[] property = fields.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.GetProperty);
            foreach (PropertyInfo tempProperty in property)
            {
                resultDictionary.Add(tempProperty.Name, tempProperty.GetValue(fields, null).ToString());
            }
            //指定默认值
            if (!resultDictionary.Keys.Contains("ColumnIndex"))
            {
                throw new Exception("未指定要合并行的索引 ColumnIndex 属性!");
            }
            if (!resultDictionary.Keys.Contains("ColumnControlID"))
            {
                resultDictionary.Add("ColumnControlID", null);
            }

            if (!resultDictionary.Keys.Contains("PropertyName"))
            {
                resultDictionary.Add("PropertyName", "Text");
            }

            if (!resultDictionary.Keys.Contains("Columns"))
            {
                resultDictionary.Add("Columns", null);
            }




            return resultDictionary;
        }

        /// <summary>
        ///  获取一个对象的一个属性..
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyName">属性名称</param>
        /// <returns>属性的值,  如果无法获取则返回null</returns>
        private static object GetPropertyValue(object obj, string PropertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(PropertyName);
            return property.GetValue(obj, null);
        }
    }
    public class ShenPiyijian
    {
        public int ID { get; set; }
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public string UserName { get; set; }
        public string TimeStamp { get; set; }
        public string Comment { get; set; }
    }

    public class Conv<T> where T : class
    {
        public static List<T> GetList(string sql)
        {
            var dt = DbHelperSQL.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<T>(dt);
            }
            return new List<T>();
        }
        public static T GetModel(string sql)
        {
            var list = Conv<T>.GetList(sql);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public static List<T> GetListBySQLWhere(string sqlWhere)
        {
            var sql = string.Format("select * from {0} where {1}", typeof(T).Name, sqlWhere);
            return Conv<T>.GetList(sql);
        }
    }
}
