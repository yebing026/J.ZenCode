<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenCode.aspx.cs" Inherits="J.ZenCode.GenCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../public/js/template.js" type="text/javascript"></script>
    <script src="../public/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../public/js/htmlformart.js" type="text/javascript"></script>
    <link href="../public/js/prettify/prettify.css" rel="stylesheet" type="text/css" />
    <script src="../public/js/prettify/prettify.js" type="text/javascript"></script>
    <script src="../public/js/jquery.zclip.min.js" type="text/javascript"></script>
    <title></title>
</head>
<body style="padding: 10px; font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="path" runat="server" Width="200"></asp:TextBox>
        <input type="button" name="doFile" id="doFile" value=" 生成文件" />
    </div>
    <input id='formart' value='Html格式化' type="button" />
    <input id='copy' value='复制' type="button" />
    <pre class="prettyprint">
	<span id="content"></span>
	</pre>
    <script id="Template" type="text/template">
     <%=mobanContent %>
    </script>
    <script type="text/javascript">
   
    $(function () {
        var data = <%=json %>;
        var html = template.render('Template', data).replace(/ '/g, "'").replace(/(\n[\s|\t]*\r*\n)/g, '\n');
        $('#content').text(html);
        prettyPrint();
         $("#copy").zclip({
		path: "../public/js/ZeroClipboard.swf",
		copy: function(){
		return $("#content").text();
		},
		beforeCopy:function(){/* 按住鼠标时的操作 */
			$(this).css("color","orange");
		},
		afterCopy:function(){/* 复制成功后的操作 */
			$(this).val("成功复制");
        }
	});
    });
    $('#formart').click(function () {
        var html = style_html($('#content').text(), 2, ' ', 80); //template.render('Template', data);//
        $('#content').text(html);
        prettyPrint();
    });
     $('#doFile').click(function () {       
        var content=encodeURIComponent($('#content').text());
        var path=encodeURIComponent($('#path').val());
        var url="/AjaxTool/GenFile.cspx";
        $.post(url, {path:path,content:content}, function (d) {
                    if (d.Success == true) {
                        top.alertMsg.correct(d.Message);
                    } else {
                        top.alertMsg.error(d.Message);
                    }                 
                });
                event.preventDefault();
    });
    </script>
    </form>
</body>
</html>
