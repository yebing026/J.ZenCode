using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection.Emit;
using System.Reflection;
using System.ComponentModel;

namespace J.Common
{
    public static class TableHelper
    {
        public static List<string> ToStrList(this DataTable dt, int index)
        {
            List<string> list = new List<string>();
            if (dt == null || dt.Rows.Count == 0) return list;

            foreach (DataRow info in dt.Rows)
            {
                list.Add(info[index].ToString());
            }
            return list;
        }
        public static List<string> ToStrList(this DataTable dt, string name)
        {
            List<string> list = new List<string>();
            if (dt == null || dt.Rows.Count == 0) return list;

            foreach (DataRow info in dt.Rows)
            {
                list.Add(info[name].ToString());
            }
            return list;
        }
        /// <summary>
        /// 生成树结构的table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strParentColumn">父</param>
        /// <param name="strRootValue">根</param>
        /// <param name="strIndexColumn">关键字列</param>
        /// <param name="strTextColumn">显示列</param>
        /// <param name="i">缩进</param>
        /// <returns></returns>
        public static DataTable ToTreeTable(this DataTable dt, string strParentColumn, string strRootValue, string strIndexColumn, string strTextColumn, int i)
        {
            DataTable treeTb = dt.Clone();

            MakeTreeTb(dt, strParentColumn, strRootValue, strIndexColumn, strTextColumn, i, ref treeTb);
            return treeTb;
        }

        private static void MakeTreeTb(DataTable dt, string strParentColumn, string strRootValue, string strIndexColumn, string strTextColumn, int i, ref DataTable treeTb)
        {
            //每向下一层，多一个缩入单位  

            i++;
            DataView dvNodeSets = new DataView(dt);
            dvNodeSets.RowFilter = strRootValue == "null" ? strParentColumn + " is null" : strParentColumn + "=" + strRootValue;
            string strPading = ""; //缩入字符   
            //通过i来控制缩入字符的长度，我这里设定的是一个全角的空格   
            for (int j = 0; j < i; j++)
                strPading += "　";//如果要增加缩入的长度，改成两个全角的空格就可以了   
            foreach (DataRowView drv in dvNodeSets)
            {
                DataRow dr = treeTb.NewRow();
                drv[strTextColumn] = strPading + "├" + drv[strTextColumn].ToString();
                dr = drv.Row;
                treeTb.Rows.Add(dr.ItemArray);
                MakeTreeTb(dt, strParentColumn, drv[strIndexColumn].ToString(), strIndexColumn, strTextColumn, i, ref treeTb);
            }
            //递归结束，要回到上一层，所以缩入量减少一个单位   
            i--;
        }
        public static List<T> ToList<T>(this DataTable dt) where T : class,new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    //if (row[p.Name] is Int64)
                    //{
                    //    p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                    //    continue;
                    //}
                    if (dt.Columns.Contains(p.Name))
                    {
                        p.SetValue(entity, ChangeType(row[p.Name].ToString(), p.PropertyType), null);
                        //try
                        //{
                        //    p.SetValue(entity, ChangeType(row[p.Name].ToString(), p.PropertyType), null);
                        //}
                        //catch {
                        //    J.Common.FileHelper.WriteFile("d:\\log.txt",p.Name);
                        //}
                    }

                }
                list.Add(entity);
            }
            return list;
        }
        private static object ChangeType(string value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != "")
                {
                    NullableConverter nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                }
                else
                {
                    return null;
                }
            }
            return Convert.ChangeType(value, conversionType);
        }
        //public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        //{
        //    List<TResult> list = new List<TResult>();
        //    if (dt == null || dt.Rows.Count == 0) return list;
        //    DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
        //    foreach (DataRow info in dt.Rows) list.Add(eblist.Build(info));
        //    dt.Dispose(); dt = null;
        //    return list;
        //}


        //public class DataTableEntityBuilder<Entity>
        //{
        //    private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
        //    private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
        //    private delegate Entity Load(DataRow dataRecord);

        //    private Load handler;
        //    private DataTableEntityBuilder() { }

        //    public Entity Build(DataRow dataRecord)
        //    {
        //        return handler(dataRecord);
        //    }
        //    public static DataTableEntityBuilder<Entity> CreateBuilder(DataRow dataRecord)
        //    {
        //        DataTableEntityBuilder<Entity> dynamicBuilder = new DataTableEntityBuilder<Entity>();
        //        DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(Entity), new Type[] { typeof(DataRow) }, typeof(Entity), true);
        //        ILGenerator generator = method.GetILGenerator();
        //        LocalBuilder result = generator.DeclareLocal(typeof(Entity));
        //        generator.Emit(OpCodes.Newobj, typeof(Entity).GetConstructor(Type.EmptyTypes));
        //        generator.Emit(OpCodes.Stloc, result);

        //        for (int i = 0; i < dataRecord.ItemArray.Length; i++)
        //        {
        //            PropertyInfo propertyInfo = typeof(Entity).GetProperty(dataRecord.Table.Columns[i].ColumnName);
        //            Label endIfLabel = generator.DefineLabel();
        //            if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
        //            {
        //                generator.Emit(OpCodes.Ldarg_0);
        //                generator.Emit(OpCodes.Ldc_I4, i);
        //                generator.Emit(OpCodes.Callvirt, isDBNullMethod);
        //                generator.Emit(OpCodes.Brtrue, endIfLabel);
        //                generator.Emit(OpCodes.Ldloc, result);
        //                generator.Emit(OpCodes.Ldarg_0);
        //                generator.Emit(OpCodes.Ldc_I4, i);
        //                generator.Emit(OpCodes.Callvirt, getValueMethod);
        //                generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
        //                generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
        //                generator.MarkLabel(endIfLabel);
        //            }
        //        }
        //        generator.Emit(OpCodes.Ldloc, result);
        //        generator.Emit(OpCodes.Ret);
        //        dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
        //        return dynamicBuilder;
        //    }
        //}
    }
}
