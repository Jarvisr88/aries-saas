namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    internal static class GenericTypeHelper
    {
        private static readonly ConcurrentDictionary<Type, bool> genericTypes;

        static GenericTypeHelper();
        private static Type[] FindGenericArguments(Type type, Predicate<Type> match);
        private static Type FindTypeDefinition(Type type, Predicate<Type> match);
        internal static Type GetElementType(Type dataSorceType);
        private static bool IsGenericType(Type type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GenericTypeHelper.<>c <>9;
            public static Predicate<Type> <>9__0_0;
            public static Func<Type, bool> <>9__4_0;

            static <>c();
            internal bool <GetElementType>b__0_0(Type t);
            internal bool <IsGenericType>b__4_0(Type t);
        }
    }
}

