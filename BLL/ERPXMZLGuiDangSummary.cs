using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPXMZLGuiDangSummary。
	/// </summary>
	[Serializable]
    public partial class ERPXMZLGuiDangSummary
    {
        public ERPXMZLGuiDangSummary()
        { }
        #region Model
        private int _id;
        private string _leibie;
        private string _daihao;
        private int _zhiamt;
        private string _zhihehao;
        private int _dianamt;
        private string _dianpanhao;
        private string _qizhiyema;
        private string _comment;
        private int _nworkid;
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
        public string LeiBie
        {
            set { _leibie = value; }
            get { return _leibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DaiHao
        {
            set { _daihao = value; }
            get { return _daihao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ZhiAmt
        {
            set { _zhiamt = value; }
            get { return _zhiamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhiHeHao
        {
            set { _zhihehao = value; }
            get { return _zhihehao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DianAmt
        {
            set { _dianamt = value; }
            get { return _dianamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DianPanHao
        {
            set { _dianpanhao = value; }
            get { return _dianpanhao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QiZhiYeMa
        {
            set { _qizhiyema = value; }
            get { return _qizhiyema; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMZLGuiDangSummary(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LeiBie,DaiHao,ZhiAmt,ZhiHeHao,DianAmt,DianPanHao,QiZhiYeMa,Comment,NWorkID ");
            strSql.Append(" FROM [ERPXMZLGuiDangSummary] ");
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
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DaiHao"] != null)
                {
                    this.DaiHao = ds.Tables[0].Rows[0]["DaiHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiAmt"] != null && ds.Tables[0].Rows[0]["ZhiAmt"].ToString() != "")
                {
                    this.ZhiAmt = int.Parse(ds.Tables[0].Rows[0]["ZhiAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiHeHao"] != null)
                {
                    this.ZhiHeHao = ds.Tables[0].Rows[0]["ZhiHeHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DianAmt"] != null && ds.Tables[0].Rows[0]["DianAmt"].ToString() != "")
                {
                    this.DianAmt = int.Parse(ds.Tables[0].Rows[0]["DianAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DianPanHao"] != null)
                {
                    this.DianPanHao = ds.Tables[0].Rows[0]["DianPanHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QiZhiYeMa"] != null)
                {
                    this.QiZhiYeMa = ds.Tables[0].Rows[0]["QiZhiYeMa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPXMZLGuiDangSummary");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMZLGuiDangSummary]");
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
            strSql.Append("insert into [ERPXMZLGuiDangSummary] (");
            strSql.Append("LeiBie,DaiHao,ZhiAmt,ZhiHeHao,DianAmt,DianPanHao,QiZhiYeMa,Comment,NWorkID)");
            strSql.Append(" values (");
            strSql.Append("@LeiBie,@DaiHao,@ZhiAmt,@ZhiHeHao,@DianAmt,@DianPanHao,@QiZhiYeMa,@Comment,@NWorkID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LeiBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@DaiHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhiAmt", SqlDbType.Int,4),
                    new SqlParameter("@ZhiHeHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@DianAmt", SqlDbType.Int,4),
                    new SqlParameter("@DianPanHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@QiZhiYeMa", SqlDbType.NVarChar,500),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4)};
            parameters[0].Value = LeiBie;
            parameters[1].Value = DaiHao;
            parameters[2].Value = ZhiAmt;
            parameters[3].Value = ZhiHeHao;
            parameters[4].Value = DianAmt;
            parameters[5].Value = DianPanHao;
            parameters[6].Value = QiZhiYeMa;
            parameters[7].Value = QiZhiYeMa;
            parameters[8].Value = NWorkID;

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
            strSql.Append("update [ERPXMZLGuiDangSummary] set ");
            strSql.Append("LeiBie=@LeiBie,");
            strSql.Append("DaiHao=@DaiHao,");
            strSql.Append("ZhiAmt=@ZhiAmt,");
            strSql.Append("ZhiHeHao=@ZhiHeHao,");
            strSql.Append("DianAmt=@DianAmt,");
            strSql.Append("DianPanHao=@DianPanHao,");
            strSql.Append("QiZhiYeMa=@QiZhiYeMa,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("NWorkID=@NWorkID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LeiBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@DaiHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhiAmt", SqlDbType.Int,4),
                    new SqlParameter("@ZhiHeHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@DianAmt", SqlDbType.Int,4),
                    new SqlParameter("@DianPanHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@QiZhiYeMa", SqlDbType.NVarChar,500),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LeiBie;
            parameters[1].Value = DaiHao;
            parameters[2].Value = ZhiAmt;
            parameters[3].Value = ZhiHeHao;
            parameters[4].Value = DianAmt;
            parameters[5].Value = DianPanHao;
            parameters[6].Value = QiZhiYeMa;
            parameters[7].Value = Comment;
            parameters[8].Value = NWorkID;
            parameters[9].Value = ID;

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
            strSql.Append("delete from [ERPXMZLGuiDangSummary] ");
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
            strSql.Append("select ID,LeiBie,DaiHao,ZhiAmt,ZhiHeHao,DianAmt,DianPanHao,QiZhiYeMa,Comment,NWorkID ");
            strSql.Append(" FROM [ERPXMZLGuiDangSummary] ");
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
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DaiHao"] != null)
                {
                    this.DaiHao = ds.Tables[0].Rows[0]["DaiHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiAmt"] != null && ds.Tables[0].Rows[0]["ZhiAmt"].ToString() != "")
                {
                    this.ZhiAmt = int.Parse(ds.Tables[0].Rows[0]["ZhiAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiHeHao"] != null)
                {
                    this.ZhiHeHao = ds.Tables[0].Rows[0]["ZhiHeHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DianAmt"] != null && ds.Tables[0].Rows[0]["DianAmt"].ToString() != "")
                {
                    this.DianAmt = int.Parse(ds.Tables[0].Rows[0]["DianAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DianPanHao"] != null)
                {
                    this.DianPanHao = ds.Tables[0].Rows[0]["DianPanHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QiZhiYeMa"] != null)
                {
                    this.QiZhiYeMa = ds.Tables[0].Rows[0]["QiZhiYeMa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
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
            strSql.Append(" FROM [ERPXMZLGuiDangSummary] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
