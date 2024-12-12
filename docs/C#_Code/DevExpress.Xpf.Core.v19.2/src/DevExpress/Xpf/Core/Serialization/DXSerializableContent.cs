namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;

    public class DXSerializableContent : DXSerializable
    {
        protected internal override XtraSerializableProperty CreateXtraSerializableAttrubute() => 
            new XtraSerializableProperty(XtraSerializationVisibility.Content);
    }
}

