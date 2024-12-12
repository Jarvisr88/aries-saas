namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using System;

    public interface IAdvancedBandedGridView<TCol, TRow> : IBandedGridView<TCol, TRow>, IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        IAdvBandedOptionsView AdvBandedOptionsView { get; }

        int ColumnPanelRowsCount { get; }

        BandedAreaRowPattern CustomBandedRowPattern { get; }

        BandedAreaRowPattern CustomBandedFooterRowPattern { get; }

        BandedAreaRowPattern CustomBandedGroupFooterRowPattern { get; }
    }
}

