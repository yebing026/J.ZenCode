using System.Data;
using J.Common;
using J.Data;

namespace J.ZenCode
{
    public static class PageTool
    {

        public static DataTable GetPageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            var conn = ConfigHelper.GetConfigConnString("zenCode");
            var db = new J.Data.DbSqliteHelper(conn);
            return db.ExecutePageData(selectFields, tableName, where, orderExpression, startRowIndexId, maxNumberRows);
        }

        public static int GetRecordCount(string tableName, string where)
        {
            string sql = string.Format("select count(*) from {0} where {1}", tableName, where);
            var conn = ConfigHelper.GetConfigConnString("zenCode");
            var db = new J.Data.DbSqliteHelper(conn);
            return int.Parse(db.ExecuteScalar(sql));
        }
        public static DataTable GetPageData1(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            IDbSql _db = DBFactory.CreateDB();
            return _db.ExecutePageData(selectFields, tableName, where, orderExpression, startRowIndexId, maxNumberRows);
        }

        public static int GetRecordCount1(string tableName, string where)
        {
            IDbSql _db = DBFactory.CreateDB();
            string sql = string.Format("select count(*) from {0} where {1}", tableName, where);            
            string num = _db.ExecuteScalar(sql);
            return int.Parse(num);
        }
    }
}