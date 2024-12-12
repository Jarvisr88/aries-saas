namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.ComponentModel;

    public class TypeSpecificsService : ITypeSpecificsService
    {
        public virtual TypeSpecifics GetPropertyTypeSpecifics(PropertyDescriptor property);
        public virtual TypeSpecifics GetTypeSpecifics(Type type);
        protected static Type ValidateType(Type value);
    }
}

