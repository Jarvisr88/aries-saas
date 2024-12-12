namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data.Export;
    using System;

    public interface IGridViewBase<out TCol, out TRow, in TColIn, in TRowIn> where TCol: IColumn where TRow: IRowBase where TColIn: IColumn where TRowIn: IRowBase
    {
        bool GetAllowMerge(TColIn col);
        string GetRowCellDisplayText(TRowIn row, TColIn col);
        FormatSettings GetRowCellFormatting(TRowIn row, TColIn col);
        string GetRowCellHyperlink(TRowIn row, TColIn col);
        string GetRowCellHyperlinkDisplayText(TRowIn row, TColIn col);
        ISparklineInfo GetRowCellSparklineInfo(TRowIn row, TColIn col);
        object GetRowCellValue(TRowIn row, TColIn col);
        bool RaiseCustomSummaryExists(ISummaryItemEx item, int groupLevel, int groupRowHandle, bool isGroupSummary);
        object RaiseCustomUnboundColumnData(TColIn col, int listSourceRow, object value);
        int RaiseMergeEvent(int startRow, int rowLogicalPosition, TColIn col);
    }
}

