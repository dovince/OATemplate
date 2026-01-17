using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPShouWenBanWenShare。
    /// </summary>
    [Serializable]
    public partial class ERPShouWenBanWenShare
    {
        public ERPShouWenBanWenShare()
        { }
        #region Model
        private int _id;
        private string _lotid;
        private int? _docid;
        private string _titlestr;
        private string _description;
        private string _department;
        private string _username;
        private DateTime? _expiretime;
        private DateTime? _createdtime = DateTime.Now;
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
        public string LotId
        {
            set { _lotid = value; }
            get { return _lotid; }
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
        public string TitleStr
        {
            set { _titlestr = value; }
            get { return _titlestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
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
        public DateTime? ExpireTime
        {
            set { _expiretime = value; }
            get { return _expiretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPShouWenBanWenShare(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPShouWenBanWenShare] ");
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
                if (ds.Tables[0].Rows[0]["LotId"] != null)
                {
                    this.LotId = ds.Tables[0].Rows[0]["LotId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DocId"] != null && ds.Tables[0].Rows[0]["DocId"].ToString() != "")
                {
                    this.DocId = int.Parse(ds.Tables[0].Rows[0]["DocId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpireTime"] != null && ds.Tables[0].Rows[0]["ExpireTime"].ToString() != "")
                {
                    this.ExpireTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpireTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPShouWenBanWenShare]");
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
            strSql.Append("insert into [ERPShouWenBanWenShare] (");
            strSql.Append("LotId,DocId,TitleStr,Description,Department,UserName,ExpireTime,CreatedTime)");
            strSql.Append(" values (");
            strSql.Append("@LotId,@DocId,@TitleStr,@Description,@Department,@UserName,@ExpireTime,@CreatedTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotId", SqlDbType.VarChar,200),
                    new SqlParameter("@DocId", SqlDbType.Int,4),
                    new SqlParameter("@TitleStr", SqlDbType.NVarChar,500),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@Department", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpireTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
            parameters[0].Value = LotId;
            parameters[1].Value = DocId;
            parameters[2].Value = TitleStr;
            parameters[3].Value = Description;
            parameters[4].Value = Department;
            parameters[5].Value = UserName;
            parameters[6].Value = ExpireTime;
            parameters[7].Value = CreatedTime;

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
            strSql.Append("update [ERPShouWenBanWenShare] set ");
            strSql.Append("LotId=@LotId,");
            strSql.Append("DocId=@DocId,");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("Description=@Description,");
            strSql.Append("Department=@Department,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("ExpireTime=@ExpireTime,");
            strSql.Append("CreatedTime=@CreatedTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotId", SqlDbType.VarChar,200),
                    new SqlParameter("@DocId", SqlDbType.Int,4),
                    new SqlParameter("@TitleStr", SqlDbType.NVarChar,500),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@Department", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpireTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LotId;
            parameters[1].Value = DocId;
            parameters[2].Value = TitleStr;
            parameters[3].Value = Description;
            parameters[4].Value = Department;
            parameters[5].Value = UserName;
            parameters[6].Value = ExpireTime;
            parameters[7].Value = CreatedTime;
            parameters[8].Value = ID;

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
            strSql.Append("delete from [ERPShouWenBanWenShare] ");
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
            strSql.Append(" FROM [ERPShouWenBanWenShare] ");
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
                if (ds.Tables[0].Rows[0]["LotId"] != null)
                {
                    this.LotId = ds.Tables[0].Rows[0]["LotId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DocId"] != null && ds.Tables[0].Rows[0]["DocId"].ToString() != "")
                {
                    this.DocId = int.Parse(ds.Tables[0].Rows[0]["DocId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpireTime"] != null && ds.Tables[0].Rows[0]["ExpireTime"].ToString() != "")
                {
                    this.ExpireTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpireTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
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
            strSql.Append(" FROM [ERPShouWenBanWenShare] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

