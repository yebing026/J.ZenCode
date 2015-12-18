<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.Sql"
    CodeBehind="Sql.aspx.cs" MasterPageFile="~/Site.Master" %>

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
    
    <table class="tborder form">
        <tr><th>Sql手动复制
               
                </th>
            <td><pre>update  set =      where  //   delete   from    where  //   insert   into ()    values()  // truncate table </pre>
            </td>
        </tr>
         <tr><th>Sql语句
               
               
                </th>
            <td> <asp:TextBox ID="sql" runat="server" Text='update  set =   where ' placeholder="输入Sql语句" TextMode="MultiLine" />
            </td>
        </tr>
         <tr><th> 替换队列输入
                
               
                </th>
            <td><asp:TextBox ID="kuang" runat="server" Text='' placeholder="输入" TextMode="MultiLine"
                    Columns="80" Rows="8" />
                    <div class="alert">
        支持excel拷入的多行多列，及半角逗号分隔的多行多列。可替换sql语句中的{0}{1}....{n}</div>
            </td>
        </tr>
         <tr>
            <td colspan="2"><asp:Button ID="_btnUp" runat="server" Text="执行" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" />
            </td>
        </tr>
    </table>
   
</asp:Content>
