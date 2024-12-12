namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public class BandColumnCollection<TColumn> : BandCollectionBase<TColumn>, IBandColumnsCollection, IList, ICollection, IEnumerable, INotifyCollectionChanged, ILockable, ISupportGetCachedIndex<ColumnBase> where TColumn: ColumnBase
    {
        public BandColumnCollection(ICollectionOwner owner) : base(owner)
        {
        }

        int ISupportGetCachedIndex<ColumnBase>.GetCachedIndex(ColumnBase column) => 
            base.GetCachedIndex((TColumn) column);
    }
}

