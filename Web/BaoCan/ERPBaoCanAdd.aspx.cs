using System;
using System.Collections.Generic;
using System.Web.UI;
using ZWL.Common;
using ZWL.DBUtility;

public partial class RequireCreate_ERPBaoCanAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublicMethod.CheckSession();

            this.HiddenField_UserName.Value = PublicMethod.GetSessionValue("UserName");
            this.HiddenField_Department.Value = PublicMethod.GetSessionValue("Department");
            //获取菜单图片
            this.CaiDanImage.Value = GetCaiDanTuPian(DateTime.Now);
            //绑定工作名称
            this.txtWorkName.Text = PublicMethod.GetSessionValue("UserName") + "--员工报餐(" + DateTime.Now.ToShortDateString() + ")";

            var startdate = DateTime.Now;
            //当天早上10.5点前可报当天的午餐，最长可报1个月；
            if (DateTime.Now.Hour >= 10.5)
            {
                startdate = DateTime.Now.AddDays(1);
            }

            //this.txtBCRQ_Start.Text = startdate.ToString("yyyy-MM-dd");
            //this.txtBCRQ_End.Text = startdate.AddDays(7).ToString("yyyy-MM-dd");

            //var endbcrq = DbHelperSQL.GetSHSL("SELECT TOP 1 BCRQ FROM [ERPBaoCan] WHERE [BCRQ]>'" + startdate.AddDays(7).ToString("yyyy-MM-dd") + "' AND [UserName]='" + this.HiddenField_UserName.Value + "' order by BCRQ desc ");
            //if (!string.IsNullOrEmpty(endbcrq))
            //{
            //    this.txtBCRQ_End.Text = Convert.ToDateTime(endbcrq).ToString("yyyy-MM-dd");
            //}
            this.txtDJtime.Text = DateTime.Now.ToString();

            setpagedate(DropDownListBCFW.SelectedItem.Text);
            //报餐日期
            DateTime bcrqs = DateTime.Parse(this.txtBCRQ_Start.Text);
            DateTime bcrqe = DateTime.Parse(this.txtBCRQ_End.Text);
            SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
            //如果本周没有的话就下周
            if (SelectDateList.Count == 0)
            {
                DropDownListBCFW.SelectedIndex = 2;
                setpagedate(DropDownListBCFW.SelectedItem.Text);
                //报餐日期
                bcrqs = DateTime.Parse(this.txtBCRQ_Start.Text);
                bcrqe = DateTime.Parse(this.txtBCRQ_End.Text);
                SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
            }

            //绑定编号和名称
            if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
            {
                var erpbaocan = new ZWL.BLL.ERPBaoCan();
                erpbaocan.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
                setDefault(erpbaocan);
            }
        }
    }

    /// <summary>
    /// 表单的提交部分
    /// </summary>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //在提交表单的时候重新获取编号
        DateTime defaultime = new DateTime();
        PublicMethod.GetDefaultTime(out defaultime);

        //替换控件中的值到表单中
        ZWL.BLL.ERPBaoCan erpbaocan = new ZWL.BLL.ERPBaoCan();
        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            erpbaocan.GetModel(int.Parse(Request.QueryString["Nwid"].ToString()));
        }

        erpbaocan.WorkName = this.txtWorkName.Text;
        if (string.IsNullOrEmpty(this.txtDJtime.Text))
        {
            erpbaocan.DengJiTime = defaultime;//以此为默认时间
        }
        else
        {
            DateTime djtime = DateTime.Parse(this.txtDJtime.Text);
            erpbaocan.DengJiTime = djtime;
        }

        //是否取消
        erpbaocan.IsCancel = "否";

        //取消日期
        erpbaocan.CancelTime = defaultime;//以此为默认时间

        //用户名
        erpbaocan.UserName = this.HiddenField_UserName.Value;

        //部门
        erpbaocan.BuMen = this.HiddenField_Department.Value;

        if (!string.IsNullOrEmpty(Request.QueryString["Nwid"]))
        {
            erpbaocan.Update();//更新报餐管理信息

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改了报餐管理信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            MessageBox.ShowAndRedirect(this, "报餐管理信息变更成功！", "ERPBaoCanAdd.aspx");
        }
        else
        {
            //报餐日期
            DateTime bcrqs = DateTime.Parse(this.txtBCRQ_Start.Text);
            DateTime bcrqe = DateTime.Parse(this.txtBCRQ_End.Text);
            //报餐日期起不能大于报餐日期止
            if (bcrqe < bcrqs)
            {
                MessageBox.Show(this, "报餐日期起不能大于报餐日期止！");
                resettime(bcrqe);
                return;
            }
            //报餐日期不能报今天以前的日期
            if (DateTime.Now >= bcrqs && DateTime.Now.Hour >= 10.5)
            {
                MessageBox.Show(this, "10.5点后只能报明天或之后的日期！");
                resettime(bcrqe);
                return;
            }

            //var bcrq = DateTime.Parse(this.txtBCRQ_Start.Text);
            //var sjd = DropDownListShiJianDian.SelectedItem.Text;
            var workdays = PublicMethod.GetWorkDays(bcrqs, bcrqe);
            var i = 0;
            foreach (var workday in workdays)
            {
                //var sjd = Request["ERPBaoCan_ShiJianDian_" + i];
                var sjdzc = Request["ERPBaoCan_ShiJianDian_ZaoCan_" + i];
                var sjdwc = Request["ERPBaoCan_ShiJianDian_WuCan_" + i];

                var bcrq_request = Request["ERPBaoCan_BCRQ_" + i];
                if (bcrq_request != workday.ToString("yyyy-MM-dd"))
                {
                    MessageBox.Show(this, "报餐失败，界面数据异常！");
                    resettime(bcrqe);
                    return;
                }
                //if (sjd == "全天" || sjd == "早餐")

                if (workday.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") && DateTime.Now > DateTime.Now.Date.AddHours(6.5))
                {
                    //今天大于6点半就不影响早餐
                }
                else if (sjdzc == "早餐")
                {
                    var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐'");
                    if (num == 0)
                    {
                        erpbaocan.ShiJianDian = "早餐";
                        erpbaocan.BCRQ = workday;
                        erpbaocan.IsCancel = "否";
                        //新增
                        erpbaocan.Add();
                    }
                    else
                    {
                        //取消了的话就再报上
                        var num2 = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐' AND [IsCancel]='是'");
                        if (num2 == 1)
                        {
                            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '否',[CancelTime] = '" + workday.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐' AND [IsCancel]='是' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-10.5).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
                        }
                    }
                }
                else
                {
                    var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐'");
                    if (num == 0)
                    {
                        erpbaocan.ShiJianDian = "早餐";
                        erpbaocan.BCRQ = workday;
                        erpbaocan.IsCancel = "是";
                        //新增
                        erpbaocan.Add();
                    }
                    else
                    {
                        //取消了的话就再报上
                        var num2 = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐' AND [IsCancel]='否'");
                        if (num2 == 1)
                        {
                            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '是',[CancelTime] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐' AND [IsCancel]='否' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-10.5).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
                        }
                    }
                }

                //if (sjd == "全天" || sjd == "午餐")
                if (sjdwc == "午餐")
                {
                    var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐'");
                    if (num == 0)
                    {
                        erpbaocan.ShiJianDian = "午餐";
                        erpbaocan.BCRQ = workday;
                        erpbaocan.IsCancel = "否";
                        //新增
                        erpbaocan.Add();
                    }
                    else
                    {
                        //取消了的话就再报上
                        var num2 = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐' AND [IsCancel]='是'");
                        if (num2 == 1)
                        {
                            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '否',[CancelTime] = '" + workday.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐' AND [IsCancel]='是' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-10.5).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
                        }
                    }
                }
                else
                {
                    var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐'");
                    if (num == 0)
                    {
                        erpbaocan.ShiJianDian = "午餐";
                        erpbaocan.BCRQ = workday;
                        erpbaocan.IsCancel = "是";
                        //新增
                        erpbaocan.Add();
                    }
                    else
                    {
                        //取消了的话就再报上
                        var num2 = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐' AND [IsCancel]='否'");
                        if (num2 == 1)
                        {
                            var result = ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [ERPBaoCan] SET [IsCancel] = '是',[CancelTime] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐' AND [IsCancel]='否' and (([ShiJianDian] = '午餐' and [BCRQ] > '" + DateTime.Now.AddHours(-10.5).ToString("yyyy-MM-dd") + "') or ([ShiJianDian] = '早餐' and [BCRQ] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'))");
                        }
                    }
                }
                i++;
            }
            //while (bcrq <= bcrqe)
            //{
            //    if(sjd == "全天" || sjd == "早餐")
            //    {
            //        var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + bcrq.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='早餐' AND [IsCancel]='否'");
            //        if(num == 0)
            //        {
            //            erpbaocan.ShiJianDian = "早餐";
            //            erpbaocan.BCRQ = bcrq;
            //            //新增
            //            erpbaocan.Add();
            //        }
            //    }

            //    if (sjd == "全天" || sjd == "午餐")
            //    {
            //        var num = DbHelperSQL.GetSHSLInt1("SELECT COUNT(ID) FROM [ERPBaoCan] WHERE [BCRQ]='" + bcrq.ToString("yyyy-MM-dd") + "' AND [UserName]='" + erpbaocan.UserName + "' AND [ShiJianDian]='午餐' AND [IsCancel]='否'");
            //        if(num == 0)
            //        {
            //            erpbaocan.ShiJianDian = "午餐";
            //            erpbaocan.BCRQ = bcrq;
            //            //新增
            //            erpbaocan.Add();
            //        }
            //    }

            //    bcrq = bcrq.AddDays(1);
            //}

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加报餐信息(" + bcrqs.ToString("yyyy-MM-dd") + "至" + bcrqe.ToString("yyyy-MM-dd") + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            //MessageBox.ShowAndRedirect(this, "报餐成功！", "ERPBaoCanAdd.aspx");
            MessageBox.Show(this, "报餐成功！");
            SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
        }
    }

    public void setDefault(ZWL.BLL.ERPBaoCan erpbaocan)
    {

        //报餐日期

        //this.txtBCRQ.Text = erpbaocan.BCRQ != null ? erpbaocan.BCRQ.ToString("yyyy-MM-dd") : "";

        ////是否取消

        //this.txtIsCancel.Text = erpbaocan.IsCancel;

        ////取消日期

        //this.txtCancelTime.Text = erpbaocan.CancelTime != null ? erpbaocan.CancelTime.ToString("yyyy-MM-dd") : "";

        //用户名

        this.HiddenField_UserName.Value = erpbaocan.UserName;

        //部门

        this.HiddenField_Department.Value = erpbaocan.BuMen;

    }

    public List<DateTime> SelectDateList = new List<DateTime>();
    protected void Time_TextChanged(object sender, EventArgs e)
    {
        //报餐日期
        DateTime bcrqs = DateTime.Parse(this.txtBCRQ_Start.Text);
        DateTime bcrqe = DateTime.Parse(this.txtBCRQ_End.Text);

        //报餐日期不能报今天以前的日期，当天晚上24点前可报第二天及以后的早餐及午餐
        if ((DateTime.Now >= bcrqs && DateTime.Now.Hour >= 10.5) || (DateTime.Now.Hour >= 23 && DateTime.Now.AddDays(1) >= bcrqs))
        {
            if (DateTime.Now >= bcrqs && DateTime.Now.Hour >= 10.5)
            {
                MessageBox.Show(this, "10点30分后只能报明天或之后的日期！");
            }
            else
            {
                MessageBox.Show(this, "晚上24点后只能报后天或之后的日期！");
            }
            bcrqs = DateTime.Now;
            if (DateTime.Now.Hour >= 10.5)
            {
                bcrqs = DateTime.Now.AddDays(1);
            }
            bcrqs = Convert.ToDateTime(bcrqs.ToString("yyyy-MM-dd"));
            this.txtBCRQ_Start.Text = bcrqs.ToString("yyyy-MM-dd");
        }
        else if (bcrqe < bcrqs)
        {
            MessageBox.Show(this, "报餐日期起不能大于报餐日期止！");
            bcrqe = bcrqs;
            this.txtBCRQ_End.Text = bcrqs.ToString("yyyy-MM-dd");
        }
        //限制最大报餐时间最多可报餐今日起1个月内的
        else if (DateTime.Now.AddDays(31) < bcrqe)
        {
            MessageBox.Show(this, "最多可报餐今日起1个月内的！");
            bcrqe = DateTime.Now.AddDays(31);
            this.txtBCRQ_End.Text = bcrqe.ToString("yyyy-MM-dd");
        }

        SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
    }

    public void setpagedate(string bcfw)
    {
        var startdate = DateTime.Now;
        //当天早上10.5点前可报当天的午餐，最长可报1个月；
        if (DateTime.Now.Hour >= 10.5)
        {
            startdate = DateTime.Now.AddDays(1);
        }
        var enddate = DateTime.Now;

        if (bcfw == "本周")
        {
            while (enddate.DayOfWeek != DayOfWeek.Sunday)
            {
                enddate = enddate.AddDays(1);
            }
        }
        else if (bcfw == "明天")
        {
            startdate = DateTime.Now.AddDays(1);
            enddate = startdate;
        }
        else if (bcfw == "下周")
        {
            while (startdate.DayOfWeek != DayOfWeek.Monday)
            {
                startdate = startdate.AddDays(1);
            }
            enddate = startdate.AddDays(6);
        }
        else if (bcfw == "本月")
        {
            enddate = Convert.ToDateTime(startdate.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
        }
        else if (bcfw == "未来30天")
        {
            enddate = DateTime.Now.AddDays(30);
        }
        this.txtBCRQ_Start.Text = startdate.ToString("yyyy-MM-dd");
        this.txtBCRQ_End.Text = enddate.ToString("yyyy-MM-dd");
    }

    protected void DropDownListBCFW_Changed(object sender, EventArgs e)
    {
        setpagedate(DropDownListBCFW.SelectedItem.Text);
        //报餐日期
        DateTime bcrqs = DateTime.Parse(this.txtBCRQ_Start.Text);
        DateTime bcrqe = DateTime.Parse(this.txtBCRQ_End.Text);
        SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
    }

    public string GetCaiDanTuPian(DateTime dt)
    {
        //获取菜单图片
        //var caidan = DbHelperSQL.GetSHSL("SELECT TOP 1 CaiDanTuPian FROM [ERPCaiDan] WHERE [ZhanShiRiQiQi]<='" + dt.ToString("yyyy-MM-dd") + "' AND [ZhanShiRiQiZhi]>='" + dt.ToString("yyyy-MM-dd") + "' ");
        var caidan = DbHelperSQL.GetSHSL("SELECT TOP 1 CaiDanTuPian FROM [ERPCaiDan] order by id desc ");
        return caidan.Replace("|", "");
    }

    public string GetDayOfWeek(DateTime dt)
    {
        string[] DayWeekArray = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        return DayWeekArray[Convert.ToInt32(dt.DayOfWeek.ToString("d"))].ToString();
    }

    public string IsChecked(DateTime workday, string sjd)
    {
        //判断该天是否已经报餐
        var IsCancel = DbHelperSQL.GetSHSL("SELECT TOP 1 IsCancel FROM [ERPBaoCan] WHERE [BCRQ]='" + workday.ToString("yyyy-MM-dd") + "' AND [UserName]='" + this.HiddenField_UserName.Value + "' AND [ShiJianDian]='" + sjd + "'");
        if (IsCancel == "是")
        {
            return "";
        }

        //没有的话就默认全选
        return "checked=true";
    }
    //子表

    protected void resettime(DateTime bcrqe)
    {
        var bcrqs = DateTime.Now;
        if (DateTime.Now.Hour >= 10.5)
        {
            bcrqs = DateTime.Now.AddDays(1);
        }
        this.txtBCRQ_Start.Text = bcrqs.ToString("yyyy-MM-dd");
        if (bcrqe == null)
        {
            SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqs.AddDays(7));
        }
        else
        {
            SelectDateList = PublicMethod.GetWorkDays(bcrqs, bcrqe);
        }
    }
}

