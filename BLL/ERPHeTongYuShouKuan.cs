using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
using System.Collections.Generic;

namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPHeTongYuShouKuan。
	/// </summary>
	[Serializable]
	public partial class ERPHeTongYuShouKuan
	{
		public ERPHeTongYuShouKuan()
		{ }
		#region Model
		private int _id;
		private string _htbh;
		private decimal _amount;
		private string _username;
		private DateTime _createdtime;
		private DateTime _receivedtime;
		private int _nworkid;
		private int? _connectid;
		private int? _connectdaoid;
		private string _comment;
		private int _flag;
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
		public string HTBH
		{
			set { _htbh = value; }
			get { return _htbh; }
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Amount
		{
			set { _amount = value; }
			get { return _amount; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Username
		{
			set { _username = value; }
			get { return _username; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedTime
		{
			set { _createdtime = value; }
			get { return _createdtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReceivedTime
		{
			set { _receivedtime = value; }
			get { return _receivedtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int NWorkID
		{
			set { _nworkid = value; }
			get { return _nworkid; }
		}
		/// <summary>
		/// 发票NWorkID
		/// </summary>
		public int? ConnectID
		{
			set { _connectid = value; }
			get { return _connectid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ConnectDaoID
		{
			set { _connectdaoid = value; }
			get { return _connectdaoid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Comment
		{
			set { _comment = value; }
			get { return _comment; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int Flag
		{
			set { _flag = value; }
			get { return _flag; }
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPHeTongYuShouKuan(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,HTBH,Amount,Username,CreatedTime,ReceivedTime,NWorkID,ConnectID,ConnectDaoID,Comment,Flag ");
			strSql.Append(" FROM [ERPHeTongYuShouKuan] ");
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
				if (ds.Tables[0].Rows[0]["HTBH"] != null)
				{
					this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
				{
					this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Username"] != null)
				{
					this.Username = ds.Tables[0].Rows[0]["Username"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
				{
					this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ReceivedTime"] != null && ds.Tables[0].Rows[0]["ReceivedTime"].ToString() != "")
				{
					this.ReceivedTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceivedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
				{
					this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ConnectID"] != null && ds.Tables[0].Rows[0]["ConnectID"].ToString() != "")
				{
					this.ConnectID = int.Parse(ds.Tables[0].Rows[0]["ConnectID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ConnectDaoID"] != null && ds.Tables[0].Rows[0]["ConnectDaoID"].ToString() != "")
				{
					this.ConnectDaoID = int.Parse(ds.Tables[0].Rows[0]["ConnectDaoID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Comment"] != null)
				{
					this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Flag"] != null && ds.Tables[0].Rows[0]["Flag"].ToString() != "")
				{
					this.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [ERPHeTongYuShouKuan]");
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
			strSql.Append("insert into [ERPHeTongYuShouKuan] (");
			strSql.Append("HTBH,Amount,Username,CreatedTime,ReceivedTime,NWorkID,ConnectID,ConnectDaoID,Comment,Flag)");
			strSql.Append(" values (");
			strSql.Append("@HTBH,@Amount,@Username,@CreatedTime,@ReceivedTime,@NWorkID,@ConnectID,@ConnectDaoID,@Comment,@Flag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@HTBH", SqlDbType.VarChar,50),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@ReceivedTime", SqlDbType.DateTime),
					new SqlParameter("@NWorkID", SqlDbType.Int,4),
					new SqlParameter("@ConnectID", SqlDbType.Int,4),
					new SqlParameter("@ConnectDaoID", SqlDbType.Int,4),
					new SqlParameter("@Comment", SqlDbType.NVarChar,-1),
					new SqlParameter("@Flag", SqlDbType.Int,4)};
			parameters[0].Value = HTBH;
			parameters[1].Value = Amount;
			parameters[2].Value = Username;
			parameters[3].Value = CreatedTime;
			parameters[4].Value = ReceivedTime;
			parameters[5].Value = NWorkID;
			parameters[6].Value = ConnectID;
			parameters[7].Value = ConnectDaoID;
			parameters[8].Value = Comment;
			parameters[9].Value = Flag;

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
			strSql.Append("update [ERPHeTongYuShouKuan] set ");
			strSql.Append("HTBH=@HTBH,");
			strSql.Append("Amount=@Amount,");
			strSql.Append("Username=@Username,");
			strSql.Append("CreatedTime=@CreatedTime,");
			strSql.Append("ReceivedTime=@ReceivedTime,");
			strSql.Append("NWorkID=@NWorkID,");
			strSql.Append("ConnectID=@ConnectID,");
			strSql.Append("ConnectDaoID=@ConnectDaoID,");
			strSql.Append("Comment=@Comment,");
			strSql.Append("Flag=@Flag");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@HTBH", SqlDbType.VarChar,50),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@ReceivedTime", SqlDbType.DateTime),
					new SqlParameter("@NWorkID", SqlDbType.Int,4),
					new SqlParameter("@ConnectID", SqlDbType.Int,4),
					new SqlParameter("@ConnectDaoID", SqlDbType.Int,4),
					new SqlParameter("@Comment", SqlDbType.NVarChar,-1),
					new SqlParameter("@Flag", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = HTBH;
			parameters[1].Value = Amount;
			parameters[2].Value = Username;
			parameters[3].Value = CreatedTime;
			parameters[4].Value = ReceivedTime;
			parameters[5].Value = NWorkID;
			parameters[6].Value = ConnectID;
			parameters[7].Value = ConnectDaoID;
			parameters[8].Value = Comment;
			parameters[9].Value = Flag;
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
			strSql.Append("delete from [ERPHeTongYuShouKuan] ");
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
			strSql.Append("select ID,HTBH,Amount,Username,CreatedTime,ReceivedTime,NWorkID,ConnectID,ConnectDaoID,Comment,Flag ");
			strSql.Append(" FROM [ERPHeTongYuShouKuan] ");
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
				if (ds.Tables[0].Rows[0]["HTBH"] != null)
				{
					this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
				{
					this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Username"] != null)
				{
					this.Username = ds.Tables[0].Rows[0]["Username"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
				{
					this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ReceivedTime"] != null && ds.Tables[0].Rows[0]["ReceivedTime"].ToString() != "")
				{
					this.ReceivedTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceivedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
				{
					this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ConnectID"] != null && ds.Tables[0].Rows[0]["ConnectID"].ToString() != "")
				{
					this.ConnectID = int.Parse(ds.Tables[0].Rows[0]["ConnectID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ConnectDaoID"] != null && ds.Tables[0].Rows[0]["ConnectDaoID"].ToString() != "")
				{
					this.ConnectDaoID = int.Parse(ds.Tables[0].Rows[0]["ConnectDaoID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Comment"] != null)
				{
					this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Flag"] != null && ds.Tables[0].Rows[0]["Flag"].ToString() != "")
				{
					this.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
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
			strSql.Append(" FROM [ERPHeTongYuShouKuan] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
		{
			return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
		}

		public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
		{
			var strSql = new StringBuilder();
			strSql.Append(@"select * from ERPHeTongYuShouKuan");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return new Pager(strSql.ToString(), cPage, pSize, orderby);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZWL.BLL.ERPHeTongYuShouKuan> GetModelListBySqlWhere(string strWhere)
		{
			var result = new List<ZWL.BLL.ERPHeTongYuShouKuan>();
			var source = GetList(strWhere);
			if (source != null && source.Tables.Count > 0)
			{
				foreach (DataRow item in source.Tables[0].Rows)
				{
					result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPHeTongYuShouKuan>(item));
				}
			}

			return result;
		}
		public ZWL.BLL.ERPHeTongYuShouKuan GetModelByWhere(string strWhere)
		{
			var list = GetModelListBySqlWhere(strWhere);
			if (list != null && list.Count > 0)
			{
				return list[0];
			}
			return null;
		}
		public ZWL.BLL.ERPHeTongYuShouKuan GetModelByWorkId(int workid)
		{
			return GetModelByWhere("NWorkID=" + workid);
		}
		#endregion  Method
	}
}

