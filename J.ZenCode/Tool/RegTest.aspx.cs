using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Data.Model;
using System.Text.RegularExpressions;

namespace J.ZenCode
{
    public partial class RegTest : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }
        protected void _btnUp_Click(object sender, EventArgs e)
        {
           
            try
            {
                txtResult.Text = "";
                Regex reg = new Regex(txtRegex.Text);
                var matches = reg.Matches(txtSource.Text);
                string str = "";
                foreach (Match match in matches)
                {                   
                    str += match.Value + "\r\n";
                }
                txtResult.Text = str;
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }
        }
    }
}