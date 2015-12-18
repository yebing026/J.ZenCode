<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dic.aspx.cs" Inherits="J.ZenCode.Dic" %>

<%@ Register Src="../Controls/DataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        <label for="sql_eq_typeId" class="line">
            类型:
            <asp:DropDownList ID="sql_eq_typeId" runat="server">
            </asp:DropDownList>
        </label>
        <asp:Button ID="_btnSearch" runat="server" Text="查询" OnClick="_btnSearch_Click" class="btn btn-blue btn-middle"
            ClientIDMode="Static" />
        <asp:Button ID="_btnExt" runat="server" Text="EXCEL" OnClick="_btnExt_Click" class="btn btn-blue btn-middle" />
        <a title="增加" href="DicEdit.aspx" target="idialog" class="btn  btn-blue btn-middle">
            增加</a></div>
    <asp:ListView ID="ListView1" runat="server" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder pure layout">
                <tr>
                    <th>
                        编号
                    </th>
                    <th>
                        类别
                    </th>
                    <th>
                        命名
                    </th>
                    <th>
                        内容或说明
                    </th>
                    <th>
                        排序
                    </th>
                    <th>
                        操作
                    </th>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("Id")%>
                </td>
                <td>
                    <%# EnumVal<J.Data.Enum.DicType>(Eval("typeId"))%>
                </td>
                <td>
                    <input type="text" value='<%#Eval("name")%>' size="18" target="ichange" ref="Id=<%#Eval("Id")%>&tb=dic&field=name" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("inf")%>' size="60" target="ichange" ref="Id=<%#Eval("Id")%>&tb=dic&field=inf" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("sort")%>' size="2" target="ichange" ref="Id=<%#Eval("Id")%>&tb=dic&field=sort" />
                </td>
                <td>
                    <a title="编辑" href="DicEdit.aspx?Id=<%#Eval("id")%>" target="idialog" class="btn  btn-blue btn-middle">
                        编辑</a> <a href="/AjaxTool/CopyById.cspx?Id=<%#Eval("Id")%>&tb=dic" target="iajax" class="btn  btn-blue btn-middle">
                            复制</a> <a href="/AjaxTool/DelById.cspx?Id=<%#Eval("Id")%>&tb=dic" target="iajax" class="btn  btn-blue btn-middle">
                                删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <uc1:DataPager ID="Pager" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0"
        TableName="dic" OrderExpression="sort asc" PagedControlID="ListView1" />
</asp:Content>
