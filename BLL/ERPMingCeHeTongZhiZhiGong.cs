using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using ZWL.DBUtility;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPMingCeHeTongZhiZhiGong。
	/// </summary>
	[Serializable]
    public partial class ERPMingCeHeTongZhiZhiGong : ModelBase
    {
        public ERPMingCeHeTongZhiZhiGong()
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
        private string _gangweileibie;
        private string _jiguan;
        private string _bieyeyuanxiao;
        private string _zhuanye;
        private string _zuigaoxueli;
        private string _zuigaoxuewei;
        private DateTime? _biyeshijian;
        private string _zhengzhimianmao;
        private DateTime? _rudangshijian;
        private string _zhicheng;
        private DateTime? _zhichenghuodeshijian;
        private DateTime? _canbaoshijian;
        private string _congshigongzuo;
        private string _canbaodanwei;
        private DateTime? _hetongdaoqishijian;
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
        public override int ID
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
        /// 
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
        /// 岗位类别
        /// </summary>
        public string GangWeiLeiBie
        {
            set { _gangweileibie = value; }
            get { return _gangweileibie; }
        }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string JiGuan
        {
            set { _jiguan = value; }
            get { return _jiguan; }
        }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string BieYeYuanXiao
        {
            set { _bieyeyuanxiao = value; }
            get { return _bieyeyuanxiao; }
        }
        /// <summary>
        /// 所学专业
        /// </summary>
        public string ZhuanYe
        {
            set { _zhuanye = value; }
            get { return _zhuanye; }
        }
        /// <summary>
        /// 最高学历
        /// </summary>
        public string ZuiGaoXueLi
        {
            set { _zuigaoxueli = value; }
            get { return _zuigaoxueli; }
        }
        /// <summary>
        /// 最高学位
        /// </summary>
        public string ZuiGaoXueWei
        {
            set { _zuigaoxuewei = value; }
            get { return _zuigaoxuewei; }
        }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? BiYeShiJian
        {
            set { _biyeshijian = value; }
            get { return _biyeshijian; }
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string ZhengZhiMianMao
        {
            set { _zhengzhimianmao = value; }
            get { return _zhengzhimianmao; }
        }
        /// <summary>
        /// 入党时间
        /// </summary>
        public DateTime? RuDangShiJian
        {
            set { _rudangshijian = value; }
            get { return _rudangshijian; }
        }
        /// <summary>
        /// 职称
        /// </summary>
        public string ZhiCheng
        {
            set { _zhicheng = value; }
            get { return _zhicheng; }
        }
        /// <summary>
        /// 职称获得时间
        /// </summary>
        public DateTime? ZhiChengHuoDeShiJian
        {
            set { _zhichenghuodeshijian = value; }
            get { return _zhichenghuodeshijian; }
        }
        /// <summary>
        /// 参保时间
        /// </summary>
        public DateTime? CanBaoShiJian
        {
            set { _canbaoshijian = value; }
            get { return _canbaoshijian; }
        }
        /// <summary>
        /// 从事工作
        /// </summary>
        public string CongShiGongZuo
        {
            set { _congshigongzuo = value; }
            get { return _congshigongzuo; }
        }
        /// <summary>
        /// 参保单位
        /// </summary>
        public string CanBaoDanWei
        {
            set { _canbaodanwei = value; }
            get { return _canbaodanwei; }
        }
        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime? HeTongDaoQiShiJian
        {
            set { _hetongdaoqishijian = value; }
            get { return _hetongdaoqishijian; }
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
        public ERPMingCeHeTongZhiZhiGong(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPMingCeHeTongZhiZhiGong] ");
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
            strSql.Append("select count(1) from [ERPMingCeHeTongZhiZhiGong]");
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
            strSql.Append("insert into [ERPMingCeHeTongZhiZhiGong] (");
            strSql.Append("Number,XingMing,XingBie,BuMen,ShenFenZhengHao,ChuShengNianYue,NianLingCeng,GangWeiLeiBie,JiGuan,BieYeYuanXiao,ZhuanYe,ZuiGaoXueLi,ZuiGaoXueWei,BiYeShiJian,ZhengZhiMianMao,RuDangShiJian,ZhiCheng,ZhiChengHuoDeShiJian,CanBaoShiJian,CongShiGongZuo,CanBaoDanWei,HeTongDaoQiShiJian,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@Number,@XingMing,@XingBie,@BuMen,@ShenFenZhengHao,@ChuShengNianYue,@NianLingCeng,@GangWeiLeiBie,@JiGuan,@BieYeYuanXiao,@ZhuanYe,@ZuiGaoXueLi,@ZuiGaoXueWei,@BiYeShiJian,@ZhengZhiMianMao,@RuDangShiJian,@ZhiCheng,@ZhiChengHuoDeShiJian,@CanBaoShiJian,@CongShiGongZuo,@CanBaoDanWei,@HeTongDaoQiShiJian,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@XingMing", SqlDbType.NVarChar,50),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.VarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,50),
                    new SqlParameter("@GangWeiLeiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
                    new SqlParameter("@BieYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZuiGaoXueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhiChengHuoDeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CanBaoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CongShiGongZuo", SqlDbType.NVarChar,200),
                    new SqlParameter("@CanBaoDanWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@HeTongDaoQiShiJian", SqlDbType.DateTime),
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
            parameters[7].Value = GangWeiLeiBie;
            parameters[8].Value = JiGuan;
            parameters[9].Value = BieYeYuanXiao;
            parameters[10].Value = ZhuanYe;
            parameters[11].Value = ZuiGaoXueLi;
            parameters[12].Value = ZuiGaoXueWei;
            parameters[13].Value = BiYeShiJian;
            parameters[14].Value = ZhengZhiMianMao;
            parameters[15].Value = RuDangShiJian;
            parameters[16].Value = ZhiCheng;
            parameters[17].Value = ZhiChengHuoDeShiJian;
            parameters[18].Value = CanBaoShiJian;
            parameters[19].Value = CongShiGongZuo;
            parameters[20].Value = CanBaoDanWei;
            parameters[21].Value = HeTongDaoQiShiJian;
            parameters[22].Value = Description;
            parameters[23].Value = SortCode;
            parameters[24].Value = EnabledMark;
            parameters[25].Value = CreatorTime;
            parameters[26].Value = CreatorUser;
            parameters[27].Value = LastModifyTime;
            parameters[28].Value = LastModifyUser;
            parameters[29].Value = DeleteTime;
            parameters[30].Value = DeleteUser;
            parameters[31].Value = DeleteMark;

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
            strSql.Append("update [ERPMingCeHeTongZhiZhiGong] set ");
            strSql.Append("Number=@Number,");
            strSql.Append("XingMing=@XingMing,");
            strSql.Append("XingBie=@XingBie,");
            strSql.Append("BuMen=@BuMen,");
            strSql.Append("ShenFenZhengHao=@ShenFenZhengHao,");
            strSql.Append("ChuShengNianYue=@ChuShengNianYue,");
            strSql.Append("NianLingCeng=@NianLingCeng,");
            strSql.Append("GangWeiLeiBie=@GangWeiLeiBie,");
            strSql.Append("JiGuan=@JiGuan,");
            strSql.Append("BieYeYuanXiao=@BieYeYuanXiao,");
            strSql.Append("ZhuanYe=@ZhuanYe,");
            strSql.Append("ZuiGaoXueLi=@ZuiGaoXueLi,");
            strSql.Append("ZuiGaoXueWei=@ZuiGaoXueWei,");
            strSql.Append("BiYeShiJian=@BiYeShiJian,");
            strSql.Append("ZhengZhiMianMao=@ZhengZhiMianMao,");
            strSql.Append("RuDangShiJian=@RuDangShiJian,");
            strSql.Append("ZhiCheng=@ZhiCheng,");
            strSql.Append("ZhiChengHuoDeShiJian=@ZhiChengHuoDeShiJian,");
            strSql.Append("CanBaoShiJian=@CanBaoShiJian,");
            strSql.Append("CongShiGongZuo=@CongShiGongZuo,");
            strSql.Append("CanBaoDanWei=@CanBaoDanWei,");
            strSql.Append("HeTongDaoQiShiJian=@HeTongDaoQiShiJian,");
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
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.VarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,50),
                    new SqlParameter("@GangWeiLeiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
                    new SqlParameter("@BieYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZuiGaoXueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhiChengHuoDeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CanBaoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CongShiGongZuo", SqlDbType.NVarChar,200),
                    new SqlParameter("@CanBaoDanWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@HeTongDaoQiShiJian", SqlDbType.DateTime),
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
            parameters[7].Value = GangWeiLeiBie;
            parameters[8].Value = JiGuan;
            parameters[9].Value = BieYeYuanXiao;
            parameters[10].Value = ZhuanYe;
            parameters[11].Value = ZuiGaoXueLi;
            parameters[12].Value = ZuiGaoXueWei;
            parameters[13].Value = BiYeShiJian;
            parameters[14].Value = ZhengZhiMianMao;
            parameters[15].Value = RuDangShiJian;
            parameters[16].Value = ZhiCheng;
            parameters[17].Value = ZhiChengHuoDeShiJian;
            parameters[18].Value = CanBaoShiJian;
            parameters[19].Value = CongShiGongZuo;
            parameters[20].Value = CanBaoDanWei;
            parameters[21].Value = HeTongDaoQiShiJian;
            parameters[22].Value = Description;
            parameters[23].Value = SortCode;
            parameters[24].Value = EnabledMark;
            parameters[25].Value = CreatorTime;
            parameters[26].Value = CreatorUser;
            parameters[27].Value = LastModifyTime;
            parameters[28].Value = LastModifyUser;
            parameters[29].Value = DeleteTime;
            parameters[30].Value = DeleteUser;
            parameters[31].Value = DeleteMark;
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
            strSql.Append("delete from [ERPMingCeHeTongZhiZhiGong] ");
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
            strSql.Append(" FROM [ERPMingCeHeTongZhiZhiGong] ");
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
                if (ds.Tables[0].Rows[0]["GangWeiLeiBie"] != null)
                {
                    this.GangWeiLeiBie = ds.Tables[0].Rows[0]["GangWeiLeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiGuan"] != null)
                {
                    this.JiGuan = ds.Tables[0].Rows[0]["JiGuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BieYeYuanXiao"] != null)
                {
                    this.BieYeYuanXiao = ds.Tables[0].Rows[0]["BieYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanYe"] != null)
                {
                    this.ZhuanYe = ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueLi"] != null)
                {
                    this.ZuiGaoXueLi = ds.Tables[0].Rows[0]["ZuiGaoXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueWei"] != null)
                {
                    this.ZuiGaoXueWei = ds.Tables[0].Rows[0]["ZuiGaoXueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeShiJian"] != null && ds.Tables[0].Rows[0]["BiYeShiJian"].ToString() != "")
                {
                    this.BiYeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["BiYeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMao"] != null)
                {
                    this.ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RuDangShiJian"] != null && ds.Tables[0].Rows[0]["RuDangShiJian"].ToString() != "")
                {
                    this.RuDangShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["RuDangShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiCheng"] != null)
                {
                    this.ZhiCheng = ds.Tables[0].Rows[0]["ZhiCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiChengHuoDeShiJian"] != null && ds.Tables[0].Rows[0]["ZhiChengHuoDeShiJian"].ToString() != "")
                {
                    this.ZhiChengHuoDeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["ZhiChengHuoDeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CanBaoShiJian"] != null && ds.Tables[0].Rows[0]["CanBaoShiJian"].ToString() != "")
                {
                    this.CanBaoShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["CanBaoShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CongShiGongZuo"] != null)
                {
                    this.CongShiGongZuo = ds.Tables[0].Rows[0]["CongShiGongZuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanBaoDanWei"] != null)
                {
                    this.CanBaoDanWei = ds.Tables[0].Rows[0]["CanBaoDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeTongDaoQiShiJian"] != null && ds.Tables[0].Rows[0]["HeTongDaoQiShiJian"].ToString() != "")
                {
                    this.HeTongDaoQiShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["HeTongDaoQiShiJian"].ToString());
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
            strSql.Append(" FROM [ERPMingCeHeTongZhiZhiGong] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPMingCeHeTongZhiZhiGong> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPMingCeHeTongZhiZhiGong>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeHeTongZhiZhiGong>(ds.Tables[0]);
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
            strSql.Append(@"select * from ERPMingCeHeTongZhiZhiGong");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}
