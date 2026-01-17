using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPTelFile。
    /// </summary>
    [Serializable]
    public partial class ERPTelFile
    {
        public ERPTelFile()
        { }
        #region Model
        private int _id;
        private string _titlestr;
        private string _fromuser;
        private DateTime? _timestr = DateTime.Now;
        private string _filetype;
        private string _touser;
        private string _yijieshouren;
        private string _contentstr;
        private string _fujianstr;
        private string _chuanyueyijian;
        private string _qianshouhouidlist;
        private string _chuanyuehouidlist1;
        private string _zihao = "NULL";
        private string _tousertype;
        private string _fileuploadid;
        private DateTime? _limitdate = DateTime.Now;
        
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string TitleStr
        {
            set { _titlestr = value; }
            get { return _titlestr; }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string FromUser
        {
            set { _fromuser = value; }
            get { return _fromuser; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 文件类别
        /// </summary>
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 接收人
        /// </summary>
        public string ToUser
        {
            set { _touser = value; }
            get { return _touser; }
        }
        /// <summary>
        /// 已经接收人
        /// </summary>
        public string YiJieShouRen
        {
            set { _yijieshouren = value; }
            get { return _yijieshouren; }
        }
        /// <summary>
        /// 详细说明
        /// </summary>
        public string ContentStr
        {
            set { _contentstr = value; }
            get { return _contentstr; }
        }
        /// <summary>
        /// 附件文件
        /// </summary>
        public string FuJianStr
        {
            set { _fujianstr = value; }
            get { return _fujianstr; }
        }
        /// <summary>
        /// 传阅意见
        /// </summary>
        public string ChuanYueYiJian
        {
            set { _chuanyueyijian = value; }
            get { return _chuanyueyijian; }
        }
        /// <summary>
        /// 签收文件夹
        /// </summary>
        public string QianShouHouIDList
        {
            set { _qianshouhouidlist = value; }
            get { return _qianshouhouidlist; }
        }
        /// <summary>
        /// 传阅文件夹
        /// </summary>
        public string ChuanYueHouIDList1
        {
            set { _chuanyuehouidlist1 = value; }
            get { return _chuanyuehouidlist1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zihao
        {
            set { _zihao = value; }
            get { return _zihao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToUserType
        {
            set { _tousertype = value; }
            get { return _tousertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileUploadID
        {
            set { _fileuploadid = value; }
            get { return _fileuploadid; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? LimitDate
        {
            set { _limitdate = value; }
            get { return _limitdate; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPTelFile(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TitleStr,FromUser,TimeStr,FileType,ToUser,YiJieShouRen,ContentStr,FuJianStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1,Zihao,ToUserType,FileUploadID,LimitDate ");
            strSql.Append(" FROM [ERPTelFile] ");
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
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FromUser"] != null)
                {
                    this.FromUser = ds.Tables[0].Rows[0]["FromUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStr"] != null && ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    this.TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LimitDate"] != null && ds.Tables[0].Rows[0]["LimitDate"].ToString() != "")
                {
                    this.LimitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LimitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileType"] != null)
                {
                    this.FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ToUser"] != null)
                {
                    this.ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiJieShouRen"] != null)
                {
                    this.YiJieShouRen = ds.Tables[0].Rows[0]["YiJieShouRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContentStr"] != null)
                {
                    this.ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianStr"] != null)
                {
                    this.FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuanYueYiJian"] != null)
                {
                    this.ChuanYueYiJian = ds.Tables[0].Rows[0]["ChuanYueYiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QianShouHouIDList"] != null)
                {
                    this.QianShouHouIDList = ds.Tables[0].Rows[0]["QianShouHouIDList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuanYueHouIDList1"] != null)
                {
                    this.ChuanYueHouIDList1 = ds.Tables[0].Rows[0]["ChuanYueHouIDList1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zihao"] != null)
                {
                    this.Zihao = ds.Tables[0].Rows[0]["Zihao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ToUserType"] != null)
                {
                    this.ToUserType = ds.Tables[0].Rows[0]["ToUserType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FileUploadID"] != null)
                {
                    this.FileUploadID = ds.Tables[0].Rows[0]["FileUploadID"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPTelFile]");
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
            strSql.Append("insert into [ERPTelFile] (");
            strSql.Append("TitleStr,FromUser,TimeStr,FileType,ToUser,YiJieShouRen,ContentStr,FuJianStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1,Zihao,ToUserType,FileUploadID,LimitDate)");
            strSql.Append(" values (");
            strSql.Append("@TitleStr,@FromUser,@TimeStr,@FileType,@ToUser,@YiJieShouRen,@ContentStr,@FuJianStr,@ChuanYueYiJian,@QianShouHouIDList,@ChuanYueHouIDList1,@Zihao,@ToUserType,@FileUploadID,@LimitDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
                    new SqlParameter("@FileType", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,8000),
					new SqlParameter("@YiJieShouRen", SqlDbType.VarChar,8000),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@FuJianStr", SqlDbType.VarChar,1000),
					new SqlParameter("@ChuanYueYiJian", SqlDbType.Text),
					new SqlParameter("@QianShouHouIDList", SqlDbType.VarChar,8000),
					new SqlParameter("@ChuanYueHouIDList1", SqlDbType.VarChar,8000),
					new SqlParameter("@Zihao", SqlDbType.NVarChar,20),
					new SqlParameter("@ToUserType", SqlDbType.NVarChar,20),
					new SqlParameter("@FileUploadID", SqlDbType.NVarChar,-1),
                    new SqlParameter("@LimitDate", SqlDbType.DateTime)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = FromUser;
            parameters[2].Value = TimeStr;
            parameters[3].Value = FileType;
            parameters[4].Value = ToUser;
            parameters[5].Value = YiJieShouRen;
            parameters[6].Value = ContentStr;
            parameters[7].Value = FuJianStr;
            parameters[8].Value = ChuanYueYiJian;
            parameters[9].Value = QianShouHouIDList;
            parameters[10].Value = ChuanYueHouIDList1;
            parameters[11].Value = Zihao;
            parameters[12].Value = ToUserType;
            parameters[13].Value = FileUploadID;
            parameters[14].Value = LimitDate;

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
            strSql.Append("update [ERPTelFile] set ");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("FromUser=@FromUser,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("ToUser=@ToUser,");
            strSql.Append("YiJieShouRen=@YiJieShouRen,");
            strSql.Append("ContentStr=@ContentStr,");
            strSql.Append("FuJianStr=@FuJianStr,");
            strSql.Append("ChuanYueYiJian=@ChuanYueYiJian,");
            strSql.Append("QianShouHouIDList=@QianShouHouIDList,");
            strSql.Append("ChuanYueHouIDList1=@ChuanYueHouIDList1,");
            strSql.Append("Zihao=@Zihao,");
            strSql.Append("ToUserType=@ToUserType,");
            strSql.Append("FileUploadID=@FileUploadID,");
            strSql.Append("LimitDate=@LimitDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@FileType", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,8000),
					new SqlParameter("@YiJieShouRen", SqlDbType.VarChar,8000),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@FuJianStr", SqlDbType.VarChar,1000),
					new SqlParameter("@ChuanYueYiJian", SqlDbType.Text),
					new SqlParameter("@QianShouHouIDList", SqlDbType.VarChar,8000),
					new SqlParameter("@ChuanYueHouIDList1", SqlDbType.VarChar,8000),
					new SqlParameter("@Zihao", SqlDbType.NVarChar,20),
					new SqlParameter("@ToUserType", SqlDbType.NVarChar,20),
					new SqlParameter("@FileUploadID", SqlDbType.NVarChar,-1),
					new SqlParameter("@LimitDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = FromUser;
            parameters[2].Value = TimeStr;
            parameters[3].Value = FileType;
            parameters[4].Value = ToUser;
            parameters[5].Value = YiJieShouRen;
            parameters[6].Value = ContentStr;
            parameters[7].Value = FuJianStr;
            parameters[8].Value = ChuanYueYiJian;
            parameters[9].Value = QianShouHouIDList;
            parameters[10].Value = ChuanYueHouIDList1;
            parameters[11].Value = Zihao;
            parameters[12].Value = ToUserType;
            parameters[13].Value = FileUploadID;
            parameters[14].Value = LimitDate;
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
            strSql.Append("delete from [ERPTelFile] ");
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
            strSql.Append("select ID,TitleStr,FromUser,TimeStr,FileType,ToUser,YiJieShouRen,ContentStr,FuJianStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1,Zihao,ToUserType,FileUploadID,LimitDate ");
            strSql.Append(" FROM [ERPTelFile] ");
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
                if (ds.Tables[0].Rows[0]["TitleStr"] != null)
                {
                    this.TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FromUser"] != null)
                {
                    this.FromUser = ds.Tables[0].Rows[0]["FromUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStr"] != null && ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    this.TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LimitDate"] != null && ds.Tables[0].Rows[0]["LimitDate"].ToString() != "")
                {
                    this.LimitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LimitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileType"] != null)
                {
                    this.FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ToUser"] != null)
                {
                    this.ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiJieShouRen"] != null)
                {
                    this.YiJieShouRen = ds.Tables[0].Rows[0]["YiJieShouRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContentStr"] != null)
                {
                    this.ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianStr"] != null)
                {
                    this.FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuanYueYiJian"] != null)
                {
                    this.ChuanYueYiJian = ds.Tables[0].Rows[0]["ChuanYueYiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QianShouHouIDList"] != null)
                {
                    this.QianShouHouIDList = ds.Tables[0].Rows[0]["QianShouHouIDList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuanYueHouIDList1"] != null)
                {
                    this.ChuanYueHouIDList1 = ds.Tables[0].Rows[0]["ChuanYueHouIDList1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zihao"] != null)
                {
                    this.Zihao = ds.Tables[0].Rows[0]["Zihao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ToUserType"] != null)
                {
                    this.ToUserType = ds.Tables[0].Rows[0]["ToUserType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FileUploadID"] != null)
                {
                    this.FileUploadID = ds.Tables[0].Rows[0]["FileUploadID"].ToString();
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
            strSql.Append(" FROM [ERPTelFile] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

