namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.LayoutControl;
    using System;

    public interface ISerializationController : IDisposable
    {
        void OnClearCollection(XtraItemRoutedEventArgs e);
        object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e);
        object OnFindCollectionItem(XtraFindCollectionItemEventArgs e);
        void RestoreLayout(object path);
        void SaveLayout(object path);

        DevExpress.Xpf.LayoutControl.LayoutControl Container { get; }

        SerializableItemCollection Items { get; set; }

        bool IsDeserializing { get; }
    }
}

