using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class ERPHTJieSuan 
    {
        public ERPHTJieSuan()
        { }
        #region Model
        private int _id;
        private int? _round = 1;
        private string _htbh;
        private string _htname;
        private string _bm;
        private string _jbr;
        private decimal? _jsje;
        private DateTime? _jstime;
        private string _state;
        private int? _nworktodoid;
        private string _beiyong1;
        private string _beiyong2;
        private string _yjfjsgzl;
        private decimal? _xmdj;
        private decimal? _tcjsje;
        private string _jsbgqk;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 次数
        /// </summary>
        public int? Round
        {
            set { _round = value; }
            get { return _round; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTBH
        {
            set { _htbh = value; }
            get { return _htbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTName
        {
            set { _htname = value; }
            get { return _htname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BM
        {
            set { _bm = value; }
            get { return _bm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JBR
        {
            set { _jbr = value; }
            get { return _jbr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JSJE
        {
            set { _jsje = value; }
            get { return _jsje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JSTime
        {
            set { _jstime = value; }
            get { return _jstime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string beiyong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string beiyong2
        {
            set { _beiyong2 = value; }
            get { return _beiyong2; }
        }
        /// <summary>
        /// 与甲方结算工作量
        /// </summary>
        public string YJFJSGZL
        {
            set { _yjfjsgzl = value; }
            get { return _yjfjsgzl; }
        }
        /// <summary>
        /// 项目单价
        /// </summary>
        public decimal? XMDJ
        {
            set { _xmdj = value; }
            get { return _xmdj; }
        }
        /// <summary>
        /// 我方向甲方提出结算额
        /// </summary>
        public decimal? TCJSJE
        {
            set { _tcjsje = value; }
            get { return _tcjsje; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JSBGQK
        {
            set { _jsbgqk = value; }
            get { return _jsbgqk; }
        }
        #endregion Model

        #region MyRegion
        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo
        {
            get
            {
                var _currentWorkToDo = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkToDoID > 0)
                {
                    _currentWorkToDo.GetModel(NWorkToDoID.Value);
                }
                return _currentWorkToDo;
            }
        } 
        #endregion

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPHTJieSuan(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPHTJieSuan] ");
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
                if (ds.Tables[0].Rows[0]["Round"] != null && ds.Tables[0].Rows[0]["Round"].ToString() != "")
                {
                    this.Round = int.Parse(ds.Tables[0].Rows[0]["Round"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTName"] != null)
                {
                    this.HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJE"] != null && ds.Tables[0].Rows[0]["JSJE"].ToString() != "")
                {
                    this.JSJE = decimal.Parse(ds.Tables[0].Rows[0]["JSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JSTime"] != null && ds.Tables[0].Rows[0]["JSTime"].ToString() != "")
                {
                    this.JSTime = DateTime.Parse(ds.Tables[0].Rows[0]["JSTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["state"] != null)
                {
                    this.state = ds.Tables[0].Rows[0]["state"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["beiyong1"] != null)
                {
                    this.beiyong1 = ds.Tables[0].Rows[0]["beiyong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["beiyong2"] != null)
                {
                    this.beiyong2 = ds.Tables[0].Rows[0]["beiyong2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YJFJSGZL"] != null)
                {
                    this.YJFJSGZL = ds.Tables[0].Rows[0]["YJFJSGZL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMDJ"] != null && ds.Tables[0].Rows[0]["XMDJ"].ToString() != "")
                {
                    this.XMDJ = decimal.Parse(ds.Tables[0].Rows[0]["XMDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TCJSJE"] != null && ds.Tables[0].Rows[0]["TCJSJE"].ToString() != "")
                {
                    this.TCJSJE = decimal.Parse(ds.Tables[0].Rows[0]["TCJSJE"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPHTJieSuan]");
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
            strSql.Append("insert into [ERPHTJieSuan] (");
            strSql.Append("Round,HTBH,HTName,BM,JBR,JSJE,JSTime,state,NWorkToDoID,beiyong1,beiyong2,YJFJSGZL,XMDJ,TCJSJE,JSBGQK)");
            strSql.Append(" values (");
            strSql.Append("@Round,@HTBH,@HTName,@BM,@JBR,@JSJE,@JSTime,@state,@NWorkToDoID,@beiyong1,@beiyong2,@YJFJSGZL,@XMDJ,@TCJSJE,@JSBGQK)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Round", SqlDbType.Int,4),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.NVarChar,50),
                    new SqlParameter("@JSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSTime", SqlDbType.DateTime),
                    new SqlParameter("@state", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@beiyong1", SqlDbType.NVarChar,50),
                    new SqlParameter("@beiyong2", SqlDbType.NVarChar,50),
                    new SqlParameter("@YJFJSGZL", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMDJ", SqlDbType.Decimal,9),
                    new SqlParameter("@TCJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSBGQK", SqlDbType.NVarChar,255)};
            parameters[0].Value = Round;
            parameters[1].Value = HTBH;
            parameters[2].Value = HTName;
            parameters[3].Value = BM;
            parameters[4].Value = JBR;
            parameters[5].Value = JSJE;
            parameters[6].Value = JSTime;
            parameters[7].Value = state;
            parameters[8].Value = NWorkToDoID;
            parameters[9].Value = beiyong1;
            parameters[10].Value = beiyong2;
            parameters[11].Value = YJFJSGZL;
            parameters[12].Value = XMDJ;
            parameters[13].Value = TCJSJE;
            parameters[14].Value = JSBGQK;

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
            strSql.Append("update [ERPHTJieSuan] set ");
            strSql.Append("Round=@Round,");
            strSql.Append("HTName=@HTName,");
            strSql.Append("BM=@BM,");
            strSql.Append("JBR=@JBR,");
            strSql.Append("JSJE=@JSJE,");
            strSql.Append("JSTime=@JSTime,");
            strSql.Append("state=@state,");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("beiyong2=@beiyong2,");
            strSql.Append("YJFJSGZL=@YJFJSGZL,");
            strSql.Append("XMDJ=@XMDJ,");
            strSql.Append("TCJSJE=@TCJSJE,");
            strSql.Append("JSBGQK=@JSBGQK");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Round", SqlDbType.Int,4),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.NVarChar,50),
                    new SqlParameter("@JSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSTime", SqlDbType.DateTime),
                    new SqlParameter("@state", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@beiyong2", SqlDbType.NVarChar,50),
                    new SqlParameter("@YJFJSGZL", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMDJ", SqlDbType.Decimal,9),
                    new SqlParameter("@TCJSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSBGQK", SqlDbType.NVarChar,255),
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@beiyong1", SqlDbType.NVarChar,50)};
            parameters[0].Value = Round;
            parameters[1].Value = HTName;
            parameters[2].Value = BM;
            parameters[3].Value = JBR;
            parameters[4].Value = JSJE;
            parameters[5].Value = JSTime;
            parameters[6].Value = state;
            parameters[7].Value = NWorkToDoID;
            parameters[8].Value = beiyong2;
            parameters[9].Value = YJFJSGZL;
            parameters[10].Value = XMDJ;
            parameters[11].Value = TCJSJE;
            parameters[12].Value = JSBGQK;
            parameters[13].Value = ID;
            parameters[14].Value = HTBH;
            parameters[15].Value = beiyong1;

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
            strSql.Append("delete from [ERPHTJieSuan] ");
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
            strSql.Append(" FROM [ERPHTJieSuan] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Round"] != null && ds.Tables[0].Rows[0]["Round"].ToString() != "")
                {
                    this.Round = int.Parse(ds.Tables[0].Rows[0]["Round"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTName"] != null)
                {
                    this.HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJE"] != null && ds.Tables[0].Rows[0]["JSJE"].ToString() != "")
                {
                    this.JSJE = decimal.Parse(ds.Tables[0].Rows[0]["JSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JSTime"] != null && ds.Tables[0].Rows[0]["JSTime"].ToString() != "")
                {
                    this.JSTime = DateTime.Parse(ds.Tables[0].Rows[0]["JSTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["state"] != null)
                {
                    this.state = ds.Tables[0].Rows[0]["state"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["beiyong1"] != null)
                {
                    this.beiyong1 = ds.Tables[0].Rows[0]["beiyong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["beiyong2"] != null)
                {
                    this.beiyong2 = ds.Tables[0].Rows[0]["beiyong2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YJFJSGZL"] != null)
                {
                    this.YJFJSGZL = ds.Tables[0].Rows[0]["YJFJSGZL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMDJ"] != null && ds.Tables[0].Rows[0]["XMDJ"].ToString() != "")
                {
                    this.XMDJ = decimal.Parse(ds.Tables[0].Rows[0]["XMDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TCJSJE"] != null && ds.Tables[0].Rows[0]["TCJSJE"].ToString() != "")
                {
                    this.TCJSJE = decimal.Parse(ds.Tables[0].Rows[0]["TCJSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JSBGQK"] != null)
                {
                    this.JSBGQK = ds.Tables[0].Rows[0]["JSBGQK"].ToString();
                }
            }
        }
        public void GetModelByWorkId(int workid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPHTJieSuan] ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4)};
            parameters[0].Value = workid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPHTJieSuan] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ERPHTJieSuan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }
}
