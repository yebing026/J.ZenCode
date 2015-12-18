using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace J.Data
{
    static class DbHelper
    {

        public static string GetOleDbType(int type)
        {
            switch (type)
            {
                case 2:
                    return "short";
                case 3:
                    return "int";
                case 4:
                    return "single";
                case 5:
                    return "double";
                case 6:
                    return "money";
                case 7:
                case 133:
                    return "datetime";
                case 11:
                    return "bit";
                case 13:
                    return "timestamp";
                case 17:
                    return "byte";
                case 72:
                    return "uniqueidentifier";
                case 128:
                case 204:
                    return "binary";
                case 129:
                    return "char";
                case 130:
                case 200:
                case 202:
                    return "varchar";
                case 131:
                    return "float";
                case 135:
                    return "smalldatetime";
                case 201:
                case 203:
                    return "text";
                case 205:
                    return "image";
                default:
                    return "no";
            }
        }
        public static int? GetLength(string type)
        {
            int index = type.IndexOf("(");
            if (index > 0)
            {
                int num2 = type.IndexOf(")");
                var str = type.Substring(index + 1, (num2 - index) - 1);
                int lenth = 0;
                int.TryParse(str.Split(',')[0], out lenth);
                return lenth;
            }
            else
            {
                return null;
            }

        }
    }
}
