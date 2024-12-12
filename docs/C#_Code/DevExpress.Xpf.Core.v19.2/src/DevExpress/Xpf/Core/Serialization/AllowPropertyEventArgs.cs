namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class AllowPropertyEventArgs : PropertyEventArgs
    {
        public AllowPropertyEventArgs(int id, bool isSerializing, PropertyDescriptor property, object source) : base(property, source, DXSerializer.AllowPropertyEvent)
        {
            this.Allow = true;
            this.PropertyId = id;
            this.IsSerializing = isSerializing;
        }

        public int PropertyId { get; private set; }

        public bool IsSerializing { get; private set; }

        public bool Allow { get; set; }
    }
}

