namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public class TypeDescriptorHelper
    {
        public static PropertyDescriptor CreateProperty(Type type, PropertyDescriptor oldPropertyDescriptor, Attribute[] attributes) => 
            (oldPropertyDescriptor != null) ? TypeDescriptor.CreateProperty(type, oldPropertyDescriptor, attributes) : null;
    }
}

