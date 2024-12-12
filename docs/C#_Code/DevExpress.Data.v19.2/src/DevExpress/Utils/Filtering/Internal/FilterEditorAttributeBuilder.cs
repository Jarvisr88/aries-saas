namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class FilterEditorAttributeBuilder
    {
        private static readonly ConstructorInfo numericRangeAttributeCtor;
        private static readonly ConstructorInfo dateTimeRangeAttributeCtor;
        private static readonly ConstructorInfo lookupAttributeCtor;
        private static readonly ConstructorInfo enumAttributeCtor;
        private static readonly ConstructorInfo booleanChoiceAttributeCtor;
        private static readonly ConstructorInfo groupAttributeCtor;

        static FilterEditorAttributeBuilder();
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric);
    }
}

