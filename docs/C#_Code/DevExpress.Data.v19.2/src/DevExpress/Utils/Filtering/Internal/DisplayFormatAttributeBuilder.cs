namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class DisplayFormatAttributeBuilder
    {
        private static readonly ConstructorInfo attributeCtor;
        private static readonly PropertyInfo[] attributeProperties;

        static DisplayFormatAttributeBuilder();
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric);
    }
}

