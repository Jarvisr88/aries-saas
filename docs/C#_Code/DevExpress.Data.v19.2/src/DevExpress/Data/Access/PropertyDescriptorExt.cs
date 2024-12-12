namespace DevExpress.Data.Access
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public static class PropertyDescriptorExt
    {
        public static T GetAttribute<T>(this PropertyDescriptor prop) where T: Attribute;
        private static AttributeCollection GetAttributes(PropertyDescriptor property);
    }
}

