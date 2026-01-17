using ZWL.Common;

/// <summary>
/// DataHelper 的摘要说明
/// </summary>
public class DataHelper
{
    public DataHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static DataHelper Instance { get; set; }
    /// <summary>
    /// amount的获取顺序：
    /// 1，ERPProjectCost的项目金额
    /// 2，ERPProjectCost的合同金额
    /// 3，ERPProjectCost的结算金额
    /// 4，ERPHeTong的合同金额
    /// 5，ERPHTJieSuan的结算金额
    /// </summary>
    /// <param name="proId"></param>
    /// <returns></returns>
    public static decimal GetProjectCostJieSuanAmt(int proId)
    {
        decimal amt = 0;
        if (proId > 0)
        {
            var infoModel = new ZWL.BLL.ERPProjectCost();
            infoModel.GetModel(proId);
            amt = infoModel.XMJF;
            if (!infoModel.HTBH.IsNullOrEmpty())
            {
                decimal tempAmt = 0;
                if (infoModel.HTJE > 0)
                    tempAmt = infoModel.HTJE;
                if (infoModel.JSJE > 0)
                    tempAmt = infoModel.JSJE;
                if (tempAmt <= 0)
                {
                    var jssql = @"SELECT TOP 1 h.* from ERPHTJieSuan h join ERPNWorkToDo d
                              on h.NworkToDoID = d.ID 
                              where d.StateNow='正常结束'	and HTBH='{0}' ORDER BY ID desc".FormatWith(infoModel.HTBH);
                    var jsModel = Conv<ZWL.BLL.ERPHTJieSuan>.GetModel(jssql);
                    if (jsModel != null)
                    {
                        if (jsModel.TCJSJE.HasValue)
                            tempAmt = jsModel.TCJSJE.Value;
                        if (jsModel.JSJE.HasValue)
                            tempAmt = jsModel.JSJE.Value;
                    }
                }
                if (tempAmt <= 0)
                {
                    var htModel = new ZWL.BLL.ERPHeTong();
                    htModel.GetstrModel(infoModel.HTBH);
                    tempAmt = htModel.HTJE;
                }
                if (tempAmt > 0)
                    amt = tempAmt;
            }
            if (amt <= 0)
            {
                var xmModel = new ZWL.BLL.ERPXMJBXX();
                xmModel = xmModel.GetModelByXMBH(infoModel.XMBH);
                if (xmModel != null)
                {
                    amt = xmModel.XMJF;
                }
            }
        }
        return amt;
    }
}