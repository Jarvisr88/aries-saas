namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class LookupUtils
    {
        private static readonly object InvalidKeyValue;
        private static readonly Func<object, object> defaultAccessor;
        private static HashSet<Type> dictionaryKeyValueTypes;

        static LookupUtils();
        public static object CheckDictionaryDisplayValue(object displayValue, ref string displayMember);
        public static object CheckDictionaryKeyValue(object keyValue, object dataSource, ref string valueMember);
        public static object CheckDictionaryKeyValue(object keyValue, object dataSource, ref string valueMember, ref string displayMember);
        private static object CheckDisplayValueAndDisplayMemberCore(object displayValue, ref string displayMember, bool isDictionaryKeyValue);
        private static object CheckKeyValueAndValueMemberCore(object keyValue, object dataSource, ref string valueMember, ref string displayMember, bool isDictionaryKeyValue);
        private static object GetDataRowColumnValue(DataRow dataRow, string keyMember);
        private static bool GetIsDictionaryKeyValueOrCache(object keyValue, Func<Type, bool> checkKeyValueType);
        private static bool GetIsDictionaryKeyValueTypeOrCache(Type keyValueType, Func<Type, bool> checkKeyValueType);
        public static object GetKeyValue(object entity, string keyMember);
        public static object[] GetKeyValues(object entity, string[] keyMembers);
        public static string[] GuessCascadingMembers(PropertyDescriptorCollection ownerProperties, PropertyDescriptorCollection properties);
        public static string[] GuessDisplayMembers(PropertyDescriptorCollection properties);
        public static string[] GuessValueMembers(PropertyDescriptorCollection properties);
        private static bool IsDictionaryEntry(Type type);
        private static bool IsDictionaryKey(object keyValue, object dataSource);
        public static bool IsDictionaryKeyMember(string memberName, Type memberType);
        public static bool IsDictionaryKeyValue(object keyValue);
        private static bool IsDictionaryKeyValueType(Type keyValueType);
        private static bool IsDictionaryType(Type type);
        public static bool IsInvalidKeyValue(object keyValue);
        private static bool IsKeyValuePair(Type type);
        public static void Reset(Type entityType, string keyMember = null);
        private static object UnwrapAsyncServerModeProxy(object entity);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookupUtils.<>c <>9;
            public static Func<DataRowView, DataRow> <>9__3_0;
            public static Func<object, Type> <>9__14_0;
            public static Func<Type, bool> <>9__19_0;
            public static Func<object, Type> <>9__20_0;
            public static Func<object, Type> <>9__20_1;
            public static Func<Type, bool> <>9__20_2;
            public static Func<Type, bool> <>9__21_0;
            public static Func<PropertyDescriptor, string> <>9__22_0;
            public static Func<PropertyDescriptor, string> <>9__23_1;
            public static Func<PropertyDescriptor, string> <>9__23_2;
            public static Func<PropertyDescriptor, string> <>9__25_0;

            static <>c();
            internal object <.cctor>b__28_0(object entity);
            internal Type <GetIsDictionaryKeyValueOrCache>b__14_0(object kv);
            internal DataRow <GetKeyValue>b__3_0(DataRowView drv);
            internal string <GuessCascadingMembers>b__25_0(PropertyDescriptor p);
            internal string <GuessDisplayMembers>b__23_1(PropertyDescriptor p);
            internal string <GuessDisplayMembers>b__23_2(PropertyDescriptor p);
            internal string <GuessValueMembers>b__22_0(PropertyDescriptor p);
            internal Type <IsDictionaryKey>b__20_0(object kv);
            internal Type <IsDictionaryKey>b__20_1(object ds);
            internal bool <IsDictionaryKey>b__20_2(Type i);
            internal bool <IsDictionaryType>b__21_0(Type gType);
            internal bool <IsKeyValuePair>b__19_0(Type gType);
        }

        private sealed class DisplayMemberAwareComparer : IComparer<string>
        {
            internal static readonly IComparer<string> Instance;

            static DisplayMemberAwareComparer();
            public int Compare(string x, string y);
        }

        private static class RelationsHelper
        {
            private static readonly PropertyDescriptor[] EmptyMembers;
            private static readonly string[] KeySuffixes;
            private static IDictionary<Type, string[]> findMethodsCache;
            private static IDictionary<Type, Type> rowTypesCache;
            private static PropertyInfo ColumnPropertyInfo;
            private static Type DataColumnPropertyDescriptorType;
            private static string[] KeyAttributes;

            static RelationsHelper();
            private static string[] GetFindByMethods(Type tableType);
            private static string GetTableName(PropertyDescriptor pd);
            private static Type GetTableType(PropertyDescriptor pd);
            private static Type GetTableType(Type rowType);
            private static Type GetTableType(PropertyDescriptor property, Type rowType);
            internal static PropertyDescriptor[] GuessKeyMembers(PropertyDescriptorCollection properties);
            internal static PropertyDescriptor[] GuessRelationshipMembers(PropertyDescriptorCollection ownerProperties, PropertyDescriptorCollection properties);
            private static bool HasKeyAttribute(PropertyDescriptor pd);
            private static bool IsEntityKeyProperty(string attributeName, Attribute a);
            private static bool IsExplicitKey(PropertyDescriptor pd);
            private static bool IsKey(Attribute a);
            private static bool IsKeyAttribute(string attributeName, Attribute a);
            private static bool IsKeyMember(PropertyDescriptor pd, PropertyDescriptor[] possibleKeys);
            private static bool IsPrimaryKey(string attributeName, Attribute a);
            private static bool IsRelatedTo(PropertyDescriptor property, PropertyDescriptor[] ownerKeys);
            private static bool IsRowKey(PropertyDescriptor property);
            private static bool IsRowKey(PropertyDescriptor property, PropertyDescriptor rowKeyProperty);
            private static bool IsRowKey(PropertyDescriptor property, Type rowType);
            private static bool IsRowKey(string propertyName, string rowElementName, Type tableType, string keySuffix);
            private static bool PossibleIsKeyMember(PropertyDescriptor pd);
            private static bool PossibleIsRelatedTo(PropertyDescriptor property, PropertyDescriptor[] keys, PropertyDescriptor[] ownerKeys);
            private static string Singularize(string tableName);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LookupUtils.RelationsHelper.<>c <>9;
                public static Func<MethodInfo, bool> <>9__15_0;
                public static Func<MethodInfo, string> <>9__15_1;
                public static Func<DataColumn, DataTable> <>9__21_1;
                public static Func<DataTable, Type> <>9__21_2;
                public static Func<DataColumn, DataTable> <>9__22_1;
                public static Func<DataTable, string> <>9__22_2;

                static <>c();
                internal bool <GetFindByMethods>b__15_0(MethodInfo m);
                internal string <GetFindByMethods>b__15_1(MethodInfo m);
                internal DataTable <GetTableName>b__22_1(DataColumn col);
                internal string <GetTableName>b__22_2(DataTable table);
                internal DataTable <GetTableType>b__21_1(DataColumn col);
                internal Type <GetTableType>b__21_2(DataTable table);
            }
        }
    }
}

