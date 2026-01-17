using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;
using ZWL.DBUtility;

//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPProjectRequire。
    /// </summary>
    public partial class ERPProjectRequire
    {
        public ERPProjectRequire()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _tablename;
        private string _username;
        private DateTime _createdate;
        private DateTime _changedate;
        private string _bz;
        private string _formid;
        private string _workflowid;
        private string _xmname;
        private string _xmrootpath;
        private string _dbname;
        private string _beiyong1;
        private string _beforeupdatecode;
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
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
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ChangeDate
        {
            set { _changedate = value; }
            get { return _changedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkFlowId
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMRootPath
        {
            set { _xmrootpath = value; }
            get { return _xmrootpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DbName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Beiyong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeforeUpdateCode
        {
            set { _beforeupdatecode = value; }
            get { return _beforeupdatecode; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPProjectRequire(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,TableName,UserName,CreateDate,ChangeDate,BZ,FormId,WorkFlowId,XMName,XMRootPath,DbName,Beiyong1,BeforeUpdateCode ");
            strSql.Append(" FROM [ERPProjectRequire] ");
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
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TableName"] != null)
                {
                    this.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeDate"] != null && ds.Tables[0].Rows[0]["ChangeDate"].ToString() != "")
                {
                    this.ChangeDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChangeDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormId"] != null)
                {
                    this.FormId = ds.Tables[0].Rows[0]["FormId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkFlowId"] != null)
                {
                    this.WorkFlowId = ds.Tables[0].Rows[0]["WorkFlowId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMRootPath"] != null)
                {
                    this.XMRootPath = ds.Tables[0].Rows[0]["XMRootPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DbName"] != null)
                {
                    this.DbName = ds.Tables[0].Rows[0]["DbName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Beiyong1"] != null)
                {
                    this.Beiyong1 = ds.Tables[0].Rows[0]["Beiyong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BeforeUpdateCode"] != null)
                {
                    this.BeforeUpdateCode = ds.Tables[0].Rows[0]["BeforeUpdateCode"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPProjectRequire]");
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
            strSql.Append("insert into [ERPProjectRequire] (");
            strSql.Append("Name,TableName,UserName,CreateDate,ChangeDate,BZ,FormId,WorkFlowId,XMName,XMRootPath,DbName,Beiyong1,BeforeUpdateCode)");
            strSql.Append(" values (");
            strSql.Append("@Name,@TableName,@UserName,@CreateDate,@ChangeDate,@BZ,@FormId,@WorkFlowId,@XMName,@XMRootPath,@DbName,@Beiyong1,@BeforeUpdateCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@TableName", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@ChangeDate", SqlDbType.DateTime),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@FormId", SqlDbType.NVarChar,100),
                    new SqlParameter("@WorkFlowId", SqlDbType.NVarChar,100),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,100),
                    new SqlParameter("@XMRootPath", SqlDbType.NVarChar,100),
                    new SqlParameter("@DbName", SqlDbType.NVarChar,100),
                    new SqlParameter("@Beiyong1", SqlDbType.NVarChar,100),
                    new SqlParameter("@BeforeUpdateCode", SqlDbType.Text)};
            parameters[0].Value = Name;
            parameters[1].Value = TableName;
            parameters[2].Value = UserName;
            parameters[3].Value = CreateDate;
            parameters[4].Value = ChangeDate;
            parameters[5].Value = BZ;
            parameters[6].Value = FormId;
            parameters[7].Value = WorkFlowId;
            parameters[8].Value = XMName;
            parameters[9].Value = XMRootPath;
            parameters[10].Value = DbName;
            parameters[11].Value = Beiyong1;
            parameters[12].Value = BeforeUpdateCode;

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
            strSql.Append("update [ERPProjectRequire] set ");
            strSql.Append("Name=@Name,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ChangeDate=@ChangeDate,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("FormId=@FormId,");
            strSql.Append("WorkFlowId=@WorkFlowId,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("XMRootPath=@XMRootPath,");
            strSql.Append("DbName=@DbName,");
            strSql.Append("Beiyong1=@Beiyong1,");
            strSql.Append("BeforeUpdateCode=@BeforeUpdateCode");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@TableName", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@ChangeDate", SqlDbType.DateTime),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@FormId", SqlDbType.NVarChar,100),
                    new SqlParameter("@WorkFlowId", SqlDbType.NVarChar,100),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,100),
                    new SqlParameter("@XMRootPath", SqlDbType.NVarChar,100),
                    new SqlParameter("@DbName", SqlDbType.NVarChar,100),
                    new SqlParameter("@Beiyong1", SqlDbType.NVarChar,100),
                    new SqlParameter("@BeforeUpdateCode", SqlDbType.Text),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = Name;
            parameters[1].Value = TableName;
            parameters[2].Value = UserName;
            parameters[3].Value = CreateDate;
            parameters[4].Value = ChangeDate;
            parameters[5].Value = BZ;
            parameters[6].Value = FormId;
            parameters[7].Value = WorkFlowId;
            parameters[8].Value = XMName;
            parameters[9].Value = XMRootPath;
            parameters[10].Value = DbName;
            parameters[11].Value = Beiyong1;
            parameters[12].Value = BeforeUpdateCode;
            parameters[13].Value = ID;

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
            strSql.Append("delete from [ERPProjectRequire] ");
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
            strSql.Append("select ID,Name,TableName,UserName,CreateDate,ChangeDate,BZ,FormId,WorkFlowId,XMName,XMRootPath,DbName,Beiyong1,BeforeUpdateCode ");
            strSql.Append(" FROM [ERPProjectRequire] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TableName"] != null)
                {
                    this.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeDate"] != null && ds.Tables[0].Rows[0]["ChangeDate"].ToString() != "")
                {
                    this.ChangeDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChangeDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormId"] != null)
                {
                    this.FormId = ds.Tables[0].Rows[0]["FormId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkFlowId"] != null)
                {
                    this.WorkFlowId = ds.Tables[0].Rows[0]["WorkFlowId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMRootPath"] != null)
                {
                    this.XMRootPath = ds.Tables[0].Rows[0]["XMRootPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DbName"] != null)
                {
                    this.DbName = ds.Tables[0].Rows[0]["DbName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Beiyong1"] != null)
                {
                    this.Beiyong1 = ds.Tables[0].Rows[0]["Beiyong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BeforeUpdateCode"] != null)
                {
                    this.BeforeUpdateCode = ds.Tables[0].Rows[0]["BeforeUpdateCode"].ToString();
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
            strSql.Append(" FROM [ERPProjectRequire] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetPagingList(string strWhere, int cPage, int pSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM ERPProjectRequire ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetRequireField()
        {
            var erpProjectRequireField = new ZWL.BLL.ERPProjectRequireField();
            var ds = erpProjectRequireField.GetList(string.Format("ParentId='{0}'", ID));
            var dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetModeDt()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 0 * ");
            strSql.Append(" FROM " + TableName);

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetModeByNWorkToDoID(string nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM " + TableName);
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetModeListByWhere(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM " + TableName);
            strSql.Append(" where " + where);

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddByTable(DataTable dt)
        {
            if (DbHelperSQL.AddTable(dt, TableName))
            {
                return DbHelperSQL.GetMaxID("ID", TableName);
            }
            else
            {
                return 0;
            }
        }
        public int UpdateByTable(DataTable dt)
        {
            return DbHelperSQL.UpdateTable(dt, TableName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public int DeleteByWhere(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete FROM " + TableName);
            strSql.Append(" where " + where);

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

    }
}

