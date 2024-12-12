namespace DevExpress.Data.ODataLinq.Helpers
{
    using System;
    using System.Collections.Generic;

    public class ElementDescriptorCache
    {
        private Dictionary<Type, ElementDescriptor> descriptorDict;

        public ElementDescriptorCache();
        public ElementDescriptor GetDescriptor(Type type);
    }
}

