using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPDangBanActivityInfo。
    /// </summary>
    [Serializable]
    public partial class ERPDangBanActivityInfo
    {
        public ERPDangBanActivityInfo()
        { }
        #region Model
        private int _id;
        private string _activityname;
        private string _createperson;
        private DateTime? _createdate;
        private string _huodongdidian;
        private string _fuzeren;
        private DateTime? _enddate;
        private DateTime? _activitystartdate;
        private DateTime? _activityenddate;
        private string _activityinfo;
        private string _bz;
        private DateTime? _qdtime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 党办活动人
        /// </summary>
        public string ActivityName
        {
            set { _activityname = value; }
            get { return _activityname; }
        }
        /// <summary>
        /// 创建人/组织者
        /// </summary>
        public string CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 党办地点
        /// </summary>
        public string HuoDongDiDian
        {
            set { _huodongdidian = value; }
            get { return _huodongdidian; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string FuZeRen
        {
            set { _fuzeren = value; }
            get { return _fuzeren; }
        }
        /// <summary>
        /// 报告截止时间
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? ActivityStartDate
        {
            set { _activitystartdate = value; }
            get { return _activitystartdate; }
        }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? ActivityEndDate
        {
            set { _activityenddate = value; }
            get { return _activityenddate; }
        }
        /// <summary>
        /// 活动简介
        /// </summary>
        public string ActivityInfo
        {
            set { _activityinfo = value; }
            get { return _activityinfo; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? QDTime
        {
            set { _qdtime = value; }
            get { return _qdtime; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPDangBanActivityInfo(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ActivityName,CreatePerson,CreateDate,HuoDongDiDian,FuZeRen,EndDate,ActivityStartDate,ActivityEndDate,ActivityInfo,BZ,QDTime ");
            strSql.Append(" FROM [ERPDangBanActivityInfo] ");
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
                if (ds.Tables[0].Rows[0]["ActivityName"] != null)
                {
                    this.ActivityName = ds.Tables[0].Rows[0]["ActivityName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatePerson"] != null)
                {
                    this.CreatePerson = ds.Tables[0].Rows[0]["CreatePerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HuoDongDiDian"] != null)
                {
                    this.HuoDongDiDian = ds.Tables[0].Rows[0]["HuoDongDiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuZeRen"] != null)
                {
                    this.FuZeRen = ds.Tables[0].Rows[0]["FuZeRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    this.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityStartDate"] != null && ds.Tables[0].Rows[0]["ActivityStartDate"].ToString() != "")
                {
                    this.ActivityStartDate = DateTime.Parse(ds.Tables[0].Rows[0]["ActivityStartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityEndDate"] != null && ds.Tables[0].Rows[0]["ActivityEndDate"].ToString() != "")
                {
                    this.ActivityEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ActivityEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityInfo"] != null)
                {
                    this.ActivityInfo = ds.Tables[0].Rows[0]["ActivityInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QDTime"] != null && ds.Tables[0].Rows[0]["QDTime"].ToString() != "")
                {
                    this.QDTime = DateTime.Parse(ds.Tables[0].Rows[0]["QDTime"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPDangBanActivityInfo]");
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
            strSql.Append("insert into [ERPDangBanActivityInfo] (");
            strSql.Append("ActivityName,CreatePerson,CreateDate,HuoDongDiDian,FuZeRen,EndDate,ActivityStartDate,ActivityEndDate,ActivityInfo,BZ,QDTime)");
            strSql.Append(" values (");
            strSql.Append("@ActivityName,@CreatePerson,@CreateDate,@HuoDongDiDian,@FuZeRen,@EndDate,@ActivityStartDate,@ActivityEndDate,@ActivityInfo,@BZ,@QDTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,10),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@HuoDongDiDian", SqlDbType.NVarChar,200),
					new SqlParameter("@FuZeRen", SqlDbType.NVarChar,10),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityStartDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityEndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@BZ", SqlDbType.NVarChar,100),
					new SqlParameter("@QDTime", SqlDbType.DateTime)};
            parameters[0].Value = ActivityName;
            parameters[1].Value = CreatePerson;
            parameters[2].Value = CreateDate;
            parameters[3].Value = HuoDongDiDian;
            parameters[4].Value = FuZeRen;
            parameters[5].Value = EndDate;
            parameters[6].Value = ActivityStartDate;
            parameters[7].Value = ActivityEndDate;
            parameters[8].Value = ActivityInfo;
            parameters[9].Value = BZ;
            parameters[10].Value = QDTime;

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
            strSql.Append("update [ERPDangBanActivityInfo] set ");
            strSql.Append("ActivityName=@ActivityName,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("HuoDongDiDian=@HuoDongDiDian,");
            strSql.Append("FuZeRen=@FuZeRen,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("ActivityStartDate=@ActivityStartDate,");
            strSql.Append("ActivityEndDate=@ActivityEndDate,");
            strSql.Append("ActivityInfo=@ActivityInfo,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("QDTime=@QDTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,10),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@HuoDongDiDian", SqlDbType.NVarChar,200),
					new SqlParameter("@FuZeRen", SqlDbType.NVarChar,10),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityStartDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityEndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivityInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@BZ", SqlDbType.NVarChar,100),
					new SqlParameter("@QDTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ActivityName;
            parameters[1].Value = CreatePerson;
            parameters[2].Value = CreateDate;
            parameters[3].Value = HuoDongDiDian;
            parameters[4].Value = FuZeRen;
            parameters[5].Value = EndDate;
            parameters[6].Value = ActivityStartDate;
            parameters[7].Value = ActivityEndDate;
            parameters[8].Value = ActivityInfo;
            parameters[9].Value = BZ;
            parameters[10].Value = QDTime;
            parameters[11].Value = ID;

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
            strSql.Append("delete from [ERPDangBanActivityInfo] ");
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
            strSql.Append("select ID,ActivityName,CreatePerson,CreateDate,HuoDongDiDian,FuZeRen,EndDate,ActivityStartDate,ActivityEndDate,ActivityInfo,BZ,QDTime ");
            strSql.Append(" FROM [ERPDangBanActivityInfo] ");
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
                if (ds.Tables[0].Rows[0]["ActivityName"] != null)
                {
                    this.ActivityName = ds.Tables[0].Rows[0]["ActivityName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatePerson"] != null)
                {
                    this.CreatePerson = ds.Tables[0].Rows[0]["CreatePerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HuoDongDiDian"] != null)
                {
                    this.HuoDongDiDian = ds.Tables[0].Rows[0]["HuoDongDiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuZeRen"] != null)
                {
                    this.FuZeRen = ds.Tables[0].Rows[0]["FuZeRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    this.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityStartDate"] != null && ds.Tables[0].Rows[0]["ActivityStartDate"].ToString() != "")
                {
                    this.ActivityStartDate = DateTime.Parse(ds.Tables[0].Rows[0]["ActivityStartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityEndDate"] != null && ds.Tables[0].Rows[0]["ActivityEndDate"].ToString() != "")
                {
                    this.ActivityEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ActivityEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ActivityInfo"] != null)
                {
                    this.ActivityInfo = ds.Tables[0].Rows[0]["ActivityInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QDTime"] != null && ds.Tables[0].Rows[0]["QDTime"].ToString() != "")
                {
                    this.QDTime = DateTime.Parse(ds.Tables[0].Rows[0]["QDTime"].ToString());
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
            strSql.Append(" FROM [ERPDangBanActivityInfo] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }



        public DataSet GetActivityInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ROW_NUMBER() OVER (ORDER BY tmp.CreateDate DESC) as row, ");
            strSql.Append("(SELECT COUNT(ID) FROM [dbo].[ERPDangBanActivityAttendInfo] where ActivityID=tmp.ID) as AttendCount,");
            strSql.Append("(SELECT COUNT(ID) FROM [dbo].[ERPDangBanActivityAttendInfo] where ActivityID=tmp.ID and AttendState='参加') as RealAttendCount,");
            strSql.Append(" tmp.* from (");
            strSql.Append("SELECT a.ID,ActivityName,CreatePerson,CreateDate,HuoDongDiDian,FuZeRen,EndDate,ActivityStartDate,ActivityEndDate,STUFF((SELECT','+u.UserName FROM ");
            strSql.Append(" ERPDangBanActivityAttendInfo tmpb LEFT JOIN ERPUser u ON tmpb.AttendPerson = u.ID where a.ID =tmpb.ActivityID for xml path('')),1,1,'') AS AttendPerson");
            strSql.Append(" FROM ERPDangBanActivityInfo a LEFT JOIN ERPDangBanActivityAttendInfo b ON a.ID=b.ActivityID ) tmp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" GROUP BY tmp.ID,tmp.ActivityName,tmp.CreatePerson,tmp.CreateDate,tmp.HuoDongDiDian,tmp.FuZeRen,tmp.EndDate,tmp.ActivityStartDate,tmp.ActivityEndDate,tmp.AttendPerson ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetActivityInfoByAttendPerson(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT b.ID,a.ID as ActivityID,ActivityName,CreatePerson,CreateDate,HuoDongDiDian,FuZeRen,EndDate,ActivityStartDate,ActivityEndDate,b.AttendPerson,b.AttendState ");
            strSql.Append(" FROM ERPDangBanActivityInfo a LEFT JOIN ERPDangBanActivityAttendInfo b ON a.ID=b.ActivityID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append("  ORDER BY a.ID desc");

            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

