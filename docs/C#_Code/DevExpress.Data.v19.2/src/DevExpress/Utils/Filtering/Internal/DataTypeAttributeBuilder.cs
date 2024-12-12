namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class DataTypeAttributeBuilder
    {
        private static readonly bool IsConditionallyAPTCAIssueThreat;
        private static readonly ConstructorInfo attributeCtor;

        static DataTypeAttributeBuilder();
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric);
        private static CustomAttributeBuilder BuildCore(IEndUserFilteringMetric metric);
    }
}

