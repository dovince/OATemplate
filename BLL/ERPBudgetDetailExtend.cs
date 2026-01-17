using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPBudgetDetailExtend。
	/// </summary>
	[Serializable]
    public partial class ERPBudgetDetailExtend
    {
        public ERPBudgetDetailExtend()
        { }
        #region Model
        private int _id;
        private int _refid;
        private string _type;
        private string _name;
        private decimal _value;
        private string _comment;
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
        public int RefID
        {
            set { _refid = value; }
            get { return _refid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
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
        public decimal Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPBudgetDetailExtend(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RefID,Type,Name,Value,Comment ");
            strSql.Append(" FROM [ERPBudgetDetailExtend] ");
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
                if (ds.Tables[0].Rows[0]["Type"] != null)
                {
                    this.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value"] != null && ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    this.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPBudgetDetailExtend]");
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
            strSql.Append("insert into [ERPBudgetDetailExtend] (");
            strSql.Append("RefID,Type,Name,Value,Comment)");
            strSql.Append(" values (");
            strSql.Append("@RefID,@Type,@Name,@Value,@Comment)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RefID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.Decimal,9),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,50)};
            parameters[0].Value = RefID;
            parameters[1].Value = Type;
            parameters[2].Value = Name;
            parameters[3].Value = Value;
            parameters[4].Value = Comment;

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
            strSql.Append("update [ERPBudgetDetailExtend] set ");
            strSql.Append("RefID=@RefID,");
            strSql.Append("Type=@Type,");
            strSql.Append("Name=@Name,");
            strSql.Append("Value=@Value,");
            strSql.Append("Comment=@Comment");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RefID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.Decimal,9),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = RefID;
            parameters[1].Value = Type;
            parameters[2].Value = Name;
            parameters[3].Value = Value;
            parameters[4].Value = Comment;
            parameters[5].Value = ID;

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
            strSql.Append("delete from [ERPBudgetDetailExtend] ");
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
            strSql.Append("select ID,RefID,Type,Name,Value,Comment ");
            strSql.Append(" FROM [ERPBudgetDetailExtend] ");
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
                if (ds.Tables[0].Rows[0]["Type"] != null)
                {
                    this.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Value"] != null && ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    this.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
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
            strSql.Append(" FROM [ERPBudgetDetailExtend] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

