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
	/// 类ERPMingCeLiTuiXiu。
	/// </summary>
	[Serializable]
    public partial class ERPMingCeLiTuiXiu : ModelBase
    {
        public ERPMingCeLiTuiXiu()
        { }
        #region Model
        private int _id;
        private string _number;
        private string _xingming;
        private string _lituileibie;
        private DateTime? _lituixiushijian;
        private DateTime? _shebaojubanlituixiushijian;
        private string _xingbie;
        private string _shenfenzhenghao;
        private DateTime? _chushengnianyue;
        private string _nianlingceng;
        private string _minzu;
        private string _jiguan;
        private string _zuigaoxueli;
        private string _xuewei;
        private string _biyeyuanxiao;
        private DateTime? _biyeshijian;
        private DateTime? _canjiagongzuoshijian;
        private string _zhuanyejishuzhicheng;
        private DateTime? _qudezigeshijian;
        private string _jibie;
        private string _zhengzhimianmao;
        private DateTime? _rudangshijian;
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
        /// 离退类别
        /// </summary>
        public string LiTuiLeiBie
        {
            set { _lituileibie = value; }
            get { return _lituileibie; }
        }
        /// <summary>
        /// 离退休时间
        /// </summary>
        public DateTime? LiTuiXiuShiJian
        {
            set { _lituixiushijian = value; }
            get { return _lituixiushijian; }
        }
        /// <summary>
        /// 社保局办理退休时间
        /// </summary>
        public DateTime? SheBaoJuBanLiTuiXiuShiJian
        {
            set { _shebaojubanlituixiushijian = value; }
            get { return _shebaojubanlituixiushijian; }
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
        /// 民族
        /// </summary>
        public string MinZu
        {
            set { _minzu = value; }
            get { return _minzu; }
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
        /// 最高学历  
        /// </summary>
        public string ZuiGaoXueLi
        {
            set { _zuigaoxueli = value; }
            get { return _zuigaoxueli; }
        }
        /// <summary>
        /// 学位
        /// </summary>
        public string XueWei
        {
            set { _xuewei = value; }
            get { return _xuewei; }
        }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string BiYeYuanXiao
        {
            set { _biyeyuanxiao = value; }
            get { return _biyeyuanxiao; }
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
        /// 参加工作时间
        /// </summary>
        public DateTime? CanJiaGongZuoShiJian
        {
            set { _canjiagongzuoshijian = value; }
            get { return _canjiagongzuoshijian; }
        }
        /// <summary>
        /// 专业技术职称
        /// </summary>
        public string ZhuanYeJiShuZhiCheng
        {
            set { _zhuanyejishuzhicheng = value; }
            get { return _zhuanyejishuzhicheng; }
        }
        /// <summary>
        /// 取得资格时间
        /// </summary>
        public DateTime? QuDeZiGeShiJian
        {
            set { _qudezigeshijian = value; }
            get { return _qudezigeshijian; }
        }
        /// <summary>
        /// 退休时岗位级别
        /// </summary>
        public string JiBie
        {
            set { _jibie = value; }
            get { return _jibie; }
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
        /// 入党团时间
        /// </summary>
        public DateTime? RuDangShiJian
        {
            set { _rudangshijian = value; }
            get { return _rudangshijian; }
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
        public ERPMingCeLiTuiXiu(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPMingCeLiTuiXiu] ");
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
            strSql.Append("select count(1) from [ERPMingCeLiTuiXiu]");
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
            strSql.Append("insert into [ERPMingCeLiTuiXiu] (");
            strSql.Append("Number,XingMing,LiTuiLeiBie,LiTuiXiuShiJian,SheBaoJuBanLiTuiXiuShiJian,XingBie,ShenFenZhengHao,ChuShengNianYue,NianLingCeng,MinZu,JiGuan,ZuiGaoXueLi,XueWei,BiYeYuanXiao,BiYeShiJian,CanJiaGongZuoShiJian,ZhuanYeJiShuZhiCheng,QuDeZiGeShiJian,JiBie,ZhengZhiMianMao,RuDangShiJian,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@Number,@XingMing,@LiTuiLeiBie,@LiTuiXiuShiJian,@SheBaoJuBanLiTuiXiuShiJian,@XingBie,@ShenFenZhengHao,@ChuShengNianYue,@NianLingCeng,@MinZu,@JiGuan,@ZuiGaoXueLi,@XueWei,@BiYeYuanXiao,@BiYeShiJian,@CanJiaGongZuoShiJian,@ZhuanYeJiShuZhiCheng,@QuDeZiGeShiJian,@JiBie,@ZhengZhiMianMao,@RuDangShiJian,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@XingMing", SqlDbType.NVarChar,50),
                    new SqlParameter("@LiTuiLeiBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@LiTuiXiuShiJian", SqlDbType.DateTime),
                    new SqlParameter("@SheBaoJuBanLiTuiXiuShiJian", SqlDbType.DateTime),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,200),
                    new SqlParameter("@MinZu", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@QuDeZiGeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
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
            parameters[2].Value = LiTuiLeiBie;
            parameters[3].Value = LiTuiXiuShiJian;
            parameters[4].Value = SheBaoJuBanLiTuiXiuShiJian;
            parameters[5].Value = XingBie;
            parameters[6].Value = ShenFenZhengHao;
            parameters[7].Value = ChuShengNianYue;
            parameters[8].Value = NianLingCeng;
            parameters[9].Value = MinZu;
            parameters[10].Value = JiGuan;
            parameters[11].Value = ZuiGaoXueLi;
            parameters[12].Value = XueWei;
            parameters[13].Value = BiYeYuanXiao;
            parameters[14].Value = BiYeShiJian;
            parameters[15].Value = CanJiaGongZuoShiJian;
            parameters[16].Value = ZhuanYeJiShuZhiCheng;
            parameters[17].Value = QuDeZiGeShiJian;
            parameters[18].Value = JiBie;
            parameters[19].Value = ZhengZhiMianMao;
            parameters[20].Value = RuDangShiJian;
            parameters[21].Value = Description;
            parameters[22].Value = SortCode;
            parameters[23].Value = EnabledMark;
            parameters[24].Value = CreatorTime;
            parameters[25].Value = CreatorUser;
            parameters[26].Value = LastModifyTime;
            parameters[27].Value = LastModifyUser;
            parameters[28].Value = DeleteTime;
            parameters[29].Value = DeleteUser;
            parameters[30].Value = DeleteMark;

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
            strSql.Append("update [ERPMingCeLiTuiXiu] set ");
            strSql.Append("Number=@Number,");
            strSql.Append("XingMing=@XingMing,");
            strSql.Append("LiTuiLeiBie=@LiTuiLeiBie,");
            strSql.Append("LiTuiXiuShiJian=@LiTuiXiuShiJian,");
            strSql.Append("SheBaoJuBanLiTuiXiuShiJian=@SheBaoJuBanLiTuiXiuShiJian,");
            strSql.Append("XingBie=@XingBie,");
            strSql.Append("ShenFenZhengHao=@ShenFenZhengHao,");
            strSql.Append("ChuShengNianYue=@ChuShengNianYue,");
            strSql.Append("NianLingCeng=@NianLingCeng,");
            strSql.Append("MinZu=@MinZu,");
            strSql.Append("JiGuan=@JiGuan,");
            strSql.Append("ZuiGaoXueLi=@ZuiGaoXueLi,");
            strSql.Append("XueWei=@XueWei,");
            strSql.Append("BiYeYuanXiao=@BiYeYuanXiao,");
            strSql.Append("BiYeShiJian=@BiYeShiJian,");
            strSql.Append("CanJiaGongZuoShiJian=@CanJiaGongZuoShiJian,");
            strSql.Append("ZhuanYeJiShuZhiCheng=@ZhuanYeJiShuZhiCheng,");
            strSql.Append("QuDeZiGeShiJian=@QuDeZiGeShiJian,");
            strSql.Append("JiBie=@JiBie,");
            strSql.Append("ZhengZhiMianMao=@ZhengZhiMianMao,");
            strSql.Append("RuDangShiJian=@RuDangShiJian,");
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
                    new SqlParameter("@LiTuiLeiBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@LiTuiXiuShiJian", SqlDbType.DateTime),
                    new SqlParameter("@SheBaoJuBanLiTuiXiuShiJian", SqlDbType.DateTime),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,200),
                    new SqlParameter("@MinZu", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@QuDeZiGeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
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
            parameters[2].Value = LiTuiLeiBie;
            parameters[3].Value = LiTuiXiuShiJian;
            parameters[4].Value = SheBaoJuBanLiTuiXiuShiJian;
            parameters[5].Value = XingBie;
            parameters[6].Value = ShenFenZhengHao;
            parameters[7].Value = ChuShengNianYue;
            parameters[8].Value = NianLingCeng;
            parameters[9].Value = MinZu;
            parameters[10].Value = JiGuan;
            parameters[11].Value = ZuiGaoXueLi;
            parameters[12].Value = XueWei;
            parameters[13].Value = BiYeYuanXiao;
            parameters[14].Value = BiYeShiJian;
            parameters[15].Value = CanJiaGongZuoShiJian;
            parameters[16].Value = ZhuanYeJiShuZhiCheng;
            parameters[17].Value = QuDeZiGeShiJian;
            parameters[18].Value = JiBie;
            parameters[19].Value = ZhengZhiMianMao;
            parameters[20].Value = RuDangShiJian;
            parameters[21].Value = Description;
            parameters[22].Value = SortCode;
            parameters[23].Value = EnabledMark;
            parameters[24].Value = CreatorTime;
            parameters[25].Value = CreatorUser;
            parameters[26].Value = LastModifyTime;
            parameters[27].Value = LastModifyUser;
            parameters[28].Value = DeleteTime;
            parameters[29].Value = DeleteUser;
            parameters[30].Value = DeleteMark;
            parameters[31].Value = ID;

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
            strSql.Append("delete from [ERPMingCeLiTuiXiu] ");
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
            strSql.Append(" FROM [ERPMingCeLiTuiXiu] ");
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
                if (ds.Tables[0].Rows[0]["LiTuiLeiBie"] != null)
                {
                    this.LiTuiLeiBie = ds.Tables[0].Rows[0]["LiTuiLeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LiTuiXiuShiJian"] != null && ds.Tables[0].Rows[0]["LiTuiXiuShiJian"].ToString() != "")
                {
                    this.LiTuiXiuShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["LiTuiXiuShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SheBaoJuBanLiTuiXiuShiJian"] != null && ds.Tables[0].Rows[0]["SheBaoJuBanLiTuiXiuShiJian"].ToString() != "")
                {
                    this.SheBaoJuBanLiTuiXiuShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["SheBaoJuBanLiTuiXiuShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingBie"] != null)
                {
                    this.XingBie = ds.Tables[0].Rows[0]["XingBie"].ToString();
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
                if (ds.Tables[0].Rows[0]["MinZu"] != null)
                {
                    this.MinZu = ds.Tables[0].Rows[0]["MinZu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiGuan"] != null)
                {
                    this.JiGuan = ds.Tables[0].Rows[0]["JiGuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueLi"] != null)
                {
                    this.ZuiGaoXueLi = ds.Tables[0].Rows[0]["ZuiGaoXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueWei"] != null)
                {
                    this.XueWei = ds.Tables[0].Rows[0]["XueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeYuanXiao"] != null)
                {
                    this.BiYeYuanXiao = ds.Tables[0].Rows[0]["BiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeShiJian"] != null && ds.Tables[0].Rows[0]["BiYeShiJian"].ToString() != "")
                {
                    this.BiYeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["BiYeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"] != null && ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString() != "")
                {
                    this.CanJiaGongZuoShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhuanYeJiShuZhiCheng"] != null)
                {
                    this.ZhuanYeJiShuZhiCheng = ds.Tables[0].Rows[0]["ZhuanYeJiShuZhiCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QuDeZiGeShiJian"] != null && ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString() != "")
                {
                    this.QuDeZiGeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiBie"] != null)
                {
                    this.JiBie = ds.Tables[0].Rows[0]["JiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMao"] != null)
                {
                    this.ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RuDangShiJian"] != null && ds.Tables[0].Rows[0]["RuDangShiJian"].ToString() != "")
                {
                    this.RuDangShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["RuDangShiJian"].ToString());
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
            strSql.Append(" FROM [ERPMingCeLiTuiXiu] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPMingCeLiTuiXiu> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPMingCeLiTuiXiu>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeLiTuiXiu>(ds.Tables[0]);
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
            strSql.Append(@"select * from ERPMingCeLiTuiXiu");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}
