<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.RegTest"
    CodeBehind="RegTest.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <table class="tborder form">
        <tr>
            <th>
                Regex
            </th>
            <td>
                <asp:TextBox ID="txtRegex" runat="server" Text='' placeholder="输入正则" class="required"
                    TextMode="MultiLine" Columns="80" Rows="8" />
            </td>
        </tr>
        <tr>
            <th>
                Source
            </th>
            <td>
                <asp:TextBox ID="txtSource" runat="server" Text='' placeholder="输入" class="required"
                    TextMode="MultiLine" Columns="80" Rows="8" />
            </td>
        </tr>
        <tr>
            <th>
                Result
            </th>
            <td>
                <asp:TextBox ID="txtResult" runat="server" Text='' placeholder="输出" TextMode="MultiLine"
                    Columns="80" Rows="8" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="_btnUp" runat="server" Text="执行" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
            </td>
        </tr>
    </table>
</asp:Content>
