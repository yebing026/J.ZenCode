<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.DbDoc"
    CodeBehind="DbDoc.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <asp:ListView ID="ListView1" runat="server" EnableViewState="false" OnItemDataBound="ListView1_ItemDataBound">
        <LayoutTemplate>
            <table class="tborder">
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <h5>
                表名:
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("tbName")%>'></asp:Label>
            </h5>
            <asp:ListView ID="ListView2" runat="server">
                <LayoutTemplate>
                    <table class=" tborder" width="80%">
                        <tr>
                            <th>
                                序号
                            </th>
                            <th>
                                列名
                            </th>
                            <th>
                                显示名
                            </th>
                            <th>
                                数据类型
                            </th>
                            <th>
                                长度
                            </th>
                            <th>
                                主键
                            </th>
                            <th>
                                必需
                            </th>
                            <th>
                                默认值
                            </th>
                            <th>
                                说明
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.DataItemIndex+1%>
                        </td>
                        <td>
                            <%#Eval("Name")%>
                        </td>
                        <td>
                            <%#Eval("showName")%>
                        </td>
                        <td>
                            <%#Eval("Type")%>
                        </td>
                        <td>
                            <%#Eval("Length")%>
                        </td>
                        <td>
                            <%#Eval("IsKey")%>
                        </td>
                        <td>
                            <%#Eval("Required")%>
                        </td>
                        <td>
                            <%#Eval("Default")%>
                        </td>
                        <td>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </ItemTemplate>
    </asp:ListView>
    <div class="alert">
        页面要复制到word中，可拉窄后处理</div>
</asp:Content>
