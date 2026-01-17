using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPShouWenBanWenShareRecipient。
    /// </summary>
    [Serializable]
    public partial class ERPShouWenBanWenShareRecipient
    {
        public ERPShouWenBanWenShareRecipient()
        { }
        #region Model
        private int _id;
        private int? _docid;
        private string _receiver;
        private string _state;
        private DateTime? _logdate;
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
        public int? DocId
        {
            set { _docid = value; }
            get { return _docid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LogDate
        {
            set { _logdate = value; }
            get { return _logdate; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPShouWenBanWenShareRecipient(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPShouWenBanWenShareRecipient] ");
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
                if (ds.Tables[0].Rows[0]["DocId"] != null && ds.Tables[0].Rows[0]["DocId"].ToString() != "")
                {
                    this.DocId = int.Parse(ds.Tables[0].Rows[0]["DocId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Receiver"] != null)
                {
                    this.Receiver = ds.Tables[0].Rows[0]["Receiver"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LogDate"] != null && ds.Tables[0].Rows[0]["LogDate"].ToString() != "")
                {
                    this.LogDate = DateTime.Parse(ds.Tables[0].Rows[0]["LogDate"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPShouWenBanWenShareRecipient]");
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
            strSql.Append("insert into [ERPShouWenBanWenShareRecipient] (");
            strSql.Append("DocId,Receiver,State,LogDate)");
            strSql.Append(" values (");
            strSql.Append("@DocId,@Receiver,@State,@LogDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@DocId", SqlDbType.Int,4),
                    new SqlParameter("@Receiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,20),
                    new SqlParameter("@LogDate", SqlDbType.DateTime)};
            parameters[0].Value = DocId;
            parameters[1].Value = Receiver;
            parameters[2].Value = State;
            parameters[3].Value = LogDate;

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
            strSql.Append("update [ERPShouWenBanWenShareRecipient] set ");
            strSql.Append("DocId=@DocId,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("State=@State,");
            strSql.Append("LogDate=@LogDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DocId", SqlDbType.Int,4),
                    new SqlParameter("@Receiver", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,20),
                    new SqlParameter("@LogDate", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = DocId;
            parameters[1].Value = Receiver;
            parameters[2].Value = State;
            parameters[3].Value = LogDate;
            parameters[4].Value = ID;

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
            strSql.Append("delete from [ERPShouWenBanWenShareRecipient] ");
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
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPShouWenBanWenShareRecipient] ");
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
                if (ds.Tables[0].Rows[0]["DocId"] != null && ds.Tables[0].Rows[0]["DocId"].ToString() != "")
                {
                    this.DocId = int.Parse(ds.Tables[0].Rows[0]["DocId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Receiver"] != null)
                {
                    this.Receiver = ds.Tables[0].Rows[0]["Receiver"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LogDate"] != null && ds.Tables[0].Rows[0]["LogDate"].ToString() != "")
                {
                    this.LogDate = DateTime.Parse(ds.Tables[0].Rows[0]["LogDate"].ToString());
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
            strSql.Append(" FROM [ERPShouWenBanWenShareRecipient] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

