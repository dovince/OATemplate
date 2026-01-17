
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPDZZLCGJieYueDtl,地质资料（成果）借阅子表
	/// </summary>
	public class ERPDZZLCGJieYueDtl
	{
		public ERPDZZLCGJieYueDtl()
		{}
		#region Model
		private int _id;//主键
        
         private string _danganhao = "";
          
         private string _ziliaomingcheng = "";
          
         private string _baogaozz = "";
          
         private string _futuzz = "";
          
         private string _fujianzz = "";
          
         private string _baogaodz = "";
          
         private string _futudz = "";
          
         private string _fujiandz = "";
          
         private int _mainid;
          

		/// <summary>
		/// 主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        
        /// <summary>
        /// 档案号
        /// </summary>
        public string DangAnHao
        {
            set { _danganhao = value; }
            get { return _danganhao; }
        }
       
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ZiLiaoMingCheng
        {
            set { _ziliaomingcheng = value; }
            get { return _ziliaomingcheng; }
        }
       
        /// <summary>
        /// 报告
        /// </summary>
        public string BaoGaozz
        {
            set { _baogaozz = value; }
            get { return _baogaozz; }
        }
       
        /// <summary>
        /// 附图
        /// </summary>
        public string FuTuzz
        {
            set { _futuzz = value; }
            get { return _futuzz; }
        }
       
        /// <summary>
        /// 附件
        /// </summary>
        public string FuJianzz
        {
            set { _fujianzz = value; }
            get { return _fujianzz; }
        }
       
        /// <summary>
        /// 报告
        /// </summary>
        public string BaoGaodz
        {
            set { _baogaodz = value; }
            get { return _baogaodz; }
        }
       
        /// <summary>
        /// 附图
        /// </summary>
        public string FuTudz
        {
            set { _futudz = value; }
            get { return _futudz; }
        }
       
        /// <summary>
        /// 附件
        /// </summary>
        public string FuJiandz
        {
            set { _fujiandz = value; }
            get { return _fujiandz; }
        }
       
        /// <summary>
        /// 主表id
        /// </summary>
        public int MainID
        {
            set { _mainid = value; }
            get { return _mainid; }
        }
       

		#endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPDZZLCGJieYueDtl(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DangAnHao,ZiLiaoMingCheng,BaoGaozz,FuTuzz,FuJianzz,BaoGaodz,FuTudz,FuJiandz,MainID ");
            strSql.Append(" FROM [ERPDZZLCGJieYueDtl] ");
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
                
                if (ds.Tables[0].Rows[0]["DangAnHao"] != null)
                {
                    this.DangAnHao = ds.Tables[0].Rows[0]["DangAnHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiLiaoMingCheng"] != null)
                {
                    this.ZiLiaoMingCheng = ds.Tables[0].Rows[0]["ZiLiaoMingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BaoGaozz"] != null)
                {
                    this.BaoGaozz = ds.Tables[0].Rows[0]["BaoGaozz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuTuzz"] != null)
                {
                    this.FuTuzz = ds.Tables[0].Rows[0]["FuTuzz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianzz"] != null)
                {
                    this.FuJianzz = ds.Tables[0].Rows[0]["FuJianzz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BaoGaodz"] != null)
                {
                    this.BaoGaodz = ds.Tables[0].Rows[0]["BaoGaodz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuTudz"] != null)
                {
                    this.FuTudz = ds.Tables[0].Rows[0]["FuTudz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJiandz"] != null)
                {
                    this.FuJiandz = ds.Tables[0].Rows[0]["FuJiandz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MainID"] != null && ds.Tables[0].Rows[0]["MainID"].ToString() != "")
                {
                    this.MainID = int.Parse(ds.Tables[0].Rows[0]["MainID"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPDZZLCGJieYueDtl]");
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
            strSql.Append("insert into [ERPDZZLCGJieYueDtl] (");
            strSql.Append("DangAnHao,ZiLiaoMingCheng,BaoGaozz,FuTuzz,FuJianzz,BaoGaodz,FuTudz,FuJiandz,MainID)");
            strSql.Append(" values (");
            strSql.Append("@DangAnHao,@ZiLiaoMingCheng,@BaoGaozz,@FuTuzz,@FuJianzz,@BaoGaodz,@FuTudz,@FuJiandz,@MainID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@DangAnHao", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@ZiLiaoMingCheng", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@BaoGaozz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuTuzz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuJianzz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BaoGaodz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuTudz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuJiandz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@MainID", SqlDbType.Int)};
       
            parameters[0].Value = DangAnHao;
       
            parameters[1].Value = ZiLiaoMingCheng;
       
            parameters[2].Value = BaoGaozz;
       
            parameters[3].Value = FuTuzz;
       
            parameters[4].Value = FuJianzz;
       
            parameters[5].Value = BaoGaodz;
       
            parameters[6].Value = FuTudz;
       
            parameters[7].Value = FuJiandz;
       
            parameters[8].Value = MainID;
       

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
            strSql.Append("update [ERPDZZLCGJieYueDtl] set ");

            strSql.Append("DangAnHao=@DangAnHao,");

            strSql.Append("ZiLiaoMingCheng=@ZiLiaoMingCheng,");

            strSql.Append("BaoGaozz=@BaoGaozz,");

            strSql.Append("FuTuzz=@FuTuzz,");

            strSql.Append("FuJianzz=@FuJianzz,");

            strSql.Append("BaoGaodz=@BaoGaodz,");

            strSql.Append("FuTudz=@FuTudz,");

            strSql.Append("FuJiandz=@FuJiandz,");

            strSql.Append("MainID=@MainID");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@DangAnHao", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@ZiLiaoMingCheng", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@BaoGaozz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuTuzz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuJianzz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BaoGaodz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuTudz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FuJiandz", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@MainID", SqlDbType.Int),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = DangAnHao;
       
            parameters[1].Value = ZiLiaoMingCheng;
       
            parameters[2].Value = BaoGaozz;
       
            parameters[3].Value = FuTuzz;
       
            parameters[4].Value = FuJianzz;
       
            parameters[5].Value = BaoGaodz;
       
            parameters[6].Value = FuTudz;
       
            parameters[7].Value = FuJiandz;
       
            parameters[8].Value = MainID;
       
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
            strSql.Append("delete from [ERPDZZLCGJieYueDtl] ");
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
            strSql.Append("select ID,DangAnHao,ZiLiaoMingCheng,BaoGaozz,FuTuzz,FuJianzz,BaoGaodz,FuTudz,FuJiandz,MainID ");
            strSql.Append(" FROM [ERPDZZLCGJieYueDtl] ");
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
                
                if (ds.Tables[0].Rows[0]["DangAnHao"] != null)
                {
                    this.DangAnHao = ds.Tables[0].Rows[0]["DangAnHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiLiaoMingCheng"] != null)
                {
                    this.ZiLiaoMingCheng = ds.Tables[0].Rows[0]["ZiLiaoMingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BaoGaozz"] != null)
                {
                    this.BaoGaozz = ds.Tables[0].Rows[0]["BaoGaozz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuTuzz"] != null)
                {
                    this.FuTuzz = ds.Tables[0].Rows[0]["FuTuzz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianzz"] != null)
                {
                    this.FuJianzz = ds.Tables[0].Rows[0]["FuJianzz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BaoGaodz"] != null)
                {
                    this.BaoGaodz = ds.Tables[0].Rows[0]["BaoGaodz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuTudz"] != null)
                {
                    this.FuTudz = ds.Tables[0].Rows[0]["FuTudz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJiandz"] != null)
                {
                    this.FuJiandz = ds.Tables[0].Rows[0]["FuJiandz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MainID"] != null && ds.Tables[0].Rows[0]["MainID"].ToString() != "")
                {
                    this.MainID = int.Parse(ds.Tables[0].Rows[0]["MainID"].ToString());
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
            strSql.Append(" FROM [ERPDZZLCGJieYueDtl] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPDZZLCGJieYueDtl ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                GetModel(ID);
            }
        }
	}
}