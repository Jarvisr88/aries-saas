namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IClipboardExporter<TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        void AddBandedHeader(ClipboardBandLayoutInfo info);
        void AddGroupHeader(ClipboardCellInfo headerInfo, int columnCount);
        void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo);
        void AddRow(IEnumerable<ClipboardCellInfo> rowInfo);
        void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo);
        void BeginExport();
        void EndExport();
        void SetDataObject(DataObject data);
    }
}

