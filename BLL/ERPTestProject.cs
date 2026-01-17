
using System;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZWL.DBUtility;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPTestProject,测试功能
    /// </summary>
    public class ERPTestProject
    {
        public ERPTestProject()
        { }
        #region Model
        private int _id;//主键

        private string _number = "";

        private string _xmname = "";

        private string _zylb = "";

        private double _xmzj;

        private int _nworkid;

        private DateTime _djtime = DateTime.Now;

        private string _djbm = "";

        private string _djr = "";


        /// <summary>
        /// 主键
        /// </summary>
        [Description("主键")]
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 自动编码
        /// </summary>
        [Description("自动编码")]
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Description("项目名称")]
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }

        /// <summary>
        /// 专业类别
        /// </summary>
        [Description("专业类别")]
        public string ZYLB
        {
            set { _zylb = value; }
            get { return _zylb; }
        }

        /// <summary>
        /// 项目资金
        /// </summary>
        [Description("项目资金")]
        public double XMZJ
        {
            set { _xmzj = value; }
            get { return _xmzj; }
        }

        /// <summary>
        /// 工作关联ID
        /// </summary>
        [Description("工作关联ID")]
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }

        /// <summary>
        /// 登记时间
        /// </summary>
        [Description("登记时间")]
        public DateTime DJTime
        {
            set { _djtime = value; }
            get { return _djtime; }
        }

        /// <summary>
        /// 登记部门
        /// </summary>
        [Description("登记部门")]
        public string DJBM
        {
            set { _djbm = value; }
            get { return _djbm; }
        }

        /// <summary>
        /// 登记人
        /// </summary>
        [Description("登记人")]
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }


        #endregion Model
        #region Relative Model
        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(DJR))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + DJR + "'");
                    if (tempUser != null)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }

        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo
        {
            get
            {
                var _currentToDo = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkID > 0)
                {
                    _currentToDo.GetModel(NWorkID);
                }
                return _currentToDo;
            }
        }
        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPTestProject(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPTestProject] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPTestProject]");
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
            strSql.Append("insert into [ERPTestProject] (");
            strSql.Append("Number,XMName,ZYLB,XMZJ,NWorkID,DJTime,DJBM,DJR)");
            strSql.Append(" values (");
            strSql.Append("@Number,@XMName,@ZYLB,@XMZJ,@NWorkID,@DJTime,@DJBM,@DJR)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZYLB", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMZJ", SqlDbType.Float),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50)};

            parameters[0].Value = Number;

            parameters[1].Value = XMName;

            parameters[2].Value = ZYLB;

            parameters[3].Value = XMZJ;

            parameters[4].Value = NWorkID;

            parameters[5].Value = DJTime;

            parameters[6].Value = DJBM;

            parameters[7].Value = DJR;


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
            strSql.Append("update [ERPTestProject] set ");

            strSql.Append("Number=@Number,");

            strSql.Append("XMName=@XMName,");

            strSql.Append("ZYLB=@ZYLB,");

            strSql.Append("XMZJ=@XMZJ,");

            strSql.Append("NWorkID=@NWorkID,");

            strSql.Append("DJTime=@DJTime,");

            strSql.Append("DJBM=@DJBM,");

            strSql.Append("DJR=@DJR");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMName", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZYLB", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XMZJ", SqlDbType.Float),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = Number;

            parameters[1].Value = XMName;

            parameters[2].Value = ZYLB;

            parameters[3].Value = XMZJ;

            parameters[4].Value = NWorkID;

            parameters[5].Value = DJTime;

            parameters[6].Value = DJBM;

            parameters[7].Value = DJR;

            parameters[8].Value = ID;

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
            strSql.Append("delete from [ERPTestProject] ");
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
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPTestProject] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        private void SetPropertyValue(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMZJ"] != null && ds.Tables[0].Rows[0]["XMZJ"].ToString() != "")
                {
                    this.XMZJ = Convert.ToDouble(ds.Tables[0].Rows[0]["XMZJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null)
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
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
            strSql.Append(" FROM [ERPTestProject] ");
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
        public void GetNWorkModel(int nworktid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPTestProject ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworktid;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public ZWL.BLL.ERPTestProject GetModelByWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPTestProject>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        public void GetModelByNWorkId(int workid)
        {
            var ds = GetList("NWorkID=" + workid);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPTestProject> GetListModel(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPTestProject>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPTestProject>(item));
                }
            }

            return result;
        }
        /// <summary>
        /// 获得分页后的数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int currPage, int pageSize)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat(@"select *  FROM (
                                  select p.*,WorkName,WenHao,FormID
                                  ,WorkFlowID,UserName,TimeStr
                                  ,FuJianList,ShenPiYiJian
                                  ,JieDianID,JieDianName
                                  ,ShenPiUserList,OKUserList,StateNow
                                  ,LateTime,BeiYong1,BeiYong2
                                  --,SUBSTRING(BeiYong1,0,CHARINDEX('@',BeiYong1)) Number
	                              ,SUBSTRING(BeiYong1,CHARINDEX('@',BeiYong1)+1,LEN(BeiYong1)) Name
                                  from {0} p join ERPNWorkToDo d on p.NWorkID=d.ID ) t ", "ERPTestProject");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currPage, pageSize);
        }

        public static bool DeleteAllModels(int workid)
        {
            var _currentToDo = new ZWL.BLL.ERPNWorkToDo();
            _currentToDo.GetModel(workid);
            _currentToDo.Delete(workid);
            var _currentModel = new ZWL.BLL.ERPTestProject();
            _currentModel.GetModelByNWorkId(workid);
            return _currentModel.Delete(_currentModel.ID);
        }
    }
}