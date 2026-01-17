using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPSaveFileName。
	/// </summary>
	[Serializable]
    public partial class ERPSaveFileName
    {
        public ERPSaveFileName()
        { }
        #region Model
        private int _id;
        private string _nowname;
        private string _oldname;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 新文件名
        /// </summary>
        public string NowName
        {
            set { _nowname = value; }
            get { return _nowname; }
        }
        /// <summary>
        /// 原文件名
        /// </summary>
        public string OldName
        {
            set { _oldname = value; }
            get { return _oldname; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPSaveFileName(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NowName,OldName ");
            strSql.Append(" FROM [ERPSaveFileName] ");
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
                if (ds.Tables[0].Rows[0]["NowName"] != null)
                {
                    this.NowName = ds.Tables[0].Rows[0]["NowName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldName"] != null)
                {
                    this.OldName = ds.Tables[0].Rows[0]["OldName"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPSaveFileName]");
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
            strSql.Append("insert into [ERPSaveFileName] (");
            strSql.Append("NowName,OldName)");
            strSql.Append(" values (");
            strSql.Append("@NowName,@OldName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NowName", SqlDbType.VarChar,200),
                    new SqlParameter("@OldName", SqlDbType.VarChar,200)};
            parameters[0].Value = NowName;
            parameters[1].Value = OldName;

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
            strSql.Append("update [ERPSaveFileName] set ");
            strSql.Append("NowName=@NowName,");
            strSql.Append("OldName=@OldName");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NowName", SqlDbType.VarChar,200),
                    new SqlParameter("@OldName", SqlDbType.VarChar,200),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NowName;
            parameters[1].Value = OldName;
            parameters[2].Value = ID;

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
            strSql.Append("delete from [ERPSaveFileName] ");
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
            strSql.Append("select ID,NowName,OldName ");
            strSql.Append(" FROM [ERPSaveFileName] ");
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
                if (ds.Tables[0].Rows[0]["NowName"] != null)
                {
                    this.NowName = ds.Tables[0].Rows[0]["NowName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldName"] != null)
                {
                    this.OldName = ds.Tables[0].Rows[0]["OldName"].ToString();
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
            strSql.Append(" FROM [ERPSaveFileName] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public ZWL.BLL.ERPSaveFileName GetModelByNowName(string name)
        {
            var ds = GetList(" [NowName] ='"+ name + "'");
            if(ds!=null && ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return DataTableHelper.CreateItem<ZWL.BLL.ERPSaveFileName>(dt.Rows[0]);
                }
                
            }
            return null;
        }

        #endregion  Method
    }
}
