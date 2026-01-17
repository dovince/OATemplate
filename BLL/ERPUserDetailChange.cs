using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    public partial class ERPUserDetailChange
    {
        public ERPUserDetailChange()
        { }
        #region Model
        private int _id;
        private int _detailid;
        private string _name;
        private DateTime _dengjitime;
        private string _changetype;
        private string _changelog;
        private DateTime _changetime;
        private string _bz1;
        private string _bz2;
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
        public int DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DengJiTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChangeType
        {
            set { _changetype = value; }
            get { return _changetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChangeLog
        {
            set { _changelog = value; }
            get { return _changelog; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ChangeTime
        {
            set { _changetime = value; }
            get { return _changetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ1
        {
            set { _bz1 = value; }
            get { return _bz1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ2
        {
            set { _bz2 = value; }
            get { return _bz2; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPUserDetailChange(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DetailID,Name,DengJiTime,ChangeType,ChangeLog,ChangeTime,BZ1,BZ2 ");
            strSql.Append(" FROM [ERPUserDetailChange] ");
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
                if (ds.Tables[0].Rows[0]["DetailID"] != null && ds.Tables[0].Rows[0]["DetailID"].ToString() != "")
                {
                    this.DetailID = int.Parse(ds.Tables[0].Rows[0]["DetailID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeType"] != null)
                {
                    this.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeLog"] != null)
                {
                    this.ChangeLog = ds.Tables[0].Rows[0]["ChangeLog"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeTime"] != null && ds.Tables[0].Rows[0]["ChangeTime"].ToString() != "")
                {
                    this.ChangeTime = DateTime.Parse(ds.Tables[0].Rows[0]["ChangeTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ1"] != null)
                {
                    this.BZ1 = ds.Tables[0].Rows[0]["BZ1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ2"] != null)
                {
                    this.BZ2 = ds.Tables[0].Rows[0]["BZ2"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPUserDetailChange]");
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
            strSql.Append("insert into [ERPUserDetailChange] (");
            strSql.Append("DetailID,Name,DengJiTime,ChangeType,ChangeLog,ChangeTime,BZ1,BZ2)");
            strSql.Append(" values (");
            strSql.Append("@DetailID,@Name,@DengJiTime,@ChangeType,@ChangeLog,@ChangeTime,@BZ1,@BZ2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
					new SqlParameter("@ChangeType", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeLog", SqlDbType.NVarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@BZ1", SqlDbType.NVarChar,200),
					new SqlParameter("@BZ2", SqlDbType.NVarChar,200)};
            parameters[0].Value = DetailID;
            parameters[1].Value = Name;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = ChangeType;
            parameters[4].Value = ChangeLog;
            parameters[5].Value = ChangeTime;
            parameters[6].Value = BZ1;
            parameters[7].Value = BZ2;

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
            strSql.Append("update [ERPUserDetailChange] set ");
            strSql.Append("DetailID=@DetailID,");
            strSql.Append("Name=@Name,");
            strSql.Append("DengJiTime=@DengJiTime,");
            strSql.Append("ChangeType=@ChangeType,");
            strSql.Append("ChangeLog=@ChangeLog,");
            strSql.Append("ChangeTime=@ChangeTime,");
            strSql.Append("BZ1=@BZ1,");
            strSql.Append("BZ2=@BZ2");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DetailID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
					new SqlParameter("@ChangeType", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeLog", SqlDbType.NVarChar,200),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@BZ1", SqlDbType.NVarChar,200),
					new SqlParameter("@BZ2", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = DetailID;
            parameters[1].Value = Name;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = ChangeType;
            parameters[4].Value = ChangeLog;
            parameters[5].Value = ChangeTime;
            parameters[6].Value = BZ1;
            parameters[7].Value = BZ2;
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
            strSql.Append("delete from [ERPUserDetailChange] ");
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
            strSql.Append("select ID,DetailID,Name,DengJiTime,ChangeType,ChangeLog,ChangeTime,BZ1,BZ2 ");
            strSql.Append(" FROM [ERPUserDetailChange] ");
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
                if (ds.Tables[0].Rows[0]["DetailID"] != null && ds.Tables[0].Rows[0]["DetailID"].ToString() != "")
                {
                    this.DetailID = int.Parse(ds.Tables[0].Rows[0]["DetailID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeType"] != null)
                {
                    this.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeLog"] != null)
                {
                    this.ChangeLog = ds.Tables[0].Rows[0]["ChangeLog"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeTime"] != null && ds.Tables[0].Rows[0]["ChangeTime"].ToString() != "")
                {
                    this.ChangeTime = DateTime.Parse(ds.Tables[0].Rows[0]["ChangeTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ1"] != null)
                {
                    this.BZ1 = ds.Tables[0].Rows[0]["BZ1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ2"] != null)
                {
                    this.BZ2 = ds.Tables[0].Rows[0]["BZ2"].ToString();
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
            strSql.Append(" FROM [ERPUserDetailChange] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
