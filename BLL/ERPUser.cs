using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Web;//请先添加引用
using ZWL.Common;
using System.Collections.Generic;
using System.Web.UI;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPUser。
    /// </summary>
    public class ERPUser
    {
        public ERPUser()
        { }
        public bool checkactive = false;
        #region Model
        private int _id;
        private int _paixu;
        private string _username;
        private string _userpwd;
        private string _truename;
        private string _serils;
        private string _department;
        private string _jiaose;
        private DateTime? _activetime;
        private string _zhiwei;
        private string _zaigang;
        private string _emailstr;
        private string _iflogin;
        private string _sex;
        private string _backinfo;
        private string _birthday;
        private string _mingzu;
        private string _sfzserils;
        private string _hunying;
        private string _zhengzhimianmao;
        private string _jiguan;
        private string _hukou;
        private string _xueli;
        private string _zhicheng;
        private string _biyeyuanxiao;
        private string _zhuanye;
        private string _canjiagongzuotime;
        private string _jiarubendanweitime;
        private int _gongling;//工龄
        private string _jiatingdianhua;
        private string _jiatingaddress;
        private string _gangweibiandong;
        private string _jiaoyuebeijing;
        private string _gongzuojianli;
        private string _shehuiguanxi;
        private string _jiangchengjilu;
        private string _zhiwuqingkuang;
        private string _peixunjilu;
        private string _danbaojilu;
        private string _naodonghetong;
        private string _shebaojiaona;
        private string _tijianjilu;
        private string _beizhustr;
        private string _fujian;
        private int _pwdFailCount;//输入密码失败次数
        private DateTime? _pwdFailTime;//输入密码失败时间
        private string _biyezhuanye;
        private string _zhibie;
        private DateTime? _biyetime;
        private DateTime? _jiarudangpaitime;
        private string _zhichenglevel;
        private string _foshanjurencaiku;
        private DateTime? _getzhichengtime;
        private string _xuewei;
        private DateTime? _pwdModifyTime;


        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Paixu
        {
            set { _paixu = value; }
            get { return _paixu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Serils
        {
            set { _serils = value; }
            get { return _serils; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiaoSe
        {
            set { _jiaose = value; }
            get { return _jiaose; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ActiveTime
        {
            set { _activetime = value; }
            get { return _activetime; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string ZhiWei
        {
            set { _zhiwei = value; }
            get { return _zhiwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZaiGang
        {
            set { _zaigang = value; }
            get { return _zaigang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmailStr
        {
            set { _emailstr = value; }
            get { return _emailstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IfLogin
        {
            set { _iflogin = value; }
            get { return _iflogin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackInfo
        {
            set { _backinfo = value; }
            get { return _backinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BirthDay
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MingZu
        {
            set { _mingzu = value; }
            get { return _mingzu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SFZSerils
        {
            set { _sfzserils = value; }
            get { return _sfzserils; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HunYing
        {
            set { _hunying = value; }
            get { return _hunying; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhengZhiMianMao
        {
            set { _zhengzhimianmao = value; }
            get { return _zhengzhimianmao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiGuan
        {
            set { _jiguan = value; }
            get { return _jiguan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HuKou
        {
            set { _hukou = value; }
            get { return _hukou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XueLi
        {
            set { _xueli = value; }
            get { return _xueli; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhiCheng
        {
            set { _zhicheng = value; }
            get { return _zhicheng; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BiYeYuanXiao
        {
            set { _biyeyuanxiao = value; }
            get { return _biyeyuanxiao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuanYe
        {
            set { _zhuanye = value; }
            get { return _zhuanye; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanJiaGongZuoTime
        {
            set { _canjiagongzuotime = value; }
            get { return _canjiagongzuotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiaRuBenDanWeiTime
        {
            set { _jiarubendanweitime = value; }
            get { return _jiarubendanweitime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GongLing
        {
            set { _gongling = value; }
            get { return _gongling; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiaTingDianHua
        {
            set { _jiatingdianhua = value; }
            get { return _jiatingdianhua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiaTingAddress
        {
            set { _jiatingaddress = value; }
            get { return _jiatingaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GangWeiBianDong
        {
            set { _gangweibiandong = value; }
            get { return _gangweibiandong; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiaoYueBeiJing
        {
            set { _jiaoyuebeijing = value; }
            get { return _jiaoyuebeijing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GongZuoJianLi
        {
            set { _gongzuojianli = value; }
            get { return _gongzuojianli; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SheHuiGuanXi
        {
            set { _shehuiguanxi = value; }
            get { return _shehuiguanxi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiangChengJiLu
        {
            set { _jiangchengjilu = value; }
            get { return _jiangchengjilu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhiWuQingKuang
        {
            set { _zhiwuqingkuang = value; }
            get { return _zhiwuqingkuang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PeiXunJiLu
        {
            set { _peixunjilu = value; }
            get { return _peixunjilu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DanBaoJiLu
        {
            set { _danbaojilu = value; }
            get { return _danbaojilu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NaoDongHeTong
        {
            set { _naodonghetong = value; }
            get { return _naodonghetong; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SheBaoJiaoNa
        {
            set { _shebaojiaona = value; }
            get { return _shebaojiaona; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TiJianJiLu
        {
            set { _tijianjilu = value; }
            get { return _tijianjilu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeiZhuStr
        {
            set { _beizhustr = value; }
            get { return _beizhustr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FuJian
        {
            set { _fujian = value; }
            get { return _fujian; }
        }
        public int PwdFailCount
        {
            set { _pwdFailCount = value; }
            get { return _pwdFailCount; }
        }
        public DateTime? PwdFailTime
        {
            set { _pwdFailTime = value; }
            get { return _pwdFailTime; }
        }
        /// <summary>
        /// 毕业专业
        /// </summary>
        public string BiYeZhuanYe
        {
            set { _biyezhuanye = value; }
            get { return _biyezhuanye; }
        }
        /// <summary>
        /// 职别
        /// </summary>
        public string ZhiBie
        {
            set { _zhibie = value; }
            get { return _zhibie; }
        }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? BiYeTime
        {
            set { _biyetime = value; }
            get { return _biyetime; }
        }
        /// <summary>
        /// 加入党派时间
        /// </summary>
        public DateTime? JiaRuDangPaiTime
        {
            set { _jiarudangpaitime = value; }
            get { return _jiarudangpaitime; }
        }
        /// <summary>
        /// 职称职别
        /// </summary>
        public string ZhiChengLevel
        {
            set { _zhichenglevel = value; }
            get { return _zhichenglevel; }
        }
        /// <summary>
        /// 佛山局人才库
        /// </summary>
        public string FoShanJuRenCaiKu
        {
            set { _foshanjurencaiku = value; }
            get { return _foshanjurencaiku; }
        }
        /// <summary>
        /// 获取职称时间
        /// </summary>
        public DateTime? GetZhiChengTIme
        {
            set { _getzhichengtime = value; }
            get { return _getzhichengtime; }
        }
        /// <summary>
        /// 学位
        /// </summary>
        public string XueWei
        {
            set { _xuewei = value; }
            get { return _xuewei; }
        }
        /// <summary>
        /// 获取职称时间
        /// </summary>
        public DateTime? PwdModifyTime
        {
            set { _pwdModifyTime = value; }
            get { return _pwdModifyTime; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPUser");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPUser(");
            strSql.Append("UserName,UserPwd,TrueName,Serils,Department,JiaoSe,ActiveTime,ZhiWei,ZaiGang,EmailStr,IfLogin,Sex,BackInfo,BirthDay,MingZu,SFZSerils,HunYing,ZhengZhiMianMao,JiGuan,HuKou,XueLi,ZhiCheng,BiYeYuanXiao,ZhuanYe,CanJiaGongZuoTime,JiaRuBenDanWeiTime,GongLing,JiaTingDianHua,JiaTingAddress,GangWeiBianDong,JiaoYueBeiJing,GongZuoJianLi,SheHuiGuanXi,JiangChengJiLu,ZhiWuQingKuang,PeiXunJiLu,DanBaoJiLu,NaoDongHeTong,SheBaoJiaoNa,TiJianJiLu,BeiZhuStr,FuJian,Paixu)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@UserPwd,@TrueName,@Serils,@Department,@JiaoSe,@ActiveTime,@ZhiWei,@ZaiGang,@EmailStr,@IfLogin,@Sex,@BackInfo,@BirthDay,@MingZu,@SFZSerils,@HunYing,@ZhengZhiMianMao,@JiGuan,@HuKou,@XueLi,@ZhiCheng,@BiYeYuanXiao,@ZhuanYe,@CanJiaGongZuoTime,@JiaRuBenDanWeiTime,@GongLing,@JiaTingDianHua,@JiaTingAddress,@GangWeiBianDong,@JiaoYueBeiJing,@GongZuoJianLi,@SheHuiGuanXi,@JiangChengJiLu,@ZhiWuQingKuang,@PeiXunJiLu,@DanBaoJiLu,@NaoDongHeTong,@SheBaoJiaoNa,@TiJianJiLu,@BeiZhuStr,@FuJian,@Paixu)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserPwd", SqlDbType.VarChar,200),
                    new SqlParameter("@TrueName", SqlDbType.VarChar,50),
                    new SqlParameter("@Serils", SqlDbType.VarChar,50),
                    new SqlParameter("@Department", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaoSe", SqlDbType.VarChar,200),
                    new SqlParameter("@ActiveTime", SqlDbType.DateTime),
                    new SqlParameter("@ZhiWei", SqlDbType.VarChar,50),
                    new SqlParameter("@ZaiGang", SqlDbType.VarChar,50),
                    new SqlParameter("@EmailStr", SqlDbType.VarChar,50),
                    new SqlParameter("@IfLogin", SqlDbType.VarChar,50),
                    new SqlParameter("@Sex", SqlDbType.VarChar,50),
                    new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
                    new SqlParameter("@BirthDay", SqlDbType.VarChar,50),
                    new SqlParameter("@MingZu", SqlDbType.VarChar,50),
                    new SqlParameter("@SFZSerils", SqlDbType.VarChar,50),
                    new SqlParameter("@HunYing", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.VarChar,50),
                    new SqlParameter("@JiGuan", SqlDbType.VarChar,50),
                    new SqlParameter("@HuKou", SqlDbType.VarChar,500),
                    new SqlParameter("@XueLi", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhiCheng", SqlDbType.VarChar,50),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhuanYe", SqlDbType.VarChar,50),
                    new SqlParameter("@CanJiaGongZuoTime", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaRuBenDanWeiTime", SqlDbType.VarChar,50),
                    new SqlParameter("@GongLing", SqlDbType.Int,6),
                    new SqlParameter("@JiaTingDianHua", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaTingAddress", SqlDbType.VarChar,500),
                    new SqlParameter("@GangWeiBianDong", SqlDbType.Text),
                    new SqlParameter("@JiaoYueBeiJing", SqlDbType.Text),
                    new SqlParameter("@GongZuoJianLi", SqlDbType.Text),
                    new SqlParameter("@SheHuiGuanXi", SqlDbType.Text),
                    new SqlParameter("@JiangChengJiLu", SqlDbType.Text),
                    new SqlParameter("@ZhiWuQingKuang", SqlDbType.Text),
                    new SqlParameter("@PeiXunJiLu", SqlDbType.Text),
                    new SqlParameter("@DanBaoJiLu", SqlDbType.Text),
                    new SqlParameter("@NaoDongHeTong", SqlDbType.Text),
                    new SqlParameter("@SheBaoJiaoNa", SqlDbType.Text),
                    new SqlParameter("@TiJianJiLu", SqlDbType.Text),
                    new SqlParameter("@BeiZhuStr", SqlDbType.Text),
                    new SqlParameter("@FuJian", SqlDbType.VarChar,5000),
                    new SqlParameter("@Paixu",SqlDbType.Int,6)};
            parameters[0].Value = UserName;
            parameters[1].Value = UserPwd;
            parameters[2].Value = TrueName;
            parameters[3].Value = Serils;
            parameters[4].Value = Department;
            parameters[5].Value = JiaoSe;
            parameters[6].Value = ActiveTime;
            parameters[7].Value = ZhiWei;
            parameters[8].Value = ZaiGang;
            parameters[9].Value = EmailStr;
            parameters[10].Value = IfLogin;
            parameters[11].Value = Sex;
            parameters[12].Value = BackInfo;
            parameters[13].Value = BirthDay;
            parameters[14].Value = MingZu;
            parameters[15].Value = SFZSerils;
            parameters[16].Value = HunYing;
            parameters[17].Value = ZhengZhiMianMao;
            parameters[18].Value = JiGuan;
            parameters[19].Value = HuKou;
            parameters[20].Value = XueLi;
            parameters[21].Value = ZhiCheng;
            parameters[22].Value = BiYeYuanXiao;
            parameters[23].Value = ZhuanYe;
            parameters[24].Value = CanJiaGongZuoTime;
            parameters[25].Value = JiaRuBenDanWeiTime;
            parameters[26].Value = GongLing;
            parameters[27].Value = JiaTingDianHua;
            parameters[28].Value = JiaTingAddress;
            parameters[29].Value = GangWeiBianDong;
            parameters[30].Value = JiaoYueBeiJing;
            parameters[31].Value = GongZuoJianLi;
            parameters[32].Value = SheHuiGuanXi;
            parameters[33].Value = JiangChengJiLu;
            parameters[34].Value = ZhiWuQingKuang;
            parameters[35].Value = PeiXunJiLu;
            parameters[36].Value = DanBaoJiLu;
            parameters[37].Value = NaoDongHeTong;
            parameters[38].Value = SheBaoJiaoNa;
            parameters[39].Value = TiJianJiLu;
            parameters[40].Value = BeiZhuStr;
            parameters[41].Value = FuJian;
            parameters[42].Value = Paixu;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdatePwd()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPUser set ");
            strSql.Append("UserPwd=@UserPwd,PwdModifyTime=@PwdModifyTime");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
                    new SqlParameter("@UserPwd", SqlDbType.VarChar,200),
                    new SqlParameter("@PwdModifyTime", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = UserPwd;
            parameters[2].Value = PwdModifyTime;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPUser set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPwd=@UserPwd,");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("Serils=@Serils,");
            strSql.Append("Department=@Department,");
            strSql.Append("JiaoSe=@JiaoSe,");
            strSql.Append("ZhiWei=@ZhiWei,");
            strSql.Append("ZaiGang=@ZaiGang,");
            strSql.Append("EmailStr=@EmailStr,");
            strSql.Append("IfLogin=@IfLogin,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("BackInfo=@BackInfo,");
            strSql.Append("BirthDay=@BirthDay,");
            strSql.Append("MingZu=@MingZu,");
            strSql.Append("SFZSerils=@SFZSerils,");
            strSql.Append("HunYing=@HunYing,");
            strSql.Append("ZhengZhiMianMao=@ZhengZhiMianMao,");
            strSql.Append("JiGuan=@JiGuan,");
            strSql.Append("HuKou=@HuKou,");
            strSql.Append("XueLi=@XueLi,");
            strSql.Append("ZhiCheng=@ZhiCheng,");
            strSql.Append("BiYeYuanXiao=@BiYeYuanXiao,");
            strSql.Append("ZhuanYe=@ZhuanYe,");
            strSql.Append("CanJiaGongZuoTime=@CanJiaGongZuoTime,");
            strSql.Append("JiaRuBenDanWeiTime=@JiaRuBenDanWeiTime,");
            strSql.Append("GongLing=@GongLing,");
            strSql.Append("JiaTingDianHua=@JiaTingDianHua,");
            strSql.Append("JiaTingAddress=@JiaTingAddress,");
            strSql.Append("GangWeiBianDong=@GangWeiBianDong,");
            strSql.Append("JiaoYueBeiJing=@JiaoYueBeiJing,");
            strSql.Append("GongZuoJianLi=@GongZuoJianLi,");
            strSql.Append("SheHuiGuanXi=@SheHuiGuanXi,");
            strSql.Append("JiangChengJiLu=@JiangChengJiLu,");
            strSql.Append("ZhiWuQingKuang=@ZhiWuQingKuang,");
            strSql.Append("PeiXunJiLu=@PeiXunJiLu,");
            strSql.Append("DanBaoJiLu=@DanBaoJiLu,");
            strSql.Append("NaoDongHeTong=@NaoDongHeTong,");
            strSql.Append("SheBaoJiaoNa=@SheBaoJiaoNa,");
            strSql.Append("TiJianJiLu=@TiJianJiLu,");
            strSql.Append("BeiZhuStr=@BeiZhuStr,");
            strSql.Append("FuJian=@FuJian");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserPwd", SqlDbType.VarChar,200),
                    new SqlParameter("@TrueName", SqlDbType.VarChar,50),
                    new SqlParameter("@Serils", SqlDbType.VarChar,50),
                    new SqlParameter("@Department", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaoSe", SqlDbType.VarChar,200),
                    new SqlParameter("@ZhiWei", SqlDbType.VarChar,50),
                    new SqlParameter("@ZaiGang", SqlDbType.VarChar,50),
                    new SqlParameter("@EmailStr", SqlDbType.VarChar,50),
                    new SqlParameter("@IfLogin", SqlDbType.VarChar,50),
                    new SqlParameter("@Sex", SqlDbType.VarChar,50),
                    new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
                    new SqlParameter("@BirthDay", SqlDbType.VarChar,50),
                    new SqlParameter("@MingZu", SqlDbType.VarChar,50),
                    new SqlParameter("@SFZSerils", SqlDbType.VarChar,50),
                    new SqlParameter("@HunYing", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.VarChar,50),
                    new SqlParameter("@JiGuan", SqlDbType.VarChar,50),
                    new SqlParameter("@HuKou", SqlDbType.VarChar,500),
                    new SqlParameter("@XueLi", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhiCheng", SqlDbType.VarChar,50),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.VarChar,50),
                    new SqlParameter("@ZhuanYe", SqlDbType.VarChar,50),
                    new SqlParameter("@CanJiaGongZuoTime", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaRuBenDanWeiTime", SqlDbType.VarChar,50),
                    new SqlParameter("@GongLing", SqlDbType.Int,6),
                    new SqlParameter("@JiaTingDianHua", SqlDbType.VarChar,50),
                    new SqlParameter("@JiaTingAddress", SqlDbType.VarChar,500),
                    new SqlParameter("@GangWeiBianDong", SqlDbType.Text),
                    new SqlParameter("@JiaoYueBeiJing", SqlDbType.Text),
                    new SqlParameter("@GongZuoJianLi", SqlDbType.Text),
                    new SqlParameter("@SheHuiGuanXi", SqlDbType.Text),
                    new SqlParameter("@JiangChengJiLu", SqlDbType.Text),
                    new SqlParameter("@ZhiWuQingKuang", SqlDbType.Text),
                    new SqlParameter("@PeiXunJiLu", SqlDbType.Text),
                    new SqlParameter("@DanBaoJiLu", SqlDbType.Text),
                    new SqlParameter("@NaoDongHeTong", SqlDbType.Text),
                    new SqlParameter("@SheBaoJiaoNa", SqlDbType.Text),
                    new SqlParameter("@TiJianJiLu", SqlDbType.Text),
                    new SqlParameter("@BeiZhuStr", SqlDbType.Text),
                    new SqlParameter("@FuJian", SqlDbType.VarChar,5000)};
            parameters[0].Value = ID;
            parameters[1].Value = UserName;
            parameters[2].Value = UserPwd;
            parameters[3].Value = TrueName;
            parameters[4].Value = Serils;
            parameters[5].Value = Department;
            parameters[6].Value = JiaoSe;
            parameters[7].Value = ZhiWei;
            parameters[8].Value = ZaiGang;
            parameters[9].Value = EmailStr;
            parameters[10].Value = IfLogin;
            parameters[11].Value = Sex;
            parameters[12].Value = BackInfo;
            parameters[13].Value = BirthDay;
            parameters[14].Value = MingZu;
            parameters[15].Value = SFZSerils;
            parameters[16].Value = HunYing;
            parameters[17].Value = ZhengZhiMianMao;
            parameters[18].Value = JiGuan;
            parameters[19].Value = HuKou;
            parameters[20].Value = XueLi;
            parameters[21].Value = ZhiCheng;
            parameters[22].Value = BiYeYuanXiao;
            parameters[23].Value = ZhuanYe;
            parameters[24].Value = CanJiaGongZuoTime;
            parameters[25].Value = JiaRuBenDanWeiTime;
            parameters[26].Value = GongLing;
            parameters[27].Value = JiaTingDianHua;
            parameters[28].Value = JiaTingAddress;
            parameters[29].Value = GangWeiBianDong;
            parameters[30].Value = JiaoYueBeiJing;
            parameters[31].Value = GongZuoJianLi;
            parameters[32].Value = SheHuiGuanXi;
            parameters[33].Value = JiangChengJiLu;
            parameters[34].Value = ZhiWuQingKuang;
            parameters[35].Value = PeiXunJiLu;
            parameters[36].Value = DanBaoJiLu;
            parameters[37].Value = NaoDongHeTong;
            parameters[38].Value = SheBaoJiaoNa;
            parameters[39].Value = TiJianJiLu;
            parameters[40].Value = BeiZhuStr;
            parameters[41].Value = FuJian;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPUser ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserName,UserPwd,TrueName,Serils,Department,JiaoSe,ActiveTime,ZhiWei,ZaiGang,EmailStr,IfLogin,Sex,BackInfo,BirthDay,MingZu,SFZSerils,HunYing,ZhengZhiMianMao,JiGuan,HuKou,XueLi,ZhiCheng,BiYeYuanXiao,ZhuanYe,CanJiaGongZuoTime,JiaRuBenDanWeiTime,GongLing,JiaTingDianHua,JiaTingAddress,GangWeiBianDong,JiaoYueBeiJing,GongZuoJianLi,SheHuiGuanXi,JiangChengJiLu,ZhiWuQingKuang,PeiXunJiLu,DanBaoJiLu,NaoDongHeTong,SheBaoJiaoNa,TiJianJiLu,BeiZhuStr,FuJian,PwdFailCount,PwdFailTime,BiYeZhuanYe,ZhiBie,BiYeTime,JiaRuDangPaiTime,ZhiChengLevel,FoShanJuRenCaiKu,GetZhiChengTIme,XueWei,PwdModifyTime  ");
            strSql.Append(" FROM ERPUser ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                TrueName = ds.Tables[0].Rows[0]["TrueName"].ToString();
                Serils = ds.Tables[0].Rows[0]["Serils"].ToString();
                Department = ds.Tables[0].Rows[0]["Department"].ToString();
                JiaoSe = ds.Tables[0].Rows[0]["JiaoSe"].ToString();
                if (ds.Tables[0].Rows[0]["ActiveTime"].ToString() != "")
                {
                    ActiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["ActiveTime"].ToString());
                }
                ZhiWei = ds.Tables[0].Rows[0]["ZhiWei"].ToString();
                ZaiGang = ds.Tables[0].Rows[0]["ZaiGang"].ToString();
                EmailStr = ds.Tables[0].Rows[0]["EmailStr"].ToString();
                IfLogin = ds.Tables[0].Rows[0]["IfLogin"].ToString();
                Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                BirthDay = ds.Tables[0].Rows[0]["BirthDay"].ToString();
                MingZu = ds.Tables[0].Rows[0]["MingZu"].ToString();
                SFZSerils = ds.Tables[0].Rows[0]["SFZSerils"].ToString();
                HunYing = ds.Tables[0].Rows[0]["HunYing"].ToString();
                ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                JiGuan = ds.Tables[0].Rows[0]["JiGuan"].ToString();
                HuKou = ds.Tables[0].Rows[0]["HuKou"].ToString();
                XueLi = ds.Tables[0].Rows[0]["XueLi"].ToString();
                ZhiCheng = ds.Tables[0].Rows[0]["ZhiCheng"].ToString();
                BiYeYuanXiao = ds.Tables[0].Rows[0]["BiYeYuanXiao"].ToString();
                ZhuanYe = ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                CanJiaGongZuoTime = ds.Tables[0].Rows[0]["CanJiaGongZuoTime"].ToString();
                JiaRuBenDanWeiTime = ds.Tables[0].Rows[0]["JiaRuBenDanWeiTime"].ToString();
                GongLing = Convert.ToInt32(ds.Tables[0].Rows[0]["GongLing"].ToString());
                JiaTingDianHua = ds.Tables[0].Rows[0]["JiaTingDianHua"].ToString();
                JiaTingAddress = ds.Tables[0].Rows[0]["JiaTingAddress"].ToString();
                GangWeiBianDong = ds.Tables[0].Rows[0]["GangWeiBianDong"].ToString();
                JiaoYueBeiJing = ds.Tables[0].Rows[0]["JiaoYueBeiJing"].ToString();
                GongZuoJianLi = ds.Tables[0].Rows[0]["GongZuoJianLi"].ToString();
                SheHuiGuanXi = ds.Tables[0].Rows[0]["SheHuiGuanXi"].ToString();
                JiangChengJiLu = ds.Tables[0].Rows[0]["JiangChengJiLu"].ToString();
                ZhiWuQingKuang = ds.Tables[0].Rows[0]["ZhiWuQingKuang"].ToString();
                PeiXunJiLu = ds.Tables[0].Rows[0]["PeiXunJiLu"].ToString();
                DanBaoJiLu = ds.Tables[0].Rows[0]["DanBaoJiLu"].ToString();
                NaoDongHeTong = ds.Tables[0].Rows[0]["NaoDongHeTong"].ToString();
                SheBaoJiaoNa = ds.Tables[0].Rows[0]["SheBaoJiaoNa"].ToString();
                TiJianJiLu = ds.Tables[0].Rows[0]["TiJianJiLu"].ToString();
                BeiZhuStr = ds.Tables[0].Rows[0]["BeiZhuStr"].ToString();
                FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                if (ds.Tables[0].Rows[0]["PwdFailCount"].ToString() != "")
                {
                    PwdFailCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PwdFailCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PwdFailTime"].ToString() != "")
                {
                    PwdFailTime = DateTime.Parse(ds.Tables[0].Rows[0]["PwdFailTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BiYeZhuanYe"] != null)
                {
                    this.BiYeZhuanYe = ds.Tables[0].Rows[0]["BiYeZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiBie"] != null)
                {
                    this.ZhiBie = ds.Tables[0].Rows[0]["ZhiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeTime"] != null && ds.Tables[0].Rows[0]["BiYeTime"].ToString() != "")
                {
                    this.BiYeTime = DateTime.Parse(ds.Tables[0].Rows[0]["BiYeTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiaRuDangPaiTime"] != null && ds.Tables[0].Rows[0]["JiaRuDangPaiTime"].ToString() != "")
                {
                    this.JiaRuDangPaiTime = DateTime.Parse(ds.Tables[0].Rows[0]["JiaRuDangPaiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiChengLevel"] != null)
                {
                    this.ZhiChengLevel = ds.Tables[0].Rows[0]["ZhiChengLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoShanJuRenCaiKu"] != null)
                {
                    this.FoShanJuRenCaiKu = ds.Tables[0].Rows[0]["FoShanJuRenCaiKu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GetZhiChengTIme"] != null && ds.Tables[0].Rows[0]["GetZhiChengTIme"].ToString() != "")
                {
                    this.GetZhiChengTIme = DateTime.Parse(ds.Tables[0].Rows[0]["GetZhiChengTIme"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XueWei"] != null)
                {
                    this.XueWei = ds.Tables[0].Rows[0]["XueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PwdModifyTime"] != null && ds.Tables[0].Rows[0]["PwdModifyTime"].ToString() != "")
                {
                    this.PwdModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["PwdModifyTime"].ToString());
                }

            }
        }

        /**/
        /// <summary>
        /// 分析用户请求是否正常
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        public string ProcessSqlStr(string Str)
        {
            string SqlStr = "'|exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare";
            string ReturnValue = Str;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue = "";
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = "";
            }
            if (Str.Length > 20)
            {
                ReturnValue = "";
            }
            return ReturnValue;
        }

        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="MyUserName"></param>
        /// <param name="MyUserPwd"></param>
        public string UserLogin(string MyUserName, string MyUserPwd)
        {
            string SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "' or Serils='" + ProcessSqlStr(MyUserName) + "'";
            DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
            if (MyDataRow == null)
            {
                return "您所输入的用户名不存在";
            }
            else
            {
                if (MyUserPwd == DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"))
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                    {
                        return "ok";
                    }
                }
            }
            return "false";
        }

        /// <summary>
        /// 登陆系统，需要短信验证
        /// </summary>
        /// <param name="MyUserName"></param>
        /// <param name="MyUserPwd"></param>
        public void UserLogin(string MyUserName, string MyUserPwd, string IFPop, string LoginType, string LoginToUrl, bool rem, string strcheckcode)
        {
            string strcode = ProcessSqlStr(strcheckcode);
            string SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            if (LoginType == "0")
            {
                SqlSTr = "select * from ERPUser where Serils='" + ProcessSqlStr(MyUserName) + "'";
            }
            else if (LoginType == "1")
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            }
            else
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "' or Serils='" + ProcessSqlStr(MyUserName) + "'";
            }

            DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
            if (MyDataRow == null)
            {
                //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('您所输入的用户名不存在！');</script>");
                System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('您所输入的用户名不存在！');</script>");
                return;
            }
            if (strcode == "")
            {
                System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('您所输入的短信验证码不合法！');</script>");
            }
            else
            {
                //查询当前用户最后获取的手机短信验证码
                string strsql = "SELECT TOP 1 [Code] FROM [ValidateCode] where UserName = '" + ProcessSqlStr(MyUserName) + "' order by Time desc";
                string phonecode = ZWL.DBUtility.DbHelperSQL.GetSHSL(strsql);
                if (phonecode == strcode)
                {
                    var pwd = DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd");
                    pwd = PublicMethod.GetMd5(ZWL.Common.DEncrypt.DESEncrypt.Decrypt(pwd));
                    if (MyUserPwd == pwd)
                    {
                        if (DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                        {
                            System.Web.HttpContext.Current.Session["UserID"] = DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                            System.Web.HttpContext.Current.Session["UserName"] = DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                            System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                            System.Web.HttpContext.Current.Session["JiaoSe"] = DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                            System.Web.HttpContext.Current.Session["Department"] = DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                            System.Web.HttpContext.Current.Session["TrueName"] = DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                            System.Web.HttpContext.Current.Session["ZhiWei"] = DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                            System.Web.HttpContext.Current.Session["QuanXian"] = ZWL.DBUtility.DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");
                            //写登陆日志
                            ERPRiZhi MyRiZhi = new ERPRiZhi();
                            MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                            MyRiZhi.DoSomething = "用户登陆系统";
                            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                            MyRiZhi.Add();
                            //写入Cookies
                            if (rem)
                            {
                                PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString());
                            }
                            else
                            {
                                PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString(), -14400);
                            }
                            PublicMethod.WriteCookie("AdminName", "DTcms", System.Web.HttpContext.Current.Session["UserName"].ToString());
                            PublicMethod.WriteCookie("AdminPwd", "DTcms", System.Web.HttpContext.Current.Session["Password"].ToString());
                            if (IFPop == "否")
                            {
                                //系统跳转
                                System.Web.HttpContext.Current.Response.Redirect(LoginToUrl);
                            }
                            else
                            {
                                System.Web.HttpContext.Current.Response.Write("<script language=javascript>window.open ('" + LoginToUrl + "','_blank', 'width='+screen.availWidth+',height='+screen.availHeight-20+', left=0,top=0,toolbar=no, menubar=no, scrollbars=no,location=no, status=no') ;window.opener='';window.close();</script>");
                            }
                        }
                        else
                        {
                            //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('该用户暂时不允许登陆系统，请联系管理员！');</script>");
                            System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                            page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('该用户暂时不允许登陆系统，请联系管理员！');</script>");
                        }
                    }
                    else
                    {
                        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('该用户名对应的密码错误！');</script>");
                    }
                }
                else
                {
                    //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('该用户名对应的密码错误！');</script>");
                    System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                    page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('输入的手机验证码错误！');</script>");
                }
            }
        }
        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="MyUserName"></param>
        /// <param name="MyUserPwd"></param>
        public void UserLogin(string MyUserName, string MyUserPwd, string IFPop, string LoginType, string LoginToUrl, bool rem)
        {
            string SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            if (LoginType == "0")
            {
                SqlSTr = "select * from ERPUser where Serils='" + ProcessSqlStr(MyUserName) + "'";
            }
            else if (LoginType == "1")
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            }
            else
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "' or Serils='" + ProcessSqlStr(MyUserName) + "'";
            }

            DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
            if (MyDataRow == null)
            {
                //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('您所输入的用户名不存在！');</script>");
                System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('您所输入的用户名不存在！');</script>");
            }
            else
            {
                if (checkactive)
                {
                    var diffcount = DbHelperSQL.GetSHSL("select count(*) from ERPUser where datediff(second,ActiveTime,getdate())<80 and UserName='" + ProcessSqlStr(MyUserName) + "'");
                    if (diffcount == "1")
                    {
                        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('此用户已登陆,不能重复登陆！');</script>");
                        return;
                    }
                }
                var pwd = DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd");
                pwd = PublicMethod.GetMd5(ZWL.Common.DEncrypt.DESEncrypt.Decrypt(pwd));
                ID = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "ID"));

                if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount") != "")
                {
                    PwdFailCount = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount"));
                }
                if (PwdFailCount >= 1)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime").ToString() != "")
                    {
                        PwdFailTime = Convert.ToDateTime(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime"));
                    }
                    //每过1小时累计数量清空
                    if (PwdFailTime.Value.AddHours(1) < DateTime.Now)
                    {
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                if (PwdFailCount >= 5)
                {
                    if (PwdFailTime.Value.AddMinutes(10) > DateTime.Now)
                    {
                        //在锁定的时间内
                        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript",
                            "<script language='javascript'>alert('由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！');</script>");
                        return;
                    }
                    else
                    {
                        //过了锁定的时间内
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                if (MyUserPwd == pwd)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                    {

                        ZWL.BLL.ERPipstr newrecord = new ZWL.BLL.ERPipstr();
                        newrecord.ipstr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        newrecord.username = MyUserName.ToString().Trim();
                        newrecord.Add();

                        System.Web.HttpContext.Current.Session["UserID"] = DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                        System.Web.HttpContext.Current.Session["UserName"] = DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                        System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                        System.Web.HttpContext.Current.Session["JiaoSe"] = DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                        System.Web.HttpContext.Current.Session["Department"] = DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                        System.Web.HttpContext.Current.Session["TrueName"] = DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                        System.Web.HttpContext.Current.Session["ZhiWei"] = DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                        System.Web.HttpContext.Current.Session["QuanXian"] = ZWL.DBUtility.DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");
                        //写登陆日志
                        ERPRiZhi MyRiZhi = new ERPRiZhi();
                        MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                        MyRiZhi.DoSomething = "用户登陆系统";
                        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        MyRiZhi.Add();
                        //写入Cookies
                        if (rem)
                        {
                            PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString());
                        }
                        else
                        {
                            PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString(), -14400);
                        }
                        PublicMethod.WriteCookie("AdminName", "DTcms", System.Web.HttpContext.Current.Session["UserName"].ToString());
                        PublicMethod.WriteCookie("AdminPwd", "DTcms", System.Web.HttpContext.Current.Session["Password"].ToString());
                        if (IFPop == "否")
                        {
                            //系统跳转
                            System.Web.HttpContext.Current.Response.Redirect(LoginToUrl);
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Response.Write("<script language=javascript>window.open ('" + LoginToUrl + "','_blank', 'width='+screen.availWidth+',height='+screen.availHeight-20+', left=0,top=0,toolbar=no, menubar=no, scrollbars=no,location=no, status=no') ;window.opener='';window.close();</script>");
                        }
                    }
                    else
                    {
                        //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('该用户暂时不允许登陆系统，请联系管理员！');</script>");
                        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('该用户暂时不允许登陆系统，请联系管理员！');</script>");
                    }
                }
                else
                {
                    PwdFailCount = PwdFailCount + 1;
                    PwdFailTime = DateTime.Now;
                    UpdatePwdFail();
                    //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('该用户名对应的密码错误！');</script>");
                    System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                    if (PwdFailCount < 5)
                    {
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript", "<script language='javascript'>alert('该用户名对应的密码错误,还剩" + (5 - PwdFailCount) + "次机会！');</script>");
                    }
                    else
                    {
                        //在锁定的时间内
                        page.ClientScript.RegisterStartupScript(page.GetType(), "clientScript",
                            "<script language='javascript'>alert('由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！');</script>");
                    }
                }
            }
        }
        public bool UserLogin(string MyUserName, string MyUserPwd, string LoginType, ref string msg)
        {
            var result = true;
            string SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            if (LoginType == "0")
            {
                SqlSTr = "select * from ERPUser where Serils='" + ProcessSqlStr(MyUserName) + "'";
            }
            else if (LoginType == "1")
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";
            }
            else
            {
                SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "' or Serils='" + ProcessSqlStr(MyUserName) + "'";
            }

            DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
            if (MyDataRow == null)
            {
                msg = "您所输入的用户名不存在！";
                return false;
            }
            else
            {
                if (checkactive)
                {
                    var diffcount = DbHelperSQL.GetSHSL("select count(*) from ERPUser where datediff(second,ActiveTime,getdate())<80 and UserName='" + ProcessSqlStr(MyUserName) + "'");
                    if (diffcount == "1")
                    {
                        msg = "此用户已登陆,不能重复登陆！";
                        return false;
                    }
                }
                var pwd = DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd");
                pwd = PublicMethod.GetMd5(ZWL.Common.DEncrypt.DESEncrypt.Decrypt(pwd));
                ID = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "ID"));

                if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount") != "")
                {
                    PwdFailCount = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount"));
                }
                if (PwdFailCount >= 1)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime").ToString() != "")
                    {
                        PwdFailTime = Convert.ToDateTime(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime"));
                    }
                    //每过1小时累计数量清空
                    if (PwdFailTime.Value.AddHours(1) < DateTime.Now)
                    {
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                if (PwdFailCount >= 5)
                {
                    if (PwdFailTime.Value.AddMinutes(10) > DateTime.Now)
                    {
                        //在锁定的时间内
                        msg = "由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！";
                        return false;
                    }
                    else
                    {
                        //过了锁定的时间内
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                if (MyUserPwd == pwd)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                    {

                        ZWL.BLL.ERPipstr newrecord = new ZWL.BLL.ERPipstr();
                        newrecord.ipstr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        newrecord.username = MyUserName.ToString().Trim();
                        newrecord.Add();

                        
                        //写登陆日志
                        ERPRiZhi MyRiZhi = new ERPRiZhi();
                        MyRiZhi.UserName = MyUserName;
                        MyRiZhi.DoSomething = "用户登陆系统";
                        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        MyRiZhi.Add();

                    }
                    else
                    {
                        msg = "该用户暂时不允许登陆系统，请联系管理员！";
                        return false;
                    }
                }
                else
                {
                    PwdFailCount = PwdFailCount + 1;
                    PwdFailTime = DateTime.Now;
                    UpdatePwdFail();
                    if (PwdFailCount < 5)
                    {
                        msg = "该用户名对应的密码错误,还剩" + (5 - PwdFailCount) + "次机会！";
                        return false;
                    }
                    else
                    {
                        //在锁定的时间内
                        msg = "由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！";
                        return false;
                    }
                }
            }
            return result;
        }
        public bool UserLogin(string token, ref string msg)
        {
            var info = new ZWL.BLL.Token();
            info.GetModel(token);
            if (info.ID <= 0)
            {
                return false;
            }
            var user = new ZWL.BLL.ERPUser();
            user = user.GetModel("UserName='" + info.UserName + "'");
            if (user == null)
            { return false; }
            return UserLogin(user.UserName, user.UserPwd, "1", ref msg);

            //return UserLogin();
        }
        /// <summary>
        /// 手机app登陆系统
        /// </summary>
        /// <param name="MyUserName"></param>
        /// <param name="MyUserPwd"></param>
        public string NewUserLogin(string MyUserName, string MyUserPwd)
        {
            string SqlSTr = "select * from ERPUser where UserName='" + ProcessSqlStr(MyUserName) + "'";

            DataRow MyDataRow = DbHelperSQL.GetDataRow(SqlSTr);
            if (MyDataRow == null)
            {
                return "您所输入的用户名不存在！";
            }
            else
            {
                var pwd = DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd");
                pwd = PublicMethod.GetMd5(ZWL.Common.DEncrypt.DESEncrypt.Decrypt(pwd));
                ID = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "ID"));

                #region 密码错误次数锁定
                if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount") != "")
                {
                    PwdFailCount = Convert.ToInt32(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailCount"));
                }
                if (PwdFailCount >= 1)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime").ToString() != "")
                    {
                        PwdFailTime = Convert.ToDateTime(DataValidate.ValidateDataRow_S(MyDataRow, "PwdFailTime"));
                    }
                    //每过1小时累计数量清空
                    if (PwdFailTime.Value.AddHours(1) < DateTime.Now)
                    {
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                if (PwdFailCount >= 5)
                {
                    if (PwdFailTime.Value.AddMinutes(10) > DateTime.Now)
                    {
                        //在锁定的时间内
                        return "由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！";
                    }
                    else
                    {
                        //过了锁定的时间内
                        PwdFailCount = 0;
                        UpdatePwdFail();
                    }
                }
                #endregion

                if (MyUserPwd == pwd)
                {
                    if (DataValidate.ValidateDataRow_S(MyDataRow, "IfLogin").Trim() == "是")
                    {

                        ZWL.BLL.ERPipstr newrecord = new ZWL.BLL.ERPipstr();
                        newrecord.ipstr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        newrecord.username = MyUserName.ToString().Trim();
                        newrecord.Add();

                        System.Web.HttpContext.Current.Session["UserID"] = DataValidate.ValidateDataRow_S(MyDataRow, "ID");
                        System.Web.HttpContext.Current.Session["UserName"] = DataValidate.ValidateDataRow_S(MyDataRow, "UserName");
                        System.Web.HttpContext.Current.Session["Password"] = PublicMethod.GetMd5(DataValidate.ValidateDataRow_S(MyDataRow, "UserPwd"));
                        System.Web.HttpContext.Current.Session["JiaoSe"] = DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe");
                        System.Web.HttpContext.Current.Session["Department"] = DataValidate.ValidateDataRow_S(MyDataRow, "Department");
                        System.Web.HttpContext.Current.Session["TrueName"] = DataValidate.ValidateDataRow_S(MyDataRow, "TrueName");
                        System.Web.HttpContext.Current.Session["ZhiWei"] = DataValidate.ValidateDataRow_S(MyDataRow, "ZhiWei");
                        System.Web.HttpContext.Current.Session["QuanXian"] = ZWL.DBUtility.DbHelperSQL.GetStringList("select QuanXian from ERPJiaoSe where JiaoSeName in(" + "'" + DataValidate.ValidateDataRow_S(MyDataRow, "JiaoSe").Replace(",", "','") + "'" + ")");
                        //写登陆日志
                        ERPRiZhi MyRiZhi = new ERPRiZhi();
                        MyRiZhi.UserName = PublicMethod.GetSessionValue("UserName");
                        MyRiZhi.DoSomething = "用户登陆系统";
                        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        MyRiZhi.Add();
                        //写入Cookies
                        PublicMethod.WriteCookie("DTRememberName", System.Web.HttpContext.Current.Session["UserName"].ToString(), -14400);
                        PublicMethod.WriteCookie("AdminName", "DTcms", System.Web.HttpContext.Current.Session["UserName"].ToString());
                        PublicMethod.WriteCookie("AdminPwd", "DTcms", System.Web.HttpContext.Current.Session["Password"].ToString());
                        return "ok";
                    }
                    else
                    {
                        return "该用户暂时不允许登陆系统，请联系管理员！";
                    }
                }
                else
                {
                    PwdFailCount = PwdFailCount + 1;
                    PwdFailTime = DateTime.Now;
                    UpdatePwdFail();
                    //System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('该用户名对应的密码错误！');</script>");
                    System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                    if (PwdFailCount < 5)
                    {
                        return "该用户名对应的密码错误,还剩" + (5 - PwdFailCount) + "次机会！";
                    }
                    else
                    {
                        //在锁定的时间内
                        return "由于密码错误过多,用户已被锁定,于" + PwdFailTime.Value.AddMinutes(10).ToString("HH:mm:ss") + "后解锁！";
                    }
                }
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],[IfLogin],[Sex],[BackInfo],[BirthDay],[MingZu],[SFZSerils],[HunYing],[ZhengZhiMianMao],[JiGuan],[HuKou],[XueLi],[ZhiCheng],[BiYeYuanXiao],[ZhuanYe],[CanJiaGongZuoTime],[JiaRuBenDanWeiTime],[GongLing],[JiaTingDianHua],[JiaTingAddress],[GangWeiBianDong],[JiaoYueBeiJing],[GongZuoJianLi],[SheHuiGuanXi],[JiangChengJiLu],[ZhiWuQingKuang],[PeiXunJiLu],[DanBaoJiLu],[NaoDongHeTong],[SheBaoJiaoNa],[TiJianJiLu],[BeiZhuStr],[FuJian],[DisplayID],[BiYeZhuanYe],[ZhiBie],[BiYeTime],[JiaRuDangPaiTime],[ZhiChengLevel],[FoShanJuRenCaiKu],[GetZhiChengTIme],[XueWei] ");
            strSql.Append(" FROM ERPUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public ZWL.BLL.ERPUser GetModel(string strWhere)
        {
            var list = GetListModel(strWhere);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public List<ZWL.BLL.ERPUser> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPUser>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPUser>(ds.Tables[0]);
            }
            return list;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdatePwdFail()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPUser set ");
            strSql.Append("PwdFailCount=@PwdFailCount,");
            strSql.Append("PwdFailTime=@PwdFailTime");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PwdFailCount", SqlDbType.Int,6),
                    new SqlParameter("@PwdFailTime", SqlDbType.DateTime)};
            parameters[0].Value = PwdFailCount;
            parameters[1].Value = PwdFailTime;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        #endregion  成员方法

    }
}