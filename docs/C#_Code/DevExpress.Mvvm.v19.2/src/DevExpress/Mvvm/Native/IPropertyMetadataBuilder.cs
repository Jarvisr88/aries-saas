namespace DevExpress.Mvvm.Native
{
    using System.Collections.Generic;

    internal interface IPropertyMetadataBuilder
    {
        IEnumerable<Attribute> Attributes { get; }
    }
}

