namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public class BandCollection<TBand> : BandCollectionBase<TBand>, IBandsCollection, IList, ICollection, IEnumerable, INotifyCollectionChanged, ILockable, ISupportGetCachedIndex<BandBase> where TBand: BandBase
    {
        public BandCollection(ICollectionOwner owner) : base(owner)
        {
        }

        int ISupportGetCachedIndex<BandBase>.GetCachedIndex(BandBase band) => 
            base.GetCachedIndex((TBand) band);
    }
}

