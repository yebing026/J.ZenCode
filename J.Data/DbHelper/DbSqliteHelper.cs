using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using J.Data.Model;
using J.Common;
using J.Data.Enum;


namespace J.Data
{

    public class DbSqliteHelper : IDbSql
    {
        //abmartstorageConnectionString
        private string ConnectionString;

        public DbSqliteHelper(string conn)
        {
            ConnectionString = conn;
        }

        private  void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params SQLiteParameter[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                    //cmd.Parameters.AddWithValue(string.Empty, parm);
                    cmd.Parameters.Add(parm);
            }
        }

        public DataSet ExecuteDataSet(string cmdText)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(command, conn, cmdText,null);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        public DataTable ExecuteDataTable(string cmdText)        {
           DataSet ds= ExecuteDataSet( cmdText);            
           return ds.Tables[0];               
        }        
        public  int ExecuteNonQuery(string cmdText)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText, null);
                    return command.ExecuteNonQuery();
                }
            }
        }
        public  SQLiteDataReader ExecuteReader(string cmdText, params SQLiteParameter[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }
        public  string ExecuteScalar(string cmdText)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, cmdText,null);
                    return string.Format("{0}",command.ExecuteScalar());
                }
            }
        }

        public List<string> GetDbSql()
        {
            var ls = new List<string>();
             ls.AddRange(ExecuteDataTable("select sql from sqlite_master  where name not like 'sqlite%' order by type,name").ToStrList(0));            
            return ls;
        }
        public List<string> ExecuteItem(int type) {
            var ls = new List<string>();
            switch (type)
            {
                case (int)DbItemType.Table:
                    return ExecuteDataTable("SELECT name as Name FROM sqlite_master where name not like 'sqlite%' and type='table'").ToStrList(0);

                case (int)DbItemType.View:
                    return ExecuteDataTable("SELECT name as Name FROM sqlite_master where type='view'").ToStrList(0);
              
            }
            return ls;
        
        }
        public DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows){
            string sql = string.Format("select {0} from {1} where {2} order by {3} limit {4},{5}", selectFields, tableName, where, orderExpression, (startRowIndexId - 1) * maxNumberRows, maxNumberRows);
            return ExecuteDataTable(sql);
        }
        public IEnumerable<ITbField> ExecuteTbField(string tbName)
        {
            string sql = @"PRAGMA table_info($table)".Replace("$table", tbName);            
            var fs = ExecuteDataTable(sql).ToList<SqliteTbField>();
            foreach (var f in fs)
            {
                ITbField _f = f;
                yield return _f;
            }
        }
    }
}