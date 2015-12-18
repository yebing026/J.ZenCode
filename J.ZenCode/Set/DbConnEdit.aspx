<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.DbConnEdit"
    CodeBehind="DbConnEdit.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
   
    <table class="form tborder">
        <tr>
            <th>
                数据库类型 ：
            </th>
            <td>
                <asp:DropDownList ID="dbType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                数据库名 ：
            </th>
            <td>
                <asp:TextBox ID="dbName" Width="120" runat="server" Text='' placeholder="数据库名" class=" required" />
            </td>
        </tr>
        <tr>
            <th>
                连接字符串：
            </th>
            <td>
                <asp:TextBox ID="conn" runat="server" TextMode="MultiLine" Rows="4" Columns="60" />
            </td>
        </tr>
        <tr>
            <th>
                说明 ：
            </th>
            <td>
                <asp:TextBox ID="dec" runat="server" TextMode="MultiLine" Rows="3" Columns="60" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="_btnUp" runat="server" Text="更新" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
            </td>
        </tr>
    </table>
</asp:Content>
