
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPFoodOrder,订餐信息
    /// </summary>
    public class ERPFoodOrder
    {
        public ERPFoodOrder()
        { }
        #region Model
        private int _id;//主键

        private int _nworktodoid;

        private string _workname = "";

        private DateTime _dengjitime = DateTime.Now;

        private string _orderid = "";

        private string _ordername = "";

        private string _orderdep = "";

        private string _orderfoodid = "";

        private string _orderfoodname = "";

        private double _ordernum;

        private string _ordertype = "";

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
        /// 订单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 订餐人姓名
        /// </summary>
        public string OrderName
        {
            set { _ordername = value; }
            get { return _ordername; }
        }

        /// <summary>
        /// 订餐人部门
        /// </summary>
        public string OrderDep
        {
            set { _orderdep = value; }
            get { return _orderdep; }
        }

        /// <summary>
        /// 选择菜品
        /// </summary>
        public string OrderFoodid
        {
            set { _orderfoodid = value; }
            get { return _orderfoodid; }
        }

        /// <summary>
        /// 选择菜品名称
        /// </summary>
        public string OrderFoodName
        {
            set { _orderfoodname = value; }
            get { return _orderfoodname; }
        }

        /// <summary>
        /// 选择数量
        /// </summary>
        public double OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }

        /// <summary>
        /// 订餐类别
        /// </summary>
        public string OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
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
        public ERPFoodOrder(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,OrderId,OrderName,OrderDep,OrderFoodid,OrderFoodName,OrderNum,OrderType,BZ ");
            strSql.Append(" FROM [ERPFoodOrder] ");
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
                if (ds.Tables[0].Rows[0]["OrderId"] != null)
                {
                    this.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderName"] != null)
                {
                    this.OrderName = ds.Tables[0].Rows[0]["OrderName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderDep"] != null)
                {
                    this.OrderDep = ds.Tables[0].Rows[0]["OrderDep"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderFoodid"] != null)
                {
                    this.OrderFoodid = ds.Tables[0].Rows[0]["OrderFoodid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderFoodName"] != null)
                {
                    this.OrderFoodName = ds.Tables[0].Rows[0]["OrderFoodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderNum"] != null && ds.Tables[0].Rows[0]["OrderNum"].ToString() != "")
                {
                    this.OrderNum = Convert.ToDouble(ds.Tables[0].Rows[0]["OrderNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderType"] != null)
                {
                    this.OrderType = ds.Tables[0].Rows[0]["OrderType"].ToString();
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
            strSql.Append("select count(1) from [ERPFoodOrder]");
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
            strSql.Append("insert into [ERPFoodOrder] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,OrderId,OrderName,OrderDep,OrderFoodid,OrderFoodName,OrderNum,OrderType,BZ)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@OrderId,@OrderName,@OrderDep,@OrderFoodid,@OrderFoodName,@OrderNum,@OrderType,@BZ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@OrderId", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderDep", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderFoodid", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderFoodName", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@OrderNum", SqlDbType.Float),
       
					new SqlParameter("@OrderType", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BZ", SqlDbType.NVarChar, 200)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = OrderId;

            parameters[4].Value = OrderName;

            parameters[5].Value = OrderDep;

            parameters[6].Value = OrderFoodid;

            parameters[7].Value = OrderFoodName;

            parameters[8].Value = OrderNum;

            parameters[9].Value = OrderType;

            parameters[10].Value = BZ;


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
            strSql.Append("update [ERPFoodOrder] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("OrderId=@OrderId,");

            strSql.Append("OrderName=@OrderName,");

            strSql.Append("OrderDep=@OrderDep,");

            strSql.Append("OrderFoodid=@OrderFoodid,");

            strSql.Append("OrderFoodName=@OrderFoodName,");

            strSql.Append("OrderNum=@OrderNum,");

            strSql.Append("OrderType=@OrderType,");

            strSql.Append("BZ=@BZ");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@OrderId", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderDep", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderFoodid", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@OrderFoodName", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@OrderNum", SqlDbType.Float),
       
					new SqlParameter("@OrderType", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@BZ", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = OrderId;

            parameters[4].Value = OrderName;

            parameters[5].Value = OrderDep;

            parameters[6].Value = OrderFoodid;

            parameters[7].Value = OrderFoodName;

            parameters[8].Value = OrderNum;

            parameters[9].Value = OrderType;

            parameters[10].Value = BZ;

            parameters[11].Value = ID;

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
            strSql.Append("delete from [ERPFoodOrder] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,OrderId,OrderName,OrderDep,OrderFoodid,OrderFoodName,OrderNum,OrderType,BZ ");
            strSql.Append(" FROM [ERPFoodOrder] ");
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
                if (ds.Tables[0].Rows[0]["OrderId"] != null)
                {
                    this.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderName"] != null)
                {
                    this.OrderName = ds.Tables[0].Rows[0]["OrderName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderDep"] != null)
                {
                    this.OrderDep = ds.Tables[0].Rows[0]["OrderDep"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderFoodid"] != null)
                {
                    this.OrderFoodid = ds.Tables[0].Rows[0]["OrderFoodid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderFoodName"] != null)
                {
                    this.OrderFoodName = ds.Tables[0].Rows[0]["OrderFoodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderNum"] != null && ds.Tables[0].Rows[0]["OrderNum"].ToString() != "")
                {
                    this.OrderNum = Convert.ToDouble(ds.Tables[0].Rows[0]["OrderNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderType"] != null)
                {
                    this.OrderType = ds.Tables[0].Rows[0]["OrderType"].ToString();
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
            strSql.Append(" FROM [ERPFoodOrder] ");
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
            strSql.Append(" FROM ERPFoodOrder ");
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