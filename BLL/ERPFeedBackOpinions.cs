using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPFeedBackOpinions。
    /// </summary>
    [Serializable]
    public partial class ERPFeedBackOpinions
    {
        public ERPFeedBackOpinions()
        { }
        #region Model
        private int _id;
        private int? _feedbackid;
        private int? _userid;
        private string _opinioncontent;
        private DateTime? _createtime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 反馈ID
        /// </summary>
        public int? FeedBackID
        {
            set { _feedbackid = value; }
            get { return _feedbackid; }
        }
        /// <summary>
        /// 消息发送人
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string OpinionContent
        {
            set { _opinioncontent = value; }
            get { return _opinioncontent; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPFeedBackOpinions(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,FeedBackID,UserID,OpinionContent,Createtime ");
            strSql.Append(" FROM [ERPFeedBackOpinions] ");
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
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OpinionContent"] != null)
                {
                    this.OpinionContent = ds.Tables[0].Rows[0]["OpinionContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Createtime"] != null && ds.Tables[0].Rows[0]["Createtime"].ToString() != "")
                {
                    this.Createtime = DateTime.Parse(ds.Tables[0].Rows[0]["Createtime"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPFeedBackOpinions]");
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
            strSql.Append("insert into [ERPFeedBackOpinions] (");
            strSql.Append("FeedBackID,UserID,OpinionContent,Createtime)");
            strSql.Append(" values (");
            strSql.Append("@FeedBackID,@UserID,@OpinionContent,@Createtime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FeedBackID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@OpinionContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@Createtime", SqlDbType.DateTime)};
            parameters[0].Value = FeedBackID;
            parameters[1].Value = UserID;
            parameters[2].Value = OpinionContent;
            parameters[3].Value = Createtime;

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
            strSql.Append("update [ERPFeedBackOpinions] set ");
            strSql.Append("FeedBackID=@FeedBackID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("OpinionContent=@OpinionContent,");
            strSql.Append("Createtime=@Createtime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FeedBackID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@OpinionContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@Createtime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FeedBackID;
            parameters[1].Value = UserID;
            parameters[2].Value = OpinionContent;
            parameters[3].Value = Createtime;
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
            strSql.Append("delete from [ERPFeedBackOpinions] ");
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
            strSql.Append("select ID,FeedBackID,UserID,OpinionContent,Createtime ");
            strSql.Append(" FROM [ERPFeedBackOpinions] ");
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
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OpinionContent"] != null)
                {
                    this.OpinionContent = ds.Tables[0].Rows[0]["OpinionContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Createtime"] != null && ds.Tables[0].Rows[0]["Createtime"].ToString() != "")
                {
                    this.Createtime = DateTime.Parse(ds.Tables[0].Rows[0]["Createtime"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,u.UserName ");
            strSql.Append(" FROM [ERPFeedBackOpinions] a left join ERPUser u on a.UserID=u.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Createtime ");
            return DbHelperSQL.Query(strSql.ToString());
        }



        #endregion  Method
    }
}

