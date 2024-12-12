namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.InteropServices;

    public interface IEndUserFilteringMetricAttributesFactory
    {
        IEndUserFilteringMetricAttributes Create(string path, Type type, Attribute[] attributes = null);
        IEndUserFilteringMetricAttributes Create(string path, Type type, AnnotationAttributes attributes);
    }
}

