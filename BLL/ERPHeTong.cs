using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;//请先添加引用
using System.Collections.Generic;
using System.ComponentModel;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPHeTong。
    /// </summary>
    public class ERPHeTong
    {
        public ERPHeTong()
        { }
        #region Model
        private int _id;
        private int _formid;
        private string _workname;
        private string _htid;
        private string _htname;
        private string _htlb;
        private string _htlx;
        private string _zylb;
        private string _hylb;
        private string _jyfs;
        private string _jfdw;
        private string _jffzr;
        private string _yfdw;
        private string _yffzr;
        private string _bfdw;
        private string _bffzr;
        private string _hzdw1;
        private string _hzfzr1;
        private string _hzdw2;
        private string _hzfzr2;
        private decimal _htje;
        private string _jjfs;
        private DateTime _qdtime;
        private DateTime _kstime;
        private DateTime _jztime;
        private string _xmid;
        private string _xmname;
        private string _jfly;
        private int _qdfs;

        private string _jejh1;
        private DateTime _rqjh1;
        private string _bzjh1;
        private string _jejh2;
        private DateTime _rqjh2;
        private string _bzjh2;

        private string _jejh3;
        private DateTime _rqjh3;
        private string _bzjh3;
        private string _jejh4;
        private DateTime _rqjh4;
        private string _bzjh4;

        private string _jejh5;
        private DateTime _rqjh5;
        private string _bzjh5;

        private string _cjbm;
        private string _xmcjr;
        private string _jbr;
        private string _htjystate;//存储合同借阅状态
        private string _htzt;//合同状态（未执行、执行、完成、中止）
        private string _htgd;
        private DateTime _gdtime;
        private string _xmzt;//项目状态（在建、已建）
        private int _nworkid;
        private int? _parentid;

        //private DateTime? _jstime;
        //private string _kpfs;
        private string _htzynr;//合同主要内容
        private string _httype;//合同专业大类
        private string _zbtzs;
        private string _hgzs;
        private string _adress;
        private string _htxs;
        private string _remark;

        private string _jfdwxz;
        private string _ywlxr;
        private DateTime? _wgtime;
        private string _htgzl;
        private decimal? _htdj;
        private string _iswg;
        private string _lyqk;
        private string _sfyjf;
        
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 合同主要内容20150414增加字段用于导入2014年之前的数据
        /// </summary>
        public string HTZYNR
        {
            set { _htzynr = value; }
            get { return _htzynr; }
        }
        /// <su
        /// <summary>
        /// 合同借阅状态
        /// </summary>
        [Description("合同借阅状态")]
        public string HTJYState
        {
            set { _htjystate = value; }
            get { return _htjystate; }
        }
        /// <summary>
        /// Nworktodo中的表单id
        /// </summary>
        public int FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 工作名称
        /// </summary>
        [Description("工作名称")]
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }

        /// <summary>
        /// 合同编号
        /// </summary>
        [Description("合同编号")]
        public string HTID
        {
            set { _htid = value; }
            get { return _htid; }
        }
        /// <summary>
		/// 合同名称
		/// </summary>
        [Description("合同名称")]
        public string HTName
        {
            set { _htname = value; }
            get { return _htname; }
        }
        /// <summary>
		/// 合同类别
		/// </summary>
        [Description("合同类别")]
        public string HTLB
        {
            set { _htlb = value; }
            get { return _htlb; }
        }
        [Description("合同类型")]
        public string HTLX
        {
            set { _htlx = value; }
            get { return _htlx; }
        }
        /// <summary>
		/// 专业类别
		/// </summary>
        [Description("专业类别")]
        public string ZYLB
        {
            set { _zylb = value; }
            get { return _zylb; }
        }
        /// <summary>
        /// 行业类别
        /// </summary>
        [Description("行业类别")]
        public string HYLB
        {
            set { _hylb = value; }
            get { return _hylb; }
        }
        /// <summary>
		/// 经营方式
		/// </summary>
        [Description("经营方式")]
        public string JYFS
        {
            set { _jyfs = value; }
            get { return _jyfs; }
        }
        /// <summary>
		/// 甲方单位
		/// </summary>
        [Description("甲方单位")]
        public string JFDW
        {
            set { _jfdw = value; }
            get { return _jfdw; }
        }
        /// <summary>
		/// 甲方负责人
		/// </summary>
        [Description("甲方负责人")]
        public string JFFZR
        {
            set { _jffzr = value; }
            get { return _jffzr; }
        }
        /// <summary>
		/// 乙方单位
		/// </summary>
        [Description("乙方单位")]
        public string YFDW
        {
            set { _yfdw = value; }
            get { return _yfdw; }
        }
        /// <summary>
		/// 乙方负责人
		/// </summary>
        [Description("乙方负责人")]
        public string YFFZR
        {
            set { _yffzr = value; }
            get { return _yffzr; }
        }
        /// <summary>
        /// 丙方单位
        /// </summary>
        [Description("丙方单位")]
        public string BFDW
        {
            set { _bfdw = value; }
            get { return _bfdw; }
        }
        /// <summary>
        /// 丙方负责人
        /// </summary>
        [Description("丙方负责人")]
        public string BFFZR
        {
            set { _bffzr = value; }
            get { return _bffzr; }
        }
        /// <summary>
		/// 合作单位1
		/// </summary>
        [Description("合作单位1")]
        public string HZDW1
        {
            set { _hzdw1 = value; }
            get { return _hzdw1; }
        }
        /// <summary>
		/// 合作单位负责人1
		/// </summary>
        [Description("合作单位负责人1")]
        public string HZFZR1
        {
            set { _hzfzr1 = value; }
            get { return _hzfzr1; }
        }
        /// <summary>
		/// 合作单位2
		/// </summary>
        [Description("合作单位2")]
        public string HZDW2
        {
            set { _hzdw2 = value; }
            get { return _hzdw2; }
        }
        /// <summary>
		/// 合作单位负责人2
		/// </summary>
        [Description("合作单位负责人2")]
        public string HZFZR2
        {
            set { _hzfzr2 = value; }
            get { return _hzfzr2; }
        }
        /// <summary>
		/// 合同金额
		/// </summary>
        [Description("合同金额")]
        public decimal HTJE
        {
            set { _htje = value; }
            get { return _htje; }
        }
        /// <summary>
		/// 计价方式
		/// </summary>
        [Description("计价方式")]
        public string JJFS
        {
            set { _jjfs = value; }
            get { return _jjfs; }
        }
        /// <summary>
		/// 合同签订日期
		/// </summary>
        [Description("合同签订日期")]
        public DateTime QDTime
        {
            set { _qdtime = value; }
            get { return _qdtime; }
        }
        /// <summary>
		/// 合同开始时间
		/// </summary>
        [Description("合同开始时间")]
        public DateTime KSTime
        {
            set { _kstime = value; }
            get { return _kstime; }
        }
        /// <summary>
		/// 合同截止时间
		/// </summary>
        [Description("合同截止时间")]
        public DateTime JZTime
        {
            set { _jztime = value; }
            get { return _jztime; }
        }
        /// <summary>
		/// 项目编号
		/// </summary>
        [Description("项目编号")]
        public string XMID
        {
            set { _xmid = value; }
            get { return _xmid; }
        }
        /// <summary>
		/// 项目名称
		/// </summary>
        [Description("项目名称")]
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }

        }
        /// <summary>
		/// 经费来源
		/// </summary>
        [Description("经费来源")]
        public string JFLY
        {
            set { _jfly = value; }
            get { return _jfly; }
        }
        /// <summary>
		/// 签订份数
		/// </summary>
        [Description("签订份数")]
        public int QDFS
        {
            set { _qdfs = value; }
            get { return _qdfs; }
        }

        /// <summary>
        /// 金额计划1
        /// </summary>
        [Description("金额计划1")]
        public string JEJH1
        {
            set { _jejh1 = value; }
            get { return _jejh1; }
        }
        /// <summary>
        /// 收付款日期计划1
        /// </summary>
        [Description("收付款日期计划1")]
        public DateTime RQJH1
        {
            set { _rqjh1 = value; }
            get { return _rqjh1; }
        }
        /// <summary>
        /// 备注计划1
        /// </summary>
        [Description("备注计划1")]
        public string BZJH1
        {
            set { _bzjh1 = value; }
            get { return _bzjh1; }
        }
        /// <summary>
        /// 金额计划2
        /// </summary>
        [Description("金额计划2")]
        public string JEJH2
        {
            set { _jejh2 = value; }
            get { return _jejh2; }
        }
        /// <summary>
        /// 收付款日期计划2
        /// </summary>
        [Description("收付款日期计划2")]
        public DateTime RQJH2
        {
            set { _rqjh2 = value; }
            get { return _rqjh2; }
        }
        /// <summary>
        /// 备注计划2
        /// </summary>
        [Description("收付款日期计划2")]
        public string BZJH2
        {
            set { _bzjh2 = value; }
            get { return _bzjh2; }
        }
        /// <summary>
        /// 金额计划3
        /// </summary>
        [Description("金额计划3")]
        public string JEJH3
        {
            set { _jejh3 = value; }
            get { return _jejh3; }
        }
        /// <summary>
        /// 收付款日期计划3
        /// </summary>
        [Description("收付款日期计划3")]
        public DateTime RQJH3
        {
            set { _rqjh3 = value; }
            get { return _rqjh3; }
        }
        /// <summary>
        /// 备注计划3
        /// </summary>
        [Description("备注计划3")]
        public string BZJH3
        {
            set { _bzjh3 = value; }
            get { return _bzjh3; }
        }
        /// <summary>
        /// 金额计划4
        /// </summary>
        [Description("金额计划4")]
        public string JEJH4
        {
            set { _jejh4 = value; }
            get { return _jejh4; }
        }
        /// <summary>
        /// 收付款日期计划4
        /// </summary>
        [Description("收付款日期计划4")]
        public DateTime RQJH4
        {
            set { _rqjh4 = value; }
            get { return _rqjh4; }
        }
        /// <summary>
        /// 备注计划4
        /// </summary>
        [Description("备注计划4")]
        public string BZJH4
        {
            set { _bzjh4 = value; }
            get { return _bzjh4; }
        }
        /// <summary>
        /// 金额计划5
        /// </summary>
        [Description("金额计划5")]
        public string JEJH5
        {
            set { _jejh5 = value; }
            get { return _jejh5; }
        }
        /// <summary>
        /// 收付款日期计划5
        /// </summary>
        [Description("收付款日期计划5")]
        public DateTime RQJH5
        {
            set { _rqjh5 = value; }
            get { return _rqjh5; }
        }
        /// <summary>
        /// 备注计划5
        /// </summary>
        [Description("备注计划5")]
        public string BZJH5
        {
            set { _bzjh5 = value; }
            get { return _bzjh5; }
        }

        /// <summary>
		/// 承接部门
		/// </summary>
        [Description("承接部门")]
        public string CJBM
        {
            set { _cjbm = value; }
            get { return _cjbm; }
        }
        /// <summary>
		/// 项目承接人
		/// </summary>
        [Description("项目承接人")]
        public string XMCJR
        {
            set { _xmcjr = value; }
            get { return _xmcjr; }

        }
        /// <summary>
        /// 经办人
        /// </summary>
        [Description("经办人")]
        public string JBR
        {
            set { _jbr = value; }
            get { return _jbr; }

        }
        /// <summary>
		/// 合同状态
		/// </summary>
        [Description("合同状态")]
        public string HTZT
        {
            set { _htzt = value; }
            get { return _htzt; }
        }
        /// <summary>
		/// 合同归档
		/// </summary>
        [Description("合同归档")]
        public string HTGD
        {
            set { _htgd = value; }
            get { return _htgd; }

        }
        /// <summary>
		/// 归档日期
		/// </summary>
        [Description("归档日期")]
        public DateTime GDTime
        {
            set { _gdtime = value; }
            get { return _gdtime; }
        }
        /// <summary>
        /// 项目状态
        /// </summary>
        [Description("项目状态")]
        public string XMZT
        {
            set { _xmzt = value; }
            get { return _xmzt; }
        }
        /// <summary>
        /// 工作编号
        /// </summary>
        [Description("工作编号")]
        public int NWorkToDoID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
		/// 合同专业大类
		/// </summary>
        [Description("合同专业大类")]
        public string HTTYPE
        {
            set { _httype = value; }
            get { return _httype; }

        }
        /// <summary>
        /// 结算时间
        /// </summary>
        //public DateTime? JSTime
        //{
        //    set{ _jstime=value;}
        //    get{return _jstime;}
        //}
        ///// <summary>
        ///// 开票方式
        ///// </summary>
        //public string KPFS
        //{
        //    set{ _kpfs=value;}
        //    get{return _kpfs;}

        //}
        ///// <summary>
        ///// 开票金额
        ///// </summary>
        //public string KPJE
        //{
        //    set{ _kpje=value;}
        //    get{return _kpje;}
        //}

        /// <summary>
        /// 中标通知书
        /// </summary>
        [Description("中标通知书")]
        public string ZBTZS
        {
            set { _zbtzs = value; }
            get { return _zbtzs; }
        }
        /// <summary>
        /// 合格证书
        /// </summary>
        [Description("合格证书")]
        public string HGZS
        {
            set { _hgzs = value; }
            get { return _hgzs; }
        }
        /// <summary>
        /// 项目地址
        /// </summary>
        [Description("项目地址")]
        public string Adress
        {
            set { _adress = value; }
            get { return _adress; }
        }
        /// <summary>
        /// 合同形式
        /// </summary>
        [Description("合同形式")]
        public string HTXS
        {
            set { _htxs = value; }
            get { return _htxs; }
        }
        /// <summary>
        /// 其他说明
        /// </summary>
        [Description("其他说明")]
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 甲方单位性质
        /// </summary>
        public string JFDWXZ
        {
            set { _jfdwxz = value; }
            get { return _jfdwxz; }
        }
        /// <summary>
        /// 业务联系人
        /// </summary>
        public string YWLXR
        {
            set { _ywlxr = value; }
            get { return _ywlxr; }
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? WGTime
        {
            set { _wgtime = value; }
            get { return _wgtime; }
        }
        /// <summary>
        /// 合同工作量
        /// </summary>
        public string HTGZL
        {
            set { _htgzl = value; }
            get { return _htgzl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HTDJ
        {
            set { _htdj = value; }
            get { return _htdj; }
        }

        /// <summary>
        /// 是否完工
        /// </summary>
        public string ISWG
        {
            set { _iswg = value; }
            get { return _iswg; }
        }
        /// <summary>
        /// 履约情况
        /// </summary>
        public string LYQK
        {
            set { _lyqk = value; }
            get { return _lyqk; }
        }
        /// <summary>
        /// 是否有纠纷
        /// </summary>
        public string SFYJF
        {
            set { _sfyjf = value; }
            get { return _sfyjf; }
        }
        
        #endregion Model

        #region Relative Model
        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(JBR))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + JBR + "'");
                    if (tempUser != null)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }

        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo
        {
            get
            {
                var _currentToDo = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkToDoID > 0)
                {
                    _currentToDo.GetModel(NWorkToDoID);
                }
                return _currentToDo;
            }
        }
        #endregion
        #region  成员方法

        /// <summary>
        /// 根据合同的id更新数据库中对应的一条合同信息,更新时需要新建合同类，将更新的字段写入类的对应字段中
        /// </summary>
        /// <param name="strhetongid"></param>
        public void UpdateBD(string strhetongid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPHeTong set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("HTID=@HTID,");
            strSql.Append("HTName=@HTName,");
            strSql.Append("HTLB=@HTLB,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("JFDW=@JFDW,");
            strSql.Append("JFFZR=@JFFZR,");
            strSql.Append("YFDW=@YFDW,");
            strSql.Append("YFFZR=@YFFZR,");
            strSql.Append("BFDW=@BFDW,");
            strSql.Append("BFFZR=@BFFZR,");
            strSql.Append("HZDW1=@HZDW1,");
            strSql.Append("HZFZR1=@HZFZR1,");
            strSql.Append("HZDW2=@HZDW2,");
            strSql.Append("HZFZR2=@HZFZR2,");
            strSql.Append("HTJE=@HTJE,");
            strSql.Append("JJFS=@JJFS,");
            strSql.Append("QDTime=@QDTime,");
            strSql.Append("KSTime=@KSTime,");
            strSql.Append("JZTime=@JZTime,");
            strSql.Append("XMID=@XMID,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("JFLY=@JFLY,");
            strSql.Append("QDFS=@QDFS,");

            strSql.Append("JEJH1=@JEJH1,");
            strSql.Append("RQJH1=@RQJH1,");
            strSql.Append("BZJH1=@BZJH1,");
            strSql.Append("JEJH2=@JEJH2,");
            strSql.Append("RQJH2=@RQJH2,");
            strSql.Append("BZJH2=@BZJH2,");
            strSql.Append("JEJH3=@JEJH3,");
            strSql.Append("RQJH3=@RQJH3,");
            strSql.Append("BZJH3=@BZJH3,");
            strSql.Append("JEJH4=@JEJH4,");
            strSql.Append("RQJH4=@RQJH4,");
            strSql.Append("BZJH4=@BZJH4,");
            strSql.Append("JEJH5=@JEJH5,");
            strSql.Append("RQJH5=@RQJH5,");
            strSql.Append("BZJH5=@BZJH5,");

            strSql.Append("CJBM=@CJBM,");
            strSql.Append("XMCJR=@XMCJR,");
            strSql.Append("JBR=@JBR,");
            strSql.Append("HTJYState=@HTJYState,");
            strSql.Append("HTZT=@HTZT,");
            strSql.Append("HTGD=@HTGD,");
            strSql.Append("HTTYPE=@HTTYPE,");
            strSql.Append("ZBTZS=@ZBTZS,");
            strSql.Append("HGZS=@HGZS,");
            strSql.Append("Adress=@Adress,");
            strSql.Append("HTXS=@HTXS,");
            strSql.Append("GDTime=@GDTime,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("HTLX=@HTLX");
            strSql.Append(" where HTID=@HTID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FormID", SqlDbType.Int,6),
                    new SqlParameter("@WorkName", SqlDbType.VarChar,50),
                    new SqlParameter("@HTID", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@HTLB", SqlDbType.VarChar,50),

                    new SqlParameter("@ZYLB", SqlDbType.VarChar,200),
                    new SqlParameter("@HYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@JYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@JFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@YFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@YFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@BFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@BFFZR", SqlDbType.VarChar,50),

                    new SqlParameter("@HZDW1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZFZR1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZDW2", SqlDbType.VarChar,50),

                    new SqlParameter("@HZFZR2", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJE", SqlDbType.Decimal),
                    new SqlParameter("@JJFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDTime", SqlDbType.DateTime),
                    new SqlParameter("@KSTime", SqlDbType.DateTime),

                    new SqlParameter("@JZTime", SqlDbType.DateTime),
                    new SqlParameter("@XMID", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.VarChar,500),
                    new SqlParameter("@JFLY", SqlDbType.VarChar,50),
                    new SqlParameter("@QDFS", SqlDbType.Int),

                    new SqlParameter("@JEJH1", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH1", SqlDbType.DateTime),
                    new SqlParameter("@BZJH1", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH2", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH2", SqlDbType.DateTime),

                    new SqlParameter("@BZJH2", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH3", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH3", SqlDbType.DateTime),
                    new SqlParameter("@BZJH3", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH4", SqlDbType.VarChar,50),

                    new SqlParameter("@RQJH4", SqlDbType.DateTime),
                    new SqlParameter("@BZJH4", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH5", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH5", SqlDbType.DateTime),
                    new SqlParameter("@BZJH5", SqlDbType.VarChar,200),

                    new SqlParameter("@CJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@XMCJR", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJYState",SqlDbType.VarChar,50),
                    new SqlParameter("@HTZT",SqlDbType.VarChar,50),

                    new SqlParameter("@HTGD",SqlDbType.VarChar,50),
                    new SqlParameter("@HTTYPE",SqlDbType.VarChar,100),
                    new SqlParameter("@ZBTZS",SqlDbType.VarChar,10),
                    new SqlParameter("@HGZS",SqlDbType.VarChar,10),
                    new SqlParameter("@Adress",SqlDbType.NVarChar,500),
                    new SqlParameter("@HTXS",SqlDbType.NVarChar,50),
                    new SqlParameter("@GDTime",SqlDbType.DateTime),
                    new SqlParameter("@ParentId", SqlDbType.Int,6),
                    new SqlParameter("@HTLX", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = FormID;
            parameters[1].Value = WorkName;
            parameters[2].Value = strhetongid;
            parameters[3].Value = HTName;
            parameters[4].Value = HTLB;

            parameters[5].Value = ZYLB;
            parameters[6].Value = HYLB;
            parameters[7].Value = JYFS;
            parameters[8].Value = JFDW;
            parameters[9].Value = JFFZR;

            parameters[10].Value = YFDW;
            parameters[11].Value = YFFZR;
            parameters[12].Value = BFDW;
            parameters[13].Value = BFFZR;
            parameters[14].Value = HZDW1;
            parameters[15].Value = HZFZR1;
            parameters[16].Value = HZDW2;

            parameters[17].Value = HZFZR2;
            parameters[18].Value = HTJE;
            parameters[19].Value = JJFS;
            parameters[20].Value = QDTime;
            parameters[21].Value = KSTime;

            parameters[22].Value = JZTime;
            parameters[23].Value = XMID;
            parameters[24].Value = XMName;
            parameters[25].Value = JFLY;
            parameters[26].Value = QDFS;

            parameters[27].Value = JEJH1;
            parameters[28].Value = RQJH1;
            parameters[29].Value = BZJH1;
            parameters[30].Value = JEJH2;
            parameters[31].Value = RQJH2;

            parameters[32].Value = BZJH2;
            parameters[33].Value = JEJH3;
            parameters[34].Value = RQJH3;
            parameters[35].Value = BZJH3;
            parameters[36].Value = JEJH4;

            parameters[37].Value = RQJH4;
            parameters[38].Value = BZJH4;
            parameters[39].Value = JEJH5;
            parameters[40].Value = RQJH5;
            parameters[41].Value = BZJH5;

            parameters[42].Value = CJBM;
            parameters[43].Value = XMCJR;
            parameters[44].Value = JBR;
            parameters[45].Value = HTJYState;//合同借阅状态
            parameters[46].Value = HTZT;

            parameters[47].Value = HTGD;
            parameters[48].Value = HTTYPE;
            parameters[49].Value = ZBTZS;
            parameters[50].Value = HGZS;
            parameters[51].Value = Adress;
            parameters[52].Value = HTXS;
            parameters[53].Value = GDTime;
            parameters[54].Value = ParentId;
            parameters[55].Value = HTLX;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPHeTong] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("HTID=@HTID,");
            strSql.Append("HTName=@HTName,");
            strSql.Append("XMID=@XMID,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("HTLB=@HTLB,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("JFDW=@JFDW,");
            strSql.Append("JFFZR=@JFFZR,");
            strSql.Append("YFDW=@YFDW,");
            strSql.Append("YFFZR=@YFFZR,");
            strSql.Append("HZDW1=@HZDW1,");
            strSql.Append("HZFZR1=@HZFZR1,");
            strSql.Append("HZDW2=@HZDW2,");
            strSql.Append("HZFZR2=@HZFZR2,");
            strSql.Append("HTJE=@HTJE,");
            strSql.Append("JJFS=@JJFS,");
            strSql.Append("QDTime=@QDTime,");
            strSql.Append("KSTime=@KSTime,");
            strSql.Append("JZTime=@JZTime,");
            strSql.Append("JFLY=@JFLY,");
            strSql.Append("QDFS=@QDFS,");
            strSql.Append("JEJH1=@JEJH1,");
            strSql.Append("RQJH1=@RQJH1,");
            strSql.Append("BZJH1=@BZJH1,");
            strSql.Append("JEJH2=@JEJH2,");
            strSql.Append("RQJH2=@RQJH2,");
            strSql.Append("BZJH2=@BZJH2,");
            strSql.Append("JEJH3=@JEJH3,");
            strSql.Append("RQJH3=@RQJH3,");
            strSql.Append("BZJH3=@BZJH3,");
            strSql.Append("JEJH4=@JEJH4,");
            strSql.Append("RQJH4=@RQJH4,");
            strSql.Append("BZJH4=@BZJH4,");
            strSql.Append("JEJH5=@JEJH5,");
            strSql.Append("RQJH5=@RQJH5,");
            strSql.Append("BZJH5=@BZJH5,");
            strSql.Append("CJBM=@CJBM,");
            strSql.Append("HTZT=@HTZT,");
            strSql.Append("XMCJR=@XMCJR,");
            strSql.Append("JBR=@JBR,");
            strSql.Append("HTGD=@HTGD,");
            strSql.Append("GDTime=@GDTime,");
            strSql.Append("FormID=@FormID,");
            strSql.Append("HTJYState=@HTJYState,");
            strSql.Append("XMZT=@XMZT,");
            strSql.Append("HTZYNR=@HTZYNR,");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("BFDW=@BFDW,");
            strSql.Append("BFFZR=@BFFZR,");
            strSql.Append("HTTYPE=@HTTYPE,");
            strSql.Append("ZBTZS=@ZBTZS,");
            strSql.Append("HGZS=@HGZS,");
            strSql.Append("Adress=@Adress,");
            strSql.Append("HTXS=@HTXS,");
            strSql.Append("HTLX=@HTLX");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,50),
                    new SqlParameter("@HTID", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMID", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTLB", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,200),
                    new SqlParameter("@HYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@JYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@JFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@YFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@YFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@HZDW1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZFZR1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZDW2", SqlDbType.VarChar,50),
                    new SqlParameter("@HZFZR2", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JJFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDTime", SqlDbType.DateTime),
                    new SqlParameter("@KSTime", SqlDbType.DateTime),
                    new SqlParameter("@JZTime", SqlDbType.DateTime),
                    new SqlParameter("@JFLY", SqlDbType.VarChar,50),
                    new SqlParameter("@QDFS", SqlDbType.Int,4),
                    new SqlParameter("@JEJH1", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH1", SqlDbType.DateTime),
                    new SqlParameter("@BZJH1", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH2", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH2", SqlDbType.DateTime),
                    new SqlParameter("@BZJH2", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH3", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH3", SqlDbType.DateTime),
                    new SqlParameter("@BZJH3", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH4", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH4", SqlDbType.DateTime),
                    new SqlParameter("@BZJH4", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH5", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH5", SqlDbType.DateTime),
                    new SqlParameter("@BZJH5", SqlDbType.VarChar,200),
                    new SqlParameter("@CJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@HTZT", SqlDbType.VarChar,50),
                    new SqlParameter("@XMCJR", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.VarChar,50),
                    new SqlParameter("@HTGD", SqlDbType.VarChar,50),
                    new SqlParameter("@GDTime", SqlDbType.DateTime),
                    new SqlParameter("@FormID", SqlDbType.Int,4),
                    new SqlParameter("@HTJYState", SqlDbType.VarChar,50),
                    new SqlParameter("@XMZT", SqlDbType.VarChar,50),
                    new SqlParameter("@HTZYNR", SqlDbType.NVarChar,-1),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@BFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@BFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@HTTYPE", SqlDbType.VarChar,100),
                    new SqlParameter("@ZBTZS", SqlDbType.VarChar,10),
                    new SqlParameter("@HGZS", SqlDbType.VarChar,10),
                    new SqlParameter("@Adress", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTXS", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTLX", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = HTID;
            parameters[2].Value = HTName;
            parameters[3].Value = XMID;
            parameters[4].Value = XMName;
            parameters[5].Value = HTLB;
            parameters[6].Value = ZYLB;
            parameters[7].Value = HYLB;
            parameters[8].Value = JYFS;
            parameters[9].Value = JFDW;
            parameters[10].Value = JFFZR;
            parameters[11].Value = YFDW;
            parameters[12].Value = YFFZR;
            parameters[13].Value = HZDW1;
            parameters[14].Value = HZFZR1;
            parameters[15].Value = HZDW2;
            parameters[16].Value = HZFZR2;
            parameters[17].Value = HTJE;
            parameters[18].Value = JJFS;
            parameters[19].Value = QDTime;
            parameters[20].Value = KSTime;
            parameters[21].Value = JZTime;
            parameters[22].Value = JFLY;
            parameters[23].Value = QDFS;
            parameters[24].Value = JEJH1;
            parameters[25].Value = RQJH1;
            parameters[26].Value = BZJH1;
            parameters[27].Value = JEJH2;
            parameters[28].Value = RQJH2;
            parameters[29].Value = BZJH2;
            parameters[30].Value = JEJH3;
            parameters[31].Value = RQJH3;
            parameters[32].Value = BZJH3;
            parameters[33].Value = JEJH4;
            parameters[34].Value = RQJH4;
            parameters[35].Value = BZJH4;
            parameters[36].Value = JEJH5;
            parameters[37].Value = RQJH5;
            parameters[38].Value = BZJH5;
            parameters[39].Value = CJBM;
            parameters[40].Value = HTZT;
            parameters[41].Value = XMCJR;
            parameters[42].Value = JBR;
            parameters[43].Value = HTGD;
            parameters[44].Value = GDTime;
            parameters[45].Value = FormID;
            parameters[46].Value = HTJYState;
            parameters[47].Value = XMZT;
            parameters[48].Value = HTZYNR;
            parameters[49].Value = NWorkToDoID;
            parameters[50].Value = ParentId;
            parameters[51].Value = BFDW;
            parameters[52].Value = BFFZR;
            parameters[53].Value = HTTYPE;
            parameters[54].Value = ZBTZS;
            parameters[55].Value = HGZS;
            parameters[56].Value = Adress;
            parameters[57].Value = HTXS;
            parameters[58].Value = HTLX;
            parameters[59].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPHeTong(string htID)
        {
            DateTime defaultime = new DateTime();
            ZWL.Common.PublicMethod.GetDefaultTime(out defaultime);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHeTong ");
            strSql.Append(" where HTID=@HTID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTID", SqlDbType.VarChar,50)};
            parameters[0].Value = htID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPHeTong(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPHeTong] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTID"] != null)
                {
                    this.HTID = ds.Tables[0].Rows[0]["HTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTName"] != null)
                {
                    this.HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMID"] != null)
                {
                    this.XMID = ds.Tables[0].Rows[0]["XMID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTLB"] != null)
                {
                    this.HTLB = ds.Tables[0].Rows[0]["HTLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTLX"] != null)
                {
                    this.HTLX = ds.Tables[0].Rows[0]["HTLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYLB"] != null)
                {
                    this.HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JYFS"] != null)
                {
                    this.JYFS = ds.Tables[0].Rows[0]["JYFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFDW"] != null)
                {
                    this.JFDW = ds.Tables[0].Rows[0]["JFDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFFZR"] != null)
                {
                    this.JFFZR = ds.Tables[0].Rows[0]["JFFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YFDW"] != null)
                {
                    this.YFDW = ds.Tables[0].Rows[0]["YFDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YFFZR"] != null)
                {
                    this.YFFZR = ds.Tables[0].Rows[0]["YFFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDW1"] != null)
                {
                    this.HZDW1 = ds.Tables[0].Rows[0]["HZDW1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZFZR1"] != null)
                {
                    this.HZFZR1 = ds.Tables[0].Rows[0]["HZFZR1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDW2"] != null)
                {
                    this.HZDW2 = ds.Tables[0].Rows[0]["HZDW2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZFZR2"] != null)
                {
                    this.HZFZR2 = ds.Tables[0].Rows[0]["HZFZR2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJE"] != null && ds.Tables[0].Rows[0]["HTJE"].ToString() != "")
                {
                    this.HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JJFS"] != null)
                {
                    this.JJFS = ds.Tables[0].Rows[0]["JJFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QDTime"] != null && ds.Tables[0].Rows[0]["QDTime"].ToString() != "")
                {
                    this.QDTime = DateTime.Parse(ds.Tables[0].Rows[0]["QDTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KSTime"] != null && ds.Tables[0].Rows[0]["KSTime"].ToString() != "")
                {
                    this.KSTime = DateTime.Parse(ds.Tables[0].Rows[0]["KSTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JZTime"] != null && ds.Tables[0].Rows[0]["JZTime"].ToString() != "")
                {
                    this.JZTime = DateTime.Parse(ds.Tables[0].Rows[0]["JZTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JFLY"] != null)
                {
                    this.JFLY = ds.Tables[0].Rows[0]["JFLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QDFS"] != null && ds.Tables[0].Rows[0]["QDFS"].ToString() != "")
                {
                    this.QDFS = int.Parse(ds.Tables[0].Rows[0]["QDFS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JEJH1"] != null)
                {
                    this.JEJH1 = ds.Tables[0].Rows[0]["JEJH1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RQJH1"] != null && ds.Tables[0].Rows[0]["RQJH1"].ToString() != "")
                {
                    this.RQJH1 = DateTime.Parse(ds.Tables[0].Rows[0]["RQJH1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZJH1"] != null)
                {
                    this.BZJH1 = ds.Tables[0].Rows[0]["BZJH1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JEJH2"] != null)
                {
                    this.JEJH2 = ds.Tables[0].Rows[0]["JEJH2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RQJH2"] != null && ds.Tables[0].Rows[0]["RQJH2"].ToString() != "")
                {
                    this.RQJH2 = DateTime.Parse(ds.Tables[0].Rows[0]["RQJH2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZJH2"] != null)
                {
                    this.BZJH2 = ds.Tables[0].Rows[0]["BZJH2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JEJH3"] != null)
                {
                    this.JEJH3 = ds.Tables[0].Rows[0]["JEJH3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RQJH3"] != null && ds.Tables[0].Rows[0]["RQJH3"].ToString() != "")
                {
                    this.RQJH3 = DateTime.Parse(ds.Tables[0].Rows[0]["RQJH3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZJH3"] != null)
                {
                    this.BZJH3 = ds.Tables[0].Rows[0]["BZJH3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JEJH4"] != null)
                {
                    this.JEJH4 = ds.Tables[0].Rows[0]["JEJH4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RQJH4"] != null && ds.Tables[0].Rows[0]["RQJH4"].ToString() != "")
                {
                    this.RQJH4 = DateTime.Parse(ds.Tables[0].Rows[0]["RQJH4"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZJH4"] != null)
                {
                    this.BZJH4 = ds.Tables[0].Rows[0]["BZJH4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JEJH5"] != null)
                {
                    this.JEJH5 = ds.Tables[0].Rows[0]["JEJH5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RQJH5"] != null && ds.Tables[0].Rows[0]["RQJH5"].ToString() != "")
                {
                    this.RQJH5 = DateTime.Parse(ds.Tables[0].Rows[0]["RQJH5"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZJH5"] != null)
                {
                    this.BZJH5 = ds.Tables[0].Rows[0]["BZJH5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CJBM"] != null)
                {
                    this.CJBM = ds.Tables[0].Rows[0]["CJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTZT"] != null)
                {
                    this.HTZT = ds.Tables[0].Rows[0]["HTZT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMCJR"] != null)
                {
                    this.XMCJR = ds.Tables[0].Rows[0]["XMCJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTGD"] != null)
                {
                    this.HTGD = ds.Tables[0].Rows[0]["HTGD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GDTime"] != null && ds.Tables[0].Rows[0]["GDTime"].ToString() != "")
                {
                    this.GDTime = DateTime.Parse(ds.Tables[0].Rows[0]["GDTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FormID"] != null && ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    this.FormID = int.Parse(ds.Tables[0].Rows[0]["FormID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTJYState"] != null)
                {
                    this.HTJYState = ds.Tables[0].Rows[0]["HTJYState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMZT"] != null)
                {
                    this.XMZT = ds.Tables[0].Rows[0]["XMZT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTZYNR"] != null)
                {
                    this.HTZYNR = ds.Tables[0].Rows[0]["HTZYNR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BFDW"] != null)
                {
                    this.BFDW = ds.Tables[0].Rows[0]["BFDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BFFZR"] != null)
                {
                    this.BFFZR = ds.Tables[0].Rows[0]["BFFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTTYPE"] != null)
                {
                    this.HTTYPE = ds.Tables[0].Rows[0]["HTTYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZBTZS"] != null)
                {
                    this.ZBTZS = ds.Tables[0].Rows[0]["ZBTZS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HGZS"] != null)
                {
                    this.HGZS = ds.Tables[0].Rows[0]["HGZS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Adress"] != null)
                {
                    this.Adress = ds.Tables[0].Rows[0]["Adress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTXS"] != null)
                {
                    this.HTXS = ds.Tables[0].Rows[0]["HTXS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (CurrentWorkToDo.ID > 0)
                {
                    var parser = new ZWL.Common.ParseHtml();
                    parser.GetAttListFormHTMLtextarea(CurrentWorkToDo.FormContent);
                    this.Remark = parser.getValue("其他说明");
                }
            }
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPHeTong");
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHeTong");
            strSql.Append(" where FormID=@FormID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@FormID", SqlDbType.Int,6)};
            parameters[0].Value = FormID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该合同编号的记录
        /// </summary>
        public bool Exists(string htID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHeTong");
            strSql.Append(" where HTID=@HTID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@HTID", SqlDbType.VarChar,50)};
            parameters[0].Value = htID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据,增加合同主要内容字段
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPHeTong] (");
            strSql.Append("WorkName,HTID,HTName,XMID,XMName,HTLB,ZYLB,HYLB,JYFS,JFDW,JFFZR,YFDW,YFFZR,HZDW1,HZFZR1,HZDW2,HZFZR2,HTJE,JJFS,QDTime,KSTime,JZTime,JFLY,QDFS,JEJH1,RQJH1,BZJH1,JEJH2,RQJH2,BZJH2,JEJH3,RQJH3,BZJH3,JEJH4,RQJH4,BZJH4,JEJH5,RQJH5,BZJH5,CJBM,HTZT,XMCJR,JBR,HTGD,GDTime,FormID,HTJYState,XMZT,HTZYNR,NWorkToDoID,ParentId,BFDW,BFFZR,HTTYPE,ZBTZS,HGZS,Adress,HTXS,HTLX)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@HTID,@HTName,@XMID,@XMName,@HTLB,@ZYLB,@HYLB,@JYFS,@JFDW,@JFFZR,@YFDW,@YFFZR,@HZDW1,@HZFZR1,@HZDW2,@HZFZR2,@HTJE,@JJFS,@QDTime,@KSTime,@JZTime,@JFLY,@QDFS,@JEJH1,@RQJH1,@BZJH1,@JEJH2,@RQJH2,@BZJH2,@JEJH3,@RQJH3,@BZJH3,@JEJH4,@RQJH4,@BZJH4,@JEJH5,@RQJH5,@BZJH5,@CJBM,@HTZT,@XMCJR,@JBR,@HTGD,@GDTime,@FormID,@HTJYState,@XMZT,@HTZYNR,@NWorkToDoID,@ParentId,@BFDW,@BFFZR,@HTTYPE,@ZBTZS,@HGZS,@Adress,@HTXS,@HTLX)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,50),
                    new SqlParameter("@HTID", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMID", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTLB", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,200),
                    new SqlParameter("@HYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@JYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@JFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@YFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@YFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@HZDW1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZFZR1", SqlDbType.VarChar,50),
                    new SqlParameter("@HZDW2", SqlDbType.VarChar,50),
                    new SqlParameter("@HZFZR2", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JJFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDTime", SqlDbType.DateTime),
                    new SqlParameter("@KSTime", SqlDbType.DateTime),
                    new SqlParameter("@JZTime", SqlDbType.DateTime),
                    new SqlParameter("@JFLY", SqlDbType.VarChar,50),
                    new SqlParameter("@QDFS", SqlDbType.Int,4),
                    new SqlParameter("@JEJH1", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH1", SqlDbType.DateTime),
                    new SqlParameter("@BZJH1", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH2", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH2", SqlDbType.DateTime),
                    new SqlParameter("@BZJH2", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH3", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH3", SqlDbType.DateTime),
                    new SqlParameter("@BZJH3", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH4", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH4", SqlDbType.DateTime),
                    new SqlParameter("@BZJH4", SqlDbType.VarChar,200),
                    new SqlParameter("@JEJH5", SqlDbType.VarChar,50),
                    new SqlParameter("@RQJH5", SqlDbType.DateTime),
                    new SqlParameter("@BZJH5", SqlDbType.VarChar,200),
                    new SqlParameter("@CJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@HTZT", SqlDbType.VarChar,50),
                    new SqlParameter("@XMCJR", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.VarChar,50),
                    new SqlParameter("@HTGD", SqlDbType.VarChar,50),
                    new SqlParameter("@GDTime", SqlDbType.DateTime),
                    new SqlParameter("@FormID", SqlDbType.Int,4),
                    new SqlParameter("@HTJYState", SqlDbType.VarChar,50),
                    new SqlParameter("@XMZT", SqlDbType.VarChar,50),
                    new SqlParameter("@HTZYNR", SqlDbType.NVarChar,-1),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@BFDW", SqlDbType.VarChar,200),
                    new SqlParameter("@BFFZR", SqlDbType.VarChar,50),
                    new SqlParameter("@HTTYPE", SqlDbType.VarChar,100),
                    new SqlParameter("@ZBTZS", SqlDbType.VarChar,10),
                    new SqlParameter("@HGZS", SqlDbType.VarChar,10),
                    new SqlParameter("@Adress", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTXS", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTLX", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = WorkName;
            parameters[1].Value = HTID;
            parameters[2].Value = HTName;
            parameters[3].Value = XMID;
            parameters[4].Value = XMName;
            parameters[5].Value = HTLB;
            parameters[6].Value = ZYLB;
            parameters[7].Value = HYLB;
            parameters[8].Value = JYFS;
            parameters[9].Value = JFDW;
            parameters[10].Value = JFFZR;
            parameters[11].Value = YFDW;
            parameters[12].Value = YFFZR;
            parameters[13].Value = HZDW1;
            parameters[14].Value = HZFZR1;
            parameters[15].Value = HZDW2;
            parameters[16].Value = HZFZR2;
            parameters[17].Value = HTJE;
            parameters[18].Value = JJFS;
            parameters[19].Value = QDTime;
            parameters[20].Value = KSTime;
            parameters[21].Value = JZTime;
            parameters[22].Value = JFLY;
            parameters[23].Value = QDFS;
            parameters[24].Value = JEJH1;
            parameters[25].Value = RQJH1;
            parameters[26].Value = BZJH1;
            parameters[27].Value = JEJH2;
            parameters[28].Value = RQJH2;
            parameters[29].Value = BZJH2;
            parameters[30].Value = JEJH3;
            parameters[31].Value = RQJH3;
            parameters[32].Value = BZJH3;
            parameters[33].Value = JEJH4;
            parameters[34].Value = RQJH4;
            parameters[35].Value = BZJH4;
            parameters[36].Value = JEJH5;
            parameters[37].Value = RQJH5;
            parameters[38].Value = BZJH5;
            parameters[39].Value = CJBM;
            parameters[40].Value = HTZT;
            parameters[41].Value = XMCJR;
            parameters[42].Value = JBR;
            parameters[43].Value = HTGD;
            parameters[44].Value = GDTime;
            parameters[45].Value = FormID;
            parameters[46].Value = HTJYState;
            parameters[47].Value = XMZT;
            parameters[48].Value = HTZYNR;
            parameters[49].Value = NWorkToDoID;
            parameters[50].Value = ParentId;
            parameters[51].Value = BFDW;
            parameters[52].Value = BFFZR;
            parameters[53].Value = HTTYPE;
            parameters[54].Value = ZBTZS;
            parameters[55].Value = HGZS;
            parameters[56].Value = Adress;
            parameters[57].Value = HTXS;
            parameters[58].Value = HTLX;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPHeTong ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
            strSql.Append(" FROM ERPHeTong ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public void GetModelByWorkId(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
            strSql.Append(" FROM ERPHeTong ");
            strSql.Append(" where NWorkToDoID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetstrModel(string strhtbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPHeTong ");
            strSql.Append(" where HTID=@HTID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTID", SqlDbType.NVarChar,50)};
            parameters[0].Value = strhtbh;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            SetPropertyValue(ds);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHeTong ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public ZWL.BLL.ERPHeTong GetModelByWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPHeTong>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        public ZWL.BLL.ERPHeTong GetModelByNo(string no)
        {
            return GetModelByWhere("HTID='" + no + "'");
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZWL.BLL.ERPHeTong GetModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return PublicMethod.ConvertToModel<ZWL.BLL.ERPHeTong>(ds.Tables[0]);
            }

            return null;
        }
        public ZWL.BLL.ERPHeTongDaoZhang GetReceiptState(string htbh)
        {
            var result = new ZWL.BLL.ERPHeTongDaoZhang();
            GetstrModel(htbh);
            decimal dzje = 0;
            result.HTJE = HTJE;
            result.KaiPiaoJE = HTJE;
            var list = result.GetModelListBySqlWhere("HTBH='" + htbh + "'");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.CurrentWorkToDo.StateNow == "正常结束")
                        dzje += item.DaoZhangJE;
                }
            }
            var yModel = new ZWL.BLL.ERPHeTongYuShouKuan();
            var sqlWhere = string.Format("Flag<>0 and NWorkID={0}", NWorkToDoID);
            var ylist = yModel.GetModelListBySqlWhere(sqlWhere);
            foreach (var item in ylist)
            {
                if (!item.ConnectID.HasValue)
                    dzje += item.Amount;
            }
            result.DaoZhangJE = dzje;
            result.KaiPiaoJE = HTJE - dzje;
            return result;
        }
        public ZWL.BLL.ERPHeTongDaoZhang GetReceiptState_Temp(string htbh)
        {
            var result = new ZWL.BLL.ERPHeTongDaoZhang();
            GetstrModel(htbh);
            result.HTJE = HTJE;
            result.KaiPiaoJE = HTJE;
            decimal dzje = 0;
            var list = result.GetModelListBySqlWhere("HTBH='" + htbh + "'");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.CurrentWorkToDo.StateNow != "不通过")
                        dzje += item.DaoZhangJE;
                }
            }
            var dModel = new ZWL.BLL.ERPHeTongYuShouKuan();
            var list1 = dModel.GetModelListBySqlWhere("Flag=1 and HTBH='" + htbh + "'");
            if (list1 != null && list1.Count > 0)
            {
                foreach (var item in list1)
                {
                    if (!item.ConnectDaoID.HasValue)
                        dzje += item.Amount;
                }
            }
            result.DaoZhangJE = dzje;
            result.KaiPiaoJE = HTJE - dzje;
            return result;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPHeTong> GetListModel(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPHeTong>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPHeTong>(item));
                }
            }

            return result;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPHeTong");
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.合同编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("43") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 合同签订日期 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得分页后的数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int currPage, int pageSize)
        {
            var strSql = "";
            var method = new PublicMethod();
            var strmapping = method.getSQLTable("ERPHeTong");
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where 合同编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("43") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere;
            }
            return new Pager(strSql, currPage, pageSize);
        }
        #endregion  成员方法
    }
}

