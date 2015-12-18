using System;
using System.Linq;
using System.Web.UI;
using J.Common;
using J.Data;
using J.Data.Model;

namespace J.ZenCode
{
    public partial class TbData : BasePage
    {

        IDbSql _db;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var type = Get("type") == "" ? "0" : Get("type");
                BindTbs(type);
                BindSort();
                Bind();
            }

        }

        private void Bind()
        {
            Pager.TableName = this.tbs.SelectedValue;
            Pager.OrderExpression = this.sort.SelectedValue + " " + fXang.SelectedValue;
            Pager.Where = this.where.Text.Trim() == "" ? "0=0" : this.where.Text.Trim();
            Pager.SelectFields = this.showFields.Text;
            this.sql.Text = string.Format("select {0} from {1} where {2} order by {3}", this.showFields.Text, this.tbs.SelectedValue, Pager.Where, Pager.OrderExpression);
        }
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
            Pager.UpdatePaging();
        }


        protected void BindTbs(string type)
        {
            _db = DBFactory.CreateDB();
            var dtNames = _db.ExecuteItem(Convert.ToInt16(type));
            if (dtNames.Count() == 0)
            {
                Response.Write("无数据，不能处理");
                Response.End();
            }
            DropDownListHelper.BindList<string>(tbs, dtNames, "", "", "");
        }

        protected void tbs_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindSort();
            Bind();
            Pager.UpdatePaging();
        }
        protected void BindSort()
        {
            var tbName = tbs.SelectedValue;
            if (tbName != "")
            {
                _btnSearch.Visible = true;
                _db = DBFactory.CreateDB();
                string[] names = new string[] { "numeric", "decimal", "int", "float", "datetime","varchar" };
                var fs = _db.ExecuteTbField(tbName).ToList();
                this.ListView1.DataSource = fs;
                this.ListView1.DataBind();
                this.showFields.Text = "*";
                this.where.Text = "";
                DropDownListHelper.BindList<ITbField>(sort, fs.Where(z => (z.Type.ToLower().InLike(names))).ToList(), "name", "name");//使用了LambdaHelper
            }
        }
        protected void _btnExt_Click(object sender, EventArgs e)
        {
            IDbSql _db = DBFactory.CreateDB();
            var sql = string.Format("select {0} from {1} where {2} order by {3} {4}",
                this.showFields.Text,
                this.tbs.SelectedValue,
                this.where.Text,
                this.sort.SelectedValue,
                fXang.SelectedValue);
            try
            {
                var ds = _db.ExecuteDataSet(sql);
                OutExcel(ds);
            }
            catch (Exception ex)
            {
                Dialog(ex.Message.ToString());
            }

        }
    }
}