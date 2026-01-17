using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
using System.Collections.Generic;//请先添加引用
using System.Reflection;
using System.Security.Cryptography;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPNWorkToDo。
    /// </summary>
    public class ERPNWorkToDo
    {
        public ERPNWorkToDo()
        { }
        #region Model
        private int _id;
        private string _workname;
        private int? _formid;
        private int? _workflowid;
        private string _username;
        private DateTime? _timestr;
        private string _formcontent;
        private string _fujianlist;
        private string _shenpiyijian;
        private int? _jiedianid;
        private string _jiedianname;
        private string _shenpiuserlist;
        private string _okuserlist;
        private string _statenow;
        private DateTime? _latetime;
        private string _beiyong1;
        private string _beiyong2;
        private string _number;
        private string _name;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 所用表单
        /// </summary>
        public int? FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 所用工作流程
        /// </summary>
        public int? WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 发起人
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormContent
        {
            set { _formcontent = value; }
            get { return _formcontent; }
        }
        /// <summary>
        /// 附件文件
        /// </summary>
        public string FuJianList
        {
            set { _fujianlist = value; }
            get { return _fujianlist; }
        }
        /// <summary>
        /// 签注审批
        /// </summary>
        public string ShenPiYiJian
        {
            set { _shenpiyijian = value; }
            get { return _shenpiyijian; }
        }
        /// <summary>
        /// 当前所在节点
        /// </summary>
        public int? JieDianID
        {
            set { _jiedianid = value; }
            get { return _jiedianid; }
        }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string JieDianName
        {
            set { _jiedianname = value; }
            get { return _jiedianname; }
        }
        /// <summary>
        /// 当前审批用户（可以多个人）
        /// </summary>
        public string ShenPiUserList
        {
            set { _shenpiuserlist = value; }
            get { return _shenpiuserlist; }
        }
        /// <summary>
        /// 当前已审批通过的用户（可以多个人）
        /// </summary>
        public string OKUserList
        {
            set { _okuserlist = value; }
            get { return _okuserlist; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string StateNow
        {
            set { _statenow = value; }
            get { return _statenow; }
        }
        /// <summary>
        /// 超时时间（何时超时）
        /// </summary>
        public DateTime? LateTime
        {
            set { _latetime = value; }
            get { return _latetime; }
        }
        /// <summary>
        /// 存放表单中的关键字段
        /// </summary>
        public string BeiYong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 存放表单中的关键字段
        /// </summary>
        public string BeiYong2
        {
            set { _beiyong2 = value; }
            get { return _beiyong2; }
        }
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        #endregion Model
        #region Relative Model

        public ZWL.BLL.ERPUser CurrentUser()
        {
            var _currentUser = new ZWL.BLL.ERPUser();
            if (!string.IsNullOrEmpty(UserName))
            {
                var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + UserName + "'");
                if (tempUser != null)
                    _currentUser = tempUser;
            }
            return _currentUser;
        }

        public ZWL.BLL.ERPNWorkFlowNode CurrentNode()
        {
            var _currentNode = new ZWL.BLL.ERPNWorkFlowNode();
            if (JieDianID > 0)
            {
                _currentNode.GetModel(JieDianID.Value);
            }
            return _currentNode;
        }
        public ZWL.BLL.ERPNForm CurrentForm()
        {
            var _currentForm = new ZWL.BLL.ERPNForm();
            if (FormID > 0)
            {
                _currentForm.GetModel(FormID.Value);
            }
            return _currentForm;
        }
        public ZWL.BLL.ERPNWorkFlow CurrentWorkFlow()
        {
            var _currentWorkFlow = new ZWL.BLL.ERPNWorkFlow();
            if (WorkFlowID > 0)
            {
                _currentWorkFlow.GetModel(WorkFlowID.Value);
            }
            return _currentWorkFlow;
        }
        public ZWL.BLL.ERPNWorkToDoExtend CurrentWorkToDoExtend()
        {
            var _currentWorkFlow = new ZWL.BLL.ERPNWorkToDoExtend();
            if (ID > 0)
            {
                _currentWorkFlow.GetModelByNWorkId(ID);
            }
            return _currentWorkFlow;
        }
        #endregion


        #region  成员方法
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateBD()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPNWorkToDoExtend set ");
            strSql.Append("FormContent=@FormContent ");
            strSql.Append(" where NWorkID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
                    new SqlParameter("@FormContent", SqlDbType.Text)};
            parameters[0].Value = ID;
            parameters[1].Value = FormContent;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPNWorkToDo(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select *,(select FormContent from ERPNWorkToDoExtend e where NWorkID=ERPNWorkToDo.ID) FormContent,
                                            SUBSTRING([BeiYong1],0,CHARINDEX('@',beiyong1)) Number,
                                            SUBSTRING([BeiYong1],CHARINDEX('@',beiyong1)+1,LEN(beiyong1)) Name ");
            strSql.Append(" FROM ERPNWorkToDo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormID"] != null && ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    this.FormID = int.Parse(ds.Tables[0].Rows[0]["FormID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"] != null && ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    this.WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimeStr"] != null && ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    this.TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShenPiYiJian"] != null)
                {
                    this.ShenPiYiJian = ds.Tables[0].Rows[0]["ShenPiYiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianList"] != null)
                {
                    this.FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JieDianID"] != null && ds.Tables[0].Rows[0]["JieDianID"].ToString() != "")
                {
                    this.JieDianID = int.Parse(ds.Tables[0].Rows[0]["JieDianID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieDianName"] != null)
                {
                    this.JieDianName = ds.Tables[0].Rows[0]["JieDianName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenPiUserList"] != null)
                {
                    this.ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OKUserList"] != null)
                {
                    this.OKUserList = ds.Tables[0].Rows[0]["OKUserList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StateNow"] != null)
                {
                    this.StateNow = ds.Tables[0].Rows[0]["StateNow"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LateTime"] != null && ds.Tables[0].Rows[0]["LateTime"].ToString() != "")
                {
                    this.LateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeiYong1"] != null)
                {
                    this.BeiYong1 = ds.Tables[0].Rows[0]["BeiYong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BeiYong2"] != null)
                {
                    this.BeiYong2 = ds.Tables[0].Rows[0]["BeiYong2"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("FormContent") && ds.Tables[0].Rows[0]["FormContent"] != null)
                {
                    this.FormContent = ds.Tables[0].Rows[0]["FormContent"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("Number") && ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("Name") && ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPNWorkToDo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPNWorkToDo");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPNWorkToDo] (");
            strSql.Append("WorkName,FormID,WorkFlowID,UserName,TimeStr,ShenPiYiJian,FuJianList,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime,BeiYong1,BeiYong2)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@FormID,@WorkFlowID,@UserName,@TimeStr,@ShenPiYiJian,@FuJianList,@JieDianID,@JieDianName,@ShenPiUserList,@OKUserList,@StateNow,@LateTime,@BeiYong1,@BeiYong2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,200),
                    new SqlParameter("@FormID", SqlDbType.Int,4),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@TimeStr", SqlDbType.DateTime),
                    new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
                    new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
                    new SqlParameter("@JieDianID", SqlDbType.Int,4),
                    new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
                    new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,8000),
                    new SqlParameter("@OKUserList", SqlDbType.VarChar,8000),
                    new SqlParameter("@StateNow", SqlDbType.VarChar,50),
                    new SqlParameter("@LateTime", SqlDbType.DateTime),
                    new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
                    new SqlParameter("@BeiYong2", SqlDbType.VarChar,100)};
            parameters[0].Value = WorkName;
            parameters[1].Value = FormID;
            parameters[2].Value = WorkFlowID;
            parameters[3].Value = UserName;
            parameters[4].Value = TimeStr;
            parameters[5].Value = ShenPiYiJian;
            parameters[6].Value = FuJianList;
            parameters[7].Value = JieDianID;
            parameters[8].Value = JieDianName;
            parameters[9].Value = ShenPiUserList;
            parameters[10].Value = OKUserList;
            parameters[11].Value = StateNow;
            parameters[12].Value = LateTime;
            parameters[13].Value = BeiYong1;
            parameters[14].Value = BeiYong2;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                this.ID = Convert.ToInt32(obj);
                new ZWL.BLL.ERPNWorkToDoExtend()
                {
                    NWorkID = this.ID,
                    FormContent = FormContent
                }.Add();
                return this.ID;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPNWorkToDo] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("FormID=@FormID,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("ShenPiYiJian=@ShenPiYiJian,");
            strSql.Append("FuJianList=@FuJianList,");
            strSql.Append("JieDianID=@JieDianID,");
            strSql.Append("JieDianName=@JieDianName,");
            strSql.Append("ShenPiUserList=@ShenPiUserList,");
            strSql.Append("OKUserList=@OKUserList,");
            strSql.Append("StateNow=@StateNow,");
            strSql.Append("LateTime=@LateTime,");
            strSql.Append("BeiYong1=@BeiYong1,");
            strSql.Append("BeiYong2=@BeiYong2");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkName", SqlDbType.VarChar,200),
                    new SqlParameter("@FormID", SqlDbType.Int,4),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@TimeStr", SqlDbType.DateTime),
                    new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
                    new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
                    new SqlParameter("@JieDianID", SqlDbType.Int,4),
                    new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
                    new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,8000),
                    new SqlParameter("@OKUserList", SqlDbType.VarChar,8000),
                    new SqlParameter("@StateNow", SqlDbType.VarChar,50),
                    new SqlParameter("@LateTime", SqlDbType.DateTime),
                    new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
                    new SqlParameter("@BeiYong2", SqlDbType.VarChar,100),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = FormID;
            parameters[2].Value = WorkFlowID;
            parameters[3].Value = UserName;
            parameters[4].Value = TimeStr;
            parameters[5].Value = ShenPiYiJian;
            parameters[6].Value = FuJianList;
            parameters[7].Value = JieDianID;
            parameters[8].Value = JieDianName;
            parameters[9].Value = ShenPiUserList;
            parameters[10].Value = OKUserList;
            parameters[11].Value = StateNow;
            parameters[12].Value = LateTime;
            parameters[13].Value = BeiYong1;
            parameters[14].Value = BeiYong2;
            parameters[15].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                UpdateBD();
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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPNWorkToDo] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                var modelExt = new ZWL.BLL.ERPNWorkToDoExtend();
                modelExt.GetModelByNWorkId(id);
                modelExt.Delete(id);
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
            strSql.Append(@"select top 1 *,(select top 1 FormContent from ERPNWorkToDoExtend e where NWorkID=ERPNWorkToDo.ID) FormContent,
                                            SUBSTRING([BeiYong1],0,CHARINDEX('@',beiyong1)) Number,
                                            SUBSTRING([BeiYong1],CHARINDEX('@',beiyong1)+1,LEN(beiyong1)) Name ");
            strSql.Append(" FROM ERPNWorkToDo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string strbeiyong1, string strformid, string strworkflowid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *,(select FormContent from ERPNWorkToDoExtend e where NWorkID=ERPNWorkToDo.ID) FormContent ");
            strSql.Append(" FROM ERPNWorkToDo ");
            strSql.Append(" where BeiYong1=@BeiYong1 and FormID=@FormID and WorkFlowID=@WorkFlowID");
            SqlParameter[] parameters = {
                    new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
                    new SqlParameter("@FormID", SqlDbType.Int,6),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,6)};
            parameters[0].Value = strbeiyong1;
            parameters[1].Value = int.Parse(strformid);
            parameters[2].Value = int.Parse(strworkflowid);
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select *,SUBSTRING([BeiYong1],0,CHARINDEX('@',beiyong1)) Number
	          ,SUBSTRING([BeiYong1],CHARINDEX('@',beiyong1)+1,LEN(beiyong1)) Name ");
            strSql.Append(" FROM ERPNWorkToDo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList2(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM ERPNWorkToDo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListInManager(string strWhere, string tablename)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.AppendFormat(@" FROM (select *  FROM (select h.*,d.StateNow,d.ShenPiUserList,d.FormID,d.WorkFlowID,
                                d.JieDianName,d.JieDianID,u.Department,d.UserName,d.TimeStr,d.LateTime,d.OKUserList,d.BeiYong1,d.BeiYong2 from {0} h
                                join ERPNWorkToDo d
                                on h.NWorkToDoID=d.ID
                                join ERPUser u 
                                on d.UserName = u.UserName
                                ) t) t ", tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by NWorkToDoID desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListNotInManager(string strWhere, string tablename,string orderby = "")
        {
            var strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.AppendFormat(@" FROM {0} ", tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if(orderby == "")
            {
                strSql.Append(" order by ID desc ");
            }
            else
            {
                strSql.Append(" order by " + orderby + " ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (select *
        ,SUBSTRING([BeiYong1],0,CHARINDEX('@',beiyong1)) Number
	          ,SUBSTRING([BeiYong1],CHARINDEX('@',beiyong1)+1,LEN(beiyong1)) Name
        --,(isnull((select  max(TimeStr) from ERPLanEmail u where u.WorkToDoID = t.ID),t.TimeStr)) LatestTime
			   FROM ERPNWorkToDo t) b ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }



        public List<DataRow> GetAllTodoWorkType(string quanXian)
        {
            var result = new List<DataRow>();
            var sql = "select * from ERPTreeList where TextStr like '待办工作%' order by PaiXuStr";
            var allTodo = DbHelperSQL.Query(sql);
            if (allTodo != null && allTodo.Tables.Count > 0)
            {
                foreach (DataRow item in allTodo.Tables[0].Rows)
                {
                    var valStr = "|" + item["ValueStr"].ToString() + "|";
                    if (PublicMethod.StrIFInLongStr(valStr, quanXian))
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public string GetWorkNum(string userName, string quanXian)
        {
            string num = "";
            Dictionary<DataRow, List<DataRow>> result = GetAllTodoWork(userName, quanXian);
            foreach (var datarow in result)
            {
                num = datarow.Value.Count.ToString();
            }
            return num;
        }

        public Dictionary<DataRow, List<DataRow>> GetAllTodoWork(string userName, string quanXian)
        {
            var result = new Dictionary<DataRow, List<DataRow>>();
            var source = GetAllTodoWorkType(quanXian);
            var sqlFormat = @"  StateNow='正在办理' and ','+ShenPiUserList+',' like '%,{0},%' and ','+OKUserList+',' not like '%,{1},%'
                and {2} order by ID desc";

            foreach (var item in source)
            {
                var keyWord = item["TextStr"].ToString();
                var valStr = item["ValueStr"].ToString();
                item["ParentClass"] = item["NavigateUrlStr"].ToString().Split('/')[1];
                var jj = string.Empty;
                var sql = string.Empty;
                switch (valStr)
                {
                    case "016":
                        keyWord += "(个人办公)";
                        item["TextStr"] = keyWord;
                        sql = string.Format(sqlFormat, userName, userName, "1=1");
                        break;
                    case "X013":
                        jj = ZWL.Common.PublicMethod.XiangMuFormIDs;
                        sql = string.Format(sqlFormat, userName, userName, " FormID in(" + jj + ")");
                        break;
                    case "j017":
                        jj = ZWL.Common.PublicMethod.JingYingFormIDs;
                        sql = string.Format(sqlFormat, userName, userName, " FormID in(" + jj + ")");
                        break;
                    case "hr002":
                        jj = ZWL.Common.PublicMethod.HRFormIDList;
                        sql = string.Format(sqlFormat, userName, userName, " FormID in(" + jj + ")");
                        break;
                    case "gw002":
                        jj = ZWL.Common.PublicMethod.GongWenFormIDs;
                        sql = string.Format(sqlFormat, userName, userName, " FormID in(" + jj + ")");
                        break;
                }
                var data = new ZWL.BLL.ERPNWorkToDo().GetList(sql);
                var rows = new List<DataRow>();
                if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow subItem in data.Tables[0].Rows)
                    {
                        rows.Add(subItem);
                    }
                }
                result.Add(item, rows);
            }

            return result;
        }

        public static bool DeleteNwByIds(string IDlist)
        {
            var idl = IDlist.Split(',');
            foreach (var id in idl)
            {
                if (string.IsNullOrEmpty(id)) continue;
                if (DeleteNwById(id) == false)
                    return false;
            }
            return true;
        }

        public static bool DeleteNwById(string Id)
        {
            var id = PublicMethod.GetInt(Id);
            var nwork = new ERPNWorkToDo(id);
            if (nwork.ID <= 0)
            {
                return false;
            }

            //如果是请假的话处理一下年休假的情况
            if (nwork.FormID == 50)
            {
                returnnxj(nwork.ID);
            }

            //可以单独设置特定节点执行的sql
            var deleteActions = DbHelperSQL.Query(
                "SELECT * FROM ERPNworkToDoDeleteAction where FormID='" + nwork.FormID + "' and WorkFlowID='" + nwork.WorkFlowID +
                "' and (JieDianID='" + nwork.JieDianID + "' or JieDianID='')");
            if (deleteActions.Tables.Count > 0)
            {
                var dt = deleteActions.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    var sql = dr["Sql"].ToString();

                    if (sql.Length > 0)
                    {
                        SqlParameter[] parameters2 = {
                            new SqlParameter("@NWorkToDoID", SqlDbType.NVarChar),
                            new SqlParameter("@BeiYong1", SqlDbType.NVarChar),
                            new SqlParameter("@BeiYong2", SqlDbType.NVarChar)
                        };
                        parameters2[0].Value = id;
                        parameters2[1].Value = nwork.BeiYong1;
                        parameters2[2].Value = nwork.BeiYong2;

                        var res = DbHelperSQL.ExecuteSqlNew(sql, parameters2);
                        if (res < 0)
                        {
                            return false;
                        }
                    }
                }
            }

            SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", SqlDbType.NVarChar)
                };
            parameters[0].Value = id;
            var rint = DbHelperSQL.ExecuteSql("delete from ERPNWorkToDo  where ID = @ID ", parameters);
            return rint > 0;
        }

        private static void returnnxj(int nwid)
        {
            ERPQingJia qingjia = new ERPQingJia();
            qingjia.GetNWorkModel(nwid);
            if (qingjia.ID > 0)
            {
                if (qingjia.ShiYongNXJ > 0)
                {
                    var nxj = qingjia.ShiYongNXJ;
                    ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
                    amodel.GetModel(qingjia.QJR);

                    amodel.returnnxj(nxj, "职工请假审批:" + qingjia.NWorkID);
                }
                if(qingjia.QJLX == "独生子女父母护理假")
                {
                    var nxj = qingjia.QJTS;
                    ZWL.BLL.DuShengLeave amodel = new ZWL.BLL.DuShengLeave();
                    amodel.GetModel(qingjia.QJR);

                    amodel.returnnxj(nxj, "职工请假审批:" + qingjia.NWorkID);
                }
                if (qingjia.QJLX == "育儿假")
                {
                    var nxj = qingjia.QJTS;
                    var amodel = new ZWL.BLL.YuErLeave();
                    amodel.GetModel(qingjia.QJR);

                    amodel.returnnxj(nxj, "职工请假审批:" + qingjia.NWorkID);
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListWithTop(string strWhere, int top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Top " + top + @" * ");
            strSql.Append(" FROM ERPNWorkToDo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public List<ZWL.BLL.ERPNWorkToDo> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPNWorkToDo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM ERPNWorkToDo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            var ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPNWorkToDo>(ds.Tables[0]);
            }
            return result;
        }

        /// <summary>
        /// 获取哪些已经签名，但是没有选择下一个流程节点的工作
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ZWL.BLL.ERPNWorkToDo> GetYiBanModelList()
        {
            var result = new List<ZWL.BLL.ERPNWorkToDo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*,n.PSType");
            strSql.Append(" FROM ERPNWorkToDo a ");
            strSql.Append(" LEFT JOIN ERPNWorkFlowNode n on a.JieDianID=n.ID ");
            strSql.Append(" where a.ShenPiUserList is not null and a.OKUserList like '%'+a.ShenPiUserList+'%' and a.StateNow='正在办理' and n.PSType='一人通过可向下流转' order by a.ID desc ");
            var ds = DbHelperSQL.Query(strSql.ToString()); ;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPNWorkToDo>(ds.Tables[0]);
            }
            return result;
        }

        public static string TableRelativeNWorkTodoSQLFormat
        {
            get
            {
                var sql = @"select * FROM (select *  FROM (select h.*,d.StateNow,d.ShenPiUserList,d.FormID,d.WorkFlowID,(select top 1 WorkFlowName from ERPNWorkFlow f where f.ID=d.WorkFlowID) WorkFlowName,
                                d.JieDianName,d.JieDianID,u.Department,d.UserName,d.TimeStr,d.LateTime,d.OKUserList,d.BeiYong1,d.BeiYong2 from {0} h
                                join ERPNWorkToDo d
                                on h.NWorkID=d.ID
                                join ERPUser u 
                                on d.UserName = u.UserName
                                ) t) t ";
                return sql;
            }
        }
        public string MD5()
        {
            var result = string.Empty;
            if (this.ID > 0)
            {
                var notlist = new List<string>() { "ShenPiYiJian", "FormContent" };
                var prs = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (prs != null && prs.Length > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var item in prs)
                    {
                        if (notlist.Contains(item.Name)) continue;
                        try
                        {
                            sb.Append(item.GetValue(this, null).ToString());
                        }
                        catch { }
                    }
                    result = PublicMethod.GetMd5(sb.ToString());
                }
            }
            return result;
        }
        #endregion  成员方法
    }
    public abstract class FlowBase
    {
        public abstract int? NWorkID { get; set; }
        public virtual T GetModel<T>(int id)
        {
            T obj = default(T);
            if (id > 0)
            {
                var list = GetModelList<T>("ID=" + id);
                if (list != null && list.Count > 0)
                    return list[0];
            }
            return obj;
        }
        public virtual List<T> GetModelList<T>(string strWhere)
        {
            var strSql = new StringBuilder();
            var tablename = typeof(T).Name;
            strSql.AppendFormat("select * from {0} ", tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dataSet = DbHelperSQL.Query(strSql.ToString());
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<T>(dataSet.Tables[0]);
            }
            return new List<T>();
        }
        public virtual Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging<T>(strWhere, cPage, pSize, this.GetType().BaseType.GetProperties()[0].Name + " desc");
        }

        public virtual Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize, string orderBy)
        {
            var strSql = new StringBuilder();
            var tablename = typeof(T).Name;
            strSql.AppendFormat(ERPNWorkToDo.TableRelativeNWorkTodoSQLFormat, tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderBy);
        }
        public virtual T GetModelByNWorkId<T>()
        {
            return GetModelByNWorkId<T>(this.NWorkID);
        }
        public virtual T GetModelByNWorkId<T>(int? nworkId)
        {
            T obj = default(T);
            if (nworkId.HasValue)
            {
                var tablename = typeof(T).Name;
                var strSql = new StringBuilder();
                strSql.AppendFormat("select top 1 * FROM {0} where {1}={2}", tablename, this.GetType().BaseType.GetProperties()[0].Name, nworkId);
                var dataSet = DbHelperSQL.Query(strSql.ToString());
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    var list = DataTableHelper.ConvertTo<T>(dataSet.Tables[0]);
                    obj = list[0];
                }
            }
            return obj;
        }
    }
    public abstract class ModelBase
    {
        public abstract int ID { get; set; }

        public virtual Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging<T>(strWhere, cPage, pSize, this.GetType().BaseType.GetProperties()[0].Name + " desc");
        }

        public virtual Pager GetListAndPaging<T>(string strWhere, int cPage, int pSize, string orderBy)
        {
            var strSql = new StringBuilder();
            var tablename = typeof(T).Name;
            strSql.AppendFormat("select * from {0} where (DeleteMark is null or DeleteMark=0)", tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderBy);
        }
        public virtual T GetModel<T>()
        {
            return GetModel<T>(this.ID);
        }
        public virtual T GetModel<T>(int id)
        {
            T obj = default(T);
            if (id > 0)
            {
                var list = GetModelList<T>("ID=" + id);
                if (list != null && list.Count > 0)
                    return list[0];
            }
            return obj;
        }
        public virtual List<T> GetModelList<T>(string strWhere)
        {
            var strSql = new StringBuilder();
            var tablename = typeof(T).Name;
            strSql.AppendFormat("select * from {0} ", tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dataSet = DbHelperSQL.Query(strSql.ToString());
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<T>(dataSet.Tables[0]);
            }
            return new List<T>();
        }
    }
}

