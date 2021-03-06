﻿using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using J.Data;
using J.Data.Model;
using J.Common;
using J.Data.Enum;

namespace J.ZenCode
{
    public partial class DicEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownListHelper.BindEnum(typeId, typeof(DicType));
                if (Get("Id") != "")
                {
                    Entity2Form<dic>();
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
                UpOrAdd<dic>();
                DialogUp("操作成功");
            }
            catch (Exception ex)
            {
                Dialog( ex.Message.ToString());
            }
        }
    }
}