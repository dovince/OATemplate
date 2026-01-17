using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;
using System.Collections.Generic;//Please add references

namespace ZWL.BLL
{
    public class ContractTeamMoney
    {
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ContractTeamID
        /// </summary>		
        private int _contractteamid;
        public int ContractTeamID
        {
            get { return _contractteamid; }
            set { _contractteamid = value; }
        }
        /// <summary>
        /// 年份
        /// </summary>		
        private string _year;
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        /// <summary>
        /// 营业额
        /// </summary>		
        private decimal _money;
        public decimal Money
        {
            get { return _money; }
            set { _money = value; }
        }
        /// <summary>
        /// 工程名称
        /// </summary>		
        private string _gcname;
        public string GCName
        {
            get { return _gcname; }
            set { _gcname = value; }
        }
        /// <summary>
        /// 结构
        /// </summary>		
        private string _structure;
        public string Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }
        /// <summary>
        /// 规模
        /// </summary>		
        private string _size;
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        /// <summary>
        /// 质量
        /// </summary>		
        private string _quality;
        public string Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }
        /// <summary>
        /// 履约情况
        /// </summary>		
        private string _performance;
        public string Performance
        {
            get { return _performance; }
            set { _performance = value; }
        }


        public bool Exists(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ContractTeamMoney");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = nID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ContractTeamMoney(");
            strSql.Append("ContractTeamID,Year,Money,GCName,Structure,Size,Quality,Performance");
            strSql.Append(") values (");
            strSql.Append("@ContractTeamID,@Year,@Money,@GCName,@Structure,@Size,@Quality,@Performance");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ContractTeamID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Year", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCName", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Structure", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Size", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Quality", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Performance", SqlDbType.NText)             
              
            };

            parameters[0].Value = ContractTeamID;
            parameters[1].Value = Year;
            parameters[2].Value = Money;
            parameters[3].Value = GCName;
            parameters[4].Value = Structure;
            parameters[5].Value = Size;
            parameters[6].Value = Quality;
            parameters[7].Value = Performance;

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
            strSql.Append("update ContractTeamMoney set ");

            strSql.Append(" ContractTeamID = @ContractTeamID , ");
            strSql.Append(" Year = @Year , ");
            strSql.Append(" Money = @Money , ");
            strSql.Append(" GCName = @GCName , ");
            strSql.Append(" Structure = @Structure , ");
            strSql.Append(" Size = @Size , ");
            strSql.Append(" Quality = @Quality , ");
            strSql.Append(" Performance = @Performance  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ContractTeamID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Year", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCName", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Structure", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Size", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Quality", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Performance", SqlDbType.NText)             
              
            };

            parameters[0].Value = ID;
            parameters[1].Value = ContractTeamID;
            parameters[2].Value = Year;
            parameters[3].Value = Money;
            parameters[4].Value = GCName;
            parameters[5].Value = Structure;
            parameters[6].Value = Size;
            parameters[7].Value = Quality;
            parameters[8].Value = Performance;
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
        public bool Delete(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ContractTeamMoney ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = nID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ContractTeamMoney ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public void GetModel(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ContractTeamID, Year, Money, GCName, Structure, Size, Quality, Performance  ");
            strSql.Append("  from ContractTeamMoney ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = nID;


            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractTeamID"].ToString() != "")
                {
                    ContractTeamID = int.Parse(ds.Tables[0].Rows[0]["ContractTeamID"].ToString());
                }
                Year = ds.Tables[0].Rows[0]["Year"].ToString();
                if (ds.Tables[0].Rows[0]["Money"].ToString() != "")
                {
                    Money = decimal.Parse(ds.Tables[0].Rows[0]["Money"].ToString());
                }
                GCName = ds.Tables[0].Rows[0]["GCName"].ToString();
                Structure = ds.Tables[0].Rows[0]["Structure"].ToString();
                Size = ds.Tables[0].Rows[0]["Size"].ToString();
                Quality = ds.Tables[0].Rows[0]["Quality"].ToString();
                Performance = ds.Tables[0].Rows[0]["Performance"].ToString();

            }
            else
            {
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ContractTeamMoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ContractTeamMoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得多个数据实体
        /// </summary>
        public List<ZWL.BLL.ContractTeamMoney> GetModelList(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.ConvertTo_1<ZWL.BLL.ContractTeamMoney>(dt);
            }
            return new List<ZWL.BLL.ContractTeamMoney>();
        }


        /// <summary>
        /// 新增一条记录，如果该记录存在，修改该记录
        /// </summary>
        /// <param name="strWhere">检测该数据是否存在</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(string strWhere, ContractTeamMoney model)
        {
            ContractTeamMoney Model = new ContractTeamMoney();
            string sql = "select ID from ContractTeamMoney where " + strWhere;
            string ID = ZWL.DBUtility.DbHelperSQL.GetSHSLInt(sql);
            if ("0" == ID)
            {
                if (model.Add() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                Model.GetModel(int.Parse(ID));
                Model = model;
                Model.ID = int.Parse(ID);
                return Model.Update();
            }
        }
    }
}
