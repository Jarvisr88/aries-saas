namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IGridView<TCol, TRow> : IGridViewBase<TCol, TRow, TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        IEnumerable<TCol> GetAllColumns();
        IEnumerable<TRow> GetAllRows();
        IEnumerable<TCol> GetGroupedColumns();
        IList<HypertextExportInfoContainer> GetHypertextCellInfo(TRow row, TCol col);
        IList<XlRichTextRun> GetRichTextRuns(TRow row, TCol col);

        int RowHeight { get; }

        int FixedRowsCount { get; }

        long RowCount { get; }

        bool IsCancelPending { get; }

        string ViewCaption { get; }

        string FilterString { get; }

        IAdditionalSheetInfo AdditionalSheetInfo { get; }

        IEnumerable<ISummaryItemEx> GridGroupSummaryItemCollection { get; }

        IEnumerable<ISummaryItemEx> GridTotalSummaryItemCollection { get; }

        IEnumerable<FormatConditionObject> FormatConditionsCollection { get; }

        IEnumerable<IFormatRuleBase> FormatRules { get; }

        IEnumerable<ISummaryItemEx> GroupHeaderSummaryItems { get; }

        IEnumerable<ISummaryItemEx> FixedSummary { get; }

        IGridViewAppearance Appearance { get; }

        IGridViewAppearancePrint AppearancePrint { get; }

        IGridOptionsBehavior OptionsBehavior { get; }

        IGridOptionsView OptionsView { get; }

        bool ConcatenateAlignedByColumnsSummaryItems { get; }
    }
}

