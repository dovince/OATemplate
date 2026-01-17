using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class ERPFaWenDJ
    {
        public ERPFaWenDJ()
        { }
        #region Model
        private int _id;
        
        private string _nigaokeshi;//拟稿科室
        private string _ngr;//拟稿人
        private string _fawenzihao;//发文字号
        private DateTime _nigaotime;//拟稿日期
        private string _fawentype;//发文类型
        private string _filetype;//紧急程度
        private string _keyword;//关键词
        private string _titlestr;//标题
        private string _zhusongdanwei;//主送单位
        private string _chaosong;//抄送
        private string _zhengwen;//正文
        private string _fujianstr;//附件
        private int _nworkid;//工作编号
        private string _touser;//接收人
        private string _yijieshouren;//已经接收人
        private string _contentstr;//详细情况        
        private string _chuanyueyijian;//传阅意见
        private string _qianshouhouidlist;//签收文件夹
        private string _chuanyuehouidlist1;//传阅文件夹
        
        
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
         /// <summary>
        /// 拟稿科室
        /// </summary>
        public string NGKeShi
        {
            set { _nigaokeshi = value; }
            get { return _nigaokeshi; }
        }
        /// <summary>
        /// 拟稿人
        /// </summary>
        public string NGR
        {
            set { _ngr = value; }
            get { return _ngr; }
        }        
        /// <summary>
        /// 发文字号
        /// </summary>
        public string FaWenZH
        {
            set { _fawenzihao = value; }
            get { return _fawenzihao; }
        }
        /// <summary>
        /// 拟稿时间
        /// </summary>
        public DateTime NGTime
        {
            set { _nigaotime = value; }
            get { return _nigaotime; }
        }
        /// <summary>
        /// 发文类型
        /// </summary>
        public string FWType
        {
            set { _fawentype = value; }
            get { return _fawentype; }
        }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 关键词
        /// </summary>
        public string KeyWord
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string TitleStr
        {
            set { _titlestr = value; }
            get { return _titlestr; }
        }
        /// <summary>
        /// 主送单位
        /// </summary>
        public string ZhuSongDW
        {
            set { _zhusongdanwei = value; }
            get { return _zhusongdanwei; }
        }
        /// <summary>
        /// 抄送
        /// </summary>
        public string ChaoSong
        {
            set { _chaosong = value; }
            get { return _chaosong; }
        }
        /// <summary>
        /// 正文
        /// </summary>
        public string ZhengWen
        {
            set { _zhengwen = value; }
            get { return _zhengwen; }
        }        
        /// <summary>
        /// 附件
        /// </summary>
        public string FuJianStr
        {
            set { _fujianstr = value; }
            get { return _fujianstr; }
        }
        /// <summary>
        /// 工作编号
        /// </summary>
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 接收人列表
        /// </summary>
        public string ToUser
        {
            set { _touser = value; }
            get { return _touser; }
        }
        /// <summary>
        /// 已经接收人
        /// </summary>
        public string YiJieShouRen
        {
            set { _yijieshouren = value; }
            get { return _yijieshouren; }
        }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string ContenStr
        {
            set { _contentstr = value; }
            get { return _contentstr; }
        }        
        
        /// <summary>
        /// 传阅意见
        /// </summary>
        public string ChuanYueYiJian
        {
            set { _chuanyueyijian = value; }
            get { return _chuanyueyijian; }
        }
        /// <summary>
        /// 签收后属于文件夹ID列表
        /// </summary>
        public string QianShouHouIDList
        {
            set { _qianshouhouidlist = value; }
            get { return _qianshouhouidlist; }
        }
        /// <summary>
        /// 传阅后属于文件夹ID列表
        /// </summary>
        public string ChuanYueHouIDList1
        {
            set { _chuanyuehouidlist1 = value; }
            get { return _chuanyuehouidlist1; }
        }
        
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPFaWenDJ(");
            strSql.Append("NGKeShi,NGR,FaWenZH,NGTime,FWType,FileType,KeyWord,ZhuSongDW,ChaoSong,ZhengWen,TitleStr,NWorkID,FuJianStr,ToUser,YiJieShouRen,ContenStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1)");
            strSql.Append(" values (");
            strSql.Append("@NGKeShi,@NGR,@FaWenZH,@NGTime,@FWType,@FileType,@KeyWord,@ZhuSongDW,@ChaoSong,@ZhengWen,@TitleStr,@NWorkID,@FuJianStr,@ToUser,@YiJieShouRen,@ContenStr,@ChuanYueYiJian,@QianShouHouIDList,@ChuanYueHouIDList1)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {					
                    new SqlParameter("@NGKeShi", SqlDbType.VarChar,50),
                    new SqlParameter("@NGR", SqlDbType.VarChar,20),
                    new SqlParameter("@FaWenZH", SqlDbType.VarChar,100),
                    new SqlParameter("@NGTime",SqlDbType.DateTime),
                    new SqlParameter("@FWType", SqlDbType.VarChar,100),
                    new SqlParameter("@FileType", SqlDbType.VarChar,20),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,200),
                    new SqlParameter("@ZhuSongDW",SqlDbType.VarChar,500),
                    new SqlParameter("@ChaoSong",SqlDbType.VarChar,500),
                    new SqlParameter("@ZhengWen",SqlDbType.VarChar,5000),
                    new SqlParameter("@TitleStr",SqlDbType.VarChar,500),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@FuJianStr",SqlDbType.VarChar,5000),
                    new SqlParameter("@ToUser", SqlDbType.VarChar,8000),
					new SqlParameter("@YiJieShouRen", SqlDbType.VarChar,8000),
					new SqlParameter("@ContenStr", SqlDbType.Text),                    
					new SqlParameter("@ChuanYueYiJian", SqlDbType.Text),
					new SqlParameter("@QianShouHouIDList", SqlDbType.VarChar,8000),
					new SqlParameter("@ChuanYueHouIDList1", SqlDbType.VarChar,8000)
                                        };

            parameters[0].Value = NGKeShi;
            parameters[1].Value = NGR;
            parameters[2].Value = FaWenZH;
            parameters[3].Value = NGTime;
            parameters[4].Value = FWType;
            parameters[5].Value = FileType;
            parameters[6].Value = KeyWord;
            parameters[7].Value = ZhuSongDW;
            parameters[8].Value = ChaoSong;
            parameters[9].Value = ZhengWen;
            parameters[10].Value = TitleStr;
            parameters[11].Value = NWorkID;
            parameters[12].Value = FuJianStr;
            parameters[13].Value = ToUser;
            parameters[14].Value = YiJieShouRen;
            parameters[15].Value = ContenStr;
            parameters[16].Value = ChuanYueYiJian;
            parameters[17].Value = QianShouHouIDList;
            parameters[18].Value = ChuanYueHouIDList1;
            

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPFaWenDJ(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPFaWenDJ ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                NGKeShi = ds.Tables[0].Rows[0]["NGKeShi"].ToString();
                NGR = ds.Tables[0].Rows[0]["NGR"].ToString();                
                FaWenZH = ds.Tables[0].Rows[0]["FaWenZH"].ToString();
                if (ds.Tables[0].Rows[0]["NGTime"].ToString() != "")
                {
                    NGTime = DateTime.Parse(ds.Tables[0].Rows[0]["NGTime"].ToString());
                }
                FWType = ds.Tables[0].Rows[0]["FWType"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                ZhuSongDW = ds.Tables[0].Rows[0]["ZhuSongDW"].ToString();
                ChaoSong = ds.Tables[0].Rows[0]["ChaoSong"].ToString();
                ZhengWen = ds.Tables[0].Rows[0]["ZhengWen"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                YiJieShouRen = ds.Tables[0].Rows[0]["YiJieShouRen"].ToString();
                ContenStr = ds.Tables[0].Rows[0]["ContenStr"].ToString();                
                ChuanYueYiJian = ds.Tables[0].Rows[0]["ChuanYueYiJian"].ToString();
                QianShouHouIDList = ds.Tables[0].Rows[0]["QianShouHouIDList"].ToString();
                ChuanYueHouIDList1 = ds.Tables[0].Rows[0]["ChuanYueHouIDList1"].ToString();
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPFaWenDJ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
 
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPFaWenDJ set ");
            strSql.Append("NGKeShi=@NGKeShi,");
            strSql.Append("NGR=@NGR,");
            strSql.Append("FaWenZH=@FaWenZH,");
            strSql.Append("NGTime=@NGTime,");
            strSql.Append("FWType=@FWType,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("KeyWord=@KeyWord,");
            strSql.Append("ZhuSongDW=@ZhuSongDW,");
            strSql.Append("ChaoSong=@ChaoSong,");
            strSql.Append("ZhengWen=@ZhengWen,");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("FuJianStr=@FuJianStr,");
            strSql.Append("ToUser=@ToUser,");
            strSql.Append("YiJieShouRen=@YiJieShouRen,");
            strSql.Append("ContenStr=@ContenStr,");            
            strSql.Append("ChuanYueYiJian=@ChuanYueYiJian,");
            strSql.Append("QianShouHouIDList=@QianShouHouIDList,");
            strSql.Append("ChuanYueHouIDList1=@ChuanYueHouIDList1");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                     new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@NGKeShi", SqlDbType.VarChar,50),
                    new SqlParameter("@NGR", SqlDbType.VarChar,20),
                    new SqlParameter("@FaWenZH", SqlDbType.VarChar,100),
                    new SqlParameter("@NGTime",SqlDbType.DateTime),
                    new SqlParameter("@FWType", SqlDbType.VarChar,100),
                    new SqlParameter("@FileType", SqlDbType.VarChar,20),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,200),
                    new SqlParameter("@ZhuSongDW",SqlDbType.VarChar,500),
                    new SqlParameter("@ChaoSong",SqlDbType.VarChar,500),
                    new SqlParameter("@ZhengWen",SqlDbType.VarChar,5000),
                    new SqlParameter("@TitleStr",SqlDbType.VarChar,500),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@FuJianStr",SqlDbType.VarChar,5000),
                    new SqlParameter("@ToUser", SqlDbType.VarChar,8000),
					new SqlParameter("@YiJieShouRen", SqlDbType.VarChar,8000),
					new SqlParameter("@ContenStr", SqlDbType.Text),					
					new SqlParameter("@ChuanYueYiJian", SqlDbType.Text),
					new SqlParameter("@QianShouHouIDList", SqlDbType.VarChar,8000),
					new SqlParameter("@ChuanYueHouIDList1", SqlDbType.VarChar,8000)
                                        };
            parameters[0].Value = ID;
            parameters[1].Value = NGKeShi;
            parameters[2].Value = NGR;
            parameters[3].Value = FaWenZH;
            parameters[4].Value = NGTime;
            parameters[5].Value = FWType;
            parameters[6].Value = FileType;
            parameters[7].Value = KeyWord;
            parameters[8].Value = ZhuSongDW;
            parameters[9].Value = ChaoSong;
            parameters[10].Value = ZhengWen;
            parameters[11].Value = TitleStr;
            parameters[12].Value = NWorkID;
            parameters[13].Value = FuJianStr;
            parameters[14].Value = ToUser;
            parameters[15].Value = YiJieShouRen;
            parameters[16].Value = ContenStr;            
            parameters[17].Value = ChuanYueYiJian;
            parameters[18].Value = QianShouHouIDList;
            parameters[19].Value = ChuanYueHouIDList1;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPFaWenDJ ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,NGKeShi,NGR,FaWenZH,NGTime,FWType,FileType,KeyWord,ZhuSongDW,ChaoSong,ZhengWen,TitleStr,NWorkID,FuJianStr,ToUser,YiJieShouRen,ContenStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1 ");
            strSql.Append(" FROM ERPFaWenDJ ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                NGKeShi = ds.Tables[0].Rows[0]["NGKeShi"].ToString();
                NGR = ds.Tables[0].Rows[0]["NGR"].ToString();
                FaWenZH = ds.Tables[0].Rows[0]["FaWenZH"].ToString();
                if (ds.Tables[0].Rows[0]["NGTime"].ToString() != "")
                {
                    NGTime = DateTime.Parse(ds.Tables[0].Rows[0]["NGTime"].ToString());
                }
                FWType = ds.Tables[0].Rows[0]["FWType"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                ZhuSongDW = ds.Tables[0].Rows[0]["ZhuSongDW"].ToString();
                ChaoSong = ds.Tables[0].Rows[0]["ChaoSong"].ToString();
                ZhengWen = ds.Tables[0].Rows[0]["ZhengWen"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                YiJieShouRen = ds.Tables[0].Rows[0]["YiJieShouRen"].ToString();
                ContenStr = ds.Tables[0].Rows[0]["ContenStr"].ToString();               
                ChuanYueYiJian = ds.Tables[0].Rows[0]["ChuanYueYiJian"].ToString();
                QianShouHouIDList = ds.Tables[0].Rows[0]["QianShouHouIDList"].ToString();
                ChuanYueHouIDList1 = ds.Tables[0].Rows[0]["ChuanYueHouIDList1"].ToString();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void NWorkGetModel(int nworkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,NGKeShi,NGR,FaWenZH,NGTime,FWType,FileType,KeyWord,ZhuSongDW,ChaoSong,ZhengWen,TitleStr,NWorkID,FuJianStr,ToUser,YiJieShouRen,ContenStr,ChuanYueYiJian,QianShouHouIDList,ChuanYueHouIDList1 ");
            strSql.Append(" FROM ERPFaWenDJ ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworkID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                NGKeShi = ds.Tables[0].Rows[0]["NGKeShi"].ToString();
                NGR = ds.Tables[0].Rows[0]["NGR"].ToString();
                FaWenZH = ds.Tables[0].Rows[0]["FaWenZH"].ToString();
                if (ds.Tables[0].Rows[0]["NGTime"].ToString() != "")
                {
                    NGTime = DateTime.Parse(ds.Tables[0].Rows[0]["NGTime"].ToString());
                }
                FWType = ds.Tables[0].Rows[0]["FWType"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                ZhuSongDW = ds.Tables[0].Rows[0]["ZhuSongDW"].ToString();
                ChaoSong = ds.Tables[0].Rows[0]["ChaoSong"].ToString();
                ZhengWen = ds.Tables[0].Rows[0]["ZhengWen"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();
                YiJieShouRen = ds.Tables[0].Rows[0]["YiJieShouRen"].ToString();
                ContenStr = ds.Tables[0].Rows[0]["ContenStr"].ToString();               
                ChuanYueYiJian = ds.Tables[0].Rows[0]["ChuanYueYiJian"].ToString();
                QianShouHouIDList = ds.Tables[0].Rows[0]["QianShouHouIDList"].ToString();
                ChuanYueHouIDList1 = ds.Tables[0].Rows[0]["ChuanYueHouIDList1"].ToString();
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPFaWenDJ ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}
