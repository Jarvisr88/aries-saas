namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.ComponentModel;

    public interface ITypeSpecificsService
    {
        TypeSpecifics GetPropertyTypeSpecifics(PropertyDescriptor property);
        TypeSpecifics GetTypeSpecifics(Type type);
    }
}

