using System;
using System.Linq;
using System.Web.UI;
using J.Data.Model;
using J.Common;
using J.Data.Enum;


namespace J.ZenCode
{
    public partial class Moban : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    DropDownListHelper.BindTable(this.mId, db.ExecuteDataTable("select * from mbtype"), "name", "Id");                   
                    Bind();
                }
                catch { Dialog("请正确设置默认模板类别"); }
            }
        }
        protected void Bind()
        {
            var mId=Convert.ToInt64(this.mId.SelectedValue);
            this.ListView1.DataSource = db.ExecuteDataTable("select * from moban where mId="+mId+" order by sort "); 
            this.ListView1.DataBind();
        }
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }

        

    }
}