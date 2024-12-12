namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IXtraCollectionDeserializationOptionsProvider
    {
        bool RemoveOldItems { get; }

        bool AddNewItems { get; }
    }
}

