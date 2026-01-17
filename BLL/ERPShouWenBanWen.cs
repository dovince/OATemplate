using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class ERPShouWenBanWen
    {
        public ERPShouWenBanWen()
        { }
        #region Model
        private int _id;
        private DateTime _shouwentime;//收文日期
        private string _laiwendanwei;//来文单位
        private string _laiwenzihao;//来文字号
        private string _miji;//密级
        private string _filetype;//紧急程度
        private string _fenbianhao;//分编号
        private string _jingbanren;//经办人
        private string _pibanren;//批办人
        private string _chengbanren;//承办人
        private string _titlestr;//标题
        private string _keyword;//关键词
        private string _fujianstr;//附件
        private int _nworkid;//工作编号
        private DateTime? _xianbantime;
        
        
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 收文日期
        /// </summary>
        public DateTime ShouWenTime
        {
            set { _shouwentime = value; }
            get { return _shouwentime; }
        }
        /// <summary>
        /// 来文单位
        /// </summary>
        public string LaiWenDW
        {
            set { _laiwendanwei = value; }
            get { return _laiwendanwei; }
        }
        /// <summary>
        /// 来文字号
        /// </summary>
        public string LaiWenZH
        {
            set { _laiwenzihao = value; }
            get { return _laiwenzihao; }
        }
        
        /// <summary>
        /// 密级
        /// </summary>
        public string MiJi
        {
            set { _miji = value; }
            get { return _miji; }
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
        /// 分编号
        /// </summary>
        public string FenBH
        {
            set { _fenbianhao = value; }
            get { return _fenbianhao; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string JBR
        {
            set { _jingbanren = value; }
            get { return _jingbanren; }
        }
        /// <summary>
        /// 批办人
        /// </summary>
        public string PBR
        {
            set { _pibanren = value; }
            get { return _pibanren; }
        }
        /// <summary>
        /// 承办人
        /// </summary>
        public string CBR
        {
            set { _chengbanren = value; }
            get { return _chengbanren; }
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
        /// 关键词
        /// </summary>
        public string KeyWord
        {
            set { _keyword = value; }
            get { return _keyword; }
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
        /// 限办日期
        /// </summary>
        public DateTime? XianBanTime
        {
            set { _xianbantime = value; }
            get { return _xianbantime; }
        }
        
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPShouWenBanWen(");
            strSql.Append("ShouWenTime,LaiWenDW,LaiWenZH,MiJi,FileType,FenBH,JBR,PBR,CBR,TitleStr,KeyWord,NWorkID,FuJianStr,XianBanTime)");
            strSql.Append(" values (");
            strSql.Append("@ShouWenTime,@LaiWenDW,@LaiWenZH,@MiJi,@FileType,@FenBH,@JBR,@PBR,@CBR,@TitleStr,@KeyWord,@NWorkID,@FuJianStr,@XianBanTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ShouWenTime",SqlDbType.DateTime),
                    new SqlParameter("@LaiWenDW", SqlDbType.VarChar,200),
                    new SqlParameter("@LaiWenZH", SqlDbType.VarChar,50),
                    new SqlParameter("@MiJi", SqlDbType.VarChar,20),
                    new SqlParameter("@FileType", SqlDbType.VarChar,20),
                    new SqlParameter("@FenBH", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.VarChar,20),
                    new SqlParameter("@PBR", SqlDbType.VarChar,200),
                    new SqlParameter("@CBR", SqlDbType.VarChar,200),
                    new SqlParameter("@TitleStr",SqlDbType.VarChar,500),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,200),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@FuJianStr",SqlDbType.VarChar,5000),
                    new SqlParameter("@XianBanTime",SqlDbType.DateTime)
                                        };

            parameters[0].Value = ShouWenTime;
            parameters[1].Value = LaiWenDW;
            parameters[2].Value = LaiWenZH;
            parameters[3].Value = MiJi;
            parameters[4].Value = FileType;
            parameters[5].Value = FenBH;
            parameters[6].Value = JBR;
            parameters[7].Value = PBR;
            parameters[8].Value = CBR;
            parameters[9].Value = TitleStr;
            parameters[10].Value = KeyWord;
            parameters[11].Value = NWorkID;
            parameters[12].Value = FuJianStr;
            parameters[13].Value = XianBanTime;
            

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
        public ERPShouWenBanWen(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPShouWenBanWen ");
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
                if (ds.Tables[0].Rows[0]["ShouWenTime"].ToString() != "")
                {
                    ShouWenTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShouWenTime"].ToString());
                }
                LaiWenDW = ds.Tables[0].Rows[0]["LaiWenDW"].ToString();
                LaiWenZH = ds.Tables[0].Rows[0]["LaiWenZH"].ToString();
                MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                FenBH = ds.Tables[0].Rows[0]["FenBH"].ToString();
                JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                PBR = ds.Tables[0].Rows[0]["PBR"].ToString();
                CBR = ds.Tables[0].Rows[0]["CBR"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                if (ds.Tables[0].Rows[0]["XianBanTime"].ToString() != "")
                {
                    XianBanTime = DateTime.Parse(ds.Tables[0].Rows[0]["XianBanTime"].ToString());
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPShouWenBanWen");
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
            strSql.Append("update ERPShouWenBanWen set ");
            strSql.Append("ShouWenTime=@ShouWenTime,");
            strSql.Append("LaiWenDW=@LaiWenDW,");
            strSql.Append("LaiWenZH=@LaiWenZH,");
            strSql.Append("MiJi=@MiJi,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("FenBH=@FenBH,");
            strSql.Append("JBR=@JBR,");
            strSql.Append("PBR=@PBR,");
            strSql.Append("CBR=@CBR,");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("KeyWord=@KeyWord,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("FuJianStr=@FuJianStr");
            strSql.Append("XianBanTime=@XianBanTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@ShouWenTime",SqlDbType.DateTime),
                    new SqlParameter("@LaiWenDW", SqlDbType.VarChar,200),
                    new SqlParameter("@LaiWenZH", SqlDbType.VarChar,50),
                    new SqlParameter("@MiJi", SqlDbType.VarChar,20),
                    new SqlParameter("@FileType", SqlDbType.VarChar,20),
                    new SqlParameter("@FenBH", SqlDbType.VarChar,50),
                    new SqlParameter("@JBR", SqlDbType.VarChar,20),
                    new SqlParameter("@PBR", SqlDbType.VarChar,200),
                    new SqlParameter("@CBR", SqlDbType.VarChar,200),
                    new SqlParameter("@TitleStr",SqlDbType.VarChar,500),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,200),
                    new SqlParameter("@NWorkID",SqlDbType.Int,6),
                    new SqlParameter("@FuJianStr",SqlDbType.VarChar,5000),
                    new SqlParameter("@XianBanTime",SqlDbType.DateTime)
                                        };
            parameters[0].Value = ID;
            parameters[1].Value = ShouWenTime;
            parameters[2].Value = LaiWenDW;
            parameters[3].Value = LaiWenZH;
            parameters[4].Value = MiJi;
            parameters[5].Value = FileType;
            parameters[6].Value = FenBH;
            parameters[7].Value = JBR;
            parameters[8].Value = PBR;
            parameters[9].Value = CBR;
            parameters[10].Value = TitleStr;
            parameters[11].Value = KeyWord;
            parameters[12].Value = NWorkID;
            parameters[13].Value = FuJianStr;
            parameters[14].Value = XianBanTime;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPShouWenBanWen ");
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
            strSql.Append("select  top 1 ID,ShouWenTime,LaiWenDW,LaiWenZH,MiJi,FileType,FenBH,JBR,PBR,CBR,TitleStr,KeyWord,NWorkID,FuJianStr,XianBanTime ");
            strSql.Append(" FROM ERPShouWenBanWen ");
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
                if (ds.Tables[0].Rows[0]["ShouWenTime"].ToString() != "")
                {
                    ShouWenTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShouWenTime"].ToString());
                }
                LaiWenDW = ds.Tables[0].Rows[0]["LaiWenDW"].ToString();
                LaiWenZH = ds.Tables[0].Rows[0]["LaiWenZH"].ToString();
                MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                FenBH = ds.Tables[0].Rows[0]["FenBH"].ToString();
                JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                PBR = ds.Tables[0].Rows[0]["PBR"].ToString();
                CBR = ds.Tables[0].Rows[0]["CBR"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                if (ds.Tables[0].Rows[0]["XianBanTime"].ToString() != "")
                {
                    XianBanTime = DateTime.Parse(ds.Tables[0].Rows[0]["XianBanTime"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void NWorkGetModel(int nworkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ShouWenTime,LaiWenDW,LaiWenZH,MiJi,FileType,FenBH,JBR,PBR,CBR,TitleStr,KeyWord,NWorkID,FuJianStr,XianBanTime ");
            strSql.Append(" FROM ERPShouWenBanWen ");
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
                if (ds.Tables[0].Rows[0]["ShouWenTime"].ToString() != "")
                {
                    ShouWenTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShouWenTime"].ToString());
                }
                LaiWenDW = ds.Tables[0].Rows[0]["LaiWenDW"].ToString();
                LaiWenZH = ds.Tables[0].Rows[0]["LaiWenZH"].ToString();
                MiJi = ds.Tables[0].Rows[0]["MiJi"].ToString();
                FileType = ds.Tables[0].Rows[0]["FileType"].ToString();
                FenBH = ds.Tables[0].Rows[0]["FenBH"].ToString();
                JBR = ds.Tables[0].Rows[0]["JBR"].ToString();
                PBR = ds.Tables[0].Rows[0]["PBR"].ToString();
                CBR = ds.Tables[0].Rows[0]["CBR"].ToString();
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                if (ds.Tables[0].Rows[0]["XianBanTime"].ToString() != "")
                {
                    XianBanTime = DateTime.Parse(ds.Tables[0].Rows[0]["XianBanTime"].ToString());
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
            strSql.Append(" FROM ERPShouWenBanWen ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}
