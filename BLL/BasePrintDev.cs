using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    public class BasePrintDev
    {
		public BasePrintDev()
		{ }
		#region Model
		private int _id;
		private string _fullname;
		private string _encode;
		private string _category;
		private int _type;
		private string _description;
		private int? _sortcode;
		private int? _enabledmark;
		private DateTime _creatortime;
		private string _creatoruser;
		private int? _deletemark;
		private DateTime? _deletetime;
		private string _deleteuser;
		private string _sqltemplate;
		private string _leftfields;
		private string _printtemplate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string FullName
		{
			set { _fullname = value; }
			get { return _fullname; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Encode
		{
			set { _encode = value; }
			get { return _encode; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Category
		{
			set { _category = value; }
			get { return _category; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type
		{
			set { _type = value; }
			get { return _type; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set { _description = value; }
			get { return _description; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SortCode
		{
			set { _sortcode = value; }
			get { return _sortcode; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnabledMark
		{
			set { _enabledmark = value; }
			get { return _enabledmark; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatorTime
		{
			set { _creatortime = value; }
			get { return _creatortime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatorUser
		{
			set { _creatoruser = value; }
			get { return _creatoruser; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeleteMark
		{
			set { _deletemark = value; }
			get { return _deletemark; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DeleteTime
		{
			set { _deletetime = value; }
			get { return _deletetime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeleteUser
		{
			set { _deleteuser = value; }
			get { return _deleteuser; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string SqlTemplate
		{
			set { _sqltemplate = value; }
			get { return _sqltemplate; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LeftFields
		{
			set { _leftfields = value; }
			get { return _leftfields; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrintTemplate
		{
			set { _printtemplate = value; }
			get { return _printtemplate; }
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BasePrintDev(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,FullName,Encode,Category,Type,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,DeleteMark,DeleteTime,DeleteUser,SqlTemplate,LeftFields,PrintTemplate ");
			strSql.Append(" FROM [BasePrintDev] ");
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
				if (ds.Tables[0].Rows[0]["FullName"] != null)
				{
					this.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Encode"] != null)
				{
					this.Encode = ds.Tables[0].Rows[0]["Encode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Category"] != null)
				{
					this.Category = ds.Tables[0].Rows[0]["Category"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
				{
					this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Description"] != null)
				{
					this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SortCode"] != null && ds.Tables[0].Rows[0]["SortCode"].ToString() != "")
				{
					this.SortCode = int.Parse(ds.Tables[0].Rows[0]["SortCode"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
				{
					this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
				{
					this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreatorUser"] != null)
				{
					this.CreatorUser = ds.Tables[0].Rows[0]["CreatorUser"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
				{
					this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
				{
					this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
				{
					this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SqlTemplate"] != null)
				{
					this.SqlTemplate = ds.Tables[0].Rows[0]["SqlTemplate"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LeftFields"] != null)
				{
					this.LeftFields = ds.Tables[0].Rows[0]["LeftFields"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PrintTemplate"] != null)
				{
					this.PrintTemplate = ds.Tables[0].Rows[0]["PrintTemplate"].ToString();
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [BasePrintDev]");
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
			strSql.Append("insert into [BasePrintDev] (");
			strSql.Append("FullName,Encode,Category,Type,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,DeleteMark,DeleteTime,DeleteUser,SqlTemplate,LeftFields,PrintTemplate)");
			strSql.Append(" values (");
			strSql.Append("@FullName,@Encode,@Category,@Type,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@DeleteMark,@DeleteTime,@DeleteUser,@SqlTemplate,@LeftFields,@PrintTemplate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@FullName", SqlDbType.NVarChar,50),
					new SqlParameter("@Encode", SqlDbType.VarChar,50),
					new SqlParameter("@Category", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@SortCode", SqlDbType.Int,4),
					new SqlParameter("@EnabledMark", SqlDbType.Int,4),
					new SqlParameter("@CreatorTime", SqlDbType.DateTime),
					new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
					new SqlParameter("@DeleteMark", SqlDbType.Int,4),
					new SqlParameter("@DeleteTime", SqlDbType.DateTime),
					new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
					new SqlParameter("@SqlTemplate", SqlDbType.Text),
					new SqlParameter("@LeftFields", SqlDbType.Text),
					new SqlParameter("@PrintTemplate", SqlDbType.Text)};
			parameters[0].Value = FullName;
			parameters[1].Value = Encode;
			parameters[2].Value = Category;
			parameters[3].Value = Type;
			parameters[4].Value = Description;
			parameters[5].Value = SortCode;
			parameters[6].Value = EnabledMark;
			parameters[7].Value = CreatorTime;
			parameters[8].Value = CreatorUser;
			parameters[9].Value = DeleteMark;
			parameters[10].Value = DeleteTime;
			parameters[11].Value = DeleteUser;
			parameters[12].Value = SqlTemplate;
			parameters[13].Value = LeftFields;
			parameters[14].Value = PrintTemplate;

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
			strSql.Append("update [BasePrintDev] set ");
			strSql.Append("FullName=@FullName,");
			strSql.Append("Encode=@Encode,");
			strSql.Append("Category=@Category,");
			strSql.Append("Type=@Type,");
			strSql.Append("Description=@Description,");
			strSql.Append("SortCode=@SortCode,");
			strSql.Append("EnabledMark=@EnabledMark,");
			strSql.Append("CreatorTime=@CreatorTime,");
			strSql.Append("CreatorUser=@CreatorUser,");
			strSql.Append("DeleteMark=@DeleteMark,");
			strSql.Append("DeleteTime=@DeleteTime,");
			strSql.Append("DeleteUser=@DeleteUser,");
			strSql.Append("SqlTemplate=@SqlTemplate,");
			strSql.Append("LeftFields=@LeftFields,");
			strSql.Append("PrintTemplate=@PrintTemplate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@FullName", SqlDbType.NVarChar,50),
					new SqlParameter("@Encode", SqlDbType.VarChar,50),
					new SqlParameter("@Category", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.NVarChar,50),
					new SqlParameter("@SortCode", SqlDbType.Int,4),
					new SqlParameter("@EnabledMark", SqlDbType.Int,4),
					new SqlParameter("@CreatorTime", SqlDbType.DateTime),
					new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
					new SqlParameter("@DeleteMark", SqlDbType.Int,4),
					new SqlParameter("@DeleteTime", SqlDbType.DateTime),
					new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
					new SqlParameter("@SqlTemplate", SqlDbType.Text),
					new SqlParameter("@LeftFields", SqlDbType.Text),
					new SqlParameter("@PrintTemplate", SqlDbType.Text),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = FullName;
			parameters[1].Value = Encode;
			parameters[2].Value = Category;
			parameters[3].Value = Type;
			parameters[4].Value = Description;
			parameters[5].Value = SortCode;
			parameters[6].Value = EnabledMark;
			parameters[7].Value = CreatorTime;
			parameters[8].Value = CreatorUser;
			parameters[9].Value = DeleteMark;
			parameters[10].Value = DeleteTime;
			parameters[11].Value = DeleteUser;
			parameters[12].Value = SqlTemplate;
			parameters[13].Value = LeftFields;
			parameters[14].Value = PrintTemplate;
			parameters[15].Value = ID;

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
			strSql.Append("delete from [BasePrintDev] ");
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
			strSql.Append("select ID,FullName,Encode,Category,Type,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,DeleteMark,DeleteTime,DeleteUser,SqlTemplate,LeftFields,PrintTemplate ");
			strSql.Append(" FROM [BasePrintDev] ");
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
				if (ds.Tables[0].Rows[0]["FullName"] != null)
				{
					this.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Encode"] != null)
				{
					this.Encode = ds.Tables[0].Rows[0]["Encode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Category"] != null)
				{
					this.Category = ds.Tables[0].Rows[0]["Category"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
				{
					this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Description"] != null)
				{
					this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SortCode"] != null && ds.Tables[0].Rows[0]["SortCode"].ToString() != "")
				{
					this.SortCode = int.Parse(ds.Tables[0].Rows[0]["SortCode"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
				{
					this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
				{
					this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreatorUser"] != null)
				{
					this.CreatorUser = ds.Tables[0].Rows[0]["CreatorUser"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
				{
					this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
				{
					this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
				{
					this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SqlTemplate"] != null)
				{
					this.SqlTemplate = ds.Tables[0].Rows[0]["SqlTemplate"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LeftFields"] != null)
				{
					this.LeftFields = ds.Tables[0].Rows[0]["LeftFields"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PrintTemplate"] != null)
				{
					this.PrintTemplate = ds.Tables[0].Rows[0]["PrintTemplate"].ToString();
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
			strSql.Append(" FROM [BasePrintDev] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}
