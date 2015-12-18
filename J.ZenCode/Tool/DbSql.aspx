<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="J.ZenCode.DbSql"
    CodeBehind="DbSql.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../public/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../public/js/prettify/prettify.css" rel="stylesheet" type="text/css" />
    <script src="../public/js/prettify/prettify.js" type="text/javascript"></script>
    <script src="../public/js/prettify/lang-sql.js" type="text/javascript"></script>
    <title></title>
</head>
<body style="font-size: 12px; padding: 15px;">
    <asp:ListView ID="ListView1" runat="server" EnableViewState="false">
        <LayoutTemplate>
            <pre class="prettyprint lang-sql">
                <div id="itemPlaceholder" runat="server">
                </div>
            </pre>
        </LayoutTemplate>
        <ItemTemplate>
            <div>
                <%# Eval("val")%></div>
        </ItemTemplate>
    </asp:ListView>
    <script type="text/javascript">
        $(function () { prettyPrint(); });
    </script>
</body>
</html>
