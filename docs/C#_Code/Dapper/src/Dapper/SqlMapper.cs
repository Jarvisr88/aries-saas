namespace Dapper
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.ExceptionServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    public static class SqlMapper
    {
        private static readonly ConcurrentDictionary<Identity, CacheInfo> _queryCache = new ConcurrentDictionary<Identity, CacheInfo>();
        private const int COLLECT_PER_ITEMS = 0x3e8;
        private const int COLLECT_HIT_COUNT_MIN = 0;
        private static int collect;
        private static Dictionary<Type, DbType> typeMap;
        private static Dictionary<Type, ITypeHandler> typeHandlers;
        internal const string LinqBinary = "System.Data.Linq.Binary";
        private const string ObsoleteInternalUsageOnly = "This method is for internal use only";
        private static readonly int[] ErrTwoRows = new int[2];
        private static readonly int[] ErrZeroRows = new int[0];
        private static readonly Regex smellsLikeOleDb = new Regex(@"(?<![\p{L}\p{N}@_])[?@:](?![\p{L}\p{N}@_])", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static readonly Regex literalTokens = new Regex(@"(?<![\p{L}\p{N}_])\{=([\p{L}\p{N}_]+)\}", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static readonly Regex pseudoPositional = new Regex(@"\?([\p{L}_][\p{L}\p{N}_]*)\?", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        internal static readonly MethodInfo format = typeof(SqlMapper).GetMethod("Format", BindingFlags.Public | BindingFlags.Static);
        private static readonly Dictionary<TypeCode, MethodInfo> toStrings;
        private static readonly MethodInfo StringReplace;
        private static readonly MethodInfo InvariantCulture;
        private static readonly MethodInfo enumParse;
        private static readonly MethodInfo getItem;
        public static Func<Type, ITypeMap> TypeMapProvider;
        private static readonly Hashtable _typeMaps;
        private static IEqualityComparer<string> connectionStringComparer;
        private const string DataTableTypeNameKey = "dapper:TypeName";
        [ThreadStatic]
        private static StringBuilder perThreadStringBuilderCache;

        public static event EventHandler QueryCachePurged;

        static SqlMapper()
        {
            Type[] source = new Type[12];
            source[0] = typeof(bool);
            source[1] = typeof(sbyte);
            source[2] = typeof(byte);
            source[3] = typeof(ushort);
            source[4] = typeof(short);
            source[5] = typeof(uint);
            source[6] = typeof(int);
            source[7] = typeof(ulong);
            source[8] = typeof(long);
            source[9] = typeof(float);
            source[10] = typeof(double);
            source[11] = typeof(decimal);
            toStrings = source.ToDictionary<Type, TypeCode, MethodInfo>(x => Type.GetTypeCode(x), x => x.GetPublicInstanceMethod("ToString", new Type[] { typeof(IFormatProvider) }));
            Type[] types = new Type[] { typeof(string), typeof(string) };
            StringReplace = typeof(string).GetPublicInstanceMethod("Replace", types);
            InvariantCulture = typeof(CultureInfo).GetProperty("InvariantCulture", BindingFlags.Public | BindingFlags.Static).GetGetMethod();
            Type[] typeArray3 = new Type[] { typeof(Type), typeof(string), typeof(bool) };
            enumParse = typeof(Enum).GetMethod("Parse", typeArray3);
            getItem = (from p in typeof(IDataRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where (p.GetIndexParameters().Length != 0) && (p.GetIndexParameters()[0].ParameterType == typeof(int))
                select p.GetGetMethod()).First<MethodInfo>();
            TypeMapProvider = type => new DefaultTypeMap(type);
            _typeMaps = new Hashtable();
            connectionStringComparer = StringComparer.Ordinal;
            Dictionary<Type, DbType> dictionary = new Dictionary<Type, DbType> {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(TimeSpan)] = DbType.Time,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                [typeof(TimeSpan?)] = DbType.Time,
                [typeof(object)] = DbType.Object
            };
            typeMap = dictionary;
            ResetTypeHandlers(false);
        }

        private static string __ToStringRecycle(this StringBuilder obj)
        {
            if (obj == null)
            {
                return "";
            }
            string str = obj.ToString();
            perThreadStringBuilderCache ??= obj;
            return str;
        }

        public static void AddTypeHandler<T>(TypeHandler<T> handler)
        {
            AddTypeHandlerImpl(typeof(T), handler, true);
        }

        public static void AddTypeHandler(Type type, ITypeHandler handler)
        {
            AddTypeHandlerImpl(type, handler, true);
        }

        public static void AddTypeHandlerImpl(Type type, ITypeHandler handler, bool clone)
        {
            ITypeHandler handler2;
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Type key = null;
            if (type.IsValueType)
            {
                Type underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null)
                {
                    key = type;
                    type = underlyingType;
                }
                else
                {
                    Type[] typeArguments = new Type[] { type };
                    key = typeof(Nullable<>).MakeGenericType(typeArguments);
                }
            }
            Dictionary<Type, ITypeHandler> typeHandlers = SqlMapper.typeHandlers;
            if (!typeHandlers.TryGetValue(type, out handler2) || !ReferenceEquals(handler, handler2))
            {
                Dictionary<Type, ITypeHandler> dictionary2 = clone ? new Dictionary<Type, ITypeHandler>(typeHandlers) : typeHandlers;
                Type[] typeArguments = new Type[] { type };
                object[] parameters = new object[] { handler };
                typeof(TypeHandlerCache).MakeGenericType(typeArguments).GetMethod("SetHandler", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, parameters);
                if (key != null)
                {
                    Type[] typeArray3 = new Type[] { key };
                    object[] objArray2 = new object[] { handler };
                    typeof(TypeHandlerCache).MakeGenericType(typeArray3).GetMethod("SetHandler", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, objArray2);
                }
                if (handler == null)
                {
                    dictionary2.Remove(type);
                    if (key != null)
                    {
                        dictionary2.Remove(key);
                    }
                }
                else
                {
                    dictionary2[type] = handler;
                    if (key != null)
                    {
                        dictionary2[key] = handler;
                    }
                }
                SqlMapper.typeHandlers = dictionary2;
            }
        }

        public static void AddTypeMap(Type type, DbType dbType)
        {
            DbType type2;
            Dictionary<Type, DbType> typeMap = SqlMapper.typeMap;
            if (!typeMap.TryGetValue(type, out type2) || (type2 != dbType))
            {
                Type type3 = type;
                Dictionary<Type, DbType> dictionary1 = new Dictionary<Type, DbType>(typeMap);
                dictionary1[type3] = dbType;
                SqlMapper.typeMap = dictionary1;
            }
        }

        public static List<T> AsList<T>(this IEnumerable<T> source) => 
            ((source == null) || (source is List<T>)) ? ((List<T>) source) : source.ToList<T>();

        public static ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> list, string typeName = null) where T: IDataRecord => 
            new SqlDataRecordListTVPParameter<T>(list, typeName);

        public static ICustomQueryParameter AsTableValuedParameter(this DataTable table, string typeName = null) => 
            new TableValuedParameter(table, typeName);

        private static void CollectCacheGarbage()
        {
            try
            {
                foreach (KeyValuePair<Identity, CacheInfo> pair in _queryCache)
                {
                    if (pair.Value.GetHitCount() <= 0)
                    {
                        CacheInfo info;
                        _queryCache.TryRemove(pair.Key, out info);
                    }
                }
            }
            finally
            {
                Interlocked.Exchange(ref collect, 0);
            }
        }

        public static Action<IDbCommand, object> CreateParamInfoGenerator(Identity identity, bool checkForDuplicates, bool removeUnused) => 
            CreateParamInfoGenerator(identity, checkForDuplicates, removeUnused, GetLiteralTokens(identity.sql));

        internal static Action<IDbCommand, object> CreateParamInfoGenerator(Identity identity, bool checkForDuplicates, bool removeUnused, IList<LiteralToken> literals)
        {
            LocalBuilder _sizeLocal;
            ILGenerator il;
            LocalBuilder GetSizeLocal()
            {
                LocalBuilder builder2 = _sizeLocal;
                if (_sizeLocal == null)
                {
                    LocalBuilder local1 = _sizeLocal;
                    builder2 = _sizeLocal = il.DeclareLocal(typeof(int));
                }
                return builder2;
            }
            LocalBuilder builder;
            Type parametersType = identity.parametersType;
            if (IsValueTuple(parametersType))
            {
                throw new NotSupportedException("ValueTuple should not be used for parameters - the language-level names are not available to use as parameter names, and it adds unnecessary boxing");
            }
            bool flag = false;
            if (removeUnused && (((CommandType) identity.commandType.GetValueOrDefault(CommandType.Text)) == CommandType.Text))
            {
                flag = !smellsLikeOleDb.IsMatch(identity.sql);
            }
            Type[] parameterTypes = new Type[] { typeof(IDbCommand), typeof(object) };
            DynamicMethod method = new DynamicMethod("ParamInfo" + Guid.NewGuid().ToString(), null, parameterTypes, parametersType, true);
            il = method.GetILGenerator();
            bool isValueType = parametersType.IsValueType;
            _sizeLocal = null;
            il.Emit(OpCodes.Ldarg_1);
            if (isValueType)
            {
                builder = il.DeclareLocal(parametersType.MakeByRefType());
                il.Emit(OpCodes.Unbox, parametersType);
            }
            else
            {
                builder = il.DeclareLocal(parametersType);
                il.Emit(OpCodes.Castclass, parametersType);
            }
            il.Emit(OpCodes.Stloc, builder);
            il.Emit(OpCodes.Ldarg_0);
            il.EmitCall(OpCodes.Callvirt, typeof(IDbCommand).GetProperty("Parameters").GetGetMethod(), null);
            PropertyInfo[] properties = parametersType.GetProperties();
            List<PropertyInfo> list = new List<PropertyInfo>(properties.Length);
            int index = 0;
            while (true)
            {
                if (index >= properties.Length)
                {
                    ParameterInfo[] infoArray3;
                    ConstructorInfo[] constructors = parametersType.GetConstructors();
                    IEnumerable<PropertyInfo> parameters = null;
                    if ((constructors.Length == 1) && (list.Count == (infoArray3 = constructors[0].GetParameters()).Length))
                    {
                        bool flag3 = true;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 < list.Count)
                            {
                                if (string.Equals(list[num2].Name, infoArray3[num2].Name, StringComparison.OrdinalIgnoreCase))
                                {
                                    num2++;
                                    continue;
                                }
                                flag3 = false;
                            }
                            if (flag3)
                            {
                                parameters = list;
                            }
                            else
                            {
                                Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                                ParameterInfo[] infoArray4 = infoArray3;
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 >= infoArray4.Length)
                                    {
                                        if (dictionary.Count == list.Count)
                                        {
                                            int[] keys = new int[list.Count];
                                            flag3 = true;
                                            int num4 = 0;
                                            while (true)
                                            {
                                                if (num4 < list.Count)
                                                {
                                                    int num5;
                                                    if (dictionary.TryGetValue(list[num4].Name, out num5))
                                                    {
                                                        keys[num4] = num5;
                                                        num4++;
                                                        continue;
                                                    }
                                                    flag3 = false;
                                                }
                                                if (flag3)
                                                {
                                                    parameters = list.ToArray();
                                                    Array.Sort<int, PropertyInfo>(keys, (PropertyInfo[]) parameters);
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    ParameterInfo info2 = infoArray4[num3];
                                    dictionary[info2.Name] = info2.Position;
                                    num3++;
                                }
                            }
                            break;
                        }
                    }
                    if (parameters == null)
                    {
                        list.Sort(new PropertyInfoByNameComparer());
                        parameters = list;
                    }
                    if (flag)
                    {
                        parameters = FilterParameters(parameters, identity.sql);
                    }
                    OpCode opcode = isValueType ? OpCodes.Call : OpCodes.Callvirt;
                    foreach (PropertyInfo info3 in parameters)
                    {
                        ITypeHandler handler;
                        bool flag4;
                        if (typeof(ICustomQueryParameter).IsAssignableFrom(info3.PropertyType))
                        {
                            il.Emit(OpCodes.Ldloc, builder);
                            il.Emit(opcode, info3.GetGetMethod());
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldstr, info3.Name);
                            il.EmitCall(OpCodes.Callvirt, info3.PropertyType.GetMethod("AddParameter"), null);
                            continue;
                        }
                        DbType type2 = LookupDbType(info3.PropertyType, info3.Name, true, out handler);
                        if (type2 == ~DbType.AnsiString)
                        {
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldstr, info3.Name);
                            il.Emit(OpCodes.Ldloc, builder);
                            il.Emit(opcode, info3.GetGetMethod());
                            if (info3.PropertyType.IsValueType)
                            {
                                il.Emit(OpCodes.Box, info3.PropertyType);
                            }
                            il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("PackListParameters"), null);
                            continue;
                        }
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldarg_0);
                        if (checkForDuplicates)
                        {
                            il.Emit(OpCodes.Ldstr, info3.Name);
                            il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("FindOrAddParameter"), null);
                        }
                        else
                        {
                            il.EmitCall(OpCodes.Callvirt, typeof(IDbCommand).GetMethod("CreateParameter"), null);
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Ldstr, info3.Name);
                            il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("ParameterName").GetSetMethod(), null);
                        }
                        if ((type2 != DbType.Time) && (handler == null))
                        {
                            il.Emit(OpCodes.Dup);
                            if ((type2 != DbType.Object) || !(info3.PropertyType == typeof(object)))
                            {
                                EmitInt32(il, (int) type2);
                            }
                            else
                            {
                                il.Emit(OpCodes.Ldloc, builder);
                                il.Emit(opcode, info3.GetGetMethod());
                                il.Emit(OpCodes.Call, typeof(SqlMapper).GetMethod("GetDbType", BindingFlags.Public | BindingFlags.Static));
                            }
                            il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("DbType").GetSetMethod(), null);
                        }
                        il.Emit(OpCodes.Dup);
                        EmitInt32(il, 1);
                        il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("Direction").GetSetMethod(), null);
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldloc, builder);
                        il.Emit(opcode, info3.GetGetMethod());
                        if (!info3.PropertyType.IsValueType)
                        {
                            flag4 = true;
                        }
                        else
                        {
                            Type propertyType = info3.PropertyType;
                            Type underlyingType = Nullable.GetUnderlyingType(propertyType);
                            bool flag5 = false;
                            if (!(underlyingType ?? propertyType).IsEnum)
                            {
                                flag4 = underlyingType != null;
                            }
                            else if (underlyingType != null)
                            {
                                flag5 = flag4 = true;
                            }
                            else
                            {
                                flag4 = false;
                                switch (Type.GetTypeCode(Enum.GetUnderlyingType(propertyType)))
                                {
                                    case TypeCode.SByte:
                                        propertyType = typeof(sbyte);
                                        break;

                                    case TypeCode.Byte:
                                        propertyType = typeof(byte);
                                        break;

                                    case TypeCode.Int16:
                                        propertyType = typeof(short);
                                        break;

                                    case TypeCode.UInt16:
                                        propertyType = typeof(ushort);
                                        break;

                                    case TypeCode.Int32:
                                        propertyType = typeof(int);
                                        break;

                                    case TypeCode.UInt32:
                                        propertyType = typeof(uint);
                                        break;

                                    case TypeCode.Int64:
                                        propertyType = typeof(long);
                                        break;

                                    case TypeCode.UInt64:
                                        propertyType = typeof(ulong);
                                        break;

                                    default:
                                        break;
                                }
                            }
                            il.Emit(OpCodes.Box, propertyType);
                            if (flag5)
                            {
                                flag4 = false;
                                il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("SanitizeParameterValue"), null);
                            }
                        }
                        if (flag4)
                        {
                            Label? nullable1;
                            il.Emit(OpCodes.Dup);
                            Label label = il.DefineLabel();
                            if ((type2 == DbType.String) || (type2 == DbType.AnsiString))
                            {
                                nullable1 = new Label?(il.DefineLabel());
                            }
                            else
                            {
                                nullable1 = null;
                            }
                            Label? nullable2 = nullable1;
                            il.Emit(OpCodes.Brtrue_S, label);
                            il.Emit(OpCodes.Pop);
                            il.Emit(OpCodes.Ldsfld, typeof(DBNull).GetField("Value"));
                            if ((type2 == DbType.String) || (type2 == DbType.AnsiString))
                            {
                                EmitInt32(il, 0);
                                il.Emit(OpCodes.Stloc, GetSizeLocal());
                            }
                            if (nullable2 != null)
                            {
                                il.Emit(OpCodes.Br_S, nullable2.Value);
                            }
                            il.MarkLabel(label);
                            if (info3.PropertyType == typeof(string))
                            {
                                il.Emit(OpCodes.Dup);
                                il.EmitCall(OpCodes.Callvirt, typeof(string).GetProperty("Length").GetGetMethod(), null);
                                EmitInt32(il, 0xfa0);
                                il.Emit(OpCodes.Cgt);
                                Label label2 = il.DefineLabel();
                                Label label3 = il.DefineLabel();
                                il.Emit(OpCodes.Brtrue_S, label2);
                                EmitInt32(il, 0xfa0);
                                il.Emit(OpCodes.Br_S, label3);
                                il.MarkLabel(label2);
                                EmitInt32(il, -1);
                                il.MarkLabel(label3);
                                il.Emit(OpCodes.Stloc, GetSizeLocal());
                            }
                            if (info3.PropertyType.FullName == "System.Data.Linq.Binary")
                            {
                                il.EmitCall(OpCodes.Callvirt, info3.PropertyType.GetMethod("ToArray", BindingFlags.Public | BindingFlags.Instance), null);
                            }
                            if (nullable2 != null)
                            {
                                il.MarkLabel(nullable2.Value);
                            }
                        }
                        if (handler == null)
                        {
                            il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("Value").GetSetMethod(), null);
                        }
                        else
                        {
                            Type[] typeArguments = new Type[] { info3.PropertyType };
                            il.Emit(OpCodes.Call, typeof(TypeHandlerCache).MakeGenericType(typeArguments).GetMethod("SetValue"));
                        }
                        if (info3.PropertyType == typeof(string))
                        {
                            Label label = il.DefineLabel();
                            LocalBuilder sizeLocal = GetSizeLocal();
                            il.Emit(OpCodes.Ldloc, sizeLocal);
                            il.Emit(OpCodes.Brfalse_S, label);
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Ldloc, sizeLocal);
                            il.EmitCall(OpCodes.Callvirt, typeof(IDbDataParameter).GetProperty("Size").GetSetMethod(), null);
                            il.MarkLabel(label);
                        }
                        if (checkForDuplicates)
                        {
                            il.Emit(OpCodes.Pop);
                        }
                        else
                        {
                            il.EmitCall(OpCodes.Callvirt, typeof(IList).GetMethod("Add"), null);
                            il.Emit(OpCodes.Pop);
                        }
                    }
                    il.Emit(OpCodes.Pop);
                    if ((literals.Count == 0) || (list == null))
                    {
                        break;
                    }
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_0);
                    PropertyInfo property = typeof(IDbCommand).GetProperty("CommandText");
                    il.EmitCall(OpCodes.Callvirt, property.GetGetMethod(), null);
                    Dictionary<Type, LocalBuilder> dictionary2 = null;
                    LocalBuilder builder3 = null;
                    using (IEnumerator<LiteralToken> enumerator2 = literals.GetEnumerator())
                    {
                        LiteralToken current;
                        PropertyInfo info5;
                        PropertyInfo info6;
                        PropertyInfo info7;
                        goto TR_0026;
                    TR_001A:
                        info7 = info5 ?? info6;
                        if (info7 != null)
                        {
                            il.Emit(OpCodes.Ldstr, current.Token);
                            il.Emit(OpCodes.Ldloc, builder);
                            il.EmitCall(opcode, info7.GetGetMethod(), null);
                            Type propertyType = info7.PropertyType;
                            TypeCode typeCode = Type.GetTypeCode(propertyType);
                            if (typeCode == TypeCode.Boolean)
                            {
                                Label label = il.DefineLabel();
                                Label label6 = il.DefineLabel();
                                il.Emit(OpCodes.Brtrue_S, label);
                                il.Emit(OpCodes.Ldstr, "0");
                                il.Emit(OpCodes.Br_S, label6);
                                il.MarkLabel(label);
                                il.Emit(OpCodes.Ldstr, "1");
                                il.MarkLabel(label6);
                            }
                            else if ((typeCode - 5) > TypeCode.UInt32)
                            {
                                if (propertyType.IsValueType)
                                {
                                    il.Emit(OpCodes.Box, propertyType);
                                }
                                il.EmitCall(OpCodes.Call, format, null);
                            }
                            else
                            {
                                MethodInfo toString = GetToString(typeCode);
                                if ((builder3 == null) || (builder3.LocalType != propertyType))
                                {
                                    if (dictionary2 == null)
                                    {
                                        dictionary2 = new Dictionary<Type, LocalBuilder>();
                                        builder3 = null;
                                    }
                                    else if (!dictionary2.TryGetValue(propertyType, out builder3))
                                    {
                                        builder3 = null;
                                    }
                                    if (builder3 == null)
                                    {
                                        builder3 = il.DeclareLocal(propertyType);
                                        dictionary2.Add(propertyType, builder3);
                                    }
                                }
                                il.Emit(OpCodes.Stloc, builder3);
                                il.Emit(OpCodes.Ldloca, builder3);
                                il.EmitCall(OpCodes.Call, InvariantCulture, null);
                                il.EmitCall(OpCodes.Call, toString, null);
                            }
                            il.EmitCall(OpCodes.Callvirt, StringReplace, null);
                        }
                    TR_0026:
                        while (true)
                        {
                            if (enumerator2.MoveNext())
                            {
                                current = enumerator2.Current;
                                info5 = null;
                                info6 = null;
                                string member = current.Member;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    string name = list[i].Name;
                                    if (string.Equals(name, member, StringComparison.OrdinalIgnoreCase))
                                    {
                                        info6 = list[i];
                                        if (string.Equals(name, member, StringComparison.Ordinal))
                                        {
                                            info5 = info6;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                il.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);
                                break;
                            }
                            break;
                        }
                        goto TR_001A;
                    }
                }
                PropertyInfo item = properties[index];
                if (item.GetIndexParameters().Length == 0)
                {
                    list.Add(item);
                }
                index++;
            }
            il.Emit(OpCodes.Ret);
            return (Action<IDbCommand, object>) method.CreateDelegate(typeof(Action<IDbCommand, object>));
        }

        private static void EmitInt32(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;

                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;

                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;

                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;

                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;

                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;

                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;

                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;

                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;

                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            if ((value >= -128) && (value <= 0x7f))
            {
                il.Emit(OpCodes.Ldc_I4_S, (sbyte) value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }

        public static int Execute(this IDbConnection cnn, CommandDefinition command) => 
            cnn.ExecuteImpl(ref command);

        public static int Execute(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken);
            return cnn.ExecuteImpl(ref command);
        }

        public static Task<int> ExecuteAsync(this IDbConnection cnn, CommandDefinition command)
        {
            object parameters = command.Parameters;
            IEnumerable multiExec = GetMultiExec(parameters);
            return ((multiExec == null) ? ExecuteImplAsync(cnn, command, parameters) : ExecuteMultiImplAsync(cnn, command, multiExec));
        }

        public static Task<int> ExecuteAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.ExecuteAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        private static int ExecuteCommand(IDbConnection cnn, ref CommandDefinition command, Action<IDbCommand, object> paramReader)
        {
            IDbCommand command2 = null;
            int num2;
            bool flag = cnn.State == ConnectionState.Closed;
            try
            {
                command2 = command.SetupCommand(cnn, paramReader);
                if (flag)
                {
                    cnn.Open();
                }
                int num = command2.ExecuteNonQuery();
                command.OnCompleted();
                num2 = num;
            }
            finally
            {
                if (flag)
                {
                    cnn.Close();
                }
                if (command2 != null)
                {
                    command2.Dispose();
                }
            }
            return num2;
        }

        private static int ExecuteImpl(this IDbConnection cnn, ref CommandDefinition command)
        {
            object parameters = command.Parameters;
            IEnumerable multiExec = GetMultiExec(parameters);
            CacheInfo info = null;
            if (multiExec == null)
            {
                if (parameters != null)
                {
                    info = GetCacheInfo(new Identity(command.CommandText, command.CommandType, cnn, null, parameters.GetType()), parameters, command.AddToCache);
                }
                return ExecuteCommand(cnn, ref command, (parameters == null) ? null : info.ParamReader);
            }
            if ((command.Flags & CommandFlags.Pipelined) != CommandFlags.None)
            {
                return ExecuteMultiImplAsync(cnn, command, multiExec).Result;
            }
            bool flag = true;
            int num = 0;
            bool flag2 = cnn.State == ConnectionState.Closed;
            try
            {
                if (flag2)
                {
                    cnn.Open();
                }
                using (IDbCommand command2 = command.SetupCommand(cnn, null))
                {
                    string commandText = null;
                    foreach (object obj3 in multiExec)
                    {
                        if (!flag)
                        {
                            command2.CommandText = commandText;
                            command2.Parameters.Clear();
                        }
                        else
                        {
                            commandText = command2.CommandText;
                            flag = false;
                            info = GetCacheInfo(new Identity(command.CommandText, new CommandType?(command2.CommandType), cnn, null, obj3.GetType()), obj3, command.AddToCache);
                        }
                        info.ParamReader(command2, obj3);
                        num += command2.ExecuteNonQuery();
                    }
                }
                command.OnCompleted();
            }
            finally
            {
                if (flag2)
                {
                    cnn.Close();
                }
            }
            return num;
        }

        [AsyncStateMachine(typeof(<ExecuteImplAsync>d__39))]
        private static Task<int> ExecuteImplAsync(IDbConnection cnn, CommandDefinition command, object param)
        {
            <ExecuteImplAsync>d__39 d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.param = param;
            d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteImplAsync>d__39>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ExecuteMultiImplAsync>d__38))]
        private static Task<int> ExecuteMultiImplAsync(IDbConnection cnn, CommandDefinition command, IEnumerable multiExec)
        {
            <ExecuteMultiImplAsync>d__38 d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.multiExec = multiExec;
            d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteMultiImplAsync>d__38>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static IDataReader ExecuteReader(this IDbConnection cnn, CommandDefinition command)
        {
            IDbCommand command2;
            return WrappedReader.Create(command2, ExecuteReaderImpl(cnn, ref command, CommandBehavior.Default, out command2));
        }

        public static IDataReader ExecuteReader(this IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
        {
            IDbCommand command2;
            return WrappedReader.Create(command2, ExecuteReaderImpl(cnn, ref command, commandBehavior, out command2));
        }

        public static IDataReader ExecuteReader(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            IDbCommand command;
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition definition = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken);
            return WrappedReader.Create(command, ExecuteReaderImpl(cnn, ref definition, CommandBehavior.Default, out command));
        }

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, CommandDefinition command) => 
            ExecuteWrappedReaderImplAsync(cnn, command, CommandBehavior.Default);

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command) => 
            ExecuteWrappedReaderImplAsync(cnn, command, CommandBehavior.Default).CastResult<DbDataReader, IDataReader>();

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior) => 
            ExecuteWrappedReaderImplAsync(cnn, command, commandBehavior);

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior) => 
            ExecuteWrappedReaderImplAsync(cnn, command, commandBehavior).CastResult<DbDataReader, IDataReader>();

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return ExecuteWrappedReaderImplAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken), CommandBehavior.Default);
        }

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return ExecuteWrappedReaderImplAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken), CommandBehavior.Default).CastResult<DbDataReader, IDataReader>();
        }

        private static IDataReader ExecuteReaderImpl(IDbConnection cnn, ref CommandDefinition command, CommandBehavior commandBehavior, out IDbCommand cmd)
        {
            IDataReader reader2;
            Action<IDbCommand, object> parameterReader = GetParameterReader(cnn, ref command);
            cmd = null;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            bool flag2 = true;
            try
            {
                cmd = command.SetupCommand(cnn, parameterReader);
                if (wasClosed)
                {
                    cnn.Open();
                }
                wasClosed = false;
                flag2 = false;
                reader2 = ExecuteReaderWithFlagsFallback(cmd, wasClosed, commandBehavior);
            }
            finally
            {
                if (wasClosed)
                {
                    cnn.Close();
                }
                if ((cmd != null) & flag2)
                {
                    cmd.Dispose();
                }
            }
            return reader2;
        }

        [IteratorStateMachine(typeof(<ExecuteReaderSync>d__55))]
        private static IEnumerable<T> ExecuteReaderSync<T>(IDataReader reader, Func<IDataReader, object> func, object parameters)
        {
            <ExecuteReaderSync>d__55<T> d__1 = new <ExecuteReaderSync>d__55<T>(-2);
            d__1.<>3__reader = reader;
            d__1.<>3__func = func;
            d__1.<>3__parameters = parameters;
            return d__1;
        }

        private static IDataReader ExecuteReaderWithFlagsFallback(IDbCommand cmd, bool wasClosed, CommandBehavior behavior)
        {
            try
            {
                return cmd.ExecuteReader(GetBehavior(wasClosed, behavior));
            }
            catch (ArgumentException exception)
            {
                if (!Settings.DisableCommandBehaviorOptimizations(behavior, exception))
                {
                    throw;
                }
                return cmd.ExecuteReader(GetBehavior(wasClosed, behavior));
            }
        }

        private static Task<DbDataReader> ExecuteReaderWithFlagsFallbackAsync(DbCommand cmd, bool wasClosed, CommandBehavior behavior, CancellationToken cancellationToken)
        {
            Task<DbDataReader> task = cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken);
            return (((task.Status != TaskStatus.Faulted) || !Settings.DisableCommandBehaviorOptimizations(behavior, task.Exception.InnerException)) ? task : cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken));
        }

        public static object ExecuteScalar(this IDbConnection cnn, CommandDefinition command) => 
            ExecuteScalarImpl<object>(cnn, ref command);

        public static T ExecuteScalar<T>(this IDbConnection cnn, CommandDefinition command) => 
            ExecuteScalarImpl<T>(cnn, ref command);

        public static object ExecuteScalar(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken);
            return ExecuteScalarImpl<object>(cnn, ref command);
        }

        public static T ExecuteScalar<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken);
            return ExecuteScalarImpl<T>(cnn, ref command);
        }

        public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, CommandDefinition command) => 
            ExecuteScalarImplAsync<object>(cnn, command);

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            ExecuteScalarImplAsync<T>(cnn, command);

        public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return ExecuteScalarImplAsync<object>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        private static T ExecuteScalarImpl<T>(IDbConnection cnn, ref CommandDefinition command)
        {
            Action<IDbCommand, object> paramReader = null;
            object obj3;
            object parameters = command.Parameters;
            if (parameters != null)
            {
                paramReader = GetCacheInfo(new Identity(command.CommandText, command.CommandType, cnn, null, parameters.GetType()), command.Parameters, command.AddToCache).ParamReader;
            }
            IDbCommand command2 = null;
            bool flag = cnn.State == ConnectionState.Closed;
            try
            {
                command2 = command.SetupCommand(cnn, paramReader);
                if (flag)
                {
                    cnn.Open();
                }
                obj3 = command2.ExecuteScalar();
                command.OnCompleted();
            }
            finally
            {
                if (flag)
                {
                    cnn.Close();
                }
                if (command2 != null)
                {
                    command2.Dispose();
                }
            }
            return Parse<T>(obj3);
        }

        [AsyncStateMachine(typeof(<ExecuteScalarImplAsync>d__69))]
        private static Task<T> ExecuteScalarImplAsync<T>(IDbConnection cnn, CommandDefinition command)
        {
            <ExecuteScalarImplAsync>d__69<T> d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteScalarImplAsync>d__69<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ExecuteWrappedReaderImplAsync>d__64))]
        private static Task<DbDataReader> ExecuteWrappedReaderImplAsync(IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
        {
            <ExecuteWrappedReaderImplAsync>d__64 d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.commandBehavior = commandBehavior;
            d__.<>t__builder = AsyncTaskMethodBuilder<DbDataReader>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteWrappedReaderImplAsync>d__64>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static IEnumerable<PropertyInfo> FilterParameters(IEnumerable<PropertyInfo> parameters, string sql)
        {
            List<PropertyInfo> list = new List<PropertyInfo>(0x10);
            foreach (PropertyInfo info in parameters)
            {
                if (Regex.IsMatch(sql, "[?@:]" + info.Name + @"([^\p{L}\p{N}_]+|$)", RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    list.Add(info);
                }
            }
            return list;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method is for internal use only", true)]
        public static IDbDataParameter FindOrAddParameter(IDataParameterCollection parameters, IDbCommand command, string name)
        {
            IDbDataParameter parameter;
            if (parameters.Contains(name))
            {
                parameter = (IDbDataParameter) parameters[name];
            }
            else
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameters.Add(parameter);
            }
            return parameter;
        }

        private static void FlexibleConvertBoxedFromHeadOfStack(ILGenerator il, Type from, Type to, Type via)
        {
            Type type1 = via;
            if (via == null)
            {
                Type local1 = via;
                type1 = to;
            }
            if (from == type1)
            {
                il.Emit(OpCodes.Unbox_Any, to);
            }
            else
            {
                MethodInfo @operator = GetOperator(from, to);
                if (@operator != null)
                {
                    il.Emit(OpCodes.Unbox_Any, from);
                    il.Emit(OpCodes.Call, @operator);
                }
                else
                {
                    bool flag = false;
                    OpCode opcode = new OpCode();
                    TypeCode typeCode = Type.GetTypeCode(from);
                    if ((typeCode == TypeCode.Boolean) || ((typeCode - 5) <= TypeCode.Int32))
                    {
                        flag = true;
                        switch (Type.GetTypeCode(via ?? to))
                        {
                            case TypeCode.Boolean:
                            case TypeCode.Int32:
                                opcode = OpCodes.Conv_Ovf_I4;
                                break;

                            case TypeCode.SByte:
                                opcode = OpCodes.Conv_Ovf_I1;
                                break;

                            case TypeCode.Byte:
                                opcode = OpCodes.Conv_Ovf_I1_Un;
                                break;

                            case TypeCode.Int16:
                                opcode = OpCodes.Conv_Ovf_I2;
                                break;

                            case TypeCode.UInt16:
                                opcode = OpCodes.Conv_Ovf_I2_Un;
                                break;

                            case TypeCode.UInt32:
                                opcode = OpCodes.Conv_Ovf_I4_Un;
                                break;

                            case TypeCode.Int64:
                                opcode = OpCodes.Conv_Ovf_I8;
                                break;

                            case TypeCode.UInt64:
                                opcode = OpCodes.Conv_Ovf_I8_Un;
                                break;

                            case TypeCode.Single:
                                opcode = OpCodes.Conv_R4;
                                break;

                            case TypeCode.Double:
                                opcode = OpCodes.Conv_R8;
                                break;

                            default:
                                flag = false;
                                break;
                        }
                    }
                    if (flag)
                    {
                        il.Emit(OpCodes.Unbox_Any, from);
                        il.Emit(opcode);
                        if (to == typeof(bool))
                        {
                            il.Emit(OpCodes.Ldc_I4_0);
                            il.Emit(OpCodes.Ceq);
                            il.Emit(OpCodes.Ldc_I4_0);
                            il.Emit(OpCodes.Ceq);
                        }
                    }
                    else
                    {
                        Type cls = via;
                        if (via == null)
                        {
                            Type local3 = via;
                            cls = to;
                        }
                        il.Emit(OpCodes.Ldtoken, cls);
                        il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
                        il.EmitCall(OpCodes.Call, InvariantCulture, null);
                        Type[] types = new Type[] { typeof(object), typeof(Type), typeof(IFormatProvider) };
                        il.EmitCall(OpCodes.Call, typeof(Convert).GetMethod("ChangeType", types), null);
                        il.Emit(OpCodes.Unbox_Any, to);
                    }
                }
            }
        }

        [Obsolete("This method is for internal use only")]
        public static string Format(object value)
        {
            if (value == null)
            {
                return "null";
            }
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.DBNull:
                    return "null";

                case TypeCode.Boolean:
                    return (((bool) value) ? "1" : "0");

                case TypeCode.SByte:
                    return ((sbyte) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Byte:
                    return ((byte) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Int16:
                    return ((short) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.UInt16:
                    return ((ushort) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Int32:
                    return ((int) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.UInt32:
                    return ((uint) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Int64:
                    return ((long) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.UInt64:
                    return ((ulong) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Single:
                    return ((float) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Double:
                    return ((double) value).ToString(CultureInfo.InvariantCulture);

                case TypeCode.Decimal:
                    return ((decimal) value).ToString(CultureInfo.InvariantCulture);
            }
            IEnumerable multiExec = GetMultiExec(value);
            if (multiExec == null)
            {
                throw new NotSupportedException("The type '" + value.GetType().Name + "' is not supported for SQL literals.");
            }
            StringBuilder builder = null;
            bool flag = true;
            foreach (object obj2 in multiExec)
            {
                if (!flag)
                {
                    builder.Append(',');
                }
                else
                {
                    builder = GetStringBuilder().Append('(');
                    flag = false;
                }
                builder.Append(Format(obj2));
            }
            return (!flag ? builder.Append(')').__ToStringRecycle() : "(select null where 1=0)");
        }

        private static void GenerateDeserializerFromMap(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing, ILGenerator il)
        {
            LocalBuilder local = il.DeclareLocal(typeof(int));
            LocalBuilder builder2 = il.DeclareLocal(type);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc, local);
            string[] names = (from i in Enumerable.Range(startBound, length) select reader.GetName(i)).ToArray<string>();
            ITypeMap typeMap = GetTypeMap(type);
            int num = startBound;
            ConstructorInfo specializedConstructor = null;
            bool flag = false;
            Dictionary<Type, LocalBuilder> locals = null;
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Ldloca, builder2);
                il.Emit(OpCodes.Initobj, type);
            }
            else
            {
                Type[] types = new Type[length];
                int num2 = startBound;
                while (true)
                {
                    if (num2 >= (startBound + length))
                    {
                        ConstructorInfo con = typeMap.FindExplicitConstructor();
                        if (con != null)
                        {
                            ParameterInfo[] parameters = con.GetParameters();
                            int index = 0;
                            while (true)
                            {
                                if (index >= parameters.Length)
                                {
                                    il.Emit(OpCodes.Newobj, con);
                                    il.Emit(OpCodes.Stloc, builder2);
                                    if (typeof(ISupportInitialize).IsAssignableFrom(type))
                                    {
                                        il.Emit(OpCodes.Ldloc, builder2);
                                        il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("BeginInit"), null);
                                    }
                                    break;
                                }
                                ParameterInfo info2 = parameters[index];
                                if (!info2.ParameterType.IsValueType)
                                {
                                    il.Emit(OpCodes.Ldnull);
                                }
                                else
                                {
                                    GetTempLocal(il, ref locals, info2.ParameterType, true);
                                }
                                index++;
                            }
                        }
                        else
                        {
                            ConstructorInfo info3 = typeMap.FindConstructor(names, types);
                            if (info3 == null)
                            {
                                string str = "(" + string.Join(", ", types.Select<Type, string>((t, i) => (t.FullName + " " + names[i])).ToArray<string>()) + ")";
                                string[] textArray1 = new string[] { "A parameterless default constructor or one matching signature ", str, " is required for ", type.FullName, " materialization" };
                                throw new InvalidOperationException(string.Concat(textArray1));
                            }
                            if (info3.GetParameters().Length != 0)
                            {
                                specializedConstructor = info3;
                            }
                            else
                            {
                                il.Emit(OpCodes.Newobj, info3);
                                il.Emit(OpCodes.Stloc, builder2);
                                if (typeof(ISupportInitialize).IsAssignableFrom(type))
                                {
                                    il.Emit(OpCodes.Ldloc, builder2);
                                    il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("BeginInit"), null);
                                }
                            }
                        }
                        break;
                    }
                    types[num2 - startBound] = reader.GetFieldType(num2);
                    num2++;
                }
            }
            il.BeginExceptionBlock();
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Ldloca, builder2);
            }
            else if (specializedConstructor == null)
            {
                il.Emit(OpCodes.Ldloc, builder2);
            }
            bool flag2 = true;
            Label label = il.DefineLabel();
            LocalBuilder stringEnumLocal = null;
            LocalBuilder valueCopyLocal = il.DeclareLocal(typeof(object));
            bool applyNullValues = Settings.ApplyNullValues;
            foreach (IMemberMap map in ((specializedConstructor != null) ? (from n in names select typeMap.GetConstructorParameter(specializedConstructor, n)) : (from n in names select typeMap.GetMember(n))).ToList<IMemberMap>())
            {
                if (map != null)
                {
                    Label label3;
                    if (specializedConstructor == null)
                    {
                        il.Emit(OpCodes.Dup);
                    }
                    Label label2 = il.DefineLabel();
                    Type memberType = map.MemberType;
                    EmitInt32(il, num);
                    il.Emit(OpCodes.Stloc, local);
                    LoadReaderValueOrBranchToDBNullLabel(il, num, ref stringEnumLocal, valueCopyLocal, reader.GetFieldType(num), memberType, out label3);
                    if (specializedConstructor == null)
                    {
                        if (map.Property != null)
                        {
                            il.Emit(type.IsValueType ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(map.Property, type));
                        }
                        else
                        {
                            il.Emit(OpCodes.Stfld, map.Field);
                        }
                    }
                    il.Emit(OpCodes.Br_S, label2);
                    il.MarkLabel(label3);
                    if (specializedConstructor != null)
                    {
                        il.Emit(OpCodes.Pop);
                        LoadDefaultValue(il, map.MemberType);
                    }
                    else if (!applyNullValues || (memberType.IsValueType && (Nullable.GetUnderlyingType(memberType) == null)))
                    {
                        il.Emit(OpCodes.Pop);
                        il.Emit(OpCodes.Pop);
                    }
                    else
                    {
                        il.Emit(OpCodes.Pop);
                        if (memberType.IsValueType)
                        {
                            GetTempLocal(il, ref locals, memberType, true);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ldnull);
                        }
                        if (map.Property != null)
                        {
                            il.Emit(type.IsValueType ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(map.Property, type));
                        }
                        else
                        {
                            il.Emit(OpCodes.Stfld, map.Field);
                        }
                    }
                    if (flag2 & returnNullIfFirstMissing)
                    {
                        il.Emit(OpCodes.Pop);
                        il.Emit(OpCodes.Ldnull);
                        il.Emit(OpCodes.Stloc, builder2);
                        il.Emit(OpCodes.Br, label);
                    }
                    il.MarkLabel(label2);
                }
                flag2 = false;
                num++;
            }
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Pop);
            }
            else
            {
                if (specializedConstructor != null)
                {
                    il.Emit(OpCodes.Newobj, specializedConstructor);
                }
                il.Emit(OpCodes.Stloc, builder2);
                if (flag)
                {
                    il.Emit(OpCodes.Ldloc, builder2);
                    il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("EndInit"), null);
                }
            }
            il.MarkLabel(label);
            il.BeginCatchBlock(typeof(Exception));
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc, valueCopyLocal);
            il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("ThrowDataException"), null);
            il.EndExceptionBlock();
            il.Emit(OpCodes.Ldloc, builder2);
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
            il.Emit(OpCodes.Ret);
        }

        private static Func<IDataReader, object>[] GenerateDeserializers(Identity identity, string splitOn, IDataReader reader)
        {
            List<Func<IDataReader, object>> list = new List<Func<IDataReader, object>>();
            char[] separator = new char[] { ',' };
            Func<string, string> selector = <>c.<>9__160_0;
            if (<>c.<>9__160_0 == null)
            {
                Func<string, string> local1 = <>c.<>9__160_0;
                selector = <>c.<>9__160_0 = s => s.Trim();
            }
            string[] strArray = splitOn.Split(separator).Select<string, string>(selector).ToArray<string>();
            bool flag = strArray.Length > 1;
            int typeCount = identity.TypeCount;
            if (identity.GetType(0) == typeof(object))
            {
                bool flag2 = true;
                int startIdx = 0;
                int index = 0;
                string str = strArray[index];
                for (int i = 0; i < typeCount; i++)
                {
                    Type type = identity.GetType(i);
                    if (type == typeof(DontMap))
                    {
                        break;
                    }
                    int num5 = GetNextSplitDynamic(startIdx, str, reader);
                    if (flag && (index < (strArray.Length - 1)))
                    {
                        str = strArray[++index];
                    }
                    list.Add(GetDeserializer(type, reader, startIdx, num5 - startIdx, !flag2));
                    startIdx = num5;
                    flag2 = false;
                }
            }
            else
            {
                int fieldCount = reader.FieldCount;
                int index = strArray.Length - 1;
                string str2 = strArray[index];
                int num8 = typeCount - 1;
                while (true)
                {
                    if (num8 < 0)
                    {
                        list.Reverse();
                        break;
                    }
                    Type type = identity.GetType(num8);
                    if (type != typeof(DontMap))
                    {
                        int startBound = 0;
                        if (num8 > 0)
                        {
                            startBound = GetNextSplit(fieldCount, str2, reader);
                            if (flag && (index > 0))
                            {
                                str2 = strArray[--index];
                            }
                        }
                        list.Add(GetDeserializer(type, reader, startBound, fieldCount - startBound, num8 > 0));
                        fieldCount = startBound;
                    }
                    num8--;
                }
            }
            return list.ToArray();
        }

        private static Func<IDataReader, TReturn> GenerateMapper<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<IDataReader, object> deserializer, Func<IDataReader, object>[] otherDeserializers, object map)
        {
            switch (otherDeserializers.Length)
            {
                case 1:
                    return r => ((System.Func<TFirst, TSecond, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r));

                case 2:
                    return r => ((Func<TFirst, TSecond, TThird, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r), (TThird) otherDeserializers[1](r));

                case 3:
                    return r => ((Func<TFirst, TSecond, TThird, TFourth, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r), (TThird) otherDeserializers[1](r), (TFourth) otherDeserializers[2](r));

                case 4:
                    return r => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r), (TThird) otherDeserializers[1](r), (TFourth) otherDeserializers[2](r), (TFifth) otherDeserializers[3](r));

                case 5:
                    return r => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r), (TThird) otherDeserializers[1](r), (TFourth) otherDeserializers[2](r), (TFifth) otherDeserializers[3](r), (TSixth) otherDeserializers[4](r));

                case 6:
                    return r => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>) map)((TFirst) deserializer(r), otherDeserializers[0](r), (TThird) otherDeserializers[1](r), (TFourth) otherDeserializers[2](r), (TFifth) otherDeserializers[3](r), (TSixth) otherDeserializers[4](r), (TSeventh) otherDeserializers[5](r));
            }
            throw new NotSupportedException();
        }

        private static Func<IDataReader, TReturn> GenerateMapper<TReturn>(int length, Func<IDataReader, object> deserializer, Func<IDataReader, object>[] otherDeserializers, Func<object[], TReturn> map) => 
            delegate (IDataReader r) {
                object[] arg = new object[] { deserializer(r) };
                for (int i = 1; i < length; i++)
                {
                    arg[i] = otherDeserializers[i - 1](r);
                }
                return map(arg);
            };

        private static void GenerateValueTupleDeserializer(Type valueTupleType, IDataReader reader, int startBound, int length, ILGenerator il)
        {
            Type fieldType = valueTupleType;
            List<ConstructorInfo> list = new List<ConstructorInfo>();
            List<Type> list2 = new List<Type>();
            while (true)
            {
                int num = int.Parse(fieldType.Name.Substring("ValueTuple`".Length), CultureInfo.InvariantCulture);
                Type[] types = new Type[num];
                FieldInfo info = null;
                FieldInfo[] fields = fieldType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                int index = 0;
                while (true)
                {
                    if (index >= fields.Length)
                    {
                        int num2 = types.Length;
                        if (info != null)
                        {
                            num2--;
                        }
                        int num5 = 0;
                        while (true)
                        {
                            if (num5 >= num2)
                            {
                                if (info != null)
                                {
                                    types[types.Length - 1] = info.FieldType;
                                }
                                list.Add(fieldType.GetConstructor(types));
                                if (info != null)
                                {
                                    fieldType = info.FieldType;
                                    if (IsValueTuple(fieldType))
                                    {
                                        break;
                                    }
                                    throw new InvalidOperationException("The Rest field of a ValueTuple must contain a nested ValueTuple of arity 1 or greater.");
                                }
                                LocalBuilder stringEnumLocal = null;
                                for (int i = 0; i < list2.Count; i++)
                                {
                                    Type type2 = list2[i];
                                    if (i >= length)
                                    {
                                        LoadDefaultValue(il, type2);
                                    }
                                    else
                                    {
                                        Label label;
                                        LoadReaderValueOrBranchToDBNullLabel(il, startBound + i, ref stringEnumLocal, null, reader.GetFieldType(startBound + i), type2, out label);
                                        Label label2 = il.DefineLabel();
                                        il.Emit(OpCodes.Br_S, label2);
                                        il.MarkLabel(label);
                                        il.Emit(OpCodes.Pop);
                                        LoadDefaultValue(il, type2);
                                        il.MarkLabel(label2);
                                    }
                                }
                                for (int j = list.Count - 1; j >= 0; j--)
                                {
                                    il.Emit(OpCodes.Newobj, list[j]);
                                }
                                il.Emit(OpCodes.Box, valueTupleType);
                                il.Emit(OpCodes.Ret);
                                return;
                            }
                            list2.Add(types[num5]);
                            num5++;
                        }
                        break;
                    }
                    FieldInfo info2 = fields[index];
                    if (info2.Name == "Rest")
                    {
                        info = info2;
                    }
                    else if (info2.Name.StartsWith("Item", StringComparison.Ordinal))
                    {
                        types[int.Parse(info2.Name.Substring("Item".Length), CultureInfo.InvariantCulture) - 1] = info2.FieldType;
                    }
                    index++;
                }
            }
        }

        private static CommandBehavior GetBehavior(bool close, CommandBehavior @default) => 
            (close ? (@default | CommandBehavior.CloseConnection) : @default) & Settings.AllowedCommandBehaviors;

        public static IEnumerable<Tuple<string, string, int>> GetCachedSQL(int ignoreHitCountAbove = 0x7fffffff)
        {
            Func<KeyValuePair<Identity, CacheInfo>, Tuple<string, string, int>> selector = <>c.<>9__87_0;
            if (<>c.<>9__87_0 == null)
            {
                Func<KeyValuePair<Identity, CacheInfo>, Tuple<string, string, int>> local1 = <>c.<>9__87_0;
                selector = <>c.<>9__87_0 = pair => Tuple.Create<string, string, int>(pair.Key.connectionString, pair.Key.sql, pair.Value.GetHitCount());
            }
            IEnumerable<Tuple<string, string, int>> enumerable = _queryCache.Select<KeyValuePair<Identity, CacheInfo>, Tuple<string, string, int>>(selector);
            return ((ignoreHitCountAbove < 0x7fffffff) ? (from tuple in enumerable
                where tuple.Item3 <= ignoreHitCountAbove
                select tuple) : enumerable);
        }

        public static int GetCachedSQLCount() => 
            _queryCache.Count;

        private static CacheInfo GetCacheInfo(Identity identity, object exampleParameters, bool addToCache)
        {
            CacheInfo info;
            Action<IDbCommand, object> action;
            if (TryGetQueryCache(identity, out info))
            {
                return info;
            }
            else
            {
                if (GetMultiExec(exampleParameters) != null)
                {
                    throw new InvalidOperationException("An enumerable sequence of parameters (arrays, lists, etc) is not allowed in this context");
                }
                info = new CacheInfo();
                if (identity.parametersType == null)
                {
                    goto TR_0003;
                }
                else
                {
                    if (exampleParameters is IDynamicParameters)
                    {
                        action = delegate (IDbCommand cmd, object obj) {
                            ((IDynamicParameters) obj).AddParameters(cmd, identity);
                        };
                    }
                    else if (exampleParameters is IEnumerable<KeyValuePair<string, object>>)
                    {
                        action = delegate (IDbCommand cmd, object obj) {
                            ((IDynamicParameters) new DynamicParameters(obj)).AddParameters(cmd, identity);
                        };
                    }
                    else
                    {
                        IList<LiteralToken> literalTokens = GetLiteralTokens(identity.sql);
                        action = CreateParamInfoGenerator(identity, false, true, literalTokens);
                    }
                    if (identity.commandType != null)
                    {
                        CommandType? commandType = identity.commandType;
                        CommandType text = CommandType.Text;
                        if (!((((CommandType) commandType.GetValueOrDefault()) == text) & (commandType != null)))
                        {
                            goto TR_0004;
                        }
                    }
                    if (ShouldPassByPosition(identity.sql))
                    {
                        Action<IDbCommand, object> tail = action;
                        action = delegate (IDbCommand cmd, object obj) {
                            tail(cmd, obj);
                            PassByPosition(cmd);
                        };
                    }
                }
            }
            goto TR_0004;
        TR_0003:
            if (addToCache)
            {
                SetQueryCache(identity, info);
            }
            return info;
        TR_0004:
            info.ParamReader = action;
            goto TR_0003;
        }

        private static int GetColumnHash(IDataReader reader, int startBound = 0, int length = -1)
        {
            int num = (length < 0) ? reader.FieldCount : (startBound + length);
            int num2 = (-37 * startBound) + num;
            for (int i = startBound; i < num; i++)
            {
                int hashCode;
                object name = reader.GetName(i);
                Type fieldType = reader.GetFieldType(i);
                if (fieldType != null)
                {
                    hashCode = (-79 * ((num2 * 0x1f) + ((name != null) ? name.GetHashCode() : 0))).GetHashCode();
                }
                else
                {
                    int local1 = -79 * ((num2 * 0x1f) + ((name != null) ? name.GetHashCode() : 0));
                    hashCode = 0;
                }
                num2 = ((int) fieldType) + hashCode;
            }
            return num2;
        }

        internal static Func<IDataReader, object> GetDapperRowDeserializer(IDataRecord reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            int fieldCount = reader.FieldCount;
            if (length == -1)
            {
                length = fieldCount - startBound;
            }
            if (fieldCount <= startBound)
            {
                throw MultiMapException(reader);
            }
            int effectiveFieldCount = Math.Min(fieldCount - startBound, length);
            DapperTable table = null;
            return delegate (IDataReader r) {
                if (table == null)
                {
                    string[] fieldNames = new string[effectiveFieldCount];
                    int index = 0;
                    while (true)
                    {
                        if (index >= effectiveFieldCount)
                        {
                            table = new DapperTable(fieldNames);
                            break;
                        }
                        fieldNames[index] = r.GetName(index + startBound);
                        index++;
                    }
                }
                object[] values = new object[effectiveFieldCount];
                if (returnNullIfFirstMissing)
                {
                    values[0] = r.GetValue(startBound);
                    if (values[0] is DBNull)
                    {
                        return null;
                    }
                }
                if (startBound == 0)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        object obj2 = r.GetValue(i);
                        values[i] = (obj2 is DBNull) ? null : obj2;
                    }
                }
                else
                {
                    for (int i = returnNullIfFirstMissing ? 1 : 0; i < effectiveFieldCount; i++)
                    {
                        object obj3 = r.GetValue(i + startBound);
                        values[i] = (obj3 is DBNull) ? null : obj3;
                    }
                }
                return new DapperRow(table, values);
            };
        }

        [Obsolete("This method is for internal use only", false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static DbType GetDbType(object value)
        {
            ITypeHandler handler;
            return (((value == null) || (value is DBNull)) ? DbType.Object : LookupDbType(value.GetType(), "n/a", false, out handler));
        }

        private static Func<IDataReader, object> GetDeserializer(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            if ((type == typeof(object)) || (type == typeof(DapperRow)))
            {
                return GetDapperRowDeserializer(reader, startBound, length, returnNullIfFirstMissing);
            }
            Type type2 = null;
            if (!typeMap.ContainsKey(type) && (!type.IsEnum && ((type.FullName != "System.Data.Linq.Binary") && (!type.IsValueType || (((type2 = Nullable.GetUnderlyingType(type)) == null) || !type2.IsEnum)))))
            {
                ITypeHandler handler;
                return (!typeHandlers.TryGetValue(type, out handler) ? GetTypeDeserializer(type, reader, startBound, length, returnNullIfFirstMissing) : GetHandlerDeserializer(handler, type, startBound));
            }
            Type effectiveType = type2;
            if (type2 == null)
            {
                Type local1 = type2;
                effectiveType = type;
            }
            return GetStructDeserializer(type, effectiveType, startBound);
        }

        private static Func<IDataReader, object> GetHandlerDeserializer(ITypeHandler handler, Type type, int startBound) => 
            reader => handler.Parse(type, reader.GetValue(startBound));

        public static IEnumerable<Tuple<int, int>> GetHashCollissions()
        {
            Dictionary<int, int> source = new Dictionary<int, int>();
            foreach (Identity identity in _queryCache.Keys)
            {
                int num;
                if (!source.TryGetValue(identity.hashCode, out num))
                {
                    source.Add(identity.hashCode, 1);
                    continue;
                }
                source[identity.hashCode] = num + 1;
            }
            Func<KeyValuePair<int, int>, bool> predicate = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Func<KeyValuePair<int, int>, bool> local1 = <>c.<>9__88_0;
                predicate = <>c.<>9__88_0 = pair => pair.Value > 1;
            }
            Func<KeyValuePair<int, int>, Tuple<int, int>> selector = <>c.<>9__88_1;
            if (<>c.<>9__88_1 == null)
            {
                Func<KeyValuePair<int, int>, Tuple<int, int>> local2 = <>c.<>9__88_1;
                selector = <>c.<>9__88_1 = pair => Tuple.Create<int, int>(pair.Key, pair.Value);
            }
            return source.Where<KeyValuePair<int, int>>(predicate).Select<KeyValuePair<int, int>, Tuple<int, int>>(selector);
        }

        private static string GetInListRegex(string name, bool byPosition) => 
            byPosition ? (@"(\?)" + Regex.Escape(name) + @"\?(?!\w)(\s+(?i)unknown(?-i))?") : ("([?@:]" + Regex.Escape(name) + @")(?!\w)(\s+(?i)unknown(?-i))?");

        internal static int GetListPaddingExtraCount(int count)
        {
            int num;
            if (count <= 5)
            {
                return 0;
            }
            if (count < 0)
            {
                return 0;
            }
            if (count <= 150)
            {
                num = 10;
            }
            else if (count <= 750)
            {
                num = 50;
            }
            else if (count <= 0x7d0)
            {
                num = 100;
            }
            else if (count <= 0x816)
            {
                num = 10;
            }
            else
            {
                if (count <= 0x834)
                {
                    return 0;
                }
                num = 200;
            }
            int num2 = count % num;
            return ((num2 == 0) ? 0 : (num - num2));
        }

        internal static IList<LiteralToken> GetLiteralTokens(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return LiteralToken.None;
            }
            if (!literalTokens.IsMatch(sql))
            {
                return LiteralToken.None;
            }
            MatchCollection matchs = literalTokens.Matches(sql);
            HashSet<string> set = new HashSet<string>(StringComparer.Ordinal);
            List<LiteralToken> list = new List<LiteralToken>(matchs.Count);
            foreach (Match match in matchs)
            {
                string token = match.Value;
                if (set.Add(match.Value))
                {
                    list.Add(new LiteralToken(token, match.Groups[1].Value));
                }
            }
            return ((list.Count == 0) ? LiteralToken.None : list);
        }

        private static IEnumerable GetMultiExec(object param) => 
            (!(param is IEnumerable) || ((param is string) || ((param is IEnumerable<KeyValuePair<string, object>>) || (param is IDynamicParameters)))) ? null : ((IEnumerable) param);

        private static int GetNextSplit(int startIdx, string splitOn, IDataReader reader)
        {
            if (splitOn == "*")
            {
                return --startIdx;
            }
            for (int i = startIdx - 1; i > 0; i--)
            {
                if (string.Equals(splitOn, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            throw MultiMapException(reader);
        }

        private static int GetNextSplitDynamic(int startIdx, string splitOn, IDataReader reader)
        {
            if (startIdx == reader.FieldCount)
            {
                throw MultiMapException(reader);
            }
            if (splitOn == "*")
            {
                return ++startIdx;
            }
            for (int i = startIdx + 1; i < reader.FieldCount; i++)
            {
                if (string.Equals(splitOn, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return reader.FieldCount;
        }

        private static MethodInfo GetOperator(Type from, Type to)
        {
            MethodInfo[] infoArray;
            if (to == null)
            {
                return null;
            }
            MethodInfo info1 = ResolveOperator(infoArray = from.GetMethods(BindingFlags.Public | BindingFlags.Static), from, to, "op_Implicit");
            MethodInfo info4 = info1;
            if (info1 == null)
            {
                MethodInfo[] infoArray2;
                MethodInfo local1 = info1;
                MethodInfo info2 = ResolveOperator(infoArray2 = to.GetMethods(BindingFlags.Public | BindingFlags.Static), from, to, "op_Implicit");
                info4 = info2;
                if (info2 == null)
                {
                    MethodInfo local2 = info2;
                    MethodInfo info3 = ResolveOperator(infoArray, from, to, "op_Explicit");
                    info4 = info3;
                    if (info3 == null)
                    {
                        MethodInfo local3 = info3;
                        info4 = ResolveOperator(infoArray2, from, to, "op_Explicit");
                    }
                }
            }
            return info4;
        }

        private static Action<IDbCommand, object> GetParameterReader(IDbConnection cnn, ref CommandDefinition command)
        {
            object parameters = command.Parameters;
            CacheInfo info = null;
            if (GetMultiExec(parameters) != null)
            {
                throw new NotSupportedException("MultiExec is not supported by ExecuteReader");
            }
            if (parameters != null)
            {
                info = GetCacheInfo(new Identity(command.CommandText, command.CommandType, cnn, null, parameters.GetType()), parameters, command.AddToCache);
            }
            return info?.ParamReader;
        }

        public static Func<IDataReader, object> GetRowParser(this IDataReader reader, Type type, int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false) => 
            GetDeserializer(type, reader, startIndex, length, returnNullIfFirstMissing);

        public static Func<IDataReader, T> GetRowParser<T>(this IDataReader reader, Type concreteType = null, int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false)
        {
            concreteType ??= typeof(T);
            Func<IDataReader, object> func = GetDeserializer(concreteType, reader, startIndex, length, returnNullIfFirstMissing);
            return (!concreteType.IsValueType ? ((Func<IDataReader, T>) func) : _ => ((T) func(_)));
        }

        private static StringBuilder GetStringBuilder()
        {
            StringBuilder perThreadStringBuilderCache = SqlMapper.perThreadStringBuilderCache;
            if (perThreadStringBuilderCache == null)
            {
                return new StringBuilder();
            }
            SqlMapper.perThreadStringBuilderCache = null;
            perThreadStringBuilderCache.Length = 0;
            return perThreadStringBuilderCache;
        }

        private static Func<IDataReader, object> GetStructDeserializer(Type type, Type effectiveType, int index)
        {
            ITypeHandler handler;
            return (!(type == typeof(char)) ? (!(type == typeof(char?)) ? ((type.FullName != "System.Data.Linq.Binary") ? (!effectiveType.IsEnum ? (!typeHandlers.TryGetValue(type, out handler) ? delegate (IDataReader r) {
                object obj2 = r.GetValue(index);
                return ((obj2 is DBNull) ? null : obj2);
            } : delegate (IDataReader r) {
                object obj2 = r.GetValue(index);
                return ((obj2 is DBNull) ? null : handler.Parse(type, obj2));
            }) : delegate (IDataReader r) {
                object obj2 = r.GetValue(index);
                if ((obj2 is float) || ((obj2 is double) || (obj2 is decimal)))
                {
                    obj2 = Convert.ChangeType(obj2, Enum.GetUnderlyingType(effectiveType), CultureInfo.InvariantCulture);
                }
                return ((obj2 is DBNull) ? null : Enum.ToObject(effectiveType, obj2));
            }) : r => Activator.CreateInstance(type, new object[] { r.GetValue(index) })) : r => ReadNullableChar(r.GetValue(index))) : r => ReadChar(r.GetValue(index)));
        }

        private static LocalBuilder GetTempLocal(ILGenerator il, ref Dictionary<Type, LocalBuilder> locals, Type type, bool initAndLoad)
        {
            LocalBuilder builder;
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ref Dictionary<Type, LocalBuilder> dictionaryRef1 = locals;
            if (locals == null)
            {
                ref Dictionary<Type, LocalBuilder> local1 = locals;
                dictionaryRef1 = new Dictionary<Type, LocalBuilder>();
            }
            locals = dictionaryRef1;
            if (!locals.TryGetValue(type, out builder))
            {
                builder = il.DeclareLocal(type);
                locals.Add(type, builder);
            }
            if (initAndLoad)
            {
                il.Emit(OpCodes.Ldloca, builder);
                il.Emit(OpCodes.Initobj, type);
                il.Emit(OpCodes.Ldloca, builder);
                il.Emit(OpCodes.Ldobj, type);
            }
            return builder;
        }

        private static MethodInfo GetToString(TypeCode typeCode)
        {
            MethodInfo info;
            return (toStrings.TryGetValue(typeCode, out info) ? info : null);
        }

        public static Func<IDataReader, object> GetTypeDeserializer(Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false) => 
            TypeDeserializerCache.GetReader(type, reader, startBound, length, returnNullIfFirstMissing);

        private static Func<IDataReader, object> GetTypeDeserializerImpl(Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false)
        {
            if (length == -1)
            {
                length = reader.FieldCount - startBound;
            }
            if (reader.FieldCount <= startBound)
            {
                throw MultiMapException(reader);
            }
            Type returnType = type.IsValueType ? typeof(object) : type;
            Type[] parameterTypes = new Type[] { typeof(IDataReader) };
            DynamicMethod method = new DynamicMethod("Deserialize" + Guid.NewGuid().ToString(), returnType, parameterTypes, type, true);
            ILGenerator iLGenerator = method.GetILGenerator();
            if (IsValueTuple(type))
            {
                GenerateValueTupleDeserializer(type, reader, startBound, length, iLGenerator);
            }
            else
            {
                GenerateDeserializerFromMap(type, reader, startBound, length, returnNullIfFirstMissing, iLGenerator);
            }
            Type[] typeArgs = new Type[] { typeof(IDataReader), returnType };
            return (Func<IDataReader, object>) method.CreateDelegate(Expression.GetFuncType(typeArgs));
        }

        public static ITypeMap GetTypeMap(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ITypeMap map = (ITypeMap) _typeMaps[type];
            if (map == null)
            {
                Hashtable hashtable = _typeMaps;
                lock (hashtable)
                {
                    map = (ITypeMap) _typeMaps[type];
                    if (map == null)
                    {
                        map = TypeMapProvider(type);
                        _typeMaps[type] = map;
                    }
                }
            }
            return map;
        }

        public static string GetTypeName(this DataTable table) => 
            ((table != null) ? ((string) table.ExtendedProperties["dapper:TypeName"]) : null) as string;

        internal static bool HasTypeHandler(Type type) => 
            typeHandlers.ContainsKey(type);

        private static bool IsValueTuple(Type type) => 
            (type != null) && (type.IsValueType && type.FullName.StartsWith("System.ValueTuple`", StringComparison.Ordinal));

        private static void LoadDefaultValue(ILGenerator il, Type type)
        {
            if (!type.IsValueType)
            {
                il.Emit(OpCodes.Ldnull);
            }
            else
            {
                LocalBuilder local = il.DeclareLocal(type);
                il.Emit(OpCodes.Ldloca, local);
                il.Emit(OpCodes.Initobj, type);
                il.Emit(OpCodes.Ldloc, local);
            }
        }

        private static void LoadReaderValueOrBranchToDBNullLabel(ILGenerator il, int index, ref LocalBuilder stringEnumLocal, LocalBuilder valueCopyLocal, Type colType, Type memberType, out Label isDbNullLabel)
        {
            isDbNullLabel = il.DefineLabel();
            il.Emit(OpCodes.Ldarg_0);
            EmitInt32(il, index);
            il.Emit(OpCodes.Callvirt, getItem);
            if (valueCopyLocal != null)
            {
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Stloc, valueCopyLocal);
            }
            if ((memberType == typeof(char)) || (memberType == typeof(char?)))
            {
                il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod((memberType == typeof(char)) ? "ReadChar" : "ReadNullableChar", BindingFlags.Public | BindingFlags.Static), null);
            }
            else
            {
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Isinst, typeof(DBNull));
                il.Emit(OpCodes.Brtrue_S, isDbNullLabel);
                Type underlyingType = Nullable.GetUnderlyingType(memberType);
                Type enumType = ((underlyingType == null) || !underlyingType.IsEnum) ? memberType : underlyingType;
                if (enumType.IsEnum)
                {
                    Type via = Enum.GetUnderlyingType(enumType);
                    if (!(colType == typeof(string)))
                    {
                        FlexibleConvertBoxedFromHeadOfStack(il, colType, enumType, via);
                    }
                    else
                    {
                        stringEnumLocal ??= il.DeclareLocal(typeof(string));
                        il.Emit(OpCodes.Castclass, typeof(string));
                        il.Emit(OpCodes.Stloc, stringEnumLocal);
                        il.Emit(OpCodes.Ldtoken, enumType);
                        il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
                        il.Emit(OpCodes.Ldloc, stringEnumLocal);
                        il.Emit(OpCodes.Ldc_I4_1);
                        il.EmitCall(OpCodes.Call, enumParse, null);
                        il.Emit(OpCodes.Unbox_Any, enumType);
                    }
                    if (underlyingType != null)
                    {
                        Type[] types = new Type[] { underlyingType };
                        il.Emit(OpCodes.Newobj, memberType.GetConstructor(types));
                    }
                }
                else if (memberType.FullName == "System.Data.Linq.Binary")
                {
                    il.Emit(OpCodes.Unbox_Any, typeof(byte[]));
                    Type[] types = new Type[] { typeof(byte[]) };
                    il.Emit(OpCodes.Newobj, memberType.GetConstructor(types));
                }
                else
                {
                    bool flag;
                    TypeCode typeCode = Type.GetTypeCode(colType);
                    TypeCode code2 = Type.GetTypeCode(enumType);
                    if ((flag = typeHandlers.ContainsKey(enumType)) || ((colType == enumType) || ((typeCode == code2) || (typeCode == Type.GetTypeCode(underlyingType)))))
                    {
                        if (!flag)
                        {
                            il.Emit(OpCodes.Unbox_Any, enumType);
                        }
                        else
                        {
                            Type[] typeArguments = new Type[] { enumType };
                            il.EmitCall(OpCodes.Call, typeof(TypeHandlerCache).MakeGenericType(typeArguments).GetMethod("Parse"), null);
                        }
                    }
                    else
                    {
                        Type to = underlyingType;
                        if (underlyingType == null)
                        {
                            Type local1 = underlyingType;
                            to = enumType;
                        }
                        FlexibleConvertBoxedFromHeadOfStack(il, colType, to, null);
                        if (underlyingType != null)
                        {
                            Type[] types = new Type[] { underlyingType };
                            il.Emit(OpCodes.Newobj, enumType.GetConstructor(types));
                        }
                    }
                }
            }
        }

        [Obsolete("This method is for internal use only", false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static DbType LookupDbType(Type type, string name, bool demand, out ITypeHandler handler)
        {
            DbType type3;
            handler = null;
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                type = underlyingType;
            }
            if (type.IsEnum && !typeMap.ContainsKey(type))
            {
                type = Enum.GetUnderlyingType(type);
            }
            if (typeMap.TryGetValue(type, out type3))
            {
                return type3;
            }
            if (type.FullName == "System.Data.Linq.Binary")
            {
                return DbType.Binary;
            }
            if (typeHandlers.TryGetValue(type, out handler))
            {
                return DbType.Object;
            }
            if (!typeof(IEnumerable).IsAssignableFrom(type))
            {
                string fullName = type.FullName;
                if (fullName != null)
                {
                    ITypeHandler handler2;
                    if (fullName == "Microsoft.SqlServer.Types.SqlGeography")
                    {
                        handler = handler2 = new UdtTypeHandler("geography");
                        AddTypeHandler(type, handler2);
                        return DbType.Object;
                    }
                    if (fullName == "Microsoft.SqlServer.Types.SqlGeometry")
                    {
                        handler = handler2 = new UdtTypeHandler("geometry");
                        AddTypeHandler(type, handler2);
                        return DbType.Object;
                    }
                    if (fullName == "Microsoft.SqlServer.Types.SqlHierarchyId")
                    {
                        handler = handler2 = new UdtTypeHandler("hierarchyid");
                        AddTypeHandler(type, handler2);
                        return DbType.Object;
                    }
                }
                if (!demand)
                {
                    return DbType.Object;
                }
                string[] textArray1 = new string[] { "The member ", name, " of type ", type.FullName, " cannot be used as a parameter value" };
                throw new NotSupportedException(string.Concat(textArray1));
            }
            if (type.IsInterface && (type.IsGenericType && ((type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) && typeof(IEnumerable<IDataRecord>).IsAssignableFrom(type))))
            {
                Type[] genericArguments = type.GetGenericArguments();
                if (typeof(IDataRecord).IsAssignableFrom(genericArguments[0]))
                {
                    try
                    {
                        handler = (ITypeHandler) Activator.CreateInstance(typeof(SqlDataRecordHandler<>).MakeGenericType(genericArguments));
                        AddTypeHandlerImpl(type, handler, true);
                        return DbType.Object;
                    }
                    catch
                    {
                        handler = null;
                    }
                }
            }
            return ~DbType.AnsiString;
        }

        private static IEnumerable<TReturn> MultiMap<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Delegate map, object param, IDbTransaction transaction, bool buffered, string splitOn, int? commandTimeout, CommandType? commandType)
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
            IEnumerable<TReturn> source = cnn.MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(command, map, splitOn, null, null, true);
            return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
        }

        [AsyncStateMachine(typeof(<MultiMapAsync>d__52))]
        private static Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Delegate map, string splitOn)
        {
            <MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.map = map;
            d__.splitOn = splitOn;
            d__.<>t__builder = AsyncTaskMethodBuilder<IEnumerable<TReturn>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<MultiMapAsync>d__54))]
        private static Task<IEnumerable<TReturn>> MultiMapAsync<TReturn>(this IDbConnection cnn, CommandDefinition command, Type[] types, Func<object[], TReturn> map, string splitOn)
        {
            <MultiMapAsync>d__54<TReturn> d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.types = types;
            d__.map = map;
            d__.splitOn = splitOn;
            d__.<>t__builder = AsyncTaskMethodBuilder<IEnumerable<TReturn>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<MultiMapAsync>d__54<TReturn>>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static Exception MultiMapException(IDataRecord reader)
        {
            bool flag = false;
            try
            {
                flag = (reader != null) && (reader.FieldCount != 0);
            }
            catch
            {
            }
            return (!flag ? ((Exception) new InvalidOperationException("No columns were selected")) : ((Exception) new ArgumentException("When using the multi-mapping APIs ensure you set the splitOn param if you have keys other than Id", "splitOn")));
        }

        [IteratorStateMachine(typeof(<MultiMapImpl>d__155))]
        private static IEnumerable<TReturn> MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Delegate map, string splitOn, IDataReader reader, Identity identity, bool finalize)
        {
            <MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> d__1 = new <MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(-2);
            d__1.<>3__cnn = cnn;
            d__1.<>3__command = command;
            d__1.<>3__map = map;
            d__1.<>3__splitOn = splitOn;
            d__1.<>3__reader = reader;
            d__1.<>3__identity = identity;
            d__1.<>3__finalize = finalize;
            return d__1;
        }

        [IteratorStateMachine(typeof(<MultiMapImpl>d__157))]
        private static IEnumerable<TReturn> MultiMapImpl<TReturn>(this IDbConnection cnn, CommandDefinition command, Type[] types, Func<object[], TReturn> map, string splitOn, IDataReader reader, Identity identity, bool finalize)
        {
            <MultiMapImpl>d__157<TReturn> d__1 = new <MultiMapImpl>d__157<TReturn>(-2);
            d__1.<>3__cnn = cnn;
            d__1.<>3__command = command;
            d__1.<>3__types = types;
            d__1.<>3__map = map;
            d__1.<>3__splitOn = splitOn;
            d__1.<>3__reader = reader;
            d__1.<>3__identity = identity;
            d__1.<>3__finalize = finalize;
            return d__1;
        }

        private static void OnQueryCachePurged()
        {
            EventHandler queryCachePurged = QueryCachePurged;
            if (queryCachePurged != null)
            {
                queryCachePurged(null, EventArgs.Empty);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method is for internal use only", false)]
        public static void PackListParameters(IDbCommand command, string namePrefix, object value)
        {
            if (FeatureSupport.Get(command.Connection).Arrays)
            {
                IDbDataParameter parameter = command.CreateParameter();
                parameter.Value = SanitizeParameterValue(value);
                parameter.ParameterName = namePrefix;
                command.Parameters.Add(parameter);
            }
            else
            {
                bool byPosition = ShouldPassByPosition(command.CommandText);
                IEnumerable list = value as IEnumerable;
                int count = 0;
                bool flag = value is IEnumerable<string>;
                bool flag2 = value is IEnumerable<DbString>;
                DbType ansiString = DbType.AnsiString;
                int inListStringSplitCount = Settings.InListStringSplitCount;
                bool flag3 = (inListStringSplitCount >= 0) && TryStringSplit(ref list, inListStringSplitCount, namePrefix, command, byPosition);
                if ((list != null) && !flag3)
                {
                    int num2;
                    object obj2 = null;
                    foreach (object obj3 in list)
                    {
                        num2 = count + 1;
                        count = num2;
                        if (num2 == 1)
                        {
                            if (obj3 == null)
                            {
                                throw new NotSupportedException("The first item in a list-expansion cannot be null");
                            }
                            if (!flag2)
                            {
                                ITypeHandler handler;
                                ansiString = LookupDbType(obj3.GetType(), "", true, out handler);
                            }
                        }
                        string name = namePrefix + count.ToString();
                        if (flag2 && (obj3 is DbString))
                        {
                            (obj3 as DbString).AddParameter(command, name);
                        }
                        else
                        {
                            IDbDataParameter parameter2 = command.CreateParameter();
                            parameter2.ParameterName = name;
                            if (flag)
                            {
                                parameter2.Size = 0xfa0;
                                if ((obj3 != null) && (((string) obj3).Length > 0xfa0))
                                {
                                    parameter2.Size = -1;
                                }
                            }
                            object obj4 = parameter2.Value = SanitizeParameterValue(obj3);
                            if ((obj4 != null) && !(obj4 is DBNull))
                            {
                                obj2 = obj4;
                            }
                            if (parameter2.DbType != ansiString)
                            {
                                parameter2.DbType = ansiString;
                            }
                            command.Parameters.Add(parameter2);
                        }
                    }
                    if (Settings.PadListExpansions && (!flag2 && (obj2 != null)))
                    {
                        int listPaddingExtraCount = GetListPaddingExtraCount(count);
                        for (int i = 0; i < listPaddingExtraCount; i++)
                        {
                            num2 = count;
                            count = num2 + 1;
                            IDbDataParameter parameter3 = command.CreateParameter();
                            parameter3.ParameterName = namePrefix + count.ToString();
                            if (flag)
                            {
                                parameter3.Size = 0xfa0;
                            }
                            parameter3.DbType = ansiString;
                            parameter3.Value = obj2;
                            command.Parameters.Add(parameter3);
                        }
                    }
                }
                if (!flag3)
                {
                    string inListRegex = GetInListRegex(namePrefix, byPosition);
                    if (count == 0)
                    {
                        MatchEvaluator evaluator = <>c.<>9__175_1;
                        if (<>c.<>9__175_1 == null)
                        {
                            MatchEvaluator local1 = <>c.<>9__175_1;
                            evaluator = <>c.<>9__175_1 = match => !match.Groups[2].Success ? ("(SELECT " + match.Groups[1].Value + " WHERE 1 = 0)") : match.Value;
                        }
                        command.CommandText = Regex.Replace(command.CommandText, inListRegex, evaluator, RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        IDbDataParameter parameter4 = command.CreateParameter();
                        parameter4.ParameterName = namePrefix;
                        parameter4.Value = DBNull.Value;
                        command.Parameters.Add(parameter4);
                    }
                    else
                    {
                        command.CommandText = Regex.Replace(command.CommandText, inListRegex, delegate (Match match) {
                            string str = match.Groups[1].Value;
                            if (match.Groups[2].Success)
                            {
                                string str2 = match.Groups[2].Value;
                                StringBuilder builder = GetStringBuilder().Append(str).Append(1).Append(str2);
                                for (int j = 2; j <= count; j++)
                                {
                                    builder.Append(',').Append(str).Append(j).Append(str2);
                                }
                                return builder.__ToStringRecycle();
                            }
                            StringBuilder builder2 = GetStringBuilder().Append('(').Append(str);
                            if (!byPosition)
                            {
                                builder2.Append(1);
                            }
                            else
                            {
                                builder2.Append(namePrefix).Append(1).Append(str);
                            }
                            for (int i = 2; i <= count; i++)
                            {
                                builder2.Append(',').Append(str);
                                if (!byPosition)
                                {
                                    builder2.Append(i);
                                }
                                else
                                {
                                    builder2.Append(namePrefix).Append(i).Append(str);
                                }
                            }
                            return builder2.Append(')').__ToStringRecycle();
                        }, RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    }
                }
            }
        }

        [return: Dynamic(new bool[] { false, true })]
        [IteratorStateMachine(typeof(<Parse>d__240))]
        public static IEnumerable<object> Parse(this IDataReader reader)
        {
            <Parse>d__240 d__1 = new <Parse>d__240(-2);
            d__1.<>3__reader = reader;
            return d__1;
        }

        [IteratorStateMachine(typeof(<Parse>d__238))]
        public static IEnumerable<T> Parse<T>(this IDataReader reader)
        {
            Func<IDataReader, object> <deser>5__2;
            Type <convertToType>5__3;
            object obj2;
            if (reader.Read())
            {
                Type type = typeof(T);
                <deser>5__2 = GetDeserializer(type, reader, 0, -1, false);
                Type underlyingType = Nullable.GetUnderlyingType(type);
                Type type2 = underlyingType;
                if (underlyingType == null)
                {
                    Type local1 = underlyingType;
                    type2 = type;
                }
                <convertToType>5__3 = type2;
            }
            else
            {
                goto TR_0009;
            }
        TR_0003:
            obj2 = <deser>5__2(reader);
            if ((obj2 != null) && !(obj2 is T))
            {
                yield return (T) Convert.ChangeType(obj2, <convertToType>5__3, CultureInfo.InvariantCulture);
            }
            else
            {
                yield return (T) obj2;
            }
            if (reader.Read())
            {
                goto TR_0003;
            }
            else
            {
                <deser>5__2 = null;
                <convertToType>5__3 = null;
            }
        TR_0009:;
        }

        private static T Parse<T>(object value)
        {
            if ((value == null) || (value is DBNull))
            {
                return default(T);
            }
            if (value is T)
            {
                return (T) value;
            }
            Type nullableType = typeof(T);
            nullableType = Nullable.GetUnderlyingType(nullableType) ?? nullableType;
            if (!nullableType.IsEnum)
            {
                ITypeHandler handler;
                return (!typeHandlers.TryGetValue(nullableType, out handler) ? ((T) Convert.ChangeType(value, nullableType, CultureInfo.InvariantCulture)) : ((T) handler.Parse(nullableType, value)));
            }
            if ((value is float) || ((value is double) || (value is decimal)))
            {
                value = Convert.ChangeType(value, Enum.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture);
            }
            return (T) Enum.ToObject(nullableType, value);
        }

        [IteratorStateMachine(typeof(<Parse>d__239))]
        public static IEnumerable<object> Parse(this IDataReader reader, Type type)
        {
            <Parse>d__239 d__1 = new <Parse>d__239(-2);
            d__1.<>3__reader = reader;
            d__1.<>3__type = type;
            return d__1;
        }

        private static void PassByPosition(IDbCommand cmd)
        {
            if (cmd.Parameters.Count != 0)
            {
                Dictionary<string, IDbDataParameter> parameters = new Dictionary<string, IDbDataParameter>(StringComparer.Ordinal);
                foreach (IDbDataParameter parameter in cmd.Parameters)
                {
                    if (!string.IsNullOrEmpty(parameter.ParameterName))
                    {
                        parameters[parameter.ParameterName] = parameter;
                    }
                }
                HashSet<string> consumed = new HashSet<string>(StringComparer.Ordinal);
                bool firstMatch = true;
                cmd.CommandText = pseudoPositional.Replace(cmd.CommandText, delegate (Match match) {
                    IDbDataParameter parameter;
                    string item = match.Groups[1].Value;
                    if (!consumed.Add(item))
                    {
                        throw new InvalidOperationException("When passing parameters by position, each parameter can only be referenced once");
                    }
                    if (!parameters.TryGetValue(item, out parameter))
                    {
                        return match.Value;
                    }
                    if (firstMatch)
                    {
                        firstMatch = false;
                        cmd.Parameters.Clear();
                    }
                    cmd.Parameters.Add(parameter);
                    parameters.Remove(item);
                    consumed.Add(item);
                    return "?";
                });
            }
        }

        public static void PurgeQueryCache()
        {
            _queryCache.Clear();
            TypeDeserializerCache.Purge();
            OnQueryCachePurged();
        }

        private static void PurgeQueryCacheByType(Type type)
        {
            foreach (KeyValuePair<Identity, CacheInfo> pair in _queryCache)
            {
                if (pair.Key.type == type)
                {
                    CacheInfo info;
                    _queryCache.TryRemove(pair.Key, out info);
                }
            }
            TypeDeserializerCache.Purge(type);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, CommandDefinition command)
        {
            IEnumerable<T> source = cnn.QueryImpl<T>(command, typeof(T));
            return (command.Buffered ? source.ToList<T>() : source);
        }

        [return: Dynamic(new bool[] { false, true })]
        public static IEnumerable<object> Query(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.Query<DapperRow>(sql, param, transaction, buffered, commandTimeout, commandType);

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
            IEnumerable<T> source = cnn.QueryImpl<T>(command, typeof(T));
            return (command.Buffered ? source.ToList<T>() : source);
        }

        public static IEnumerable<object> Query(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
            IEnumerable<object> source = cnn.QueryImpl<object>(command, type);
            return (command.Buffered ? source.ToList<object>() : source);
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, System.Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontMap, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, TThird, TFourth, DontMap, DontMap, DontMap, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, TThird, TFourth, TFifth, DontMap, DontMap, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.MultiMap<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
            IEnumerable<TReturn> source = cnn.MultiMapImpl<TReturn>(command, types, map, splitOn, null, null, true);
            return (buffered ? source.ToList<TReturn>() : source);
        }

        [return: Dynamic(new bool[] { false, false, true })]
        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryAsync<object>(typeof(DapperRow), command);

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryAsync<T>(typeof(T), command);

        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, CommandDefinition command) => 
            cnn.QueryAsync<object>(type, command);

        [AsyncStateMachine(typeof(<QueryAsync>d__33))]
        private static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Type effectiveType, CommandDefinition command)
        {
            <QueryAsync>d__33<T> d__;
            d__.cnn = cnn;
            d__.effectiveType = effectiveType;
            d__.command = command;
            d__.<>t__builder = AsyncTaskMethodBuilder<IEnumerable<T>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<QueryAsync>d__33<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, CommandDefinition command, System.Func<TFirst, TSecond, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontMap, TReturn>(command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, DontMap, DontMap, DontMap, TReturn>(command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, DontMap, DontMap, TReturn>(command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id") => 
            cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(command, map, splitOn);

        [return: Dynamic(new bool[] { false, false, true })]
        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryAsync<object>(typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryAsync<T>(typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryAsync<object>(type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, System.Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontMap, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, DontMap, DontMap, DontMap, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, DontMap, DontMap, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken), map, splitOn);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, cancellationToken);
            return cnn.MultiMapAsync<TReturn>(command, types, map, splitOn);
        }

        public static T QueryFirst<T>(this IDbConnection cnn, CommandDefinition command) => 
            QueryRowImpl<T>(cnn, Row.First, ref command, typeof(T));

        [return: Dynamic]
        public static object QueryFirst(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.QueryFirst<DapperRow>(sql, param, transaction, commandTimeout, commandType);

        public static T QueryFirst<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<T>(cnn, Row.First, ref command, typeof(T));
        }

        public static object QueryFirst(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<object>(cnn, Row.First, ref command, type);
        }

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QueryFirstAsync(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.First, typeof(DapperRow), command);

        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<T>(Row.First, typeof(T), command);

        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.First, type, command);

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QueryFirstAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.First, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<T>(Row.First, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.First, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static T QueryFirstOrDefault<T>(this IDbConnection cnn, CommandDefinition command) => 
            QueryRowImpl<T>(cnn, Row.FirstOrDefault, ref command, typeof(T));

        [return: Dynamic]
        public static object QueryFirstOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.QueryFirstOrDefault<DapperRow>(sql, param, transaction, commandTimeout, commandType);

        public static T QueryFirstOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<T>(cnn, Row.FirstOrDefault, ref command, typeof(T));
        }

        public static object QueryFirstOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<object>(cnn, Row.FirstOrDefault, ref command, type);
        }

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.FirstOrDefault, typeof(DapperRow), command);

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<T>(Row.FirstOrDefault, typeof(T), command);

        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.FirstOrDefault, type, command);

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.FirstOrDefault, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<T>(Row.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.FirstOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        [IteratorStateMachine(typeof(<QueryImpl>d__140))]
        private static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, CommandDefinition command, Type effectiveType)
        {
            <QueryImpl>d__140<T> d__1 = new <QueryImpl>d__140<T>(-2);
            d__1.<>3__cnn = cnn;
            d__1.<>3__command = command;
            d__1.<>3__effectiveType = effectiveType;
            return d__1;
        }

        public static GridReader QueryMultiple(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryMultipleImpl(ref command);

        public static GridReader QueryMultiple(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken);
            return cnn.QueryMultipleImpl(ref command);
        }

        [AsyncStateMachine(typeof(<QueryMultipleAsync>d__57))]
        public static Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, CommandDefinition command)
        {
            <QueryMultipleAsync>d__57 d__;
            d__.cnn = cnn;
            d__.command = command;
            d__.<>t__builder = AsyncTaskMethodBuilder<GridReader>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<QueryMultipleAsync>d__57>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryMultipleAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, cancellationToken));
        }

        private static GridReader QueryMultipleImpl(this IDbConnection cnn, ref CommandDefinition command)
        {
            GridReader reader3;
            object parameters = command.Parameters;
            Identity identity = new Identity(command.CommandText, command.CommandType, cnn, typeof(GridReader), parameters?.GetType());
            CacheInfo info = GetCacheInfo(identity, parameters, command.AddToCache);
            IDbCommand command2 = null;
            IDataReader reader = null;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                if (wasClosed)
                {
                    cnn.Open();
                }
                command2 = command.SetupCommand(cnn, info.ParamReader);
                command2 = null;
                wasClosed = false;
                reader3 = new GridReader(command2, ExecuteReaderWithFlagsFallback(command2, wasClosed, CommandBehavior.SequentialAccess), identity, command.Parameters as DynamicParameters, command.AddToCache);
            }
            catch
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                    {
                        try
                        {
                            if (command2 != null)
                            {
                                command2.Cancel();
                            }
                        }
                        catch
                        {
                        }
                    }
                    reader.Dispose();
                }
                if (command2 != null)
                {
                    command2.Dispose();
                }
                if (wasClosed)
                {
                    cnn.Close();
                }
                throw;
            }
            return reader3;
        }

        [AsyncStateMachine(typeof(<QueryRowAsync>d__34))]
        private static Task<T> QueryRowAsync<T>(this IDbConnection cnn, Row row, Type effectiveType, CommandDefinition command)
        {
            <QueryRowAsync>d__34<T> d__;
            d__.cnn = cnn;
            d__.row = row;
            d__.effectiveType = effectiveType;
            d__.command = command;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<QueryRowAsync>d__34<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static T QueryRowImpl<T>(IDbConnection cnn, Row row, ref CommandDefinition command, Type effectiveType)
        {
            T local2;
            object parameters = command.Parameters;
            Identity identity = new Identity(command.CommandText, command.CommandType, cnn, effectiveType, parameters?.GetType());
            CacheInfo info = GetCacheInfo(identity, parameters, command.AddToCache);
            IDbCommand cmd = null;
            IDataReader reader = null;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                cmd = command.SetupCommand(cnn, info.ParamReader);
                if (wasClosed)
                {
                    cnn.Open();
                }
                reader = ExecuteReaderWithFlagsFallback(cmd, wasClosed, ((row & Row.Single) != Row.First) ? (CommandBehavior.SequentialAccess | CommandBehavior.SingleResult) : (CommandBehavior.SequentialAccess | CommandBehavior.SingleRow | CommandBehavior.SingleResult));
                wasClosed = false;
                T local = default(T);
                if (!reader.Read() || (reader.FieldCount == 0))
                {
                    if ((row & Row.FirstOrDefault) == Row.First)
                    {
                        ThrowZeroRows(row);
                    }
                }
                else
                {
                    DeserializerState deserializer = info.Deserializer;
                    int hash = GetColumnHash(reader, 0, -1);
                    if ((deserializer.Func == null) || (deserializer.Hash != hash))
                    {
                        deserializer = info.Deserializer = new DeserializerState(hash, GetDeserializer(effectiveType, reader, 0, -1, false));
                        if (command.AddToCache)
                        {
                            SetQueryCache(identity, info);
                        }
                    }
                    object obj3 = deserializer.Func(reader);
                    local = ((obj3 == null) || (obj3 is T)) ? ((T) obj3) : ((T) Convert.ChangeType(obj3, Nullable.GetUnderlyingType(effectiveType) ?? effectiveType, CultureInfo.InvariantCulture));
                    if (((row & Row.Single) != Row.First) && reader.Read())
                    {
                        ThrowMultipleRows(row);
                    }
                    while (reader.Read())
                    {
                    }
                }
                while (true)
                {
                    if (!reader.NextResult())
                    {
                        reader.Dispose();
                        reader = null;
                        command.OnCompleted();
                        local2 = local;
                        break;
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                    {
                        try
                        {
                            cmd.Cancel();
                        }
                        catch
                        {
                        }
                    }
                    reader.Dispose();
                }
                if (wasClosed)
                {
                    cnn.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return local2;
        }

        public static T QuerySingle<T>(this IDbConnection cnn, CommandDefinition command) => 
            QueryRowImpl<T>(cnn, Row.Single, ref command, typeof(T));

        [return: Dynamic]
        public static object QuerySingle(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.QuerySingle<DapperRow>(sql, param, transaction, commandTimeout, commandType);

        public static T QuerySingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<T>(cnn, Row.Single, ref command, typeof(T));
        }

        public static object QuerySingle(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<object>(cnn, Row.Single, ref command, type);
        }

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QuerySingleAsync(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.Single, typeof(DapperRow), command);

        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<T>(Row.Single, typeof(T), command);

        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.Single, type, command);

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QuerySingleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.Single, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<T>(Row.Single, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.Single, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static T QuerySingleOrDefault<T>(this IDbConnection cnn, CommandDefinition command) => 
            QueryRowImpl<T>(cnn, Row.SingleOrDefault, ref command, typeof(T));

        [return: Dynamic]
        public static object QuerySingleOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?()) => 
            cnn.QuerySingleOrDefault<DapperRow>(sql, param, transaction, commandTimeout, commandType);

        public static T QuerySingleOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<T>(cnn, Row.SingleOrDefault, ref command, typeof(T));
        }

        public static object QuerySingleOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken);
            return QueryRowImpl<object>(cnn, Row.SingleOrDefault, ref command, type);
        }

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.SingleOrDefault, typeof(DapperRow), command);

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command) => 
            cnn.QueryRowAsync<T>(Row.SingleOrDefault, typeof(T), command);

        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command) => 
            cnn.QueryRowAsync<object>(Row.SingleOrDefault, type, command);

        [return: Dynamic(new bool[] { false, true })]
        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.SingleOrDefault, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<T>(Row.SingleOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), CommandType? commandType = new CommandType?())
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            CancellationToken cancellationToken = new CancellationToken();
            return cnn.QueryRowAsync<object>(Row.SingleOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, cancellationToken));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method is for internal use only", false)]
        public static char ReadChar(object value)
        {
            if ((value == null) || (value is DBNull))
            {
                throw new ArgumentNullException("value");
            }
            string str = value as string;
            if ((str != null) && (str.Length == 1))
            {
                return str[0];
            }
            if (!(value is char))
            {
                throw new ArgumentException("A single-character was expected", "value");
            }
            return (char) value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method is for internal use only", false)]
        public static char? ReadNullableChar(object value)
        {
            if ((value == null) || (value is DBNull))
            {
                return null;
            }
            string str = value as string;
            if ((str != null) && (str.Length == 1))
            {
                return new char?(str[0]);
            }
            if (!(value is char))
            {
                throw new ArgumentException("A single-character was expected", "value");
            }
            return new char?((char) value);
        }

        public static void RemoveTypeMap(Type type)
        {
            Dictionary<Type, DbType> typeMap = SqlMapper.typeMap;
            if (typeMap.ContainsKey(type))
            {
                Dictionary<Type, DbType> dictionary2 = new Dictionary<Type, DbType>(typeMap);
                dictionary2.Remove(type);
                SqlMapper.typeMap = dictionary2;
            }
        }

        public static void ReplaceLiterals(this IParameterLookup parameters, IDbCommand command)
        {
            IList<LiteralToken> literalTokens = GetLiteralTokens(command.CommandText);
            if (literalTokens.Count != 0)
            {
                ReplaceLiterals(parameters, command, literalTokens);
            }
        }

        internal static void ReplaceLiterals(IParameterLookup parameters, IDbCommand command, IList<LiteralToken> tokens)
        {
            string commandText = command.CommandText;
            foreach (LiteralToken token in tokens)
            {
                object obj2 = parameters[token.Member];
                string newValue = Format(obj2);
                commandText = commandText.Replace(token.Token, newValue);
            }
            command.CommandText = commandText;
        }

        public static void ResetTypeHandlers()
        {
            ResetTypeHandlers(true);
        }

        private static void ResetTypeHandlers(bool clone)
        {
            typeHandlers = new Dictionary<Type, ITypeHandler>();
            AddTypeHandlerImpl(typeof(DataTable), new DataTableHandler(), clone);
            AddTypeHandlerImpl(typeof(XmlDocument), new XmlDocumentHandler(), clone);
            AddTypeHandlerImpl(typeof(XDocument), new XDocumentHandler(), clone);
            AddTypeHandlerImpl(typeof(XElement), new XElementHandler(), clone);
        }

        private static MethodInfo ResolveOperator(MethodInfo[] methods, Type from, Type to, string name)
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if ((methods[i].Name == name) && (methods[i].ReturnType == to))
                {
                    ParameterInfo[] parameters = methods[i].GetParameters();
                    if ((parameters.Length == 1) && (parameters[0].ParameterType == from))
                    {
                        return methods[i];
                    }
                }
            }
            return null;
        }

        [Obsolete("This method is for internal use only", false)]
        public static object SanitizeParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            if (value is Enum)
            {
                TypeCode code = !(value is IConvertible) ? Type.GetTypeCode(Enum.GetUnderlyingType(value.GetType())) : ((IConvertible) value).GetTypeCode();
                switch (code)
                {
                    case TypeCode.SByte:
                        return (sbyte) value;

                    case TypeCode.Byte:
                        return (byte) value;

                    case TypeCode.Int16:
                        return (short) value;

                    case TypeCode.UInt16:
                        return (ushort) value;

                    case TypeCode.Int32:
                        return (int) value;

                    case TypeCode.UInt32:
                        return (uint) value;

                    case TypeCode.Int64:
                        return (long) value;

                    case TypeCode.UInt64:
                        return (ulong) value;

                    default:
                        break;
                }
            }
            return value;
        }

        private static void SetQueryCache(Identity key, CacheInfo value)
        {
            if (Interlocked.Increment(ref collect) == 0x3e8)
            {
                CollectCacheGarbage();
            }
            _queryCache[key] = value;
        }

        public static void SetTypeMap(Type type, ITypeMap map)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if ((map != null) && !(map is DefaultTypeMap))
            {
                Hashtable hashtable2 = _typeMaps;
                lock (hashtable2)
                {
                    _typeMaps[type] = map;
                }
            }
            else
            {
                Hashtable hashtable = _typeMaps;
                lock (hashtable)
                {
                    _typeMaps.Remove(type);
                }
            }
            PurgeQueryCacheByType(type);
        }

        public static void SetTypeName(this DataTable table, string typeName)
        {
            if (table != null)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    table.ExtendedProperties.Remove("dapper:TypeName");
                }
                else
                {
                    table.ExtendedProperties["dapper:TypeName"] = typeName;
                }
            }
        }

        private static bool ShouldPassByPosition(string sql) => 
            (sql != null) && ((sql.IndexOf('?') >= 0) && pseudoPositional.IsMatch(sql));

        [Obsolete("This method is for internal use only", false)]
        public static void ThrowDataException(Exception ex, int index, IDataReader reader, object value)
        {
            Exception exception;
            try
            {
                string name = "(n/a)";
                string message = "(n/a)";
                if ((reader != null) && ((index >= 0) && (index < reader.FieldCount)))
                {
                    name = reader.GetName(index);
                    try
                    {
                        message = ((value == null) || (value is DBNull)) ? "<null>" : (Convert.ToString(value) + " - " + Type.GetTypeCode(value.GetType()).ToString());
                    }
                    catch (Exception exception1)
                    {
                        message = exception1.Message;
                    }
                }
                exception = new DataException($"Error parsing column {index} ({name}={message})", ex);
            }
            catch
            {
                exception = new DataException(ex.Message, ex);
            }
            throw exception;
        }

        private static void ThrowMultipleRows(Row row)
        {
            if (row == Row.Single)
            {
                ErrTwoRows.Single<int>();
            }
            else
            {
                if (row != Row.SingleOrDefault)
                {
                    throw new InvalidOperationException();
                }
                ErrTwoRows.SingleOrDefault<int>();
            }
        }

        private static void ThrowZeroRows(Row row)
        {
            if (row == Row.First)
            {
                ErrZeroRows.First<int>();
            }
            else
            {
                if (row != Row.Single)
                {
                    throw new InvalidOperationException();
                }
                ErrZeroRows.Single<int>();
            }
        }

        private static bool TryGetQueryCache(Identity key, out CacheInfo value)
        {
            if (_queryCache.TryGetValue(key, out value))
            {
                value.RecordHit();
                return true;
            }
            value = null;
            return false;
        }

        private static Task TryOpenAsync(this IDbConnection cnn, CancellationToken cancel)
        {
            DbConnection connection = cnn as DbConnection;
            if (connection == null)
            {
                throw new InvalidOperationException("Async operations require use of a DbConnection or an already-open IDbConnection");
            }
            return connection.OpenAsync(cancel);
        }

        private static DbCommand TrySetupAsyncCommand(this CommandDefinition command, IDbConnection cnn, Action<IDbCommand, object> paramReader)
        {
            DbCommand command2 = command.SetupCommand(cnn, paramReader) as DbCommand;
            if (command2 == null)
            {
                throw new InvalidOperationException("Async operations require use of a DbConnection or an IDbConnection where .CreateCommand() returns a DbCommand");
            }
            return command2;
        }

        private static bool TryStringSplit(ref IEnumerable list, int splitAt, string namePrefix, IDbCommand command, bool byPosition)
        {
            if ((list == null) || (splitAt < 0))
            {
                return false;
            }
            IEnumerable<int> enumerable = list as IEnumerable<int>;
            if (enumerable != null)
            {
                Action<StringBuilder, int> action4 = <>c.<>9__176_0;
                if (<>c.<>9__176_0 == null)
                {
                    Action<StringBuilder, int> local1 = <>c.<>9__176_0;
                    action4 = <>c.<>9__176_0 = delegate (StringBuilder sb, int i) {
                        sb.Append(i.ToString(CultureInfo.InvariantCulture));
                    };
                }
                return TryStringSplit<int>(ref enumerable, splitAt, namePrefix, command, "int", byPosition, action4);
            }
            IEnumerable<long> enumerable2 = list as IEnumerable<long>;
            if (enumerable2 != null)
            {
                Action<StringBuilder, long> action3 = <>c.<>9__176_1;
                if (<>c.<>9__176_1 == null)
                {
                    Action<StringBuilder, long> local2 = <>c.<>9__176_1;
                    action3 = <>c.<>9__176_1 = delegate (StringBuilder sb, long i) {
                        sb.Append(i.ToString(CultureInfo.InvariantCulture));
                    };
                }
                return TryStringSplit<long>(ref enumerable2, splitAt, namePrefix, command, "bigint", byPosition, action3);
            }
            IEnumerable<short> enumerable3 = list as IEnumerable<short>;
            if (enumerable3 != null)
            {
                Action<StringBuilder, short> action2 = <>c.<>9__176_2;
                if (<>c.<>9__176_2 == null)
                {
                    Action<StringBuilder, short> local3 = <>c.<>9__176_2;
                    action2 = <>c.<>9__176_2 = delegate (StringBuilder sb, short i) {
                        sb.Append(i.ToString(CultureInfo.InvariantCulture));
                    };
                }
                return TryStringSplit<short>(ref enumerable3, splitAt, namePrefix, command, "smallint", byPosition, action2);
            }
            IEnumerable<byte> enumerable4 = list as IEnumerable<byte>;
            if (enumerable4 == null)
            {
                return false;
            }
            Action<StringBuilder, byte> append = <>c.<>9__176_3;
            if (<>c.<>9__176_3 == null)
            {
                Action<StringBuilder, byte> local4 = <>c.<>9__176_3;
                append = <>c.<>9__176_3 = delegate (StringBuilder sb, byte i) {
                    sb.Append(i.ToString(CultureInfo.InvariantCulture));
                };
            }
            return TryStringSplit<byte>(ref enumerable4, splitAt, namePrefix, command, "tinyint", byPosition, append);
        }

        private static bool TryStringSplit<T>(ref IEnumerable<T> list, int splitAt, string namePrefix, IDbCommand command, string colType, bool byPosition, Action<StringBuilder, T> append)
        {
            string str3;
            ICollection<T> is2 = list as ICollection<T>;
            if (is2 == null)
            {
                is2 = list.ToList<T>();
                list = is2;
            }
            if (is2.Count < splitAt)
            {
                return false;
            }
            string varName = null;
            string inListRegex = GetInListRegex(namePrefix, byPosition);
            string str2 = Regex.Replace(command.CommandText, inListRegex, delegate (Match match) {
                string str = match.Groups[1].Value;
                if (match.Groups[2].Success)
                {
                    return match.Value;
                }
                varName = str;
                string[] textArray1 = new string[] { "(select cast([value] as ", colType, ") from string_split(", str, ",','))" };
                return string.Concat(textArray1);
            }, RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (varName == null)
            {
                return false;
            }
            command.CommandText = str2;
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = namePrefix;
            parameter.DbType = DbType.AnsiString;
            parameter.Size = -1;
            using (IEnumerator<T> enumerator = is2.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    str3 = "";
                }
                else
                {
                    StringBuilder stringBuilder = GetStringBuilder();
                    append(stringBuilder, enumerator.Current);
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            str3 = stringBuilder.ToString();
                            break;
                        }
                        append(stringBuilder.Append(','), enumerator.Current);
                    }
                }
            }
            parameter.Value = str3;
            command.Parameters.Add(parameter);
            return true;
        }

        public static IEqualityComparer<string> ConnectionStringComparer
        {
            get => 
                connectionStringComparer;
            set => 
                connectionStringComparer = value ?? StringComparer.Ordinal;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SqlMapper.<>c <>9 = new SqlMapper.<>c();
            public static Func<KeyValuePair<SqlMapper.Identity, SqlMapper.CacheInfo>, Tuple<string, string, int>> <>9__87_0;
            public static Func<KeyValuePair<int, int>, bool> <>9__88_0;
            public static Func<KeyValuePair<int, int>, Tuple<int, int>> <>9__88_1;
            public static Func<string, string> <>9__160_0;
            public static MatchEvaluator <>9__175_1;
            public static Action<StringBuilder, int> <>9__176_0;
            public static Action<StringBuilder, long> <>9__176_1;
            public static Action<StringBuilder, short> <>9__176_2;
            public static Action<StringBuilder, byte> <>9__176_3;

            internal TypeCode <.cctor>b__90_0(Type x) => 
                Type.GetTypeCode(x);

            internal MethodInfo <.cctor>b__90_1(Type x)
            {
                Type[] types = new Type[] { typeof(IFormatProvider) };
                return x.GetPublicInstanceMethod("ToString", types);
            }

            internal bool <.cctor>b__90_2(PropertyInfo p) => 
                (p.GetIndexParameters().Length != 0) && (p.GetIndexParameters()[0].ParameterType == typeof(int));

            internal MethodInfo <.cctor>b__90_3(PropertyInfo p) => 
                p.GetGetMethod();

            internal SqlMapper.ITypeMap <.cctor>b__90_4(Type type) => 
                new DefaultTypeMap(type);

            internal string <GenerateDeserializers>b__160_0(string s) => 
                s.Trim();

            internal Tuple<string, string, int> <GetCachedSQL>b__87_0(KeyValuePair<SqlMapper.Identity, SqlMapper.CacheInfo> pair) => 
                Tuple.Create<string, string, int>(pair.Key.connectionString, pair.Key.sql, pair.Value.GetHitCount());

            internal bool <GetHashCollissions>b__88_0(KeyValuePair<int, int> pair) => 
                pair.Value > 1;

            internal Tuple<int, int> <GetHashCollissions>b__88_1(KeyValuePair<int, int> pair) => 
                Tuple.Create<int, int>(pair.Key, pair.Value);

            internal string <PackListParameters>b__175_1(Match match)
            {
                string str = match.Groups[1].Value;
                return (!match.Groups[2].Success ? ("(SELECT " + str + " WHERE 1 = 0)") : match.Value);
            }

            internal void <TryStringSplit>b__176_0(StringBuilder sb, int i)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
            }

            internal void <TryStringSplit>b__176_1(StringBuilder sb, long i)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
            }

            internal void <TryStringSplit>b__176_2(StringBuilder sb, short i)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
            }

            internal void <TryStringSplit>b__176_3(StringBuilder sb, byte i)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
            }
        }

        [CompilerGenerated]
        private struct <ExecuteImplAsync>d__39 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<int> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            public object param;
            private bool <wasClosed>5__2;
            private DbCommand <cmd>5__3;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num > 1)
                    {
                        Type type;
                        if (this.param != null)
                        {
                            type = this.param.GetType();
                        }
                        else
                        {
                            object param = this.param;
                            type = null;
                        }
                        SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, null, type), this.param, this.command.AddToCache);
                        this.<wasClosed>5__2 = this.cnn.State == ConnectionState.Closed;
                        this.<cmd>5__3 = this.command.TrySetupAsyncCommand(this.cnn, info.ParamReader);
                    }
                    try
                    {
                        int num5 = num;
                        try
                        {
                            int num3;
                            ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                            ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
                            if (num == 0)
                            {
                                awaiter = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_000E;
                            }
                            else
                            {
                                if (num == 1)
                                {
                                    awaiter2 = this.<>u__2;
                                    this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_000B;
                                }
                                else
                                {
                                    if (!this.<wasClosed>5__2)
                                    {
                                        goto TR_000C;
                                    }
                                    else
                                    {
                                        awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter.IsCompleted)
                                        {
                                            goto TR_000E;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 0;
                                            this.<>u__1 = awaiter;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<ExecuteImplAsync>d__39>(ref awaiter, ref this);
                                        }
                                    }
                                    return;
                                }
                                goto TR_000E;
                            }
                            goto TR_000C;
                        TR_000B:
                            num3 = awaiter2.GetResult();
                            this.command.OnCompleted();
                            int result = num3;
                            this.<>1__state = -2;
                            this.<>t__builder.SetResult(result);
                            return;
                        TR_000C:
                            awaiter2 = this.<cmd>5__3.ExecuteNonQueryAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_000B;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteImplAsync>d__39>(ref awaiter2, ref this);
                            }
                            return;
                        TR_000E:
                            awaiter.GetResult();
                            goto TR_000C;
                        }
                        finally
                        {
                            if ((num < 0) && this.<wasClosed>5__2)
                            {
                                this.cnn.Close();
                            }
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<cmd>5__3 != null))
                        {
                            this.<cmd>5__3.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteMultiImplAsync>d__38 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<int> <>t__builder;
            public IDbConnection cnn;
            public CommandDefinition command;
            public IEnumerable multiExec;
            private bool <isFirst>5__2;
            private int <total>5__3;
            private bool <wasClosed>5__4;
            private SqlMapper.CacheInfo <info>5__5;
            private string <masterSql>5__6;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private Queue<SqlMapper.AsyncExecState> <pending>5__7;
            private DbCommand <cmd>5__8;
            private IEnumerator <>7__wrap8;
            private object <obj>5__10;
            private SqlMapper.AsyncExecState <recycled>5__11;
            private int <>7__wrap11;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    int num2;
                    if (num > 3)
                    {
                        this.<isFirst>5__2 = true;
                        this.<total>5__3 = 0;
                        this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        IDisposable disposable;
                        switch (num)
                        {
                            case 0:
                                awaiter = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_005B;

                            case 1:
                            case 2:
                                goto TR_003C;

                            case 3:
                                goto TR_0057;

                            default:
                                if (!this.<wasClosed>5__4)
                                {
                                    goto TR_0059;
                                }
                                else
                                {
                                    awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_005B;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<ExecuteMultiImplAsync>d__38>(ref awaiter, ref this);
                                    }
                                }
                                break;
                        }
                        return;
                    TR_001B:
                        this.command.OnCompleted();
                        this.<info>5__5 = null;
                        this.<masterSql>5__6 = null;
                        goto TR_001A;
                    TR_001C:
                        this.<pending>5__7 = null;
                        this.<cmd>5__8 = null;
                        goto TR_001B;
                    TR_003C:
                        try
                        {
                            int num4;
                            ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
                            if (num == 1)
                            {
                                goto TR_0037;
                            }
                            else if (num == 2)
                            {
                                awaiter3 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                            }
                            else
                            {
                                this.<>7__wrap8 = this.multiExec.GetEnumerator();
                                goto TR_0037;
                            }
                        TR_0021:
                            num4 = awaiter3.GetResult();
                            this.<total>5__3 = this.<>7__wrap11 + num4;
                        TR_0027:
                            while (true)
                            {
                                if (this.<pending>5__7.Count != 0)
                                {
                                    SqlMapper.AsyncExecState state = this.<pending>5__7.Dequeue();
                                    DbCommand command = state.Command;
                                    try
                                    {
                                    }
                                    finally
                                    {
                                        if ((num < 0) && (command != null))
                                        {
                                            command.Dispose();
                                        }
                                    }
                                    this.<>7__wrap11 = this.<total>5__3;
                                    awaiter3 = state.Task.ConfigureAwait(false).GetAwaiter();
                                    if (awaiter3.IsCompleted)
                                    {
                                        goto TR_0021;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 2;
                                        this.<>u__2 = awaiter3;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteMultiImplAsync>d__38>(ref awaiter3, ref this);
                                    }
                                    break;
                                }
                                else
                                {
                                    goto TR_001C;
                                }
                                goto TR_0021;
                            }
                            return;
                        TR_0028:
                            this.<>7__wrap8 = null;
                            goto TR_0027;
                        TR_0037:
                            try
                            {
                                int num3;
                                ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
                                if (num == 1)
                                {
                                    awaiter2 = this.<>u__2;
                                    this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_002C;
                                }
                                goto TR_0034;
                            TR_0029:
                                this.<info>5__5.ParamReader(this.<cmd>5__8, this.<obj>5__10);
                                Task<int> task = this.<cmd>5__8.ExecuteNonQueryAsync(this.command.CancellationToken);
                                this.<pending>5__7.Enqueue(new SqlMapper.AsyncExecState(this.<cmd>5__8, task));
                                this.<cmd>5__8 = null;
                                this.<obj>5__10 = null;
                                goto TR_0034;
                            TR_002C:
                                num3 = awaiter2.GetResult();
                                this.<total>5__3 = this.<>7__wrap11 + num3;
                                this.<cmd>5__8 = this.<recycled>5__11.Command;
                                this.<cmd>5__8.CommandText = this.<masterSql>5__6;
                                this.<cmd>5__8.Parameters.Clear();
                                this.<recycled>5__11 = new SqlMapper.AsyncExecState();
                                goto TR_0029;
                            TR_0034:
                                while (true)
                                {
                                    if (this.<>7__wrap8.MoveNext())
                                    {
                                        this.<obj>5__10 = this.<>7__wrap8.Current;
                                        if (!this.<isFirst>5__2)
                                        {
                                            if (this.<pending>5__7.Count < 100)
                                            {
                                                this.<cmd>5__8 = this.command.TrySetupAsyncCommand(this.cnn, null);
                                                goto TR_0029;
                                            }
                                            else
                                            {
                                                this.<recycled>5__11 = this.<pending>5__7.Dequeue();
                                                this.<>7__wrap11 = this.<total>5__3;
                                                awaiter2 = this.<recycled>5__11.Task.ConfigureAwait(false).GetAwaiter();
                                                if (awaiter2.IsCompleted)
                                                {
                                                    goto TR_002C;
                                                }
                                                else
                                                {
                                                    this.<>1__state = num = 1;
                                                    this.<>u__2 = awaiter2;
                                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteMultiImplAsync>d__38>(ref awaiter2, ref this);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.<isFirst>5__2 = false;
                                            this.<cmd>5__8 = this.command.TrySetupAsyncCommand(this.cnn, null);
                                            this.<masterSql>5__6 = this.<cmd>5__8.CommandText;
                                            SqlMapper.Identity identity = new SqlMapper.Identity(this.command.CommandText, new CommandType?(this.<cmd>5__8.CommandType), this.cnn, null, this.<obj>5__10.GetType());
                                            this.<info>5__5 = SqlMapper.GetCacheInfo(identity, this.<obj>5__10, this.command.AddToCache);
                                            goto TR_0029;
                                        }
                                    }
                                    else
                                    {
                                        goto TR_0028;
                                    }
                                    break;
                                }
                                return;
                            }
                            finally
                            {
                                if (num < 0)
                                {
                                    disposable = this.<>7__wrap8 as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                            }
                        }
                        finally
                        {
                            if (num < 0)
                            {
                                DbCommand command2 = this.<cmd>5__8;
                                try
                                {
                                }
                                finally
                                {
                                    if ((num < 0) && (command2 != null))
                                    {
                                        command2.Dispose();
                                    }
                                }
                                while (this.<pending>5__7.Count != 0)
                                {
                                    DbCommand command = this.<pending>5__7.Dequeue().Command;
                                    try
                                    {
                                    }
                                    finally
                                    {
                                        if ((num < 0) && (command != null))
                                        {
                                            command.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    TR_0046:
                        this.<cmd>5__8 = null;
                        goto TR_001B;
                    TR_0057:
                        try
                        {
                            if (num != 3)
                            {
                                this.<>7__wrap8 = this.multiExec.GetEnumerator();
                            }
                            try
                            {
                                int num5;
                                ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter4;
                                if (num == 3)
                                {
                                    awaiter4 = this.<>u__2;
                                    this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                }
                                else
                                {
                                    goto TR_0051;
                                }
                            TR_0049:
                                num5 = awaiter4.GetResult();
                                this.<total>5__3 = this.<>7__wrap11 + num5;
                            TR_0051:
                                while (true)
                                {
                                    if (this.<>7__wrap8.MoveNext())
                                    {
                                        object current = this.<>7__wrap8.Current;
                                        if (!this.<isFirst>5__2)
                                        {
                                            this.<cmd>5__8.CommandText = this.<masterSql>5__6;
                                            this.<cmd>5__8.Parameters.Clear();
                                        }
                                        else
                                        {
                                            this.<masterSql>5__6 = this.<cmd>5__8.CommandText;
                                            this.<isFirst>5__2 = false;
                                            SqlMapper.Identity identity = new SqlMapper.Identity(this.command.CommandText, new CommandType?(this.<cmd>5__8.CommandType), this.cnn, null, current.GetType());
                                            this.<info>5__5 = SqlMapper.GetCacheInfo(identity, current, this.command.AddToCache);
                                        }
                                        this.<info>5__5.ParamReader(this.<cmd>5__8, current);
                                        this.<>7__wrap11 = this.<total>5__3;
                                        awaiter4 = this.<cmd>5__8.ExecuteNonQueryAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter4.IsCompleted)
                                        {
                                            goto TR_0049;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 3;
                                            this.<>u__2 = awaiter4;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteMultiImplAsync>d__38>(ref awaiter4, ref this);
                                        }
                                    }
                                    else
                                    {
                                        goto TR_0047;
                                    }
                                    break;
                                }
                                return;
                            }
                            finally
                            {
                                if (num < 0)
                                {
                                    disposable = this.<>7__wrap8 as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                            }
                        TR_0047:
                            this.<>7__wrap8 = null;
                            goto TR_0046;
                        }
                        finally
                        {
                            if ((num < 0) && (this.<cmd>5__8 != null))
                            {
                                this.<cmd>5__8.Dispose();
                            }
                        }
                    TR_0059:
                        this.<info>5__5 = null;
                        this.<masterSql>5__6 = null;
                        if ((this.command.Flags & CommandFlags.Pipelined) == CommandFlags.None)
                        {
                            this.<cmd>5__8 = this.command.TrySetupAsyncCommand(this.cnn, null);
                            goto TR_0057;
                        }
                        else
                        {
                            this.<pending>5__7 = new Queue<SqlMapper.AsyncExecState>(100);
                            this.<cmd>5__8 = null;
                        }
                        goto TR_003C;
                    TR_005B:
                        awaiter.GetResult();
                        goto TR_0059;
                    }
                    finally
                    {
                        if ((num < 0) && this.<wasClosed>5__4)
                        {
                            this.cnn.Close();
                        }
                    }
                    return;
                TR_001A:
                    num2 = this.<total>5__3;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(num2);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private sealed class <ExecuteReaderSync>d__55<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private Func<IDataReader, object> func;
            public Func<IDataReader, object> <>3__func;
            private object parameters;
            public object <>3__parameters;
            private IDataReader <>7__wrap1;

            [DebuggerHidden]
            public <ExecuteReaderSync>d__55(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.reader;
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (this.reader.Read())
                    {
                        this.<>2__current = (T) this.func(this.reader);
                        this.<>1__state = 1;
                        flag = true;
                    }
                    else
                    {
                        while (true)
                        {
                            if (!this.reader.NextResult())
                            {
                                SqlMapper.IParameterCallbacks parameters = this.parameters as SqlMapper.IParameterCallbacks;
                                if (parameters == null)
                                {
                                    SqlMapper.IParameterCallbacks local1 = parameters;
                                }
                                else
                                {
                                    parameters.OnCompleted();
                                }
                                this.<>m__Finally1();
                                this.<>7__wrap1 = null;
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                SqlMapper.<ExecuteReaderSync>d__55<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<ExecuteReaderSync>d__55<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (SqlMapper.<ExecuteReaderSync>d__55<T>) this;
                }
                d__.reader = this.<>3__reader;
                d__.func = this.<>3__func;
                d__.parameters = this.<>3__parameters;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private struct <ExecuteScalarImplAsync>d__69<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            private DbCommand <cmd>5__2;
            private bool <wasClosed>5__3;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    T local;
                    Action<IDbCommand, object> paramReader;
                    object obj3;
                    if (num > 1)
                    {
                        paramReader = null;
                        object parameters = this.command.Parameters;
                        if (parameters != null)
                        {
                            paramReader = SqlMapper.GetCacheInfo(new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, null, parameters.GetType()), this.command.Parameters, this.command.AddToCache).ParamReader;
                        }
                        this.<cmd>5__2 = null;
                        this.<wasClosed>5__3 = this.cnn.State == ConnectionState.Closed;
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0010;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_000D;
                            }
                            else
                            {
                                this.<cmd>5__2 = this.command.TrySetupAsyncCommand(this.cnn, paramReader);
                                if (!this.<wasClosed>5__3)
                                {
                                    goto TR_000E;
                                }
                                else
                                {
                                    awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0010;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<ExecuteScalarImplAsync>d__69<T>>(ref awaiter, ref (SqlMapper.<ExecuteScalarImplAsync>d__69<T>) ref this);
                                    }
                                }
                                return;
                            }
                            goto TR_0010;
                        }
                        goto TR_000E;
                    TR_000D:
                        obj3 = awaiter2.GetResult();
                        this.command.OnCompleted();
                        goto TR_000C;
                    TR_000E:
                        awaiter2 = this.<cmd>5__2.ExecuteScalarAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter2.IsCompleted)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            this.<>1__state = num = 1;
                            this.<>u__2 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteScalarImplAsync>d__69<T>>(ref awaiter2, ref (SqlMapper.<ExecuteScalarImplAsync>d__69<T>) ref this);
                        }
                        return;
                    TR_0010:
                        awaiter.GetResult();
                        goto TR_000E;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            if (this.<wasClosed>5__3)
                            {
                                this.cnn.Close();
                            }
                            if (this.<cmd>5__2 == null)
                            {
                                DbCommand local1 = this.<cmd>5__2;
                            }
                            else
                            {
                                this.<cmd>5__2.Dispose();
                            }
                        }
                    }
                    return;
                TR_000C:
                    local = SqlMapper.Parse<T>(obj3);
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(local);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteWrappedReaderImplAsync>d__64 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<DbDataReader> <>t__builder;
            public IDbConnection cnn;
            public CommandDefinition command;
            public CommandBehavior commandBehavior;
            private DbCommand <cmd>5__2;
            private bool <wasClosed>5__3;
            private bool <disposeCommand>5__4;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    Action<IDbCommand, object> parameterReader;
                    if (num > 1)
                    {
                        parameterReader = SqlMapper.GetParameterReader(this.cnn, ref this.command);
                        this.<cmd>5__2 = null;
                        this.<wasClosed>5__3 = this.cnn.State == ConnectionState.Closed;
                        this.<disposeCommand>5__4 = true;
                    }
                    try
                    {
                        DbDataReader reader2;
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000E;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_000B;
                            }
                            else
                            {
                                this.<cmd>5__2 = this.command.TrySetupAsyncCommand(this.cnn, parameterReader);
                                if (!this.<wasClosed>5__3)
                                {
                                    goto TR_000C;
                                }
                                else
                                {
                                    awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_000E;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<ExecuteWrappedReaderImplAsync>d__64>(ref awaiter, ref this);
                                    }
                                }
                                return;
                            }
                            goto TR_000E;
                        }
                        goto TR_000C;
                    TR_000B:
                        reader2 = awaiter2.GetResult();
                        this.<wasClosed>5__3 = false;
                        this.<disposeCommand>5__4 = false;
                        DbDataReader result = WrappedReader.Create(this.<cmd>5__2, reader2);
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    TR_000C:
                        awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__2, this.<wasClosed>5__3, this.commandBehavior, this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter2.IsCompleted)
                        {
                            goto TR_000B;
                        }
                        else
                        {
                            this.<>1__state = num = 1;
                            this.<>u__2 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<ExecuteWrappedReaderImplAsync>d__64>(ref awaiter2, ref this);
                        }
                        return;
                    TR_000E:
                        awaiter.GetResult();
                        goto TR_000C;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            if (this.<wasClosed>5__3)
                            {
                                this.cnn.Close();
                            }
                            if ((this.<cmd>5__2 != null) & this.<disposeCommand>5__4)
                            {
                                this.<cmd>5__2.Dispose();
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IEnumerable<TReturn>> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            public Delegate map;
            public string splitOn;
            private SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh> <identity>5__2;
            private SqlMapper.CacheInfo <info>5__3;
            private bool <wasClosed>5__4;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private DbCommand <cmd>5__5;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num > 1)
                    {
                        object parameters = this.command.Parameters;
                        this.<identity>5__2 = new SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this.command.CommandText, this.command.CommandType, this.cnn, typeof(TFirst), parameters?.GetType(), 0);
                        this.<info>5__3 = SqlMapper.GetCacheInfo(this.<identity>5__2, parameters, this.command.AddToCache);
                        this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                goto TR_0017;
                            }
                            else if (!this.<wasClosed>5__4)
                            {
                                goto TR_0018;
                            }
                            else
                            {
                                awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0019;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>>(ref awaiter, ref (SqlMapper.<MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>) ref this);
                                }
                            }
                            return;
                        }
                        goto TR_0019;
                    TR_0017:
                        try
                        {
                            IEnumerable<TReturn> enumerable;
                            DbDataReader reader;
                            ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0012;
                            }
                            else
                            {
                                awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__5, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto TR_0012;
                                }
                                else
                                {
                                    this.<>1__state = num = 1;
                                    this.<>u__2 = awaiter2;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>>(ref awaiter2, ref (SqlMapper.<MultiMapAsync>d__52<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>) ref this);
                                }
                            }
                            return;
                        TR_0012:
                            reader = awaiter2.GetResult();
                            try
                            {
                                if (!this.command.Buffered)
                                {
                                    this.<wasClosed>5__4 = false;
                                }
                                IEnumerable<TReturn> source = null.MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(CommandDefinition.ForCallback(this.command.Parameters), this.map, this.splitOn, reader, this.<identity>5__2, true);
                                enumerable = this.command.Buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source;
                            }
                            finally
                            {
                                if ((num < 0) && (reader != null))
                                {
                                    reader.Dispose();
                                }
                            }
                            this.<>1__state = -2;
                            this.<>t__builder.SetResult(enumerable);
                            return;
                        }
                        finally
                        {
                            if ((num < 0) && (this.<cmd>5__5 != null))
                            {
                                this.<cmd>5__5.Dispose();
                            }
                        }
                        return;
                    TR_0018:
                        this.<cmd>5__5 = this.command.TrySetupAsyncCommand(this.cnn, this.<info>5__3.ParamReader);
                        goto TR_0017;
                    TR_0019:
                        awaiter.GetResult();
                        goto TR_0018;
                    }
                    finally
                    {
                        if ((num < 0) && this.<wasClosed>5__4)
                        {
                            this.cnn.Close();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <MultiMapAsync>d__54<TReturn> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IEnumerable<TReturn>> <>t__builder;
            public Type[] types;
            public CommandDefinition command;
            public IDbConnection cnn;
            public Func<object[], TReturn> map;
            public string splitOn;
            private SqlMapper.IdentityWithTypes <identity>5__2;
            private SqlMapper.CacheInfo <info>5__3;
            private bool <wasClosed>5__4;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private DbCommand <cmd>5__5;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num > 1)
                    {
                        if (this.types.Length < 1)
                        {
                            throw new ArgumentException("you must provide at least one type to deserialize");
                        }
                        object parameters = this.command.Parameters;
                        this.<identity>5__2 = new SqlMapper.IdentityWithTypes(this.command.CommandText, this.command.CommandType, this.cnn, this.types[0], parameters?.GetType(), this.types, 0);
                        this.<info>5__3 = SqlMapper.GetCacheInfo(this.<identity>5__2, parameters, this.command.AddToCache);
                        this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                goto TR_0016;
                            }
                            else if (!this.<wasClosed>5__4)
                            {
                                goto TR_0017;
                            }
                            else
                            {
                                awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0018;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<MultiMapAsync>d__54<TReturn>>(ref awaiter, ref (SqlMapper.<MultiMapAsync>d__54<TReturn>) ref this);
                                }
                            }
                            return;
                        }
                        goto TR_0018;
                    TR_0016:
                        try
                        {
                            IEnumerable<TReturn> enumerable;
                            DbDataReader reader;
                            ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0011;
                            }
                            else
                            {
                                awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__5, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto TR_0011;
                                }
                                else
                                {
                                    this.<>1__state = num = 1;
                                    this.<>u__2 = awaiter2;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<MultiMapAsync>d__54<TReturn>>(ref awaiter2, ref (SqlMapper.<MultiMapAsync>d__54<TReturn>) ref this);
                                }
                            }
                            return;
                        TR_0011:
                            reader = awaiter2.GetResult();
                            try
                            {
                                CommandDefinition command = new CommandDefinition();
                                IEnumerable<TReturn> source = null.MultiMapImpl<TReturn>(command, this.types, this.map, this.splitOn, reader, this.<identity>5__2, true);
                                enumerable = this.command.Buffered ? source.ToList<TReturn>() : source;
                            }
                            finally
                            {
                                if ((num < 0) && (reader != null))
                                {
                                    reader.Dispose();
                                }
                            }
                            this.<>1__state = -2;
                            this.<>t__builder.SetResult(enumerable);
                            return;
                        }
                        finally
                        {
                            if ((num < 0) && (this.<cmd>5__5 != null))
                            {
                                this.<cmd>5__5.Dispose();
                            }
                        }
                        return;
                    TR_0017:
                        this.<cmd>5__5 = this.command.TrySetupAsyncCommand(this.cnn, this.<info>5__3.ParamReader);
                        goto TR_0016;
                    TR_0018:
                        awaiter.GetResult();
                        goto TR_0017;
                    }
                    finally
                    {
                        if ((num < 0) && this.<wasClosed>5__4)
                        {
                            this.cnn.Close();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private sealed class <MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> : IEnumerable<TReturn>, IEnumerable, IEnumerator<TReturn>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TReturn <>2__current;
            private int <>l__initialThreadId;
            private CommandDefinition command;
            public CommandDefinition <>3__command;
            private SqlMapper.Identity identity;
            public SqlMapper.Identity <>3__identity;
            private IDbConnection cnn;
            public IDbConnection <>3__cnn;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private string splitOn;
            public string <>3__splitOn;
            private Delegate map;
            public Delegate <>3__map;
            private bool finalize;
            public bool <>3__finalize;
            private IDbCommand <ownedCommand>5__2;
            private IDataReader <ownedReader>5__3;
            private bool <wasClosed>5__4;
            private Func<IDataReader, TReturn> <mapIt>5__5;

            [DebuggerHidden]
            public <MultiMapImpl>d__155(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                try
                {
                    if (this.<ownedReader>5__3 == null)
                    {
                        IDataReader local1 = this.<ownedReader>5__3;
                    }
                    else
                    {
                        this.<ownedReader>5__3.Dispose();
                    }
                }
                finally
                {
                    if (this.<ownedCommand>5__2 == null)
                    {
                        IDbCommand local2 = this.<ownedCommand>5__2;
                    }
                    else
                    {
                        this.<ownedCommand>5__2.Dispose();
                    }
                    if (this.<wasClosed>5__4)
                    {
                        this.cnn.Close();
                    }
                }
            }

            private bool MoveNext()
            {
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        Func<IDataReader, object>[] funcArray;
                        bool flag1;
                        this.<>1__state = -1;
                        object parameters = this.command.Parameters;
                        SqlMapper.Identity identity = this.identity;
                        if (this.identity == null)
                        {
                            SqlMapper.Identity local1 = this.identity;
                            identity = new SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this.command.CommandText, this.command.CommandType, this.cnn, typeof(TFirst), parameters?.GetType(), 0);
                        }
                        this.identity = identity;
                        SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(this.identity, parameters, this.command.AddToCache);
                        this.<ownedCommand>5__2 = null;
                        this.<ownedReader>5__3 = null;
                        if (this.cnn != null)
                        {
                            flag1 = this.cnn.State == ConnectionState.Closed;
                        }
                        else
                        {
                            IDbConnection cnn = this.cnn;
                            flag1 = false;
                        }
                        this.<wasClosed>5__4 = flag1;
                        this.<>1__state = -3;
                        if (this.reader == null)
                        {
                            this.<ownedCommand>5__2 = this.command.SetupCommand(this.cnn, info.ParamReader);
                            if (this.<wasClosed>5__4)
                            {
                                this.cnn.Open();
                            }
                            this.<ownedReader>5__3 = SqlMapper.ExecuteReaderWithFlagsFallback(this.<ownedCommand>5__2, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult);
                            this.reader = this.<ownedReader>5__3;
                        }
                        SqlMapper.DeserializerState state = new SqlMapper.DeserializerState();
                        int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                        if (((state = info.Deserializer).Func == null) || (((funcArray = info.OtherDeserializers) == null) || (hash != state.Hash)))
                        {
                            Func<IDataReader, object>[] source = SqlMapper.GenerateDeserializers(this.identity, this.splitOn, this.reader);
                            state = info.Deserializer = new SqlMapper.DeserializerState(hash, source[0]);
                            funcArray = info.OtherDeserializers = source.Skip<Func<IDataReader, object>>(1).ToArray<Func<IDataReader, object>>();
                            if (this.command.AddToCache)
                            {
                                SqlMapper.SetQueryCache(this.identity, info);
                            }
                        }
                        this.<mapIt>5__5 = SqlMapper.GenerateMapper<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(state.Func, funcArray, this.map);
                        if (this.<mapIt>5__5 == null)
                        {
                            goto TR_0003;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (this.reader.Read())
                    {
                        this.<>2__current = this.<mapIt>5__5(this.reader);
                        this.<>1__state = 1;
                        return true;
                    }
                    else if (this.finalize)
                    {
                        while (true)
                        {
                            if (!this.reader.NextResult())
                            {
                                this.command.OnCompleted();
                                break;
                            }
                        }
                    }
                TR_0003:
                    this.<mapIt>5__5 = null;
                    this.<>m__Finally1();
                    return false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
            }

            [DebuggerHidden]
            IEnumerator<TReturn> IEnumerable<TReturn>.GetEnumerator()
            {
                SqlMapper.<MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (SqlMapper.<MultiMapImpl>d__155<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>) this;
                }
                d__.cnn = this.<>3__cnn;
                d__.command = this.<>3__command;
                d__.map = this.<>3__map;
                d__.splitOn = this.<>3__splitOn;
                d__.reader = this.<>3__reader;
                d__.identity = this.<>3__identity;
                d__.finalize = this.<>3__finalize;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TReturn>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            TReturn IEnumerator<TReturn>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <MultiMapImpl>d__157<TReturn> : IEnumerable<TReturn>, IEnumerable, IEnumerator<TReturn>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TReturn <>2__current;
            private int <>l__initialThreadId;
            private Type[] types;
            public Type[] <>3__types;
            private CommandDefinition command;
            public CommandDefinition <>3__command;
            private SqlMapper.Identity identity;
            public SqlMapper.Identity <>3__identity;
            private IDbConnection cnn;
            public IDbConnection <>3__cnn;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private string splitOn;
            public string <>3__splitOn;
            private Func<object[], TReturn> map;
            public Func<object[], TReturn> <>3__map;
            private bool finalize;
            public bool <>3__finalize;
            private IDbCommand <ownedCommand>5__2;
            private IDataReader <ownedReader>5__3;
            private bool <wasClosed>5__4;
            private Func<IDataReader, TReturn> <mapIt>5__5;

            [DebuggerHidden]
            public <MultiMapImpl>d__157(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                try
                {
                    if (this.<ownedReader>5__3 == null)
                    {
                        IDataReader local1 = this.<ownedReader>5__3;
                    }
                    else
                    {
                        this.<ownedReader>5__3.Dispose();
                    }
                }
                finally
                {
                    if (this.<ownedCommand>5__2 == null)
                    {
                        IDbCommand local2 = this.<ownedCommand>5__2;
                    }
                    else
                    {
                        this.<ownedCommand>5__2.Dispose();
                    }
                    if (this.<wasClosed>5__4)
                    {
                        this.cnn.Close();
                    }
                }
            }

            private bool MoveNext()
            {
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        SqlMapper.DeserializerState state;
                        Func<IDataReader, object>[] funcArray;
                        bool flag1;
                        this.<>1__state = -1;
                        if (this.types.Length < 1)
                        {
                            throw new ArgumentException("you must provide at least one type to deserialize");
                        }
                        object parameters = this.command.Parameters;
                        SqlMapper.Identity identity = this.identity;
                        if (this.identity == null)
                        {
                            SqlMapper.Identity local1 = this.identity;
                            identity = new SqlMapper.IdentityWithTypes(this.command.CommandText, this.command.CommandType, this.cnn, this.types[0], parameters?.GetType(), this.types, 0);
                        }
                        this.identity = identity;
                        SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(this.identity, parameters, this.command.AddToCache);
                        this.<ownedCommand>5__2 = null;
                        this.<ownedReader>5__3 = null;
                        if (this.cnn != null)
                        {
                            flag1 = this.cnn.State == ConnectionState.Closed;
                        }
                        else
                        {
                            IDbConnection cnn = this.cnn;
                            flag1 = false;
                        }
                        this.<wasClosed>5__4 = flag1;
                        this.<>1__state = -3;
                        if (this.reader == null)
                        {
                            this.<ownedCommand>5__2 = this.command.SetupCommand(this.cnn, info.ParamReader);
                            if (this.<wasClosed>5__4)
                            {
                                this.cnn.Open();
                            }
                            this.<ownedReader>5__3 = SqlMapper.ExecuteReaderWithFlagsFallback(this.<ownedCommand>5__2, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult);
                            this.reader = this.<ownedReader>5__3;
                        }
                        int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                        if (((state = info.Deserializer).Func == null) || (((funcArray = info.OtherDeserializers) == null) || (hash != state.Hash)))
                        {
                            Func<IDataReader, object>[] source = SqlMapper.GenerateDeserializers(this.identity, this.splitOn, this.reader);
                            state = info.Deserializer = new SqlMapper.DeserializerState(hash, source[0]);
                            funcArray = info.OtherDeserializers = source.Skip<Func<IDataReader, object>>(1).ToArray<Func<IDataReader, object>>();
                            SqlMapper.SetQueryCache(this.identity, info);
                        }
                        this.<mapIt>5__5 = SqlMapper.GenerateMapper<TReturn>(this.types.Length, state.Func, funcArray, this.map);
                        if (this.<mapIt>5__5 == null)
                        {
                            goto TR_0003;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (this.reader.Read())
                    {
                        this.<>2__current = this.<mapIt>5__5(this.reader);
                        this.<>1__state = 1;
                        return true;
                    }
                    else if (this.finalize)
                    {
                        while (true)
                        {
                            if (!this.reader.NextResult())
                            {
                                this.command.OnCompleted();
                                break;
                            }
                        }
                    }
                TR_0003:
                    this.<mapIt>5__5 = null;
                    this.<>m__Finally1();
                    return false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
            }

            [DebuggerHidden]
            IEnumerator<TReturn> IEnumerable<TReturn>.GetEnumerator()
            {
                SqlMapper.<MultiMapImpl>d__157<TReturn> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<MultiMapImpl>d__157<TReturn>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (SqlMapper.<MultiMapImpl>d__157<TReturn>) this;
                }
                d__.cnn = this.<>3__cnn;
                d__.command = this.<>3__command;
                d__.types = this.<>3__types;
                d__.map = this.<>3__map;
                d__.splitOn = this.<>3__splitOn;
                d__.reader = this.<>3__reader;
                d__.identity = this.<>3__identity;
                d__.finalize = this.<>3__finalize;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TReturn>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            TReturn IEnumerator<TReturn>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <Parse>d__238<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private Func<IDataReader, object> <deser>5__2;
            private Type <convertToType>5__3;

            [DebuggerHidden]
            public <Parse>d__238(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                object obj2;
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if (this.reader.Read())
                        {
                            Type type = typeof(T);
                            this.<deser>5__2 = SqlMapper.GetDeserializer(type, this.reader, 0, -1, false);
                            Type underlyingType = Nullable.GetUnderlyingType(type);
                            Type type2 = underlyingType;
                            if (underlyingType == null)
                            {
                                Type local1 = underlyingType;
                                type2 = type;
                            }
                            this.<convertToType>5__3 = type2;
                            goto TR_0003;
                        }
                        goto TR_0009;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        break;

                    default:
                        return false;
                }
                if (this.reader.Read())
                {
                    goto TR_0003;
                }
                else
                {
                    this.<deser>5__2 = null;
                    this.<convertToType>5__3 = null;
                }
                goto TR_0009;
            TR_0003:
                obj2 = this.<deser>5__2(this.reader);
                if ((obj2 != null) && !(obj2 is T))
                {
                    this.<>2__current = (T) Convert.ChangeType(obj2, this.<convertToType>5__3, CultureInfo.InvariantCulture);
                    this.<>1__state = 2;
                    return true;
                }
                this.<>2__current = (T) obj2;
                this.<>1__state = 1;
                return true;
            TR_0009:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                SqlMapper.<Parse>d__238<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<Parse>d__238<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (SqlMapper.<Parse>d__238<T>) this;
                }
                d__.reader = this.<>3__reader;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <Parse>d__239 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private Type type;
            public Type <>3__type;
            private Func<IDataReader, object> <deser>5__2;

            [DebuggerHidden]
            public <Parse>d__239(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    if (!this.reader.Read())
                    {
                        goto TR_0001;
                    }
                    else
                    {
                        this.<deser>5__2 = SqlMapper.GetDeserializer(this.type, this.reader, 0, -1, false);
                    }
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    if (!this.reader.Read())
                    {
                        this.<deser>5__2 = null;
                        goto TR_0001;
                    }
                }
                this.<>2__current = this.<deser>5__2(this.reader);
                this.<>1__state = 1;
                return true;
            TR_0001:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                SqlMapper.<Parse>d__239 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<Parse>d__239(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.reader = this.<>3__reader;
                d__.type = this.<>3__type;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <Parse>d__240 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private IDataReader reader;
            public IDataReader <>3__reader;
            private Func<IDataReader, object> <deser>5__2;

            [DebuggerHidden]
            public <Parse>d__240(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    if (!this.reader.Read())
                    {
                        goto TR_0001;
                    }
                    else
                    {
                        this.<deser>5__2 = SqlMapper.GetDapperRowDeserializer(this.reader, 0, -1, false);
                    }
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    if (!this.reader.Read())
                    {
                        this.<deser>5__2 = null;
                        goto TR_0001;
                    }
                }
                this.<>2__current = this.<deser>5__2(this.reader);
                this.<>1__state = 1;
                return true;
            TR_0001:
                return false;
            }

            [return: Dynamic(new bool[] { false, true })]
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                SqlMapper.<Parse>d__240 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<Parse>d__240(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.reader = this.<>3__reader;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<dynamic>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private struct <QueryAsync>d__33<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IEnumerable<T>> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            public Type effectiveType;
            private SqlMapper.Identity <identity>5__2;
            private SqlMapper.CacheInfo <info>5__3;
            private bool <wasClosed>5__4;
            private CancellationToken <cancel>5__5;
            private DbCommand <cmd>5__6;
            private DbDataReader <reader>5__7;
            private Func<IDataReader, object> <func>5__8;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;
            private List<T> <buffer>5__9;
            private Type <convertToType>5__10;
            private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;

            private void MoveNext()
            {
                IEnumerable<T> enumerable;
                int num = this.<>1__state;
                try
                {
                    if (num > 3)
                    {
                        object parameters = this.command.Parameters;
                        this.<identity>5__2 = new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, this.effectiveType, parameters?.GetType());
                        this.<info>5__3 = SqlMapper.GetCacheInfo(this.<identity>5__2, parameters, this.command.AddToCache);
                        this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                        this.<cancel>5__5 = this.command.CancellationToken;
                        this.<cmd>5__6 = this.command.TrySetupAsyncCommand(this.cnn, this.<info>5__3.ParamReader);
                    }
                    try
                    {
                        if (num > 3)
                        {
                            this.<reader>5__7 = null;
                        }
                        try
                        {
                            ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                            DbDataReader reader;
                            ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter4;
                            switch (num)
                            {
                                case 0:
                                    awaiter = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0032;

                                case 1:
                                    awaiter2 = this.<>u__2;
                                    this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_002E;

                                case 2:
                                    awaiter3 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_001D;

                                case 3:
                                    awaiter4 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0014;

                                default:
                                    if (!this.<wasClosed>5__4)
                                    {
                                        break;
                                    }
                                    awaiter = this.cnn.TryOpenAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0032;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<QueryAsync>d__33<T>>(ref awaiter, ref (SqlMapper.<QueryAsync>d__33<T>) ref this);
                                    }
                                    return;
                            }
                            goto TR_0030;
                        TR_0014:
                            if (awaiter4.GetResult())
                            {
                                goto TR_0018;
                            }
                            else
                            {
                                this.command.OnCompleted();
                                enumerable = this.<buffer>5__9;
                            }
                            goto TR_0012;
                        TR_0018:
                            while (true)
                            {
                                ConfiguredTaskAwaitable<bool> awaitable3 = this.<reader>5__7.NextResultAsync(this.<cancel>5__5).ConfigureAwait(false);
                                awaiter4 = awaitable3.GetAwaiter();
                                if (!awaiter4.IsCompleted)
                                {
                                    this.<>1__state = num = 3;
                                    this.<>u__3 = awaiter4;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryAsync>d__33<T>>(ref awaiter4, ref (SqlMapper.<QueryAsync>d__33<T>) ref this);
                                    break;
                                }
                                goto TR_0014;
                            }
                            return;
                        TR_001D:
                            if (awaiter3.GetResult())
                            {
                                object obj3 = this.<func>5__8(this.<reader>5__7);
                                if ((obj3 != null) && !(obj3 is T))
                                {
                                    this.<buffer>5__9.Add((T) Convert.ChangeType(obj3, this.<convertToType>5__10, CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    this.<buffer>5__9.Add((T) obj3);
                                }
                            }
                            else
                            {
                                goto TR_0018;
                            }
                        TR_0021:
                            while (true)
                            {
                                awaiter3 = this.<reader>5__7.ReadAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                                if (!awaiter3.IsCompleted)
                                {
                                    this.<>1__state = num = 2;
                                    this.<>u__3 = awaiter3;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryAsync>d__33<T>>(ref awaiter3, ref (SqlMapper.<QueryAsync>d__33<T>) ref this);
                                    break;
                                }
                                goto TR_001D;
                            }
                            return;
                        TR_002E:
                            reader = awaiter2.GetResult();
                            this.<reader>5__7 = reader;
                            SqlMapper.DeserializerState deserializer = this.<info>5__3.Deserializer;
                            int hash = SqlMapper.GetColumnHash(this.<reader>5__7, 0, -1);
                            if ((deserializer.Func == null) || (deserializer.Hash != hash))
                            {
                                if (this.<reader>5__7.FieldCount != 0)
                                {
                                    deserializer = this.<info>5__3.Deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(this.effectiveType, this.<reader>5__7, 0, -1, false));
                                    if (this.command.AddToCache)
                                    {
                                        SqlMapper.SetQueryCache(this.<identity>5__2, this.<info>5__3);
                                    }
                                }
                                else
                                {
                                    enumerable = Enumerable.Empty<T>();
                                    goto TR_0012;
                                }
                            }
                            this.<func>5__8 = deserializer.Func;
                            if (!this.command.Buffered)
                            {
                                this.<wasClosed>5__4 = false;
                                IEnumerable<T> enumerable2 = SqlMapper.ExecuteReaderSync<T>(this.<reader>5__7, this.<func>5__8, this.command.Parameters);
                                this.<reader>5__7 = null;
                                enumerable = enumerable2;
                            }
                            else
                            {
                                this.<buffer>5__9 = new List<T>();
                                Type underlyingType = Nullable.GetUnderlyingType(this.effectiveType);
                                Type effectiveType = underlyingType;
                                if (underlyingType == null)
                                {
                                    Type local1 = underlyingType;
                                    effectiveType = this.effectiveType;
                                }
                                this.<convertToType>5__10 = effectiveType;
                                goto TR_0021;
                            }
                            goto TR_0012;
                        TR_0030:
                            awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__6, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_002E;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<QueryAsync>d__33<T>>(ref awaiter2, ref (SqlMapper.<QueryAsync>d__33<T>) ref this);
                            }
                            return;
                        TR_0032:
                            awaiter.GetResult();
                            goto TR_0030;
                        }
                        finally
                        {
                            if (num < 0)
                            {
                                DbDataReader reader2 = this.<reader>5__7;
                                try
                                {
                                }
                                finally
                                {
                                    if ((num < 0) && (reader2 != null))
                                    {
                                        reader2.Dispose();
                                    }
                                }
                                if (this.<wasClosed>5__4)
                                {
                                    this.cnn.Close();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<cmd>5__6 != null))
                        {
                            this.<cmd>5__6.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0012:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(enumerable);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private sealed class <QueryImpl>d__140<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private CommandDefinition command;
            public CommandDefinition <>3__command;
            private IDbConnection cnn;
            public IDbConnection <>3__cnn;
            private Type effectiveType;
            public Type <>3__effectiveType;
            private IDbCommand <cmd>5__2;
            private IDataReader <reader>5__3;
            private bool <wasClosed>5__4;
            private Func<IDataReader, object> <func>5__5;
            private Type <convertToType>5__6;

            [DebuggerHidden]
            public <QueryImpl>d__140(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<reader>5__3 != null)
                {
                    if (!this.<reader>5__3.IsClosed)
                    {
                        try
                        {
                            this.<cmd>5__2.Cancel();
                        }
                        catch
                        {
                        }
                    }
                    this.<reader>5__3.Dispose();
                }
                if (this.<wasClosed>5__4)
                {
                    this.cnn.Close();
                }
                if (this.<cmd>5__2 == null)
                {
                    IDbCommand local2 = this.<cmd>5__2;
                }
                else
                {
                    this.<cmd>5__2.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    SqlMapper.DeserializerState deserializer;
                    switch (this.<>1__state)
                    {
                        case 0:
                        {
                            this.<>1__state = -1;
                            object parameters = this.command.Parameters;
                            SqlMapper.Identity identity = new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, this.effectiveType, parameters?.GetType());
                            SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, parameters, this.command.AddToCache);
                            this.<cmd>5__2 = null;
                            this.<reader>5__3 = null;
                            this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                            this.<>1__state = -3;
                            this.<cmd>5__2 = this.command.SetupCommand(this.cnn, info.ParamReader);
                            if (this.<wasClosed>5__4)
                            {
                                this.cnn.Open();
                            }
                            this.<reader>5__3 = SqlMapper.ExecuteReaderWithFlagsFallback(this.<cmd>5__2, this.<wasClosed>5__4, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult);
                            this.<wasClosed>5__4 = false;
                            deserializer = info.Deserializer;
                            int hash = SqlMapper.GetColumnHash(this.<reader>5__3, 0, -1);
                            if ((deserializer.Func == null) || (deserializer.Hash != hash))
                            {
                                if (this.<reader>5__3.FieldCount != 0)
                                {
                                    deserializer = info.Deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(this.effectiveType, this.<reader>5__3, 0, -1, false));
                                    if (this.command.AddToCache)
                                    {
                                        SqlMapper.SetQueryCache(identity, info);
                                    }
                                    goto TR_000D;
                                }
                                else
                                {
                                    flag = false;
                                    this.<>m__Finally1();
                                }
                            }
                            else
                            {
                                goto TR_000D;
                            }
                            break;
                        }
                        case 1:
                            this.<>1__state = -3;
                            goto TR_000A;

                        case 2:
                            this.<>1__state = -3;
                            goto TR_000A;

                        default:
                            flag = false;
                            break;
                    }
                    return flag;
                TR_000A:
                    if (!this.<reader>5__3.Read())
                    {
                        while (true)
                        {
                            if (!this.<reader>5__3.NextResult())
                            {
                                this.<reader>5__3.Dispose();
                                this.<reader>5__3 = null;
                                this.command.OnCompleted();
                                this.<func>5__5 = null;
                                this.<convertToType>5__6 = null;
                                this.<>m__Finally1();
                                flag = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        object obj3 = this.<func>5__5(this.<reader>5__3);
                        if ((obj3 != null) && !(obj3 is T))
                        {
                            this.<>2__current = (T) Convert.ChangeType(obj3, this.<convertToType>5__6, CultureInfo.InvariantCulture);
                            this.<>1__state = 2;
                            flag = true;
                        }
                        else
                        {
                            this.<>2__current = (T) obj3;
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    return flag;
                TR_000D:
                    this.<func>5__5 = deserializer.Func;
                    Type underlyingType = Nullable.GetUnderlyingType(this.effectiveType);
                    Type effectiveType = underlyingType;
                    if (underlyingType == null)
                    {
                        Type local1 = underlyingType;
                        effectiveType = this.effectiveType;
                    }
                    this.<convertToType>5__6 = effectiveType;
                    goto TR_000A;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                SqlMapper.<QueryImpl>d__140<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new SqlMapper.<QueryImpl>d__140<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (SqlMapper.<QueryImpl>d__140<T>) this;
                }
                d__.cnn = this.<>3__cnn;
                d__.command = this.<>3__command;
                d__.effectiveType = this.<>3__effectiveType;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || ((num - 1) <= 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private struct <QueryMultipleAsync>d__57 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SqlMapper.GridReader> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            private SqlMapper.Identity <identity>5__2;
            private SqlMapper.CacheInfo <info>5__3;
            private DbCommand <cmd>5__4;
            private IDataReader <reader>5__5;
            private bool <wasClosed>5__6;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num > 1)
                    {
                        object parameters = this.command.Parameters;
                        this.<identity>5__2 = new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, typeof(SqlMapper.GridReader), parameters?.GetType());
                        this.<info>5__3 = SqlMapper.GetCacheInfo(this.<identity>5__2, parameters, this.command.AddToCache);
                        this.<cmd>5__4 = null;
                        this.<reader>5__5 = null;
                        this.<wasClosed>5__6 = this.cnn.State == ConnectionState.Closed;
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        DbDataReader reader3;
                        ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0013;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0010;
                            }
                            else
                            {
                                if (!this.<wasClosed>5__6)
                                {
                                    goto TR_0011;
                                }
                                else
                                {
                                    awaiter = this.cnn.TryOpenAsync(this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0013;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<QueryMultipleAsync>d__57>(ref awaiter, ref this);
                                    }
                                }
                                return;
                            }
                            goto TR_0013;
                        }
                        goto TR_0011;
                    TR_0010:
                        reader3 = awaiter2.GetResult();
                        this.<reader>5__5 = reader3;
                        SqlMapper.GridReader reader2 = new SqlMapper.GridReader(this.<cmd>5__4, this.<reader>5__5, this.<identity>5__2, this.command.Parameters as DynamicParameters, this.command.AddToCache, this.command.CancellationToken);
                        this.<wasClosed>5__6 = false;
                        SqlMapper.GridReader result = reader2;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    TR_0011:
                        this.<cmd>5__4 = this.command.TrySetupAsyncCommand(this.cnn, this.<info>5__3.ParamReader);
                        awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__4, this.<wasClosed>5__6, CommandBehavior.SequentialAccess, this.command.CancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter2.IsCompleted)
                        {
                            goto TR_0010;
                        }
                        else
                        {
                            this.<>1__state = num = 1;
                            this.<>u__2 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<QueryMultipleAsync>d__57>(ref awaiter2, ref this);
                        }
                        return;
                    TR_0013:
                        awaiter.GetResult();
                        goto TR_0011;
                    }
                    catch
                    {
                        if (this.<reader>5__5 != null)
                        {
                            if (!this.<reader>5__5.IsClosed)
                            {
                                try
                                {
                                    this.<cmd>5__4.Cancel();
                                }
                                catch
                                {
                                }
                            }
                            this.<reader>5__5.Dispose();
                        }
                        if (this.<cmd>5__4 == null)
                        {
                            DbCommand local3 = this.<cmd>5__4;
                        }
                        else
                        {
                            this.<cmd>5__4.Dispose();
                        }
                        if (this.<wasClosed>5__6)
                        {
                            this.cnn.Close();
                        }
                        throw;
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <QueryRowAsync>d__34<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            public CommandDefinition command;
            public IDbConnection cnn;
            public Type effectiveType;
            public SqlMapper.Row row;
            private SqlMapper.Identity <identity>5__2;
            private SqlMapper.CacheInfo <info>5__3;
            private bool <wasClosed>5__4;
            private CancellationToken <cancel>5__5;
            private DbCommand <cmd>5__6;
            private DbDataReader <reader>5__7;
            private T <result>5__8;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__2;
            private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num > 5)
                    {
                        object parameters = this.command.Parameters;
                        this.<identity>5__2 = new SqlMapper.Identity(this.command.CommandText, this.command.CommandType, this.cnn, this.effectiveType, parameters?.GetType());
                        this.<info>5__3 = SqlMapper.GetCacheInfo(this.<identity>5__2, parameters, this.command.AddToCache);
                        this.<wasClosed>5__4 = this.cnn.State == ConnectionState.Closed;
                        this.<cancel>5__5 = this.command.CancellationToken;
                        this.<cmd>5__6 = this.command.TrySetupAsyncCommand(this.cnn, this.<info>5__3.ParamReader);
                    }
                    try
                    {
                        if (num > 5)
                        {
                            this.<reader>5__7 = null;
                        }
                        try
                        {
                            ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                            DbDataReader reader;
                            ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter2;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
                            bool flag3;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter4;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter5;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter6;
                            switch (num)
                            {
                                case 0:
                                    awaiter = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    break;

                                case 1:
                                    awaiter2 = this.<>u__2;
                                    this.<>u__2 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_002F;

                                case 2:
                                    awaiter3 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_002E;

                                case 3:
                                    awaiter4 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0022;

                                case 4:
                                    awaiter5 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_001B;

                                case 5:
                                    awaiter6 = this.<>u__3;
                                    this.<>u__3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0016;

                                default:
                                    if (!this.<wasClosed>5__4)
                                    {
                                        goto TR_0031;
                                    }
                                    else
                                    {
                                        awaiter = this.cnn.TryOpenAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter.IsCompleted)
                                        {
                                            break;
                                        }
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                                    }
                                    return;
                            }
                            awaiter.GetResult();
                            goto TR_0031;
                        TR_0016:
                            if (!awaiter6.GetResult())
                            {
                                T result = this.<result>5__8;
                                this.<>1__state = -2;
                                this.<>t__builder.SetResult(result);
                                return;
                            }
                        TR_001A:
                            while (true)
                            {
                                ConfiguredTaskAwaitable<bool> awaitable3 = this.<reader>5__7.NextResultAsync(this.<cancel>5__5).ConfigureAwait(false);
                                awaiter6 = awaitable3.GetAwaiter();
                                if (!awaiter6.IsCompleted)
                                {
                                    this.<>1__state = num = 5;
                                    this.<>u__3 = awaiter6;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter6, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                                    break;
                                }
                                goto TR_0016;
                            }
                            return;
                        TR_001B:
                            if (!awaiter5.GetResult())
                            {
                                goto TR_001A;
                            }
                        TR_001F:
                            while (true)
                            {
                                awaiter5 = this.<reader>5__7.ReadAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                                if (!awaiter5.IsCompleted)
                                {
                                    this.<>1__state = num = 4;
                                    this.<>u__3 = awaiter5;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter5, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                                    break;
                                }
                                goto TR_001B;
                            }
                            return;
                        TR_0021:
                            if (flag3)
                            {
                                SqlMapper.ThrowMultipleRows(this.row);
                            }
                            goto TR_001F;
                        TR_0022:
                            flag3 = awaiter4.GetResult();
                            goto TR_0021;
                        TR_002E:
                            if (!awaiter3.GetResult() || (this.<reader>5__7.FieldCount == 0))
                            {
                                if ((this.row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
                                {
                                    SqlMapper.ThrowZeroRows(this.row);
                                }
                            }
                            else
                            {
                                SqlMapper.DeserializerState deserializer = this.<info>5__3.Deserializer;
                                int hash = SqlMapper.GetColumnHash(this.<reader>5__7, 0, -1);
                                if ((deserializer.Func == null) || (deserializer.Hash != hash))
                                {
                                    deserializer = this.<info>5__3.Deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(this.effectiveType, this.<reader>5__7, 0, -1, false));
                                    if (this.command.AddToCache)
                                    {
                                        SqlMapper.SetQueryCache(this.<identity>5__2, this.<info>5__3);
                                    }
                                }
                                object obj3 = deserializer.Func(this.<reader>5__7);
                                this.<result>5__8 = ((obj3 == null) || (obj3 is T)) ? ((T) obj3) : ((T) Convert.ChangeType(obj3, Nullable.GetUnderlyingType(this.effectiveType) ?? this.effectiveType, CultureInfo.InvariantCulture));
                                if ((this.row & SqlMapper.Row.Single) == SqlMapper.Row.First)
                                {
                                    goto TR_0021;
                                }
                                else
                                {
                                    awaiter4 = this.<reader>5__7.ReadAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter4.IsCompleted)
                                    {
                                        goto TR_0022;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 3;
                                        this.<>u__3 = awaiter4;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter4, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                                    }
                                }
                                return;
                            }
                            goto TR_001A;
                        TR_002F:
                            reader = awaiter2.GetResult();
                            this.<reader>5__7 = reader;
                            this.<result>5__8 = default(T);
                            awaiter3 = this.<reader>5__7.ReadAsync(this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                            if (!awaiter3.IsCompleted)
                            {
                                this.<>1__state = num = 2;
                                this.<>u__3 = awaiter3;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter3, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                                return;
                            }
                            goto TR_002E;
                        TR_0031:
                            awaiter2 = SqlMapper.ExecuteReaderWithFlagsFallbackAsync(this.<cmd>5__6, this.<wasClosed>5__4, ((this.row & SqlMapper.Row.Single) != SqlMapper.Row.First) ? (CommandBehavior.SequentialAccess | CommandBehavior.SingleResult) : (CommandBehavior.SequentialAccess | CommandBehavior.SingleRow | CommandBehavior.SingleResult), this.<cancel>5__5).ConfigureAwait(false).GetAwaiter();
                            if (!awaiter2.IsCompleted)
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, SqlMapper.<QueryRowAsync>d__34<T>>(ref awaiter2, ref (SqlMapper.<QueryRowAsync>d__34<T>) ref this);
                            }
                            else
                            {
                                goto TR_002F;
                            }
                        }
                        finally
                        {
                            if (num < 0)
                            {
                                DbDataReader reader2 = this.<reader>5__7;
                                try
                                {
                                }
                                finally
                                {
                                    if ((num < 0) && (reader2 != null))
                                    {
                                        reader2.Dispose();
                                    }
                                }
                                if (this.<wasClosed>5__4)
                                {
                                    this.cnn.Close();
                                }
                            }
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<cmd>5__6 != null))
                        {
                            this.<cmd>5__6.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AsyncExecState
        {
            public readonly DbCommand Command;
            public readonly Task<int> Task;
            public AsyncExecState(DbCommand command, Task<int> task)
            {
                this.Command = command;
                this.Task = task;
            }
        }

        private class CacheInfo
        {
            private int hitCount;

            public int GetHitCount() => 
                Interlocked.CompareExchange(ref this.hitCount, 0, 0);

            public void RecordHit()
            {
                Interlocked.Increment(ref this.hitCount);
            }

            public SqlMapper.DeserializerState Deserializer { get; set; }

            public Func<IDataReader, object>[] OtherDeserializers { get; set; }

            public Action<IDbCommand, object> ParamReader { get; set; }
        }

        [TypeDescriptionProvider(typeof(SqlMapper.DapperRow.DapperRowTypeDescriptionProvider))]
        private sealed class DapperRow : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyDictionary<string, object>, IReadOnlyCollection<KeyValuePair<string, object>>, IDynamicMetaObjectProvider
        {
            private readonly SqlMapper.DapperTable table;
            private object[] values;

            public DapperRow(SqlMapper.DapperTable table, object[] values)
            {
                if (table == null)
                {
                    SqlMapper.DapperTable local1 = table;
                    throw new ArgumentNullException("table");
                }
                this.table = table;
                if (values == null)
                {
                    object[] local2 = values;
                    throw new ArgumentNullException("values");
                }
                this.values = values;
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__9))]
            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                <GetEnumerator>d__9 d__1 = new <GetEnumerator>d__9(0);
                d__1.<>4__this = this;
                return d__1;
            }

            internal bool Remove(int index)
            {
                if ((index < 0) || ((index >= this.values.Length) || (this.values[index] is DeadValue)))
                {
                    return false;
                }
                this.values[index] = DeadValue.Default;
                return true;
            }

            internal object SetValue(int index, object value)
            {
                object obj2;
                int length = this.values.Length;
                if (length <= index)
                {
                    Array.Resize<object>(ref this.values, this.table.FieldCount);
                    for (int i = length; i < this.values.Length; i++)
                    {
                        this.values[i] = DeadValue.Default;
                    }
                }
                this.values[index] = obj2 = value;
                return obj2;
            }

            public object SetValue(string key, object value) => 
                this.SetValue(key, value, false);

            private object SetValue(string key, object value, bool isAdd)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                int index = this.table.IndexOfName(key);
                if (index < 0)
                {
                    index = this.table.AddField(key);
                }
                else if (isAdd && ((index < this.values.Length) && !(this.values[index] is DeadValue)))
                {
                    throw new ArgumentException("An item with the same key has already been added", "key");
                }
                return this.SetValue(index, value);
            }

            void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
            {
                ((IDictionary<string, object>) this).Add(item.Key, item.Value);
            }

            void ICollection<KeyValuePair<string, object>>.Clear()
            {
                for (int i = 0; i < this.values.Length; i++)
                {
                    this.values[i] = DeadValue.Default;
                }
            }

            bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
            {
                object obj2;
                return (this.TryGetValue(item.Key, out obj2) && Equals(obj2, item.Value));
            }

            void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                foreach (KeyValuePair<string, object> pair in this)
                {
                    array[arrayIndex++] = pair;
                }
            }

            bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) => 
                ((IDictionary<string, object>) this).Remove(item.Key);

            void IDictionary<string, object>.Add(string key, object value)
            {
                this.SetValue(key, value, true);
            }

            bool IDictionary<string, object>.ContainsKey(string key)
            {
                int index = this.table.IndexOfName(key);
                return ((index >= 0) && ((index < this.values.Length) && !(this.values[index] is DeadValue)));
            }

            bool IDictionary<string, object>.Remove(string key) => 
                this.Remove(this.table.IndexOfName(key));

            bool IReadOnlyDictionary<string, object>.ContainsKey(string key)
            {
                int index = this.table.IndexOfName(key);
                return ((index >= 0) && ((index < this.values.Length) && !(this.values[index] is DeadValue)));
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter) => 
                new SqlMapper.DapperRowMetaObject(parameter, BindingRestrictions.Empty, this);

            public override string ToString()
            {
                StringBuilder builder = SqlMapper.GetStringBuilder().Append("{DapperRow");
                foreach (KeyValuePair<string, object> pair in this)
                {
                    object obj2 = pair.Value;
                    builder.Append(", ").Append(pair.Key);
                    if (obj2 != null)
                    {
                        builder.Append(" = '").Append(pair.Value).Append('\'');
                        continue;
                    }
                    builder.Append(" = NULL");
                }
                return builder.Append('}').__ToStringRecycle();
            }

            internal bool TryGetValue(int index, out object value)
            {
                if (index < 0)
                {
                    value = null;
                    return false;
                }
                value = (index < this.values.Length) ? this.values[index] : null;
                if (!(value is DeadValue))
                {
                    return true;
                }
                value = null;
                return false;
            }

            public bool TryGetValue(string key, out object value) => 
                this.TryGetValue(this.table.IndexOfName(key), out value);

            int ICollection<KeyValuePair<string, object>>.Count
            {
                get
                {
                    int num = 0;
                    for (int i = 0; i < this.values.Length; i++)
                    {
                        if (!(this.values[i] is DeadValue))
                        {
                            num++;
                        }
                    }
                    return num;
                }
            }

            bool ICollection<KeyValuePair<string, object>>.IsReadOnly =>
                false;

            object IDictionary<string, object>.this[string key]
            {
                get
                {
                    object obj2;
                    this.TryGetValue(key, out obj2);
                    return obj2;
                }
                set => 
                    this.SetValue(key, value, false);
            }

            ICollection<string> IDictionary<string, object>.Keys
            {
                get
                {
                    Func<KeyValuePair<string, object>, string> selector = <>c.<>9__29_0;
                    if (<>c.<>9__29_0 == null)
                    {
                        Func<KeyValuePair<string, object>, string> local1 = <>c.<>9__29_0;
                        selector = <>c.<>9__29_0 = new Func<KeyValuePair<string, object>, string>(this.<System.Collections.Generic.IDictionary<System.String,System.Object>.get_Keys>b__29_0);
                    }
                    return this.Select<KeyValuePair<string, object>, string>(selector).ToArray<string>();
                }
            }

            ICollection<object> IDictionary<string, object>.Values
            {
                get
                {
                    Func<KeyValuePair<string, object>, object> selector = <>c.<>9__31_0;
                    if (<>c.<>9__31_0 == null)
                    {
                        Func<KeyValuePair<string, object>, object> local1 = <>c.<>9__31_0;
                        selector = <>c.<>9__31_0 = new Func<KeyValuePair<string, object>, object>(this.<System.Collections.Generic.IDictionary<System.String,System.Object>.get_Values>b__31_0);
                    }
                    return this.Select<KeyValuePair<string, object>, object>(selector).ToArray<object>();
                }
            }

            int IReadOnlyCollection<KeyValuePair<string, object>>.Count
            {
                get
                {
                    Func<object, bool> predicate = <>c.<>9__33_0;
                    if (<>c.<>9__33_0 == null)
                    {
                        Func<object, bool> local1 = <>c.<>9__33_0;
                        predicate = <>c.<>9__33_0 = new Func<object, bool>(this.<System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.get_Count>b__33_0);
                    }
                    return this.values.Count<object>(predicate);
                }
            }

            object IReadOnlyDictionary<string, object>.this[string key]
            {
                get
                {
                    object obj2;
                    this.TryGetValue(key, out obj2);
                    return obj2;
                }
            }

            IEnumerable<string> IReadOnlyDictionary<string, object>.Keys
            {
                get
                {
                    Func<KeyValuePair<string, object>, string> selector = <>c.<>9__38_0;
                    if (<>c.<>9__38_0 == null)
                    {
                        Func<KeyValuePair<string, object>, string> local1 = <>c.<>9__38_0;
                        selector = <>c.<>9__38_0 = new Func<KeyValuePair<string, object>, string>(this.<System.Collections.Generic.IReadOnlyDictionary<System.String,System.Object>.get_Keys>b__38_0);
                    }
                    return this.Select<KeyValuePair<string, object>, string>(selector);
                }
            }

            IEnumerable<object> IReadOnlyDictionary<string, object>.Values
            {
                get
                {
                    Func<KeyValuePair<string, object>, object> selector = <>c.<>9__40_0;
                    if (<>c.<>9__40_0 == null)
                    {
                        Func<KeyValuePair<string, object>, object> local1 = <>c.<>9__40_0;
                        selector = <>c.<>9__40_0 = new Func<KeyValuePair<string, object>, object>(this.<System.Collections.Generic.IReadOnlyDictionary<System.String,System.Object>.get_Values>b__40_0);
                    }
                    return this.Select<KeyValuePair<string, object>, object>(selector);
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly SqlMapper.DapperRow.<>c <>9 = new SqlMapper.DapperRow.<>c();
                public static Func<KeyValuePair<string, object>, string> <>9__29_0;
                public static Func<KeyValuePair<string, object>, object> <>9__31_0;
                public static Func<object, bool> <>9__33_0;
                public static Func<KeyValuePair<string, object>, string> <>9__38_0;
                public static Func<KeyValuePair<string, object>, object> <>9__40_0;

                internal string <System.Collections.Generic.IDictionary<System.String,System.Object>.get_Keys>b__29_0(KeyValuePair<string, object> kv) => 
                    kv.Key;

                internal object <System.Collections.Generic.IDictionary<System.String,System.Object>.get_Values>b__31_0(KeyValuePair<string, object> kv) => 
                    kv.Value;

                internal bool <System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.get_Count>b__33_0(object t) => 
                    !(t is SqlMapper.DapperRow.DeadValue);

                internal string <System.Collections.Generic.IReadOnlyDictionary<System.String,System.Object>.get_Keys>b__38_0(KeyValuePair<string, object> kv) => 
                    kv.Key;

                internal object <System.Collections.Generic.IReadOnlyDictionary<System.String,System.Object>.get_Values>b__40_0(KeyValuePair<string, object> kv) => 
                    kv.Value;
            }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__9 : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private KeyValuePair<string, object> <>2__current;
                public SqlMapper.DapperRow <>4__this;
                private string[] <names>5__2;
                private int <i>5__3;

                [DebuggerHidden]
                public <GetEnumerator>d__9(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    SqlMapper.DapperRow row = this.<>4__this;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<names>5__2 = row.table.FieldNames;
                        this.<i>5__3 = 0;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        goto TR_0007;
                    }
                TR_0005:
                    if (this.<i>5__3 >= this.<names>5__2.Length)
                    {
                        return false;
                    }
                    object obj2 = (this.<i>5__3 < row.values.Length) ? row.values[this.<i>5__3] : null;
                    if (!(obj2 is SqlMapper.DapperRow.DeadValue))
                    {
                        this.<>2__current = new KeyValuePair<string, object>(this.<names>5__2[this.<i>5__3], obj2);
                        this.<>1__state = 1;
                        return true;
                    }
                TR_0007:
                    while (true)
                    {
                        int num2 = this.<i>5__3;
                        this.<i>5__3 = num2 + 1;
                        break;
                    }
                    goto TR_0005;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                KeyValuePair<string, object> IEnumerator<KeyValuePair<string, object>>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            private sealed class DapperRowTypeDescriptionProvider : TypeDescriptionProvider
            {
                public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance) => 
                    new SqlMapper.DapperRow.DapperRowTypeDescriptor(instance);

                public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance) => 
                    new SqlMapper.DapperRow.DapperRowTypeDescriptor(instance);
            }

            private sealed class DapperRowTypeDescriptor : ICustomTypeDescriptor
            {
                private readonly SqlMapper.DapperRow _row;
                private static readonly TypeConverter s_converter = new ExpandableObjectConverter();

                public DapperRowTypeDescriptor(object instance)
                {
                    this._row = (SqlMapper.DapperRow) instance;
                }

                internal static PropertyDescriptorCollection GetProperties(SqlMapper.DapperRow row) => 
                    GetProperties((row != null) ? row.table : null, row);

                internal static PropertyDescriptorCollection GetProperties(SqlMapper.DapperTable table, IDictionary<string, object> row = null)
                {
                    string[] fieldNames = table?.FieldNames;
                    if ((fieldNames == null) || (fieldNames.Length == 0))
                    {
                        return PropertyDescriptorCollection.Empty;
                    }
                    PropertyDescriptor[] properties = new PropertyDescriptor[fieldNames.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        object obj2;
                        properties[i] = new SqlMapper.DapperRow.RowBoundPropertyDescriptor(((row == null) || (!row.TryGetValue(fieldNames[i], out obj2) || (obj2 == null))) ? typeof(object) : obj2.GetType(), fieldNames[i], i);
                    }
                    return new PropertyDescriptorCollection(properties, true);
                }

                AttributeCollection ICustomTypeDescriptor.GetAttributes() => 
                    AttributeCollection.Empty;

                string ICustomTypeDescriptor.GetClassName() => 
                    typeof(SqlMapper.DapperRow).FullName;

                string ICustomTypeDescriptor.GetComponentName() => 
                    null;

                TypeConverter ICustomTypeDescriptor.GetConverter() => 
                    s_converter;

                EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => 
                    null;

                PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => 
                    null;

                object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => 
                    null;

                EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => 
                    EventDescriptorCollection.Empty;

                EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => 
                    EventDescriptorCollection.Empty;

                PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => 
                    GetProperties(this._row);

                PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => 
                    GetProperties(this._row);

                object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => 
                    this._row;
            }

            private sealed class DeadValue
            {
                public static readonly SqlMapper.DapperRow.DeadValue Default = new SqlMapper.DapperRow.DeadValue();

                private DeadValue()
                {
                }
            }

            private sealed class RowBoundPropertyDescriptor : PropertyDescriptor
            {
                private readonly Type _type;
                private readonly int _index;

                public RowBoundPropertyDescriptor(Type type, string name, int index) : base(name, null)
                {
                    this._type = type;
                    this._index = index;
                }

                public override bool CanResetValue(object component) => 
                    true;

                public override object GetValue(object component)
                {
                    object obj2;
                    return (((SqlMapper.DapperRow) component).TryGetValue(this._index, out obj2) ? (obj2 ?? DBNull.Value) : DBNull.Value);
                }

                public override void ResetValue(object component)
                {
                    ((SqlMapper.DapperRow) component).Remove(this._index);
                }

                public override void SetValue(object component, object value)
                {
                    ((SqlMapper.DapperRow) component).SetValue(this._index, (value is DBNull) ? null : value);
                }

                public override bool ShouldSerializeValue(object component)
                {
                    object obj2;
                    return ((SqlMapper.DapperRow) component).TryGetValue(this._index, out obj2);
                }

                public override bool IsReadOnly =>
                    false;

                public override Type ComponentType =>
                    typeof(SqlMapper.DapperRow);

                public override Type PropertyType =>
                    this._type;
            }
        }

        private sealed class DapperRowMetaObject : DynamicMetaObject
        {
            private static readonly MethodInfo getValueMethod = typeof(IDictionary<string, object>).GetProperty("Item").GetGetMethod();
            private static readonly MethodInfo setValueMethod;
            private static readonly string[] s_nixKeys;

            static DapperRowMetaObject()
            {
                Type[] types = new Type[] { typeof(string), typeof(object) };
                setValueMethod = typeof(SqlMapper.DapperRow).GetMethod("SetValue", types);
                s_nixKeys = new string[0];
            }

            public DapperRowMetaObject(Expression expression, BindingRestrictions restrictions) : base(expression, restrictions)
            {
            }

            public DapperRowMetaObject(Expression expression, BindingRestrictions restrictions, object value) : base(expression, restrictions, value)
            {
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                Expression[] parameters = new Expression[] { Expression.Constant(binder.Name) };
                return this.CallMethod(getValueMethod, parameters);
            }

            public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
            {
                Expression[] parameters = new Expression[] { Expression.Constant(binder.Name) };
                return this.CallMethod(getValueMethod, parameters);
            }

            public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
            {
                Expression[] parameters = new Expression[] { Expression.Constant(binder.Name), value.Expression };
                return this.CallMethod(setValueMethod, parameters);
            }

            private DynamicMetaObject CallMethod(MethodInfo method, Expression[] parameters) => 
                new DynamicMetaObject(Expression.Call(Expression.Convert(base.Expression, base.LimitType), method, parameters), BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType));

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                if (base.HasValue)
                {
                    IDictionary<string, object> dictionary = base.Value as IDictionary<string, object>;
                    if (dictionary != null)
                    {
                        return dictionary.Keys;
                    }
                }
                return s_nixKeys;
            }
        }

        private sealed class DapperTable
        {
            private string[] fieldNames;
            private readonly Dictionary<string, int> fieldNameLookup;

            public DapperTable(string[] fieldNames)
            {
                if (fieldNames == null)
                {
                    string[] local1 = fieldNames;
                    throw new ArgumentNullException("fieldNames");
                }
                this.fieldNames = fieldNames;
                this.fieldNameLookup = new Dictionary<string, int>(fieldNames.Length, StringComparer.Ordinal);
                for (int i = fieldNames.Length - 1; i >= 0; i--)
                {
                    string str = fieldNames[i];
                    if (str != null)
                    {
                        this.fieldNameLookup[str] = i;
                    }
                }
            }

            internal int AddField(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                if (this.fieldNameLookup.ContainsKey(name))
                {
                    throw new InvalidOperationException("Field already exists: " + name);
                }
                int length = this.fieldNames.Length;
                Array.Resize<string>(ref this.fieldNames, length + 1);
                this.fieldNames[length] = name;
                this.fieldNameLookup[name] = length;
                return length;
            }

            internal bool FieldExists(string key) => 
                (key != null) && this.fieldNameLookup.ContainsKey(key);

            internal int IndexOfName(string name)
            {
                int num;
                return (((name == null) || !this.fieldNameLookup.TryGetValue(name, out num)) ? -1 : num);
            }

            internal string[] FieldNames =>
                this.fieldNames;

            public int FieldCount =>
                this.fieldNames.Length;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DeserializerState
        {
            public readonly int Hash;
            public readonly Func<IDataReader, object> Func;
            public DeserializerState(int hash, Func<IDataReader, object> func)
            {
                this.Hash = hash;
                this.Func = func;
            }
        }

        private class DontMap
        {
        }

        public class GridReader : IDisposable
        {
            private readonly CancellationToken cancel;
            private IDataReader reader;
            private readonly SqlMapper.Identity identity;
            private readonly bool addToCache;
            private int gridIndex;
            private int readCount;
            private readonly SqlMapper.IParameterCallbacks callbacks;

            internal GridReader(IDbCommand command, IDataReader reader, SqlMapper.Identity identity, SqlMapper.IParameterCallbacks callbacks, bool addToCache)
            {
                this.Command = command;
                this.reader = reader;
                this.identity = identity;
                this.callbacks = callbacks;
                this.addToCache = addToCache;
            }

            internal GridReader(IDbCommand command, IDataReader reader, SqlMapper.Identity identity, DynamicParameters dynamicParams, bool addToCache, CancellationToken cancel) : this(command, reader, identity, dynamicParams, addToCache)
            {
                this.cancel = cancel;
            }

            public void Dispose()
            {
                if (this.reader != null)
                {
                    if (!this.reader.IsClosed)
                    {
                        IDbCommand command = this.Command;
                        if (command == null)
                        {
                            IDbCommand local1 = command;
                        }
                        else
                        {
                            command.Cancel();
                        }
                    }
                    this.reader.Dispose();
                    this.reader = null;
                }
                if (this.Command != null)
                {
                    this.Command.Dispose();
                    this.Command = null;
                }
            }

            [IteratorStateMachine(typeof(<MultiReadInternal>d__43<,,,,,,,>))]
            private IEnumerable<TReturn> MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Delegate func, string splitOn)
            {
                <MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> d__1 = new <MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(-2);
                d__1.<>4__this = this;
                d__1.<>3__func = func;
                d__1.<>3__splitOn = splitOn;
                return d__1;
            }

            [IteratorStateMachine(typeof(<MultiReadInternal>d__44<>))]
            private IEnumerable<TReturn> MultiReadInternal<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn)
            {
                <MultiReadInternal>d__44<TReturn> d__1 = new <MultiReadInternal>d__44<TReturn>(-2);
                d__1.<>4__this = this;
                d__1.<>3__types = types;
                d__1.<>3__map = map;
                d__1.<>3__splitOn = splitOn;
                return d__1;
            }

            private void NextResult()
            {
                if (this.reader.NextResult())
                {
                    this.readCount++;
                    this.gridIndex++;
                    this.IsConsumed = false;
                }
                else
                {
                    this.reader.Dispose();
                    this.reader = null;
                    if (this.callbacks == null)
                    {
                        SqlMapper.IParameterCallbacks callbacks = this.callbacks;
                    }
                    else
                    {
                        this.callbacks.OnCompleted();
                    }
                    this.Dispose();
                }
            }

            [AsyncStateMachine(typeof(<NextResultAsync>d__17))]
            private Task NextResultAsync()
            {
                <NextResultAsync>d__17 d__;
                d__.<>4__this = this;
                d__.<>t__builder = AsyncTaskMethodBuilder.Create();
                d__.<>1__state = -1;
                d__.<>t__builder.Start<<NextResultAsync>d__17>(ref d__);
                return d__.<>t__builder.Task;
            }

            [return: Dynamic(new bool[] { false, true })]
            public IEnumerable<object> Read(bool buffered = true) => 
                this.ReadImpl<object>(typeof(SqlMapper.DapperRow), buffered);

            public IEnumerable<T> Read<T>(bool buffered = true) => 
                this.ReadImpl<T>(typeof(T), buffered);

            public IEnumerable<object> Read(Type type, bool buffered = true)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadImpl<object>(type, buffered);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TReturn>(System.Func<TFirst, TSecond, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, TThird, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, SqlMapper.DontMap, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(func, splitOn);
                return (buffered ? ((IEnumerable<TReturn>) source.ToList<TReturn>()) : source);
            }

            public IEnumerable<TReturn> Read<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn = "id", bool buffered = true)
            {
                IEnumerable<TReturn> source = this.MultiReadInternal<TReturn>(types, map, splitOn);
                return (buffered ? source.ToList<TReturn>() : source);
            }

            [return: Dynamic(new bool[] { false, false, true })]
            public Task<IEnumerable<object>> ReadAsync(bool buffered = true) => 
                this.ReadAsyncImpl<object>(typeof(SqlMapper.DapperRow), buffered);

            public Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true) => 
                this.ReadAsyncImpl<T>(typeof(T), buffered);

            public Task<IEnumerable<object>> ReadAsync(Type type, bool buffered = true)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadAsyncImpl<object>(type, buffered);
            }

            private Task<IEnumerable<T>> ReadAsyncImpl<T>(Type type, bool buffered)
            {
                if (this.reader == null)
                {
                    throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
                }
                if (this.IsConsumed)
                {
                    throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
                }
                SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(this.identity.ForGrid(type, this.gridIndex), null, this.addToCache);
                SqlMapper.DeserializerState deserializer = info.Deserializer;
                int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                if ((deserializer.Func == null) || (deserializer.Hash != hash))
                {
                    deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
                    info.Deserializer = deserializer;
                }
                this.IsConsumed = true;
                if (buffered && (this.reader is DbDataReader))
                {
                    return this.ReadBufferedAsync<T>(this.gridIndex, deserializer.Func);
                }
                IEnumerable<T> source = this.ReadDeferred<T>(this.gridIndex, deserializer.Func, type);
                if (buffered)
                {
                    source = source.ToList<T>();
                }
                return Task.FromResult<IEnumerable<T>>(source);
            }

            [AsyncStateMachine(typeof(<ReadBufferedAsync>d__21<>))]
            private Task<IEnumerable<T>> ReadBufferedAsync<T>(int index, Func<IDataReader, object> deserializer)
            {
                <ReadBufferedAsync>d__21<T> d__;
                d__.<>4__this = this;
                d__.index = index;
                d__.deserializer = deserializer;
                d__.<>t__builder = AsyncTaskMethodBuilder<IEnumerable<T>>.Create();
                d__.<>1__state = -1;
                d__.<>t__builder.Start<<ReadBufferedAsync>d__21<T>>(ref d__);
                return d__.<>t__builder.Task;
            }

            [IteratorStateMachine(typeof(<ReadDeferred>d__52<>))]
            private IEnumerable<T> ReadDeferred<T>(int index, Func<IDataReader, object> deserializer, Type effectiveType)
            {
                <ReadDeferred>d__52<T> d__1 = new <ReadDeferred>d__52<T>(-2);
                d__1.<>4__this = this;
                d__1.<>3__index = index;
                d__1.<>3__deserializer = deserializer;
                d__1.<>3__effectiveType = effectiveType;
                return d__1;
            }

            [return: Dynamic]
            public object ReadFirst() => 
                this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.First);

            public T ReadFirst<T>() => 
                this.ReadRow<T>(typeof(T), SqlMapper.Row.First);

            public object ReadFirst(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRow<object>(type, SqlMapper.Row.First);
            }

            [return: Dynamic(new bool[] { false, true })]
            public Task<object> ReadFirstAsync() => 
                this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.First);

            public Task<T> ReadFirstAsync<T>() => 
                this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.First);

            public Task<object> ReadFirstAsync(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.First);
            }

            [return: Dynamic]
            public object ReadFirstOrDefault() => 
                this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.FirstOrDefault);

            public T ReadFirstOrDefault<T>() => 
                this.ReadRow<T>(typeof(T), SqlMapper.Row.FirstOrDefault);

            public object ReadFirstOrDefault(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRow<object>(type, SqlMapper.Row.FirstOrDefault);
            }

            [return: Dynamic(new bool[] { false, true })]
            public Task<object> ReadFirstOrDefaultAsync() => 
                this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.FirstOrDefault);

            public Task<T> ReadFirstOrDefaultAsync<T>() => 
                this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.FirstOrDefault);

            public Task<object> ReadFirstOrDefaultAsync(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.FirstOrDefault);
            }

            private IEnumerable<T> ReadImpl<T>(Type type, bool buffered)
            {
                if (this.reader == null)
                {
                    throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
                }
                if (this.IsConsumed)
                {
                    throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
                }
                SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(this.identity.ForGrid(type, this.gridIndex), null, this.addToCache);
                SqlMapper.DeserializerState deserializer = info.Deserializer;
                int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                if ((deserializer.Func == null) || (deserializer.Hash != hash))
                {
                    deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
                    info.Deserializer = deserializer;
                }
                this.IsConsumed = true;
                IEnumerable<T> source = this.ReadDeferred<T>(this.gridIndex, deserializer.Func, type);
                return (buffered ? source.ToList<T>() : source);
            }

            private T ReadRow<T>(Type type, SqlMapper.Row row)
            {
                if (this.reader == null)
                {
                    throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
                }
                if (this.IsConsumed)
                {
                    throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
                }
                this.IsConsumed = true;
                T local = default(T);
                if (!this.reader.Read() || (this.reader.FieldCount == 0))
                {
                    if ((row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
                    {
                        SqlMapper.ThrowZeroRows(row);
                    }
                }
                else
                {
                    SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(this.identity.ForGrid(type, this.gridIndex), null, this.addToCache);
                    SqlMapper.DeserializerState deserializer = info.Deserializer;
                    int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                    if ((deserializer.Func == null) || (deserializer.Hash != hash))
                    {
                        deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
                        info.Deserializer = deserializer;
                    }
                    object obj2 = deserializer.Func(this.reader);
                    local = ((obj2 == null) || (obj2 is T)) ? ((T) obj2) : ((T) Convert.ChangeType(obj2, Nullable.GetUnderlyingType(type) ?? type, CultureInfo.InvariantCulture));
                    if (((row & SqlMapper.Row.Single) != SqlMapper.Row.First) && this.reader.Read())
                    {
                        SqlMapper.ThrowMultipleRows(row);
                    }
                    while (this.reader.Read())
                    {
                    }
                }
                this.NextResult();
                return local;
            }

            private Task<T> ReadRowAsyncImpl<T>(Type type, SqlMapper.Row row)
            {
                DbDataReader reader = this.reader as DbDataReader;
                return ((reader == null) ? Task.FromResult<T>(this.ReadRow<T>(type, row)) : this.ReadRowAsyncImplViaDbReader<T>(reader, type, row));
            }

            [AsyncStateMachine(typeof(<ReadRowAsyncImplViaDbReader>d__20<>))]
            private Task<T> ReadRowAsyncImplViaDbReader<T>(DbDataReader reader, Type type, SqlMapper.Row row)
            {
                <ReadRowAsyncImplViaDbReader>d__20<T> d__;
                d__.<>4__this = this;
                d__.reader = reader;
                d__.type = type;
                d__.row = row;
                d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
                d__.<>1__state = -1;
                d__.<>t__builder.Start<<ReadRowAsyncImplViaDbReader>d__20<T>>(ref d__);
                return d__.<>t__builder.Task;
            }

            [return: Dynamic]
            public object ReadSingle() => 
                this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.Single);

            public T ReadSingle<T>() => 
                this.ReadRow<T>(typeof(T), SqlMapper.Row.Single);

            public object ReadSingle(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRow<object>(type, SqlMapper.Row.Single);
            }

            [return: Dynamic(new bool[] { false, true })]
            public Task<object> ReadSingleAsync() => 
                this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.Single);

            public Task<T> ReadSingleAsync<T>() => 
                this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.Single);

            public Task<object> ReadSingleAsync(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.Single);
            }

            [return: Dynamic]
            public object ReadSingleOrDefault() => 
                this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.SingleOrDefault);

            public T ReadSingleOrDefault<T>() => 
                this.ReadRow<T>(typeof(T), SqlMapper.Row.SingleOrDefault);

            public object ReadSingleOrDefault(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRow<object>(type, SqlMapper.Row.SingleOrDefault);
            }

            [return: Dynamic(new bool[] { false, true })]
            public Task<object> ReadSingleOrDefaultAsync() => 
                this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.SingleOrDefault);

            public Task<T> ReadSingleOrDefaultAsync<T>() => 
                this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.SingleOrDefault);

            public Task<object> ReadSingleOrDefaultAsync(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.SingleOrDefault);
            }

            public bool IsConsumed { get; private set; }

            public IDbCommand Command { get; set; }

            [CompilerGenerated]
            private sealed class <MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> : IEnumerable<TReturn>, IEnumerable, IEnumerator<TReturn>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private TReturn <>2__current;
                private int <>l__initialThreadId;
                public SqlMapper.GridReader <>4__this;
                private Delegate func;
                public Delegate <>3__func;
                private string splitOn;
                public string <>3__splitOn;
                private IEnumerator<TReturn> <>7__wrap1;

                [DebuggerHidden]
                public <MultiReadInternal>d__43(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    this.<>4__this.NextResult();
                }

                private void <>m__Finally2()
                {
                    this.<>1__state = -3;
                    if (this.<>7__wrap1 != null)
                    {
                        this.<>7__wrap1.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        SqlMapper.GridReader reader = this.<>4__this;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            SqlMapper.Identity identity = reader.identity.ForGrid<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(typeof(TReturn), reader.gridIndex);
                            reader.IsConsumed = true;
                            this.<>1__state = -3;
                            CommandDefinition command = new CommandDefinition();
                            this.<>7__wrap1 = null.MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(command, this.func, this.splitOn, reader.reader, identity, false).GetEnumerator();
                            this.<>1__state = -4;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -4;
                        }
                        else
                        {
                            return false;
                        }
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap1 = null;
                            this.<>m__Finally1();
                            flag = false;
                        }
                        else
                        {
                            TReturn current = this.<>7__wrap1.Current;
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<TReturn> IEnumerable<TReturn>.GetEnumerator()
                {
                    SqlMapper.GridReader.<MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = (SqlMapper.GridReader.<MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>) this;
                    }
                    else
                    {
                        d__ = new SqlMapper.GridReader.<MultiReadInternal>d__43<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    d__.func = this.<>3__func;
                    d__.splitOn = this.<>3__splitOn;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<TReturn>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if (((num - -4) <= 1) || (num == 1))
                    {
                        try
                        {
                            if ((num == -4) || (num == 1))
                            {
                                try
                                {
                                }
                                finally
                                {
                                    this.<>m__Finally2();
                                }
                            }
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                TReturn IEnumerator<TReturn>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private sealed class <MultiReadInternal>d__44<TReturn> : IEnumerable<TReturn>, IEnumerable, IEnumerator<TReturn>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private TReturn <>2__current;
                private int <>l__initialThreadId;
                public SqlMapper.GridReader <>4__this;
                private Type[] types;
                public Type[] <>3__types;
                private Func<object[], TReturn> map;
                public Func<object[], TReturn> <>3__map;
                private string splitOn;
                public string <>3__splitOn;
                private IEnumerator<TReturn> <>7__wrap1;

                [DebuggerHidden]
                public <MultiReadInternal>d__44(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    this.<>4__this.NextResult();
                }

                private void <>m__Finally2()
                {
                    this.<>1__state = -3;
                    if (this.<>7__wrap1 != null)
                    {
                        this.<>7__wrap1.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        SqlMapper.GridReader reader = this.<>4__this;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            SqlMapper.Identity identity = reader.identity.ForGrid(typeof(TReturn), this.types, reader.gridIndex);
                            this.<>1__state = -3;
                            CommandDefinition command = new CommandDefinition();
                            this.<>7__wrap1 = null.MultiMapImpl<TReturn>(command, this.types, this.map, this.splitOn, reader.reader, identity, false).GetEnumerator();
                            this.<>1__state = -4;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -4;
                        }
                        else
                        {
                            return false;
                        }
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap1 = null;
                            this.<>m__Finally1();
                            flag = false;
                        }
                        else
                        {
                            TReturn current = this.<>7__wrap1.Current;
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<TReturn> IEnumerable<TReturn>.GetEnumerator()
                {
                    SqlMapper.GridReader.<MultiReadInternal>d__44<TReturn> d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = (SqlMapper.GridReader.<MultiReadInternal>d__44<TReturn>) this;
                    }
                    else
                    {
                        d__ = new SqlMapper.GridReader.<MultiReadInternal>d__44<TReturn>(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    d__.types = this.<>3__types;
                    d__.map = this.<>3__map;
                    d__.splitOn = this.<>3__splitOn;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<TReturn>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if (((num - -4) <= 1) || (num == 1))
                    {
                        try
                        {
                            if ((num == -4) || (num == 1))
                            {
                                try
                                {
                                }
                                finally
                                {
                                    this.<>m__Finally2();
                                }
                            }
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                TReturn IEnumerator<TReturn>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private struct <NextResultAsync>d__17 : IAsyncStateMachine
            {
                public int <>1__state;
                public AsyncTaskMethodBuilder <>t__builder;
                public SqlMapper.GridReader <>4__this;
                private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

                private void MoveNext()
                {
                    int num = this.<>1__state;
                    SqlMapper.GridReader reader = this.<>4__this;
                    try
                    {
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000A;
                        }
                        else
                        {
                            awaiter = ((DbDataReader) reader.reader).NextResultAsync(reader.cancel).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000A;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.GridReader.<NextResultAsync>d__17>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_000A:
                        if (awaiter.GetResult())
                        {
                            reader.readCount++;
                            reader.gridIndex++;
                            reader.IsConsumed = false;
                        }
                        else
                        {
                            reader.reader.Dispose();
                            reader.reader = null;
                            if (reader.callbacks == null)
                            {
                                SqlMapper.IParameterCallbacks callbacks = reader.callbacks;
                            }
                            else
                            {
                                reader.callbacks.OnCompleted();
                            }
                            reader.Dispose();
                        }
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult();
                    }
                    catch (Exception exception)
                    {
                        this.<>1__state = -2;
                        this.<>t__builder.SetException(exception);
                    }
                }

                [DebuggerHidden]
                private void SetStateMachine(IAsyncStateMachine stateMachine)
                {
                    this.<>t__builder.SetStateMachine(stateMachine);
                }
            }

            [CompilerGenerated]
            private struct <ReadBufferedAsync>d__21<T> : IAsyncStateMachine
            {
                public int <>1__state;
                public AsyncTaskMethodBuilder<IEnumerable<T>> <>t__builder;
                public SqlMapper.GridReader <>4__this;
                public Func<IDataReader, object> deserializer;
                public int index;
                private object <>7__wrap1;
                private int <>7__wrap2;
                private IEnumerable<T> <>7__wrap3;
                private DbDataReader <reader>5__5;
                private List<T> <buffer>5__6;
                private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
                private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

                private void MoveNext()
                {
                    Exception exception;
                    int num = this.<>1__state;
                    SqlMapper.GridReader reader = this.<>4__this;
                    try
                    {
                        IEnumerable<T> enumerable;
                        object obj2;
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
                        if (num != 0)
                        {
                            if (num == 1)
                            {
                                awaiter2 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_000D;
                            }
                            else
                            {
                                this.<>7__wrap1 = null;
                                this.<>7__wrap2 = 0;
                            }
                        }
                        try
                        {
                            bool flag2;
                            ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
                            if (num == 0)
                            {
                                awaiter = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0016;
                            }
                            else
                            {
                                this.<reader>5__5 = (DbDataReader) reader.reader;
                                this.<buffer>5__6 = new List<T>();
                            }
                            goto TR_001B;
                        TR_0015:
                            if (flag2)
                            {
                                this.<buffer>5__6.Add((T) this.deserializer(this.<reader>5__5));
                            }
                            else
                            {
                                this.<>7__wrap3 = this.<buffer>5__6;
                                this.<>7__wrap2 = 1;
                                goto TR_0010;
                            }
                            goto TR_001B;
                        TR_0016:
                            flag2 = awaiter.GetResult();
                            goto TR_0015;
                        TR_001B:
                            while (true)
                            {
                                if (this.index != reader.gridIndex)
                                {
                                    goto TR_0015;
                                }
                                else
                                {
                                    awaiter = this.<reader>5__5.ReadAsync(reader.cancel).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0016;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadBufferedAsync>d__21<T>>(ref awaiter, ref (SqlMapper.GridReader.<ReadBufferedAsync>d__21<T>) ref this);
                                    }
                                }
                                break;
                            }
                            return;
                        }
                        catch (object obj1)
                        {
                            obj2 = obj1;
                            this.<>7__wrap1 = obj2;
                        }
                        goto TR_0010;
                    TR_000B:
                        obj2 = this.<>7__wrap1;
                        if (obj2 != null)
                        {
                            exception = obj2 as Exception;
                            if (exception == null)
                            {
                                throw obj2;
                            }
                            ExceptionDispatchInfo.Capture(exception).Throw();
                        }
                        if (this.<>7__wrap2 == 1)
                        {
                            enumerable = this.<>7__wrap3;
                        }
                        else
                        {
                            this.<>7__wrap1 = null;
                            this.<>7__wrap3 = null;
                        }
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(enumerable);
                        return;
                    TR_000D:
                        awaiter2.GetResult();
                        goto TR_000B;
                    TR_0010:
                        if (this.index != reader.gridIndex)
                        {
                            goto TR_000B;
                        }
                        else
                        {
                            awaiter2 = reader.NextResultAsync().ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_000D;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadBufferedAsync>d__21<T>>(ref awaiter2, ref (SqlMapper.GridReader.<ReadBufferedAsync>d__21<T>) ref this);
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        this.<>1__state = -2;
                        this.<>t__builder.SetException(exception);
                    }
                }

                [DebuggerHidden]
                private void SetStateMachine(IAsyncStateMachine stateMachine)
                {
                    this.<>t__builder.SetStateMachine(stateMachine);
                }
            }

            [CompilerGenerated]
            private sealed class <ReadDeferred>d__52<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                private int <>l__initialThreadId;
                private Type effectiveType;
                public Type <>3__effectiveType;
                private Func<IDataReader, object> deserializer;
                public Func<IDataReader, object> <>3__deserializer;
                public SqlMapper.GridReader <>4__this;
                private int index;
                public int <>3__index;
                private Type <convertToType>5__2;

                [DebuggerHidden]
                public <ReadDeferred>d__52(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    SqlMapper.GridReader reader = this.<>4__this;
                    if (this.index == reader.gridIndex)
                    {
                        reader.NextResult();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        SqlMapper.GridReader reader = this.<>4__this;
                        switch (this.<>1__state)
                        {
                            case 0:
                            {
                                this.<>1__state = -1;
                                this.<>1__state = -3;
                                Type underlyingType = Nullable.GetUnderlyingType(this.effectiveType);
                                Type effectiveType = underlyingType;
                                if (underlyingType == null)
                                {
                                    Type local1 = underlyingType;
                                    effectiveType = this.effectiveType;
                                }
                                this.<convertToType>5__2 = effectiveType;
                                break;
                            }
                            case 1:
                                this.<>1__state = -3;
                                break;

                            case 2:
                                this.<>1__state = -3;
                                break;

                            default:
                                return false;
                        }
                        if ((this.index != reader.gridIndex) || !reader.reader.Read())
                        {
                            this.<convertToType>5__2 = null;
                            this.<>m__Finally1();
                            flag = false;
                        }
                        else
                        {
                            object obj2 = this.deserializer(reader.reader);
                            if ((obj2 != null) && !(obj2 is T))
                            {
                                this.<>2__current = (T) Convert.ChangeType(obj2, this.<convertToType>5__2, CultureInfo.InvariantCulture);
                                this.<>1__state = 2;
                                flag = true;
                            }
                            else
                            {
                                this.<>2__current = (T) obj2;
                                this.<>1__state = 1;
                                flag = true;
                            }
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<T> IEnumerable<T>.GetEnumerator()
                {
                    SqlMapper.GridReader.<ReadDeferred>d__52<T> d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = (SqlMapper.GridReader.<ReadDeferred>d__52<T>) this;
                    }
                    else
                    {
                        d__ = new SqlMapper.GridReader.<ReadDeferred>d__52<T>(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    d__.index = this.<>3__index;
                    d__.deserializer = this.<>3__deserializer;
                    d__.effectiveType = this.<>3__effectiveType;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || ((num - 1) <= 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private struct <ReadRowAsyncImplViaDbReader>d__20<T> : IAsyncStateMachine
            {
                public int <>1__state;
                public AsyncTaskMethodBuilder<T> <>t__builder;
                public DbDataReader reader;
                public SqlMapper.GridReader <>4__this;
                public Type type;
                public SqlMapper.Row row;
                private T <result>5__2;
                private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
                private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

                private void MoveNext()
                {
                    int num = this.<>1__state;
                    SqlMapper.GridReader reader = this.<>4__this;
                    try
                    {
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
                        bool flag3;
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
                        bool flag5;
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter4;
                        switch (num)
                        {
                            case 0:
                                awaiter = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                break;

                            case 1:
                                awaiter2 = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0013;

                            case 2:
                                awaiter3 = this.<>u__1;
                                this.<>u__1 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_000C;

                            case 3:
                                awaiter4 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                                goto TR_0009;

                            default:
                                if (this.reader == null)
                                {
                                    throw new ObjectDisposedException(reader.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
                                }
                                if (reader.IsConsumed)
                                {
                                    throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
                                }
                                reader.IsConsumed = true;
                                this.<result>5__2 = default(T);
                                awaiter = this.reader.ReadAsync(reader.cancel).ConfigureAwait(false).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    break;
                                }
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>>(ref awaiter, ref (SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>) ref this);
                                return;
                        }
                        if (!awaiter.GetResult() || (this.reader.FieldCount == 0))
                        {
                            if ((this.row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
                            {
                                SqlMapper.ThrowZeroRows(this.row);
                            }
                            goto TR_000A;
                        }
                        else
                        {
                            SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(reader.identity.ForGrid(this.type, reader.gridIndex), null, reader.addToCache);
                            SqlMapper.DeserializerState deserializer = info.Deserializer;
                            int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
                            if ((deserializer.Func == null) || (deserializer.Hash != hash))
                            {
                                deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(this.type, this.reader, 0, -1, false));
                                info.Deserializer = deserializer;
                            }
                            this.<result>5__2 = (T) deserializer.Func(this.reader);
                            flag3 = (this.row & SqlMapper.Row.Single) != SqlMapper.Row.First;
                            if (!flag3)
                            {
                                goto TR_0012;
                            }
                            else
                            {
                                awaiter2 = this.reader.ReadAsync(reader.cancel).ConfigureAwait(false).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto TR_0013;
                                }
                                else
                                {
                                    this.<>1__state = num = 1;
                                    this.<>u__1 = awaiter2;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>>(ref awaiter2, ref (SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>) ref this);
                                }
                            }
                        }
                        return;
                    TR_0009:
                        awaiter4.GetResult();
                        T result = this.<result>5__2;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    TR_000A:
                        awaiter4 = reader.NextResultAsync().ConfigureAwait(false).GetAwaiter();
                        if (awaiter4.IsCompleted)
                        {
                            goto TR_0009;
                        }
                        else
                        {
                            this.<>1__state = num = 3;
                            this.<>u__2 = awaiter4;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>>(ref awaiter4, ref (SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>) ref this);
                        }
                        return;
                    TR_000C:
                        flag5 = awaiter3.GetResult();
                        if (!flag5)
                        {
                            goto TR_000A;
                        }
                    TR_0010:
                        while (true)
                        {
                            awaiter3 = this.reader.ReadAsync(reader.cancel).ConfigureAwait(false).GetAwaiter();
                            if (!awaiter3.IsCompleted)
                            {
                                this.<>1__state = num = 2;
                                this.<>u__1 = awaiter3;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>>(ref awaiter3, ref (SqlMapper.GridReader.<ReadRowAsyncImplViaDbReader>d__20<T>) ref this);
                                break;
                            }
                            goto TR_000C;
                        }
                        return;
                    TR_0012:
                        if (flag3)
                        {
                            SqlMapper.ThrowMultipleRows(this.row);
                        }
                        goto TR_0010;
                    TR_0013:
                        flag3 = awaiter2.GetResult();
                        goto TR_0012;
                    }
                    catch (Exception exception)
                    {
                        this.<>1__state = -2;
                        this.<>t__builder.SetException(exception);
                    }
                }

                [DebuggerHidden]
                private void SetStateMachine(IAsyncStateMachine stateMachine)
                {
                    this.<>t__builder.SetStateMachine(stateMachine);
                }
            }
        }

        public interface ICustomQueryParameter
        {
            void AddParameter(IDbCommand command, string name);
        }

        public class Identity : IEquatable<SqlMapper.Identity>
        {
            public readonly string sql;
            public readonly CommandType? commandType;
            public readonly int hashCode;
            public readonly int gridIndex;
            public readonly Type type;
            public readonly string connectionString;
            public readonly Type parametersType;

            internal Identity(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType) : this(sql, commandType, connection.ConnectionString, type, parametersType, 0, 0)
            {
            }

            private protected Identity(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType, int otherTypesHash, int gridIndex)
            {
                this.sql = sql;
                this.commandType = commandType;
                this.connectionString = connectionString;
                this.type = type;
                this.parametersType = parametersType;
                this.gridIndex = gridIndex;
                this.hashCode = 0x11;
                this.hashCode = (this.hashCode * 0x17) + commandType.GetHashCode();
                this.hashCode = (this.hashCode * 0x17) + gridIndex.GetHashCode();
                this.hashCode = (this.hashCode * 0x17) + ((sql != null) ? sql.GetHashCode() : 0);
                this.hashCode = (this.hashCode * 0x17) + ((type != null) ? type.GetHashCode() : 0);
                this.hashCode = (this.hashCode * 0x17) + otherTypesHash;
                this.hashCode = (this.hashCode * 0x17) + ((connectionString == null) ? 0 : SqlMapper.connectionStringComparer.GetHashCode(connectionString));
                this.hashCode = (this.hashCode * 0x17) + ((parametersType != null) ? parametersType.GetHashCode() : 0);
            }

            public bool Equals(SqlMapper.Identity other)
            {
                int num;
                if (ReferenceEquals(this, other))
                {
                    return true;
                }
                if (other == null)
                {
                    return false;
                }
                if ((this.gridIndex != other.gridIndex) || (!(this.type == other.type) || (this.sql != other.sql)))
                {
                    return false;
                }
                CommandType? commandType = this.commandType;
                CommandType? nullable2 = other.commandType;
                return (((commandType.GetValueOrDefault() == nullable2.GetValueOrDefault()) & ((commandType != null) == (nullable2 != null))) && (SqlMapper.connectionStringComparer.Equals(this.connectionString, other.connectionString) && ((this.parametersType == other.parametersType) && (((num = this.TypeCount) == other.TypeCount) && ((num == 0) || TypesEqual(this, other, num))))));
            }

            public override bool Equals(object obj) => 
                this.Equals(obj as SqlMapper.Identity);

            public SqlMapper.Identity ForDynamicParameters(Type type) => 
                new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, this.type, type, 0, -1);

            internal SqlMapper.Identity ForGrid(Type primaryType, int gridIndex) => 
                new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, 0, gridIndex);

            internal SqlMapper.Identity ForGrid<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(Type primaryType, int gridIndex) => 
                new SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, gridIndex);

            internal SqlMapper.Identity ForGrid(Type primaryType, Type[] otherTypes, int gridIndex) => 
                ((otherTypes == null) || (otherTypes.Length == 0)) ? new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, 0, gridIndex) : new SqlMapper.IdentityWithTypes(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, otherTypes, gridIndex);

            public override int GetHashCode() => 
                this.hashCode;

            internal virtual Type GetType(int index)
            {
                throw new IndexOutOfRangeException("index");
            }

            public override string ToString() => 
                this.sql;

            [MethodImpl(MethodImplOptions.NoInlining)]
            private static bool TypesEqual(SqlMapper.Identity x, SqlMapper.Identity y, int count)
            {
                if (y.TypeCount != count)
                {
                    return false;
                }
                for (int i = 0; i < count; i++)
                {
                    if (x.GetType(i) != y.GetType(i))
                    {
                        return false;
                    }
                }
                return true;
            }

            internal virtual int TypeCount =>
                0;
        }

        internal sealed class Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh> : SqlMapper.Identity
        {
            private static readonly int s_typeHash;
            private static readonly int s_typeCount;

            static Identity()
            {
                SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.s_typeCount = SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.CountNonTrivial(out SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.s_typeHash);
            }

            internal Identity(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType, int gridIndex = 0) : base(sql, commandType, connection.ConnectionString, type, parametersType, SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.s_typeHash, gridIndex)
            {
            }

            internal Identity(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType, int gridIndex = 0) : base(sql, commandType, connectionString, type, parametersType, SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.s_typeHash, gridIndex)
            {
            }

            private static int CountNonTrivial(out int hashCode)
            {
                int count;
                int hashCodeLocal;
                bool Map<TFirst>()
                {
                    if (!(typeof(TFirst) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TFirst).GetHashCode();
                    return true;
                }
                bool Map<TSecond>()
                {
                    if (!(typeof(TSecond) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TSecond).GetHashCode();
                    return true;
                }
                bool Map<TThird>()
                {
                    if (!(typeof(TThird) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TThird).GetHashCode();
                    return true;
                }
                bool Map<TFourth>()
                {
                    if (!(typeof(TFourth) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TFourth).GetHashCode();
                    return true;
                }
                bool Map<TFifth>()
                {
                    if (!(typeof(TFifth) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TFifth).GetHashCode();
                    return true;
                }
                bool Map<TSixth>()
                {
                    if (!(typeof(TSixth) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TSixth).GetHashCode();
                    return true;
                }
                bool Map<TSeventh>()
                {
                    if (!(typeof(TSeventh) != typeof(SqlMapper.DontMap)))
                    {
                        return false;
                    }
                    int num = count;
                    count = num + 1;
                    hashCodeLocal = (hashCodeLocal * 0x17) + typeof(TSeventh).GetHashCode();
                    return true;
                }
                hashCodeLocal = 0;
                count = 0;
                bool local1 = (SqlMapper.Identity<, , , , , , >.Map() && (SqlMapper.Identity<, , , , , , >.Map() && (SqlMapper.Identity<, , , , , , >.Map() && (SqlMapper.Identity<, , , , , , >.Map() && (SqlMapper.Identity<, , , , , , >.Map() && SqlMapper.Identity<, , , , , , >.Map()))))) && SqlMapper.Identity<, , , , , , >.Map();
                hashCode = hashCodeLocal;
                return count;
            }

            internal override Type GetType(int index)
            {
                switch (index)
                {
                    case 0:
                        return typeof(TFirst);

                    case 1:
                        return typeof(TSecond);

                    case 2:
                        return typeof(TThird);

                    case 3:
                        return typeof(TFourth);

                    case 4:
                        return typeof(TFifth);

                    case 5:
                        return typeof(TSixth);

                    case 6:
                        return typeof(TSeventh);
                }
                return base.GetType(index);
            }

            internal override int TypeCount =>
                SqlMapper.Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.s_typeCount;
        }

        internal sealed class IdentityWithTypes : SqlMapper.Identity
        {
            private readonly Type[] _types;

            internal IdentityWithTypes(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType, Type[] otherTypes, int gridIndex = 0) : base(sql, commandType, connection.ConnectionString, type, parametersType, HashTypes(otherTypes), gridIndex)
            {
                Type[] emptyTypes = otherTypes;
                if (otherTypes == null)
                {
                    Type[] local1 = otherTypes;
                    emptyTypes = Type.EmptyTypes;
                }
                this._types = emptyTypes;
            }

            internal IdentityWithTypes(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType, Type[] otherTypes, int gridIndex = 0) : base(sql, commandType, connectionString, type, parametersType, HashTypes(otherTypes), gridIndex)
            {
                Type[] emptyTypes = otherTypes;
                if (otherTypes == null)
                {
                    Type[] local1 = otherTypes;
                    emptyTypes = Type.EmptyTypes;
                }
                this._types = emptyTypes;
            }

            internal override Type GetType(int index) => 
                this._types[index];

            private static int HashTypes(Type[] types)
            {
                int num = 0;
                if (types != null)
                {
                    foreach (Type type in types)
                    {
                        num = (num * 0x17) + ((type != null) ? type.GetHashCode() : 0);
                    }
                }
                return num;
            }

            internal override int TypeCount =>
                this._types.Length;
        }

        public interface IDynamicParameters
        {
            void AddParameters(IDbCommand command, SqlMapper.Identity identity);
        }

        public interface IMemberMap
        {
            string ColumnName { get; }

            Type MemberType { get; }

            PropertyInfo Property { get; }

            FieldInfo Field { get; }

            ParameterInfo Parameter { get; }
        }

        public interface IParameterCallbacks : SqlMapper.IDynamicParameters
        {
            void OnCompleted();
        }

        public interface IParameterLookup : SqlMapper.IDynamicParameters
        {
            object this[string name] { get; }
        }

        public interface ITypeHandler
        {
            object Parse(Type destinationType, object value);
            void SetValue(IDbDataParameter parameter, object value);
        }

        public interface ITypeMap
        {
            ConstructorInfo FindConstructor(string[] names, Type[] types);
            ConstructorInfo FindExplicitConstructor();
            SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName);
            SqlMapper.IMemberMap GetMember(string columnName);
        }

        internal class Link<TKey, TValue> where TKey: class
        {
            private Link(TKey key, TValue value, SqlMapper.Link<TKey, TValue> tail)
            {
                this.<Key>k__BackingField = key;
                this.<Value>k__BackingField = value;
                this.<Tail>k__BackingField = tail;
            }

            public static bool TryAdd(ref SqlMapper.Link<TKey, TValue> head, TKey key, ref TValue value)
            {
                while (true)
                {
                    TValue local;
                    SqlMapper.Link<TKey, TValue> link = Interlocked.CompareExchange<SqlMapper.Link<TKey, TValue>>(ref head, null, null);
                    if (SqlMapper.Link<TKey, TValue>.TryGet(link, key, out local))
                    {
                        value = local;
                        return false;
                    }
                    SqlMapper.Link<TKey, TValue> link2 = new SqlMapper.Link<TKey, TValue>(key, value, link);
                    if (Interlocked.CompareExchange<SqlMapper.Link<TKey, TValue>>(ref head, link2, link) == link)
                    {
                        return true;
                    }
                }
            }

            public static bool TryGet(SqlMapper.Link<TKey, TValue> link, TKey key, out TValue value)
            {
                while (link != null)
                {
                    if (key == link.Key)
                    {
                        value = link.Value;
                        return true;
                    }
                    link = link.Tail;
                }
                value = default(TValue);
                return false;
            }

            public TKey Key { get; }

            public TValue Value { get; }

            public SqlMapper.Link<TKey, TValue> Tail { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LiteralToken
        {
            internal static readonly IList<SqlMapper.LiteralToken> None;
            public string Token { get; }
            public string Member { get; }
            internal LiteralToken(string token, string member)
            {
                this.<Token>k__BackingField = token;
                this.<Member>k__BackingField = member;
            }

            static LiteralToken()
            {
                None = new SqlMapper.LiteralToken[0];
            }
        }

        private class PropertyInfoByNameComparer : IComparer<PropertyInfo>
        {
            public int Compare(PropertyInfo x, PropertyInfo y) => 
                string.CompareOrdinal(x.Name, y.Name);
        }

        [Flags]
        internal enum Row
        {
            First,
            FirstOrDefault,
            Single,
            SingleOrDefault
        }

        public static class Settings
        {
            private const CommandBehavior DefaultAllowedCommandBehaviors = ~CommandBehavior.SingleResult;

            static Settings()
            {
                SetDefaults();
            }

            internal static bool DisableCommandBehaviorOptimizations(CommandBehavior behavior, Exception ex)
            {
                if ((AllowedCommandBehaviors != ~CommandBehavior.SingleResult) || (((behavior & (CommandBehavior.SingleRow | CommandBehavior.SingleResult)) == CommandBehavior.Default) || (!ex.Message.Contains("SingleResult") && !ex.Message.Contains("SingleRow"))))
                {
                    return false;
                }
                SetAllowedCommandBehaviors(CommandBehavior.SingleRow | CommandBehavior.SingleResult, false);
                return true;
            }

            private static void SetAllowedCommandBehaviors(CommandBehavior behavior, bool enabled)
            {
                if (enabled)
                {
                    AllowedCommandBehaviors |= behavior;
                }
                else
                {
                    AllowedCommandBehaviors &= ~behavior;
                }
            }

            public static void SetDefaults()
            {
                CommandTimeout = null;
                ApplyNullValues = false;
            }

            internal static CommandBehavior AllowedCommandBehaviors { get; private set; }

            public static bool UseSingleResultOptimization
            {
                get => 
                    (AllowedCommandBehaviors & CommandBehavior.SingleResult) != CommandBehavior.Default;
                set => 
                    SetAllowedCommandBehaviors(CommandBehavior.SingleResult, value);
            }

            public static bool UseSingleRowOptimization
            {
                get => 
                    (AllowedCommandBehaviors & CommandBehavior.SingleRow) != CommandBehavior.Default;
                set => 
                    SetAllowedCommandBehaviors(CommandBehavior.SingleRow, value);
            }

            public static int? CommandTimeout { get; set; }

            public static bool ApplyNullValues { get; set; }

            public static bool PadListExpansions { get; set; }

            public static int InListStringSplitCount { get; set; }
        }

        public abstract class StringTypeHandler<T> : SqlMapper.TypeHandler<T>
        {
            protected StringTypeHandler()
            {
            }

            protected abstract string Format(T xml);
            public override T Parse(object value)
            {
                if ((value != null) && !(value is DBNull))
                {
                    return this.Parse((string) value);
                }
                return default(T);
            }

            protected abstract T Parse(string xml);
            public override void SetValue(IDbDataParameter parameter, T value)
            {
                parameter.Value = (value == null) ? ((object) DBNull.Value) : ((object) this.Format(value));
            }
        }

        private class TypeDeserializerCache
        {
            private static readonly Hashtable byType = new Hashtable();
            private readonly Type type;
            private readonly Dictionary<DeserializerKey, Func<IDataReader, object>> readers = new Dictionary<DeserializerKey, Func<IDataReader, object>>();

            private TypeDeserializerCache(Type type)
            {
                this.type = type;
            }

            private Func<IDataReader, object> GetReader(IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
            {
                Func<IDataReader, object> func;
                if (length < 0)
                {
                    length = reader.FieldCount - startBound;
                }
                int hashCode = SqlMapper.GetColumnHash(reader, startBound, length);
                if (returnNullIfFirstMissing)
                {
                    hashCode *= -27;
                }
                DeserializerKey key = new DeserializerKey(hashCode, startBound, length, returnNullIfFirstMissing, reader, false);
                Dictionary<DeserializerKey, Func<IDataReader, object>> readers = this.readers;
                lock (readers)
                {
                    if (this.readers.TryGetValue(key, out func))
                    {
                        return func;
                    }
                }
                func = SqlMapper.GetTypeDeserializerImpl(this.type, reader, startBound, length, returnNullIfFirstMissing);
                key = new DeserializerKey(hashCode, startBound, length, returnNullIfFirstMissing, reader, true);
                Dictionary<DeserializerKey, Func<IDataReader, object>> dictionary2 = this.readers;
                lock (dictionary2)
                {
                    return (this.readers[key] = func);
                }
            }

            internal static Func<IDataReader, object> GetReader(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
            {
                SqlMapper.TypeDeserializerCache cache = (SqlMapper.TypeDeserializerCache) SqlMapper.TypeDeserializerCache.byType[type];
                if (cache == null)
                {
                    Hashtable byType = SqlMapper.TypeDeserializerCache.byType;
                    lock (byType)
                    {
                        cache = (SqlMapper.TypeDeserializerCache) SqlMapper.TypeDeserializerCache.byType[type];
                        if (cache == null)
                        {
                            SqlMapper.TypeDeserializerCache.byType[type] = cache = new SqlMapper.TypeDeserializerCache(type);
                        }
                    }
                }
                return cache.GetReader(reader, startBound, length, returnNullIfFirstMissing);
            }

            internal static void Purge()
            {
                Hashtable byType = SqlMapper.TypeDeserializerCache.byType;
                lock (byType)
                {
                    SqlMapper.TypeDeserializerCache.byType.Clear();
                }
            }

            internal static void Purge(Type type)
            {
                Hashtable byType = SqlMapper.TypeDeserializerCache.byType;
                lock (byType)
                {
                    SqlMapper.TypeDeserializerCache.byType.Remove(type);
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct DeserializerKey : IEquatable<SqlMapper.TypeDeserializerCache.DeserializerKey>
            {
                private readonly int startBound;
                private readonly int length;
                private readonly bool returnNullIfFirstMissing;
                private readonly IDataReader reader;
                private readonly string[] names;
                private readonly Type[] types;
                private readonly int hashCode;
                public DeserializerKey(int hashCode, int startBound, int length, bool returnNullIfFirstMissing, IDataReader reader, bool copyDown)
                {
                    this.hashCode = hashCode;
                    this.startBound = startBound;
                    this.length = length;
                    this.returnNullIfFirstMissing = returnNullIfFirstMissing;
                    if (!copyDown)
                    {
                        this.reader = reader;
                        this.names = null;
                        this.types = null;
                    }
                    else
                    {
                        this.reader = null;
                        this.names = new string[length];
                        this.types = new Type[length];
                        int i = startBound;
                        for (int j = 0; j < length; j++)
                        {
                            this.names[j] = reader.GetName(i);
                            this.types[j] = reader.GetFieldType(i++);
                        }
                    }
                }

                public override int GetHashCode() => 
                    this.hashCode;

                public override string ToString()
                {
                    if (this.names != null)
                    {
                        return string.Join(", ", this.names);
                    }
                    if (this.reader == null)
                    {
                        return this.ToString();
                    }
                    StringBuilder builder = new StringBuilder();
                    int startBound = this.startBound;
                    for (int i = 0; i < this.length; i++)
                    {
                        if (i != 0)
                        {
                            builder.Append(", ");
                        }
                        builder.Append(this.reader.GetName(startBound++));
                    }
                    return builder.ToString();
                }

                public override bool Equals(object obj) => 
                    (obj is SqlMapper.TypeDeserializerCache.DeserializerKey) && this.Equals((SqlMapper.TypeDeserializerCache.DeserializerKey) obj);

                public bool Equals(SqlMapper.TypeDeserializerCache.DeserializerKey other)
                {
                    if ((this.hashCode == other.hashCode) && ((this.startBound == other.startBound) && ((this.length == other.length) && (this.returnNullIfFirstMissing == other.returnNullIfFirstMissing))))
                    {
                        int index = 0;
                        while (true)
                        {
                            string text1;
                            string text3;
                            Type type1;
                            Type type3;
                            if (index >= this.length)
                            {
                                return true;
                            }
                            if (this.names != null)
                            {
                                text1 = this.names[index];
                            }
                            else
                            {
                                string[] names = this.names;
                                text1 = null;
                            }
                            string local2 = text1;
                            string name = local2;
                            if (local2 == null)
                            {
                                string local3 = local2;
                                if (this.reader != null)
                                {
                                    name = this.reader.GetName(this.startBound + index);
                                }
                                else
                                {
                                    IDataReader reader = this.reader;
                                    name = null;
                                }
                            }
                            if (other.names != null)
                            {
                                text3 = other.names[index];
                            }
                            else
                            {
                                string[] names = other.names;
                                text3 = null;
                            }
                            string local6 = text3;
                            string name = local6;
                            if (local6 == null)
                            {
                                string local7 = local6;
                                if (other.reader != null)
                                {
                                    name = other.reader.GetName(this.startBound + index);
                                }
                                else
                                {
                                    IDataReader reader = other.reader;
                                    name = null;
                                }
                            }
                            if (name != name)
                            {
                                break;
                            }
                            if (this.types != null)
                            {
                                type1 = this.types[index];
                            }
                            else
                            {
                                Type[] types = this.types;
                                type1 = null;
                            }
                            Type local10 = type1;
                            Type fieldType = local10;
                            if (local10 == null)
                            {
                                Type local11 = local10;
                                if (this.reader != null)
                                {
                                    fieldType = this.reader.GetFieldType(this.startBound + index);
                                }
                                else
                                {
                                    IDataReader reader = this.reader;
                                    fieldType = null;
                                }
                            }
                            if (other.types != null)
                            {
                                type3 = other.types[index];
                            }
                            else
                            {
                                Type[] types = other.types;
                                type3 = null;
                            }
                            Type local14 = type3;
                            Type fieldType = local14;
                            if (local14 == null)
                            {
                                Type local15 = local14;
                                if (other.reader != null)
                                {
                                    fieldType = other.reader.GetFieldType(this.startBound + index);
                                }
                                else
                                {
                                    IDataReader reader = other.reader;
                                    fieldType = null;
                                }
                            }
                            if (fieldType != fieldType)
                            {
                                break;
                            }
                            index++;
                        }
                    }
                    return false;
                }
            }
        }

        public abstract class TypeHandler<T> : SqlMapper.ITypeHandler
        {
            protected TypeHandler()
            {
            }

            object SqlMapper.ITypeHandler.Parse(Type destinationType, object value) => 
                this.Parse(value);

            void SqlMapper.ITypeHandler.SetValue(IDbDataParameter parameter, object value)
            {
                if (value is DBNull)
                {
                    parameter.Value = value;
                }
                else
                {
                    this.SetValue(parameter, (T) value);
                }
            }

            public abstract T Parse(object value);
            public abstract void SetValue(IDbDataParameter parameter, T value);
        }

        [Obsolete("This method is for internal use only", false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static class TypeHandlerCache<T>
        {
            private static SqlMapper.ITypeHandler handler;

            [Obsolete("This method is for internal use only", true)]
            public static T Parse(object value) => 
                (T) SqlMapper.TypeHandlerCache<T>.handler.Parse(typeof(T), value);

            internal static void SetHandler(SqlMapper.ITypeHandler handler)
            {
                SqlMapper.TypeHandlerCache<T>.handler = handler;
            }

            [Obsolete("This method is for internal use only", true)]
            public static void SetValue(IDbDataParameter parameter, object value)
            {
                SqlMapper.TypeHandlerCache<T>.handler.SetValue(parameter, value);
            }
        }

        public class UdtTypeHandler : SqlMapper.ITypeHandler
        {
            private readonly string udtTypeName;

            public UdtTypeHandler(string udtTypeName)
            {
                if (string.IsNullOrEmpty(udtTypeName))
                {
                    throw new ArgumentException("Cannot be null or empty", udtTypeName);
                }
                this.udtTypeName = udtTypeName;
            }

            object SqlMapper.ITypeHandler.Parse(Type destinationType, object value) => 
                (value is DBNull) ? null : value;

            void SqlMapper.ITypeHandler.SetValue(IDbDataParameter parameter, object value)
            {
                parameter.Value = SqlMapper.SanitizeParameterValue(value);
                if (!(value is DBNull))
                {
                    StructuredHelper.ConfigureUDT(parameter, this.udtTypeName);
                }
            }
        }
    }
}

