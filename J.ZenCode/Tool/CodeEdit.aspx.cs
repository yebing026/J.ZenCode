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
    public partial class CodeEdit : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                var dics = db.ExecuteDataTable("select * from dic where  typeId=" + (int)DicType.模板语言);
                DropDownListHelper.BindTable(this.mode, dics, "name", "name");
                if (Get("Id") != "")
                {
                    Entity2Form<code>();
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
                UpOrAdd<code>();
                Dialog("操作成功");
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }
        }
    }
}