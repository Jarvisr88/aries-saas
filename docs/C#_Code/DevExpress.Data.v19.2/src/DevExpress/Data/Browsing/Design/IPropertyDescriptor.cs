namespace DevExpress.Data.Browsing.Design
{
    using System;

    public interface IPropertyDescriptor
    {
        TypeSpecifics Specifics { get; }

        bool IsComplex { get; }

        bool IsListType { get; }

        string Name { get; }

        string DisplayName { get; }
    }
}

