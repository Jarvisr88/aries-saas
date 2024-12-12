namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class RequiredAttributeBuilder
    {
        private static readonly ConstructorInfo attributeCtor;

        static RequiredAttributeBuilder();
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric);
    }
}

