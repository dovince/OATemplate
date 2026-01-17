using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using System.Collections.Generic;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPBuMen。
    /// </summary>
    public class ERPBuMen
    {
        public ERPBuMen()
        { }
        #region Model
        private int _id;
        private string _bumenname;
        private string _chargeman;
        private string _telstr;
        private string _chuanzhen;
        private string _backinfo;
        private int? _dirid;
        private string _superiorman;
        private string _leadinggroup;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string BuMenName
        {
            set { _bumenname = value; }
            get { return _bumenname; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ChargeMan
        {
            set { _chargeman = value; }
            get { return _chargeman; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string TelStr
        {
            set { _telstr = value; }
            get { return _telstr; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string ChuanZhen
        {
            set { _chuanzhen = value; }
            get { return _chuanzhen; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string BackInfo
        {
            set { _backinfo = value; }
            get { return _backinfo; }
        }
        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int? DirID
        {
            set { _dirid = value; }
            get { return _dirid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperiorMan
        {
            set { _superiorman = value; }
            get { return _superiorman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeadingGroup
        {
            set { _leadinggroup = value; }
            get { return _leadinggroup; }
        }
        #endregion Model


        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPBuMen");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPBuMen");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPBuMen] (");
            strSql.Append("BuMenName,ChargeMan,TelStr,ChuanZhen,BackInfo,DirID,SuperiorMan,LeadingGroup)");
            strSql.Append(" values (");
            strSql.Append("@BuMenName,@ChargeMan,@TelStr,@ChuanZhen,@BackInfo,@DirID,@SuperiorMan,@LeadingGroup)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@BuMenName", SqlDbType.VarChar,50),
                    new SqlParameter("@ChargeMan", SqlDbType.VarChar,50),
                    new SqlParameter("@TelStr", SqlDbType.VarChar,50),
                    new SqlParameter("@ChuanZhen", SqlDbType.VarChar,50),
                    new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
                    new SqlParameter("@DirID", SqlDbType.Int,4),
                    new SqlParameter("@SuperiorMan", SqlDbType.NVarChar,50),
                    new SqlParameter("@LeadingGroup", SqlDbType.NVarChar,1000)};
            parameters[0].Value = BuMenName;
            parameters[1].Value = ChargeMan;
            parameters[2].Value = TelStr;
            parameters[3].Value = ChuanZhen;
            parameters[4].Value = BackInfo;
            parameters[5].Value = DirID;
            parameters[6].Value = SuperiorMan;
            parameters[7].Value = LeadingGroup;

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
            strSql.Append("update [ERPBuMen] set ");
            strSql.Append("BuMenName=@BuMenName,");
            strSql.Append("ChargeMan=@ChargeMan,");
            strSql.Append("TelStr=@TelStr,");
            strSql.Append("ChuanZhen=@ChuanZhen,");
            strSql.Append("BackInfo=@BackInfo,");
            strSql.Append("DirID=@DirID,");
            strSql.Append("SuperiorMan=@SuperiorMan,");
            strSql.Append("LeadingGroup=@LeadingGroup");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BuMenName", SqlDbType.VarChar,50),
                    new SqlParameter("@ChargeMan", SqlDbType.VarChar,50),
                    new SqlParameter("@TelStr", SqlDbType.VarChar,50),
                    new SqlParameter("@ChuanZhen", SqlDbType.VarChar,50),
                    new SqlParameter("@BackInfo", SqlDbType.VarChar,8000),
                    new SqlParameter("@DirID", SqlDbType.Int,4),
                    new SqlParameter("@SuperiorMan", SqlDbType.NVarChar,50),
                    new SqlParameter("@LeadingGroup", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = BuMenName;
            parameters[1].Value = ChargeMan;
            parameters[2].Value = TelStr;
            parameters[3].Value = ChuanZhen;
            parameters[4].Value = BackInfo;
            parameters[5].Value = DirID;
            parameters[6].Value = SuperiorMan;
            parameters[7].Value = LeadingGroup;
            parameters[8].Value = ID;

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
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPBuMen ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPBuMen ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)                };
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
                if (ds.Tables[0].Rows[0]["BuMenName"] != null)
                {
                    this.BuMenName = ds.Tables[0].Rows[0]["BuMenName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChargeMan"] != null)
                {
                    this.ChargeMan = ds.Tables[0].Rows[0]["ChargeMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelStr"] != null)
                {
                    this.TelStr = ds.Tables[0].Rows[0]["TelStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuanZhen"] != null)
                {
                    this.ChuanZhen = ds.Tables[0].Rows[0]["ChuanZhen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BackInfo"] != null)
                {
                    this.BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DirID"] != null && ds.Tables[0].Rows[0]["DirID"].ToString() != "")
                {
                    this.DirID = int.Parse(ds.Tables[0].Rows[0]["DirID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperiorMan"] != null)
                {
                    this.SuperiorMan = ds.Tables[0].Rows[0]["SuperiorMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LeadingGroup"] != null)
                {
                    this.LeadingGroup = ds.Tables[0].Rows[0]["LeadingGroup"].ToString();
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
            strSql.Append(" FROM ERPBuMen ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public List<ZWL.BLL.ERPBuMen> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPBuMen>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPBuMen>(ds.Tables[0]);
            }
            return list;
        }
        #endregion  成员方法
    }
}