using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPFeedBackImgs。
    /// </summary>
    [Serializable]
    public partial class ERPFeedBackImgs
    {
        public ERPFeedBackImgs()
        { }
        #region Model
        private int _id;
        private int? _feedbackid;
        private string _feedbackwatercode;
        private string _imgpath;
        private string _imgname;
        private string _oldname;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 反馈消息ID
        /// </summary>
        public int? FeedBackID
        {
            set { _feedbackid = value; }
            get { return _feedbackid; }
        }
        /// <summary>
        /// 反馈流水号
        /// </summary>
        public string FeedBackWaterCode
        {
            set { _feedbackwatercode = value; }
            get { return _feedbackwatercode; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string imgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string imgName
        {
            set { _imgname = value; }
            get { return _imgname; }
        }
        /// <summary>
        /// 文件旧的名字
        /// </summary>
        public string oldName
        {
            set { _oldname = value; }
            get { return _oldname; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPFeedBackImgs(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,FeedBackID,FeedBackWaterCode,imgPath,imgName,oldName ");
            strSql.Append(" FROM [ERPFeedBackImgs] ");
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
                if (ds.Tables[0].Rows[0]["FeedBackID"] != null && ds.Tables[0].Rows[0]["FeedBackID"].ToString() != "")
                {
                    this.FeedBackID = int.Parse(ds.Tables[0].Rows[0]["FeedBackID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedBackWaterCode"] != null)
                {
                    this.FeedBackWaterCode = ds.Tables[0].Rows[0]["FeedBackWaterCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["imgPath"] != null)
                {
                    this.imgPath = ds.Tables[0].Rows[0]["imgPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["imgName"] != null)
                {
                    this.imgName = ds.Tables[0].Rows[0]["imgName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["oldName"] != null)
                {
                    this.oldName = ds.Tables[0].Rows[0]["oldName"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPFeedBackImgs]");
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
            strSql.Append("insert into [ERPFeedBackImgs] (");
            strSql.Append("FeedBackID,FeedBackWaterCode,imgPath,imgName,oldName)");
            strSql.Append(" values (");
            strSql.Append("@FeedBackID,@FeedBackWaterCode,@imgPath,@imgName,@oldName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FeedBackID", SqlDbType.Int,4),
					new SqlParameter("@FeedBackWaterCode", SqlDbType.NVarChar,100),
					new SqlParameter("@imgPath", SqlDbType.NVarChar,-1),
					new SqlParameter("@imgName", SqlDbType.NVarChar,200),
					new SqlParameter("@oldName", SqlDbType.NVarChar,200)};
            parameters[0].Value = FeedBackID;
            parameters[1].Value = FeedBackWaterCode;
            parameters[2].Value = imgPath;
            parameters[3].Value = imgName;
            parameters[4].Value = oldName;

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
            strSql.Append("update [ERPFeedBackImgs] set ");
            strSql.Append("FeedBackID=@FeedBackID,");
            strSql.Append("FeedBackWaterCode=@FeedBackWaterCode,");
            strSql.Append("imgPath=@imgPath,");
            strSql.Append("imgName=@imgName,");
            strSql.Append("oldName=@oldName");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FeedBackID", SqlDbType.Int,4),
					new SqlParameter("@FeedBackWaterCode", SqlDbType.NVarChar,100),
					new SqlParameter("@imgPath", SqlDbType.NVarChar,-1),
					new SqlParameter("@imgName", SqlDbType.NVarChar,200),
					new SqlParameter("@oldName", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FeedBackID;
            parameters[1].Value = FeedBackWaterCode;
            parameters[2].Value = imgPath;
            parameters[3].Value = imgName;
            parameters[4].Value = oldName;
            parameters[5].Value = ID;

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
            strSql.Append("delete from [ERPFeedBackImgs] ");
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
            strSql.Append("select ID,FeedBackID,FeedBackWaterCode,imgPath,imgName,oldName ");
            strSql.Append(" FROM [ERPFeedBackImgs] ");
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
                if (ds.Tables[0].Rows[0]["FeedBackID"] != null && ds.Tables[0].Rows[0]["FeedBackID"].ToString() != "")
                {
                    this.FeedBackID = int.Parse(ds.Tables[0].Rows[0]["FeedBackID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedBackWaterCode"] != null)
                {
                    this.FeedBackWaterCode = ds.Tables[0].Rows[0]["FeedBackWaterCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["imgPath"] != null)
                {
                    this.imgPath = ds.Tables[0].Rows[0]["imgPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["imgName"] != null)
                {
                    this.imgName = ds.Tables[0].Rows[0]["imgName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["oldName"] != null)
                {
                    this.oldName = ds.Tables[0].Rows[0]["oldName"].ToString();
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
            strSql.Append(" FROM [ERPFeedBackImgs] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

