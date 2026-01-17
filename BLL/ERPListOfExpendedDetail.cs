using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPListOfExpendedDetail,费用成本报销详细
    /// </summary>
    public class ERPListOfExpendedDetail
    {
        public ERPListOfExpendedDetail()
        { }
        #region Model
        private int _id;//主键

        private int _refid;

        private string _xmname = "";

        private string _zclb = "";

        private string _summary = "";

        private string _xhbh = "";

        private string _amount = "";

        private string _budget = "";

        private string _costedamt = "";

        private string _costingamt = "";

        private string _costingpercent = "";

        private string _costedpercent = "";


        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报销ID
        /// </summary>
        public int RefID
        {
            set { _refid = value; }
            get { return _refid; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }

        /// <summary>
        /// 支出类别
        /// </summary>
        public string ZCLB
        {
            set { _zclb = value; }
            get { return _zclb; }
        }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string XHBH
        {
            set { _xhbh = value; }
            get { return _xhbh; }
        }

        /// <summary>
        /// 结算金额
        /// </summary>
        public string Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

        /// <summary>
        /// 预算金额
        /// </summary>
        public string Budget
        {
            set { _budget = value; }
            get { return _budget; }
        }

        /// <summary>
        /// 已支付
        /// </summary>
        public string CostedAmt
        {
            set { _costedamt = value; }
            get { return _costedamt; }
        }

        /// <summary>
        /// 报销费用金额
        /// </summary>
        public string CostingAmt
        {
            set { _costingamt = value; }
            get { return _costingamt; }
        }

        /// <summary>
        /// 单项支出比例
        /// </summary>
        public string CostingPercent
        {
            set { _costingpercent = value; }
            get { return _costingpercent; }
        }

        /// <summary>
        /// 项目所有支出比例
        /// </summary>
        public string CostedPercent
        {
            set { _costedpercent = value; }
            get { return _costedpercent; }
        }


        #endregion Model


        #region Relative Model

        public ZWL.BLL.ERPListOfExpended ReferenceListOfExpended
        {
            get
            {
                ZWL.BLL.ERPListOfExpended _currentUser = null;
                if (RefID > 0)
                {
                    var tempUser = new ZWL.BLL.ERPListOfExpended();
                    tempUser.GetModel(RefID);
                    if (tempUser.ID > 0)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }
        #endregion
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPListOfExpendedDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RefID,XMName,ZCLB,Summary,XHBH,Amount,Budget,CostedAmt,CostingAmt,CostingPercent,CostedPercent ");
            strSql.Append(" FROM [ERPListOfExpendedDetail] ");
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
                if (ds.Tables[0].Rows[0]["RefID"] != null && ds.Tables[0].Rows[0]["RefID"].ToString() != "")
                {
                    this.RefID = int.Parse(ds.Tables[0].Rows[0]["RefID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZCLB"] != null)
                {
                    this.ZCLB = ds.Tables[0].Rows[0]["ZCLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Summary"] != null)
                {
                    this.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XHBH"] != null)
                {
                    this.XHBH = ds.Tables[0].Rows[0]["XHBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null)
                {
                    this.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Budget"] != null)
                {
                    this.Budget = ds.Tables[0].Rows[0]["Budget"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostedAmt"] != null)
                {
                    this.CostedAmt = ds.Tables[0].Rows[0]["CostedAmt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostingAmt"] != null)
                {
                    this.CostingAmt = ds.Tables[0].Rows[0]["CostingAmt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostingPercent"] != null)
                {
                    this.CostingPercent = ds.Tables[0].Rows[0]["CostingPercent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostedPercent"] != null)
                {
                    this.CostedPercent = ds.Tables[0].Rows[0]["CostedPercent"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPListOfExpendedDetail]");
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
            strSql.Append("insert into [ERPListOfExpendedDetail] (");
            strSql.Append("RefID,XMName,ZCLB,Summary,XHBH,Amount,Budget,CostedAmt,CostingAmt,CostingPercent,CostedPercent)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@RefID,@XMName,@ZCLB,@Summary,@XHBH,@Amount,@Budget,@CostedAmt,@CostingAmt,@CostingPercent,@CostedPercent)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@RefID", SqlDbType.Int),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 8000),

                    new SqlParameter("@ZCLB", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Summary", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XHBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Amount", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Budget", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostedAmt", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostingAmt", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostingPercent", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostedPercent", SqlDbType.NVarChar, 50)};

            parameters[0].Value = RefID;

            parameters[1].Value = XMName;

            parameters[2].Value = ZCLB;

            parameters[3].Value = Summary;

            parameters[4].Value = XHBH;

            parameters[5].Value = Amount;

            parameters[6].Value = Budget;

            parameters[7].Value = CostedAmt;

            parameters[8].Value = CostingAmt;

            parameters[9].Value = CostingPercent;

            parameters[10].Value = CostedPercent;


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
            strSql.Append("update [ERPListOfExpendedDetail] set ");

            strSql.Append("RefID=@RefID,");

            strSql.Append("XMName=@XMName,");

            strSql.Append("ZCLB=@ZCLB,");

            strSql.Append("Summary=@Summary,");

            strSql.Append("XHBH=@XHBH,");

            strSql.Append("Amount=@Amount,");

            strSql.Append("Budget=@Budget,");

            strSql.Append("CostedAmt=@CostedAmt,");

            strSql.Append("CostingAmt=@CostingAmt,");

            strSql.Append("CostingPercent=@CostingPercent,");

            strSql.Append("CostedPercent=@CostedPercent");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@RefID", SqlDbType.Int),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 8000),

                    new SqlParameter("@ZCLB", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Summary", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XHBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Amount", SqlDbType.NVarChar, 50),

                    new SqlParameter("@Budget", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostedAmt", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostingAmt", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostingPercent", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CostedPercent", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[1].Value = RefID;

            parameters[2].Value = XMName;

            parameters[3].Value = ZCLB;

            parameters[4].Value = Summary;

            parameters[5].Value = XHBH;

            parameters[6].Value = Amount;

            parameters[7].Value = Budget;

            parameters[8].Value = CostedAmt;

            parameters[9].Value = CostingAmt;

            parameters[10].Value = CostingPercent;

            parameters[11].Value = CostedPercent;

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
            strSql.Append("delete from [ERPListOfExpendedDetail] ");
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
            strSql.Append("select ID,RefID,XMName,ZCLB,Summary,XHBH,Amount,Budget,CostedAmt,CostingAmt,CostingPercent,CostedPercent ");
            strSql.Append(" FROM [ERPListOfExpendedDetail] ");
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
                if (ds.Tables[0].Rows[0]["RefID"] != null && ds.Tables[0].Rows[0]["RefID"].ToString() != "")
                {
                    this.RefID = int.Parse(ds.Tables[0].Rows[0]["RefID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZCLB"] != null)
                {
                    this.ZCLB = ds.Tables[0].Rows[0]["ZCLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Summary"] != null)
                {
                    this.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XHBH"] != null)
                {
                    this.XHBH = ds.Tables[0].Rows[0]["XHBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null)
                {
                    this.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Budget"] != null)
                {
                    this.Budget = ds.Tables[0].Rows[0]["Budget"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostedAmt"] != null)
                {
                    this.CostedAmt = ds.Tables[0].Rows[0]["CostedAmt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostingAmt"] != null)
                {
                    this.CostingAmt = ds.Tables[0].Rows[0]["CostingAmt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostingPercent"] != null)
                {
                    this.CostingPercent = ds.Tables[0].Rows[0]["CostingPercent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CostedPercent"] != null)
                {
                    this.CostedPercent = ds.Tables[0].Rows[0]["CostedPercent"].ToString();
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
            strSql.Append(" FROM [ERPListOfExpendedDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPListOfExpendedDetail ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                GetModel(ID);
            }
        }
    }
}