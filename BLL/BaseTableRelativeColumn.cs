using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类BaseTableRelativeColumn。
    /// </summary>
    [Serializable]
    public partial class BaseTableRelativeColumn:ModelBase
    {
        public BaseTableRelativeColumn()
        { }
        #region Model
        private int _id;
        private string _ltable;
        private string _lc1;
        private string _lv1;
        private string _lc2;
        private string _lv2;
        private string _rtable;
        private string _rc1;
        private string _rv1;
        private string _rc2;
        private string _rv2;
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
        public string LTable
        {
            set { _ltable = value; }
            get { return _ltable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LC1
        {
            set { _lc1 = value; }
            get { return _lc1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LV1
        {
            set { _lv1 = value; }
            get { return _lv1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LC2
        {
            set { _lc2 = value; }
            get { return _lc2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LV2
        {
            set { _lv2 = value; }
            get { return _lv2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RTable
        {
            set { _rtable = value; }
            get { return _rtable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RC1
        {
            set { _rc1 = value; }
            get { return _rc1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RV1
        {
            set { _rv1 = value; }
            get { return _rv1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RC2
        {
            set { _rc2 = value; }
            get { return _rc2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RV2
        {
            set { _rv2 = value; }
            get { return _rv2; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseTableRelativeColumn(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [BaseTableRelativeColumn] ");
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
                if (ds.Tables[0].Rows[0]["LTable"] != null)
                {
                    this.LTable = ds.Tables[0].Rows[0]["LTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LC1"] != null)
                {
                    this.LC1 = ds.Tables[0].Rows[0]["LC1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LV1"] != null)
                {
                    this.LV1 = ds.Tables[0].Rows[0]["LV1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LC2"] != null)
                {
                    this.LC2 = ds.Tables[0].Rows[0]["LC2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LV2"] != null)
                {
                    this.LV2 = ds.Tables[0].Rows[0]["LV2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RTable"] != null)
                {
                    this.RTable = ds.Tables[0].Rows[0]["RTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RC1"] != null)
                {
                    this.RC1 = ds.Tables[0].Rows[0]["RC1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RV1"] != null)
                {
                    this.RV1 = ds.Tables[0].Rows[0]["RV1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RC2"] != null)
                {
                    this.RC2 = ds.Tables[0].Rows[0]["RC2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RV2"] != null)
                {
                    this.RV2 = ds.Tables[0].Rows[0]["RV2"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [BaseTableRelativeColumn]");
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
            strSql.Append("insert into [BaseTableRelativeColumn] (");
            strSql.Append("LTable,LC1,LV1,LC2,LV2,RTable,RC1,RV1,RC2,RV2)");
            strSql.Append(" values (");
            strSql.Append("@LTable,@LC1,@LV1,@LC2,@LV2,@RTable,@RC1,@RV1,@RC2,@RV2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LTable", SqlDbType.VarChar,100),
                    new SqlParameter("@LC1", SqlDbType.VarChar,100),
                    new SqlParameter("@LV1", SqlDbType.VarChar,100),
                    new SqlParameter("@LC2", SqlDbType.VarChar,100),
                    new SqlParameter("@LV2", SqlDbType.VarChar,100),
                    new SqlParameter("@RTable", SqlDbType.VarChar,100),
                    new SqlParameter("@RC1", SqlDbType.VarChar,100),
                    new SqlParameter("@RV1", SqlDbType.VarChar,100),
                    new SqlParameter("@RC2", SqlDbType.VarChar,100),
                    new SqlParameter("@RV2", SqlDbType.VarChar,100)};
            parameters[0].Value = LTable;
            parameters[1].Value = LC1;
            parameters[2].Value = LV1;
            parameters[3].Value = LC2;
            parameters[4].Value = LV2;
            parameters[5].Value = RTable;
            parameters[6].Value = RC1;
            parameters[7].Value = RV1;
            parameters[8].Value = RC2;
            parameters[9].Value = RV2;

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
            strSql.Append("update [BaseTableRelativeColumn] set ");
            strSql.Append("LTable=@LTable,");
            strSql.Append("LC1=@LC1,");
            strSql.Append("LV1=@LV1,");
            strSql.Append("LC2=@LC2,");
            strSql.Append("LV2=@LV2,");
            strSql.Append("RTable=@RTable,");
            strSql.Append("RC1=@RC1,");
            strSql.Append("RV1=@RV1,");
            strSql.Append("RC2=@RC2,");
            strSql.Append("RV2=@RV2");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LTable", SqlDbType.VarChar,100),
                    new SqlParameter("@LC1", SqlDbType.VarChar,100),
                    new SqlParameter("@LV1", SqlDbType.VarChar,100),
                    new SqlParameter("@LC2", SqlDbType.VarChar,100),
                    new SqlParameter("@LV2", SqlDbType.VarChar,100),
                    new SqlParameter("@RTable", SqlDbType.VarChar,100),
                    new SqlParameter("@RC1", SqlDbType.VarChar,100),
                    new SqlParameter("@RV1", SqlDbType.VarChar,100),
                    new SqlParameter("@RC2", SqlDbType.VarChar,100),
                    new SqlParameter("@RV2", SqlDbType.VarChar,100),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LTable;
            parameters[1].Value = LC1;
            parameters[2].Value = LV1;
            parameters[3].Value = LC2;
            parameters[4].Value = LV2;
            parameters[5].Value = RTable;
            parameters[6].Value = RC1;
            parameters[7].Value = RV1;
            parameters[8].Value = RC2;
            parameters[9].Value = RV2;
            parameters[10].Value = ID;

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
            strSql.Append("delete from [BaseTableRelativeColumn] ");
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
            strSql.Append(" FROM [BaseTableRelativeColumn] ");
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
                if (ds.Tables[0].Rows[0]["LTable"] != null)
                {
                    this.LTable = ds.Tables[0].Rows[0]["LTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LC1"] != null)
                {
                    this.LC1 = ds.Tables[0].Rows[0]["LC1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LV1"] != null)
                {
                    this.LV1 = ds.Tables[0].Rows[0]["LV1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LC2"] != null)
                {
                    this.LC2 = ds.Tables[0].Rows[0]["LC2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LV2"] != null)
                {
                    this.LV2 = ds.Tables[0].Rows[0]["LV2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RTable"] != null)
                {
                    this.RTable = ds.Tables[0].Rows[0]["RTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RC1"] != null)
                {
                    this.RC1 = ds.Tables[0].Rows[0]["RC1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RV1"] != null)
                {
                    this.RV1 = ds.Tables[0].Rows[0]["RV1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RC2"] != null)
                {
                    this.RC2 = ds.Tables[0].Rows[0]["RC2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RV2"] != null)
                {
                    this.RV2 = ds.Tables[0].Rows[0]["RV2"].ToString();
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
            strSql.Append(" FROM [BaseTableRelativeColumn] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

