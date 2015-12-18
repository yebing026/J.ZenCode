using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using J.Common;
using J.Data.Model;
using J.Data.Enum;


namespace J.Data
{
    /// <summary>
    /// SqlHelper 的摘要说明
    /// </summary>
    public class DbMySqlHelper : IDbSql
    {
        private string ContString;

        public DbMySqlHelper(string conn)
        {
            ContString = conn;
        }

        private MySqlDataAdapter myAdapter;
        //数据查询操作 
        public DataSet ExecuteDataSet(String sql)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ContString;
            myConnection.Open();
            MySqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = sql;
            myAdapter = new MySqlDataAdapter(myCommand);
            DataSet mySet = new DataSet();
            myAdapter.Fill(mySet, "selectDa");
            myConnection.Close();
            return mySet;

        }
        public DataTable ExecuteDataTable(String sql)
        {
            DataSet mySet = ExecuteDataSet(sql);
            return mySet.Tables[0];

        }
        //数据插入,删除,更新操作 
        public int ExecuteNonQuery(String sql)
        {

            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ContString;
            myConnection.Open();
            MySqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = sql;
            var num = myCommand.ExecuteNonQuery();
            myConnection.Close();
            return num;
        }

        //返回得到data数据
        public string ExecuteScalar(string SQLStr)
        {
            DataTable datatable = ExecuteDataTable(SQLStr);
            string reData = datatable.Rows[0][0].ToString();
            return reData;
        }

        public List<string> GetDbSql()
        {
            var ls = new List<string>();           
            var tbs = ExecuteItem((int)DbItemType.Table);
            foreach (var tb in tbs)
            {
                ls.AddRange(ExecuteDataTable("SHOW CREATE TABLE " + tb).ToStrList(1));
            }
            
             var vws = ExecuteItem((int)DbItemType.View);
            foreach (var vw in vws)
            {
                ls.AddRange(ExecuteDataTable("SHOW CREATE VIEW " + vw).ToStrList(1));
            }            
            return ls;

        }
        public List<string> ExecuteItem(int type)
        {
            var ls = new List<string>();
            switch (type)
            {
                case (int)DbItemType.Table:
                    return ExecuteDataTable("show table status WHERE engine is  not null").ToStrList(0);
                case (int)DbItemType.View:
                    return ExecuteDataTable("show table status WHERE engine is null ").ToStrList(0);                   

            }
            return ls;

        }
        public DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            string sql = string.Format("select {0} from {1} where {2} order by {3} limit {4},{5}", selectFields, tableName, where, orderExpression, (startRowIndexId - 1) * maxNumberRows, maxNumberRows);
            return ExecuteDataTable(sql);
        }
        public IEnumerable<ITbField> ExecuteTbField(string tbName)
        {
            string sql = @"describe  $table".Replace("$table", tbName);            
            var fs = ExecuteDataTable(sql).ToList<MySqlTbField>();
            foreach (var f in fs)
            {
                ITbField _f = f;
                yield return _f;
            }
        }
    }
}