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
    public partial class DbSql : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                IDbSql _db = DBFactory.CreateDB();               
                var ds = _db.GetDbSql().Select(z => new { val = z.Trim() });
                this.ListView1.DataSource = ds;
                this.ListView1.DataBind();
            }
        }
       
       
    }
}