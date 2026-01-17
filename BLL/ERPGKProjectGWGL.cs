
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
    /// 类ERPGKProjectGWGL,工程勘察项目野外施工岗位管理
    /// </summary>
    public class ERPGKProjectGWGL
    {
        public ERPGKProjectGWGL()
        { }
        #region Model
        private int _id;//主键

        private string _number = "";

        private string _shengchanjingyingdanwei = "";

        private string _xmbh = "";

        private string _xiangmumingcheng = "";

        private string _zylb = "";

        private string _xiangmudizhi = "";

        private double _hetongjine;

        private string _xiangmufuzeren = "";

        private string _dizhibianluyuan = "";

        private string _gongdianquanyuan = "";

        private int _zuankongshu;

        private int _zuanjishu;

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
        /// 生产经营单位
        /// </summary>
        [Description("生产经营单位")]
        public string ShengChanJingYingDanWei
        {
            set { _shengchanjingyingdanwei = value; }
            get { return _shengchanjingyingdanwei; }
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
        /// 项目名称
        /// </summary>
        [Description("项目名称")]
        public string XiangMuMingCheng
        {
            set { _xiangmumingcheng = value; }
            get { return _xiangmumingcheng; }
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
        /// 项目地址
        /// </summary>
        [Description("项目地址")]
        public string XiangMuDiZhi
        {
            set { _xiangmudizhi = value; }
            get { return _xiangmudizhi; }
        }

        /// <summary>
        /// 合同金额
        /// </summary>
        [Description("合同金额")]
        public double HeTongJinE
        {
            set { _hetongjine = value; }
            get { return _hetongjine; }
        }

        /// <summary>
        /// 项目负责人
        /// </summary>
        [Description("项目负责人")]
        public string XiangMuFuZeRen
        {
            set { _xiangmufuzeren = value; }
            get { return _xiangmufuzeren; }
        }

        /// <summary>
        /// 地质编录员
        /// </summary>
        [Description("地质编录员")]
        public string DiZhiBianLuYuan
        {
            set { _dizhibianluyuan = value; }
            get { return _dizhibianluyuan; }
        }

        /// <summary>
        /// 工地安全员
        /// </summary>
        [Description("工地安全员")]
        public string GongDiAnQuanYuan
        {
            set { _gongdianquanyuan = value; }
            get { return _gongdianquanyuan; }
        }

        /// <summary>
        /// 钻孔数
        /// </summary>
        [Description("钻孔数")]
        public int ZuanKongShu
        {
            set { _zuankongshu = value; }
            get { return _zuankongshu; }
        }

        /// <summary>
        /// 钻机数
        /// </summary>
        [Description("钻机数")]
        public int ZuanJiShu
        {
            set { _zuanjishu = value; }
            get { return _zuanjishu; }
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
        public ERPGKProjectGWGL(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPGKProjectGWGL] ");
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
            strSql.Append("select count(1) from [ERPGKProjectGWGL]");
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
            strSql.Append("insert into [ERPGKProjectGWGL] (");
            strSql.Append("Number,ShengChanJingYingDanWei,XMBH,XiangMuMingCheng,ZYLB,XiangMuDiZhi,HeTongJinE,XiangMuFuZeRen,DiZhiBianLuYuan,GongDiAnQuanYuan,ZuanKongShu,ZuanJiShu,NWorkID,DJTime,DJBM,DJR)");
            strSql.Append(" values (");
            strSql.Append("@Number,@ShengChanJingYingDanWei,@XMBH,@XiangMuMingCheng,@ZYLB,@XiangMuDiZhi,@HeTongJinE,@XiangMuFuZeRen,@DiZhiBianLuYuan,@GongDiAnQuanYuan,@ZuanKongShu,@ZuanJiShu,@NWorkID,@DJTime,@DJBM,@DJR)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ShengChanJingYingDanWei", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XiangMuMingCheng", SqlDbType.NVarChar, 500),

                    new SqlParameter("@ZYLB", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XiangMuDiZhi", SqlDbType.NVarChar, 4000),

                    new SqlParameter("@HeTongJinE", SqlDbType.Float),

                    new SqlParameter("@XiangMuFuZeRen", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DiZhiBianLuYuan", SqlDbType.NVarChar, 50),

                    new SqlParameter("@GongDiAnQuanYuan", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZuanKongShu", SqlDbType.Int),

                    new SqlParameter("@ZuanJiShu", SqlDbType.Int),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50)};

            parameters[0].Value = Number;

            parameters[1].Value = ShengChanJingYingDanWei;

            parameters[2].Value = XMBH;

            parameters[3].Value = XiangMuMingCheng;

            parameters[4].Value = ZYLB;

            parameters[5].Value = XiangMuDiZhi;

            parameters[6].Value = HeTongJinE;

            parameters[7].Value = XiangMuFuZeRen;

            parameters[8].Value = DiZhiBianLuYuan;

            parameters[9].Value = GongDiAnQuanYuan;

            parameters[10].Value = ZuanKongShu;

            parameters[11].Value = ZuanJiShu;

            parameters[12].Value = NWorkID;

            parameters[13].Value = DJTime;

            parameters[14].Value = DJBM;

            parameters[15].Value = DJR;


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
            strSql.Append("update [ERPGKProjectGWGL] set ");

            strSql.Append("Number=@Number,");

            strSql.Append("ShengChanJingYingDanWei=@ShengChanJingYingDanWei,");

            strSql.Append("XMBH=@XMBH,");

            strSql.Append("XiangMuMingCheng=@XiangMuMingCheng,");

            strSql.Append("ZYLB=@ZYLB,");

            strSql.Append("XiangMuDiZhi=@XiangMuDiZhi,");

            strSql.Append("HeTongJinE=@HeTongJinE,");

            strSql.Append("XiangMuFuZeRen=@XiangMuFuZeRen,");

            strSql.Append("DiZhiBianLuYuan=@DiZhiBianLuYuan,");

            strSql.Append("GongDiAnQuanYuan=@GongDiAnQuanYuan,");

            strSql.Append("ZuanKongShu=@ZuanKongShu,");

            strSql.Append("ZuanJiShu=@ZuanJiShu,");

            strSql.Append("NWorkID=@NWorkID,");

            strSql.Append("DJTime=@DJTime,");

            strSql.Append("DJBM=@DJBM,");

            strSql.Append("DJR=@DJR");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

                    new SqlParameter("@Number", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ShengChanJingYingDanWei", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XMBH", SqlDbType.NVarChar, 50),

                    new SqlParameter("@XiangMuMingCheng", SqlDbType.NVarChar, 500),

                    new SqlParameter("@ZYLB", SqlDbType.NVarChar, 200),

                    new SqlParameter("@XiangMuDiZhi", SqlDbType.NVarChar, 4000),

                    new SqlParameter("@HeTongJinE", SqlDbType.Float),

                    new SqlParameter("@XiangMuFuZeRen", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DiZhiBianLuYuan", SqlDbType.NVarChar, 50),

                    new SqlParameter("@GongDiAnQuanYuan", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ZuanKongShu", SqlDbType.Int),

                    new SqlParameter("@ZuanJiShu", SqlDbType.Int),

                    new SqlParameter("@NWorkID", SqlDbType.Int),

                    new SqlParameter("@DJTime", SqlDbType.DateTime),

                    new SqlParameter("@DJBM", SqlDbType.NVarChar, 50),

                    new SqlParameter("@DJR", SqlDbType.NVarChar, 50),

                    new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = Number;

            parameters[1].Value = ShengChanJingYingDanWei;

            parameters[2].Value = XMBH;

            parameters[3].Value = XiangMuMingCheng;

            parameters[4].Value = ZYLB;

            parameters[5].Value = XiangMuDiZhi;

            parameters[6].Value = HeTongJinE;

            parameters[7].Value = XiangMuFuZeRen;

            parameters[8].Value = DiZhiBianLuYuan;

            parameters[9].Value = GongDiAnQuanYuan;

            parameters[10].Value = ZuanKongShu;

            parameters[11].Value = ZuanJiShu;

            parameters[12].Value = NWorkID;

            parameters[13].Value = DJTime;

            parameters[14].Value = DJBM;

            parameters[15].Value = DJR;

            parameters[16].Value = ID;

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
            strSql.Append("delete from [ERPGKProjectGWGL] ");
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
            strSql.Append(" FROM [ERPGKProjectGWGL] ");
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
                if (ds.Tables[0].Rows[0]["ShengChanJingYingDanWei"] != null)
                {
                    this.ShengChanJingYingDanWei = ds.Tables[0].Rows[0]["ShengChanJingYingDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiangMuMingCheng"] != null)
                {
                    this.XiangMuMingCheng = ds.Tables[0].Rows[0]["XiangMuMingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiangMuDiZhi"] != null)
                {
                    this.XiangMuDiZhi = ds.Tables[0].Rows[0]["XiangMuDiZhi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeTongJinE"] != null && ds.Tables[0].Rows[0]["HeTongJinE"].ToString() != "")
                {
                    this.HeTongJinE = Convert.ToDouble(ds.Tables[0].Rows[0]["HeTongJinE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XiangMuFuZeRen"] != null)
                {
                    this.XiangMuFuZeRen = ds.Tables[0].Rows[0]["XiangMuFuZeRen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiZhiBianLuYuan"] != null)
                {
                    this.DiZhiBianLuYuan = ds.Tables[0].Rows[0]["DiZhiBianLuYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GongDiAnQuanYuan"] != null)
                {
                    this.GongDiAnQuanYuan = ds.Tables[0].Rows[0]["GongDiAnQuanYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuanKongShu"] != null && ds.Tables[0].Rows[0]["ZuanKongShu"].ToString() != "")
                {
                    this.ZuanKongShu = int.Parse(ds.Tables[0].Rows[0]["ZuanKongShu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZuanJiShu"] != null && ds.Tables[0].Rows[0]["ZuanJiShu"].ToString() != "")
                {
                    this.ZuanJiShu = int.Parse(ds.Tables[0].Rows[0]["ZuanJiShu"].ToString());
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
            strSql.Append(" FROM [ERPGKProjectGWGL] ");
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
            strSql.Append(" FROM ERPGKProjectGWGL ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworktid;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public ZWL.BLL.ERPGKProjectGWGL GetModelByWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPGKProjectGWGL>(ds.Tables[0].Rows[0]);
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
        public List<ZWL.BLL.ERPGKProjectGWGL> GetListModel(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPGKProjectGWGL>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPGKProjectGWGL>(item));
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
                                  select d.ID RelativeID, p.*,WorkName,FormID
                                  ,WorkFlowID,UserName,TimeStr
                                  ,FuJianList,ShenPiYiJian
                                  ,JieDianID,JieDianName
                                  ,ShenPiUserList,OKUserList,StateNow
                                  ,LateTime,BeiYong1,BeiYong2
                                 --, SUBSTRING(BeiYong1,0,CHARINDEX('@',BeiYong1)) Number
	                              ,SUBSTRING(BeiYong1,CHARINDEX('@',BeiYong1)+1,LEN(BeiYong1)) Name from (																	
                                select (case when FormID=108 then ID when FormID=109 then cast(BeiYong2 as INT) else 0 end) RelativeID,d.* from ERPNWorkToDo d where FormID in (108,109)
                                ) d LEFT JOIN {0} p on p.NWorkID=d.RelativeID ) t ", "ERPGKProjectGWGL");
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
            var _refToDo = new ZWL.BLL.ERPNWorkToDo();
            var list = _refToDo.GetModelList("FormID=109 and BeiYong2='" + workid + "'");
            if (list != null && list.Count > 0)
            {
                _refToDo = list[0];
                _refToDo.Delete(_refToDo.ID);
            }
            _currentToDo.Delete(workid);
            var _currentModel = new ZWL.BLL.ERPGKProjectGWGL();
            _currentModel.GetModelByNWorkId(workid);
            return _currentModel.Delete(_currentModel.ID);
        }
    }
}