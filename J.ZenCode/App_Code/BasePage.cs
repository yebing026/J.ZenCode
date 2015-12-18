using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using J.Data;
using System.Collections.Specialized;
using System.ComponentModel;
using J.Common;
using System.Data;
using System.Text.RegularExpressions;
namespace J.ZenCode
{

    public class BasePage : Page
    {

        public DbSqliteHelper db = new Db().GetDb();
       
        public void Dialog(string msg)
        {
            DialogDeal(0, msg);
           
        }
       
        public void DialogReload(string msg)
        {
            DialogDeal(1, msg);
        }
       
        public void DialogUp(string msg)
        {
            DialogDeal(2, msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="refresh">2则父页面刷新且对话框关闭，1对话框关闭且页面刷新，0只有对话框</param>
        private void DialogDeal(int refresh, string msg)
        {
            msg =  StringHelper.ReplaceHtml(HttpUtility.HtmlEncode(msg));
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ShowMessage", "backDialog(\"" + msg + "\"," + refresh + ");", true);
        }

        public string EnumVal<T>(object val)
        {
            if (Convert.IsDBNull(val))
            {
                return "待定";
            }
            else
            {
                return Enum.GetName(typeof(T), Convert.ToByte(val));
            }
        }

        public void zLog(string str)
        {
            FileHelper.ReadAndAppendFile("d:/zenLog.txt", str + "\n");
        }
        public void OutExcel(DataSet ds)
        {
            var path = HttpRuntime.AppDomainAppPath + "/out/" + DateTime.Now.ToFileTime().ToString() + ".xls";
            ExcelHelper.DataSetToExcel(ds, path);
        }
        #region form
        public long GetInt(string name)
        {
            return Convert.ToInt64(HttpContext.Current.Request.QueryString[name].Trim());
        }
        public string Get(string name)
        {
            string str = HttpContext.Current.Request.QueryString[name];
            return ((str == null) ? string.Empty : str.Trim());
        }
        public string Post(string name)
        {
            string str = HttpContext.Current.Request.Form[name];
            return ((str == null) ? string.Empty : str.Trim());
        }

        public void UpOrAdd<T>() where T : class
        {
            if (Get("Id") != "")
            {
                var id = GetInt("Id");
                Form2Update<T>(id);
            }
            else
            {
                Form2Add<T>();
            }
        }
        /// <summary>
        /// 将表单控件赋值,从主页开始找
        /// </summary>
        /// <typeparam name="T"></typeparam>       
        /// <param name="holder">ContentPlaceHolderID</param>
        public void Entity2Form<T>() where T : class,new()
        {
            string holder = "ybContent";
            var id = GetInt("Id");
            var tbName = typeof(T).Name;
            var m = db.ExecuteDataTable("select * from "+tbName+" where Id="+id).ToList<T>().First();
            PropertyInfo[] pi = m.GetType().GetProperties();
            foreach (PropertyInfo p in pi)
            {
                Control ctrl = this.Page.Master.FindControl(holder).FindControl(p.Name);
                if (ctrl != null)
                {
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = string.Format("{0}", p.GetValue(m, null));
                    }
                    else if (ctrl is DropDownList)
                    {
                        ((DropDownList)ctrl).SelectedValue = string.Format("{0}", p.GetValue(m, null));
                    }
                    else if (ctrl is RadioButtonList)
                    {
                        ((RadioButtonList)ctrl).SelectedValue = string.Format("{0}", p.GetValue(m, null));
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        ((CheckBoxList)ctrl).SelectedValue = string.Format("{0}", p.GetValue(m, null));
                    }
                    else if (ctrl is Label)
                    {
                        ((Label)ctrl).Text = string.Format("{0}", p.GetValue(m, null));

                    }
                    else { }
                };

            }
        }
        /// <summary>
        /// 表单控件映射实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="hao">控件name中表属性位置，有母版页一般为2</param>
        public void Form2Add<T>()
        {
            int hao = 2;
            PropertyInfo[] attrs = typeof(T).GetProperties();
            var rs = this.Request.Form;
            var fields = "";
            var vals = "";
            foreach (string key in rs.AllKeys)
            {
                if (!key.Contains("$"))
                {
                    continue;
                }
                var _key = key.Split('$')[hao];
                foreach (PropertyInfo p in attrs)
                {
                    if (string.Compare(p.Name, _key, true) == 0)
                    {
                        fields += p.Name + ",";
                        vals += "'" + StringHelper.SqlJFilter(rs[key].Trim()) + "',";                       
                    }
                }
            }
            var sql = string.Format("insert into {0}({1}) values({2})", typeof(T).Name, fields.TrimEnd(','),vals.TrimEnd(','));
            db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 表单控件映射实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="hao">控件name中表属性位置，有母版页一般为2</param>
        public void Form2Update<T>(long id)
        {
            int hao = 2;
            PropertyInfo[] attrs =typeof(T).GetProperties();
            var rs = this.Request.Form;
            var fields = "set ";
            foreach (string key in rs.AllKeys)
            {
                if (!key.Contains("$"))
                {
                    continue;
                }
                var _key = key.Split('$')[hao];
                foreach (PropertyInfo p in attrs)
                {
                    if (string.Compare(p.Name, _key, true) == 0)
                    {
                        var val = rs[key].Trim();
                        fields += string.Format(" {0}='{1}',", p.Name, StringHelper.SqlJFilter(val));                      
                    }
                }
            }
            var sql = string.Format("update {0} {1} where Id={2}", typeof(T).Name, fields.TrimEnd(','), id);
            zLog(sql);
            db.ExecuteNonQuery(sql);
        }
    
        protected string GetDateSql(string name, string date1, string date2)
        {
            if (name == "")
            {
                return "1=1";
            }
            else if (!string.IsNullOrEmpty(date1) && string.IsNullOrEmpty(date2))
            {
                return string.Format("{0}>='{1}'", name, date1);
            }
            else if (string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                return string.Format("{0}<'{1}'", name, Convert.ToDateTime(date2).AddHours(23).AddMinutes(59));
            }
            else if (!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                return string.Format("{0} between '{1}' and '{2}'", name, date1, Convert.ToDateTime(date2).AddHours(23).AddMinutes(59));
            }
            else
            {
                return name + " is null";
            }

        }
        /// <summary>
        /// 拼接查询Sql
        /// </summary>
        /// <param name="elseSql"></param>
        /// <param name="hao">控件name中表属性位置，有母版页一般为2</param>
        /// <returns></returns>
        public string Form2Search()
        {
            return Form2Search("1=1");
        }
        public string Form2Search(string elseSql)
        {
            int hao = 2;
            var sqls = new List<string>();
            sqls.Add(elseSql);
            var rs = this.Request.Form;

            foreach (string key in rs.AllKeys)
            {
                if (!key.Contains("$sql_"))
                {
                    continue;
                }
                var _key = key.Split('$')[hao].Split('_');
                sqls.Add(GetKeySql(_key[1], _key[2], rs[key].Trim()));

            }
            return string.Join(" and ", sqls.Distinct().ToArray());
        }
        private string GetKeySql(string mode, string key, string val)
        {
            switch (mode)
            {
                case "eq": return val == "" ? "1=1" : string.Format("{0}='{1}'", key, val);
                case "gt": return val == "" ? "1=1" : string.Format("{0}>{1}", key, val);
                case "ge": return val == "" ? "1=1" : string.Format("{0}>={1}", key, val);
                case "nu": return " IS NULL ";
                case "nn": return " IS NOT NULL ";
                case "lt": return val == "" ? "1=1" : string.Format("{0}<{1}", key, val);
                case "le": return val == "" ? "1=1" : string.Format("{0}<={1}", key, val);
                case "lk": return val == "" ? "1=1" : string.Format("{0} like '%{1}%'", key, val);
                case "rlk": return val == "" ? "1=1" : string.Format("{0} like '%{1}'", key, val);
                case "llk": return val == "" ? "1=1" : string.Format("{0} like '{1}%'", key, val);
                case "ne": return val == "" ? "1=1" : string.Format("{0}<>{1}", key, val);
                case "in": return val == "" ? "1=1" : string.Format("{0} IN ({1})", key, val);
                case "ni": return val == "" ? "1=1" : string.Format("{0} not in ({1})", key, val);
                case "sql": return val == "" ? "1=1" : val;
                default: return "1=1";
            }

        }
        #endregion
    }
}
