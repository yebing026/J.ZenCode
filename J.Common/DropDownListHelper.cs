namespace J.Common
{
    using System;
    using System.Web.UI.WebControls;
    using System.Data;
    using System.Collections.Generic;


    public class DropDownListHelper
    {
        public static void BindList<T>(DropDownList dd, List<T> list, string text, string value, string selectVal = "")
        {           
            dd.DataSource = list;
            dd.DataTextField = text;
            dd.DataValueField = value;
            if (selectVal != "")
            {
                dd.SelectedValue = selectVal;
            }
           
            dd.DataBind();
        }
        public static void BindTable(DropDownList dd, DataTable dt, string text, string value, string selectVal = "", string appendSel = "")
        {
            if (appendSel != "")
            {
                ListItem li = new ListItem(appendSel, "");
                dd.Items.Add(li);
            }            
            foreach (DataRow dr in dt.Rows)
            {              
                ListItem li = new ListItem( dr[text].ToString(), dr[value].ToString());
                dd.Items.Add(li);                
            }
            if (selectVal != "")
            {
                dd.SelectedValue = selectVal;
            }
          
        }
        public static void MakeTree(DropDownList dd, DataTable dt, string strParentColumn, string strRootValue, string strIndexColumn, string strTextColumn, int i = 2)
        {
            //每向下一层，多一个缩入单位  
            i++;
            DataView dvNodeSets = new DataView(dt);
            dvNodeSets.RowFilter = strRootValue == "null" ? strParentColumn + " is null" : strParentColumn + "=" + strRootValue;
            string strPading = ""; //缩入字符   
            //通过i来控制缩入字符的长度，我这里设定的是一个全角的空格   
            for (int j = 0; j < i; j++)
                strPading += "　";//如果要增加缩入的长度，改成两个全角的空格就可以了   
            foreach (DataRowView drv in dvNodeSets)
            {
                TreeNode tnNode = new TreeNode();
                ListItem li = new ListItem(strPading + "├" + drv[strTextColumn].ToString(), drv[strIndexColumn].ToString());
                dd.Items.Add(li);
                MakeTree(dd, dt, strParentColumn, drv[strIndexColumn].ToString(), strIndexColumn, strTextColumn, i);
            }
            //递归结束，要回到上一层，所以缩入量减少一个单位   
            i--;
        }
        /// <summary>
        /// 枚举值绑定到下拉框
        /// </summary>
        /// <param name="dd">下拉框</param>
        /// <param name="type">Enum名</param>
        /// <param name="value">默认值</param>
        /// <param name="appendSel">追加空值名</param>
        public static void BindEnum(DropDownList dd, Type type, string value = null, string appendSel = "")
        {
            dd.Items.Clear();
            if (appendSel != null && appendSel != "")
            {
                ListItem dl = new ListItem(appendSel, "");
                dd.Items.Add(dl);
            }
            if (type != null)
            {
                int i = 0;
                string[] names = Enum.GetNames(type);
                foreach (int v in Enum.GetValues(type))
                {
                    ListItem li = new ListItem(names[i], v.ToString());
                    if (value == v.ToString()) li.Selected = true;
                    dd.Items.Add(li);
                    i++;
                }
            }           
        }
        public static void BindEnumCheckBox(CheckBoxList select, Type type, string value = "", string appendSel = "")
        {
            select.Items.Clear();
            if (appendSel != null && appendSel != "")
            {
                ListItem dl = new ListItem(appendSel, "");
                select.Items.Add(dl);
            }
            if (type != null)
            {
                int i = 0;
                string[] names = Enum.GetNames(type);
                foreach (int v in Enum.GetValues(type))
                {
                    ListItem li = new ListItem(names[i], v.ToString());
                    foreach (var _v in value.Split(','))
                    {
                        if (_v == v.ToString()) li.Selected = true;
                    }
                    select.Items.Add(li);
                    i++;
                }
            }
        }
        
    }
}

