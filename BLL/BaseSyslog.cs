using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类BaseSyslog。
	/// </summary>
	[Serializable]
    public partial class BaseSyslog
    {
        public BaseSyslog()
        { }
        #region Model
        private int _id;
        private string _userid;
        private string _username;
        private int? _category;
        private int? _type;
        private int? _level;
        private string _ipaddress;
        private string _ipaddressname;
        private string _requesturl;
        private string _requestmethod;
        private int? _requestduration;
        private string _abstracts;
        private string _json;
        private string _platform;
        private DateTime? _creatortime;
        private string _moduleid;
        private string _modulename;
        private string _objectid;
        /// <summary>
        /// 自然主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 日志分类
        /// </summary>
        public int? Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 日志级别
        /// </summary>
        public int? Level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// IP所在城市
        /// </summary>
        public string IPAddressName
        {
            set { _ipaddressname = value; }
            get { return _ipaddressname; }
        }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestURL
        {
            set { _requesturl = value; }
            get { return _requesturl; }
        }
        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod
        {
            set { _requestmethod = value; }
            get { return _requestmethod; }
        }
        /// <summary>
        /// 请求耗时
        /// </summary>
        public int? RequestDuration
        {
            set { _requestduration = value; }
            get { return _requestduration; }
        }
        /// <summary>
        /// 日志摘要
        /// </summary>
        public string Abstracts
        {
            set { _abstracts = value; }
            get { return _abstracts; }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Json
        {
            set { _json = value; }
            get { return _json; }
        }
        /// <summary>
        /// 平台设备
        /// </summary>
        public string PlatForm
        {
            set { _platform = value; }
            get { return _platform; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? CreatorTime
        {
            set { _creatortime = value; }
            get { return _creatortime; }
        }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        /// <summary>
        /// 对象Id
        /// </summary>
        public string ObjectId
        {
            set { _objectid = value; }
            get { return _objectid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseSyslog(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [BaseSyslog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                if (ds.Tables[0].Rows[0]["UserId"] != null)
                {
                    this.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Category"] != null && ds.Tables[0].Rows[0]["Category"].ToString() != "")
                {
                    this.Category = int.Parse(ds.Tables[0].Rows[0]["Category"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Level"] != null && ds.Tables[0].Rows[0]["Level"].ToString() != "")
                {
                    this.Level = int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IPAddress"] != null)
                {
                    this.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IPAddressName"] != null)
                {
                    this.IPAddressName = ds.Tables[0].Rows[0]["IPAddressName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestURL"] != null)
                {
                    this.RequestURL = ds.Tables[0].Rows[0]["RequestURL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestMethod"] != null)
                {
                    this.RequestMethod = ds.Tables[0].Rows[0]["RequestMethod"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestDuration"] != null && ds.Tables[0].Rows[0]["RequestDuration"].ToString() != "")
                {
                    this.RequestDuration = int.Parse(ds.Tables[0].Rows[0]["RequestDuration"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Abstracts"] != null)
                {
                    this.Abstracts = ds.Tables[0].Rows[0]["Abstracts"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Json"] != null)
                {
                    this.Json = ds.Tables[0].Rows[0]["Json"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PlatForm"] != null)
                {
                    this.PlatForm = ds.Tables[0].Rows[0]["PlatForm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
                {
                    this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModuleId"] != null)
                {
                    this.ModuleId = ds.Tables[0].Rows[0]["ModuleId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ModuleName"] != null)
                {
                    this.ModuleName = ds.Tables[0].Rows[0]["ModuleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ObjectId"] != null)
                {
                    this.ObjectId = ds.Tables[0].Rows[0]["ObjectId"].ToString();
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [BaseSyslog]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [BaseSyslog] (");
            strSql.Append("ID,UserId,UserName,Category,Type,Level,IPAddress,IPAddressName,RequestURL,RequestMethod,RequestDuration,Abstracts,Json,PlatForm,CreatorTime,ModuleId,ModuleName,ObjectId)");
            strSql.Append(" values (");
            strSql.Append("@ID,@UserId,@UserName,@Category,@Type,@Level,@IPAddress,@IPAddressName,@RequestURL,@RequestMethod,@RequestDuration,@Abstracts,@Json,@PlatForm,@CreatorTime,@ModuleId,@ModuleName,@ObjectId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt,8),
                    new SqlParameter("@UserId", SqlDbType.VarChar,50),
                    new SqlParameter("@UserName", SqlDbType.VarChar,100),
                    new SqlParameter("@Category", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@Level", SqlDbType.Int,4),
                    new SqlParameter("@IPAddress", SqlDbType.VarChar,100),
                    new SqlParameter("@IPAddressName", SqlDbType.VarChar,100),
                    new SqlParameter("@RequestURL", SqlDbType.VarChar,-1),
                    new SqlParameter("@RequestMethod", SqlDbType.VarChar,50),
                    new SqlParameter("@RequestDuration", SqlDbType.Int,4),
                    new SqlParameter("@Abstracts", SqlDbType.VarChar,-1),
                    new SqlParameter("@Json", SqlDbType.VarChar,-1),
                    new SqlParameter("@PlatForm", SqlDbType.VarChar,-1),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@ModuleId", SqlDbType.VarChar,50),
                    new SqlParameter("@ModuleName", SqlDbType.VarChar,50),
                    new SqlParameter("@ObjectId", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = UserId;
            parameters[2].Value = UserName;
            parameters[3].Value = Category;
            parameters[4].Value = Type;
            parameters[5].Value = Level;
            parameters[6].Value = IPAddress;
            parameters[7].Value = IPAddressName;
            parameters[8].Value = RequestURL;
            parameters[9].Value = RequestMethod;
            parameters[10].Value = RequestDuration;
            parameters[11].Value = Abstracts;
            parameters[12].Value = Json;
            parameters[13].Value = PlatForm;
            parameters[14].Value = CreatorTime;
            parameters[15].Value = ModuleId;
            parameters[16].Value = ModuleName;
            parameters[17].Value = ObjectId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [BaseSyslog] set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Category=@Category,");
            strSql.Append("Type=@Type,");
            strSql.Append("Level=@Level,");
            strSql.Append("IPAddress=@IPAddress,");
            strSql.Append("IPAddressName=@IPAddressName,");
            strSql.Append("RequestURL=@RequestURL,");
            strSql.Append("RequestMethod=@RequestMethod,");
            strSql.Append("RequestDuration=@RequestDuration,");
            strSql.Append("Abstracts=@Abstracts,");
            strSql.Append("Json=@Json,");
            strSql.Append("PlatForm=@PlatForm,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("ModuleId=@ModuleId,");
            strSql.Append("ModuleName=@ModuleName,");
            strSql.Append("ObjectId=@ObjectId");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.VarChar,50),
                    new SqlParameter("@UserName", SqlDbType.VarChar,100),
                    new SqlParameter("@Category", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@Level", SqlDbType.Int,4),
                    new SqlParameter("@IPAddress", SqlDbType.VarChar,100),
                    new SqlParameter("@IPAddressName", SqlDbType.VarChar,100),
                    new SqlParameter("@RequestURL", SqlDbType.VarChar,-1),
                    new SqlParameter("@RequestMethod", SqlDbType.VarChar,50),
                    new SqlParameter("@RequestDuration", SqlDbType.Int,4),
                    new SqlParameter("@Abstracts", SqlDbType.VarChar,-1),
                    new SqlParameter("@Json", SqlDbType.VarChar,-1),
                    new SqlParameter("@PlatForm", SqlDbType.VarChar,-1),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@ModuleId", SqlDbType.VarChar,50),
                    new SqlParameter("@ModuleName", SqlDbType.VarChar,50),
                    new SqlParameter("@ObjectId", SqlDbType.VarChar,50),
                    new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = UserId;
            parameters[1].Value = UserName;
            parameters[2].Value = Category;
            parameters[3].Value = Type;
            parameters[4].Value = Level;
            parameters[5].Value = IPAddress;
            parameters[6].Value = IPAddressName;
            parameters[7].Value = RequestURL;
            parameters[8].Value = RequestMethod;
            parameters[9].Value = RequestDuration;
            parameters[10].Value = Abstracts;
            parameters[11].Value = Json;
            parameters[12].Value = PlatForm;
            parameters[13].Value = CreatorTime;
            parameters[14].Value = ModuleId;
            parameters[15].Value = ModuleName;
            parameters[16].Value = ObjectId;
            parameters[17].Value = ID;

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
        public bool Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [BaseSyslog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt)};
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
        public void GetModel(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [BaseSyslog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"] != null)
                {
                    this.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Category"] != null && ds.Tables[0].Rows[0]["Category"].ToString() != "")
                {
                    this.Category = int.Parse(ds.Tables[0].Rows[0]["Category"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Level"] != null && ds.Tables[0].Rows[0]["Level"].ToString() != "")
                {
                    this.Level = int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IPAddress"] != null)
                {
                    this.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IPAddressName"] != null)
                {
                    this.IPAddressName = ds.Tables[0].Rows[0]["IPAddressName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestURL"] != null)
                {
                    this.RequestURL = ds.Tables[0].Rows[0]["RequestURL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestMethod"] != null)
                {
                    this.RequestMethod = ds.Tables[0].Rows[0]["RequestMethod"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestDuration"] != null && ds.Tables[0].Rows[0]["RequestDuration"].ToString() != "")
                {
                    this.RequestDuration = int.Parse(ds.Tables[0].Rows[0]["RequestDuration"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Abstracts"] != null)
                {
                    this.Abstracts = ds.Tables[0].Rows[0]["Abstracts"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Json"] != null)
                {
                    this.Json = ds.Tables[0].Rows[0]["Json"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PlatForm"] != null)
                {
                    this.PlatForm = ds.Tables[0].Rows[0]["PlatForm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
                {
                    this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModuleId"] != null)
                {
                    this.ModuleId = ds.Tables[0].Rows[0]["ModuleId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ModuleName"] != null)
                {
                    this.ModuleName = ds.Tables[0].Rows[0]["ModuleName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ObjectId"] != null)
                {
                    this.ObjectId = ds.Tables[0].Rows[0]["ObjectId"].ToString();
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
            strSql.Append(" FROM [BaseSyslog] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
