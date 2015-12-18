<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.DicEdit"
    CodeBehind="DicEdit.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <table class=" tborder form">
        <tr><th>类型
        </th>
            <td> <asp:DropDownList ID="typeId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr><th>命名
        </th>
            <td> <asp:TextBox ID="name" Width="120" runat="server" Text='' placeholder="命名" class=" required" />
            </td>
        </tr>
        <tr><th>内容或说明
        </th>
            <td>  <asp:TextBox ID="inf" runat="server" TextMode="MultiLine" Rows="3" Columns="60" class=" required" />
            </td>
        </tr>
        <tr><th colspan="2"> <asp:Button ID="_btnUp" runat="server" Text="更新" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
        </th>
          
        </tr>
    </table>
    
</asp:Content>
