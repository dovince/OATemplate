using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPInnerInviteBidRequest。
	/// </summary>
	[Serializable]
    public partial class ERPInnerInviteBidRequest
    {
        public ERPInnerInviteBidRequest()
        { }
        #region Model
        private int _id;
        private int _nworktodoid;
        private string _workname;
        private DateTime _dengjitime;
        private string _no;
        private string _xmbh;
        private string _xmname;
        private decimal _amount;
        private DateTime _estimatedate;
        private string _purchasedetails;
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
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
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
        public string No
        {
            set { _no = value; }
            get { return _no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
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
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EstimateDate
        {
            set { _estimatedate = value; }
            get { return _estimatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PurchaseDetails
        {
            set { _purchasedetails = value; }
            get { return _purchasedetails; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPInnerInviteBidRequest(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,No,XMBH,XMName,Amount,EstimateDate,PurchaseDetails ");
            strSql.Append(" FROM [ERPInnerInviteBidRequest] ");
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
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EstimateDate"] != null && ds.Tables[0].Rows[0]["EstimateDate"].ToString() != "")
                {
                    this.EstimateDate = DateTime.Parse(ds.Tables[0].Rows[0]["EstimateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PurchaseDetails"] != null)
                {
                    this.PurchaseDetails = ds.Tables[0].Rows[0]["PurchaseDetails"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPInnerInviteBidRequest]");
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
            strSql.Append("insert into [ERPInnerInviteBidRequest] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,No,XMBH,XMName,Amount,EstimateDate,PurchaseDetails)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@No,@XMBH,@XMName,@Amount,@EstimateDate,@PurchaseDetails)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,200),
                    new SqlParameter("@DengJiTime", SqlDbType.DateTime),
                    new SqlParameter("@No", SqlDbType.VarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@EstimateDate", SqlDbType.DateTime),
                    new SqlParameter("@PurchaseDetails", SqlDbType.NVarChar,1000)};
            parameters[0].Value = NWorkToDoID;
            parameters[1].Value = WorkName;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = No;
            parameters[4].Value = XMBH;
            parameters[5].Value = XMName;
            parameters[6].Value = Amount;
            parameters[7].Value = EstimateDate;
            parameters[8].Value = PurchaseDetails;

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
            strSql.Append("update [ERPInnerInviteBidRequest] set ");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("DengJiTime=@DengJiTime,");
            strSql.Append("No=@No,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("EstimateDate=@EstimateDate,");
            strSql.Append("PurchaseDetails=@PurchaseDetails");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,200),
                    new SqlParameter("@DengJiTime", SqlDbType.DateTime),
                    new SqlParameter("@No", SqlDbType.VarChar,50),
                    new SqlParameter("@XMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@EstimateDate", SqlDbType.DateTime),
                    new SqlParameter("@PurchaseDetails", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkToDoID;
            parameters[1].Value = WorkName;
            parameters[2].Value = DengJiTime;
            parameters[3].Value = No;
            parameters[4].Value = XMBH;
            parameters[5].Value = XMName;
            parameters[6].Value = Amount;
            parameters[7].Value = EstimateDate;
            parameters[8].Value = PurchaseDetails;
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
            strSql.Append("delete from [ERPInnerInviteBidRequest] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,No,XMBH,XMName,Amount,EstimateDate,PurchaseDetails ");
            strSql.Append(" FROM [ERPInnerInviteBidRequest] ");
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
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EstimateDate"] != null && ds.Tables[0].Rows[0]["EstimateDate"].ToString() != "")
                {
                    this.EstimateDate = DateTime.Parse(ds.Tables[0].Rows[0]["EstimateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PurchaseDetails"] != null)
                {
                    this.PurchaseDetails = ds.Tables[0].Rows[0]["PurchaseDetails"].ToString();
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
            strSql.Append(" FROM [ERPInnerInviteBidRequest] ");
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
            strSql.Append(" FROM ERPInnerInviteBidRequest ");
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

