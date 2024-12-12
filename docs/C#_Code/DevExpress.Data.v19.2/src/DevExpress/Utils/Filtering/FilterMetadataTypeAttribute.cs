namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public sealed class FilterMetadataTypeAttribute : Attribute
    {
        public FilterMetadataTypeAttribute(Type metadataClassType)
        {
            this.MetadataClassType = metadataClassType;
        }

        public Type MetadataClassType { get; private set; }
    }
}

