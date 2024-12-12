namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    internal static class DefaultValuesCache
    {
        private static readonly ConcurrentDictionary<Type, object> cache;

        static DefaultValuesCache();
        internal static object Get(Type type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultValuesCache.<>c <>9;
            public static Func<Type, object> <>9__2_0;

            static <>c();
            internal object <Get>b__2_0(Type t);
        }
    }
}

