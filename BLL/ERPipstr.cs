using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZWL.DBUtility;

namespace ZWL.BLL
{

		public class ERPipstr
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// username
        /// </summary>		
		private string _username;
        public string username
        {
            get{ return _username; }
            set{ _username = value; }
        }        
		/// <summary>
		/// userid
        /// </summary>		
		private int _userid;
        public int userid
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// ipstr
        /// </summary>		
		private string _ipstr;
        public string ipstr
        {
            get{ return _ipstr; }
            set{ _ipstr = value; }
        }        
		/// <summary>
		/// macadress
        /// </summary>		
		private string _macadress;
        public string macadress
        {
            get{ return _macadress; }
            set{ _macadress = value; }
        }        
		/// <summary>
		/// flag
        /// </summary>		
		private string _flag;
        public string flag
        {
            get{ return _flag; }
            set{ _flag = value; }
        }        
		/// <summary>
		/// mode
        /// </summary>		
		private string _mode;
        public string mode
        {
            get{ return _mode; }
            set{ _mode = value; }
        }
        public bool Exists(string username, int userid, string ipstr, string macadress, string flag, string mode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPipstr");
            strSql.Append(" where ");
            strSql.Append(" username = @username and  ");
            strSql.Append(" userid = @userid and  ");
            strSql.Append(" ipstr = @ipstr and  ");
            strSql.Append(" macadress = @macadress and  ");
            strSql.Append(" flag = @flag and  ");
            strSql.Append(" mode = @mode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPipstr(");
            strSql.Append("username,userid,ipstr,macadress,flag,mode");
            strSql.Append(") values (");
            strSql.Append("@username,@userid,@ipstr,@macadress,@flag,@mode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@username", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@ipstr", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@macadress", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@flag", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@mode", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = username;
            parameters[1].Value = userid;
            parameters[2].Value = ipstr;
            parameters[3].Value = macadress;
            parameters[4].Value = flag;
            parameters[5].Value = mode;

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
            strSql.Append("update ERPipstr set ");

            strSql.Append(" username = @username , ");
            strSql.Append(" userid = @userid , ");
            strSql.Append(" ipstr = @ipstr , ");
            strSql.Append(" macadress = @macadress , ");
            strSql.Append(" flag = @flag , ");
            strSql.Append(" mode = @mode  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@username", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@ipstr", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@macadress", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@flag", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@mode", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = ID;
            parameters[1].Value = username;
            parameters[2].Value = userid;
            parameters[3].Value = ipstr;
            parameters[4].Value = macadress;
            parameters[5].Value = flag;
            parameters[6].Value = mode;
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
        public bool Delete(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPipstr ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = nID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPipstr ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public ERPipstr GetModel(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, username, userid, ipstr, macadress, flag, mode  ");
            strSql.Append("  from ERPipstr ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = nID;


            ERPipstr model = new ERPipstr();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                username = ds.Tables[0].Rows[0]["username"].ToString();
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                ipstr = ds.Tables[0].Rows[0]["ipstr"].ToString();
                macadress = ds.Tables[0].Rows[0]["macadress"].ToString();
                flag = ds.Tables[0].Rows[0]["flag"].ToString();
                mode = ds.Tables[0].Rows[0]["mode"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPipstr ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ERPipstr ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

		   
	}

}

