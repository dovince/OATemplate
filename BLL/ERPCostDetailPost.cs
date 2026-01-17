using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPCostDetailPost。
    /// </summary>
    [Serializable]
    public partial class ERPCostDetailPost : ModelBase
    {
        public ERPCostDetailPost()
        { }
        #region Model
        private int _id;
        private string _lotno;
        private string _djbm;
        private string _djr;
        private DateTime _djtime;
        private string _state;
        private string _comment;
        private int? _enabledmark;
        private int? _deletemark;
        private DateTime? _deletetime;
        private string _deleteuser;
        private string _signuser;
        private DateTime? _signtime;
        /// <summary>
        /// 
        /// </summary>
        public override int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LotNo
        {
            set { _lotno = value; }
            get { return _lotno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DJBM
        {
            set { _djbm = value; }
            get { return _djbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DJTime
        {
            set { _djtime = value; }
            get { return _djtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
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
        public int? EnabledMark
        {
            set { _enabledmark = value; }
            get { return _enabledmark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeleteMark
        {
            set { _deletemark = value; }
            get { return _deletemark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeleteTime
        {
            set { _deletetime = value; }
            get { return _deletetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeleteUser
        {
            set { _deleteuser = value; }
            get { return _deleteuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SignUser
        {
            set { _signuser = value; }
            get { return _signuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SignTime
        {
            set { _signtime = value; }
            get { return _signtime; }
        }
        #endregion Model
        #region Relative Model

        private List<ZWL.BLL.ERPCostDetailPostItems> _subItems = null;
        public List<ZWL.BLL.ERPCostDetailPostItems> SubItems
        {
            get
            {
                if (_subItems == null)
                    _subItems = new List<ZWL.BLL.ERPCostDetailPostItems>();
                if (this.ID > 0)
                {
                    var _currentModel = new ZWL.BLL.ERPCostDetailPostItems();
                    _subItems = _currentModel.GetModelList<ZWL.BLL.ERPCostDetailPostItems>("ParentID=" + this.ID);
                }
                return _subItems;
            }
            set
            {
                _subItems = value;
            }
        }
        #endregion
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPCostDetailPost(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPCostDetailPost] ");
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
                if (ds.Tables[0].Rows[0]["LotNo"] != null)
                {
                    this.LotNo = ds.Tables[0].Rows[0]["LotNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
                {
                    this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
                {
                    this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
                {
                    this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SignUser"] != null)
                {
                    this.SignUser = ds.Tables[0].Rows[0]["SignUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SignTime"] != null && ds.Tables[0].Rows[0]["SignTime"].ToString() != "")
                {
                    this.SignTime = DateTime.Parse(ds.Tables[0].Rows[0]["SignTime"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPCostDetailPost]");
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
            strSql.Append("insert into [ERPCostDetailPost] (");
            strSql.Append("LotNo,DJBM,DJR,DJTime,State,Comment,EnabledMark,DeleteMark,DeleteTime,DeleteUser,SignUser,SignTime)");
            strSql.Append(" values (");
            strSql.Append("@LotNo,@DJBM,@DJR,@DJTime,@State,@Comment,@EnabledMark,@DeleteMark,@DeleteTime,@DeleteUser,@SignUser,@SignTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,50),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@State", SqlDbType.NVarChar,50),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,2000),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@SignUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@SignTime", SqlDbType.DateTime)};
            parameters[0].Value = LotNo;
            parameters[1].Value = DJBM;
            parameters[2].Value = DJR;
            parameters[3].Value = DJTime;
            parameters[4].Value = State;
            parameters[5].Value = Comment;
            parameters[6].Value = EnabledMark;
            parameters[7].Value = DeleteMark;
            parameters[8].Value = DeleteTime;
            parameters[9].Value = DeleteUser;
            parameters[10].Value = SignUser;
            parameters[11].Value = SignTime;

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
            strSql.Append("update [ERPCostDetailPost] set ");
            strSql.Append("LotNo=@LotNo,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("DJTime=@DJTime,");
            strSql.Append("State=@State,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("EnabledMark=@EnabledMark,");
            strSql.Append("DeleteMark=@DeleteMark,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("SignUser=@SignUser,");
            strSql.Append("SignTime=@SignTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,50),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@State", SqlDbType.NVarChar,50),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,2000),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@SignUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@SignTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LotNo;
            parameters[1].Value = DJBM;
            parameters[2].Value = DJR;
            parameters[3].Value = DJTime;
            parameters[4].Value = State;
            parameters[5].Value = Comment;
            parameters[6].Value = EnabledMark;
            parameters[7].Value = DeleteMark;
            parameters[8].Value = DeleteTime;
            parameters[9].Value = DeleteUser;
            parameters[10].Value = SignUser;
            parameters[11].Value = SignTime;
            parameters[12].Value = ID;

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
            strSql.Append("delete from [ERPCostDetailPost] ");
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
            strSql.Append(" FROM [ERPCostDetailPost] ");
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
                if (ds.Tables[0].Rows[0]["LotNo"] != null)
                {
                    this.LotNo = ds.Tables[0].Rows[0]["LotNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
                {
                    this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
                {
                    this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
                {
                    this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SignUser"] != null)
                {
                    this.SignUser = ds.Tables[0].Rows[0]["SignUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SignTime"] != null && ds.Tables[0].Rows[0]["SignTime"].ToString() != "")
                {
                    this.SignTime = DateTime.Parse(ds.Tables[0].Rows[0]["SignTime"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ERPCostDetailPost where DeleteMark is null");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

