using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPChuShengShenQing。
    /// </summary>
    [Serializable]
    public partial class ERPChuShengShenQing
    {
        public ERPChuShengShenQing()
        { }
        #region Model
        private int _id;
        private string _shiyou;
        private DateTime? _starttime;
        private DateTime? _backtime;
        private string _didian;
        private string _iszhonggaofengxiandiqu;
        private string _dianhua;
        private string _xingcheng;
        private string _content;
        private string _startjiaotongfangshi;
        private string _backjiaotongfangshi;
        private string _sqr;
        private string _djbm;
        private DateTime _createdtime;
        private int? _nworkid;
        private string _tongxingren;
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
        public string ShiYou
        {
            set { _shiyou = value; }
            get { return _shiyou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BackTime
        {
            set { _backtime = value; }
            get { return _backtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiDian
        {
            set { _didian = value; }
            get { return _didian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsZhongGaoFengXianDiQu
        {
            set { _iszhonggaofengxiandiqu = value; }
            get { return _iszhonggaofengxiandiqu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DianHua
        {
            set { _dianhua = value; }
            get { return _dianhua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XingCheng
        {
            set { _xingcheng = value; }
            get { return _xingcheng; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartJiaoTongFangshi
        {
            set { _startjiaotongfangshi = value; }
            get { return _startjiaotongfangshi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackJiaoTongFangshi
        {
            set { _backjiaotongfangshi = value; }
            get { return _backjiaotongfangshi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SQR
        {
            set { _sqr = value; }
            get { return _sqr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DJBM
        {
            set { _djbm = value; }
            get { return _djbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TongXingRen
        {
            set { _tongxingren = value; }
            get { return _tongxingren; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPChuShengShenQing(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ShiYou,StartTime,BackTime,DiDian,IsZhongGaoFengXianDiQu,DianHua,XingCheng,Content,StartJiaoTongFangshi,BackJiaoTongFangshi,SQR,DJBM,CreatedTime,NWorkID,TongXingRen ");
            strSql.Append(" FROM [ERPChuShengShenQing] ");
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
                if (ds.Tables[0].Rows[0]["ShiYou"] != null)
                {
                    this.ShiYou = ds.Tables[0].Rows[0]["ShiYou"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
                {
                    this.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackTime"] != null && ds.Tables[0].Rows[0]["BackTime"].ToString() != "")
                {
                    this.BackTime = DateTime.Parse(ds.Tables[0].Rows[0]["BackTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DiDian"] != null)
                {
                    this.DiDian = ds.Tables[0].Rows[0]["DiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsZhongGaoFengXianDiQu"] != null)
                {
                    this.IsZhongGaoFengXianDiQu = ds.Tables[0].Rows[0]["IsZhongGaoFengXianDiQu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DianHua"] != null)
                {
                    this.DianHua = ds.Tables[0].Rows[0]["DianHua"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingCheng"] != null)
                {
                    this.XingCheng = ds.Tables[0].Rows[0]["XingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Content"] != null)
                {
                    this.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartJiaoTongFangshi"] != null)
                {
                    this.StartJiaoTongFangshi = ds.Tables[0].Rows[0]["StartJiaoTongFangshi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BackJiaoTongFangshi"] != null)
                {
                    this.BackJiaoTongFangshi = ds.Tables[0].Rows[0]["BackJiaoTongFangshi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TongXingRen"] != null)
                {
                    this.TongXingRen = ds.Tables[0].Rows[0]["TongXingRen"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPChuShengShenQing]");
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
            strSql.Append("insert into [ERPChuShengShenQing] (");
            strSql.Append("ShiYou,StartTime,BackTime,DiDian,IsZhongGaoFengXianDiQu,DianHua,XingCheng,Content,StartJiaoTongFangshi,BackJiaoTongFangshi,SQR,DJBM,CreatedTime,NWorkID,TongXingRen)");
            strSql.Append(" values (");
            strSql.Append("@ShiYou,@StartTime,@BackTime,@DiDian,@IsZhongGaoFengXianDiQu,@DianHua,@XingCheng,@Content,@StartJiaoTongFangshi,@BackJiaoTongFangshi,@SQR,@DJBM,@CreatedTime,@NWorkID,@TongXingRen)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ShiYou", SqlDbType.VarChar,4000),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@BackTime", SqlDbType.DateTime),
                    new SqlParameter("@DiDian", SqlDbType.VarChar,500),
                    new SqlParameter("@IsZhongGaoFengXianDiQu", SqlDbType.VarChar,50),
                    new SqlParameter("@DianHua", SqlDbType.VarChar,80),
                    new SqlParameter("@XingCheng", SqlDbType.VarChar,1000),
                    new SqlParameter("@Content", SqlDbType.VarChar,1000),
                    new SqlParameter("@StartJiaoTongFangshi", SqlDbType.NVarChar,50),
                    new SqlParameter("@BackJiaoTongFangshi", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQR", SqlDbType.VarChar,20),
                    new SqlParameter("@DJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@TongXingRen", SqlDbType.NVarChar,1000)};
            parameters[0].Value = ShiYou;
            parameters[1].Value = StartTime;
            parameters[2].Value = BackTime;
            parameters[3].Value = DiDian;
            parameters[4].Value = IsZhongGaoFengXianDiQu;
            parameters[5].Value = DianHua;
            parameters[6].Value = XingCheng;
            parameters[7].Value = Content;
            parameters[8].Value = StartJiaoTongFangshi;
            parameters[9].Value = BackJiaoTongFangshi;
            parameters[10].Value = SQR;
            parameters[11].Value = DJBM;
            parameters[12].Value = CreatedTime;
            parameters[13].Value = NWorkID;
            parameters[14].Value = TongXingRen;

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
            strSql.Append("update [ERPChuShengShenQing] set ");
            strSql.Append("ShiYou=@ShiYou,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("BackTime=@BackTime,");
            strSql.Append("DiDian=@DiDian,");
            strSql.Append("IsZhongGaoFengXianDiQu=@IsZhongGaoFengXianDiQu,");
            strSql.Append("DianHua=@DianHua,");
            strSql.Append("XingCheng=@XingCheng,");
            strSql.Append("Content=@Content,");
            strSql.Append("StartJiaoTongFangshi=@StartJiaoTongFangshi,");
            strSql.Append("BackJiaoTongFangshi=@BackJiaoTongFangshi,");
            strSql.Append("SQR=@SQR,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("TongXingRen=@TongXingRen");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ShiYou", SqlDbType.VarChar,4000),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@BackTime", SqlDbType.DateTime),
                    new SqlParameter("@DiDian", SqlDbType.VarChar,500),
                    new SqlParameter("@IsZhongGaoFengXianDiQu", SqlDbType.VarChar,50),
                    new SqlParameter("@DianHua", SqlDbType.VarChar,80),
                    new SqlParameter("@XingCheng", SqlDbType.VarChar,1000),
                    new SqlParameter("@Content", SqlDbType.VarChar,1000),
                    new SqlParameter("@StartJiaoTongFangshi", SqlDbType.NVarChar,50),
                    new SqlParameter("@BackJiaoTongFangshi", SqlDbType.NVarChar,50),
                    new SqlParameter("@SQR", SqlDbType.VarChar,20),
                    new SqlParameter("@DJBM", SqlDbType.VarChar,50),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@TongXingRen", SqlDbType.NVarChar,1000),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ShiYou;
            parameters[1].Value = StartTime;
            parameters[2].Value = BackTime;
            parameters[3].Value = DiDian;
            parameters[4].Value = IsZhongGaoFengXianDiQu;
            parameters[5].Value = DianHua;
            parameters[6].Value = XingCheng;
            parameters[7].Value = Content;
            parameters[8].Value = StartJiaoTongFangshi;
            parameters[9].Value = BackJiaoTongFangshi;
            parameters[10].Value = SQR;
            parameters[11].Value = DJBM;
            parameters[12].Value = CreatedTime;
            parameters[13].Value = NWorkID;
            parameters[14].Value = TongXingRen;
            parameters[15].Value = ID;

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
            strSql.Append("delete from [ERPChuShengShenQing] ");
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
            strSql.Append("select ID,ShiYou,StartTime,BackTime,DiDian,IsZhongGaoFengXianDiQu,DianHua,XingCheng,Content,StartJiaoTongFangshi,BackJiaoTongFangshi,SQR,DJBM,CreatedTime,NWorkID,TongXingRen ");
            strSql.Append(" FROM [ERPChuShengShenQing] ");
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
                if (ds.Tables[0].Rows[0]["ShiYou"] != null)
                {
                    this.ShiYou = ds.Tables[0].Rows[0]["ShiYou"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
                {
                    this.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackTime"] != null && ds.Tables[0].Rows[0]["BackTime"].ToString() != "")
                {
                    this.BackTime = DateTime.Parse(ds.Tables[0].Rows[0]["BackTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DiDian"] != null)
                {
                    this.DiDian = ds.Tables[0].Rows[0]["DiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsZhongGaoFengXianDiQu"] != null)
                {
                    this.IsZhongGaoFengXianDiQu = ds.Tables[0].Rows[0]["IsZhongGaoFengXianDiQu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DianHua"] != null)
                {
                    this.DianHua = ds.Tables[0].Rows[0]["DianHua"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XingCheng"] != null)
                {
                    this.XingCheng = ds.Tables[0].Rows[0]["XingCheng"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Content"] != null)
                {
                    this.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartJiaoTongFangshi"] != null)
                {
                    this.StartJiaoTongFangshi = ds.Tables[0].Rows[0]["StartJiaoTongFangshi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BackJiaoTongFangshi"] != null)
                {
                    this.BackJiaoTongFangshi = ds.Tables[0].Rows[0]["BackJiaoTongFangshi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TongXingRen"] != null)
                {
                    this.TongXingRen = ds.Tables[0].Rows[0]["TongXingRen"].ToString();
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
            strSql.Append(" FROM [ERPChuShengShenQing] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM (select b.*,(select ZhiWei from ERPUser where UserName= b.SQR) ZhiWu
							  , d.WorkName, d.[FormID]
							  , d.[WorkFlowID]
							  , d.[UserName]
							  , d.[TimeStr]
							  , d.[ShenPiYiJian]
							  , d.[JieDianName]
							  , d.[ShenPiUserList]
							  , d.[OKUserList]
							  , d.[StateNow]
							  , d.[LateTime] from [ERPChuShengShenQing] b join ERPNWorkToDo d
						  on b.NWorkID = d.ID) h ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }

        #endregion  Method
    }
}

