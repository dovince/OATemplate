
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPFoodProduct,菜品信息
    /// </summary>
    public class ERPFoodProduct
    {
        public ERPFoodProduct()
        { }
        #region Model
        private int _id;//主键

        private int _nworktodoid;

        private string _workname = "";

        private DateTime _dengjitime = DateTime.Now;

        private string _foodname = "";

        private string _foodcategory = "";

        private string _foodtype = "";

        private string _foodimage = "";


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
        /// 菜品名
        /// </summary>
        public string FoodName
        {
            set { _foodname = value; }
            get { return _foodname; }
        }

        /// <summary>
        /// 菜品种类
        /// </summary>
        public string FoodCategory
        {
            set { _foodcategory = value; }
            get { return _foodcategory; }
        }

        /// <summary>
        /// 菜品类别
        /// </summary>
        public string FoodType
        {
            set { _foodtype = value; }
            get { return _foodtype; }
        }

        /// <summary>
        /// 菜品图片
        /// </summary>
        public string FoodImage
        {
            set { _foodimage = value; }
            get { return _foodimage; }
        }


        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPFoodProduct(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,FoodName,FoodCategory,FoodType,FoodImage ");
            strSql.Append(" FROM [ERPFoodProduct] ");
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
                if (ds.Tables[0].Rows[0]["FoodName"] != null)
                {
                    this.FoodName = ds.Tables[0].Rows[0]["FoodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodCategory"] != null)
                {
                    this.FoodCategory = ds.Tables[0].Rows[0]["FoodCategory"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodType"] != null)
                {
                    this.FoodType = ds.Tables[0].Rows[0]["FoodType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodImage"] != null)
                {
                    this.FoodImage = ds.Tables[0].Rows[0]["FoodImage"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPFoodProduct]");
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
            strSql.Append("insert into [ERPFoodProduct] (");
            strSql.Append("NWorkToDoID,WorkName,DengJiTime,FoodName,FoodCategory,FoodType,FoodImage)");
            strSql.Append(" values (");
            strSql.Append("@NWorkToDoID,@WorkName,@DengJiTime,@FoodName,@FoodCategory,@FoodType,@FoodImage)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
        
					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@FoodName", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@FoodCategory", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FoodType", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FoodImage", SqlDbType.NVarChar, 200)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = FoodName;

            parameters[4].Value = FoodCategory;

            parameters[5].Value = FoodType;

            parameters[6].Value = FoodImage;


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
            strSql.Append("update [ERPFoodProduct] set ");

            strSql.Append("NWorkToDoID=@NWorkToDoID,");

            strSql.Append("WorkName=@WorkName,");

            strSql.Append("DengJiTime=@DengJiTime,");

            strSql.Append("FoodName=@FoodName,");

            strSql.Append("FoodCategory=@FoodCategory,");

            strSql.Append("FoodType=@FoodType,");

            strSql.Append("FoodImage=@FoodImage");

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {

					new SqlParameter("@NWorkToDoID", SqlDbType.Int),
       
					new SqlParameter("@WorkName", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@DengJiTime", SqlDbType.DateTime),
       
					new SqlParameter("@FoodName", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@FoodCategory", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FoodType", SqlDbType.NVarChar, 50),
       
					new SqlParameter("@FoodImage", SqlDbType.NVarChar, 200),
       
					new SqlParameter("@ID", SqlDbType.Int,4)};

            parameters[0].Value = NWorkToDoID;

            parameters[1].Value = WorkName;

            parameters[2].Value = DengJiTime;

            parameters[3].Value = FoodName;

            parameters[4].Value = FoodCategory;

            parameters[5].Value = FoodType;

            parameters[6].Value = FoodImage;

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
            strSql.Append("delete from [ERPFoodProduct] ");
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
            strSql.Append("select ID,NWorkToDoID,WorkName,DengJiTime,FoodName,FoodCategory,FoodType,FoodImage ");
            strSql.Append(" FROM [ERPFoodProduct] ");
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
                if (ds.Tables[0].Rows[0]["FoodName"] != null)
                {
                    this.FoodName = ds.Tables[0].Rows[0]["FoodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodCategory"] != null)
                {
                    this.FoodCategory = ds.Tables[0].Rows[0]["FoodCategory"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodType"] != null)
                {
                    this.FoodType = ds.Tables[0].Rows[0]["FoodType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FoodImage"] != null)
                {
                    this.FoodImage = ds.Tables[0].Rows[0]["FoodImage"].ToString();
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
            strSql.Append(" FROM [ERPFoodProduct] ");
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
            strSql.Append(" FROM ERPFoodProduct ");
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