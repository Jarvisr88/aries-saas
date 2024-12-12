namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public abstract class CustomTypeDescriptorBase : ICustomTypeDescriptor
    {
        protected CustomTypeDescriptorBase()
        {
        }

        public abstract PropertyDescriptorCollection GetProperties(Attribute[] attributes);
        AttributeCollection ICustomTypeDescriptor.GetAttributes() => 
            new AttributeCollection(null);

        string ICustomTypeDescriptor.GetClassName() => 
            null;

        string ICustomTypeDescriptor.GetComponentName() => 
            null;

        TypeConverter ICustomTypeDescriptor.GetConverter() => 
            null;

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => 
            null;

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => 
            null;

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => 
            null;

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => 
            new EventDescriptorCollection(null);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => 
            new EventDescriptorCollection(null);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => 
            this.GetProperties(null);

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => 
            this;
    }
}

