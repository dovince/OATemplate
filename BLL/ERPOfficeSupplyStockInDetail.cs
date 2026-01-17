using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPOfficeSupplyStockInDetail。
    /// </summary>
    [Serializable]
    public partial class ERPOfficeSupplyStockInDetail
    {
        public ERPOfficeSupplyStockInDetail()
        { }
        #region Model
        private int _id;
        private int _stockinid;
        private int? _referenceid;
        private int? _itemid;
        private string _name;
        private string _spec;
        private string _unit;
        private int _quantity;
        private decimal? _price;
        private decimal? _total;
        private string _supplier;
        private int? _supplierid;
        private string _comment;
        private DateTime? _stockintime;
        private string _stockinuser;
        private DateTime? _creatortime;
        private string _creatoruser;
        private DateTime? _lastmodifytime;
        private string _lastmodifyuser;
        private DateTime? _deletetime;
        private string _deleteuser;
        private int? _deletemark;
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
        public int StockInID
        {
            set { _stockinid = value; }
            get { return _stockinid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReferenceID
        {
            set { _referenceid = value; }
            get { return _referenceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Spec
        {
            set { _spec = value; }
            get { return _spec; }
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
        public int Quantity
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
        public decimal? Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
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
        public DateTime? StockInTime
        {
            set { _stockintime = value; }
            get { return _stockintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StockInUser
        {
            set { _stockinuser = value; }
            get { return _stockinuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatorTime
        {
            set { _creatortime = value; }
            get { return _creatortime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUser
        {
            set { _creatoruser = value; }
            get { return _creatoruser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifyUser
        {
            set { _lastmodifyuser = value; }
            get { return _lastmodifyuser; }
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
        public int? DeleteMark
        {
            set { _deletemark = value; }
            get { return _deletemark; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPOfficeSupplyStockInDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,StockInID,ReferenceID,ItemID,Name,Spec,Unit,Quantity,Price,Total,Supplier,SupplierID,Comment,StockInTime,StockInUser,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark ");
            strSql.Append(" FROM [ERPOfficeSupplyStockInDetail] ");
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
                if (ds.Tables[0].Rows[0]["StockInID"] != null && ds.Tables[0].Rows[0]["StockInID"].ToString() != "")
                {
                    this.StockInID = int.Parse(ds.Tables[0].Rows[0]["StockInID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReferenceID"] != null && ds.Tables[0].Rows[0]["ReferenceID"].ToString() != "")
                {
                    this.ReferenceID = int.Parse(ds.Tables[0].Rows[0]["ReferenceID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemID"] != null && ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    this.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spec"] != null)
                {
                    this.Spec = ds.Tables[0].Rows[0]["Spec"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Unit"] != null)
                {
                    this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Quantity"] != null && ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    this.Quantity = int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    this.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Total"] != null && ds.Tables[0].Rows[0]["Total"].ToString() != "")
                {
                    this.Total = decimal.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Supplier"] != null)
                {
                    this.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SupplierID"] != null && ds.Tables[0].Rows[0]["SupplierID"].ToString() != "")
                {
                    this.SupplierID = int.Parse(ds.Tables[0].Rows[0]["SupplierID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StockInTime"] != null && ds.Tables[0].Rows[0]["StockInTime"].ToString() != "")
                {
                    this.StockInTime = DateTime.Parse(ds.Tables[0].Rows[0]["StockInTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StockInUser"] != null)
                {
                    this.StockInUser = ds.Tables[0].Rows[0]["StockInUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
                {
                    this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorUser"] != null)
                {
                    this.CreatorUser = ds.Tables[0].Rows[0]["CreatorUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastModifyTime"] != null && ds.Tables[0].Rows[0]["LastModifyTime"].ToString() != "")
                {
                    this.LastModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifyUser"] != null)
                {
                    this.LastModifyUser = ds.Tables[0].Rows[0]["LastModifyUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
                {
                    this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
                {
                    this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPOfficeSupplyStockInDetail]");
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
            strSql.Append("insert into [ERPOfficeSupplyStockInDetail] (");
            strSql.Append("StockInID,ReferenceID,ItemID,Name,Spec,Unit,Quantity,Price,Total,Supplier,SupplierID,Comment,StockInTime,StockInUser,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@StockInID,@ReferenceID,@ItemID,@Name,@Spec,@Unit,@Quantity,@Price,@Total,@Supplier,@SupplierID,@Comment,@StockInTime,@StockInUser,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@StockInID", SqlDbType.Int,4),
                    new SqlParameter("@ReferenceID", SqlDbType.Int,4),
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,1),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
                    new SqlParameter("@Supplier", SqlDbType.NVarChar,1000),
                    new SqlParameter("@SupplierID", SqlDbType.Int,4),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,1000),
                    new SqlParameter("@StockInTime", SqlDbType.DateTime),
                    new SqlParameter("@StockInUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4)};
            parameters[0].Value = StockInID;
            parameters[1].Value = ReferenceID;
            parameters[2].Value = ItemID;
            parameters[3].Value = Name;
            parameters[4].Value = Spec;
            parameters[5].Value = Unit;
            parameters[6].Value = Quantity;
            parameters[7].Value = Price;
            parameters[8].Value = Total;
            parameters[9].Value = Supplier;
            parameters[10].Value = SupplierID;
            parameters[11].Value = Comment;
            parameters[12].Value = StockInTime;
            parameters[13].Value = StockInUser;
            parameters[14].Value = CreatorTime;
            parameters[15].Value = CreatorUser;
            parameters[16].Value = LastModifyTime;
            parameters[17].Value = LastModifyUser;
            parameters[18].Value = DeleteTime;
            parameters[19].Value = DeleteUser;
            parameters[20].Value = DeleteMark;

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
            strSql.Append("update [ERPOfficeSupplyStockInDetail] set ");
            strSql.Append("StockInID=@StockInID,");
            strSql.Append("ReferenceID=@ReferenceID,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Spec=@Spec,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("Price=@Price,");
            strSql.Append("Total=@Total,");
            strSql.Append("Supplier=@Supplier,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("StockInTime=@StockInTime,");
            strSql.Append("StockInUser=@StockInUser,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("CreatorUser=@CreatorUser,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@StockInID", SqlDbType.Int,4),
                    new SqlParameter("@ReferenceID", SqlDbType.Int,4),
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,1),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
                    new SqlParameter("@Supplier", SqlDbType.NVarChar,1000),
                    new SqlParameter("@SupplierID", SqlDbType.Int,4),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,1000),
                    new SqlParameter("@StockInTime", SqlDbType.DateTime),
                    new SqlParameter("@StockInUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = StockInID;
            parameters[1].Value = ReferenceID;
            parameters[2].Value = ItemID;
            parameters[3].Value = Name;
            parameters[4].Value = Spec;
            parameters[5].Value = Unit;
            parameters[6].Value = Quantity;
            parameters[7].Value = Price;
            parameters[8].Value = Total;
            parameters[9].Value = Supplier;
            parameters[10].Value = SupplierID;
            parameters[11].Value = Comment;
            parameters[12].Value = StockInTime;
            parameters[13].Value = StockInUser;
            parameters[14].Value = CreatorTime;
            parameters[15].Value = CreatorUser;
            parameters[16].Value = LastModifyTime;
            parameters[17].Value = LastModifyUser;
            parameters[18].Value = DeleteTime;
            parameters[19].Value = DeleteUser;
            parameters[20].Value = DeleteMark;
            parameters[21].Value = ID;

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
            strSql.Append("delete from [ERPOfficeSupplyStockInDetail] ");
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
            strSql.Append("select ID,StockInID,ReferenceID,ItemID,Name,Spec,Unit,Quantity,Price,Total,Supplier,SupplierID,Comment,StockInTime,StockInUser,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark ");
            strSql.Append(" FROM [ERPOfficeSupplyStockInDetail] ");
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
                if (ds.Tables[0].Rows[0]["StockInID"] != null && ds.Tables[0].Rows[0]["StockInID"].ToString() != "")
                {
                    this.StockInID = int.Parse(ds.Tables[0].Rows[0]["StockInID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReferenceID"] != null && ds.Tables[0].Rows[0]["ReferenceID"].ToString() != "")
                {
                    this.ReferenceID = int.Parse(ds.Tables[0].Rows[0]["ReferenceID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemID"] != null && ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    this.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spec"] != null)
                {
                    this.Spec = ds.Tables[0].Rows[0]["Spec"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Unit"] != null)
                {
                    this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Quantity"] != null && ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    this.Quantity = int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    this.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Total"] != null && ds.Tables[0].Rows[0]["Total"].ToString() != "")
                {
                    this.Total = decimal.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Supplier"] != null)
                {
                    this.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SupplierID"] != null && ds.Tables[0].Rows[0]["SupplierID"].ToString() != "")
                {
                    this.SupplierID = int.Parse(ds.Tables[0].Rows[0]["SupplierID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StockInTime"] != null && ds.Tables[0].Rows[0]["StockInTime"].ToString() != "")
                {
                    this.StockInTime = DateTime.Parse(ds.Tables[0].Rows[0]["StockInTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StockInUser"] != null)
                {
                    this.StockInUser = ds.Tables[0].Rows[0]["StockInUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
                {
                    this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorUser"] != null)
                {
                    this.CreatorUser = ds.Tables[0].Rows[0]["CreatorUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastModifyTime"] != null && ds.Tables[0].Rows[0]["LastModifyTime"].ToString() != "")
                {
                    this.LastModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifyUser"] != null)
                {
                    this.LastModifyUser = ds.Tables[0].Rows[0]["LastModifyUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
                {
                    this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
                {
                    this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
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
            strSql.Append(" FROM [ERPOfficeSupplyStockInDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

