namespace DevExpress.Xpf.Grid
{
    using System;

    public interface IGridDataRow
    {
        void UpdateContentLayout();

        DevExpress.Xpf.Grid.RowData RowData { get; }
    }
}

