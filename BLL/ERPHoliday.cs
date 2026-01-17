using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    /// <summary>
    /// 节假日类
    /// </summary>
    public class ERPHoliday
    {
        public ERPHoliday()
        { }
        #region Model
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string holidayName;
        public string HolidayName
        {
            get { return holidayName; }
            set { holidayName = value; }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private string backInfo;
        public string BackInfo
        {
            get { return backInfo; }
            set { backInfo = value; }
        }

        private double days;
        public double Days
        {
            get { return days; }
            set { days = value; }
        }

        private string switchWorkTime;
        public string SwitchWorkTime
        {
            get { return switchWorkTime; }
            set { switchWorkTime = value; }
        }

        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHoliday");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPHoliday ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHoliday ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HolidayName,StartTime,EndTime,BackInfo,Days,SwitchWorkTime ");
            strSql.Append(" FROM ERPHoliday ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            DataSet ds = ZWL.DBUtility.DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                HolidayName = ds.Tables[0].Rows[0]["HolidayName"].ToString();
                //节假日起始时间
                string strstarttime = ds.Tables[0].Rows[0]["StartTime"].ToString();
                DateTime stime = new DateTime();
                ZWL.Common.PublicMethod.GetDefaultTime(out stime);
                DateTime.TryParse(strstarttime, out stime);
                StartTime = stime;
                string strendtime = ds.Tables[0].Rows[0]["EndTime"].ToString();
                DateTime etime = new DateTime();
                ZWL.Common.PublicMethod.GetDefaultTime(out etime);
                DateTime.TryParse(strendtime, out etime);
                EndTime = etime;//结束时间
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                double ndays = 0;
                ndays =Double.Parse(ds.Tables[0].Rows[0]["Days"].ToString());
                Days = ndays;
                SwitchWorkTime = ds.Tables[0].Rows[0]["SwitchWorkTime"].ToString();
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPHoliday set ");
            strSql.Append("HolidayName=@HolidayName,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("BackInfo=@BackInfo,");
            strSql.Append("Days=@Days,");
            strSql.Append("SwitchWorkTime=@SwitchWorkTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@HolidayName", SqlDbType.VarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,2000),
					new SqlParameter("@Days", SqlDbType.Decimal),
					new SqlParameter("@SwitchWorkTime", SqlDbType.VarChar,500)};
            parameters[0].Value = ID;
            parameters[1].Value = HolidayName;
            parameters[2].Value = StartTime;//开始时间
            parameters[3].Value = EndTime;//结束时间
            parameters[4].Value = BackInfo;//备注信息
            parameters[5].Value = Days;
            parameters[6].Value = SwitchWorkTime;
            object obj= ZWL.DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPHoliday(");
            strSql.Append("HolidayName,StartTime,EndTime,BackInfo,Days,SwitchWorkTime)");
            strSql.Append(" values (");
            strSql.Append("@HolidayName,@StartTime,@EndTime,@BackInfo,@Days,@SwitchWorkTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@HolidayName", SqlDbType.VarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@BackInfo", SqlDbType.VarChar,2000),
					new SqlParameter("@Days", SqlDbType.Decimal),
					new SqlParameter("@SwitchWorkTime", SqlDbType.VarChar,500)
					};
            parameters[0].Value = HolidayName;
            parameters[1].Value = StartTime;//开始时间
            parameters[2].Value = EndTime;//结束时间
            parameters[3].Value = BackInfo;//备注信息
            parameters[4].Value = Days;
            parameters[5].Value = SwitchWorkTime;
            object obj = ZWL.DBUtility.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion 成员方法
    }
}
