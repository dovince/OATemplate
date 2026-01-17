using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPCarUseSP。
    /// </summary>
    public partial class ERPCarUseSP
    {
        public ERPCarUseSP()
        { }
        #region Model
        private int _id;
        private string _sqr;
        private string _sqbm;
        private DateTime? _sqtime;
        private string _syr;
        private decimal? _syrs;
        private DateTime? _sytime;
        private DateTime? _sytime2;
        private string _adress;
        private string _sfzj;
        private string _ycreason;
        private int _nworktodoid;
        private string _caruseinfo;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string SQR
        {
            set { _sqr = value; }
            get { return _sqr; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string SQBM
        {
            set { _sqbm = value; }
            get { return _sqbm; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? SQTime
        {
            set { _sqtime = value; }
            get { return _sqtime; }
        }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string SYR
        {
            set { _syr = value; }
            get { return _syr; }
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public decimal? SYRS
        {
            set { _syrs = value; }
            get { return _syrs; }
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public DateTime? SYTime
        {
            set { _sytime = value; }
            get { return _sytime; }
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public DateTime? SYTime2
        {
            set { _sytime2 = value; }
            get { return _sytime2; }
        }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string Adress
        {
            set { _adress = value; }
            get { return _adress; }
        }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string SFZJ
        {
            set { _sfzj = value; }
            get { return _sfzj; }
        }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string YCReason
        {
            set { _ycreason = value; }
            get { return _ycreason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 所用车的信息
        /// </summary>
        public string CarUseInfo
        {
            set { _caruseinfo = value; }
            get { return _caruseinfo; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPCarUseSP(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SQR,SQBM,SQTime,SYR,SYRS,SYTime,SYTime2,Adress,SFZJ,YCReason,NWorkToDoID,CarUseInfo ");
            strSql.Append(" FROM [ERPCarUseSP] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQBM"] != null)
                {
                    this.SQBM = ds.Tables[0].Rows[0]["SQBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQTime"] != null && ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    this.SQTime = DateTime.Parse(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYR"] != null)
                {
                    this.SYR = ds.Tables[0].Rows[0]["SYR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SYRS"] != null && ds.Tables[0].Rows[0]["SYRS"].ToString() != "")
                {
                    this.SYRS = decimal.Parse(ds.Tables[0].Rows[0]["SYRS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYTime"] != null && ds.Tables[0].Rows[0]["SYTime"].ToString() != "")
                {
                    this.SYTime = DateTime.Parse(ds.Tables[0].Rows[0]["SYTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYTime2"] != null && ds.Tables[0].Rows[0]["SYTime2"].ToString() != "")
                {
                    this.SYTime2 = DateTime.Parse(ds.Tables[0].Rows[0]["SYTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Adress"] != null)
                {
                    this.Adress = ds.Tables[0].Rows[0]["Adress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFZJ"] != null)
                {
                    this.SFZJ = ds.Tables[0].Rows[0]["SFZJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YCReason"] != null)
                {
                    this.YCReason = ds.Tables[0].Rows[0]["YCReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CarUseInfo"] != null)
                {
                    this.CarUseInfo = ds.Tables[0].Rows[0]["CarUseInfo"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPCarUseSP]");
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
            strSql.Append("insert into [ERPCarUseSP] (");
            strSql.Append("SQR,SQBM,SQTime,SYR,SYRS,SYTime,SYTime2,Adress,SFZJ,YCReason,NWorkToDoID,CarUseInfo)");
            strSql.Append(" values (");
            strSql.Append("@SQR,@SQBM,@SQTime,@SYR,@SYRS,@SYTime,@SYTime2,@Adress,@SFZJ,@YCReason,@NWorkToDoID,@CarUseInfo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SQR", SqlDbType.NVarChar,50),
					new SqlParameter("@SQBM", SqlDbType.NVarChar,50),
					new SqlParameter("@SQTime", SqlDbType.DateTime),
					new SqlParameter("@SYR", SqlDbType.NVarChar,300),
					new SqlParameter("@SYRS", SqlDbType.Float,8),
					new SqlParameter("@SYTime", SqlDbType.DateTime),
					new SqlParameter("@SYTime2", SqlDbType.DateTime),
					new SqlParameter("@Adress", SqlDbType.NVarChar,300),
					new SqlParameter("@SFZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YCReason", SqlDbType.NVarChar,500),
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@CarUseInfo", SqlDbType.NVarChar,50)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQBM;
            parameters[2].Value = SQTime;
            parameters[3].Value = SYR;
            parameters[4].Value = SYRS;
            parameters[5].Value = SYTime;
            parameters[6].Value = SYTime2;
            parameters[7].Value = Adress;
            parameters[8].Value = SFZJ;
            parameters[9].Value = YCReason;
            parameters[10].Value = NWorkToDoID;
            parameters[11].Value = CarUseInfo;

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
            strSql.Append("update [ERPCarUseSP] set ");
            strSql.Append("SQR=@SQR,");
            strSql.Append("SQBM=@SQBM,");
            strSql.Append("SQTime=@SQTime,");
            strSql.Append("SYR=@SYR,");
            strSql.Append("SYRS=@SYRS,");
            strSql.Append("SYTime=@SYTime,");
            strSql.Append("SYTime2=@SYTime2,");
            strSql.Append("Adress=@Adress,");
            strSql.Append("SFZJ=@SFZJ,");
            strSql.Append("YCReason=@YCReason,");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("CarUseInfo=@CarUseInfo");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SQR", SqlDbType.NVarChar,50),
					new SqlParameter("@SQBM", SqlDbType.NVarChar,50),
					new SqlParameter("@SQTime", SqlDbType.DateTime),
					new SqlParameter("@SYR", SqlDbType.NVarChar,300),
					new SqlParameter("@SYRS", SqlDbType.Float,8),
					new SqlParameter("@SYTime", SqlDbType.DateTime),
					new SqlParameter("@SYTime2", SqlDbType.DateTime),
					new SqlParameter("@Adress", SqlDbType.NVarChar,300),
					new SqlParameter("@SFZJ", SqlDbType.NVarChar,50),
					new SqlParameter("@YCReason", SqlDbType.NVarChar,500),
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@CarUseInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQBM;
            parameters[2].Value = SQTime;
            parameters[3].Value = SYR;
            parameters[4].Value = SYRS;
            parameters[5].Value = SYTime;
            parameters[6].Value = SYTime2;
            parameters[7].Value = Adress;
            parameters[8].Value = SFZJ;
            parameters[9].Value = YCReason;
            parameters[10].Value = NWorkToDoID;
            parameters[11].Value = CarUseInfo;
            parameters[12].Value = ID;

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
            strSql.Append("delete from [ERPCarUseSP] ");
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
            strSql.Append("select ID,SQR,SQBM,SQTime,SYR,SYRS,SYTime,SYTime2,Adress,SFZJ,YCReason,NWorkToDoID,CarUseInfo ");
            strSql.Append(" FROM [ERPCarUseSP] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQBM"] != null)
                {
                    this.SQBM = ds.Tables[0].Rows[0]["SQBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQTime"] != null && ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    this.SQTime = DateTime.Parse(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYR"] != null)
                {
                    this.SYR = ds.Tables[0].Rows[0]["SYR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SYRS"] != null && ds.Tables[0].Rows[0]["SYRS"].ToString() != "")
                {
                    this.SYRS = decimal.Parse(ds.Tables[0].Rows[0]["SYRS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYTime"] != null && ds.Tables[0].Rows[0]["SYTime"].ToString() != "")
                {
                    this.SYTime = DateTime.Parse(ds.Tables[0].Rows[0]["SYTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SYTime2"] != null && ds.Tables[0].Rows[0]["SYTime2"].ToString() != "")
                {
                    this.SYTime2 = DateTime.Parse(ds.Tables[0].Rows[0]["SYTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Adress"] != null)
                {
                    this.Adress = ds.Tables[0].Rows[0]["Adress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFZJ"] != null)
                {
                    this.SFZJ = ds.Tables[0].Rows[0]["SFZJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YCReason"] != null)
                {
                    this.YCReason = ds.Tables[0].Rows[0]["YCReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CarUseInfo"] != null)
                {
                    this.CarUseInfo = ds.Tables[0].Rows[0]["CarUseInfo"].ToString();
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
            strSql.Append(" FROM [ERPCarUseSP] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPCarUseSP ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                GetModel(ID);
            }
        }
    }
}

