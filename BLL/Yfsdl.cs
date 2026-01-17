using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class Yfsdl
    {
        /// <summary>
        /// 无参构造方法
        /// </summary>
        public Yfsdl() { }


        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _mobile;
        public string mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _content;
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }

        private DateTime _deadtime;
        public DateTime deadtime
        {
            get { return _deadtime; }
            set { _deadtime = value; }
        }

        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _eid;
        public string eid
        {
            get { return _eid; }
            set { _eid = value; }
        }

        private string _userid;
        public string userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _userport;
        public string userport
        {
            get { return _userport; }
            set { _userport = value; }
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPMobile");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)				};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            string strinsert = "INSERT INTO [dbo].[yfsdl] ([id], [mobile], [content], [deadtime], [status], [eid]" +
                ", [userid], [password], [userport]) VALUES (@id, @mobile, @content, @deadtime, @" +
                "status, @eid, @userid, @password, @userport)";
            strSql.Append(strinsert);
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@mobile", global::System.Data.SqlDbType.VarChar, 15, global::System.Data.ParameterDirection.Input, 0, 0, "mobile", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@content", global::System.Data.SqlDbType.VarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "content", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@deadtime", global::System.Data.SqlDbType.Date, 3, global::System.Data.ParameterDirection.Input, 0, 0, "deadtime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@status", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@eid", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "eid", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@userid", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "userid", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@password", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "password", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@userport", global::System.Data.SqlDbType.VarChar, 4, global::System.Data.ParameterDirection.Input, 0, 0, "userport", global::System.Data.DataRowVersion.Current, false, null, "", "", "")
                                        };
            parameters[0].Value = mobile;
            parameters[1].Value = content;
            parameters[2].Value = deadtime;
            parameters[3].Value = status;
            parameters[4].Value = eid;
            parameters[5].Value = userid;
            parameters[6].Value = password;
            parameters[7].Value = userport;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.yfsdl set ");
            strSql.Append("mobile=@mobile,");
            strSql.Append("content=@content,");
            strSql.Append("deadtime=@deadtime");
            strSql.Append("status=@status");
            strSql.Append("eid=@eid");
            strSql.Append("userid=@userid");
            strSql.Append("password=@password");
            strSql.Append("userport=@userport");
            strSql.Append(" where id=" + id + " ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@mobile", global::System.Data.SqlDbType.VarChar, 15, global::System.Data.ParameterDirection.Input, 0, 0, "mobile", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@content", global::System.Data.SqlDbType.VarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "content", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@deadtime", global::System.Data.SqlDbType.Date, 3, global::System.Data.ParameterDirection.Input, 0, 0, "deadtime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@status", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@eid", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "eid", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@userid", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "userid", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@password", global::System.Data.SqlDbType.VarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "password", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@userport", global::System.Data.SqlDbType.VarChar, 4, global::System.Data.ParameterDirection.Input, 0, 0, "userport", global::System.Data.DataRowVersion.Current, false, null, "", "", "")};
            parameters[0].Value = id;
            parameters[1].Value = mobile;
            parameters[2].Value = content;
            parameters[3].Value = deadtime;
            parameters[4].Value = status;
            parameters[5].Value = eid;
            parameters[6].Value = userid;
            parameters[7].Value = password;
            parameters[8].Value = userport;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete yfsdl ");
            strSql.Append(" where id=" + nID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,6)				};
            parameters[0].Value = nID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id, mobile, content, deadtime, status, eid, userid, password, userport FROM dbo.yfsdl");
            strSql.Append(" where id=" + nID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,6)				};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                content = ds.Tables[0].Rows[0]["content"].ToString();
                status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                eid = ds.Tables[0].Rows[0]["eid"].ToString();
                userid = ds.Tables[0].Rows[0]["userid"].ToString();
                password = ds.Tables[0].Rows[0]["password"].ToString();
                userport = ds.Tables[0].Rows[0]["userport"].ToString();
                string strdeadtime = ds.Tables[0].Rows[0]["deadtime"].ToString();
                deadtime = DateTime.Parse(strdeadtime);
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append( "SELECT id, mobile, content, deadtime, status, eid, userid, password, userport FROM dbo.yfsdl");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
