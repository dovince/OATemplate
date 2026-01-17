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
	/// 类ERPXMQQDJ。
	/// </summary>
	[Serializable]
    public partial class ERPXMQQDJ
    {
        public ERPXMQQDJ()
        { }
        #region Model
        private int _id;
        private string _xmbh;
        private string _xmname;
        private string _jyfs;
        private string _xmadress;
        private string _wtdwname;
        private string _wtdwlxr;
        private string _wtdwlxdh;
        private string _wtfs;
        private DateTime _yjtbtime;
        private string _wttbdw;
        private string _zytype;
        private string _hytype;
        private double _yjxmzj;
        private string _xmzjly;
        private DateTime _xmbegintime;
        private DateTime _xmendtime;
        private string _hzdwname;
        private string _hzdwlxr;
        private string _hzdwlxdh;
        private string _djbm;
        private string _djr;
        private DateTime _djsj;
        private string _workname;
        private double _yjxmdj;
        private DateTime _gxtime;
        private int? _nworkid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string XMName
        {
            set { _xmname = value; }
            get { return _xmname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYFS
        {
            set { _jyfs = value; }
            get { return _jyfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMAdress
        {
            set { _xmadress = value; }
            get { return _xmadress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WTDWName
        {
            set { _wtdwname = value; }
            get { return _wtdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WTDWLXR
        {
            set { _wtdwlxr = value; }
            get { return _wtdwlxr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WTDWLXDH
        {
            set { _wtdwlxdh = value; }
            get { return _wtdwlxdh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WTFS
        {
            set { _wtfs = value; }
            get { return _wtfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime YJTBTime
        {
            set { _yjtbtime = value; }
            get { return _yjtbtime; }
        }
        /// <summary>
        /// 委托投标单位
        /// </summary>
        public string WTTBDW
        {
            set { _wttbdw = value; }
            get { return _wttbdw; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string ZYType
        {
            set { _zytype = value; }
            get { return _zytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HYType
        {
            set { _hytype = value; }
            get { return _hytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double YJXMZJ
        {
            set { _yjxmzj = value; }
            get { return _yjxmzj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMZJLY
        {
            set { _xmzjly = value; }
            get { return _xmzjly; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime XMBeginTime
        {
            set { _xmbegintime = value; }
            get { return _xmbegintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime XMEndTime
        {
            set { _xmendtime = value; }
            get { return _xmendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HZDWName
        {
            set { _hzdwname = value; }
            get { return _hzdwname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HZDWLXR
        {
            set { _hzdwlxr = value; }
            get { return _hzdwlxr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HZDWLXDH
        {
            set { _hzdwlxdh = value; }
            get { return _hzdwlxdh; }
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
        public string DJR
        {
            set { _djr = value; }
            get { return _djr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DJSJ
        {
            set { _djsj = value; }
            get { return _djsj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WORKNAME
        {
            set { _workname = value; }
            get { return _workname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double YJXMDJ
        {
            set { _yjxmdj = value; }
            get { return _yjxmdj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime GXTime
        {
            set { _gxtime = value; }
            get { return _gxtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nworkid
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPXMQQDJ(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQQDJ] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public ERPXMQQDJ(string xmID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPXMQQDJ ");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
            parameters[0].Value = xmID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPXMQQDJ]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public bool Exists(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPXMQQDJ");
            strSql.Append(" where XMBH=@XMBH ");

            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
            parameters[0].Value = strxmbh;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPXMQQDJ] (");
            strSql.Append("XMBH,XMName,JYFS,XMAdress,WTDWName,WTDWLXR,WTDWLXDH,WTFS,YJTBTime,WTTBDW,ZYType,HYType,YJXMZJ,XMZJLY,XMBeginTime,XMEndTime,HZDWName,HZDWLXR,HZDWLXDH,DJBM,DJR,DJSJ,WORKNAME,YJXMDJ,GXTime,nworkid)");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@XMName,@JYFS,@XMAdress,@WTDWName,@WTDWLXR,@WTDWLXDH,@WTFS,@YJTBTime,@WTTBDW,@ZYType,@HYType,@YJXMZJ,@XMZJLY,@XMBeginTime,@XMEndTime,@HZDWName,@HZDWLXR,@HZDWLXDH,@DJBM,@DJR,@DJSJ,@WORKNAME,@YJXMDJ,@GXTime,@nworkid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@JYFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@YJTBTime", SqlDbType.DateTime),
                    new SqlParameter("@WTTBDW", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZYType", SqlDbType.NVarChar,20),
                    new SqlParameter("@HYType", SqlDbType.NVarChar,20),
                    new SqlParameter("@YJXMZJ", SqlDbType.Float,8),
                    new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime", SqlDbType.DateTime),
                    new SqlParameter("@HZDWName", SqlDbType.NVarChar,50),
                    new SqlParameter("@HZDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,200),
                    new SqlParameter("@DJSJ", SqlDbType.DateTime),
                    new SqlParameter("@WORKNAME", SqlDbType.NVarChar,50),
                    new SqlParameter("@YJXMDJ", SqlDbType.Float,8),
                    new SqlParameter("@GXTime", SqlDbType.DateTime),
                    new SqlParameter("@nworkid", SqlDbType.Int,4)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = JYFS;
            parameters[3].Value = XMAdress;
            parameters[4].Value = WTDWName;
            parameters[5].Value = WTDWLXR;
            parameters[6].Value = WTDWLXDH;
            parameters[7].Value = WTFS;
            parameters[8].Value = YJTBTime;
            parameters[9].Value = WTTBDW;
            parameters[10].Value = ZYType;
            parameters[11].Value = HYType;
            parameters[12].Value = YJXMZJ;
            parameters[13].Value = XMZJLY;
            parameters[14].Value = XMBeginTime;
            parameters[15].Value = XMEndTime;
            parameters[16].Value = HZDWName;
            parameters[17].Value = HZDWLXR;
            parameters[18].Value = HZDWLXDH;
            parameters[19].Value = DJBM;
            parameters[20].Value = DJR;
            parameters[21].Value = DJSJ;
            parameters[22].Value = WORKNAME;
            parameters[23].Value = YJXMDJ;
            parameters[24].Value = GXTime;
            parameters[25].Value = nworkid;

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
            strSql.Append("update [ERPXMQQDJ] set ");
            strSql.Append("XMBH=@XMBH,");
            strSql.Append("XMName=@XMName,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("XMAdress=@XMAdress,");
            strSql.Append("WTDWName=@WTDWName,");
            strSql.Append("WTDWLXR=@WTDWLXR,");
            strSql.Append("WTDWLXDH=@WTDWLXDH,");
            strSql.Append("WTFS=@WTFS,");
            strSql.Append("YJTBTime=@YJTBTime,");
            strSql.Append("WTTBDW=@WTTBDW,");
            strSql.Append("ZYType=@ZYType,");
            strSql.Append("HYType=@HYType,");
            strSql.Append("YJXMZJ=@YJXMZJ,");
            strSql.Append("XMZJLY=@XMZJLY,");
            strSql.Append("XMBeginTime=@XMBeginTime,");
            strSql.Append("XMEndTime=@XMEndTime,");
            strSql.Append("HZDWName=@HZDWName,");
            strSql.Append("HZDWLXR=@HZDWLXR,");
            strSql.Append("HZDWLXDH=@HZDWLXDH,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("DJSJ=@DJSJ,");
            strSql.Append("WORKNAME=@WORKNAME,");
            strSql.Append("YJXMDJ=@YJXMDJ,");
            strSql.Append("GXTime=@GXTime,");
            strSql.Append("nworkid=@nworkid");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@JYFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,-1),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,-1),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@YJTBTime", SqlDbType.DateTime),
                    new SqlParameter("@WTTBDW", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZYType", SqlDbType.NVarChar,20),
                    new SqlParameter("@HYType", SqlDbType.NVarChar,20),
                    new SqlParameter("@YJXMZJ", SqlDbType.Float,8),
                    new SqlParameter("@XMZJLY", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime", SqlDbType.DateTime),
                    new SqlParameter("@HZDWName", SqlDbType.NVarChar,50),
                    new SqlParameter("@HZDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH", SqlDbType.NVarChar,20),
                    new SqlParameter("@DJBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@DJR", SqlDbType.NVarChar,200),
                    new SqlParameter("@DJSJ", SqlDbType.DateTime),
                    new SqlParameter("@WORKNAME", SqlDbType.NVarChar,50),
                    new SqlParameter("@YJXMDJ", SqlDbType.Float,8),
                    new SqlParameter("@GXTime", SqlDbType.DateTime),
                    new SqlParameter("@nworkid", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = XMBH;
            parameters[1].Value = XMName;
            parameters[2].Value = JYFS;
            parameters[3].Value = XMAdress;
            parameters[4].Value = WTDWName;
            parameters[5].Value = WTDWLXR;
            parameters[6].Value = WTDWLXDH;
            parameters[7].Value = WTFS;
            parameters[8].Value = YJTBTime;
            parameters[9].Value = WTTBDW;
            parameters[10].Value = ZYType;
            parameters[11].Value = HYType;
            parameters[12].Value = YJXMZJ;
            parameters[13].Value = XMZJLY;
            parameters[14].Value = XMBeginTime;
            parameters[15].Value = XMEndTime;
            parameters[16].Value = HZDWName;
            parameters[17].Value = HZDWLXR;
            parameters[18].Value = HZDWLXDH;
            parameters[19].Value = DJBM;
            parameters[20].Value = DJR;
            parameters[21].Value = DJSJ;
            parameters[22].Value = WORKNAME;
            parameters[23].Value = YJXMDJ;
            parameters[24].Value = GXTime;
            parameters[25].Value = nworkid;
            parameters[26].Value = ID;

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

        public void UpdateBD(string strxmqqbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPXMQQDJ set ");
            strSql.Append("XMName=@XMName,");
            strSql.Append("JYFS=@JYFS,");
            strSql.Append("WTDWName=@WTDWName,");
            strSql.Append("WTDWLXR=@WTDWLXR,");
            strSql.Append("WTDWLXDH=@WTDWLXDH,");
            strSql.Append("WTFS=@WTFS,");
            strSql.Append("YJTBTime=@YJTBTime,");
            strSql.Append("WTTBDW=@WTTBDW,");
            strSql.Append("ZYType=@ZYType,");
            strSql.Append("HYType=@HYType,");
            strSql.Append("YJXMZJ=@YJXMZJ,");
            strSql.Append("XMZJLY=@XMZJLY,");
            strSql.Append("XMBeginTime=@XMBeginTime,");
            strSql.Append("XMEndTime=@XMEndTime,");
            strSql.Append("HZDWName=@HZDWName,");
            strSql.Append("HZDWLXR=@HZDWLXR,");
            strSql.Append("HZDWLXDH=@HZDWLXDH,");
            strSql.Append("DJBM=@DJBM,");
            strSql.Append("DJR=@DJR,");
            strSql.Append("DJSJ=@DJSJ,");
            strSql.Append("YJXMDJ=@YJXMDJ,");
            strSql.Append("XMAdress=@XMAdress,");
            strSql.Append("WORKNAME=@WORKNAME,");
            strSql.Append("GXTime=@GXTime");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@JYFS", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWName", SqlDbType.NVarChar,200),
                    new SqlParameter("@WTDWLXR", SqlDbType.NVarChar,20),
                    new SqlParameter("@WTDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@WTFS",SqlDbType.NVarChar,20),
                    new SqlParameter("@YJTBTime",SqlDbType.DateTime),
                    new SqlParameter("@WTTBDW",SqlDbType.NVarChar,50),
                    new SqlParameter("@ZYType",SqlDbType.NVarChar,20),
                    new SqlParameter("@HYType",SqlDbType.NVarChar,20),
                    new SqlParameter("@YJXMZJ",SqlDbType.Float),
                    new SqlParameter("@XMZJLY",SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBeginTime",SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime",SqlDbType.DateTime),
                    new SqlParameter("@HZDWName",SqlDbType.NVarChar,50),
                    new SqlParameter("@HZDWLXR",SqlDbType.NVarChar,20),
                    new SqlParameter("@HZDWLXDH",SqlDbType.NVarChar,20),
                    new SqlParameter("@DJBM",SqlDbType.NVarChar,200),
                    new SqlParameter("@DJR",SqlDbType.NVarChar,200),
                    new SqlParameter("@DJSJ",SqlDbType.DateTime),
                    new SqlParameter("@WORKNAME",SqlDbType.NVarChar,50),
                    new SqlParameter("@YJXMDJ",SqlDbType.Float),
                    new SqlParameter("@XMAdress", SqlDbType.NVarChar,1000),
                    new SqlParameter("@GXTime",SqlDbType.DateTime)};
            parameters[0].Value = strxmqqbh;
            parameters[1].Value = XMName;
            parameters[2].Value = JYFS;
            parameters[3].Value = WTDWName;
            parameters[4].Value = WTDWLXR;
            parameters[5].Value = WTDWLXDH;
            parameters[6].Value = WTFS;
            parameters[7].Value = YJTBTime;
            parameters[8].Value = WTTBDW;
            parameters[9].Value = ZYType;
            parameters[10].Value = HYType;
            parameters[11].Value = YJXMZJ;
            parameters[12].Value = XMZJLY;
            parameters[13].Value = XMBeginTime;
            parameters[14].Value = XMEndTime;
            parameters[15].Value = HZDWName;
            parameters[16].Value = HZDWLXR;
            parameters[17].Value = HZDWLXDH;
            parameters[18].Value = DJBM;
            parameters[19].Value = DJR;
            parameters[20].Value = DJSJ;
            parameters[21].Value = WORKNAME;
            parameters[22].Value = YJXMDJ;
            parameters[23].Value = XMAdress;
            parameters[24].Value = GXTime;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ERPXMQQDJ] ");
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
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMName"] != null)
                {
                    this.XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JYFS"] != null)
                {
                    this.JYFS = ds.Tables[0].Rows[0]["JYFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMAdress"] != null)
                {
                    this.XMAdress = ds.Tables[0].Rows[0]["XMAdress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWName"] != null)
                {
                    this.WTDWName = ds.Tables[0].Rows[0]["WTDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWLXR"] != null)
                {
                    this.WTDWLXR = ds.Tables[0].Rows[0]["WTDWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTDWLXDH"] != null)
                {
                    this.WTDWLXDH = ds.Tables[0].Rows[0]["WTDWLXDH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WTFS"] != null)
                {
                    this.WTFS = ds.Tables[0].Rows[0]["WTFS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YJTBTime"] != null && ds.Tables[0].Rows[0]["YJTBTime"].ToString() != "")
                {
                    this.YJTBTime = DateTime.Parse(ds.Tables[0].Rows[0]["YJTBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WTTBDW"] != null)
                {
                    this.WTTBDW = ds.Tables[0].Rows[0]["WTTBDW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYType"] != null)
                {
                    this.ZYType = ds.Tables[0].Rows[0]["ZYType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYType"] != null)
                {
                    this.HYType = ds.Tables[0].Rows[0]["HYType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YJXMZJ"] != null && ds.Tables[0].Rows[0]["YJXMZJ"].ToString() != "")
                {
                    this.YJXMZJ = double.Parse(ds.Tables[0].Rows[0]["YJXMZJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMZJLY"] != null)
                {
                    this.XMZJLY = ds.Tables[0].Rows[0]["XMZJLY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XMBeginTime"] != null && ds.Tables[0].Rows[0]["XMBeginTime"].ToString() != "")
                {
                    this.XMBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMEndTime"] != null && ds.Tables[0].Rows[0]["XMEndTime"].ToString() != "")
                {
                    this.XMEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HZDWName"] != null)
                {
                    this.HZDWName = ds.Tables[0].Rows[0]["HZDWName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDWLXR"] != null)
                {
                    this.HZDWLXR = ds.Tables[0].Rows[0]["HZDWLXR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZDWLXDH"] != null)
                {
                    this.HZDWLXDH = ds.Tables[0].Rows[0]["HZDWLXDH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJBM"] != null)
                {
                    this.DJBM = ds.Tables[0].Rows[0]["DJBM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJR"] != null)
                {
                    this.DJR = ds.Tables[0].Rows[0]["DJR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DJSJ"] != null && ds.Tables[0].Rows[0]["DJSJ"].ToString() != "")
                {
                    this.DJSJ = DateTime.Parse(ds.Tables[0].Rows[0]["DJSJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WORKNAME"] != null)
                {
                    this.WORKNAME = ds.Tables[0].Rows[0]["WORKNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YJXMDJ"] != null && ds.Tables[0].Rows[0]["YJXMDJ"].ToString() != "")
                {
                    this.YJXMDJ = double.Parse(ds.Tables[0].Rows[0]["YJXMDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GXTime"] != null && ds.Tables[0].Rows[0]["GXTime"].ToString() != "")
                {
                    this.GXTime = DateTime.Parse(ds.Tables[0].Rows[0]["GXTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["nworkid"] != null && ds.Tables[0].Rows[0]["nworkid"].ToString() != "")
                {
                    this.nworkid = int.Parse(ds.Tables[0].Rows[0]["nworkid"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQQDJ] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string strxmbh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ERPXMQQDJ ");
            strSql.Append(" where XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,30)};
            parameters[0].Value = strxmbh;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        public ZWL.BLL.ERPXMQQDJ GetModelByWhere(string strWhere)
        {
            var list = GetListModel(strWhere);
            if (list!=null && list.Count>0)
            {
                return list[0];
            }
            return null;
        }
        public ZWL.BLL.ERPXMQQDJ GetModelByWorkId(int workid)
        {
            return GetModelByWhere("nworkid="+ workid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPXMQQDJ] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPXMQQDJ");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.信息编号 in (select LEFT(BeiYong1,CHARINDEX('@',BeiYong1)-1) BeiYong1 from ERPNWorkToDo where FormID=60) ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 登记时间 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPXMQQDJ");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.信息编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("60") + ") ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere + " order by 登记时间 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPXMQQDJ");

            strSql = "select * from (" + strmapping + ") as LB_MrALLFint where 工作状态='正常结束' ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }
        public Pager GetAllListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            strSql += method.getSQLTable("ERPXMQQDJ");
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }
        public List<ZWL.BLL.ERPXMQQDJ> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPXMQQDJ>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableHelper.ConvertTo<ZWL.BLL.ERPXMQQDJ>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMapping(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = method.getSQLTable("ERPXMQQDJ");

            //strSql = "select * from (" + strmapping + ") as LB_MrALLFint where LB_MrALLFint.信息编号 in (" + ZWL.Common.PublicMethod.GetNWorkToDoIDList("60") + ") ";
            strSql = "select * from (" + strmapping + " and d.StateNow='正常结束' and d.FormID=60) as LB_MrALLFint where 1=1 ";
            if (strWhere.Trim() != "")
            {
                strSql += " and " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }

        #endregion  Method
    }
}
