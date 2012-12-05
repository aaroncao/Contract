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
    }

    public class DynamicBuilder<T>
    {
        private DynamicBuilder()
        { }

        //IDataReader
        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
        private delegate T Load(IDataRecord dataRecord);
        private Load handler;
        private T Build(IDataRecord dataRecord)
        {
            return handler(dataRecord);
        }

        /// <summary>
        /// 创建 IDataReader
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        private static DynamicBuilder<T> CreateBuilder(IDataRecord dataRecord)
        {
            DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>();

            DynamicMethod method = new DynamicMethod("DynamicCreate", typeof(T), new Type[] { typeof(IDataRecord) }, typeof(T), true);
            ILGenerator generator = method.GetILGenerator();
            LocalBuilder result = generator.DeclareLocal(typeof(T));

            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(dataRecord.GetName(i), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                Label endIfLabel = generator.DefineLabel();

                if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    generator.Emit(OpCodes.Ldloc, result);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, getValueMethod);
                    var nullable = propertyInfo.PropertyType.Name.Contains("Nullable") ? TypeOf.GetNullableType(dataRecord.GetFieldType(i)) : dataRecord.GetFieldType(i);
                    generator.Emit(OpCodes.Unbox_Any, nullable);
                    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                    generator.MarkLabel(endIfLabel);
                }
            }

            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);

            dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }

        /// <summary>
        /// 缓存创建器
        /// </summary>
        private static Dictionary<Type, DynamicBuilder<T>> dictBuilder = new Dictionary<Type, DynamicBuilder<T>>();

        /// <summary>
        /// 把IDataReader转化为List对象列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(IDataReader reader)
        {
            List<T> list = new List<T>();
            DynamicBuilder<T> builder;

            if (dictBuilder.ContainsKey(typeof(T)))
            {
                builder = dictBuilder[typeof(T)];
            }
            else
            {
                builder = DynamicBuilder<T>.CreateBuilder(reader);
                dictBuilder.Add(typeof(T), builder);
            }

            while (reader.Read())
            {
                list.Add(builder.Build(reader));
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
