<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.Kuang"
    CodeBehind="Kuang.aspx.cs" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <table class="tborder form">
        <tr>
            <th>
                模板
            </th>
            <td>
                <asp:DropDownList ID="mobanId" runat="server" OnSelectedIndexChanged="mobanId_SelectedIndexChanged"
                    AutoPostBack="true" AppendDataBoundItems="true">
                    <asp:ListItem Text="请选择模板" Value="" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                模板内容
            </th>
            <td>
                <asp:TextBox ID="mobanContent" runat="server" Text='' TextMode="MultiLine" Columns="80"
                    Rows="8" />
            </td>
        </tr>
        <tr>
            <th>
                自定义属性
            </th>
            <td>
                <asp:TextBox ID="mySet" runat="server" Text='' placeholder="输入" class="" />
            </td>
        </tr>
        <tr>
            <th>
                输入
            </th>
            <td>
                <asp:TextBox ID="kuang" runat="server" Text='' placeholder="输入" class="required"
                    TextMode="MultiLine" Columns="80" Rows="8" />
                    <div class="alert">
        支持excel拷入的多行多列，及半角逗号分隔的多行多列。</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="button" value="代码生成" onclick="return genfile();" class="btn btn-red" />
            </td>
           
        </tr>
        
    </table>
   
    <script type="text/javascript">
        function genfile() {
            $.post("/AjaxTool/SaveKuang.cspx?", { kuang: encodeURIComponent($.trim($('#ctl00_ybContent_kuang').val())), mobanContent: encodeURIComponent($.trim($('#ctl00_ybContent_mobanContent').val())) }); //放入sessoin中
            top.navTab.openTab('codeshow', "Tool/KuangCode.aspx?mySet=" + $('#ctl00_ybContent_mySet').val(), { title: '查看代码', external: true, fresh: true, data: {} });
            return false;
        }
    </script>
</asp:Content>
