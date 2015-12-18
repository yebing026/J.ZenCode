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


namespace J.ZenCode
{
    public partial class DbCount : BasePage
    {
        IDbSql _db = DBFactory.CreateDB();  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                             
                var ds1 = _db.ExecuteItem(0).Select(z => new { tbName = z });
                this.ListView1.DataSource = ds1;
                this.ListView1.DataBind();
                var ds2 = _db.ExecuteItem(1).Select(z => new { vwName = z });
                this.ListView2.DataSource = ds2;
                this.ListView2.DataBind();
            }
        }
     
        protected string GetCount(object tbName)
        {
            try
            {
                return _db.ExecuteScalar(string.Format("select count(*) from {0}", tbName));
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
       
    }
}