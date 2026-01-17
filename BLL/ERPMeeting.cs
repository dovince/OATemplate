using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPMeeting。
    /// </summary>
    public class ERPMeeting
    {
   
            public ERPMeeting()
            { }
            #region Model
            private int _id;//主键

            private int _nworktodoid;

            private string _workname = "";

            private DateTime _dengjitime = DateTime.Now;

            private string _meetingtitle = "";

            private string _meetingzhuti = "";

            private string _huiyididian = "";

            private DateTime _huiyistarttime = DateTime.Now;

            private DateTime _huiyiendtime = DateTime.Now;

            private int _canhuirenshu;

            private string _canhuirenyuan;

            private string _shenqingren = "";

            private string _shenqingbumen = "";

            private string _phone = "";

            private string _state = "";

            private string _beizhu = "";


            /// <summary>
            /// 主键
            /// </summary>
            public int ID
            {
                set { _id = value; }
                get { return _id; }
            }

            /// <summary>
            /// NWorkToDoID
            /// </summary>
            public int NWorkToDoID
            {
                set { _nworktodoid = value; }
                get { return _nworktodoid; }
            }

            /// <summary>
            /// 工作名称
            /// </summary>
            public string WorkName
            {
                set { _workname = value; }
                get { return _workname; }
            }

            /// <summary>
            /// 登记时间
            /// </summary>
            public DateTime DengJiTime
            {
                set { _dengjitime = value; }
                get { return _dengjitime; }
            }

            /// <summary>
            /// 会议名称
            /// </summary>
            public string MeetingTitle
            {
                set { _meetingtitle = value; }
                get { return _meetingtitle; }
            }

            /// <summary>
            /// 会议主题
            /// </summary>
            public string MeetingZhuTi
            {
                set { _meetingzhuti = value; }
                get { return _meetingzhuti; }
            }

            /// <summary>
            /// 会议地点
            /// </summary>
            public string HuiYiDiDian
            {
                set { _huiyididian = value; }
                get { return _huiyididian; }
            }

            /// <summary>
            /// 会议开始时间
            /// </summary>
            public DateTime HuiYiStartTime
            {
                set { _huiyistarttime = value; }
                get { return _huiyistarttime; }
            }

            /// <summary>
            /// 会议结束时间
            /// </summary>
            public DateTime HuiYiEndTime
            {
                set { _huiyiendtime = value; }
                get { return _huiyiendtime; }
            }

            /// <summary>
            /// 参会人数
            /// </summary>
            public int CanHuiRenShu
            {
                set { _canhuirenshu = value; }
                get { return _canhuirenshu; }
            }

            /// <summary>
            /// 参会人员
            /// </summary>
            public string CanHuiRenYuan
            {
                set { _canhuirenyuan = value; }
                get { return _canhuirenyuan; }
            }

            /// <summary>
            /// 申请人
            /// </summary>
            public string ShenQingRen
            {
                set { _shenqingren = value; }
                get { return _shenqingren; }
            }

            /// <summary>
            /// 申请部门
            /// </summary>
            public string ShenQingBuMen
            {
                set { _shenqingbumen = value; }
                get { return _shenqingbumen; }
            }

            /// <summary>
            /// 联系手机号
            /// </summary>
            public string Phone
            {
                set { _phone = value; }
                get { return _phone; }
            }

            /// <summary>
            /// 会议室状态
            /// </summary>
            public string State
            {
                set { _state = value; }
                get { return _state; }
            }

            /// <summary>
            /// 备注
            /// </summary>
            public string BeiZhu
            {
                set { _beizhu = value; }
                get { return _beizhu; }
            }


            #endregion Model


            #region  Method

            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public ERPMeeting(int ID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,MeetingTitle,MeetingZhuTi,HuiYiDiDian,HuiYiStartTime,HuiYiEndTime,CanHuiRenShu,CanHuiRenYuan,ShenQingRen,ShenQingBuMen,Phone,State,BeiZhu ");
                strSql.Append(" FROM [ERPMeeting] ");
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

                    if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                    {
                        this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["WorkName"] != null)
                    {
                        this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                    {
                        this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["MeetingTitle"] != null)
                    {
                        this.MeetingTitle = ds.Tables[0].Rows[0]["MeetingTitle"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["MeetingZhuTi"] != null)
                    {
                        this.MeetingZhuTi = ds.Tables[0].Rows[0]["MeetingZhuTi"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiDiDian"] != null)
                    {
                        this.HuiYiDiDian = ds.Tables[0].Rows[0]["HuiYiDiDian"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiStartTime"] != null)
                    {
                        this.HuiYiStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["HuiYiStartTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiEndTime"] != null)
                    {
                        this.HuiYiEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["HuiYiEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["CanHuiRenShu"] != null && ds.Tables[0].Rows[0]["CanHuiRenShu"].ToString() != "")
                    {
                        this.CanHuiRenShu = int.Parse(ds.Tables[0].Rows[0]["CanHuiRenShu"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["ShenQingRen"] != null)
                    {
                        this.ShenQingRen = ds.Tables[0].Rows[0]["ShenQingRen"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ShenQingBuMen"] != null)
                    {
                        this.ShenQingBuMen = ds.Tables[0].Rows[0]["ShenQingBuMen"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Phone"] != null)
                    {
                        this.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["State"] != null)
                    {
                        this.State = ds.Tables[0].Rows[0]["State"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["BeiZhu"] != null)
                    {
                        this.BeiZhu = ds.Tables[0].Rows[0]["BeiZhu"].ToString();
                    }
                }
            }
            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(int ID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from [ERPMeeting]");
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
            strSql.Append("insert into [ERPMeeting] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,MeetingTitle,MeetingZhuTi,HuiYiDiDian,HuiYiStartTime,HuiYiEndTime,CanHuiRenShu,CanHuiRenYuan,ShenQingRen,ShenQingBuMen,Phone,State,BeiZhu)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@MeetingTitle,@MeetingZhuTi,@HuiYiDiDian,@HuiYiStartTime,@HuiYiEndTime,@CanHuiRenShu,@CanHuiRenYuan,@ShenQingRen,@ShenQingBuMen,@Phone,@State,@BeiZhu)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@MeetingTitle", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@MeetingZhuTi", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@HuiYiDiDian", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@HuiYiStartTime", SqlDbType.DateTime),
       
					new SqlParameter("@HuiYiEndTime", SqlDbType.DateTime),
       
					new SqlParameter("@CanHuiRenShu", SqlDbType.Int),
       
					new SqlParameter("@CanHuiRenYuan", SqlDbType.Text),
       
					new SqlParameter("@ShenQingRen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingBuMen", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@Phone", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@State", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BeiZhu", SqlDbType.NVarChar, 50)};
       
            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = MeetingTitle;
       
            parameters[4].Value = MeetingZhuTi;
       
            parameters[5].Value = HuiYiDiDian;
       
            parameters[6].Value = HuiYiStartTime;
       
            parameters[7].Value = HuiYiEndTime;
       
            parameters[8].Value = CanHuiRenShu;
       
            parameters[9].Value = CanHuiRenYuan;
       
            parameters[10].Value = ShenQingRen;
       
            parameters[11].Value = ShenQingBuMen;
       
            parameters[12].Value = Phone;
       
            parameters[13].Value = State;
       
            parameters[14].Value = BeiZhu;
       

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
            strSql.Append("update [ERPMeeting] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("MeetingTitle=@MeetingTitle,");

            strSql.Append("MeetingZhuTi=@MeetingZhuTi,");

            strSql.Append("HuiYiDiDian=@HuiYiDiDian,");

            strSql.Append("HuiYiStartTime=@HuiYiStartTime,");

            strSql.Append("HuiYiEndTime=@HuiYiEndTime,");

            strSql.Append("CanHuiRenShu=@CanHuiRenShu,");

            strSql.Append("CanHuiRenYuan=@CanHuiRenYuan,");

            strSql.Append("ShenQingRen=@ShenQingRen,");

            strSql.Append("ShenQingBuMen=@ShenQingBuMen,");

            strSql.Append("Phone=@Phone,");

            strSql.Append("State=@State,");

            strSql.Append("BeiZhu=@BeiZhu");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@MeetingTitle", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@MeetingZhuTi", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@HuiYiDiDian", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@HuiYiStartTime", SqlDbType.DateTime),
       
					new SqlParameter("@HuiYiEndTime", SqlDbType.DateTime),
       
					new SqlParameter("@CanHuiRenShu", SqlDbType.Int),
       
					new SqlParameter("@CanHuiRenYuan",  SqlDbType.Text),
       
					new SqlParameter("@ShenQingRen", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ShenQingBuMen", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@Phone", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@State", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BeiZhu", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;
       
            parameters[1].Value = WorkName;
       
            parameters[2].Value = DengJiTime;
       
            parameters[3].Value = MeetingTitle;
       
            parameters[4].Value = MeetingZhuTi;
       
            parameters[5].Value = HuiYiDiDian;
       
            parameters[6].Value = HuiYiStartTime;
       
            parameters[7].Value = HuiYiEndTime;
       
            parameters[8].Value = CanHuiRenShu;
       
            parameters[9].Value = CanHuiRenYuan;
       
            parameters[10].Value = ShenQingRen;
       
            parameters[11].Value = ShenQingBuMen;
       
            parameters[12].Value = Phone;
       
            parameters[13].Value = State;
       
            parameters[14].Value = BeiZhu;
       
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
                strSql.Append("delete from [ERPMeeting] ");
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
                strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,MeetingTitle,MeetingZhuTi,HuiYiDiDian,HuiYiStartTime,HuiYiEndTime,CanHuiRenShu,CanHuiRenYuan,ShenQingRen,ShenQingBuMen,Phone,State,BeiZhu ");
                strSql.Append(" FROM [ERPMeeting] ");
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

                    if (ds.Tables[0].Rows[0]["NWorkToDoID"] != null && ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                    {
                        this.NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["WorkName"] != null)
                    {
                        this.WorkName = ds.Tables[0].Rows[0]["WorkName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DengJiTime"] != null)
                    {
                        this.DengJiTime = DateTime.Parse(ds.Tables[0].Rows[0]["DengJiTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["MeetingTitle"] != null)
                    {
                        this.MeetingTitle = ds.Tables[0].Rows[0]["MeetingTitle"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["MeetingZhuTi"] != null)
                    {
                        this.MeetingZhuTi = ds.Tables[0].Rows[0]["MeetingZhuTi"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiDiDian"] != null)
                    {
                        this.HuiYiDiDian = ds.Tables[0].Rows[0]["HuiYiDiDian"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiStartTime"] != null)
                    {
                        this.HuiYiStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["HuiYiStartTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["HuiYiEndTime"] != null)
                    {
                        this.HuiYiEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["HuiYiEndTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["CanHuiRenShu"] != null && ds.Tables[0].Rows[0]["CanHuiRenShu"].ToString() != "")
                    {
                        this.CanHuiRenShu = int.Parse(ds.Tables[0].Rows[0]["CanHuiRenShu"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["CanHuiRenYuan"] != null && ds.Tables[0].Rows[0]["CanHuiRenYuan"].ToString() != "")
                    {
                        this.CanHuiRenYuan = ds.Tables[0].Rows[0]["CanHuiRenYuan"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ShenQingRen"] != null)
                    {
                        this.ShenQingRen = ds.Tables[0].Rows[0]["ShenQingRen"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ShenQingBuMen"] != null)
                    {
                        this.ShenQingBuMen = ds.Tables[0].Rows[0]["ShenQingBuMen"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Phone"] != null)
                    {
                        this.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["State"] != null)
                    {
                        this.State = ds.Tables[0].Rows[0]["State"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["BeiZhu"] != null)
                    {
                        this.BeiZhu = ds.Tables[0].Rows[0]["BeiZhu"].ToString();
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
                strSql.Append(" FROM [ERPMeeting] ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return DbHelperSQL.Query(strSql.ToString());
            }

            #endregion  Method

            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public void GetNWorkModel(int nworktodoid)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 * ");
                strSql.Append(" FROM ERPMeeting ");
                strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
                SqlParameter[] parameters = {
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
                parameters[0].Value = nworktodoid;

                DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                    {
                        ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                    }

                    GetModel(ID);
                }
        }
    }
}