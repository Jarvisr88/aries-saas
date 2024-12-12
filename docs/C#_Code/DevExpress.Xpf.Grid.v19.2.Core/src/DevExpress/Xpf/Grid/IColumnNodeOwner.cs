namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Specialized;

    internal interface IColumnNodeOwner
    {
        BandsMoverHierarchy GetRoot();
        void OnNodeChanging(NodeChangingInfo change);
        void OnNodeCollectionChanged(ColumnNodeCollection source, NotifyCollectionChangedEventArgs e);

        ColumnNodeCollection BandNodes { get; }

        ColumnNodeCollection ColumnNodes { get; }

        int CollectionIndex { get; }

        IColumnNodeOwner Owner { get; }

        bool IsInitializing { get; }
    }
}

