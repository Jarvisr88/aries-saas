namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    internal static class FilterPropertyAttributeBuilder
    {
        private static readonly ConstructorInfo attributeCtor;

        static FilterPropertyAttributeBuilder();
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric);
        internal static CustomAttributeBuilder Build(bool isFilterProperty = true);
    }
}

