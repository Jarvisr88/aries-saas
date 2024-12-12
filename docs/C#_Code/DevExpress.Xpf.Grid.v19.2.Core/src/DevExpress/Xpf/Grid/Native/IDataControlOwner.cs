namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public interface IDataControlOwner
    {
        bool CanGroupColumn(ColumnBase column);
        bool CanSortColumn(ColumnBase column);
        void EnumerateOwnerDataControls(Action<DataControlBase> action);
        void EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action);
        DataControlBase FindDetailDataControlByRow(object detailRow);
        object GetParentRow(object detailRow);
        void ValidateMasterDetailConsistency();
    }
}

