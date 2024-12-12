namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IXtraRootSerializationObject
    {
        void AfterSerialize();
        SerializationInfo GetIndexByObject(string propertyName, object obj);
        object GetObjectByIndex(string propertyName, int index);
    }
}

