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
    /// 类ERPListOfExpended,费用成本报销
    /// </summary>
	[Serializable]
    public partial class ERPListOfExpended
    {
        public ERPListOfExpended()
        { }
        #region Model
        private int _id;
        private string _workname;
        private decimal _amount;
        private string _department;
        private string _username;
        private DateTime _createdtime;
        private int _nworktodoid;
        private string _state;
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
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
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
        public string Department
        {
            set { _department = value; }
            get { return _department; }
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
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        #endregion Model

        #region Relative Model

        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(Username))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("Username='" + Username + "'");
                    if (tempUser != null)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }

        public ZWL.BLL.ERPNWorkToDo CurrentToDo
        {
            get
            {
                var _currentNode = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkToDoID > 0)
                {
                    _currentNode.GetModel(NWorkToDoID);
                }
                return _currentNode;
            }
        }
        public List<ZWL.BLL.ERPListOfExpendedDetail> RelativeListOfExpendedDetails
        {
            get
            {
                var result = new List<ZWL.BLL.ERPListOfExpendedDetail>();
                var _currentForm = new ZWL.BLL.ERPListOfExpendedDetail();
                if (ID > 0)
                {
                   var ds = _currentForm.GetList("RefID=" + ID);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        result = DataTableHelper.ConvertTo<ZWL.BLL.ERPListOfExpendedDetail>(ds.Tables[0]);
                    }
                }
                return result;
            }
        }
        #endregion
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPListOfExpended(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,WorkName,Amount,Department,Username,CreatedTime,NWorkToDoID,State ");
            strSql.Append(" FROM [ERPListOfExpended] ");
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
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Username"] != null)
                {
                    this.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPListOfExpended");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPListOfExpended]");
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
            strSql.Append("insert into [ERPListOfExpended] (");
            strSql.Append("ID,WorkName,Amount,Department,Username,CreatedTime,NWorkToDoID,State)");
            strSql.Append(" values (");
            strSql.Append("@ID,@WorkName,@Amount,@Department,@Username,@CreatedTime,@NWorkToDoID,@State)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@Department", SqlDbType.NVarChar,100),
                    new SqlParameter("@Username", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.NVarChar,20)};
            parameters[0].Value = ID;
            parameters[1].Value = WorkName;
            parameters[2].Value = Amount;
            parameters[3].Value = Department;
            parameters[4].Value = Username;
            parameters[5].Value = CreatedTime;
            parameters[6].Value = NWorkToDoID;
            parameters[7].Value = State;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                ID = Convert.ToInt32(obj);
                return ID;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPListOfExpended] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("Department=@Department,");
            strSql.Append("Username=@Username,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("State=@State");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@Department", SqlDbType.NVarChar,100),
                    new SqlParameter("@Username", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.NVarChar,20),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = Amount;
            parameters[2].Value = Department;
            parameters[3].Value = Username;
            parameters[4].Value = CreatedTime;
            parameters[5].Value = NWorkToDoID;
            parameters[6].Value = State;
            parameters[7].Value = ID;

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
            strSql.Append("delete from [ERPListOfExpended] ");
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
            strSql.Append("select ID,WorkName,Amount,Department,Username,CreatedTime,NWorkToDoID,State ");
            strSql.Append(" FROM [ERPListOfExpended] ");
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
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Username"] != null)
                {
                    this.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
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
            strSql.Append(" FROM [ERPListOfExpended] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int NWorkToDoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPListOfExpended ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = NWorkToDoID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                GetModel(ID);
            }
        }
    }
}