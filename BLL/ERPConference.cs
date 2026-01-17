using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPConference。
	/// </summary>
	[Serializable]
    public partial class ERPConference
    {
        public ERPConference()
        { }
        #region Model
        private int _id;
        private string _sqr;
        private DateTime? _sqtime;
        private string _hyname;
        private string _hyadress;
        private string _zbdw;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private string _hyleve;
        private string _cjhypeople;
        private string _hycontent;
        private decimal? _hymoney;
        private int? _nworkid;
        private string _bz;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string SQR
        {
            set { _sqr = value; }
            get { return _sqr; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? SQTime
        {
            set { _sqtime = value; }
            get { return _sqtime; }
        }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string HYName
        {
            set { _hyname = value; }
            get { return _hyname; }
        }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string HYAdress
        {
            set { _hyadress = value; }
            get { return _hyadress; }
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string ZBDW
        {
            set { _zbdw = value; }
            get { return _zbdw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 会议规格
        /// </summary>
        public string HYLeve
        {
            set { _hyleve = value; }
            get { return _hyleve; }
        }
        /// <summary>
        /// 参加会议人员
        /// </summary>
        public string CJHYPeople
        {
            set { _cjhypeople = value; }
            get { return _cjhypeople; }
        }
        /// <summary>
        /// 会议主要内容
        /// </summary>
        public string HYContent
        {
            set { _hycontent = value; }
            get { return _hycontent; }
        }
        /// <summary>
        /// 会议经费预算
        /// </summary>
        public decimal? HYMoney
        {
            set { _hymoney = value; }
            get { return _hymoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPConference(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SQR,SQTime,HYName,HYAdress,ZBDW,StartTime,EndTime,HYLeve,CJHYPeople,HYContent,HYMoney,NWorkID,BZ ");
            strSql.Append(" FROM [ERPConference] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQTime"] != null && ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    this.SQTime = DateTime.Parse(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HYName"] != null)
                {
                    this.HYName = ds.Tables[0].Rows[0]["HYName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYAdress"] != null)
                {
                    this.HYAdress = ds.Tables[0].Rows[0]["HYAdress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZBDW"] != null)
                {
                    this.ZBDW = ds.Tables[0].Rows[0]["ZBDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
                {
                    this.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    this.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HYLeve"] != null)
                {
                    this.HYLeve = ds.Tables[0].Rows[0]["HYLeve"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CJHYPeople"] != null)
                {
                    this.CJHYPeople = ds.Tables[0].Rows[0]["CJHYPeople"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYContent"] != null)
                {
                    this.HYContent = ds.Tables[0].Rows[0]["HYContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYMoney"] != null && ds.Tables[0].Rows[0]["HYMoney"].ToString() != "")
                {
                    this.HYMoney = decimal.Parse(ds.Tables[0].Rows[0]["HYMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPConference]");
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
            strSql.Append("insert into [ERPConference] (");
            strSql.Append("SQR,SQTime,HYName,HYAdress,ZBDW,StartTime,EndTime,HYLeve,CJHYPeople,HYContent,HYMoney,NWorkID,BZ)");
            strSql.Append(" values (");
            strSql.Append("@SQR,@SQTime,@HYName,@HYAdress,@ZBDW,@StartTime,@EndTime,@HYLeve,@CJHYPeople,@HYContent,@HYMoney,@NWorkID,@BZ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SQR", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQTime", SqlDbType.DateTime),
                    new SqlParameter("@HYName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZBDW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@HYLeve", SqlDbType.NVarChar,50),
                    new SqlParameter("@CJHYPeople", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQTime;
            parameters[2].Value = HYName;
            parameters[3].Value = HYAdress;
            parameters[4].Value = ZBDW;
            parameters[5].Value = StartTime;
            parameters[6].Value = EndTime;
            parameters[7].Value = HYLeve;
            parameters[8].Value = CJHYPeople;
            parameters[9].Value = HYContent;
            parameters[10].Value = HYMoney;
            parameters[11].Value = NWorkID;
            parameters[12].Value = BZ;

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
            strSql.Append("update [ERPConference] set ");
            strSql.Append("SQR=@SQR,");
            strSql.Append("SQTime=@SQTime,");
            strSql.Append("HYName=@HYName,");
            strSql.Append("HYAdress=@HYAdress,");
            strSql.Append("ZBDW=@ZBDW,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("HYLeve=@HYLeve,");
            strSql.Append("CJHYPeople=@CJHYPeople,");
            strSql.Append("HYContent=@HYContent,");
            strSql.Append("HYMoney=@HYMoney,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("BZ=@BZ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SQR", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQTime", SqlDbType.DateTime),
                    new SqlParameter("@HYName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZBDW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@HYLeve", SqlDbType.NVarChar,50),
                    new SqlParameter("@CJHYPeople", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HYMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQTime;
            parameters[2].Value = HYName;
            parameters[3].Value = HYAdress;
            parameters[4].Value = ZBDW;
            parameters[5].Value = StartTime;
            parameters[6].Value = EndTime;
            parameters[7].Value = HYLeve;
            parameters[8].Value = CJHYPeople;
            parameters[9].Value = HYContent;
            parameters[10].Value = HYMoney;
            parameters[11].Value = NWorkID;
            parameters[12].Value = BZ;
            parameters[13].Value = ID;

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
            strSql.Append("delete from [ERPConference] ");
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
            strSql.Append("select ID,SQR,SQTime,HYName,HYAdress,ZBDW,StartTime,EndTime,HYLeve,CJHYPeople,HYContent,HYMoney,NWorkID,BZ ");
            strSql.Append(" FROM [ERPConference] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQTime"] != null && ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    this.SQTime = DateTime.Parse(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HYName"] != null)
                {
                    this.HYName = ds.Tables[0].Rows[0]["HYName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYAdress"] != null)
                {
                    this.HYAdress = ds.Tables[0].Rows[0]["HYAdress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZBDW"] != null)
                {
                    this.ZBDW = ds.Tables[0].Rows[0]["ZBDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
                {
                    this.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    this.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HYLeve"] != null)
                {
                    this.HYLeve = ds.Tables[0].Rows[0]["HYLeve"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CJHYPeople"] != null)
                {
                    this.CJHYPeople = ds.Tables[0].Rows[0]["CJHYPeople"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYContent"] != null)
                {
                    this.HYContent = ds.Tables[0].Rows[0]["HYContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYMoney"] != null && ds.Tables[0].Rows[0]["HYMoney"].ToString() != "")
                {
                    this.HYMoney = decimal.Parse(ds.Tables[0].Rows[0]["HYMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM (select a.*,t.WorkName,t.JieDianName,t.ShenPiUserList,t.OKUserList,t.StateNow,t.WorkFlowID,t.FormID  FROM [ERPConference] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID) m ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZWL.BLL.ERPConference GetModel(string strWhere)
        {
            var dt = GetList(strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                return PublicMethod.ConvertToModel<ZWL.BLL.ERPConference>(dt);
            }

            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ERPConference> GetModelList(string strWhere)
        {
            var result = new List<ERPConference>();
            var source = GetList(strWhere);
            if (source != null && source.Rows.Count > 0)
            {
                foreach (DataRow item in source.Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPConference>(item));
                }
            }

            return result;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetPagingList(string strWhere, int cPage, int pSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM (select a.*,t.WorkName,t.JieDianName,t.ShenPiUserList,t.OKUserList,t.StateNow,t.WorkFlowID,t.FormID  FROM [ERPConference] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID) m ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize);
        }

        #endregion 成员方法
    }
}
