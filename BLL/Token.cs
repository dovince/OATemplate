using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类Token。
    /// </summary>
    [Serializable]
    public partial class Token
    {
        public Token()
        { }
        #region Model
        private int _id;
        private string _username;
        private string _tokenvalue;
        private string _type;
        private DateTime? _expirestime;
        private DateTime? _createdtime;
        private int? _enabledmark;
        private string _deviceid;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TokenValue
        {
            set { _tokenvalue = value; }
            get { return _tokenvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpiresTime
        {
            set { _expirestime = value; }
            get { return _expirestime; }
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
        /// 
        /// </summary>
        public int? EnabledMark
        {
            set { _enabledmark = value; }
            get { return _enabledmark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Token(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [Token] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Token]");
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
            strSql.Append("insert into [Token] (");
            strSql.Append("UserName,TokenValue,Type,ExpiresTime,CreatedTime,EnabledMark,DeviceId)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@TokenValue,@Type,@ExpiresTime,@CreatedTime,@EnabledMark,@DeviceId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TokenValue", SqlDbType.VarChar,1000),
                    new SqlParameter("@Type", SqlDbType.VarChar,255),
                    new SqlParameter("@ExpiresTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeviceId", SqlDbType.VarChar,255)};
            parameters[0].Value = UserName;
            parameters[1].Value = TokenValue;
            parameters[2].Value = Type;
            parameters[3].Value = ExpiresTime;
            parameters[4].Value = CreatedTime;
            parameters[5].Value = EnabledMark;
            parameters[6].Value = DeviceId;

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
            strSql.Append("update [Token] set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TokenValue=@TokenValue,");
            strSql.Append("Type=@Type,");
            strSql.Append("ExpiresTime=@ExpiresTime,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("EnabledMark=@EnabledMark,");
            strSql.Append("DeviceId=@DeviceId");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@TokenValue", SqlDbType.VarChar,1000),
                    new SqlParameter("@Type", SqlDbType.VarChar,255),
                    new SqlParameter("@ExpiresTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeviceId", SqlDbType.VarChar,255),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = UserName;
            parameters[1].Value = TokenValue;
            parameters[2].Value = Type;
            parameters[3].Value = ExpiresTime;
            parameters[4].Value = CreatedTime;
            parameters[5].Value = EnabledMark;
            parameters[6].Value = DeviceId;
            parameters[7].Value = ID;

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
            strSql.Append("delete from [Token] ");
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
            strSql.Append(" FROM [Token] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

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
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TokenValue"] != null)
                {
                    this.TokenValue = ds.Tables[0].Rows[0]["TokenValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null)
                {
                    this.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpiresTime"] != null && ds.Tables[0].Rows[0]["ExpiresTime"].ToString() != "")
                {
                    this.ExpiresTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpiresTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
                {
                    this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeviceId"] != null)
                {
                    this.DeviceId = ds.Tables[0].Rows[0]["DeviceId"].ToString();
                }
            }
        }

        public void GetModel(string token)
        {
            //DataTableHelper.CreateItem<ZWL.BLL.Token>
            var ds = GetList("[TokenValue]='" + token + "'");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                SetPropertyValue(ds);
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [Token] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

