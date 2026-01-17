using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPQingJia。
    /// </summary>
    [Serializable]
    public partial class ERPQingJia
    {
        public ERPQingJia()
        { }
        #region Model
        private int _id;
        private string _qjr;
        private string _bm;
        private DateTime _tbtime;
        private DateTime _qjsjstart;
        private DateTime _qjsjend;
        private float _qjts;
        private DateTime? _xjtime;
        private string _qjlx;
        private string _qjyy;
        private int _nworkid;
        private string _jiaose;
        private float _shiyongnxj;
        private string _qjstate;
        private string _bz;
        /// <summary>
        /// 序号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 请假人
        /// </summary>
        public string QJR
        {
            set { _qjr = value; }
            get { return _qjr; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string BM
        {
            set { _bm = value; }
            get { return _bm; }
        }
        /// <summary>
        /// 填表时间
        /// </summary>
        public DateTime TBTime
        {
            set { _tbtime = value; }
            get { return _tbtime; }
        }
        /// <summary>
        /// 请假时间起
        /// </summary>
        public DateTime QJSJStart
        {
            set { _qjsjstart = value; }
            get { return _qjsjstart; }
        }
        /// <summary>
        /// 请假时间止
        /// </summary>
        public DateTime QJSJEnd
        {
            set { _qjsjend = value; }
            get { return _qjsjend; }
        }
        /// <summary>
        /// 请假天数
        /// </summary>
        public float QJTS
        {
            set { _qjts = value; }
            get { return _qjts; }
        }
        /// <summary>
        /// 销假时间
        /// </summary>
        public DateTime? XJTime
        {
            set { _xjtime = value; }
            get { return _xjtime; }
        }
        /// <summary>
        /// 请假类型
        /// </summary>
        public string QJLX
        {
            set { _qjlx = value; }
            get { return _qjlx; }
        }
        /// <summary>
        /// 请假原因
        /// </summary>
        public string QJYY
        {
            set { _qjyy = value; }
            get { return _qjyy; }
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
        /// 角色
        /// </summary>
        public string JiaoSe
        {
            set { _jiaose = value; }
            get { return _jiaose; }
        }
        /// <summary>
        /// 使用年休假
        /// </summary>
        public float ShiYongNXJ
        {
            set { _shiyongnxj = value; }
            get { return _shiyongnxj; }
        }
        /// <summary>
        /// 请假状态
        /// </summary>
        public string QJState
        {
            set { _qjstate = value; }
            get { return _qjstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ
        {
            set { _bz = value; }
            get { return _bz; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPQingJia(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPQingJia] ");
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
                if (ds.Tables[0].Rows[0]["QJR"] != null)
                {
                    this.QJR = ds.Tables[0].Rows[0]["QJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null && ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJStart"] != null && ds.Tables[0].Rows[0]["QJSJStart"].ToString() != "")
                {
                    this.QJSJStart = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJEnd"] != null && ds.Tables[0].Rows[0]["QJSJEnd"].ToString() != "")
                {
                    this.QJSJEnd = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJTS"] != null && ds.Tables[0].Rows[0]["QJTS"].ToString() != "")
                {
                    this.QJTS = float.Parse(ds.Tables[0].Rows[0]["QJTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XJTime"] != null && ds.Tables[0].Rows[0]["XJTime"].ToString() != "")
                {
                    this.XJTime = DateTime.Parse(ds.Tables[0].Rows[0]["XJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJLX"] != null)
                {
                    this.QJLX = ds.Tables[0].Rows[0]["QJLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJYY"] != null)
                {
                    this.QJYY = ds.Tables[0].Rows[0]["QJYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiaoSe"] != null)
                {
                    this.JiaoSe = ds.Tables[0].Rows[0]["JiaoSe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShiYongNXJ"] != null && ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString() != "")
                {
                    this.ShiYongNXJ = float.Parse(ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJState"] != null)
                {
                    this.QJState = ds.Tables[0].Rows[0]["QJState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPQingJia]");
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
            strSql.Append("insert into [ERPQingJia] (");
            strSql.Append("QJR,BM,TBTime,QJSJStart,QJSJEnd,QJTS,XJTime,QJLX,QJYY,NWorkID,JiaoSe,ShiYongNXJ,QJState,BZ)");
            strSql.Append(" values (");
            strSql.Append("@QJR,@BM,@TBTime,@QJSJStart,@QJSJEnd,@QJTS,@XJTime,@QJLX,@QJYY,@NWorkID,@JiaoSe,@ShiYongNXJ,@QJState,@BZ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@QJR", SqlDbType.VarChar,50),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@QJSJStart", SqlDbType.DateTime),
                    new SqlParameter("@QJSJEnd", SqlDbType.DateTime),
                    new SqlParameter("@QJTS", SqlDbType.Decimal,9),
                    new SqlParameter("@XJTime", SqlDbType.DateTime),
                    new SqlParameter("@QJLX", SqlDbType.VarChar,50),
                    new SqlParameter("@QJYY", SqlDbType.VarChar,8000),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@JiaoSe", SqlDbType.VarChar,50),
                    new SqlParameter("@ShiYongNXJ", SqlDbType.Float,8),
                    new SqlParameter("@QJState", SqlDbType.VarChar,10),
                    new SqlParameter("@BZ", SqlDbType.VarChar,500)};
            parameters[0].Value = QJR;
            parameters[1].Value = BM;
            parameters[2].Value = TBTime;
            parameters[3].Value = QJSJStart;
            parameters[4].Value = QJSJEnd;
            parameters[5].Value = QJTS;
            parameters[6].Value = XJTime;
            parameters[7].Value = QJLX;
            parameters[8].Value = QJYY;
            parameters[9].Value = NWorkID;
            parameters[10].Value = JiaoSe;
            parameters[11].Value = ShiYongNXJ;
            parameters[12].Value = QJState;
            parameters[13].Value = BZ;

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
            strSql.Append("update [ERPQingJia] set ");
            strSql.Append("QJR=@QJR,");
            strSql.Append("BM=@BM,");
            strSql.Append("TBTime=@TBTime,");
            strSql.Append("QJSJStart=@QJSJStart,");
            strSql.Append("QJSJEnd=@QJSJEnd,");
            strSql.Append("QJTS=@QJTS,");
            strSql.Append("XJTime=@XJTime,");
            strSql.Append("QJLX=@QJLX,");
            strSql.Append("QJYY=@QJYY,");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("JiaoSe=@JiaoSe,");
            strSql.Append("ShiYongNXJ=@ShiYongNXJ,");
            strSql.Append("QJState=@QJState,");
            strSql.Append("BZ=@BZ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@QJR", SqlDbType.VarChar,50),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@TBTime", SqlDbType.DateTime),
                    new SqlParameter("@QJSJStart", SqlDbType.DateTime),
                    new SqlParameter("@QJSJEnd", SqlDbType.DateTime),
                    new SqlParameter("@QJTS", SqlDbType.Decimal,9),
                    new SqlParameter("@XJTime", SqlDbType.DateTime),
                    new SqlParameter("@QJLX", SqlDbType.VarChar,50),
                    new SqlParameter("@QJYY", SqlDbType.VarChar,8000),
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@JiaoSe", SqlDbType.VarChar,50),
                    new SqlParameter("@ShiYongNXJ", SqlDbType.Float,8),
                    new SqlParameter("@QJState", SqlDbType.VarChar,10),
                    new SqlParameter("@BZ", SqlDbType.VarChar,500),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = QJR;
            parameters[1].Value = BM;
            parameters[2].Value = TBTime;
            parameters[3].Value = QJSJStart;
            parameters[4].Value = QJSJEnd;
            parameters[5].Value = QJTS;
            parameters[6].Value = XJTime;
            parameters[7].Value = QJLX;
            parameters[8].Value = QJYY;
            parameters[9].Value = NWorkID;
            parameters[10].Value = JiaoSe;
            parameters[11].Value = ShiYongNXJ;
            parameters[12].Value = QJState;
            parameters[13].Value = BZ;
            parameters[14].Value = ID;

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
            strSql.Append("delete from [ERPQingJia] ");
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
            strSql.Append(" FROM [ERPQingJia] ");
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
                if (ds.Tables[0].Rows[0]["QJR"] != null)
                {
                    this.QJR = ds.Tables[0].Rows[0]["QJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null && ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJStart"] != null && ds.Tables[0].Rows[0]["QJSJStart"].ToString() != "")
                {
                    this.QJSJStart = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJEnd"] != null && ds.Tables[0].Rows[0]["QJSJEnd"].ToString() != "")
                {
                    this.QJSJEnd = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJTS"] != null && ds.Tables[0].Rows[0]["QJTS"].ToString() != "")
                {
                    this.QJTS = float.Parse(ds.Tables[0].Rows[0]["QJTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XJTime"] != null && ds.Tables[0].Rows[0]["XJTime"].ToString() != "")
                {
                    this.XJTime = DateTime.Parse(ds.Tables[0].Rows[0]["XJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJLX"] != null)
                {
                    this.QJLX = ds.Tables[0].Rows[0]["QJLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJYY"] != null)
                {
                    this.QJYY = ds.Tables[0].Rows[0]["QJYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiaoSe"] != null)
                {
                    this.JiaoSe = ds.Tables[0].Rows[0]["JiaoSe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShiYongNXJ"] != null && ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString() != "")
                {
                    this.ShiYongNXJ = float.Parse(ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJState"] != null)
                {
                    this.QJState = ds.Tables[0].Rows[0]["QJState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetNWorkModel(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM ERPQingJia ");
            strSql.Append(" where NWorkID=@NWorkID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,6)};
            parameters[0].Value = nworktodoid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJR"] != null)
                {
                    this.QJR = ds.Tables[0].Rows[0]["QJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null && ds.Tables[0].Rows[0]["TBTime"].ToString() != "")
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJStart"] != null && ds.Tables[0].Rows[0]["QJSJStart"].ToString() != "")
                {
                    this.QJSJStart = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJSJEnd"] != null && ds.Tables[0].Rows[0]["QJSJEnd"].ToString() != "")
                {
                    this.QJSJEnd = DateTime.Parse(ds.Tables[0].Rows[0]["QJSJEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJTS"] != null && ds.Tables[0].Rows[0]["QJTS"].ToString() != "")
                {
                    this.QJTS = float.Parse(ds.Tables[0].Rows[0]["QJTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XJTime"] != null && ds.Tables[0].Rows[0]["XJTime"].ToString() != "")
                {
                    this.XJTime = DateTime.Parse(ds.Tables[0].Rows[0]["XJTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJLX"] != null)
                {
                    this.QJLX = ds.Tables[0].Rows[0]["QJLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJYY"] != null)
                {
                    this.QJYY = ds.Tables[0].Rows[0]["QJYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiaoSe"] != null)
                {
                    this.JiaoSe = ds.Tables[0].Rows[0]["JiaoSe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShiYongNXJ"] != null && ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString() != "")
                {
                    this.ShiYongNXJ = float.Parse(ds.Tables[0].Rows[0]["ShiYongNXJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QJState"] != null)
                {
                    this.QJState = ds.Tables[0].Rows[0]["QJState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BZ"] != null)
                {
                    this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
                }
            }

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.*,d.WorkName,d.FormID,d.WorkFlowID,d.StateNow,d.UserName,d.TimeStr,d.JieDianName,d.JieDianID,d.ShenPiUserList,d.OKUserList
                               from ERPQingJia c join ERPNWorkToDo d 
                              on c.NWorkID = d.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetDayList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPQingJia] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetDaySL(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM [ERPQingJia] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public string GetDaySLSum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum([QJTS])");
            strSql.Append(" FROM [ERPQingJia] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.GetSHSL(strSql.ToString());
        }
        //删除ERPQingJia表的当前用户春节值班补休假，疫情值班补休假的信息
        public void UpdateWorkYearAndBXJ()
        {

            DeleteBXJ();
        }
        public DataSet DeleteBXJ()
        {
            String strWhere = "QJR='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and (QJLX='疫情值班补休假'or QJLX='春节值班补休假')";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE");
            strSql.Append(" FROM [ERPQingJia] ");
            strSql.Append(" where " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetListMapping(string strWhere)
        //{
        //    string strSql = "";
        //    ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
        //    string strmapping = method.getSQLTable("ERPQingJia");
        //    strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql += " where " + "LB_MrALLFint." + strWhere + " order by 序号 desc";
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.*,d.WorkName,d.FormID,d.WorkFlowID,d.StateNow,d.UserName,d.TimeStr,d.JieDianName,d.JieDianID,d.ShenPiUserList,d.OKUserList
                               from ERPQingJia c join ERPNWorkToDo d 
                              on c.NWorkID = d.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }

        #endregion  成员方法
    }
}

