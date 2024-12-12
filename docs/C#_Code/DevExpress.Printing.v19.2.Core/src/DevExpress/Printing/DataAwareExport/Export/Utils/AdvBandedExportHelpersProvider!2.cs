namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class AdvBandedExportHelpersProvider<TCol, TRow> : ExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public AdvBandedExportHelpersProvider(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override ConditionalFormattingExporter<TCol, TRow> CreateConditionalFormattingExporter() => 
            !base.exportInfo.LinearBandsAndColumns ? new AdvBandedConditionalFormattingExporter<TCol, TRow>(base.exportInfo) : new ConditionalFormattingExporter<TCol, TRow>(base.exportInfo);

        protected override LookUpValuesExporter<TCol, TRow> CreateLookUpValuesExporter() => 
            !base.exportInfo.LinearBandsAndColumns ? new AdvBandedLookUpValuesExporter<TCol, TRow>(base.exportInfo) : new LookUpValuesExporter<TCol, TRow>(base.exportInfo);

        protected override SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            !base.exportInfo.LinearBandsAndColumns ? new AdvBandedSummaryExportHelper<TCol, TRow>(base.exportInfo) : new SummaryExportHelper<TCol, TRow>(base.exportInfo);
    }
}

