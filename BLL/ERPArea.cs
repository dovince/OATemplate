using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPArea。
	/// </summary>
	[Serializable]
	public partial class ERPArea
	{
		public ERPArea()
		{}
		#region Model
		private decimal? _id;
		private string _areaname;
		private decimal? _areaid;
		private decimal? _parentid;
		private decimal? _child;
		/// <summary>
		/// 
		/// </summary>
		public decimal? ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Child
		{
			set{ _child=value;}
			get{return _child;}
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPArea(decimal ID,string AreaName,decimal AreaID,decimal ParentID,decimal Child)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,AreaName,AreaID,ParentID,Child ");
			strSql.Append(" FROM [ERPArea] ");
			strSql.Append(" where ID=@ID and AreaName=@AreaName and AreaID=@AreaID and ParentID=@ParentID and Child=@Child ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float),
					new SqlParameter("@ParentID", SqlDbType.Float),
					new SqlParameter("@Child", SqlDbType.Float)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					this.ID=decimal.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AreaName"]!=null)
				{
					this.AreaName=ds.Tables[0].Rows[0]["AreaName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AreaID"]!=null && ds.Tables[0].Rows[0]["AreaID"].ToString()!="")
				{
					this.AreaID=decimal.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					this.ParentID=decimal.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Child"]!=null && ds.Tables[0].Rows[0]["Child"].ToString()!="")
				{
					this.Child=decimal.Parse(ds.Tables[0].Rows[0]["Child"].ToString());
				}
			}
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal ID,string AreaName,decimal AreaID,decimal ParentID,decimal Child)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [ERPArea]");
			strSql.Append(" where ID=@ID and AreaName=@AreaName and AreaID=@AreaID and ParentID=@ParentID and Child=@Child ");

			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float),
					new SqlParameter("@ParentID", SqlDbType.Float),
					new SqlParameter("@Child", SqlDbType.Float)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [ERPArea] (");
			strSql.Append("ID,AreaName,AreaID,ParentID,Child)");
			strSql.Append(" values (");
			strSql.Append("@ID,@AreaName,@AreaID,@ParentID,@Child)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float,8),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float,8),
					new SqlParameter("@ParentID", SqlDbType.Float,8),
					new SqlParameter("@Child", SqlDbType.Float,8)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [ERPArea] set ");
			strSql.Append("ID=@ID,");
			strSql.Append("AreaName=@AreaName,");
			strSql.Append("AreaID=@AreaID,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("Child=@Child");
			strSql.Append(" where ID=@ID and AreaName=@AreaName and AreaID=@AreaID and ParentID=@ParentID and Child=@Child ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float,8),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float,8),
					new SqlParameter("@ParentID", SqlDbType.Float,8),
					new SqlParameter("@Child", SqlDbType.Float,8)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(decimal ID,string AreaName,decimal AreaID,decimal ParentID,decimal Child)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [ERPArea] ");
			strSql.Append(" where ID=@ID and AreaName=@AreaName and AreaID=@AreaID and ParentID=@ParentID and Child=@Child ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float),
					new SqlParameter("@ParentID", SqlDbType.Float),
					new SqlParameter("@Child", SqlDbType.Float)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public void GetModel(decimal ID,string AreaName,decimal AreaID,decimal ParentID,decimal Child)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,AreaName,AreaID,ParentID,Child ");
			strSql.Append(" FROM [ERPArea] ");
			strSql.Append(" where ID=@ID and AreaName=@AreaName and AreaID=@AreaID and ParentID=@ParentID and Child=@Child ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Float),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,-1),
					new SqlParameter("@AreaID", SqlDbType.Float),
					new SqlParameter("@ParentID", SqlDbType.Float),
					new SqlParameter("@Child", SqlDbType.Float)};
			parameters[0].Value = ID;
			parameters[1].Value = AreaName;
			parameters[2].Value = AreaID;
			parameters[3].Value = ParentID;
			parameters[4].Value = Child;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					this.ID=decimal.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AreaName"]!=null )
				{
					this.AreaName=ds.Tables[0].Rows[0]["AreaName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AreaID"]!=null && ds.Tables[0].Rows[0]["AreaID"].ToString()!="")
				{
					this.AreaID=decimal.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					this.ParentID=decimal.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Child"]!=null && ds.Tables[0].Rows[0]["Child"].ToString()!="")
				{
					this.Child=decimal.Parse(ds.Tables[0].Rows[0]["Child"].ToString());
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
			strSql.Append(" FROM [ERPArea] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

