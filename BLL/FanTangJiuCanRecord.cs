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
	/// 类FanTangJiuCanRecord。
	/// </summary>
	[Serializable]
    public partial class FanTangJiuCanRecord
    {
        public FanTangJiuCanRecord()
        { }
        #region Model
        private int _id;
        private string _lotid;
        private string _number;
        private string _name;
        private string _dept;
        private string _zhiwu;
        private string _sex;
        private DateTime _recorddate;
        private string _xingqi;
        private string _shijianduan;
        private string _canshi;
        private string _kaoqinrecord;
        private string _memo;
        private string _isChuChai;
        private string _chuChaiDiDian;
        private string _chuChaiNWorkID;
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
        public string LotID
        {
            set { _lotid = value; }
            get { return _lotid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dept
        {
            set { _dept = value; }
            get { return _dept; }
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
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XingQi
        {
            set { _xingqi = value; }
            get { return _xingqi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShiJianDuan
        {
            set { _shijianduan = value; }
            get { return _shijianduan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanShi
        {
            set { _canshi = value; }
            get { return _canshi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KaoQinRecord
        {
            set { _kaoqinrecord = value; }
            get { return _kaoqinrecord; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsChuChai
        {
            set { _isChuChai = value; }
            get { return _isChuChai; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuChaiDiDian
        {
            set { _chuChaiDiDian = value; }
            get { return _chuChaiDiDian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuChaiNWorkID
        {
            set { _chuChaiNWorkID = value; }
            get { return _chuChaiNWorkID; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FanTangJiuCanRecord(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [FanTangJiuCanRecord] ");
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
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dept"] != null)
                {
                    this.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiWu"] != null)
                {
                    this.ZhiWu = ds.Tables[0].Rows[0]["ZhiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null)
                {
                    this.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RecordDate"] != null && ds.Tables[0].Rows[0]["RecordDate"].ToString() != "")
                {
                    this.RecordDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecordDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingQi"] != null)
                {
                    this.XingQi = ds.Tables[0].Rows[0]["XingQi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShiJianDuan"] != null)
                {
                    this.ShiJianDuan = ds.Tables[0].Rows[0]["ShiJianDuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanShi"] != null)
                {
                    this.CanShi = ds.Tables[0].Rows[0]["CanShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KaoQinRecord"] != null)
                {
                    this.KaoQinRecord = ds.Tables[0].Rows[0]["KaoQinRecord"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Memo"] != null)
                {
                    this.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsChuChai"] != null)
                {
                    this.IsChuChai = ds.Tables[0].Rows[0]["IsChuChai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiDiDian"] != null)
                {
                    this.ChuChaiDiDian = ds.Tables[0].Rows[0]["ChuChaiDiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiDiDian"] != null)
                {
                    this.ChuChaiNWorkID = ds.Tables[0].Rows[0]["ChuChaiNWorkID"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [FanTangJiuCanRecord]");
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
            strSql.Append("insert into [FanTangJiuCanRecord] (");
            strSql.Append("LotID,Number,Name,Dept,ZhiWu,Sex,RecordDate,XingQi,ShiJianDuan,CanShi,KaoQinRecord,Memo,IsChuChai,ChuChaiDiDian,ChuChaiNWorkID)");
            strSql.Append(" values (");
            strSql.Append("@LotID,@Number,@Name,@Dept,@ZhiWu,@Sex,@RecordDate,@XingQi,@ShiJianDuan,@CanShi,@KaoQinRecord,@Memo,@IsChuChai,@ChuChaiDiDian,@ChuChaiNWorkID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhiWu", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sex", SqlDbType.NVarChar,10),
                    new SqlParameter("@RecordDate", SqlDbType.DateTime),
                    new SqlParameter("@XingQi", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShiJianDuan", SqlDbType.NVarChar,100),
                    new SqlParameter("@CanShi", SqlDbType.NVarChar,50),
                    new SqlParameter("@KaoQinRecord", SqlDbType.NVarChar,500),
                    new SqlParameter("@Memo", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsChuChai", SqlDbType.NVarChar,2),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.NVarChar,500),
                    new SqlParameter("@ChuChaiNWorkID", SqlDbType.VarChar,1000)
            };
            parameters[0].Value = LotID;
            parameters[1].Value = Number;
            parameters[2].Value = Name;
            parameters[3].Value = Dept;
            parameters[4].Value = ZhiWu;
            parameters[5].Value = Sex;
            parameters[6].Value = RecordDate;
            parameters[7].Value = XingQi;
            parameters[8].Value = ShiJianDuan;
            parameters[9].Value = CanShi;
            parameters[10].Value = KaoQinRecord;
            parameters[11].Value = Memo;
            parameters[12].Value = IsChuChai;
            parameters[13].Value = ChuChaiDiDian;
            parameters[14].Value = ChuChaiNWorkID;

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
            strSql.Append("update [FanTangJiuCanRecord] set ");
            strSql.Append("LotID=@LotID,");
            strSql.Append("Number=@Number,");
            strSql.Append("Name=@Name,");
            strSql.Append("Dept=@Dept,");
            strSql.Append("ZhiWu=@ZhiWu,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("RecordDate=@RecordDate,");
            strSql.Append("XingQi=@XingQi,");
            strSql.Append("ShiJianDuan=@ShiJianDuan,");
            strSql.Append("CanShi=@CanShi,");
            strSql.Append("KaoQinRecord=@KaoQinRecord,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("IsChuChai=@IsChuChai,");
            strSql.Append("ChuChaiDiDian=@ChuChaiDiDian,");
            strSql.Append("ChuChaiNWorkID=@ChuChaiNWorkID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotID", SqlDbType.VarChar,50),
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Dept", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZhiWu", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sex", SqlDbType.NVarChar,10),
                    new SqlParameter("@RecordDate", SqlDbType.DateTime),
                    new SqlParameter("@XingQi", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShiJianDuan", SqlDbType.NVarChar,100),
                    new SqlParameter("@CanShi", SqlDbType.NVarChar,50),
                    new SqlParameter("@KaoQinRecord", SqlDbType.NVarChar,500),
                    new SqlParameter("@Memo", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsChuChai", SqlDbType.NVarChar,2),
                    new SqlParameter("@ChuChaiDiDian", SqlDbType.NVarChar,500),
                    new SqlParameter("@ChuChaiNWorkID", SqlDbType.VarChar,1000),
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = LotID;
            parameters[1].Value = Number;
            parameters[2].Value = Name;
            parameters[3].Value = Dept;
            parameters[4].Value = ZhiWu;
            parameters[5].Value = Sex;
            parameters[6].Value = RecordDate;
            parameters[7].Value = XingQi;
            parameters[8].Value = ShiJianDuan;
            parameters[9].Value = CanShi;
            parameters[10].Value = KaoQinRecord;
            parameters[11].Value = Memo;
            parameters[12].Value = IsChuChai;
            parameters[13].Value = ChuChaiDiDian;
            parameters[14].Value = ChuChaiNWorkID;
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
            strSql.Append("delete from [FanTangJiuCanRecord] ");
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
            strSql.Append(" FROM [FanTangJiuCanRecord] ");
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
                if (ds.Tables[0].Rows[0]["LotID"] != null)
                {
                    this.LotID = ds.Tables[0].Rows[0]["LotID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Number"] != null)
                {
                    this.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dept"] != null)
                {
                    this.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiWu"] != null)
                {
                    this.ZhiWu = ds.Tables[0].Rows[0]["ZhiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null)
                {
                    this.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RecordDate"] != null && ds.Tables[0].Rows[0]["RecordDate"].ToString() != "")
                {
                    this.RecordDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecordDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XingQi"] != null)
                {
                    this.XingQi = ds.Tables[0].Rows[0]["XingQi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShiJianDuan"] != null)
                {
                    this.ShiJianDuan = ds.Tables[0].Rows[0]["ShiJianDuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CanShi"] != null)
                {
                    this.CanShi = ds.Tables[0].Rows[0]["CanShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KaoQinRecord"] != null)
                {
                    this.KaoQinRecord = ds.Tables[0].Rows[0]["KaoQinRecord"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Memo"] != null)
                {
                    this.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsChuChai"] != null)
                {
                    this.IsChuChai = ds.Tables[0].Rows[0]["IsChuChai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiDiDian"] != null)
                {
                    this.ChuChaiDiDian = ds.Tables[0].Rows[0]["ChuChaiDiDian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChaiNWorkID"] != null)
                {
                    this.ChuChaiNWorkID = ds.Tables[0].Rows[0]["ChuChaiNWorkID"].ToString();
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
            strSql.Append(" FROM [FanTangJiuCanRecord] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.FanTangJiuCanRecord> GetModelList(string strWhere)
        {
            var result = new List<ZWL.BLL.FanTangJiuCanRecord>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.FanTangJiuCanRecord>(ds.Tables[0]);
            }
            return result;
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize)
        {
            return GetListAndPaging(strWhere, cPage, pSize, "ID desc");
        }
        public Pager GetListAndPaging(string strWhere, int cPage, int pSize, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from FanTangJiuCanRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, orderby);
        }

        #endregion  Method
    }
}
