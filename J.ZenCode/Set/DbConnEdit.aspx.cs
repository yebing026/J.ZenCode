using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Data.Model;
using J.Data.Enum;
using J.Common;

namespace J.ZenCode
{
    public partial class DbConnEdit : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownListHelper.BindEnum(this.dbType,typeof(DbTypes));
                if (Get("Id") != "")
                {                   
                    Entity2Form<dbconn>();
                }
                else
                {
                    this._btnUp.Text = "增加";
                }
            }
        }
        protected void _btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                UpOrAdd<dbconn>();
                DialogUp( "操作成功");
            }
            catch (Exception ex)
            {
                Dialog( ex.Message.ToString());
            }
        }
    }
}