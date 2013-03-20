using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ContractWeb.Common
{
    /// <summary>
    ///      Obtain the System.Type object for a nullable types
    /// </summary>
    /// <remarks>
    ///     The Class Required for Dynamics IReader to handle Sql Nullable types
    /// </remarks>
    [System.Runtime.InteropServices.GuidAttribute("0DD7ADFF-979E-4998-98E5-0155F9140601")]
    internal static class TypeOf
    {
        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type Int32Nullable = typeof(int?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type Int64Nullable = typeof(long?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type BooleanNullable = typeof(bool?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type FloatNullable = typeof(float?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DoubleNullable = typeof(double?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type ByteNullable = typeof(byte?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DateTimeNullable = typeof(DateTime?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type GuidNullable = typeof(Guid?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DecimalNullable = typeof(decimal?);

        /// <summary>
        ///     Obtain the object as a Nullable type
        /// </summary>
        /// <param name="type">The type of DbReader field</param>
        /// <returns>The Nullable type to the corresponding type</returns>
        public static Type GetNullableType(Type type)
        {
            switch (type.Name)
            {
                case "Object":
                case "String":
                    break;

                case "Int32":
                    type = Int32Nullable;
                    break;

                case "Int64":
                    type = Int64Nullable;
                    break;

                case "Boolean":
                    type = BooleanNullable;
                    break;

                case "Double":
                    type = DoubleNullable;
                    break;

                case "DateTime":
                    type = DateTimeNullable;
                    break;

                case "Byte":
                    type = ByteNullable;
                    break;

                case "Guid":
                    type = GuidNullable;
                    break;

                case "Single":
                    type = FloatNullable;
                    break;

                case "Decimal":
                    type = DecimalNullable;
                    break;
            }

            return type;
        }
        public static bool IsDBNull(IDataRecord iDataRecord, string name)
        {
            return iDataRecord.IsDBNull(iDataRecord.GetOrdinal(name));
        }
        public static bool IsDBNull(DataRow dataRow, string name)
        {
            return dataRow.IsNull(dataRow.Table.Columns[name]);
        }
    }

    public class DynamicBuilder<T>
    {
        private DynamicBuilder()
        { }

        //IDataReader
        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(string) });
        private static readonly MethodInfo isDBNullMethod = typeof(TypeOf).GetMethod("IsDBNull", new Type[] { typeof(IDataRecord), typeof(string) });
        private static readonly MethodInfo hasColumnMethod = typeof(Dictionary<string, string>).GetMethod("ContainsKey", new Type[] { typeof(string) });
        private delegate T Load(IDataRecord dataRecord, Dictionary<string, string> columns);
        private Load handler;
        private T Build(IDataRecord dataRecord, Dictionary<string, string> columns)
        {
            return handler(dataRecord, columns);
        }
        //DataRow
        private static readonly MethodInfo getValueMethodDT = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(string) });
        private static readonly MethodInfo isDBNullMethodDT = typeof(TypeOf).GetMethod("IsDBNull", new Type[] { typeof(DataRow), typeof(string) });
        private delegate T LoadT(DataRow dataRecord, Dictionary<string, string> columns);
        private LoadT handlerT;
        private T BuildT(DataRow dataRecord, Dictionary<string, string> columns)
        {
            return handlerT(dataRecord, columns);
        }
        /// <summary>
        /// 创建 IDataReader
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        private static DynamicBuilder<T> CreateBuilder(IDataRecord dataRecord)
        {
            DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>();

            DynamicMethod method = new DynamicMethod("DynamicCreate", typeof(T), new Type[] { typeof(IDataRecord), typeof(Dictionary<string, string>) }, typeof(T), true);
            ILGenerator generator = method.GetILGenerator();
            LocalBuilder result = generator.DeclareLocal(typeof(T));

            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (var prop in props)
            {
#if CheckColumnExists
                //if(dictionary.ContainsKey(prop.Name){
                Label endIfLabel = generator.DefineLabel();
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldstr, prop.Name);
                generator.Emit(OpCodes.Callvirt, hasColumnMethod);
                generator.Emit(OpCodes.Brfalse, endIfLabel);
#endif

                //if(TypeOf.IsDBNull(iDataRecord,prop.Name){
                Label endIfLabel2 = generator.DefineLabel();
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldstr, prop.Name);
                generator.Emit(OpCodes.Call, isDBNullMethod);
                generator.Emit(OpCodes.Brtrue, endIfLabel2);


                // Depending on the type of the property, convert the datareader column value to the type
                switch (prop.PropertyType.Name)
                {
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "UInt16":
                    case "UInt32":
                    case "UInt64":
                    case "Byte":
                    case "SByte":
                    case "Boolean":
                    case "String":
                    case "DateTime":
                    case "Single":
                    case "Double":
                    case "Decimal":
                    case "Char":
                        // Load the T instance, SqlDataReader parameter and the field name onto the stack
                        generator.Emit(OpCodes.Ldloc_0);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldstr, prop.Name);

                        // Push the column value onto the stack
                        generator.Emit(OpCodes.Callvirt, getValueMethod);

                        //Convert Common Type
                        generator.Emit(OpCodes.Call, typeof(Convert).GetMethod("To" + prop.PropertyType.Name, new Type[] { typeof(object) }));
                        break;
                    case "Nullable`1":
                        // Load the T instance, SqlDataReader parameter and the field name onto the stack
                        generator.Emit(OpCodes.Ldloc_0);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldstr, prop.Name);

                        // Push the column value onto the stack
                        generator.Emit(OpCodes.Callvirt, getValueMethod);

                        //Convert Nullable Type
                        generator.Emit(OpCodes.Unbox_Any, prop.PropertyType);
                        break;
                    default:
#if CheckColumnExists
                generator.MarkLabel(endIfLabel);
                //}
#endif
                        generator.MarkLabel(endIfLabel2);
                        // Don't set the field value as it's an unsupported type
                        continue;
                }

                generator.Emit(OpCodes.Callvirt, prop.GetSetMethod());
                //}
                generator.MarkLabel(endIfLabel2);
#if CheckColumnExists
                generator.MarkLabel(endIfLabel);
                //}
#endif
            }
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);

            dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }
        /// <summary>
        /// 创建 DataRow
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        private static DynamicBuilder<T> CreateBuilder(DataRow dataRecord)
        {
            DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>();
            DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(T), new Type[] { typeof(DataRow), typeof(Dictionary<string, string>) }, typeof(T), true);
            ILGenerator generator = method.GetILGenerator();
            LocalBuilder result = generator.DeclareLocal(typeof(T));

            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (var prop in props)
            {
#if CheckColumnExists
                //if(dictionary.ContainsKey(prop.Name){
                Label endIfLabel = generator.DefineLabel();
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldstr, prop.Name);
                generator.Emit(OpCodes.Callvirt, hasColumnMethod);
                generator.Emit(OpCodes.Brfalse, endIfLabel);
#endif

                //if(TypeOf.IsDBNull(iDataRecord,prop.Name){
                Label endIfLabel2 = generator.DefineLabel();
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldstr, prop.Name);
                generator.Emit(OpCodes.Call, isDBNullMethodDT);
                generator.Emit(OpCodes.Brtrue, endIfLabel2);


                // Depending on the type of the property, convert the datareader column value to the type
                switch (prop.PropertyType.Name)
                {
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "UInt16":
                    case "UInt32":
                    case "UInt64":
                    case "Byte":
                    case "SByte":
                    case "Boolean":
                    case "String":
                    case "DateTime":
                    case "Single":
                    case "Double":
                    case "Decimal":
                    case "Char":
                        // Load the T instance, SqlDataReader parameter and the field name onto the stack
                        generator.Emit(OpCodes.Ldloc_0);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldstr, prop.Name);

                        // Push the column value onto the stack
                        generator.Emit(OpCodes.Callvirt, getValueMethodDT);

                        //Convert Common Type
                        generator.Emit(OpCodes.Call, typeof(Convert).GetMethod("To" + prop.PropertyType.Name, new Type[] { typeof(object) }));
                        break;
                    case "Nullable`1":
                        // Load the T instance, SqlDataReader parameter and the field name onto the stack
                        generator.Emit(OpCodes.Ldloc_0);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldstr, prop.Name);

                        // Push the column value onto the stack
                        generator.Emit(OpCodes.Callvirt, getValueMethodDT);

                        //Convert Nullable Type
                        generator.Emit(OpCodes.Unbox_Any, prop.PropertyType);
                        break;
                    default:
#if CheckColumnExists
                        generator.MarkLabel(endIfLabel);
                        //}
#endif
                        generator.MarkLabel(endIfLabel2);
                        // Don't set the field value as it's an unsupported type
                        continue;
                }

                generator.Emit(OpCodes.Callvirt, prop.GetSetMethod());
                //}
                generator.MarkLabel(endIfLabel2);
#if CheckColumnExists
                generator.MarkLabel(endIfLabel);
                //}
#endif
            }
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);

            dynamicBuilder.handlerT = (LoadT)method.CreateDelegate(typeof(LoadT));
            return dynamicBuilder;
        }

        /// <summary>
        /// 缓存创建器
        /// </summary>
        private static Dictionary<Type, DynamicBuilder<T>> dictBuilder = new Dictionary<Type, DynamicBuilder<T>>();
        private static Dictionary<Type, DynamicBuilder<T>> dictBuilderDt = new Dictionary<Type, DynamicBuilder<T>>();
        /// <summary>
        /// 把IDataReader转化为List对象列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(IDataReader reader)
        {
            List<T> list = new List<T>();
            DynamicBuilder<T> builder;
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
#if CheckColumnExists
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                if (!columns.ContainsKey(name))
                    columns.Add(name, name);
            }
#endif
            if (dictBuilder.ContainsKey(typeof(T)))
            {
                builder = dictBuilder[typeof(T)];
            }
            else
            {
                builder = DynamicBuilder<T>.CreateBuilder(reader);
                lock (dictBuilder)
                {
                    if (!dictBuilder.ContainsKey(typeof(T)))
                        dictBuilder.Add(typeof(T), builder);
                }
            }

            while (reader.Read())
            {
                list.Add(builder.Build(reader, columns));
            }

            return list;
        }

        /// <summary>
        /// 将DataTable转化成List对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataTable dt)
        {
            List<T> list = new List<T>();
            DynamicBuilder<T> builder;
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
#if CheckColumnExists
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string name = dt.Columns[i].ColumnName;
                if (!columns.ContainsKey(name))
                    columns.Add(name, name);
            }
#endif
            if (dictBuilderDt.ContainsKey(typeof(T)))
            {
                builder = dictBuilderDt[typeof(T)];
            }
            else
            {
                builder = DynamicBuilder<T>.CreateBuilder(dt.NewRow());
                dictBuilderDt.Add(typeof(T), builder);
            }

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(builder.BuildT(dr, columns));
            }

            return list;
        }

        /// <summary>
        /// 将DataRow集合转换成List对象
        /// </summary>
        /// <param name="drc"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataRow[] drc)
        {
            if (drc.Length == 0)
            {
                return new List<T>();
            }
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
#if CheckColumnExists
            for (int i = 0; i < drc[0].Table.Columns.Count; i++)
            {
                string name = drc[0].Table.Columns[i].ColumnName;
                if (!columns.ContainsKey(name))
                    columns.Add(name, name);
            }
#endif
            List<T> list = new List<T>();
            DynamicBuilder<T> builder;
            if (dictBuilderDt.ContainsKey(typeof(T)))
            {
                builder = dictBuilderDt[typeof(T)];
            }
            else
            {
                builder = DynamicBuilder<T>.CreateBuilder(drc[0].Table.NewRow());
                dictBuilderDt.Add(typeof(T), builder);
            }

            foreach (DataRow dr in drc)
            {
                list.Add(builder.BuildT(dr, columns));
            }

            return list;
        }

        /// <summary>
        /// 同类型List合并成一个
        /// </summary>
        /// <param name="listSource"></param>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static IList<T> mergeList(IList<T> listSource, params IList<T>[] lists)
        {
            IList<T> temp = listSource;
            for (int j = 0; j < lists.Length; j++)
            {
                for (int i = 0; i < lists[j].Count; i++)
                {
                    temp.Add(lists[j][i]);
                }
            }
            return temp;
        }
    }
}
