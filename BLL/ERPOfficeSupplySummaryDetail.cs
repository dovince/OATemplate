using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;
using ZWL.Common;

namespace ZWL.BLL
{
    /// <summary>
	/// 类ERPOfficeSupplySummaryDetail。
	/// </summary>
	[Serializable]
    public partial class ERPOfficeSupplySummaryDetail
    {
        public ERPOfficeSupplySummaryDetail()
        { }
        #region Model
        private int _id;
        private int _supplysummaryid;
        private int _supplydetailid;
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
        public int SupplySummaryID
        {
            set { _supplysummaryid = value; }
            get { return _supplysummaryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SupplyDetailID
        {
            set { _supplydetailid = value; }
            get { return _supplydetailid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPOfficeSupplySummaryDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SupplySummaryID,SupplyDetailID ");
            strSql.Append(" FROM [ERPOfficeSupplySummaryDetail] ");
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
                if (ds.Tables[0].Rows[0]["SupplySummaryID"] != null && ds.Tables[0].Rows[0]["SupplySummaryID"].ToString() != "")
                {
                    this.SupplySummaryID = int.Parse(ds.Tables[0].Rows[0]["SupplySummaryID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplyDetailID"] != null && ds.Tables[0].Rows[0]["SupplyDetailID"].ToString() != "")
                {
                    this.SupplyDetailID = int.Parse(ds.Tables[0].Rows[0]["SupplyDetailID"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPOfficeSupplySummaryDetail]");
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
            strSql.Append("insert into [ERPOfficeSupplySummaryDetail] (");
            strSql.Append("SupplySummaryID,SupplyDetailID)");
            strSql.Append(" values (");
            strSql.Append("@SupplySummaryID,@SupplyDetailID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplySummaryID", SqlDbType.Int,4),
                    new SqlParameter("@SupplyDetailID", SqlDbType.Int,4)};
            parameters[0].Value = SupplySummaryID;
            parameters[1].Value = SupplyDetailID;

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
            strSql.Append("update [ERPOfficeSupplySummaryDetail] set ");
            strSql.Append("SupplySummaryID=@SupplySummaryID,");
            strSql.Append("SupplyDetailID=@SupplyDetailID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplySummaryID", SqlDbType.Int,4),
                    new SqlParameter("@SupplyDetailID", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = SupplySummaryID;
            parameters[1].Value = SupplyDetailID;
            parameters[2].Value = ID;

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
            strSql.Append("delete from [ERPOfficeSupplySummaryDetail] ");
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
            strSql.Append("select ID,SupplySummaryID,SupplyDetailID ");
            strSql.Append(" FROM [ERPOfficeSupplySummaryDetail] ");
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
                if (ds.Tables[0].Rows[0]["SupplySummaryID"] != null && ds.Tables[0].Rows[0]["SupplySummaryID"].ToString() != "")
                {
                    this.SupplySummaryID = int.Parse(ds.Tables[0].Rows[0]["SupplySummaryID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplyDetailID"] != null && ds.Tables[0].Rows[0]["SupplyDetailID"].ToString() != "")
                {
                    this.SupplyDetailID = int.Parse(ds.Tables[0].Rows[0]["SupplyDetailID"].ToString());
                }
            }
        }


        public List<ZWL.BLL.ERPOfficeSupplyDetail> GetListBySqlWhere(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPOfficeSupplyDetail>(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select d.* ");
            strSql.Append(@" FROM ERPOfficeSupplyDetail d join ERPOfficeSupplySummaryDetail s
on d.ID=s.SupplyDetailID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetPagingList(string strWhere, int cPage, int pSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(@" FROM (select d.*,
			(select case when o.No is null then null else s.No end) as BeforeNo,
			(select case when o.No is null then s.No else o.No end) as CurrentNo,
			(select case when s.Operator is null then o.Operator else s.Operator end) as Operator,
			(select case when s.Department is null then o.Department else s.Department end) as Department,
			(select case when o.CreatedDate is null then s.CreatedDate else o.CreatedDate end) as CreatedDate,
			(case d.State when '1' then '待采购' when '2' then '采购审批中' when '3' then '已审批' else '待采购' end) as StateText,
			(select case when o.NWorkID is null then s.NWorkID else o.NWorkID end) as NWorkID,
			(select case when o.NWorkID is null then null else s.NWorkID end) as ONWorkID,
			(select case when t.JieDianName is null then n.JieDianName else t.JieDianName end) as JieDianName,
			(select case when t.FormID is null then n.FormID else t.FormID end) as FormID,
			(select case when t.WorkFlowID is null then n.WorkFlowID else t.WorkFlowID end) as WorkFlowID,
			(select case when t.StateNow is null then n.StateNow else t.StateNow end) as StateNow,
			(select case when t.ShenPiUserList is null then n.ShenPiUserList else t.ShenPiUserList end) as ShenPiUserList,
			(select case when t.OKUserList is null then n.OKUserList else t.OKUserList end) as OKUserList,
			(select case when f.WorkFlowName is null then w.WorkFlowName else f.WorkFlowName end) as WorkFlowName 
			FROM [ERPOfficeSupplyDetail] d left join [ERPOfficeSupply] s
  on d.SupplyID=s.ID left join [ERPNWorkToDo] n on s.NWorkID = n.ID left join [ERPNWorkFlow] w on n.WorkFlowID=w.ID
  left join ERPOfficeSupplySummaryDetail p on d.ID=p.SupplyDetailID left join [dbo].[ERPOfficeSupplySummary] o on p.SupplySummaryID = o.ID left join [ERPNWorkToDo] t on o.NWorkID=t.ID
  left join [dbo].[ERPNWorkFlow] f on t.WorkFlowID = f.ID) m ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), cPage, pSize, " State asc,ID desc");
        }

        #endregion  Method
    }
}

