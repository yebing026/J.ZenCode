namespace J.Data
{
    using System;
    using System.Linq;
    using J.Data.Enum;
    using J.Common;
    using J.Data.Model;


    public class DBFactory
    {
        public static IDbSql CreateDB()
        {
            try
            {
                var db = new Db().GetDb();
                var conn = db.ExecuteDataTable("select  * from dbconn where status=1 order by Id limit 1 ").ToList<dbconn>().First();
                    switch (conn.dbType)
                    {
                        case (int)DbTypes.Sqlite:
                            return new DbSqliteHelper(conn.conn);

                        case (int)DbTypes.MySql:
                            return new DbMySqlHelper(conn.conn);

                        case (int)DbTypes.SqlServer:
                            return new DbMsSqlHelper(conn.conn);
                        case (int)DbTypes.OleDb:
                            return new DbOleDbHelper(conn.conn);
                        case (int)DbTypes.Oracle:
                            return new DbOracleHelper(conn.conn);
                    }
                    throw new Exception("不支持此类型的数据库！");
            }
            catch
            {
                throw new Exception("没有设定默认数据库或不支持此类型的数据库！");
            }           
        }

       
    }
}

