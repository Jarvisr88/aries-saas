namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Serialization;
    using System;

    public interface ISerializationController : IDisposable
    {
        T CreateCommand<T>(object path) where T: SerializationControllerCommand, new();
        void OnClearCollection(XtraItemRoutedEventArgs e);
        object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e);
        object OnFindCollectionItem(XtraFindCollectionItemEventArgs e);
        void RestoreLayout(object path);
        void SaveLayout(object path);

        DockLayoutManager Container { get; }

        SerializableItemCollection Items { get; set; }

        bool IsDeserializing { get; }
    }
}

