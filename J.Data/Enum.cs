using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace J.Data.Enum
{
    public enum DbItemType
    {
        Table,        
        View,
        Procedure,
        Function
    }
    public enum DbTypes
    {
        Sqlite,
        MySql,
        SqlServer,
        OleDb,
        Oracle,
    }
    public enum DicType
    {       
       前台控件,
       查询逻辑,      
       正则,
       CSS,
       命名空间,
       模板语言,
    }  
    
}
