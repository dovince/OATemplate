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
    /// 类ERPWaiChuHuoDong。
	/// </summary>
	public class ERPWaiChuHuoDong
	{
        public ERPWaiChuHuoDong()
		{}
		#region Model
		private int _id;
        private string _tianbiaoren;//填表人
        private string _zuzhizhe;//组织者
        private string _bm;//部门
        private DateTime _tbtime;//填表时间
        private string _waichushiyou;//外出事由
        private DateTime _wcsjStart;//外出时间开始
        private DateTime _wcsjEnd;//外出时间结束
        private string _wcdidian;//外出地点
        private string _canjiarenyuan;//参加人员名单
        private int _nworkid;//工作编号
        private string _wcstate;//外出状态(分为通过和不通过)
        private string _beizhu;//备注
        

        /// <summary>
        /// id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 填表人
        /// </summary>
        public string TBR
        {
            set { _tianbiaoren = value; }
            get { return _tianbiaoren; }
        }        
		/// <summary>
        /// 组织者
		/// </summary>
        public string ZuZhiZhe
		{
            set { _zuzhizhe = value; }
            get { return _zuzhizhe; }
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
        /// 外出事由
        /// </summary>
        public string WaiChuShiYou
        {
            set { _waichushiyou = value; }
            get { return _waichushiyou; }
        }
        /// <summary>
        /// 外出时间开始
        /// </summary>
        public DateTime WCSJStart
        {
            set { _wcsjStart = value; }
            get { return _wcsjStart; }
        }
        /// <summary>
        /// 外出时间结束
        /// </summary>
        public DateTime WCSJEnd
        {
            set { _wcsjEnd = value; }
            get { return _wcsjEnd; }
        }
        /// <summary>
        /// 外出地点
        /// </summary>
        public string WaiChuDiDian
        {
            set { _wcdidian = value; }
            get { return _wcdidian; }
        }
        /// <summary>
        /// 参加人员名单
        /// </summary>
        public string CanJiaRenYuan
        {
            set { _canjiarenyuan = value; }
            get { return _canjiarenyuan; }
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
        /// 外出活动状态
        /// </summary>
        public string WCState
        {
            set { _wcstate = value; }
            get { return _wcstate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            set { _beizhu = value; }
            get { return _beizhu; }
        }
                
        #endregion Model


		#region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPWaiChuHuoDong(");
            strSql.Append("TBR,ZuZhiZhe,BM,TBTime,WaiChuShiYou,WCSJStart,WCSJEnd,WaiChuDiDian,CanJiaRenYuan,NWorkID,BZ,WCState )");
            strSql.Append(" values (");
            strSql.Append("@TBR,@ZuZhiZhe,@BM,@TBTime,@WaiChuShiYou,@WCSJStart,@WCSJEnd,@WaiChuDiDian,@CanJiaRenYuan,@NWorkID,@BZ,@WCState )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {   
                    new SqlParameter("@TBR", SqlDbType.VarChar,20), 
					new SqlParameter("@ZuZhiZhe", SqlDbType.VarChar,50),                 
                    new SqlParameter("@BM", SqlDbType.VarChar,20),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@WaiChuShiYou", SqlDbType.VarChar,8000),
                    new SqlParameter("@WCSJStart", SqlDbType.DateTime),
                    new SqlParameter("@WCSJEnd", SqlDbType.DateTime),
                    new SqlParameter("@WaiChuDiDian",SqlDbType.VarChar,500),
                    new SqlParameter("@CanJiaRenYuan",SqlDbType.VarChar,8000),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@BZ",SqlDbType.VarChar,500),
                    new SqlParameter("@WCState",SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = TBR;
            parameters[1].Value = ZuZhiZhe;
            parameters[2].Value = BM;
            parameters[3].Value = TBTime;
            parameters[4].Value = WaiChuShiYou;
            parameters[5].Value = WCSJStart;
            parameters[6].Value = WCSJEnd;
            parameters[7].Value = WaiChuDiDian;
            parameters[8].Value = CanJiaRenYuan;
            parameters[9].Value = NWorkID;
            parameters[10].Value = BZ;
            parameters[11].Value = WCState;


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
            strSql.Append("update ERPWaiChuHuoDong set ");
            strSql.Append("TBR=@TBR,");
            strSql.Append("ZuZhiZhe=@ZuZhiZhe,");
            strSql.Append("BM=@BM,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("WaiChuShiYou=@WaiChuShiYou,");
            strSql.Append("WCSJStart=@WCSJStart,");
            strSql.Append("WCSJEnd=@WCSJEnd,");
            strSql.Append("WaiChuDiDian=@WaiChuDiDian,");
            strSql.Append("CanJiaRenYuan=@CanJiaRenYuan,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("WCState=@WCState");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6),					
                    new SqlParameter("@TBR", SqlDbType.VarChar,20), 
					new SqlParameter("@ZuZhiZhe", SqlDbType.VarChar,50),                 
                    new SqlParameter("@BM", SqlDbType.VarChar,20),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@WaiChuShiYou", SqlDbType.VarChar,8000),
                    new SqlParameter("@WCSJStart", SqlDbType.DateTime),
                    new SqlParameter("@WCSJEnd", SqlDbType.DateTime),
                    new SqlParameter("@WaiChuDiDian",SqlDbType.VarChar,500),
                    new SqlParameter("@CanJiaRenYuan",SqlDbType.VarChar,8000),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@BZ",SqlDbType.VarChar,500),
                    new SqlParameter("@WCState",SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = ID;
            parameters[1].Value = TBR;
            parameters[2].Value = ZuZhiZhe;
            parameters[3].Value = BM;
            parameters[4].Value = TBTime;
            parameters[5].Value = WaiChuShiYou;
            parameters[6].Value = WCSJStart;
            parameters[7].Value = WCSJEnd;
            parameters[8].Value = WaiChuDiDian;
            parameters[9].Value = CanJiaRenYuan;
            parameters[10].Value = NWorkID;
            parameters[11].Value = BZ;
            parameters[12].Value = WCState;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from ERPWaiChuHuoDong ");
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
            strSql.Append("select  top 1 ID,TBR,ZuZhiZhe,BM,TBTime,WaiChuShiYou,WCSJStart,WCSJEnd,WaiChuDiDian,CanJiaRenYuan,NWorkID,BZ,WCState ");
            strSql.Append(" FROM ERPWaiChuHuoDong ");
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
                TBR = ds.Tables[0].Rows[0]["TBR"].ToString();
                ZuZhiZhe = ds.Tables[0].Rows[0]["ZuZhiZhe"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();                
                
                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }              

                WaiChuShiYou = ds.Tables[0].Rows[0]["WaiChuShiYou"].ToString();

                if (ds.Tables[0].Rows[0]["WCSJStart"].ToString() != "")
                {
                    WCSJStart = Convert.ToDateTime(ds.Tables[0].Rows[0]["WCSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WCSJEnd"].ToString() != "")
                {
                    WCSJEnd = Convert.ToDateTime(ds.Tables[0].Rows[0]["WCSJEnd"].ToString());
                }
                WaiChuDiDian = ds.Tables[0].Rows[0]["WaiChuDiDian"].ToString();
                CanJiaRenYuan = ds.Tables[0].Rows[0]["CanJiaRenYuan"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                WCState = ds.Tables[0].Rows[0]["WCState"].ToString();
            }
           
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworkid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TBR,ZuZhiZhe,BM,TBTime,WaiChuShiYou,WCSJStart,WCSJEnd,WaiChuDiDian,CanJiaRenYuan,NWorkID,BZ,WCState ");
            strSql.Append(" FROM ERPWaiChuHuoDong ");
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
                TBR = ds.Tables[0].Rows[0]["TBR"].ToString();
                ZuZhiZhe = ds.Tables[0].Rows[0]["ZuZhiZhe"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();

                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }

                WaiChuShiYou = ds.Tables[0].Rows[0]["WaiChuShiYou"].ToString();

                if (ds.Tables[0].Rows[0]["WCSJStart"].ToString() != "")
                {
                    WCSJStart = Convert.ToDateTime(ds.Tables[0].Rows[0]["WCSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WCSJEnd"].ToString() != "")
                {
                    WCSJEnd = Convert.ToDateTime(ds.Tables[0].Rows[0]["WCSJEnd"].ToString());
                }
                WaiChuDiDian = ds.Tables[0].Rows[0]["WaiChuDiDian"].ToString();
                CanJiaRenYuan = ds.Tables[0].Rows[0]["CanJiaRenYuan"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                WCState = ds.Tables[0].Rows[0]["WCState"].ToString();
            }

        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM ERPWaiChuHuoDong ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where " + strWhere );
			}            
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPWaiChuHuoDong> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPWaiChuHuoDong>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPWaiChuHuoDong>(ds.Tables[0]);
            }
            return result;
        }

        #endregion  成员方法
    }
}

