using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPGongGao。
	/// </summary>
	[Serializable]
    public partial class ERPGongGao
    {
        public ERPGongGao()
        { }
        #region Model
        private int _id;
        private string _titlestr;
        private DateTime _timestr = DateTime.Now;
        private string _username;
        private string _userbumen;
        private string _noticetype;
        private string _fujian;
        private string _summary;
        private string _imgpath;
        private string _contentstr;
        private string _typestr;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公告主题
        /// </summary>
        public string TitleStr
        {
            set { _titlestr = value; }
            get { return _titlestr; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 接收部门
        /// </summary>
        public string UserBuMen
        {
            set { _userbumen = value; }
            get { return _userbumen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NoticeType
        {
            set { _noticetype = value; }
            get { return _noticetype; }
        }
        /// <summary>
        /// 附件文件
        /// </summary>
        public string FuJian
        {
            set { _fujian = value; }
            get { return _fujian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string ContentStr
        {
            set { _contentstr = value; }
            get { return _contentstr; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string TypeStr
        {
            set { _typestr = value; }
            get { return _typestr; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPGongGao(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TitleStr,TimeStr,UserName,UserBuMen,NoticeType,FuJian,Summary,ImgPath,ContentStr,TypeStr ");
            strSql.Append(" FROM [ERPGongGao] ");
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
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStr"] != null && ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    this.TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserBuMen"] != null)
                {
                    this.UserBuMen = ds.Tables[0].Rows[0]["UserBuMen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NoticeType"] != null)
                {
                    this.NoticeType = ds.Tables[0].Rows[0]["NoticeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian"] != null)
                {
                    this.FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Summary"] != null)
                {
                    this.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImgPath"] != null)
                {
                    this.ImgPath = ds.Tables[0].Rows[0]["ImgPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContentStr"] != null)
                {
                    this.ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TypeStr"] != null)
                {
                    this.TypeStr = ds.Tables[0].Rows[0]["TypeStr"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPGongGao]");
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
            strSql.Append("insert into [ERPGongGao] (");
            strSql.Append("TitleStr,TimeStr,UserName,UserBuMen,NoticeType,FuJian,Summary,ImgPath,ContentStr,TypeStr)");
            strSql.Append(" values (");
            strSql.Append("@TitleStr,@TimeStr,@UserName,@UserBuMen,@NoticeType,@FuJian,@Summary,@ImgPath,@ContentStr,@TypeStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
                    new SqlParameter("@TimeStr", SqlDbType.DateTime),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserBuMen", SqlDbType.VarChar,5000),
                    new SqlParameter("@NoticeType", SqlDbType.NVarChar,50),
                    new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
                    new SqlParameter("@Summary", SqlDbType.VarChar,5000),
                    new SqlParameter("@ImgPath", SqlDbType.VarChar,500),
                    new SqlParameter("@ContentStr", SqlDbType.Text),
                    new SqlParameter("@TypeStr", SqlDbType.VarChar,50)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = TimeStr;
            parameters[2].Value = UserName;
            parameters[3].Value = UserBuMen;
            parameters[4].Value = NoticeType;
            parameters[5].Value = FuJian;
            parameters[6].Value = Summary;
            parameters[7].Value = ImgPath;
            parameters[8].Value = ContentStr;
            parameters[9].Value = TypeStr;

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
            strSql.Append("update [ERPGongGao] set ");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserBuMen=@UserBuMen,");
            strSql.Append("NoticeType=@NoticeType,");
            strSql.Append("FuJian=@FuJian,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("ImgPath=@ImgPath,");
            strSql.Append("ContentStr=@ContentStr,");
            strSql.Append("TypeStr=@TypeStr");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
                    new SqlParameter("@TimeStr", SqlDbType.DateTime),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserBuMen", SqlDbType.VarChar,5000),
                    new SqlParameter("@NoticeType", SqlDbType.NVarChar,50),
                    new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
                    new SqlParameter("@Summary", SqlDbType.VarChar,5000),
                    new SqlParameter("@ImgPath", SqlDbType.VarChar,500),
                    new SqlParameter("@ContentStr", SqlDbType.Text),
                    new SqlParameter("@TypeStr", SqlDbType.VarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = TimeStr;
            parameters[2].Value = UserName;
            parameters[3].Value = UserBuMen;
            parameters[4].Value = NoticeType;
            parameters[5].Value = FuJian;
            parameters[6].Value = Summary;
            parameters[7].Value = ImgPath;
            parameters[8].Value = ContentStr;
            parameters[9].Value = TypeStr;
            parameters[10].Value = ID;

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
            strSql.Append("delete from [ERPGongGao] ");
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
            strSql.Append("select ID,TitleStr,TimeStr,UserName,UserBuMen,NoticeType,FuJian,Summary,ImgPath,ContentStr,TypeStr ");
            strSql.Append(" FROM [ERPGongGao] ");
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
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStr"] != null && ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    this.TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserBuMen"] != null)
                {
                    this.UserBuMen = ds.Tables[0].Rows[0]["UserBuMen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NoticeType"] != null)
                {
                    this.NoticeType = ds.Tables[0].Rows[0]["NoticeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian"] != null)
                {
                    this.FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Summary"] != null)
                {
                    this.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImgPath"] != null)
                {
                    this.ImgPath = ds.Tables[0].Rows[0]["ImgPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContentStr"] != null)
                {
                    this.ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TypeStr"] != null)
                {
                    this.TypeStr = ds.Tables[0].Rows[0]["TypeStr"].ToString();
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
            strSql.Append(" FROM [ERPGongGao] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}