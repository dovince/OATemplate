using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using ZWL.DBUtility;//Please add references
namespace ZWL.BLL
{
    /// <summary>
    /// 类VacationLeave。
    /// </summary>
    [Serializable]
    public partial class VacationLeave
    {
        public VacationLeave()
        { }
        #region Model
        private int _id;
        private string _username;
        private DateTime? _startdate;
        private DateTime? _enddate;
        private decimal? _tiandays;
        private string _department;
        private string _zhiwu;
        private string _buxiujiatype;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TianDays
        {
            set { _tiandays = value; }
            get { return _tiandays; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhiWu
        {
            set { _zhiwu = value; }
            get { return _zhiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuXiuJiaType
        {
            set { _buxiujiatype = value; }
            get { return _buxiujiatype; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VacationLeave(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserName,StartDate,EndDate,TianDays,Department,ZhiWu,BuXiuJiaType ");
            strSql.Append(" FROM [VacationLeave] ");
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
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartDate"] != null && ds.Tables[0].Rows[0]["StartDate"].ToString() != "")
                {
                    this.StartDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    this.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TianDays"] != null && ds.Tables[0].Rows[0]["TianDays"].ToString() != "")
                {
                    this.TianDays = decimal.Parse(ds.Tables[0].Rows[0]["TianDays"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiWu"] != null)
                {
                    this.ZhiWu = ds.Tables[0].Rows[0]["ZhiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuXiuJiaType"] != null)
                {
                    this.BuXiuJiaType = ds.Tables[0].Rows[0]["BuXiuJiaType"].ToString();
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [VacationLeave]");
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
            strSql.Append("insert into [VacationLeave] (");
            strSql.Append("UserName,StartDate,EndDate,TianDays,Department,ZhiWu,BuXiuJiaType)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@StartDate,@EndDate,@TianDays,@Department,@ZhiWu,@BuXiuJiaType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TianDays", SqlDbType.Decimal,9),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@ZhiWu", SqlDbType.NVarChar,50),
					new SqlParameter("@BuXiuJiaType", SqlDbType.NVarChar,50)};
            parameters[0].Value = UserName;
            parameters[1].Value = StartDate;
            parameters[2].Value = EndDate;
            parameters[3].Value = TianDays;
            parameters[4].Value = Department;
            parameters[5].Value = ZhiWu;
            parameters[6].Value = BuXiuJiaType;

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
            strSql.Append("update [VacationLeave] set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("TianDays=@TianDays,");
            strSql.Append("Department=@Department,");
            strSql.Append("ZhiWu=@ZhiWu,");
            strSql.Append("BuXiuJiaType=@BuXiuJiaType");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TianDays", SqlDbType.Decimal,9),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@ZhiWu", SqlDbType.NVarChar,50),
					new SqlParameter("@BuXiuJiaType", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = UserName;
            parameters[1].Value = StartDate;
            parameters[2].Value = EndDate;
            parameters[3].Value = TianDays;
            parameters[4].Value = Department;
            parameters[5].Value = ZhiWu;
            parameters[6].Value = BuXiuJiaType;
            parameters[7].Value = ID;

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
            strSql.Append("delete from [VacationLeave] ");
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
            strSql.Append("select ID,UserName,StartDate,EndDate,TianDays,Department,ZhiWu,BuXiuJiaType ");
            strSql.Append(" FROM [VacationLeave] ");
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
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StartDate"] != null && ds.Tables[0].Rows[0]["StartDate"].ToString() != "")
                {
                    this.StartDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    this.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TianDays"] != null && ds.Tables[0].Rows[0]["TianDays"].ToString() != "")
                {
                    this.TianDays = decimal.Parse(ds.Tables[0].Rows[0]["TianDays"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiWu"] != null)
                {
                    this.ZhiWu = ds.Tables[0].Rows[0]["ZhiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BuXiuJiaType"] != null)
                {
                    this.BuXiuJiaType = ds.Tables[0].Rows[0]["BuXiuJiaType"].ToString();
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
            strSql.Append(" FROM [VacationLeave] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetSL(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM [VacationLeave] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public static string GetSLSum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(TianDays)");
            strSql.Append(" FROM [VacationLeave] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.GetSHSL(strSql.ToString());
        }
        public DataSet DeleteBXJ()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE");
            strSql.Append(" FROM [VacationLeave] ");
           
            return DbHelperSQL.Query(strSql.ToString());
        }

        public double GetCanUse(string username, string type)
        {
            double res = 0;
            var startyear = 2020;//2020年是第一年，这里是不断循环计算现在剩余补休假
            double qntsSY = 0;//去年天数剩余
            while (startyear <= DateTime.Now.Year)
            {
                //var beginyear = DateTime.Now.AddYears(-1).ToString("yyyy-01-01 00:00:00");
                //var endyear = DateTime.Now.ToString("yyyy-12-31 23:59:59");
                var beginyear = startyear + "-01-01 00:00:00";
                var endyear = startyear + "-12-31 23:59:59";
                ZWL.BLL.ERPQingJia qmodel = new ZWL.BLL.ERPQingJia();
                var tianshusum_cjzb = GetSLSum("UserName='" + username + "' and BuXiuJiaType='" + type + "' and StartDate >= '" + beginyear + "' and StartDate <= '" + endyear + "'");
                var tianshusumUsed_cjzb = qmodel.GetDaySLSum("QJR='" + username + "' and QJLX='" + type + "' and TBTime >= '" + beginyear + "' and TBTime <= '" + endyear + "' and (QJState = '正在办理' or QJState = '正常结束')");
                double ts = 0;
                double tsUsed = 0;
                double.TryParse(tianshusum_cjzb, out ts);
                double.TryParse(tianshusumUsed_cjzb, out tsUsed);
                if (startyear >= 2022)
                {
                    if (qntsSY > 0)
                    {
                        res -= qntsSY;//如果是2022后的天数则要减去前年没用的天数
                    }
                }
                //2020 -2 2021 3-2=1 2022 3-1-3=-1
                qntsSY = res - tsUsed;//获取去年天数剩余
                                      //2020 5-2=3 2021 2+3-2=3 2022 2-3
                res = ts + res - tsUsed;
                startyear++;
            }
            return res;
        }

        public static string GetIsUse(string username, string type, DateTime startDate, double tianshu)
        {
            double res = 0;
            var startyear = 2020;//2020年是第一年，这里是不断循环计算现在剩余补休假
            double qntsSY = 0;//去年天数剩余
            while (startyear <= startDate.Year + 1)
            {
                //var beginyear = DateTime.Now.AddYears(-1).ToString("yyyy-01-01 00:00:00");
                //var endyear = DateTime.Now.ToString("yyyy-12-31 23:59:59");
                var beginyear = startyear + "-01-01 00:00:00";
                var endyear = startyear + "-12-31 23:59:59";
                var sd = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                ZWL.BLL.ERPQingJia qmodel = new ZWL.BLL.ERPQingJia();
                var tianshusum_cjzb = ZWL.BLL.VacationLeave.GetSLSum("UserName='" + username + "' and BuXiuJiaType='" + type + "' and StartDate >= '" + beginyear + "' and StartDate <= '" + endyear + "' and StartDate<= '" + sd + "'");
                var tianshusumUsed_cjzb = qmodel.GetDaySLSum("QJR='" + username + "' and QJLX='" + type + "' and TBTime >= '" + beginyear + "' and TBTime <= '" + endyear + "' and (QJState = '正在办理' or QJState = '正常结束')");
                double ts = 0;
                double tsUsed = 0;
                double.TryParse(tianshusum_cjzb, out ts);
                double.TryParse(tianshusumUsed_cjzb, out tsUsed);
                //if (startyear == DateTime.Now.Year)//如果是今年的话
                //{
                //    res += ts;
                //}
                //else
                //{
                //    if (res >= tsUsed)
                //    {
                //        res = ts;
                //    }
                //    else
                //    {
                //        res = ts + res - tsUsed;
                //    }
                //}
                if (startyear >= 2022)
                {
                    if (qntsSY > 0)
                    {
                        res -= qntsSY;//如果是2022后的天数则要减去前年没用的天数
                    }
                }
                //2020 -2 2021 3-2=1 2022 3-1-3=-1
                qntsSY = res - tsUsed;//获取去年天数剩余
                                      //2020 5-2=3 2021 2+3-2=3 2022 2-3
                res = ts + res - tsUsed;
                startyear++;
            }
            if (res > 0)
            {
                if (tianshu > res)
                {
                    return "已使用部分,剩余" + res + "天未用";
                }
                else
                {
                    return "未使用";
                }
            }
            else
            {
                return "已使用";
            }
        }

        #endregion  Method



        public void UpdateWorkYearAndBXJ()
        {
            //跨年的时候，清空补休假数据表
            //DeleteBXJ();
        }

       
    }
}
