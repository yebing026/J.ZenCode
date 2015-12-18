using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace J.Data.Model
{  
  
    public class dic
    {       
        public long Id { get; set; }
        public int typeId { get; set; }
        public string name { get; set; }
        public string inf { get; set; }
        public bool status { get; set; }
        public int sort { get; set; }

    }
    public class tool
    { 
        public long Id { get; set; }       
        public string name { get; set; }
        public string url { get; set; }
        public bool status { get; set; }
        public int sort { get; set; }

    }
    public class dbconn
    {
        
        public long Id { get; set; }
        public String conn { get; set; }
        public int dbType { get; set; }
        public String dbName { get; set; }
        public String dec { get; set; }
        public bool status { get; set; }
    }
    public class mbtype
    {      
        public long Id { get; set; }       
        public String name { get; set; }
        public String inf { get; set; }
        public bool status { get; set; }
    }
    public class moban
    {
        public long Id { get; set; }
        public long mId { get; set; }
        public String name { get; set; }
        public String mode { get; set; }
        public String dec { get; set; }
        public String content { get; set; }        
        public bool status { get; set; }
        public String path { get; set; }
        public int sort { get; set; }
    }
    public class code
    {
        public long Id { get; set; }        
        public String name { get; set; }
        public String mode { get; set; }       
        public String content { get; set; }      
    }   
    public class field
    {
        public long Id { get; set; }
        public bool isKey { get; set; }
        public String tbName { get; set; }
        public String dbName { get; set; }
        public String fieldName { get; set; }
        public String showName { get; set; }
        public String uiType { get; set; }
        public String css { get; set; }
        public String search { get; set; }
        public String fieldType { get; set; }
        /// <summary>
        /// C#字段属性
        /// </summary>
        public String propType { get; set; }
        public String dfltValue { get; set; }        
        public bool required { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; } 
        public String fun { get; set; }
        public String reg { get; set; }
        public bool isSearch { get; set; }
        public bool isSort { get; set; }
        public int sort { get; set; }
    }
}
