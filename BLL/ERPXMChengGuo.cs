using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class ERPXMChengGuo
    {
        public ERPXMChengGuo()
        { }
        #region Model
        private int _id;
        private string _xmid;
        private string _xmname;

        private int _zuankongshu; //钻孔数
        private float _zuankongjinchi;//钻孔进尺
        private string _pinggudengji;//评估等级
        private float _pinggumianji;//评估面积
        private float _diaochamianji;//调查面积
        private float _tancao;//探槽
        private float _kengtan;//坑探
        private float _zuantan;//钻探
        private string _kanchadengji;//勘察等级
        private string _bianxingdengji;//变形测量等级

        private string _baogaomingcheng;//报告名称

        private int _nworkid;//工作编号
        private string _beiyong1;

        private DateTime _shtime;//项目成果审核通过时间
        //测量专业相关的九个字段
        private float _GCCLlength;//工程测量长度
        private float _GCCLarea;//工程测量面积
        private float _JKJCarea;//基坑监测面积
        private float _JKJCdepth;//基坑监测深度
        private float _JKSJlength;//基坑监测长度；
        private float _JKSJdepth;//基坑设计深度
        private float _JKSJarea;//基坑设计面积
        private float _GXTC;//管线探测
        private float _TRDJC;//土壤氡监测

        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 项目名称
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string BGMC
        {
            set { _baogaomingcheng = value; }
            get { return _baogaomingcheng; }
        }
        /// <summary>
        /// 钻孔数
        /// </summary>
        public int ZKS
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
        /// 工作编号
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// beiyong1
        /// </summary>
        public string BeiYong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }

        public DateTime ShTime
        {
            set { _shtime = value; }
            get { return _shtime; }
        }
        /// <summary>
        /// 工程测量相关的九个字段
        /// </summary>
        public float GCCLlength
        {
            set { _GCCLlength = value; }
            get { return _GCCLlength; }
        }

        public float GCCLarea
        {
            set { _GCCLarea = value; }
            get { return _GCCLarea; }
        }

        public float JKJCarea
        {
            set { _JKJCarea = value; }
            get { return _JKJCarea; }
        }

        public float JKJCdepth
        {
            set { _JKJCdepth = value; }
            get { return _JKJCdepth; }
        }

        public float JKSJlength
        {
            set { _JKSJlength = value; }
            get { return _JKSJlength; }
        }
        /// <summary>
        /// 基坑设计深度
        /// </summary>
        public float JKSJdepth
        {
            set { _JKSJdepth = value; }
            get { return _JKSJdepth; }
        }
        /// <summary>
        /// 基坑设计范围
        /// </summary>
        public float JKSJarea
        {
            set { _JKSJarea = value; }
            get { return _JKSJarea; }
        }
        /// <summary>
        /// 管线探测
        /// </summary>
        public float GXTC
        {
            set { _GXTC = value; }
            get { return _GXTC; }
        }
        /// <summary>
        /// 土壤氡监测
        /// </summary>
        public float TRDJC
        {
            set { _TRDJC = value; }
            get { return _TRDJC; }
        }

        #endregion Model
        #region  成员方法
        /// <summary>
        /// 根据项目的id更新数据库中对应的一条项目信息,更新时需要新建项目类，将更新的字段写入类的对应字段中
        /// </summary>
        /// <param name="strhetongid"></param>
        //public void UpdateBD(string strxmbh)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("update ERPXMJBXX set ");
        //    strSql.Append("WorkName=@WorkName,");
        //    strSql.Append("XMBH=@XMBH,");
        //    strSql.Append("XMQQBH=@XMQQBH,");
        //    strSql.Append("HTBH=@HTBH,");
        //    strSql.Append("XMName=@XMName,");
        //    strSql.Append("XMState=@XMState,");
        //    strSql.Append("XMAdress=@XMAdress,");
        //    strSql.Append("WTDWName=@WTDWName,");
        //    strSql.Append("WTDWLXR=@WTDWLXR,");
        //    strSql.Append("WTDWLXDH=@WTDWLXDH,");
        //    strSql.Append("HZDWName=@HZDWName,");
        //    strSql.Append("HZDWLXR=@HZDWLXR,");
        //    strSql.Append("HZDWLXDH=@HZDWLXDH,");
        //    strSql.Append("WTFS=@WTFS,");
        //    strSql.Append("ZYLB=@ZYLB,");
        //    strSql.Append("HYLB=@HYLB,");
        //    strSql.Append("XMZJLY=@XMZJLY,");
        //    strSql.Append("XMJF=@XMJF,");
        //    strSql.Append("XMBeginTime=@XMBeginTime,");
        //    strSql.Append("XMEndTime=@XMEndTime,");
        //    strSql.Append("XMBM=@XMBM,");
        //    strSql.Append("XMFZR=@XMFZR,");
        //    strSql.Append("ZYLBMain=@ZYLBMain,");
        //    strSql.Append("BeiYong1=@BeiYong1");
        //    strSql.Append(" where XMBH=@XMBH ");
        //    SqlParameter[] parameters = {					
        //            new SqlParameter("@WorkName",SqlDbType.NVarChar,50),
        //            new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
        //            new SqlParameter("@XMQQBH", SqlDbType.NVarChar,30),
        //            new SqlParameter("@HTBH", SqlDbType.NVarChar,30),
        //            new SqlParameter("@XMName", SqlDbType.NVarChar,500),
        //            new SqlParameter("@XMState", SqlDbType.NVarChar,30),
        //            new SqlParameter("@XMAdress", SqlDbType.NVarChar,200),
        //            new SqlParameter("@WTDWName", SqlDbType.NVarChar,200),
        //            new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
        //            new SqlParameter("@WTDWLXDH",SqlDbType.NVarChar,20),
        //            new SqlParameter("@HZDWName",SqlDbType.NVarChar,50),
        //            new SqlParameter("@HZDWLXR",SqlDbType.NVarChar,20),
        //            new SqlParameter("@HZDWLXDH",SqlDbType.NVarChar,20),
        //            new SqlParameter("@WTFS",SqlDbType.NVarChar,20),					
        //            new SqlParameter("@ZYLB", SqlDbType.NVarChar,20),
        //            new SqlParameter("@HYLB", SqlDbType.NVarChar,20),
        //            new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
        //            new SqlParameter("@XMJF",SqlDbType.Float),                    
        //            new SqlParameter("@XMBeginTime",SqlDbType.DateTime),
        //            new SqlParameter("@XMEndTime",SqlDbType.DateTime),                    
        //            new SqlParameter("@XMBM",SqlDbType.NVarChar,200),
        //            new SqlParameter("@XMFZR",SqlDbType.NVarChar,200),
        //            new SqlParameter("@ZYLBMain",SqlDbType.NVarChar,20),
        //            new SqlParameter("@BeiYong1",SqlDbType.NVarChar,500)};
        //    parameters[0].Value = WorkName;
        //    parameters[1].Value = strxmbh;
        //    parameters[2].Value = XMQQBH;
        //    parameters[3].Value = HTBH;
        //    parameters[4].Value = XMName;
        //    parameters[5].Value = XMState;
        //    parameters[6].Value = XMAdress;
        //    parameters[7].Value = WTDWName;
        //    parameters[8].Value = WTDWLXR;
        //    parameters[9].Value = WTDWLXDH;
        //    parameters[10].Value = HZDWName;
        //    parameters[11].Value = HZDWLXR;
        //    parameters[12].Value = HZDWLXDH;
        //    parameters[13].Value = WTFS;
        //    parameters[14].Value = ZYLB;
        //    parameters[15].Value = HYLB;
        //    parameters[16].Value = XMZJLY;
        //    parameters[17].Value = XMJF;
        //    parameters[18].Value = XMBeginTime;
        //    parameters[19].Value = XMEndTime;
        //    parameters[20].Value = XMBM;
        //    parameters[21].Value = XMFZR;
        //    parameters[22].Value = ZYLBMain;
        //    parameters[23].Value = BeiYong1;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}
        /// <summary>
        /// 根据项目的id更新数据库中对应的一条项目信息,更新时需要新建项目类，将更新的字段写入类的对应字段中
        /// </summary>
        /// <param name="strhetongid"></param>
        //public void UpdateHTBH(string strxmbh)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("update ERPXMJBXX set ");
        //    strSql.Append("HTBH=@HTBH,");
        //    strSql.Append("XMBH=@XMBH");
        //    strSql.Append(" where XMBH=@XMBH ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@HTBH", SqlDbType.NVarChar,30),
        //            new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
        //    parameters[0].Value = HTBH;
        //    parameters[1].Value = strxmbh;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}
        /// <summary>
        /// 增加项目成果审核表内容
        /// </summary>
        /// <param name="strhetongid"></param>
        //public void UpdateXMCGSHB(string strxmbh)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("update ERPXMJBXX set ");
        //    strSql.Append("ZKS=@ZKS,");
        //    strSql.Append("ZKJC=@ZKJC,");
        //    strSql.Append("PGDJ=@PGDJ,");
        //    strSql.Append("PGMJ=@PGMJ,");
        //    strSql.Append("KCDJ=@KCDJ,");
        //    strSql.Append("BXCLDJ=@BXCLDJ,");
        //    strSql.Append("DCMJ=@DCMJ,");
        //    strSql.Append("TC=@TC,");
        //    strSql.Append("KT=@KT,");
        //    strSql.Append("ZT=@ZT");
        //    strSql.Append(" where XMBH='" + strxmbh + "'");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ZKS", SqlDbType.Int),
        //            new SqlParameter("@ZKJC", SqlDbType.Decimal),
        //            new SqlParameter("@PGDJ", SqlDbType.NVarChar,50),
        //            new SqlParameter("@PGMJ", SqlDbType.Decimal),
        //            new SqlParameter("@KCDJ", SqlDbType.NVarChar,50),
        //            new SqlParameter("@BXCLDJ", SqlDbType.NVarChar,50),
        //            new SqlParameter("@DCMJ", SqlDbType.Decimal),
        //            new SqlParameter("@TC", SqlDbType.Decimal),
        //            new SqlParameter("@KT", SqlDbType.Decimal),
        //            new SqlParameter("@ZT", SqlDbType.Decimal)};
        //    parameters[0].Value = ZKS;
        //    parameters[1].Value = ZKJC;
        //    parameters[2].Value = PGDJ;
        //    parameters[3].Value = PGMJ;
        //    parameters[4].Value = KCDJ;
        //    parameters[5].Value = BXCLDJ;
        //    parameters[6].Value = DCMJ;
        //    parameters[7].Value = TC;
        //    parameters[8].Value = KT;
        //    parameters[9].Value = ZT;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}
        /// <summary>
        /// 得到最大ID
        /// </summary>
        //public int GetMaxId()
        //{

        //    return DbHelperSQL.GetMaxID("ID", "ERPXMChengGuo");
        //}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        //public bool Exists(int ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from ERPXMChengGuo");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,6)};
        //    parameters[0].Value = ID;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPXMChengGuo(");
            strSql.Append("XMBH,XMName,ZKS,ZKJC,PGDJ,PGMJ,KCDJ,BXCLDJ,DCMJ,TC,KT,ZT,NWorkToDoID,BeiYong1,BGMC,ShTime,[GCCLlength],[GCCLarea],[JKJCarea],[JKJCdepth],[JKSJlength],[JKSJdepth],[JKSJarea],[GXTC],[TRDJC])");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@XMName,@ZKS,@ZKJC,@PGDJ,@PGMJ,@KCDJ,@BXCLDJ,@DCMJ,@TC,@KT,@ZT,@NWorkToDoID,@BeiYong1,@BGMC,@ShTime,@GCCLlength,@GCCLarea,@JKJCarea,@JKJCdepth,@JKSJlength,@JKSJdepth,@JKSJarea,@GXTC,@TRDJC)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZKS", SqlDbType.Int),
                    new SqlParameter("@ZKJC", SqlDbType.Decimal),
                    new SqlParameter("@PGDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@PGMJ", SqlDbType.Decimal),
                    new SqlParameter("@KCDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@BXCLDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@DCMJ", SqlDbType.Decimal),
                    new SqlParameter("@TC", SqlDbType.Decimal),
                    new SqlParameter("@KT", SqlDbType.Decimal),
                    new SqlParameter("@ZT", SqlDbType.Decimal),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int),
                    new SqlParameter("@BeiYong1", SqlDbType.NVarChar,500),
                    new SqlParameter("@BGMC", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ShTime", SqlDbType.DateTime),
                    new SqlParameter("@GCCLlength", SqlDbType.Decimal),
                    new SqlParameter("@GCCLarea", SqlDbType.Decimal),
                    new SqlParameter("@JKJCarea", SqlDbType.Decimal),
                    new SqlParameter("@JKJCdepth", SqlDbType.Decimal),
                    new SqlParameter("@JKSJlength", SqlDbType.Decimal),
                    new SqlParameter("@JKSJdepth", SqlDbType.Decimal),
                    new SqlParameter("@JKSJarea", SqlDbType.Decimal),
                    new SqlParameter("@GXTC", SqlDbType.Decimal),
                    new SqlParameter("@TRDJC", SqlDbType.Decimal)
                                        };
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = ZKS;
            parameters[3].Value = ZKJC;
            parameters[4].Value = PGDJ;
            parameters[5].Value = PGMJ;
            parameters[6].Value = KCDJ;
            parameters[7].Value = BXCLDJ;
            parameters[8].Value = DCMJ;
            parameters[9].Value = TC;
            parameters[10].Value = KT;
            parameters[11].Value = ZT;
            parameters[12].Value = NWorkToDoID;
            parameters[13].Value = BeiYong1;
            parameters[14].Value = BGMC;
            parameters[15].Value = ShTime;
            parameters[16].Value = GCCLlength;
            parameters[17].Value = GCCLarea;
            parameters[18].Value = JKJCarea;
            parameters[19].Value = JKJCdepth;
            parameters[20].Value = JKSJlength;
            parameters[21].Value = JKSJdepth;
            parameters[22].Value = JKSJarea;
            parameters[23].Value = GXTC;
            parameters[24].Value = TRDJC;
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
        /// <summary>
        /// 增加项目成果审核表内容
        /// </summary>
        /// <param name="strhetongid"></param>
        public void UpdateXMCG(int nNWorkID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update ERPXMChengGuo set ");
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
            strSql.Append("ShTime=@ShTime,");
            strSql.Append("GCCLlength = @GCCLlength,");
            strSql.Append("GCCLarea = @GCCLarea,");
            strSql.Append("JKJCarea = @JKJCarea,");
            strSql.Append("JKJCdepth = @JKJCdepth,");
            strSql.Append("JKSJlength = @JKSJlength,");
            strSql.Append("JKSJdepth = @JKSJdepth,");
            strSql.Append("JKSJarea= @JKSJarea,");
            strSql.Append("GXTC = @GXTC,");
            strSql.Append("TRDJC = @TRDJC");
            strSql.Append(" where NWorkToDoID='" + nNWorkID + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@ZKS", SqlDbType.Int),
                    new SqlParameter("@ZKJC", SqlDbType.Decimal),
                    new SqlParameter("@PGDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@PGMJ", SqlDbType.Decimal),
                    new SqlParameter("@KCDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@BXCLDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@DCMJ", SqlDbType.Decimal),
                    new SqlParameter("@TC", SqlDbType.Decimal),
                    new SqlParameter("@KT", SqlDbType.Decimal),
                    new SqlParameter("@ZT", SqlDbType.Decimal),
                    new SqlParameter("@ShTime", SqlDbType.DateTime),
                    new SqlParameter("@GCCLlength", SqlDbType.Decimal),
                    new SqlParameter("@GCCLarea", SqlDbType.Decimal),
                    new SqlParameter("@JKJCarea", SqlDbType.Decimal),
                    new SqlParameter("@JKJCdepth", SqlDbType.Decimal),
                    new SqlParameter("@JKSJlength", SqlDbType.Decimal),
                    new SqlParameter("@JKSJdepth", SqlDbType.Decimal),
                    new SqlParameter("@JKSJarea", SqlDbType.Decimal),
                    new SqlParameter("@GXTC", SqlDbType.Decimal),
                    new SqlParameter("@TRDJC", SqlDbType.Decimal)
                                        };
            parameters[0].Value = ZKS;
            parameters[1].Value = ZKJC;
            parameters[2].Value = PGDJ;
            parameters[3].Value = PGMJ;
            parameters[4].Value = KCDJ;
            parameters[5].Value = BXCLDJ;
            parameters[6].Value = DCMJ;
            parameters[7].Value = TC;
            parameters[8].Value = KT;
            parameters[9].Value = ZT;
            parameters[10].Value = ShTime;
            parameters[11].Value = GCCLlength;
            parameters[12].Value = GCCLarea;
            parameters[13].Value = JKJCarea;
            parameters[14].Value = JKJCdepth;
            parameters[15].Value = JKSJlength;
            parameters[16].Value = JKSJdepth;
            parameters[17].Value = JKSJarea;
            parameters[18].Value = GXTC;
            parameters[19].Value = TRDJC;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public void Delete(int ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from ERPXMJBXX ");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,6)};
        //    parameters[0].Value = ID;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        //public void GetModel(string strxmbh)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select *");
        //    strSql.Append(" FROM ERPXMJBXX ");
        //    strSql.Append(" where XMBH=@XMBH ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
        //    parameters[0].Value = strxmbh;

        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables.Count>0&&ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["XMBH"].ToString() != "")
        //        {
        //            XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
        //        }
        //        WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
        //        XMQQBH = ds.Tables[0].Rows[0]["XMQQBH"].ToString();
        //        HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
        //        XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
        //        XMState = ds.Tables[0].Rows[0]["XMState"].ToString();
        //        XMAdress = ds.Tables[0].Rows[0]["XMAdress"].ToString();
        //        WTDWName = ds.Tables[0].Rows[0]["WTDWName"].ToString();
        //        WTDWLXR = ds.Tables[0].Rows[0]["WTDWLXR"].ToString();
        //        WTDWLXDH = ds.Tables[0].Rows[0]["WTDWLXDH"].ToString();
        //        HZDWName = ds.Tables[0].Rows[0]["HZDWName"].ToString();
        //        HZDWLXR = ds.Tables[0].Rows[0]["HZDWLXR"].ToString();
        //        HZDWLXDH = ds.Tables[0].Rows[0]["HZDWLXDH"].ToString();
        //        WTFS = ds.Tables[0].Rows[0]["WTFS"].ToString();
        //        ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
        //        HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
        //        XMZJLY = ds.Tables[0].Rows[0]["XMZJLY"].ToString();
        //        XMJF = float.Parse(ds.Tables[0].Rows[0]["XMJF"].ToString());

        //        if (ds.Tables[0].Rows[0]["XMBeginTime"].ToString() != "")
        //        {
        //            XMBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMBeginTime"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["XMEndTime"].ToString() != "")
        //        {
        //            XMEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMEndTime"].ToString());
        //        }
        //        XMBM = ds.Tables[0].Rows[0]["XMBM"].ToString();
        //        XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();

        //        if (ds.Tables[0].Rows[0]["ZKS"].ToString() != "")
        //        {
        //            ZKS = int.Parse(ds.Tables[0].Rows[0]["ZKS"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["ZKJC"].ToString() != "")
        //        {
        //            ZKJC = float.Parse(ds.Tables[0].Rows[0]["ZKJC"].ToString());
        //        }
                
        //        PGDJ = ds.Tables[0].Rows[0]["PGDJ"].ToString();
        //        KCDJ = ds.Tables[0].Rows[0]["KCDJ"].ToString();
        //        BXCLDJ = ds.Tables[0].Rows[0]["BXCLDJ"].ToString();
               
        //        if (ds.Tables[0].Rows[0]["PGMJ"].ToString() != "")
        //        {
        //            PGMJ = float.Parse(ds.Tables[0].Rows[0]["PGMJ"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["DCMJ"].ToString() != "")
        //        {
        //            DCMJ = float.Parse(ds.Tables[0].Rows[0]["DCMJ"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["TC"].ToString() != "")
        //        {
        //            TC = float.Parse(ds.Tables[0].Rows[0]["TC"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["KT"].ToString() != "")
        //        {
        //            KT = float.Parse(ds.Tables[0].Rows[0]["KT"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["ZT"].ToString() != "")
        //        {
        //            ZT = float.Parse(ds.Tables[0].Rows[0]["ZT"].ToString());
        //        }

        //        ZYLBMain = ds.Tables[0].Rows[0]["ZYLBMain"].ToString();
        //        if (ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
        //        {
        //            DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
        //        }
        //        SHState = ds.Tables[0].Rows[0]["SHState"].ToString();
        //        BeiYong1 = ds.Tables[0].Rows[0]["BeiYong1"].ToString();

        //    }
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        //public ERPXMJBXX(string xmbh)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM ERPXMJBXX ");
        //    strSql.Append(" where XMBH=@XMBH ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@XMBH", SqlDbType.VarChar,30)};
        //    parameters[0].Value = xmbh;

        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
        //        {
        //            ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
        //        }
        //        WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
        //        XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
        //        XMQQBH = ds.Tables[0].Rows[0]["XMQQBH"].ToString();
        //        HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
        //        XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
        //        XMState = ds.Tables[0].Rows[0]["XMState"].ToString();
        //        XMAdress = ds.Tables[0].Rows[0]["XMAdress"].ToString();
        //        WTDWName = ds.Tables[0].Rows[0]["WTDWName"].ToString();
        //        WTDWLXR = ds.Tables[0].Rows[0]["WTDWLXR"].ToString();
        //        WTDWLXDH = ds.Tables[0].Rows[0]["WTDWLXDH"].ToString();
        //        HZDWName = ds.Tables[0].Rows[0]["HZDWName"].ToString();
        //        HZDWLXR = ds.Tables[0].Rows[0]["HZDWLXR"].ToString();
        //        HZDWLXDH = ds.Tables[0].Rows[0]["HZDWLXDH"].ToString();
        //        WTFS = ds.Tables[0].Rows[0]["WTFS"].ToString();
        //        ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
        //        HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
        //        XMZJLY = ds.Tables[0].Rows[0]["XMZJLY"].ToString();
        //        XMJF = float.Parse(ds.Tables[0].Rows[0]["XMJF"].ToString());
        //        if (ds.Tables[0].Rows[0]["XMBeginTime"].ToString() != "")
        //        {
        //            XMBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMBeginTime"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["XMEndTime"].ToString() != "")
        //        {
        //            XMEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMEndTime"].ToString());
        //        }
        //        XMBM = ds.Tables[0].Rows[0]["XMBM"].ToString();
        //        XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();

        //        if (ds.Tables[0].Rows[0]["ZKS"].ToString() != "")
        //        {
        //            ZKS = int.Parse(ds.Tables[0].Rows[0]["ZKS"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["ZKJC"].ToString() != "")
        //        {
        //            ZKJC = float.Parse(ds.Tables[0].Rows[0]["ZKJC"].ToString());
        //        }

        //        PGDJ = ds.Tables[0].Rows[0]["PGDJ"].ToString();
        //        KCDJ = ds.Tables[0].Rows[0]["KCDJ"].ToString();
        //        BXCLDJ = ds.Tables[0].Rows[0]["BXCLDJ"].ToString();

        //        if (ds.Tables[0].Rows[0]["PGMJ"].ToString() != "")
        //        {
        //            PGMJ = float.Parse(ds.Tables[0].Rows[0]["PGMJ"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["DCMJ"].ToString() != "")
        //        {
        //            DCMJ = float.Parse(ds.Tables[0].Rows[0]["DCMJ"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["TC"].ToString() != "")
        //        {
        //            TC = float.Parse(ds.Tables[0].Rows[0]["TC"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["KT"].ToString() != "")
        //        {
        //            KT = float.Parse(ds.Tables[0].Rows[0]["KT"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["ZT"].ToString() != "")
        //        {
        //            ZT = float.Parse(ds.Tables[0].Rows[0]["ZT"].ToString());
        //        }

        //        ZYLBMain = ds.Tables[0].Rows[0]["ZYLBMain"].ToString();
        //        if (ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
        //        {
        //            DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
        //        }
        //        SHState = ds.Tables[0].Rows[0]["SHState"].ToString();
        //        BeiYong1 = ds.Tables[0].Rows[0]["BeiYong1"].ToString();
        //    }
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM ERPXMJBXX ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetListMapping(string strWhere)
        //{
        //    string strSql = "";
        //    ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
        //    string strmapping = method.getSQLTable("ERPXMJBXX");
        //    strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql += " where " + "LB_MrALLFint." + strWhere + " order by 序号 desc";
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            //string strmapping = method.getSQLTable("ERPXMJBXX");
            var strmapping = "select ID,ID as 序号,XMBH as 项目编号,XMName as 项目名称,BGMC as 报告名称 from ERPXMChengGuo";
            strSql = "select top 100 * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("54") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = "select ID,ID as 序号,XMBH as 项目编号,XMName as 项目名称,BGMC as 报告名称 from ERPXMChengGuo";
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
