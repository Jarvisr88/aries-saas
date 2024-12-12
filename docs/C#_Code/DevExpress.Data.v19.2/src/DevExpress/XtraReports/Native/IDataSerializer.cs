namespace DevExpress.XtraReports.Native
{
    using System;

    public interface IDataSerializer
    {
        bool CanDeserialize(string value, string typeName, object extensionProvider);
        bool CanSerialize(object data, object extensionProvider);
        object Deserialize(string value, string typeName, object extensionProvider);
        string Serialize(object data, object extensionProvider);
    }
}

