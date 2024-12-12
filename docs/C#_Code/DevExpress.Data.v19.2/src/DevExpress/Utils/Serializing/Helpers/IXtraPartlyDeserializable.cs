namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IXtraPartlyDeserializable
    {
        void Deserialize(object rootObject, IXtraPropertyCollection properties);
    }
}

