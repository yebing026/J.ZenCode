<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DbConn.aspx.cs" Inherits="J.ZenCode.DbConn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">

    <asp:ListView ID="ListView1" runat="server" DataKeyNames="id" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder">
                <tr>
                    <th>
                        编号
                    </th>
                    <th>
                        数据库类型
                    </th>
                    <th>
                        数据库名
                    </th>
                    <th>
                        连接
                    </th>
                    <th>
                        说明
                    </th>
                    <th>
                        启用
                    </th>
                    <th>
                        操作
                    </th>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div class="alert">
                没有数据</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("id")%>
                </td>
                <td>
                    <%# EnumVal<J.Data.Enum.DbTypes>(Eval("dbType"))%>
                </td>
                <td>
                    <input type="text" value='<%#Eval("dbName")%>' size="18" target="ichange" ref="Id=<%#Eval("Id")%>&tb=dbconn&field=dbName" />
                </td>
                <td>
                    <%# Eval("conn")%>
                </td>
                <td>
                    <%# Eval("dec")%>
                </td>
                <td>
                    <a href="#" class="status<%# Eval("status")%>">状态</a>
                </td>
                <td>
                    <a title="编辑" href="DbConnEdit.aspx?Id=<%#Eval("id")%>" target="idialog" class="btn  btn-blue btn-middle">
                        编辑</a> <a href="/AjaxTool/CopyById.cspx?Id=<%#Eval("Id")%>&tb=dbconn" target="iajax" class="btn  btn-blue btn-middle">
                            复制</a><a href="/AjaxTool/DelById.cspx?Id=<%#Eval("Id")%>&tb=dbconn" target="iajax"
                                class="btn  btn-blue btn-middle"> 删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <a title="增加" href="DbConnEdit.aspx" target="idialog" class="btn  btn-blue btn-middle">
        增加</a>
</asp:Content>
