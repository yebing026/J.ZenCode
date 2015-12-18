<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Code.aspx.cs" Inherits="J.ZenCode.Code" %>
<%@ Register Src="../Controls/DataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        <label for="mode" class="line">
            语言:
            <asp:DropDownList ID="sql_eq_mode" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="请选择" Value="" />
            </asp:DropDownList>
        </label>
        <asp:Button ID="_btnSearch" runat="server" Text="查询" OnClick="_btnSearch_Click" class=" btn  btn-blue btn-middle" />
     <a title="增加" href="/Tool/CodeEdit.aspx" target="itab" class="btn  btn-blue btn-middle">
        增加</a>
    </div>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder layout">
                <tr>
                
                    <th>
                        编号
                    </th>
                   
                    <th>
                        名称
                    </th>
                   
                    <th>
                        语言
                    </th>
                   
                    <th>
                        操作
                    </th>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
      
        <ItemTemplate>
            <tr>
            
                <td>
                    <%# Eval("id")%>
                </td>               
                <td>
                <a title="查看代码" href="/Tool/CodeShow.aspx?Id=<%#Eval("id")%>" target="itab" class="blue">
                        <%# Eval("name")%></a>
                    
                </td>
              
                <td>
                    <%# Eval("mode")%>
                </td>
              
                <td>
                    <a title="编辑代码" href="/Tool/CodeEdit.aspx?Id=<%#Eval("id")%>" target="itab" class="btn  btn-blue btn-middle">
                        编辑</a>  <a href="/AjaxTool/CopyById.cspx?Id=<%#Eval("Id")%>&tb=code" target="iajax" class="btn  btn-blue btn-middle">
                            复制</a> <a href="/AjaxTool/DelById.cspx?Id=<%#Eval("Id")%>&tb=code" target="iajax"
                                class="btn  btn-blue btn-middle">删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <uc1:DataPager ID="Pager" runat="server" PageIndex="1" RecordsPerPage="20" TotalRecords="0"
        TableName="code" OrderExpression="Id desc" PagedControlID="ListView1" />
</asp:Content>
