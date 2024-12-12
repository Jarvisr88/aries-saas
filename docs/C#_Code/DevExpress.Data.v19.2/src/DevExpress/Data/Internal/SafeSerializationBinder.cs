namespace DevExpress.Data.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class SafeSerializationBinder
    {
        internal static BinaryFormatter CreateFormatter(ref BinaryFormatter formatter);
        public static void Ensure(string assemblyName, string typeName);
        internal static TFormatter Initialize<TFormatter>(TFormatter formatter) where TFormatter: IFormatter;
        public static void RegisterKnownType(Type type);

        private static class DXKnownTypes
        {
            private static readonly object syncObj;
            private static readonly List<KeyValuePair<string, string>> controlDependencies;

            static DXKnownTypes();
            internal static string GetDXAssembly(string assembly);
            internal static bool IsControlDependency(string assembly, string type);
            private static bool IsKnownControlTypeCore(string assembly, string type);
            internal static bool Match(KeyValuePair<string, string>[] types, string assembly, string type);
            internal static void RegisterControlDependency(string assembly, string type);
        }

        private sealed class DXSerializationBinder : SerializationBinder
        {
            private readonly bool isNetDataContractsSerializationBinder;

            public DXSerializationBinder(bool isNetDataContracts);
            public sealed override Type BindToType(string assemblyName, string typeName);
            private Assembly GetAssembly(string assemblyName);
        }

        private static class TypeRanges
        {
            internal static bool Match(KeyValuePair<string, string> asmAndType, string assembly, string type);
            internal static bool Match(KeyValuePair<string, string>[] ranges, string assembly, string type);
            internal static bool MatchDX(KeyValuePair<string, string>[] ranges, string assembly, string type);
        }

        private static class UnsafeTypes
        {
            private static readonly HashSet<string> types;
            private static readonly KeyValuePair<string, string>[] typeRanges;
            private static readonly KeyValuePair<string, string>[] dxTypeRanges;

            static UnsafeTypes();
            public static bool Match(string assembly, string type);
        }

        private static class WhiteList
        {
            private static readonly HashSet<string> types;
            private static readonly KeyValuePair<string, string>[] dxKnownTypes;
            private static readonly KeyValuePair<string, string>[] typeRanges;
            private static readonly KeyValuePair<string, string>[] dxTypeRanges;

            static WhiteList();
            public static bool Match(string assembly, string type);
            public static void RegisterDXControlDependency(Type type);
            public static void RegisterDXControlDependency(string assembly, string type);
        }
    }
}

