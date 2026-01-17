using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;//Please add references
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPXMZLGuiDang。
    /// </summary>
    [Serializable]
    public partial class ERPXMZLGuiDang
    {
        public ERPXMZLGuiDang()
        { }
        #region Model
        private int _id;
        private int? _nworkid;
        private string _dah;
        private string _xmbh;
        private string _reportbh;
        private string _jbr;
        private DateTime? _gddate;
        private DateTime? _djtime;
        private string _gdr;
        private string _xmfzr;
        private string _workdate;
        private DateTime? _startworkdate;
        private DateTime? _endworkdate;
        private string _xmname;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// NWorkToDoID
        /// </summary>
        public int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 档案号
        /// </summary>
        public string DAH
        {
            set { _dah = value; }
            get { return _dah; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 报告编号
        /// </summary>
        public string ReportBH
        {
            set { _reportbh = value; }
            get { return _reportbh; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string JBR
        {
            set { _jbr = value; }
            get { return _jbr; }
        }
        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? GDDate
        {
            set { _gddate = value; }
            get { return _gddate; }
        }
        /// <summary>
        /// 给号日期
        /// </summary>
        public DateTime? DJTime
        {
            set { _djtime = value; }
            get { return _djtime; }
        }
        /// <summary>
        /// 归档人
        /// </summary>
        public string GDR
        {
            set { _gdr = value; }
            get { return _gdr; }
        }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string XMFZR
        {
            set { _xmfzr = value; }
            get { return _xmfzr; }
        }
        /// <summary>
        /// 工作日期
        /// </summary>
        public string WorkDate
        {
            set { _workdate = value; }
            get { return _workdate; }
        }
        /// <summary>
        /// 开始工作日期
        /// </summary>
        public DateTime? StartWorkDate
        {
            set { _startworkdate = value; }
            get { return _startworkdate; }
        }
        /// <summary>
        /// 结束工作日期
        /// </summary>
        public DateTime? EndWorkDate
        {
            set { _endworkdate = value; }
            get { return _endworkdate; }
        }
        /// <summary>
        /// 报告名称
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        #endregion Model


        #region Relative Model

        private List<ZWL.BLL.ERPXMZLGuiDangDetail> _subItems = null;
        public List<ZWL.BLL.ERPXMZLGuiDangDetail> SubItems
        {
            get
            {
                if (_subItems == null)
                    _subItems = new List<ZWL.BLL.ERPXMZLGuiDangDetail>();
                if (NWorkID.HasValue && NWorkID.Value > 0)
                {
                    var _currentModel = new ZWL.BLL.ERPXMZLGuiDangDetail();
                    var sqlWhere = @"ID in (
                                        select max(t.ID) from ERPXMZLGuiDangDetail t JOIN ERPNWorkToDo d on t.NWorkID=d.ID
                                        LEFT JOIN ERPXMZLGuiDang g on t.DAH=t.DAH and g.NWorkID=t.NWorkID
                                        where StateNow not in ('已被驳回','不通过') and g.NWorkID={0}
                                        GROUP BY t.DAH,g.XMBH,XuHao
                                    )";
                    _subItems = _currentModel.GetModelList(sqlWhere.FormatWith(NWorkID.Value));
                }
                return _subItems;
            }
            set
            {
                _subItems = value;
            }
        }

        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMZLGuiDang(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkID,DAH,XMBH,ReportBH,JBR,GDDate,DJTime,GDR,XMFZR,WorkDate,StartWorkDate,EndWorkDate,XMName ");
            strSql.Append(" FROM [ERPXMZLGuiDang] ");
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
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAH"] != null)
                {
                    this.DAH = ds.Tables[0].Rows[0]["DAH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReportBH"] != null)
                {
                    this.ReportBH = ds.Tables[0].Rows[0]["ReportBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GDDate"] != null && ds.Tables[0].Rows[0]["GDDate"].ToString() != "")
                {
                    this.GDDate = DateTime.Parse(ds.Tables[0].Rows[0]["GDDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GDR"] != null)
                {
                    this.GDR = ds.Tables[0].Rows[0]["GDR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkDate"] != null)
                {
                    this.WorkDate = ds.Tables[0].Rows[0]["WorkDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartWorkDate"] != null && ds.Tables[0].Rows[0]["StartWorkDate"].ToString() != "")
                {
                    this.StartWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndWorkDate"] != null && ds.Tables[0].Rows[0]["EndWorkDate"].ToString() != "")
                {
                    this.EndWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMZLGuiDang]");
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
            strSql.Append("insert into [ERPXMZLGuiDang] (");
            strSql.Append("NWorkID,DAH,XMBH,ReportBH,JBR,GDDate,DJTime,GDR,XMFZR,WorkDate,StartWorkDate,EndWorkDate,XMName)");
            strSql.Append(" values (");
            strSql.Append("@NWorkID,@DAH,@XMBH,@ReportBH,@JBR,@GDDate,@DJTime,@GDR,@XMFZR,@WorkDate,@StartWorkDate,@EndWorkDate,@XMName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@DAH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReportBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@JBR", SqlDbType.NVarChar,20),
                    new SqlParameter("@GDDate", SqlDbType.DateTime),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@GDR", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,10),
                    new SqlParameter("@WorkDate", SqlDbType.NVarChar,30),
                    new SqlParameter("@StartWorkDate", SqlDbType.DateTime),
                    new SqlParameter("@EndWorkDate", SqlDbType.DateTime),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,-1)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = DAH;
            parameters[2].Value = XMBH;
            parameters[3].Value = ReportBH;
            parameters[4].Value = JBR;
            parameters[5].Value = GDDate;
            parameters[6].Value = DJTime;
            parameters[7].Value = GDR;
            parameters[8].Value = XMFZR;
            parameters[9].Value = WorkDate;
            parameters[10].Value = StartWorkDate;
            parameters[11].Value = EndWorkDate;
            parameters[12].Value = XMName;

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
            strSql.Append("update [ERPXMZLGuiDang] set ");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("DAH=@DAH,");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("ReportBH=@ReportBH,");
            strSql.Append("JBR=@JBR,");
            strSql.Append("GDDate=@GDDate,");
            strSql.Append("DJTime=@DJTime,");
            strSql.Append("GDR=@GDR,");
            strSql.Append("XMFZR=@XMFZR,");
            strSql.Append("WorkDate=@WorkDate,");
            strSql.Append("StartWorkDate=@StartWorkDate,");
            strSql.Append("EndWorkDate=@EndWorkDate,");
            strSql.Append("XMName=@XMName");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@DAH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@ReportBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@JBR", SqlDbType.NVarChar,20),
                    new SqlParameter("@GDDate", SqlDbType.DateTime),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@GDR", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,10),
                    new SqlParameter("@WorkDate", SqlDbType.NVarChar,30),
                    new SqlParameter("@StartWorkDate", SqlDbType.DateTime),
                    new SqlParameter("@EndWorkDate", SqlDbType.DateTime),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = DAH;
            parameters[2].Value = XMBH;
            parameters[3].Value = ReportBH;
            parameters[4].Value = JBR;
            parameters[5].Value = GDDate;
            parameters[6].Value = DJTime;
            parameters[7].Value = GDR;
            parameters[8].Value = XMFZR;
            parameters[9].Value = WorkDate;
            parameters[10].Value = StartWorkDate;
            parameters[11].Value = EndWorkDate;
            parameters[12].Value = XMName;
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
            strSql.Append("delete from [ERPXMZLGuiDang] ");
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
            strSql.Append("select ID,NWorkID,DAH,XMBH,ReportBH,JBR,GDDate,DJTime,GDR,XMFZR,WorkDate,StartWorkDate,EndWorkDate,XMName ");
            strSql.Append(" FROM [ERPXMZLGuiDang] ");
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
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAH"] != null)
                {
                    this.DAH = ds.Tables[0].Rows[0]["DAH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReportBH"] != null)
                {
                    this.ReportBH = ds.Tables[0].Rows[0]["ReportBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GDDate"] != null && ds.Tables[0].Rows[0]["GDDate"].ToString() != "")
                {
                    this.GDDate = DateTime.Parse(ds.Tables[0].Rows[0]["GDDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GDR"] != null)
                {
                    this.GDR = ds.Tables[0].Rows[0]["GDR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkDate"] != null)
                {
                    this.WorkDate = ds.Tables[0].Rows[0]["WorkDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartWorkDate"] != null && ds.Tables[0].Rows[0]["StartWorkDate"].ToString() != "")
                {
                    this.StartWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndWorkDate"] != null && ds.Tables[0].Rows[0]["EndWorkDate"].ToString() != "")
                {
                    this.EndWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
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
            strSql.Append(" FROM [ERPXMZLGuiDang] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = method.getSQLTable("ERPXMZLGuiDang");
            //strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.项目编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("73") + ") ";
            //if (strWhere.Trim() != "")
            //{
            //    strSql += " and " +  strWhere;
            //}
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint ";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }

            return new Pager(strSql, cPage, pSize, "给号日期 desc");
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModelByNWorkID(int NWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkID,DAH,XMBH,ReportBH,JBR,GDDate,DJTime,GDR,XMFZR,WorkDate,StartWorkDate,EndWorkDate,XMName ");
            strSql.Append(" FROM [ERPXMZLGuiDang] ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAH"] != null)
                {
                    this.DAH = ds.Tables[0].Rows[0]["DAH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReportBH"] != null)
                {
                    this.ReportBH = ds.Tables[0].Rows[0]["ReportBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JBR"] != null)
                {
                    this.JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GDDate"] != null && ds.Tables[0].Rows[0]["GDDate"].ToString() != "")
                {
                    this.GDDate = DateTime.Parse(ds.Tables[0].Rows[0]["GDDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    this.DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GDR"] != null)
                {
                    this.GDR = ds.Tables[0].Rows[0]["GDR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMFZR"] != null)
                {
                    this.XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkDate"] != null)
                {
                    this.WorkDate = ds.Tables[0].Rows[0]["WorkDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartWorkDate"] != null && ds.Tables[0].Rows[0]["StartWorkDate"].ToString() != "")
                {
                    this.StartWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndWorkDate"] != null && ds.Tables[0].Rows[0]["EndWorkDate"].ToString() != "")
                {
                    this.EndWorkDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndWorkDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
            }
        }
    }
}

