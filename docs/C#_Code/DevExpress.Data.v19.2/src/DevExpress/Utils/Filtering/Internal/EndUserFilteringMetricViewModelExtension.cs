namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public static class EndUserFilteringMetricViewModelExtension
    {
        public static void CompleteDataBinding(this IEndUserFilteringMetricViewModel metricViewModel, IServiceProvider serviceProvider);
        public static void InitializeDataBinding(this IEndUserFilteringMetricViewModel metricViewModel, IServiceProvider serviceProvider);
        public static void InitializeUIProperties(this IEndUserFilteringMetricViewModel metricViewModel, IFilterUIEditorProperties properties, IServiceProvider serviceProvider);
    }
}

