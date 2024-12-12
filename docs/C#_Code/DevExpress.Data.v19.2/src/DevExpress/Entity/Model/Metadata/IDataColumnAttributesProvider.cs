namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.ComponentModel;

    public interface IDataColumnAttributesProvider
    {
        DataColumnAttributes GetAtrributes(PropertyDescriptor property, Type ownerType);
    }
}

