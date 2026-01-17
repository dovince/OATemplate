using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;
using ZWL.Common;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPNWorkToDoModifyLog。
    /// </summary>
    public partial class ERPNWorkToDoModifyLog
    {
        public ERPNWorkToDoModifyLog()
        { }
        #region Model
        private long _id;
        private string _workname;
        private string _wenhao;
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
        private string _bohuiren;
        private string _departmentnw;
        private int _nworktodoid;
        /// <summary>
        /// 
        /// </summary>
        public long ID
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
        public string WenHao
        {
            set { _wenhao = value; }
            get { return _wenhao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeStr
        {
            set { _timestr = value; }
            get { return _timestr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormContent
        {
            set { _formcontent = value; }
            get { return _formcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FuJianList
        {
            set { _fujianlist = value; }
            get { return _fujianlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShenPiYiJian
        {
            set { _shenpiyijian = value; }
            get { return _shenpiyijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? JieDianID
        {
            set { _jiedianid = value; }
            get { return _jiedianid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JieDianName
        {
            set { _jiedianname = value; }
            get { return _jiedianname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShenPiUserList
        {
            set { _shenpiuserlist = value; }
            get { return _shenpiuserlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OKUserList
        {
            set { _okuserlist = value; }
            get { return _okuserlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StateNow
        {
            set { _statenow = value; }
            get { return _statenow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LateTime
        {
            set { _latetime = value; }
            get { return _latetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeiYong1
        {
            set { _beiyong1 = value; }
            get { return _beiyong1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeiYong2
        {
            set { _beiyong2 = value; }
            get { return _beiyong2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BoHuiRen
        {
            set { _bohuiren = value; }
            get { return _bohuiren; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentNW
        {
            set { _departmentnw = value; }
            get { return _departmentnw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPNWorkToDoModifyLog(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,WorkName,WenHao,FormID,WorkFlowID,UserName,TimeStr,FormContent,FuJianList,ShenPiYiJian,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime,BeiYong1,BeiYong2,BoHuiRen,DepartmentNW,NWorkToDoID ");
            strSql.Append(" FROM [ERPNWorkToDoModifyLog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormID"] != null && ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    this.ID = long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenHao"] != null)
                {
                    this.WenHao = ds.Tables[0].Rows[0]["WenHao"].ToString();
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
                if (ds.Tables[0].Rows[0]["FormContent"] != null)
                {
                    this.FormContent = ds.Tables[0].Rows[0]["FormContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianList"] != null)
                {
                    this.FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenPiYiJian"] != null)
                {
                    this.ShenPiYiJian = ds.Tables[0].Rows[0]["ShenPiYiJian"].ToString();
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
                if (ds.Tables[0].Rows[0]["BoHuiRen"] != null)
                {
                    this.BoHuiRen = ds.Tables[0].Rows[0]["BoHuiRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DepartmentNW"] != null)
                {
                    this.DepartmentNW = ds.Tables[0].Rows[0]["DepartmentNW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPNWorkToDoModifyLog]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPNWorkToDoModifyLog] (");
            strSql.Append("WorkName,WenHao,FormID,WorkFlowID,UserName,TimeStr,FormContent,FuJianList,ShenPiYiJian,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime,BeiYong1,BeiYong2,BoHuiRen,DepartmentNW,NWorkToDoID)");
            strSql.Append(" values (");
            strSql.Append("@WorkName,@WenHao,@FormID,@WorkFlowID,@UserName,@TimeStr,@FormContent,@FuJianList,@ShenPiYiJian,@JieDianID,@JieDianName,@ShenPiUserList,@OKUserList,@StateNow,@LateTime,@BeiYong1,@BeiYong2,@BoHuiRen,@DepartmentNW,@NWorkToDoID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@WenHao", SqlDbType.VarChar,200),
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@FormContent", SqlDbType.Text),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
					new SqlParameter("@JieDianID", SqlDbType.Int,4),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@OKUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50),
					new SqlParameter("@LateTime", SqlDbType.DateTime),
					new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
					new SqlParameter("@BeiYong2", SqlDbType.VarChar,100),
					new SqlParameter("@BoHuiRen", SqlDbType.VarChar,50),
					new SqlParameter("@DepartmentNW", SqlDbType.VarChar,100),
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4)};
            parameters[0].Value = WorkName;
            parameters[1].Value = WenHao;
            parameters[2].Value = FormID;
            parameters[3].Value = WorkFlowID;
            parameters[4].Value = UserName;
            parameters[5].Value = TimeStr;
            parameters[6].Value = FormContent;
            parameters[7].Value = FuJianList;
            parameters[8].Value = ShenPiYiJian;
            parameters[9].Value = JieDianID;
            parameters[10].Value = JieDianName;
            parameters[11].Value = ShenPiUserList;
            parameters[12].Value = OKUserList;
            parameters[13].Value = StateNow;
            parameters[14].Value = LateTime;
            parameters[15].Value = BeiYong1;
            parameters[16].Value = BeiYong2;
            parameters[17].Value = BoHuiRen;
            parameters[18].Value = DepartmentNW;
            parameters[19].Value = NWorkToDoID;

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
            strSql.Append("update [ERPNWorkToDoModifyLog] set ");
            strSql.Append("WorkName=@WorkName,");
            strSql.Append("WenHao=@WenHao,");
            strSql.Append("FormID=@FormID,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("FormContent=@FormContent,");
            strSql.Append("FuJianList=@FuJianList,");
            strSql.Append("ShenPiYiJian=@ShenPiYiJian,");
            strSql.Append("JieDianID=@JieDianID,");
            strSql.Append("JieDianName=@JieDianName,");
            strSql.Append("ShenPiUserList=@ShenPiUserList,");
            strSql.Append("OKUserList=@OKUserList,");
            strSql.Append("StateNow=@StateNow,");
            strSql.Append("LateTime=@LateTime,");
            strSql.Append("BeiYong1=@BeiYong1,");
            strSql.Append("BeiYong2=@BeiYong2,");
            strSql.Append("BoHuiRen=@BoHuiRen,");
            strSql.Append("DepartmentNW=@DepartmentNW,");
            strSql.Append("NWorkToDoID=@NWorkToDoID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkName", SqlDbType.VarChar,200),
					new SqlParameter("@WenHao", SqlDbType.VarChar,200),
					new SqlParameter("@FormID", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@FormContent", SqlDbType.Text),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@ShenPiYiJian", SqlDbType.Text),
					new SqlParameter("@JieDianID", SqlDbType.Int,4),
					new SqlParameter("@JieDianName", SqlDbType.VarChar,50),
					new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@OKUserList", SqlDbType.VarChar,8000),
					new SqlParameter("@StateNow", SqlDbType.VarChar,50),
					new SqlParameter("@LateTime", SqlDbType.DateTime),
					new SqlParameter("@BeiYong1", SqlDbType.VarChar,500),
					new SqlParameter("@BeiYong2", SqlDbType.VarChar,100),
					new SqlParameter("@BoHuiRen", SqlDbType.VarChar,50),
					new SqlParameter("@DepartmentNW", SqlDbType.VarChar,100),
					new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = WorkName;
            parameters[1].Value = WenHao;
            parameters[2].Value = FormID;
            parameters[3].Value = WorkFlowID;
            parameters[4].Value = UserName;
            parameters[5].Value = TimeStr;
            parameters[6].Value = FormContent;
            parameters[7].Value = FuJianList;
            parameters[8].Value = ShenPiYiJian;
            parameters[9].Value = JieDianID;
            parameters[10].Value = JieDianName;
            parameters[11].Value = ShenPiUserList;
            parameters[12].Value = OKUserList;
            parameters[13].Value = StateNow;
            parameters[14].Value = LateTime;
            parameters[15].Value = BeiYong1;
            parameters[16].Value = BeiYong2;
            parameters[17].Value = BoHuiRen;
            parameters[18].Value = DepartmentNW;
            parameters[19].Value = NWorkToDoID;
            parameters[20].Value = ID;

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
        public bool Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPNWorkToDoModifyLog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
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
        public void GetModel(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,WorkName,WenHao,FormID,WorkFlowID,UserName,TimeStr,FormContent,FuJianList,ShenPiYiJian,JieDianID,JieDianName,ShenPiUserList,OKUserList,StateNow,LateTime,BeiYong1,BeiYong2,BoHuiRen,DepartmentNW,NWorkToDoID ");
            strSql.Append(" FROM [ERPNWorkToDoModifyLog] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkName"] != null)
                {
                    this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenHao"] != null)
                {
                    this.WenHao = ds.Tables[0].Rows[0]["WenHao"].ToString();
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
                if (ds.Tables[0].Rows[0]["FormContent"] != null)
                {
                    this.FormContent = ds.Tables[0].Rows[0]["FormContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJianList"] != null)
                {
                    this.FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenPiYiJian"] != null)
                {
                    this.ShenPiYiJian = ds.Tables[0].Rows[0]["ShenPiYiJian"].ToString();
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
                if (ds.Tables[0].Rows[0]["BoHuiRen"] != null)
                {
                    this.BoHuiRen = ds.Tables[0].Rows[0]["BoHuiRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DepartmentNW"] != null)
                {
                    this.DepartmentNW = ds.Tables[0].Rows[0]["DepartmentNW"].ToString();
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
            strSql.Append(" FROM [ERPNWorkToDoModifyLog] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method

        public static void Log(int id)
        {
            try
            {
                if (id > 0)
                {
                    ZWL.BLL.ERPNWorkToDo model = new ZWL.BLL.ERPNWorkToDo(id);
                    ZWL.BLL.ERPNWorkToDoModifyLog log = new ZWL.BLL.ERPNWorkToDoModifyLog();
                    if (model.ID <= 0)
                        return;

                    log.NWorkToDoID = model.ID;
                    log.WorkName = model.WorkName;
                    log.FormID = model.FormID;
                    log.WorkFlowID = model.WorkFlowID;
                    log.UserName = model.UserName;
                    log.TimeStr = model.TimeStr;
                    log.FormContent = model.FormContent;
                    log.FuJianList = model.FuJianList;
                    log.ShenPiYiJian = model.ShenPiYiJian;
                    log.JieDianID = model.JieDianID;
                    log.JieDianName = model.JieDianName;
                    log.ShenPiUserList = model.ShenPiUserList;
                    log.OKUserList = model.OKUserList;
                    log.StateNow = model.StateNow;
                    log.BeiYong1 = model.BeiYong1;
                    log.BeiYong2 = model.BeiYong2;
                    //log.BoHuiRen = model.BoHuiRen;
                    //log.DepartmentNW = model.DepartmentNW;
                    log.LateTime = DateTime.Now;
                    log.Add();
                }
            }
            catch
            {
            }
        }

        public static void LogList(string ids)
        {
            if(ids == null)
            {
                return;
            }
            var idarr = ids.Split(",");
            foreach(var idstr in idarr)
            {
                try
                {
                    var id = Convert.ToInt32(idstr);
                    if (id > 0)
                    {
                        ZWL.BLL.ERPNWorkToDo model = new ZWL.BLL.ERPNWorkToDo(id);
                        ZWL.BLL.ERPNWorkToDoModifyLog log = new ZWL.BLL.ERPNWorkToDoModifyLog();
                        if (model.ID <= 0)
                            return;

                        log.NWorkToDoID = model.ID;
                        log.WorkName = model.WorkName;
                        log.FormID = model.FormID;
                        log.WorkFlowID = model.WorkFlowID;
                        log.UserName = model.UserName;
                        log.TimeStr = model.TimeStr;
                        log.FormContent = model.FormContent;
                        log.FuJianList = model.FuJianList;
                        log.ShenPiYiJian = model.ShenPiYiJian;
                        log.JieDianID = model.JieDianID;
                        log.JieDianName = model.JieDianName;
                        log.ShenPiUserList = model.ShenPiUserList;
                        log.OKUserList = model.OKUserList;
                        log.StateNow = model.StateNow;
                        log.BeiYong1 = model.BeiYong1;
                        log.BeiYong2 = model.BeiYong2;
                        //log.BoHuiRen = model.BoHuiRen;
                        //log.DepartmentNW = model.DepartmentNW;
                        log.LateTime = DateTime.Now;
                        log.Add();
                    }
                }
                catch
                {
                }
            }
        }

    }
}

