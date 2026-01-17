
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPBuMenQingJia,部门集体请假申请表
    /// </summary>
    public class ERPBuMenQingJia
    {
        public ERPBuMenQingJia()
        { }
        #region Model
        private int _id;//主键

        private int _nworktodoid;

        private string _workname = "";

        private DateTime _dengjitime = DateTime.Now;

        private string _sqr = "";

        private DateTime _tbtime = DateTime.Now;

        private string _bm = "";

        private string _qjlx = "";

        private string _qjyy = "";

        private string _bz = "";


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
        /// 申请人
        /// </summary>
        public string SQR
        {
            set { _sqr = value; }
            get { return _sqr; }
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
        /// 申请部门
        /// </summary>
        public string BM
        {
            set { _bm = value; }
            get { return _bm; }
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
        /// 备注
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
        public ERPBuMenQingJia(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,SQR,TBTime,BM,QJLX,QJYY,BZ ");
            strSql.Append(" FROM [ERPBuMenQingJia] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null)
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJLX"] != null)
                {
                    this.QJLX = ds.Tables[0].Rows[0]["QJLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJYY"] != null)
                {
                    this.QJYY = ds.Tables[0].Rows[0]["QJYY"].ToString();
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
            strSql.Append("select count(1) from [ERPBuMenQingJia]");
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
            strSql.Append("insert into [ERPBuMenQingJia] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,SQR,TBTime,BM,QJLX,QJYY,BZ)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@SQR,@TBTime,@BM,@QJLX,@QJYY,@BZ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@SQR", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@TBTime", SqlDbType.DateTime),
       
					new SqlParameter("@BM", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@QJLX", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@QJYY", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@BZ", SqlDbType.NVarChar, 500)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = SQR;

            parameters[4].Value = TBTime;

            parameters[5].Value = BM;

            parameters[6].Value = QJLX;

            parameters[7].Value = QJYY;

            parameters[8].Value = BZ;


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
            strSql.Append("update [ERPBuMenQingJia] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("SQR=@SQR,");

            strSql.Append("TBTime=@TBTime,");

            strSql.Append("BM=@BM,");

            strSql.Append("QJLX=@QJLX,");

            strSql.Append("QJYY=@QJYY,");

            strSql.Append("BZ=@BZ");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@SQR", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@TBTime", SqlDbType.DateTime),
       
					new SqlParameter("@BM", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@QJLX", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@QJYY", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@BZ", SqlDbType.NVarChar, 500),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = SQR;

            parameters[4].Value = TBTime;

            parameters[5].Value = BM;

            parameters[6].Value = QJLX;

            parameters[7].Value = QJYY;

            parameters[8].Value = BZ;

            parameters[9].Value = ID;

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
            strSql.Append("delete from [ERPBuMenQingJia] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,SQR,TBTime,BM,QJLX,QJYY,BZ ");
            strSql.Append(" FROM [ERPBuMenQingJia] ");
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
                if (ds.Tables[0].Rows[0]["SQR"] != null)
                {
                    this.SQR = ds.Tables[0].Rows[0]["SQR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TBTime"] != null)
                {
                    this.TBTime = DateTime.Parse(ds.Tables[0].Rows[0]["TBTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BM"] != null)
                {
                    this.BM = ds.Tables[0].Rows[0]["BM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJLX"] != null)
                {
                    this.QJLX = ds.Tables[0].Rows[0]["QJLX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QJYY"] != null)
                {
                    this.QJYY = ds.Tables[0].Rows[0]["QJYY"].ToString();
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
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPBuMenQingJia] ");
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
            strSql.Append(" FROM ERPBuMenQingJia ");
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

        /// <summary>
        /// 检查年休假是否可以扣除
        /// </summary>
        public string checknxj(List<ERPBuMenQingJiaDtl> dtllist)
        {
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            if (dtllist.Count > 0)
            {
                for (int i = 0; i < dtllist.Count; i++)
                {
                    var dr = dtllist[i];
                    var qjrsplit = dr.QJR.ToString().Split(',');
                    var qjts = dr.QJTS;

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                var res = amodel.checknxj(qjts);
                                if (res != "OK")
                                {
                                    return "用户[" + qjr + "]的" + res;
                                }
                            }
                            else
                            {
                                return "未找到用户[" + qjr + "]的年休假信息，不能请年休假，请联系人事科完善用户资料";
                            }
                        }
                    }
                }
            }
            return "OK";
        }

        /// <summary>
        /// 冻结年休假
        /// </summary>
        public string addnxj(List<ERPBuMenQingJiaDtl> dtllist)
        {
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            if (dtllist.Count > 0)
            {
                for (int i = 0; i < dtllist.Count; i++)
                {
                    var dr = dtllist[i];
                    var qjrsplit = dr.QJR.ToString().Split(',');
                    var qjts = dr.QJTS;

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                amodel.addnxj(qjts);
                            }
                        }
                    }
                }
            }
            return "OK";
        }

        /// <summary>
        /// 冻结年休假
        /// </summary>
        public string addnxj()
        {
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            var dtl = new ZWL.BLL.ERPBuMenQingJiaDtl();
            var ds = dtl.GetList("MainID='" + this.ID + "'");
            if (ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var qjrsplit = dr["QJR"].ToString().Split(',');
                    var qjts = Convert.ToInt32(dr["QJTS"].ToString());

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                var res = amodel.checknxj(qjts);
                                if (res != "OK")
                                {
                                    return res;
                                }
                            }
                        }
                    }

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                amodel.addnxj(qjts);
                            }
                        }
                    }
                }
            }
            return "OK";
        }

        /// <summary>
        /// 返还冻结的年休假
        /// </summary>
        public void returnnxj()
        {
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            var dtl = new ZWL.BLL.ERPBuMenQingJiaDtl();
            var ds = dtl.GetList("MainID='" + this.ID + "'");
            if (ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var qjrsplit = dr["QJR"].ToString().Split(',');
                    var qjts = Convert.ToInt32(dr["QJTS"].ToString());

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                amodel.bumenreturnnxj(qjts, "部门集体请假申请:" + this.ID);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 年休假完成后去掉冻结的年休假
        /// </summary>
        public void nxjconfirm()
        {
            ZWL.BLL.AnnualLeave amodel = new ZWL.BLL.AnnualLeave();
            var dtl = new ZWL.BLL.ERPBuMenQingJiaDtl();
            var ds = dtl.GetList("MainID='" + this.ID + "'");
            if (ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var qjrsplit = dr["QJR"].ToString().Split(',');
                    var qjts = Convert.ToInt32(dr["QJTS"].ToString());
                    var tstart = Convert.ToDateTime(dr["QJSJStart"]);
                    var tend = Convert.ToDateTime(dr["QJSJEnd"]);

                    foreach (var qjr in qjrsplit)
                    {
                        if (!string.IsNullOrEmpty(qjr))
                        {
                            amodel.GetModel(qjr);
                            if (amodel.ID > 0)
                            {
                                //解除冻结，减去年休假
                                //amodel.nxjconfirm(qjts);

                                ZWL.BLL.ERPQingJia qj = new ZWL.BLL.ERPQingJia();
                                qj.QJR = qjr;
                                qj.BM = BM;
                                qj.QJLX = QJLX;
                                qj.QJSJStart = tstart;
                                qj.QJSJEnd = tend;
                                qj.QJTS = qjts;
                                qj.NWorkID = this.NWorkToDoID;
                                qj.QJState = "正常结束";
                                qj.XJTime = DateTime.Now;
                                qj.TBTime = this.TBTime;
                                qj.Add();
                            }
                        }
                    }
                }
            }
        }
    }
}