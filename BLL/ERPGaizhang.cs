using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;

namespace ZWL.BLL
{

    public class ERPGaizhang
    {
        public ERPGaizhang()
        { }
        #region Model
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Department
        /// </summary>		
        private string _department;
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// UserName
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// WorkType
        /// </summary>		
        private string _worktype;
        public string WorkType
        {
            get { return _worktype; }
            set { _worktype = value; }
        }
        /// <summary>
        /// NWorkID
        /// </summary>		
        private int _nworkid;
        public int NWorkID
        {
            get { return _nworkid; }
            set { _nworkid = value; }
        }
        /// <summary>
        /// state
        /// </summary>		
        private string _state;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// beiyong1
        /// </summary>		
        private string _beiyong1;
        public string beiyong1
        {
            get { return _beiyong1; }
            set { _beiyong1 = value; }
        }
        /// <summary>
        /// beiyong2
        /// </summary>		
        private string _beiyong2;
        public string beiyong2
        {
            get { return _beiyong2; }
            set { _beiyong2 = value; }
        }
        /// <summary>
        /// zhuangtai
        /// </summary>		
        private string _zhuangtai;
        public string zhuangtai
        {
            get { return _zhuangtai; }
            set { _zhuangtai = value; }
        }
        /// <summary>
        /// Type
        /// </summary>		
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// code
        /// </summary>		
        private string _code;
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _FSDW;
        public string FSDW
        {
            get { return _FSDW; }
            set { _FSDW = value; }
        }

        private DateTime _beiyong3;
        public DateTime beiyong3
        {
            set { _beiyong3 = value; }
            get { return _beiyong3; }
        }

        /// <summary>
        /// beiyong4
        /// </summary>		
        private string _beiyong4;
        public string beiyong4
        {
            get { return _beiyong4; }
            set { _beiyong4 = value; }
        }

        #endregion
        #region  成员方法

        public bool Exists(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPGaizhang");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)            };
            parameters[0].Value = nID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPGaizhang");
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPGaizhang(");
            strSql.Append("Department,UserName,WorkType,NWorkID,state,beiyong1,beiyong2,zhuangtai,Type,code,FSDW,beiyong3,beiyong4");
            strSql.Append(") values (");
            strSql.Append("@Department,@UserName,@WorkType,@NWorkID,@state,@beiyong1,@beiyong2,@zhuangtai,@Type,@code,@FSDW,@beiyong3,@beiyong4");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Department", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@UserName", SqlDbType.NChar,10) ,
                        new SqlParameter("@WorkType", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@NWorkID", SqlDbType.Int,4) ,
                        new SqlParameter("@state", SqlDbType.NChar,10) ,
                        new SqlParameter("@beiyong1", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@beiyong2", SqlDbType.NVarChar,50),
                        new SqlParameter("@zhuangtai", SqlDbType.NVarChar,50),
                        new SqlParameter("@Type", SqlDbType.NVarChar,50),
                        new SqlParameter("@code", SqlDbType.NVarChar,50),
                        new SqlParameter("@FSDW", SqlDbType.NVarChar,50),
                        new SqlParameter("@beiyong3", SqlDbType.DateTime),
                        new SqlParameter("@beiyong4", SqlDbType.NVarChar,50)




            };


            parameters[0].Value = Department;
            parameters[1].Value = UserName;
            parameters[2].Value = WorkType;
            parameters[3].Value = NWorkID;
            parameters[4].Value = state;
            parameters[5].Value = beiyong1;
            parameters[6].Value = beiyong2;
            parameters[7].Value = zhuangtai;
            parameters[8].Value = Type;
            parameters[9].Value = code;
            parameters[10].Value = FSDW;
            parameters[11].Value = beiyong3;
            parameters[12].Value = beiyong4;


            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPGaizhang set ");


            strSql.Append(" Department = @Department , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" WorkType = @WorkType , ");
            strSql.Append(" NWorkID = @NWorkID , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" beiyong1 = @beiyong1 , ");
            strSql.Append(" beiyong2 = @beiyong2 ,");
            strSql.Append(" zhuangtai = @zhuangtai ,");
            strSql.Append(" Type = @Type ,");
            strSql.Append(" code = @code ,");
            strSql.Append(" FSDW = @FSDW , ");
            strSql.Append(" beiyong3 = @beiyong3 ,");
            strSql.Append(" beiyong4 = @beiyong4 ");



            strSql.Append(" where ID=@ID  ");

            SqlParameter[] parameters = {

                        new SqlParameter("@Department", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@UserName", SqlDbType.NChar,10) ,
                        new SqlParameter("@WorkType", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@NWorkID", SqlDbType.Int,4) ,
                        new SqlParameter("@state", SqlDbType.NChar,10) ,
                        new SqlParameter("@beiyong1", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@beiyong2", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@zhuangtai", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Type", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@code", SqlDbType.NVarChar,50),
                        new SqlParameter("@FSDW", SqlDbType.NVarChar,50),
                        new SqlParameter("@beiyong3", SqlDbType.DateTime),
                        new SqlParameter("@beiyong4", SqlDbType.NVarChar,50),



                        new SqlParameter("@ID", SqlDbType.Int,4)



            };


            parameters[0].Value = Department;
            parameters[1].Value = UserName;
            parameters[2].Value = WorkType;
            parameters[3].Value = NWorkID;
            parameters[4].Value = state;
            parameters[5].Value = beiyong1;
            parameters[6].Value = beiyong2;
            parameters[7].Value = zhuangtai;
            parameters[8].Value = Type;
            parameters[9].Value = code;
            parameters[10].Value = FSDW;
            parameters[11].Value = beiyong3;
            parameters[12].Value = beiyong4;


            parameters[13].Value = ID;
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
            strSql.Append("delete from ERPGaizhang ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)            };
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
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, Department, UserName, WorkType, NWorkID, state, beiyong1, beiyong2,zhuangtai,Type,code,FSDW,beiyong3,beiyong4 ");
            strSql.Append("  from ERPGaizhang ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)            };
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                Department = ds.Tables[0].Rows[0]["Department"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                WorkType = ds.Tables[0].Rows[0]["WorkType"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                state = ds.Tables[0].Rows[0]["state"].ToString();
                beiyong1 = ds.Tables[0].Rows[0]["beiyong1"].ToString();
                beiyong2 = ds.Tables[0].Rows[0]["beiyong2"].ToString();
                zhuangtai = ds.Tables[0].Rows[0]["zhuangtai"].ToString();
                Type = ds.Tables[0].Rows[0]["Type"].ToString();
                code = ds.Tables[0].Rows[0]["code"].ToString();
                FSDW = ds.Tables[0].Rows[0]["FSDW"].ToString();
                if (ds.Tables[0].Rows[0]["beiyong3"].ToString() != "")
                {
                    beiyong3 = Convert.ToDateTime(ds.Tables[0].Rows[0]["beiyong3"].ToString());
                }
                beiyong4 = ds.Tables[0].Rows[0]["beiyong4"].ToString();

            }

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPGaizhang ");
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
            strSql.Append(" FROM ERPGaizhang ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListAndPaging(string strWhere, int currPage, int pageSize)
        {
            return GetListAndPaging(strWhere, currPage, pageSize, "ID desc");
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public Pager GetListAndPaging(string strWhere, int currPage, int pageSize, string orderBy)
        {
            var strSql = new StringBuilder();
            strSql.Append("select gz.*,wtd.StateNow StateNow,wtd.FormID");
            strSql.Append(" FROM ERPGaizhang gz  left outer join ERPNWorkToDo wtd on gz.NWorkID=wtd.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currPage, pageSize, orderBy);
        }
        #endregion

    }
}
