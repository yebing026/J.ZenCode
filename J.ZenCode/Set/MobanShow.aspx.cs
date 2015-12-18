using System;
using System.Linq;
using System.Web.UI;
using J.Data.Model;
using J.Common;
using J.Data.Enum;


namespace J.ZenCode
{
    public partial class MobanShow : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Entity2Form<moban>();
               
            }
        }
       

    }
}