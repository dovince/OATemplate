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
    /// 类DuShengLeaveChangeLog。
    /// </summary>
    [Serializable]
    public class DuShengLeaveChangeLog
    {
        public DuShengLeaveChangeLog()
        { }
		#region Model
		private int _id;
		private DateTime _currentdate = DateTime.Now;
		private string _username;
		private string _department;
		private DateTime _jiontime;
		private int _nowworkage;
		private int _nowworkageafter;
		private double _nowdays;
		private double _nowdaysafter;
		private double _nowuseddays;
		private double _nowuseddaysafter;
		private double _nowremaindays;
		private double _nowremaindaysafter;
		private double _lastremaindays;
		private double _lastremaindaysafter;
		private double _lastuseddays;
		private double _lastuseddaysafter;
		private double _freezonday;
		private double _freezondayafter;
		private double _lastfreezonday;
		private double _lastfreezondayafter;
		private string _fujianlist;
		private string _fujianlistafter;
		private string _changetype;
		private double _change;
		private string _bz;
		private double _zyhljdays;
		private double _zyhljdaysafter;
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
		public DateTime CurrentDate
		{
			set { _currentdate = value; }
			get { return _currentdate; }
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
		public string Department
		{
			set { _department = value; }
			get { return _department; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime JionTime
		{
			set { _jiontime = value; }
			get { return _jiontime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int NowWorkAge
		{
			set { _nowworkage = value; }
			get { return _nowworkage; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int NowWorkAgeAfter
		{
			set { _nowworkageafter = value; }
			get { return _nowworkageafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowDays
		{
			set { _nowdays = value; }
			get { return _nowdays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowDaysAfter
		{
			set { _nowdaysafter = value; }
			get { return _nowdaysafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowUsedDays
		{
			set { _nowuseddays = value; }
			get { return _nowuseddays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowUsedDaysAfter
		{
			set { _nowuseddaysafter = value; }
			get { return _nowuseddaysafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowRemainDays
		{
			set { _nowremaindays = value; }
			get { return _nowremaindays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double NowRemainDaysAfter
		{
			set { _nowremaindaysafter = value; }
			get { return _nowremaindaysafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastRemainDays
		{
			set { _lastremaindays = value; }
			get { return _lastremaindays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastRemainDaysAfter
		{
			set { _lastremaindaysafter = value; }
			get { return _lastremaindaysafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastUsedDays
		{
			set { _lastuseddays = value; }
			get { return _lastuseddays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastUsedDaysAfter
		{
			set { _lastuseddaysafter = value; }
			get { return _lastuseddaysafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double FreezonDay
		{
			set { _freezonday = value; }
			get { return _freezonday; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double FreezonDayAfter
		{
			set { _freezondayafter = value; }
			get { return _freezondayafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastFreezonDay
		{
			set { _lastfreezonday = value; }
			get { return _lastfreezonday; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double LastFreezonDayAfter
		{
			set { _lastfreezondayafter = value; }
			get { return _lastfreezondayafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuJianList
		{
			set { _fujianlist = value; }
			get { return _fujianlist; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuJianListAfter
		{
			set { _fujianlistafter = value; }
			get { return _fujianlistafter; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeType
		{
			set { _changetype = value; }
			get { return _changetype; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double Change
		{
			set { _change = value; }
			get { return _change; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string BZ
		{
			set { _bz = value; }
			get { return _bz; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double ZYHLJDays
		{
			set { _zyhljdays = value; }
			get { return _zyhljdays; }
		}
		/// <summary>
		/// 
		/// </summary>
		public double ZYHLJDaysAfter
		{
			set { _zyhljdaysafter = value; }
			get { return _zyhljdaysafter; }
		}
		#endregion Model


		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DuShengLeaveChangeLog(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,CurrentDate,UserName,Department,JionTime,NowWorkAge,NowWorkAgeAfter,NowDays,NowDaysAfter,NowUsedDays,NowUsedDaysAfter,NowRemainDays,NowRemainDaysAfter,LastRemainDays,LastRemainDaysAfter,LastUsedDays,LastUsedDaysAfter,FreezonDay,FreezonDayAfter,LastFreezonDay,LastFreezonDayAfter,FuJianList,FuJianListAfter,ChangeType,Change,BZ,ZYHLJDays,ZYHLJDaysAfter ");
			strSql.Append(" FROM [DuShengLeaveChangeLog] ");
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
				if (ds.Tables[0].Rows[0]["CurrentDate"] != null && ds.Tables[0].Rows[0]["CurrentDate"].ToString() != "")
				{
					this.CurrentDate = DateTime.Parse(ds.Tables[0].Rows[0]["CurrentDate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null)
				{
					this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Department"] != null)
				{
					this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
				}
				if (ds.Tables[0].Rows[0]["JionTime"] != null && ds.Tables[0].Rows[0]["JionTime"].ToString() != "")
				{
					this.JionTime = DateTime.Parse(ds.Tables[0].Rows[0]["JionTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowWorkAge"] != null && ds.Tables[0].Rows[0]["NowWorkAge"].ToString() != "")
				{
					this.NowWorkAge = int.Parse(ds.Tables[0].Rows[0]["NowWorkAge"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowWorkAgeAfter"] != null && ds.Tables[0].Rows[0]["NowWorkAgeAfter"].ToString() != "")
				{
					this.NowWorkAgeAfter = int.Parse(ds.Tables[0].Rows[0]["NowWorkAgeAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowDays"] != null && ds.Tables[0].Rows[0]["NowDays"].ToString() != "")
				{
					this.NowDays = double.Parse(ds.Tables[0].Rows[0]["NowDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowDaysAfter"] != null && ds.Tables[0].Rows[0]["NowDaysAfter"].ToString() != "")
				{
					this.NowDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowUsedDays"] != null && ds.Tables[0].Rows[0]["NowUsedDays"].ToString() != "")
				{
					this.NowUsedDays = double.Parse(ds.Tables[0].Rows[0]["NowUsedDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowUsedDaysAfter"] != null && ds.Tables[0].Rows[0]["NowUsedDaysAfter"].ToString() != "")
				{
					this.NowUsedDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowUsedDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowRemainDays"] != null && ds.Tables[0].Rows[0]["NowRemainDays"].ToString() != "")
				{
					this.NowRemainDays = double.Parse(ds.Tables[0].Rows[0]["NowRemainDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowRemainDaysAfter"] != null && ds.Tables[0].Rows[0]["NowRemainDaysAfter"].ToString() != "")
				{
					this.NowRemainDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowRemainDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastRemainDays"] != null && ds.Tables[0].Rows[0]["LastRemainDays"].ToString() != "")
				{
					this.LastRemainDays = double.Parse(ds.Tables[0].Rows[0]["LastRemainDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastRemainDaysAfter"] != null && ds.Tables[0].Rows[0]["LastRemainDaysAfter"].ToString() != "")
				{
					this.LastRemainDaysAfter = double.Parse(ds.Tables[0].Rows[0]["LastRemainDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastUsedDays"] != null && ds.Tables[0].Rows[0]["LastUsedDays"].ToString() != "")
				{
					this.LastUsedDays = double.Parse(ds.Tables[0].Rows[0]["LastUsedDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastUsedDaysAfter"] != null && ds.Tables[0].Rows[0]["LastUsedDaysAfter"].ToString() != "")
				{
					this.LastUsedDaysAfter = double.Parse(ds.Tables[0].Rows[0]["LastUsedDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FreezonDay"] != null && ds.Tables[0].Rows[0]["FreezonDay"].ToString() != "")
				{
					this.FreezonDay = double.Parse(ds.Tables[0].Rows[0]["FreezonDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FreezonDayAfter"] != null && ds.Tables[0].Rows[0]["FreezonDayAfter"].ToString() != "")
				{
					this.FreezonDayAfter = double.Parse(ds.Tables[0].Rows[0]["FreezonDayAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastFreezonDay"] != null && ds.Tables[0].Rows[0]["LastFreezonDay"].ToString() != "")
				{
					this.LastFreezonDay = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastFreezonDayAfter"] != null && ds.Tables[0].Rows[0]["LastFreezonDayAfter"].ToString() != "")
				{
					this.LastFreezonDayAfter = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDayAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FuJianList"] != null)
				{
					this.FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FuJianListAfter"] != null)
				{
					this.FuJianListAfter = ds.Tables[0].Rows[0]["FuJianListAfter"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ChangeType"] != null)
				{
					this.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Change"] != null && ds.Tables[0].Rows[0]["Change"].ToString() != "")
				{
					this.Change = double.Parse(ds.Tables[0].Rows[0]["Change"].ToString());
				}
				if (ds.Tables[0].Rows[0]["BZ"] != null)
				{
					this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ZYHLJDays"] != null && ds.Tables[0].Rows[0]["ZYHLJDays"].ToString() != "")
				{
					this.ZYHLJDays = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ZYHLJDaysAfter"] != null && ds.Tables[0].Rows[0]["ZYHLJDaysAfter"].ToString() != "")
				{
					this.ZYHLJDaysAfter = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDaysAfter"].ToString());
				}
			}
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from [DuShengLeaveChangeLog]");
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
			strSql.Append("insert into [DuShengLeaveChangeLog] (");
			strSql.Append("CurrentDate,UserName,Department,JionTime,NowWorkAge,NowWorkAgeAfter,NowDays,NowDaysAfter,NowUsedDays,NowUsedDaysAfter,NowRemainDays,NowRemainDaysAfter,LastRemainDays,LastRemainDaysAfter,LastUsedDays,LastUsedDaysAfter,FreezonDay,FreezonDayAfter,LastFreezonDay,LastFreezonDayAfter,FuJianList,FuJianListAfter,ChangeType,Change,BZ,ZYHLJDays,ZYHLJDaysAfter)");
			strSql.Append(" values (");
			strSql.Append("@CurrentDate,@UserName,@Department,@JionTime,@NowWorkAge,@NowWorkAgeAfter,@NowDays,@NowDaysAfter,@NowUsedDays,@NowUsedDaysAfter,@NowRemainDays,@NowRemainDaysAfter,@LastRemainDays,@LastRemainDaysAfter,@LastUsedDays,@LastUsedDaysAfter,@FreezonDay,@FreezonDayAfter,@LastFreezonDay,@LastFreezonDayAfter,@FuJianList,@FuJianListAfter,@ChangeType,@Change,@BZ,@ZYHLJDays,@ZYHLJDaysAfter)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Department", SqlDbType.NVarChar,200),
					new SqlParameter("@JionTime", SqlDbType.DateTime),
					new SqlParameter("@NowWorkAge", SqlDbType.Int,4),
					new SqlParameter("@NowWorkAgeAfter", SqlDbType.Int,4),
					new SqlParameter("@NowDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@NowUsedDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowUsedDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@NowRemainDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowRemainDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastRemainDays", SqlDbType.Decimal,9),
					new SqlParameter("@LastRemainDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastUsedDays", SqlDbType.Decimal,9),
					new SqlParameter("@LastUsedDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@FreezonDay", SqlDbType.Decimal,9),
					new SqlParameter("@FreezonDayAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastFreezonDay", SqlDbType.Decimal,9),
					new SqlParameter("@LastFreezonDayAfter", SqlDbType.Decimal,9),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@FuJianListAfter", SqlDbType.VarChar,5000),
					new SqlParameter("@ChangeType", SqlDbType.NVarChar,200),
					new SqlParameter("@Change", SqlDbType.Decimal,9),
					new SqlParameter("@BZ", SqlDbType.NVarChar,200),
					new SqlParameter("@ZYHLJDays", SqlDbType.Decimal,9),
					new SqlParameter("@ZYHLJDaysAfter", SqlDbType.Decimal,9)};
			parameters[0].Value = CurrentDate;
			parameters[1].Value = UserName;
			parameters[2].Value = Department;
			parameters[3].Value = JionTime;
			parameters[4].Value = NowWorkAge;
			parameters[5].Value = NowWorkAgeAfter;
			parameters[6].Value = NowDays;
			parameters[7].Value = NowDaysAfter;
			parameters[8].Value = NowUsedDays;
			parameters[9].Value = NowUsedDaysAfter;
			parameters[10].Value = NowRemainDays;
			parameters[11].Value = NowRemainDaysAfter;
			parameters[12].Value = LastRemainDays;
			parameters[13].Value = LastRemainDaysAfter;
			parameters[14].Value = LastUsedDays;
			parameters[15].Value = LastUsedDaysAfter;
			parameters[16].Value = FreezonDay;
			parameters[17].Value = FreezonDayAfter;
			parameters[18].Value = LastFreezonDay;
			parameters[19].Value = LastFreezonDayAfter;
			parameters[20].Value = FuJianList;
			parameters[21].Value = FuJianListAfter;
			parameters[22].Value = ChangeType;
			parameters[23].Value = Change;
			parameters[24].Value = BZ;
			parameters[25].Value = ZYHLJDays;
			parameters[26].Value = ZYHLJDaysAfter;

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
			strSql.Append("update [DuShengLeaveChangeLog] set ");
			strSql.Append("CurrentDate=@CurrentDate,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Department=@Department,");
			strSql.Append("JionTime=@JionTime,");
			strSql.Append("NowWorkAge=@NowWorkAge,");
			strSql.Append("NowWorkAgeAfter=@NowWorkAgeAfter,");
			strSql.Append("NowDays=@NowDays,");
			strSql.Append("NowDaysAfter=@NowDaysAfter,");
			strSql.Append("NowUsedDays=@NowUsedDays,");
			strSql.Append("NowUsedDaysAfter=@NowUsedDaysAfter,");
			strSql.Append("NowRemainDays=@NowRemainDays,");
			strSql.Append("NowRemainDaysAfter=@NowRemainDaysAfter,");
			strSql.Append("LastRemainDays=@LastRemainDays,");
			strSql.Append("LastRemainDaysAfter=@LastRemainDaysAfter,");
			strSql.Append("LastUsedDays=@LastUsedDays,");
			strSql.Append("LastUsedDaysAfter=@LastUsedDaysAfter,");
			strSql.Append("FreezonDay=@FreezonDay,");
			strSql.Append("FreezonDayAfter=@FreezonDayAfter,");
			strSql.Append("LastFreezonDay=@LastFreezonDay,");
			strSql.Append("LastFreezonDayAfter=@LastFreezonDayAfter,");
			strSql.Append("FuJianList=@FuJianList,");
			strSql.Append("FuJianListAfter=@FuJianListAfter,");
			strSql.Append("ChangeType=@ChangeType,");
			strSql.Append("Change=@Change,");
			strSql.Append("BZ=@BZ,");
			strSql.Append("ZYHLJDays=@ZYHLJDays,");
			strSql.Append("ZYHLJDaysAfter=@ZYHLJDaysAfter");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Department", SqlDbType.NVarChar,200),
					new SqlParameter("@JionTime", SqlDbType.DateTime),
					new SqlParameter("@NowWorkAge", SqlDbType.Int,4),
					new SqlParameter("@NowWorkAgeAfter", SqlDbType.Int,4),
					new SqlParameter("@NowDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@NowUsedDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowUsedDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@NowRemainDays", SqlDbType.Decimal,9),
					new SqlParameter("@NowRemainDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastRemainDays", SqlDbType.Decimal,9),
					new SqlParameter("@LastRemainDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastUsedDays", SqlDbType.Decimal,9),
					new SqlParameter("@LastUsedDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@FreezonDay", SqlDbType.Decimal,9),
					new SqlParameter("@FreezonDayAfter", SqlDbType.Decimal,9),
					new SqlParameter("@LastFreezonDay", SqlDbType.Decimal,9),
					new SqlParameter("@LastFreezonDayAfter", SqlDbType.Decimal,9),
					new SqlParameter("@FuJianList", SqlDbType.VarChar,5000),
					new SqlParameter("@FuJianListAfter", SqlDbType.VarChar,5000),
					new SqlParameter("@ChangeType", SqlDbType.NVarChar,200),
					new SqlParameter("@Change", SqlDbType.Decimal,9),
					new SqlParameter("@BZ", SqlDbType.NVarChar,200),
					new SqlParameter("@ZYHLJDays", SqlDbType.Decimal,9),
					new SqlParameter("@ZYHLJDaysAfter", SqlDbType.Decimal,9),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = CurrentDate;
			parameters[1].Value = UserName;
			parameters[2].Value = Department;
			parameters[3].Value = JionTime;
			parameters[4].Value = NowWorkAge;
			parameters[5].Value = NowWorkAgeAfter;
			parameters[6].Value = NowDays;
			parameters[7].Value = NowDaysAfter;
			parameters[8].Value = NowUsedDays;
			parameters[9].Value = NowUsedDaysAfter;
			parameters[10].Value = NowRemainDays;
			parameters[11].Value = NowRemainDaysAfter;
			parameters[12].Value = LastRemainDays;
			parameters[13].Value = LastRemainDaysAfter;
			parameters[14].Value = LastUsedDays;
			parameters[15].Value = LastUsedDaysAfter;
			parameters[16].Value = FreezonDay;
			parameters[17].Value = FreezonDayAfter;
			parameters[18].Value = LastFreezonDay;
			parameters[19].Value = LastFreezonDayAfter;
			parameters[20].Value = FuJianList;
			parameters[21].Value = FuJianListAfter;
			parameters[22].Value = ChangeType;
			parameters[23].Value = Change;
			parameters[24].Value = BZ;
			parameters[25].Value = ZYHLJDays;
			parameters[26].Value = ZYHLJDaysAfter;
			parameters[27].Value = ID;

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
			strSql.Append("delete from [DuShengLeaveChangeLog] ");
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
			strSql.Append("select ID,CurrentDate,UserName,Department,JionTime,NowWorkAge,NowWorkAgeAfter,NowDays,NowDaysAfter,NowUsedDays,NowUsedDaysAfter,NowRemainDays,NowRemainDaysAfter,LastRemainDays,LastRemainDaysAfter,LastUsedDays,LastUsedDaysAfter,FreezonDay,FreezonDayAfter,LastFreezonDay,LastFreezonDayAfter,FuJianList,FuJianListAfter,ChangeType,Change,BZ,ZYHLJDays,ZYHLJDaysAfter ");
			strSql.Append(" FROM [DuShengLeaveChangeLog] ");
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
				if (ds.Tables[0].Rows[0]["CurrentDate"] != null && ds.Tables[0].Rows[0]["CurrentDate"].ToString() != "")
				{
					this.CurrentDate = DateTime.Parse(ds.Tables[0].Rows[0]["CurrentDate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null)
				{
					this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Department"] != null)
				{
					this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
				}
				if (ds.Tables[0].Rows[0]["JionTime"] != null && ds.Tables[0].Rows[0]["JionTime"].ToString() != "")
				{
					this.JionTime = DateTime.Parse(ds.Tables[0].Rows[0]["JionTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowWorkAge"] != null && ds.Tables[0].Rows[0]["NowWorkAge"].ToString() != "")
				{
					this.NowWorkAge = int.Parse(ds.Tables[0].Rows[0]["NowWorkAge"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowWorkAgeAfter"] != null && ds.Tables[0].Rows[0]["NowWorkAgeAfter"].ToString() != "")
				{
					this.NowWorkAgeAfter = int.Parse(ds.Tables[0].Rows[0]["NowWorkAgeAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowDays"] != null && ds.Tables[0].Rows[0]["NowDays"].ToString() != "")
				{
					this.NowDays = double.Parse(ds.Tables[0].Rows[0]["NowDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowDaysAfter"] != null && ds.Tables[0].Rows[0]["NowDaysAfter"].ToString() != "")
				{
					this.NowDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowUsedDays"] != null && ds.Tables[0].Rows[0]["NowUsedDays"].ToString() != "")
				{
					this.NowUsedDays = double.Parse(ds.Tables[0].Rows[0]["NowUsedDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowUsedDaysAfter"] != null && ds.Tables[0].Rows[0]["NowUsedDaysAfter"].ToString() != "")
				{
					this.NowUsedDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowUsedDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowRemainDays"] != null && ds.Tables[0].Rows[0]["NowRemainDays"].ToString() != "")
				{
					this.NowRemainDays = double.Parse(ds.Tables[0].Rows[0]["NowRemainDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NowRemainDaysAfter"] != null && ds.Tables[0].Rows[0]["NowRemainDaysAfter"].ToString() != "")
				{
					this.NowRemainDaysAfter = double.Parse(ds.Tables[0].Rows[0]["NowRemainDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastRemainDays"] != null && ds.Tables[0].Rows[0]["LastRemainDays"].ToString() != "")
				{
					this.LastRemainDays = double.Parse(ds.Tables[0].Rows[0]["LastRemainDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastRemainDaysAfter"] != null && ds.Tables[0].Rows[0]["LastRemainDaysAfter"].ToString() != "")
				{
					this.LastRemainDaysAfter = double.Parse(ds.Tables[0].Rows[0]["LastRemainDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastUsedDays"] != null && ds.Tables[0].Rows[0]["LastUsedDays"].ToString() != "")
				{
					this.LastUsedDays = double.Parse(ds.Tables[0].Rows[0]["LastUsedDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastUsedDaysAfter"] != null && ds.Tables[0].Rows[0]["LastUsedDaysAfter"].ToString() != "")
				{
					this.LastUsedDaysAfter = double.Parse(ds.Tables[0].Rows[0]["LastUsedDaysAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FreezonDay"] != null && ds.Tables[0].Rows[0]["FreezonDay"].ToString() != "")
				{
					this.FreezonDay = double.Parse(ds.Tables[0].Rows[0]["FreezonDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FreezonDayAfter"] != null && ds.Tables[0].Rows[0]["FreezonDayAfter"].ToString() != "")
				{
					this.FreezonDayAfter = double.Parse(ds.Tables[0].Rows[0]["FreezonDayAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastFreezonDay"] != null && ds.Tables[0].Rows[0]["LastFreezonDay"].ToString() != "")
				{
					this.LastFreezonDay = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LastFreezonDayAfter"] != null && ds.Tables[0].Rows[0]["LastFreezonDayAfter"].ToString() != "")
				{
					this.LastFreezonDayAfter = double.Parse(ds.Tables[0].Rows[0]["LastFreezonDayAfter"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FuJianList"] != null)
				{
					this.FuJianList = ds.Tables[0].Rows[0]["FuJianList"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FuJianListAfter"] != null)
				{
					this.FuJianListAfter = ds.Tables[0].Rows[0]["FuJianListAfter"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ChangeType"] != null)
				{
					this.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Change"] != null && ds.Tables[0].Rows[0]["Change"].ToString() != "")
				{
					this.Change = double.Parse(ds.Tables[0].Rows[0]["Change"].ToString());
				}
				if (ds.Tables[0].Rows[0]["BZ"] != null)
				{
					this.BZ = ds.Tables[0].Rows[0]["BZ"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ZYHLJDays"] != null && ds.Tables[0].Rows[0]["ZYHLJDays"].ToString() != "")
				{
					this.ZYHLJDays = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDays"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ZYHLJDaysAfter"] != null && ds.Tables[0].Rows[0]["ZYHLJDaysAfter"].ToString() != "")
				{
					this.ZYHLJDaysAfter = double.Parse(ds.Tables[0].Rows[0]["ZYHLJDaysAfter"].ToString());
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
			strSql.Append(" FROM [DuShengLeaveChangeLog] ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method

		public DuShengLeaveChangeLog(DuShengLeave al)
        {
            this.UserName = al.UserName;
            this.Department = al.Department;
            this.JionTime = al.JionTime;
            this.NowWorkAge = al.NowWorkAge;
            this.NowDays = al.NowDays;
            this.NowUsedDays = al.NowUsedDays;
            this.NowRemainDays = al.NowRemainDays;
            this.LastRemainDays = al.LastRemainDays;
            this.LastUsedDays = al.LastUsedDays;
            this.FreezonDay = al.FreezonDay;
            this.LastFreezonDay = al.LastFreezonDay;
            this.FuJianList = al.FuJianList;
            this.ZYHLJDays = al.ZYHLJDays;
		}

        public void LogChange(DuShengLeave DuShengLeave, string changeType, double nxj, string bz = "")
        {
            this.NowWorkAgeAfter = DuShengLeave.NowWorkAge;
            this.NowDaysAfter = DuShengLeave.NowDays;
            this.NowUsedDaysAfter = DuShengLeave.NowUsedDays;
            this.NowRemainDaysAfter = DuShengLeave.NowRemainDays;
            this.LastRemainDaysAfter = DuShengLeave.LastRemainDays;
            this.LastUsedDaysAfter = DuShengLeave.LastUsedDays;
            this.FreezonDayAfter = DuShengLeave.FreezonDay;
            this.LastFreezonDayAfter = DuShengLeave.LastFreezonDay;
            this.FuJianListAfter = DuShengLeave.FuJianList;
            this.ZYHLJDaysAfter = DuShengLeave.ZYHLJDays;
			this.ChangeType = changeType;
            this.Change = nxj;
            this.BZ = bz;
            this.Add();
        }

        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM DuShengLeaveChangeLog");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }
    }
}
