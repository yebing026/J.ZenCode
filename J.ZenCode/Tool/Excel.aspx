<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.Excel"
    CodeBehind="Excel.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../public/codeMirror/lib/codemirror.css">
    <script src="../public/codeMirror/lib/codemirror.js"></script>
    <script src="../public/codeMirror/mode/sql/sql.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/edit/closebrackets.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/edit/matchbrackets.js" type="text/javascript"></script>
    <style type="text/css">
        .CodeMirror
        {
            border-top: 1px solid black;
            border-bottom: 1px solid black;
            height: auto;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            var editor = CodeMirror.fromTextArea($("textarea[name$='sql']")[0], {
                lineNumbers: true,
                mode: 'text/x-sql',
               autoCloseBrackets: true,
                matchBrackets: true,
               
            });
            
        });
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <table class="tlist form">
        <tr>
            <th>
                Sql语句
            </th>
            <td>
                <asp:TextBox ID="sql" runat="server" Text='select * from  where ' placeholder="输入Sql语句"
                    TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="_btnUp" runat="server" Text="导出Excel" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
                <asp:Button ID="_btnShow" runat="server" Text="列表显示" OnClick="_btnShow_Click" class="btn btn-blue btn-middle" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="tborder">
    </asp:GridView>
    </div>
</asp:Content>
