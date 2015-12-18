<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.DbCount"
    CodeBehind="DbCount.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
  
            <asp:ListView ID="ListView1" runat="server">
                <LayoutTemplate>
                    <table class=" tborder" width="80%">
                        <tr>
                            <th>
                                序号
                            </th>
                            <th>
                                表名
                            </th>
                            <th>
                                数据量
                            </th>
                            
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td  align="middle">
                            <%# Container.DataItemIndex+1%>
                        </td>
                        <td align="middle">
                            <%# Eval("tbName")%>
                        </td>
                        <td align="middle">
                            <%#GetCount(Eval("tbName"))%>
                        </td>
                       
                    </tr>
                </ItemTemplate>
            </asp:ListView>
         <asp:ListView ID="ListView2" runat="server">
                <LayoutTemplate>
                    <table class=" tborder" width="80%">
                        <tr>
                            <th>
                                序号
                            </th>
                            <th>
                                视图名
                            </th>
                            <th>
                                数据量
                            </th>
                            
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td  align="middle">
                            <%# Container.DataItemIndex+1%>
                        </td>
                        <td  align="middle">
                            <%# Eval("vwName")%>
                        </td>
                        <td  align="middle">
                            <%#GetCount(Eval("vwName"))%>
                        </td>
                       
                    </tr>
                </ItemTemplate>
            </asp:ListView>
</asp:Content>
