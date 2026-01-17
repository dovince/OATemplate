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
	/// 类ERPBudgetDetail。
	/// </summary>
	[Serializable]
    public partial class ERPBudgetDetail
    {
        public ERPBudgetDetail()
        { }
        #region Model
        private int _id;
        private int? _parentid;
        private int _version;
        private string _xmbh;
        private string _htbh;
        private decimal _工资及津贴 = 0M;
        private decimal _工程出包费 = 0M;
        private decimal _材料费 = 0M;
        private decimal _租赁费 = 0M;
        private decimal _劳务费 = 0M;
        private decimal _安全生产费用 = 0M;
        private decimal _办公费 = 0M;
        private decimal _维修费用 = 0M;
        private decimal _交通运输费用 = 0M;
        private decimal _差旅费 = 0M;
        private decimal _邮电费用 = 0M;
        private decimal _水电费 = 0M;
        private decimal _会议费 = 0M;
        private decimal _印刷费 = 0M;
        private decimal _其它费用 = 0M;
        private DateTime _createdtime;
        private string _comment;
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
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMBH
        {
            set { _xmbh = value; }
            get { return _xmbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HTBH
        {
            set { _htbh = value; }
            get { return _htbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 工资及津贴
        {
            set { _工资及津贴 = value; }
            get { return _工资及津贴; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 工程出包费
        {
            set { _工程出包费 = value; }
            get { return _工程出包费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 材料费
        {
            set { _材料费 = value; }
            get { return _材料费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 租赁费
        {
            set { _租赁费 = value; }
            get { return _租赁费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 劳务费
        {
            set { _劳务费 = value; }
            get { return _劳务费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 安全生产费用
        {
            set { _安全生产费用 = value; }
            get { return _安全生产费用; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 办公费
        {
            set { _办公费 = value; }
            get { return _办公费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 维修费用
        {
            set { _维修费用 = value; }
            get { return _维修费用; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 交通运输费用
        {
            set { _交通运输费用 = value; }
            get { return _交通运输费用; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 差旅费
        {
            set { _差旅费 = value; }
            get { return _差旅费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 邮电费用
        {
            set { _邮电费用 = value; }
            get { return _邮电费用; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 其它费用
        {
            set { _其它费用 = value; }
            get { return _其它费用; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 水电费
        {
            set { _水电费 = value; }
            get { return _水电费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 会议费
        {
            set { _会议费 = value; }
            get { return _会议费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal 印刷费
        {
            set { _印刷费 = value; }
            get { return _印刷费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPBudgetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPBudgetDetail] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPBudgetDetail]");
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
            strSql.Append("insert into [ERPBudgetDetail] (");
            strSql.Append("Version,XMBH,HTBH,工资及津贴,工程出包费,材料费,租赁费,劳务费,安全生产费用,办公费,维修费用,交通运输费用,差旅费,邮电费用,会议费,印刷费,水电费,其它费用,CreatedTime,Comment,ParentId)");
            strSql.Append(" values (");
            strSql.Append("@Version,@XMBH,@HTBH,@工资及津贴,@工程出包费,@材料费,@租赁费,@劳务费,@安全生产费用,@办公费,@维修费用,@交通运输费用,@差旅费,@邮电费用,@会议费,@印刷费,@水电费,@其它费用,@CreatedTime,@Comment,@ParentId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Version", SqlDbType.Int,4),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@工资及津贴", SqlDbType.Decimal,9),
                    new SqlParameter("@工程出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@材料费", SqlDbType.Decimal,9),
                    new SqlParameter("@租赁费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳务费", SqlDbType.Decimal,9),
                    new SqlParameter("@安全生产费用", SqlDbType.Decimal,9),
                    new SqlParameter("@办公费", SqlDbType.Decimal,9),
                    new SqlParameter("@维修费用", SqlDbType.Decimal,9),
                    new SqlParameter("@交通运输费用", SqlDbType.Decimal,9),
                    new SqlParameter("@差旅费", SqlDbType.Decimal,9),
                    new SqlParameter("@邮电费用", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ParentId", SqlDbType.Int,4)
            };
            parameters[0].Value = Version;
            parameters[1].Value = XMBH;
            parameters[2].Value = HTBH;
            parameters[3].Value = 工资及津贴;
            parameters[4].Value = 工程出包费;
            parameters[5].Value = 材料费;
            parameters[6].Value = 租赁费;
            parameters[7].Value = 劳务费;
            parameters[8].Value = 安全生产费用;
            parameters[9].Value = 办公费;
            parameters[10].Value = 维修费用;
            parameters[11].Value = 交通运输费用;
            parameters[12].Value = 差旅费;
            parameters[13].Value = 邮电费用;
            parameters[14].Value = 会议费;
            parameters[15].Value = 印刷费;
            parameters[16].Value = 水电费;
            parameters[17].Value = 其它费用;
            parameters[18].Value = CreatedTime;
            parameters[19].Value = Comment;
            parameters[20].Value = ParentId;

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
            strSql.Append("update [ERPBudgetDetail] set ");
            strSql.Append("Version=@Version,");
            strSql.Append("工资及津贴=@工资及津贴,");
            strSql.Append("工程出包费=@工程出包费,");
            strSql.Append("材料费=@材料费,");
            strSql.Append("租赁费=@租赁费,");
            strSql.Append("劳务费=@劳务费,");
            strSql.Append("安全生产费用=@安全生产费用,");
            strSql.Append("办公费=@办公费,");
            strSql.Append("维修费用=@维修费用,");
            strSql.Append("交通运输费用=@交通运输费用,");
            strSql.Append("差旅费=@差旅费,");
            strSql.Append("邮电费用=@邮电费用,");
            strSql.Append("会议费=@会议费,");
            strSql.Append("印刷费=@印刷费,");
            strSql.Append("水电费=@水电费,");
            strSql.Append("其它费用=@其它费用,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("ParentId=@ParentId");
            strSql.Append(" where ID=@ID and XMBH=@XMBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Version", SqlDbType.Int,4),
                    new SqlParameter("@工资及津贴", SqlDbType.Decimal,9),
                    new SqlParameter("@工程出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@材料费", SqlDbType.Decimal,9),
                    new SqlParameter("@租赁费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳务费", SqlDbType.Decimal,9),
                    new SqlParameter("@安全生产费用", SqlDbType.Decimal,9),
                    new SqlParameter("@办公费", SqlDbType.Decimal,9),
                    new SqlParameter("@维修费用", SqlDbType.Decimal,9),
                    new SqlParameter("@交通运输费用", SqlDbType.Decimal,9),
                    new SqlParameter("@差旅费", SqlDbType.Decimal,9),
                    new SqlParameter("@邮电费用", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@Comment", SqlDbType.NVarChar,500),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50)};
            parameters[0].Value = Version;
            parameters[1].Value = 工资及津贴;
            parameters[2].Value = 工程出包费;
            parameters[3].Value = 材料费;
            parameters[4].Value = 租赁费;
            parameters[5].Value = 劳务费;
            parameters[6].Value = 安全生产费用;
            parameters[7].Value = 办公费;
            parameters[8].Value = 维修费用;
            parameters[9].Value = 交通运输费用;
            parameters[10].Value = 差旅费;
            parameters[11].Value = 邮电费用;
            parameters[12].Value = 会议费;
            parameters[13].Value = 印刷费;
            parameters[14].Value = 水电费;
            parameters[15].Value = 其它费用;
            parameters[16].Value = CreatedTime;
            parameters[17].Value = Comment;
            parameters[18].Value = ParentId;
            parameters[19].Value = ID;
            parameters[20].Value = XMBH;
            parameters[21].Value = HTBH;

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
            strSql.Append("delete from [ERPBudgetDetail] ");
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
            strSql.Append(" FROM [ERPBudgetDetail] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Version"] != null && ds.Tables[0].Rows[0]["Version"].ToString() != "")
                {
                    this.Version = int.Parse(ds.Tables[0].Rows[0]["Version"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["工资及津贴"] != null && ds.Tables[0].Rows[0]["工资及津贴"].ToString() != "")
                {
                    this.工资及津贴 = decimal.Parse(ds.Tables[0].Rows[0]["工资及津贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["工程出包费"] != null && ds.Tables[0].Rows[0]["工程出包费"].ToString() != "")
                {
                    this.工程出包费 = decimal.Parse(ds.Tables[0].Rows[0]["工程出包费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["材料费"] != null && ds.Tables[0].Rows[0]["材料费"].ToString() != "")
                {
                    this.材料费 = decimal.Parse(ds.Tables[0].Rows[0]["材料费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["租赁费"] != null && ds.Tables[0].Rows[0]["租赁费"].ToString() != "")
                {
                    this.租赁费 = decimal.Parse(ds.Tables[0].Rows[0]["租赁费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["劳务费"] != null && ds.Tables[0].Rows[0]["劳务费"].ToString() != "")
                {
                    this.劳务费 = decimal.Parse(ds.Tables[0].Rows[0]["劳务费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["安全生产费用"] != null && ds.Tables[0].Rows[0]["安全生产费用"].ToString() != "")
                {
                    this.安全生产费用 = decimal.Parse(ds.Tables[0].Rows[0]["安全生产费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["办公费"] != null && ds.Tables[0].Rows[0]["办公费"].ToString() != "")
                {
                    this.办公费 = decimal.Parse(ds.Tables[0].Rows[0]["办公费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["维修费用"] != null && ds.Tables[0].Rows[0]["维修费用"].ToString() != "")
                {
                    this.维修费用 = decimal.Parse(ds.Tables[0].Rows[0]["维修费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["交通运输费用"] != null && ds.Tables[0].Rows[0]["交通运输费用"].ToString() != "")
                {
                    this.交通运输费用 = decimal.Parse(ds.Tables[0].Rows[0]["交通运输费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["差旅费"] != null && ds.Tables[0].Rows[0]["差旅费"].ToString() != "")
                {
                    this.差旅费 = decimal.Parse(ds.Tables[0].Rows[0]["差旅费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["邮电费用"] != null && ds.Tables[0].Rows[0]["邮电费用"].ToString() != "")
                {
                    this.邮电费用 = decimal.Parse(ds.Tables[0].Rows[0]["邮电费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["会议费"] != null && ds.Tables[0].Rows[0]["会议费"].ToString() != "")
                {
                    this.会议费 = decimal.Parse(ds.Tables[0].Rows[0]["会议费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["印刷费"] != null && ds.Tables[0].Rows[0]["印刷费"].ToString() != "")
                {
                    this.印刷费 = decimal.Parse(ds.Tables[0].Rows[0]["印刷费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["水电费"] != null && ds.Tables[0].Rows[0]["水电费"].ToString() != "")
                {
                    this.水电费 = decimal.Parse(ds.Tables[0].Rows[0]["水电费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["其它费用"] != null && ds.Tables[0].Rows[0]["其它费用"].ToString() != "")
                {
                    this.其它费用 = decimal.Parse(ds.Tables[0].Rows[0]["其它费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"] != null && ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    this.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Comment"] != null)
                {
                    this.Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
            }
        }
        public ZWL.BLL.ERPBudgetDetail GetModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPBudgetDetail>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [ERPBudgetDetail] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPBudgetDetail> GetListModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPBudgetDetail>(ds.Tables[0]);
            }
            return null;
        }
        public List<ZWL.BLL.ERPBudgetDetail> GetListModelByParentId(int parentId)
        {
            return GetListModel("ParentId=" + parentId);
        }

        /// <summary>
        /// 获得分页数据
        /// 
        /// </summary>
        public Pager GetListAndPaging(string strWhere, int currentPage, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,(工资及津贴 +工程出包费+材料费 +租赁费+劳务费 +安全生产费用  +办公费 +维修费用 +差旅费 +交通运输费用 +邮电费用+水电费+会议费+印刷费 + 其它费用) sum ");
            strSql.Append(" FROM ERPBudgetDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currentPage, pageSize);

        }

        #region MyRegion
        /// <summary>
        /// 动态获取需要相加的列
        /// </summary>
        /*
        DECLARE @sql NVARCHAR(MAX);

        -- 构建动态SQL查询语句
        SELECT @sql = 'SELECT *, (' +
            STUFF(
                (
                    SELECT '+' + 'ISNULL(' + QUOTENAME(COLUMN_NAME) + ', 0)' 
                    FROM information_schema.columns
                    WHERE table_name = 'ERPCostDetail' AND PATINDEX('%[a-z]%', LOWER(COLUMN_NAME)) <= 0 AND PATINDEX('%_备注', LOWER(COLUMN_NAME)) <= 0
                    FOR XML PATH('')
                ), 1, 1, ''
            ) + ') AS sum FROM ERPCostDetail WHERE xmbh=''X20190421'';';

        -- 执行动态SQL语句
        EXEC sp_executesql @sql; 
        */
        #endregion
        #endregion  Method
    }
}

