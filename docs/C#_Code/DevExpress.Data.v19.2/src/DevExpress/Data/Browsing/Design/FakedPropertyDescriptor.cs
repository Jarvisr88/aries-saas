namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class FakedPropertyDescriptor : IPropertyDescriptor
    {
        private bool isComplex;
        private string displayName;
        private TypeSpecifics specific;
        private PropertyDescriptor propertyDescriptor;

        public FakedPropertyDescriptor(PropertyDescriptor propertyDescriptor, TypeSpecifics kind);
        public FakedPropertyDescriptor(PropertyDescriptor propertyDescriptor, string displayName, TypeSpecifics kind);
        public FakedPropertyDescriptor(PropertyDescriptor propertyDescriptor, string displayName, bool isComplex, TypeSpecifics specific);
        public static FakedPropertyDescriptor[] ToFakedProperties(IList properties, ITypeSpecificsService serv);

        public PropertyDescriptor RealProperty { get; }

        public TypeSpecifics Specifics { get; }

        public bool IsComplex { get; }

        public bool IsListType { get; }

        public string Name { get; }

        public string DisplayName { get; }
    }
}

