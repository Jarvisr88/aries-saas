namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class EndUserFilteringViewModelPropertyValuesExtension
    {
        public static object EnsureDataBinding(this IEndUserFilteringViewModelPropertyValues values, IServiceProvider serviceProvider);
        public static void UpdateDataBinding(this IEndUserFilteringViewModelPropertyValues values, IServiceProvider serviceProvider, string excludePath = null);
        public static void UpdateDataBindings(this IEndUserFilteringViewModelPropertyValues values, IServiceProvider serviceProvider, string path = null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringViewModelPropertyValuesExtension.<>c <>9;
            public static Func<IServiceProvider, IEndUserFilteringViewModelProvider> <>9__0_0;
            public static Func<IServiceProvider, IEndUserFilteringViewModelDataContext> <>9__1_0;
            public static Func<IServiceProvider, IEndUserFilteringViewModelDataContext> <>9__2_0;

            static <>c();
            internal IEndUserFilteringViewModelProvider <EnsureDataBinding>b__0_0(IServiceProvider provider);
            internal IEndUserFilteringViewModelDataContext <UpdateDataBinding>b__1_0(IServiceProvider provider);
            internal IEndUserFilteringViewModelDataContext <UpdateDataBindings>b__2_0(IServiceProvider provider);
        }
    }
}

