using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data.Model;
using J.Data.Enum;


namespace J.ZenCode
{
    public partial class Default : BasePage
    {
        public string mobanType = "";
        public string dbName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mobanType = db.ExecuteScalar("select name from mbtype order by status desc limit 1");
                dbName = db.ExecuteScalar("select dbName from dbconn order by status desc limit 1");
            }
        }
    }
}