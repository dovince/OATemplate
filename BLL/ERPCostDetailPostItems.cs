using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPCostDetailPostItems。
    /// </summary>
    [Serializable]
    public partial class ERPCostDetailPostItems : ModelBase
    {
        public ERPCostDetailPostItems()
        { }
        #region Model
        private int _id;
        private int _parentid;
        private int _recordid;
        private int? _relativeid;
        private string _item;
        private string _description;
        private decimal? _amount;
        private decimal? _settleamt;
        private decimal? _receivedamt;
        private decimal? _budgetamt;
        private decimal? _costedamt;
        private decimal _submitamt = 0.00M;
        private decimal? _totalamt;
        private decimal? _accuamt;
        private decimal? _itemscale;
        private decimal? _costscale;
        private int? _enabledmark;
        private int? _deletemark;
        private DateTime? _deletetime;
        private string _deleteuser;
        /// <summary>
        /// 
        /// </summary>
        public override int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 父表ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 记录ProjectCost ID
        /// </summary>
        public int RecordId
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 关联CostDetail ID
        /// </summary>
        public int? RelativeId
        {
            set { _relativeid = value; }
            get { return _relativeid; }
        }
        /// <summary>
        /// 支出类别
        /// </summary>
        public string Item
        {
            set { _item = value; }
            get { return _item; }
        }
        /// <summary>
        /// 支出明细
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal? SettleAmt
        {
            set { _settleamt = value; }
            get { return _settleamt; }
        }
        /// <summary>
        /// 到账金额
        /// </summary>
        public decimal? ReceivedAmt
        {
            set { _receivedamt = value; }
            get { return _receivedamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BudgetAmt
        {
            set { _budgetamt = value; }
            get { return _budgetamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CostedAmt
        {
            set { _costedamt = value; }
            get { return _costedamt; }
        }
        /// <summary>
        /// 报销费用
        /// </summary>
        public decimal SubmitAmt
        {
            set { _submitamt = value; }
            get { return _submitamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TotalAmt
        {
            set { _totalamt = value; }
            get { return _totalamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AccuAmt
        {
            set { _accuamt = value; }
            get { return _accuamt; }
        }
        /// <summary>
        /// 支出预算占比
        /// </summary>
        public decimal? ItemScale
        {
            set { _itemscale = value; }
            get { return _itemscale; }
        }
        /// <summary>
        /// 支出合同占比
        /// </summary>
        public decimal? CostScale
        {
            set { _costscale = value; }
            get { return _costscale; }
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
        public string XMName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTBH
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Workload
        {
            set;
            get;
        }
        #endregion Model

        #region  Relative Model

        private ZWL.BLL.ERPProjectCost _currentprocost = null;
        public ZWL.BLL.ERPProjectCost CurrentProjectCost
        {
            get
            {
                if (_currentprocost == null)
                    _currentprocost = new ZWL.BLL.ERPProjectCost();
                if (this.RecordId > 0)
                {
                    _currentprocost.GetModel(this.RecordId);
                }
                return _currentprocost;
            }
            set
            {
                _currentprocost = value;
            }
        }
        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPCostDetailPostItems(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPCostDetailPostItems] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPCostDetailPostItems]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPCostDetailPostItems] (");
            strSql.Append("ParentId,RecordId,RelativeId,Item,Description,SettleAmt,ReceivedAmt,SubmitAmt,TotalAmt,ItemScale,CostScale,EnabledMark,DeleteMark,DeleteTime,DeleteUser,BudgetAmt,CostedAmt,Amount,AccuAmt)");
            strSql.Append(" values (");
            strSql.Append("@ParentId,@RecordId,@RelativeId,@Item,@Description,@SettleAmt,@ReceivedAmt,@SubmitAmt,@TotalAmt,@ItemScale,@CostScale,@EnabledMark,@DeleteMark,@DeleteTime,@DeleteUser,@BudgetAmt,@CostedAmt,@Amount,@AccuAmt)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@RecordId", SqlDbType.Int,4),
                    new SqlParameter("@RelativeId", SqlDbType.Int,4),
                    new SqlParameter("@Item", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@SettleAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@ReceivedAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@SubmitAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@ItemScale", SqlDbType.Decimal,9),
                    new SqlParameter("@CostScale", SqlDbType.Decimal,9),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@BudgetAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@CostedAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@AccuAmt", SqlDbType.Decimal,9)};
            parameters[0].Value = ParentId;
            parameters[1].Value = RecordId;
            parameters[2].Value = RelativeId;
            parameters[3].Value = Item;
            parameters[4].Value = Description;
            parameters[5].Value = SettleAmt;
            parameters[6].Value = ReceivedAmt;
            parameters[7].Value = SubmitAmt;
            parameters[8].Value = TotalAmt;
            parameters[9].Value = ItemScale;
            parameters[10].Value = CostScale;
            parameters[11].Value = EnabledMark;
            parameters[12].Value = DeleteMark;
            parameters[13].Value = DeleteTime;
            parameters[14].Value = DeleteUser;
            parameters[15].Value = BudgetAmt;
            parameters[16].Value = CostedAmt;
            parameters[17].Value = Amount;
            parameters[18].Value = AccuAmt;

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
            strSql.Append("update [ERPCostDetailPostItems] set ");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("RecordId=@RecordId,");
            strSql.Append("RelativeId=@RelativeId,");
            strSql.Append("Item=@Item,");
            strSql.Append("Description=@Description,");
            strSql.Append("SettleAmt=@SettleAmt,");
            strSql.Append("ReceivedAmt=@ReceivedAmt,");
            strSql.Append("SubmitAmt=@SubmitAmt,");
            strSql.Append("TotalAmt=@TotalAmt,");
            strSql.Append("ItemScale=@ItemScale,");
            strSql.Append("CostScale=@CostScale,");
            strSql.Append("EnabledMark=@EnabledMark,");
            strSql.Append("DeleteMark=@DeleteMark,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("BudgetAmt=@BudgetAmt,");
            strSql.Append("CostedAmt=@CostedAmt,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("AccuAmt=@AccuAmt");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@RecordId", SqlDbType.Int,4),
                    new SqlParameter("@RelativeId", SqlDbType.Int,4),
                    new SqlParameter("@Item", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@SettleAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@ReceivedAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@SubmitAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@TotalAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@ItemScale", SqlDbType.Decimal,9),
                    new SqlParameter("@CostScale", SqlDbType.Decimal,9),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@BudgetAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@CostedAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@AccuAmt", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ParentId;
            parameters[1].Value = RecordId;
            parameters[2].Value = RelativeId;
            parameters[3].Value = Item;
            parameters[4].Value = Description;
            parameters[5].Value = SettleAmt;
            parameters[6].Value = ReceivedAmt;
            parameters[7].Value = SubmitAmt;
            parameters[8].Value = TotalAmt;
            parameters[9].Value = ItemScale;
            parameters[10].Value = CostScale;
            parameters[11].Value = EnabledMark;
            parameters[12].Value = DeleteMark;
            parameters[13].Value = DeleteTime;
            parameters[14].Value = DeleteUser;
            parameters[15].Value = BudgetAmt;
            parameters[16].Value = CostedAmt;
            parameters[17].Value = Amount;
            parameters[18].Value = AccuAmt;
            parameters[19].Value = ID;

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
            strSql.Append("delete from [ERPCostDetailPostItems] ");
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
            strSql.Append(" FROM [ERPCostDetailPostItems] ");
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
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RecordId"] != null && ds.Tables[0].Rows[0]["RecordId"].ToString() != "")
                {
                    this.RecordId = int.Parse(ds.Tables[0].Rows[0]["RecordId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RelativeId"] != null && ds.Tables[0].Rows[0]["RelativeId"].ToString() != "")
                {
                    this.RelativeId = int.Parse(ds.Tables[0].Rows[0]["RelativeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Item"] != null)
                {
                    this.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SettleAmt"] != null && ds.Tables[0].Rows[0]["SettleAmt"].ToString() != "")
                {
                    this.SettleAmt = decimal.Parse(ds.Tables[0].Rows[0]["SettleAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceivedAmt"] != null && ds.Tables[0].Rows[0]["ReceivedAmt"].ToString() != "")
                {
                    this.ReceivedAmt = decimal.Parse(ds.Tables[0].Rows[0]["ReceivedAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BudgetAmt"] != null && ds.Tables[0].Rows[0]["BudgetAmt"].ToString() != "")
                {
                    this.BudgetAmt = decimal.Parse(ds.Tables[0].Rows[0]["BudgetAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostedAmt"] != null && ds.Tables[0].Rows[0]["CostedAmt"].ToString() != "")
                {
                    this.CostedAmt = decimal.Parse(ds.Tables[0].Rows[0]["CostedAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubmitAmt"] != null && ds.Tables[0].Rows[0]["SubmitAmt"].ToString() != "")
                {
                    this.SubmitAmt = decimal.Parse(ds.Tables[0].Rows[0]["SubmitAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalAmt"] != null && ds.Tables[0].Rows[0]["TotalAmt"].ToString() != "")
                {
                    this.TotalAmt = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccuAmt"] != null && ds.Tables[0].Rows[0]["AccuAmt"].ToString() != "")
                {
                    this.AccuAmt = decimal.Parse(ds.Tables[0].Rows[0]["AccuAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemScale"] != null && ds.Tables[0].Rows[0]["ItemScale"].ToString() != "")
                {
                    this.ItemScale = decimal.Parse(ds.Tables[0].Rows[0]["ItemScale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostScale"] != null && ds.Tables[0].Rows[0]["CostScale"].ToString() != "")
                {
                    this.CostScale = decimal.Parse(ds.Tables[0].Rows[0]["CostScale"].ToString());
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
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPCostDetailPostItems]  where EnabledMark<>1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public override Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging<T>(strWhere, cPage, pSize,"ID desc");
        }
        public override Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize, string orderBy)
        {
            var sql = @"select p.*,XMName,XMBH,HTBH,ZYLB,Amount,JSJE,CostMoneySUM,XMJF from ERPCostDetailPostItems p join ERPProjectCost c on p.RecordId=c.ID where p.DeleteMark is null";
            var strSql = new StringBuilder();
            strSql.AppendFormat(sql);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderBy);
        }
        public override List<T> GetModelList<T>(string strWhere)
        {
            var sql = @"select p.*,(select top 1 XMName from ERPXMJBXX x where x.XMBH=c.XMBH) XMName,XMBH,
                    (case when HTBH is null or HTBH='' then XMBH else HTBH end) HTBH,ZYLB,Amount,JSJE,CostMoneySUM,XMJF 
                    from ERPCostDetailPostItems p join ERPProjectCost c on p.RecordId=c.ID 
                    where p.DeleteMark is null";
            var strSql = new StringBuilder();
            strSql.Append(sql);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            var dataSet = DbHelperSQL.Query(strSql.ToString());
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<T>(dataSet.Tables[0]);
            }
            return new List<T>();
        }
        #endregion  Method
    }
}

