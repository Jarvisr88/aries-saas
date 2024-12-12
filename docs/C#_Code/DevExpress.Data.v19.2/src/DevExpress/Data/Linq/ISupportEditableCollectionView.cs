namespace DevExpress.Data.Linq
{
    using System;
    using System.ComponentModel;

    public interface ISupportEditableCollectionView : IEditableCollectionView
    {
        bool IsSupportEditableCollectionView { get; }
    }
}

