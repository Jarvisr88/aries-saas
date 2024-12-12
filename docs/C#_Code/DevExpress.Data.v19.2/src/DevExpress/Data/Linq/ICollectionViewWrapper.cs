namespace DevExpress.Data.Linq
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public interface ICollectionViewWrapper : ICollectionView, IEnumerable, INotifyCollectionChanged
    {
        ICollectionView WrappedView { get; }
    }
}

