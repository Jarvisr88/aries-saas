namespace DevExpress.XtraPrinting.Native
{
    using System;

    public sealed class OptionKindPropertyTypeAttribute : TypeProviderAttribute
    {
        public OptionKindPropertyTypeAttribute(Type propertyType);

        public Type PropertyType { get; }
    }
}

