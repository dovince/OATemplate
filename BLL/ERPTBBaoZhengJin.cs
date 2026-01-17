using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPTBBaoZhengJin。
	/// </summary>
	public class ERPTBBaoZhengJin
	{
		public ERPTBBaoZhengJin()
		{}
		#region Model
		private int _id;
        private string _tbxmbh;
		private string _tbxmmc;
        private string _tbbm;
        private float _tbbzj;//投标保证金
        private string _zbdwname;//招标单位
        private string _kaihuhang;//开户行
        private string _yhzh;//银行账号
        private string _zffs;//支付方式
        private DateTime _jztime;//截止时间
        private DateTime _thtime;//退还时间
        private string _bzjly;//保证金来源
        private string _tbjbr;//投标经办人
        		

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		
        /// <summary>
		/// 投标项目编号
		/// </summary>
        public string TBXMBH
		{
			set{ _tbxmbh=value;}
			get{return _tbxmbh;}
		}
      
		/// <summary>
		/// 投标项目名称
		/// </summary>
        public string TBXMMC
		{
			set{ _tbxmmc=value;}
            get { return _tbxmmc; }
		}

        /// <summary>
        /// 投标部门
        /// </summary>
        public string TBBM
        {
            set { _tbbm = value; }
            get { return _tbbm; }
        }
		/// <summary>
		/// 投标保证金
		/// </summary>
		public float TBBZJ
		{
            set { _tbbzj = value; }
            get { return _tbbzj; }
		}
        /// <summary>
        /// 招标单位
        /// </summary>
        public string ZBDWMC
        {
            set { _zbdwname = value; }
            get { return _zbdwname; }
        }
		/// <summary>
		/// 开户行
		/// </summary>
        public string KaiHuHang
		{
            set { _kaihuhang = value; }
            get { return _kaihuhang; }
		}
        /// <summary>
        /// 银行账号
        /// </summary>
        public string YHZH
        {
            set { _yhzh = value; }
            get { return _yhzh; }
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string ZFFS
        {
            set { _zffs = value; }
            get { return _zffs; }
        }
 
		/// <summary>
		/// 截止时间
		/// </summary>
        public DateTime JZTime
		{
            set { _jztime = value; }
            get { return _jztime; }
		}

        /// <summary>
        /// 退还时间
        /// </summary>
        public DateTime THTime
        {
            set { _thtime = value; }
            get { return _thtime; }
        }
		
		/// <summary>
		/// 保证金来源
		/// </summary>
        public string BZJLY
		{
            set { _bzjly = value; }
            get { return _bzjly; }
		}
    	/// <summary>
		/// 投标经办人
		/// </summary>
        public string TBJBR
		{
            set { _tbjbr = value; }
            get { return _tbjbr; }
		}
		


		#endregion Model


		#region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateBD(string strtbxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPTBBaoZhengJin set ");
            strSql.Append("TBXMMC=@TBXMMC,");
            strSql.Append("TBBM=@TBBM,");
            strSql.Append("TBBZJ=@TBBZJ,");
            strSql.Append("ZBDWMC=@ZBDWMC,");
            strSql.Append("KaiHuHang=@KaiHuHang,");
            strSql.Append("YHZH=@YHZH,");
            strSql.Append("ZFFS=@ZFFS,");
            strSql.Append("JZTime=@JZTime,");
            strSql.Append("THTime=@THTime,");
            strSql.Append("BZJLY=@BZJLY,");
            strSql.Append("TBJBR=@TBJBR");
            strSql.Append(" where TBXMBH=@TBXMBH ");
            SqlParameter[] parameters = {		
			        new SqlParameter("@TBXMBH", SqlDbType.NVarChar,50),
					new SqlParameter("@TBXMMC", SqlDbType.NVarChar,200),
					new SqlParameter("@TBBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@TBBZJ", SqlDbType.Decimal),
                    new SqlParameter("@ZBDWMC", SqlDbType.NVarChar,200),
                    new SqlParameter("@KaiHuHang",SqlDbType.NVarChar,200),
                    new SqlParameter("@YHZH",SqlDbType.NVarChar,200),
                    new SqlParameter("@ZFFS",SqlDbType.NVarChar,50),
                    new SqlParameter("@JZTime",SqlDbType.DateTime),
                    new SqlParameter("@THTime",SqlDbType.DateTime),
                    new SqlParameter("@BZJLY",SqlDbType.NVarChar,50),
                    new SqlParameter("@TBJBR",SqlDbType.NVarChar,50)};
            parameters[0].Value = strtbxmbh;
            parameters[1].Value = TBXMMC;
            parameters[2].Value = TBBM;
            parameters[3].Value = TBBZJ;
            parameters[4].Value = ZBDWMC;
            parameters[5].Value = KaiHuHang;
            parameters[6].Value = YHZH;
            parameters[7].Value = ZFFS;
            parameters[8].Value = JZTime;
            parameters[9].Value = THTime;
            parameters[10].Value = BZJLY;
            parameters[11].Value = TBJBR;
            
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ERPTBBaoZhengJin(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,TBXMBH,TBXMMC,TBBM,TBBZJ,ZBDWMC,KaiHuHang,YHZH,ZFFS,JZTime,THTime,BZJLY,TBJBR ");
			strSql.Append(" FROM ERPTBBaoZhengJin ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);            
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("ID", "ERPTBBaoZhengJin"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from ERPTBBaoZhengJin");
			strSql.Append(" where ID=@ID ");

			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ERPTBBaoZhengJin(");
            strSql.Append("TBXMBH,TBXMMC,TBBM,TBBZJ,ZBDWMC,KaiHuHang,YHZH,ZFFS,JZTime,THTime,BZJLY,TBJBR )");
			strSql.Append(" values (");
            strSql.Append("@TBXMBH,@TBXMMC,@TBBM,@TBBZJ,@ZBDWMC,@KaiHuHang,@YHZH,@ZFFS,@JZTime,@THTime,@BZJLY,@TBJBR )");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
                    new SqlParameter("@TBXMBH", SqlDbType.NVarChar,50),
					new SqlParameter("@TBXMMC", SqlDbType.NVarChar,200),
					new SqlParameter("@TBBM", SqlDbType.NVarChar,50),
                    new SqlParameter("@TBBZJ", SqlDbType.Decimal),
                    new SqlParameter("@ZBDWMC", SqlDbType.NVarChar,200),
                    new SqlParameter("@KaiHuHang",SqlDbType.NVarChar,200),
                    new SqlParameter("@YHZH",SqlDbType.NVarChar,200),
                    new SqlParameter("@ZFFS",SqlDbType.NVarChar,50),
                    new SqlParameter("@JZTime",SqlDbType.DateTime),
                    new SqlParameter("@THTime",SqlDbType.DateTime),
                    new SqlParameter("@BZJLY",SqlDbType.NVarChar,50),
                    new SqlParameter("@TBJBR",SqlDbType.NVarChar,50)};
            parameters[0].Value = TBXMBH;
            parameters[1].Value = TBXMMC;
            parameters[2].Value = TBBM;
            parameters[3].Value = TBBZJ;
            parameters[4].Value = ZBDWMC;
            parameters[5].Value = KaiHuHang;
            parameters[6].Value = YHZH;
            parameters[7].Value = ZFFS;
            parameters[8].Value = JZTime;
            parameters[9].Value = THTime;
            parameters[10].Value = BZJLY;
            parameters[11].Value = TBJBR;


			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
           
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ERPTBBaoZhengJin ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);

		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,TBXMBH,TBXMMC,TBBM,TBBZJ,ZBDWMC,KaiHuHang,YHZH,ZFFS,JZTime,THTime,BZJLY,TBJBR ");
            strSql.Append(" FROM ERPTBBaoZhengJin ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                TBXMBH = ds.Tables[0].Rows[0]["TBXMBH"].ToString();
                TBXMMC = ds.Tables[0].Rows[0]["TBXMMC"].ToString();
                TBBM = ds.Tables[0].Rows[0]["TBBM"].ToString();
                if (ds.Tables[0].Rows[0]["TBBZJ"].ToString() != "")
                {
                    TBBZJ = float.Parse(ds.Tables[0].Rows[0]["TBBZJ"].ToString());
                }
                ZBDWMC = ds.Tables[0].Rows[0]["ZBDWMC"].ToString();
                KaiHuHang = ds.Tables[0].Rows[0]["KaiHuHang"].ToString();
                YHZH = ds.Tables[0].Rows[0]["YHZH"].ToString();
                ZFFS = ds.Tables[0].Rows[0]["ZFFS"].ToString();

                if (ds.Tables[0].Rows[0]["JZTime"].ToString() != "")
                {
                    JZTime = DateTime.Parse(ds.Tables[0].Rows[0]["JZTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["THTime"].ToString() != "")
                {
                    THTime = DateTime.Parse(ds.Tables[0].Rows[0]["THTime"].ToString());
                }
                BZJLY = ds.Tables[0].Rows[0]["BZJLY"].ToString();
                TBJBR = ds.Tables[0].Rows[0]["TBJBR"].ToString();

            }
           
		}
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetSTRModel(string strtbxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPTBBaoZhengJin ");
            strSql.Append(" where TBXMBH=@TBXMBH ");
            SqlParameter[] parameters = {
					new SqlParameter("@TBXMBH", SqlDbType.NVarChar,50)};
            parameters[0].Value = strtbxmbh;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                TBXMBH = ds.Tables[0].Rows[0]["TBXMBH"].ToString();
                TBXMMC = ds.Tables[0].Rows[0]["TBXMMC"].ToString();
                TBBM = ds.Tables[0].Rows[0]["TBBM"].ToString();
                if (ds.Tables[0].Rows[0]["TBBZJ"].ToString() != "")
                {
                    TBBZJ = float.Parse(ds.Tables[0].Rows[0]["TBBZJ"].ToString());
                }
                ZBDWMC = ds.Tables[0].Rows[0]["ZBDWMC"].ToString();
                KaiHuHang = ds.Tables[0].Rows[0]["KaiHuHang"].ToString();
                YHZH = ds.Tables[0].Rows[0]["YHZH"].ToString();
                ZFFS = ds.Tables[0].Rows[0]["ZFFS"].ToString();

                if (ds.Tables[0].Rows[0]["JZTime"].ToString() != "")
                {
                    JZTime = DateTime.Parse(ds.Tables[0].Rows[0]["JZTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["THTime"].ToString() != "")
                {
                    THTime = DateTime.Parse(ds.Tables[0].Rows[0]["THTime"].ToString());
                }
                BZJLY = ds.Tables[0].Rows[0]["BZJLY"].ToString();
                TBJBR = ds.Tables[0].Rows[0]["TBJBR"].ToString();
            }

        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM ERPTBBaoZhengJin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPTBBaoZhengJin");            
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.投标项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("40") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


		#endregion  成员方法
	}
}

