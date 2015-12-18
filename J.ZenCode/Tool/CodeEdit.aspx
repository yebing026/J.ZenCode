<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.CodeEdit"
    CodeBehind="CodeEdit.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../public/codeMirror/lib/codemirror.css">
    <script src="../public/codeMirror/lib/codemirror.js"></script>
    <script src="../public/codeMirror/addon/mode/loadmode.js"></script>
    <script src="../public/codeMirror/mode/meta.js"></script>
    <script src="../public/codeMirror/mode/meta.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../public/codeMirror/addon/display/fullscreen.css">
    <link rel="stylesheet" href="../public/codeMirror/theme/night.css">
    <script src="../public/codeMirror/addon/display/fullscreen.js"></script>
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
        }
    </style>
    <script type="text/javascript">
        $(function () {
            CodeMirror.modeURL = "../public/codeMirror/mode/%N/%N.js";
            var editor = CodeMirror.fromTextArea($("textarea[name$='content']")[0], {
                lineNumbers: true,
                theme: "night",
                lineNumbers: true,
                autoCloseTags: true,
                autoCloseBrackets: true,
                matchBrackets: true,
                matchTags: { bothTags: true },
                showCursorWhenSelecting: true,
                tabSize: 2,
                extraKeys: {
                    "F11": function (cm) {
                        cm.setOption("fullScreen", !cm.getOption("fullScreen"));
                    },
                    "Esc": function (cm) {
                        if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
                    }
                }
            });
            change();
            $("#mode").change(function () { change(); });
            function change() {
                var val = $("select[name$='mode']").val(), mode, spec;
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
    <div class=" form form-aligned">
        <fieldset>
            <div class="pure-control-group">
                <label for="name">
                    名称</label>
                <asp:TextBox ID="name" Width="500" runat="server" Text='' placeholder="名称" class="required" />
            </div>
            <div class="pure-control-group">
                <label for="mode">
                    模板语言</label>
                <asp:DropDownList ID="mode" runat="server">
                </asp:DropDownList>
            </div>
            <div class="pure-control-group">
                <label for="content">
                    模板内容(F11全屏)</label>
                <asp:TextBox ID="content" runat="server" Text='' placeholder="内容" 
                    TextMode="MultiLine" />
            </div>
            <div class="pure-controls">
                <asp:Button ID="_btnUp" runat="server" Text="更新" OnClick="_btnUp_Click" class="btn btn-blue btn-middle" /></div>
        </fieldset>
    </div>
</asp:Content>
