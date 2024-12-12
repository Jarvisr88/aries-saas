namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class CustomShouldSerializePropertyEventArgs : PropertyEventArgs
    {
        public CustomShouldSerializePropertyEventArgs(PropertyDescriptor property, object source) : base(property, source, DXSerializer.CustomShouldSerializePropertyEvent)
        {
            this.CustomShouldSerialize = null;
        }

        public bool? CustomShouldSerialize { get; set; }
    }
}

