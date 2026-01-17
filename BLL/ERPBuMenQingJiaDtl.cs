
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPBuMenQingJiaDtl,部门集体请假申请明细
    /// </summary>
    public class ERPBuMenQingJiaDtl
    {
        public ERPBuMenQingJiaDtl()
        { }
        #region Model
        private int _id;//主键

        private int _mainid;

        private string _qjr = "";

        private DateTime _qjsjstart = DateTime.Now;

        private DateTime _qjsjend = DateTime.Now;

        private double _qjts;


        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 主表ID
        /// </summary>
        public int MainID
        {
            set { _mainid = value; }
            get { return _mainid; }
        }

        /// <summary>
        /// 请假人
        /// </summary>
        public string QJR
        {
            set { _qjr = value; }
            get { return _qjr; }
        }

        /// <summary>
        /// 请假时间开始
        /// </summary>
        public DateTime QJSJStart
        {
            set { _qjsjstart = value; }
            get { return _qjsjstart; }
        }

        /// <summary>
        /// 请假时间结束
        /// </summary>
        public DateTime QJSJEnd
        {
            set { _qjsjend = value; }
            get { return _qjsjend; }
        }

        /// <summary>
        /// 请假天数
        /// </summary>
        public double QJTS
        {
            set { _qjts = value; }
            get { return _qjts; }
        }


        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPBuMenQingJiaDtl(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MainID,QJR,QJSJStart,QJSJEnd,QJTS ");
            strSql.Append(" FROM [ERPBuMenQingJiaDtl] ");
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

                if (ds.Tables[0].Rows[0]["MainID"] != null && ds.Tables[0].Rows[0]["MainID"].ToString() != "")
                {
                    this.MainID = int.Parse(ds.Tables[0].Rows[0]["MainID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJR"] != null)
                {
                    this.QJR = ds.Tables[0].Rows[0]["QJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJSJStart"] != null)
                {
                    this.QJSJStart = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJEnd"] != null)
                {
                    this.QJSJEnd = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJTS"] != null && ds.Tables[0].Rows[0]["QJTS"].ToString() != "")
                {
                    this.QJTS = Convert.ToDouble(ds.Tables[0].Rows[0]["QJTS"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPBuMenQingJiaDtl]");
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
            strSql.Append("insert into [ERPBuMenQingJiaDtl] (");
            strSql.Append("MainID,QJR,QJSJStart,QJSJEnd,QJTS)");
            strSql.Append(" values (");
            strSql.Append("@MainID,@QJR,@QJSJStart,@QJSJEnd,@QJTS)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@MainID", SqlDbType.Int),
       
					new SqlParameter("@QJR", SqlDbType.NVarChar),
       
					new SqlParameter("@QJSJStart", SqlDbType.DateTime),
       
					new SqlParameter("@QJSJEnd", SqlDbType.DateTime),
       
					new SqlParameter("@QJTS", SqlDbType.Float)};

            parameters[0].Value = MainID;

            parameters[1].Value = QJR;

            parameters[2].Value = QJSJStart;

            parameters[3].Value = QJSJEnd;

            parameters[4].Value = QJTS;


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
            strSql.Append("update [ERPBuMenQingJiaDtl] set ");

            strSql.Append("MainID=@MainID,");

            strSql.Append("QJR=@QJR,");

            strSql.Append("QJSJStart=@QJSJStart,");

            strSql.Append("QJSJEnd=@QJSJEnd,");

            strSql.Append("QJTS=@QJTS");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@MainID", SqlDbType.Int),
       
					new SqlParameter("@QJR", SqlDbType.NVarChar),
       
					new SqlParameter("@QJSJStart", SqlDbType.DateTime),
       
					new SqlParameter("@QJSJEnd", SqlDbType.DateTime),
       
					new SqlParameter("@QJTS", SqlDbType.Float),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = MainID;

            parameters[1].Value = QJR;

            parameters[2].Value = QJSJStart;

            parameters[3].Value = QJSJEnd;

            parameters[4].Value = QJTS;

            parameters[5].Value = ID;

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
            strSql.Append("delete from [ERPBuMenQingJiaDtl] ");
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
            strSql.Append("select ID,MainID,QJR,QJSJStart,QJSJEnd,QJTS ");
            strSql.Append(" FROM [ERPBuMenQingJiaDtl] ");
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

                if (ds.Tables[0].Rows[0]["MainID"] != null && ds.Tables[0].Rows[0]["MainID"].ToString() != "")
                {
                    this.MainID = int.Parse(ds.Tables[0].Rows[0]["MainID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJR"] != null)
                {
                    this.QJR = ds.Tables[0].Rows[0]["QJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJSJStart"] != null)
                {
                    this.QJSJStart = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJEnd"] != null)
                {
                    this.QJSJEnd = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJTS"] != null && ds.Tables[0].Rows[0]["QJTS"].ToString() != "")
                {
                    this.QJTS = Convert.ToDouble(ds.Tables[0].Rows[0]["QJTS"].ToString());
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
            strSql.Append(" FROM [ERPBuMenQingJiaDtl] ");
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
        public void GetNWorkModel(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPBuMenQingJiaDtl ");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

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