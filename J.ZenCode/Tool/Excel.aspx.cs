using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Data.Model;
using System.Data;
using System.Web;
using J.Common;

namespace J.ZenCode
{
    public partial class Excel : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        protected void _btnUp_Click(object sender, EventArgs e)
        {
            var sql = this.sql.Text;
            try
            {
                IDbSql _db = DBFactory.CreateDB();
                var ds = _db.ExecuteDataSet(sql);
                OutExcel(ds);               
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }
        }
        protected void _btnShow_Click(object sender, EventArgs e)
        {
            var sql = this.sql.Text;
            try
            {
                IDbSql _db = DBFactory.CreateDB();
                var ds = _db.ExecuteDataSet(sql);
                this.GridView1.DataSource = ds;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }
        }
    }
}