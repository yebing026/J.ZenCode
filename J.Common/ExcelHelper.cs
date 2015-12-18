﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace J.Common
{
    /// <summary>
    /// Excel 操作类
    /// </summary>
    public class ExcelHelper
    {
        #region Public Field
        /// <summary>
        /// Excel 版本
        /// </summary>
        public enum ExcelType
        {
            Excel2003,
            Excel2007
        }

        /// <summary>
        /// IMEX 三种模式。
        /// IMEX是用来告诉驱动程序使用Excel文件的模式，其值有0、1、2三种，分别代表导出、导入、混合模式。
        /// </summary>
        public enum IMEXType
        {
            ExportMode = 0,
            ImportMode = 1,
            LinkedMode = 2
        }
        #endregion

        #region 获取Excel连接字符串

        /// <summary>
        /// 返回Excel 连接字符串   [IMEX=1]
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回值</returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType)
        {
            return GetExcelConnectstring(excelPath, header, eType, IMEXType.ImportMode);
        }

        /// <summary>
        /// 返回Excel 连接字符串
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <param name="imex">IMEX模式</param>
        /// <returns>返回值</returns>
        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType, IMEXType imex)
        {
            if (!FileHelper.IsExistFile(excelPath))
                throw new FileNotFoundException("Excel路径不存在!");

            string connectstring;

            string hdr = "NO";
            if (header)
                hdr = "YES";

            if (eType == ExcelType.Excel2003)
                connectstring = "Provider=Microsoft.Jet.OleDb.4.0; data source=" + excelPath +
                                ";Extended Properties='Excel 8.0; HDR=" + hdr + "; IMEX=" + imex.GetHashCode() + "'";
            else
                connectstring = "Provider=Microsoft.ACE.OLEDB.12.0; data source=" + excelPath +
                                ";Extended Properties='Excel 12.0 Xml; HDR=" + hdr + "; IMEX=" + imex.GetHashCode() +
                                "'";

            return connectstring;
        }

        #endregion

        #region 获取Excel工作表名

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回值</returns>
        public static List<string> GetExcelTablesName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelTablesName(connectstring);
        }

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns>返回值</returns>
        public static List<string> GetExcelTablesName(string connectstring)
        {
            using (var conn = new OleDbConnection(connectstring))
            {
                return GetExcelTablesName(conn);
            }
        }

        /// <summary>
        /// 返回Excel工作表名
        /// </summary>
        /// <param name="connection">excel连接</param>
        /// <returns>返回值</returns>
        public static List<string> GetExcelTablesName(OleDbConnection connection)
        {
            var list = new List<string>();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(string.Format("{0}",dt.Rows[i][2]));
                }
            }

            return list;
        }

        /// <summary>
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回值</returns>
        public static string GetExcelFirstTableName(string excelPath, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            return GetExcelFirstTableName(connectstring);
        }

        /// <summary>
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns>返回值</returns>
        public static string GetExcelFirstTableName(string connectstring)
        {
            using (var conn = new OleDbConnection(connectstring))
            {
                return GetExcelFirstTableName(conn);
            }
        }

        /// <summary>
        /// 返回Excel第一个工作表表名
        /// </summary>
        /// <param name="connection">excel连接</param>
        /// <returns>返回值</returns>
        public static string GetExcelFirstTableName(OleDbConnection connection)
        {
            string tableName = string.Empty;

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                tableName = string.Format("{0}", dt.Rows[0][2]);
            }

            return tableName;
        }

        /// <summary>
        /// 获取Excel文件中指定工作表的列
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="eType">ExcelType</param>
        /// <param name="table">名称 excel table  例如：Sheet1$</param>
        /// <returns>返回值</returns>
        public static List<string> GetColumnsList(string excelPath, ExcelType eType, string table)
        {
            DataTable tableColumns;
            string connectstring = GetExcelConnectstring(excelPath, true, eType);
            using (var conn = new OleDbConnection(connectstring))
            {
                conn.Open();
                tableColumns = GetReaderSchema(table, conn);
            }

            return (from DataRow dr in tableColumns.Rows let columnName = dr["ColumnName"].ToString() let datatype = ((OleDbType)dr["ProviderType"]).ToString() let netType = dr["DataType"].ToString() select columnName).ToList();
        }

        private static DataTable GetReaderSchema(string tableName, OleDbConnection connection)
        {
            DataTable schemaTable;
            IDbCommand cmd = new OleDbCommand();
            cmd.CommandText = string.Format("select * from [{0}]", tableName);
            cmd.Connection = connection;

            using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
            {
                schemaTable = reader.GetSchemaTable();
            }
            return schemaTable;
        }

        #endregion

        #region EXCEL导入DataSet

        /// <summary>
        /// EXCEL导入DataSet
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns>
        public static DataSet ExcelToDataSet(string excelPath, string table, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring, table);
        }

        /// <summary>
        /// 判断工作表名是否存在
        /// </summary>
        /// <param name="connection">excel连接</param>
        /// <param name="table">名称 excel table  例如：Sheet1$</param>
        /// <returns>返回值</returns>
        private static bool IsExistExcelTableName(OleDbConnection connection, string table)
        {
            List<string> list = GetExcelTablesName(connection);
            return list.Any(tName => tName.ToLower() == table.ToLower());
        }

        /// <summary>
        /// EXCEL导入DataSet
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <param name="table">名称 excel table  例如：Sheet1$ </param>
        /// <returns>返回Excel相应工作表中的数据 DataSet   [table不存在时返回空的DataSet]</returns>
        public static DataSet ExcelToDataSet(string connectstring, string table)
        {
            using (var conn = new OleDbConnection(connectstring))
            {
                var ds = new DataSet();

                //判断该工作表在Excel中是否存在
                if (IsExistExcelTableName(conn, table))
                {
                    var adapter = new OleDbDataAdapter("SELECT * FROM [" + table + "]", conn);
                    adapter.Fill(ds, table);
                }

                return ds;
            }
        }

        /// <summary>
        /// EXCEL所有工作表导入DataSet
        /// </summary>
        /// <param name="excelPath">Excel文件 绝对路径</param>
        /// <param name="header">是否把第一行作为列名</param>
        /// <param name="eType">Excel 版本 </param>
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
        public static DataSet ExcelToDataSet(string excelPath, bool header, ExcelType eType)
        {
            string connectstring = GetExcelConnectstring(excelPath, header, eType);
            return ExcelToDataSet(connectstring);
        }

        /// <summary>
        /// EXCEL所有工作表导入DataSet
        /// </summary>
        /// <param name="connectstring">excel连接字符串</param>
        /// <returns>返回Excel第一个工作表中的数据 DataSet </returns>
        public static DataSet ExcelToDataSet(string connectstring)
        {
            using (var conn = new OleDbConnection(connectstring))
            {
                var ds = new DataSet();
                List<string> tableNames = GetExcelTablesName(conn);

                foreach (string tableName in tableNames)
                {
                    var adapter = new OleDbDataAdapter("SELECT * FROM [" + tableName + "]", conn);
                    adapter.Fill(ds, tableName);
                }
                return ds;
            }
        }

        #endregion

        #region DataSet导入EXCEL
        /// <summary>
        /// 把一个数据集中的数据导出到Excel文件中(XML格式操作)
        /// </summary>
        /// <param name="source">DataSet数据</param>
        /// <param name="fileName">保存的Excel文件名</param>
        public static void DataSetToExcel(DataSet source, string fileName)
        {
            #region Excel格式内容

            var excelDoc = new StreamWriter(fileName);
            const string startExcelXML = "<xml version>\r\n<Workbook " +
                                         "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                                         " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                                         "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                                         "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                                         "office:spreadsheet\">\r\n <Styles>\r\n " +
                                         "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                                         "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                                         "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                                         "\r\n <Protection/>\r\n </Style>\r\n " +
                                         "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                                         "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                                         "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                                         " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                                         "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                                         "ss:Format=\"#,##0.###\"/>\r\n </Style>\r\n " +
                                         "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                                         "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                                         "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                                         "ss:Format=\"yyyy-mm-dd;@\"/>\r\n </Style>\r\n " +
                                         "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            #endregion

            int sheetCount = 1;
            excelDoc.Write(startExcelXML);
            for (int i = 0; i < source.Tables.Count; i++)
            {
                int rowCount = 0;
                DataTable dt = source.Tables[i];

                excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                excelDoc.Write("<Table>");
                excelDoc.Write("<Row>");
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(source.Tables[0].Columns[x].ColumnName);
                    excelDoc.Write("</Data></Cell>");
                }
                excelDoc.Write("</Row>");
                foreach (DataRow x in dt.Rows)
                {
                    rowCount++;
                    //if the number of rows is > 64000 create a new page to continue output

                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("</Table>");
                        excelDoc.Write(" </Worksheet>");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                        excelDoc.Write("<Table>");
                    }
                    excelDoc.Write("<Row>"); //ID=" + rowCount + "

                    for (int y = 0; y < source.Tables[0].Columns.Count; y++)
                    {
                        Type rowType = x[y].GetType();

                        #region 根据不同数据类型生成内容

                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string xmLstring = x[y].ToString();
                                xmLstring = xmLstring.Trim();
                                xmLstring = xmLstring.Replace("&", "&");
                                xmLstring = xmLstring.Replace(">", ">");
                                xmLstring = xmLstring.Replace("<", "<");
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(xmLstring);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by
                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                                //The Following Code puts the date stored in XMLDate
                                //to the format above

                                var xmlDate = (DateTime)x[y];

                                string xmlDatetoString = xmlDate.Year +
                                                         "-" +
                                                         (xmlDate.Month < 10
                                                              ? "0" +
                                                                xmlDate.Month
                                                              : xmlDate.Month.ToString()) +
                                                         "-" +
                                                         (xmlDate.Day < 10
                                                              ? "0" +
                                                                xmlDate.Day
                                                              : xmlDate.Day.ToString()) +
                                                         "T" +
                                                         (xmlDate.Hour < 10
                                                              ? "0" +
                                                                xmlDate.Hour
                                                              : xmlDate.Hour.ToString()) +
                                                         ":" +
                                                         (xmlDate.Minute < 10
                                                              ? "0" +
                                                                xmlDate.Minute
                                                              : xmlDate.Minute.ToString()) +
                                                         ":" +
                                                         (xmlDate.Second < 10
                                                              ? "0" +
                                                                xmlDate.Second
                                                              : xmlDate.Second.ToString()) +
                                                         ".000";
                                excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                               "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(xmlDatetoString);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                            case "System.Decimal":
                                excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                               "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                           
                            case "System.Double":
                                excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                               "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>");
                                break;
                            default:
                                throw (new Exception(rowType + " not handled."));
                        }

                        #endregion
                    }
                    excelDoc.Write("</Row>");
                }
                excelDoc.Write("</Table>");
                excelDoc.Write(" </Worksheet>");

                sheetCount++;
            }

            excelDoc.Write(endExcelXML);
            excelDoc.Close();
            FileHelper.OpenFile(fileName);
        }
       
        #endregion
    }
}