namespace DevExpress.Utils.Design.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class FilteringModelMetadataAttribute : Attribute
    {
        public string Platform { get; set; }

        public string ModelTypeProperty { get; set; }

        public string CustomAttributesProperty { get; set; }
    }
}

