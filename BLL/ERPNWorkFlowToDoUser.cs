namespace ZWL.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using ZWL.DBUtility;

    public class ERPNWorkFlowToDoUser
    {
        private int _id;
        private int _nodeid;
        private string _shenpiuserlist;
        private int _todoid;
        private int _workflowid;

        public ERPNWorkFlowToDoUser()
        {
        }

        public ERPNWorkFlowToDoUser(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ToDoID,NodeID,WorkFlowID,ShenPiUserList ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parameters[0].Value = ID;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ToDoID"].ToString() != "")
                {
                    this.ToDoID = int.Parse(ds.Tables[0].Rows[0]["ToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NodeID"].ToString() != "")
                {
                    this.NodeID = int.Parse(ds.Tables[0].Rows[0]["NodeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    this.WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                this.ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
            }
        }

        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPNWorkFlowToDoUser(");
            strSql.Append("ToDoID,NodeID,WorkFlowID,ShenPiUserList)");
            strSql.Append(" values (");
            strSql.Append("@ToDoID,@NodeID,@WorkFlowID,@ShenPiUserList)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ToDoID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4), new SqlParameter("@WorkFlowID", SqlDbType.Int, 4), new SqlParameter("@ShenPiUserList", SqlDbType.VarChar, 200) };
            parameters[0].Value = this.ToDoID;
            parameters[1].Value = this.NodeID;
            parameters[2].Value = this.WorkFlowID;
            parameters[3].Value = this.ShenPiUserList;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            return Convert.ToInt32(obj);
        }

        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPNWorkFlowToDoUser ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPNWorkFlowToDoUser");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool Exists(int ToDoID, int NodeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPNWorkFlowToDoUser");
            strSql.Append(" where ToDoID=@ToDoID And  NodeID=@NodeID");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ToDoID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            parameters[0].Value = ToDoID;
            parameters[1].Value = NodeID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), new SqlParameter[0]);
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ERPNWorkFlowToDoUser");
        }

        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ToDoID,NodeID,WorkFlowID,ShenPiUserList ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            parameters[0].Value = ID;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ToDoID"].ToString() != "")
                {
                    this.ToDoID = int.Parse(ds.Tables[0].Rows[0]["ToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NodeID"].ToString() != "")
                {
                    this.NodeID = int.Parse(ds.Tables[0].Rows[0]["NodeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    this.WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                this.ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
            }
        }

        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPNWorkFlowToDoUser set ");
            strSql.Append("ToDoID=@ToDoID,");
            strSql.Append("NodeID=@NodeID,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("ShenPiUserList=@ShenPiUserList");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@ToDoID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4), new SqlParameter("@WorkFlowID", SqlDbType.Int, 4), new SqlParameter("@ShenPiUserList", SqlDbType.VarChar, 200) };
            parameters[0].Value = this.ID;
            parameters[1].Value = this.ToDoID;
            parameters[2].Value = this.NodeID;
            parameters[3].Value = this.WorkFlowID;
            parameters[4].Value = this.ShenPiUserList;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void UpdateUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPNWorkFlowToDoUser set ");
            strSql.Append("ShenPiUserList=@ShenPiUserList");
            strSql.Append(" where ToDoID=@ToDoID And NodeID=@NodeID ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ToDoID", SqlDbType.Int, 4), new SqlParameter("@NodeID", SqlDbType.Int, 4), new SqlParameter("@ShenPiUserList", SqlDbType.VarChar, 200) };
            parameters[0].Value = this.ToDoID;
            parameters[1].Value = this.NodeID;
            parameters[2].Value = this.ShenPiUserList;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int NodeID
        {
            get
            {
                return this._nodeid;
            }
            set
            {
                this._nodeid = value;
            }
        }

        public string ShenPiUserList
        {
            get
            {
                return this._shenpiuserlist;
            }
            set
            {
                this._shenpiuserlist = value;
            }
        }

        public int ToDoID
        {
            get
            {
                return this._todoid;
            }
            set
            {
                this._todoid = value;
            }
        }

        public int WorkFlowID
        {
            get
            {
                return this._workflowid;
            }
            set
            {
                this._workflowid = value;
            }
        }
    }
}

