using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using J.ZenCode;

public partial class Controls_DataPager : System.Web.UI.UserControl
{

    //为减少手写代码，本控件整合了本框架的其它代码,无通用性，如要独立使用，请自行修改

    #region "Properties"
    public string SelectFields
    {
        get
        {
            object o = ViewState["selectFields"];
            if (o == null)
                return "*";
            return o.ToString();
        }
        set { ViewState["selectFields"] = value; }
    }
    public string TableName
    {
        get
        {
            return ViewState["tableName"].ToString();
        }
        set { ViewState["tableName"] = value; }
    }
    public string Where
    {
        get
        {
            object o = ViewState["where"];
            if (o == null)
                return "1=1";
            return o.ToString();
        }
        set { ViewState["where"] = value; }
    }
    public string OrderExpression
    {
        get
        {
            return ViewState["orderExpression"].ToString();
        }
        set { ViewState["orderExpression"] = value; }
    }
    public string PagedControlID
    {
        get
        {
            return ViewState["PagedControlID"].ToString();
        }
        set { ViewState["PagedControlID"] = value; }
    }


    [Category("Behavior")]
    [Description("Total number of records")]
    [DefaultValue(0)]
    public int TotalRecords
    {
        get
        {
            object o = ViewState["TotalRecords"];
            if (o == null)
                return 0;
            return (int)o;
        }
        set { ViewState["TotalRecords"] = value; }
    }
    [Category("Behavior")]
    [Description("Current page index")]
    [DefaultValue(1)]
    public int PageIndex
    {
        get
        {
            object o = ViewState["PageIndex"];
            if (o == null)
                return 1;
            return (int)o;
        }
        set { ViewState["PageIndex"] = value; }
    }
    [Category("Behavior")]
    [Description("Total number of records to each page")]
    [DefaultValue(20)]
    public int RecordsPerPage
    {
        get
        {
            object o = ViewState["RecordsPerPage"];
            if (o == null)
                return 20;
            return (int)o;
        }
        set { ViewState["RecordsPerPage"] = value; }
    }
    private decimal TotalPages
    {
        get
        {
            object o = ViewState["TotalPages"];
            if (o == null)
                return 0;
            return (decimal)o;
        }
        set { ViewState["TotalPages"] = value; }
    }
    #endregion

    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int[] Paging = { 10, 20, 30, 50, 100, 200, 300, 500, 1000 };
            for (int p = 0; p < Paging.Length; p++)
            {
                ddlRecords.Items.Add(Paging[p].ToString());
                ddlRecords.SelectedValue = this.RecordsPerPage.ToString();
            }
            UpdatePaging(this.PageIndex);
        }
    }
    #endregion

    #region "Control Events"
    protected void btnMove_Click(object sender, CommandEventArgs e)
    {
        switch (Convert.ToString(e.CommandArgument))
        {
            case "First":
                this.PageIndex = 1;
                break;
            case "Previous":
                this.PageIndex--;
                break;
            case "Next":
                this.PageIndex++;
                break;
            case "Last":
                this.PageIndex = (int)this.TotalPages;
                break;
        }
        UpdatePaging(this.PageIndex, this.TotalRecords);
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        int newPage = Convert.ToInt32(txtPage.Text);
        if (newPage > this.TotalPages)
            this.PageIndex = (int)this.TotalPages;
        else
            this.PageIndex = newPage;
        UpdatePaging(this.PageIndex, this.TotalRecords);
    }
    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.RecordsPerPage = Convert.ToInt32(ddlRecords.Text);
        this.PageIndex = 1;
        UpdatePaging(this.PageIndex, this.TotalRecords);
    }
    #endregion

    #region "Web Methods"
    public void UpdatePaging(int pageIndex=1, int? recordCount = null)
    {
        if (recordCount == null)
        {
            try
            {
                recordCount = PageTool.GetRecordCount(TableName, Where);
                this.TotalRecords = int.Parse(recordCount.ToString());
            }
            catch
            {
                lblTotalRecord.Text = "请检查查询条件是否有误";
                lblTotalRecord.ForeColor = Color.Red;
                return;
            }
        }

        if (recordCount != 0)
        {
            var pageSize = this.RecordsPerPage;
            lblTotalRecord.ForeColor = Color.Black;
            ddlRecords.Enabled = true;
            int currentEndRow = (pageIndex * pageSize);
            if (currentEndRow > recordCount) currentEndRow = int.Parse(recordCount.ToString());

            if (currentEndRow < pageSize) pageSize = currentEndRow;
            int currentStartRow = (currentEndRow - pageSize) + 1;

            this.TotalPages = Math.Ceiling((decimal)recordCount / pageSize);
            txtPage.Text = string.Format("{0:00}", this.PageIndex);
            lblTotalRecord.Text = string.Format("{0:00}-{1:00} of {2:00} 记录数(s)", currentStartRow, currentEndRow, recordCount);
            lblTotalPage.Text = string.Format(" of {0:00} 页(s)", this.TotalPages);

            btnMoveFirst.Enabled = (pageIndex == 1) ? false : true;
            btnMovePrevious.Enabled = (pageIndex > 1) ? true : false;
            btnMoveNext.Enabled = (pageIndex * pageSize < recordCount) ? true : false;
            btnMoveLast.Enabled = (pageIndex * pageSize >= recordCount) ? false : true;

            Control ctrl = this.Page.Master.FindControl("ybContent").FindControl(PagedControlID);//有主页得手动指定ybContent
            if (ctrl != null)
            {
                var dt = PageTool.GetPageData(SelectFields, TableName, Where, OrderExpression, pageIndex, pageSize);
                if (ctrl is ListView)
                {
                    ListView lv = (ListView)ctrl;
                    lv.DataSource = dt;
                    lv.DataBind();
                }
                else if (ctrl is GridView)
                {
                    GridView gv = (GridView)ctrl;
                    gv.DataSource = dt;
                    gv.DataBind();
                }
            }
        }
        else
        {
            lblTotalPage.Text = "";
            lblTotalRecord.Text = "无记录!";
            lblTotalRecord.ForeColor = Color.Red;
            btnMoveFirst.Enabled = false;
            btnMovePrevious.Enabled = false;
            btnMoveNext.Enabled = false;
            btnMoveLast.Enabled = false;
            txtPage.Enabled = false;
            ddlRecords.Enabled = false;
        }
    }
    #endregion
}