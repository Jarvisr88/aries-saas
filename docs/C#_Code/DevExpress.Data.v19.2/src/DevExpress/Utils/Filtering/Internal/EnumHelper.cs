namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Type, Func<string, object>> parseMethodsCache;
        private static MethodInfo mInfo_TryParse;

        static EnumHelper();
        private static Func<string, object> CreateParseEnumMethod(Type enumType);
        internal static T[] GetFlags<T>(T enumValue) where T: struct;
        private static MethodInfo GetTryParseMethod();
        internal static bool IsFlags(Type enumType);
        internal static bool TryParse<TEnum>(Type enumType, string value, out TEnum result) where TEnum: struct;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumHelper.<>c <>9;
            public static Func<MethodInfo, bool> <>9__6_0;

            static <>c();
            internal bool <GetTryParseMethod>b__6_0(MethodInfo x);
        }
    }
}

