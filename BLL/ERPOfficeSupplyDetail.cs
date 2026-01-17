using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPOfficeSupplyDetail。
	/// </summary>
	[Serializable]
    public partial class ERPOfficeSupplyDetail
    {
        public ERPOfficeSupplyDetail()
        { }
        #region Model
        private int _id;
        private int _supplyid;
        private int? _itemid;
        private string _name;
        private string _model;
        private int _quantity;
        private string _unit;
        private decimal? _price;
        private decimal? _total;
        private string _state;
        private string _comment;
        private string _reservedfield1;
        private string _reservedfield2;
        private string _reservedfield3;
        private DateTime _creatortime;
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
        public int SupplyID
        {
            set { _supplyid = value; }
            get { return _supplyid; }
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
        public string Model
        {
            set { _model = value; }
            get { return _model; }
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
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
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
        public string ReservedField1
        {
            set { _reservedfield1 = value; }
            get { return _reservedfield1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReservedField2
        {
            set { _reservedfield2 = value; }
            get { return _reservedfield2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReservedField3
        {
            set { _reservedfield3 = value; }
            get { return _reservedfield3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatorTime
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

        #region  relative model
        public ZWL.BLL.ERPOfficeSupply CurrentOfficeSupply()
        {
            var _currentprocost = new ZWL.BLL.ERPOfficeSupply();
            if (this.SupplyID > 0)
            {
                _currentprocost.GetModel(this.SupplyID);
            }
            return _currentprocost;
        }
        #endregion relative model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPOfficeSupplyDetail(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPOfficeSupplyDetail] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPOfficeSupplyDetail]");
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
            strSql.Append("insert into [ERPOfficeSupplyDetail] (");
            strSql.Append("SupplyID,ItemID,Name,Model,Quantity,Unit,Price,Total,State,Comment,ReservedField1,ReservedField2,ReservedField3,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@SupplyID,@ItemID,@Name,@Model,@Quantity,@Unit,@Price,@Total,@State,@Comment,@ReservedField1,@ReservedField2,@ReservedField3,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplyID", SqlDbType.Int,4),
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@Model", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,10),
                    new SqlParameter("@Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
                    new SqlParameter("@State", SqlDbType.NVarChar,10),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField1", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField2", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField3", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4)};
            parameters[0].Value = SupplyID;
            parameters[1].Value = ItemID;
            parameters[2].Value = Name;
            parameters[3].Value = Model;
            parameters[4].Value = Quantity;
            parameters[5].Value = Unit;
            parameters[6].Value = Price;
            parameters[7].Value = Total;
            parameters[8].Value = State;
            parameters[9].Value = Comment;
            parameters[10].Value = ReservedField1;
            parameters[11].Value = ReservedField2;
            parameters[12].Value = ReservedField3;
            parameters[13].Value = CreatorTime;
            parameters[14].Value = CreatorUser;
            parameters[15].Value = LastModifyTime;
            parameters[16].Value = LastModifyUser;
            parameters[17].Value = DeleteTime;
            parameters[18].Value = DeleteUser;
            parameters[19].Value = DeleteMark;

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
            strSql.Append("update [ERPOfficeSupplyDetail] set ");
            strSql.Append("SupplyID=@SupplyID,");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Model=@Model,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Price=@Price,");
            strSql.Append("Total=@Total,");
            strSql.Append("State=@State,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("ReservedField1=@ReservedField1,");
            strSql.Append("ReservedField2=@ReservedField2,");
            strSql.Append("ReservedField3=@ReservedField3,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("CreatorUser=@CreatorUser,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplyID", SqlDbType.Int,4),
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@Model", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,10),
                    new SqlParameter("@Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
                    new SqlParameter("@State", SqlDbType.NVarChar,10),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField1", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField2", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField3", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = SupplyID;
            parameters[1].Value = ItemID;
            parameters[2].Value = Name;
            parameters[3].Value = Model;
            parameters[4].Value = Quantity;
            parameters[5].Value = Unit;
            parameters[6].Value = Price;
            parameters[7].Value = Total;
            parameters[8].Value = State;
            parameters[9].Value = Comment;
            parameters[10].Value = ReservedField1;
            parameters[11].Value = ReservedField2;
            parameters[12].Value = ReservedField3;
            parameters[13].Value = CreatorTime;
            parameters[14].Value = CreatorUser;
            parameters[15].Value = LastModifyTime;
            parameters[16].Value = LastModifyUser;
            parameters[17].Value = DeleteTime;
            parameters[18].Value = DeleteUser;
            parameters[19].Value = DeleteMark;
            parameters[20].Value = ID;

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
            strSql.Append("delete from [ERPOfficeSupplyDetail] ");
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
            strSql.Append(" FROM [ERPOfficeSupplyDetail] ");
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
                if (ds.Tables[0].Rows[0]["SupplyID"] != null && ds.Tables[0].Rows[0]["SupplyID"].ToString() != "")
                {
                    this.SupplyID = int.Parse(ds.Tables[0].Rows[0]["SupplyID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemID"] != null && ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    this.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Model"] != null)
                {
                    this.Model = ds.Tables[0].Rows[0]["Model"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Quantity"] != null && ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    this.Quantity = int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Unit"] != null)
                {
                    this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    this.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Total"] != null && ds.Tables[0].Rows[0]["Total"].ToString() != "")
                {
                    this.Total = decimal.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField1"] != null)
                {
                    this.ReservedField1 = ds.Tables[0].Rows[0]["ReservedField1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField2"] != null)
                {
                    this.ReservedField2 = ds.Tables[0].Rows[0]["ReservedField2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField3"] != null)
                {
                    this.ReservedField3 = ds.Tables[0].Rows[0]["ReservedField3"].ToString();
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

        public ZWL.BLL.ERPOfficeSupplyDetail GetModelBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPOfficeSupplyDetail>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        public List<ZWL.BLL.ERPOfficeSupplyDetail> GetListBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPOfficeSupplyDetail>(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPOfficeSupplyDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetPagingList(string strWhere, int cPage, int pSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM (
	                                select d.*,isnull((select top 1 t.Category from ERPOfficeSupplyItem i LEFT JOIN ERPOfficeSupplyType t on i.CategoryID=t.ID where i.ID=d.ItemID),'未分类') Type,
	                                s.No,s.Operator,s.Department,s.CreatedDate,s.NWorkID,s.Form,t.FormID,t.WorkFlowID,t.JieDianName,t.StateNow 
	                                FROM [ERPOfficeSupplyDetail] d join [ERPOfficeSupply] s
	                                on d.SupplyID=s.ID
	                                join [ERPNWorkToDo] t on s.NWorkID=t.ID	
                                ) m");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, " State asc,ID desc");
        }
        #endregion  Method
    }
}
