using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using J.Common;
using System.Web.Script.Serialization;

namespace J.ZenCode.Tool
{
    public partial class KuangCode : BasePage
    {
        public string moban;
        public string json;
        protected void Page_Load(object sender, EventArgs e)
        {
            moban = HttpUtility.UrlDecode(HttpContext.Current.Session["mobanContent"].ToString());
            var kuang = HttpUtility.UrlDecode(HttpContext.Current.Session["kuang"].ToString());

            List<string[]> ks = new List<string[]>(); 
            var lines = Regex.Split(kuang,"\r\n|\n");
            foreach (var line in lines)
            {
                ks.Add(Regex.Split(line, "[,|\t]"));
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["mySet"] = Get("mySet");
            data["items"] = ks;
            json =  new JavaScriptSerializer().Serialize(data);
        }
    }
}