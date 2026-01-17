using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPLanEmail。
    /// </summary>
    public class ERPLanEmail
    {
        public ERPLanEmail()
        { }
        #region Model
        private int _id;
        private string _emailtitle;
        private DateTime? _timestr;
        private string _emailcontent;
        private string _fujian;
        private string _fromuser;
        private string _touser;
        private string _emailstate;
        private int _formid; 
        private int _workflowid;
        private int _worktoid;
        private string _beiyong1;
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
        public int FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        public int WorkToDoID
        {
            set { _worktoid = value; }
            get { return _worktoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeiYong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmailTitle
        {
            set { _emailtitle = value; }
            get { return _emailtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmailContent
        {
            set { _emailcontent = value; }
            get { return _emailcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FuJian
        {
            set { _fujian = value; }
            get { return _fujian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromUser
        {
            set { _fromuser = value; }
            get { return _fromuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToUser
        {
            set { _touser = value; }
            get { return _touser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmailState
        {
            set { _emailstate = value; }
            get { return _emailstate; }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPLanEmail");
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
            //发送ajpush信息
            var res = ZWL.Common.PublicMethod.SendAjpush(ToUser, EmailTitle, EmailContent);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPLanEmail(");
            strSql.Append("FormID,WorkFlowID,BeiYong1,EmailTitle,EmailContent,FuJian,FromUser,ToUser,EmailState,WorkToDoID)");
            strSql.Append(" values (");
            strSql.Append("@FormID,@WorkFlowID,@BeiYong1,@EmailTitle,@EmailContent,@FuJian,@FromUser,@ToUser,@EmailState,@WorkToDoID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@FormID", SqlDbType.Int,6),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,6),
					new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
                    new SqlParameter("@EmailTitle", SqlDbType.VarChar,500),					
					new SqlParameter("@EmailContent", SqlDbType.Text),
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,50),
					new SqlParameter("@EmailState", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = FormID;
            parameters[1].Value = WorkFlowID;
            parameters[2].Value = BeiYong1;
            parameters[3].Value = EmailTitle;            
            parameters[4].Value = EmailContent;
            parameters[5].Value = FuJian;
            parameters[6].Value = FromUser;
            parameters[7].Value = ToUser;
            parameters[8].Value = EmailState;
            parameters[9].Value = WorkToDoID;
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
            strSql.Append("update ERPLanEmail set ");
            strSql.Append("FormID=@FormID,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("BeiYong1=@BeiYong1,");
            strSql.Append("EmailTitle=@EmailTitle,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("EmailContent=@EmailContent,");
            strSql.Append("FuJian=@FuJian,");
            strSql.Append("FromUser=@FromUser,");
            strSql.Append("ToUser=@ToUser,");
            strSql.Append("WorkToDoID=@WorkToDoID,");
            strSql.Append("EmailState=@EmailState");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6),
                    new SqlParameter("@FormID", SqlDbType.Int,6),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,6),
                    new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
					new SqlParameter("@EmailTitle", SqlDbType.VarChar,500),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@EmailContent", SqlDbType.Text),
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkToDoID", SqlDbType.Int,6),
					new SqlParameter("@EmailState", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = FormID;
            parameters[2].Value = WorkFlowID;
            parameters[3].Value = BeiYong1;
            parameters[4].Value = EmailTitle;
            parameters[5].Value = TimeStr;
            parameters[6].Value = EmailContent;
            parameters[7].Value = FuJian;
            parameters[8].Value = FromUser;
            parameters[9].Value = ToUser;
            parameters[10].Value = WorkToDoID;
            parameters[11].Value = EmailState;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPLanEmail ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)				};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,FormID,WorkFlowID,BeiYong1,EmailTitle,TimeStr,EmailContent,FuJian,FromUser,ToUser,EmailState,WorkToDoID ");
            strSql.Append(" FROM ERPLanEmail ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)				};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    FormID = int.Parse(ds.Tables[0].Rows[0]["FormID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                BeiYong1 = ds.Tables[0].Rows[0]["BeiYong1"].ToString();
                EmailTitle = ds.Tables[0].Rows[0]["EmailTitle"].ToString();
                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                EmailContent = ds.Tables[0].Rows[0]["EmailContent"].ToString();
                FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                FromUser = ds.Tables[0].Rows[0]["FromUser"].ToString();
                ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                EmailState = ds.Tables[0].Rows[0]["EmailState"].ToString();
                int nid = 0;
                if (int.TryParse(ds.Tables[0].Rows[0]["WorkToDoID"].ToString(), out nid))
                {
                    WorkToDoID = nid;
                }
                
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[FormID],[WorkFlowID],[BeiYong1],[EmailTitle],[TimeStr],[EmailContent],[FuJian],[FromUser],[ToUser],[EmailState],[WorkToDoID] ");
            strSql.Append(" FROM ERPLanEmail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
