namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class PropertyDescriptorProvider
    {
        private Dictionary<Type, PropertyDescriptorCollection> cache;

        public PropertyDescriptorProvider();
        private static PropertyDescriptorCollection CreateEmptyProperties();
        private static PropertyDescriptorCollection CreateFastProperties(Type type);
        public PropertyDescriptorCollection GetProperties(object obj);
    }
}

