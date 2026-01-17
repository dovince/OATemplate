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
    /// 类ERPInnerInviteBidReportDetail。
    /// </summary>
    [Serializable]
    public partial class ERPInnerInviteBidReportDetail
    {
        public ERPInnerInviteBidReportDetail()
        { }
        #region Model
        private int _id;
        private int _refid;
        private string _suppliername;
        private decimal _amount;
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
        public int RefID
        {
            set { _refid = value; }
            get { return _refid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPInnerInviteBidReportDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RefID,SupplierName,Amount ");
            strSql.Append(" FROM [ERPInnerInviteBidReportDetail] ");
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
                if (ds.Tables[0].Rows[0]["RefID"] != null && ds.Tables[0].Rows[0]["RefID"].ToString() != "")
                {
                    this.RefID = int.Parse(ds.Tables[0].Rows[0]["RefID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplierName"] != null)
                {
                    this.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPInnerInviteBidReportDetail]");
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
            strSql.Append("insert into [ERPInnerInviteBidReportDetail] (");
            strSql.Append("RefID,SupplierName,Amount)");
            strSql.Append(" values (");
            strSql.Append("@RefID,@SupplierName,@Amount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RefID", SqlDbType.Int,4),
                    new SqlParameter("@SupplierName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9)};
            parameters[0].Value = RefID;
            parameters[1].Value = SupplierName;
            parameters[2].Value = Amount;

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
            strSql.Append("update [ERPInnerInviteBidReportDetail] set ");
            strSql.Append("RefID=@RefID,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("Amount=@Amount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RefID", SqlDbType.Int,4),
                    new SqlParameter("@SupplierName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = RefID;
            parameters[1].Value = SupplierName;
            parameters[2].Value = Amount;
            parameters[3].Value = ID;

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
            strSql.Append("delete from [ERPInnerInviteBidReportDetail] ");
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
            strSql.Append("select ID,RefID,SupplierName,Amount ");
            strSql.Append(" FROM [ERPInnerInviteBidReportDetail] ");
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
                if (ds.Tables[0].Rows[0]["RefID"] != null && ds.Tables[0].Rows[0]["RefID"].ToString() != "")
                {
                    this.RefID = int.Parse(ds.Tables[0].Rows[0]["RefID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplierName"] != null)
                {
                    this.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"] != null && ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    this.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
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
            strSql.Append(" FROM [ERPInnerInviteBidReportDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public ZWL.BLL.ERPInnerInviteBidReportDetail GetModelBySqlWhere(string sqlWhere)
        {
            var ds = GetList(sqlWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPInnerInviteBidReportDetail>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        public List<ZWL.BLL.ERPInnerInviteBidReportDetail> GetModelListBySqlWhere(string sqlWhere)
        {
            var ds = GetList(sqlWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo_1<ZWL.BLL.ERPInnerInviteBidReportDetail>(ds.Tables[0]);
            }
            return new List<ZWL.BLL.ERPInnerInviteBidReportDetail>();
        }
        public List<ZWL.BLL.ERPInnerInviteBidReportDetail> GetModelListByRefID(int refId)
        {
            return GetModelListBySqlWhere("RefID=" + refId);
        }

        #endregion  Method
    }
}

