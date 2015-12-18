<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Tools.aspx.cs" Inherits="J.ZenCode.Tools" %>

<%@ Register Src="../Controls/DataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        
        <a title="增加" href="toolEdit.aspx" target="idialog" class="btn  btn-blue btn-middle">
            增加</a></div>
    <asp:ListView ID="ListView1" runat="server" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder pure layout">
                <tr>
                    <th>
                        编号
                    </th>
                  
                    <th>
                        命名
                    </th>
                    <th>
                        网址
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
                    <input type="text" value='<%#Eval("name")%>' size="18" target="ichange" ref="Id=<%#Eval("Id")%>&tb=tool&field=name" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("url")%>' size="60" target="ichange" ref="Id=<%#Eval("Id")%>&tb=tool&field=url" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("sort")%>' size="2" target="ichange" ref="Id=<%#Eval("Id")%>&tb=tool&field=sort" />
                </td>
                <td>
                    <a title="编辑" href="ToolEdit.aspx?Id=<%#Eval("id")%>" target="idialog" class="btn  btn-blue btn-middle">
                        编辑</a>  <a href="/AjaxTool/DelById.cspx?Id=<%#Eval("Id")%>&tb=tool" target="iajax" class="btn  btn-blue btn-middle">
                                删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <uc1:DataPager ID="Pager" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0"
        TableName="tool" OrderExpression="sort asc" PagedControlID="ListView1" />
</asp:Content>
