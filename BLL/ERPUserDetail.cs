using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    public partial class ERPUserDetail
    {
        public ERPUserDetail()
        { }
        #region Model
        private int _id;
        private DateTime _dengjitime;
        private string _no = "";
        private string _leibie = "";
        private string _xingming = "";
        private string _bumenjizhiwu = "";
        private string _jibie = "";
        private string _bumen = "";
        private string _xingbie = "";
        private string _shenfenzhenghaoma = "";
        private string _chushengnianyue = "";
        private string _nianling = "";
        private string _jiguan = "";
        private string _diyixueli = "";
        private string _diyixuewei = "";
        private string _diyibiyeyuanxiao = "";
        private string _diyisuoxuezhuanye = "";
        private string _diyibiyeshijian = "";
        private string _zuigaoxueli = "";
        private string _zuigaoxuewei = "";
        private string _zuigaobiyeyuanxiao = "";
        private string _zuigaosuoxuezhuanye = "";
        private string _zuigaobiyeshijian = "";
        private string _canjiagongzuoshijian = "";
        private string _zhuanyejishuzhicheng = "";
        private string _zhichengjibie = "";
        private string _gangweipinyongjibie = "";
        private string _qudezigeshijian = "";
        private string _zhuceleizigezhengshu = "";
        private string _zhengzhimianmaorudangshijian = "";
        private string _renxianzhishijian = "";
        private string _rentongzhijishijian = "";
        private string _gerenshebaohao = "";
        private string _dangpai = "";
        private string _canbaoshijian = "";
        private string _congshigongzuo = "";
        private string _canbaodanwei = "";
        private string _minzu = "";
        private string _lituixiushijian = "";
        private string _tuixiushigangwei = "";
        private string _zhengzhimianmao = "";
        private string _rudangtuanshijian = "";
        private string _renzhiwushijian = "";
        private string _tuixiuzhiwu = "";
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
        public DateTime DengJiTime
        {
            set { _dengjitime = value; }
            get { return _dengjitime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string No
        {
            set { _no = value; }
            get { return _no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeiBie
        {
            set { _leibie = value; }
            get { return _leibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XingMing
        {
            set { _xingming = value; }
            get { return _xingming; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuMenJiZhiWu
        {
            set { _bumenjizhiwu = value; }
            get { return _bumenjizhiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiBie
        {
            set { _jibie = value; }
            get { return _jibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuMen
        {
            set { _bumen = value; }
            get { return _bumen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XingBie
        {
            set { _xingbie = value; }
            get { return _xingbie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShenFenZhengHaoMa
        {
            set { _shenfenzhenghaoma = value; }
            get { return _shenfenzhenghaoma; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuShengNianYue
        {
            set { _chushengnianyue = value; }
            get { return _chushengnianyue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NianLing
        {
            set { _nianling = value; }
            get { return _nianling; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiGuan
        {
            set { _jiguan = value; }
            get { return _jiguan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiYiXueLi
        {
            set { _diyixueli = value; }
            get { return _diyixueli; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiYiXueWei
        {
            set { _diyixuewei = value; }
            get { return _diyixuewei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiYiBiYeYuanXiao
        {
            set { _diyibiyeyuanxiao = value; }
            get { return _diyibiyeyuanxiao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiYiSuoXueZhuanYe
        {
            set { _diyisuoxuezhuanye = value; }
            get { return _diyisuoxuezhuanye; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiYiBiYeShiJian
        {
            set { _diyibiyeshijian = value; }
            get { return _diyibiyeshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZuiGaoXueLi
        {
            set { _zuigaoxueli = value; }
            get { return _zuigaoxueli; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZuiGaoXueWei
        {
            set { _zuigaoxuewei = value; }
            get { return _zuigaoxuewei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZuiGaoBiYeYuanXiao
        {
            set { _zuigaobiyeyuanxiao = value; }
            get { return _zuigaobiyeyuanxiao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZuiGaoSuoXueZhuanYe
        {
            set { _zuigaosuoxuezhuanye = value; }
            get { return _zuigaosuoxuezhuanye; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZuiGaoBiYeShiJian
        {
            set { _zuigaobiyeshijian = value; }
            get { return _zuigaobiyeshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanJiaGongZuoShiJian
        {
            set { _canjiagongzuoshijian = value; }
            get { return _canjiagongzuoshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuanYeJiShuZhiCheng
        {
            set { _zhuanyejishuzhicheng = value; }
            get { return _zhuanyejishuzhicheng; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhiChengJiBie
        {
            set { _zhichengjibie = value; }
            get { return _zhichengjibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GangWeiPinYongJiBie
        {
            set { _gangweipinyongjibie = value; }
            get { return _gangweipinyongjibie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuDeZiGeShiJian
        {
            set { _qudezigeshijian = value; }
            get { return _qudezigeshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuCeLeiZiGeZhengShu
        {
            set { _zhuceleizigezhengshu = value; }
            get { return _zhuceleizigezhengshu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhengZhiMianMaoRuDangShiJian
        {
            set { _zhengzhimianmaorudangshijian = value; }
            get { return _zhengzhimianmaorudangshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RenXianZhiShiJian
        {
            set { _renxianzhishijian = value; }
            get { return _renxianzhishijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RenTongZhiJiShiJian
        {
            set { _rentongzhijishijian = value; }
            get { return _rentongzhijishijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GeRenSheBaoHao
        {
            set { _gerenshebaohao = value; }
            get { return _gerenshebaohao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DangPai
        {
            set { _dangpai = value; }
            get { return _dangpai; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanBaoShiJian
        {
            set { _canbaoshijian = value; }
            get { return _canbaoshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CongShiGongZuo
        {
            set { _congshigongzuo = value; }
            get { return _congshigongzuo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanBaoDanWei
        {
            set { _canbaodanwei = value; }
            get { return _canbaodanwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MinZu
        {
            set { _minzu = value; }
            get { return _minzu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LiTuiXiuShiJian
        {
            set { _lituixiushijian = value; }
            get { return _lituixiushijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TuiXiuShiGangWei
        {
            set { _tuixiushigangwei = value; }
            get { return _tuixiushigangwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhengZhiMianMao
        {
            set { _zhengzhimianmao = value; }
            get { return _zhengzhimianmao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RuDangTuanShiJian
        {
            set { _rudangtuanshijian = value; }
            get { return _rudangtuanshijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RenZhiWuShiJian
        {
            set { _renzhiwushijian = value; }
            get { return _renzhiwushijian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TuiXiuZhiWu
        {
            set { _tuixiuzhiwu = value; }
            get { return _tuixiuzhiwu; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPUserDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DengJiTime,No,LeiBie,XingMing,BuMenJiZhiWu,JiBie,BuMen,XingBie,ShenFenZhengHaoMa,ChuShengNianYue,NianLing,JiGuan,DiYiXueLi,DiYiXueWei,DiYiBiYeYuanXiao,DiYiSuoXueZhuanYe,DiYiBiYeShiJian,ZuiGaoXueLi,ZuiGaoXueWei,ZuiGaoBiYeYuanXiao,ZuiGaoSuoXueZhuanYe,ZuiGaoBiYeShiJian,CanJiaGongZuoShiJian,ZhuanYeJiShuZhiCheng,ZhiChengJiBie,GangWeiPinYongJiBie,QuDeZiGeShiJian,ZhuCeLeiZiGeZhengShu,ZhengZhiMianMaoRuDangShiJian,RenXianZhiShiJian,RenTongZhiJiShiJian,GeRenSheBaoHao,DangPai,CanBaoShiJian,CongShiGongZuo,CanBaoDanWei,MinZu,LiTuiXiuShiJian,TuiXiuShiGangWei,ZhengZhiMianMao,RuDangTuanShiJian,RenZhiWuShiJian,TuiXiuZhiWu ");
            strSql.Append(" FROM [ERPUserDetail] ");
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
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingMing"] != null)
                {
                    this.XingMing = ds.Tables[0].Rows[0]["XingMing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMenJiZhiWu"] != null)
                {
                    this.BuMenJiZhiWu = ds.Tables[0].Rows[0]["BuMenJiZhiWu"].ToString();
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
                if (ds.Tables[0].Rows[0]["ShenFenZhengHaoMa"] != null)
                {
                    this.ShenFenZhengHaoMa = ds.Tables[0].Rows[0]["ShenFenZhengHaoMa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuShengNianYue"] != null)
                {
                    this.ChuShengNianYue = ds.Tables[0].Rows[0]["ChuShengNianYue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NianLing"] != null)
                {
                    this.NianLing = ds.Tables[0].Rows[0]["NianLing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiGuan"] != null)
                {
                    this.JiGuan = ds.Tables[0].Rows[0]["JiGuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiXueLi"] != null)
                {
                    this.DiYiXueLi = ds.Tables[0].Rows[0]["DiYiXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiXueWei"] != null)
                {
                    this.DiYiXueWei = ds.Tables[0].Rows[0]["DiYiXueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiBiYeYuanXiao"] != null)
                {
                    this.DiYiBiYeYuanXiao = ds.Tables[0].Rows[0]["DiYiBiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiSuoXueZhuanYe"] != null)
                {
                    this.DiYiSuoXueZhuanYe = ds.Tables[0].Rows[0]["DiYiSuoXueZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiBiYeShiJian"] != null)
                {
                    this.DiYiBiYeShiJian = ds.Tables[0].Rows[0]["DiYiBiYeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueLi"] != null)
                {
                    this.ZuiGaoXueLi = ds.Tables[0].Rows[0]["ZuiGaoXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueWei"] != null)
                {
                    this.ZuiGaoXueWei = ds.Tables[0].Rows[0]["ZuiGaoXueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoBiYeYuanXiao"] != null)
                {
                    this.ZuiGaoBiYeYuanXiao = ds.Tables[0].Rows[0]["ZuiGaoBiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoSuoXueZhuanYe"] != null)
                {
                    this.ZuiGaoSuoXueZhuanYe = ds.Tables[0].Rows[0]["ZuiGaoSuoXueZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoBiYeShiJian"] != null)
                {
                    this.ZuiGaoBiYeShiJian = ds.Tables[0].Rows[0]["ZuiGaoBiYeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"] != null)
                {
                    this.CanJiaGongZuoShiJian = ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString();
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
                if (ds.Tables[0].Rows[0]["QuDeZiGeShiJian"] != null)
                {
                    this.QuDeZiGeShiJian = ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"] != null)
                {
                    this.ZhuCeLeiZiGeZhengShu = ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMaoRuDangShiJian"] != null)
                {
                    this.ZhengZhiMianMaoRuDangShiJian = ds.Tables[0].Rows[0]["ZhengZhiMianMaoRuDangShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenXianZhiShiJian"] != null)
                {
                    this.RenXianZhiShiJian = ds.Tables[0].Rows[0]["RenXianZhiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"] != null)
                {
                    this.RenTongZhiJiShiJian = ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GeRenSheBaoHao"] != null)
                {
                    this.GeRenSheBaoHao = ds.Tables[0].Rows[0]["GeRenSheBaoHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DangPai"] != null)
                {
                    this.DangPai = ds.Tables[0].Rows[0]["DangPai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanBaoShiJian"] != null)
                {
                    this.CanBaoShiJian = ds.Tables[0].Rows[0]["CanBaoShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CongShiGongZuo"] != null)
                {
                    this.CongShiGongZuo = ds.Tables[0].Rows[0]["CongShiGongZuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanBaoDanWei"] != null)
                {
                    this.CanBaoDanWei = ds.Tables[0].Rows[0]["CanBaoDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZu"] != null)
                {
                    this.MinZu = ds.Tables[0].Rows[0]["MinZu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LiTuiXiuShiJian"] != null)
                {
                    this.LiTuiXiuShiJian = ds.Tables[0].Rows[0]["LiTuiXiuShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TuiXiuShiGangWei"] != null)
                {
                    this.TuiXiuShiGangWei = ds.Tables[0].Rows[0]["TuiXiuShiGangWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMao"] != null)
                {
                    this.ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RuDangTuanShiJian"] != null)
                {
                    this.RuDangTuanShiJian = ds.Tables[0].Rows[0]["RuDangTuanShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenZhiWuShiJian"] != null)
                {
                    this.RenZhiWuShiJian = ds.Tables[0].Rows[0]["RenZhiWuShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TuiXiuZhiWu"] != null)
                {
                    this.TuiXiuZhiWu = ds.Tables[0].Rows[0]["TuiXiuZhiWu"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPUserDetail]");
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
            strSql.Append("insert into [ERPUserDetail] (");
            strSql.Append("DengJiTime,No,LeiBie,XingMing,BuMenJiZhiWu,JiBie,BuMen,XingBie,ShenFenZhengHaoMa,ChuShengNianYue,NianLing,JiGuan,DiYiXueLi,DiYiXueWei,DiYiBiYeYuanXiao,DiYiSuoXueZhuanYe,DiYiBiYeShiJian,ZuiGaoXueLi,ZuiGaoXueWei,ZuiGaoBiYeYuanXiao,ZuiGaoSuoXueZhuanYe,ZuiGaoBiYeShiJian,CanJiaGongZuoShiJian,ZhuanYeJiShuZhiCheng,ZhiChengJiBie,GangWeiPinYongJiBie,QuDeZiGeShiJian,ZhuCeLeiZiGeZhengShu,ZhengZhiMianMaoRuDangShiJian,RenXianZhiShiJian,RenTongZhiJiShiJian,GeRenSheBaoHao,DangPai,CanBaoShiJian,CongShiGongZuo,CanBaoDanWei,MinZu,LiTuiXiuShiJian,TuiXiuShiGangWei,ZhengZhiMianMao,RuDangTuanShiJian,RenZhiWuShiJian,TuiXiuZhiWu)");
            strSql.Append(" values (");
            strSql.Append("@DengJiTime,@No,@LeiBie,@XingMing,@BuMenJiZhiWu,@JiBie,@BuMen,@XingBie,@ShenFenZhengHaoMa,@ChuShengNianYue,@NianLing,@JiGuan,@DiYiXueLi,@DiYiXueWei,@DiYiBiYeYuanXiao,@DiYiSuoXueZhuanYe,@DiYiBiYeShiJian,@ZuiGaoXueLi,@ZuiGaoXueWei,@ZuiGaoBiYeYuanXiao,@ZuiGaoSuoXueZhuanYe,@ZuiGaoBiYeShiJian,@CanJiaGongZuoShiJian,@ZhuanYeJiShuZhiCheng,@ZhiChengJiBie,@GangWeiPinYongJiBie,@QuDeZiGeShiJian,@ZhuCeLeiZiGeZhengShu,@ZhengZhiMianMaoRuDangShiJian,@RenXianZhiShiJian,@RenTongZhiJiShiJian,@GeRenSheBaoHao,@DangPai,@CanBaoShiJian,@CongShiGongZuo,@CanBaoDanWei,@MinZu,@LiTuiXiuShiJian,@TuiXiuShiGangWei,@ZhengZhiMianMao,@RuDangTuanShiJian,@RenZhiWuShiJian,@TuiXiuZhiWu)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
					new SqlParameter("@No", SqlDbType.NVarChar,200),
					new SqlParameter("@LeiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@XingMing", SqlDbType.NVarChar,200),
					new SqlParameter("@BuMenJiZhiWu", SqlDbType.NVarChar,200),
					new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
					new SqlParameter("@XingBie", SqlDbType.NVarChar,200),
					new SqlParameter("@ShenFenZhengHaoMa", SqlDbType.NVarChar,200),
					new SqlParameter("@ChuShengNianYue", SqlDbType.NVarChar,200),
					new SqlParameter("@NianLing", SqlDbType.NVarChar,200),
					new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiXueLi", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiXueWei", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiBiYeYuanXiao", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiSuoXueZhuanYe", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiBiYeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoXueWei", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoBiYeYuanXiao", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoSuoXueZhuanYe", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoBiYeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhiChengJiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@GangWeiPinYongJiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@QuDeZiGeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhuCeLeiZiGeZhengShu", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhengZhiMianMaoRuDangShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenXianZhiShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenTongZhiJiShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@GeRenSheBaoHao", SqlDbType.NVarChar,200),
					new SqlParameter("@DangPai", SqlDbType.NVarChar,200),
					new SqlParameter("@CanBaoShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@CongShiGongZuo", SqlDbType.NVarChar,200),
					new SqlParameter("@CanBaoDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@MinZu", SqlDbType.NVarChar,200),
					new SqlParameter("@LiTuiXiuShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@TuiXiuShiGangWei", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
					new SqlParameter("@RuDangTuanShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenZhiWuShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@TuiXiuZhiWu", SqlDbType.NVarChar,200)};
            parameters[0].Value = DengJiTime;
            parameters[1].Value = No;
            parameters[2].Value = LeiBie;
            parameters[3].Value = XingMing;
            parameters[4].Value = BuMenJiZhiWu;
            parameters[5].Value = JiBie;
            parameters[6].Value = BuMen;
            parameters[7].Value = XingBie;
            parameters[8].Value = ShenFenZhengHaoMa;
            parameters[9].Value = ChuShengNianYue;
            parameters[10].Value = NianLing;
            parameters[11].Value = JiGuan;
            parameters[12].Value = DiYiXueLi;
            parameters[13].Value = DiYiXueWei;
            parameters[14].Value = DiYiBiYeYuanXiao;
            parameters[15].Value = DiYiSuoXueZhuanYe;
            parameters[16].Value = DiYiBiYeShiJian;
            parameters[17].Value = ZuiGaoXueLi;
            parameters[18].Value = ZuiGaoXueWei;
            parameters[19].Value = ZuiGaoBiYeYuanXiao;
            parameters[20].Value = ZuiGaoSuoXueZhuanYe;
            parameters[21].Value = ZuiGaoBiYeShiJian;
            parameters[22].Value = CanJiaGongZuoShiJian;
            parameters[23].Value = ZhuanYeJiShuZhiCheng;
            parameters[24].Value = ZhiChengJiBie;
            parameters[25].Value = GangWeiPinYongJiBie;
            parameters[26].Value = QuDeZiGeShiJian;
            parameters[27].Value = ZhuCeLeiZiGeZhengShu;
            parameters[28].Value = ZhengZhiMianMaoRuDangShiJian;
            parameters[29].Value = RenXianZhiShiJian;
            parameters[30].Value = RenTongZhiJiShiJian;
            parameters[31].Value = GeRenSheBaoHao;
            parameters[32].Value = DangPai;
            parameters[33].Value = CanBaoShiJian;
            parameters[34].Value = CongShiGongZuo;
            parameters[35].Value = CanBaoDanWei;
            parameters[36].Value = MinZu;
            parameters[37].Value = LiTuiXiuShiJian;
            parameters[38].Value = TuiXiuShiGangWei;
            parameters[39].Value = ZhengZhiMianMao;
            parameters[40].Value = RuDangTuanShiJian;
            parameters[41].Value = RenZhiWuShiJian;
            parameters[42].Value = TuiXiuZhiWu;

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
            strSql.Append("update [ERPUserDetail] set ");
            strSql.Append("DengJiTime=@DengJiTime,");
            strSql.Append("No=@No,");
            strSql.Append("LeiBie=@LeiBie,");
            strSql.Append("XingMing=@XingMing,");
            strSql.Append("BuMenJiZhiWu=@BuMenJiZhiWu,");
            strSql.Append("JiBie=@JiBie,");
            strSql.Append("BuMen=@BuMen,");
            strSql.Append("XingBie=@XingBie,");
            strSql.Append("ShenFenZhengHaoMa=@ShenFenZhengHaoMa,");
            strSql.Append("ChuShengNianYue=@ChuShengNianYue,");
            strSql.Append("NianLing=@NianLing,");
            strSql.Append("JiGuan=@JiGuan,");
            strSql.Append("DiYiXueLi=@DiYiXueLi,");
            strSql.Append("DiYiXueWei=@DiYiXueWei,");
            strSql.Append("DiYiBiYeYuanXiao=@DiYiBiYeYuanXiao,");
            strSql.Append("DiYiSuoXueZhuanYe=@DiYiSuoXueZhuanYe,");
            strSql.Append("DiYiBiYeShiJian=@DiYiBiYeShiJian,");
            strSql.Append("ZuiGaoXueLi=@ZuiGaoXueLi,");
            strSql.Append("ZuiGaoXueWei=@ZuiGaoXueWei,");
            strSql.Append("ZuiGaoBiYeYuanXiao=@ZuiGaoBiYeYuanXiao,");
            strSql.Append("ZuiGaoSuoXueZhuanYe=@ZuiGaoSuoXueZhuanYe,");
            strSql.Append("ZuiGaoBiYeShiJian=@ZuiGaoBiYeShiJian,");
            strSql.Append("CanJiaGongZuoShiJian=@CanJiaGongZuoShiJian,");
            strSql.Append("ZhuanYeJiShuZhiCheng=@ZhuanYeJiShuZhiCheng,");
            strSql.Append("ZhiChengJiBie=@ZhiChengJiBie,");
            strSql.Append("GangWeiPinYongJiBie=@GangWeiPinYongJiBie,");
            strSql.Append("QuDeZiGeShiJian=@QuDeZiGeShiJian,");
            strSql.Append("ZhuCeLeiZiGeZhengShu=@ZhuCeLeiZiGeZhengShu,");
            strSql.Append("ZhengZhiMianMaoRuDangShiJian=@ZhengZhiMianMaoRuDangShiJian,");
            strSql.Append("RenXianZhiShiJian=@RenXianZhiShiJian,");
            strSql.Append("RenTongZhiJiShiJian=@RenTongZhiJiShiJian,");
            strSql.Append("GeRenSheBaoHao=@GeRenSheBaoHao,");
            strSql.Append("DangPai=@DangPai,");
            strSql.Append("CanBaoShiJian=@CanBaoShiJian,");
            strSql.Append("CongShiGongZuo=@CongShiGongZuo,");
            strSql.Append("CanBaoDanWei=@CanBaoDanWei,");
            strSql.Append("MinZu=@MinZu,");
            strSql.Append("LiTuiXiuShiJian=@LiTuiXiuShiJian,");
            strSql.Append("TuiXiuShiGangWei=@TuiXiuShiGangWei,");
            strSql.Append("ZhengZhiMianMao=@ZhengZhiMianMao,");
            strSql.Append("RuDangTuanShiJian=@RuDangTuanShiJian,");
            strSql.Append("RenZhiWuShiJian=@RenZhiWuShiJian,");
            strSql.Append("TuiXiuZhiWu=@TuiXiuZhiWu");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
					new SqlParameter("@No", SqlDbType.NVarChar,200),
					new SqlParameter("@LeiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@XingMing", SqlDbType.NVarChar,200),
					new SqlParameter("@BuMenJiZhiWu", SqlDbType.NVarChar,200),
					new SqlParameter("@JiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@BuMen", SqlDbType.NVarChar,200),
					new SqlParameter("@XingBie", SqlDbType.NVarChar,200),
					new SqlParameter("@ShenFenZhengHaoMa", SqlDbType.NVarChar,200),
					new SqlParameter("@ChuShengNianYue", SqlDbType.NVarChar,200),
					new SqlParameter("@NianLing", SqlDbType.NVarChar,200),
					new SqlParameter("@JiGuan", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiXueLi", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiXueWei", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiBiYeYuanXiao", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiSuoXueZhuanYe", SqlDbType.NVarChar,200),
					new SqlParameter("@DiYiBiYeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoXueLi", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoXueWei", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoBiYeYuanXiao", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoSuoXueZhuanYe", SqlDbType.NVarChar,200),
					new SqlParameter("@ZuiGaoBiYeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@CanJiaGongZuoShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhuanYeJiShuZhiCheng", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhiChengJiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@GangWeiPinYongJiBie", SqlDbType.NVarChar,200),
					new SqlParameter("@QuDeZiGeShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhuCeLeiZiGeZhengShu", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhengZhiMianMaoRuDangShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenXianZhiShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenTongZhiJiShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@GeRenSheBaoHao", SqlDbType.NVarChar,200),
					new SqlParameter("@DangPai", SqlDbType.NVarChar,200),
					new SqlParameter("@CanBaoShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@CongShiGongZuo", SqlDbType.NVarChar,200),
					new SqlParameter("@CanBaoDanWei", SqlDbType.NVarChar,200),
					new SqlParameter("@MinZu", SqlDbType.NVarChar,200),
					new SqlParameter("@LiTuiXiuShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@TuiXiuShiGangWei", SqlDbType.NVarChar,200),
					new SqlParameter("@ZhengZhiMianMao", SqlDbType.NVarChar,200),
					new SqlParameter("@RuDangTuanShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@RenZhiWuShiJian", SqlDbType.NVarChar,200),
					new SqlParameter("@TuiXiuZhiWu", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = DengJiTime;
            parameters[1].Value = No;
            parameters[2].Value = LeiBie;
            parameters[3].Value = XingMing;
            parameters[4].Value = BuMenJiZhiWu;
            parameters[5].Value = JiBie;
            parameters[6].Value = BuMen;
            parameters[7].Value = XingBie;
            parameters[8].Value = ShenFenZhengHaoMa;
            parameters[9].Value = ChuShengNianYue;
            parameters[10].Value = NianLing;
            parameters[11].Value = JiGuan;
            parameters[12].Value = DiYiXueLi;
            parameters[13].Value = DiYiXueWei;
            parameters[14].Value = DiYiBiYeYuanXiao;
            parameters[15].Value = DiYiSuoXueZhuanYe;
            parameters[16].Value = DiYiBiYeShiJian;
            parameters[17].Value = ZuiGaoXueLi;
            parameters[18].Value = ZuiGaoXueWei;
            parameters[19].Value = ZuiGaoBiYeYuanXiao;
            parameters[20].Value = ZuiGaoSuoXueZhuanYe;
            parameters[21].Value = ZuiGaoBiYeShiJian;
            parameters[22].Value = CanJiaGongZuoShiJian;
            parameters[23].Value = ZhuanYeJiShuZhiCheng;
            parameters[24].Value = ZhiChengJiBie;
            parameters[25].Value = GangWeiPinYongJiBie;
            parameters[26].Value = QuDeZiGeShiJian;
            parameters[27].Value = ZhuCeLeiZiGeZhengShu;
            parameters[28].Value = ZhengZhiMianMaoRuDangShiJian;
            parameters[29].Value = RenXianZhiShiJian;
            parameters[30].Value = RenTongZhiJiShiJian;
            parameters[31].Value = GeRenSheBaoHao;
            parameters[32].Value = DangPai;
            parameters[33].Value = CanBaoShiJian;
            parameters[34].Value = CongShiGongZuo;
            parameters[35].Value = CanBaoDanWei;
            parameters[36].Value = MinZu;
            parameters[37].Value = LiTuiXiuShiJian;
            parameters[38].Value = TuiXiuShiGangWei;
            parameters[39].Value = ZhengZhiMianMao;
            parameters[40].Value = RuDangTuanShiJian;
            parameters[41].Value = RenZhiWuShiJian;
            parameters[42].Value = TuiXiuZhiWu;
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
            strSql.Append("delete from [ERPUserDetail] ");
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
            strSql.Append("select ID,DengJiTime,No,LeiBie,XingMing,BuMenJiZhiWu,JiBie,BuMen,XingBie,ShenFenZhengHaoMa,ChuShengNianYue,NianLing,JiGuan,DiYiXueLi,DiYiXueWei,DiYiBiYeYuanXiao,DiYiSuoXueZhuanYe,DiYiBiYeShiJian,ZuiGaoXueLi,ZuiGaoXueWei,ZuiGaoBiYeYuanXiao,ZuiGaoSuoXueZhuanYe,ZuiGaoBiYeShiJian,CanJiaGongZuoShiJian,ZhuanYeJiShuZhiCheng,ZhiChengJiBie,GangWeiPinYongJiBie,QuDeZiGeShiJian,ZhuCeLeiZiGeZhengShu,ZhengZhiMianMaoRuDangShiJian,RenXianZhiShiJian,RenTongZhiJiShiJian,GeRenSheBaoHao,DangPai,CanBaoShiJian,CongShiGongZuo,CanBaoDanWei,MinZu,LiTuiXiuShiJian,TuiXiuShiGangWei,ZhengZhiMianMao,RuDangTuanShiJian,RenZhiWuShiJian,TuiXiuZhiWu ");
            strSql.Append(" FROM [ERPUserDetail] ");
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
                if (ds.Tables[0].Rows[0]["DengJiTime"] != null && ds.Tables[0].Rows[0]["DengJiTime"].ToString() != "")
                {
                    this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["No"] != null)
                {
                    this.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LeiBie"] != null)
                {
                    this.LeiBie = ds.Tables[0].Rows[0]["LeiBie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingMing"] != null)
                {
                    this.XingMing = ds.Tables[0].Rows[0]["XingMing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuMenJiZhiWu"] != null)
                {
                    this.BuMenJiZhiWu = ds.Tables[0].Rows[0]["BuMenJiZhiWu"].ToString();
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
                if (ds.Tables[0].Rows[0]["ShenFenZhengHaoMa"] != null)
                {
                    this.ShenFenZhengHaoMa = ds.Tables[0].Rows[0]["ShenFenZhengHaoMa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuShengNianYue"] != null)
                {
                    this.ChuShengNianYue = ds.Tables[0].Rows[0]["ChuShengNianYue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NianLing"] != null)
                {
                    this.NianLing = ds.Tables[0].Rows[0]["NianLing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiGuan"] != null)
                {
                    this.JiGuan = ds.Tables[0].Rows[0]["JiGuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiXueLi"] != null)
                {
                    this.DiYiXueLi = ds.Tables[0].Rows[0]["DiYiXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiXueWei"] != null)
                {
                    this.DiYiXueWei = ds.Tables[0].Rows[0]["DiYiXueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiBiYeYuanXiao"] != null)
                {
                    this.DiYiBiYeYuanXiao = ds.Tables[0].Rows[0]["DiYiBiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiSuoXueZhuanYe"] != null)
                {
                    this.DiYiSuoXueZhuanYe = ds.Tables[0].Rows[0]["DiYiSuoXueZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DiYiBiYeShiJian"] != null)
                {
                    this.DiYiBiYeShiJian = ds.Tables[0].Rows[0]["DiYiBiYeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueLi"] != null)
                {
                    this.ZuiGaoXueLi = ds.Tables[0].Rows[0]["ZuiGaoXueLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoXueWei"] != null)
                {
                    this.ZuiGaoXueWei = ds.Tables[0].Rows[0]["ZuiGaoXueWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoBiYeYuanXiao"] != null)
                {
                    this.ZuiGaoBiYeYuanXiao = ds.Tables[0].Rows[0]["ZuiGaoBiYeYuanXiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoSuoXueZhuanYe"] != null)
                {
                    this.ZuiGaoSuoXueZhuanYe = ds.Tables[0].Rows[0]["ZuiGaoSuoXueZhuanYe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuiGaoBiYeShiJian"] != null)
                {
                    this.ZuiGaoBiYeShiJian = ds.Tables[0].Rows[0]["ZuiGaoBiYeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"] != null)
                {
                    this.CanJiaGongZuoShiJian = ds.Tables[0].Rows[0]["CanJiaGongZuoShiJian"].ToString();
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
                if (ds.Tables[0].Rows[0]["QuDeZiGeShiJian"] != null)
                {
                    this.QuDeZiGeShiJian = ds.Tables[0].Rows[0]["QuDeZiGeShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"] != null)
                {
                    this.ZhuCeLeiZiGeZhengShu = ds.Tables[0].Rows[0]["ZhuCeLeiZiGeZhengShu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMaoRuDangShiJian"] != null)
                {
                    this.ZhengZhiMianMaoRuDangShiJian = ds.Tables[0].Rows[0]["ZhengZhiMianMaoRuDangShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenXianZhiShiJian"] != null)
                {
                    this.RenXianZhiShiJian = ds.Tables[0].Rows[0]["RenXianZhiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"] != null)
                {
                    this.RenTongZhiJiShiJian = ds.Tables[0].Rows[0]["RenTongZhiJiShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GeRenSheBaoHao"] != null)
                {
                    this.GeRenSheBaoHao = ds.Tables[0].Rows[0]["GeRenSheBaoHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DangPai"] != null)
                {
                    this.DangPai = ds.Tables[0].Rows[0]["DangPai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanBaoShiJian"] != null)
                {
                    this.CanBaoShiJian = ds.Tables[0].Rows[0]["CanBaoShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CongShiGongZuo"] != null)
                {
                    this.CongShiGongZuo = ds.Tables[0].Rows[0]["CongShiGongZuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanBaoDanWei"] != null)
                {
                    this.CanBaoDanWei = ds.Tables[0].Rows[0]["CanBaoDanWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZu"] != null)
                {
                    this.MinZu = ds.Tables[0].Rows[0]["MinZu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LiTuiXiuShiJian"] != null)
                {
                    this.LiTuiXiuShiJian = ds.Tables[0].Rows[0]["LiTuiXiuShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TuiXiuShiGangWei"] != null)
                {
                    this.TuiXiuShiGangWei = ds.Tables[0].Rows[0]["TuiXiuShiGangWei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhengZhiMianMao"] != null)
                {
                    this.ZhengZhiMianMao = ds.Tables[0].Rows[0]["ZhengZhiMianMao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RuDangTuanShiJian"] != null)
                {
                    this.RuDangTuanShiJian = ds.Tables[0].Rows[0]["RuDangTuanShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenZhiWuShiJian"] != null)
                {
                    this.RenZhiWuShiJian = ds.Tables[0].Rows[0]["RenZhiWuShiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TuiXiuZhiWu"] != null)
                {
                    this.TuiXiuZhiWu = ds.Tables[0].Rows[0]["TuiXiuZhiWu"].ToString();
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
            strSql.Append(" FROM [ERPUserDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
