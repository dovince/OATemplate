using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;
using System.Collections.Generic;

namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPNWorkToDoLog。
	/// </summary>
	[Serializable]
	public partial class ERPNWorkToDoLog
	{
        public ERPNWorkToDoLog()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _uniqueid;
        private int? _parentid;
        private int? _recordid;
        private string _operation;
        private string _action;
        private string _description;
        private string _username;
        private DateTime _timestamp;
        private string _statenow;
        private string _shenpiuserlist;
        private string _okuserlist;
        private string _signature;
        private string _yinzhangpath;
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueID
        {
            set { _uniqueid = value; }
            get { return _uniqueid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Action
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeStamp
        {
            set { _timestamp = value; }
            get { return _timestamp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StateNow
        {
            set { _statenow = value; }
            get { return _statenow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShenPiUserList
        {
            set { _shenpiuserlist = value; }
            get { return _shenpiuserlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OKUserList
        {
            set { _okuserlist = value; }
            get { return _okuserlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Signature
        {
            set { _signature = value; }
            get { return _signature; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string YinZhangPath
        {
            set { _yinzhangpath = value; }
            get { return _yinzhangpath; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPNWorkToDoLog(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,UniqueID,ParentID,RecordID,Operation,Action,Description,UserName,TimeStamp,StateNow,ShenPiUserList,OKUserList,Signature,YinZhangPath ");
            strSql.Append(" FROM [ERPNWorkToDoLog] ");
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
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UniqueID"] != null)
                {
                    this.UniqueID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentID"] != null && ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    this.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RecordID"] != null && ds.Tables[0].Rows[0]["RecordID"].ToString() != "")
                {
                    this.RecordID = int.Parse(ds.Tables[0].Rows[0]["RecordID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Operation"] != null)
                {
                    this.Operation = ds.Tables[0].Rows[0]["Operation"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Action"] != null)
                {
                    this.Action = ds.Tables[0].Rows[0]["Action"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStamp"] != null && ds.Tables[0].Rows[0]["TimeStamp"].ToString() != "")
                {
                    this.TimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStamp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StateNow"] != null)
                {
                    this.StateNow = ds.Tables[0].Rows[0]["StateNow"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenPiUserList"] != null)
                {
                    this.ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OKUserList"] != null)
                {
                    this.OKUserList = ds.Tables[0].Rows[0]["OKUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Signature"] != null)
                {
                    this.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinZhangPath"] != null)
                {
                    this.YinZhangPath = ds.Tables[0].Rows[0]["YinZhangPath"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPNWorkToDoLog]");
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
            strSql.Append("insert into [ERPNWorkToDoLog] (");
            strSql.Append("Name,UniqueID,ParentID,RecordID,Operation,Action,Description,UserName,TimeStamp,StateNow,ShenPiUserList,OKUserList,Signature,YinZhangPath)");
            strSql.Append(" values (");
            strSql.Append("@Name,@UniqueID,@ParentID,@RecordID,@Operation,@Action,@Description,@UserName,@TimeStamp,@StateNow,@ShenPiUserList,@OKUserList,@Signature,@YinZhangPath)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@UniqueID", SqlDbType.VarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@RecordID", SqlDbType.Int,4),
					new SqlParameter("@Operation", SqlDbType.NVarChar,50),
					new SqlParameter("@Action", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,4000),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@TimeStamp", SqlDbType.DateTime),
					new SqlParameter("@StateNow", SqlDbType.NVarChar,50),
					new SqlParameter("@ShenPiUserList", SqlDbType.NVarChar,2000),
					new SqlParameter("@OKUserList", SqlDbType.NVarChar,2000),
					new SqlParameter("@Signature", SqlDbType.VarChar,500),
					new SqlParameter("@YinZhangPath", SqlDbType.NVarChar,500)};
            parameters[0].Value = Name;
            parameters[1].Value = UniqueID;
            parameters[2].Value = ParentID;
            parameters[3].Value = RecordID;
            parameters[4].Value = Operation;
            parameters[5].Value = Action;
            parameters[6].Value = Description;
            parameters[7].Value = UserName;
            parameters[8].Value = TimeStamp;
            parameters[9].Value = StateNow;
            parameters[10].Value = ShenPiUserList;
            parameters[11].Value = OKUserList;
            parameters[12].Value = Signature;
            parameters[13].Value = YinZhangPath;

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
            strSql.Append("update [ERPNWorkToDoLog] set ");
            strSql.Append("Name=@Name,");
            strSql.Append("UniqueID=@UniqueID,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("RecordID=@RecordID,");
            strSql.Append("Operation=@Operation,");
            strSql.Append("Action=@Action,");
            strSql.Append("Description=@Description,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStamp=@TimeStamp,");
            strSql.Append("StateNow=@StateNow,");
            strSql.Append("ShenPiUserList=@ShenPiUserList,");
            strSql.Append("OKUserList=@OKUserList,");
            strSql.Append("Signature=@Signature,");
            strSql.Append("YinZhangPath=@YinZhangPath");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@UniqueID", SqlDbType.VarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@RecordID", SqlDbType.Int,4),
					new SqlParameter("@Operation", SqlDbType.NVarChar,50),
					new SqlParameter("@Action", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,4000),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@TimeStamp", SqlDbType.DateTime),
					new SqlParameter("@StateNow", SqlDbType.NVarChar,50),
					new SqlParameter("@ShenPiUserList", SqlDbType.NVarChar,2000),
					new SqlParameter("@OKUserList", SqlDbType.NVarChar,2000),
					new SqlParameter("@Signature", SqlDbType.VarChar,500),
					new SqlParameter("@YinZhangPath", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = Name;
            parameters[1].Value = UniqueID;
            parameters[2].Value = ParentID;
            parameters[3].Value = RecordID;
            parameters[4].Value = Operation;
            parameters[5].Value = Action;
            parameters[6].Value = Description;
            parameters[7].Value = UserName;
            parameters[8].Value = TimeStamp;
            parameters[9].Value = StateNow;
            parameters[10].Value = ShenPiUserList;
            parameters[11].Value = OKUserList;
            parameters[12].Value = Signature;
            parameters[13].Value = YinZhangPath;
            parameters[14].Value = ID;

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
            strSql.Append("delete from [ERPNWorkToDoLog] ");
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
            strSql.Append("select ID,Name,UniqueID,ParentID,RecordID,Operation,Action,Description,UserName,TimeStamp,StateNow,ShenPiUserList,OKUserList,Signature,YinZhangPath ");
            strSql.Append(" FROM [ERPNWorkToDoLog] ");
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
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UniqueID"] != null)
                {
                    this.UniqueID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentID"] != null && ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    this.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RecordID"] != null && ds.Tables[0].Rows[0]["RecordID"].ToString() != "")
                {
                    this.RecordID = int.Parse(ds.Tables[0].Rows[0]["RecordID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Operation"] != null)
                {
                    this.Operation = ds.Tables[0].Rows[0]["Operation"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Action"] != null)
                {
                    this.Action = ds.Tables[0].Rows[0]["Action"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStamp"] != null && ds.Tables[0].Rows[0]["TimeStamp"].ToString() != "")
                {
                    this.TimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStamp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StateNow"] != null)
                {
                    this.StateNow = ds.Tables[0].Rows[0]["StateNow"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenPiUserList"] != null)
                {
                    this.ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OKUserList"] != null)
                {
                    this.OKUserList = ds.Tables[0].Rows[0]["OKUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Signature"] != null)
                {
                    this.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinZhangPath"] != null)
                {
                    this.YinZhangPath = ds.Tables[0].Rows[0]["YinZhangPath"].ToString();
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
            strSql.Append(" FROM [ERPNWorkToDoLog] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public ZWL.BLL.ERPNWorkToDoLog GetModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.CreateItem<ZWL.BLL.ERPNWorkToDoLog>(dt.Rows[0]);
            }
            return null;
        }


        public List<ZWL.BLL.ERPNWorkToDoLog> GetModelList(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPNWorkToDoLog>(dt);
            }
            return new List<ZWL.BLL.ERPNWorkToDoLog>();
        }

        #endregion  Method
    }
}

