using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;//请先添加引用
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPTouBiao。
    /// </summary>
    public class ERPTouBiao
    {
        public ERPTouBiao()
        { }
        #region Model
        private int _id;
        private string _workname;
        private string _tbxmbh;
        private DateTime? _dengjitime;
        private string _tbxmmc;
        private string _tbfs;
        private string _zylb;
        private string _hylb;
        private string _yzdwname;
        private string _zbdlname;
        private string _wz;
        private string _zzdwname;
        private string _lhdwname;
        private DateTime? _tbtime;
        private string _tbbm;
        private string _jyfs;
        private string _sqwtr;
        private string _tbmanager;
        private DateTime? _kbtime;
        private string _xmqqbh;
        private string _zbqk;
        private int _nworkid;

        private decimal _tbbj;//投标报价

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
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TBXMBH
        {
            set { _tbxmbh = value; }
            get { return _tbxmbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DengJiTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }
        /// <summary>
        /// 投标报价
        /// </summary>
        public decimal TBBJ
        {
            set { _tbbj = value; }
            get { return _tbbj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TBXMMC
        {
            set { _tbxmmc = value; }
            get { return _tbxmmc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TBFS
        {
            set { _tbfs = value; }
            get { return _tbfs; }
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
        public string HYLB
        {
            set { _hylb = value; }
            get { return _hylb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YZDWName
        {
            set { _yzdwname = value; }
            get { return _yzdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZBDLName
        {
            set { _zbdlname = value; }
            get { return _zbdlname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WZ
        {
            set { _wz = value; }
            get { return _wz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZZDWName
        {
            set { _zzdwname = value; }
            get { return _zzdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LHDWName
        {
            set { _lhdwname = value; }
            get { return _lhdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TBTime
        {
            set { _tbtime = value; }
            get { return _tbtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TBBM
        {
            set { _tbbm = value; }
            get { return _tbbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYFS
        {
            set { _jyfs = value; }
            get { return _jyfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SQWTR
        {
            set { _sqwtr = value; }
            get { return _sqwtr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TBManager
        {
            set { _tbmanager = value; }
            get { return _tbmanager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? KBTime
        {
            set { _kbtime = value; }
            get { return _kbtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMQQBH
        {
            set { _xmqqbh = value; }
            get { return _xmqqbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZBQK
        {
            set { _zbqk = value; }
            get { return _zbqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }


        #endregion Model

        #region Relative Model

        public ZWL.BLL.ERPNWorkToDo CurrentWorkTodo
        {
            get
            {
                var _worktodo = new ZWL.BLL.ERPNWorkToDo();
                _worktodo.GetModel(NWorkID);
                return _worktodo;
            }
        }
        #endregion

        #region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateBD(string strtbxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPTouBiao set ");
            strSql.Append("TBXMMC=@TBXMMC,");
            strSql.Append("TBFS=@TBFS,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("YZDWName=@YZDWName,");
            strSql.Append("ZBDLName=@ZBDLName,");
            strSql.Append("WZ=@WZ,");
            strSql.Append("ZZDWName=@ZZDWName,");
            strSql.Append("LHDWName=@LHDWName,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("TBBM=@TBBM,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("SQWTR=@SQWTR,");
            strSql.Append("TBManager=@TBManager,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("ZBQK=@ZBQK,");
            strSql.Append("KBTime=@KBTime,");
            strSql.Append("TBBJ=@TBBJ");
            strSql.Append(" where TBXMBH=@TBXMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TBXMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@TBXMMC", SqlDbType.NVarChar,500),
                    new SqlParameter("@TBFS", SqlDbType.NVarChar,50),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@YZDWName", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZBDLName",SqlDbType.NVarChar,200),
                    new SqlParameter("@WZ",SqlDbType.NVarChar,200),
                    new SqlParameter("@ZZDWName",SqlDbType.NVarChar,200),
                    new SqlParameter("@LHDWName",SqlDbType.NVarChar,200),
                    new SqlParameter("@TBTime",SqlDbType.DateTime),
                    new SqlParameter("@TBBM",SqlDbType.NVarChar,200),
                    new SqlParameter("@JYFS",SqlDbType.NVarChar,50),
                    new SqlParameter("@SQWTR",SqlDbType.NVarChar,200),
                    new SqlParameter("@TBManager",SqlDbType.NVarChar,1000),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZBQK",SqlDbType.NVarChar,50),
                    new SqlParameter("@KBTime",SqlDbType.DateTime),
                    new SqlParameter("@TBBJ",SqlDbType.Decimal)};
            parameters[0].Value = strtbxmbh;
            parameters[1].Value = TBXMMC;
            parameters[2].Value = TBFS;
            parameters[3].Value = HYLB;
            parameters[4].Value = YZDWName;
            parameters[5].Value = ZBDLName;
            parameters[6].Value = WZ;
            parameters[7].Value = ZZDWName;
            parameters[8].Value = LHDWName;
            parameters[9].Value = TBTime;
            parameters[10].Value = TBBM;
            parameters[11].Value = JYFS;
            parameters[12].Value = SQWTR;
            parameters[13].Value = TBManager;
            parameters[14].Value = ZYLB;
            parameters[15].Value = ZBQK;
            parameters[16].Value = KBTime;
            parameters[17].Value = TBBJ;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPTouBiao(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPTouBiao] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPTouBiao(string tbID)
        {
            DateTime defaultime = new DateTime();
            PublicMethod.GetDefaultTime(out defaultime);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPTouBiao ");
            strSql.Append(" where TBXMBH=@TBXMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TBXMBH", SqlDbType.VarChar,50)};
            parameters[0].Value = tbID;

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
                if (ds.Tables[0].Rows[0]["TBXMBH"] != null)
                {
                    this.TBXMBH = ds.Tables[0].Rows[0]["TBXMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TBXMMC"] != null)
                {
                    this.TBXMMC = ds.Tables[0].Rows[0]["TBXMMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBFS"] != null)
                {
                    this.TBFS = ds.Tables[0].Rows[0]["TBFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYLB"] != null)
                {
                    this.HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YZDWName"] != null)
                {
                    this.YZDWName = ds.Tables[0].Rows[0]["YZDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZBDLName"] != null)
                {
                    this.ZBDLName = ds.Tables[0].Rows[0]["ZBDLName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WZ"] != null)
                {
                    this.WZ = ds.Tables[0].Rows[0]["WZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZZDWName"] != null)
                {
                    this.ZZDWName = ds.Tables[0].Rows[0]["ZZDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LHDWName"] != null)
                {
                    this.LHDWName = ds.Tables[0].Rows[0]["LHDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null && ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TBBM"] != null)
                {
                    this.TBBM = ds.Tables[0].Rows[0]["TBBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JYFS"] != null)
                {
                    this.JYFS = ds.Tables[0].Rows[0]["JYFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQWTR"] != null)
                {
                    this.SQWTR = ds.Tables[0].Rows[0]["SQWTR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBManager"] != null)
                {
                    this.TBManager = ds.Tables[0].Rows[0]["TBManager"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KBTime"] != null && ds.Tables[0].Rows[0]["KBTime"].ToString() != "")
                {
                    this.KBTime = DateTime.Parse(ds.Tables[0].Rows[0]["KBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMQQBH"] != null)
                {
                    this.XMQQBH = ds.Tables[0].Rows[0]["XMQQBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZBQK"] != null)
                {
                    this.ZBQK = ds.Tables[0].Rows[0]["ZBQK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TBBJ"] != null && ds.Tables[0].Rows[0]["TBBJ"].ToString() != "")
                {
                    this.TBBJ = decimal.Parse(ds.Tables[0].Rows[0]["TBBJ"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPTouBiao");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPTouBiao");
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
            strSql.Append("insert into [ERPTouBiao] (");
            strSql.Append("WorkName,TBXMBH,DengJiTime,TBXMMC,TBFS,ZYLB,HYLB,YZDWName,ZBDLName,WZ,ZZDWName,LHDWName,TBTime,TBBM,JYFS,SQWTR,TBManager,KBTime,XMQQBH,ZBQK,NWorkID,TBBJ)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@TBXMBH,@DengJiTime,@TBXMMC,@TBFS,@ZYLB,@HYLB,@YZDWName,@ZBDLName,@WZ,@ZZDWName,@LHDWName,@TBTime,@TBBM,@JYFS,@SQWTR,@TBManager,@KBTime,@XMQQBH,@ZBQK,@NWorkID,@TBBJ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,200),
                    new SqlParameter("@TBXMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@DengJiTime", SqlDbType.Date,3),
                    new SqlParameter("@TBXMMC", SqlDbType.VarChar,500),
                    new SqlParameter("@TBFS", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,200),
                    new SqlParameter("@HYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@YZDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@ZBDLName", SqlDbType.VarChar,200),
                    new SqlParameter("@WZ", SqlDbType.VarChar,200),
                    new SqlParameter("@ZZDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@LHDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@TBTime", SqlDbType.Date,3),
                    new SqlParameter("@TBBM", SqlDbType.VarChar,200),
                    new SqlParameter("@JYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@SQWTR", SqlDbType.VarChar,1000),
                    new SqlParameter("@TBManager", SqlDbType.VarChar,1000),
                    new SqlParameter("@KBTime", SqlDbType.Date,3),
                    new SqlParameter("@XMQQBH", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBQK", SqlDbType.VarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@TBBJ", SqlDbType.Decimal)};
            parameters[0].Value = WorkName;
            parameters[1].Value = TBXMBH;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = TBXMMC;
            parameters[4].Value = TBFS;
            parameters[5].Value = ZYLB;
            parameters[6].Value = HYLB;
            parameters[7].Value = YZDWName;
            parameters[8].Value = ZBDLName;
            parameters[9].Value = WZ;
            parameters[10].Value = ZZDWName;
            parameters[11].Value = LHDWName;
            parameters[12].Value = TBTime;
            parameters[13].Value = TBBM;
            parameters[14].Value = JYFS;
            parameters[15].Value = SQWTR;
            parameters[16].Value = TBManager;
            parameters[17].Value = KBTime;
            parameters[18].Value = XMQQBH;
            parameters[19].Value = ZBQK;
            parameters[20].Value = NWorkID;
            parameters[21].Value = TBBJ;

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
            strSql.Append("update [ERPTouBiao] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("TBXMBH=@TBXMBH,");
            strSql.Append("DengJiTime=@DengJiTime,");
            strSql.Append("TBXMMC=@TBXMMC,");
            strSql.Append("TBFS=@TBFS,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("YZDWName=@YZDWName,");
            strSql.Append("ZBDLName=@ZBDLName,");
            strSql.Append("WZ=@WZ,");
            strSql.Append("ZZDWName=@ZZDWName,");
            strSql.Append("LHDWName=@LHDWName,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("TBBM=@TBBM,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("SQWTR=@SQWTR,");
            strSql.Append("TBManager=@TBManager,");
            strSql.Append("KBTime=@KBTime,");
            strSql.Append("XMQQBH=@XMQQBH,");
            strSql.Append("ZBQK=@ZBQK,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("TBBJ=@TBBJ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,200),
                    new SqlParameter("@TBXMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@DengJiTime", SqlDbType.Date,3),
                    new SqlParameter("@TBXMMC", SqlDbType.VarChar,500),
                    new SqlParameter("@TBFS", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,200),
                    new SqlParameter("@HYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@YZDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@ZBDLName", SqlDbType.VarChar,200),
                    new SqlParameter("@WZ", SqlDbType.VarChar,200),
                    new SqlParameter("@ZZDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@LHDWName", SqlDbType.VarChar,200),
                    new SqlParameter("@TBTime", SqlDbType.Date,3),
                    new SqlParameter("@TBBM", SqlDbType.VarChar,200),
                    new SqlParameter("@JYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@SQWTR", SqlDbType.VarChar,1000),
                    new SqlParameter("@TBManager", SqlDbType.VarChar,1000),
                    new SqlParameter("@KBTime", SqlDbType.Date,3),
                    new SqlParameter("@XMQQBH", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBQK", SqlDbType.VarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@TBBJ", SqlDbType.Decimal),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = TBXMBH;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = TBXMMC;
            parameters[4].Value = TBFS;
            parameters[5].Value = ZYLB;
            parameters[6].Value = HYLB;
            parameters[7].Value = YZDWName;
            parameters[8].Value = ZBDLName;
            parameters[9].Value = WZ;
            parameters[10].Value = ZZDWName;
            parameters[11].Value = LHDWName;
            parameters[12].Value = TBTime;
            parameters[13].Value = TBBM;
            parameters[14].Value = JYFS;
            parameters[15].Value = SQWTR;
            parameters[16].Value = TBManager;
            parameters[17].Value = KBTime;
            parameters[18].Value = XMQQBH;
            parameters[19].Value = ZBQK;
            parameters[20].Value = NWorkID;
            parameters[21].Value = TBBJ;
            parameters[22].Value = ID;
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
            strSql.Append("delete from ERPTouBiao ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPTouBiao ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetSTRModel(string strtbxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPTouBiao ");
            strSql.Append(" where TBXMBH=@TBXMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TBXMBH", SqlDbType.NVarChar,50)};
            parameters[0].Value = strtbxmbh;

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
            strSql.Append(" FROM ERPTouBiao ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public List<ZWL.BLL.ERPTouBiao> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPTouBiao>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableHelper.ConvertTo<ZWL.BLL.ERPTouBiao>(ds.Tables[0]);
            }
            return list;
        }
        public ZWL.BLL.ERPTouBiao GetModelByWhere(string strWhere)
        {
            var list = GetListModel(strWhere);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public ZWL.BLL.ERPTouBiao GetModelByWorkId(int workid)
        {
            return GetModelByWhere("NWorkID=" + workid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            PublicMethod method = new PublicMethod();
            string strmapping = method.getSQLTable("ERPTouBiao");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where 投标项目编号 in (" + PublicMethod.GetNWorkToDoIDList("39") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 登记时间 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new PublicMethod();
            var strmapping = method.getSQLTable("ERPTouBiao");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where 投标项目编号 in (" + PublicMethod.GetNWorkToDoIDList("39") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }


        #endregion  成员方法
    }
}

