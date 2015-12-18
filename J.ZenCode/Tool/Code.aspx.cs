using System;
using System.Linq;
using System.Web.UI;
using J.Data.Model;
using J.Common;
using J.Data.Enum;


namespace J.ZenCode
{
    public partial class Code : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownListHelper.BindTable(this.sql_eq_mode,db.ExecuteDataTable("select * from dic where typeId="+(int)DicType.模板语言), "name", "name"); 
            }
        }
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            Pager.Where = Form2Search();
            Pager.UpdatePaging();
        }


    }
}