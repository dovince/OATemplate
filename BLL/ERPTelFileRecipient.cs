using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPTelFileRecipient。
    /// </summary>
    [Serializable]
    public partial class ERPTelFileRecipient
    {
        public ERPTelFileRecipient()
        { }
        #region Model
        private int _id;
        private int? _telfileid;
        private int? _userid;
        private string _name;
        private string _state;
        private DateTime? _lookdate;
        private int? _alertcount;
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
        public int? TelFileID
        {
            set { _telfileid = value; }
            get { return _telfileid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? userID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 查看文件状态
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 查看日期
        /// </summary>
        public DateTime? LookDate
        {
            set { _lookdate = value; }
            get { return _lookdate; }
        }
        /// <summary>
        /// 通知查看次数
        /// </summary>
        public int? AlertCount
        {
            set { _alertcount = value; }
            get { return _alertcount; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPTelFileRecipient(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TelFileID,userID,name,State,LookDate,AlertCount ");
            strSql.Append(" FROM [ERPTelFileRecipient] ");
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
                if (ds.Tables[0].Rows[0]["TelFileID"] != null && ds.Tables[0].Rows[0]["TelFileID"].ToString() != "")
                {
                    this.TelFileID = int.Parse(ds.Tables[0].Rows[0]["TelFileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userID"] != null && ds.Tables[0].Rows[0]["userID"].ToString() != "")
                {
                    this.userID = int.Parse(ds.Tables[0].Rows[0]["userID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null)
                {
                    this.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LookDate"] != null && ds.Tables[0].Rows[0]["LookDate"].ToString() != "")
                {
                    this.LookDate = DateTime.Parse(ds.Tables[0].Rows[0]["LookDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertCount"] != null && ds.Tables[0].Rows[0]["AlertCount"].ToString() != "")
                {
                    this.AlertCount = int.Parse(ds.Tables[0].Rows[0]["AlertCount"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPTelFileRecipient]");
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
            strSql.Append("insert into [ERPTelFileRecipient] (");
            strSql.Append("TelFileID,userID,name,State,LookDate,AlertCount)");
            strSql.Append(" values (");
            strSql.Append("@TelFileID,@userID,@name,@State,@LookDate,@AlertCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TelFileID", SqlDbType.Int,4),
					new SqlParameter("@userID", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,20),
					new SqlParameter("@State", SqlDbType.NVarChar,20),
					new SqlParameter("@LookDate", SqlDbType.DateTime),
					new SqlParameter("@AlertCount", SqlDbType.Int,4)};
            parameters[0].Value = TelFileID;
            parameters[1].Value = userID;
            parameters[2].Value = name;
            parameters[3].Value = State;
            parameters[4].Value = LookDate;
            parameters[5].Value = AlertCount;

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
            strSql.Append("update [ERPTelFileRecipient] set ");
            strSql.Append("TelFileID=@TelFileID,");
            strSql.Append("userID=@userID,");
            strSql.Append("name=@name,");
            strSql.Append("State=@State,");
            strSql.Append("LookDate=@LookDate,");
            strSql.Append("AlertCount=@AlertCount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TelFileID", SqlDbType.Int,4),
					new SqlParameter("@userID", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,20),
					new SqlParameter("@State", SqlDbType.NVarChar,20),
					new SqlParameter("@LookDate", SqlDbType.DateTime),
					new SqlParameter("@AlertCount", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = TelFileID;
            parameters[1].Value = userID;
            parameters[2].Value = name;
            parameters[3].Value = State;
            parameters[4].Value = LookDate;
            parameters[5].Value = AlertCount;
            parameters[6].Value = ID;

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
            strSql.Append("delete from [ERPTelFileRecipient] ");
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
            strSql.Append("select ID,TelFileID,userID,name,State,LookDate,AlertCount ");
            strSql.Append(" FROM [ERPTelFileRecipient] ");
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
                if (ds.Tables[0].Rows[0]["TelFileID"] != null && ds.Tables[0].Rows[0]["TelFileID"].ToString() != "")
                {
                    this.TelFileID = int.Parse(ds.Tables[0].Rows[0]["TelFileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userID"] != null && ds.Tables[0].Rows[0]["userID"].ToString() != "")
                {
                    this.userID = int.Parse(ds.Tables[0].Rows[0]["userID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null)
                {
                    this.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null)
                {
                    this.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LookDate"] != null && ds.Tables[0].Rows[0]["LookDate"].ToString() != "")
                {
                    this.LookDate = DateTime.Parse(ds.Tables[0].Rows[0]["LookDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlertCount"] != null && ds.Tables[0].Rows[0]["AlertCount"].ToString() != "")
                {
                    this.AlertCount = int.Parse(ds.Tables[0].Rows[0]["AlertCount"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *, ");
            strSql.Append(" (SELECT (CASE WHEN COUNT(ID)>0 THEN '是' ELSE '否' END) from ERPBuMen where ChargeMan=name) as IsChargeMan ");
            strSql.Append(" FROM [ERPTelFileRecipient] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by (CASE STATE WHEN '已查看' THEN '2' ELSE '1' END) DESC ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_File(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.TitleStr,b.TimeStr,b.ID,b.FromUser,a.State,a.name ");
            strSql.Append(" from ERPTelFileRecipient  a  ");
            strSql.Append(" right join ERPTelFile b on a.TelFileID = b.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

