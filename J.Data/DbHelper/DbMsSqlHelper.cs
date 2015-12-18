using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using J.Common;
using J.Data.Model;
using J.Data.Enum;

//该源码下载自www.51aspx.com(５１ａｓｐｘ．ｃｏｍ)
namespace J.Data
{
    /// <summary>
    /// SqlHelper 的摘要说明
    /// </summary>
    public class DbMsSqlHelper : IDbSql
    {
        private string connectionString;

        public DbMsSqlHelper(string conn)
        {
            connectionString = conn;
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>

        #region ExecuteNonQuery
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParams)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(con, cmd, cmdType, cmdText, commandParams);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }

        public int ExecuteNonQuery(string cmdText, params SqlParameter[] commandParams)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(con, cmd, CommandType.Text, cmdText, commandParams);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }


        public int ExecuteNonQuery(string cmdText)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    con.Open();
                    int val = cmd.ExecuteNonQuery();
                    return val;
                }
            }
        }

        public int QueryInsertIdentity(string sql)
        {
            int iResult = -1;
            try
            {
                iResult = Convert.ToInt32(ExecuteScalar(sql + "; select @@IDENTITY"));
            }
            catch
            {
            }
            return iResult;
        }
        #endregion

        #region ExecuteReader

        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(con, cmd, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                con.Close();
                throw;
            }
        }
        public SqlDataReader ExecuteReader(string cmdText)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(con, cmd, CommandType.Text, cmdText, null);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch
            {
                con.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteDataSet


        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] para)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlCommand cmd = new SqlCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(con, cmd, cmdType, cmdText, para);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    return ds;
                }
            }
        }



        public DataSet ExecuteDataSet(string cmdtext)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(con, cmd, CommandType.Text, cmdtext, null);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    return ds;
                }
            }
        }

        /// <summary>
        /// 根据指定的SQL语句,返回DATASET
        /// </summary>
        /// <param name="cmdtext">要执行带参的SQL语句</param>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdtext, params SqlParameter[] para)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(con, cmd, CommandType.Text, cmdtext, para);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    return ds;
                }
            }
        }


        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// 根据条件获取指定表中的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string tablename, string where)
        {
            string s = "select * from " + tablename;
            if (where != "")
                s += " where " + where;

            return ExecuteDataSet(s).Tables[0];
        }
        /// <summary>
        /// 根据条件获取指定表中的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataSet(sql).Tables[0];
        }


        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(con, cmd, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }


        public string ExecuteScalar(string cmdText)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    con.Open();
                    return string.Format("{0}", cmd.ExecuteScalar());
                }
            }
        }


        public object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(con, cmd, CommandType.Text, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }
        #endregion

        #region 建立SqlCommand
        /// <summary>
        /// 建立SqlCommand
        /// </summary>
        /// <param name="con">SqlConnection　对象</param>
        /// <param name="cmd">要建立的Command</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="cmdParms">参数</param>
        private void PrepareCommand(SqlConnection con, SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            cmd.Connection = con;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (cmdParms != null)
                foreach (SqlParameter para in cmdParms)
                    cmd.Parameters.Add(para);
        }

        #endregion

        public List<string> GetDbSql(){
            var ls = new List<string>();
            ls.Add("创建表暂不支持");
            var sql = "SELECT definition FROM sys.sql_modules JOIN sys.objects ON sys.sql_modules.object_id = sys.objects.object_id order by type desc";
            ls.AddRange(ExecuteDataTable(sql).ToStrList(0));
            return ls;
        
        }
        public List<string> ExecuteItem(int type)
        {
            var ls = new List<string>();
            switch (type)
            {
                case (int)DbItemType.Table:
                    return ExecuteDataTable(@"SELECT TABLE_NAME AS Name
FROM INFORMATION_SCHEMA.TABLES
WHERE (TABLE_TYPE = 'BASE TABLE') order by TABLE_NAME").ToStrList(0);
                case (int)DbItemType.View:
                    return ExecuteDataTable(@"SELECT TABLE_NAME AS Name
FROM INFORMATION_SCHEMA.TABLES
WHERE (TABLE_TYPE = 'VIEW') order by TABLE_NAME").ToStrList(0);

            }
            return ls;

        }

        public DataTable ExecutePageData(string selectFields, string tableName, string where, string orderExpression, int startRowIndexId, int maxNumberRows)
        {
            var sql = string.Format(@"SELECT TOP {0} {1} 
FROM
    (
        SELECT ROW_NUMBER() OVER (ORDER BY {2}) AS RowNumber,{1} FROM {3} 
    )   as A  
WHERE {4} and  RowNumber > {0}*({5}-1)", maxNumberRows, selectFields, orderExpression, tableName, where, startRowIndexId);
          //  FileHelper.WriteFile("d:/log.txt",sql);
            return ExecuteDataTable(sql);
        }
        public IEnumerable<ITbField> ExecuteTbField(string tbName)
        {
            string sql = @"SELECT clmns.column_id, clmns.name AS Name, baset.name AS Type, 
      CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND 
      clmns.max_length <> - 1 THEN clmns.max_length / 2 ELSE clmns.max_length END AS int)
       AS Length, (CASE WHEN clmns.is_identity = 1 THEN 1 ELSE 0 END) AS Pk, 
      (CASE WHEN clmns.is_nullable = 1 THEN 0 ELSE 1 END) AS NotNull
FROM sys.tables AS tbl INNER JOIN
      sys.all_columns AS clmns ON clmns.object_id = tbl.object_id LEFT OUTER JOIN
      sys.types AS baset ON baset.user_type_id = clmns.system_type_id AND 
      baset.user_type_id = baset.system_type_id LEFT OUTER JOIN
      sys.schemas AS sclmns ON 
      sclmns.schema_id = baset.schema_id LEFT OUTER JOIN
      sys.identity_columns AS ic ON ic.object_id = clmns.object_id AND 
      ic.column_id = clmns.column_id
WHERE (tbl.name = N'$table')
UNION
SELECT clmns.column_id, clmns.name AS Name, baset.name AS Type, 
      CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND 
      clmns.max_length <> - 1 THEN clmns.max_length / 2 ELSE clmns.max_length END AS int)
       AS Length, (CASE WHEN clmns.is_identity = 1 THEN 1 ELSE 0 END) AS Pk, 
      (CASE WHEN clmns.is_nullable = 1 THEN 0 ELSE 1 END) AS NotNull
FROM sys.views AS tbl INNER JOIN
      sys.all_columns AS clmns ON clmns.object_id = tbl.object_id LEFT OUTER JOIN
      sys.types AS baset ON baset.user_type_id = clmns.system_type_id AND 
      baset.user_type_id = baset.system_type_id LEFT OUTER JOIN
      sys.schemas AS sclmns ON 
      sclmns.schema_id = baset.schema_id LEFT OUTER JOIN
      sys.identity_columns AS ic ON ic.object_id = clmns.object_id AND 
      ic.column_id = clmns.column_id
WHERE (tbl.name = N'$table')
ORDER BY clmns.column_id".Replace("$table", tbName);
            var fs= ExecuteDataTable(sql).ToList<SqlServerTbField>();
            foreach (var f in fs)
            {
                 ITbField _f=f;
                 yield return _f;
            }
        }
    }

}