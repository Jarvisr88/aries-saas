namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;

    internal sealed class DefaultEndUserFilteringMetricAttributesFactory : IEndUserFilteringMetricAttributesFactory
    {
        internal static IEndUserFilteringMetricAttributesFactory Instance;

        static DefaultEndUserFilteringMetricAttributesFactory();
        private DefaultEndUserFilteringMetricAttributesFactory();
        IEndUserFilteringMetricAttributes IEndUserFilteringMetricAttributesFactory.Create(string path, Type type, Attribute[] attributes);
        IEndUserFilteringMetricAttributes IEndUserFilteringMetricAttributesFactory.Create(string path, Type type, AnnotationAttributes attributes);
    }
}

