namespace DevExpress.XtraPrinting
{
    using DevExpress.Export;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class CsvExportOptionsEx : CsvExportOptions, IDataAwareExportOptions
    {
        private DevExpress.Export.ExportType exportTypeCore;

        public event CustomizeCellEventHandler CustomizeCell;

        event AfterAddRowEventHandler IDataAwareExportOptions.AfterAddRow
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event BeforeExportTable IDataAwareExportOptions.BeforeExportTable
        {
            add
            {
            }
            remove
            {
            }
        }

        event CustomizeDocumentColumnEventHandler IDataAwareExportOptions.CustomizeDocumentColumn
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event CustomizeSheetFooterEventHandler IDataAwareExportOptions.CustomizeSheetFooter
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event CustomizeSheetHeaderEventHandler IDataAwareExportOptions.CustomizeSheetHeader
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event CustomizeSheetSettingsEventHandler IDataAwareExportOptions.CustomizeSheetSettings
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event DocumentColumnFilteringEventHandler IDataAwareExportOptions.DocumentColumnFiltering
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event SkipFooterRowEventHandler IDataAwareExportOptions.SkipFooterRow
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event ExportProgressCallback ExportProgress;

        public CsvExportOptionsEx()
        {
            this.Init();
        }

        public CsvExportOptionsEx(string separator, Encoding encoding, TextExportMode textExportMode, bool skipEmptyRows, bool skipEmptyColumns) : base(separator, encoding, textExportMode, skipEmptyRows, skipEmptyColumns)
        {
            this.Init();
        }

        void IDataAwareExportOptions.InitDefaults()
        {
            ((IDataAwareExportOptions) this).ShowColumnHeaders = DataAwareExportOptionsFactory.UpdateDefaultBoolean(((IDataAwareExportOptions) this).ShowColumnHeaders, true);
        }

        void IDataAwareExportOptions.RaiseAfterAddRowEvent(AfterAddRowEventArgs e)
        {
        }

        void IDataAwareExportOptions.RaiseBeforeExportTable(BeforeExportTableEventArgs ea)
        {
        }

        void IDataAwareExportOptions.RaiseCustomizeCellEvent(CustomizeCellEventArgs e)
        {
            if (this.CustomizeCell != null)
            {
                this.CustomizeCell(e);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeDocumentColumn(CustomizeDocumentColumnEventArgs ea)
        {
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetFooterEvent(ContextEventArgs e)
        {
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetHeaderEvent(ContextEventArgs e)
        {
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetSettingsEvent(CustomizeSheetEventArgs e)
        {
        }

        void IDataAwareExportOptions.RaiseDocumentColumnFiltering(DocumentColumnFilteringEventArgs ea)
        {
        }

        void IDataAwareExportOptions.RaiseSkipFooterRowEvent(SkipFooterRowEventArgs ea)
        {
        }

        void IDataAwareExportOptions.ReportProgress(ProgressChangedEventArgs e)
        {
            if (this.ExportProgress != null)
            {
                this.ExportProgress(e);
            }
        }

        private void Init()
        {
            ((IDataAwareExportOptions) this).ShowColumnHeaders = DefaultBoolean.Default;
            ((IDataAwareExportOptions) this).ShowBandHeaders = DefaultBoolean.Default;
            this.DocumentCulture = CultureInfo.CurrentCulture;
        }

        public string Separator
        {
            get => 
                this.GetActualSeparator();
            set => 
                base.Separator = value;
        }

        DefaultBoolean IDataAwareExportOptions.AllowConditionalFormatting
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AutoCalcConditionalFormattingIconSetMinValue
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowSortingAndFiltering
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.ShowTotalSummaries
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowCellMerge
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowLookupValues
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowGrouping
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowSparklines
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        GroupState IDataAwareExportOptions.GroupState
        {
            get => 
                GroupState.Default;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowFixedColumnHeaderPanel
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.ShowGroupSummaries
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowFixedColumns
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.ShowPageTitle
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.ShowColumnHeaders { get; set; }

        DefaultBoolean IDataAwareExportOptions.ShowBandHeaders { get; set; }

        DefaultBoolean IDataAwareExportOptions.AllowHorzLines
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowVertLines
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowHyperLinks
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.RightToLeftDocument
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        bool IDataAwareExportOptions.SummaryCountBlankCells
        {
            get => 
                false;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.ApplyFormattingToEntireColumn
        {
            get => 
                DefaultBoolean.Default;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowGroupingRows
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        DefaultBoolean IDataAwareExportOptions.AllowCombinedBandAndColumnHeaderCellMerge
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }

        bool IDataAwareExportOptions.CalcTotalSummaryOnCompositeRange
        {
            get => 
                false;
            set
            {
            }
        }

        bool IDataAwareExportOptions.ShowDataValidationErrorMessage
        {
            get => 
                true;
            set
            {
            }
        }

        public CultureInfo DocumentCulture { get; set; }

        LayoutMode IDataAwareExportOptions.LayoutMode
        {
            get => 
                LayoutMode.Standard;
            set
            {
            }
        }

        BandedLayoutMode IDataAwareExportOptions.BandedLayoutMode
        {
            get => 
                BandedLayoutMode.Default;
            set
            {
            }
        }

        UnboundExpressionExportMode IDataAwareExportOptions.UnboundExpressionExportMode
        {
            get => 
                UnboundExpressionExportMode.AsValue;
            set
            {
            }
        }

        string IDataAwareExportOptions.CSVSeparator
        {
            get => 
                this.Separator;
            set => 
                this.Separator = value;
        }

        Encoding IDataAwareExportOptions.CSVEncoding
        {
            get => 
                base.Encoding;
            set => 
                base.Encoding = value;
        }

        public bool WritePreamble { get; set; }

        public bool SuppressEmptyStrings { get; set; }

        bool IDataAwareExportOptions.FitToPrintedPageHeight
        {
            get => 
                false;
            set
            {
            }
        }

        bool IDataAwareExportOptions.FitToPrintedPageWidth
        {
            get => 
                false;
            set
            {
            }
        }

        ExportTarget IDataAwareExportOptions.ExportTarget
        {
            get => 
                ExportTarget.Csv;
            set
            {
            }
        }

        XlIgnoreErrors IDataAwareExportOptions.IgnoreErrors
        {
            get => 
                XlIgnoreErrors.None;
            set
            {
            }
        }

        string IDataAwareExportOptions.SheetName
        {
            get => 
                null;
            set
            {
            }
        }

        bool IDataAwareExportOptions.CanRaiseCustomizeDocumentColumnEvent =>
            false;

        bool IDataAwareExportOptions.SuppressHyperlinkMaxCountWarning
        {
            get => 
                false;
            set
            {
            }
        }

        bool IDataAwareExportOptions.CanRaiseDocumentColumnFilteringEvent =>
            false;

        bool IDataAwareExportOptions.CanRaiseAfterAddRow =>
            false;

        bool IDataAwareExportOptions.CanRaiseCustomizeCellEvent =>
            this.CustomizeCell != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeSheetSettingsEvent =>
            false;

        bool IDataAwareExportOptions.CanRaiseCustomizeHeaderEvent =>
            false;

        bool IDataAwareExportOptions.CanRaiseCustomizeFooterEvent =>
            false;

        bool IDataAwareExportOptions.CanRaiseBeforeExportTable =>
            false;

        bool IDataAwareExportOptions.CanRaiseSkipFooterRowEvent =>
            false;

        [Description("Gets or sets export type."), DefaultValue(1), XtraSerializableProperty]
        public DevExpress.Export.ExportType ExportType
        {
            get => 
                (this.exportTypeCore == DevExpress.Export.ExportType.Default) ? ExportSettings.DefaultExportType : this.exportTypeCore;
            set => 
                this.exportTypeCore = value;
        }

        DefaultBoolean IDataAwareExportOptions.AllowBandHeaderCellMerge
        {
            get => 
                DefaultBoolean.False;
            set
            {
            }
        }
    }
}

