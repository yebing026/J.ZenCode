using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Data.Model;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using J.Common;


namespace J.ZenCode
{
    public partial class DbDoc : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var dbName = db.ExecuteScalar("select  dbName from dbconn order by status desc limit 1");
                var fs = db.ExecuteDataTable("select * from field where dbName='" + dbName + "'").ToList<field>();
                J.Common.CacheHelper.SetCache("dbFields", fs);
                IDbSql _db = DBFactory.CreateDB();
                var type = int.Parse(Get("type"));
                var ds = _db.ExecuteItem(type).Select(z => new { tbName = z });
                this.ListView1.DataSource = ds;
                this.ListView1.DataBind();
            }
        }
        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lb = e.Item.FindControl("Label1") as Label;
                ListView lv1 = e.Item.FindControl("ListView2") as ListView;
                IDbSql _db = DBFactory.CreateDB();
                var tb = lb.Text.Trim();
                var ds = _db.ExecuteTbField(tb).ToList().Select(z => new { z.Name, z.Default, z.IsKey, z.Length, z.Required, z.Type, showName = GetShowName(z, tb) });
                lv1.DataSource = ds;
                lv1.DataBind();
            }
        }
        private string GetShowName(ITbField f, string tbName)
        {
            var fs=J.Common.CacheHelper.GetCache("dbFields") as List<field> ;
            var field = fs.SingleOrDefault(z => z.fieldName == f.Name && z.tbName == tbName);
            if (field != null)
            {
                return field.showName;
            }
            else
            {
                return f.Name;
            }

        }
       
    }
}