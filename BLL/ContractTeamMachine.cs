using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;
using System.Collections.Generic;//Please add references

namespace ZWL.BLL
{
    /// <summary>
    /// 专业承包、劳务分包施工队伍自有主要机械
    /// </summary>
    public class ContractTeamMachine
    {
        /// <summary>
        /// 自由主要机械ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 劳务分包施工队伍ID
        /// </summary>		
        private int _contractteamid;
        public int ContractTeamID
        {
            get { return _contractteamid; }
            set { _contractteamid = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 型号
        /// </summary>		
        private string _model;
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        /// <summary>
        /// 功率
        /// </summary>		
        private string _power;
        public string Power
        {
            get { return _power; }
            set { _power = value; }
        }
        /// <summary>
        /// 数量
        /// </summary>		
        private int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        /// <summary>
        /// 产地
        /// </summary>		
        private string _origin;
        public string Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ContractTeamMachine");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ContractTeamMachine(");
            strSql.Append("ContractTeamID,Name,Model,Power,Number,Origin,State");
            strSql.Append(") values (");
            strSql.Append("@ContractTeamID,@Name,@Model,@Power,@Number,@Origin,@State");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ContractTeamID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Model", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Power", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Number", SqlDbType.Int,4) ,            
                        new SqlParameter("@Origin", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@State", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = ContractTeamID;
            parameters[1].Value = Name;
            parameters[2].Value = Model;
            parameters[3].Value = Power;
            parameters[4].Value = Number;
            parameters[5].Value = Origin;
            parameters[6].Value = State;

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
            strSql.Append("update ContractTeamMachine set ");

            strSql.Append(" ContractTeamID = @ContractTeamID , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Model = @Model , ");
            strSql.Append(" Power = @Power , ");
            strSql.Append(" Number = @Number , ");
            strSql.Append(" Origin = @Origin , ");
            strSql.Append(" State = @State  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ContractTeamID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Model", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Power", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Number", SqlDbType.Int,4) ,            
                        new SqlParameter("@Origin", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@State", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = ID;
            parameters[1].Value = ContractTeamID;
            parameters[2].Value = Name;
            parameters[3].Value = Model;
            parameters[4].Value = Power;
            parameters[5].Value = Number;
            parameters[6].Value = Origin;
            parameters[7].Value = State;
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
            strSql.Append("delete from ContractTeamMachine ");
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
            strSql.Append("delete from ContractTeamMachine ");
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
            strSql.Append("select ID, ContractTeamID, Name, Model, Power, Number, Origin, State  ");
            strSql.Append("  from ContractTeamMachine ");
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
                Name = ds.Tables[0].Rows[0]["Name"].ToString();
                Model = ds.Tables[0].Rows[0]["Model"].ToString();
                Power = ds.Tables[0].Rows[0]["Power"].ToString();
                if (ds.Tables[0].Rows[0]["Number"].ToString() != "")
                {
                    Number = int.Parse(ds.Tables[0].Rows[0]["Number"].ToString());
                }
                Origin = ds.Tables[0].Rows[0]["Origin"].ToString();
                State = ds.Tables[0].Rows[0]["State"].ToString();


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
            strSql.Append(" FROM ContractTeamMachine ");
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
            strSql.Append(" FROM ContractTeamMachine ");
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
        public List<ZWL.BLL.ContractTeamMachine> GetModelList(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.ConvertTo_1<ZWL.BLL.ContractTeamMachine>(dt);
            }
            return new List<ZWL.BLL.ContractTeamMachine>();
        }

        /// <summary>
        /// 新增一条记录，如果该记录存在，修改该记录
        /// </summary>
        /// <param name="strWhere">检测该数据是否存在</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(string strWhere, ContractTeamMachine model)
        {
            ContractTeamMachine Model = new ContractTeamMachine();
            string sql = "select ID from ContractTeamMachine where " + strWhere;
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
                //this.model = model;
                Model.ID = int.Parse(ID);
                return Model.Update();
            }
        }

    }
}
