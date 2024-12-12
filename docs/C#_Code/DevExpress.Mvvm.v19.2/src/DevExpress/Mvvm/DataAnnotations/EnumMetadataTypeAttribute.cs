namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Enum, AllowMultiple=false)]
    public sealed class EnumMetadataTypeAttribute : Attribute
    {
        public EnumMetadataTypeAttribute(Type metadataClassType)
        {
            this.MetadataClassType = metadataClassType;
        }

        public Type MetadataClassType { get; private set; }
    }
}

