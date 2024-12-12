namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using System.Windows;

    public interface ISerializableItem : ISerializableCollectionItem
    {
        FrameworkElement Element { get; }
    }
}

