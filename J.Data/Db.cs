using System.Data.Entity;
using J.Data.Model;

namespace J.Data
{
    public class Db 
    {
        public DbSqliteHelper GetDb()           
        {
            var conn = J.Common.ConfigHelper.GetConfigConnString("zenCode");
            var db = new J.Data.DbSqliteHelper(conn);
            return db;
        }
      
      
    }
}
