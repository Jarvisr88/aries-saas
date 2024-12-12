namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    internal static class NullableTypesCache
    {
        private static readonly ConcurrentDictionary<Type, Type> cache;
        private static readonly Type NullableBase;

        static NullableTypesCache();
        internal static Type Get(Type type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NullableTypesCache.<>c <>9;
            public static Func<Type, Type> <>9__3_0;

            static <>c();
            internal Type <Get>b__3_0(Type t);
        }
    }
}

