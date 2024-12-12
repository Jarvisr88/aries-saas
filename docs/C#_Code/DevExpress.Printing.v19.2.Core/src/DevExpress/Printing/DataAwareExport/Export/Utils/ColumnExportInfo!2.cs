namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ColumnExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private ExportInfo<TCol, TRow> exportInfo;
        private XlExportOptionsBase optionsBase;
        private IGridViewAppearance appearanceView;

        public ColumnExportInfo(ExportInfo<TCol, TRow> exportInfo)
        {
            Guard.ArgumentNotNull(exportInfo, "ExportInfo");
            this.exportInfo = exportInfo;
            this.optionsBase = exportInfo.Options as XlExportOptionsBase;
        }

        private DocumentColumnFilteringEventArgs CreateDocFilteringEventArgs(TCol col) => 
            new DocumentColumnFilteringEventArgs { 
                ColumnFieldName = col.FieldName,
                ColumnPosition = col.LogicalPosition
            };

        public virtual void RaiseDocumentColumnFilteringEventArgs(TCol col)
        {
            DocumentColumnFilteringEventArgs ea = this.CreateDocFilteringEventArgs(col);
            this.exportInfo.Options.RaiseDocumentColumnFiltering(ea);
            if ((ea.Filter != null) && string.Equals(ea.ColumnFieldName, col.FieldName))
            {
                XlFilterColumn column = new XlFilterColumn(col.VisibleIndex, ea.Filter);
                if (!this.exportInfo.Sheet.AutoFilterColumns.Contains(column))
                {
                    this.exportInfo.Sheet.AutoFilterColumns.Add(column);
                }
            }
        }

        public IGridViewAppearance AppearanceView
        {
            get
            {
                this.appearanceView ??= this.View.Appearance;
                return this.appearanceView;
            }
        }

        public ExportInfo<TCol, TRow> ExportInfo =>
            this.exportInfo;

        public IDataAwareExportOptions Options =>
            this.ExportInfo.Options;

        public IGridView<TCol, TRow> View =>
            this.ExportInfo.View;

        internal IXlSheet Sheet =>
            this.ExportInfo.Sheet;

        internal CriteriaOperatorToXlExpressionConverter ExpressionConverter { get; set; }

        internal ExportHelpersProvider<TCol, TRow> HelpersProvider =>
            this.ExportInfo.HelpersProvider;

        internal IXlExport Exporter =>
            this.ExportInfo.Exporter;

        internal ExportColumnsCollection<TCol> ColumnsInfoCollection =>
            this.ExportInfo.ColumnsInfoColl;

        internal List<XlGroup> PrecalculatedGroupsList =>
            this.ExportInfo.PrecalculatedGroupsList;

        internal virtual bool AllowHorzLines =>
            this.ExportInfo.AllowHorzLines;

        internal virtual bool AllowVertLines =>
            this.ExportInfo.AllowVertLines;

        public virtual bool ApplyFormattingToEntireColumn =>
            this.ExportInfo.ApplyFormattingToEntireColumn;

        public bool RawDataMode =>
            (this.optionsBase != null) && this.optionsBase.RawDataMode;

        public bool AllowCellMerge =>
            this.Options.AllowCellMerge == DefaultBoolean.True;
    }
}

