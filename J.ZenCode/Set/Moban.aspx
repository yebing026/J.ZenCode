<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Moban.aspx.cs" Inherits="J.ZenCode.Moban" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $("#btnSelMoban").live('click', function () {
            var mIds = getCbVal('selId');
            if (mIds != false) {
                $.each(mIds.split(','), function (i, n) {
                    var title = "_" + $("#mb" + n).attr("rel");                    
                    var url = "Set/MobanShow.aspx?Id=" + n;
                    top.navTab.openTab(title, url, { title: title, external: true, fresh: true, data: {} });
                });
            } else {
                top.alertMsg.error('请选择模板');
            }
        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="panel_header form">
        <span class="line">
            类别:
            <asp:DropDownList ID="mId" runat="server">
            </asp:DropDownList>
        </span>
        <asp:Button ID="_btnSearch" runat="server" Text="查询" OnClick="_btnSearch_Click" class=" btn  btn-blue btn-middle" />
      <input id="btnSelMoban" type="button" value="批量查看模板" class=" btn  btn-blue btn-middle"/>
    </div>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder layout">
                <tr>
                  <th>
                        <input type="checkbox" id="checkAll" name="checkAll" class="selAll" target="selId" />
                    </th>
                    <th>
                        编号
                    </th>
                    <th>
                        排序
                    </th>
                    <th>
                        名称
                    </th>
                    <th>
                        描述
                    </th>
                    <th>
                        语言
                    </th>
                    <th>
                        是否启用
                    </th>
                    <th>
                        路径
                    </th>
                    <th>
                        操作
                    </th>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div class="alert error">
                没有数据</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
             <td>
                    <input type="checkbox" name="selId" value='<%#Eval("Id")%>' id='mb<%#Eval("Id")%>' rel='<%# Eval("name")%>'/>
                </td>
                <td>
                    <%# Eval("id")%>
                </td>
                <td>
                    <input type="text" value='<%#Eval("sort")%>' size="2" target="ichange" ref="Id=<%#Eval("Id")%>&tb=moban&field=sort" />
                </td>
                <td>
                <a title="查看模板" href="Set/MobanShow.aspx?Id=<%#Eval("id")%>" target="itab" class="blue">
                        <%# Eval("name")%></a>
                    
                </td>
                <td>
                    <%# Eval("dec")%>
                </td>
                <td>
                    <%# Eval("mode")%>
                </td>
                <td>
                    <a target="istatus" href="/AjaxTool/SetStatus.cspx?Id=<%#Eval("Id")%>&tb=moban&field=status"
                        class="status<%# Eval("status")%>">状态</a>
                </td>
                <td>
                    <%# Eval("path")%>
                </td>
                <td>
                    <a title="编辑模板" href="Set/MobanEdit.aspx?Id=<%#Eval("id")%>" target="itab" class="btn  btn-blue btn-middle">
                        编辑</a>  <a href="/AjaxTool/CopyById.cspx?Id=<%#Eval("Id")%>&tb=moban" target="iajax" class="btn  btn-blue btn-middle">
                            复制</a> <a href="/AjaxTool/DelById.cspx?Id=<%#Eval("Id")%>&tb=moban" target="iajax"
                                class="btn  btn-blue btn-middle">删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <a title="增加" href="Set/MobanEdit.aspx" target="itab" class="btn  btn-blue btn-middle">
        增加</a>
</asp:Content>
