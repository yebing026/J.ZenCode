using System;
using System.IO;
using System.Linq;
using System.Web;
using J.Ajax;
using J.Common;
using J.Data;
using J.Data.Enum;
using J.Data.Model;



public class AjaxTool
{
    [Action(Verb = "*")]
    public object SetStatus(string tb, long Id, string field)
    {
        var sql = string.Format("update {0} set {1}=(1-{1}) where Id={2}", tb, field, Id);
        var db = new Db().GetDb();
        db.ExecuteNonQuery(sql);
        //db.cat.Update(z => new cat { status = false });
        //db.cat.Update(u=>u.Id==Id,z => new cat { status = true });
        return new JsonResult(new JsonMessage { });


    }
    [Action(Verb = "*")]
    public object SetVal(string tb, long Id, string field, string val)
    {

        var sql = string.Format("update {0} set {1}='{2}' where Id={3}", tb, field, val, Id);

        var db = new Db().GetDb();
        db.ExecuteNonQuery(sql);
        return new JsonResult(new JsonMessage { });

    }


    [Action(Verb = "*")]
    public object DelById(long Id, string tb)
    {

        var sql = string.Format("delete from {0} where Id ={1}", tb, Id);
        var db = new Db().GetDb();
        db.ExecuteNonQuery(sql);
        return new JsonResult(new JsonMessage { Reload = true });

    }
    [Action(Verb = "*")]
    public object DelMbType(long Id)
    {
        var db = new Db().GetDb();
        if (db.ExecuteScalar("select count(*) from moban where mId=" + Id) == "0")
        {
            db.ExecuteNonQuery("delete from dic where Id=" + Id);
            return new JsonResult(new JsonMessage { Reload = true });
        }
        else
        {
            return new JsonResult(new JsonMessage { Success = false, Message = "请先删除相关模板" });
        }

    }


    [Action(Verb = "*")]
    public object GetTool()
    {
        var db = new Db().GetDb();
        var mbs = db.ExecuteDataTable("select * from tool where status=1 order by sort").ToList<tool>();
        return new JsonResult(mbs);

    }
    [Action(Verb = "*")]
    public object GetMoban()
    {
        var db = new Db().GetDb();
        var mbs = db.ExecuteDataTable("select * from mbtype ").ToList<mbtype>();
        return new JsonResult(mbs);

    }
    [Action(Verb = "*")]
    public object ChangeMoban(long Id)
    {
        var db = new Db().GetDb();
        db.ExecuteNonQuery("update mbtype set status=0 ");
        db.ExecuteNonQuery("update mbtype set status=1 where Id=" + Id);
        return "alertMsg.correct('成功改变模板分组');";

    }
    [Action(Verb = "*")]
    public object GetDbs()
    {
        var db = new Db().GetDb();
        var dbs = db.ExecuteDataTable("select * from dbconn").ToList<dbconn>();

        return new JsonResult(dbs);

    }
    [Action(Verb = "*")]
    public object ChangeDb(long Id)
    {
        var db = new Db().GetDb();
        db.ExecuteNonQuery("update dbconn set status=0");
        db.ExecuteNonQuery("update dbconn set status=1 where Id=" + Id);
        return "initDbTree(); $('#zencode').initUI();";

    }
    [Action(Verb = "POST")]
    public object CopyById(long Id, string tb)
    {
        var db = new Db().GetDb();
        var sql = "";
        switch (tb)
        {
            case "dbconn":
                sql = "insert into dbconn select (select max(Id)+1 from dbconn),conn,dbType,dbName,dec,0 from dbconn where Id=2" + Id;
                break;
            case "dic":
                sql = "insert into dic select (select max(Id)+1 from dic),typeId,name,inf,status,sort from dic where Id=" + Id;
                break;
            case "code":
                sql = "insert into code select (select max(Id)+1 from code),name,mode,content from code where Id=" + Id;
                break;
            case "moban":
                sql = "insert into moban select (select max(Id)+1 from moban),mId,name,mode,dec,content,status,path,sort from moban where Id=" + Id;
                break;
            default:
                break;
        }
        db.ExecuteNonQuery(sql);
        return new JsonResult(new JsonMessage { Reload = true });

    }


    [Action(Verb = "*")]
    public object GetTbTree(int type)
    {
        try
        {
            IDbSql _db = DBFactory.CreateDB();
            var dt = _db.ExecuteItem(type);
            return new JsonResult(dt);
        }
        catch (Exception ex)
        {
            return new JsonResult(new JsonMessage { Success = false, Message = ex.Message.ToString() });
        }
    }
    #region 代码生成
    [SessionMode(SessionMode.Support)]
    [Action(Verb = "*")]
    public object SaveKuang(string kuang, string mobanContent)
    {
        System.Web.HttpContext.Current.Session["kuang"] = kuang;
        System.Web.HttpContext.Current.Session["mobanContent"] = mobanContent;
        return System.Web.HttpContext.Current.Session["kuang"];
    }
    [Action(Verb = "*")]
    public object GenFile(string path, string content)
    {
        path = HttpUtility.UrlDecode(path);
        content = HttpUtility.UrlDecode(content);
        File.WriteAllText(path, content);
        return new JsonResult(new JsonMessage { });
    }

    #endregion
}