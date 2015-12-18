using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Common;
using J.Data.Model;
using J.Data.Enum;

namespace J.ZenCode
{
    public partial class Kuang : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var mbtype = db.ExecuteDataTable("select * from mbtype order by status desc").ToList<mbtype>().FirstOrDefault();
                if (mbtype != null)
                {
                    var ms = db.ExecuteDataTable("select * from moban where mId="+mbtype.Id).ToList<moban>();
                   
                    DropDownListHelper.BindList<moban>(mobanId,ms,"name","Id");
                }

            }
        }
        protected void _btnUp_Click(object sender, EventArgs e)
        {
            
        }

        protected void mobanId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mId = this.mobanId.SelectedValue;
            mobanContent.Text = db.ExecuteScalar("select content from moban where Id=" + mId);
        }
    }
}