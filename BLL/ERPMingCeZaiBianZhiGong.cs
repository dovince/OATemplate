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
	/// 类ERPMingCeZaiBianZhiGong。
	/// </summary>
	[Serializable]
    public partial class ERPMingCeZaiBianZhiGong : ModelBase
    {
        public ERPMingCeZaiBianZhiGong()
        { }
        #region Model
        private int _id;
        private string _number;
        private string _xingming;
        private string _zhiwu;
        private string _jibie;
        private string _bumen;
        private string _xingbie;
        private string _shenfenzhenghao;
        private DateTime? _chushengnianyue;
        private string _nianlingceng;
        private string _minzu;
        private string _jiguan;
        private string _xueli;
        private string _xuewei;
        private string _biyeyuanxiao;
        private string _zhuanye;
        private DateTime? _biyeshijian;
        private string _zuigaoxueli;
        private string _xuewei2;
        private string _biyeyuanxiao2;
        private string _zhuanye2;
        private DateTime? _biyeshijian2;
        private DateTime? _canjiagongzuoshijian;
        private string _zhuanyejishuzhicheng;
        private string _zhichengjibie;
        private string _gangweipinyongjibie;
        private DateTime? _qudezigeshijian;
        private DateTime? _gangweipinyongshijian;
        private string _zhuceleizigezhengshu;
        private string _zhengzhimianmao;
        private DateTime? _rudangshijian;
        private string _xianrenzhishijian;
        private string _rentongzhijishijian;
        private string _gerenshenfen;
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
        /// GUID
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
        /// 部门及职务
        /// </summary>
        public string ZhiWu
        {
            set { _zhiwu = value; }
            get { return _zhiwu; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public string JiBie
        {
            set { _jibie = value; }
            get { return _jibie; }
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
        /// 第一学历
        /// </summary>
        public string XueLi
        {
            set { _xueli = value; }
            get { return _xueli; }
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
        /// 所学专业
        /// </summary>
        public string ZhuanYe
        {
            set { _zhuanye = value; }
            get { return _zhuanye; }
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
        public string XueWei2
        {
            set { _xuewei2 = value; }
            get { return _xuewei2; }
        }
        /// <summary>
        /// 最高学历毕业院校
        /// </summary>
        public string BiYeYuanXiao2
        {
            set { _biyeyuanxiao2 = value; }
            get { return _biyeyuanxiao2; }
        }
        /// <summary>
        /// 最高学历所学专业
        /// </summary>
        public string ZhuanYe2
        {
            set { _zhuanye2 = value; }
            get { return _zhuanye2; }
        }
        /// <summary>
        /// 最高学历毕业时间
        /// </summary>
        public DateTime? BiYeShiJian2
        {
            set { _biyeshijian2 = value; }
            get { return _biyeshijian2; }
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
        /// 职称级别
        /// </summary>
        public string ZhiChengJiBie
        {
            set { _zhichengjibie = value; }
            get { return _zhichengjibie; }
        }
        /// <summary>
        /// 岗位聘用级别
        /// </summary>
        public string GangWeiPinYongJiBie
        {
            set { _gangweipinyongjibie = value; }
            get { return _gangweipinyongjibie; }
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
        /// 岗位聘用时间
        /// </summary>
        public DateTime? GangWeiPinYongShiJian
        {
            set { _gangweipinyongshijian = value; }
            get { return _gangweipinyongshijian; }
        }
        /// <summary>
        /// 注册类资格证书
        /// </summary>
        public string ZhuCeLeiZiGeZhengShu
        {
            set { _zhuceleizigezhengshu = value; }
            get { return _zhuceleizigezhengshu; }
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
        /// 任现职时间
        /// </summary>
        public string XianRenZhiShiJian
        {
            set { _xianrenzhishijian = value; }
            get { return _xianrenzhishijian; }
        }
        /// <summary>
        /// 任同职级时间
        /// </summary>
        public string RenTongZhiJiShiJian
        {
            set { _rentongzhijishijian = value; }
            get { return _rentongzhijishijian; }
        }
        /// <summary>
        /// 个人身份
        /// </summary>
        public string GeRenShenFen
        {
            set { _gerenshenfen = value; }
            get { return _gerenshenfen; }
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
        public ERPMingCeZaiBianZhiGong(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPMingCeZaiBianZhiGong] ");
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
            strSql.Append("select count(1) from [ERPMingCeZaiBianZhiGong]");
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
            strSql.Append("insert into [ERPMingCeZaiBianZhiGong] (");
            strSql.Append("Number,XingMing,ZhiWu,JiBie,BuMen,XingBie,ShenFenZhengHao,ChuShengNianYue,NianLingCeng,MinZu,JiGuan,XueLi,XueWei,BiYeYuanXiao,ZhuanYe,BiYeShiJian,ZuiGaoXueLi,XueWei2,BiYeYuanXiao2,ZhuanYe2,BiYeShiJian2,CanJiaGongZuoShiJian,ZhuanYeJiShuZhiCheng,ZhiChengJiBie,GangWeiPinYongJiBie,QuDeZiGeShiJian,GangWeiPinYongShiJian,ZhuCeLeiZiGeZhengShu,ZhengZhiMianMao,RuDangShiJian,XianRenZhiShiJian,RenTongZhiJiShiJian,GeRenShenFen,Description,SortCode,EnabledMark,CreatorTime,CreatorUser,LastModifyTime,LastModifyUser,DeleteTime,DeleteUser,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@Number,@XingMing,@ZhiWu,@JiBie,@BuMen,@XingBie,@ShenFenZhengHao,@ChuShengNianYue,@NianLingCeng,@MinZu,@JiGuan,@XueLi,@XueWei,@BiYeYuanXiao,@ZhuanYe,@BiYeShiJian,@ZuiGaoXueLi,@XueWei2,@BiYeYuanXiao2,@ZhuanYe2,@BiYeShiJian2,@CanJiaGongZuoShiJian,@ZhuanYeJiShuZhiCheng,@ZhiChengJiBie,@GangWeiPinYongJiBie,@QuDeZiGeShiJian,@GangWeiPinYongShiJian,@ZhuCeLeiZiGeZhengShu,@ZhengZhiMianMao,@RuDangShiJian,@XianRenZhiShiJian,@RenTongZhiJiShiJian,@GeRenShenFen,@Description,@SortCode,@EnabledMark,@CreatorTime,@CreatorUser,@LastModifyTime,@LastModifyUser,@DeleteTime,@DeleteUser,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@XingMing", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhiWu", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,50),
                    new SqlParameter("@MinZu", SqlDbType.NVarChar,50),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei2", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao2", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe2", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian2", SqlDbType.DateTime),
                    new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhiChengJiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@GangWeiPinYongJiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@QuDeZiGeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@GangWeiPinYongShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuCeLeiZiGeZhengShu", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
                    new SqlParameter("@XianRenZhiShiJian", SqlDbType.NVarChar,200),
                    new SqlParameter("@RenTongZhiJiShiJian", SqlDbType.NVarChar,50),
                    new SqlParameter("@GeRenShenFen", SqlDbType.NVarChar,50),
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
            parameters[2].Value = ZhiWu;
            parameters[3].Value = JiBie;
            parameters[4].Value = BuMen;
            parameters[5].Value = XingBie;
            parameters[6].Value = ShenFenZhengHao;
            parameters[7].Value = ChuShengNianYue;
            parameters[8].Value = NianLingCeng;
            parameters[9].Value = MinZu;
            parameters[10].Value = JiGuan;
            parameters[11].Value = XueLi;
            parameters[12].Value = XueWei;
            parameters[13].Value = BiYeYuanXiao;
            parameters[14].Value = ZhuanYe;
            parameters[15].Value = BiYeShiJian;
            parameters[16].Value = ZuiGaoXueLi;
            parameters[17].Value = XueWei2;
            parameters[18].Value = BiYeYuanXiao2;
            parameters[19].Value = ZhuanYe2;
            parameters[20].Value = BiYeShiJian2;
            parameters[21].Value = CanJiaGongZuoShiJian;
            parameters[22].Value = ZhuanYeJiShuZhiCheng;
            parameters[23].Value = ZhiChengJiBie;
            parameters[24].Value = GangWeiPinYongJiBie;
            parameters[25].Value = QuDeZiGeShiJian;
            parameters[26].Value = GangWeiPinYongShiJian;
            parameters[27].Value = ZhuCeLeiZiGeZhengShu;
            parameters[28].Value = ZhengZhiMianMao;
            parameters[29].Value = RuDangShiJian;
            parameters[30].Value = XianRenZhiShiJian;
            parameters[31].Value = RenTongZhiJiShiJian;
            parameters[32].Value = GeRenShenFen;
            parameters[33].Value = Description;
            parameters[34].Value = SortCode;
            parameters[35].Value = EnabledMark;
            parameters[36].Value = CreatorTime;
            parameters[37].Value = CreatorUser;
            parameters[38].Value = LastModifyTime;
            parameters[39].Value = LastModifyUser;
            parameters[40].Value = DeleteTime;
            parameters[41].Value = DeleteUser;
            parameters[42].Value = DeleteMark;

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
            strSql.Append("update [ERPMingCeZaiBianZhiGong] set ");
            strSql.Append("Number=@Number,");
            strSql.Append("XingMing=@XingMing,");
            strSql.Append("ZhiWu=@ZhiWu,");
            strSql.Append("JiBie=@JiBie,");
            strSql.Append("BuMen=@BuMen,");
            strSql.Append("XingBie=@XingBie,");
            strSql.Append("ShenFenZhengHao=@ShenFenZhengHao,");
            strSql.Append("ChuShengNianYue=@ChuShengNianYue,");
            strSql.Append("NianLingCeng=@NianLingCeng,");
            strSql.Append("MinZu=@MinZu,");
            strSql.Append("JiGuan=@JiGuan,");
            strSql.Append("XueLi=@XueLi,");
            strSql.Append("XueWei=@XueWei,");
            strSql.Append("BiYeYuanXiao=@BiYeYuanXiao,");
            strSql.Append("ZhuanYe=@ZhuanYe,");
            strSql.Append("BiYeShiJian=@BiYeShiJian,");
            strSql.Append("ZuiGaoXueLi=@ZuiGaoXueLi,");
            strSql.Append("XueWei2=@XueWei2,");
            strSql.Append("BiYeYuanXiao2=@BiYeYuanXiao2,");
            strSql.Append("ZhuanYe2=@ZhuanYe2,");
            strSql.Append("BiYeShiJian2=@BiYeShiJian2,");
            strSql.Append("CanJiaGongZuoShiJian=@CanJiaGongZuoShiJian,");
            strSql.Append("ZhuanYeJiShuZhiCheng=@ZhuanYeJiShuZhiCheng,");
            strSql.Append("ZhiChengJiBie=@ZhiChengJiBie,");
            strSql.Append("GangWeiPinYongJiBie=@GangWeiPinYongJiBie,");
            strSql.Append("QuDeZiGeShiJian=@QuDeZiGeShiJian,");
            strSql.Append("GangWeiPinYongShiJian=@GangWeiPinYongShiJian,");
            strSql.Append("ZhuCeLeiZiGeZhengShu=@ZhuCeLeiZiGeZhengShu,");
            strSql.Append("ZhengZhiMianMao=@ZhengZhiMianMao,");
            strSql.Append("RuDangShiJian=@RuDangShiJian,");
            strSql.Append("XianRenZhiShiJian=@XianRenZhiShiJian,");
            strSql.Append("RenTongZhiJiShiJian=@RenTongZhiJiShiJian,");
            strSql.Append("GeRenShenFen=@GeRenShenFen,");
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
                    new SqlParameter("@ZhiWu", SqlDbType.NVarChar,200),
                    new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
                    new SqlParameter("@XingBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@ShenFenZhengHao", SqlDbType.NVarChar,50),
                    new SqlParameter("@ChuShengNianYue", SqlDbType.DateTime),
                    new SqlParameter("@NianLingCeng", SqlDbType.NVarChar,50),
                    new SqlParameter("@MinZu", SqlDbType.NVarChar,50),
                    new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
                    new SqlParameter("@XueWei2", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeYuanXiao2", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhuanYe2", SqlDbType.NVarChar,200),
                    new SqlParameter("@BiYeShiJian2", SqlDbType.DateTime),
                    new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZhiChengJiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@GangWeiPinYongJiBie", SqlDbType.NVarChar,200),
                    new SqlParameter("@QuDeZiGeShiJian", SqlDbType.DateTime),
                    new SqlParameter("@GangWeiPinYongShiJian", SqlDbType.DateTime),
                    new SqlParameter("@ZhuCeLeiZiGeZhengShu", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
                    new SqlParameter("@RuDangShiJian", SqlDbType.DateTime),
                    new SqlParameter("@XianRenZhiShiJian", SqlDbType.NVarChar,200),
                    new SqlParameter("@RenTongZhiJiShiJian", SqlDbType.NVarChar,50),
                    new SqlParameter("@GeRenShenFen", SqlDbType.NVarChar,50),
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
            parameters[2].Value = ZhiWu;
            parameters[3].Value = JiBie;
            parameters[4].Value = BuMen;
            parameters[5].Value = XingBie;
            parameters[6].Value = ShenFenZhengHao;
            parameters[7].Value = ChuShengNianYue;
            parameters[8].Value = NianLingCeng;
            parameters[9].Value = MinZu;
            parameters[10].Value = JiGuan;
            parameters[11].Value = XueLi;
            parameters[12].Value = XueWei;
            parameters[13].Value = BiYeYuanXiao;
            parameters[14].Value = ZhuanYe;
            parameters[15].Value = BiYeShiJian;
            parameters[16].Value = ZuiGaoXueLi;
            parameters[17].Value = XueWei2;
            parameters[18].Value = BiYeYuanXiao2;
            parameters[19].Value = ZhuanYe2;
            parameters[20].Value = BiYeShiJian2;
            parameters[21].Value = CanJiaGongZuoShiJian;
            parameters[22].Value = ZhuanYeJiShuZhiCheng;
            parameters[23].Value = ZhiChengJiBie;
            parameters[24].Value = GangWeiPinYongJiBie;
            parameters[25].Value = QuDeZiGeShiJian;
            parameters[26].Value = GangWeiPinYongShiJian;
            parameters[27].Value = ZhuCeLeiZiGeZhengShu;
            parameters[28].Value = ZhengZhiMianMao;
            parameters[29].Value = RuDangShiJian;
            parameters[30].Value = XianRenZhiShiJian;
            parameters[31].Value = RenTongZhiJiShiJian;
            parameters[32].Value = GeRenShenFen;
            parameters[33].Value = Description;
            parameters[34].Value = SortCode;
            parameters[35].Value = EnabledMark;
            parameters[36].Value = CreatorTime;
            parameters[37].Value = CreatorUser;
            parameters[38].Value = LastModifyTime;
            parameters[39].Value = LastModifyUser;
            parameters[40].Value = DeleteTime;
            parameters[41].Value = DeleteUser;
            parameters[42].Value = DeleteMark;
            parameters[43].Value = ID;

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
            strSql.Append("delete from [ERPMingCeZaiBianZhiGong] ");
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
            strSql.Append(" FROM [ERPMingCeZaiBianZhiGong] ");
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
                if (ds.Tables[0].Rows[0]["ZhiWu"] != null)
                {
                    this.ZhiWu = ds.Tables[0].Rows[0]["ZhiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiBie"] != null)
                {
                    this.JiBie = ds.Tables[0].Rows[0]["JiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMen"] != null)
                {
                    this.BuMen = ds.Tables[0].Rows[0]["BuMen"].ToString();
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
                if (ds.Tables[0].Rows[0]["XueLi"] != null)
                {
                    this.XueLi = ds.Tables[0].Rows[0]["XueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueWei"] != null)
                {
                    this.XueWei = ds.Tables[0].Rows[0]["XueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeYuanXiao"] != null)
                {
                    this.BiYeYuanXiao = ds.Tables[0].Rows[0]["BiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanYe"] != null)
                {
                    this.ZhuanYe = ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeShiJian"] != null && ds.Tables[0].Rows[0]["BiYeShiJian"].ToString() != "")
                {
                    this.BiYeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["BiYeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueLi"] != null)
                {
                    this.ZuiGaoXueLi = ds.Tables[0].Rows[0]["ZuiGaoXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueWei2"] != null)
                {
                    this.XueWei2 = ds.Tables[0].Rows[0]["XueWei2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeYuanXiao2"] != null)
                {
                    this.BiYeYuanXiao2 = ds.Tables[0].Rows[0]["BiYeYuanXiao2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanYe2"] != null)
                {
                    this.ZhuanYe2 = ds.Tables[0].Rows[0]["ZhuanYe2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BiYeShiJian2"] != null && ds.Tables[0].Rows[0]["BiYeShiJian2"].ToString() != "")
                {
                    this.BiYeShiJian2 = DateTime.Parse(ds.Tables[0].Rows[0]["BiYeShiJian2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"] != null && ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString() != "")
                {
                    this.CanJiaGongZuoShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhuanYeJiShuZhiCheng"] != null)
                {
                    this.ZhuanYeJiShuZhiCheng = ds.Tables[0].Rows[0]["ZhuanYeJiShuZhiCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiChengJiBie"] != null)
                {
                    this.ZhiChengJiBie = ds.Tables[0].Rows[0]["ZhiChengJiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GangWeiPinYongJiBie"] != null)
                {
                    this.GangWeiPinYongJiBie = ds.Tables[0].Rows[0]["GangWeiPinYongJiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QuDeZiGeShiJian"] != null && ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString() != "")
                {
                    this.QuDeZiGeShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GangWeiPinYongShiJian"] != null && ds.Tables[0].Rows[0]["GangWeiPinYongShiJian"].ToString() != "")
                {
                    this.GangWeiPinYongShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["GangWeiPinYongShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"] != null)
                {
                    this.ZhuCeLeiZiGeZhengShu = ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMao"] != null)
                {
                    this.ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RuDangShiJian"] != null && ds.Tables[0].Rows[0]["RuDangShiJian"].ToString() != "")
                {
                    this.RuDangShiJian = DateTime.Parse(ds.Tables[0].Rows[0]["RuDangShiJian"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XianRenZhiShiJian"] != null)
                {
                    this.XianRenZhiShiJian = ds.Tables[0].Rows[0]["XianRenZhiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"] != null)
                {
                    this.RenTongZhiJiShiJian = ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GeRenShenFen"] != null)
                {
                    this.GeRenShenFen = ds.Tables[0].Rows[0]["GeRenShenFen"].ToString();
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
            strSql.Append(" FROM [ERPMingCeZaiBianZhiGong] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPMingCeZaiBianZhiGong> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPMingCeZaiBianZhiGong>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPMingCeZaiBianZhiGong>(ds.Tables[0]);
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
            strSql.Append(@"select * from ERPMingCeZaiBianZhiGong");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
        #endregion  Method
    }
}
