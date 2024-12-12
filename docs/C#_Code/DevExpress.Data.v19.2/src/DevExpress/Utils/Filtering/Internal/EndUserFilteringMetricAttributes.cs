namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class EndUserFilteringMetricAttributes : IEndUserFilteringMetricAttributes, IEnumerable<Attribute>, IEnumerable
    {
        internal static readonly Attribute[] Empty;
        private readonly Attribute[] attributes;
        private readonly AnnotationAttributes annotationAttributes;

        static EndUserFilteringMetricAttributes();
        public EndUserFilteringMetricAttributes(string path, System.Type type, Attribute[] attributes = null);
        public EndUserFilteringMetricAttributes(string path, System.Type type, AnnotationAttributes attributes);
        private AnnotationAttributes GetAttributes();
        public static AnnotationAttributes GetAttributes(IEndUserFilteringMetricAttributes attributes);
        private AnnotationAttributes GetAttributes(IEnumerable<Attribute> attributes);
        public IEnumerator<Attribute> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public string Path { get; private set; }

        public System.Type Type { get; private set; }

        public AttributesMergeMode MergeMode { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringMetricAttributes.<>c <>9;
            public static Func<AnnotationAttributes, Attribute[]> <>9__4_0;

            static <>c();
            internal Attribute[] <.ctor>b__4_0(AnnotationAttributes a);
        }
    }
}

