<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Field.aspx.cs" Inherits="J.ZenCode.Field" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ybContent" runat="server">
    <div class="form panel_header">
        <label class="line">
            <img title="所选翻译显示名" src="/public/images/fanYi.png" id="fanYi" />
        </label>
        <label class="line">
            当前表:<asp:TextBox ID="tbName" runat="server" ReadOnly Width="80"></asp:TextBox></label>
        <label class="line">
            模板:<a title="选择模板" href="#showSelMoban" target="iback" class="btnLook" backid="#mIds"></a>
            <input type="text" readonly id="mIds" size="8" /></label>
        <label class="line">
            命名空间:<a title="选择命名空间" href="#typeId4" target="iback" class="btnLook" backid="#nameSpace"></a>
            <input type="text" id="nameSpace" size="8" /></label>
        <label class="line">
            文件名:<asp:TextBox ID="fileName" runat="server" placeholder="文件名" Width="70"></asp:TextBox></label>
        <label class="line">
            自定义1:<asp:TextBox ID="mySet1" runat="server" placeholder="自定义属性1" Width="70"></asp:TextBox></label>
        <label class="line">
            自定义2:<asp:TextBox ID="mySet2" runat="server" placeholder="自定义属性2" Width="70"></asp:TextBox></label>
        <input type="button" class="btn btn-blue btn-middle" id="doCode" value="所选生成代码" />
        <asp:Button ID="_btnDel" runat="server" Text="删除所选" OnClick="_btnDel_Click" class="btn btn-blue btn-middle" />
    </div>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" EnableViewState="false">
        <LayoutTemplate>
            <table id="tlv" class="tborder layout">
                <tr>
                    <th>
                        <input type="checkbox" id="checkAll" name="checkAll" class="selAll" target="selId" />
                    </th>
                    <th>
                        次序
                    </th>
                    <th>
                        字段名
                    </th>
                    <th>
                        显示
                    </th>
                    <th>
                        主键
                    </th>
                    <th>
                        必须
                    </th>
                    <th>
                        查询
                    </th>
                    <th>
                        排序
                    </th>
                    <th>
                        前台属性
                    </th>
                    <th>
                        CSS
                    </th>
                    <th>
                        查询属性
                    </th>
                    <th>
                        属性
                    </th>
                    
                    <th>
                        长度
                    </th>
                    <th>
                        宽
                    </th>
                    <th>
                        高
                    </th>
                    <th>
                        函数
                    </th>
                    <th>
                        正则
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
                    <input type="checkbox" name="selId" value='<%#Eval("Id")%>' />
                </td>
                <td>
                    <input type="text" value='<%#Eval("sort")%>' size="1" target="ichange" ref="Id=<%#Eval("Id")%>&tb=field&field=sort" />
                </td>
                <td>
                    <%# Eval("fieldName")%>
                </td>
                <td>
                    <input type="text" id="show<%#Eval("Id")%>" rel="<%# Eval("fieldName")%>" value='<%#Eval("showName")%>'
                        size="8" target="ichange" ref="Id=<%#Eval("Id")%>&tb=field&field=showName" />
                </td>
                <td>
                    <a href="/AjaxTool/SetStatus.cspx?Id=<%#Eval("Id")%>&tb=field&field=isKey" class="status<%# Eval("isKey")%>"
                        target="istatus">isKey</a>
                </td>
                <td>
                    <a href="/AjaxTool/SetStatus.cspx?Id=<%#Eval("Id")%>&tb=field&field=required" class="status<%# Eval("required")%>"
                        target="istatus">required</a>
                </td>
                <td>
                    <a href="/AjaxTool/SetStatus.cspx?Id=<%#Eval("Id")%>&tb=field&field=isSearch" class="status<%# Eval("isSearch")%>"
                        target="istatus">isSearch</a>
                </td>
                <td>
                    <a href="/AjaxTool/SetStatus.cspx?Id=<%#Eval("Id")%>&tb=field&field=isSort" class="status<%# Eval("isSort")%>"
                        target="istatus">isSort</a>
                </td>
                <td>
                    <a title="选择前台控件" href="#typeId0" target="iback" class="btnLook" backid="#uiType<%#Eval("Id")%>">
                    </a>
                    <input type="text" id="uiType<%#Eval("Id")%>" value='<%#Eval("uiType")%>' size="4"
                        ref="Id=<%#Eval("Id")%>&tb=field&field=uiType" readonly />
                </td>
                <td>
                    <a title="选择Css" href="#typeId3" target="iback" class="btnLook" backid="#css<%#Eval("Id")%>">
                    </a>
                    <input type="text" id="css<%#Eval("Id")%>" value='<%#Eval("css")%>' size="5" ref="Id=<%#Eval("Id")%>&tb=field&field=css"
                        target="ichange" />
                </td>
                <td>
                    <a title="选择查询逻辑" href="#typeId1" target="iback" class="btnLook" backid="#search<%#Eval("Id")%>">
                    </a>
                    <input type="text" id="search<%#Eval("Id")%>" value='<%#Eval("search")%>' size="5"
                        ref="Id=<%#Eval("Id")%>&tb=field&field=search" target="ichange" />
                </td>
                <td>
                    <%# Eval("fieldType")%>
                </td>
               
                <td>
                    <%# Eval("length")%>
                </td>
                <td>
                    <input type="text" value='<%#Eval("width")%>' size="2" target="ichange" ref="Id=<%#Eval("Id")%>&tb=field&field=width" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("height")%>' size="2" target="ichange" ref="Id=<%#Eval("Id")%>&tb=field&field=height" />
                </td>
                <td>
                    <input type="text" value='<%#Eval("fun")%>' size="8" target="ichange" ref="Id=<%#Eval("Id")%>&tb=field&field=fun" />
                </td>
                <td>
                    <a title="选择正则" href="#typeId2" target="iback" class="btnLook" backid="#reg<%#Eval("Id")%>">
                    </a>
                    <input type="text" id="reg<%#Eval("Id")%>" value='<%#Eval("reg")%>' size="8" ref="Id=<%#Eval("Id")%>&tb=field&field=reg"
                        target="ichange" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div class="error">
        input输入框支持onchange事件数据库值同步更新，<i class="statusTrue">v</i><i class="statusFalse">v</i>支持点击切换数据库布尔值
    </div>
    
    <div id="showSelMoban" style="display: none;">
        <asp:ListView ID="lvMoban" runat="server">
            <LayoutTemplate>
                <div class="form">
                    <tr id="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <label for="mb<%#Eval("Id")%>"  class="line-checkbox">
                    <input id="mb<%#Eval("Id")%>" type="checkbox" value='<%#Eval("Id")%>' name="mbItem" rel='<%#Eval("name")%>'/>
                    <%#Eval("name")%>
                </label>
            </ItemTemplate>
        </asp:ListView>
        <input type="checkbox" name="ckmb" target="mbItem" class="selAll" /><input id="btnSelMoban" type="button" value="确定选择" class="btn btn-small btn-yellow"/>
    </div>
    <asp:Literal ID="DicStr" runat="server"></asp:Literal>
    <script type="text/javascript">
        $(function () {
            $('.selBack').live('click', function () {
                var val = $(this).find('input:checked').val();
                $(backId).val(val);
                if ($(backId).attr("ref")) {
                    var url = "/AjaxTool/SetVal.cspx?val=" + val + "&" + $(backId).attr("ref");
                    $.ajaxjson(url);
                }
                $('.popover').hide();
            });
            $("#btnSelMoban").live('click', function () {
                var fs = getCbVal('mbItem');
                if (fs != false) {
                    $(backId).val(fs);
                    $('.popover').hide();
                }
            });
            $("#doCode").click(function () {
                var ids = getCbVal("selId", '请先选择字段');
                var tbName =$("input[name$='tbName']").val();
                var fileName =$("input[name$='fileName']").val();
                var mySet1 =$("input[name$='mySet1']").val();
                var mySet2 =$("input[name$='mySet2']").val();
                var nameSpace = $("#nameSpace").val();
                if (ids == false) {
                    return;
                }
                var mIds = $('#mIds').val();
                if (mIds == '') {
                    top.alertMsg.error('请选择模板');
                    return;
                }
                $.each(mIds.split(','), function (i, n) {
                    var title = $("#mb" + n).attr("rel");
                    var url = "GenCode.aspx?tb={0}&mySet1={1}&mySet2={2}&mId={3}&ids={4}&fileName={5}&nameSpace={6}".format(tbName, mySet1, mySet2, n, ids, fileName, nameSpace);
                    top.navTab.openTab(title, url, { title: title, external: true, fresh: true, data: {} });
                });
            });
            $("#fanYi").live('click', function () {
                var fIds = getCbVal('selId');
                if (fIds != false) {
                    $.each(fIds.split(','), function (i, n) {
                        var fieldName = $("#show" + n).attr("rel");
                        var data = [];
                        $.ajax({
                            async: false,
                            method: 'GET',
                            url: 'https://openapi.baidu.com/public/2.0/bmt/translate',
                            dataType: 'JSONP',
                            data: {
                                client_id: 'Hs18iW3px3gQ6Yfy6Za0QGg4',
                                from: 'auto',
                                to: 'auto',
                                //需要翻译的内容
                                q: fieldName
                            }
                        }).done(function (response) {
                            response.trans_result.forEach(function (v) {
                                data.push(v.dst);
                            });
                            var val = data.join(',');
                            $("#show" + n).val(val);
                            var url = "/AjaxTool/SetVal.cspx?val=" + val + "&" + $("#show" + n).attr("ref");
                            $.ajaxjson(url);
                        });
                    });
                }
            });

        });
    </script>
</asp:Content>
