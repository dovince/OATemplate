using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;
using System.Collections.Generic;//Please add references

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPProjectFenBaoShenPi 工程项目分包审批
    /// </summary>
    public class ERPProjectFenBaoShenPi
    {
        public ERPProjectFenBaoShenPi()
        { }
        #region Model
        private int _id;
        private int _nworktodoid;
        private string _xmbh;
        private string _projectname;
        private string _workname;
        private string _xmssdanwei;
        private string _xmwtdanwei;
        private decimal? _hetongmoney;
        private string _xmfbtype;
        private decimal? _xmfbmoney;
        private string _xmfbcompanyname;
        private string _djbm;
        private string _djr;
        private DateTime? _djtime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 项目实施单位
        /// </summary>
        public string XMSSDanWei
        {
            set { _xmssdanwei = value; }
            get { return _xmssdanwei; }
        }
        /// <summary>
        /// 项目委托单位
        /// </summary>
        public string XMWTDanWei
        {
            set { _xmwtdanwei = value; }
            get { return _xmwtdanwei; }
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? HeTongMoney
        {
            set { _hetongmoney = value; }
            get { return _hetongmoney; }
        }
        /// <summary>
        /// 项目分包类别
        /// </summary>
        public string XMFBType
        {
            set { _xmfbtype = value; }
            get { return _xmfbtype; }
        }
        /// <summary>
        /// 项目分包金额
        /// </summary>
        public decimal? XMFBMoney
        {
            set { _xmfbmoney = value; }
            get { return _xmfbmoney; }
        }
        /// <summary>
        /// 项目分包公司名称
        /// </summary>
        public string XMFBCompanyName
        {
            set { _xmfbcompanyname = value; }
            get { return _xmfbcompanyname; }
        }
        /// <summary>
        /// 登记部门
        /// </summary>
        public string DJBM
        {
            set { _djbm = value; }
            get { return _djbm; }
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime? DJTime
        {
            set { _djtime = value; }
            get { return _djtime; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPProjectFenBaoShenPi(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,XMBH,ProjectName,WorkName,XMSSDanWei,XMWTDanWei,HeTongMoney,XMFBType,XMFBMoney,XMFBCompanyName,DJBM,DJR,DJTime ");
            strSql.Append(" FROM [ERPProjectFenBaoShenPi] ");
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
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProjectName"] != null)
                {
                    this.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMSSDanWei"] != null)
                {
                    this.XMSSDanWei = ds.Tables[0].Rows[0]["XMSSDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMWTDanWei"] != null)
                {
                    this.XMWTDanWei = ds.Tables[0].Rows[0]["XMWTDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeTongMoney"] != null && ds.Tables[0].Rows[0]["HeTongMoney"].ToString() != "")
                {
                    this.HeTongMoney = decimal.Parse(ds.Tables[0].Rows[0]["HeTongMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFBType"] != null)
                {
                    this.XMFBType = ds.Tables[0].Rows[0]["XMFBType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFBMoney"] != null && ds.Tables[0].Rows[0]["XMFBMoney"].ToString() != "")
                {
                    this.XMFBMoney = decimal.Parse(ds.Tables[0].Rows[0]["XMFBMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFBCompanyName"] != null)
                {
                    this.XMFBCompanyName = ds.Tables[0].Rows[0]["XMFBCompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPProjectFenBaoShenPi]");
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
            strSql.Append("insert into [ERPProjectFenBaoShenPi] (");
            strSql.Append("NWorkToDoID,XMBH,ProjectName,WorkName,XMSSDanWei,XMWTDanWei,HeTongMoney,XMFBType,XMFBMoney,XMFBCompanyName,DJBM,DJR,DJTime)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@XMBH,@ProjectName,@WorkName,@XMSSDanWei,@XMWTDanWei,@HeTongMoney,@XMFBType,@XMFBMoney,@XMFBCompanyName,@DJBM,@DJR,@DJTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@XMBH", SqlDbType.VarChar,200),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@XMSSDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@XMWTDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@HeTongMoney", SqlDbType.Decimal,9),
					new SqlParameter("@XMFBType", SqlDbType.NVarChar,50),
					new SqlParameter("@XMFBMoney", SqlDbType.Decimal,9),
					new SqlParameter("@XMFBCompanyName", SqlDbType.NVarChar,200),
					new SqlParameter("@DJBM", SqlDbType.NVarChar,50),
					new SqlParameter("@DJR", SqlDbType.NVarChar,50),
					new SqlParameter("@DJTime", SqlDbType.DateTime)};
            parameters[0].Value = NWorkToDoID;
            parameters[1].Value = XMBH;
            parameters[2].Value = ProjectName;
            parameters[3].Value = WorkName;
            parameters[4].Value = XMSSDanWei;
            parameters[5].Value = XMWTDanWei;
            parameters[6].Value = HeTongMoney;
            parameters[7].Value = XMFBType;
            parameters[8].Value = XMFBMoney;
            parameters[9].Value = XMFBCompanyName;
            parameters[10].Value = DJBM;
            parameters[11].Value = DJR;
            parameters[12].Value = DJTime;

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
            strSql.Append("update [ERPProjectFenBaoShenPi] set ");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("XMSSDanWei=@XMSSDanWei,");
            strSql.Append("XMWTDanWei=@XMWTDanWei,");
            strSql.Append("HeTongMoney=@HeTongMoney,");
            strSql.Append("XMFBType=@XMFBType,");
            strSql.Append("XMFBMoney=@XMFBMoney,");
            strSql.Append("XMFBCompanyName=@XMFBCompanyName,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("DJTime=@DJTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@XMBH", SqlDbType.VarChar,200),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@XMSSDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@XMWTDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@HeTongMoney", SqlDbType.Decimal,9),
					new SqlParameter("@XMFBType", SqlDbType.NVarChar,50),
					new SqlParameter("@XMFBMoney", SqlDbType.Decimal,9),
					new SqlParameter("@XMFBCompanyName", SqlDbType.NVarChar,200),
					new SqlParameter("@DJBM", SqlDbType.NVarChar,50),
					new SqlParameter("@DJR", SqlDbType.NVarChar,50),
					new SqlParameter("@DJTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkToDoID;
            parameters[1].Value = XMBH;
            parameters[2].Value = ProjectName;
            parameters[3].Value = WorkName;
            parameters[4].Value = XMSSDanWei;
            parameters[5].Value = XMWTDanWei;
            parameters[6].Value = HeTongMoney;
            parameters[7].Value = XMFBType;
            parameters[8].Value = XMFBMoney;
            parameters[9].Value = XMFBCompanyName;
            parameters[10].Value = DJBM;
            parameters[11].Value = DJR;
            parameters[12].Value = DJTime;
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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPProjectFenBaoShenPi] ");
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
            strSql.Append("select ID,NWorkToDoID,XMBH,ProjectName,WorkName,XMSSDanWei,XMWTDanWei,HeTongMoney,XMFBType,XMFBMoney,XMFBCompanyName,DJBM,DJR,DJTime ");
            strSql.Append(" FROM [ERPProjectFenBaoShenPi] ");
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
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProjectName"] != null)
                {
                    this.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMSSDanWei"] != null)
                {
                    this.XMSSDanWei = ds.Tables[0].Rows[0]["XMSSDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMWTDanWei"] != null)
                {
                    this.XMWTDanWei = ds.Tables[0].Rows[0]["XMWTDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeTongMoney"] != null && ds.Tables[0].Rows[0]["HeTongMoney"].ToString() != "")
                {
                    this.HeTongMoney = decimal.Parse(ds.Tables[0].Rows[0]["HeTongMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFBType"] != null)
                {
                    this.XMFBType = ds.Tables[0].Rows[0]["XMFBType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFBMoney"] != null && ds.Tables[0].Rows[0]["XMFBMoney"].ToString() != "")
                {
                    this.XMFBMoney = decimal.Parse(ds.Tables[0].Rows[0]["XMFBMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFBCompanyName"] != null)
                {
                    this.XMFBCompanyName = ds.Tables[0].Rows[0]["XMFBCompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
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
            strSql.Append(" FROM [ERPProjectFenBaoShenPi] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得多个数据实体
        /// </summary>
        public List<ZWL.BLL.ERPProjectFenBaoShenPi> GetModelList(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.ConvertTo_1<ZWL.BLL.ERPProjectFenBaoShenPi>(dt);
            }
            return new List<ZWL.BLL.ERPProjectFenBaoShenPi>();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            //var strmapping = "select ID,NWorkToDoID,ROW_NUMBER() OVER (ORDER BY ProjectName) as 序号,XMBH as 项目编号, ProjectName as 项目名称,XMSSDanWei as 项目实施单位,XMWTDanWei as 项目委托单位 ,HeTongMoney as 合同金额 ,XMFBType as 项目分包类别 ,XMFBMoney as 项目分包金额 ,XMFBCompanyName as 项目分包公司名称, DJBM as 登记部门, DJR as 登记人 , DJTime as 登记时间 from ERPProjectFenBaoShenPi";
            var strmapping = "select p.ID,p.NWorkToDoID,ROW_NUMBER() OVER (ORDER BY p.ProjectName) as 序号,p.XMBH as 项目编号, p.ProjectName as 项目名称,p.XMSSDanWei as 项目实施单位,p.XMWTDanWei as 项目委托单位 ,p.HeTongMoney as 合同金额 ," +
            "p.XMFBType as 项目分包类别 ,p.XMFBMoney as 项目分包金额 ,p.XMFBCompanyName as 项目分包公司名称, p.DJBM as 登记部门,p.DJR as 登记人 ,p.DJTime as 登记时间,w.WorkName as 节点名称,w.ShenPiUserList as 审批用户,w.StateNow as 当前状态" +
            " from ERPProjectFenBaoShenPi p left join ERPNWorkToDo w on p.NWorkToDoID = w.ID";
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere + " order by 序号 ";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
        #endregion  Method
}
