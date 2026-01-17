using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using Microsoft.SqlServer.Server;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPNWorkToDoExtend。
    /// </summary>
    [Serializable]
    public partial class ERPNWorkToDoExtend
    {
        public ERPNWorkToDoExtend()
        { }
        #region Model
        private int _id;
        private int _nworkid;
        private string _formcontent;
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
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormContent
        {
            set { _formcontent = value; }
            get { return _formcontent; }
        }
        #endregion Model

        #region Relative Model
        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo()
        {
            var _currentWorkFlow = new ZWL.BLL.ERPNWorkToDo();
            if (NWorkID > 0)
            {
                _currentWorkFlow.GetModel(NWorkID);
            }
            return _currentWorkFlow;
        }
        #endregion


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPNWorkToDoExtend(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkID,FormContent,ShenPiYiJian ");
            strSql.Append(" FROM [ERPNWorkToDoExtend] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FormContent"] != null)
                {
                    this.FormContent = ds.Tables[0].Rows[0]["FormContent"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPNWorkToDoExtend]");
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
            strSql.Append("insert into [ERPNWorkToDoExtend] (");
            strSql.Append("NWorkID,FormContent)");
            strSql.Append(" values (");
            strSql.Append("@NWorkID,@FormContent)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@FormContent", SqlDbType.Text)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = FormContent;

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
            strSql.Append("update [ERPNWorkToDoExtend] set ");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("FormContent=@FormContent");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@FormContent", SqlDbType.Text),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = FormContent;
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
            strSql.Append("delete from [ERPNWorkToDoExtend] ");
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
            strSql.Append("select ID,NWorkID,FormContent,ShenPiYiJian ");
            strSql.Append(" FROM [ERPNWorkToDoExtend] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        public void GetModelByNWorkId(int nworkid)
        {
            var ds = GetList("NWorkID=" + nworkid);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPNWorkToDoExtend] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

