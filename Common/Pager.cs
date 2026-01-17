using System.Data;
using ZWL.DBUtility;

namespace ZWL.Common
{
    public class Pager
    {
        private string mainSQL = @"select * from 
            (select t.*,ROW_NUMBER() over(order by {0}) row from ({1}) t) u
            where row between {2} and {3}";
        private string subMainSQL = @"select * from 
            (select t.*,ROW_NUMBER() over(order by {0}) row from ({1}) t) u ";
        private string rowCountSQL = @"select count(1) from  ({0}) t";
        private int currentPage;
        private int pageSize;
        private int rows;
        private int totalPage;
        private string tempSQL;
        private string orderBy;
        public int CurrentPage { get { return currentPage; } }
        public int PageSize { get { return pageSize; } }
        public int Rows { get { return rows; } }
        public object Result { get; set; }
        public string SQL { get { return tempSQL; } }
        public string ResultSQL { get { return string.Format(mainSQL, orderBy, tempSQL, ((currentPage - 1) * pageSize) + 1, currentPage * pageSize); } }
        public string CountSQL { get { return string.Format(rowCountSQL, tempSQL); } }
        public int TotalPage
        {
            get
            {
                if (rows % pageSize == 0)
                    totalPage = rows / pageSize;
                else
                    totalPage = rows / pageSize + 1;
                return totalPage;
            }
        }
        public Pager() { }
        public Pager(string strSql)
        {
            tempSQL = strSql;
            currentPage = 1;
            pageSize = 20;
            orderBy = "ID desc";
        }
        public Pager(string strSql, int cpage)
        {
            tempSQL = strSql;
            currentPage = cpage;
            pageSize = 20;
            orderBy = "ID desc";
        }
        public Pager(string strSql, int cpage, int psize)
        {
            tempSQL = strSql;
            currentPage = cpage;
            pageSize = psize;
            orderBy = "ID desc";
        }
        public Pager(string strSql, int cpage, int psize, string strOrderBy)
        {
            tempSQL = strSql;
            currentPage = cpage;
            pageSize = psize;
            orderBy = strOrderBy;
        }
        public bool ExecuteToDataSet()
        {
            var rSqlCount = string.Format(rowCountSQL, tempSQL);
            rows = PublicMethod.GetInto(DbHelperSQL.GetSingle(rSqlCount));
            currentPage = TotalPage < currentPage ? 1 : currentPage;
            var rSql = string.Format(mainSQL, orderBy, tempSQL, ((currentPage - 1) * pageSize) + 1, currentPage * pageSize);
            var ds = DbHelperSQL.Query(rSql);
            Result = ds;
            return ds.Tables.Count > 0;
        }
        public bool ExecuteToDataTable()
        {
            DataTable result = null;
            var rSqlCount = string.Format(rowCountSQL, tempSQL);
            rows = PublicMethod.GetInto(DbHelperSQL.GetSingle(rSqlCount));
            currentPage = TotalPage < currentPage ? 1 : currentPage;
            var rSql = string.Format(mainSQL, orderBy, tempSQL, ((currentPage - 1) * pageSize) + 1, currentPage * pageSize);
            var ds = DbHelperSQL.Query(rSql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = ds.Tables[0];
            }
            Result = result;
            return ds != null && ds.Tables.Count > 0;
        }
        public bool ExecuteToDataTableWithOutPaging()
        {
            DataTable result = null;
            var rSql = string.Format(subMainSQL, orderBy, tempSQL);
            var rSqlCount = string.Format(rowCountSQL, tempSQL);
            var ds = DbHelperSQL.Query(rSql);

            if (ds != null && ds.Tables.Count > 0)
            {
                result = ds.Tables[0];
            }
            Result = result;
            rows = PublicMethod.GetInto(DbHelperSQL.GetSingle(rSqlCount));
            return ds != null && ds.Tables.Count > 0;
        }
        public void SetOuterResult(object result, int rowsCount)
        {
            Result = result;
            rows = rowsCount;
        }
    }
}
