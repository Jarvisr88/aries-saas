namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Printing.ExportHelpers.Helpers;
    using System;
    using System.Collections.Generic;

    internal class ExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected List<ExportHelperBase<TCol, TRow>> helpers;
        protected ExportInfo<TCol, TRow> exportInfo;
        private ConditionalFormattingExporter<TCol, TRow> conditionalFormattingExporter;
        private ExportCellMerger<TCol, TRow> cellMerger;
        private SummaryExportHelper<TCol, TRow> summaryExporter;
        private LookUpValuesExporter<TCol, TRow> lookupExpoter;
        private SparklineExportHelper<TCol, TRow> sparklineExporter;
        private DevExpress.Printing.ExportHelpers.HyperlinkExporter hyperlinkExporter;

        public ExportHelpersProvider(ExportInfo<TCol, TRow> exportInfo)
        {
            this.exportInfo = exportInfo;
            this.FillHelpers();
        }

        protected virtual ConditionalFormattingExporter<TCol, TRow> CreateConditionalFormattingExporter() => 
            new ConditionalFormattingExporter<TCol, TRow>(this.exportInfo);

        protected virtual ExportCellMerger<TCol, TRow> CreateExportCellMerger() => 
            new ExportCellMerger<TCol, TRow>(this.exportInfo);

        protected virtual DevExpress.Printing.ExportHelpers.HyperlinkExporter CreateHyperlinkExporter() => 
            new DevExpress.Printing.ExportHelpers.HyperlinkExporter();

        protected virtual LookUpValuesExporter<TCol, TRow> CreateLookUpValuesExporter() => 
            new LookUpValuesExporter<TCol, TRow>(this.exportInfo);

        protected virtual SparklineExportHelper<TCol, TRow> CreateSparklineHelper() => 
            new SparklineExportHelper<TCol, TRow>(this.exportInfo);

        protected virtual SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            new SummaryExportHelper<TCol, TRow>(this.exportInfo);

        protected virtual void FillHelpers()
        {
            if (this.exportInfo != null)
            {
                this.helpers = new List<ExportHelperBase<TCol, TRow>>();
                if (this.exportInfo.AllowConditionalFormatting)
                {
                    this.helpers.Add(this.ConditionalFormattingExporter);
                }
                if (this.exportInfo.AllowSparklines)
                {
                    this.helpers.Add(this.SparklineExporter);
                }
                if (this.exportInfo.AllowLookupValues)
                {
                    this.helpers.Add(this.LookupExporter);
                }
            }
        }

        public void Run()
        {
            for (int i = 0; i < this.helpers.Count; i++)
            {
                this.helpers[i].Execute();
            }
        }

        public ConditionalFormattingExporter<TCol, TRow> ConditionalFormattingExporter
        {
            get
            {
                this.conditionalFormattingExporter ??= this.CreateConditionalFormattingExporter();
                return this.conditionalFormattingExporter;
            }
        }

        public ExportCellMerger<TCol, TRow> CellMerger
        {
            get
            {
                this.cellMerger ??= this.CreateExportCellMerger();
                return this.cellMerger;
            }
        }

        public SummaryExportHelper<TCol, TRow> SummaryExporter
        {
            get
            {
                this.summaryExporter ??= this.CreateSummaryExportHelper();
                return this.summaryExporter;
            }
        }

        public LookUpValuesExporter<TCol, TRow> LookupExporter
        {
            get
            {
                this.lookupExpoter ??= this.CreateLookUpValuesExporter();
                return this.lookupExpoter;
            }
        }

        public SparklineExportHelper<TCol, TRow> SparklineExporter
        {
            get
            {
                this.sparklineExporter ??= this.CreateSparklineHelper();
                return this.sparklineExporter;
            }
        }

        public DevExpress.Printing.ExportHelpers.HyperlinkExporter HyperlinkExporter
        {
            get
            {
                this.hyperlinkExporter ??= this.CreateHyperlinkExporter();
                return this.hyperlinkExporter;
            }
        }
    }
}

