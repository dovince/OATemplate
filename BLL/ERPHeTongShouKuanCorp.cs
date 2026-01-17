using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;
using System.Collections.Generic;//Please add references
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPHeTongShouKuanCorp。
	/// </summary>
	[Serializable]
	public partial class ERPHeTongShouKuanCorp
	{
		public ERPHeTongShouKuanCorp()
		{}
		#region Model
		private int _id;
		private string _corpname;
		private string _identifycode;
		private string _billaddress;
		private string _accountbank;
		private string _billcontent;
		private string _content;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CorpName
		{
			set{ _corpname=value;}
			get{return _corpname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IdentifyCode
		{
			set{ _identifycode=value;}
			get{return _identifycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillAddress
		{
			set{ _billaddress=value;}
			get{return _billaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountBank
		{
			set{ _accountbank=value;}
			get{return _accountbank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillContent
		{
			set{ _billcontent=value;}
			get{return _billcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPHeTongShouKuanCorp(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CorpName,IdentifyCode,BillAddress,AccountBank,BillContent,Content ");
			strSql.Append(" FROM [ERPHeTongShouKuanCorp] ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					this.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpName"]!=null)
				{
					this.CorpName=ds.Tables[0].Rows[0]["CorpName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IdentifyCode"]!=null)
				{
					this.IdentifyCode=ds.Tables[0].Rows[0]["IdentifyCode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BillAddress"]!=null)
				{
					this.BillAddress=ds.Tables[0].Rows[0]["BillAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AccountBank"]!=null)
				{
					this.AccountBank=ds.Tables[0].Rows[0]["AccountBank"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BillContent"]!=null)
				{
					this.BillContent=ds.Tables[0].Rows[0]["BillContent"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null)
				{
					this.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [ERPHeTongShouKuanCorp]");
			strSql.Append(" where ID=@ID ");

			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [ERPHeTongShouKuanCorp] (");
			strSql.Append("CorpName,IdentifyCode,BillAddress,AccountBank,BillContent,Content)");
			strSql.Append(" values (");
			strSql.Append("@CorpName,@IdentifyCode,@BillAddress,@AccountBank,@BillContent,@Content)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CorpName", SqlDbType.NVarChar,500),
					new SqlParameter("@IdentifyCode", SqlDbType.NVarChar,500),
					new SqlParameter("@BillAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,500),
					new SqlParameter("@BillContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Content", SqlDbType.NVarChar,500)};
			parameters[0].Value = CorpName;
			parameters[1].Value = IdentifyCode;
			parameters[2].Value = BillAddress;
			parameters[3].Value = AccountBank;
			parameters[4].Value = BillContent;
			parameters[5].Value = Content;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [ERPHeTongShouKuanCorp] set ");
			strSql.Append("CorpName=@CorpName,");
			strSql.Append("IdentifyCode=@IdentifyCode,");
			strSql.Append("BillAddress=@BillAddress,");
			strSql.Append("AccountBank=@AccountBank,");
			strSql.Append("BillContent=@BillContent,");
			strSql.Append("Content=@Content");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CorpName", SqlDbType.NVarChar,500),
					new SqlParameter("@IdentifyCode", SqlDbType.NVarChar,500),
					new SqlParameter("@BillAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,500),
					new SqlParameter("@BillContent", SqlDbType.NVarChar,500),
					new SqlParameter("@Content", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = CorpName;
			parameters[1].Value = IdentifyCode;
			parameters[2].Value = BillAddress;
			parameters[3].Value = AccountBank;
			parameters[4].Value = BillContent;
			parameters[5].Value = Content;
			parameters[6].Value = ID;

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
		public bool Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [ERPHeTongShouKuanCorp] ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

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
		public void GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CorpName,IdentifyCode,BillAddress,AccountBank,BillContent,Content ");
			strSql.Append(" FROM [ERPHeTongShouKuanCorp] ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					this.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpName"]!=null )
				{
					this.CorpName=ds.Tables[0].Rows[0]["CorpName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IdentifyCode"]!=null )
				{
					this.IdentifyCode=ds.Tables[0].Rows[0]["IdentifyCode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BillAddress"]!=null )
				{
					this.BillAddress=ds.Tables[0].Rows[0]["BillAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AccountBank"]!=null )
				{
					this.AccountBank=ds.Tables[0].Rows[0]["AccountBank"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BillContent"]!=null )
				{
					this.BillContent=ds.Tables[0].Rows[0]["BillContent"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null )
				{
					this.Content=ds.Tables[0].Rows[0]["Content"].ToString();
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
			strSql.Append(" FROM [ERPHeTongShouKuanCorp] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

