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
	/// 类ERPOfficeSupplySummary。
	/// </summary>
	[Serializable]
    public partial class ERPOfficeSupplySummary
    {
        public ERPOfficeSupplySummary()
        { }
        #region Model
        private int _id;
        private string _no;
        private string _workname;
        private DateTime? _createddate;
        private string _department;
        private string _operator;
        private string _nworkid;
        private string _state;
        private string _comment;
        private string _reservedfield1;
        private string _reservedfield2;
        private string _reservedfield3;
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
        public string No
        {
            set { _no = value; }
            get { return _no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
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
        public string ReservedField1
        {
            set { _reservedfield1 = value; }
            get { return _reservedfield1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReservedField2
        {
            set { _reservedfield2 = value; }
            get { return _reservedfield2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReservedField3
        {
            set { _reservedfield3 = value; }
            get { return _reservedfield3; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPOfficeSupplySummary(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,No,WorkName,CreatedDate,Department,Operator,NWorkID,State,Comment,ReservedField1,ReservedField2,ReservedField3 ");
            strSql.Append(" FROM [ERPOfficeSupplySummary] ");
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
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    this.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Operator"] != null)
                {
                    this.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null)
                {
                    this.NWorkID = ds.Tables[0].Rows[0]["NWorkID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField1"] != null)
                {
                    this.ReservedField1 = ds.Tables[0].Rows[0]["ReservedField1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField2"] != null)
                {
                    this.ReservedField2 = ds.Tables[0].Rows[0]["ReservedField2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField3"] != null)
                {
                    this.ReservedField3 = ds.Tables[0].Rows[0]["ReservedField3"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPOfficeSupplySummary]");
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
            strSql.Append("insert into [ERPOfficeSupplySummary] (");
            strSql.Append("No,WorkName,CreatedDate,Department,Operator,NWorkID,State,Comment,ReservedField1,ReservedField2,ReservedField3)");
            strSql.Append(" values (");
            strSql.Append("@No,@WorkName,@CreatedDate,@Department,@Operator,@NWorkID,@State,@Comment,@ReservedField1,@ReservedField2,@ReservedField3)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@No", SqlDbType.NVarChar,50),
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@Department", SqlDbType.NVarChar,200),
                    new SqlParameter("@Operator", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,10),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField1", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField2", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField3", SqlDbType.NVarChar,500)};
            parameters[0].Value = No;
            parameters[1].Value = WorkName;
            parameters[2].Value = CreatedDate;
            parameters[3].Value = Department;
            parameters[4].Value = Operator;
            parameters[5].Value = NWorkID;
            parameters[6].Value = State;
            parameters[7].Value = Comment;
            parameters[8].Value = ReservedField1;
            parameters[9].Value = ReservedField2;
            parameters[10].Value = ReservedField3;

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
            strSql.Append("update [ERPOfficeSupplySummary] set ");
            strSql.Append("No=@No,");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("Department=@Department,");
            strSql.Append("Operator=@Operator,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("State=@State,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("ReservedField1=@ReservedField1,");
            strSql.Append("ReservedField2=@ReservedField2,");
            strSql.Append("ReservedField3=@ReservedField3");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@No", SqlDbType.NVarChar,50),
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@Department", SqlDbType.NVarChar,200),
                    new SqlParameter("@Operator", SqlDbType.NVarChar,50),
                    new SqlParameter("@NWorkID", SqlDbType.NVarChar,50),
                    new SqlParameter("@State", SqlDbType.NVarChar,10),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField1", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField2", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReservedField3", SqlDbType.NVarChar,500),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = No;
            parameters[1].Value = WorkName;
            parameters[2].Value = CreatedDate;
            parameters[3].Value = Department;
            parameters[4].Value = Operator;
            parameters[5].Value = NWorkID;
            parameters[6].Value = State;
            parameters[7].Value = Comment;
            parameters[8].Value = ReservedField1;
            parameters[9].Value = ReservedField2;
            parameters[10].Value = ReservedField3;
            parameters[11].Value = ID;

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
            strSql.Append("delete from [ERPOfficeSupplySummary] ");
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
            strSql.Append("select ID,No,WorkName,CreatedDate,Department,Operator,NWorkID,State,Comment,ReservedField1,ReservedField2,ReservedField3 ");
            strSql.Append(" FROM [ERPOfficeSupplySummary] ");
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
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    this.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Operator"] != null)
                {
                    this.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null)
                {
                    this.NWorkID = ds.Tables[0].Rows[0]["NWorkID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField1"] != null)
                {
                    this.ReservedField1 = ds.Tables[0].Rows[0]["ReservedField1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField2"] != null)
                {
                    this.ReservedField2 = ds.Tables[0].Rows[0]["ReservedField2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReservedField3"] != null)
                {
                    this.ReservedField3 = ds.Tables[0].Rows[0]["ReservedField3"].ToString();
                }
            }
        }


        public ZWL.BLL.ERPOfficeSupplySummary GetModelBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPOfficeSupplySummary>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPOfficeSupplySummary] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetPagingList(string strWhere, int cPage, int pSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM (select a.*,t.JieDianName,t.ShenPiUserList,t.OKUserList,t.StateNow,t.WorkFlowID,t.FormID FROM ERPOfficeSupplySummary a join [ERPNWorkToDo] t
  on a.NWorkID= t.ID) m ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize);
        }
        #endregion  Method
    }
}

