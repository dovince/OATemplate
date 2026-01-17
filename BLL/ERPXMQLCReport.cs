using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPXMQLCReport。
    /// </summary>
    [Serializable]
    public partial class ERPXMQLCReport
    {
        public ERPXMQLCReport()
        { }
        #region Model
        private string _id;
        private string _lotid;
        private string _parentid;
        private string _xmbh;
        private string _xmname;
        private string _zylb;
        private string _dwmc;
        private string _ssbm;
        private string _xmfzr;
        private string _pzr;
        private string _ywlxr;
        private string _jfdwxz;
        private string _jfdwmc;
        private string _jfprovince;
        private string _jfcity;
        private string _jfdistrict;
        private string _zl;
        private string _xmbegintime;
        private string _xmendtime;
        private string _htbh;
        private string _sfwg;
        private string _gwsj;
        private string _lyqk;
        private string _htbgqk;
        private string _sfyjf;
        private decimal? _htje;
        private decimal? _tcjsje;
        private string _jssj;
        private decimal? _jfqrjsje;
        private decimal? _hjjsje;
        private string _kpsj;
        private decimal? _fpje;
        private string _fpbh;
        private decimal? _hjskje;
        private decimal? _wskje;
        private decimal? _wskzygz;
        private decimal? _qpje;
        private decimal? _qzyqqpje;
        private decimal? _wskwgzje;
        private string _sfyjwhz;
        private string _zqfssj;
        private string _zqzsq;
        private string _cscs;
        private string _fbdwmc;
        private string _fbprovince;
        private string _fbcity;
        private string _fbdistrict;
        private string _fbkstime;
        private string _fbjztime;
        private string _fbhtbh;
        private decimal? _fbhtje;
        private decimal? _fbhthjfkje;
        private decimal? _fbhtwfkje;
        private string _bz;
        private string _xmid;
        private int _orderby;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LotID
        {
            set { _lotid = value; }
            get { return _lotid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZYLB
        {
            set { _zylb = value; }
            get { return _zylb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DWMC
        {
            set { _dwmc = value; }
            get { return _dwmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSBM
        {
            set { _ssbm = value; }
            get { return _ssbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMFZR
        {
            set { _xmfzr = value; }
            get { return _xmfzr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PZR
        {
            set { _pzr = value; }
            get { return _pzr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YWLXR
        {
            set { _ywlxr = value; }
            get { return _ywlxr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JFDWXZ
        {
            set { _jfdwxz = value; }
            get { return _jfdwxz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JFDWMC
        {
            set { _jfdwmc = value; }
            get { return _jfdwmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JFProvince
        {
            set { _jfprovince = value; }
            get { return _jfprovince; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JFCity
        {
            set { _jfcity = value; }
            get { return _jfcity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JFDistrict
        {
            set { _jfdistrict = value; }
            get { return _jfdistrict; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZL
        {
            set { _zl = value; }
            get { return _zl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMBeginTime
        {
            set { _xmbegintime = value; }
            get { return _xmbegintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMEndTime
        {
            set { _xmendtime = value; }
            get { return _xmendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTBH
        {
            set { _htbh = value; }
            get { return _htbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SFWG
        {
            set { _sfwg = value; }
            get { return _sfwg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GWSJ
        {
            set { _gwsj = value; }
            get { return _gwsj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LYQK
        {
            set { _lyqk = value; }
            get { return _lyqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTBGQK
        {
            set { _htbgqk = value; }
            get { return _htbgqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SFYJF
        {
            set { _sfyjf = value; }
            get { return _sfyjf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HTJE
        {
            set { _htje = value; }
            get { return _htje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TCJSJE
        {
            set { _tcjsje = value; }
            get { return _tcjsje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JSSJ
        {
            set { _jssj = value; }
            get { return _jssj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JFQRJSJE
        {
            set { _jfqrjsje = value; }
            get { return _jfqrjsje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HJJSJE
        {
            set { _hjjsje = value; }
            get { return _hjjsje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KPSJ
        {
            set { _kpsj = value; }
            get { return _kpsj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FPJE
        {
            set { _fpje = value; }
            get { return _fpje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FPBH
        {
            set { _fpbh = value; }
            get { return _fpbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HJSKJE
        {
            set { _hjskje = value; }
            get { return _hjskje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WSKJE
        {
            set { _wskje = value; }
            get { return _wskje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WSKZYGZ
        {
            set { _wskzygz = value; }
            get { return _wskzygz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? QPJE
        {
            set { _qpje = value; }
            get { return _qpje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? QZYQQPJE
        {
            set { _qzyqqpje = value; }
            get { return _qzyqqpje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WSKWGZJE
        {
            set { _wskwgzje = value; }
            get { return _wskwgzje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SFYJWHZ
        {
            set { _sfyjwhz = value; }
            get { return _sfyjwhz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZQFSSJ
        {
            set { _zqfssj = value; }
            get { return _zqfssj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZQZSQ
        {
            set { _zqzsq = value; }
            get { return _zqzsq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CSCS
        {
            set { _cscs = value; }
            get { return _cscs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBDWMC
        {
            set { _fbdwmc = value; }
            get { return _fbdwmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBProvince
        {
            set { _fbprovince = value; }
            get { return _fbprovince; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBCity
        {
            set { _fbcity = value; }
            get { return _fbcity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBDistrict
        {
            set { _fbdistrict = value; }
            get { return _fbdistrict; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBKSTime
        {
            set { _fbkstime = value; }
            get { return _fbkstime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBJZTime
        {
            set { _fbjztime = value; }
            get { return _fbjztime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FBHTBH
        {
            set { _fbhtbh = value; }
            get { return _fbhtbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FBHTJE
        {
            set { _fbhtje = value; }
            get { return _fbhtje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FBHTHJFKJE
        {
            set { _fbhthjfkje = value; }
            get { return _fbhthjfkje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FBHTWFKJE
        {
            set { _fbhtwfkje = value; }
            get { return _fbhtwfkje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMID
        {
            set { _xmid = value; }
            get { return _xmid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderBy
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMQLCReport(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQLCReport] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.VarChar,-1)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMQLCReport]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.VarChar,-1)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPXMQLCReport] (");
            strSql.Append("ID,LotID,ParentID,XMBH,XMName,ZYLB,DWMC,SSBM,XMFZR,PZR,YWLXR,JFDWXZ,JFDWMC,JFProvince,JFCity,JFDistrict,ZL,XMBeginTime,XMEndTime,HTBH,SFWG,GWSJ,LYQK,HTBGQK,SFYJF,HTJE,TCJSJE,JSSJ,JFQRJSJE,HJJSJE,KPSJ,FPJE,FPBH,HJSKJE,WSKJE,WSKZYGZ,QPJE,QZYQQPJE,WSKWGZJE,SFYJWHZ,ZQFSSJ,ZQZSQ,CSCS,FBDWMC,FBProvince,FBCity,FBDistrict,FBKSTime,FBJZTime,FBHTBH,FBHTJE,FBHTHJFKJE,FBHTWFKJE,BZ,XMID,OrderBy)");
            strSql.Append(" values (");
            strSql.Append("@ID,@LotID,@ParentID,@XMBH,@XMName,@ZYLB,@DWMC,@SSBM,@XMFZR,@PZR,@YWLXR,@JFDWXZ,@JFDWMC,@JFProvince,@JFCity,@JFDistrict,@ZL,@XMBeginTime,@XMEndTime,@HTBH,@SFWG,@GWSJ,@LYQK,@HTBGQK,@SFYJF,@HTJE,@TCJSJE,@JSSJ,@JFQRJSJE,@HJJSJE,@KPSJ,@FPJE,@FPBH,@HJSKJE,@WSKJE,@WSKZYGZ,@QPJE,@QZYQQPJE,@WSKWGZJE,@SFYJWHZ,@ZQFSSJ,@ZQZSQ,@CSCS,@FBDWMC,@FBProvince,@FBCity,@FBDistrict,@FBKSTime,@FBJZTime,@FBHTBH,@FBHTJE,@FBHTHJFKJE,@FBHTWFKJE,@BZ,@XMID,@OrderBy)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.VarChar,255),
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@ParentID", SqlDbType.VarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.VarChar,255),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,255),
                    new SqlParameter("@DWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@SSBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,50),
                    new SqlParameter("@PZR", SqlDbType.NVarChar,50),
                    new SqlParameter("@YWLXR", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDWXZ", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@JFProvince", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDistrict", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZL", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBeginTime", SqlDbType.VarChar,50),
                    new SqlParameter("@XMEndTime", SqlDbType.VarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,255),
                    new SqlParameter("@SFWG", SqlDbType.NVarChar,10),
                    new SqlParameter("@GWSJ", SqlDbType.VarChar,50),
                    new SqlParameter("@LYQK", SqlDbType.VarChar,255),
                    new SqlParameter("@HTBGQK", SqlDbType.VarChar,255),
                    new SqlParameter("@SFYJF", SqlDbType.VarChar,10),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@TCJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSSJ", SqlDbType.VarChar,255),
                    new SqlParameter("@JFQRJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@HJJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@KPSJ", SqlDbType.VarChar,255),
                    new SqlParameter("@FPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FPBH", SqlDbType.VarChar,255),
                    new SqlParameter("@HJSKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKZYGZ", SqlDbType.Decimal,9),
                    new SqlParameter("@QPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@QZYQQPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKWGZJE", SqlDbType.Decimal,9),
                    new SqlParameter("@SFYJWHZ", SqlDbType.NVarChar,10),
                    new SqlParameter("@ZQFSSJ", SqlDbType.VarChar,50),
                    new SqlParameter("@ZQZSQ", SqlDbType.VarChar,50),
                    new SqlParameter("@CSCS", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@FBProvince", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDistrict", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBKSTime", SqlDbType.VarChar,50),
                    new SqlParameter("@FBJZTime", SqlDbType.VarChar,50),
                    new SqlParameter("@FBHTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@FBHTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FBHTHJFKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FBHTWFKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@XMID", SqlDbType.VarChar,255),
                    new SqlParameter("@OrderBy", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            parameters[1].Value = LotID;
            parameters[2].Value = ParentID;
            parameters[3].Value = XMBH;
            parameters[4].Value = XMName;
            parameters[5].Value = ZYLB;
            parameters[6].Value = DWMC;
            parameters[7].Value = SSBM;
            parameters[8].Value = XMFZR;
            parameters[9].Value = PZR;
            parameters[10].Value = YWLXR;
            parameters[11].Value = JFDWXZ;
            parameters[12].Value = JFDWMC;
            parameters[13].Value = JFProvince;
            parameters[14].Value = JFCity;
            parameters[15].Value = JFDistrict;
            parameters[16].Value = ZL;
            parameters[17].Value = XMBeginTime;
            parameters[18].Value = XMEndTime;
            parameters[19].Value = HTBH;
            parameters[20].Value = SFWG;
            parameters[21].Value = GWSJ;
            parameters[22].Value = LYQK;
            parameters[23].Value = HTBGQK;
            parameters[24].Value = SFYJF;
            parameters[25].Value = HTJE;
            parameters[26].Value = TCJSJE;
            parameters[27].Value = JSSJ;
            parameters[28].Value = JFQRJSJE;
            parameters[29].Value = HJJSJE;
            parameters[30].Value = KPSJ;
            parameters[31].Value = FPJE;
            parameters[32].Value = FPBH;
            parameters[33].Value = HJSKJE;
            parameters[34].Value = WSKJE;
            parameters[35].Value = WSKZYGZ;
            parameters[36].Value = QPJE;
            parameters[37].Value = QZYQQPJE;
            parameters[38].Value = WSKWGZJE;
            parameters[39].Value = SFYJWHZ;
            parameters[40].Value = ZQFSSJ;
            parameters[41].Value = ZQZSQ;
            parameters[42].Value = CSCS;
            parameters[43].Value = FBDWMC;
            parameters[44].Value = FBProvince;
            parameters[45].Value = FBCity;
            parameters[46].Value = FBDistrict;
            parameters[47].Value = FBKSTime;
            parameters[48].Value = FBJZTime;
            parameters[49].Value = FBHTBH;
            parameters[50].Value = FBHTJE;
            parameters[51].Value = FBHTHJFKJE;
            parameters[52].Value = FBHTWFKJE;
            parameters[53].Value = BZ;
            parameters[54].Value = XMID;
            parameters[55].Value = OrderBy;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPXMQLCReport] set ");
            strSql.Append("LotID=@LotID,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("DWMC=@DWMC,");
            strSql.Append("SSBM=@SSBM,");
            strSql.Append("XMFZR=@XMFZR,");
            strSql.Append("PZR=@PZR,");
            strSql.Append("YWLXR=@YWLXR,");
            strSql.Append("JFDWXZ=@JFDWXZ,");
            strSql.Append("JFDWMC=@JFDWMC,");
            strSql.Append("JFProvince=@JFProvince,");
            strSql.Append("JFCity=@JFCity,");
            strSql.Append("JFDistrict=@JFDistrict,");
            strSql.Append("ZL=@ZL,");
            strSql.Append("XMBeginTime=@XMBeginTime,");
            strSql.Append("XMEndTime=@XMEndTime,");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("SFWG=@SFWG,");
            strSql.Append("GWSJ=@GWSJ,");
            strSql.Append("LYQK=@LYQK,");
            strSql.Append("HTBGQK=@HTBGQK,");
            strSql.Append("SFYJF=@SFYJF,");
            strSql.Append("HTJE=@HTJE,");
            strSql.Append("TCJSJE=@TCJSJE,");
            strSql.Append("JSSJ=@JSSJ,");
            strSql.Append("JFQRJSJE=@JFQRJSJE,");
            strSql.Append("HJJSJE=@HJJSJE,");
            strSql.Append("KPSJ=@KPSJ,");
            strSql.Append("FPJE=@FPJE,");
            strSql.Append("FPBH=@FPBH,");
            strSql.Append("HJSKJE=@HJSKJE,");
            strSql.Append("WSKJE=@WSKJE,");
            strSql.Append("WSKZYGZ=@WSKZYGZ,");
            strSql.Append("QPJE=@QPJE,");
            strSql.Append("QZYQQPJE=@QZYQQPJE,");
            strSql.Append("WSKWGZJE=@WSKWGZJE,");
            strSql.Append("SFYJWHZ=@SFYJWHZ,");
            strSql.Append("ZQFSSJ=@ZQFSSJ,");
            strSql.Append("ZQZSQ=@ZQZSQ,");
            strSql.Append("CSCS=@CSCS,");
            strSql.Append("FBDWMC=@FBDWMC,");
            strSql.Append("FBProvince=@FBProvince,");
            strSql.Append("FBCity=@FBCity,");
            strSql.Append("FBDistrict=@FBDistrict,");
            strSql.Append("FBKSTime=@FBKSTime,");
            strSql.Append("FBJZTime=@FBJZTime,");
            strSql.Append("FBHTBH=@FBHTBH,");
            strSql.Append("FBHTJE=@FBHTJE,");
            strSql.Append("FBHTHJFKJE=@FBHTHJFKJE,");
            strSql.Append("FBHTWFKJE=@FBHTWFKJE,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("XMID=@XMID,");
            strSql.Append("OrderBy=@OrderBy");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@ParentID", SqlDbType.VarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.VarChar,255),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,255),
                    new SqlParameter("@DWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@SSBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,50),
                    new SqlParameter("@PZR", SqlDbType.NVarChar,50),
                    new SqlParameter("@YWLXR", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDWXZ", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@JFProvince", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFDistrict", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZL", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBeginTime", SqlDbType.VarChar,50),
                    new SqlParameter("@XMEndTime", SqlDbType.VarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,255),
                    new SqlParameter("@SFWG", SqlDbType.NVarChar,10),
                    new SqlParameter("@GWSJ", SqlDbType.VarChar,50),
                    new SqlParameter("@LYQK", SqlDbType.VarChar,255),
                    new SqlParameter("@HTBGQK", SqlDbType.VarChar,255),
                    new SqlParameter("@SFYJF", SqlDbType.VarChar,10),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@TCJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSSJ", SqlDbType.VarChar,255),
                    new SqlParameter("@JFQRJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@HJJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@KPSJ", SqlDbType.VarChar,255),
                    new SqlParameter("@FPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FPBH", SqlDbType.VarChar,255),
                    new SqlParameter("@HJSKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKZYGZ", SqlDbType.Decimal,9),
                    new SqlParameter("@QPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@QZYQQPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@WSKWGZJE", SqlDbType.Decimal,9),
                    new SqlParameter("@SFYJWHZ", SqlDbType.NVarChar,10),
                    new SqlParameter("@ZQFSSJ", SqlDbType.VarChar,50),
                    new SqlParameter("@ZQZSQ", SqlDbType.VarChar,50),
                    new SqlParameter("@CSCS", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@FBProvince", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBCity", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDistrict", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBKSTime", SqlDbType.VarChar,50),
                    new SqlParameter("@FBJZTime", SqlDbType.VarChar,50),
                    new SqlParameter("@FBHTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@FBHTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FBHTHJFKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@FBHTWFKJE", SqlDbType.Decimal,9),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@XMID", SqlDbType.VarChar,255),
                    new SqlParameter("@OrderBy", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.VarChar,255)};
            parameters[0].Value = LotID;
            parameters[1].Value = ParentID;
            parameters[2].Value = XMBH;
            parameters[3].Value = XMName;
            parameters[4].Value = ZYLB;
            parameters[5].Value = DWMC;
            parameters[6].Value = SSBM;
            parameters[7].Value = XMFZR;
            parameters[8].Value = PZR;
            parameters[9].Value = YWLXR;
            parameters[10].Value = JFDWXZ;
            parameters[11].Value = JFDWMC;
            parameters[12].Value = JFProvince;
            parameters[13].Value = JFCity;
            parameters[14].Value = JFDistrict;
            parameters[15].Value = ZL;
            parameters[16].Value = XMBeginTime;
            parameters[17].Value = XMEndTime;
            parameters[18].Value = HTBH;
            parameters[19].Value = SFWG;
            parameters[20].Value = GWSJ;
            parameters[21].Value = LYQK;
            parameters[22].Value = HTBGQK;
            parameters[23].Value = SFYJF;
            parameters[24].Value = HTJE;
            parameters[25].Value = TCJSJE;
            parameters[26].Value = JSSJ;
            parameters[27].Value = JFQRJSJE;
            parameters[28].Value = HJJSJE;
            parameters[29].Value = KPSJ;
            parameters[30].Value = FPJE;
            parameters[31].Value = FPBH;
            parameters[32].Value = HJSKJE;
            parameters[33].Value = WSKJE;
            parameters[34].Value = WSKZYGZ;
            parameters[35].Value = QPJE;
            parameters[36].Value = QZYQQPJE;
            parameters[37].Value = WSKWGZJE;
            parameters[38].Value = SFYJWHZ;
            parameters[39].Value = ZQFSSJ;
            parameters[40].Value = ZQZSQ;
            parameters[41].Value = CSCS;
            parameters[42].Value = FBDWMC;
            parameters[43].Value = FBProvince;
            parameters[44].Value = FBCity;
            parameters[45].Value = FBDistrict;
            parameters[46].Value = FBKSTime;
            parameters[47].Value = FBJZTime;
            parameters[48].Value = FBHTBH;
            parameters[49].Value = FBHTJE;
            parameters[50].Value = FBHTHJFKJE;
            parameters[51].Value = FBHTWFKJE;
            parameters[52].Value = BZ;
            parameters[53].Value = XMID;
            parameters[54].Value = OrderBy;
            parameters[55].Value = ID;

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
        public bool Delete(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPXMQLCReport] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.VarChar,-1)};
            parameters[0].Value = ID;

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
        public void GetModel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQLCReport] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.VarChar,-1)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null)
                {
                    this.ID = ds.Tables[0].Rows[0]["ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentID"] != null)
                {
                    this.ParentID = ds.Tables[0].Rows[0]["ParentID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DWMC"] != null)
                {
                    this.DWMC = ds.Tables[0].Rows[0]["DWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SSBM"] != null)
                {
                    this.SSBM = ds.Tables[0].Rows[0]["SSBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PZR"] != null)
                {
                    this.PZR = ds.Tables[0].Rows[0]["PZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YWLXR"] != null)
                {
                    this.YWLXR = ds.Tables[0].Rows[0]["YWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFDWXZ"] != null)
                {
                    this.JFDWXZ = ds.Tables[0].Rows[0]["JFDWXZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFDWMC"] != null)
                {
                    this.JFDWMC = ds.Tables[0].Rows[0]["JFDWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFProvince"] != null)
                {
                    this.JFProvince = ds.Tables[0].Rows[0]["JFProvince"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFCity"] != null)
                {
                    this.JFCity = ds.Tables[0].Rows[0]["JFCity"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFDistrict"] != null)
                {
                    this.JFDistrict = ds.Tables[0].Rows[0]["JFDistrict"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZL"] != null)
                {
                    this.ZL = ds.Tables[0].Rows[0]["ZL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBeginTime"] != null)
                {
                    this.XMBeginTime = ds.Tables[0].Rows[0]["XMBeginTime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMEndTime"] != null)
                {
                    this.XMEndTime = ds.Tables[0].Rows[0]["XMEndTime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFWG"] != null)
                {
                    this.SFWG = ds.Tables[0].Rows[0]["SFWG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GWSJ"] != null)
                {
                    this.GWSJ = ds.Tables[0].Rows[0]["GWSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LYQK"] != null)
                {
                    this.LYQK = ds.Tables[0].Rows[0]["LYQK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTBGQK"] != null)
                {
                    this.HTBGQK = ds.Tables[0].Rows[0]["HTBGQK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFYJF"] != null)
                {
                    this.SFYJF = ds.Tables[0].Rows[0]["SFYJF"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJE"] != null && ds.Tables[0].Rows[0]["HTJE"].ToString() != "")
                {
                    this.HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TCJSJE"] != null && ds.Tables[0].Rows[0]["TCJSJE"].ToString() != "")
                {
                    this.TCJSJE = decimal.Parse(ds.Tables[0].Rows[0]["TCJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JSSJ"] != null)
                {
                    this.JSSJ = ds.Tables[0].Rows[0]["JSSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFQRJSJE"] != null && ds.Tables[0].Rows[0]["JFQRJSJE"].ToString() != "")
                {
                    this.JFQRJSJE = decimal.Parse(ds.Tables[0].Rows[0]["JFQRJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HJJSJE"] != null && ds.Tables[0].Rows[0]["HJJSJE"].ToString() != "")
                {
                    this.HJJSJE = decimal.Parse(ds.Tables[0].Rows[0]["HJJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KPSJ"] != null)
                {
                    this.KPSJ = ds.Tables[0].Rows[0]["KPSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FPJE"] != null && ds.Tables[0].Rows[0]["FPJE"].ToString() != "")
                {
                    this.FPJE = decimal.Parse(ds.Tables[0].Rows[0]["FPJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FPBH"] != null)
                {
                    this.FPBH = ds.Tables[0].Rows[0]["FPBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HJSKJE"] != null && ds.Tables[0].Rows[0]["HJSKJE"].ToString() != "")
                {
                    this.HJSKJE = decimal.Parse(ds.Tables[0].Rows[0]["HJSKJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WSKJE"] != null && ds.Tables[0].Rows[0]["WSKJE"].ToString() != "")
                {
                    this.WSKJE = decimal.Parse(ds.Tables[0].Rows[0]["WSKJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WSKZYGZ"] != null && ds.Tables[0].Rows[0]["WSKZYGZ"].ToString() != "")
                {
                    this.WSKZYGZ = decimal.Parse(ds.Tables[0].Rows[0]["WSKZYGZ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QPJE"] != null && ds.Tables[0].Rows[0]["QPJE"].ToString() != "")
                {
                    this.QPJE = decimal.Parse(ds.Tables[0].Rows[0]["QPJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QZYQQPJE"] != null && ds.Tables[0].Rows[0]["QZYQQPJE"].ToString() != "")
                {
                    this.QZYQQPJE = decimal.Parse(ds.Tables[0].Rows[0]["QZYQQPJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WSKWGZJE"] != null && ds.Tables[0].Rows[0]["WSKWGZJE"].ToString() != "")
                {
                    this.WSKWGZJE = decimal.Parse(ds.Tables[0].Rows[0]["WSKWGZJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SFYJWHZ"] != null)
                {
                    this.SFYJWHZ = ds.Tables[0].Rows[0]["SFYJWHZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQFSSJ"] != null)
                {
                    this.ZQFSSJ = ds.Tables[0].Rows[0]["ZQFSSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQZSQ"] != null)
                {
                    this.ZQZSQ = ds.Tables[0].Rows[0]["ZQZSQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSCS"] != null)
                {
                    this.CSCS = ds.Tables[0].Rows[0]["CSCS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBDWMC"] != null)
                {
                    this.FBDWMC = ds.Tables[0].Rows[0]["FBDWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBProvince"] != null)
                {
                    this.FBProvince = ds.Tables[0].Rows[0]["FBProvince"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBCity"] != null)
                {
                    this.FBCity = ds.Tables[0].Rows[0]["FBCity"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBDistrict"] != null)
                {
                    this.FBDistrict = ds.Tables[0].Rows[0]["FBDistrict"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBKSTime"] != null)
                {
                    this.FBKSTime = ds.Tables[0].Rows[0]["FBKSTime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBJZTime"] != null)
                {
                    this.FBJZTime = ds.Tables[0].Rows[0]["FBJZTime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBHTBH"] != null)
                {
                    this.FBHTBH = ds.Tables[0].Rows[0]["FBHTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBHTJE"] != null && ds.Tables[0].Rows[0]["FBHTJE"].ToString() != "")
                {
                    this.FBHTJE = decimal.Parse(ds.Tables[0].Rows[0]["FBHTJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBHTHJFKJE"] != null && ds.Tables[0].Rows[0]["FBHTHJFKJE"].ToString() != "")
                {
                    this.FBHTHJFKJE = decimal.Parse(ds.Tables[0].Rows[0]["FBHTHJFKJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBHTWFKJE"] != null && ds.Tables[0].Rows[0]["FBHTWFKJE"].ToString() != "")
                {
                    this.FBHTWFKJE = decimal.Parse(ds.Tables[0].Rows[0]["FBHTWFKJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMID"] != null)
                {
                    this.XMID = ds.Tables[0].Rows[0]["XMID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderBy"] != null && ds.Tables[0].Rows[0]["OrderBy"].ToString() != "")
                {
                    this.OrderBy = int.Parse(ds.Tables[0].Rows[0]["OrderBy"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQLCReport] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

