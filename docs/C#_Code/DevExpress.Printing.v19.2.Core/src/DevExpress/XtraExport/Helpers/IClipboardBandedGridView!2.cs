namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IClipboardBandedGridView<TCol, TRow> : IClipboardGridView<TCol, TRow>, IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        ClipboardBandLayoutInfo GetBandsInfo();
        bool IsAdvBandedView();
        bool RaiseClipboardAdvBandRowCopying(IEnumerable<IEnumerable<ClipboardBandCellInfo>> panelInfo);
        bool RaiseClipboardAdvDataRowCopying(int rowHandle, IEnumerable<IEnumerable<ClipboardBandCellInfo>> panelInfo);
        bool RaiseClipboardAdvHeaderRowCopying(IEnumerable<IEnumerable<ClipboardBandCellInfo>> panelInfo);
        bool RaiseClipboardBandRowCopying(IEnumerable<ClipboardBandCellInfo> rowInfo);
    }
}

