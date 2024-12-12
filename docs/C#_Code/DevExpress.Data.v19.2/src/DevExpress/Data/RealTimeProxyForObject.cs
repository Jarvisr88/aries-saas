namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RealTimeProxyForObject
    {
        public RealTimeProxyForObject(object source, RealTimePropertyDescriptorCollection pdc);
        public RealTimeProxyForObject(object source, RealTimePropertyDescriptorCollection pdc, PropertyDescriptor pdSource);
        public void Assign(RealTimeProxyForObject source);
        public override bool Equals(object obj);
        private static Dictionary<RealTimePropertyDescriptor, object> FillValue(object source, RealTimePropertyDescriptorCollection pdc, PropertyDescriptor pdSource);
        public RealTimePropertyDescriptor GetChangedPropertyDescriptor();
        public override int GetHashCode();

        public Dictionary<RealTimePropertyDescriptor, object> Content { get; private set; }
    }
}

