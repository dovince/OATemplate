using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPSubcontractTeam,分包队伍管理
	/// </summary>
	public class ERPSubcontractTeam
    {
        public ERPSubcontractTeam()
        { }
        #region Model
        private int _id;//主键

        private string _fbdwbh = "";

        private string _fbdwmc = "";

        private string _fbfw = "";

        private string _tyxydm = "";

        private string _jyfw = "";

        private string _zzzs = "";

        private string _fzrxm = "";

        private string _fzrsfzhm = "";

        private string _fzrlxdh = "";

        private string _zcdz = "";

        private string _zj = "";

        private string _gxy = "";

        private string _qzy = "";

        private string _gpsjsj = "";

        private string _gdqwj = "";

        private string _csy = "";

        private string _szy = "";

        private string _hty = "";

        private string _tjbm = "";

        private DateTime? _tjtime;

        private string _khjg = "";

        private DateTime? _khtime;

        private string _khbz = "";

        private string _gd = "";

        private string _js = "";

        private string _zxds = "";

        private string _bz = "";

        private int _isdeleted;

        private int _orderby;

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
        /// 分包队伍编号
        /// </summary>
        [Description("分包队伍编号")]
        public string FBDWBH
        {
            set { _fbdwbh = value; }
            get { return _fbdwbh; }
        }

        /// <summary>
        /// 分包队伍名称
        /// </summary>
        [Description("分包队伍名称")]
        public string FBDWMC
        {
            set { _fbdwmc = value; }
            get { return _fbdwmc; }
        }

        /// <summary>
        /// 分包范围
        /// </summary>
        [Description("分包范围")]
        public string FBFW
        {
            set { _fbfw = value; }
            get { return _fbfw; }
        }

        /// <summary>
        /// 统一信用代码
        /// </summary>
        [Description("统一信用代码")]
        public string TYXYDM
        {
            set { _tyxydm = value; }
            get { return _tyxydm; }
        }

        /// <summary>
        /// 经营范围
        /// </summary>
        [Description("经营范围")]
        public string JYFW
        {
            set { _jyfw = value; }
            get { return _jyfw; }
        }

        /// <summary>
        /// 资质证书
        /// </summary>
        [Description("资质证书")]
        public string ZZZS
        {
            set { _zzzs = value; }
            get { return _zzzs; }
        }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [Description("负责人姓名")]
        public string FZRXM
        {
            set { _fzrxm = value; }
            get { return _fzrxm; }
        }

        /// <summary>
        /// 负责人身份证
        /// </summary>
        [Description("负责人身份证")]
        public string FZRSFZHM
        {
            set { _fzrsfzhm = value; }
            get { return _fzrsfzhm; }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Description("联系电话")]
        public string FZRLXDH
        {
            set { _fzrlxdh = value; }
            get { return _fzrlxdh; }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        [Description("注册地址")]
        public string ZCDZ
        {
            set { _zcdz = value; }
            get { return _zcdz; }
        }

        /// <summary>
        /// 钻机
        /// </summary>
        [Description("钻机")]
        public string ZJ
        {
            set { _zj = value; }
            get { return _zj; }
        }

        /// <summary>
        /// 管线仪
        /// </summary>
        [Description("管线仪")]
        public string GXY
        {
            set { _gxy = value; }
            get { return _gxy; }
        }

        /// <summary>
        /// 全站仪
        /// </summary>
        [Description("全站仪")]
        public string QZY
        {
            set { _qzy = value; }
            get { return _qzy; }
        }

        /// <summary>
        /// GPS接收机
        /// </summary>
        [Description("GPS接收机")]
        public string GPSJSJ
        {
            set { _gpsjsj = value; }
            get { return _gpsjsj; }
        }

        /// <summary>
        /// 管道潜望镜
        /// </summary>
        [Description("管道潜望镜")]
        public string GDQWJ
        {
            set { _gdqwj = value; }
            get { return _gdqwj; }
        }

        /// <summary>
        /// 测深仪
        /// </summary>
        [Description("测深仪")]
        public string CSY
        {
            set { _csy = value; }
            get { return _csy; }
        }

        /// <summary>
        /// 水准仪
        /// </summary>
        [Description("水准仪")]
        public string SZY
        {
            set { _szy = value; }
            get { return _szy; }
        }

        /// <summary>
        /// 绘图仪
        /// </summary>
        [Description("绘图仪")]
        public string HTY
        {
            set { _hty = value; }
            get { return _hty; }
        }

        /// <summary>
        /// 推荐部门
        /// </summary>
        [Description("推荐部门")]
        public string TJBM
        {
            set { _tjbm = value; }
            get { return _tjbm; }
        }

        /// <summary>
        /// 推荐时间
        /// </summary>
        [Description("推荐时间")]
        public DateTime? TJTime
        {
            set { _tjtime = value; }
            get { return _tjtime; }
        }

        /// <summary>
        /// 考核结果
        /// </summary>
        [Description("考核结果")]
        public string KHJG
        {
            set { _khjg = value; }
            get { return _khjg; }
        }

        /// <summary>
        /// 考核时间
        /// </summary>
        [Description("考核时间")]
        public DateTime? KHTime
        {
            set { _khtime = value; }
            get { return _khtime; }
        }

        /// <summary>
        /// 考核备注
        /// </summary>
        [Description("考核备注")]
        public string KHBZ
        {
            set { _khbz = value; }
            get { return _khbz; }
        }

        /// <summary>
        /// 股东
        /// </summary>
        [Description("股东")]
        public string GD
        {
            set { _gd = value; }
            get { return _gd; }
        }

        /// <summary>
        /// 监事
        /// </summary>
        [Description("监事")]
        public string JS
        {
            set { _js = value; }
            get { return _js; }
        }

        /// <summary>
        /// 执行董事
        /// </summary>
        [Description("执行董事")]
        public string ZXDS
        {
            set { _zxds = value; }
            get { return _zxds; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }

        /// <summary>
        /// 删除标识
        /// </summary>
        [Description("删除标识")]
        public int IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int OrderBy
        {
            set { _orderby = value; }
            get { return _orderby; }
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
        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPSubcontractTeam(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPSubcontractTeam] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ERPSubcontractTeam");
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPSubcontractTeam]");
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
            strSql.Append("insert into [ERPSubcontractTeam] (");
            strSql.Append("FBDWBH,FBDWMC,FBFW,TYXYDM,JYFW,ZZZS,FZRXM,FZRSFZHM,FZRLXDH,ZCDZ,ZJ,GXY,QZY,GPSJSJ,GDQWJ,CSY,SZY,HTY,TJBM,TJTime,KHJG,KHTime,KHBZ,GD,JS,ZXDS,BZ,DJTime,DJBM,DJR,OrderBy,IsDeleted)");
            strSql.Append(" values (");
            strSql.Append("@FBDWBH,@FBDWMC,@FBFW,@TYXYDM,@JYFW,@ZZZS,@FZRXM,@FZRSFZHM,@FZRLXDH,@ZCDZ,@ZJ,@GXY,@QZY,@GPSJSJ,@GDQWJ,@CSY,@SZY,@HTY,@TJBM,@TJTime,@KHJG,@KHTime,@KHBZ,@GD,@JS,@ZXDS,@BZ,@DJTime,@DJBM,@DJR,@OrderBy,@IsDeleted)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@FBDWBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDWMC", SqlDbType.NVarChar,500),
                    new SqlParameter("@FBFW", SqlDbType.NVarChar,500),
                    new SqlParameter("@TYXYDM", SqlDbType.NVarChar,500),
                    new SqlParameter("@JYFW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZZZS", SqlDbType.NVarChar,500),
                    new SqlParameter("@FZRXM", SqlDbType.NVarChar,200),
                    new SqlParameter("@FZRSFZHM", SqlDbType.NVarChar,200),
                    new SqlParameter("@FZRLXDH", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZCDZ", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GXY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@QZY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GPSJSJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GDQWJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CSY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@SZY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@HTY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@TJBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@TJTime", SqlDbType.DateTime),
                    new SqlParameter("@KHJG", SqlDbType.NVarChar,200),
                    new SqlParameter("@KHTime", SqlDbType.DateTime),
                    new SqlParameter("@KHBZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@GD", SqlDbType.NVarChar,500),
                    new SqlParameter("@JS", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZXDS", SqlDbType.NVarChar,500),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,100),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,50),
                    new SqlParameter("@OrderBy", SqlDbType.Int,4),
                    new SqlParameter("@IsDeleted", SqlDbType.Int,4)};
            parameters[0].Value = FBDWBH;
            parameters[1].Value = FBDWMC;
            parameters[2].Value = FBFW;
            parameters[3].Value = TYXYDM;
            parameters[4].Value = JYFW;
            parameters[5].Value = ZZZS;
            parameters[6].Value = FZRXM;
            parameters[7].Value = FZRSFZHM;
            parameters[8].Value = FZRLXDH;
            parameters[9].Value = ZCDZ;
            parameters[10].Value = ZJ;
            parameters[11].Value = GXY;
            parameters[12].Value = QZY;
            parameters[13].Value = GPSJSJ;
            parameters[14].Value = GDQWJ;
            parameters[15].Value = CSY;
            parameters[16].Value = SZY;
            parameters[17].Value = HTY;
            parameters[18].Value = TJBM;
            parameters[19].Value = TJTime;
            parameters[20].Value = KHJG;
            parameters[21].Value = KHTime;
            parameters[22].Value = KHBZ;
            parameters[23].Value = GD;
            parameters[24].Value = JS;
            parameters[25].Value = ZXDS;
            parameters[26].Value = BZ;
            parameters[27].Value = DJTime;
            parameters[28].Value = DJBM;
            parameters[29].Value = DJR;
            parameters[30].Value = OrderBy;
            parameters[31].Value = IsDeleted;

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
            strSql.Append("update [ERPSubcontractTeam] set ");
            strSql.Append("FBDWBH=@FBDWBH,");
            strSql.Append("FBDWMC=@FBDWMC,");
            strSql.Append("FBFW=@FBFW,");
            strSql.Append("TYXYDM=@TYXYDM,");
            strSql.Append("JYFW=@JYFW,");
            strSql.Append("ZZZS=@ZZZS,");
            strSql.Append("FZRXM=@FZRXM,");
            strSql.Append("FZRSFZHM=@FZRSFZHM,");
            strSql.Append("FZRLXDH=@FZRLXDH,");
            strSql.Append("ZCDZ=@ZCDZ,");
            strSql.Append("ZJ=@ZJ,");
            strSql.Append("GXY=@GXY,");
            strSql.Append("QZY=@QZY,");
            strSql.Append("GPSJSJ=@GPSJSJ,");
            strSql.Append("GDQWJ=@GDQWJ,");
            strSql.Append("CSY=@CSY,");
            strSql.Append("SZY=@SZY,");
            strSql.Append("HTY=@HTY,");
            strSql.Append("TJBM=@TJBM,");
            strSql.Append("TJTime=@TJTime,");
            strSql.Append("KHJG=@KHJG,");
            strSql.Append("KHTime=@KHTime,");
            strSql.Append("KHBZ=@KHBZ,");
            strSql.Append("GD=@GD,");
            strSql.Append("JS=@JS,");
            strSql.Append("ZXDS=@ZXDS,");
            strSql.Append("BZ=@BZ,");
            strSql.Append("DJTime=@DJTime,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("OrderBy=@OrderBy,");
            strSql.Append("IsDeleted=@IsDeleted");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FBDWBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@FBDWMC", SqlDbType.NVarChar,500),
                    new SqlParameter("@FBFW", SqlDbType.NVarChar,500),
                    new SqlParameter("@TYXYDM", SqlDbType.NVarChar,500),
                    new SqlParameter("@JYFW", SqlDbType.NVarChar,-1),
                    new SqlParameter("@ZZZS", SqlDbType.NVarChar,500),
                    new SqlParameter("@FZRXM", SqlDbType.NVarChar,200),
                    new SqlParameter("@FZRSFZHM", SqlDbType.NVarChar,200),
                    new SqlParameter("@FZRLXDH", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZCDZ", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GXY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@QZY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GPSJSJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@GDQWJ", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CSY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@SZY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@HTY", SqlDbType.NVarChar,2000),
                    new SqlParameter("@TJBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@TJTime", SqlDbType.DateTime),
                    new SqlParameter("@KHJG", SqlDbType.NVarChar,200),
                    new SqlParameter("@KHTime", SqlDbType.DateTime),
                    new SqlParameter("@KHBZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@GD", SqlDbType.NVarChar,500),
                    new SqlParameter("@JS", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZXDS", SqlDbType.NVarChar,500),
                    new SqlParameter("@BZ", SqlDbType.NVarChar,-1),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,100),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,50),
                    new SqlParameter("@OrderBy", SqlDbType.Int,4),
                    new SqlParameter("@IsDeleted", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FBDWBH;
            parameters[1].Value = FBDWMC;
            parameters[2].Value = FBFW;
            parameters[3].Value = TYXYDM;
            parameters[4].Value = JYFW;
            parameters[5].Value = ZZZS;
            parameters[6].Value = FZRXM;
            parameters[7].Value = FZRSFZHM;
            parameters[8].Value = FZRLXDH;
            parameters[9].Value = ZCDZ;
            parameters[10].Value = ZJ;
            parameters[11].Value = GXY;
            parameters[12].Value = QZY;
            parameters[13].Value = GPSJSJ;
            parameters[14].Value = GDQWJ;
            parameters[15].Value = CSY;
            parameters[16].Value = SZY;
            parameters[17].Value = HTY;
            parameters[18].Value = TJBM;
            parameters[19].Value = TJTime;
            parameters[20].Value = KHJG;
            parameters[21].Value = KHTime;
            parameters[22].Value = KHBZ;
            parameters[23].Value = GD;
            parameters[24].Value = JS;
            parameters[25].Value = ZXDS;
            parameters[26].Value = BZ;
            parameters[27].Value = DJTime;
            parameters[28].Value = DJBM;
            parameters[29].Value = DJR;
            parameters[30].Value = OrderBy;
            parameters[31].Value = IsDeleted;
            parameters[32].Value = ID;

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
            strSql.Append("update [ERPSubcontractTeam] set IsDeleted=1 ");
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
            strSql.Append(" FROM [ERPSubcontractTeam] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBDWBH"] != null)
                {
                    this.FBDWBH = ds.Tables[0].Rows[0]["FBDWBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBDWMC"] != null)
                {
                    this.FBDWMC = ds.Tables[0].Rows[0]["FBDWMC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FBFW"] != null)
                {
                    this.FBFW = ds.Tables[0].Rows[0]["FBFW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TYXYDM"] != null)
                {
                    this.TYXYDM = ds.Tables[0].Rows[0]["TYXYDM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JYFW"] != null)
                {
                    this.JYFW = ds.Tables[0].Rows[0]["JYFW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZZZS"] != null)
                {
                    this.ZZZS = ds.Tables[0].Rows[0]["ZZZS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZRXM"] != null)
                {
                    this.FZRXM = ds.Tables[0].Rows[0]["FZRXM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZRSFZHM"] != null)
                {
                    this.FZRSFZHM = ds.Tables[0].Rows[0]["FZRSFZHM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZRLXDH"] != null)
                {
                    this.FZRLXDH = ds.Tables[0].Rows[0]["FZRLXDH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZCDZ"] != null)
                {
                    this.ZCDZ = ds.Tables[0].Rows[0]["ZCDZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZJ"] != null)
                {
                    this.ZJ = ds.Tables[0].Rows[0]["ZJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GXY"] != null)
                {
                    this.GXY = ds.Tables[0].Rows[0]["GXY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QZY"] != null)
                {
                    this.QZY = ds.Tables[0].Rows[0]["QZY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GPSJSJ"] != null)
                {
                    this.GPSJSJ = ds.Tables[0].Rows[0]["GPSJSJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GDQWJ"] != null)
                {
                    this.GDQWJ = ds.Tables[0].Rows[0]["GDQWJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSY"] != null)
                {
                    this.CSY = ds.Tables[0].Rows[0]["CSY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SZY"] != null)
                {
                    this.SZY = ds.Tables[0].Rows[0]["SZY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTY"] != null)
                {
                    this.HTY = ds.Tables[0].Rows[0]["HTY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TJBM"] != null)
                {
                    this.TJBM = ds.Tables[0].Rows[0]["TJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TJTime"] != null && ds.Tables[0].Rows[0]["TJTime"].ToString() != "")
                {
                    this.TJTime = DateTime.Parse(ds.Tables[0].Rows[0]["TJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KHJG"] != null)
                {
                    this.KHJG = ds.Tables[0].Rows[0]["KHJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KHTime"] != null && ds.Tables[0].Rows[0]["KHTime"].ToString() != "")
                {
                    this.KHTime = DateTime.Parse(ds.Tables[0].Rows[0]["KHTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KHBZ"] != null)
                {
                    this.KHBZ = ds.Tables[0].Rows[0]["KHBZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GD"] != null)
                {
                    this.GD = ds.Tables[0].Rows[0]["GD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JS"] != null)
                {
                    this.JS = ds.Tables[0].Rows[0]["JS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZXDS"] != null)
                {
                    this.ZXDS = ds.Tables[0].Rows[0]["ZXDS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJTime"] != null && ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
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
                if (ds.Tables[0].Rows[0]["OrderBy"] != null && ds.Tables[0].Rows[0]["OrderBy"].ToString() != "")
                {
                    this.OrderBy = int.Parse(ds.Tables[0].Rows[0]["OrderBy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDeleted"] != null && ds.Tables[0].Rows[0]["IsDeleted"].ToString() != "")
                {
                    this.IsDeleted = int.Parse(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
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
            strSql.Append(" FROM [ERPSubcontractTeam] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
        public ZWL.BLL.ERPSubcontractTeam GetModelByWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPSubcontractTeam>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPSubcontractTeam> GetListModel(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPSubcontractTeam>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPSubcontractTeam>(item));
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
            strSql.AppendFormat(@"select *  FROM ERPSubcontractTeam");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currPage, pageSize);
        }
        public Pager GetListMappingAndPagingHan(string strWhere, int currPage, int pageSize)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from(select {0}  FROM ERPSubcontractTeam) t", GetListColumnsSQLHan);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currPage, pageSize);
        }
        private string GetListColumnsSQLHan
        {
            get
            {
                var sql = @"ID
      ,FBDWBH
      ,FBDWMC 分包队伍名称
      ,FBFW 分包范围
      ,TYXYDM 统一信用代码
      ,JYFW 经营范围
      ,ZZZS 资质证书
      ,FZRXM 负责人姓名
      ,FZRSFZHM 负责人身份证号码
      ,FZRLXDH 联系电话
      ,ZCDZ 注册地址
      ,ZJ 钻机
      ,GXY 管线仪
      ,QZY 全站仪
      ,GPSJSJ GPS接收机
      ,GDQWJ 管道潜望镜
      ,CSY 测深仪
      ,SZY 水准仪
      ,HTY 绘图仪
      ,TJBM 推荐部门
      ,TJTime 推荐时间
      ,KHJG 考核结果
      ,KHTime 考核时间
      ,KHBZ 考核备注
      ,GD 股东
      ,JS 监事
      ,ZXDS 执行董事
      ,BZ 备注
      ,DJTime 登记时间
      ,DJBM 登记部门
      ,DJR 登记人
      ,OrderBy
      ,IsDeleted 标识";
                return sql;
            }
        }
    }
}