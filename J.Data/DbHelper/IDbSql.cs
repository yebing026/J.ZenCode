namespace J.Data
{
    using System.Collections.Generic;
    using System.Data;
    using J.Data.Model;

    public interface IDbSql
    {
        int ExecuteNonQuery(string cmdText);
        string ExecuteScalar(string cmdText);
        DataTable ExecuteDataTable(string cmdText);
        DataSet ExecuteDataSet(string cmdText);
        DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows);
        List<string> ExecuteItem(int type);
        List<string> GetDbSql();
        IEnumerable<ITbField> ExecuteTbField(string tbName);
    }
}

