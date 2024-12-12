namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using System;

    public interface IBandedGridView<TCol, TRow> : IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        IBandedViewAppearance BandedViewAppearance { get; }

        IBandedViewAppearance BandedViewAppearancePrint { get; }

        IBandedGridOptionsView BandedGridOptionsView { get; }

        int BandRowCount { get; }

        int HeaderRowCount { get; }

        BandedAreaRowPattern CustomBandedHeaderRowPattern { get; }
    }
}

