namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface IBandsOwner
    {
        IBandsOwner FindClone(DataControlBase dataControl);
        void OnBandsChanged(NotifyCollectionChangedEventArgs e);
        void OnColumnsChanged(NotifyCollectionChangedEventArgs e);
        void OnLayoutPropertyChanged();

        IBandsCollection BandsCore { get; }

        List<BandBase> VisibleBands { get; }

        DataControlBase DataControl { get; }
    }
}

