using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
    /// 类ERPBuQian。
	/// </summary>
	public class ERPBuQian
	{
        public ERPBuQian()
		{}
		#region Model
		private int _id;
        private string _tbr;//填表人
        private string _bm;//部门
        private DateTime _tbtime;//填表时间
        private DateTime _bqtime;//补签时间
        private string _bqyy;//补签原因
        private int _nworkid;//工作编号
        private string _bqstate;//补签状态(分为通过和不通过)
        private DateTime? _dengjitime1;
        private DateTime? _dengjitime2;        
        private DateTime? _dengjitime3;
        private DateTime? _dengjitime4;

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
            set { _tbr = value; }
            get { return _tbr; }
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
        /// 补签时间
        /// </summary>
        public DateTime BQTime
        {
            set { _bqtime = value; }
            get { return _bqtime; }
        }      
        /// <summary>
        /// 补签原因
        /// </summary>
        public string BQYY
        {
            set { _bqyy = value; }
            get { return _bqyy; }
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
        /// 补签状态
        /// </summary>
        public string BQState
        {
            set { _bqstate = value; }
            get { return _bqstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DengJiTime1
        {
            set { _dengjitime1 = value; }
            get { return _dengjitime1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DengJiTime2
        {
            set { _dengjitime2 = value; }
            get { return _dengjitime2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DengJiTime3
        {
            set { _dengjitime3 = value; }
            get { return _dengjitime3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DengJiTime4
        {
            set { _dengjitime4 = value; }
            get { return _dengjitime4; }
        }
       
                
        #endregion Model


		#region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPBuQian(");
            strSql.Append("TBR,BM,TBTime,BQTime,BQYY,NWorkID,BQState,DengJiTime1,DengJiTime2,DengJiTime3,DengJiTime4 )");
            strSql.Append(" values (");
            strSql.Append("@TBR,@BM,@TBTime,@BQTime,@BQYY,@NWorkID,@BQState,@DengJiTime1,@DengJiTime2,@DengJiTime3,@DengJiTime4 )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {                    
					new SqlParameter("@TBR", SqlDbType.VarChar,20),                 
                    new SqlParameter("@BM", SqlDbType.VarChar,20),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@BQTime", SqlDbType.DateTime),                     
                    new SqlParameter("@BQYY", SqlDbType.VarChar,500),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@BQState",SqlDbType.VarChar,10),
                    new SqlParameter("@DengJiTime1", SqlDbType.DateTime),
                    new SqlParameter("@DengJiTime2", SqlDbType.DateTime),
                    new SqlParameter("@DengJiTime3", SqlDbType.DateTime),
                    new SqlParameter("@DengJiTime4", SqlDbType.DateTime)
                                        };
            parameters[0].Value = TBR;
            parameters[1].Value = BM;
            parameters[2].Value = TBTime;
            parameters[3].Value = BQTime;
            parameters[4].Value = BQYY;
            parameters[5].Value = NWorkID;
            parameters[6].Value = BQState;
            parameters[7].Value = DengJiTime1;
            parameters[8].Value = DengJiTime2;
            parameters[9].Value = DengJiTime3;
            parameters[10].Value = DengJiTime4;


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
            strSql.Append("update ERPBuQian set ");
            strSql.Append("TBR=@TBR,");
            strSql.Append("BM=@BM,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("BQTime=@BQTime,");
            strSql.Append("BQYY=@BQYY,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("BQState=@BQState");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@TBR", SqlDbType.VarChar,20),
					new SqlParameter("@BM", SqlDbType.VarChar,20),
					new SqlParameter("@TBTime", SqlDbType.DateTime),
					new SqlParameter("@BQTime", SqlDbType.DateTime),
					new SqlParameter("@BQYY", SqlDbType.VarChar,500),
					new SqlParameter("@NWorkID", SqlDbType.Int,6),
					new SqlParameter("@BQState", SqlDbType.VarChar,10)};
            parameters[0].Value = ID;
            parameters[1].Value = TBR;
            parameters[2].Value = BM;
            parameters[3].Value = TBTime;
            parameters[4].Value = BQTime;
            parameters[5].Value = BQYY;
            parameters[6].Value = NWorkID;
            parameters[7].Value = BQState;
            
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from ERPBuQian ");
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
            strSql.Append("select  top 1 ID,TBR,BM,TBTime,BQTime,BQYY,NWorkID,BQState,DengJiTime1,DengJiTime2,DengJiTime3,DengJiTime4 ");
            strSql.Append(" FROM ERPBuQian ");
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
                BM = ds.Tables[0].Rows[0]["BM"].ToString();

                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BQTime"].ToString() != "")
                {
                    BQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BQTime"].ToString());
                }

                BQYY = ds.Tables[0].Rows[0]["BQYY"].ToString();
                
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                BQState = ds.Tables[0].Rows[0]["BQState"].ToString();
                if (ds.Tables[0].Rows[0]["DengJiTime1"].ToString() != "")
                {
                    DengJiTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime2"].ToString() != "")
                {
                    DengJiTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime3"].ToString() != "")
                {
                    DengJiTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime4"].ToString() != "")
                {
                    DengJiTime4 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime4"].ToString());
                }
            }
           
		}

        /// <summary>
        /// 得到一个对象实体()
        /// </summary>
        public void GetFixModel(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TBR,BM,TBTime,BQTime,BQYY,NWorkID,BQState,DengJiTime1,DengJiTime2,DengJiTime3,DengJiTime4 ");
            strSql.Append(" FROM ERPBuQian ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                TBR = ds.Tables[0].Rows[0]["TBR"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();

                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BQTime"].ToString() != "")
                {
                    BQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BQTime"].ToString());
                }

                BQYY = ds.Tables[0].Rows[0]["BQYY"].ToString();

                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                BQState = ds.Tables[0].Rows[0]["BQState"].ToString();
                if (ds.Tables[0].Rows[0]["DengJiTime1"].ToString() != "")
                {
                    DengJiTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime2"].ToString() != "")
                {
                    DengJiTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime3"].ToString() != "")
                {
                    DengJiTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime4"].ToString() != "")
                {
                    DengJiTime4 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime4"].ToString());
                }
            }
            //将其他相关补签的也加上去
            var sql = "SELECT ID,TBR,BM,TBTime,BQTime,BQYY,NWorkID,BQState,DengJiTime1,DengJiTime2,DengJiTime3,DengJiTime4 FROM [ERPBuQian] where ([TBR] like '%" + TBR + "%') and DATEDIFF(SECOND,[BQTime],'" + BQTime + "')=0 and [BQState]='正常结束'";
            DataSet ds2 = DbHelperSQL.Query(sql);
            foreach (DataRow row in ds2.Tables[0].Rows)
            {
                if (DengJiTime1 == null && row["DengJiTime1"].ToString() != "")
                {
                    DengJiTime1 = Convert.ToDateTime(row["DengJiTime1"].ToString());
                }
                if (DengJiTime2 == null && row["DengJiTime2"].ToString() != "")
                {
                    DengJiTime2 = Convert.ToDateTime(row["DengJiTime2"].ToString());
                }
                if (DengJiTime3 == null && row["DengJiTime3"].ToString() != "")
                {
                    DengJiTime3 = Convert.ToDateTime(row["DengJiTime3"].ToString());
                }
                if (DengJiTime4 == null && row["DengJiTime4"].ToString() != "")
                {
                    DengJiTime4 = Convert.ToDateTime(row["DengJiTime4"].ToString());
                }
            }

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworkid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TBR,BM,TBTime,BQTime,BQYY,NWorkID,BQState,DengJiTime1,DengJiTime2,DengJiTime3,DengJiTime4 ");
            strSql.Append(" FROM ERPBuQian ");
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
                BM = ds.Tables[0].Rows[0]["BM"].ToString();
                
                if (ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    TBTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BQTime"].ToString() != "")
                {
                    BQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BQTime"].ToString());
                }

                BQYY = ds.Tables[0].Rows[0]["BQYY"].ToString();
                
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                BQState = ds.Tables[0].Rows[0]["BQState"].ToString();
                if (ds.Tables[0].Rows[0]["DengJiTime1"].ToString() != "")
                {
                    DengJiTime1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime2"].ToString() != "")
                {
                    DengJiTime2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime3"].ToString() != "")
                {
                    DengJiTime3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DengJiTime4"].ToString() != "")
                {
                    DengJiTime4 = Convert.ToDateTime(ds.Tables[0].Rows[0]["DengJiTime4"].ToString());
                }
            }

        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM ERPBuQian ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}

