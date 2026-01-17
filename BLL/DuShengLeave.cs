using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

namespace ZWL.BLL
{
    public class DuShengLeave
    {
        /// <summary>
        /// 无参构造方法
        /// </summary>
        public DuShengLeave() { }
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string department;
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        private DateTime jionTime;
        public DateTime JionTime
        {
            get { return jionTime; }
            set { jionTime = value; }
        }

        private int nowWorkAge;
        public int NowWorkAge
        {
            get { return nowWorkAge; }
            set { nowWorkAge = value; }
        }

        private double nowDays;
        public double NowDays
        {
            get { return nowDays; }
            set { nowDays = value; }
        }

        private double nowUsedDays;
        public double NowUsedDays
        {
            get { return nowUsedDays; }
            set { nowUsedDays = value; }
        }

        private double nowRemainDays;
        public double NowRemainDays
        {
            get { return nowRemainDays; }
            set { nowRemainDays = value; }
        }

        private double lastDays;
        public double LastDays
        {
            get { return lastDays; }
            set { lastDays = value; }
        }

        private double lastRemainDays;
        public double LastRemainDays
        {
            get { return lastRemainDays; }
            set { lastRemainDays = value; }
        }
        /// <summary>
        /// 去年剩余独生子女护理假中已经用掉的天数
        /// </summary>
        private double lastUsedDays;
        public double LastUsedDays
        {
            get { return lastUsedDays; }
            set { lastUsedDays = value; }
        }
        private string backInfo;
        public string BackInfo
        {
            get { return backInfo; }
            set { backInfo = value; }
        }
        private double freezonDay;
        public double FreezonDay
        {
            get { return freezonDay; }
            set { freezonDay = value; }
        }
        private double lastfreezonDay;
        public double LastFreezonDay
        {
            get { return lastfreezonDay; }
            set { lastfreezonDay = value; }
        }
        private string _fujianlist;
        /// <summary>
        /// 附件文件
        /// </summary>
        public string FuJianList
        {
            set { _fujianlist = value; }
            get { return _fujianlist; }
        }

        private double _zyhljdays;
        public double ZYHLJDays
        {
            set { _zyhljdays = value; }
            get { return _zyhljdays; }
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DuShengLeave");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string strname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DuShengLeave");
            strSql.Append(" where UserName=@UserName ");

            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
            parameters[0].Value = strname;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DuShengLeave ");
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
            strSql.Append(" FROM DuShengLeave ");
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
            strSql.Append("SELECT [ID],[UserName],[Department],[JionTime],[NowWorkAge],[NowDays],[NowUsedDays],[NowRemainDays],[LastRemainDays],[BackInfo],[LastUsedDays],[FreezonDay],[LastFreezonDay],[FuJianList],[ZYHLJDays] FROM [DuShengLeave] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = nid;

            DataSet ds = ZWL.DBUtility.DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                string strJionTime = ds.Tables[0].Rows[0]["JionTime"].ToString();
                DateTime stime = new DateTime();
                ZWL.Common.PublicMethod.GetDefaultTime(out stime);
                DateTime.TryParse(strJionTime, out stime);
                JionTime = stime;
                Department = ds.Tables[0].Rows[0]["Department"].ToString();
                //工龄
                NowWorkAge = int.Parse(ds.Tables[0].Rows[0]["NowWorkAge"].ToString());
                //今年的独生子女护理假
                NowDays = double.Parse(ds.Tables[0].Rows[0]["NowDays"].ToString());
                //今年已经休的独生子女护理假
                NowUsedDays = double.Parse(ds.Tables[0].Rows[0]["NowUsedDays"].ToString());
                //今年还未休的独生子女护理假
                NowRemainDays = double.Parse(ds.Tables[0].Rows[0]["NowRemainDays"].ToString());
                //去年还剩余的独生子女护理假
                LastRemainDays = double.Parse(ds.Tables[0].Rows[0]["LastRemainDays"].ToString());
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                //去年独生子女护理假中已经用掉的天数
                LastUsedDays = double.Parse(ds.Tables[0].Rows[0]["LastUsedDays"].ToString());
                FreezonDay = double.Parse(ds.Tables[0].Rows[0]["FreezonDay"].ToString());
                LastFreezonDay = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDay"].ToString());

                FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                ZYHLJDays = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDays"].ToString());
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string strusername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ID],[UserName],[Department],[JionTime],[NowWorkAge],[NowDays],[NowUsedDays],[NowRemainDays],[LastRemainDays],[BackInfo],[LastUsedDays],[FreezonDay],[LastFreezonDay],[FuJianList],[ZYHLJDays] FROM [DuShengLeave] ");
            strSql.Append(" where UserName=@UserName ");
            SqlParameter[] parameters = {
				new SqlParameter("@UserName", SqlDbType.NVarChar,200)};
            parameters[0].Value = strusername;
            DataSet ds = ZWL.DBUtility.DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                string strJionTime = ds.Tables[0].Rows[0]["JionTime"].ToString();
                DateTime stime = new DateTime();
                ZWL.Common.PublicMethod.GetDefaultTime(out stime);
                DateTime.TryParse(strJionTime, out stime);
                JionTime = stime;
                Department = ds.Tables[0].Rows[0]["Department"].ToString();
                //工龄
                NowWorkAge = int.Parse(ds.Tables[0].Rows[0]["NowWorkAge"].ToString());
                //今年的独生子女护理假
                NowDays = double.Parse(ds.Tables[0].Rows[0]["NowDays"].ToString());
                //今年已经休的独生子女护理假
                NowUsedDays = double.Parse(ds.Tables[0].Rows[0]["NowUsedDays"].ToString());
                //今年还未休的独生子女护理假
                NowRemainDays = double.Parse(ds.Tables[0].Rows[0]["NowRemainDays"].ToString());
                //去年还剩余的独生子女护理假
                LastRemainDays = double.Parse(ds.Tables[0].Rows[0]["LastRemainDays"].ToString());
                BackInfo = ds.Tables[0].Rows[0]["BackInfo"].ToString();
                //去年独生子女护理假中已经用掉的天数
                LastUsedDays = double.Parse(ds.Tables[0].Rows[0]["LastUsedDays"].ToString());
                FreezonDay = double.Parse(ds.Tables[0].Rows[0]["FreezonDay"].ToString());
                LastFreezonDay = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDay"].ToString());

                FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
                ZYHLJDays = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDays"].ToString());
            }
        }
        /// <summary>
        /// 更新一条数据,变为内部方法防止随意调用
        /// </summary>
        private void Update()
        {
            StringBuilder strSql = new StringBuilder();
            string strsql = @"UPDATE [dbo].[DuShengLeave] SET [UserName] = @UserName, [Department] = @Department
            , [JionTime] = @JionTime, [NowWorkAge] = @NowWorkAge, [NowDays] = @NowDays, [NowUsedDays] = @NowUsedDays
            , [NowRemainDays] = @NowRemainDays, [LastRemainDays] = @LastRemainDays, [BackInfo] = @BackInfo,[LastUsedDays]=@LastUsedDays,[FreezonDay]=@FreezonDay,[LastFreezonDay]=@LastFreezonDay, [FuJianList] = @FuJianList, [ZYHLJDays] = @ZYHLJDays  WHERE ID = @ID ";
            strSql.Append(strsql);
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@UserName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@Department", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Department", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@JionTime", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "JionTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowWorkAge", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NowWorkAge", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowUsedDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowUsedDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@NowRemainDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowRemainDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@LastRemainDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastRemainDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@LastUsedDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastUsedDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@BackInfo", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BackInfo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@FreezonDay", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "FreezonDay", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                     new SqlParameter("@LastFreezonDay", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastFreezonDay", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@FuJianList", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FuJianList", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                     new SqlParameter("@ZYHLJDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "ZYHLJDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    };
            parameters[0].Value = ID;
            parameters[1].Value = UserName;
            parameters[2].Value = Department;
            parameters[3].Value = JionTime;
            parameters[4].Value = NowWorkAge;
            parameters[5].Value = NowDays;
            parameters[6].Value = NowUsedDays;
            parameters[7].Value = NowRemainDays;
            parameters[8].Value = LastRemainDays;
            parameters[9].Value = LastUsedDays;
            parameters[10].Value = BackInfo;
            parameters[11].Value = FreezonDay;
            parameters[12].Value = LastFreezonDay;
            parameters[13].Value = FuJianList;
            parameters[14].Value = ZYHLJDays;
            
            object obj = ZWL.DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            string strsql = @"INSERT INTO [dbo].[DuShengLeave] ([UserName], [Department], [JionTime], [NowWorkAge], [NowDays]
                            , [NowUsedDays], [NowRemainDays], [LastRemainDays], [BackInfo],[LastUsedDays],FreezonDay,[LastFreezonDay],[FuJianList],[ZYHLJDays])
                            VALUES (@UserName, @Department, @JionTime, @NowWorkAge, @NowDays, @NowUsedDays, @NowRemainDays
                            , @LastRemainDays, @BackInfo,@LastUsedDays,@FreezonDay,@LastFreezonDay,@FuJianList,@ZYHLJDays);
            SELECT ID, UserName, Department, JionTime, NowWorkAge, NowDays, NowUsedDays, NowRemainDays, LastRemainDays, BackInfo,LastUsedDays,FreezonDay,LastFreezonDay,FuJianList,ZYHLJDays FROM DuShengLeave WHERE (ID = SCOPE_IDENTITY())";
            strSql.Append(strsql);
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@Department", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Department", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@JionTime", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "JionTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowWorkAge", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NowWorkAge", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					new SqlParameter("@NowUsedDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowUsedDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@NowRemainDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "NowRemainDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@LastRemainDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastRemainDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@BackInfo", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BackInfo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@LastUsedDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastUsedDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@FreezonDay", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "FreezonDay", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@LastFreezonDay", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "LastFreezonDay", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@FuJianList", global::System.Data.SqlDbType.NText, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FuJianList", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
                    new SqlParameter("@ZYHLJDays", global::System.Data.SqlDbType.Decimal, 0, global::System.Data.ParameterDirection.Input, 18, 2, "ZYHLJDays", global::System.Data.DataRowVersion.Current, false, null, "", "", ""),
					}; 
            parameters[0].Value = UserName;
            parameters[1].Value = Department;
            parameters[2].Value = JionTime;
            parameters[3].Value = NowWorkAge;
            parameters[4].Value = NowDays;
            parameters[5].Value = NowUsedDays;
            parameters[6].Value = NowRemainDays;
            parameters[7].Value = LastRemainDays;
            parameters[8].Value = BackInfo;
            parameters[9].Value = LastUsedDays;
            parameters[10].Value = FreezonDay;
            parameters[11].Value = LastFreezonDay;
            parameters[12].Value = FuJianList;
            parameters[13].Value = ZYHLJDays;
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

        /// <summary>
        /// 检测独生子女护理假是否足够扣除
        /// </summary>
        /// <param name="nxj"></param>
        /// <returns></returns>
        public string checknxj(double nxj)
        {
            double nowremain = this.NowRemainDays;
            double lastremain = this.LastRemainDays;
            double lastky = lastremain - this.LastUsedDays;
            double dallky = nowremain + lastky;
            if (nxj <= dallky)
            {
                return "OK";
            }
            else
            {
                return "可用独生子女护理假剩余天数不足！请重新输入请假时间！";
            }
        }

        /// <summary>
        /// 检测独生子女护理假是否足够扣除(只检测今年的独生子女护理假)
        /// </summary>
        /// <param name="nxj"></param>
        /// <returns></returns>
        //public string checknxj(double nxj)
        //{
        //    double nowremain = this.NowRemainDays;
        //    if (nxj <= nowremain)
        //    {
        //        return "OK";
        //    }
        //    else
        //    {
        //        return "可用独生子女护理假剩余天数不足！请重新输入请假时间！";
        //    }
        //}

        /// <summary>
        /// 扣除独生子女护理假，同时冻结
        /// </summary>
        /// <param name="nxj"></param>
        /// <returns></returns>
        public string addnxj(double nxj)
        {
            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(this);

            double nowremain = this.NowRemainDays;
            double lastremain = this.LastRemainDays;
            double lastky = lastremain - this.LastUsedDays;
            double dallky = nowremain + lastky;
            if (nxj <= dallky)
            {
                if (nxj < lastky)//去年剩下的够用了
                {
                    //去年的还有剩余，今年的不变
                    this.LastUsedDays += nxj;
                    this.LastFreezonDay += nxj;//冻结独生子女护理假

                }
                else//去年剩的还不够
                {
                    this.LastUsedDays = this.LastRemainDays;//用完去年的

                    double ntemp = nxj - lastky;//还差几天
                    //this.NowUsedDays += ntemp;//今年已用的
                    this.FreezonDay += ntemp;//冻结独生子女护理假
                    this.LastFreezonDay += lastky;
                    this.NowRemainDays -= ntemp;//从今年剩余的扣除
                    this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假
                }

                //将更改写入日志中
                allog.LogChange(this, "扣除独生子女护理假", nxj, "扣除独生子女护理假" + nxj + "天");
                this.Update();
                return "OK";
            }
            else
            {
                return "可用独生子女护理假剩余天数不足！请重新输入请假时间！";
            }
        }

        /// <summary>
        /// 扣除独生子女护理假，同时冻结(不算去年的)
        /// </summary>
        /// <param name="nxj"></param>
        /// <returns></returns>
        //public string addnxj(double nxj)
        //{
        //    //初始化更改日志类，记录更改前的数据
        //    var allog = new DuShengLeaveChangeLog(this);

        //    double nowremain = this.NowRemainDays;
        //    if (nxj <= nowremain)
        //    {
        //        double ntemp = nxj;//还差几天
        //        this.FreezonDay += ntemp;//冻结独生子女护理假
        //        this.NowRemainDays -= ntemp;//从今年剩余的扣除
        //        this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假

        //        //将更改写入日志中
        //        allog.LogChange(this, "扣除独生子女护理假", nxj, "扣除独生子女护理假" + nxj + "天");
        //        this.Update();
        //        return "OK";
        //    }
        //    else
        //    {
        //        return "可用独生子女护理假剩余天数不足！请重新输入请假时间！";
        //    }
        //}

        /// <summary>
        /// 部门集体请假返还部分独生子女护理假，并解除冻结
        /// </summary>
        /// <param name="nxj"></param>
        public void bumenreturnnxj(double nxj, string bz)
        {
            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(this);

            if (nxj <= this.FreezonDay)//如果独生子女护理假小于等于今年冻结独生子女护理假
            {
                this.NowRemainDays += nxj;//返还今年可用天数
                this.FreezonDay -= nxj;//减掉冻结天数
                this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假
            }
            else//如果独生子女护理假大于今年冻结独生子女护理假，就要从去年冻结里面扣
            {
                var temp = nxj - this.FreezonDay;//减去今年冻结的，算出需要从去年冻结扣减的天数
                this.NowRemainDays += this.FreezonDay;//将今年冻结的加回到今年可用
                this.FreezonDay = 0;//清掉今年冻结
                this.LastUsedDays -= temp;//返还去年已用
                this.LastFreezonDay -= temp;//减掉去年可用独生子女护理假的冻结天数
                this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假
            }

            //将更改写入日志中
            allog.LogChange(this, "返还独生子女护理假", nxj, "返还独生子女护理假" + nxj + "天" + "," + bz);
            this.Update();
        }

        /// <summary>
        /// 返还部分独生子女护理假，并解除冻结
        /// </summary>
        /// <param name="nxj"></param>
        public void returnnxj(double nxj, string bz)
        {
            //判断nwid是否已经退了，如果退了的话就不需要再返还了
            if (DbHelperSQL.Exists(string.Format("select Top 1 * From DuShengLeaveChangeLog Where ChangeType='返还独生子女护理假' AND BZ like '%{0}'", bz)))
            {
                return;
            }

            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(this);

            if (nxj <= this.FreezonDay)//如果独生子女护理假小于等于今年冻结独生子女护理假
            {
                this.NowRemainDays += nxj;//返还今年可用天数
                this.FreezonDay -= nxj;//减掉冻结天数
                this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假
            }
            else//如果独生子女护理假大于今年冻结独生子女护理假，就要从去年冻结里面扣
            {
                var temp = nxj - this.FreezonDay;//减去今年冻结的，算出需要从去年冻结扣减的天数
                this.NowRemainDays += this.FreezonDay;//将今年冻结的加回到今年可用
                this.FreezonDay = 0;//清掉今年冻结
                this.LastUsedDays -= temp;//返还去年已用
                this.LastFreezonDay -= temp;//减掉去年可用独生子女护理假的冻结天数
                this.NowUsedDays = this.NowDays - this.NowRemainDays;//算出今年已用独生子女护理假
            }

            //将更改写入日志中
            allog.LogChange(this, "返还独生子女护理假", nxj, "返还独生子女护理假" + nxj + "天" + "," + bz);
            this.Update();
        }

        /// <summary>
        /// 解除冻结的独生子女护理假
        /// </summary>
        /// <param name="nxj"></param>
        public void nxjconfirm(double nxj)
        {
            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(this);

            if (nxj > this.LastFreezonDay)//如果使用独生子女护理假大于去年冻结独生子女护理假，就需要分段扣掉
            {
                double temp = nxj - this.LastFreezonDay;//算出还需要在今年冻结独生子女护理假扣掉的天数
                this.LastFreezonDay -= this.LastFreezonDay;//把去年冻结的独生子女护理假全部扣掉
                this.FreezonDay -= temp;//扣掉今年冻结
            }
            else//如果使用独生子女护理假小于或等于去年冻结的，只要在去年冻结扣掉即可
            {
                this.LastFreezonDay -= nxj;//扣掉扣掉扣掉
            }

            //将更改写入日志中
            allog.LogChange(this, "解除独生子女护理假冻结", nxj, "请假正常结束，解除独生子女护理假的冻结" + nxj + "天");

            this.Update();
        }

        /// <summary>
        /// 在独生子女护理假管理页面修改独生子女护理假
        /// </summary>
        /// <param name="username"></param>
        public void nxjmodify(string username, string bz)
        {
            ZWL.BLL.DuShengLeave tempmodel = new ZWL.BLL.DuShengLeave();
            tempmodel.GetModel(this.ID);

            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(tempmodel);

            //将更改写入日志中
            allog.LogChange(this, "修改独生子女护理假", 0, username + "修改独生子女护理假,备注：" + bz);

            this.Update();
        }

        /// <summary>
        /// 在独生子女护理假管理页面导入独生子女护理假
        /// </summary>
        /// <param name="nowremain"></param>
        /// <param name="lastremain"></param>
        public void Importnxj(string nowremain, string lastremain, string username)
        {
            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(this);
            this.NowUsedDays = 0;
            this.NowRemainDays = double.Parse(nowremain);
            this.LastUsedDays = 0;
            this.LastRemainDays = Double.Parse(lastremain);
            this.FreezonDay = 0;
            this.LastFreezonDay = 0;

            //将更改写入日志中
            allog.LogChange(this, "导入独生子女护理假", 0, username + "导入独生子女护理假");
            this.Update();
            //ZWL.DBUtility.DbHelperSQL.ExecuteSQL("UPDATE [DuShengLeave] SET [NowUsedDays] = 0 ,[NowRemainDays] = " + nowremain + " ,[LastUsedDays] = 0 ,[LastRemainDays] = " + lastremain + " ,[FreezonDay] = 0 ,[LastFreezonDay] = 0 WHERE [UserName] = '" + name + "'");

        }
        
        /// <summary>
        /// 跨年的时候更新工龄与独生子女护理假(全体人员)
        /// </summary>
        public void UpdateWorkYearAndNXJ()
        {
            ZWL.BLL.DuShengLeave model = new ZWL.BLL.DuShengLeave();
            DataSet ds = new DataSet();
            string strwhere = "";
            ds = model.GetList(strwhere);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int nid = int.Parse(ds.Tables[0].Rows[i]["ID"].ToString());
                    this.UpdateWorkYearAndNXJ(nid);
                }
            }
        }

        /// <summary>
        /// 单个人员
        /// </summary>
        /// <param name="nid"></param>
        public void UpdateWorkYearAndNXJ(int nid)
        {
            ZWL.BLL.DuShengLeave tempmodel = new ZWL.BLL.DuShengLeave();
            tempmodel.GetModel(nid);

            //初始化更改日志类，记录更改前的数据
            var allog = new DuShengLeaveChangeLog(tempmodel);

            DateTime worktime = new DateTime();
            worktime = tempmodel.JionTime;//参加工作时间
            DateTime nowtime = new DateTime();
            nowtime = DateTime.Now;
            int gongling = Math.Abs(nowtime.Year - worktime.Year);
            //计算独生子女护理假
            tempmodel.NowWorkAge = gongling;
            tempmodel.NowDays = GetDuShengLeave(worktime);

            //每年1月1日时，今年未休独生子女护理假变成去年未休独生子女护理假
            tempmodel.LastRemainDays = tempmodel.NowRemainDays;
            tempmodel.LastUsedDays = 0;
            tempmodel.LastFreezonDay = tempmodel.FreezonDay;
            tempmodel.NowRemainDays = tempmodel.NowDays;
            tempmodel.FreezonDay = 0;
            tempmodel.NowUsedDays = 0;

            //将更改写入日志中
            allog.LogChange(tempmodel, "跨年更新工龄与独生子女护理假", 0, "跨年更新工龄与独生子女护理假");

            //更新工龄和独生子女护理假
            tempmodel.Update();
        }

        /// <summary>
        /// 根据工作时间计算独生子女护理假
        /// </summary>
        /// <returns></returns>
        public double GetDuShengLeave(DateTime worktime)
        {
            //独生子女护理假：一般5天，住院护理加10天
            var dNowDays = 5 + ZYHLJDays;

            return dNowDays;
        }

        /// <summary>
        /// 计算特殊情况下的独生子女护理假10年和20年的
        /// </summary>
        /// <param name="nmonth"></param>
        /// <returns></returns>
        public double GetSpacilLeave(int nmonth, int year)
        {
            double nLeave = 0.0;
            ArrayList canshu = new ArrayList();
            if (year == 10)
            {
                //10年
                canshu.Add(5);
                canshu.Add(10);
            }
            else
            {
                //20年
                canshu.Add(10);
                canshu.Add(15);
            }
            int midday = (nmonth - 1) * 30;
            double dnxj = (midday / 365.0) * (int)canshu[0] + ((365 - midday) / 365.0) * (int)canshu[1];
            //double dnxj = (double)canshu[1] - midday * ((int)canshu[0] / 365.0);
            nLeave = Math.Floor(dnxj);

            return nLeave;
        }
        
        /// <summary>
        /// 可用独生子女护理假
        /// </summary>
        /// <returns></returns>
        public double GetKyNxj()
        {
            return this.NowRemainDays + this.LastRemainDays + this.FreezonDay + this.LastFreezonDay - this.LastUsedDays;
            //return this.NowRemainDays + this.FreezonDay;
        }

        /// <summary>
        /// 实际可用的独生子女护理假天数
        /// </summary>
        /// <returns></returns>
        public double GetSjkyNxj()
        {
            return this.NowRemainDays + this.LastRemainDays - this.LastUsedDays;
            //return this.NowRemainDays;
        }

        /// <summary>
        /// 今年已经休的独生子女护理假（包括已休去年剩余的和今年可用的独生子女护理假）
        /// </summary>
        /// <returns></returns>
        public double GetDayallused()
        {
            return this.NowUsedDays + this.LastUsedDays;
            //return this.NowUsedDays;
        }

        /// <summary>
        /// 冻结独生子女护理假
        /// </summary>
        /// <returns></returns>
        public double GetFreezon()
        {
            return this.FreezonDay + this.LastFreezonDay;
            //return this.FreezonDay;
        }

        /// <summary>
        /// 获取上年可用独生子女护理假
        /// </summary>
        /// <returns></returns>
        public double GetLastyear()
        {
            return this.LastRemainDays - this.LastUsedDays;
            //return 0;
        }

        public static void UpdateLastLog(int nWorkId)
        {
            DbHelperSQL.ExecuteSQL(string.Format("UpDate DuShengLeaveChangeLog Set BZ=BZ+',职工请假审批:{0}' WHERE ID='{1}' AND ChangeType='扣除独生子女护理假'", nWorkId, DbHelperSQL.GetMaxID("ID", "DuShengLeaveChangeLog")));
        }
    }
}
