
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPRenChaiTiDui,人才梯队
    /// </summary>
    public class ERPRenChaiTiDui
    {
        public ERPRenChaiTiDui()
        { }
        #region Model
        private int _id;//主键

        private int _nworktodoid;

        private string _workname = "";

        private DateTime _dengjitime = DateTime.Now;

        private string _xingming = "";

        private string _cengci = "";

        private string _zhuanyejishuleibie = "";

        private string _dengjiren = "";


        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// NWorkToDoID
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }

        /// <summary>
        /// 工作名称
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }

        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime DengJiTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing
        {
            set { _xingming = value; }
            get { return _xingming; }
        }

        /// <summary>
        /// 层次
        /// </summary>
        public string CengCi
        {
            set { _cengci = value; }
            get { return _cengci; }
        }

        /// <summary>
        /// 专业技术类别
        /// </summary>
        public string ZhuanYeJiShuLeiBie
        {
            set { _zhuanyejishuleibie = value; }
            get { return _zhuanyejishuleibie; }
        }

        /// <summary>
        /// 登记人
        /// </summary>
        public string DengJiRen
        {
            set { _dengjiren = value; }
            get { return _dengjiren; }
        }


        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPRenChaiTiDui(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,XingMing,CengCi,ZhuanYeJiShuLeiBie,DengJiRen ");
            strSql.Append(" FROM [ERPRenChaiTiDui] ");
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

                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingMing"] != null)
                {
                    this.XingMing = ds.Tables[0].Rows[0]["XingMing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CengCi"] != null)
                {
                    this.CengCi = ds.Tables[0].Rows[0]["CengCi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanYeJiShuLeiBie"] != null)
                {
                    this.ZhuanYeJiShuLeiBie = ds.Tables[0].Rows[0]["ZhuanYeJiShuLeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiRen"] != null)
                {
                    this.DengJiRen = ds.Tables[0].Rows[0]["DengJiRen"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPRenChaiTiDui]");
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
            strSql.Append("insert into [ERPRenChaiTiDui] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,XingMing,CengCi,ZhuanYeJiShuLeiBie,DengJiRen)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@XingMing,@CengCi,@ZhuanYeJiShuLeiBie,@DengJiRen)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@NWorkToDoID", SqlDbType.Int),

                    new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DengJiTime", SqlDbType.DateTime),

                    new SqlParameter("@XingMing", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CengCi", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZhuanYeJiShuLeiBie", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DengJiRen", SqlDbType.NVarChar, 50)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = XingMing;

            parameters[4].Value = CengCi;

            parameters[5].Value = ZhuanYeJiShuLeiBie;

            parameters[6].Value = DengJiRen;


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
            strSql.Append("update [ERPRenChaiTiDui] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("XingMing=@XingMing,");

            strSql.Append("CengCi=@CengCi,");

            strSql.Append("ZhuanYeJiShuLeiBie=@ZhuanYeJiShuLeiBie,");

            strSql.Append("DengJiRen=@DengJiRen");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@NWorkToDoID", SqlDbType.Int),

                    new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DengJiTime", SqlDbType.DateTime),

                    new SqlParameter("@XingMing", SqlDbType.NVarChar, 50),

                    new SqlParameter("@CengCi", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZhuanYeJiShuLeiBie", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DengJiRen", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = XingMing;

            parameters[4].Value = CengCi;

            parameters[5].Value = ZhuanYeJiShuLeiBie;

            parameters[6].Value = DengJiRen;

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
            strSql.Append("delete from [ERPRenChaiTiDui] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,XingMing,CengCi,ZhuanYeJiShuLeiBie,DengJiRen ");
            strSql.Append(" FROM [ERPRenChaiTiDui] ");
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

                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingMing"] != null)
                {
                    this.XingMing = ds.Tables[0].Rows[0]["XingMing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CengCi"] != null)
                {
                    this.CengCi = ds.Tables[0].Rows[0]["CengCi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanYeJiShuLeiBie"] != null)
                {
                    this.ZhuanYeJiShuLeiBie = ds.Tables[0].Rows[0]["ZhuanYeJiShuLeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DengJiRen"] != null)
                {
                    this.DengJiRen = ds.Tables[0].Rows[0]["DengJiRen"].ToString();
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
            strSql.Append(" FROM [ERPRenChaiTiDui] ");
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
            strSql.Append(" FROM ERPRenChaiTiDui ");
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