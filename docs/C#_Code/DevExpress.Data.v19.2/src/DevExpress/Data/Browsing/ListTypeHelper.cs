namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    public static class ListTypeHelper
    {
        private static readonly ConcurrentDictionary<Type, bool> genericTypes;

        static ListTypeHelper();
        public static Type[] FindGenericArguments(Type type, Predicate<Type> match);
        public static Type FindTypeDefinition(Type type, Predicate<Type> match);
        private static bool IsGenericType(Type type);
        private static bool IsGenericTypeCore(Type type);
        public static bool IsListType(Type type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListTypeHelper.<>c <>9;
            public static Predicate<Type> <>9__1_0;

            static <>c();
            internal bool <IsListType>b__1_0(Type x);
        }
    }
}

