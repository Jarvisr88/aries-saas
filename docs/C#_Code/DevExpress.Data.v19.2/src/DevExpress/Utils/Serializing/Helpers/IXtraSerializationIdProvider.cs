namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface IXtraSerializationIdProvider
    {
        object GetSerializationId(XtraSerializableProperty property, object item);
    }
}

