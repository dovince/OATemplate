using System;
using System.Linq;
using System.Web.UI;
using ZWL.BLL;
using ZWL.Common;

public partial class Financial_ProjectCostModify : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();
            var Model = new ZWL.BLL.ERPProjectCost();

            Model.GetModel(Id);
            decimal costsum = PublicMethod.GetDecimal(Request.QueryString["sum"].ToString());
            this.txtCBZCHJ.Text = Model.CostMoneySUM.ToString();
            this.txtXMName.Text = Model.XMName;//用项目名称初始化合同名称


            //如果该项目不为旧项目
            if (!Model.XMBH.Contains("C"))
            {
                // 该合同金额从ERPHETong表中取出
                // txtHTJEz中的内容是合同金额
                if (Model.HTJE > 0)
                {
                    this.txtHTJE.Text = Model.HTJE.ToString();
                }
                else
                {
                    decimal htje = 0;
                    if (!Model.HTBH.IsNullOrEmpty())
                    {
                        var ht = new ZWL.BLL.ERPHeTong();
                        ht = ht.GetModelByNo(Model.HTBH);
                        if (ht != null)
                            htje = ht.HTJE;
                    }
                    this.txtHTJE.Text = htje.ToString();
                }
                if (Model.XMJF > 0)
                {
                    this.txtXMJE.Text = Model.XMJF.ToString();
                }
                else
                {
                    decimal xmjf = 0;
                    if (!Model.XMBH.IsNullOrEmpty())
                    {
                        var xm = new ZWL.BLL.ERPXMJBXX();
                        xm = xm.GetModelByXMBH(Model.XMBH);
                        if (xm != null)
                            xmjf = xm.XMJF;
                    }
                    txtXMJE.Text = xmjf.ToString();
                }

                var jsamt = Model.JSJE;
                if (jsamt > 0)
                {
                    txtJSJE.Text = jsamt.ToString();
                }
                else
                {
                    txtJSJE.Text = "0";
                    var jssql = @"SELECT top 1 * FROM ERPHTJieSuan 
                                    WHERE ID in (
                                    select max(ID) from ERPHTJieSuan where beiyong1='{0}' and HTBH='{1}'
                                    )
                                    ".FormatWith(Model.XMBH, Model.HTBH);
                    var js = Conv<ZWL.BLL.ERPHTJieSuan>.GetModel(jssql);
                    if (js != null)
                    {
                        txtJSJE.Text = js.JSJE.ToString();
                    }
                }

            }
            else
            {
                this.txtHTJE.Text = Model.HTJE.ToString();
                txtJSJE.Text = Model.JSJE.ToString();
                txtXMJE.Text = Model.XMJF.ToString();

            }


            this.txtXMID.Text = Model.XMBH;
            this.txtZYLB.Text = Model.ZYLB;
            //绑定时将几个时间设置为当前时间，修改时也方便改。
            this.txtXMstarttime.Text = Model.XMBeginTime.ToString("yyyy-MM-dd");
            this.txtXMendtime.Text = Model.XMEndTime.ToString("yyyy-MM-dd");

            this.txtXMState.Text = Model.XMState;
            this.txtHTBH.Text = Model.HTBH;


            this.txtXMbumen.Text = Model.XMBM;
            this.txtXMFZR.Text = Model.XMFZR;

        }
    }

    /// <summary>
    /// 提交按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        var msg = "";
        if (Validate(ref msg))
        {
            DateTime XMBeginTime = DateTime.Parse(txtXMstarttime.Text);

            DateTime XMEndTime = DateTime.Parse(txtXMendtime.Text);
            ZWL.BLL.ERPProjectCost Model = new ZWL.BLL.ERPProjectCost();

            Model.GetModel(Id);
            decimal XMJF = Model.XMJF;
            decimal HTJE = Model.HTJE;
            decimal JSJE = Model.JSJE;
            if (!Model.XMBH.Contains("C"))
            {
                //用于计算原结算金额与修改金额的差值
                var jsje = PublicMethod.GetDecimal(txtJSJE.Text);
                if (jsje > 0 && jsje != Model.JSJE)
                {
                    JSJE = jsje;
                    ZWL.BLL.ERPHTJieSuan jiesuan = new ZWL.BLL.ERPHTJieSuan();
                    jiesuan.beiyong1 = Model.XMBH;
                    jiesuan.HTBH = Model.HTBH;
                    jiesuan.HTName = Model.XMName;
                    jiesuan.JSJE = JSJE;
                    jiesuan.JSTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                    jiesuan.JBR = UserName;
                    jiesuan.state = "正常结束";
                    jiesuan.ID = jiesuan.Add();
                    AddLog(jiesuan);
                }
            }
            if (txtHTJE.Text != "")
            {
                HTJE = PublicMethod.GetDecimal(txtHTJE.Text);
            }


            if (txtJSJE.Text != "")
            {
                JSJE = PublicMethod.GetDecimal(txtJSJE.Text);

            }
            if (txtXMJE.Text != "")
            {
                XMJF = PublicMethod.GetDecimal(txtXMJE.Text);
            }
            decimal CostMoneySUM = 0;
            if (txtCBZCHJ.Text != "")
            {
                CostMoneySUM = PublicMethod.GetDecimal(txtCBZCHJ.Text);
            }

            string XMName = txtXMName.Text;
            string XMID = this.txtXMID.Text;
            string CostBH = "";
            string HTBH = txtHTBH.Text;
            string XMState = txtXMState.Text;
            string ZYLB = txtZYLB.Text;
            string XMBM = txtXMbumen.Text;
            string XMFZR = txtXMFZR.Text;

            var model = new ZWL.BLL.ERPProjectCost();
            model.GetModel(Id);
            if (model != null)
            {
                var shots = EditShot(model);
                model.XMBH = XMID;
                model.XMBeginTime = XMBeginTime;
                model.XMEndTime = XMEndTime;
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
                model.Update();
                EditLog(shots, model);
            }
            //写系统日志
            var MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改项目成本信息，项目名称：(" + XMBM + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            MessageBox.ShowAndRedirect(this, "项目成本信息修改成功！", "../Financial/ProjectCostView.aspx?ID=" + Id + "&sum=0");
        }
        else
        {
            MessageBox.Show(this, msg);
        }
    }
    private bool Validate(ref string msg)
    {
        var xmbh = txtXMID.Text.Trim();
        var htbh = txtHTBH.Text.Trim();
        var costList = Conv<ZWL.BLL.ERPProjectCost>.GetListBySQLWhere("XMBH='{0}' and HTBH='{1}' and ID<>{2}".FormatWith(xmbh, htbh, Id));
        if (costList != null && costList.Any())
        {
            msg = "操作失败，系统已存在相同的记录。【项目编号:{0}】【合同编号:{1}】(可考虑先随便绑一个合同编号，随后再切换回来)".FormatWith(xmbh, htbh);
            return false;
        }
        return true;
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
                this.txtHTJE.Text = ht.HTJE.ToString();
            }
            else
            {
                this.txtHTJE.Text = xmjbxx.XMJF.ToString();
            }
            txtXMJE.Text = xmjbxx.XMJF.ToString();
            //txtDJTime.Text = xmjbxx.DJTime.ToString("yyyy-MM-dd");
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
}