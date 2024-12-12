namespace DevExpress.Export
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public interface IDataAwareExportOptions
    {
        event AfterAddRowEventHandler AfterAddRow;

        event DevExpress.Export.BeforeExportTable BeforeExportTable;

        event CustomizeCellEventHandler CustomizeCell;

        event CustomizeDocumentColumnEventHandler CustomizeDocumentColumn;

        event CustomizeSheetFooterEventHandler CustomizeSheetFooter;

        event CustomizeSheetHeaderEventHandler CustomizeSheetHeader;

        event CustomizeSheetSettingsEventHandler CustomizeSheetSettings;

        event DocumentColumnFilteringEventHandler DocumentColumnFiltering;

        event ExportProgressCallback ExportProgress;

        event SkipFooterRowEventHandler SkipFooterRow;

        void InitDefaults();
        void RaiseAfterAddRowEvent(AfterAddRowEventArgs ea);
        void RaiseBeforeExportTable(BeforeExportTableEventArgs ea);
        void RaiseCustomizeCellEvent(CustomizeCellEventArgs ea);
        void RaiseCustomizeDocumentColumn(CustomizeDocumentColumnEventArgs ea);
        void RaiseCustomizeSheetFooterEvent(ContextEventArgs ea);
        void RaiseCustomizeSheetHeaderEvent(ContextEventArgs ea);
        void RaiseCustomizeSheetSettingsEvent(CustomizeSheetEventArgs ea);
        void RaiseDocumentColumnFiltering(DocumentColumnFilteringEventArgs ea);
        void RaiseSkipFooterRowEvent(SkipFooterRowEventArgs ea);
        void ReportProgress(ProgressChangedEventArgs ea);

        DevExpress.Export.ExportType ExportType { get; set; }

        DefaultBoolean AllowConditionalFormatting { get; set; }

        DefaultBoolean AutoCalcConditionalFormattingIconSetMinValue { get; set; }

        DefaultBoolean AllowSortingAndFiltering { get; set; }

        DefaultBoolean ShowTotalSummaries { get; set; }

        DefaultBoolean AllowBandHeaderCellMerge { get; set; }

        DefaultBoolean AllowCellMerge { get; set; }

        DefaultBoolean AllowLookupValues { get; set; }

        DefaultBoolean AllowGrouping { get; set; }

        DefaultBoolean AllowSparklines { get; set; }

        DefaultBoolean ApplyFormattingToEntireColumn { get; set; }

        DevExpress.Export.GroupState GroupState { get; set; }

        DefaultBoolean AllowFixedColumnHeaderPanel { get; set; }

        DefaultBoolean ShowGroupSummaries { get; set; }

        DefaultBoolean AllowFixedColumns { get; set; }

        DefaultBoolean ShowPageTitle { get; set; }

        DefaultBoolean ShowColumnHeaders { get; set; }

        DefaultBoolean ShowBandHeaders { get; set; }

        DefaultBoolean AllowHyperLinks { get; set; }

        DefaultBoolean RightToLeftDocument { get; set; }

        bool SummaryCountBlankCells { get; set; }

        DefaultBoolean AllowGroupingRows { get; set; }

        DefaultBoolean AllowHorzLines { get; set; }

        DefaultBoolean AllowVertLines { get; set; }

        DevExpress.XtraPrinting.ExportTarget ExportTarget { get; set; }

        XlIgnoreErrors IgnoreErrors { get; set; }

        DevExpress.Export.LayoutMode LayoutMode { get; set; }

        DevExpress.Export.BandedLayoutMode BandedLayoutMode { get; set; }

        DevExpress.Export.UnboundExpressionExportMode UnboundExpressionExportMode { get; set; }

        DefaultBoolean AllowCombinedBandAndColumnHeaderCellMerge { get; set; }

        DevExpress.XtraPrinting.TextExportMode TextExportMode { get; set; }

        string SheetName { get; set; }

        Encoding CSVEncoding { get; set; }

        bool WritePreamble { get; set; }

        string CSVSeparator { get; set; }

        bool CalcTotalSummaryOnCompositeRange { get; set; }

        bool ShowDataValidationErrorMessage { get; set; }

        bool SuppressEmptyStrings { get; set; }

        bool FitToPrintedPageWidth { get; set; }

        bool FitToPrintedPageHeight { get; set; }

        CultureInfo DocumentCulture { get; set; }

        bool CanRaiseDocumentColumnFilteringEvent { get; }

        bool CanRaiseSkipFooterRowEvent { get; }

        bool CanRaiseAfterAddRow { get; }

        bool CanRaiseCustomizeCellEvent { get; }

        bool CanRaiseCustomizeSheetSettingsEvent { get; }

        bool CanRaiseCustomizeHeaderEvent { get; }

        bool CanRaiseCustomizeFooterEvent { get; }

        bool CanRaiseBeforeExportTable { get; }

        bool CanRaiseCustomizeDocumentColumnEvent { get; }

        bool SuppressHyperlinkMaxCountWarning { get; set; }
    }
}

