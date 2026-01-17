
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
    /// 类ERPXMChengGuoStampFile,项目成果后续相关资料盖章
    /// </summary>
    public class ERPXMChengGuoStampFile
    {
        public ERPXMChengGuoStampFile()
        { }
        #region Model
        private int _id;//主键

        private string _number = "";

        private string _zllx = "";

        private string _zlyt = "";

        private string _xmfzr = "";

        private string _yzlx = "";

        private string _xmbh = "";

        private string _bgmc = "";

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
        /// 资料类型
        /// </summary>
        [Description("资料类型")]
        public string ZLLX
        {
            set { _zllx = value; }
            get { return _zllx; }
        }

        /// <summary>
        /// 资料用途
        /// </summary>
        [Description("资料用途")]
        public string ZLYT
        {
            set { _zlyt = value; }
            get { return _zlyt; }
        }

        /// <summary>
        /// 项目负责人
        /// </summary>
        [Description("项目负责人")]
        public string XMFZR
        {
            set { _xmfzr = value; }
            get { return _xmfzr; }
        }

        /// <summary>
        /// 印章类型
        /// </summary>
        [Description("印章类型")]
        public string YZLX
        {
            set { _yzlx = value; }
            get { return _yzlx; }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        [Description("项目编号")]
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }

        /// <summary>
        /// 报告名称
        /// </summary>
        [Description("报告名称")]
        public string BGMC
        {
            set { _bgmc = value; }
            get { return _bgmc; }
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
        public ERPXMChengGuoStampFile(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMChengGuoStampFile] ");
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
            strSql.Append("select count(1) from [ERPXMChengGuoStampFile]");
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
            strSql.Append("insert into [ERPXMChengGuoStampFile] (");
            strSql.Append("Number,ZLLX,ZLYT,XMFZR,YZLX,XMBH,BGMC,NWorkID,DJTime,DJBM,DJR)");
            strSql.Append(" values (");
            strSql.Append("@Number,@ZLLX,@ZLYT,@XMFZR,@YZLX,@XMBH,@BGMC,@NWorkID,@DJTime,@DJBM,@DJR)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZLLX", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZLYT", SqlDbType.NVarChar, 500),

                    new SqlParameter("@XMFZR", SqlDbType.NVarChar, 200),

                    new SqlParameter("@YZLX", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 200),

                    new SqlParameter("@BGMC", SqlDbType.NVarChar, 500),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50)};

            parameters[0].Value = Number;

            parameters[1].Value = ZLLX;

            parameters[2].Value = ZLYT;

            parameters[3].Value = XMFZR;

            parameters[4].Value = YZLX;

            parameters[5].Value = XMBH;

            parameters[6].Value = BGMC;

            parameters[7].Value = NWorkID;

            parameters[8].Value = DJTime;

            parameters[9].Value = DJBM;

            parameters[10].Value = DJR;


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
            strSql.Append("update [ERPXMChengGuoStampFile] set ");

            strSql.Append("Number=@Number,");

            strSql.Append("ZLLX=@ZLLX,");

            strSql.Append("ZLYT=@ZLYT,");

            strSql.Append("XMFZR=@XMFZR,");

            strSql.Append("YZLX=@YZLX,");

            strSql.Append("XMBH=@XMBH,");

            strSql.Append("BGMC=@BGMC,");

            strSql.Append("NWorkID=@NWorkID,");

            strSql.Append("DJTime=@DJTime,");

            strSql.Append("DJBM=@DJBM,");

            strSql.Append("DJR=@DJR");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZLLX", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZLYT", SqlDbType.NVarChar, 500),

                    new SqlParameter("@XMFZR", SqlDbType.NVarChar, 200),

                    new SqlParameter("@YZLX", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 200),

                    new SqlParameter("@BGMC", SqlDbType.NVarChar, 500),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = Number;

            parameters[1].Value = ZLLX;

            parameters[2].Value = ZLYT;

            parameters[3].Value = XMFZR;

            parameters[4].Value = YZLX;

            parameters[5].Value = XMBH;

            parameters[6].Value = BGMC;

            parameters[7].Value = NWorkID;

            parameters[8].Value = DJTime;

            parameters[9].Value = DJBM;

            parameters[10].Value = DJR;

            parameters[11].Value = ID;

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
            strSql.Append("delete from [ERPXMChengGuoStampFile] ");
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
            strSql.Append(" FROM [ERPXMChengGuoStampFile] ");
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
                if (ds.Tables[0].Rows[0]["ZLLX"] != null)
                {
                    this.ZLLX = ds.Tables[0].Rows[0]["ZLLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZLYT"] != null)
                {
                    this.ZLYT = ds.Tables[0].Rows[0]["ZLYT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YZLX"] != null)
                {
                    this.YZLX = ds.Tables[0].Rows[0]["YZLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BGMC"] != null)
                {
                    this.BGMC = ds.Tables[0].Rows[0]["BGMC"].ToString();
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
            strSql.Append(" FROM [ERPXMChengGuoStampFile] ");
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
            strSql.Append(" FROM ERPXMChengGuoStampFile ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworktid;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public ZWL.BLL.ERPXMChengGuoStampFile GetModelByWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPXMChengGuoStampFile>(ds.Tables[0].Rows[0]);
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
        public List<ZWL.BLL.ERPXMChengGuoStampFile> GetListModel(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPXMChengGuoStampFile>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPXMChengGuoStampFile>(item));
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
                                  select p.*,WorkName,FormID
                                  ,WorkFlowID,UserName,TimeStr
                                  ,FuJianList,ShenPiYiJian
                                  ,JieDianID,JieDianName
                                  ,ShenPiUserList,OKUserList,StateNow
                                  ,LateTime,BeiYong1,BeiYong2
                                  --,SUBSTRING(BeiYong1,0,CHARINDEX('@',BeiYong1)) Number
	                              ,SUBSTRING(BeiYong1,CHARINDEX('@',BeiYong1)+1,LEN(BeiYong1)) Name
                                  from {0} p join ERPNWorkToDo d on p.NWorkID=d.ID ) t ", "ERPXMChengGuoStampFile");
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
            var _currentModel = new ZWL.BLL.ERPXMChengGuoStampFile();
            _currentModel.GetModelByNWorkId(workid);
            return _currentModel.Delete(_currentModel.ID);
        }
    }
}