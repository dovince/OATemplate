using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;

namespace ZWL.BLL
{
    public class ERPXMJBXX
    {
        public ERPXMJBXX()
        { }
        #region Model
        private int _id;
        private string _workname;
        private string _xmid;
        private string _xmqqid;
        private string _htid;
        private string _xmname;
        private string _xmstate;
        private string _xmadress;
        private string _weituodanweiname;
        private string _weituodanweilianxiren;
        private string _weituodanweilianxidianhua;
        private string _hezuodanweiname;
        private string _hezuodanweilianxiren;
        private string _hezuodanweilianxidianhua;
        private string _weituofangshi;
        private string _zhuanyeleibie;
        private string _hangyeleibie;
        private decimal _xmjingfei;
        private string _xiangmuzijinlaiyuan;
        private DateTime _yujixiangmubegintime;
        private DateTime _yujixiangmuendtime;
        private string _xmbumen;
        private string _xmfzr;
        private DateTime? _shtime;
        private string _lng;
        private string _lat;
        private string _autosysprojectid;

        private int? _zuankongshu; //钻孔数
        private float _zuankongjinchi;//钻孔进尺
        private string _pinggudengji;//评估等级
        private float _pinggumianji;//评估面积
        private float _diaochamianji;//调查面积
        private float _tancao;//探槽
        private float _kengtan;//坑探
        private float _zuantan;//钻探
        private string _kanchadengji;//勘察等级
        private string _bianxingdengji;//变形测量等级

        private string _zylbmain;//专业类别大类
        private DateTime _dengjitime;
        private string _shstate;//审核状态：已审核，未审核
        private string _beiyong1;
        private string _xmreport;//报告名称
        private int? _nworkid;

        private string _hetongstate;//合同状态：谈判未签订，签订流程中，已签订
        private string _xmywlxr;

        private string _jfdwxz;
        private DateTime? _wgtime;
        private string _iswg;
        private string _lyqk;
        private string _sfyjf;

        private decimal? _qpje;
        private decimal? _yqqpje;
        private string _sfyjwhz;
        private string _cscs;

        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HTBH
        {
            set { _htid = value; }
            get { return _htid; }
        }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string HTState
        {
            set { _hetongstate = value; }
            get { return _hetongstate; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string XMBH
        {
            set { _xmid = value; }
            get { return _xmid; }
        }
        /// <summary>
        /// 项目前期信息编号
        /// </summary>
        public string XMQQBH
        {
            set { _xmqqid = value; }
            get { return _xmqqid; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string XMState
        {
            set { _xmstate = value; }
            get { return _xmstate; }
        }
        /// <summary>
        /// 项目地址
        /// </summary>
        public string XMAdress
        {
            set { _xmadress = value; }
            get { return _xmadress; }
        }

        /// <summary>
        /// 委托单位名称
        /// </summary>
        public string WTDWName
        {
            set { _weituodanweiname = value; }
            get { return _weituodanweiname; }
        }
        /// <summary>
        /// 委托单位联系人
        /// </summary>
        public string WTDWLXR
        {
            set { _weituodanweilianxiren = value; }
            get { return _weituodanweilianxiren; }
        }
        /// <summary>
        /// 委托单位联系电话
        /// </summary>
        public string WTDWLXDH
        {
            set { _weituodanweilianxidianhua = value; }
            get { return _weituodanweilianxidianhua; }
        }
        /// <summary>
        /// 合作单位名称
        /// </summary>
        public string HZDWName
        {
            set { _hezuodanweiname = value; }
            get { return _hezuodanweiname; }
        }
        /// <summary>
        /// 合作单位联系人
        /// </summary>
        public string HZDWLXR
        {
            set { _hezuodanweilianxiren = value; }
            get { return _hezuodanweilianxiren; }
        }
        /// <summary>
        /// 合作单位联系电话
        /// </summary>
        public string HZDWLXDH
        {
            set { _hezuodanweilianxidianhua = value; }
            get { return _hezuodanweilianxidianhua; }
        }
        /// <summary>
        /// 委托方式
        /// </summary>
        public string WTFS
        {
            set { _weituofangshi = value; }
            get { return _weituofangshi; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string ZYLB
        {
            set { _zhuanyeleibie = value; }
            get { return _zhuanyeleibie; }
        }
        /// <summary>
        /// 行业类别
        /// </summary>
        public string HYLB
        {
            set { _hangyeleibie = value; }
            get { return _hangyeleibie; }
        }
        /// <summary>
        /// 项目资金来源
        /// </summary>
        public string XMZJLY
        {
            set { _xiangmuzijinlaiyuan = value; }
            get { return _xiangmuzijinlaiyuan; }
        }
        /// <summary>
        /// 项目经费
        /// </summary>
        public decimal XMJF
        {
            set { _xmjingfei = value; }
            get { return _xmjingfei; }
        }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime XMBeginTime
        {
            set { _yujixiangmubegintime = value; }
            get { return _yujixiangmubegintime; }
        }
        /// <summary>
        /// 项目结束时间
        /// </summary>
        public DateTime XMEndTime
        {
            set { _yujixiangmuendtime = value; }
            get { return _yujixiangmuendtime; }
        }

        /// <summary>
        /// 项目实施部门
        /// </summary>
        public string XMBM
        {
            set { _xmbumen = value; }
            get { return _xmbumen; }
        }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string XMFZR
        {
            set { _xmfzr = value; }
            get { return _xmfzr; }
        }

        /// <summary>
        /// 钻孔数
        /// </summary>
        public int? ZKS
        {
            set { _zuankongshu = value; }
            get { return _zuankongshu; }
        }
        /// <summary>
        /// 钻孔进尺
        /// </summary>
        public float ZKJC
        {
            set { _zuankongjinchi = value; }
            get { return _zuankongjinchi; }
        }
        /// <summary>
        /// 评估等级
        /// </summary>
        public string PGDJ
        {
            set { _pinggudengji = value; }
            get { return _pinggudengji; }
        }
        /// <summary>
        /// 勘察等级
        /// </summary>
        public string KCDJ
        {
            set { _kanchadengji = value; }
            get { return _kanchadengji; }
        }

        /// <summary>
        /// 变形测量等级
        /// </summary>
        public string BXCLDJ
        {
            set { _bianxingdengji = value; }
            get { return _bianxingdengji; }
        }
        /// <summary>
        /// 评估面积
        /// </summary>
        public float PGMJ
        {
            set { _pinggumianji = value; }
            get { return _pinggumianji; }
        }
        /// <summary>
        /// 调查面积
        /// </summary>
        public float DCMJ
        {
            set { _diaochamianji = value; }
            get { return _diaochamianji; }
        }
        /// <summary>
        /// 探槽
        /// </summary>
        public float TC
        {
            set { _tancao = value; }
            get { return _tancao; }
        }
        /// <summary>
        /// 坑探
        /// </summary>
        public float KT
        {
            set { _kengtan = value; }
            get { return _kengtan; }
        }
        /// <summary>
        /// 钻探
        /// </summary>
        public float ZT
        {
            set { _zuantan = value; }
            get { return _zuantan; }
        }
        /// <summary>
        /// 专业类别大类
        /// </summary>
        public string ZYLBMain
        {
            set { _zylbmain = value; }
            get { return _zylbmain; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime DJTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string SHState
        {
            set { _shstate = value; }
            get { return _shstate; }
        }
        /// <summary>
        /// beiyong1
        /// </summary>
        public string BeiYong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 报告名称
        /// </summary>
        public string XMReport
        {
            set { _xmreport = value; }
            get { return _xmreport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SHTime
        {
            set { _shtime = value; }
            get { return _shtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        public string AUTOSYSPROJECTID
        {
            set { _autosysprojectid = value; }
            get { return _autosysprojectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }

        public string XMYWLXR
        {
            set { _xmywlxr = value; }
            get { return _xmywlxr; }
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
        /// 完工时间
        /// </summary>
        public DateTime? WGTime
        {
            set { _wgtime = value; }
            get { return _wgtime; }
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

        /// <summary>
        /// 期票金额
        /// </summary>
        public decimal? QPJE
        {
            set { _qpje = value; }
            get { return _qpje; }
        }
        /// <summary>
        /// 其中：逾期期票金额
        /// </summary>
        public decimal? YQQPJE
        {
            set { _yqqpje = value; }
            get { return _yqqpje; }
        }
        /// <summary>
        /// 是否预计为坏账
        /// </summary>
        public string SFYJWHZ
        {
            set { _sfyjwhz = value; }
            get { return _sfyjwhz; }
        }
        /// <summary>
        /// 催收措施
        /// </summary>
        public string CSCS
        {
            set { _cscs = value; }
            get { return _cscs; }
        }
        #endregion Model
        #region  成员方法
        /// <summary>
        /// 根据项目的id更新数据库中对应的一条项目信息,更新时需要新建项目类，将更新的字段写入类的对应字段中
        /// </summary>
        /// <param name="strhetongid"></param>
        public void UpdateBD(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update ERPXMJBXX set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMQQBH=@XMQQBH,");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("XMState=@XMState,");
            strSql.Append("XMAdress=@XMAdress,");
            strSql.Append("WTDWName=@WTDWName,");
            strSql.Append("WTDWLXR=@WTDWLXR,");
            strSql.Append("WTDWLXDH=@WTDWLXDH,");
            strSql.Append("HZDWName=@HZDWName,");
            strSql.Append("HZDWLXR=@HZDWLXR,");
            strSql.Append("HZDWLXDH=@HZDWLXDH,");
            strSql.Append("WTFS=@WTFS,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("XMZJLY=@XMZJLY,");
            strSql.Append("XMJF=@XMJF,");
            strSql.Append("XMBeginTime=@XMBeginTime,");
            strSql.Append("XMEndTime=@XMEndTime,");
            strSql.Append("XMBM=@XMBM,");
            strSql.Append("XMFZR=@XMFZR,");
            strSql.Append("ZYLBMain=@ZYLBMain,");
            strSql.Append("XMReport=@XMReport,");
            strSql.Append("BeiYong1=@BeiYong1,");
            strSql.Append("Lng=@Lng,");
            strSql.Append("Lat=@Lat,");
            strSql.Append("AUTOSYSPROJECTID=@AUTOSYSPROJECTID,");
            strSql.Append("HTState=@HTState");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName",SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMQQBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,500),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMState", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWName",SqlDbType.NVarChar,50),
                    new SqlParameter("@HZDWLXR",SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS",SqlDbType.NVarChar,20),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMJF",SqlDbType.Decimal),
                    new SqlParameter("@XMBeginTime",SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime",SqlDbType.DateTime),
                    new SqlParameter("@XMBM",SqlDbType.NVarChar,200),
                    new SqlParameter("@XMFZR",SqlDbType.NVarChar,200),
                    new SqlParameter("@ZYLBMain",SqlDbType.NVarChar,20),
                    new SqlParameter("@XMReport",SqlDbType.NVarChar,500),
                    new SqlParameter("@BeiYong1",SqlDbType.NVarChar,500),
                    new SqlParameter("@Lng",SqlDbType.NVarChar,50),
                    new SqlParameter("@Lat",SqlDbType.NVarChar,50),
                    new SqlParameter("@AUTOSYSPROJECTID", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTState",SqlDbType.NVarChar,20)};
            parameters[0].Value = WorkName;
            parameters[1].Value = strxmbh;
            parameters[2].Value = XMQQBH;
            parameters[3].Value = HTBH;
            parameters[4].Value = XMName;
            parameters[5].Value = XMState;
            parameters[6].Value = XMAdress;
            parameters[7].Value = WTDWName;
            parameters[8].Value = WTDWLXR;
            parameters[9].Value = WTDWLXDH;
            parameters[10].Value = HZDWName;
            parameters[11].Value = HZDWLXR;
            parameters[12].Value = HZDWLXDH;
            parameters[13].Value = WTFS;
            parameters[14].Value = ZYLB;
            parameters[15].Value = HYLB;
            parameters[16].Value = XMZJLY;
            parameters[17].Value = XMJF;
            parameters[18].Value = XMBeginTime;
            parameters[19].Value = XMEndTime;
            parameters[20].Value = XMBM;
            parameters[21].Value = XMFZR;
            parameters[22].Value = ZYLBMain;
            parameters[23].Value = XMReport;
            parameters[24].Value = BeiYong1;
            parameters[25].Value = Lng;
            parameters[26].Value = Lat;
            parameters[27].Value = AUTOSYSPROJECTID;
            parameters[28].Value = HTState;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据项目的id更新数据库中对应的一条项目信息,更新时需要新建项目类，将更新的字段写入类的对应字段中
        /// </summary>
        /// <param name="strhetongid"></param>
        public void UpdateHTBH(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update ERPXMJBXX set ");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("XMBH=@XMBH");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.VarChar,500),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
            parameters[0].Value = HTBH;
            parameters[1].Value = strxmbh;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加项目成果审核表内容
        /// </summary>
        /// <param name="strhetongid"></param>
        public void UpdateXMCGSHB(string strxmbh)
        {
            //int nrows = 0;
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update ERPXMJBXX set ");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("ZKS=@ZKS,");
            strSql.Append("ZKJC=@ZKJC,");
            strSql.Append("PGDJ=@PGDJ,");
            strSql.Append("PGMJ=@PGMJ,");
            strSql.Append("KCDJ=@KCDJ,");
            strSql.Append("BXCLDJ=@BXCLDJ,");
            strSql.Append("DCMJ=@DCMJ,");
            strSql.Append("TC=@TC,");
            strSql.Append("KT=@KT,");
            strSql.Append("XMReport=@XMReport,");
            strSql.Append("ZT=@ZT");
            //strSql.Append(" where XMBH='" + strxmbh + "'");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@ZKS", SqlDbType.Int,6),
                    new SqlParameter("@ZKJC", SqlDbType.Decimal),
                    new SqlParameter("@PGDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@PGMJ", SqlDbType.Decimal),
                    new SqlParameter("@KCDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@BXCLDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@DCMJ", SqlDbType.Decimal),
                    new SqlParameter("@TC", SqlDbType.Decimal),
                    new SqlParameter("@KT", SqlDbType.Decimal),
                    new SqlParameter("@XMReport", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZT", SqlDbType.Decimal)};
            parameters[0].Value = strxmbh;
            parameters[1].Value = ZKS;
            parameters[2].Value = ZKJC;
            parameters[3].Value = PGDJ;
            parameters[4].Value = PGMJ;
            parameters[5].Value = KCDJ;
            parameters[6].Value = BXCLDJ;
            parameters[7].Value = DCMJ;
            parameters[8].Value = TC;
            parameters[9].Value = KT;
            parameters[10].Value = XMReport;
            parameters[11].Value = ZT;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            //nrows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            //return nrows;
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPXMJBXX");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPXMJBXX");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPXMJBXX(");
            strSql.Append("WorkName,XMBH,XMQQBH,HTBH,XMName,XMState,XMAdress,"
            + "WTDWName,WTDWLXR,WTDWLXDH,HZDWName,HZDWLXR,HZDWLXDH,"
            + "WTFS,ZYLB,HYLB,XMZJLY,XMJF,XMBeginTime,XMEndTime,XMBM,XMFZR,ZKS,ZYLBMain,DJTime,SHState,BeiYong1,LNG,LAT,AUTOSYSPROJECTID,NWorkID,HTState)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@XMBH,@XMQQBH,@HTBH,@XMName,@XMState,@XMAdress,"
                + "@WTDWName,@WTDWLXR,@WTDWLXDH,@HZDWName,@HZDWLXR,@HZDWLXDH,"
            + "@WTFS,@ZYLB,@HYLB,@XMZJLY,@XMJF,@XMBeginTime,@XMEndTime,@XMBM,@XMFZR,@ZKS,@ZYLBMain,@DJTime,@SHState,@BeiYong1,@LNG,@LAT,@AUTOSYSPROJECTID,@NWorkID,@HTState)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName",SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMQQBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,500),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMState", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWName",SqlDbType.NVarChar,50),
                    new SqlParameter("@HZDWLXR",SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS",SqlDbType.NVarChar,20),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMJF",SqlDbType.Decimal),
                    new SqlParameter("@XMBeginTime",SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime",SqlDbType.DateTime),
                    new SqlParameter("@XMBM",SqlDbType.NVarChar,200),
                    new SqlParameter("@XMFZR",SqlDbType.NVarChar,200),
                    new SqlParameter("@ZKS",SqlDbType.Int,4),
                    new SqlParameter("@ZYLBMain", SqlDbType.NVarChar,20),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@SHState", SqlDbType.NVarChar,20),
                    new SqlParameter("@BeiYong1", SqlDbType.NVarChar,500),
                    new SqlParameter("@LNG",SqlDbType.NVarChar,50),
                    new SqlParameter("@LAT",SqlDbType.NVarChar,50),
                    new SqlParameter("@AUTOSYSPROJECTID", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@HTState", SqlDbType.NVarChar,20)};

            parameters[0].Value = WorkName;
            parameters[1].Value = XMBH;
            parameters[2].Value = XMQQBH;
            parameters[3].Value = HTBH;
            parameters[4].Value = XMName;
            parameters[5].Value = XMState;
            parameters[6].Value = XMAdress;
            parameters[7].Value = WTDWName;
            parameters[8].Value = WTDWLXR;
            parameters[9].Value = WTDWLXDH;
            parameters[10].Value = HZDWName;
            parameters[11].Value = HZDWLXR;
            parameters[12].Value = HZDWLXDH;
            parameters[13].Value = WTFS;
            parameters[14].Value = ZYLB;
            parameters[15].Value = HYLB;
            parameters[16].Value = XMZJLY;
            parameters[17].Value = XMJF;
            parameters[18].Value = XMBeginTime;
            parameters[19].Value = XMEndTime;
            parameters[20].Value = XMBM;
            parameters[21].Value = XMFZR;
            parameters[22].Value = ZKS;
            parameters[23].Value = ZYLBMain;
            parameters[24].Value = DJTime;
            parameters[25].Value = SHState;
            parameters[26].Value = BeiYong1;
            parameters[27].Value = Lng;
            parameters[28].Value = Lat;
            parameters[29].Value = AUTOSYSPROJECTID;
            parameters[30].Value = NWorkID;
            parameters[31].Value = HTState;

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
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPXMJBXX] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMQQBH=@XMQQBH,");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("XMState=@XMState,");
            strSql.Append("XMAdress=@XMAdress,");
            strSql.Append("WTDWName=@WTDWName,");
            strSql.Append("WTDWLXR=@WTDWLXR,");
            strSql.Append("WTDWLXDH=@WTDWLXDH,");
            strSql.Append("HZDWName=@HZDWName,");
            strSql.Append("HZDWLXR=@HZDWLXR,");
            strSql.Append("HZDWLXDH=@HZDWLXDH,");
            strSql.Append("WTFS=@WTFS,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("XMZJLY=@XMZJLY,");
            strSql.Append("XMJF=@XMJF,");
            strSql.Append("XMBeginTime=@XMBeginTime,");
            strSql.Append("XMEndTime=@XMEndTime,");
            strSql.Append("XMBM=@XMBM,");
            strSql.Append("XMFZR=@XMFZR,");
            strSql.Append("ZKS=@ZKS,");
            strSql.Append("ZKJC=@ZKJC,");
            strSql.Append("PGDJ=@PGDJ,");
            strSql.Append("PGMJ=@PGMJ,");
            strSql.Append("KCDJ=@KCDJ,");
            strSql.Append("BXCLDJ=@BXCLDJ,");
            strSql.Append("DCMJ=@DCMJ,");
            strSql.Append("TC=@TC,");
            strSql.Append("KT=@KT,");
            strSql.Append("ZT=@ZT,");
            strSql.Append("ZYLBMain=@ZYLBMain,");
            strSql.Append("DJTime=@DJTime,");
            strSql.Append("SHState=@SHState,");
            strSql.Append("BeiYong1=@BeiYong1,");
            strSql.Append("XMReport=@XMReport,");
            strSql.Append("SHTime=@SHTime,");
            strSql.Append("AUTOSYSPROJECTID=@AUTOSYSPROJECTID,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("HTState=@HTState, ");
            strSql.Append("Lng=@Lng,");
            strSql.Append("Lat=@Lat");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMQQBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,500),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMState", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,500),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWName", SqlDbType.NVarChar,100),
                    new SqlParameter("@HZDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMJF", SqlDbType.Decimal,9),
                    new SqlParameter("@XMBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime", SqlDbType.DateTime),
                    new SqlParameter("@XMBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZKS", SqlDbType.Int,4),
                    new SqlParameter("@ZKJC", SqlDbType.Decimal,9),
                    new SqlParameter("@PGDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@PGMJ", SqlDbType.Decimal,9),
                    new SqlParameter("@KCDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@BXCLDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@DCMJ", SqlDbType.Decimal,9),
                    new SqlParameter("@TC", SqlDbType.Decimal,9),
                    new SqlParameter("@KT", SqlDbType.Decimal,9),
                    new SqlParameter("@ZT", SqlDbType.Decimal,9),
                    new SqlParameter("@ZYLBMain", SqlDbType.NVarChar,20),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@SHState", SqlDbType.NVarChar,20),
                    new SqlParameter("@BeiYong1", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMReport", SqlDbType.NVarChar,500),
                    new SqlParameter("@SHTime", SqlDbType.DateTime),
                    new SqlParameter("@AUTOSYSPROJECTID", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@HTState", SqlDbType.NVarChar,20),
                    new SqlParameter("@Lng",SqlDbType.NVarChar,50),
                    new SqlParameter("@Lat",SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = XMBH;
            parameters[2].Value = XMQQBH;
            parameters[3].Value = HTBH;
            parameters[4].Value = XMName;
            parameters[5].Value = XMState;
            parameters[6].Value = XMAdress;
            parameters[7].Value = WTDWName;
            parameters[8].Value = WTDWLXR;
            parameters[9].Value = WTDWLXDH;
            parameters[10].Value = HZDWName;
            parameters[11].Value = HZDWLXR;
            parameters[12].Value = HZDWLXDH;
            parameters[13].Value = WTFS;
            parameters[14].Value = ZYLB;
            parameters[15].Value = HYLB;
            parameters[16].Value = XMZJLY;
            parameters[17].Value = XMJF;
            parameters[18].Value = XMBeginTime;
            parameters[19].Value = XMEndTime;
            parameters[20].Value = XMBM;
            parameters[21].Value = XMFZR;
            parameters[22].Value = ZKS;
            parameters[23].Value = ZKJC;
            parameters[24].Value = PGDJ;
            parameters[25].Value = PGMJ;
            parameters[26].Value = KCDJ;
            parameters[27].Value = BXCLDJ;
            parameters[28].Value = DCMJ;
            parameters[29].Value = TC;
            parameters[30].Value = KT;
            parameters[31].Value = ZT;
            parameters[32].Value = ZYLBMain;
            parameters[33].Value = DJTime;
            parameters[34].Value = SHState;
            parameters[35].Value = BeiYong1;
            parameters[36].Value = XMReport;
            parameters[37].Value = SHTime;
            parameters[38].Value = AUTOSYSPROJECTID;
            parameters[39].Value = NWorkID;
            parameters[40].Value = HTState;
            parameters[41].Value = Lng;
            parameters[42].Value = Lat;
            parameters[43].Value = ID;

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
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPXMJBXX ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPXMJBXX ");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
            parameters[0].Value = strxmbh;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPXMJBXX ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMJBXX(string xmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPXMJBXX ");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.VarChar,30)};
            parameters[0].Value = xmbh;

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
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMQQBH"] != null)
                {
                    this.XMQQBH = ds.Tables[0].Rows[0]["XMQQBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMState"] != null)
                {
                    this.XMState = ds.Tables[0].Rows[0]["XMState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMAdress"] != null)
                {
                    this.XMAdress = ds.Tables[0].Rows[0]["XMAdress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWName"] != null)
                {
                    this.WTDWName = ds.Tables[0].Rows[0]["WTDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWLXR"] != null)
                {
                    this.WTDWLXR = ds.Tables[0].Rows[0]["WTDWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWLXDH"] != null)
                {
                    this.WTDWLXDH = ds.Tables[0].Rows[0]["WTDWLXDH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDWName"] != null)
                {
                    this.HZDWName = ds.Tables[0].Rows[0]["HZDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDWLXR"] != null)
                {
                    this.HZDWLXR = ds.Tables[0].Rows[0]["HZDWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDWLXDH"] != null)
                {
                    this.HZDWLXDH = ds.Tables[0].Rows[0]["HZDWLXDH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTFS"] != null)
                {
                    this.WTFS = ds.Tables[0].Rows[0]["WTFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYLB"] != null)
                {
                    this.HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMZJLY"] != null)
                {
                    this.XMZJLY = ds.Tables[0].Rows[0]["XMZJLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMJF"] != null && ds.Tables[0].Rows[0]["XMJF"].ToString() != "")
                {
                    this.XMJF = decimal.Parse(ds.Tables[0].Rows[0]["XMJF"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBeginTime"] != null && ds.Tables[0].Rows[0]["XMBeginTime"].ToString() != "")
                {
                    this.XMBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMEndTime"] != null && ds.Tables[0].Rows[0]["XMEndTime"].ToString() != "")
                {
                    this.XMEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBM"] != null)
                {
                    this.XMBM = ds.Tables[0].Rows[0]["XMBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZKS"] != null && ds.Tables[0].Rows[0]["ZKS"].ToString() != "")
                {
                    this.ZKS = int.Parse(ds.Tables[0].Rows[0]["ZKS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZKJC"] != null && ds.Tables[0].Rows[0]["ZKJC"].ToString() != "")
                {
                    this.ZKJC = float.Parse(ds.Tables[0].Rows[0]["ZKJC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PGDJ"] != null)
                {
                    this.PGDJ = ds.Tables[0].Rows[0]["PGDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PGMJ"] != null && ds.Tables[0].Rows[0]["PGMJ"].ToString() != "")
                {
                    this.PGMJ = float.Parse(ds.Tables[0].Rows[0]["PGMJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KCDJ"] != null)
                {
                    this.KCDJ = ds.Tables[0].Rows[0]["KCDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BXCLDJ"] != null)
                {
                    this.BXCLDJ = ds.Tables[0].Rows[0]["BXCLDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DCMJ"] != null && ds.Tables[0].Rows[0]["DCMJ"].ToString() != "")
                {
                    this.DCMJ = float.Parse(ds.Tables[0].Rows[0]["DCMJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TC"] != null && ds.Tables[0].Rows[0]["TC"].ToString() != "")
                {
                    this.TC = float.Parse(ds.Tables[0].Rows[0]["TC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KT"] != null && ds.Tables[0].Rows[0]["KT"].ToString() != "")
                {
                    this.KT = float.Parse(ds.Tables[0].Rows[0]["KT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZT"] != null && ds.Tables[0].Rows[0]["ZT"].ToString() != "")
                {
                    this.ZT = float.Parse(ds.Tables[0].Rows[0]["ZT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZYLBMain"] != null)
                {
                    this.ZYLBMain = ds.Tables[0].Rows[0]["ZYLBMain"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SHState"] != null)
                {
                    this.SHState = ds.Tables[0].Rows[0]["SHState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BeiYong1"] != null)
                {
                    this.BeiYong1 = ds.Tables[0].Rows[0]["BeiYong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMReport"] != null)
                {
                    this.XMReport = ds.Tables[0].Rows[0]["XMReport"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SHTime"] != null && ds.Tables[0].Rows[0]["SHTime"].ToString() != "")
                {
                    this.SHTime = DateTime.Parse(ds.Tables[0].Rows[0]["SHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTState"] != null && ds.Tables[0].Rows[0]["HTState"].ToString() != "")
                {
                    this.HTState = ds.Tables[0].Rows[0]["HTState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LNG"] != null && ds.Tables[0].Rows[0]["LNG"].ToString() != "")
                {
                    this.Lng = ds.Tables[0].Rows[0]["LNG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAT"] != null && ds.Tables[0].Rows[0]["LAT"].ToString() != "")
                {
                    this.Lat = ds.Tables[0].Rows[0]["LAT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AUTOSYSPROJECTID"] != null)
                {
                    this.AUTOSYSPROJECTID = ds.Tables[0].Rows[0]["AUTOSYSPROJECTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
            }
        }
        public void GetModelByWorkId(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
            strSql.Append(" FROM ERPXMJBXX ");
            strSql.Append(" where NWorkID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        public ZWL.BLL.ERPXMJBXX GetModelBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPXMJBXX>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        public ZWL.BLL.ERPXMJBXX GetModelByXMBH(string xmbh)
        {
            var model = GetModelBySqlWhere("XMBH='" + xmbh + "'");
            if (model != null)
            {
                return model;
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPXMJBXX ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            //string strmapping = method.getSQLTable("ERPXMJBXX");
            var strmapping = "select ID,ID as 序号,XMBH as 项目编号,XMName as 项目名称 from ERPXMJBXX";
            strSql = "select top 100 * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("54") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            //string strmapping = method.getSQLTable("ERPXMJBXX");
            var strmapping = "select ID,ID as 序号,XMBH as 项目编号,XMName as 项目名称 from ERPXMJBXX";
            strSql = "select top 100 * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (select LEFT(BeiYong1,CHARINDEX('@',BeiYong1)-1) BeiYong1 from ERPNWorkToDo where FormID=54) ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " +  strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = method.getSQLTable("ERPXMJBXX");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where 项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("54") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetAllListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = method.getSQLTable("ERPXMJBXX");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint ";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }

        /// <summary>
        /// 获得数据列表与合同
        /// </summary>
        public DataSet GetListMappingWithHT(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            //string strmapping = method.getSQLTable("ERPXMJBXX");
            var strmapping = "select ERPXMJBXX.ID as ID,ERPXMJBXX.ID as 序号,ERPXMJBXX.XMBH as 项目编号,ERPXMJBXX.XMName as 项目名称,ErpHeTong.HTID as 合同编号 from ERPXMJBXX";
            strmapping += " left join ErpHeTong on ErpHeTong.XMID=ERPXMJBXX.XMBH ";
            strSql = "select top 100 * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("54") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListMappingWithHTAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = "select ERPXMJBXX.ID as ID,ERPXMJBXX.ID as 序号,ERPXMJBXX.XMBH as 项目编号,ERPXMJBXX.XMName as 项目名称,ErpHeTong.HTID as 合同编号,ERPXMJBXX.NWorkID from ERPXMJBXX";
            strmapping += " left join ErpHeTong on ErpHeTong.XMID=ERPXMJBXX.XMBH ";
            strSql = "select top 100 * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("54") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 序号 desc";
            }
            return new Pager(strSql, cPage, pSize);
        }
        #endregion 成员方法
    }
}
