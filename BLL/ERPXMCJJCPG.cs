using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPXMCJJCPG。
    /// </summary>
    [Serializable]
    public partial class ERPXMCJJCPG : FlowBase
    {
        public ERPXMCJJCPG()
        { }
        #region Model
        private int _id;
        private string _xmbh;
        private string _xmname;
        private string _wtdwname;
        private decimal _amount;
        private string _zjly;
        private string _province;
        private string _city;
        private string _district;
        private string _address;
        private string _zylb;
        private string _hylb;
        private string _xmgk;
        private string _fkfs;
        private decimal _profitrate;
        private string _xmfxpg;
        private string _sftycj;
        private string _djbm;
        private string _djr;
        private DateTime _djtime;
        private int? _nworkid;
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
        [Description("项目编号")]
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目名称")]
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("招标单位")]
        public string WTDWName
        {
            set { _wtdwname = value; }
            get { return _wtdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目金额")]
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("资金来源")]
        public string ZJLY
        {
            set { _zjly = value; }
            get { return _zjly; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("省份")]
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("城市")]
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("县区")]
        public string District
        {
            set { _district = value; }
            get { return _district; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目地址")]
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("专业类别")]
        public string ZYLB
        {
            set { _zylb = value; }
            get { return _zylb; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("行业类别")]
        public string HYLB
        {
            set { _hylb = value; }
            get { return _hylb; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目概况")]
        public string XMGK
        {
            set { _xmgk = value; }
            get { return _xmgk; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("付款方式")]
        public string FKFS
        {
            set { _fkfs = value; }
            get { return _fkfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("预计利润率")]
        public decimal ProfitRate
        {
            set { _profitrate = value; }
            get { return _profitrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目风险评估")]
        public string XMFXPG
        {
            set { _xmfxpg = value; }
            get { return _xmfxpg; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("是否同意承接")]
        public string SFTYCJ
        {
            set { _sftycj = value; }
            get { return _sftycj; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("项目承接部门")]
        public string DJBM
        {
            set { _djbm = value; }
            get { return _djbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("经办人")]
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("登记时间")]
        public DateTime DJTime
        {
            set { _djtime = value; }
            get { return _djtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
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
                if (NWorkID.HasValue && NWorkID.Value > 0)
                {
                    _currentToDo.GetModel(NWorkID.Value);
                }
                return _currentToDo;
            }
        }

        private List<ZWL.BLL.BaseTableRelativeColumn> _relativeTableItems = null;
        public List<ZWL.BLL.BaseTableRelativeColumn> RelativeItems
        {
            get
            {
                if (_relativeTableItems == null)
                    _relativeTableItems = new List<ZWL.BLL.BaseTableRelativeColumn>();
                if (ID > 0)
                {
                    var _currentModel = new ZWL.BLL.BaseTableRelativeColumn();
                    _relativeTableItems = _currentModel.GetModelList<ZWL.BLL.BaseTableRelativeColumn>("LTable='ERPXMCJJCPG' and LC1='ID' and LV1='" + this.ID + "'");
                }
                return _relativeTableItems;
            }
            set
            {
                _relativeTableItems = value;
            }
        }
        private ZWL.BLL.BaseTableRelativeColumn _relativeItem;
        public ZWL.BLL.BaseTableRelativeColumn RelativeItem
        {
            get
            {
                if (_relativeItem == null && RelativeItems.Any())
                    _relativeItem = RelativeItems.FirstOrDefault();
                return _relativeItem;
            }
            set
            {
                _relativeItem = value;
            }
        }
        #endregion

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMCJJCPG(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMCJJCPG] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMCJJCPG]");
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
            strSql.Append("insert into [ERPXMCJJCPG] (");
            strSql.Append("XMBH,XMName,WTDWName,Amount,ZJLY,Province,City,District,Address,ZYLB,HYLB,XMGK,FKFS,ProfitRate,XMFXPG,SFTYCJ,DJBM,DJR,DJTime,NWorkID)");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@XMName,@WTDWName,@Amount,@ZJLY,@Province,@City,@District,@Address,@ZYLB,@HYLB,@XMGK,@FKFS,@ProfitRate,@XMFXPG,@SFTYCJ,@DJBM,@DJR,@DJTime,@NWorkID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@ZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@Province", SqlDbType.NVarChar,50),
                    new SqlParameter("@City", SqlDbType.NVarChar,50),
                    new SqlParameter("@District", SqlDbType.NVarChar,50),
                    new SqlParameter("@Address", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMGK", SqlDbType.NVarChar,-1),
                    new SqlParameter("@FKFS", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ProfitRate", SqlDbType.Decimal,9),
                    new SqlParameter("@XMFXPG", SqlDbType.NVarChar,-1),
                    new SqlParameter("@SFTYCJ", SqlDbType.VarChar,10),
                    new SqlParameter("@DJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@DJR", SqlDbType.VarChar,50),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = WTDWName;
            parameters[3].Value = Amount;
            parameters[4].Value = ZJLY;
            parameters[5].Value = Province;
            parameters[6].Value = City;
            parameters[7].Value = District;
            parameters[8].Value = Address;
            parameters[9].Value = ZYLB;
            parameters[10].Value = HYLB;
            parameters[11].Value = XMGK;
            parameters[12].Value = FKFS;
            parameters[13].Value = ProfitRate;
            parameters[14].Value = XMFXPG;
            parameters[15].Value = SFTYCJ;
            parameters[16].Value = DJBM;
            parameters[17].Value = DJR;
            parameters[18].Value = DJTime;
            parameters[19].Value = NWorkID;

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
            strSql.Append("update [ERPXMCJJCPG] set ");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("WTDWName=@WTDWName,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("ZJLY=@ZJLY,");
            strSql.Append("Province=@Province,");
            strSql.Append("City=@City,");
            strSql.Append("District=@District,");
            strSql.Append("Address=@Address,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HYLB=@HYLB,");
            strSql.Append("XMGK=@XMGK,");
            strSql.Append("FKFS=@FKFS,");
            strSql.Append("ProfitRate=@ProfitRate,");
            strSql.Append("XMFXPG=@XMFXPG,");
            strSql.Append("SFTYCJ=@SFTYCJ,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("DJTime=@DJTime,");
            strSql.Append("NWorkID=@NWorkID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.VarChar,50),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@ZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@Province", SqlDbType.NVarChar,50),
                    new SqlParameter("@City", SqlDbType.NVarChar,50),
                    new SqlParameter("@District", SqlDbType.NVarChar,50),
                    new SqlParameter("@Address", SqlDbType.NVarChar,500),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@HYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMGK", SqlDbType.NVarChar,-1),
                    new SqlParameter("@FKFS", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ProfitRate", SqlDbType.Decimal,9),
                    new SqlParameter("@XMFXPG", SqlDbType.NVarChar,-1),
                    new SqlParameter("@SFTYCJ", SqlDbType.VarChar,10),
                    new SqlParameter("@DJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@DJR", SqlDbType.VarChar,50),
                    new SqlParameter("@DJTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = WTDWName;
            parameters[3].Value = Amount;
            parameters[4].Value = ZJLY;
            parameters[5].Value = Province;
            parameters[6].Value = City;
            parameters[7].Value = District;
            parameters[8].Value = Address;
            parameters[9].Value = ZYLB;
            parameters[10].Value = HYLB;
            parameters[11].Value = XMGK;
            parameters[12].Value = FKFS;
            parameters[13].Value = ProfitRate;
            parameters[14].Value = XMFXPG;
            parameters[15].Value = SFTYCJ;
            parameters[16].Value = DJBM;
            parameters[17].Value = DJR;
            parameters[18].Value = DJTime;
            parameters[19].Value = NWorkID;
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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPXMCJJCPG] ");
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
            strSql.Append(" FROM [ERPXMCJJCPG] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMCJJCPG] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new PublicMethod();
            var strmapping = method.getSQLTable("ERPXMCJJCPG");

            strSql = "select * from (" + strmapping + ") as t ";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }
        #endregion  Method

        private void SetPropertyValue(DataSet ds)
        {
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
                if (ds.Tables[0].Rows[0]["WTDWName"] != null)
                {
                    this.WTDWName = ds.Tables[0].Rows[0]["WTDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZJLY"] != null)
                {
                    this.ZJLY = ds.Tables[0].Rows[0]["ZJLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Province"] != null)
                {
                    this.Province = ds.Tables[0].Rows[0]["Province"].ToString();
                }
                if (ds.Tables[0].Rows[0]["City"] != null)
                {
                    this.City = ds.Tables[0].Rows[0]["City"].ToString();
                }
                if (ds.Tables[0].Rows[0]["District"] != null)
                {
                    this.District = ds.Tables[0].Rows[0]["District"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null)
                {
                    this.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYLB"] != null)
                {
                    this.ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYLB"] != null)
                {
                    this.HYLB = ds.Tables[0].Rows[0]["HYLB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMGK"] != null)
                {
                    this.XMGK = ds.Tables[0].Rows[0]["XMGK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKFS"] != null)
                {
                    this.FKFS = ds.Tables[0].Rows[0]["FKFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProfitRate"] != null && ds.Tables[0].Rows[0]["ProfitRate"].ToString() != "")
                {
                    this.ProfitRate = decimal.Parse(ds.Tables[0].Rows[0]["ProfitRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMFXPG"] != null)
                {
                    this.XMFXPG = ds.Tables[0].Rows[0]["XMFXPG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFTYCJ"] != null)
                {
                    this.SFTYCJ = ds.Tables[0].Rows[0]["SFTYCJ"].ToString();
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
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
            }
        }
    }
}

