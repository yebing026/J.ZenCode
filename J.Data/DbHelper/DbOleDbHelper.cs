using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using J.Common;
using J.Data.Model;
using J.Data.Enum;

namespace J.Data
{
    public  class DbOleDbHelper : IDbSql
    {
        private string _strconn;
        public DbOleDbHelper(string strconn)
        {
            _strconn = strconn;
        }
        public OleDbCommand MakeCommand(OleDbConnection conn, string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            if (conn.State != ConnectionState.Open)
                conn.Open();           
            return cmd;
        }
        public string ExecuteScalar(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_strconn))
            {
                var num=MakeCommand(conn, sql).ExecuteScalar();
                return string.Format("{0}", num);
            }
        }
        public int ExecuteNonQuery(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_strconn))
            {
               return MakeCommand(conn, sql).ExecuteNonQuery();
            }
        }
        public  DataSet ExecuteDataSet(string sql)
        {
            DataSet dataset = new DataSet();           
            using (OleDbConnection conn = new OleDbConnection(_strconn))
            {
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            da.Fill(dataset);
            return dataset;
                }
        }
        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataSet(sql).Tables[0];
        }
        public IDataReader ExecuteReader(string sql)
        {
            OleDbConnection conn = new OleDbConnection(_strconn);
            try
            {
                OleDbCommand cmd = MakeCommand(conn, sql);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public List<string> GetDbSql()
        {
            var ls = new List<string>();
            ls.Add("暂不支持");
            return ls;

        }
        public List<string> ExecuteItem(int type)
        {
            var ls = new List<string>();
            OleDbConnection conn = new OleDbConnection(_strconn);
            conn.Open();
            switch (type)
            {
                case (int)DbItemType.Table:
                    ls= conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" }).ToStrList(2);
                    break;
                case (int)DbItemType.View:
                    ls= conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "VIEW" }).ToStrList(2);
                    break;
            }
            conn.Close();
            return ls;
        }
        public DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            string sql = string.Format("select top 1000 {0} from {1} where {2} order by {3}", selectFields, tableName, where, orderExpression, (startRowIndexId - 1) * maxNumberRows, maxNumberRows);
            return ExecuteDataTable(sql);
        }
        public IEnumerable<ITbField> ExecuteTbField(string tbName)
        {
            OleDbConnection conn = new OleDbConnection(_strconn);
            conn.Open();
            DataTable columnTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tbName, null });           
            conn.Close();
            var fs = columnTable.ToList<OleDbTbField>().OrderBy(z=>z.ORDINAL_POSITION);
            foreach (var f in fs)
            {
                ITbField _f = f;
                yield return _f;
            }
           
        }
    }
}