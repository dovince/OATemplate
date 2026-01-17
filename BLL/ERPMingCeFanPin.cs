using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.Common;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPMingCeFanPin。
	/// </summary>
	[Serializable]
    public partial class ERPMingCeFanPin
    {
        public ERPMingCeFanPin()
        { }
        #region Model
        private int _id;
        private string _number;
        private string _xingming;
        private string _xingbie;
        private string _bumen;
        private string _shenfenzhenghao;
        private DateTime? _chushengnianyue;
        private string _nianlingceng;
        private string _fanpingangwei;
        private string _description;
        private int? _sortcode;
        private int? _enabledmark;
        private DateTime _creatortime;
        private string _creatoruser;
        private DateTime? _lastmodifytime;
        private string _lastmodifyuser;
        private DateTime? _deletetime;
        private string _deleteuser;
        private int? _deletemark;
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
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing
        {
            set { _xingming = value; }
            get { return _xingming; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string XingBie
        {
            set { _xingbie = value; }
            get { return _xingbie; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string BuMen
        {
            set { _bumen = value; }
            get { return _bumen; }
        }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string ShenFenZhengHao
        {
            set { _shenfenzhenghao = value; }
            get { return _shenfenzhenghao; }
        }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? ChuShengNianYue
        {
            set { _chushengnianyue = value; }
            get { return _chushengnianyue; }
        }
        /// <summary>
        /// 年龄层
        /// </summary>
        public string NianLingCeng
        {
            set { _nianlingceng = value; }
            get { return _nianlingceng; }
        }
        /// <summary>
        /// 返聘岗位
        /// </summary>
        public string FanPinGangWei
        {
            set { _fanpingangwei = value; }
            get { return _fanpingangwei; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode
        {
            set { _sortcode = value; }
            get { return _sortcode; }
        }
        /// <summary>
        /// 启用
        /// </summary>
        public int? EnabledMark
        {
            set { _enabledmark = value; }
            get { return _enabledmark; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime
        {
            set { _creatortime = value; }
            get { return _creatortime; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUser
        {
            set { _creatoruser = value; }
            get { return _creatoruser; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifyUser
        {
            set { _lastmodifyuser = value; }
            get { return _lastmodifyuser; }
        }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime
        {
            set { _deletetime = value; }
            get { return _deletetime; }
        }
        /// <summary>
        /// 删除人
        /// </summary>
        public string DeleteUser
        {
            set { _deleteuser = value; }
            get { return _deleteuser; }
        }
        /// <summary>
        /// 删除标志
        /// </summary>
        public int? DeleteMark
        {
            set { _deletemark = value; }
            get { return _deletemark; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPMingCeFanPin(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPMingCeFanPin] ");
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
            strSql.Append("select count(1) from [ERPMingCeFanPin]");
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
            strSql.Append("insert into [ERPMingCeFanPin] (");
            strSql.Append("Number,XingMing,XingBie,BuMen,ShenFenZhengHao,ChuShengNianYue,NianLingCeng,FanPinGangWei,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@Number,@XingMing,@XingBie,@BuMen,@ShenFenZhengHao,@ChuShengNianYue,@NianLingCeng,@FanPinGangWei,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@XingMing", SqlDbType.NVarChar,50),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,200),
                    new SqlParameter("@FanPinGangWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@Description", SqlDbType.NVarChar,50),
                    new SqlParameter("@SortCode", SqlDbType.Int,4),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4)};
            parameters[0].Value = Number;
            parameters[1].Value = XingMing;
            parameters[2].Value = XingBie;
            parameters[3].Value = BuMen;
            parameters[4].Value = ShenFenZhengHao;
            parameters[5].Value = ChuShengNianYue;
            parameters[6].Value = NianLingCeng;
            parameters[7].Value = FanPinGangWei;
            parameters[8].Value = Description;
            parameters[9].Value = SortCode;
            parameters[10].Value = EnabledMark;
            parameters[11].Value = CreatorTime;
            parameters[12].Value = CreatorUser;
            parameters[13].Value = LastModifyTime;
            parameters[14].Value = LastModifyUser;
            parameters[15].Value = DeleteTime;
            parameters[16].Value = DeleteUser;
            parameters[17].Value = DeleteMark;

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
            strSql.Append("update [ERPMingCeFanPin] set ");
            strSql.Append("Number=@Number,");
            strSql.Append("XingMing=@XingMing,");
            strSql.Append("XingBie=@XingBie,");
            strSql.Append("BuMen=@BuMen,");
            strSql.Append("ShenFenZhengHao=@ShenFenZhengHao,");
            strSql.Append("ChuShengNianYue=@ChuShengNianYue,");
            strSql.Append("NianLingCeng=@NianLingCeng,");
            strSql.Append("FanPinGangWei=@FanPinGangWei,");
            strSql.Append("Description=@Description,");
            strSql.Append("SortCode=@SortCode,");
            strSql.Append("EnabledMark=@EnabledMark,");
            strSql.Append("CreatorTime=@CreatorTime,");
            strSql.Append("CreatorUser=@CreatorUser,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyUser=@LastModifyUser,");
            strSql.Append("DeleteTime=@DeleteTime,");
            strSql.Append("DeleteUser=@DeleteUser,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@XingMing", SqlDbType.NVarChar,50),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,200),
                    new SqlParameter("@FanPinGangWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@Description", SqlDbType.NVarChar,50),
                    new SqlParameter("@SortCode", SqlDbType.Int,4),
                    new SqlParameter("@EnabledMark", SqlDbType.Int,4),
                    new SqlParameter("@CreatorTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeleteMark", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = Number;
            parameters[1].Value = XingMing;
            parameters[2].Value = XingBie;
            parameters[3].Value = BuMen;
            parameters[4].Value = ShenFenZhengHao;
            parameters[5].Value = ChuShengNianYue;
            parameters[6].Value = NianLingCeng;
            parameters[7].Value = FanPinGangWei;
            parameters[8].Value = Description;
            parameters[9].Value = SortCode;
            parameters[10].Value = EnabledMark;
            parameters[11].Value = CreatorTime;
            parameters[12].Value = CreatorUser;
            parameters[13].Value = LastModifyTime;
            parameters[14].Value = LastModifyUser;
            parameters[15].Value = DeleteTime;
            parameters[16].Value = DeleteUser;
            parameters[17].Value = DeleteMark;
            parameters[18].Value = ID;

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
            strSql.Append("delete from [ERPMingCeFanPin] ");
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
            strSql.Append(" FROM [ERPMingCeFanPin] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingMing"] != null)
                {
                    this.XingMing = ds.Tables[0].Rows[0]["XingMing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingBie"] != null)
                {
                    this.XingBie = ds.Tables[0].Rows[0]["XingBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMen"] != null)
                {
                    this.BuMen = ds.Tables[0].Rows[0]["BuMen"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShenFenZhengHao"] != null)
                {
                    this.ShenFenZhengHao = ds.Tables[0].Rows[0]["ShenFenZhengHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuShengNianYue"] != null && ds.Tables[0].Rows[0]["ChuShengNianYue"].ToString() != "")
                {
                    this.ChuShengNianYue = DateTime.Parse(ds.Tables[0].Rows[0]["ChuShengNianYue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NianLingCeng"] != null)
                {
                    this.NianLingCeng = ds.Tables[0].Rows[0]["NianLingCeng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FanPinGangWei"] != null)
                {
                    this.FanPinGangWei = ds.Tables[0].Rows[0]["FanPinGangWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    this.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SortCode"] != null && ds.Tables[0].Rows[0]["SortCode"].ToString() != "")
                {
                    this.SortCode = int.Parse(ds.Tables[0].Rows[0]["SortCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnabledMark"] != null && ds.Tables[0].Rows[0]["EnabledMark"].ToString() != "")
                {
                    this.EnabledMark = int.Parse(ds.Tables[0].Rows[0]["EnabledMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorTime"] != null && ds.Tables[0].Rows[0]["CreatorTime"].ToString() != "")
                {
                    this.CreatorTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatorTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorUser"] != null)
                {
                    this.CreatorUser = ds.Tables[0].Rows[0]["CreatorUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastModifyTime"] != null && ds.Tables[0].Rows[0]["LastModifyTime"].ToString() != "")
                {
                    this.LastModifyTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifyUser"] != null)
                {
                    this.LastModifyUser = ds.Tables[0].Rows[0]["LastModifyUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteTime"] != null && ds.Tables[0].Rows[0]["DeleteTime"].ToString() != "")
                {
                    this.DeleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["DeleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteUser"] != null)
                {
                    this.DeleteUser = ds.Tables[0].Rows[0]["DeleteUser"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"] != null && ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    this.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
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
            strSql.Append(" FROM [ERPMingCeFanPin] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPMingCeFanPin> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPMingCeFanPin>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeFanPin>(ds.Tables[0]);
            }
            return result;
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from ERPMingCeFanPin");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}
