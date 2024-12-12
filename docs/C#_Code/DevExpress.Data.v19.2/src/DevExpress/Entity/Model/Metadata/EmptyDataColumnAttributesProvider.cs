namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.ComponentModel;

    public class EmptyDataColumnAttributesProvider : IDataColumnAttributesProvider
    {
        public DataColumnAttributes GetAtrributes(PropertyDescriptor property, Type ownerType) => 
            new DataColumnAttributes(new AttributeCollection(new Attribute[0]), () => property.Converter);
    }
}

