namespace ZWL.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZWL.DBUtility;

    public class ERPCommon
    {
        public int Add()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ERPCommon (Code, CName, CType, CSort, CDescription, UpdateTime) values(");
            builder.Append("@code,@cname,@ctype,@csort,@cdescription,@updatetime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@code", SqlDbType.VarChar, 10), new SqlParameter("@cname", SqlDbType.NVarChar, 50), new SqlParameter("@ctype", SqlDbType.VarChar, 50), new SqlParameter("@csort", SqlDbType.Int, 4), new SqlParameter("@cdescription", SqlDbType.NVarChar, 200), new SqlParameter("@updatetime", SqlDbType.DateTime) };
            cmdParms[0].Value = this.Code;
            cmdParms[1].Value = this.CName;
            cmdParms[2].Value = this.CType;
            cmdParms[3].Value = this.CSort;
            cmdParms[4].Value = this.CDescription;
            cmdParms[5].Value = this.UpdateTime;
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete ERPCommon where ID=" + ID);
            return DbHelperSQL.ExecuteSQL(builder.ToString());
        }

        public bool Exists(int ID, string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ERPCommon ");
            if (!string.IsNullOrEmpty(str))
            {
                builder.Append(" where " + str);
            }
            else
            {
                builder.Append(" where ID=" + ID);
            }
            return DbHelperSQL.Exists(builder.ToString());
        }

        public DataSet GetList(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID, Code, CName, CType, CSort, CDescription, UpdateTime from ERPCommon ");
            if (!string.IsNullOrEmpty(str))
            {
                builder.Append(" where " + str);
            }
            return DbHelperSQL.Query(builder.ToString(), new SqlParameter[0]);
        }

        public void GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID, Code, CName, CType, CSort, CDescription, UpdateTime from ERPCommon where ID=" + ID + " ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 5) };
            cmdParms[0].Value = ID;
            DataSet set = new DataSet();
            set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
                }
                this.Code = set.Tables[0].Rows[0]["Code"].ToString();
                this.CName = set.Tables[0].Rows[0]["CName"].ToString();
                this.CType = set.Tables[0].Rows[0]["CType"].ToString();
                if (set.Tables[0].Rows[0]["CSort"].ToString() != "")
                {
                    this.CSort = int.Parse(set.Tables[0].Rows[0]["CSort"].ToString());
                }
                this.CDescription = set.Tables[0].Rows[0]["CDescription"].ToString();
                if (set.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    this.UpdateTime = DateTime.Parse(set.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
            }
        }

        public int Update()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ERPCommon set ");
            builder.Append("Code=@code,");
            builder.Append("CName=@cname,");
            builder.Append("CType=@ctype,");
            builder.Append("CSort=@csort,");
            builder.Append("CDescription=@cdescription,");
            builder.Append("UpdateTime=@updatetime ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@code", SqlDbType.VarChar, 10), new SqlParameter("@cname", SqlDbType.NVarChar, 50), new SqlParameter("@ctype", SqlDbType.VarChar, 50), new SqlParameter("@csort", SqlDbType.Int, 4), new SqlParameter("@cdescription", SqlDbType.NVarChar, 200), new SqlParameter("@updatetime", SqlDbType.DateTime), new SqlParameter("@ID", SqlDbType.Int, 5) };
            cmdParms[0].Value = this.Code;
            cmdParms[1].Value = this.CName;
            cmdParms[2].Value = this.CType;
            cmdParms[3].Value = this.CSort;
            cmdParms[4].Value = this.CDescription;
            cmdParms[5].Value = this.UpdateTime;
            cmdParms[6].Value = this.ID;
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public string CDescription { get; set; }

        public string CName { get; set; }

        public string Code { get; set; }

        public int CSort { get; set; }

        public string CType { get; set; }

        public int ID { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
