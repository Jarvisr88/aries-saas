namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultLazyMetricAttributesFactory : LazyMetricAttributesFactoryBase
    {
        public static readonly ILazyMetricAttributesFactory Instance = new DefaultLazyMetricAttributesFactory();

        private DefaultLazyMetricAttributesFactory()
        {
        }
    }
}

