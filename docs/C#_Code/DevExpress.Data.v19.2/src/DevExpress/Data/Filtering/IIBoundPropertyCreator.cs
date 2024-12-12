namespace DevExpress.Data.Filtering
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public interface IIBoundPropertyCreator
    {
        IBoundProperty CreateProperty(object dataSource, string dataMember, string displayName, bool isList, PropertyDescriptor property);
        void SetParent(IBoundProperty property, IBoundProperty parent);
    }
}

