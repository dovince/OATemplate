using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPCostDetailPostItemsWorkload。
    /// </summary>
    [Serializable]
    public partial class ERPCostDetailPostItemsWorkload : ModelBase
    {
        public ERPCostDetailPostItemsWorkload()
        { }
        #region Model
        private int _id;
        private int _parentid;
        private int _itemid;
        private string _unit;
        private decimal? _quantity;
        private decimal? _price;
        private decimal? _calcpercent;
        private decimal? _amount;
        private string _supplier;
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
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CalcPercent
        {
            set { _calcpercent = value; }
            get { return _calcpercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPCostDetailPostItemsWorkload(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,ParentId,ItemId,Unit,Quantity,Price,CalcPercent,Amount,Supplier ");
			strSql.Append(" FROM [ERPCostDetailPostItemsWorkload] ");
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
				if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
				{
					this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ItemId"] != null && ds.Tables[0].Rows[0]["ItemId"].ToString() != "")
				{
					this.ItemId = int.Parse(ds.Tables[0].Rows[0]["ItemId"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Unit"] != null)
				{
					this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Quantity"] != null && ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
				{
					this.Quantity = decimal.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
				{
					this.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CalcPercent"] != null && ds.Tables[0].Rows[0]["CalcPercent"].ToString() != "")
				{
					this.CalcPercent = decimal.Parse(ds.Tables[0].Rows[0]["CalcPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
				{
					this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Supplier"] != null)
				{
					this.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [ERPCostDetailPostItemsWorkload]");
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
			strSql.Append("insert into [ERPCostDetailPostItemsWorkload] (");
			strSql.Append("ParentId,ItemId,Unit,Quantity,Price,CalcPercent,Amount,Supplier)");
			strSql.Append(" values (");
			strSql.Append("@ParentId,@ItemId,@Unit,@Quantity,@Price,@CalcPercent,@Amount,@Supplier)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Quantity", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@CalcPercent", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,2000)};
			parameters[0].Value = ParentId;
			parameters[1].Value = ItemId;
			parameters[2].Value = Unit;
			parameters[3].Value = Quantity;
			parameters[4].Value = Price;
			parameters[5].Value = CalcPercent;
			parameters[6].Value = Amount;
			parameters[7].Value = Supplier;

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
			strSql.Append("update [ERPCostDetailPostItemsWorkload] set ");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("ItemId=@ItemId,");
			strSql.Append("Unit=@Unit,");
			strSql.Append("Quantity=@Quantity,");
			strSql.Append("Price=@Price,");
			strSql.Append("CalcPercent=@CalcPercent,");
			strSql.Append("Amount=@Amount,");
			strSql.Append("Supplier=@Supplier");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@ItemId", SqlDbType.Int,4),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Quantity", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@CalcPercent", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Supplier", SqlDbType.NVarChar,2000),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ParentId;
			parameters[1].Value = ItemId;
			parameters[2].Value = Unit;
			parameters[3].Value = Quantity;
			parameters[4].Value = Price;
			parameters[5].Value = CalcPercent;
			parameters[6].Value = Amount;
			parameters[7].Value = Supplier;
			parameters[8].Value = ID;

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
			strSql.Append("delete from [ERPCostDetailPostItemsWorkload] ");
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
			strSql.Append("select ID,ParentId,ItemId,Unit,Quantity,Price,CalcPercent,Amount,Supplier ");
			strSql.Append(" FROM [ERPCostDetailPostItemsWorkload] ");
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
				if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
				{
					this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ItemId"] != null && ds.Tables[0].Rows[0]["ItemId"].ToString() != "")
				{
					this.ItemId = int.Parse(ds.Tables[0].Rows[0]["ItemId"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Unit"] != null)
				{
					this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Quantity"] != null && ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
				{
					this.Quantity = decimal.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
				{
					this.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CalcPercent"] != null && ds.Tables[0].Rows[0]["CalcPercent"].ToString() != "")
				{
					this.CalcPercent = decimal.Parse(ds.Tables[0].Rows[0]["CalcPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
				{
					this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Supplier"] != null)
				{
					this.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
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
			strSql.Append(" FROM [ERPCostDetailPostItemsWorkload] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

