namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal static class SupportServices
    {
        [DebuggerStepThrough, DebuggerHidden]
        internal static TService GetService<TService>(this IServiceProvider serviceProvider) where TService: class;
        [DebuggerStepThrough, DebuggerHidden]
        internal static object GetServiceObj(this IServiceProvider serviceProvider, Type serviceType);

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TService> where TService: class
        {
            public static readonly SupportServices.<>c__0<TService> <>9;
            public static Func<IServiceProvider, TService> <>9__0_0;

            static <>c__0();
            internal TService <GetService>b__0_0(IServiceProvider x);
        }
    }
}

