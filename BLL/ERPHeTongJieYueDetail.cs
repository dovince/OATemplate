using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPHeTongJieYueDetail。
    /// </summary>
    [Serializable]
    public partial class ERPHeTongJieYueDetail
    {
        public ERPHeTongJieYueDetail()
        { }
        #region Model
        private int _id;
        private int _nworkid;
        private int _htid;
        private int _lenduserid;
        private DateTime? _lenddate;
        private DateTime? _backdate;
        private int? _backconfirmuserid;
        private string _backcomment;
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
        public int NWorkID
        {
            set { _nworkid = value; }
            get { return _nworkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int HTID
        {
            set { _htid = value; }
            get { return _htid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LendUserID
        {
            set { _lenduserid = value; }
            get { return _lenduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LendDate
        {
            set { _lenddate = value; }
            get { return _lenddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BackConfirmUserID
        {
            set { _backconfirmuserid = value; }
            get { return _backconfirmuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackComment
        {
            set { _backcomment = value; }
            get { return _backcomment; }
        }
        #endregion Model

        #region Relative Model
        public ZWL.BLL.ERPHeTong CurrentHeTong
        {
            get
            {
                var _currentHeTong = new ZWL.BLL.ERPHeTong();
                if (HTID > 0)
                {
                    var tempHeTong = new ZWL.BLL.ERPHeTong();
                    tempHeTong.GetModelByWorkId(HTID);
                    if (tempHeTong.ID > 0)
                        _currentHeTong = tempHeTong;
                }
                return _currentHeTong;
            }
        }

        #endregion Relative Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPHeTongJieYueDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkID,HTID,LendUserID,LendDate,BackDate,BackConfirmUserID,BackComment ");
            strSql.Append(" FROM [ERPHeTongJieYueDetail] ");
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
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTID"] != null && ds.Tables[0].Rows[0]["HTID"].ToString() != "")
                {
                    this.HTID = int.Parse(ds.Tables[0].Rows[0]["HTID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LendUserID"] != null && ds.Tables[0].Rows[0]["LendUserID"].ToString() != "")
                {
                    this.LendUserID = int.Parse(ds.Tables[0].Rows[0]["LendUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LendDate"] != null && ds.Tables[0].Rows[0]["LendDate"].ToString() != "")
                {
                    this.LendDate = DateTime.Parse(ds.Tables[0].Rows[0]["LendDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackDate"] != null && ds.Tables[0].Rows[0]["BackDate"].ToString() != "")
                {
                    this.BackDate = DateTime.Parse(ds.Tables[0].Rows[0]["BackDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackConfirmUserID"] != null && ds.Tables[0].Rows[0]["BackConfirmUserID"].ToString() != "")
                {
                    this.BackConfirmUserID = int.Parse(ds.Tables[0].Rows[0]["BackConfirmUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackComment"] != null)
                {
                    this.BackComment = ds.Tables[0].Rows[0]["BackComment"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPHeTongJieYueDetail]");
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
            strSql.Append("insert into [ERPHeTongJieYueDetail] (");
            strSql.Append("NWorkID,HTID,LendUserID,LendDate,BackDate,BackConfirmUserID,BackComment)");
            strSql.Append(" values (");
            strSql.Append("@NWorkID,@HTID,@LendUserID,@LendDate,@BackDate,@BackConfirmUserID,@BackComment)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@HTID", SqlDbType.Int,4),
                    new SqlParameter("@LendUserID", SqlDbType.Int,4),
                    new SqlParameter("@LendDate", SqlDbType.DateTime),
                    new SqlParameter("@BackDate", SqlDbType.DateTime),
                    new SqlParameter("@BackConfirmUserID", SqlDbType.Int,4),
                    new SqlParameter("@BackComment", SqlDbType.NVarChar,2000)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = HTID;
            parameters[2].Value = LendUserID;
            parameters[3].Value = LendDate;
            parameters[4].Value = BackDate;
            parameters[5].Value = BackConfirmUserID;
            parameters[6].Value = BackComment;

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
            strSql.Append("update [ERPHeTongJieYueDetail] set ");
            strSql.Append("NWorkID=@NWorkID,");
            strSql.Append("HTID=@HTID,");
            strSql.Append("LendUserID=@LendUserID,");
            strSql.Append("LendDate=@LendDate,");
            strSql.Append("BackDate=@BackDate,");
            strSql.Append("BackConfirmUserID=@BackConfirmUserID,");
            strSql.Append("BackComment=@BackComment");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NWorkID", SqlDbType.Int,4),
                    new SqlParameter("@HTID", SqlDbType.Int,4),
                    new SqlParameter("@LendUserID", SqlDbType.Int,4),
                    new SqlParameter("@LendDate", SqlDbType.DateTime),
                    new SqlParameter("@BackDate", SqlDbType.DateTime),
                    new SqlParameter("@BackConfirmUserID", SqlDbType.Int,4),
                    new SqlParameter("@BackComment", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = NWorkID;
            parameters[1].Value = HTID;
            parameters[2].Value = LendUserID;
            parameters[3].Value = LendDate;
            parameters[4].Value = BackDate;
            parameters[5].Value = BackConfirmUserID;
            parameters[6].Value = BackComment;
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
            strSql.Append("delete from [ERPHeTongJieYueDetail] ");
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
            strSql.Append("select ID,NWorkID,HTID,LendUserID,LendDate,BackDate,BackConfirmUserID,BackComment ");
            strSql.Append(" FROM [ERPHeTongJieYueDetail] ");
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
                if (ds.Tables[0].Rows[0]["NWorkID"] != null && ds.Tables[0].Rows[0]["NWorkID"].ToString() != "")
                {
                    this.NWorkID = int.Parse(ds.Tables[0].Rows[0]["NWorkID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTID"] != null && ds.Tables[0].Rows[0]["HTID"].ToString() != "")
                {
                    this.HTID = int.Parse(ds.Tables[0].Rows[0]["HTID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LendUserID"] != null && ds.Tables[0].Rows[0]["LendUserID"].ToString() != "")
                {
                    this.LendUserID = int.Parse(ds.Tables[0].Rows[0]["LendUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LendDate"] != null && ds.Tables[0].Rows[0]["LendDate"].ToString() != "")
                {
                    this.LendDate = DateTime.Parse(ds.Tables[0].Rows[0]["LendDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackDate"] != null && ds.Tables[0].Rows[0]["BackDate"].ToString() != "")
                {
                    this.BackDate = DateTime.Parse(ds.Tables[0].Rows[0]["BackDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackConfirmUserID"] != null && ds.Tables[0].Rows[0]["BackConfirmUserID"].ToString() != "")
                {
                    this.BackConfirmUserID = int.Parse(ds.Tables[0].Rows[0]["BackConfirmUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BackComment"] != null)
                {
                    this.BackComment = ds.Tables[0].Rows[0]["BackComment"].ToString();
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
            strSql.Append(" FROM [ERPHeTongJieYueDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }



        public ZWL.BLL.ERPHeTongJieYueDetail GetModelByWhere(string strWhere)
        {
            var list = GetListModel(strWhere);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public List<ZWL.BLL.ERPHeTongJieYueDetail> GetListModelByWorkId(int workid)
        {
            var list = GetListModel("NWorkID=" + workid);
            return list;
        }
        public List<ZWL.BLL.ERPHeTongJieYueDetail> GetListModel(string strWhere)
        {
            var list = new List<ZWL.BLL.ERPHeTongJieYueDetail>();
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableHelper.ConvertTo<ZWL.BLL.ERPHeTongJieYueDetail>(ds.Tables[0]);
            }
            return list;
        }

        #endregion  Method
    }
}

