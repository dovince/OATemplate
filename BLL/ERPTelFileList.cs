using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPTelFileList。
    /// </summary>
    [Serializable]
    public partial class ERPTelFileList
    {
        public ERPTelFileList()
        { }
        #region Model
        private int _id;
        private string _fileuploadid;
        private string _oldname;
        private string _newname;
        private string _path;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 传阅文件的ID
        /// </summary>
        public string FileUploadID
        {
            set { _fileuploadid = value; }
            get { return _fileuploadid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldName
        {
            set { _oldname = value; }
            get { return _oldname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NewName
        {
            set { _newname = value; }
            get { return _newname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPTelFileList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,FileUploadID,OldName,NewName,Path ");
            strSql.Append(" FROM [ERPTelFileList] ");
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
                if (ds.Tables[0].Rows[0]["FileUploadID"] != null)
                {
                    this.FileUploadID = ds.Tables[0].Rows[0]["FileUploadID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldName"] != null)
                {
                    this.OldName = ds.Tables[0].Rows[0]["OldName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NewName"] != null)
                {
                    this.NewName = ds.Tables[0].Rows[0]["NewName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Path"] != null)
                {
                    this.Path = ds.Tables[0].Rows[0]["Path"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPTelFileList]");
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
            strSql.Append("insert into [ERPTelFileList] (");
            strSql.Append("FileUploadID,OldName,NewName,Path)");
            strSql.Append(" values (");
            strSql.Append("@FileUploadID,@OldName,@NewName,@Path)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FileUploadID", SqlDbType.NVarChar,-1),
					new SqlParameter("@OldName", SqlDbType.NVarChar,-1),
					new SqlParameter("@NewName", SqlDbType.NVarChar,-1),
					new SqlParameter("@Path", SqlDbType.NVarChar,-1)};
            parameters[0].Value = FileUploadID;
            parameters[1].Value = OldName;
            parameters[2].Value = NewName;
            parameters[3].Value = Path;

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
            strSql.Append("update [ERPTelFileList] set ");
            strSql.Append("FileUploadID=@FileUploadID,");
            strSql.Append("OldName=@OldName,");
            strSql.Append("NewName=@NewName,");
            strSql.Append("Path=@Path");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FileUploadID", SqlDbType.NVarChar,-1),
					new SqlParameter("@OldName", SqlDbType.NVarChar,-1),
					new SqlParameter("@NewName", SqlDbType.NVarChar,-1),
					new SqlParameter("@Path", SqlDbType.NVarChar,-1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FileUploadID;
            parameters[1].Value = OldName;
            parameters[2].Value = NewName;
            parameters[3].Value = Path;
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
            strSql.Append("delete from [ERPTelFileList] ");
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
            strSql.Append("select ID,FileUploadID,OldName,NewName,Path ");
            strSql.Append(" FROM [ERPTelFileList] ");
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
                if (ds.Tables[0].Rows[0]["FileUploadID"] != null)
                {
                    this.FileUploadID = ds.Tables[0].Rows[0]["FileUploadID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldName"] != null)
                {
                    this.OldName = ds.Tables[0].Rows[0]["OldName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NewName"] != null)
                {
                    this.NewName = ds.Tables[0].Rows[0]["NewName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Path"] != null)
                {
                    this.Path = ds.Tables[0].Rows[0]["Path"].ToString();
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
            strSql.Append(" FROM [ERPTelFileList] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

