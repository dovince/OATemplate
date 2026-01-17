using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPHeTongJieYue。
    /// </summary>
    [Serializable]
    public partial class ERPHeTongJieYue
    {
        public ERPHeTongJieYue()
        { }
        #region Model
        private int _id;
        private string _workname;
        private string _htjybm;
        private string _htjyr;
        private string _htjyqd;
        private DateTime? _htjyrq;
        private DateTime? _yjghtime;
        private DateTime? _htghtime;
        private string _htjyly;
        private string _htjyid;
        private string _htjynames;
        private int? _nworktodoid;
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
        public string WorkName
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("借阅部门")]
        public string HTJYBM
        {
            set { _htjybm = value; }
            get { return _htjybm; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("借阅人")]
        public string HTJYR
        {
            set { _htjyr = value; }
            get { return _htjyr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("借阅清单")]
        public string HTJYQD
        {
            set { _htjyqd = value; }
            get { return _htjyqd; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("借阅日期")]
        public DateTime? HTJYRQ
        {
            set { _htjyrq = value; }
            get { return _htjyrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("预计归还时间")]
        public DateTime? YJGHTime
        {
            set { _yjghtime = value; }
            get { return _yjghtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("归还时间")]
        public DateTime? HTGHTime
        {
            set { _htghtime = value; }
            get { return _htghtime; }
        }
        /// <summary>
        /// 合同借阅理由
        /// </summary>
        [Description("借阅理由")]
        public string HTJYLY
        {
            set { _htjyly = value; }
            get { return _htjyly; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("借阅编号")]
        public string HTJYID
        {
            set { _htjyid = value; }
            get { return _htjyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("备注")]
        public string HTJYNames
        {
            set { _htjynames = value; }
            get { return _htjynames; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        #endregion Model

        #region Relative Model
        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(HTJYR))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + HTJYR + "'");
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
                if (NWorkToDoID.HasValue && NWorkToDoID.Value > 0)
                {
                    _currentToDo.GetModel(NWorkToDoID.Value);
                }
                return _currentToDo;
            }
        }

        private List<ZWL.BLL.ERPHeTongJieYueDetail> _jieYueItems = null;
        public List<ZWL.BLL.ERPHeTongJieYueDetail> JieYueItems
        {
            get
            {
                if (_jieYueItems == null)
                    _jieYueItems = new List<ZWL.BLL.ERPHeTongJieYueDetail>();
                if (NWorkToDoID.HasValue && NWorkToDoID.Value > 0)
                {
                    var _currentModel = new ZWL.BLL.ERPHeTongJieYueDetail();
                    _jieYueItems = _currentModel.GetListModelByWorkId(NWorkToDoID.Value);
                }
                return _jieYueItems;
            }
            set
            {
                _jieYueItems = value;
            }
        }

        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPHeTongJieYue(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,WorkName,HTJYBM,HTJYR,HTJYQD,HTJYRQ,YJGHTime,HTGHTime,HTJYLY,HTJYID,HTJYNames,NWorkToDoID ");
            strSql.Append(" FROM [ERPHeTongJieYue] ");
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
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYBM"] != null)
                {
                    this.HTJYBM = ds.Tables[0].Rows[0]["HTJYBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYR"] != null)
                {
                    this.HTJYR = ds.Tables[0].Rows[0]["HTJYR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYQD"] != null)
                {
                    this.HTJYQD = ds.Tables[0].Rows[0]["HTJYQD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYRQ"] != null && ds.Tables[0].Rows[0]["HTJYRQ"].ToString() != "")
                {
                    this.HTJYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["HTJYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YJGHTime"] != null && ds.Tables[0].Rows[0]["YJGHTime"].ToString() != "")
                {
                    this.YJGHTime = DateTime.Parse(ds.Tables[0].Rows[0]["YJGHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTGHTime"] != null && ds.Tables[0].Rows[0]["HTGHTime"].ToString() != "")
                {
                    this.HTGHTime = DateTime.Parse(ds.Tables[0].Rows[0]["HTGHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTJYLY"] != null)
                {
                    this.HTJYLY = ds.Tables[0].Rows[0]["HTJYLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYID"] != null)
                {
                    this.HTJYID = ds.Tables[0].Rows[0]["HTJYID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYNames"] != null)
                {
                    this.HTJYNames = ds.Tables[0].Rows[0]["HTJYNames"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
            }
        }
        /// <summary>
		/// 根据合同借阅id得到一个对象实体
		/// </summary>
        public ERPHeTongJieYue(string strID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHeTongJieYue ");
            strSql.Append(" where HTJYID=@HTJYID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTJYID", SqlDbType.VarChar,100)};
            parameters[0].Value = strID;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //给对应的字段赋值
            WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
            HTJYBM = ds.Tables[0].Rows[0]["HTJYBM"].ToString();
            HTJYR = ds.Tables[0].Rows[0]["HTJYR"].ToString();
            HTJYQD = ds.Tables[0].Rows[0]["HTJYQD"].ToString();
            HTJYLY = ds.Tables[0].Rows[0]["HTJYLY"].ToString();
            HTJYID = ds.Tables[0].Rows[0]["HTJYID"].ToString();
            if (ds.Tables[0].Rows[0]["HTJYRQ"].ToString() != "")
            {
                HTJYRQ = Convert.ToDateTime(ds.Tables[0].Rows[0]["HTJYRQ"].ToString());
            }
            if (ds.Tables[0].Rows[0]["YJGHTime"].ToString() != "")
            {
                YJGHTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["YJGHTime"].ToString());
            }
            if (ds.Tables[0].Rows[0]["HTGHTime"].ToString() != "")
            {
                HTGHTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["HTGHTime"].ToString());
            }
            if (ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
            {
                NWorkToDoID = Convert.ToInt32(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
            }
            HTJYNames = ds.Tables[0].Rows[0]["HTJYNames"].ToString();
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPHeTongJieYue]");
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
            strSql.Append("insert into [ERPHeTongJieYue] (");
            strSql.Append("WorkName,HTJYBM,HTJYR,HTJYQD,HTJYRQ,YJGHTime,HTGHTime,HTJYLY,HTJYID,HTJYNames,NWorkToDoID)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@HTJYBM,@HTJYR,@HTJYQD,@HTJYRQ,@YJGHTime,@HTGHTime,@HTJYLY,@HTJYID,@HTJYNames,@NWorkToDoID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTJYBM", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTJYR", SqlDbType.NVarChar,200),
                    new SqlParameter("@HTJYQD", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HTJYRQ", SqlDbType.DateTime),
                    new SqlParameter("@YJGHTime", SqlDbType.DateTime),
                    new SqlParameter("@HTGHTime", SqlDbType.DateTime),
                    new SqlParameter("@HTJYLY", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HTJYID", SqlDbType.VarChar,200),
                    new SqlParameter("@HTJYNames", SqlDbType.NVarChar,-1),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = HTJYBM;
            parameters[2].Value = HTJYR;
            parameters[3].Value = HTJYQD;
            parameters[4].Value = HTJYRQ;
            parameters[5].Value = YJGHTime;
            parameters[6].Value = HTGHTime;
            parameters[7].Value = HTJYLY;
            parameters[8].Value = HTJYID;
            parameters[9].Value = HTJYNames;
            parameters[10].Value = NWorkToDoID;

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
            strSql.Append("update [ERPHeTongJieYue] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("HTJYBM=@HTJYBM,");
            strSql.Append("HTJYR=@HTJYR,");
            strSql.Append("HTJYQD=@HTJYQD,");
            strSql.Append("HTJYRQ=@HTJYRQ,");
            strSql.Append("YJGHTime=@YJGHTime,");
            strSql.Append("HTGHTime=@HTGHTime,");
            strSql.Append("HTJYLY=@HTJYLY,");
            strSql.Append("HTJYID=@HTJYID,");
            strSql.Append("HTJYNames=@HTJYNames,");
            strSql.Append("NWorkToDoID=@NWorkToDoID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTJYBM", SqlDbType.NVarChar,500),
                    new SqlParameter("@HTJYR", SqlDbType.NVarChar,200),
                    new SqlParameter("@HTJYQD", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HTJYRQ", SqlDbType.DateTime),
                    new SqlParameter("@YJGHTime", SqlDbType.DateTime),
                    new SqlParameter("@HTGHTime", SqlDbType.DateTime),
                    new SqlParameter("@HTJYLY", SqlDbType.NVarChar,-1),
                    new SqlParameter("@HTJYID", SqlDbType.VarChar,200),
                    new SqlParameter("@HTJYNames", SqlDbType.NVarChar,-1),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = HTJYBM;
            parameters[2].Value = HTJYR;
            parameters[3].Value = HTJYQD;
            parameters[4].Value = HTJYRQ;
            parameters[5].Value = YJGHTime;
            parameters[6].Value = HTGHTime;
            parameters[7].Value = HTJYLY;
            parameters[8].Value = HTJYID;
            parameters[9].Value = HTJYNames;
            parameters[10].Value = NWorkToDoID;
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
            strSql.Append("delete from [ERPHeTongJieYue] ");
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
            strSql.Append("select ID,WorkName,HTJYBM,HTJYR,HTJYQD,HTJYRQ,YJGHTime,HTGHTime,HTJYLY,HTJYID,HTJYNames,NWorkToDoID ");
            strSql.Append(" FROM [ERPHeTongJieYue] ");
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
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYBM"] != null)
                {
                    this.HTJYBM = ds.Tables[0].Rows[0]["HTJYBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYR"] != null)
                {
                    this.HTJYR = ds.Tables[0].Rows[0]["HTJYR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYQD"] != null)
                {
                    this.HTJYQD = ds.Tables[0].Rows[0]["HTJYQD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYRQ"] != null && ds.Tables[0].Rows[0]["HTJYRQ"].ToString() != "")
                {
                    this.HTJYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["HTJYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YJGHTime"] != null && ds.Tables[0].Rows[0]["YJGHTime"].ToString() != "")
                {
                    this.YJGHTime = DateTime.Parse(ds.Tables[0].Rows[0]["YJGHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTGHTime"] != null && ds.Tables[0].Rows[0]["HTGHTime"].ToString() != "")
                {
                    this.HTGHTime = DateTime.Parse(ds.Tables[0].Rows[0]["HTGHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTJYLY"] != null)
                {
                    this.HTJYLY = ds.Tables[0].Rows[0]["HTJYLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYID"] != null)
                {
                    this.HTJYID = ds.Tables[0].Rows[0]["HTJYID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTJYNames"] != null)
                {
                    this.HTJYNames = ds.Tables[0].Rows[0]["HTJYNames"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
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
            strSql.Append(" FROM [ERPHeTongJieYue] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }

        public ZWL.BLL.ERPHeTongJieYue GetModelByWhere(string strWhere)
        {
            var list = GetListModel(strWhere);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public ZWL.BLL.ERPHeTongJieYue GetModelByWorkId(int workid)
        {
            return GetModelByWhere("NWorkToDoID=" + workid);
        }
        public List<ZWL.BLL.ERPHeTongJieYue> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPHeTongJieYue>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableHelper.ConvertTo<ZWL.BLL.ERPHeTongJieYue>(ds.Tables[0]);
            }
            return list;
        }

        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select * from (select h.ID,h.HTID,h.HTName,h.XMID,h.HTLB,h.ZYLB,h.JFDW,h.YFDW,h.HTJE,h.JBR,h.CJBM,h.HTJYState,h.NWorkToDoID NWorkID,d.TimeStr,d.UserName
                        from ERPHeTong h join ERPNWorkToDo d
                        on h.NWorkToDoID=d.ID 
                        where d.StateNow='正常结束') t ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListMappingAndPaging(strWhere, cPage, pSize, "ID Desc");
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select * from (select h.*,d.TimeStr,d.LateTime,d.FormID,d.WorkFlowID,
                                d.ShenPiUserList,d.JieDianID,d.JieDianName,d.OKUserList,d.UserName,d.StateNow 
                            from ERPHeTongJieYue h join ERPNWorkToDo d
                            on h.NWorkToDoID=d.ID) t ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}

