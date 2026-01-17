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
    /// 类ERPKeyValue。
    /// </summary>
    [Serializable]
    public partial class ERPKeyValue
    {
        public ERPKeyValue()
        { }
        #region Model
        private int _id;
        private string _category;
        private string _key1;
        private string _key2;
        private string _key3;
        private string _value1;
        private string _value2;
        private string _value3;
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
        public string Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key1
        {
            set { _key1 = value; }
            get { return _key1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key2
        {
            set { _key2 = value; }
            get { return _key2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key3
        {
            set { _key3 = value; }
            get { return _key3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value1
        {
            set { _value1 = value; }
            get { return _value1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value2
        {
            set { _value2 = value; }
            get { return _value2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value3
        {
            set { _value3 = value; }
            get { return _value3; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPKeyValue(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Category,Key1,Key2,Key3,Value1,Value2,Value3 ");
            strSql.Append(" FROM [ERPKeyValue] ");
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
                if (ds.Tables[0].Rows[0]["Category"] != null)
                {
                    this.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key1"] != null)
                {
                    this.Key1 = ds.Tables[0].Rows[0]["Key1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key2"] != null)
                {
                    this.Key2 = ds.Tables[0].Rows[0]["Key2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key3"] != null)
                {
                    this.Key3 = ds.Tables[0].Rows[0]["Key3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value1"] != null)
                {
                    this.Value1 = ds.Tables[0].Rows[0]["Value1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value2"] != null)
                {
                    this.Value2 = ds.Tables[0].Rows[0]["Value2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value3"] != null)
                {
                    this.Value3 = ds.Tables[0].Rows[0]["Value3"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPKeyValue]");
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
            strSql.Append("insert into [ERPKeyValue] (");
            strSql.Append("Category,Key1,Key2,Key3,Value1,Value2,Value3)");
            strSql.Append(" values (");
            strSql.Append("@Category,@Key1,@Key2,@Key3,@Value1,@Value2,@Value3)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Category", SqlDbType.NVarChar,50),
                    new SqlParameter("@Key1", SqlDbType.NVarChar,50),
                    new SqlParameter("@Key2", SqlDbType.NVarChar,50),
                    new SqlParameter("@Key3", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value1", SqlDbType.NVarChar,500),
                    new SqlParameter("@Value2", SqlDbType.NVarChar,500),
                    new SqlParameter("@Value3", SqlDbType.NVarChar,500)};
            parameters[0].Value = Category;
            parameters[1].Value = Key1;
            parameters[2].Value = Key2;
            parameters[3].Value = Key3;
            parameters[4].Value = Value1;
            parameters[5].Value = Value2;
            parameters[6].Value = Value3;

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
            strSql.Append("update [ERPKeyValue] set ");
            strSql.Append("Category=@Category,");
            strSql.Append("Key1=@Key1,");
            strSql.Append("Key2=@Key2,");
            strSql.Append("Key3=@Key3,");
            strSql.Append("Value1=@Value1,");
            strSql.Append("Value2=@Value2,");
            strSql.Append("Value3=@Value3");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Category", SqlDbType.NVarChar,500),
                    new SqlParameter("@Key1", SqlDbType.NVarChar,500),
                    new SqlParameter("@Key2", SqlDbType.NVarChar,500),
                    new SqlParameter("@Key3", SqlDbType.NVarChar,500),
                    new SqlParameter("@Value1", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Value2", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Value3", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = Category;
            parameters[1].Value = Key1;
            parameters[2].Value = Key2;
            parameters[3].Value = Key3;
            parameters[4].Value = Value1;
            parameters[5].Value = Value2;
            parameters[6].Value = Value3;
            parameters[7].Value = ID;

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
            strSql.Append("delete from [ERPKeyValue] ");
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
            strSql.Append("select ID,Category,Key1,Key2,Key3,Value1,Value2,Value3 ");
            strSql.Append(" FROM [ERPKeyValue] ");
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
                if (ds.Tables[0].Rows[0]["Category"] != null)
                {
                    this.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key1"] != null)
                {
                    this.Key1 = ds.Tables[0].Rows[0]["Key1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key2"] != null)
                {
                    this.Key2 = ds.Tables[0].Rows[0]["Key2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Key3"] != null)
                {
                    this.Key3 = ds.Tables[0].Rows[0]["Key3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value1"] != null)
                {
                    this.Value1 = ds.Tables[0].Rows[0]["Value1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value2"] != null)
                {
                    this.Value2 = ds.Tables[0].Rows[0]["Value2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value3"] != null)
                {
                    this.Value3 = ds.Tables[0].Rows[0]["Value3"].ToString();
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZWL.BLL.ERPKeyValue GetModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return PublicMethod.ConvertToModel<ZWL.BLL.ERPKeyValue>(ds.Tables[0]);
            }

            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ZWL.BLL.ERPKeyValue> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPKeyValue>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPKeyValue>(item));
                }
            }

            return result;
        }
        public ZWL.BLL.ERPKeyValue GetTableDepartmentWorkToDoRelationShip(int formid)
        {
            var list = GetModelList("Category='TableDepartmentWorkToDoRelationShip' and Value1<>'' and Key2='" + formid + "'");
            if (list != null && list.Count > 0)
                return list[0];
            return null;
        }
        public IList<ZWL.BLL.ERPKeyValue> GetTableDepartmentWorkToDoRelationShip()
        {
            return GetModelList("Category='TableDepartmentWorkToDoRelationShip' and Value1<>''");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPKeyValue] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize, string orderby = "ID Desc")
        {
            var strSql = "select * from  ERPKeyValue";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            if (orderby != "")
                return new Pager(strSql, cPage, pSize, orderby);
            else
                return new Pager(strSql, cPage, pSize, "ID Desc");
        }

        #endregion  Method
    }
}

