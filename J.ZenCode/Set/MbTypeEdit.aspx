<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.MbTypeEdit"
    CodeBehind="MbTypeEdit.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <table class=" tborder form">
       
        <tr><th>命名
        </th>
            <td> <asp:TextBox ID="name" Width="120" runat="server" Text='' placeholder="命名" class=" required" />
            </td>
        </tr>
        <tr><th>说明
        </th>
            <td>  <asp:TextBox ID="inf" runat="server" TextMode="MultiLine" Rows="3" Columns="60" class=" required" />
            </td>
        </tr>
        <tr><th colspan="2"> <asp:Button ID="_btnUp" runat="server" Text="更新" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
        </th>
          
        </tr>
    </table>
    
</asp:Content>
