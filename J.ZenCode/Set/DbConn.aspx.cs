using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data.Model;
using J.Data;


namespace J.ZenCode
{
    public partial class DbConn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var db = new Db().GetDb();                
                this.ListView1.DataSource = db.ExecuteDataTable("select * from dbconn");
                this.ListView1.DataBind();
            }
        }      

    }
}