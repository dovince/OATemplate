using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类FanTangJiuCanRecordReport。
    /// </summary>
    [Serializable]
    public partial class FanTangJiuCanRecordReport
    {
        public FanTangJiuCanRecordReport()
        { }
        #region Model
        private int _id;
        private string _lotid;
        private string _name;
        private string _dept;
        private DateTime? _recorddate;
        private string _xingqi;
        private string _ischuchai;
        private string _chuchaididian;
        private string _chuchaishiyou;
        private string _chuchainworkid;
        private string _zaocan;
        private string _wucan;
        private string _wancan;
        private string _received;
        private string _comment;
        private string _lastmodifyuser;
        private DateTime? _lastmodifytime;
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
        public string LotID
        {
            set { _lotid = value; }
            get { return _lotid; }
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
        public string Dept
        {
            set { _dept = value; }
            get { return _dept; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XingQi
        {
            set { _xingqi = value; }
            get { return _xingqi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsChuChai
        {
            set { _ischuchai = value; }
            get { return _ischuchai; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuChaiDiDian
        {
            set { _chuchaididian = value; }
            get { return _chuchaididian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuChaiShiYou
        {
            set { _chuchaishiyou = value; }
            get { return _chuchaishiyou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuChaiNWorkID
        {
            set { _chuchainworkid = value; }
            get { return _chuchainworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZaoCan
        {
            set { _zaocan = value; }
            get { return _zaocan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WuCan
        {
            set { _wucan = value; }
            get { return _wucan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WanCan
        {
            set { _wancan = value; }
            get { return _wancan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Received
        {
            set { _received = value; }
            get { return _received; }
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
        public string LastModifyUser
        {
            set { _lastmodifyuser = value; }
            get { return _lastmodifyuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FanTangJiuCanRecordReport(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [FanTangJiuCanRecordReport] ");
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
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [FanTangJiuCanRecordReport]");
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
            strSql.Append("insert into [FanTangJiuCanRecordReport] (");
            strSql.Append("LotID,Name,Dept,RecordDate,XingQi,IsChuChai,ChuChaiDiDian,ChuChaiShiYou,ChuChaiNWorkID,ZaoCan,WuCan,WanCan,Received,Comment,LastModifyUser,LastModifyTime)");
            strSql.Append(" values (");
            strSql.Append("@LotID,@Name,@Dept,@RecordDate,@XingQi,@IsChuChai,@ChuChaiDiDian,@ChuChaiShiYou,@ChuChaiNWorkID,@ZaoCan,@WuCan,@WanCan,@Received,@Comment,@LastModifyUser,@LastModifyTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,100),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,100),
                    new SqlParameter("@RecordDate", SqlDbType.DateTime),
                    new SqlParameter("@XingQi", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsChuChai", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ChuChaiShiYou", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ChuChaiNWorkID", SqlDbType.VarChar,1000),
                    new SqlParameter("@ZaoCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@WuCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@WanCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@Received", SqlDbType.NVarChar,50),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,1000),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,255),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime)};
            parameters[0].Value = LotID;
            parameters[1].Value = Name;
            parameters[2].Value = Dept;
            parameters[3].Value = RecordDate;
            parameters[4].Value = XingQi;
            parameters[5].Value = IsChuChai;
            parameters[6].Value = ChuChaiDiDian;
            parameters[7].Value = ChuChaiShiYou;
            parameters[8].Value = ChuChaiNWorkID;
            parameters[9].Value = ZaoCan;
            parameters[10].Value = WuCan;
            parameters[11].Value = WanCan;
            parameters[12].Value = Received;
            parameters[13].Value = Comment;
            parameters[14].Value = LastModifyUser;
            parameters[15].Value = LastModifyTime;

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
            strSql.Append("update [FanTangJiuCanRecordReport] set ");
            strSql.Append("LotID=@LotID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Dept=@Dept,");
            strSql.Append("RecordDate=@RecordDate,");
            strSql.Append("XingQi=@XingQi,");
            strSql.Append("IsChuChai=@IsChuChai,");
            strSql.Append("ChuChaiDiDian=@ChuChaiDiDian,");
            strSql.Append("ChuChaiShiYou=@ChuChaiShiYou,");
            strSql.Append("ChuChaiNWorkID=@ChuChaiNWorkID,");
            strSql.Append("ZaoCan=@ZaoCan,");
            strSql.Append("WuCan=@WuCan,");
            strSql.Append("WanCan=@WanCan,");
            strSql.Append("Received=@Received,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("LastModifyTime=@LastModifyTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,100),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,100),
                    new SqlParameter("@RecordDate", SqlDbType.DateTime),
                    new SqlParameter("@XingQi", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsChuChai", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ChuChaiShiYou", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ChuChaiNWorkID", SqlDbType.VarChar,1000),
                    new SqlParameter("@ZaoCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@WuCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@WanCan", SqlDbType.VarChar,1000),
                    new SqlParameter("@Received", SqlDbType.NVarChar,50),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,1000),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,255),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = LotID;
            parameters[1].Value = Name;
            parameters[2].Value = Dept;
            parameters[3].Value = RecordDate;
            parameters[4].Value = XingQi;
            parameters[5].Value = IsChuChai;
            parameters[6].Value = ChuChaiDiDian;
            parameters[7].Value = ChuChaiShiYou;
            parameters[8].Value = ChuChaiNWorkID;
            parameters[9].Value = ZaoCan;
            parameters[10].Value = WuCan;
            parameters[11].Value = WanCan;
            parameters[12].Value = Received;
            parameters[13].Value = Comment;
            parameters[14].Value = LastModifyUser;
            parameters[15].Value = LastModifyTime;
            parameters[16].Value = ID;

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
            strSql.Append("delete from [FanTangJiuCanRecordReport] ");
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
            strSql.Append(" FROM [FanTangJiuCanRecordReport] ");
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
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dept"] != null)
                {
                    this.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RecordDate"] != null && ds.Tables[0].Rows[0]["RecordDate"].ToString() != "")
                {
                    this.RecordDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecordDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingQi"] != null)
                {
                    this.XingQi = ds.Tables[0].Rows[0]["XingQi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsChuChai"] != null)
                {
                    this.IsChuChai = ds.Tables[0].Rows[0]["IsChuChai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiDiDian"] != null)
                {
                    this.ChuChaiDiDian = ds.Tables[0].Rows[0]["ChuChaiDiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiShiYou"] != null)
                {
                    this.ChuChaiShiYou = ds.Tables[0].Rows[0]["ChuChaiShiYou"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiNWorkID"] != null)
                {
                    this.ChuChaiNWorkID = ds.Tables[0].Rows[0]["ChuChaiNWorkID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaoCan"] != null)
                {
                    this.ZaoCan = ds.Tables[0].Rows[0]["ZaoCan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WuCan"] != null)
                {
                    this.WuCan = ds.Tables[0].Rows[0]["WuCan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WanCan"] != null)
                {
                    this.WanCan = ds.Tables[0].Rows[0]["WanCan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Received"] != null)
                {
                    this.Received = ds.Tables[0].Rows[0]["Received"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastModifyUser"] != null)
                {
                    this.LastModifyUser = ds.Tables[0].Rows[0]["LastModifyUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastModifyTime"] != null && ds.Tables[0].Rows[0]["LastModifyTime"].ToString() != "")
                {
                    this.LastModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifyTime"].ToString());
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
            strSql.Append(" FROM [FanTangJiuCanRecordReport] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

