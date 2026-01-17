using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPAutoAlertLog。
    /// </summary>
    [Serializable]
    public partial class ERPAutoAlertLog
    {
        public ERPAutoAlertLog()
        { }
        #region Model
        private int _id;
        private int? _alertnworktodoid;
        private DateTime? _alertdate;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 自动短信催办的工作ID
        /// </summary>
        public int? AlertNWorkToDoID
        {
            set { _alertnworktodoid = value; }
            get { return _alertnworktodoid; }
        }
        /// <summary>
        /// 短信通知日期
        /// </summary>
        public DateTime? AlertDate
        {
            set { _alertdate = value; }
            get { return _alertdate; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPAutoAlertLog(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,AlertNWorkToDoID,AlertDate ");
            strSql.Append(" FROM [ERPAutoAlertLog] ");
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
                if (ds.Tables[0].Rows[0]["AlertNWorkToDoID"] != null && ds.Tables[0].Rows[0]["AlertNWorkToDoID"].ToString() != "")
                {
                    this.AlertNWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["AlertNWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertDate"] != null && ds.Tables[0].Rows[0]["AlertDate"].ToString() != "")
                {
                    this.AlertDate = DateTime.Parse(ds.Tables[0].Rows[0]["AlertDate"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPAutoAlertLog]");
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
            strSql.Append("insert into [ERPAutoAlertLog] (");
            strSql.Append("AlertNWorkToDoID,AlertDate)");
            strSql.Append(" values (");
            strSql.Append("@AlertNWorkToDoID,@AlertDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlertNWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@AlertDate", SqlDbType.DateTime)};
            parameters[0].Value = AlertNWorkToDoID;
            parameters[1].Value = AlertDate;

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
            strSql.Append("update [ERPAutoAlertLog] set ");
            strSql.Append("AlertNWorkToDoID=@AlertNWorkToDoID,");
            strSql.Append("AlertDate=@AlertDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AlertNWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@AlertDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = AlertNWorkToDoID;
            parameters[1].Value = AlertDate;
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPAutoAlertLog] ");
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
            strSql.Append("select ID,AlertNWorkToDoID,AlertDate ");
            strSql.Append(" FROM [ERPAutoAlertLog] ");
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
                if (ds.Tables[0].Rows[0]["AlertNWorkToDoID"] != null && ds.Tables[0].Rows[0]["AlertNWorkToDoID"].ToString() != "")
                {
                    this.AlertNWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["AlertNWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertDate"] != null && ds.Tables[0].Rows[0]["AlertDate"].ToString() != "")
                {
                    this.AlertDate = DateTime.Parse(ds.Tables[0].Rows[0]["AlertDate"].ToString());
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
            strSql.Append(" FROM [ERPAutoAlertLog] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 添加发送短信日志
        /// </summary>
        /// <param name="AlertNWorkToDoID"></param>
        /// <param name="AlertDate"></param>
        /// <returns></returns>
        public static int AddAlertLogs(int AlertNWorkToDoID, DateTime AlertDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPAutoAlertLog] (");
            strSql.Append("AlertNWorkToDoID,AlertDate)");
            strSql.Append(" values (");
            strSql.Append("@AlertNWorkToDoID,@AlertDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AlertNWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@AlertDate", SqlDbType.DateTime)};
            parameters[0].Value = AlertNWorkToDoID;
            parameters[1].Value = AlertDate;

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

        #endregion  Method
    }
}

