using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类FanTangJiuCanSummary。
	/// </summary>
	[Serializable]
    public partial class FanTangJiuCanSummary
    {
        public FanTangJiuCanSummary()
        { }
        #region Model
        private int _id;
        private string _lotid;
        private string _number;
        private string _dept;
        private string _name;
        private int _zaocan;
        private int _wucan;
        private int _zaocan1;
        private int _wucan1;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotID
        {
            set { _lotid = value; }
            get { return _lotid; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept
        {
            set { _dept = value; }
            get { return _dept; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 早餐
        /// </summary>
        public int ZaoCan
        {
            set { _zaocan = value; }
            get { return _zaocan; }
        }
        /// <summary>
        /// 午餐
        /// </summary>
        public int WuCan
        {
            set { _wucan = value; }
            get { return _wucan; }
        }
        /// <summary>
        /// 早餐出差
        /// </summary>
        public int ZaoCan1
        {
            set { _zaocan1 = value; }
            get { return _zaocan1; }
        }
        /// <summary>
        /// 午餐出差
        /// </summary>
        public int WuCan1
        {
            set { _wucan1 = value; }
            get { return _wucan1; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FanTangJiuCanSummary(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LotID,Number,Dept,Name,ZaoCan,WuCan,ZaoCan1,WuCan1 ");
            strSql.Append(" FROM [FanTangJiuCanSummary] ");
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
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dept"] != null)
                {
                    this.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaoCan"] != null && ds.Tables[0].Rows[0]["ZaoCan"].ToString() != "")
                {
                    this.ZaoCan = int.Parse(ds.Tables[0].Rows[0]["ZaoCan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WuCan"] != null && ds.Tables[0].Rows[0]["WuCan"].ToString() != "")
                {
                    this.WuCan = int.Parse(ds.Tables[0].Rows[0]["WuCan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZaoCan1"] != null && ds.Tables[0].Rows[0]["ZaoCan1"].ToString() != "")
                {
                    this.ZaoCan1 = int.Parse(ds.Tables[0].Rows[0]["ZaoCan1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WuCan1"] != null && ds.Tables[0].Rows[0]["WuCan1"].ToString() != "")
                {
                    this.WuCan1 = int.Parse(ds.Tables[0].Rows[0]["WuCan1"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "FanTangJiuCanSummary");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [FanTangJiuCanSummary]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [FanTangJiuCanSummary] (");
            strSql.Append("ID,LotID,Number,Dept,Name,ZaoCan,WuCan,ZaoCan1,WuCan1)");
            strSql.Append(" values (");
            strSql.Append("@ID,@LotID,@Number,@Dept,@Name,@ZaoCan,@WuCan,@ZaoCan1,@WuCan1)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZaoCan", SqlDbType.Int,4),
                    new SqlParameter("@WuCan", SqlDbType.Int,4),
                    new SqlParameter("@ZaoCan1", SqlDbType.Int,4),
                    new SqlParameter("@WuCan1", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            parameters[1].Value = LotID;
            parameters[2].Value = Number;
            parameters[3].Value = Dept;
            parameters[4].Value = Name;
            parameters[5].Value = ZaoCan;
            parameters[6].Value = WuCan;
            parameters[7].Value = ZaoCan1;
            parameters[8].Value = WuCan1;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [FanTangJiuCanSummary] set ");
            strSql.Append("LotID=@LotID,");
            strSql.Append("Number=@Number,");
            strSql.Append("Dept=@Dept,");
            strSql.Append("Name=@Name,");
            strSql.Append("ZaoCan=@ZaoCan,");
            strSql.Append("WuCan=@WuCan,");
            strSql.Append("ZaoCan1=@ZaoCan1,");
            strSql.Append("WuCan1=@WuCan1");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZaoCan", SqlDbType.Int,4),
                    new SqlParameter("@WuCan", SqlDbType.Int,4),
                    new SqlParameter("@ZaoCan1", SqlDbType.Int,4),
                    new SqlParameter("@WuCan1", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LotID;
            parameters[1].Value = Number;
            parameters[2].Value = Dept;
            parameters[3].Value = Name;
            parameters[4].Value = ZaoCan;
            parameters[5].Value = WuCan;
            parameters[6].Value = ZaoCan1;
            parameters[7].Value = WuCan1;
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
            strSql.Append("delete from [FanTangJiuCanSummary] ");
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
            strSql.Append("select ID,LotID,Number,Dept,Name,ZaoCan,WuCan,ZaoCan1,WuCan1 ");
            strSql.Append(" FROM [FanTangJiuCanSummary] ");
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
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dept"] != null)
                {
                    this.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaoCan"] != null && ds.Tables[0].Rows[0]["ZaoCan"].ToString() != "")
                {
                    this.ZaoCan = int.Parse(ds.Tables[0].Rows[0]["ZaoCan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WuCan"] != null && ds.Tables[0].Rows[0]["WuCan"].ToString() != "")
                {
                    this.WuCan = int.Parse(ds.Tables[0].Rows[0]["WuCan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZaoCan1"] != null && ds.Tables[0].Rows[0]["ZaoCan1"].ToString() != "")
                {
                    this.ZaoCan1 = int.Parse(ds.Tables[0].Rows[0]["ZaoCan1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WuCan1"] != null && ds.Tables[0].Rows[0]["WuCan1"].ToString() != "")
                {
                    this.WuCan1 = int.Parse(ds.Tables[0].Rows[0]["WuCan1"].ToString());
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
            strSql.Append(" FROM [FanTangJiuCanSummary] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from FanTangJiuCanSummary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}
