using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
	/// <summary>
	/// 类Flow。
	/// </summary>
	[Serializable]
	public partial class Flow
	{
		public Flow()
		{ }
		#region Model
		private int _id;
		private string _datatable;
		private string _lotid;
		private string _parentid;
		private string _recordid;
		private string _tkey;
		private string _oldvalue;
		private string _newvalue;
		private string _username;
		private int _operation;
		private DateTime _createdtime;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 表名
		/// </summary>
		public string DataTable
		{
			set { _datatable = value; }
			get { return _datatable; }
		}
		/// <summary>
		/// 批次ID
		/// </summary>
		public string LotID
		{
			set { _lotid = value; }
			get { return _lotid; }
		}
		/// <summary>
		/// 父ID
		/// </summary>
		public string ParentID
		{
			set { _parentid = value; }
			get { return _parentid; }
		}
		/// <summary>
		/// 当前记录ID
		/// </summary>
		public string RecordID
		{
			set { _recordid = value; }
			get { return _recordid; }
		}
		/// <summary>
		/// 字段名
		/// </summary>
		public string TKey
		{
			set { _tkey = value; }
			get { return _tkey; }
		}
		/// <summary>
		/// 变更前的值
		/// </summary>
		public string OldValue
		{
			set { _oldvalue = value; }
			get { return _oldvalue; }
		}
		/// <summary>
		/// 变更后的值
		/// </summary>
		public string NewValue
		{
			set { _newvalue = value; }
			get { return _newvalue; }
		}
		/// <summary>
		/// 操作人员
		/// </summary>
		public string UserName
		{
			set { _username = value; }
			get { return _username; }
		}
		/// <summary>
		/// 何种操作;Add,1;Edit,2;Delete,3
		/// </summary>
		public int Operation
		{
			set { _operation = value; }
			get { return _operation; }
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime CreatedTime
		{
			set { _createdtime = value; }
			get { return _createdtime; }
		}
		#endregion Model
		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Flow(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM [Flow] ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			SetPropertyValue(ds);
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [Flow]");
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
			strSql.Append("insert into [Flow] (");
			strSql.Append("DataTable,LotID,ParentID,RecordID,TKey,OldValue,NewValue,UserName,Operation,CreatedTime)");
			strSql.Append(" values (");
			strSql.Append("@DataTable,@LotID,@ParentID,@RecordID,@TKey,@OldValue,@NewValue,@UserName,@Operation,@CreatedTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@DataTable", SqlDbType.VarChar,50),
					new SqlParameter("@LotID", SqlDbType.VarChar,50),
					new SqlParameter("@ParentID", SqlDbType.VarChar,50),
					new SqlParameter("@RecordID", SqlDbType.VarChar,50),
					new SqlParameter("@TKey", SqlDbType.VarChar,200),
					new SqlParameter("@OldValue", SqlDbType.NVarChar,-1),
					new SqlParameter("@NewValue", SqlDbType.NVarChar,-1),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@Operation", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
			parameters[0].Value = DataTable;
			parameters[1].Value = LotID;
			parameters[2].Value = ParentID;
			parameters[3].Value = RecordID;
			parameters[4].Value = TKey;
			parameters[5].Value = OldValue;
			parameters[6].Value = NewValue;
			parameters[7].Value = UserName;
			parameters[8].Value = Operation;
			parameters[9].Value = CreatedTime;

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
			strSql.Append("update [Flow] set ");
			strSql.Append("DataTable=@DataTable,");
			strSql.Append("LotID=@LotID,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("RecordID=@RecordID,");
			strSql.Append("TKey=@TKey,");
			strSql.Append("OldValue=@OldValue,");
			strSql.Append("NewValue=@NewValue,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Operation=@Operation,");
			strSql.Append("CreatedTime=@CreatedTime");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@DataTable", SqlDbType.VarChar,50),
					new SqlParameter("@LotID", SqlDbType.VarChar,50),
					new SqlParameter("@ParentID", SqlDbType.VarChar,50),
					new SqlParameter("@RecordID", SqlDbType.VarChar,50),
					new SqlParameter("@TKey", SqlDbType.VarChar,200),
					new SqlParameter("@OldValue", SqlDbType.NVarChar,-1),
					new SqlParameter("@NewValue", SqlDbType.NVarChar,-1),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@Operation", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = DataTable;
			parameters[1].Value = LotID;
			parameters[2].Value = ParentID;
			parameters[3].Value = RecordID;
			parameters[4].Value = TKey;
			parameters[5].Value = OldValue;
			parameters[6].Value = NewValue;
			parameters[7].Value = UserName;
			parameters[8].Value = Operation;
			parameters[9].Value = CreatedTime;
			parameters[10].Value = ID;

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
			strSql.Append("delete from [Flow] ");
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
			strSql.Append("select * ");
			strSql.Append(" FROM [Flow] ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			SetPropertyValue(ds);
		}
		private void SetPropertyValue(DataSet ds)
        {
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DataTable"] != null)
				{
					this.DataTable = ds.Tables[0].Rows[0]["DataTable"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LotID"] != null)
				{
					this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ParentID"] != null)
				{
					this.ParentID = ds.Tables[0].Rows[0]["ParentID"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RecordID"] != null)
				{
					this.RecordID = ds.Tables[0].Rows[0]["RecordID"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TKey"] != null)
				{
					this.TKey = ds.Tables[0].Rows[0]["TKey"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OldValue"] != null)
				{
					this.OldValue = ds.Tables[0].Rows[0]["OldValue"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewValue"] != null)
				{
					this.NewValue = ds.Tables[0].Rows[0]["NewValue"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null)
				{
					this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Operation"] != null && ds.Tables[0].Rows[0]["Operation"].ToString() != "")
				{
					this.Operation = int.Parse(ds.Tables[0].Rows[0]["Operation"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
				{
					this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
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
			strSql.Append(" FROM [Flow] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

