namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ResetPropertyEventArgs : PropertyEventArgs
    {
        public ResetPropertyEventArgs(DevExpress.Xpf.Core.Serialization.ResetPropertyMode resetPropertyMode, PropertyDescriptor property, object source) : base(property, source, DXSerializer.ResetPropertyEvent)
        {
            this.ResetPropertyMode = resetPropertyMode;
        }

        public DevExpress.Xpf.Core.Serialization.ResetPropertyMode ResetPropertyMode { get; set; }
    }
}

