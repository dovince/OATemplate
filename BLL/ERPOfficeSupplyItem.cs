using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPOfficeSupplyItem。
	/// </summary>
	[Serializable]
    public partial class ERPOfficeSupplyItem
    {
        public ERPOfficeSupplyItem()
        { }
        #region Model
        private int _id;
        private int _categoryid;
        private string _encode;
        private string _itemname;
        private string _spec;
        private string _unitprice;
        private string _description;
        private int? _sortcode;
        private int? _enabledmark;
        private DateTime _creatortime;
        private string _creatoruser;
        private DateTime? _lastmodifytime;
        private string _lastmodifyuser;
        private DateTime? _deletetime;
        private string _deleteuser;
        private int? _deletemark;
        private string _unit;
        private int? _unitid;
        private string _supplier;
        private int? _supplierid;
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
        public int CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Encode
        {
            set { _encode = value; }
            get { return _encode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
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
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortCode
        {
            set { _sortcode = value; }
            get { return _sortcode; }
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
        public int? UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
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
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPOfficeSupplyItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPOfficeSupplyItem] ");
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
            strSql.Append("select count(1) from [ERPOfficeSupplyItem]");
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
            strSql.Append("insert into [ERPOfficeSupplyItem] (");
            strSql.Append("CategoryID,Encode,ItemName,Spec,UnitPrice,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark,Unit,UnitID,Supplier,SupplierID)");
            strSql.Append(" values (");
            strSql.Append("@CategoryID,@Encode,@ItemName,@Spec,@UnitPrice,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark,@Unit,@UnitID,@Supplier,@SupplierID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryID", SqlDbType.Int,4),
                    new SqlParameter("@Encode", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,500),
                    new SqlParameter("@UnitPrice", SqlDbType.VarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,50),
                    new SqlParameter("@SortCode", SqlDbType.Int,4),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,50),
                    new SqlParameter("@UnitID", SqlDbType.Int,4),
                    new SqlParameter("@Supplier", SqlDbType.VarChar,255),
                    new SqlParameter("@SupplierID", SqlDbType.Int,4)};
            parameters[0].Value = CategoryID;
            parameters[1].Value = Encode;
            parameters[2].Value = ItemName;
            parameters[3].Value = Spec;
            parameters[4].Value = UnitPrice;
            parameters[5].Value = Description;
            parameters[6].Value = SortCode;
            parameters[7].Value = EnabledMark;
            parameters[8].Value = CreatorTime;
            parameters[9].Value = CreatorUser;
            parameters[10].Value = LastModifyTime;
            parameters[11].Value = LastModifyUser;
            parameters[12].Value = DeleteTime;
            parameters[13].Value = DeleteUser;
            parameters[14].Value = DeleteMark;
            parameters[15].Value = Unit;
            parameters[16].Value = UnitID;
            parameters[17].Value = Supplier;
            parameters[18].Value = SupplierID;

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
            strSql.Append("update [ERPOfficeSupplyItem] set ");
            strSql.Append("CategoryID=@CategoryID,");
            strSql.Append("Encode=@Encode,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("Spec=@Spec,");
            strSql.Append("UnitPrice=@UnitPrice,");
            strSql.Append("Description=@Description,");
            strSql.Append("SortCode=@SortCode,");
            strSql.Append("EnabledMark=@EnabledMark,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("CreatorUser=@CreatorUser,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("DeleteMark=@DeleteMark,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("UnitID=@UnitID,");
            strSql.Append("Supplier=@Supplier,");
            strSql.Append("SupplierID=@SupplierID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryID", SqlDbType.Int,4),
                    new SqlParameter("@Encode", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Spec", SqlDbType.NVarChar,500),
                    new SqlParameter("@UnitPrice", SqlDbType.VarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,50),
                    new SqlParameter("@SortCode", SqlDbType.Int,4),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@Unit", SqlDbType.NVarChar,50),
                    new SqlParameter("@UnitID", SqlDbType.Int,4),
                    new SqlParameter("@Supplier", SqlDbType.VarChar,255),
                    new SqlParameter("@SupplierID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = CategoryID;
            parameters[1].Value = Encode;
            parameters[2].Value = ItemName;
            parameters[3].Value = Spec;
            parameters[4].Value = UnitPrice;
            parameters[5].Value = Description;
            parameters[6].Value = SortCode;
            parameters[7].Value = EnabledMark;
            parameters[8].Value = CreatorTime;
            parameters[9].Value = CreatorUser;
            parameters[10].Value = LastModifyTime;
            parameters[11].Value = LastModifyUser;
            parameters[12].Value = DeleteTime;
            parameters[13].Value = DeleteUser;
            parameters[14].Value = DeleteMark;
            parameters[15].Value = Unit;
            parameters[16].Value = UnitID;
            parameters[17].Value = Supplier;
            parameters[18].Value = SupplierID;
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
            strSql.Append("delete from [ERPOfficeSupplyItem] ");
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
        public void GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPOfficeSupplyItem] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

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
                if (ds.Tables[0].Rows[0]["CategoryID"] != null && ds.Tables[0].Rows[0]["CategoryID"].ToString() != "")
                {
                    this.CategoryID = int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Encode"] != null)
                {
                    this.Encode = ds.Tables[0].Rows[0]["Encode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemName"] != null)
                {
                    this.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spec"] != null)
                {
                    this.Spec = ds.Tables[0].Rows[0]["Spec"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnitPrice"] != null)
                {
                    this.UnitPrice = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SortCode"] != null && ds.Tables[0].Rows[0]["SortCode"].ToString() != "")
                {
                    this.SortCode = int.Parse(ds.Tables[0].Rows[0]["SortCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
                {
                    this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
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
                if (ds.Tables[0].Rows[0]["Unit"] != null)
                {
                    this.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnitID"] != null && ds.Tables[0].Rows[0]["UnitID"].ToString() != "")
                {
                    this.UnitID = int.Parse(ds.Tables[0].Rows[0]["UnitID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Supplier"] != null)
                {
                    this.Supplier = ds.Tables[0].Rows[0]["Supplier"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SupplierID"] != null && ds.Tables[0].Rows[0]["SupplierID"].ToString() != "")
                {
                    this.SupplierID = int.Parse(ds.Tables[0].Rows[0]["SupplierID"].ToString());
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
            strSql.Append(" FROM [ERPOfficeSupplyItem] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPOfficeSupplyItem> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPOfficeSupplyItem>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPOfficeSupplyItem>(ds.Tables[0]);
            }
            return result;
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (select i.*,t.Category from ERPOfficeSupplyItem i JOIN ERPOfficeSupplyType t on i.CategoryID=t.ID) t");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }

        #endregion  Method
    }
}

