using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPDangBanActivityAttendInfo。
    /// </summary>
    [Serializable]
    public class ERPDangBanActivityAttendInfo
    {
        public ERPDangBanActivityAttendInfo()
        { }
        #region Model
        private int _id;
        private int? _activityid;
        private string _attendperson;
        private DateTime? _checkintime;
        private string _attendstate;
        private int? _qingjianworkid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 党办活动ID
        /// </summary>
        public int? ActivityID
        {
            set { _activityid = value; }
            get { return _activityid; }
        }
        /// <summary>
        /// 参加人
        /// </summary>
        public string AttendPerson
        {
            set { _attendperson = value; }
            get { return _attendperson; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? CheckInTime
        {
            set { _checkintime = value; }
            get { return _checkintime; }
        }
        /// <summary>
        /// 参加状态（参加、不参加、请假、未确认）
        /// </summary>
        public string AttendState
        {
            set { _attendstate = value; }
            get { return _attendstate; }
        }
        /// <summary>
        /// 请假单审批ID
        /// </summary>
        public int? QingJiaNworkID
        {
            set { _qingjianworkid = value; }
            get { return _qingjianworkid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPDangBanActivityAttendInfo(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ActivityID,AttendPerson,CheckInTime,AttendState,QingJiaNworkID ");
            strSql.Append(" FROM [ERPDangBanActivityAttendInfo] ");
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
                if (ds.Tables[0].Rows[0]["ActivityID"] != null && ds.Tables[0].Rows[0]["ActivityID"].ToString() != "")
                {
                    this.ActivityID = int.Parse(ds.Tables[0].Rows[0]["ActivityID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttendPerson"] != null)
                {
                    this.AttendPerson = ds.Tables[0].Rows[0]["AttendPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckInTime"] != null && ds.Tables[0].Rows[0]["CheckInTime"].ToString() != "")
                {
                    this.CheckInTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckInTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttendState"] != null)
                {
                    this.AttendState = ds.Tables[0].Rows[0]["AttendState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QingJiaNworkID"] != null && ds.Tables[0].Rows[0]["QingJiaNworkID"].ToString() != "")
                {
                    this.QingJiaNworkID = int.Parse(ds.Tables[0].Rows[0]["QingJiaNworkID"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPDangBanActivityAttendInfo]");
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
            strSql.Append("insert into [ERPDangBanActivityAttendInfo] (");
            strSql.Append("ActivityID,AttendPerson,CheckInTime,AttendState,QingJiaNworkID)");
            strSql.Append(" values (");
            strSql.Append("@ActivityID,@AttendPerson,@CheckInTime,@AttendState,@QingJiaNworkID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ActivityID", SqlDbType.Int,4),
                    new SqlParameter("@AttendPerson", SqlDbType.NVarChar,10),
                    new SqlParameter("@CheckInTime", SqlDbType.DateTime),
                    new SqlParameter("@AttendState", SqlDbType.NVarChar,5),
                    new SqlParameter("@QingJiaNworkID", SqlDbType.Int,4)};
            parameters[0].Value = ActivityID;
            parameters[1].Value = AttendPerson;
            parameters[2].Value = CheckInTime;
            parameters[3].Value = AttendState;
            parameters[4].Value = QingJiaNworkID;

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
            strSql.Append("update [ERPDangBanActivityAttendInfo] set ");
            strSql.Append("ActivityID=@ActivityID,");
            strSql.Append("AttendPerson=@AttendPerson,");
            strSql.Append("CheckInTime=@CheckInTime,");
            strSql.Append("AttendState=@AttendState,");
            strSql.Append("QingJiaNworkID=@QingJiaNworkID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ActivityID", SqlDbType.Int,4),
                    new SqlParameter("@AttendPerson", SqlDbType.NVarChar,10),
                    new SqlParameter("@CheckInTime", SqlDbType.DateTime),
                    new SqlParameter("@AttendState", SqlDbType.NVarChar,5),
                    new SqlParameter("@QingJiaNworkID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ActivityID;
            parameters[1].Value = AttendPerson;
            parameters[2].Value = CheckInTime;
            parameters[3].Value = AttendState;
            parameters[4].Value = QingJiaNworkID;
            parameters[5].Value = ID;

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
            strSql.Append("delete from [ERPDangBanActivityAttendInfo] ");
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
            strSql.Append("select ID,ActivityID,AttendPerson,CheckInTime,AttendState,QingJiaNworkID ");
            strSql.Append(" FROM [ERPDangBanActivityAttendInfo] ");
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
                if (ds.Tables[0].Rows[0]["ActivityID"] != null && ds.Tables[0].Rows[0]["ActivityID"].ToString() != "")
                {
                    this.ActivityID = int.Parse(ds.Tables[0].Rows[0]["ActivityID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttendPerson"] != null)
                {
                    this.AttendPerson = ds.Tables[0].Rows[0]["AttendPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckInTime"] != null && ds.Tables[0].Rows[0]["CheckInTime"].ToString() != "")
                {
                    this.CheckInTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckInTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttendState"] != null)
                {
                    this.AttendState = ds.Tables[0].Rows[0]["AttendState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QingJiaNworkID"] != null && ds.Tables[0].Rows[0]["QingJiaNworkID"].ToString() != "")
                {
                    this.QingJiaNworkID = int.Parse(ds.Tables[0].Rows[0]["QingJiaNworkID"].ToString());
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
            strSql.Append(" FROM [ERPDangBanActivityAttendInfo] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAttendInfo(string ActivityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.*,b.ZhengZhiMianMao,ZhiWuQingKuang,b.UserName ");
            strSql.Append(" FROM [dbo].[ERPDangBanActivityAttendInfo] a left join ERPUser b on a.AttendPerson = b.ID ");
            strSql.Append(" where a.ActivityID = '" + ActivityID + "' order by case when a.AttendState='参加' then 1 else 0 end asc, a.AttendState asc ");
            
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 根据ID更新参加状态
        /// </summary>
        /// <param name="AttendState"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateAttendState(string AttendState, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPDangBanActivityAttendInfo] set ");
            strSql.Append("AttendState=@AttendState");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@AttendState", SqlDbType.NVarChar,5),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = AttendState;
            parameters[1].Value = ID;
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
        /// 设置请假NworkToDoID
        /// </summary>
        /// <param name="QingJiaNworkID"></param>
        /// <param name="ID"></param>
        /// <param name="AttendState"></param>
        /// <returns></returns>
        public static bool UpdateQingJiaID(int QingJiaNworkID, int ID,string AttendState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPDangBanActivityAttendInfo] set ");
            strSql.Append("QingJiaNworkID=@QingJiaNworkID,");
            strSql.Append("AttendState=@AttendState");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@QingJiaNworkID", SqlDbType.Int,4),
                    new SqlParameter("@AttendState", SqlDbType.NVarChar,5),
                    new SqlParameter("@ID", SqlDbType.Int,4) };
            parameters[0].Value = QingJiaNworkID;
            parameters[1].Value = AttendState;
            parameters[2].Value = ID;
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


        #endregion  Method
    }
}

