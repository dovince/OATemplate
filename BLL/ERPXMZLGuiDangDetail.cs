using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
using System.Collections.Generic;//Please add references
namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPXMZLGuiDangDetail。
	/// </summary>
	[Serializable]
    public partial class ERPXMZLGuiDangDetail
    {
        public ERPXMZLGuiDangDetail()
        { }
        #region Model
        private int _id;
        private string _dah;
        private int? _xuhao;
        private string _daname;
        private string _danwei;
        private string _count;
        private int? _nworkid;
        private string _leibie;
        private string _miji;
        private string _ztxs;
        private string _bz;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 档案号
        /// </summary>
        public string DAH
        {
            set { _dah = value; }
            get { return _dah; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? XuHao
        {
            set { _xuhao = value; }
            get { return _xuhao; }
        }
        /// <summary>
        /// 档案名称
        /// </summary>
        public string DAName
        {
            set { _daname = value; }
            get { return _daname; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string DanWei
        {
            set { _danwei = value; }
            get { return _danwei; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeiBie
        {
            set { _leibie = value; }
            get { return _leibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MiJi
        {
            set { _miji = value; }
            get { return _miji; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZTXS
        {
            set { _ztxs = value; }
            get { return _ztxs; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMZLGuiDangDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DAH,XuHao,DAName,DanWei,Count,NWorkID,LeiBie,MiJi,ZTXS,BZ ");
            strSql.Append(" FROM [ERPXMZLGuiDangDetail] ");
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
                if (ds.Tables[0].Rows[0]["DAH"] != null)
                {
                    this.DAH = ds.Tables[0].Rows[0]["DAH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XuHao"] != null && ds.Tables[0].Rows[0]["XuHao"].ToString() != "")
                {
                    this.XuHao = int.Parse(ds.Tables[0].Rows[0]["XuHao"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAName"] != null)
                {
                    this.DAName = ds.Tables[0].Rows[0]["DAName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DanWei"] != null)
                {
                    this.DanWei = ds.Tables[0].Rows[0]["DanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Count"] != null)
                {
                    this.Count = ds.Tables[0].Rows[0]["Count"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MiJi"] != null)
                {
                    this.MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZTXS"] != null)
                {
                    this.ZTXS = ds.Tables[0].Rows[0]["ZTXS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMZLGuiDangDetail]");
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
            strSql.Append("insert into [ERPXMZLGuiDangDetail] (");
            strSql.Append("DAH,XuHao,DAName,DanWei,Count,NWorkID,LeiBie,MiJi,ZTXS,BZ)");
            strSql.Append(" values (");
            strSql.Append("@DAH,@XuHao,@DAName,@DanWei,@Count,@NWorkID,@LeiBie,@MiJi,@ZTXS,@BZ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@DAH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XuHao", SqlDbType.Int,4),
                    new SqlParameter("@DAName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@DanWei", SqlDbType.NVarChar,5),
                    new SqlParameter("@Count", SqlDbType.NVarChar,15),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@LeiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@MiJi", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZTXS", SqlDbType.NVarChar,200),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,200)};
            parameters[0].Value = DAH;
            parameters[1].Value = XuHao;
            parameters[2].Value = DAName;
            parameters[3].Value = DanWei;
            parameters[4].Value = Count;
            parameters[5].Value = NWorkID;
            parameters[6].Value = LeiBie;
            parameters[7].Value = MiJi;
            parameters[8].Value = ZTXS;
            parameters[9].Value = BZ;

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
            strSql.Append("update [ERPXMZLGuiDangDetail] set ");
            strSql.Append("DAH=@DAH,");
            strSql.Append("XuHao=@XuHao,");
            strSql.Append("DAName=@DAName,");
            strSql.Append("DanWei=@DanWei,");
            strSql.Append("Count=@Count,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("LeiBie=@LeiBie,");
            strSql.Append("MiJi=@MiJi,");
            strSql.Append("ZTXS=@ZTXS,");
            strSql.Append("BZ=@BZ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DAH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XuHao", SqlDbType.Int,4),
                    new SqlParameter("@DAName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@DanWei", SqlDbType.NVarChar,5),
                    new SqlParameter("@Count", SqlDbType.NVarChar,15),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@LeiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@MiJi", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZTXS", SqlDbType.NVarChar,200),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,200),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = DAH;
            parameters[1].Value = XuHao;
            parameters[2].Value = DAName;
            parameters[3].Value = DanWei;
            parameters[4].Value = Count;
            parameters[5].Value = NWorkID;
            parameters[6].Value = LeiBie;
            parameters[7].Value = MiJi;
            parameters[8].Value = ZTXS;
            parameters[9].Value = BZ;
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
            strSql.Append("delete from [ERPXMZLGuiDangDetail] ");
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
            strSql.Append("select ID,DAH,XuHao,DAName,DanWei,Count,NWorkID,LeiBie,MiJi,ZTXS,BZ ");
            strSql.Append(" FROM [ERPXMZLGuiDangDetail] ");
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
                if (ds.Tables[0].Rows[0]["DAH"] != null)
                {
                    this.DAH = ds.Tables[0].Rows[0]["DAH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XuHao"] != null && ds.Tables[0].Rows[0]["XuHao"].ToString() != "")
                {
                    this.XuHao = int.Parse(ds.Tables[0].Rows[0]["XuHao"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAName"] != null)
                {
                    this.DAName = ds.Tables[0].Rows[0]["DAName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DanWei"] != null)
                {
                    this.DanWei = ds.Tables[0].Rows[0]["DanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Count"] != null)
                {
                    this.Count = ds.Tables[0].Rows[0]["Count"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MiJi"] != null)
                {
                    this.MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZTXS"] != null)
                {
                    this.ZTXS = ds.Tables[0].Rows[0]["ZTXS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
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
            strSql.Append(" FROM [ERPXMZLGuiDangDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        public List<ZWL.BLL.ERPXMZLGuiDangDetail> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPXMZLGuiDangDetail>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPXMZLGuiDangDetail>(ds.Tables[0]);
            }
            return result;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByNWorkID(int NWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPXMZLGuiDangDetail] ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NWorkID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkID;

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
    }
}

