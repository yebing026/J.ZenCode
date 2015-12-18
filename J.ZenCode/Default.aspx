<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="J.ZenCode.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>正代码生成器-简单实用代码生成器</title>
    <link href="public/dwz/themes/default/style.css" rel="stylesheet" type="text/css"
        media="screen" />
    <link href="public/dwz/themes/css/core.css" rel="stylesheet" type="text/css" media="screen" />
    <!--[if IE]>
<link href="public/dwz/themes/css/ieHack.css" rel="stylesheet" type="text/css" media="screen"/>
<![endif]-->
    <!--[if lte IE 9]>
<script src="public/dwz/js/speedup.js" type="text/javascript"></script>
<![endif]-->
    <script src="public/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="public/dwz/js/dwz.min.js?_=123" type="text/javascript"></script>
    <script type="text/javascript">
        $.ajaxSetup({
            cache: false,
            async: false,
        });
        function initDbTree() {
            var menulist = '';
            $.getJSON("AjaxTool/GetTbTree.cspx", { 'type':0 }, function (json) {
                if (json.Success == false) {
                    alert('数据库连接出错:' + json.Message);
                } else {
                    menulist += "<li><a>由表生成代码</a><ul>";
                    $.each(json, function (j, o) {
                        menulist += '<li><a target="navTab" rel="'+o+'"   refresh="true"  external="true" href="Field.aspx?tb=' + o + '" >' + o + '</a></li> ';
                    });
                    menulist += "</ul></li>";
                    $.getJSON("AjaxTool/GetTbTree.cspx", { 'type':1 }, function (json) {
                        if (json.length > 0) {
                            menulist += "<li><a>由视图生成代码</a><ul>";
                            $.each(json, function (j, o) {
                                menulist += '<li><a target="navTab" rel="'+o+'"  refresh="true"  external="true" href="Field.aspx?tb=' + o + '" >' + o + '</a></li> ';
                            });
                            menulist += "</ul></li>";
                        }
                        $("#dbtree").find("li").remove().end().append(menulist);

                    });

                }
            });
        }
        function initSet() {
            $.getJSON("AjaxTool/GetDbs.cspx",
               function (json) {
                   var menulist = '';
                   $.each(json, function (j, o) {
                       menulist += '<li><a  href="AjaxTool/ChangeDb.cspx?Id=' + o.Id + '" >' + o.dbName + '</a></li> ';
                   })
                   $("#switch_db").append(menulist);

               });
            $.getJSON("AjaxTool/GetMoban.cspx",
                function (json) {
                    var menulist = '';
                    $.each(json, function (j, o) {
                        menulist += '<li><a  href="AjaxTool/ChangeMoban.cspx?Id=' + o.Id + '" >' + o.name + '</a></li> ';
                    })
                    $("#switch_moban").append(menulist);
                });
            $.getJSON("AjaxTool/GetTool.cspx",
                function (json) {
                    var menulist = '';
                    $.each(json, function (j, o) {
                        menulist += '<li><a target="navTab" rel="'+o.name+'"  refresh="true" external="true" href="' + o.url + '" >' + o.name + '</a></li> ';
                    })
                    $("#elsetool").append(menulist);
                });
        }
        $(function () {
            initDbTree();
            initSet();
            DWZ.init("public/dwz/dwz.frag.xml", {
                loginUrl: "login_dialog.html", loginTitle: "登录", // 弹出登录对话框
                //		loginUrl:"login.html",	// 跳到登录页面
                statusCode: { ok: 200, error: 300, timeout: 301 }, //【可选】
                pageInfo: { pageNum: "pageNum", numPerPage: "numPerPage", orderField: "orderField", orderDirection: "orderDirection" }, //【可选】
                keys: { statusCode: "statusCode", message: "message" }, //【可选】
                ui: { hideMode: 'offsets' }, //【可选】hideMode:navTab组件切换的隐藏方式，支持的值有’display’，’offsets’负数偏移位置的值，默认值为’display’
                debug: false, // 调试模式 【true|false】
                callback: function () {
                    initEnv();
                    $("#themeList").theme({ themeBase: "public/dwz/themes" }); // themeBase 相对于index页面的主题base路径
                }
            });
        });

    </script>
</head>
<body scroll="no">
    <div id="layout">
        <div id="header">
            <div class="headerNav">
                <a class="logo" href="http://j-ui.com">标志</a>
                <ul class="nav">
                    <li class="switchEnvBox"><a href="javascript:">（<span>
                        <%=dbName %></span>）切换数据库</a>
                        <ul id="switch_db">
                        </ul>
                    </li>
                    <li class="switchEnvBox"><a href="javascript:">（<span>
                        <%=mobanType %></span>）切换模板类别</a>
                        <ul id="switch_moban">
                        </ul>
                    </li>
                    <li><a href="/Tool/Code.aspx" target="navTab" rel="pageCode" title="常用代码收集" fresh="true"
                        external="true">常用代码收集</a></li>
                    <li><a href="https://me.alipay.com/dwzteam" target="_blank">捐赠</a></li>
                    <li><a href="http://www.cnblogs.com/dwzjs" target="_blank">博客</a></li>
                    <li><a href="http://weibo.com/dwzui" target="_blank">微博</a></li>
                </ul>
                <ul class="themeList" id="themeList">
                    <li theme="default">
                        <div class="selected">
                            蓝色
                        </div>
                    </li>
                    <li theme="green">
                        <div>
                            绿色
                        </div>
                    </li>
                    <!--<li theme="red"><div>红色</div></li>-->
                    <li theme="purple">
                        <div>
                            紫色
                        </div>
                    </li>
                    <li theme="silver">
                        <div>
                            银色
                        </div>
                    </li>
                    <li theme="azure">
                        <div>
                            天蓝
                        </div>
                    </li>
                </ul>
            </div>
            <!-- navMenu -->
        </div>
        <div id="leftside">
            <div id="sidebar_s">
                <div class="collapse">
                    <div class="toggleCollapse">
                        <div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div class="toggleCollapse">
                    <h2>
                        主菜单</h2>
                    <div>
                        收缩
                    </div>
                </div>
                <div class="accordion" fillspace="sidebar">
                    <div class="accordionHeader">
                        <h2>
                            <span>Folder</span>代码生成</h2>
                    </div>
                    <div class="accordionContent" id="zencode">
                        <ul class="tree treeFolder" id="dbtree">
                            <li><a>由表生成代码</a>
                                <ul>
                                    <li></li>
                                </ul>
                            </li>
                            <li><a>由视图生成代码</a>
                                <ul>
                                    <li></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <div class="accordionHeader">
                        <h2>
                            <span>Folder</span>系统设置</h2>
                    </div>
                    <div class="accordionContent">
                        <ul class="tree treeFolder">
                            <li><a target="navTab" external="true" fresh="true" href="Set/DbConn.aspx" title="数据库连接" rel="数据库连接">
                                数据库连接</a></li>
                            <li><a target="navTab" external="true" fresh="true" href="Set/Dic.aspx" title="字典设置" rel="字典设置">
                                字典设置</a></li>
                            <li><a target="navTab" external="true" fresh="true" href="Set/MbType.aspx" title="模板分组" rel="模板分组">
                                模板分组</a></li>
                            <li><a target="navTab" external="true" fresh="true" href="Set/Moban.aspx" title="模板设置" rel="模板设置">
                                模板设置</a></li>
                            <li><a target="navTab" external="true" fresh="true" href="FieldAll.aspx" title="字段汇总" rel="字段汇总">
                                字段汇总</a></li>
                            <li><a target="navTab" external="true" fresh="true" href="Set/Tools.aspx" title="工具设置" rel="工具设置">
                                工具设置</a></li>
                        </ul>
                    </div>
                    <div class="accordionHeader">
                        <h2>
                            <span>Folder</span>其它工具</h2>
                    </div>
                    <div class="accordionContent">
                        <ul class="tree" id="elsetool">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="container">
            <div id="navTab" class="tabsPage">
                <div class="tabsPageHeader">
                    <div class="tabsPageHeaderContent">
                        <!-- 显示左右控制时添加 class="tabsPageHeaderMargin" -->
                        <ul class="navTab-tab">
                            <li tabid="main" class="main"><a href="javascript:;"><span><span class="home_icon">我的主页</span></span></a></li>
                        </ul>
                    </div>
                    <div class="tabsLeft">
                        left
                    </div>
                    <!-- 禁用只需要添加一个样式 class="tabsLeft tabsLeftDisabled" -->
                    <div class="tabsRight">
                        right
                    </div>
                    <!-- 禁用只需要添加一个样式 class="tabsRight tabsRightDisabled" -->
                    <div class="tabsMore">
                        more
                    </div>
                </div>
                <ul class="tabsMoreList">
                    <li><a href="javascript:;">我的主页</a></li>
                </ul>
                <div class="navTab-panel tabsPageContent layoutBox">
                    <div class="page unitBox">
                        <div class="accountInfo">
                            <div class="alertInfo">
                                <p>
                                    <a href="https://code.csdn.net/dwzteam/dwz_jui/tree/master/doc" target="_blank" style="line-height: 19px">
                                        <span>DWZ框架使用手册</span></a>
                                </p>
                                <p>
                                    <a href="http://pan.baidu.com/s/18Bb8Z" target="_blank" style="line-height: 19px">DWZ框架开发视频教材</a>
                                </p>
                            </div>
                            <div class="right">
                                <p style="color: red">
                                    DWZ官方微博 <a href="http://weibo.com/dwzui" target="_blank">http://weibo.com/dwzui</a>
                                </p>
                            </div>
                            <p>
                                <span>DWZ富客户端框架</span>
                            </p>
                            <p>
                                DWZ官方微博:<a href="http://weibo.com/dwzui" target="_blank">http://weibo.com/dwzui</a>
                            </p>
                        </div>
                        <div class="pageFormContent" layouth="80" style="margin-right: 230px">
                            <h2>
                                DWZ系列开源项目:</h2>
                            <div class="unit">
                                <a href="https://code.csdn.net/dwzteam/dwz_jui" target="_blank">dwz富客户端框架 - jUI</a>
                            </div>
                            <div class="unit">
                                <a href="https://code.csdn.net/dwzteam/dwz_ssh2" target="_blank">dwz4j企业级Java Web快速开发框架(Hibernate+Spring+Struts2)
                                    + jUI整合应用</a>
                            </div>
                            <div class="unit">
                                <a href="https://code.csdn.net/dwzteam/dwz_springmvc" target="_blank">dwz4j企业级Java Web快速开发框架(Mybatis
                                    + SpringMVC) + jUI整合应用</a>
                            </div>
                            <div class="unit">
                                <a href="https://code.csdn.net/dwzteam/dwz_thinkphp" target="_blank">ThinkPHP + jUI整合应用</a>
                            </div>
                            <div class="unit">
                                <a href="https://code.csdn.net/dwzteam/dwz_zendframework" target="_blank">Zend Framework
                                    + jUI整合应用</a>
                            </div>
                            <div class="unit">
                                <a href="http://www.yiiframework.com/extension/dwzinterface/" target="_blank">YII +
                                    jUI整合应用</a>
                            </div>
                            <a class="button" href="https://code.csdn.net/dwzteam" target="_blank"><span style="color: red">
                                DWZ开源系列源码下载</span></a>
                            <div class="divider">
                            </div>
                            <h2>
                                常见问题及解决:</h2>
                            <pre style="margin: 5px; line-height: 1.4em">
Error loading XML document: dwz.frag.xml
直接用IE打开index.html弹出一个对话框：Error loading XML document: dwz.frag.xml
原因：没有加载成功dwz.frag.xml。IE ajax laod本地文件有限制, 是ie安全级别的问题, 不是框架的问题。
解决方法：部署到apache 等 Web容器下。
</pre>
                            <div class="divider">
                            </div>
                            <h2>
                                有偿服务(<span style="color: red;">公司培训，技术支持，解决使用jUI过程中出现的全部疑难问题</span>):</h2>
                            <br />
                            <pre style="margin: 5px; line-height: 1.4em;">
合作电话：010-52897073	18600055221
技术支持：0571-86143180	17767167745
邮箱：support@j-ui.com
</pre>
                            <a class="button" href="http://code.csdn.net/groups/2155" target="_blank"><span>DWZ讨论组</span></a>
                        </div>
                        <div style="width: 230px; position: absolute; top: 60px; right: 0" layouth="80">
                            <iframe width="100%" height="430" class="share_self" frameborder="0" scrolling="no"
                                src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
        Copyright &copy; 2010 <a href="demo_page2.html" target="dialog">DWZ团队</a> 京ICP备05019125号-10
    </div>
</body>
</html>
