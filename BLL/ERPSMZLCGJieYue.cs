
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPSMZLCGJieYue,涉密资料（成果）借阅
	/// </summary>
	public class ERPSMZLCGJieYue
	{
		public ERPSMZLCGJieYue()
		{}
		#region Model
		private int _id;//主键
        
         private int _nworktodoid;
          
         private string _workname = "";
          
         private DateTime _dengjitime = DateTime.Now;
          
         private string _xiangmubianhao = "";
          
         private string _xiangmumingcheng = "";
          
         private string _zongshuliang = "";
          
         private DateTime _jieyongshijiankaishi = DateTime.Now;
          
         private DateTime _jieyongshijianjieshu = DateTime.Now;
          
         private string _jieyongqixian = "";
          
         private string _jieyuefangshi = "";
          
         private string _miji = "";
          
         private string _shenqingbumen = "";
          
         private string _shenqingren = "";
          

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
        /// 项目编号
        /// </summary>
        public string XiangMuBianHao
        {
            set { _xiangmubianhao = value; }
            get { return _xiangmubianhao; }
        }
       
        /// <summary>
        /// 项目名称
        /// </summary>
        public string XiangMuMingCheng
        {
            set { _xiangmumingcheng = value; }
            get { return _xiangmumingcheng; }
        }
       
        /// <summary>
        /// 总数量
        /// </summary>
        public string ZongShuLiang
        {
            set { _zongshuliang = value; }
            get { return _zongshuliang; }
        }
       
        /// <summary>
        /// 借用时间开始
        /// </summary>
        public DateTime JieYongShiJianKaiShi
        {
            set { _jieyongshijiankaishi = value; }
            get { return _jieyongshijiankaishi; }
        }
       
        /// <summary>
        /// 借用时间结束
        /// </summary>
        public DateTime JieYongShiJianJieShu
        {
            set { _jieyongshijianjieshu = value; }
            get { return _jieyongshijianjieshu; }
        }
       
        /// <summary>
        /// 借用期限
        /// </summary>
        public string JieYongQiXian
        {
            set { _jieyongqixian = value; }
            get { return _jieyongqixian; }
        }
       
        /// <summary>
        /// 借阅方式
        /// </summary>
        public string JieYueFangShi
        {
            set { _jieyuefangshi = value; }
            get { return _jieyuefangshi; }
        }
       
        /// <summary>
        /// 密级
        /// </summary>
        public string MiJi
        {
            set { _miji = value; }
            get { return _miji; }
        }
       
        /// <summary>
        /// 申请部门
        /// </summary>
        public string ShenQingBuMen
        {
            set { _shenqingbumen = value; }
            get { return _shenqingbumen; }
        }
       
        /// <summary>
        /// 申请人
        /// </summary>
        public string ShenQingRen
        {
            set { _shenqingren = value; }
            get { return _shenqingren; }
        }
       

		#endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPSMZLCGJieYue(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,XiangMuBianHao,XiangMuMingCheng,ZongShuLiang,JieYongShiJianKaiShi,JieYongShiJianJieShu,JieYongQiXian,JieYueFangShi,MiJi,ShenQingBuMen,ShenQingRen ");
            strSql.Append(" FROM [ERPSMZLCGJieYue] ");
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
                if (ds.Tables[0].Rows[0]["XiangMuBianHao"] != null)
                {
                    this.XiangMuBianHao = ds.Tables[0].Rows[0]["XiangMuBianHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiangMuMingCheng"] != null)
                {
                    this.XiangMuMingCheng = ds.Tables[0].Rows[0]["XiangMuMingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZongShuLiang"] != null)
                {
                    this.ZongShuLiang = ds.Tables[0].Rows[0]["ZongShuLiang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JieYongShiJianKaiShi"] != null)
                {
                    this.JieYongShiJianKaiShi = DateTime.Parse(ds.Tables[0].Rows[0]["JieYongShiJianKaiShi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieYongShiJianJieShu"] != null)
                {
                    this.JieYongShiJianJieShu = DateTime.Parse(ds.Tables[0].Rows[0]["JieYongShiJianJieShu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieYongQiXian"] != null)
                {
                    this.JieYongQiXian = ds.Tables[0].Rows[0]["JieYongQiXian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JieYueFangShi"] != null)
                {
                    this.JieYueFangShi = ds.Tables[0].Rows[0]["JieYueFangShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MiJi"] != null)
                {
                    this.MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenQingBuMen"] != null)
                {
                    this.ShenQingBuMen = ds.Tables[0].Rows[0]["ShenQingBuMen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenQingRen"] != null)
                {
                    this.ShenQingRen = ds.Tables[0].Rows[0]["ShenQingRen"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPSMZLCGJieYue]");
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
            strSql.Append("insert into [ERPSMZLCGJieYue] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,XiangMuBianHao,XiangMuMingCheng,ZongShuLiang,JieYongShiJianKaiShi,JieYongShiJianJieShu,JieYongQiXian,JieYueFangShi,MiJi,ShenQingBuMen,ShenQingRen)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@XiangMuBianHao,@XiangMuMingCheng,@ZongShuLiang,@JieYongShiJianKaiShi,@JieYongShiJianJieShu,@JieYongQiXian,@JieYueFangShi,@MiJi,@ShenQingBuMen,@ShenQingRen)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@XiangMuBianHao", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@XiangMuMingCheng", SqlDbType.NVarChar, 4000),
       
					new SqlParameter("@ZongShuLiang", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@JieYongShiJianKaiShi", SqlDbType.DateTime),
       
					new SqlParameter("@JieYongShiJianJieShu", SqlDbType.DateTime),
       
					new SqlParameter("@JieYongQiXian", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@JieYueFangShi", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@MiJi", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingBuMen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingRen", SqlDbType.NVarChar, 50)};
       
            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = XiangMuBianHao;
       
            parameters[4].Value = XiangMuMingCheng;
       
            parameters[5].Value = ZongShuLiang;
       
            parameters[6].Value = JieYongShiJianKaiShi;
       
            parameters[7].Value = JieYongShiJianJieShu;
       
            parameters[8].Value = JieYongQiXian;
       
            parameters[9].Value = JieYueFangShi;
       
            parameters[10].Value = MiJi;
       
            parameters[11].Value = ShenQingBuMen;
       
            parameters[12].Value = ShenQingRen;
       

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
            strSql.Append("update [ERPSMZLCGJieYue] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("XiangMuBianHao=@XiangMuBianHao,");

            strSql.Append("XiangMuMingCheng=@XiangMuMingCheng,");

            strSql.Append("ZongShuLiang=@ZongShuLiang,");

            strSql.Append("JieYongShiJianKaiShi=@JieYongShiJianKaiShi,");

            strSql.Append("JieYongShiJianJieShu=@JieYongShiJianJieShu,");

            strSql.Append("JieYongQiXian=@JieYongQiXian,");

            strSql.Append("JieYueFangShi=@JieYueFangShi,");

            strSql.Append("MiJi=@MiJi,");

            strSql.Append("ShenQingBuMen=@ShenQingBuMen,");

            strSql.Append("ShenQingRen=@ShenQingRen");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@XiangMuBianHao", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@XiangMuMingCheng", SqlDbType.NVarChar, 4000),
       
					new SqlParameter("@ZongShuLiang", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@JieYongShiJianKaiShi", SqlDbType.DateTime),
       
					new SqlParameter("@JieYongShiJianJieShu", SqlDbType.DateTime),
       
					new SqlParameter("@JieYongQiXian", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@JieYueFangShi", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@MiJi", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingBuMen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingRen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = XiangMuBianHao;
       
            parameters[4].Value = XiangMuMingCheng;
       
            parameters[5].Value = ZongShuLiang;
       
            parameters[6].Value = JieYongShiJianKaiShi;
       
            parameters[7].Value = JieYongShiJianJieShu;
       
            parameters[8].Value = JieYongQiXian;
       
            parameters[9].Value = JieYueFangShi;
       
            parameters[10].Value = MiJi;
       
            parameters[11].Value = ShenQingBuMen;
       
            parameters[12].Value = ShenQingRen;
       
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
            strSql.Append("delete from [ERPSMZLCGJieYue] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,XiangMuBianHao,XiangMuMingCheng,ZongShuLiang,JieYongShiJianKaiShi,JieYongShiJianJieShu,JieYongQiXian,JieYueFangShi,MiJi,ShenQingBuMen,ShenQingRen ");
            strSql.Append(" FROM [ERPSMZLCGJieYue] ");
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
                if (ds.Tables[0].Rows[0]["XiangMuBianHao"] != null)
                {
                    this.XiangMuBianHao = ds.Tables[0].Rows[0]["XiangMuBianHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiangMuMingCheng"] != null)
                {
                    this.XiangMuMingCheng = ds.Tables[0].Rows[0]["XiangMuMingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZongShuLiang"] != null)
                {
                    this.ZongShuLiang = ds.Tables[0].Rows[0]["ZongShuLiang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JieYongShiJianKaiShi"] != null)
                {
                    this.JieYongShiJianKaiShi = DateTime.Parse(ds.Tables[0].Rows[0]["JieYongShiJianKaiShi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieYongShiJianJieShu"] != null)
                {
                    this.JieYongShiJianJieShu = DateTime.Parse(ds.Tables[0].Rows[0]["JieYongShiJianJieShu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieYongQiXian"] != null)
                {
                    this.JieYongQiXian = ds.Tables[0].Rows[0]["JieYongQiXian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JieYueFangShi"] != null)
                {
                    this.JieYueFangShi = ds.Tables[0].Rows[0]["JieYueFangShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MiJi"] != null)
                {
                    this.MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenQingBuMen"] != null)
                {
                    this.ShenQingBuMen = ds.Tables[0].Rows[0]["ShenQingBuMen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenQingRen"] != null)
                {
                    this.ShenQingRen = ds.Tables[0].Rows[0]["ShenQingRen"].ToString();
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
            strSql.Append(" FROM [ERPSMZLCGJieYue] ");
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
            strSql.Append(" FROM ERPSMZLCGJieYue ");
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