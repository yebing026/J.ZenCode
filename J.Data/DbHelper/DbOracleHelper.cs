using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using J.Common;
using J.Data.Model;
using J.Data.Enum;

namespace J.Data
{
    public class DbOracleHelper:IDbSql
    {
        private string ConnectionString;
        public DbOracleHelper(string conn)
        {
            ConnectionString = conn;
        }

        private void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            //set the command type
            command.CommandType = commandType;
            return;
        }



        public int ExecuteNonQuery(string commandText)
        {
            using (OracleConnection cn = new OracleConnection(ConnectionString))
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, cn, (OracleTransaction)null, CommandType.Text, commandText);
                int retval = cmd.ExecuteNonQuery();
                return retval;
            }
        }



        public DataSet ExecuteDataSet(string commandText)
        {
            using (OracleConnection cn = new OracleConnection(ConnectionString))
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, cn, (OracleTransaction)null, CommandType.Text, commandText);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public DataTable ExecuteDataTable(string cmdText)
        {
            DataSet ds = ExecuteDataSet(cmdText);
            return ds.Tables[0];
        }   
        public string ExecuteScalar(string commandText)
        {
            using (OracleConnection cn = new OracleConnection(ConnectionString))
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, cn, (OracleTransaction)null, CommandType.Text, commandText);
                object retval = cmd.ExecuteScalar();
                return string.Format("{0}", retval);
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
            switch (type)
            {
                case (int)DbItemType.Table:
                    return ExecuteDataTable("SELECT TNAME FROM TAB WHERE TABTYPE='TABLE'").ToStrList(0);

                case (int)DbItemType.View:
                    return ExecuteDataTable("SELECT TNAME FROM TAB WHERE TABTYPE='VIEW'").ToStrList(0);

            }
            return ls;

        }
        public DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            string sql = string.Format(@"SELECT *
  FROM (SELECT tt.*, ROWNUM AS rowno
          FROM (  SELECT {0}
                    FROM {1}
                   WHERE {2}
                ORDER BY {3}) tt
         WHERE ROWNUM <= {5}) table_alias
 WHERE table_alias.rowno >= {4};", selectFields, tableName, where, orderExpression, (startRowIndexId - 1) * maxNumberRows, maxNumberRows * startRowIndexId);
            return ExecuteDataTable(sql);
        }
        public IEnumerable<ITbField> ExecuteTbField(string tbName)
        {
            string sql = @"select COLUMN_ID,COLUMN_NAME as Name,
            DATA_TYPE as Type, 
            DECODE(DATA_TYPE, 'NUMBER',DATA_PRECISION, DATA_LENGTH) as _Length,
            NullAble,
            Data_default as  Default
             from USER_TAB_COLUMNS 
             where TABLE_NAME='$table'
             order by COLUMN_ID".Replace("$table", tbName);
            var fs = ExecuteDataTable(sql).ToList<OracleTbField>();
            foreach (var f in fs)
            {
                ITbField _f = f;
                yield return _f;
            }
        }
    }



}
