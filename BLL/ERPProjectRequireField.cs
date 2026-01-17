using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;

//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPProjectRequireField。
    /// </summary>
    public partial class ERPProjectRequireField
    {
        public ERPProjectRequireField()
		{}
		#region Model
		private int _id;
		private int _parentid;
		private int _no;
		private string _namecn;
		private string _nameen;
		private string _style;
		private string _isneed;
		private int _width;
		private string _type;
		private string _functions;
		private string _childrequire;
		private string _remark;
		private string _ishidden;
		private string _issearch;
		private string _islist;
		private string _onblurcode;
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
		public int ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int No
		{
			set{ _no=value;}
			get{return _no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NameCN
		{
			set{ _namecn=value;}
			get{return _namecn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NameEN
		{
			set{ _nameen=value;}
			get{return _nameen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Style
		{
			set{ _style=value;}
			get{return _style;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsNeed
		{
			set{ _isneed=value;}
			get{return _isneed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Functions
		{
			set{ _functions=value;}
			get{return _functions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChildRequire
		{
			set{ _childrequire=value;}
			get{return _childrequire;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsHidden
		{
			set{ _ishidden=value;}
			get{return _ishidden;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsSearch
		{
			set{ _issearch=value;}
			get{return _issearch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsList
		{
			set{ _islist=value;}
			get{return _islist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OnblurCode
		{
			set{ _onblurcode=value;}
			get{return _onblurcode;}
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ERPProjectRequireField(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,ParentId,No,NameCN,NameEN,Style,IsNeed,Width,Type,Functions,ChildRequire,Remark,IsHidden,IsSearch,IsList,OnblurCode ");
			strSql.Append(" FROM [ERPProjectRequireField] ");
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
				if(ds.Tables[0].Rows[0]["ParentId"]!=null && ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					this.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["No"]!=null && ds.Tables[0].Rows[0]["No"].ToString()!="")
				{
					this.No=int.Parse(ds.Tables[0].Rows[0]["No"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NameCN"]!=null)
				{
					this.NameCN=ds.Tables[0].Rows[0]["NameCN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NameEN"]!=null)
				{
					this.NameEN=ds.Tables[0].Rows[0]["NameEN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Style"]!=null)
				{
					this.Style=ds.Tables[0].Rows[0]["Style"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsNeed"]!=null)
				{
					this.IsNeed=ds.Tables[0].Rows[0]["IsNeed"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Width"]!=null && ds.Tables[0].Rows[0]["Width"].ToString()!="")
				{
					this.Width=int.Parse(ds.Tables[0].Rows[0]["Width"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null)
				{
					this.Type=ds.Tables[0].Rows[0]["Type"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Functions"]!=null)
				{
					this.Functions=ds.Tables[0].Rows[0]["Functions"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ChildRequire"]!=null)
				{
					this.ChildRequire=ds.Tables[0].Rows[0]["ChildRequire"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null)
				{
					this.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsHidden"]!=null)
				{
					this.IsHidden=ds.Tables[0].Rows[0]["IsHidden"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsSearch"]!=null)
				{
					this.IsSearch=ds.Tables[0].Rows[0]["IsSearch"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsList"]!=null)
				{
					this.IsList=ds.Tables[0].Rows[0]["IsList"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OnblurCode"]!=null)
				{
					this.OnblurCode=ds.Tables[0].Rows[0]["OnblurCode"].ToString();
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [ERPProjectRequireField]");
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
			strSql.Append("insert into [ERPProjectRequireField] (");
			strSql.Append("ParentId,No,NameCN,NameEN,Style,IsNeed,Width,Type,Functions,ChildRequire,Remark,IsHidden,IsSearch,IsList,OnblurCode)");
			strSql.Append(" values (");
			strSql.Append("@ParentId,@No,@NameCN,@NameEN,@Style,@IsNeed,@Width,@Type,@Functions,@ChildRequire,@Remark,@IsHidden,@IsSearch,@IsList,@OnblurCode)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@No", SqlDbType.Int,4),
					new SqlParameter("@NameCN", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN", SqlDbType.NVarChar,100),
					new SqlParameter("@Style", SqlDbType.NVarChar,100),
					new SqlParameter("@IsNeed", SqlDbType.NVarChar,100),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.NVarChar,100),
					new SqlParameter("@Functions", SqlDbType.NVarChar,500),
					new SqlParameter("@ChildRequire", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsHidden", SqlDbType.NVarChar,100),
					new SqlParameter("@IsSearch", SqlDbType.NVarChar,100),
					new SqlParameter("@IsList", SqlDbType.NVarChar,100),
					new SqlParameter("@OnblurCode", SqlDbType.Text)};
			parameters[0].Value = ParentId;
			parameters[1].Value = No;
			parameters[2].Value = NameCN;
			parameters[3].Value = NameEN;
			parameters[4].Value = Style;
			parameters[5].Value = IsNeed;
			parameters[6].Value = Width;
			parameters[7].Value = Type;
			parameters[8].Value = Functions;
			parameters[9].Value = ChildRequire;
			parameters[10].Value = Remark;
			parameters[11].Value = IsHidden;
			parameters[12].Value = IsSearch;
			parameters[13].Value = IsList;
			parameters[14].Value = OnblurCode;

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
			strSql.Append("update [ERPProjectRequireField] set ");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("No=@No,");
			strSql.Append("NameCN=@NameCN,");
			strSql.Append("NameEN=@NameEN,");
			strSql.Append("Style=@Style,");
			strSql.Append("IsNeed=@IsNeed,");
			strSql.Append("Width=@Width,");
			strSql.Append("Type=@Type,");
			strSql.Append("Functions=@Functions,");
			strSql.Append("ChildRequire=@ChildRequire,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("IsHidden=@IsHidden,");
			strSql.Append("IsSearch=@IsSearch,");
			strSql.Append("IsList=@IsList,");
			strSql.Append("OnblurCode=@OnblurCode");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@No", SqlDbType.Int,4),
					new SqlParameter("@NameCN", SqlDbType.NVarChar,100),
					new SqlParameter("@NameEN", SqlDbType.NVarChar,100),
					new SqlParameter("@Style", SqlDbType.NVarChar,100),
					new SqlParameter("@IsNeed", SqlDbType.NVarChar,100),
					new SqlParameter("@Width", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.NVarChar,100),
					new SqlParameter("@Functions", SqlDbType.NVarChar,500),
					new SqlParameter("@ChildRequire", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsHidden", SqlDbType.NVarChar,100),
					new SqlParameter("@IsSearch", SqlDbType.NVarChar,100),
					new SqlParameter("@IsList", SqlDbType.NVarChar,100),
					new SqlParameter("@OnblurCode", SqlDbType.Text),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ParentId;
			parameters[1].Value = No;
			parameters[2].Value = NameCN;
			parameters[3].Value = NameEN;
			parameters[4].Value = Style;
			parameters[5].Value = IsNeed;
			parameters[6].Value = Width;
			parameters[7].Value = Type;
			parameters[8].Value = Functions;
			parameters[9].Value = ChildRequire;
			parameters[10].Value = Remark;
			parameters[11].Value = IsHidden;
			parameters[12].Value = IsSearch;
			parameters[13].Value = IsList;
			parameters[14].Value = OnblurCode;
			parameters[15].Value = ID;

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
			strSql.Append("delete from [ERPProjectRequireField] ");
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
			strSql.Append("select ID,ParentId,No,NameCN,NameEN,Style,IsNeed,Width,Type,Functions,ChildRequire,Remark,IsHidden,IsSearch,IsList,OnblurCode ");
			strSql.Append(" FROM [ERPProjectRequireField] ");
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
				if(ds.Tables[0].Rows[0]["ParentId"]!=null && ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					this.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["No"]!=null && ds.Tables[0].Rows[0]["No"].ToString()!="")
				{
					this.No=int.Parse(ds.Tables[0].Rows[0]["No"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NameCN"]!=null )
				{
					this.NameCN=ds.Tables[0].Rows[0]["NameCN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NameEN"]!=null )
				{
					this.NameEN=ds.Tables[0].Rows[0]["NameEN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Style"]!=null )
				{
					this.Style=ds.Tables[0].Rows[0]["Style"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsNeed"]!=null )
				{
					this.IsNeed=ds.Tables[0].Rows[0]["IsNeed"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Width"]!=null && ds.Tables[0].Rows[0]["Width"].ToString()!="")
				{
					this.Width=int.Parse(ds.Tables[0].Rows[0]["Width"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null )
				{
					this.Type=ds.Tables[0].Rows[0]["Type"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Functions"]!=null )
				{
					this.Functions=ds.Tables[0].Rows[0]["Functions"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ChildRequire"]!=null )
				{
					this.ChildRequire=ds.Tables[0].Rows[0]["ChildRequire"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null )
				{
					this.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsHidden"]!=null )
				{
					this.IsHidden=ds.Tables[0].Rows[0]["IsHidden"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsSearch"]!=null )
				{
					this.IsSearch=ds.Tables[0].Rows[0]["IsSearch"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsList"]!=null )
				{
					this.IsList=ds.Tables[0].Rows[0]["IsList"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OnblurCode"]!=null )
				{
					this.OnblurCode=ds.Tables[0].Rows[0]["OnblurCode"].ToString();
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
			strSql.Append(" FROM [ERPProjectRequireField] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method

        public List<ZWL.BLL.ERPProjectRequireField> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPProjectRequireField>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM ERPProjectRequireField ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            var ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Common.DataTableHelper.ConvertTo<ZWL.BLL.ERPProjectRequireField>(ds.Tables[0]);
            }
            return result;
        }
    }
}

