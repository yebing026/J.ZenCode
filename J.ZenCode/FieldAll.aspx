<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FieldAll.aspx.cs" Inherits="J.ZenCode.FieldAll" %>

<%@ Register Src="Controls/DataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content><asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        <label class="line">
            数据库名:<asp:TextBox ID="sql_lk_dbName" runat="server" placeholder="数据库名"></asp:TextBox></label>
        <label class="line">
            字段名:<asp:TextBox ID="sql_lk_fieldName" runat="server" placeholder="字段名"></asp:TextBox></label>
             <label class="line">
            表名:<asp:TextBox ID="sql_lk_tbName" runat="server" placeholder="表名"></asp:TextBox></label>
        <asp:Button ID="_btnSearch" runat="server" Text="查询" OnClick="_btnSearch_Click" class="btn btn-blue btn-middle" />
        <asp:Button ID="_btnExt" runat="server" Text="导出EXCEL" OnClick="_btnExt_Click" class="btn btn-blue btn-middle" />
        <asp:Button ID="_btnDel" runat="server" Text="删除所选" OnClick="_btnDel_Click" class="btn btn-blue btn-middle" />
    </div>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder layout pure">
                <tr>
                    <th>
                        <input type="checkbox" id="checkAll" name="checkAll" target="item" class="selAll"/>
                    </th>
                    <th>
                        编号
                    </th>
                    <th>
                        主键
                    </th>
                    <th>
                        表名
                    </th>
                    <th>
                        数据库名
                    </th>
                    <th>
                        字段名
                    </th>
                    <th>
                        名称
                    </th>
                    <th>
                        前台属性
                    </th>
                    <th>
                        字段属性
                    </th>
                    <th>
                        C#属性
                    </th>
                    <th>
                        缺省值
                    </th>
                    <th>
                        是否必须
                    </th>
                    <th>
                        字段长度
                    </th>
                    <th>
                        宽
                    </th>
                    <th>
                        高
                    </th>
                    <th>
                        函数
                    </th>
                    <th>
                        是否查询
                    </th>
                    <th>
                        是否排序
                    </th>
                    <th>
                        显示次序
                    </th>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div class="alert error">
                没有数据</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <input type="checkbox" name="item" value='<%#Eval("Id")%>' />
                </td>
                <td>
                    <%# Eval("Id")%>
                </td>
                <td>
                    <a href="#" class="status<%# Eval("isKey")%>">状态</a>
                </td>
                <td>
                    <%# Eval("tbName")%>
                </td>
                <td>
                    <%# Eval("dbName")%>
                </td>
                <td>
                    <%# Eval("fieldName")%>
                </td>
                <td>
                    <%# Eval("showName")%>
                </td>
                <td>
                    <%# Eval("uiType")%>
                </td>
                <td>
                    <%# Eval("fieldType")%>
                </td>
                <td>
                    <%# Eval("propType")%>
                </td>
                <td>
                    <%# Eval("dfltValue")%>
                </td>
                <td>
                    <a href="#" class="status<%# Eval("required")%>">状态</a>
                </td>
                <td>
                    <%# Eval("length")%>
                </td>
                <td>
                    <%# Eval("width")%>
                </td>
                <td>
                    <%# Eval("height")%>
                </td>
                <td>
                    <%# Eval("fun")%>
                </td>
                <td>
                    <a href="#" class="status<%# Eval("isSearch")%>">状态</a>
                </td>
                <td>
                    <a href="#" class="status<%# Eval("isSort")%>">状态</a>
                </td>
                <td>
                    <%# Eval("sort")%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <uc1:DataPager ID="Pager" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0"
        TableName="field" OrderExpression="Id desc" PagedControlID="ListView1" />
</asp:Content>
