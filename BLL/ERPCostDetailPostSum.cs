using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZWL.DBUtility;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPCostDetailPostSum。
    /// </summary>
    [Serializable]
    public partial class ERPCostDetailPostSum
    {
        public ERPCostDetailPostSum()
        { }
        #region Model
        private int _id;
        private int _parentid;
        private string _itemname;
        private int _sorting;
        private decimal _total = 0M;
        private decimal _工资及津贴 = 0M;
        private decimal _工程出包费 = 0M;
        private decimal _材料费 = 0M;
        private decimal _租赁费 = 0M;
        private decimal? _劳务费 = 0M;
        private decimal _安全生产费用 = 0M;
        private decimal _办公费 = 0M;
        private decimal _维修费用 = 0M;
        private decimal _交通运输费用 = 0M;
        private decimal _差旅费 = 0M;
        private decimal _邮电费用 = 0M;
        private decimal _其它费用 = 0M;
        private decimal? _水电费 = 0M;
        private decimal? _会议费 = 0M;
        private decimal? _印刷费 = 0M;
        private decimal? _节日补贴 = 0M;
        private decimal? _养老统筹 = 0M;
        private decimal? _福利费 = 0M;
        private decimal? _劳动保护费 = 0M;
        private decimal? _住房公积金 = 0M;
        private decimal? _住房补贴 = 0M;
        private decimal? _固定资产 = 0M;
        private decimal? _物业管理费 = 0M;
        private decimal? _培训费 = 0M;
        private decimal? _业务招待费 = 0M;
        private decimal? _工会经费 = 0M;
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
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sorting
        {
            set { _sorting = value; }
            get { return _sorting; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
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
        public decimal? 劳务费
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
        public decimal? 水电费
        {
            set { _水电费 = value; }
            get { return _水电费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 会议费
        {
            set { _会议费 = value; }
            get { return _会议费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 印刷费
        {
            set { _印刷费 = value; }
            get { return _印刷费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 节日补贴
        {
            set { _节日补贴 = value; }
            get { return _节日补贴; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 养老统筹
        {
            set { _养老统筹 = value; }
            get { return _养老统筹; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 福利费
        {
            set { _福利费 = value; }
            get { return _福利费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 劳动保护费
        {
            set { _劳动保护费 = value; }
            get { return _劳动保护费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 住房公积金
        {
            set { _住房公积金 = value; }
            get { return _住房公积金; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 住房补贴
        {
            set { _住房补贴 = value; }
            get { return _住房补贴; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 固定资产
        {
            set { _固定资产 = value; }
            get { return _固定资产; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 物业管理费
        {
            set { _物业管理费 = value; }
            get { return _物业管理费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 培训费
        {
            set { _培训费 = value; }
            get { return _培训费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 业务招待费
        {
            set { _业务招待费 = value; }
            get { return _业务招待费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? 工会经费
        {
            set { _工会经费 = value; }
            get { return _工会经费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPCostDetailPostSum(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPCostDetailPostSum ");
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
            strSql.Append("select count(1) from ERPCostDetailPostSum");
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
            strSql.Append("insert into [ERPCostDetailPostSum] (");
            strSql.Append("ParentId,ItemName,Sorting,Total,工资及津贴,工程出包费,材料费,租赁费,劳务费,安全生产费用,办公费,维修费用,交通运输费用,差旅费,邮电费用,其它费用,水电费,会议费,印刷费,节日补贴,养老统筹,福利费,劳动保护费,住房公积金,住房补贴,固定资产,物业管理费,培训费,业务招待费,工会经费)");
            strSql.Append(" values (");
            strSql.Append("@ParentId,@ItemName,@Sorting,@Total,@工资及津贴,@工程出包费,@材料费,@租赁费,@劳务费,@安全生产费用,@办公费,@维修费用,@交通运输费用,@差旅费,@邮电费用,@其它费用,@水电费,@会议费,@印刷费,@节日补贴,@养老统筹,@福利费,@劳动保护费,@住房公积金,@住房补贴,@固定资产,@物业管理费,@培训费,@业务招待费,@工会经费)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,100),
                    new SqlParameter("@Sorting", SqlDbType.Int,4),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
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
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@节日补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@养老统筹", SqlDbType.Decimal,9),
                    new SqlParameter("@福利费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳动保护费", SqlDbType.Decimal,9),
                    new SqlParameter("@住房公积金", SqlDbType.Decimal,9),
                    new SqlParameter("@住房补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@固定资产", SqlDbType.Decimal,9),
                    new SqlParameter("@物业管理费", SqlDbType.Decimal,9),
                    new SqlParameter("@培训费", SqlDbType.Decimal,9),
                    new SqlParameter("@业务招待费", SqlDbType.Decimal,9),
                    new SqlParameter("@工会经费", SqlDbType.Decimal,9)};
            parameters[0].Value = ParentId;
            parameters[1].Value = ItemName;
            parameters[2].Value = Sorting;
            parameters[3].Value = Total;
            parameters[4].Value = 工资及津贴;
            parameters[5].Value = 工程出包费;
            parameters[6].Value = 材料费;
            parameters[7].Value = 租赁费;
            parameters[8].Value = 劳务费;
            parameters[9].Value = 安全生产费用;
            parameters[10].Value = 办公费;
            parameters[11].Value = 维修费用;
            parameters[12].Value = 交通运输费用;
            parameters[13].Value = 差旅费;
            parameters[14].Value = 邮电费用;
            parameters[15].Value = 其它费用;
            parameters[16].Value = 水电费;
            parameters[17].Value = 会议费;
            parameters[18].Value = 印刷费;
            parameters[19].Value = 节日补贴;
            parameters[20].Value = 养老统筹;
            parameters[21].Value = 福利费;
            parameters[22].Value = 劳动保护费;
            parameters[23].Value = 住房公积金;
            parameters[24].Value = 住房补贴;
            parameters[25].Value = 固定资产;
            parameters[26].Value = 物业管理费;
            parameters[27].Value = 培训费;
            parameters[28].Value = 业务招待费;
            parameters[29].Value = 工会经费;

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
            strSql.Append("update [ERPCostDetailPostSum] set ");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("ItemName=@ItemName,");
            strSql.Append("Sorting=@Sorting,");
            strSql.Append("Total=@Total,");
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
            strSql.Append("其它费用=@其它费用,");
            strSql.Append("水电费=@水电费,");
            strSql.Append("会议费=@会议费,");
            strSql.Append("印刷费=@印刷费,");
            strSql.Append("节日补贴=@节日补贴,");
            strSql.Append("养老统筹=@养老统筹,");
            strSql.Append("福利费=@福利费,");
            strSql.Append("劳动保护费=@劳动保护费,");
            strSql.Append("住房公积金=@住房公积金,");
            strSql.Append("住房补贴=@住房补贴,");
            strSql.Append("固定资产=@固定资产,");
            strSql.Append("物业管理费=@物业管理费,");
            strSql.Append("培训费=@培训费,");
            strSql.Append("业务招待费=@业务招待费,");
            strSql.Append("工会经费=@工会经费");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar,100),
                    new SqlParameter("@Sorting", SqlDbType.Int,4),
                    new SqlParameter("@Total", SqlDbType.Decimal,9),
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
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@节日补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@养老统筹", SqlDbType.Decimal,9),
                    new SqlParameter("@福利费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳动保护费", SqlDbType.Decimal,9),
                    new SqlParameter("@住房公积金", SqlDbType.Decimal,9),
                    new SqlParameter("@住房补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@固定资产", SqlDbType.Decimal,9),
                    new SqlParameter("@物业管理费", SqlDbType.Decimal,9),
                    new SqlParameter("@培训费", SqlDbType.Decimal,9),
                    new SqlParameter("@业务招待费", SqlDbType.Decimal,9),
                    new SqlParameter("@工会经费", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ParentId;
            parameters[1].Value = ItemName;
            parameters[2].Value = Sorting;
            parameters[3].Value = Total;
            parameters[4].Value = 工资及津贴;
            parameters[5].Value = 工程出包费;
            parameters[6].Value = 材料费;
            parameters[7].Value = 租赁费;
            parameters[8].Value = 劳务费;
            parameters[9].Value = 安全生产费用;
            parameters[10].Value = 办公费;
            parameters[11].Value = 维修费用;
            parameters[12].Value = 交通运输费用;
            parameters[13].Value = 差旅费;
            parameters[14].Value = 邮电费用;
            parameters[15].Value = 其它费用;
            parameters[16].Value = 水电费;
            parameters[17].Value = 会议费;
            parameters[18].Value = 印刷费;
            parameters[19].Value = 节日补贴;
            parameters[20].Value = 养老统筹;
            parameters[21].Value = 福利费;
            parameters[22].Value = 劳动保护费;
            parameters[23].Value = 住房公积金;
            parameters[24].Value = 住房补贴;
            parameters[25].Value = 固定资产;
            parameters[26].Value = 物业管理费;
            parameters[27].Value = 培训费;
            parameters[28].Value = 业务招待费;
            parameters[29].Value = 工会经费;
            parameters[30].Value = ID;

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
            strSql.Append("delete from ERPCostDetailPostSum ");
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
            strSql.Append(" FROM ERPCostDetailPostSum ");
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
                if (ds.Tables[0].Rows[0]["Sorting"] != null && ds.Tables[0].Rows[0]["Sorting"].ToString() != "")
                {
                    this.Sorting = int.Parse(ds.Tables[0].Rows[0]["Sorting"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Total"] != null && ds.Tables[0].Rows[0]["Total"].ToString() != "")
                {
                    this.Total = decimal.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
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
                if (ds.Tables[0].Rows[0]["其它费用"] != null && ds.Tables[0].Rows[0]["其它费用"].ToString() != "")
                {
                    this.其它费用 = decimal.Parse(ds.Tables[0].Rows[0]["其它费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["水电费"] != null && ds.Tables[0].Rows[0]["水电费"].ToString() != "")
                {
                    this.水电费 = decimal.Parse(ds.Tables[0].Rows[0]["水电费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["会议费"] != null && ds.Tables[0].Rows[0]["会议费"].ToString() != "")
                {
                    this.会议费 = decimal.Parse(ds.Tables[0].Rows[0]["会议费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["印刷费"] != null && ds.Tables[0].Rows[0]["印刷费"].ToString() != "")
                {
                    this.印刷费 = decimal.Parse(ds.Tables[0].Rows[0]["印刷费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["节日补贴"] != null && ds.Tables[0].Rows[0]["节日补贴"].ToString() != "")
                {
                    this.节日补贴 = decimal.Parse(ds.Tables[0].Rows[0]["节日补贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["养老统筹"] != null && ds.Tables[0].Rows[0]["养老统筹"].ToString() != "")
                {
                    this.养老统筹 = decimal.Parse(ds.Tables[0].Rows[0]["养老统筹"].ToString());
                }
                if (ds.Tables[0].Rows[0]["福利费"] != null && ds.Tables[0].Rows[0]["福利费"].ToString() != "")
                {
                    this.福利费 = decimal.Parse(ds.Tables[0].Rows[0]["福利费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["劳动保护费"] != null && ds.Tables[0].Rows[0]["劳动保护费"].ToString() != "")
                {
                    this.劳动保护费 = decimal.Parse(ds.Tables[0].Rows[0]["劳动保护费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["住房公积金"] != null && ds.Tables[0].Rows[0]["住房公积金"].ToString() != "")
                {
                    this.住房公积金 = decimal.Parse(ds.Tables[0].Rows[0]["住房公积金"].ToString());
                }
                if (ds.Tables[0].Rows[0]["住房补贴"] != null && ds.Tables[0].Rows[0]["住房补贴"].ToString() != "")
                {
                    this.住房补贴 = decimal.Parse(ds.Tables[0].Rows[0]["住房补贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["固定资产"] != null && ds.Tables[0].Rows[0]["固定资产"].ToString() != "")
                {
                    this.固定资产 = decimal.Parse(ds.Tables[0].Rows[0]["固定资产"].ToString());
                }
                if (ds.Tables[0].Rows[0]["物业管理费"] != null && ds.Tables[0].Rows[0]["物业管理费"].ToString() != "")
                {
                    this.物业管理费 = decimal.Parse(ds.Tables[0].Rows[0]["物业管理费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["培训费"] != null && ds.Tables[0].Rows[0]["培训费"].ToString() != "")
                {
                    this.培训费 = decimal.Parse(ds.Tables[0].Rows[0]["培训费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["业务招待费"] != null && ds.Tables[0].Rows[0]["业务招待费"].ToString() != "")
                {
                    this.业务招待费 = decimal.Parse(ds.Tables[0].Rows[0]["业务招待费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["工会经费"] != null && ds.Tables[0].Rows[0]["工会经费"].ToString() != "")
                {
                    this.工会经费 = decimal.Parse(ds.Tables[0].Rows[0]["工会经费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemName"] != null)
                {
                    this.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
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
            strSql.Append(" FROM ERPCostDetailPostSum ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

