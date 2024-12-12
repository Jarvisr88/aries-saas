namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System.Collections;
    using System.Collections.Specialized;

    public interface IBandsCollection : IList, ICollection, IEnumerable, INotifyCollectionChanged, ILockable, ISupportGetCachedIndex<BandBase>
    {
    }
}

