using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using System.Collections.Generic;
using ZWL.Common;

namespace ZWL.BLL
{
	/// <summary>
    /// 类ERPChuChai。
	/// </summary>
	public class ERPChuChai
	{
        public ERPChuChai()
		{}
		#region Model
		private int _id;
        private string _shenqingren;//申请人
        private string _bm;//部门
        private DateTime _tbtime;//填表时间
        private string _chuchaididian;//出差地点
        private string _tongxingrenyuan;//同行人员
        private string _chuchaishiyou;//出差事由
        private DateTime _ccStart;//出差时间开始
        private DateTime _ccEnd;//出差时间结束
        private string _jiaotong;//交通工具
        private string _beizhu;//备注
        private int _nworkid;//工作编号
        private string _ccstate;//出差状态(分为通过和不通过)
        private string _BSState;//报销状态
        

        /// <summary>
        /// id
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
            set { _shenqingren = value; }
            get { return _shenqingren; }
        }        
      
        /// <summary>
		/// 部门
		/// </summary>
        public string BM
		{
            set { _bm = value; }
            get { return _bm; }
		}
        /// <summary>
        /// 填表时间
		/// </summary>
        public DateTime TBTime
		{
            set { _tbtime = value; }
            get { return _tbtime; }
		}
        /// <summary>
        /// 出差地点
        /// </summary>
        public string ChuChaiDiDian
        {
            set { _chuchaididian = value; }
            get { return _chuchaididian; }
        }
        /// <summary>
        /// 同行人员
        /// </summary>
        public string TongXingRenYuan
        {
            set { _tongxingrenyuan = value; }
            get { return _tongxingrenyuan; }
        }
        /// <summary>
        /// 出差事由
        /// </summary>
        public string ChuChaiShiYou
        {
            set { _chuchaishiyou = value; }
            get { return _chuchaishiyou; }
        }
        /// <summary>
        /// 出差时间开始
        /// </summary>
        public DateTime ChuChaiStart
        {
            set { _ccStart = value; }
            get { return _ccStart; }
        }
        /// <summary>
        /// 出差时间结束
        /// </summary>
        public DateTime ChuChaiEnd
        {
            set { _ccEnd = value; }
            get { return _ccEnd; }
        }
        /// <summary>
        /// 交通工具
        /// </summary>
        public string JiaoTongGJ
        {
            set { _jiaotong = value; }
            get { return _jiaotong; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            set { _beizhu = value; }
            get { return _beizhu; }
        }
        /// <summary>
        /// 工作编号
        /// </summary>
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 出差状态
        /// </summary>
        public string CCState
        {
            set { _ccstate = value; }
            get { return _ccstate; }
        }   
        /// <summary>
        /// 报销状态
        /// </summary>
        public string BSState
        {
            set { _BSState = value; }
            get { return _BSState; }
        }


        #endregion Model

        #region Relative Model

        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(SQR))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + SQR + "'");
                    if (tempUser != null)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }
        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo
        {
            get
            {
                var _currentWorkToDo = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkID > 0)
                {
                    _currentWorkToDo.GetModel(NWorkID);
                }
                return _currentWorkToDo;
            }
        }
        #endregion

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPChuChai(");
            strSql.Append("SQR,BM,TBTime,ChuChaiDiDian,TongXingRenYuan,ChuChaiShiYou,ChuChaiStart,ChuChaiEnd,JiaoTongGJ,BZ,NWorkID,CCState,BSState )");
            strSql.Append(" values (");
            strSql.Append("@SQR,@BM,@TBTime,@ChuChaiDiDian,@TongXingRenYuan,@ChuChaiShiYou,@ChuChaiStart,@ChuChaiEnd,@JiaoTongGJ,@BZ,@NWorkID,@CCState,@BSState )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {   
                    new SqlParameter("@SQR", SqlDbType.VarChar,20),                
                    new SqlParameter("@BM", SqlDbType.VarChar,100),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.VarChar,200),
                    new SqlParameter("@TongXingRenYuan", SqlDbType.VarChar,500),
                    new SqlParameter("@ChuChaiShiYou", SqlDbType.VarChar,500),
                    new SqlParameter("@ChuChaiStart", SqlDbType.DateTime),
                    new SqlParameter("@ChuChaiEnd", SqlDbType.DateTime),
                    new SqlParameter("@JiaoTongGJ",SqlDbType.VarChar,50),
                    new SqlParameter("@BZ",SqlDbType.VarChar,500),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@CCState", SqlDbType.VarChar,10),
                    new SqlParameter("@BSState", SqlDbType.NVarChar,20)
                                        };
            parameters[0].Value = SQR;
            parameters[1].Value = BM;
            parameters[2].Value = TBTime;
            parameters[3].Value = ChuChaiDiDian;
            parameters[4].Value = TongXingRenYuan;
            parameters[5].Value = ChuChaiShiYou;
            parameters[6].Value = ChuChaiStart;
            parameters[7].Value = ChuChaiEnd;
            parameters[8].Value = JiaoTongGJ;
            parameters[9].Value = BZ;
            parameters[10].Value = NWorkID;
            parameters[11].Value = CCState;
            parameters[12].Value = BSState;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPChuChai set ");
            strSql.Append("SQR=@SQR,");
            strSql.Append("BM=@BM,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("ChuChaiDiDian=@ChuChaiDiDian,");
            strSql.Append("TongXingRenYuan=@TongXingRenYuan,");
            strSql.Append("ChuChaiShiYou=@ChuChaiShiYou,");
            strSql.Append("ChuChaiStart=@ChuChaiStart,");
            strSql.Append("ChuChaiEnd=@ChuChaiEnd,");
            strSql.Append("JiaoTongGJ=@JiaoTongGJ,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("CCState=@CCState,");
            strSql.Append("BSState=@BSState");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6),	
                    new SqlParameter("@SQR", SqlDbType.VarChar,20),                
                    new SqlParameter("@BM", SqlDbType.VarChar,100),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.VarChar,200),
                    new SqlParameter("@TongXingRenYuan", SqlDbType.VarChar,500),
                    new SqlParameter("@ChuChaiShiYou", SqlDbType.VarChar,500),
                    new SqlParameter("@ChuChaiStart", SqlDbType.DateTime),
                    new SqlParameter("@ChuChaiEnd", SqlDbType.DateTime),
                    new SqlParameter("@JiaoTongGJ",SqlDbType.VarChar,50),
                    new SqlParameter("@BZ",SqlDbType.VarChar,500),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@CCState", SqlDbType.VarChar,10),
                    new SqlParameter("@BSState", SqlDbType.NVarChar,20)
                                        };
            parameters[0].Value = ID;
            parameters[1].Value = SQR;
            parameters[2].Value = BM;
            parameters[3].Value = TBTime;
            parameters[4].Value = ChuChaiDiDian;
            parameters[5].Value = TongXingRenYuan;
            parameters[6].Value = ChuChaiShiYou;
            parameters[7].Value = ChuChaiStart;
            parameters[8].Value = ChuChaiEnd;
            parameters[9].Value = JiaoTongGJ;
            parameters[10].Value = BZ;
            parameters[11].Value = NWorkID;
            parameters[12].Value = CCState;
            parameters[13].Value = BSState;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from ERPChuChai ");
            strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public void GetModel(int nid)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,SQR,BM,TBTime,ChuChaiDiDian,TongXingRenYuan,ChuChaiShiYou,ChuChaiStart,ChuChaiEnd,JiaoTongGJ,BZ,NWorkID,CCState,BSState ");
            strSql.Append(" FROM ERPChuChai ");
            strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                SQR = ds.Tables[0].Rows[0]["SQR"].ToString();                
                BM = ds.Tables[0].Rows[0]["BM"].ToString();                
                
                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }

                ChuChaiDiDian = ds.Tables[0].Rows[0]["ChuChaiDiDian"].ToString();
                TongXingRenYuan = ds.Tables[0].Rows[0]["TongXingRenYuan"].ToString();
                ChuChaiShiYou = ds.Tables[0].Rows[0]["ChuChaiShiYou"].ToString();

                if (ds.Tables[0].Rows[0]["ChuChaiStart"].ToString() != "")
                {
                    ChuChaiStart = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChuChaiStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChuChaiEnd"].ToString() != "")
                {
                    ChuChaiEnd = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChuChaiEnd"].ToString());
                }
                JiaoTongGJ = ds.Tables[0].Rows[0]["JiaoTongGJ"].ToString();
                BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                CCState = ds.Tables[0].Rows[0]["CCState"].ToString();
                BSState = ds.Tables[0].Rows[0]["BSState"].ToString();
            }
           
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworkid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SQR,BM,TBTime,ChuChaiDiDian,TongXingRenYuan,ChuChaiShiYou,ChuChaiStart,ChuChaiEnd,JiaoTongGJ,BZ,NWorkID,CCState,BSState ");
            strSql.Append(" FROM ERPChuChai ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworkid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();

                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }

                ChuChaiDiDian = ds.Tables[0].Rows[0]["ChuChaiDiDian"].ToString();
                TongXingRenYuan = ds.Tables[0].Rows[0]["TongXingRenYuan"].ToString();
                ChuChaiShiYou = ds.Tables[0].Rows[0]["ChuChaiShiYou"].ToString();

                if (ds.Tables[0].Rows[0]["ChuChaiStart"].ToString() != "")
                {
                    ChuChaiStart = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChuChaiStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChuChaiEnd"].ToString() != "")
                {
                    ChuChaiEnd = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChuChaiEnd"].ToString());
                }
                JiaoTongGJ = ds.Tables[0].Rows[0]["JiaoTongGJ"].ToString();
                BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                CCState = ds.Tables[0].Rows[0]["CCState"].ToString();
                BSState = ds.Tables[0].Rows[0]["BSState"].ToString();
            }

        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select c.*,d.WorkName,d.FormID,d.WorkFlowID,d.StateNow,d.UserName,d.TimeStr,d.JieDianName,d.JieDianID,d.ShenPiUserList,d.OKUserList
                               from ERPChuChai c join ERPNWorkToDo d 
                              on c.NWorkID = d.ID ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where " + strWhere );
			}            
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZWL.BLL.ERPChuChai> GetModelList(string strWhere)
		{
            var result = new List<ZWL.BLL.ERPChuChai>();
            var ds = GetList(strWhere);
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPChuChai>(ds.Tables[0]);
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
            strSql.Append(@"select c.*,d.WorkName,d.FormID,d.WorkFlowID,d.StateNow,d.UserName,d.TimeStr,d.JieDianName,d.JieDianID,d.ShenPiUserList,d.OKUserList
                               from ERPChuChai c join ERPNWorkToDo d 
                              on c.NWorkID = d.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        public Pager GetListMappingAndPaging(string strWhere, int currPage, int pageSize)
        {
            return GetListMappingAndPaging(strWhere, currPage, pageSize, 0, "");
        }
        public Pager GetListMappingAndPaging(string strWhere, int currPage, int pageSize, int top, string orderby)
        {
            var strSql = string.Format(@"select {0} * FROM (select c.[ID]
      ,[SQR]
      ,[BM]
      ,[TBTime]
      ,[ChuChaiDiDian]
      ,[TongXingRenYuan]
      ,[ChuChaiShiYou]
      ,[ChuChaiStart]
      ,[ChuChaiEnd]
      ,[JiaoTongGJ]
      ,[NWorkID]
      ,[BZ]
      ,[CCState]
      ,[BSState],d.WorkName,d.FormID,d.WorkFlowID,d.JieDianName,d.StateNow,d.TimeStr,
                            d.LateTime,d.ShenPiUserList,d.UserName,c.BM Department,BeiYong1,BeiYong2 from ERPChuChai c join ERPNWorkToDo d 
                            on c.NWorkID = d.ID
 UNION
 select c.[ID]
      ,[SQR]
      ,[BM]
      ,[TBTime]
      ,[ChuChaiDiDian]
      ,[TongXingRenYuan]
      ,[ChuChaiShiYou]
      ,[ChuChaiStart]
      ,[ChuChaiEnd]
      ,[JiaoTongGJ]
      ,d.ID [NWorkID]
      ,[BZ]
      ,[CCState]
      ,[BSState]
      ,d.WorkName,d.FormID,d.WorkFlowID,d.JieDianName,d.StateNow,d.TimeStr,
                            d.LateTime,d.ShenPiUserList,d.UserName,c.BM Department,BeiYong1,BeiYong2 from ERPChuChai c join (select * from ERPNWorkToDo where WorkName like '%出差变更审批流程%' and BeiYong2 is not null and BeiYong2<>'' and BeiYong2<>'0' and FormID in (select ID from ERPNForm where FormName like '%出差审批表%' or FormName like '%部门负责人出差%')) d 
                            on c.NWorkID = cast(d.BeiYong2 as int)) t", top > 0 ? (" top " + top) : "");
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            if (string.IsNullOrEmpty(orderby))
                return new Pager(strSql, currPage, pageSize);
            else
                return new Pager(strSql, currPage, pageSize, orderby);
        }
        #endregion  成员方法
    }
}

