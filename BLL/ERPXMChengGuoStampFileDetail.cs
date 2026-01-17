using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPXMChengGuoStampFileDetail。
	/// </summary>
	[Serializable]
	public partial class ERPXMChengGuoStampFileDetail
	{
		public ERPXMChengGuoStampFileDetail()
		{ }
		#region Model
		private int _id;
		private string _zlmc;
		private int _fs;
		private int _mainid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 资料名称
		/// </summary>
		public string ZLMC
		{
			set { _zlmc = value; }
			get { return _zlmc; }
		}
		/// <summary>
		/// 份数
		/// </summary>
		public int FS
		{
			set { _fs = value; }
			get { return _fs; }
		}
		/// <summary>
		/// 关联流程
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
		public ERPXMChengGuoStampFileDetail(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,ZLMC,FS,MainID ");
			strSql.Append(" FROM [ERPXMChengGuoStampFileDetail] ");
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
				if (ds.Tables[0].Rows[0]["ZLMC"] != null)
				{
					this.ZLMC = ds.Tables[0].Rows[0]["ZLMC"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FS"] != null && ds.Tables[0].Rows[0]["FS"].ToString() != "")
				{
					this.FS = int.Parse(ds.Tables[0].Rows[0]["FS"].ToString());
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
			strSql.Append("select count(1) from [ERPXMChengGuoStampFileDetail]");
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
			strSql.Append("insert into [ERPXMChengGuoStampFileDetail] (");
			strSql.Append("ZLMC,FS,MainID)");
			strSql.Append(" values (");
			strSql.Append("@ZLMC,@FS,@MainID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ZLMC", SqlDbType.NVarChar,-1),
					new SqlParameter("@FS", SqlDbType.Int,4),
					new SqlParameter("@MainID", SqlDbType.Int,4)};
			parameters[0].Value = ZLMC;
			parameters[1].Value = FS;
			parameters[2].Value = MainID;

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
			strSql.Append("update [ERPXMChengGuoStampFileDetail] set ");
			strSql.Append("ZLMC=@ZLMC,");
			strSql.Append("FS=@FS,");
			strSql.Append("MainID=@MainID");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ZLMC", SqlDbType.NVarChar,-1),
					new SqlParameter("@FS", SqlDbType.Int,4),
					new SqlParameter("@MainID", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ZLMC;
			parameters[1].Value = FS;
			parameters[2].Value = MainID;
			parameters[3].Value = ID;

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
			strSql.Append("delete from [ERPXMChengGuoStampFileDetail] ");
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
			strSql.Append("select ID,ZLMC,FS,MainID ");
			strSql.Append(" FROM [ERPXMChengGuoStampFileDetail] ");
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
				if (ds.Tables[0].Rows[0]["ZLMC"] != null)
				{
					this.ZLMC = ds.Tables[0].Rows[0]["ZLMC"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FS"] != null && ds.Tables[0].Rows[0]["FS"].ToString() != "")
				{
					this.FS = int.Parse(ds.Tables[0].Rows[0]["FS"].ToString());
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
			strSql.Append(" FROM [ERPXMChengGuoStampFileDetail] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

