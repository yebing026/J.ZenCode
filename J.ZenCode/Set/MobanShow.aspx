<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.MobanShow"
    CodeBehind="MobanShow.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../public/codeMirror/lib/codemirror.css">
    <script src="../public/codeMirror/lib/codemirror.js"></script>
    <script src="../public/codeMirror/addon/mode/loadmode.js"></script>
    <script src="../public/codeMirror/mode/meta.js" type="text/javascript"></script>    
    <link rel="stylesheet" href="../public/codeMirror/theme/night.css">   
    <script src="../public/codeMirror/addon/edit/closebrackets.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/edit/matchbrackets.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/edit/closetag.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/edit/matchtags.js" type="text/javascript"></script>
    <script src="../public/codeMirror/addon/fold/xml-fold.js" type="text/javascript"></script>
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
            CodeMirror.modeURL = "../public/codeMirror/mode/%N/%N.js";
            var editor = CodeMirror.fromTextArea($("textarea[name$='content']")[0], {
                lineNumbers: true,
                autoCloseTags: true,
                matchTags: {bothTags: true},
                autoCloseBrackets: true,
                matchBrackets: true,                
                showCursorWhenSelecting: true,
                tabSize: 2,               
            });
            change();
            $("#mode").change(function () { change(); });
            function change() {
                var val = $("input[name$='mode']").val(), mode, spec;
                var info = CodeMirror.findModeByName(val);
                if (info) {
                    mode = info.mode;
                    spec = info.mime;
                }
                if (mode) {
                    editor.setOption("mode", spec);
                    CodeMirror.autoLoadMode(editor, mode);
                } else {
                    alert("Could not find a mode corresponding to " + val);
                }
            }
        });
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <asp:TextBox ID="mode" runat="server" Width="0"></asp:TextBox><b><asp:Label ID="name"
        runat="server" Text="Label"></asp:Label></b>
    <asp:TextBox ID="content" runat="server" Text='' TextMode="MultiLine" />
</asp:Content>
