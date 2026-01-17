using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
using System.Collections.Generic;

namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPTrain。
	/// </summary>
	[Serializable]
	public partial class ERPTrain
	{
		public ERPTrain()
		{}
        #region Model
        private int _id;
        private string _sqr;
        private DateTime? _sqtime;
        private string _pxname;
        private string _pxadress;
        private string _zbdw;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private string _pxmode;
        private string _cjpxpeople;
        private string _pxcontent;
        private decimal? _pxysmoney;
        private decimal? _pxjsmoney;
        private int _nworkid;
        private string _bz;
        private int? _pxrs;
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
        /// 培训名称
        /// </summary>
        public string PXName
        {
            set { _pxname = value; }
            get { return _pxname; }
        }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string PXAdress
        {
            set { _pxadress = value; }
            get { return _pxadress; }
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
        /// 培训方式
        /// </summary>
        public string PXMode
        {
            set { _pxmode = value; }
            get { return _pxmode; }
        }
        /// <summary>
        /// 参加培训人员
        /// </summary>
        public string CJPXPeople
        {
            set { _cjpxpeople = value; }
            get { return _cjpxpeople; }
        }
        /// <summary>
        /// 培训主要内容
        /// </summary>
        public string PXContent
        {
            set { _pxcontent = value; }
            get { return _pxcontent; }
        }
        /// <summary>
        /// 培训经费预算
        /// </summary>
        public decimal? PXYSMoney
        {
            set { _pxysmoney = value; }
            get { return _pxysmoney; }
        }
        /// <summary>
        /// 培训经费结算
        /// </summary>
        public decimal? PXJSMoney
        {
            set { _pxjsmoney = value; }
            get { return _pxjsmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NWorkID
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
        /// <summary>
        /// 
        /// </summary>
        public int? PXRS
        {
            set { _pxrs = value; }
            get { return _pxrs; }
        }
        #endregion Model


        #region  Method

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPTrain(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SQR,SQTime,PXName,PXAdress,ZBDW,StartTime,EndTime,PXMode,CJPXPeople,PXContent,PXYSMoney,PXJSMoney,NWorkID,BZ,PXRS ");
            strSql.Append(" FROM [ERPTrain] ");
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
                if (ds.Tables[0].Rows[0]["PXName"] != null)
                {
                    this.PXName = ds.Tables[0].Rows[0]["PXName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXAdress"] != null)
                {
                    this.PXAdress = ds.Tables[0].Rows[0]["PXAdress"].ToString();
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
                if (ds.Tables[0].Rows[0]["PXMode"] != null)
                {
                    this.PXMode = ds.Tables[0].Rows[0]["PXMode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CJPXPeople"] != null)
                {
                    this.CJPXPeople = ds.Tables[0].Rows[0]["CJPXPeople"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXContent"] != null)
                {
                    this.PXContent = ds.Tables[0].Rows[0]["PXContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXYSMoney"] != null && ds.Tables[0].Rows[0]["PXYSMoney"].ToString() != "")
                {
                    this.PXYSMoney = decimal.Parse(ds.Tables[0].Rows[0]["PXYSMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PXJSMoney"] != null && ds.Tables[0].Rows[0]["PXJSMoney"].ToString() != "")
                {
                    this.PXJSMoney = decimal.Parse(ds.Tables[0].Rows[0]["PXJSMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXRS"] != null && ds.Tables[0].Rows[0]["PXRS"].ToString() != "")
                {
                    this.PXRS = int.Parse(ds.Tables[0].Rows[0]["PXRS"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPTrain]");
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
            strSql.Append("insert into [ERPTrain] (");
            strSql.Append("SQR,SQTime,PXName,PXAdress,ZBDW,StartTime,EndTime,PXMode,CJPXPeople,PXContent,PXYSMoney,PXJSMoney,NWorkID,BZ,PXRS)");
            strSql.Append(" values (");
            strSql.Append("@SQR,@SQTime,@PXName,@PXAdress,@ZBDW,@StartTime,@EndTime,@PXMode,@CJPXPeople,@PXContent,@PXYSMoney,@PXJSMoney,@NWorkID,@BZ,@PXRS)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SQR", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQTime", SqlDbType.DateTime),
                    new SqlParameter("@PXName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZBDW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@PXMode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CJPXPeople", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXYSMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@PXJSMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXRS", SqlDbType.Int,4)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQTime;
            parameters[2].Value = PXName;
            parameters[3].Value = PXAdress;
            parameters[4].Value = ZBDW;
            parameters[5].Value = StartTime;
            parameters[6].Value = EndTime;
            parameters[7].Value = PXMode;
            parameters[8].Value = CJPXPeople;
            parameters[9].Value = PXContent;
            parameters[10].Value = PXYSMoney;
            parameters[11].Value = PXJSMoney;
            parameters[12].Value = NWorkID;
            parameters[13].Value = BZ;
            parameters[14].Value = PXRS;

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
            strSql.Append("update [ERPTrain] set ");
            strSql.Append("SQR=@SQR,");
            strSql.Append("SQTime=@SQTime,");
            strSql.Append("PXName=@PXName,");
            strSql.Append("PXAdress=@PXAdress,");
            strSql.Append("ZBDW=@ZBDW,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("PXMode=@PXMode,");
            strSql.Append("CJPXPeople=@CJPXPeople,");
            strSql.Append("PXContent=@PXContent,");
            strSql.Append("PXYSMoney=@PXYSMoney,");
            strSql.Append("PXJSMoney=@PXJSMoney,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("PXRS=@PXRS");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SQR", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQTime", SqlDbType.DateTime),
                    new SqlParameter("@PXName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZBDW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@PXMode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CJPXPeople", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXContent", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXYSMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@PXJSMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@PXRS", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = SQR;
            parameters[1].Value = SQTime;
            parameters[2].Value = PXName;
            parameters[3].Value = PXAdress;
            parameters[4].Value = ZBDW;
            parameters[5].Value = StartTime;
            parameters[6].Value = EndTime;
            parameters[7].Value = PXMode;
            parameters[8].Value = CJPXPeople;
            parameters[9].Value = PXContent;
            parameters[10].Value = PXYSMoney;
            parameters[11].Value = PXJSMoney;
            parameters[12].Value = NWorkID;
            parameters[13].Value = BZ;
            parameters[14].Value = PXRS;
            parameters[15].Value = ID;

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
            strSql.Append("delete from [ERPTrain] ");
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
            strSql.Append("select ID,SQR,SQTime,PXName,PXAdress,ZBDW,StartTime,EndTime,PXMode,CJPXPeople,PXContent,PXYSMoney,PXJSMoney,NWorkID,BZ,PXRS ");
            strSql.Append(" FROM [ERPTrain] ");
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
                if (ds.Tables[0].Rows[0]["PXName"] != null)
                {
                    this.PXName = ds.Tables[0].Rows[0]["PXName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXAdress"] != null)
                {
                    this.PXAdress = ds.Tables[0].Rows[0]["PXAdress"].ToString();
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
                if (ds.Tables[0].Rows[0]["PXMode"] != null)
                {
                    this.PXMode = ds.Tables[0].Rows[0]["PXMode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CJPXPeople"] != null)
                {
                    this.CJPXPeople = ds.Tables[0].Rows[0]["CJPXPeople"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXContent"] != null)
                {
                    this.PXContent = ds.Tables[0].Rows[0]["PXContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXYSMoney"] != null && ds.Tables[0].Rows[0]["PXYSMoney"].ToString() != "")
                {
                    this.PXYSMoney = decimal.Parse(ds.Tables[0].Rows[0]["PXYSMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PXJSMoney"] != null && ds.Tables[0].Rows[0]["PXJSMoney"].ToString() != "")
                {
                    this.PXJSMoney = decimal.Parse(ds.Tables[0].Rows[0]["PXJSMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PXRS"] != null && ds.Tables[0].Rows[0]["PXRS"].ToString() != "")
                {
                    this.PXRS = int.Parse(ds.Tables[0].Rows[0]["PXRS"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(@" FROM (select a.*,t.WorkName,t.JieDianName,t.ShenPiUserList,t.OKUserList,t.StateNow,t.WorkFlowID,t.FormID  FROM [ERPTrain] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID) m ");
            if (strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.GetDataTable(strSql.ToString());
		}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZWL.BLL.ERPTrain GetModel(string strWhere)
        {
            var dt = GetList(strWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                return PublicMethod.ConvertToModel<ZWL.BLL.ERPTrain>(dt);
            }

            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ERPTrain> GetModelList(string strWhere)
        {
            var result = new List<ERPTrain>();
            var source = GetList(strWhere);
            if (source != null && source.Rows.Count > 0)
            {
                foreach (DataRow item in source.Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPTrain>(item));
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
            strSql.Append(@" FROM (select a.*,t.WorkName,t.JieDianName,t.ShenPiUserList,t.OKUserList,t.StateNow,t.WorkFlowID,t.FormID  FROM [ERPTrain] a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID) m ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize);
        }

        #endregion  Method
    }
}

