using System;
using System.Web.UI;
using J.Common;
using J.Data;
using System.Web;
using J.Data.Model;
using J.Data.Enum;


namespace J.ZenCode
{
    public partial class Dic : BasePage
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownListHelper.BindEnum(sql_eq_typeId, typeof(DicType), "", "请选择");              
            }           
        }

       
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            Pager.Where = Form2Search();
            Pager.UpdatePaging(1);
        }

        protected void _btnExt_Click(object sender, EventArgs e)
        {
            var conn = ConfigHelper.GetConfigConnString("zenCode");
            var sql = "select * from dic where " + Form2Search();
            var ds = new DbSqliteHelper(conn).ExecuteDataSet(sql);
            OutExcel(ds);           
        }

    }
}