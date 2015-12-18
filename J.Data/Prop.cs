using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace J.Data.Model
{

    public interface ITbField
    {
        string Name { get; set; }
        string Type { get; set; }
        int? Length { get; set; }
        bool Required { get; }
        bool IsKey { get; }
        string Default { get; set; }
    }
    public class OleDbTbField : ITbField
    {
        public string COLUMN_NAME { get; set; }
        public long ORDINAL_POSITION { get; set; }
        public int DATA_TYPE { get; set; }
        public long? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public bool IS_NULLABLE { get; set; }
        public long COLUMN_FLAGS { get; set; }
        public string COLUMN_DEFAULT { get; set; }

        public string Type
        {
            get
            {
                return DbHelper.GetOleDbType(DATA_TYPE);

            }
            set { }
        }

        public int? Length
        {
            get
            {
                int lenth = 0;
                int.TryParse(CHARACTER_MAXIMUM_LENGTH.ToString(), out lenth);
                if (lenth == 0)
                {
                    return null;
                }
                return lenth;
            }
            set { }
        }
        public string Name
        {
            get
            {
                return COLUMN_NAME;
            }
            set { }
        }
        public bool Required
        {
            get
            {
                return IS_NULLABLE == true ? false : true;
            }
        }
        public bool IsKey
        {
            get
            {
                return DATA_TYPE == 3 && COLUMN_FLAGS == 90;
            }
        }
        public string Default
        {
            get
            {
                return COLUMN_DEFAULT;
            }
            set { }
        }
    }

    public class MySqlTbField : ITbField
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public string Null { get; set; }
        public string Key { get; set; }
        public string Default { get; set; }
        public int? Length
        {
            get
            {
                return DbHelper.GetLength(Type);
            }
            set { }
        }
        public string Name
        {
            get
            {
                return Field;
            }
            set { }
        }
        public bool Required
        {
            get
            {
                return Null == "YES" ? false : true;
            }
        }
        public bool IsKey
        {
            get
            {
                return Key == "PRI" ? true : false;
            }
        }
    }
    public class SqlServerTbField : ITbField
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Length { get; set; }
        public int NotNull { get; set; }
        public int Pk { get; set; }
        public string Default { get; set; }

        public bool Required
        {
            get
            {
                return NotNull == 0 ? false : true;
            }
        }
        public bool IsKey
        {
            get
            {
                return Pk == 0 ? false : true;
            }
        }
    }
    /// <summary>
    ///因没用过oracle,属性定义可能有问题，请高人指点
    /// </summary>
    public class OracleTbField : ITbField
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public bool NullAble { get; set; }
        public int COLUMN_ID { get; set; }
        public int? Length { get; set; }

        public bool Required
        {
            get
            {
                return NullAble == true ? false : true;
            }
        }
        public bool IsKey
        {
            get
            {
                return COLUMN_ID == 1 ? false : true;
            }
        }
    }
    public class SqliteTbField : ITbField
    {

        public string name { get; set; }
        public string type { get; set; }
        public string dflt_value { get; set; }
        public long notnull { get; set; }
        public long pk { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set { }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set { }
        }
        public string Default
        {
            get
            {
                return dflt_value;
            }
            set { }
        }
        public int? Length
        {
            get
            {
                return DbHelper.GetLength(type);
            }
            set { }
        }

        public bool Required
        {
            get
            {
                return notnull == 0 ? false : true;
            }
        }
        public bool IsKey
        {
            get
            {
                return pk == 0 ? false : true;
            }
        }
    }
    public class JsTreeNode
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class JsonMessage
    {
        public bool Success { get; set; }
        public bool Reload { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string Fun { get; set; }
        public JsonMessage()
        {
            Success = true;
            Message = "成功完成处理";
            Data = "201";
            Reload = false;
            Fun = "";
        }

    }

}
