namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class DataAnnotationAttributeBuilderHelper
    {
        private static object[] constructorArgs;

        static DataAnnotationAttributeBuilderHelper();
        internal static CustomAttributeBuilder Build(ConstructorInfo attributeCtor, object[] values);
        internal static CustomAttributeBuilder Build(ConstructorInfo attributeCtor, PropertyInfo[] attributeProperties, object[] values);
        private static void BuildAttributeArgs(PropertyInfo[] attributeProperties, object[] values, out PropertyInfo[] properties, out object[] attributeValues);
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        internal static void CheckDataAnnotations_ConditionallyAPTCAIssue<TAttrbute>();
    }
}

