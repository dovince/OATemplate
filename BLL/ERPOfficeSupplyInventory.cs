using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPOfficeSupplyInventory。
    /// </summary>
    [Serializable]
    public partial class ERPOfficeSupplyInventory
    {
        public ERPOfficeSupplyInventory()
        { }
        #region Model
        private int _id;
        private int? _itemid;
        private string _name;
        private string _spec;
        private string _unit;
        private int _quantity;
        private int _lockedinquantity;
        private int _lockedoutquantity;
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
        public int LockedInQuantity
        {
            set { _lockedinquantity = value; }
            get { return _lockedinquantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LockedOutQuantity
        {
            set { _lockedoutquantity = value; }
            get { return _lockedoutquantity; }
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
        public ERPOfficeSupplyInventory(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ItemID,Name,Spec,Unit,Quantity,LockedInQuantity,LockedOutQuantity,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark ");
            strSql.Append(" FROM [ERPOfficeSupplyInventory] ");
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
                if (ds.Tables[0].Rows[0]["LockedInQuantity"] != null && ds.Tables[0].Rows[0]["LockedInQuantity"].ToString() != "")
                {
                    this.LockedInQuantity = int.Parse(ds.Tables[0].Rows[0]["LockedInQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LockedOutQuantity"] != null && ds.Tables[0].Rows[0]["LockedOutQuantity"].ToString() != "")
                {
                    this.LockedOutQuantity = int.Parse(ds.Tables[0].Rows[0]["LockedOutQuantity"].ToString());
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
            strSql.Append("select count(1) from [ERPOfficeSupplyInventory]");
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
            strSql.Append("insert into [ERPOfficeSupplyInventory] (");
            strSql.Append("ItemID,Name,Spec,Unit,Quantity,LockedInQuantity,LockedOutQuantity,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@ItemID,@Name,@Spec,@Unit,@Quantity,@LockedInQuantity,@LockedOutQuantity,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,1),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@LockedInQuantity", SqlDbType.Int,4),
                    new SqlParameter("@LockedOutQuantity", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4)};
            parameters[0].Value = ItemID;
            parameters[1].Value = Name;
            parameters[2].Value = Spec;
            parameters[3].Value = Unit;
            parameters[4].Value = Quantity;
            parameters[5].Value = LockedInQuantity;
            parameters[6].Value = LockedOutQuantity;
            parameters[7].Value = CreatorTime;
            parameters[8].Value = CreatorUser;
            parameters[9].Value = LastModifyTime;
            parameters[10].Value = LastModifyUser;
            parameters[11].Value = DeleteTime;
            parameters[12].Value = DeleteUser;
            parameters[13].Value = DeleteMark;

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
            strSql.Append("update [ERPOfficeSupplyInventory] set ");
            strSql.Append("ItemID=@ItemID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Spec=@Spec,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("LockedInQuantity=@LockedInQuantity,");
            strSql.Append("LockedOutQuantity=@LockedOutQuantity,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("CreatorUser=@CreatorUser,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ItemID", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,1),
                    new SqlParameter("@Quantity", SqlDbType.Int,4),
                    new SqlParameter("@LockedInQuantity", SqlDbType.Int,4),
                    new SqlParameter("@LockedOutQuantity", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ItemID;
            parameters[1].Value = Name;
            parameters[2].Value = Spec;
            parameters[3].Value = Unit;
            parameters[4].Value = Quantity;
            parameters[5].Value = LockedInQuantity;
            parameters[6].Value = LockedOutQuantity;
            parameters[7].Value = CreatorTime;
            parameters[8].Value = CreatorUser;
            parameters[9].Value = LastModifyTime;
            parameters[10].Value = LastModifyUser;
            parameters[11].Value = DeleteTime;
            parameters[12].Value = DeleteUser;
            parameters[13].Value = DeleteMark;
            parameters[14].Value = ID;

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
            strSql.Append("delete from [ERPOfficeSupplyInventory] ");
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
            strSql.Append("select ID,ItemID,Name,Spec,Unit,Quantity,LockedInQuantity,LockedOutQuantity,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark ");
            strSql.Append(" FROM [ERPOfficeSupplyInventory] ");
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
                if (ds.Tables[0].Rows[0]["LockedInQuantity"] != null && ds.Tables[0].Rows[0]["LockedInQuantity"].ToString() != "")
                {
                    this.LockedInQuantity = int.Parse(ds.Tables[0].Rows[0]["LockedInQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LockedOutQuantity"] != null && ds.Tables[0].Rows[0]["LockedOutQuantity"].ToString() != "")
                {
                    this.LockedOutQuantity = int.Parse(ds.Tables[0].Rows[0]["LockedOutQuantity"].ToString());
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
            strSql.Append(" FROM [ERPOfficeSupplyInventory] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

