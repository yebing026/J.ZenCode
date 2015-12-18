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
    public partial class Sql : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }
        protected void _btnUp_Click(object sender, EventArgs e)
        {
            var sql = this.sql.Text;
            try
            {

                IDbSql _db = DBFactory.CreateDB();
                var lines = Regex.Split(this.kuang.Text, "\r\n|\n");
                var num = 0;
                if (lines.Count() > 0 && !string.IsNullOrEmpty(this.kuang.Text))
                {
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        var _sql = sql;
                        string[] ls = Regex.Split(line, @"[,|\t]");
                        _sql = string.Format(_sql, ls);
                        //zLog(_sql);
                        num += _db.ExecuteNonQuery(_sql);
                    }
                }
                else
                {
                    //zLog(sql);
                    num += _db.ExecuteNonQuery(sql);
                }

                Dialog("成功执行Sql,影响行数" + num);
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }
        }
    }
}