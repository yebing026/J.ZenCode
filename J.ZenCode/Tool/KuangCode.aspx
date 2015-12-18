<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KuangCode.aspx.cs" Inherits="J.ZenCode.Tool.KuangCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../public/js/template.js" type="text/javascript"></script>
    <script src="../public/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../public/js/htmlformart.js" type="text/javascript"></script>
    <link href="../public/js/prettify/prettify.css" rel="stylesheet" type="text/css" />
    <script src="../public/js/prettify/prettify.js" type="text/javascript"></script>
    <title></title>
</head>
<body style=" padding:10px;">
    <form id="form1" runat="server">
    <input id='formart' value='Html格式化' type="button" />
    <pre class="prettyprint lang-html">
	<span id="content"></span>
	</pre>
    <script id="Template" type="text/template">
     <%=moban %>
    </script>
    <script type="text/javascript">
    $(function () {
        var data = <%=json %>;
        var html = template.render('Template', data).replace(/ '/g, "'").replace(/(\n[\s|\t]*\r*\n)/g, '\n');
        $('#content').text(html);
        prettyPrint();
    });
    $('#formart').click(function () {
        var html = style_html($('#content').text(), 2, ' ', 80); //template.render('Template', data);//
        $('#content').text(html);
        prettyPrint();
    });
    </script>
    </form>
</body>
</html>
