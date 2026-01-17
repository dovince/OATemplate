using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类TokenLogs。
    /// </summary>
    [Serializable]
    public partial class TokenLogs
    {
        public TokenLogs()
        { }
        #region Model
        private int _id;
        private int? _tokenid;
        private string _ipaddress;
        private DateTime? _accessedtime;
        private string _action;
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
        public int? TokenID
        {
            set { _tokenid = value; }
            get { return _tokenid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IPAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AccessedTime
        {
            set { _accessedtime = value; }
            get { return _accessedtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Action
        {
            set { _action = value; }
            get { return _action; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TokenLogs(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TokenID,IPAddress,AccessedTime,Action ");
            strSql.Append(" FROM [TokenLogs] ");
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
                if (ds.Tables[0].Rows[0]["TokenID"] != null && ds.Tables[0].Rows[0]["TokenID"].ToString() != "")
                {
                    this.TokenID = int.Parse(ds.Tables[0].Rows[0]["TokenID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IPAddress"] != null)
                {
                    this.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AccessedTime"] != null && ds.Tables[0].Rows[0]["AccessedTime"].ToString() != "")
                {
                    this.AccessedTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccessedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Action"] != null)
                {
                    this.Action = ds.Tables[0].Rows[0]["Action"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [TokenLogs]");
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
            strSql.Append("insert into [TokenLogs] (");
            strSql.Append("TokenID,IPAddress,AccessedTime,Action)");
            strSql.Append(" values (");
            strSql.Append("@TokenID,@IPAddress,@AccessedTime,@Action)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@TokenID", SqlDbType.Int,4),
                    new SqlParameter("@IPAddress", SqlDbType.VarChar,255),
                    new SqlParameter("@AccessedTime", SqlDbType.DateTime),
                    new SqlParameter("@Action", SqlDbType.VarChar,255)};
            parameters[0].Value = TokenID;
            parameters[1].Value = IPAddress;
            parameters[2].Value = AccessedTime;
            parameters[3].Value = Action;

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
            strSql.Append("update [TokenLogs] set ");
            strSql.Append("TokenID=@TokenID,");
            strSql.Append("IPAddress=@IPAddress,");
            strSql.Append("AccessedTime=@AccessedTime,");
            strSql.Append("Action=@Action");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TokenID", SqlDbType.Int,4),
                    new SqlParameter("@IPAddress", SqlDbType.VarChar,255),
                    new SqlParameter("@AccessedTime", SqlDbType.DateTime),
                    new SqlParameter("@Action", SqlDbType.VarChar,255),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = TokenID;
            parameters[1].Value = IPAddress;
            parameters[2].Value = AccessedTime;
            parameters[3].Value = Action;
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
            strSql.Append("delete from [TokenLogs] ");
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
            strSql.Append("select ID,TokenID,IPAddress,AccessedTime,Action ");
            strSql.Append(" FROM [TokenLogs] ");
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
                if (ds.Tables[0].Rows[0]["TokenID"] != null && ds.Tables[0].Rows[0]["TokenID"].ToString() != "")
                {
                    this.TokenID = int.Parse(ds.Tables[0].Rows[0]["TokenID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IPAddress"] != null)
                {
                    this.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AccessedTime"] != null && ds.Tables[0].Rows[0]["AccessedTime"].ToString() != "")
                {
                    this.AccessedTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccessedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Action"] != null)
                {
                    this.Action = ds.Tables[0].Rows[0]["Action"].ToString();
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
            strSql.Append(" FROM [TokenLogs] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

