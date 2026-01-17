using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using ZWL.DBUtility;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPXMJBXXExtend。
	/// </summary>
	[Serializable]
    public partial class ERPXMJBXXExtend
    {
        public ERPXMJBXXExtend()
        { }
        #region Model
        private int _id;
        private string _xmbh;
        private string _xmname;
        private string _dwmc;
        private decimal? _jfhtjsje;
        private decimal? _ysje;
        private decimal? _ysjebase;
        private decimal? _yfje;
        private decimal? _yfjebase;
        private DateTime? _ysyfbasedate;
        private string _xmfzr;
        private string _ywlxr;
        private string _qkdwxz;
        private string _sfwg;
        private DateTime? _gwsj;
        private string _lyqk;
        private string _sfyjf;
        private string _sfyjwhz;
        private string _cscs;
        private decimal? _ysgck;
        private decimal? _ysgckbase;
        private decimal? _fbjsje;
        private decimal? _yflwf;
        private decimal? _yflwfbase;
        private string _htbh;
        private string _bm;
        private string _djr;
        private string _pzr;
        private string _zl;
        private string _zqzsq;
        private string _zqfssj;
        private string _yfdw;
        private decimal? _completedworkloadamt;
        private DateTime? _createdtime;
        private decimal? _qpje;
        private decimal? _yqqpje;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 单位名称
        /// </summary>
        public string DWMC
        {
            set { _dwmc = value; }
            get { return _dwmc; }
        }
        /// <summary>
        /// 甲方合同(结算)金额
        /// </summary>
        public decimal? JFHTJSJE
        {
            set { _jfhtjsje = value; }
            get { return _jfhtjsje; }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal? YSJE
        {
            set { _ysje = value; }
            get { return _ysje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? YSJEBase
        {
            set { _ysjebase = value; }
            get { return _ysjebase; }
        }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal? YFJE
        {
            set { _yfje = value; }
            get { return _yfje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? YFJEBase
        {
            set { _yfjebase = value; }
            get { return _yfjebase; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? YSYFBaseDate
        {
            set { _ysyfbasedate = value; }
            get { return _ysyfbasedate; }
        }
        /// <summary>
        /// 项目负责
        /// </summary>
        public string XMFZR
        {
            set { _xmfzr = value; }
            get { return _xmfzr; }
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
        /// 欠款单位性质
        /// </summary>
        public string QKDWXZ
        {
            set { _qkdwxz = value; }
            get { return _qkdwxz; }
        }
        /// <summary>
        /// 是否完工
        /// </summary>
        public string SFWG
        {
            set { _sfwg = value; }
            get { return _sfwg; }
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? GWSJ
        {
            set { _gwsj = value; }
            get { return _gwsj; }
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
        /// <summary>
        /// 已收工程款
        /// </summary>
        public decimal? YSGCK
        {
            set { _ysgck = value; }
            get { return _ysgck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? YSGCKBase
        {
            set { _ysgckbase = value; }
            get { return _ysgckbase; }
        }
        /// <summary>
        /// 分包结算金额
        /// </summary>
        public decimal? FBJSJE
        {
            set { _fbjsje = value; }
            get { return _fbjsje; }
        }
        /// <summary>
        /// 已付劳务费
        /// </summary>
        public decimal? YFLWF
        {
            set { _yflwf = value; }
            get { return _yflwf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? YFLWFBase
        {
            set { _yflwfbase = value; }
            get { return _yflwfbase; }
        }
        /// <summary>
        /// 甲方合同编号
        /// </summary>
        public string HTBH
        {
            set { _htbh = value; }
            get { return _htbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BM
        {
            set { _bm = value; }
            get { return _bm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }
        /// <summary>
        /// 批准人
        /// </summary>
        public string PZR
        {
            set { _pzr = value; }
            get { return _pzr; }
        }
        /// <summary>
        /// 账龄
        /// </summary>
        public string ZL
        {
            set { _zl = value; }
            get { return _zl; }
        }
        /// <summary>
        /// 债权追索期
        /// </summary>
        public string ZQZSQ
        {
            set { _zqzsq = value; }
            get { return _zqzsq; }
        }
        /// <summary>
        /// 债权发生时间
        /// </summary>
        public string ZQFSSJ
        {
            set { _zqfssj = value; }
            get { return _zqfssj; }
        }
        /// <summary>
        /// 合同资质单位
        /// </summary>
        public string YFDW
        {
            set { _yfdw = value; }
            get { return _yfdw; }
        }
        /// <summary>
        /// 完成的货币工作量
        /// </summary>
        public decimal? CompletedWorkLoadAmt
        {
            set { _completedworkloadamt = value; }
            get { return _completedworkloadamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
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
        /// 其中逾期期票金额
        /// </summary>
        public decimal? YQQPJE
        {
            set { _yqqpje = value; }
            get { return _yqqpje; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMJBXXExtend(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,XMBH,XMName,DWMC,JFHTJSJE,YSJE,YSJEBase,YFJE,YFJEBase,YSYFBaseDate,XMFZR,YWLXR,QKDWXZ,SFWG,GWSJ,LYQK,SFYJF,SFYJWHZ,CSCS,YSGCK,YSGCKBase,FBJSJE,YFLWF,YFLWFBase,HTBH,BM,DJR,PZR,ZL,ZQZSQ,ZQFSSJ,YFDW,CompletedWorkLoadAmt,CreatedTime,QPJE,YQQPJE ");
            strSql.Append(" FROM [ERPXMJBXXExtend] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DWMC"] != null)
                {
                    this.DWMC = ds.Tables[0].Rows[0]["DWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFHTJSJE"] != null && ds.Tables[0].Rows[0]["JFHTJSJE"].ToString() != "")
                {
                    this.JFHTJSJE = decimal.Parse(ds.Tables[0].Rows[0]["JFHTJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSJE"] != null && ds.Tables[0].Rows[0]["YSJE"].ToString() != "")
                {
                    this.YSJE = decimal.Parse(ds.Tables[0].Rows[0]["YSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSJEBase"] != null && ds.Tables[0].Rows[0]["YSJEBase"].ToString() != "")
                {
                    this.YSJEBase = decimal.Parse(ds.Tables[0].Rows[0]["YSJEBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFJE"] != null && ds.Tables[0].Rows[0]["YFJE"].ToString() != "")
                {
                    this.YFJE = decimal.Parse(ds.Tables[0].Rows[0]["YFJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFJEBase"] != null && ds.Tables[0].Rows[0]["YFJEBase"].ToString() != "")
                {
                    this.YFJEBase = decimal.Parse(ds.Tables[0].Rows[0]["YFJEBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSYFBaseDate"] != null && ds.Tables[0].Rows[0]["YSYFBaseDate"].ToString() != "")
                {
                    this.YSYFBaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["YSYFBaseDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YWLXR"] != null)
                {
                    this.YWLXR = ds.Tables[0].Rows[0]["YWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QKDWXZ"] != null)
                {
                    this.QKDWXZ = ds.Tables[0].Rows[0]["QKDWXZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFWG"] != null)
                {
                    this.SFWG = ds.Tables[0].Rows[0]["SFWG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GWSJ"] != null && ds.Tables[0].Rows[0]["GWSJ"].ToString() != "")
                {
                    this.GWSJ = DateTime.Parse(ds.Tables[0].Rows[0]["GWSJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LYQK"] != null)
                {
                    this.LYQK = ds.Tables[0].Rows[0]["LYQK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFYJF"] != null)
                {
                    this.SFYJF = ds.Tables[0].Rows[0]["SFYJF"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFYJWHZ"] != null)
                {
                    this.SFYJWHZ = ds.Tables[0].Rows[0]["SFYJWHZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSCS"] != null)
                {
                    this.CSCS = ds.Tables[0].Rows[0]["CSCS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YSGCK"] != null && ds.Tables[0].Rows[0]["YSGCK"].ToString() != "")
                {
                    this.YSGCK = decimal.Parse(ds.Tables[0].Rows[0]["YSGCK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSGCKBase"] != null && ds.Tables[0].Rows[0]["YSGCKBase"].ToString() != "")
                {
                    this.YSGCKBase = decimal.Parse(ds.Tables[0].Rows[0]["YSGCKBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBJSJE"] != null && ds.Tables[0].Rows[0]["FBJSJE"].ToString() != "")
                {
                    this.FBJSJE = decimal.Parse(ds.Tables[0].Rows[0]["FBJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFLWF"] != null && ds.Tables[0].Rows[0]["YFLWF"].ToString() != "")
                {
                    this.YFLWF = decimal.Parse(ds.Tables[0].Rows[0]["YFLWF"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFLWFBase"] != null && ds.Tables[0].Rows[0]["YFLWFBase"].ToString() != "")
                {
                    this.YFLWFBase = decimal.Parse(ds.Tables[0].Rows[0]["YFLWFBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PZR"] != null)
                {
                    this.PZR = ds.Tables[0].Rows[0]["PZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZL"] != null)
                {
                    this.ZL = ds.Tables[0].Rows[0]["ZL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQZSQ"] != null)
                {
                    this.ZQZSQ = ds.Tables[0].Rows[0]["ZQZSQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQFSSJ"] != null)
                {
                    this.ZQFSSJ = ds.Tables[0].Rows[0]["ZQFSSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YFDW"] != null)
                {
                    this.YFDW = ds.Tables[0].Rows[0]["YFDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"] != null && ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"].ToString() != "")
                {
                    this.CompletedWorkLoadAmt = decimal.Parse(ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QPJE"] != null && ds.Tables[0].Rows[0]["QPJE"].ToString() != "")
                {
                    this.QPJE = decimal.Parse(ds.Tables[0].Rows[0]["QPJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YQQPJE"] != null && ds.Tables[0].Rows[0]["YQQPJE"].ToString() != "")
                {
                    this.YQQPJE = decimal.Parse(ds.Tables[0].Rows[0]["YQQPJE"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMJBXXExtend]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPXMJBXXExtend] (");
            strSql.Append("XMBH,XMName,DWMC,JFHTJSJE,YSJE,YSJEBase,YFJE,YFJEBase,YSYFBaseDate,XMFZR,YWLXR,QKDWXZ,SFWG,GWSJ,LYQK,SFYJF,SFYJWHZ,CSCS,YSGCK,YSGCKBase,FBJSJE,YFLWF,YFLWFBase,HTBH,BM,DJR,PZR,ZL,ZQZSQ,ZQFSSJ,YFDW,CompletedWorkLoadAmt,CreatedTime,QPJE,YQQPJE)");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@XMName,@DWMC,@JFHTJSJE,@YSJE,@YSJEBase,@YFJE,@YFJEBase,@YSYFBaseDate,@XMFZR,@YWLXR,@QKDWXZ,@SFWG,@GWSJ,@LYQK,@SFYJF,@SFYJWHZ,@CSCS,@YSGCK,@YSGCKBase,@FBJSJE,@YFLWF,@YFLWFBase,@HTBH,@BM,@DJR,@PZR,@ZL,@ZQZSQ,@ZQFSSJ,@YFDW,@CompletedWorkLoadAmt,@CreatedTime,@QPJE,@YQQPJE)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,255),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,255),
                    new SqlParameter("@DWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@JFHTJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YSJEBase", SqlDbType.Decimal,9),
                    new SqlParameter("@YFJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YFJEBase", SqlDbType.Decimal,9),
                    new SqlParameter("@YSYFBaseDate", SqlDbType.DateTime),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,255),
                    new SqlParameter("@YWLXR", SqlDbType.NVarChar,255),
                    new SqlParameter("@QKDWXZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFWG", SqlDbType.NVarChar,255),
                    new SqlParameter("@GWSJ", SqlDbType.DateTime),
                    new SqlParameter("@LYQK", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFYJF", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFYJWHZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@CSCS", SqlDbType.NVarChar,255),
                    new SqlParameter("@YSGCK", SqlDbType.Decimal,9),
                    new SqlParameter("@YSGCKBase", SqlDbType.Decimal,9),
                    new SqlParameter("@FBJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YFLWF", SqlDbType.Decimal,9),
                    new SqlParameter("@YFLWFBase", SqlDbType.Decimal,9),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,255),
                    new SqlParameter("@BM", SqlDbType.NVarChar,255),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,255),
                    new SqlParameter("@PZR", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZL", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZQZSQ", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZQFSSJ", SqlDbType.NVarChar,255),
                    new SqlParameter("@YFDW", SqlDbType.NVarChar,255),
                    new SqlParameter("@CompletedWorkLoadAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@QPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YQQPJE", SqlDbType.Decimal,9)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = DWMC;
            parameters[3].Value = JFHTJSJE;
            parameters[4].Value = YSJE;
            parameters[5].Value = YSJEBase;
            parameters[6].Value = YFJE;
            parameters[7].Value = YFJEBase;
            parameters[8].Value = YSYFBaseDate;
            parameters[9].Value = XMFZR;
            parameters[10].Value = YWLXR;
            parameters[11].Value = QKDWXZ;
            parameters[12].Value = SFWG;
            parameters[13].Value = GWSJ;
            parameters[14].Value = LYQK;
            parameters[15].Value = SFYJF;
            parameters[16].Value = SFYJWHZ;
            parameters[17].Value = CSCS;
            parameters[18].Value = YSGCK;
            parameters[19].Value = YSGCKBase;
            parameters[20].Value = FBJSJE;
            parameters[21].Value = YFLWF;
            parameters[22].Value = YFLWFBase;
            parameters[23].Value = HTBH;
            parameters[24].Value = BM;
            parameters[25].Value = DJR;
            parameters[26].Value = PZR;
            parameters[27].Value = ZL;
            parameters[28].Value = ZQZSQ;
            parameters[29].Value = ZQFSSJ;
            parameters[30].Value = YFDW;
            parameters[31].Value = CompletedWorkLoadAmt;
            parameters[32].Value = CreatedTime;
            parameters[33].Value = QPJE;
            parameters[34].Value = YQQPJE;

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
            strSql.Append("update [ERPXMJBXXExtend] set ");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("DWMC=@DWMC,");
            strSql.Append("JFHTJSJE=@JFHTJSJE,");
            strSql.Append("YSJE=@YSJE,");
            strSql.Append("YSJEBase=@YSJEBase,");
            strSql.Append("YFJE=@YFJE,");
            strSql.Append("YFJEBase=@YFJEBase,");
            strSql.Append("YSYFBaseDate=@YSYFBaseDate,");
            strSql.Append("XMFZR=@XMFZR,");
            strSql.Append("YWLXR=@YWLXR,");
            strSql.Append("QKDWXZ=@QKDWXZ,");
            strSql.Append("SFWG=@SFWG,");
            strSql.Append("GWSJ=@GWSJ,");
            strSql.Append("LYQK=@LYQK,");
            strSql.Append("SFYJF=@SFYJF,");
            strSql.Append("SFYJWHZ=@SFYJWHZ,");
            strSql.Append("CSCS=@CSCS,");
            strSql.Append("YSGCK=@YSGCK,");
            strSql.Append("YSGCKBase=@YSGCKBase,");
            strSql.Append("FBJSJE=@FBJSJE,");
            strSql.Append("YFLWF=@YFLWF,");
            strSql.Append("YFLWFBase=@YFLWFBase,");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("BM=@BM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("PZR=@PZR,");
            strSql.Append("ZL=@ZL,");
            strSql.Append("ZQZSQ=@ZQZSQ,");
            strSql.Append("ZQFSSJ=@ZQFSSJ,");
            strSql.Append("YFDW=@YFDW,");
            strSql.Append("CompletedWorkLoadAmt=@CompletedWorkLoadAmt,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("QPJE=@QPJE,");
            strSql.Append("YQQPJE=@YQQPJE");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,255),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,255),
                    new SqlParameter("@DWMC", SqlDbType.NVarChar,255),
                    new SqlParameter("@JFHTJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YSJEBase", SqlDbType.Decimal,9),
                    new SqlParameter("@YFJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YFJEBase", SqlDbType.Decimal,9),
                    new SqlParameter("@YSYFBaseDate", SqlDbType.DateTime),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,255),
                    new SqlParameter("@YWLXR", SqlDbType.NVarChar,255),
                    new SqlParameter("@QKDWXZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFWG", SqlDbType.NVarChar,255),
                    new SqlParameter("@GWSJ", SqlDbType.DateTime),
                    new SqlParameter("@LYQK", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFYJF", SqlDbType.NVarChar,255),
                    new SqlParameter("@SFYJWHZ", SqlDbType.NVarChar,255),
                    new SqlParameter("@CSCS", SqlDbType.NVarChar,255),
                    new SqlParameter("@YSGCK", SqlDbType.Decimal,9),
                    new SqlParameter("@YSGCKBase", SqlDbType.Decimal,9),
                    new SqlParameter("@FBJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YFLWF", SqlDbType.Decimal,9),
                    new SqlParameter("@YFLWFBase", SqlDbType.Decimal,9),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,255),
                    new SqlParameter("@BM", SqlDbType.NVarChar,255),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,255),
                    new SqlParameter("@PZR", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZL", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZQZSQ", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZQFSSJ", SqlDbType.NVarChar,255),
                    new SqlParameter("@YFDW", SqlDbType.NVarChar,255),
                    new SqlParameter("@CompletedWorkLoadAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@QPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@YQQPJE", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = DWMC;
            parameters[3].Value = JFHTJSJE;
            parameters[4].Value = YSJE;
            parameters[5].Value = YSJEBase;
            parameters[6].Value = YFJE;
            parameters[7].Value = YFJEBase;
            parameters[8].Value = YSYFBaseDate;
            parameters[9].Value = XMFZR;
            parameters[10].Value = YWLXR;
            parameters[11].Value = QKDWXZ;
            parameters[12].Value = SFWG;
            parameters[13].Value = GWSJ;
            parameters[14].Value = LYQK;
            parameters[15].Value = SFYJF;
            parameters[16].Value = SFYJWHZ;
            parameters[17].Value = CSCS;
            parameters[18].Value = YSGCK;
            parameters[19].Value = YSGCKBase;
            parameters[20].Value = FBJSJE;
            parameters[21].Value = YFLWF;
            parameters[22].Value = YFLWFBase;
            parameters[23].Value = HTBH;
            parameters[24].Value = BM;
            parameters[25].Value = DJR;
            parameters[26].Value = PZR;
            parameters[27].Value = ZL;
            parameters[28].Value = ZQZSQ;
            parameters[29].Value = ZQFSSJ;
            parameters[30].Value = YFDW;
            parameters[31].Value = CompletedWorkLoadAmt;
            parameters[32].Value = CreatedTime;
            parameters[33].Value = QPJE;
            parameters[34].Value = YQQPJE;
            parameters[35].Value = ID;

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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPXMJBXXExtend] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
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
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,XMBH,XMName,DWMC,JFHTJSJE,YSJE,YSJEBase,YFJE,YFJEBase,YSYFBaseDate,XMFZR,YWLXR,QKDWXZ,SFWG,GWSJ,LYQK,SFYJF,SFYJWHZ,CSCS,YSGCK,YSGCKBase,FBJSJE,YFLWF,YFLWFBase,HTBH,BM,DJR,PZR,ZL,ZQZSQ,ZQFSSJ,YFDW,CompletedWorkLoadAmt,CreatedTime,QPJE,YQQPJE ");
            strSql.Append(" FROM [ERPXMJBXXExtend] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DWMC"] != null)
                {
                    this.DWMC = ds.Tables[0].Rows[0]["DWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JFHTJSJE"] != null && ds.Tables[0].Rows[0]["JFHTJSJE"].ToString() != "")
                {
                    this.JFHTJSJE = decimal.Parse(ds.Tables[0].Rows[0]["JFHTJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSJE"] != null && ds.Tables[0].Rows[0]["YSJE"].ToString() != "")
                {
                    this.YSJE = decimal.Parse(ds.Tables[0].Rows[0]["YSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSJEBase"] != null && ds.Tables[0].Rows[0]["YSJEBase"].ToString() != "")
                {
                    this.YSJEBase = decimal.Parse(ds.Tables[0].Rows[0]["YSJEBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFJE"] != null && ds.Tables[0].Rows[0]["YFJE"].ToString() != "")
                {
                    this.YFJE = decimal.Parse(ds.Tables[0].Rows[0]["YFJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFJEBase"] != null && ds.Tables[0].Rows[0]["YFJEBase"].ToString() != "")
                {
                    this.YFJEBase = decimal.Parse(ds.Tables[0].Rows[0]["YFJEBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSYFBaseDate"] != null && ds.Tables[0].Rows[0]["YSYFBaseDate"].ToString() != "")
                {
                    this.YSYFBaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["YSYFBaseDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YWLXR"] != null)
                {
                    this.YWLXR = ds.Tables[0].Rows[0]["YWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QKDWXZ"] != null)
                {
                    this.QKDWXZ = ds.Tables[0].Rows[0]["QKDWXZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFWG"] != null)
                {
                    this.SFWG = ds.Tables[0].Rows[0]["SFWG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GWSJ"] != null && ds.Tables[0].Rows[0]["GWSJ"].ToString() != "")
                {
                    this.GWSJ = DateTime.Parse(ds.Tables[0].Rows[0]["GWSJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LYQK"] != null)
                {
                    this.LYQK = ds.Tables[0].Rows[0]["LYQK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFYJF"] != null)
                {
                    this.SFYJF = ds.Tables[0].Rows[0]["SFYJF"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFYJWHZ"] != null)
                {
                    this.SFYJWHZ = ds.Tables[0].Rows[0]["SFYJWHZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSCS"] != null)
                {
                    this.CSCS = ds.Tables[0].Rows[0]["CSCS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YSGCK"] != null && ds.Tables[0].Rows[0]["YSGCK"].ToString() != "")
                {
                    this.YSGCK = decimal.Parse(ds.Tables[0].Rows[0]["YSGCK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YSGCKBase"] != null && ds.Tables[0].Rows[0]["YSGCKBase"].ToString() != "")
                {
                    this.YSGCKBase = decimal.Parse(ds.Tables[0].Rows[0]["YSGCKBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBJSJE"] != null && ds.Tables[0].Rows[0]["FBJSJE"].ToString() != "")
                {
                    this.FBJSJE = decimal.Parse(ds.Tables[0].Rows[0]["FBJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFLWF"] != null && ds.Tables[0].Rows[0]["YFLWF"].ToString() != "")
                {
                    this.YFLWF = decimal.Parse(ds.Tables[0].Rows[0]["YFLWF"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YFLWFBase"] != null && ds.Tables[0].Rows[0]["YFLWFBase"].ToString() != "")
                {
                    this.YFLWFBase = decimal.Parse(ds.Tables[0].Rows[0]["YFLWFBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PZR"] != null)
                {
                    this.PZR = ds.Tables[0].Rows[0]["PZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZL"] != null)
                {
                    this.ZL = ds.Tables[0].Rows[0]["ZL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQZSQ"] != null)
                {
                    this.ZQZSQ = ds.Tables[0].Rows[0]["ZQZSQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZQFSSJ"] != null)
                {
                    this.ZQFSSJ = ds.Tables[0].Rows[0]["ZQFSSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YFDW"] != null)
                {
                    this.YFDW = ds.Tables[0].Rows[0]["YFDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"] != null && ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"].ToString() != "")
                {
                    this.CompletedWorkLoadAmt = decimal.Parse(ds.Tables[0].Rows[0]["CompletedWorkLoadAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QPJE"] != null && ds.Tables[0].Rows[0]["QPJE"].ToString() != "")
                {
                    this.QPJE = decimal.Parse(ds.Tables[0].Rows[0]["QPJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YQQPJE"] != null && ds.Tables[0].Rows[0]["YQQPJE"].ToString() != "")
                {
                    this.YQQPJE = decimal.Parse(ds.Tables[0].Rows[0]["YQQPJE"].ToString());
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
            strSql.Append(" FROM [ERPXMJBXXExtend] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public ZWL.BLL.ERPXMJBXXExtend GetModelByXMBH(string xmbh)
        {
            return GetModelBySqlWhere("XMBH='" + xmbh + "'");
        }
        public ZWL.BLL.ERPXMJBXXExtend GetModelBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPXMJBXXExtend>(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        public List<ZWL.BLL.ERPXMJBXXExtend> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPXMJBXXExtend>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPXMJBXXExtend>(ds.Tables[0]);
            }
            return result;
        }
        #endregion  Method
    }
}
