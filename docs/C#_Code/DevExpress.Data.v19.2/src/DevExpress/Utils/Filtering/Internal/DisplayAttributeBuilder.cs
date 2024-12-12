namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class DisplayAttributeBuilder
    {
        private static readonly ConstructorInfo attributeCtor;
        private static readonly PropertyInfo[] attributeProperties;

        static DisplayAttributeBuilder();
        internal static CustomAttributeBuilder Build(string name);
        internal static CustomAttributeBuilder Build(IEndUserFilteringMetric metric, bool calculateShortName = false);
        private static TService GetService<TService>(IServiceProvider serviceProvider) where TService: class;
        private static string GetShortName(IEndUserFilteringMetric metric, bool calculateShortName);

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<TService> where TService: class
        {
            public static readonly DisplayAttributeBuilder.<>c__6<TService> <>9;
            public static Func<IServiceProvider, TService> <>9__6_0;

            static <>c__6();
            internal TService <GetService>b__6_0(IServiceProvider sp);
        }
    }
}

