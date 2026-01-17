
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPCaiDan,菜单管理
	/// </summary>
	public class ERPCaiDan
	{
		public ERPCaiDan()
		{}
		#region Model
		private int _id;//主键
        
         private int _nworktodoid;
          
         private string _workname = "";
          
         private DateTime _dengjitime = DateTime.Now;
          
         private DateTime _zhanshiriqiqi = DateTime.Now;
          
         private DateTime _zhanshiriqizhi = DateTime.Now;
          
         private string _caidantupian = "";
          
         private DateTime _modifytime = DateTime.Now;
          
         private string _username = "";
          
         private string _bumen = "";
          

		/// <summary>
		/// 主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        
        /// <summary>
        /// NWorkToDoID
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
       
        /// <summary>
        /// 工作名称
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
       
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime DengJiTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }
       
        /// <summary>
        /// 展示日期起
        /// </summary>
        public DateTime ZhanShiRiQiQi
        {
            set { _zhanshiriqiqi = value; }
            get { return _zhanshiriqiqi; }
        }
       
        /// <summary>
        /// 展示日期止
        /// </summary>
        public DateTime ZhanShiRiQiZhi
        {
            set { _zhanshiriqizhi = value; }
            get { return _zhanshiriqizhi; }
        }
       
        /// <summary>
        /// 菜单图片
        /// </summary>
        public string CaiDanTuPian
        {
            set { _caidantupian = value; }
            get { return _caidantupian; }
        }
       
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }
       
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
       
        /// <summary>
        /// 部门
        /// </summary>
        public string BuMen
        {
            set { _bumen = value; }
            get { return _bumen; }
        }
       

		#endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPCaiDan(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,ZhanShiRiQiQi,ZhanShiRiQiZhi,CaiDanTuPian,ModifyTime,UserName,BuMen ");
            strSql.Append(" FROM [ERPCaiDan] ");
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
                
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhanShiRiQiQi"] != null)
                {
                    this.ZhanShiRiQiQi = DateTime.Parse(ds.Tables[0].Rows[0]["ZhanShiRiQiQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhanShiRiQiZhi"] != null)
                {
                    this.ZhanShiRiQiZhi = DateTime.Parse(ds.Tables[0].Rows[0]["ZhanShiRiQiZhi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CaiDanTuPian"] != null)
                {
                    this.CaiDanTuPian = ds.Tables[0].Rows[0]["CaiDanTuPian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ModifyTime"] != null)
                {
                    this.ModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ModifyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMen"] != null)
                {
                    this.BuMen = ds.Tables[0].Rows[0]["BuMen"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPCaiDan]");
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
            strSql.Append("insert into [ERPCaiDan] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,ZhanShiRiQiQi,ZhanShiRiQiZhi,CaiDanTuPian,ModifyTime,UserName,BuMen)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@ZhanShiRiQiQi,@ZhanShiRiQiZhi,@CaiDanTuPian,@ModifyTime,@UserName,@BuMen)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@ZhanShiRiQiQi", SqlDbType.DateTime),
       
					new SqlParameter("@ZhanShiRiQiZhi", SqlDbType.DateTime),
       
					new SqlParameter("@CaiDanTuPian", SqlDbType.NVarChar, 4000),
       
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
       
					new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BuMen", SqlDbType.NVarChar, 50)};
       
            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = ZhanShiRiQiQi;
       
            parameters[4].Value = ZhanShiRiQiZhi;
       
            parameters[5].Value = CaiDanTuPian;
       
            parameters[6].Value = ModifyTime;
       
            parameters[7].Value = UserName;
       
            parameters[8].Value = BuMen;
       

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
            strSql.Append("update [ERPCaiDan] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("ZhanShiRiQiQi=@ZhanShiRiQiQi,");

            strSql.Append("ZhanShiRiQiZhi=@ZhanShiRiQiZhi,");

            strSql.Append("CaiDanTuPian=@CaiDanTuPian,");

            strSql.Append("ModifyTime=@ModifyTime,");

            strSql.Append("UserName=@UserName,");

            strSql.Append("BuMen=@BuMen");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@ZhanShiRiQiQi", SqlDbType.DateTime),
       
					new SqlParameter("@ZhanShiRiQiZhi", SqlDbType.DateTime),
       
					new SqlParameter("@CaiDanTuPian", SqlDbType.NVarChar, 4000),
       
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
       
					new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BuMen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = ZhanShiRiQiQi;
       
            parameters[4].Value = ZhanShiRiQiZhi;
       
            parameters[5].Value = CaiDanTuPian;
       
            parameters[6].Value = ModifyTime;
       
            parameters[7].Value = UserName;
       
            parameters[8].Value = BuMen;
       
            parameters[9].Value = ID;

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
            strSql.Append("delete from [ERPCaiDan] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,ZhanShiRiQiQi,ZhanShiRiQiZhi,CaiDanTuPian,ModifyTime,UserName,BuMen ");
            strSql.Append(" FROM [ERPCaiDan] ");
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
                
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhanShiRiQiQi"] != null)
                {
                    this.ZhanShiRiQiQi = DateTime.Parse(ds.Tables[0].Rows[0]["ZhanShiRiQiQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhanShiRiQiZhi"] != null)
                {
                    this.ZhanShiRiQiZhi = DateTime.Parse(ds.Tables[0].Rows[0]["ZhanShiRiQiZhi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CaiDanTuPian"] != null)
                {
                    this.CaiDanTuPian = ds.Tables[0].Rows[0]["CaiDanTuPian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ModifyTime"] != null)
                {
                    this.ModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ModifyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMen"] != null)
                {
                    this.BuMen = ds.Tables[0].Rows[0]["BuMen"].ToString();
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
            strSql.Append(" FROM [ERPCaiDan] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
	}
}