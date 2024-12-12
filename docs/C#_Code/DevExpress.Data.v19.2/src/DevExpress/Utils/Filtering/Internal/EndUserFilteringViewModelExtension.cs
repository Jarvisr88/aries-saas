namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class EndUserFilteringViewModelExtension
    {
        private static readonly IDictionary<Type, Func<IEndUserFilteringViewModel, IEndUserFilteringViewModelPropertyValues>> accessorsCache;

        static EndUserFilteringViewModelExtension();
        public static IEndUserFilteringMetricViewModel GetProperty(this IEndUserFilteringViewModel viewModel, string path);
        public static IServiceProvider GetServiceProvider(this IEndUserFilteringViewModel viewModel);
        private static IEndUserFilteringViewModelPropertyValues GetValues(IEndUserFilteringViewModel viewModel);
        private static Func<IEndUserFilteringViewModel, IEndUserFilteringViewModelPropertyValues> GetValuesAccessor(Type viewModelType);
        private static FieldInfo GetValuesField(Type viewModelType);
        private static Func<IEndUserFilteringViewModel, IEndUserFilteringViewModelPropertyValues> MakeValuesAccessor(Type viewModelType);
        public static bool ParseFilterCriteria(this IEndUserFilteringViewModel viewModel, string path, CriteriaOperator criteria);
        public static CriteriaOperator QueryFilterCriteria(this IEndUserFilteringViewModel viewModel, string path, CriteriaOperator criteria);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringViewModelExtension.<>c <>9;
            public static Func<FieldInfo, bool> <>9__8_0;

            static <>c();
            internal bool <GetValuesField>b__8_0(FieldInfo f);
        }
    }
}

