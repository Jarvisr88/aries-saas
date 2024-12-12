namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class RealTimePropertyDescriptorCollection : PropertyDescriptorCollection
    {
        private static readonly object syncObject;
        private readonly Dictionary<PropertyDescriptor, RealTimePropertyDescriptor> descriptorDict;
        private readonly PropertyDescriptorCollection sourcePDC;

        static RealTimePropertyDescriptorCollection();
        private RealTimePropertyDescriptorCollection(IList list, string displayableProperties);
        private RealTimePropertyDescriptorCollection(PropertyDescriptorCollection properties, string displayableProperties);
        private RealTimePropertyDescriptor AddProperty(PropertyDescriptor property, int index, bool visible);
        public static RealTimePropertyDescriptorCollection CreatePropertyDescriptorCollection(PropertyDescriptorCollection properties);
        public static RealTimePropertyDescriptorCollection CreatePropertyDescriptorCollection(IList list, string displayableProperties);
        public override PropertyDescriptor Find(string name, bool ignoreCase);
        public override IEnumerator GetEnumerator();
        public PropertyDescriptor GetPropertyDescriptor(RealTimePropertyDescriptor rpd);
        public PropertyDescriptor GetPropertyDescriptorByName(string name);
        private static PropertyDescriptorCollection GetSourcePropertyDescriptorCollection(IList source);
        public Dictionary<RealTimePropertyDescriptor, object> GetSourceValue(object component);
        public bool TryGetRealtimePropertyDescriptor(PropertyDescriptor pd, out RealTimePropertyDescriptor rpd);

        public PropertyDescriptorCollection SourcePropertyDescriptorCollection { get; }

        public RealTimePropertyDescriptor this[PropertyDescriptor pd] { get; }

        public override PropertyDescriptor this[int index] { get; }

        public override PropertyDescriptor this[string name] { get; }
    }
}

