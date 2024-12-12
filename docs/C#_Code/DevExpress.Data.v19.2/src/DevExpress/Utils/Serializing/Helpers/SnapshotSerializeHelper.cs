namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class SnapshotSerializeHelper : SerializeHelper
    {
        protected internal override bool CheckNeedSerialize(object obj, PropertyDescriptor prop, XtraSerializableProperty attr, XtraSerializationFlags parentFlags) => 
            (attr != null) && attr.Serialize;
    }
}

