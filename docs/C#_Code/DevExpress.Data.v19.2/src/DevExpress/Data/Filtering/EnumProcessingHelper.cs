namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    public static class EnumProcessingHelper
    {
        private static ReaderWriterLockSlim _RWL;
        private static readonly Dictionary<string, Type> stringsToEnumType;
        private static readonly Dictionary<Type, string> enumsToToStringName;

        static EnumProcessingHelper();
        internal static void ExtractEnumIfNeeded(UserValueProcessingEventArgs e);
        private static Type GetEnumTypeFromName(string typeName);
        private static string GetNameForEnumType(Type enumType);
        private static IDisposable LockForChange();
        private static IDisposable LockForRead();
        private static void ProcessRegistering(Assembly currentAssembly, IDictionary<Assembly, object> passedAssemblies, bool suppressReferencesProcessing);
        public static void RegisterEnum<T>();
        public static void RegisterEnum(Type enumType);
        public static void RegisterEnum(Type enumType, string toStringName);
        public static void RegisterEnums();
        public static void RegisterEnums(Assembly assembly);
        public static void RegisterEnums(Assembly[] assemblies);
        public static void RegisterEnums(Assembly assembly, bool suppressReferencesProcessing);
        public static void RegisterEnums(Assembly[] assemblies, bool suppressReferencesProcessing);
        internal static void ToStringEnumIfNeeded(UserValueProcessingEventArgs e);
    }
}

