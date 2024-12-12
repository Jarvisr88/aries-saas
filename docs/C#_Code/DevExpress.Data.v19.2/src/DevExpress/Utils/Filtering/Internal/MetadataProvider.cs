namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class MetadataProvider : IMetadataProvider
    {
        private IMetadataStorage metadataStorage;

        public MetadataProvider(IMetadataStorage metadataStorage);
        private IMetricAttributes CreateMetricAttributes(string name, Type propertyType, Type enumDataType);
        AnnotationAttributes IMetadataProvider.GetAnnotationAttributes(string name);
        FilterAttributes IMetadataProvider.GetFilterAttributes(string name);
        public bool GetApplyFormatInEditMode(string name);
        public IMetricAttributes GetAttributes(string name);
        public Type GetAttributesTypeDefinition(string name);
        public string GetCaption(string name);
        private int GetColumnIndexFromMetadata(string name);
        public string GetDataFormatString(string name);
        public DataType? GetDataType(string name);
        public string GetDescription(string name);
        public Type GetEnumDataType(string name);
        public bool GetIsVisible(string name);
        public string GetLayout(string name);
        private Type GetMetricAttributesTypeDefinition(string name, Type propertyType, Type enumDataType);
        private static string GetName(string path);
        public string GetNullDisplayText(string name);
        public int GetOrder(string name);
        public string GetShortName(string name);
        public Type GetType(string name);
        private T GetValueFromAnnotationAttributes<T>(string name, Func<AnnotationAttributes, T> getValue);
        private T GetValueFromFilterAttributes<T>(string name, Func<FilterAttributes, T> getValue);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MetadataProvider.<>c <>9;
            public static Func<AnnotationAttributes, AnnotationAttributes> <>9__2_0;
            public static Func<FilterAttributes, FilterAttributes> <>9__3_0;
            public static Func<FilterAttributes, Type> <>9__14_0;
            public static Func<FilterAttributes, Type> <>9__15_0;
            public static Func<AnnotationAttributes, int> <>9__20_0;

            static <>c();
            internal AnnotationAttributes <DevExpress.Utils.Filtering.Internal.IMetadataProvider.GetAnnotationAttributes>b__2_0(AnnotationAttributes a);
            internal FilterAttributes <DevExpress.Utils.Filtering.Internal.IMetadataProvider.GetFilterAttributes>b__3_0(FilterAttributes a);
            internal int <GetColumnIndexFromMetadata>b__20_0(AnnotationAttributes a);
            internal Type <GetEnumDataType>b__15_0(FilterAttributes a);
            internal Type <GetType>b__14_0(FilterAttributes a);
        }
    }
}

