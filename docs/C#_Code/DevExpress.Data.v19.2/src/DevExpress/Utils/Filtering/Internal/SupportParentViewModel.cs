namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class SupportParentViewModel
    {
        private static IDictionary<string, Func<object, object>> accessorsCache;
        private static IDictionary<string, Action<object, object>> mutatorsCache;

        static SupportParentViewModel();
        private static Func<object, object> GetGetParentViewModel(Type type);
        internal static object GetParentViewModel(this object @this);
        private static Action<object, object> GetSetParentViewModel(Type type);
        internal static void SetParentViewModel(this object @this, object parentViewModel);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SupportParentViewModel.<>c <>9;
            public static Func<object, Func<object, object>> <>9__0_0;
            public static Func<object, Action<object, object>> <>9__1_0;

            static <>c();
            internal Func<object, object> <GetParentViewModel>b__0_0(object x);
            internal Action<object, object> <SetParentViewModel>b__1_0(object x);
        }
    }
}

