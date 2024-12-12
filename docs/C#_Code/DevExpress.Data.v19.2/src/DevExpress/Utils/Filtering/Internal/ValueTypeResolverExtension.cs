namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ValueTypeResolverExtension
    {
        internal static Type GetAttributesType(this IValueTypeResolver typeResolver, IEndUserFilteringMetric metric);
        internal static Type GetValueBoxType(this IValueTypeResolver typeResolver, IEndUserFilteringMetric metric);
        internal static Type GetValueViewModelType(this IValueTypeResolver typeResolver, IEndUserFilteringMetric metric);
    }
}

