namespace DevExpress.XtraPrinting
{
    using DevExpress.Export;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class XlsxExportOptionsEx : XlsxExportOptions, IDataAwareExportOptions
    {
        private DefaultBoolean allowSortingAndFiltering;
        private DefaultBoolean allowCellMerge;
        private DefaultBoolean allowBandHeaderCellMerge;
        private DefaultBoolean allowLookupValues;
        private DefaultBoolean allowFixedColumnHeaderPanel;
        private DefaultBoolean allowFixedColumns;
        private DefaultBoolean allowHyperlinks;
        private DefaultBoolean allowSparklines;
        private bool suppressMaxColumnsWarning;
        private bool suppressMaxRowsWarning;
        private bool suppressHyperlinkMaxCountWarning;
        private DefaultBoolean allowConditionalFormatting;
        private DefaultBoolean allowHorzLines;
        private DefaultBoolean allowVertLines;
        private bool showDataValidationErrorMessage;
        private DevExpress.Export.ExportType exportTypeCore;

        public event AfterAddRowEventHandler AfterAddRow;

        public event DevExpress.Export.BeforeExportTable BeforeExportTable;

        public event CustomizeCellEventHandler CustomizeCell;

        public event CustomizeDocumentColumnEventHandler CustomizeDocumentColumn;

        public event CustomizeSheetFooterEventHandler CustomizeSheetFooter;

        public event CustomizeSheetHeaderEventHandler CustomizeSheetHeader;

        public event CustomizeSheetSettingsEventHandler CustomizeSheetSettings;

        public event DocumentColumnFilteringEventHandler DocumentColumnFiltering;

        public event ExportProgressCallback ExportProgress;

        public event SkipFooterRowEventHandler SkipFooterRow;

        public XlsxExportOptionsEx()
        {
            this.showDataValidationErrorMessage = true;
            this.Init();
        }

        public XlsxExportOptionsEx(TextExportMode textExportMode) : base(textExportMode)
        {
            this.showDataValidationErrorMessage = true;
            this.Init();
        }

        void IDataAwareExportOptions.InitDefaults()
        {
            this.AllowSortingAndFiltering = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowSortingAndFiltering, true);
            this.AllowLookupValues = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowLookupValues, true);
            this.AllowFixedColumns = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowFixedColumns, true);
            this.AllowFixedColumnHeaderPanel = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowFixedColumnHeaderPanel, true);
            this.AllowGrouping = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowGrouping, true);
            this.ShowGroupSummaries = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.ShowGroupSummaries, true);
            this.ShowTotalSummaries = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.ShowTotalSummaries, true);
            this.AllowCombinedBandAndColumnHeaderCellMerge = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowCombinedBandAndColumnHeaderCellMerge, false);
            this.AllowCellMerge = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowCellMerge, false);
            this.AllowBandHeaderCellMerge = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowBandHeaderCellMerge, true);
            this.ShowColumnHeaders = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.ShowColumnHeaders, true);
            this.ShowBandHeaders = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.ShowBandHeaders, true);
            ((IDataAwareExportOptions) this).AllowHorzLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(((IDataAwareExportOptions) this).AllowHorzLines, false);
            ((IDataAwareExportOptions) this).AllowVertLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(((IDataAwareExportOptions) this).AllowVertLines, false);
            ((IDataAwareExportOptions) this).AllowGroupingRows = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowGrouping, true);
            this.AllowHyperLinks = DataAwareExportOptionsFactory.UpdateDefaultBoolean(this.AllowHyperLinks, false);
        }

        void IDataAwareExportOptions.RaiseAfterAddRowEvent(AfterAddRowEventArgs ea)
        {
            if (this.AfterAddRow != null)
            {
                this.AfterAddRow(ea);
            }
        }

        void IDataAwareExportOptions.RaiseBeforeExportTable(BeforeExportTableEventArgs ea)
        {
            if (this.BeforeExportTable != null)
            {
                this.BeforeExportTable(ea);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeCellEvent(CustomizeCellEventArgs ea)
        {
            if (this.CustomizeCell != null)
            {
                this.CustomizeCell(ea);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeDocumentColumn(CustomizeDocumentColumnEventArgs ea)
        {
            if (this.CustomizeDocumentColumn != null)
            {
                this.CustomizeDocumentColumn(ea);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetFooterEvent(ContextEventArgs ea)
        {
            if (this.CustomizeSheetFooter != null)
            {
                this.CustomizeSheetFooter(ea);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetHeaderEvent(ContextEventArgs ea)
        {
            if (this.CustomizeSheetHeader != null)
            {
                this.CustomizeSheetHeader(ea);
            }
        }

        void IDataAwareExportOptions.RaiseCustomizeSheetSettingsEvent(CustomizeSheetEventArgs ea)
        {
            if (this.CustomizeSheetSettings != null)
            {
                this.CustomizeSheetSettings(ea);
            }
        }

        void IDataAwareExportOptions.RaiseDocumentColumnFiltering(DocumentColumnFilteringEventArgs ea)
        {
            if (this.DocumentColumnFiltering != null)
            {
                this.DocumentColumnFiltering(ea);
            }
        }

        void IDataAwareExportOptions.RaiseSkipFooterRowEvent(SkipFooterRowEventArgs ea)
        {
            if (this.SkipFooterRow != null)
            {
                this.SkipFooterRow(ea);
            }
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
            this.AutoCalcConditionalFormattingIconSetMinValue = DefaultBoolean.Default;
            this.AllowSortingAndFiltering = DefaultBoolean.Default;
            this.AllowLookupValues = DefaultBoolean.Default;
            this.ShowPageTitle = DefaultBoolean.Default;
            this.AllowFixedColumns = DefaultBoolean.Default;
            this.AllowFixedColumnHeaderPanel = DefaultBoolean.Default;
            this.ShowTotalSummaries = DefaultBoolean.Default;
            this.AllowGrouping = DefaultBoolean.Default;
            this.ShowGroupSummaries = DefaultBoolean.Default;
            this.ShowColumnHeaders = DefaultBoolean.Default;
            this.ShowBandHeaders = DefaultBoolean.Default;
            this.AllowHyperLinks = DefaultBoolean.Default;
            this.AllowCellMerge = DefaultBoolean.Default;
            this.AllowCombinedBandAndColumnHeaderCellMerge = DefaultBoolean.Default;
            this.AllowBandHeaderCellMerge = DefaultBoolean.Default;
            ((IDataAwareExportOptions) this).AllowVertLines = DefaultBoolean.Default;
            ((IDataAwareExportOptions) this).AllowHorzLines = DefaultBoolean.Default;
            ((IDataAwareExportOptions) this).AllowGroupingRows = DefaultBoolean.Default;
            base.ShowGridLines = true;
            base.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
            this.DocumentCulture = CultureInfo.CurrentCulture;
            base.RichTextRunsEnabled = true;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ExportHyperlinks { get; set; }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool SuppressMaxColumnsWarning
        {
            get => 
                this.suppressMaxColumnsWarning;
            set => 
                this.suppressMaxColumnsWarning = value;
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool SuppressHyperlinkMaxCountWarning
        {
            get => 
                this.suppressHyperlinkMaxCountWarning;
            set => 
                this.suppressHyperlinkMaxCountWarning = value;
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool SuppressMaxRowsWarning
        {
            get => 
                this.suppressMaxRowsWarning;
            set => 
                this.suppressMaxRowsWarning = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowCombinedBandAndColumnHeaderCellMerge { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowGrouping { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowSparklines
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowSparklines, base.RawDataMode);
            set => 
                this.allowSparklines = value;
        }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ApplyFormattingToEntireColumn { get; set; }

        [XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(EnumConverter))]
        public DevExpress.Export.GroupState GroupState { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ShowTotalSummaries { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ShowGroupSummaries { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ShowPageTitle { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ShowColumnHeaders { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean ShowBandHeaders { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowConditionalFormatting
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowConditionalFormatting, base.RawDataMode);
            set => 
                this.allowConditionalFormatting = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AutoCalcConditionalFormattingIconSetMinValue { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowSortingAndFiltering
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowSortingAndFiltering, base.RawDataMode);
            set => 
                this.allowSortingAndFiltering = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowCellMerge
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowCellMerge, base.RawDataMode);
            set => 
                this.allowCellMerge = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowBandHeaderCellMerge
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowBandHeaderCellMerge, base.RawDataMode);
            set => 
                this.allowBandHeaderCellMerge = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowLookupValues
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowLookupValues, base.RawDataMode);
            set => 
                this.allowLookupValues = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowFixedColumnHeaderPanel
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowFixedColumnHeaderPanel, base.RawDataMode);
            set => 
                this.allowFixedColumnHeaderPanel = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowFixedColumns
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowFixedColumns, base.RawDataMode);
            set => 
                this.allowFixedColumns = value;
        }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        public DefaultBoolean AllowHyperLinks
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowHyperlinks, base.RawDataMode);
            set => 
                this.allowHyperlinks = value;
        }

        DefaultBoolean IDataAwareExportOptions.AllowHorzLines
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowHorzLines, base.RawDataMode);
            set => 
                this.allowHorzLines = value;
        }

        DefaultBoolean IDataAwareExportOptions.AllowVertLines
        {
            get => 
                DataAwareExportOptionsFactory.GetActualOptionValue(this.allowVertLines, base.RawDataMode);
            set => 
                this.allowVertLines = value;
        }

        [XtraSerializableProperty]
        public bool SummaryCountBlankCells { get; set; }

        [XtraSerializableProperty, DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter))]
        DefaultBoolean IDataAwareExportOptions.AllowGroupingRows
        {
            get => 
                this.AllowGrouping;
            set => 
                this.AllowGrouping = value;
        }

        ExportTarget IDataAwareExportOptions.ExportTarget
        {
            get => 
                ExportTarget.Xlsx;
            set
            {
            }
        }

        string IDataAwareExportOptions.CSVSeparator
        {
            get => 
                null;
            set
            {
            }
        }

        public bool CalcTotalSummaryOnCompositeRange { get; set; }

        Encoding IDataAwareExportOptions.CSVEncoding
        {
            get => 
                null;
            set
            {
            }
        }

        bool IDataAwareExportOptions.WritePreamble
        {
            get => 
                false;
            set
            {
            }
        }

        [XtraSerializableProperty]
        bool IDataAwareExportOptions.ShowDataValidationErrorMessage
        {
            get => 
                this.showDataValidationErrorMessage;
            set => 
                this.showDataValidationErrorMessage = value;
        }

        [XtraSerializableProperty]
        public DevExpress.Export.LayoutMode LayoutMode { get; set; }

        bool IDataAwareExportOptions.CanRaiseBeforeExportTable =>
            this.BeforeExportTable != null;

        public CultureInfo DocumentCulture { get; set; }

        [XtraSerializableProperty]
        public DevExpress.Export.BandedLayoutMode BandedLayoutMode { get; set; }

        public DevExpress.Export.UnboundExpressionExportMode UnboundExpressionExportMode { get; set; }

        public bool SuppressEmptyStrings { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public bool RasterizeImages
        {
            get => 
                false;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public int RasterizationResolution
        {
            get => 
                0;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public string PageRange
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        bool IDataAwareExportOptions.CanRaiseDocumentColumnFilteringEvent =>
            this.DocumentColumnFiltering != null;

        bool IDataAwareExportOptions.CanRaiseAfterAddRow =>
            this.AfterAddRow != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeCellEvent =>
            this.CustomizeCell != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeSheetSettingsEvent =>
            this.CustomizeSheetSettings != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeHeaderEvent =>
            this.CustomizeSheetHeader != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeFooterEvent =>
            this.CustomizeSheetFooter != null;

        bool IDataAwareExportOptions.CanRaiseSkipFooterRowEvent =>
            this.SkipFooterRow != null;

        bool IDataAwareExportOptions.CanRaiseCustomizeDocumentColumnEvent =>
            this.CustomizeDocumentColumn != null;

        [DefaultValue(1), TypeConverter(typeof(EnumTypeConverter)), XtraSerializableProperty]
        public DevExpress.Export.ExportType ExportType
        {
            get => 
                (this.exportTypeCore == DevExpress.Export.ExportType.Default) ? ExportSettings.DefaultExportType : this.exportTypeCore;
            set => 
                this.exportTypeCore = value;
        }
    }
}

