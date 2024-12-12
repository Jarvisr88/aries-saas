namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class NullDataControlOwner : IDataControlOwner
    {
        public static readonly IDataControlOwner Instance = new NullDataControlOwner();

        private NullDataControlOwner()
        {
        }

        bool IDataControlOwner.CanGroupColumn(ColumnBase column) => 
            (column.OwnerControl != null) ? (column.ActualAllowGroupingCore && column.OwnerControl.DataProviderBase.CanGroupCollectionView()) : false;

        bool IDataControlOwner.CanSortColumn(ColumnBase column) => 
            (column.OwnerControl != null) ? (column.ActualAllowSorting && column.OwnerControl.DataProviderBase.CanSortCollectionView()) : false;

        void IDataControlOwner.EnumerateOwnerDataControls(Action<DataControlBase> action)
        {
        }

        void IDataControlOwner.EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action)
        {
        }

        DataControlBase IDataControlOwner.FindDetailDataControlByRow(object detailRow) => 
            null;

        object IDataControlOwner.GetParentRow(object detailRow) => 
            null;

        void IDataControlOwner.ValidateMasterDetailConsistency()
        {
        }
    }
}

