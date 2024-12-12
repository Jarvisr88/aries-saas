namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;

    public class DXSerializable
    {
        protected internal virtual XtraSerializableProperty CreateXtraSerializableAttrubute() => 
            new XtraSerializableProperty();
    }
}

