//图片新闻类
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    public class ERPPhotoNews
    {
        public ERPPhotoNews()
        {}
        #region Model
		private int _id;
		private string _picname;
		private string _picdescribe;
		private string _imgpath;
		private string _pichref;
		private DateTime? _uploaddate;
		
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string PicName
		{
			set{ _picname=value;}
			get{return _picname;}
		}
		/// <summary>
		/// 文字描述信息
		/// </summary>
		public string PicDescribe
		{
			set{ _picdescribe=value;}
			get{return _picdescribe;}
		}
		/// <summary>
		/// 上传图片名称
		/// </summary>
		public string ImgPath
		{
			set{ _imgpath=value;}
			get{return _imgpath;}
		}
		/// <summary>
		/// 图片链接
		/// </summary>
		public string PicHref
		{
			set{ _pichref=value;}
			get{return _pichref;}
		}

		/// <summary>
		/// 录入时间
		/// </summary>
		public DateTime? UploadDate
		{
			set{ _uploaddate=value;}
			get{return _uploaddate;}
		}
		#endregion Model


		#region  成员方法

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ERPPhotoNews(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,PicName,PicDescribe,ImgPath,PicHref,UploadDate ");
			strSql.Append(" FROM ERPPhotoNews ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
                PicName = ds.Tables[0].Rows[0]["PicName"].ToString();
                PicDescribe = ds.Tables[0].Rows[0]["PicDescribe"].ToString();
                ImgPath = ds.Tables[0].Rows[0]["ImgPath"].ToString();
                PicHref = ds.Tables[0].Rows[0]["PicHref"].ToString();
                if (ds.Tables[0].Rows[0]["UploadDate"].ToString() != "")
				{
                    UploadDate = DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString());
				}
			}
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

		return DbHelperSQL.GetMaxID("ID", "ERPPhotoNews"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ERPPhotoNews");
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
			strSql.Append("insert into ERPPhotoNews(");
            strSql.Append("PicName,PicDescribe,ImgPath,PicHref,UploadDate)");
			strSql.Append(" values (");
            strSql.Append("@PicName,@PicDescribe,@ImgPath,@PicHref,@UploadDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PicName", SqlDbType.VarChar,50),
					new SqlParameter("@PicDescribe", SqlDbType.VarChar,500),
					new SqlParameter("@ImgPath", SqlDbType.VarChar,50),
					new SqlParameter("@PicHref", SqlDbType.VarChar,500),
					new SqlParameter("@UploadDate", SqlDbType.DateTime)};
			parameters[0].Value = PicName;
			parameters[1].Value = PicDescribe;
			parameters[2].Value = ImgPath;
			parameters[3].Value = PicHref;
			parameters[4].Value = UploadDate;


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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ERPPhotoNews set ");
            strSql.Append("PicName=@PicName,");
            strSql.Append("PicDescribe=@PicDescribe,");
            strSql.Append("ImgPath=@ImgPath,");
            strSql.Append("PicHref=@PicHref,");
            strSql.Append("UploadDate=@UploadDate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@PicName", SqlDbType.VarChar,50),
					new SqlParameter("@PicDescribe", SqlDbType.VarChar,500),
					new SqlParameter("@ImgPath", SqlDbType.VarChar,50),
					new SqlParameter("@PicHref", SqlDbType.VarChar,500),
					new SqlParameter("@UploadDate", SqlDbType.DateTime)};
			parameters[0].Value = ID;
			parameters[1].Value = PicName;
			parameters[2].Value = PicDescribe;
			parameters[3].Value = ImgPath;
			parameters[4].Value = PicHref;
			parameters[5].Value = UploadDate;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ERPPhotoNews ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(int nID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select top 1 ID,PicName,PicDescribe,ImgPath,PicHref,UploadDate ");
			strSql.Append(" FROM ERPPhotoNews ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
			parameters[0].Value = nID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
                PicName = ds.Tables[0].Rows[0]["PicName"].ToString();
                PicDescribe = ds.Tables[0].Rows[0]["PicDescribe"].ToString();
                ImgPath = ds.Tables[0].Rows[0]["ImgPath"].ToString();
                PicHref = ds.Tables[0].Rows[0]["PicHref"].ToString();
                if (ds.Tables[0].Rows[0]["UploadDate"].ToString() != "")
				{
                    UploadDate = DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString());
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
			strSql.Append(" FROM ERPPhotoNews ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法

    }
}
