
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类SubWorkFlowState,项目子流程状态记录表
    /// </summary>
    public class SubWorkFlowState : ModelBase
    {
        public SubWorkFlowState()
        { }
        #region Model
        private int _id;//主键

        private string _xmbh = "";

        private string _xmname = "";

        private int _nworkid;

        private int _gwhd108;

        private int _zrgzs109;

        private int _sjsc56;

        private int _sjsc75;

        private int _kgaq62;

        private int _cg71;


        /// <summary>
        /// 主键
        /// </summary>
        public override int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// XMBH
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }

        /// <summary>
        /// XMName
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }

        /// <summary>
        /// NWorkID
        /// </summary>
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }

        /// <summary>
        /// GWHD108
        /// </summary>
        public int GWHD108
        {
            set { _gwhd108 = value; }
            get { return _gwhd108; }
        }

        /// <summary>
        /// ZRGZS109
        /// </summary>
        public int ZRGZS109
        {
            set { _zrgzs109 = value; }
            get { return _zrgzs109; }
        }

        /// <summary>
        /// SJSC56
        /// </summary>
        public int SJSC56
        {
            set { _sjsc56 = value; }
            get { return _sjsc56; }
        }

        /// <summary>
        /// SJSC75
        /// </summary>
        public int SJSC75
        {
            set { _sjsc75 = value; }
            get { return _sjsc75; }
        }

        /// <summary>
        /// KGAQ62
        /// </summary>
        public int KGAQ62
        {
            set { _kgaq62 = value; }
            get { return _kgaq62; }
        }

        /// <summary>
        /// CG71
        /// </summary>
        public int CG71
        {
            set { _cg71 = value; }
            get { return _cg71; }
        }


        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SubWorkFlowState(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,XMBH,XMName,NWorkID,GWHD108,ZRGZS109,SJSC56,SJSC75,KGAQ62,CG71 ");
            strSql.Append(" FROM [SubWorkFlowState] ");
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


                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GWHD108"] != null && ds.Tables[0].Rows[0]["GWHD108"].ToString() != "")
                {
                    this.GWHD108 = int.Parse(ds.Tables[0].Rows[0]["GWHD108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZRGZS109"] != null && ds.Tables[0].Rows[0]["ZRGZS109"].ToString() != "")
                {
                    this.ZRGZS109 = int.Parse(ds.Tables[0].Rows[0]["ZRGZS109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC56"] != null && ds.Tables[0].Rows[0]["SJSC56"].ToString() != "")
                {
                    this.SJSC56 = int.Parse(ds.Tables[0].Rows[0]["SJSC56"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC75"] != null && ds.Tables[0].Rows[0]["SJSC75"].ToString() != "")
                {
                    this.SJSC75 = int.Parse(ds.Tables[0].Rows[0]["SJSC75"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KGAQ62"] != null && ds.Tables[0].Rows[0]["KGAQ62"].ToString() != "")
                {
                    this.KGAQ62 = int.Parse(ds.Tables[0].Rows[0]["KGAQ62"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CG71"] != null && ds.Tables[0].Rows[0]["CG71"].ToString() != "")
                {
                    this.CG71 = int.Parse(ds.Tables[0].Rows[0]["CG71"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [SubWorkFlowState]");
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
            strSql.Append("insert into [SubWorkFlowState] (");
            strSql.Append("XMBH,XMName,NWorkID,GWHD108,ZRGZS109,SJSC56,SJSC75,KGAQ62,CG71)");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@XMName,@NWorkID,@GWHD108,@ZRGZS109,@SJSC56,@SJSC75,@KGAQ62,@CG71)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 500),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@GWHD108", SqlDbType.Int),

                    new SqlParameter("@ZRGZS109", SqlDbType.Int),

                    new SqlParameter("@SJSC56", SqlDbType.Int),

                    new SqlParameter("@SJSC75", SqlDbType.Int),

                    new SqlParameter("@KGAQ62", SqlDbType.Int),

                    new SqlParameter("@CG71", SqlDbType.Int)};


            parameters[0].Value = XMBH;

            parameters[1].Value = XMName;

            parameters[2].Value = NWorkID;

            parameters[3].Value = GWHD108;

            parameters[4].Value = ZRGZS109;

            parameters[5].Value = SJSC56;

            parameters[6].Value = SJSC75;

            parameters[7].Value = KGAQ62;

            parameters[8].Value = CG71;


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
            strSql.Append("update [SubWorkFlowState] set ");

            strSql.Append("XMBH=@XMBH,");

            strSql.Append("XMName=@XMName,");

            strSql.Append("NWorkID=@NWorkID,");

            strSql.Append("GWHD108=@GWHD108,");

            strSql.Append("ZRGZS109=@ZRGZS109,");

            strSql.Append("SJSC56=@SJSC56,");

            strSql.Append("SJSC75=@SJSC75,");

            strSql.Append("KGAQ62=@KGAQ62,");

            strSql.Append("CG71=@CG71");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 500),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@GWHD108", SqlDbType.Int),

                    new SqlParameter("@ZRGZS109", SqlDbType.Int),

                    new SqlParameter("@SJSC56", SqlDbType.Int),

                    new SqlParameter("@SJSC75", SqlDbType.Int),

                    new SqlParameter("@KGAQ62", SqlDbType.Int),

                    new SqlParameter("@CG71", SqlDbType.Int),

                    new SqlParameter("@ID", SqlDbType.Int,4)};


            parameters[0].Value = XMBH;

            parameters[1].Value = XMName;

            parameters[2].Value = NWorkID;

            parameters[3].Value = GWHD108;

            parameters[4].Value = ZRGZS109;

            parameters[5].Value = SJSC56;

            parameters[6].Value = SJSC75;

            parameters[7].Value = KGAQ62;

            parameters[8].Value = CG71;

            parameters[9].Value = ID;

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
            strSql.Append("delete from [SubWorkFlowState] ");
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
            strSql.Append("select ID,XMBH,XMName,NWorkID,GWHD108,ZRGZS109,SJSC56,SJSC75,KGAQ62,CG71 ");
            strSql.Append(" FROM [SubWorkFlowState] ");
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

                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GWHD108"] != null && ds.Tables[0].Rows[0]["GWHD108"].ToString() != "")
                {
                    this.GWHD108 = int.Parse(ds.Tables[0].Rows[0]["GWHD108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZRGZS109"] != null && ds.Tables[0].Rows[0]["ZRGZS109"].ToString() != "")
                {
                    this.ZRGZS109 = int.Parse(ds.Tables[0].Rows[0]["ZRGZS109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC56"] != null && ds.Tables[0].Rows[0]["SJSC56"].ToString() != "")
                {
                    this.SJSC56 = int.Parse(ds.Tables[0].Rows[0]["SJSC56"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC75"] != null && ds.Tables[0].Rows[0]["SJSC75"].ToString() != "")
                {
                    this.SJSC75 = int.Parse(ds.Tables[0].Rows[0]["SJSC75"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KGAQ62"] != null && ds.Tables[0].Rows[0]["KGAQ62"].ToString() != "")
                {
                    this.KGAQ62 = int.Parse(ds.Tables[0].Rows[0]["KGAQ62"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CG71"] != null && ds.Tables[0].Rows[0]["CG71"].ToString() != "")
                {
                    this.CG71 = int.Parse(ds.Tables[0].Rows[0]["CG71"].ToString());
                }
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModelByBH(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,XMBH,XMName,NWorkID,GWHD108,ZRGZS109,SJSC56,SJSC75,KGAQ62,CG71 ");
            strSql.Append(" FROM [SubWorkFlowState] ");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50)};
            parameters[0].Value = strxmbh;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GWHD108"] != null && ds.Tables[0].Rows[0]["GWHD108"].ToString() != "")
                {
                    this.GWHD108 = int.Parse(ds.Tables[0].Rows[0]["GWHD108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZRGZS109"] != null && ds.Tables[0].Rows[0]["ZRGZS109"].ToString() != "")
                {
                    this.ZRGZS109 = int.Parse(ds.Tables[0].Rows[0]["ZRGZS109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC56"] != null && ds.Tables[0].Rows[0]["SJSC56"].ToString() != "")
                {
                    this.SJSC56 = int.Parse(ds.Tables[0].Rows[0]["SJSC56"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC75"] != null && ds.Tables[0].Rows[0]["SJSC75"].ToString() != "")
                {
                    this.SJSC75 = int.Parse(ds.Tables[0].Rows[0]["SJSC75"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KGAQ62"] != null && ds.Tables[0].Rows[0]["KGAQ62"].ToString() != "")
                {
                    this.KGAQ62 = int.Parse(ds.Tables[0].Rows[0]["KGAQ62"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CG71"] != null && ds.Tables[0].Rows[0]["CG71"].ToString() != "")
                {
                    this.CG71 = int.Parse(ds.Tables[0].Rows[0]["CG71"].ToString());
                }
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModelByNWorkID(int nworkid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,XMBH,XMName,NWorkID,GWHD108,ZRGZS109,SJSC56,SJSC75,KGAQ62,CG71 ");
            strSql.Append(" FROM [SubWorkFlowState] ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4)};
            parameters[0].Value = nworkid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GWHD108"] != null && ds.Tables[0].Rows[0]["GWHD108"].ToString() != "")
                {
                    this.GWHD108 = int.Parse(ds.Tables[0].Rows[0]["GWHD108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZRGZS109"] != null && ds.Tables[0].Rows[0]["ZRGZS109"].ToString() != "")
                {
                    this.ZRGZS109 = int.Parse(ds.Tables[0].Rows[0]["ZRGZS109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC56"] != null && ds.Tables[0].Rows[0]["SJSC56"].ToString() != "")
                {
                    this.SJSC56 = int.Parse(ds.Tables[0].Rows[0]["SJSC56"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SJSC75"] != null && ds.Tables[0].Rows[0]["SJSC75"].ToString() != "")
                {
                    this.SJSC75 = int.Parse(ds.Tables[0].Rows[0]["SJSC75"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KGAQ62"] != null && ds.Tables[0].Rows[0]["KGAQ62"].ToString() != "")
                {
                    this.KGAQ62 = int.Parse(ds.Tables[0].Rows[0]["KGAQ62"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CG71"] != null && ds.Tables[0].Rows[0]["CG71"].ToString() != "")
                {
                    this.CG71 = int.Parse(ds.Tables[0].Rows[0]["CG71"].ToString());
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
            strSql.Append(" FROM [SubWorkFlowState] ");
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
            strSql.Append(" FROM SubWorkFlowState ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,6)};
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