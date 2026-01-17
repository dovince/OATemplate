using Microsoft.Office.Interop.Excel;
using System;
using System.Web.UI;
using ZWL.BLL;
using ZWL.Common;

public partial class Financial_ProjectCostAdd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PublicMethod.CheckSession();
        }
    }
    /// <summary>
    /// 添加按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        decimal HTJE = 0;
        if (txtXMJingFei.Text != "")
        {
            HTJE = PublicMethod.GetDecimal(txtXMJingFei.Text);
        }
        decimal XMJF = 0;
        if (txt项目金额.Text != "")
        {
            XMJF = PublicMethod.GetDecimal(txt项目金额.Text);
        }

        decimal JSJE = 0;
        if (txt结算金额.Text != "")
        {
            JSJE = PublicMethod.GetDecimal(txt结算金额.Text);
        }

        decimal CostMoneySUM = 0;
        if (txt成本支出合计.Text != "")
        {
            CostMoneySUM = PublicMethod.GetDecimal(txt成本支出合计.Text);
        }

        string XMName = txtXMName.Text;
        string XMID = this.txtXMID.Text;
        string HTBH = txtHTBH.Text;
        string XMState = txtXMState.Text;
        string ZYLB = txtZYLB.Text;
        string XMBM = txtXMbumen.Text;
        string XMFZR = txtXMFZR.Text;

        ERPProjectCost model = new ERPProjectCost();
        ERPXMJBXX xmxx = new ZWL.BLL.ERPXMJBXX();
        if (XMID != "")
        {
            xmxx.GetModel(XMID);
            model.DJTime = xmxx.DJTime;
        }
        else
        {
            if (txtDJTime.Text == "")
            {
                MessageBox.Show(this, "请关联项目或填写登记时间！");
                return;
            }
            else
            {
                XMID = "C" + DateTime.Parse(txtDJTime.Text.ToString()).Year.ToString() + new Random().Next(10000, 99999).ToString();
                model.DJTime = DateTime.Parse(txtDJTime.Text.ToString());
            }
        }
        if (txtXMstarttime.Text != "")
        {
            DateTime XMBeginTime = DateTime.Parse(txtXMstarttime.Text);
            model.XMBeginTime = XMBeginTime;

        }
        if (txtXMendtime.Text != "")
        {
            DateTime XMEndTime = DateTime.Parse(txtXMendtime.Text);
            model.XMEndTime = XMEndTime;
        }

        model.HTJE = HTJE;
        model.JSJE = JSJE;
        model.XMJF = XMJF;
        model.CostMoneySUM = CostMoneySUM;
        model.XMName = XMName;
        model.XMBH = XMID;

        model.HTBH = HTBH;
        model.XMState = XMState;
        model.ZYLB = ZYLB;
        model.XMBM = XMBM;
        model.XMFZR = XMFZR;
        if (ZWL.DBUtility.DbHelperSQL.Exists("select * from ERPProjectCost where XMBH='" + model.XMBH + "' and HTBH='" + model.HTBH + "'"))
        {
            MessageBox.Show(this, "该项目编号已被登记！请重新确认");
        }
        else
        {
            var guid = Guid.NewGuid().ToString().ToLower();
            if (model.JSJE > 0)
            {
                var jiesuan = new ZWL.BLL.ERPHTJieSuan();
                jiesuan.beiyong1 = model.XMBH;
                jiesuan.HTBH = model.HTBH;
                jiesuan.HTName = model.XMName;
                jiesuan.JSJE = JSJE;
                jiesuan.JSTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                jiesuan.JBR = UserName;
                jiesuan.state = "正常结束";
                jiesuan.ID = jiesuan.Add();
                AddLog(jiesuan, guid);
            }
            model.ID = model.Add();
            AddLog(model, guid);
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加项目成本信息，项目名称：(" + XMBM + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            MessageBox.ShowAndRedirect(this, "项目成本信息添加成功！", "../Financial/ProjectCostView.aspx?ID=" + model.ID + "&sum=0");
        }
    }
    protected void btnaddsomething_Click(object sender, EventArgs e)
    {
        //从项目基本信息中取值
        if (!string.IsNullOrEmpty(this.XMJBXXID.Value))
        {
            var xmid = this.XMJBXXID.Value;
            if (xmid.Split('|').Length == 2)
                xmid = this.XMJBXXID.Value.Split('|')[0];
            var htid = this.XMJBXXID.Value.Split('|')[1];
            ZWL.BLL.ERPXMJBXX xmjbxx = new ZWL.BLL.ERPXMJBXX();
            xmjbxx.GetModel(xmid);
            ZWL.BLL.ERPHeTong ht = new ERPHeTong();
            ht = ht.GetModelByNo(htid);
            if (ht != null)
            {
                this.txtXMJingFei.Text = ht.HTJE.ToString();
            }
            else
            {
                this.txtXMJingFei.Text = xmjbxx.XMJF.ToString();
            }
            txt项目金额.Text = xmjbxx.XMJF.ToString();
            txtDJTime.Text = xmjbxx.DJTime.ToString("yyyy-MM-dd");
            this.txtXMName.Text = xmjbxx.XMName;//用项目名称初始化合同名称

            this.txtXMID.Text = xmid;
            this.txtZYLB.Text = xmjbxx.ZYLB;
            //绑定时将几个时间设置为当前时间，修改时也方便改。
            this.txtXMstarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtXMendtime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txtXMState.Text = xmjbxx.XMState;
            this.txtHTBH.Text = htid;
            this.txtXMbumen.Text = xmjbxx.XMBM;
            this.txtXMFZR.Text = xmjbxx.XMFZR;
        }
    }
    protected void ImageButton_goback_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProjectCost.aspx");
    }
}