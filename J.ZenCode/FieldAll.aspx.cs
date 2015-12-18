using System;
using System.Web.UI;
using J.Common;
using J.Data;
using System.Web;

namespace J.ZenCode
{
    public partial class FieldAll : BasePage
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }           
        }
        
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            Pager.Where = Form2Search();
            Pager.UpdatePaging(1);
        }

        protected void _btnExt_Click(object sender, EventArgs e)
        {           
            var conn =ConfigHelper.GetConfigConnString("zenCode");
            var sql = "select * from field where " + Form2Search();
            var ds = new DbSqliteHelper(conn).ExecuteDataSet(sql);
            OutExcel(ds);
                     
        }
        protected void _btnDel_Click(object sender, EventArgs e)
        {
            var conn = ConfigHelper.GetConfigConnString("zenCode");
            var Ids = Post("item");
            if (Ids != "")
            {
                var sql = "delete from field where Id in (" + Ids + ")";
                var dt = new DbSqliteHelper(conn).ExecuteNonQuery(sql);
                DialogReload( "成功删除");
            }
            else
            {
                Dialog("请先选择");
            }
           
        }      
    }
}