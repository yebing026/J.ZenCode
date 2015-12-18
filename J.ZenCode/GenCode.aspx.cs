using System;
using System.Collections.Generic;
using System.Linq;
using J.Common;
using J.Data.Model;
using System.Web.Script.Serialization;

namespace J.ZenCode
{
    public partial class GenCode : BasePage
    {        
        public string json;
        public string mobanContent;
        protected void Page_Load(object sender, EventArgs e)
        {
            var conn = db.ExecuteDataTable("select * from dbconn where status=1 order by Id limit 1").ToList<dbconn>().First();          
            var ds = db.ExecuteDataTable("select * from field where id in (" + Get("ids") + ") order by sort").ToList<field>();
            var mId=GetInt("mId");
            var mb = db.ExecuteDataTable("select * from moban where Id=" + mId).ToList<moban>().First();
            mobanContent=mb.content;
            this.path.Text = string.Format(mb.path, Get("fileName"));            
            var tbName=Get("tb");
            var key = db.ExecuteScalar(string.Format("select fieldName from field where tbName='{0}' and dbName='{1}' and isKey=1 limit 1", tbName, conn.dbName));
           
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["nameSpace"] = Get("nameSpace");
            data["mySet1"] = Get("mySet1");
            data["mySet2"] = Get("mySet2");
            data["tbName"] = tbName;
            data["fileName"] = Get("fileName");
            data["items"] = ds;
            data["key"] = key;
            json = new JavaScriptSerializer().Serialize(data);
        }
    }
}