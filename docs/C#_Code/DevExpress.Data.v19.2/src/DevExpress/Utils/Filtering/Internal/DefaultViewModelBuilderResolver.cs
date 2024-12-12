namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    internal sealed class DefaultViewModelBuilderResolver : IViewModelBuilderResolver
    {
        internal static readonly DefaultViewModelBuilderResolver Instance;
        private static readonly IDictionary<Type, Func<IEndUserFilteringMetric, IViewModelBuilder>> initializers;

        static DefaultViewModelBuilderResolver();
        private DefaultViewModelBuilderResolver();
        IViewModelBuilder IViewModelBuilderResolver.CreateValueViewModelBuilder(IEndUserFilteringMetric metric);
        IViewModelBuilder IViewModelBuilderResolver.CreateViewModelBuilder();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultViewModelBuilderResolver.<>c <>9;

            static <>c();
            internal IViewModelBuilder <.cctor>b__11_0(IEndUserFilteringMetric metric);
            internal IViewModelBuilder <.cctor>b__11_1(IEndUserFilteringMetric metric);
            internal IViewModelBuilder <.cctor>b__11_2(IEndUserFilteringMetric metric);
            internal IViewModelBuilder <.cctor>b__11_3(IEndUserFilteringMetric metric);
        }

        private class BooleanChoiceValueViewModelBuilder : DefaultViewModelBuilderResolver.ValueViewModelBuilderBase<IBooleanChoiceMetricAttributes>
        {
            private static readonly string ValueProperty;

            static BooleanChoiceValueViewModelBuilder();
            public BooleanChoiceValueViewModelBuilder(IEndUserFilteringMetric metric);
            protected override void BuildBindablePropertyAttributesCore(PropertyInfo property, PropertyBuilder propertyBuilder);
            protected override bool CanProcessProperty(PropertyInfo property);
            protected override bool ForceBindableProperty(PropertyInfo property);
        }

        private class DefaultViewModelBuilder : IViewModelBuilder
        {
            internal static readonly IViewModelBuilder Instance;

            static DefaultViewModelBuilder();
            private DefaultViewModelBuilder();
            void IViewModelBuilder.BuildBindablePropertyAttributes(PropertyInfo property, PropertyBuilder builder);
            bool IViewModelBuilder.ForceBindableProperty(PropertyInfo property);

            string IViewModelBuilder.TypeNameModifier { get; }
        }

        private class EnumValueViewModelBuilder : DefaultViewModelBuilderResolver.ValueViewModelBuilderBase<IEnumChoiceMetricAttributes>
        {
            private static readonly string[] ForceBindableProperties;

            static EnumValueViewModelBuilder();
            public EnumValueViewModelBuilder(IEndUserFilteringMetric metric);
            protected override void BuildBindablePropertyAttributesCore(PropertyInfo property, PropertyBuilder propertyBuilder);
            protected override bool ForceBindableProperty(PropertyInfo property);
            protected override string[] GetPropertiesToProcess();
        }

        private class LookupValueViewModelBuilder : DefaultViewModelBuilderResolver.ValueViewModelBuilderBase<ILookupMetricAttributes>
        {
            private static readonly string ValuesProperty;

            static LookupValueViewModelBuilder();
            public LookupValueViewModelBuilder(IEndUserFilteringMetric metric);
            protected override void BuildBindablePropertyAttributesCore(PropertyInfo property, PropertyBuilder propertyBuilder);
            protected override bool CanProcessProperty(PropertyInfo property);
            protected override bool ForceBindableProperty(PropertyInfo property);
        }

        private class RangeValueViewModelBuilder : DefaultViewModelBuilderResolver.ValueViewModelBuilderBase<IRangeMetricAttributes>
        {
            private static readonly string[] ForceBindableProperties;

            static RangeValueViewModelBuilder();
            public RangeValueViewModelBuilder(IEndUserFilteringMetric metric);
            protected override void BuildBindablePropertyAttributesCore(PropertyInfo property, PropertyBuilder propertyBuilder);
            protected override bool ForceBindableProperty(PropertyInfo property);
            protected override string[] GetPropertiesToProcess();
        }

        private class ValueViewModelBuilderBase<TMetricAttributes> : IViewModelBuilder where TMetricAttributes: IMetricAttributes
        {
            private readonly IEndUserFilteringMetric metric;

            public ValueViewModelBuilderBase(IEndUserFilteringMetric metric);
            protected virtual void BuildBindablePropertyAttributesCore(PropertyInfo property, PropertyBuilder propertyBuilder);
            protected void BuildBindablePropertyFilterAttributes(PropertyBuilder propertyBuilder);
            protected virtual bool CanProcessProperty(PropertyInfo property);
            void IViewModelBuilder.BuildBindablePropertyAttributes(PropertyInfo property, PropertyBuilder propertyBuilder);
            bool IViewModelBuilder.ForceBindableProperty(PropertyInfo property);
            protected virtual bool ForceBindableProperty(PropertyInfo property);
            protected virtual string[] GetPropertiesToProcess();

            protected IEndUserFilteringMetric Metric { get; }

            protected TMetricAttributes MetricAttributes { get; }

            string IViewModelBuilder.TypeNameModifier { get; }
        }
    }
}

