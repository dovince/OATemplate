using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    /// <summary>
	/// 类ValidateCode。
	/// </summary>
	[Serializable]
    public partial class ValidateCode
    {
        public ValidateCode()
        { }
        #region Model
        private int _id;
        private string _username;
        private DateTime? _time;
        private string _code;
        private string _phonenumber;
        private DateTime? _expirestime;
        private int? _isused;
        private string _item;
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
        /// 发送时间
        /// </summary>
        public DateTime? Time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 短信验证码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber
        {
            set { _phonenumber = value; }
            get { return _phonenumber; }
        }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? ExpiresTime
        {
            set { _expirestime = value; }
            get { return _expirestime; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        public int? IsUsed
        {
            set { _isused = value; }
            get { return _isused; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string Item
        {
            set { _item = value; }
            get { return _item; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ValidateCode(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ValidateCode] ");
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
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Time"] != null && ds.Tables[0].Rows[0]["Time"].ToString() != "")
                {
                    this.Time = DateTime.Parse(ds.Tables[0].Rows[0]["Time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Code"] != null)
                {
                    this.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PhoneNumber"] != null)
                {
                    this.PhoneNumber = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpiresTime"] != null && ds.Tables[0].Rows[0]["ExpiresTime"].ToString() != "")
                {
                    this.ExpiresTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpiresTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
                {
                    this.IsUsed = int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Item"] != null)
                {
                    this.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ValidateCode]");
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
            strSql.Append("insert into [ValidateCode] (");
            strSql.Append("UserName,Time,Code,PhoneNumber,ExpiresTime,IsUsed,Item)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@Time,@Code,@PhoneNumber,@ExpiresTime,@IsUsed,@Item)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Time", SqlDbType.DateTime),
                    new SqlParameter("@Code", SqlDbType.NVarChar,50),
                    new SqlParameter("@PhoneNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@ExpiresTime", SqlDbType.DateTime),
                    new SqlParameter("@IsUsed", SqlDbType.Int,4),
                    new SqlParameter("@Item", SqlDbType.VarChar,255)};
            parameters[0].Value = UserName;
            parameters[1].Value = Time;
            parameters[2].Value = Code;
            parameters[3].Value = PhoneNumber;
            parameters[4].Value = ExpiresTime;
            parameters[5].Value = IsUsed;
            parameters[6].Value = Item;

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
            strSql.Append("update [ValidateCode] set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Time=@Time,");
            strSql.Append("Code=@Code,");
            strSql.Append("PhoneNumber=@PhoneNumber,");
            strSql.Append("ExpiresTime=@ExpiresTime,");
            strSql.Append("IsUsed=@IsUsed,");
            strSql.Append("Item=@Item");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Time", SqlDbType.DateTime),
                    new SqlParameter("@Code", SqlDbType.NVarChar,50),
                    new SqlParameter("@PhoneNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@ExpiresTime", SqlDbType.DateTime),
                    new SqlParameter("@IsUsed", SqlDbType.Int,4),
                    new SqlParameter("@Item", SqlDbType.VarChar,255),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = UserName;
            parameters[1].Value = Time;
            parameters[2].Value = Code;
            parameters[3].Value = PhoneNumber;
            parameters[4].Value = ExpiresTime;
            parameters[5].Value = IsUsed;
            parameters[6].Value = Item;
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
            strSql.Append("delete from [ValidateCode] ");
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
            strSql.Append("select ID,UserName,Time,Code,PhoneNumber,ExpiresTime,IsUsed,Item ");
            strSql.Append(" FROM [ValidateCode] ");
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
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Time"] != null && ds.Tables[0].Rows[0]["Time"].ToString() != "")
                {
                    this.Time = DateTime.Parse(ds.Tables[0].Rows[0]["Time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Code"] != null)
                {
                    this.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PhoneNumber"] != null)
                {
                    this.PhoneNumber = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpiresTime"] != null && ds.Tables[0].Rows[0]["ExpiresTime"].ToString() != "")
                {
                    this.ExpiresTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpiresTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
                {
                    this.IsUsed = int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Item"] != null)
                {
                    this.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                }
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM ValidateCode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }

}
