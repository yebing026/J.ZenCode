/*!
 * artTemplate - Template Engine
 * https://github.com/aui/artTemplate
 * Released under the MIT, BSD, and GPL Licenses
 */
!function(a){"use strict";var b=function(a,c){return b["string"==typeof c?"compile":"render"].apply(b,arguments)};b.version="2.0.2",b.openTag="<%",b.closeTag="%>",b.isEscape=!0,b.isCompress=!1,b.parser=null,b.render=function(a,c){var d=b.get(a)||e({id:a,name:"Render Error",message:"No Template"});return d(c)},b.compile=function(a,d){function l(c){try{return new j(c,a)+""}catch(f){return h?e(f)():b.compile(a,d,!0)(c)}}var g=arguments,h=g[2],i="anonymous";"string"!=typeof d&&(h=g[1],d=g[0],a=i);try{var j=f(a,d,h)}catch(k){return k.id=a||d,k.name="Syntax Error",e(k)}return l.prototype=j.prototype,l.toString=function(){return j.toString()},a!==i&&(c[a]=l),l};var c=b.cache={},d=b.helpers={$include:b.render,$string:function(a,b){return"string"!=typeof a&&(b=typeof a,"number"===b?a+="":a="function"===b?d.$string(a()):""),a},$escape:function(a){var b={"<":"&#60;",">":"&#62;",'"':"&#34;","'":"&#39;","&":"&#38;"};return d.$string(a).replace(/&(?![\w#]+;)|[<>"']/g,function(a){return b[a]})},$each:function(a,b){var c=Array.isArray||function(a){return"[object Array]"==={}.toString.call(a)};if(c(a))for(var d=0,e=a.length;e>d;d++)b.call(a,a[d],d,a);else for(d in a)b.call(a,a[d],d)}};b.helper=function(a,b){d[a]=b},b.onerror=function(b){var c="Template Error\n\n";for(var d in b)c+="<"+d+">\n"+b[d]+"\n\n";a.console&&console.error(c)},b.get=function(d){var e;if(c.hasOwnProperty(d))e=c[d];else if("document"in a){var f=document.getElementById(d);if(f){var g=f.value||f.innerHTML;e=b.compile(d,g.replace(/^\s*|\s*$/g,""))}}return e};var e=function(a){return b.onerror(a),function(){return"{Template Error}"}},f=function(){var a=d.$each,c="break,case,catch,continue,debugger,default,delete,do,else,false,finally,for,function,if,in,instanceof,new,null,return,switch,this,throw,true,try,typeof,var,void,while,with,abstract,boolean,byte,char,class,const,double,enum,export,extends,final,float,goto,implements,import,int,interface,long,native,package,private,protected,public,short,static,super,synchronized,throws,transient,volatile,arguments,let,yield,undefined",e=/\/\*[\w\W]*?\*\/|\/\/[^\n]*\n|\/\/[^\n]*$|"(?:[^"\\]|\\[\w\W])*"|'(?:[^'\\]|\\[\w\W])*'|[\s\t\n]*\.[\s\t\n]*[$\w\.]+/g,f=/[^\w$]+/g,g=new RegExp(["\\b"+c.replace(/,/g,"\\b|\\b")+"\\b"].join("|"),"g"),h=/^\d[^,]*|,\d[^,]*/g,i=/^,+|,+$/g,j=function(a){return a.replace(e,"").replace(f,",").replace(g,"").replace(h,"").replace(i,"").split(/^$|,+/)};return function(c,e,f){function x(a){return m+=a.split(/\n/).length-1,b.isCompress&&(a=a.replace(/[\n\r\t\s]+/g," ").replace(/<!--.*?-->/g,"")),a&&(a=r[1]+B(a)+r[2]+"\n"),a}function y(a){var c=m;if(i?a=i(a):f&&(a=a.replace(/\n/g,function(){return m++,"$line="+m+";"})),0===a.indexOf("=")){var e=0!==a.indexOf("==");if(a=a.replace(/^=*|[\s;]*$/g,""),e&&b.isEscape){var g=a.replace(/\s*\([^\)]+\)/,"");d.hasOwnProperty(g)||/^(include|print)$/.test(g)||(a="$escape("+a+")")}else a="$string("+a+")";a=r[1]+a+r[2]}return f&&(a="$line="+c+";"+a),z(a),a+"\n"}function z(b){b=j(b),a(b,function(a){n.hasOwnProperty(a)||(A(a),n[a]=!0)})}function A(a){var b;"print"===a?b=t:"include"===a?(o.$include=d.$include,b=u):(b="$data."+a,d.hasOwnProperty(a)&&(o[a]=d[a],b=0===a.indexOf("$")?"$helpers."+a:b+"===undefined?$helpers."+a+":"+b)),p+=a+"="+b+","}function B(a){return"'"+a.replace(/('|\\)/g,"\\$1").replace(/\r/g,"\\r").replace(/\n/g,"\\n")+"'"}var g=b.openTag,h=b.closeTag,i=b.parser,k=e,l="",m=1,n={$data:1,$id:1,$helpers:1,$out:1,$line:1},o={},p="var $helpers=this,"+(f?"$line=0,":""),q="".trim,r=q?["$out='';","$out+=",";","$out"]:["$out=[];","$out.push(",");","$out.join('')"],s=q?"if(content!==undefined){$out+=content;return content;}":"$out.push(content);",t="function(content){"+s+"}",u="function(id,data){data=data||$data;var content=$helpers.$include(id,data,$id);"+s+"}";a(k.split(g),function(a){a=a.split(h);var c=a[0],d=a[1];1===a.length?l+=x(c):(l+=y(c),d&&(l+=x(d)))}),k=l,f&&(k="try{"+k+"}catch(e){"+"throw {"+"id:$id,"+"name:'Render Error',"+"message:e.message,"+"line:$line,"+"source:"+B(e)+".split(/\\n/)[$line-1].replace(/^[\\s\\t]+/,'')"+"};"+"}"),k=p+r[0]+k+"return new String("+r[3]+");";try{var v=new Function("$data","$id",k);return v.prototype=o,v}catch(w){throw w.temp="function anonymous($data,$id) {"+k+"}",w}}}();"function"==typeof define?define(function(){return b}):"undefined"!=typeof exports&&(module.exports=b),a.template=b}(this);
/*!
 * artTemplate - Syntax Extensions
 * https://github.com/aui/artTemplate
 * Released under the MIT, BSD, and GPL Licenses
 */
 
(function (exports) {
    
    exports.openTag = '{{';
    exports.closeTag = '}}';


    exports.parser = function (code) {
        code = code.replace(/^\s/, '');
        
        var split = code.split(' ');
        var key = split.shift();
        var args = split.join(' ');

        switch (key) {

            case 'if':

                code = 'if(' + args + '){';
                break;

            case 'else':
                
                if (split.shift() === 'if') {
                    split = ' if(' + split.join(' ') + ')';
                } else {
                    split = '';
                }

                code = '}else' + split + '{';
                break;

            case '/if':

                code = '}';
                break;

            case 'each':
                
                var object = split[0] || '$data';
                var as     = split[1] || 'as';
                var value  = split[2] || '$value';
                var index  = split[3] || '$index';
                
                var param   = value + ',' + index;
                
                if (as !== 'as') {
                    object = '[]';
                }
                
                code =  '$each(' + object + ',function(' + param + '){';
                break;

            case '/each':

                code = '});';
                break;

            case 'echo':

                code = 'print(' + args + ');'
                break;

            case 'include':

                code = 'include(' + split.join(',') + ');';
                break;

            default:

                if (exports.helpers.hasOwnProperty(key)) {
                    
                    code = '==' + key + '(' + split.join(',') + ');';
                    
                } else {

                    code = code.replace(/[\s;]*$/, '');
                    code = '=' + code;
                }

                break;
        }
        
        
        return code;
    };
})(template);
