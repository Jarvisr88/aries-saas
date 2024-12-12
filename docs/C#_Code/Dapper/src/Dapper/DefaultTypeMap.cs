namespace Dapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class DefaultTypeMap : SqlMapper.ITypeMap
    {
        private readonly List<FieldInfo> _fields;
        private readonly Type _type;

        public DefaultTypeMap(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            this._fields = GetSettableFields(type);
            this.<Properties>k__BackingField = GetSettableProps(type);
            this._type = type;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            Func<ConstructorInfo, int> keySelector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<ConstructorInfo, int> local1 = <>c.<>9__6_0;
                keySelector = <>c.<>9__6_0 = c => c.IsPublic ? 0 : (c.IsPrivate ? 2 : 1);
            }
            Func<ConstructorInfo, int> func2 = <>c.<>9__6_1;
            if (<>c.<>9__6_1 == null)
            {
                Func<ConstructorInfo, int> local2 = <>c.<>9__6_1;
                func2 = <>c.<>9__6_1 = c => c.GetParameters().Length;
            }
            using (IEnumerator<ConstructorInfo> enumerator = this._type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).OrderBy<ConstructorInfo, int>(keySelector).ThenBy<ConstructorInfo, int>(func2).GetEnumerator())
            {
                ConstructorInfo current;
                ParameterInfo[] parameters;
                int num;
                goto TR_0014;
            TR_0007:
                if (num == parameters.Length)
                {
                    return current;
                }
            TR_0014:
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        parameters = current.GetParameters();
                        if (parameters.Length != 0)
                        {
                            if (parameters.Length != types.Length)
                            {
                                continue;
                            }
                            for (num = 0; (num < parameters.Length) && string.Equals(parameters[num].Name, names[num], StringComparison.OrdinalIgnoreCase); num++)
                            {
                                if (!(types[num] == typeof(byte[])) || (parameters[num].ParameterType.FullName != "System.Data.Linq.Binary"))
                                {
                                    Type type = Nullable.GetUnderlyingType(parameters[num].ParameterType) ?? parameters[num].ParameterType;
                                    if (((type != types[num]) && (!SqlMapper.HasTypeHandler(type) && (!type.IsEnum || (Enum.GetUnderlyingType(type) != types[num])))) && (((type != typeof(char)) || (types[num] != typeof(string))) && (!type.IsEnum || (types[num] != typeof(string)))))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return current;
                        }
                    }
                    else
                    {
                        goto TR_0003;
                    }
                    break;
                }
                goto TR_0007;
            }
        TR_0003:
            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            Func<ConstructorInfo, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = c => c.GetCustomAttributes(typeof(ExplicitConstructorAttribute), true).Length != 0;
            }
            List<ConstructorInfo> list = this._type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<ConstructorInfo>(predicate).ToList<ConstructorInfo>();
            return ((list.Count != 1) ? null : list[0]);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            return new SimpleMemberMap(columnName, parameters.FirstOrDefault<ParameterInfo>(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase)));
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            PropertyInfo property = this.Properties.Find(p => string.Equals(p.Name, columnName, StringComparison.Ordinal)) ?? this.Properties.Find(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));
            if ((property == null) && MatchNamesWithUnderscores)
            {
                property = this.Properties.Find(p => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.Ordinal)) ?? this.Properties.Find(p => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
            }
            if (property != null)
            {
                return new SimpleMemberMap(columnName, property);
            }
            string backingFieldName = "<" + columnName + ">k__BackingField";
            FieldInfo local5 = this._fields.Find(p => string.Equals(p.Name, columnName, StringComparison.Ordinal));
            FieldInfo local17 = local5;
            if (local5 == null)
            {
                FieldInfo local6 = local5;
                FieldInfo local7 = this._fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal));
                local17 = local7;
                if (local7 == null)
                {
                    FieldInfo local8 = local7;
                    FieldInfo local9 = this._fields.Find(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));
                    local17 = local9;
                    if (local9 == null)
                    {
                        FieldInfo local10 = local9;
                        local17 = this._fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }
            FieldInfo field = local17;
            if ((field == null) && MatchNamesWithUnderscores)
            {
                string effectiveColumnName = columnName.Replace("_", "");
                backingFieldName = "<" + effectiveColumnName + ">k__BackingField";
                FieldInfo local11 = this._fields.Find(p => string.Equals(p.Name, effectiveColumnName, StringComparison.Ordinal));
                FieldInfo local18 = local11;
                if (local11 == null)
                {
                    FieldInfo local12 = local11;
                    FieldInfo local13 = this._fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal));
                    local18 = local13;
                    if (local13 == null)
                    {
                        FieldInfo local14 = local13;
                        FieldInfo local15 = this._fields.Find(p => string.Equals(p.Name, effectiveColumnName, StringComparison.OrdinalIgnoreCase));
                        local18 = local15;
                        if (local15 == null)
                        {
                            FieldInfo local16 = local15;
                            local18 = this._fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                }
                field = local18;
            }
            return ((field == null) ? null : new SimpleMemberMap(columnName, field));
        }

        internal static MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type type)
        {
            if (propertyInfo.DeclaringType == type)
            {
                return propertyInfo.GetSetMethod(true);
            }
            Func<ParameterInfo, Type> selector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__3_0;
                selector = <>c.<>9__3_0 = p => p.ParameterType;
            }
            return propertyInfo.DeclaringType.GetProperty(propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, propertyInfo.PropertyType, propertyInfo.GetIndexParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>(), null).GetSetMethod(true);
        }

        internal static List<FieldInfo> GetSettableFields(Type t) => 
            t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList<FieldInfo>();

        internal static List<PropertyInfo> GetSettableProps(Type t) => 
            (from p in t.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                where GetPropertySetter(p, t) != null
                select p).ToList<PropertyInfo>();

        public static bool MatchNamesWithUnderscores { get; set; }

        public List<PropertyInfo> Properties { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultTypeMap.<>c <>9 = new DefaultTypeMap.<>c();
            public static Func<ParameterInfo, Type> <>9__3_0;
            public static Func<ConstructorInfo, int> <>9__6_0;
            public static Func<ConstructorInfo, int> <>9__6_1;
            public static Func<ConstructorInfo, bool> <>9__7_0;

            internal int <FindConstructor>b__6_0(ConstructorInfo c) => 
                c.IsPublic ? 0 : (c.IsPrivate ? 2 : 1);

            internal int <FindConstructor>b__6_1(ConstructorInfo c) => 
                c.GetParameters().Length;

            internal bool <FindExplicitConstructor>b__7_0(ConstructorInfo c) => 
                c.GetCustomAttributes(typeof(ExplicitConstructorAttribute), true).Length != 0;

            internal Type <GetPropertySetter>b__3_0(ParameterInfo p) => 
                p.ParameterType;
        }
    }
}

