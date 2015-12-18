using System;
using System.Web.UI;
using J.Common;
using J.Data;
using System.Web;
using System.Linq;
using J.Data.Model;
using J.Data.Enum;
using System.Text.RegularExpressions;
using System.Text;


namespace J.ZenCode
{
    public partial class Field : BasePage
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                Bind();
                BindSelBack();
                BindMoban();
            }           
        }
        protected void BindMoban()
        {
            var mId = db.ExecuteScalar("select Id from mbtype order by status desc limit 1");
            var lv = db.ExecuteDataTable("select * from moban where mId="+mId);
            this.lvMoban.DataSource = lv;
            this.lvMoban.DataBind();
        }
        protected void BindSelBack()
        {
            
            StringBuilder str = new StringBuilder();
            foreach (int typeId in Enum.GetValues(typeof(J.Data.Enum.DicType)))
            {
                str.Append("<div id=\"typeId"+typeId+"\" style=\"display: none;\" class=\"form\">\n");
                var dics =db.ExecuteDataTable("select * from dic where typeId="+typeId).ToList<dic>();
                foreach (var d in dics)
                {
                    str.Append(string.Format("<label for=\"{0}\" class=\"line-radio selBack\"><input id=\"{0}\" type=\"radio\" name=\"{0}\" value=\"{1}\" />{1}</label>\n","dic"+d.Id,d.name));
                }
                str.Append("</div>\n");
            }
            this.DicStr.Text = str.ToString();
            
        }
        protected void  Bind()
        {
            var tb=Get("tb");
            this.tbName.Text = tb;
            this.fileName.Text = tb;
            IDbSql _db = DBFactory.CreateDB();  
            string dbName =db.ExecuteScalar("select dbName from dbconn where status=1 order by Id limit 1");
            var fields = _db.ExecuteTbField(tb);
            var elseFields = db.ExecuteDataTable("select * from field").ToList<field>();
            var num = 1;
            foreach (var f in fields)
            {
                if (elseFields.Count(z => z.fieldName == f.Name && z.tbName == tb && z.dbName == dbName) == 0)
                {
                    var x = new field();
                    x.fieldName = f.Name;
                    x.showName = GetClearName(f.Name);
                    x.isSearch =false;
                    x.isSort =false;
                    x.fun ="";
                    x.sort = num;
                    x.dbName = dbName;
                    x.tbName = tb;
                    x.fieldType = f.Type;
                    x.length = f.Length;
                    x.dfltValue = f.Default;
                    x.required = f.Required;
                    x.isKey = f.IsKey;
                    x.propType = ConvertCType(f.Type, f.Required);
                    x.uiType = GetUIType(x);                    
                    x.css = GetCss(x);
                    var old = elseFields.OrderByDescending(z => z.Id).FirstOrDefault(z => z.fieldName == f.Name);                    
                    if (old != null)
                    {
                        x.showName = old.showName;
                        x.isSearch = old.isSearch;
                        x.isSort = old.isSort;
                        x.fun = old.fun;
                        x.uiType = old.uiType;
                        x.css = old.css;
                    }
                   
                    var sql = string.Format(@"insert into field(
                                    fieldName,
                                    showName,
                                    isSearch,
                                    isSort,
                                    fun,
                                    sort,
                                    dbName,
                                    tbName,
                                    fieldType,
                                    length,
                                    dfltValue,
                                    required,
                                    isKey,
                                    propType,
                                    uiType,
                                    css
                                    ) 
                                    values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                                    x.fieldName,
                                    x.showName,
                                    x.isSearch==true?1:0,
                                    x.isSort == true ? 1 : 0,
                                    x.fun,
                                    x.sort,
                                    x.dbName,
                                    x.tbName,
                                    x.fieldType,
                                    x.length,
                                    x.dfltValue,
                                    x.required,
                                    x.isKey == true ? 1 : 0,
                                    x.propType,
                                    x.uiType,
                                    x.css

                        );
                    db.ExecuteNonQuery(sql);
                    num++;
                }
            }
            var _sql = string.Format("select * from field where tbName='{0}' and dbName='{1}' order by sort ", tb, dbName);
            this.ListView1.DataSource = db.ExecuteDataTable(_sql);
            this.ListView1.DataBind();
        }
        private string GetCss(field x)
        {
            string css = "";
            var proType = x.propType.ToLower();
            if (proType.Contains("time"))
            {
                css = "date";
            }
            if (proType.Contains("int"))
            {
                css = "digits";
            }
            if (proType.InLike("money,float".Split(',')))
            {
                css = "number";
            }
            return css;
        }
        private string GetUIType(field x)
        {
            string type = "text";
            if (x.length > 500 && x.length < 1000)
            {
                type = "textarea";
            }
            if (x.length > 1000)
            {
                type = "editor";
            }
            if (x.propType.Contains("time"))
            {
                type = "datebox";
            }
            if (x.propType=="bit")
            {
                type = "imgBit";
            }
            return type;
        }
        private string ConvertCType(string dbtype,bool required)
        {
            string ret = "string";
            switch (dbtype.ToLower().Split('(')[0])
            {
                case "bit":
                case "boolean":                    
                    ret = "bool";
                    break;
                case "binary":
                case "long raw":
                case "raw":
                case "blob":
                case "bfile":               
                case "image":
                case "varbinary":
                    ret = "byte[]";
                    break;
                case "int":
                    ret = "int";
                    break;
                case "smallint":
                    ret = "short";
                    break;
                case "tinyint":
                    ret = "byte";
                    break;
                case "bigint":
                case "integer"://sqlite
                    ret = "long";
                    break;
                case "money":
                case "smallmoney":
                case "decimal":
                case "numeric":
                    ret = "decimal";
                    break;
                case "float":
                    ret = "double";
                    break;                
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "timestamp":                    
                    ret = "DateTime";
                    break;
                case "uniqueide":
                case "uniqueidentifier":
                    ret = "Guid";
                    break;      
                default:
                    ret = "string";
                    break;
            }
            return ret == "string" ? "string" : (required==false)?ret+"?":ret;
        }
        public string GetClearName(string Column_name, string pre = "")
        {
            Regex r = new Regex("[\u4e00-\u9fa5]", RegexOptions.IgnoreCase);
            Match m = r.Match(Column_name);
            string name = Column_name;
            if (m.Success)
            {
                name = Regex.Replace(Column_name, "^[_|A-Z|a-z]+", "");//取代汉语字段中的英文前缀
            }
            return name;
        }
        protected void _btnDel_Click(object sender, EventArgs e)
        {            
            var Ids = Post("selId");
            if (Ids != "")
            {
                var sql = "delete from field where Id in (" + Ids + ")";
                db.ExecuteNonQuery(sql);               
                Bind();
                Dialog("成功删除");
            }
            else
            {
                Bind();
                Dialog("请先选择");
            }
           
        }      
    }
}