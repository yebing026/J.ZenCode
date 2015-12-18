<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TbData.aspx.cs" Inherits="J.ZenCode.TbData" %>

<%@ Register Src="../Controls/DataPager1.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        <label for="tbs" class="line">
            表或视图:
            <asp:DropDownList ID="tbs" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="tbs_SelectedIndexChanged"
                AutoPostBack="True">
                <%--   <asp:ListItem Text="请先选择" Value="" />--%>
            </asp:DropDownList>
        </label>
        <label for="sort" class="line">
            排序名:
            <asp:DropDownList ID="sort" runat="server">
            </asp:DropDownList>
        </label>
        <label for="sort" class="line">
            方向:
            <asp:DropDownList ID="fXang" runat="server">
                <asp:ListItem Text="asc" />
                <asp:ListItem Text="desc" />
            </asp:DropDownList>
        </label>
        <label class="line">
            显示: <a title="选择字段" href="#showSel" target="iback" class="btnLook" backid="#showFields">
            </a>
            <asp:TextBox ID="showFields" runat="server" Text="*"></asp:TextBox>
        </label>
        <label for="where" class="line">
            搜索条件:<asp:TextBox ID="where" runat="server" Text=""  width="200"></asp:TextBox>
        </label>
        <asp:Button ID="_btnSearch" runat="server" Text="查询" OnClick="_btnSearch_Click" class="btn btn-blue btn-middle"
            Visible="false" />
        <asp:Button ID="_btnExt" runat="server" Text="导出EXCEL" OnClick="_btnExt_Click" class="btn btn-blue btn-middle" />
    </div>
    <div class="alert">
        <asp:Label ID="sql" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="tborder layout "
        EnableViewState="false">
    </asp:GridView>
    <uc1:DataPager ID="Pager" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0"
        PagedControlID="GridView1" />
    <div id="showSel" style="display: none;">
        <asp:ListView ID="ListView1" runat="server">
            <LayoutTemplate>
                <div class="form">
                    <tr id="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <label for="ck<%#Eval("Name")%>"  class="line-checkbox">
                    <input id="ck<%#Eval("Name")%>" type="checkbox" value='<%#Eval("Name")%>' name="item">
                    <%#Eval("Name")%>
                </label>
            </ItemTemplate>
        </asp:ListView>
        <input id="selFields" type="button" value="确定选择" class="btn btn-small btn-yellow"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#selFields").live('click', function () {
                var fs = getCbVal('item');
                if (fs != false) {
                    $(backId).val(fs);
                    $('.popover').hide();
                }
            });
        });
         
    </script>
</asp:Content>
