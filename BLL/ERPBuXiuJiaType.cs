using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    public class ERPBuXiuJiaType
    {
		public ERPBuXiuJiaType()
		{ }
		#region Model
		private int _id;
		private string _name;
		private string _username;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set { _username = value; }
			get { return _username; }
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPBuXiuJiaType(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select Id,Name,UserName ");
			strSql.Append(" FROM [ERPBuXiuJiaType] ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
				{
					this.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Name"] != null)
				{
					this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null)
				{
					this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [ERPBuXiuJiaType]");
			strSql.Append(" where Id=@Id ");

			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into [ERPBuXiuJiaType] (");
			strSql.Append("Name,UserName)");
			strSql.Append(" values (");
			strSql.Append("@Name,@UserName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
			parameters[0].Value = Name;
			parameters[1].Value = UserName;

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
			strSql.Append("update [ERPBuXiuJiaType] set ");
			strSql.Append("Name=@Name,");
			strSql.Append("UserName=@UserName");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Name;
			parameters[1].Value = UserName;
			parameters[2].Value = Id;

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
		public bool Delete(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from [ERPBuXiuJiaType] ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

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
		public void GetModel(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select Id,Name,UserName ");
			strSql.Append(" FROM [ERPBuXiuJiaType] ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
				{
					this.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Name"] != null)
				{
					this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null)
				{
					this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
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
			strSql.Append(" FROM [ERPBuXiuJiaType] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}
