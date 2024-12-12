namespace DevExpress.Export
{
    using DevExpress.Utils;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class ClipboardOptions : BaseOptions
    {
        private bool allowFormattedMode = true;
        private DevExpress.Export.ClipboardMode clipboardModeCore = DevExpress.Export.ClipboardMode.Default;
        private ProgressMode showProgressCore = ProgressMode.Automatic;
        private DefaultBoolean copyColumnHeadersCore = DefaultBoolean.Default;
        private DefaultBoolean allowTxtFormatCore = DefaultBoolean.Default;
        private DefaultBoolean allowRtfFormatCore = DefaultBoolean.Default;
        private DefaultBoolean allowHtmlFormatCore = DefaultBoolean.Default;
        private DefaultBoolean allowExcelFormatCore = DefaultBoolean.Default;
        private DefaultBoolean allowCsvFormatCore = DefaultBoolean.Default;
        private DefaultBoolean copyCollapsedDataCore;
        private DevExpress.Export.PasteMode pasteModeCore;

        public ClipboardOptions(bool allowFormattedMode = true)
        {
            this.allowFormattedMode = allowFormattedMode;
            this.copyCollapsedDataCore = DefaultBoolean.Default;
        }

        public override void Assign(BaseOptions options)
        {
            base.Assign(options);
            ClipboardOptions options2 = options as ClipboardOptions;
            if (options2 != null)
            {
                this.copyColumnHeadersCore = options2.copyColumnHeadersCore;
                this.allowRtfFormatCore = options2.allowRtfFormatCore;
                this.allowHtmlFormatCore = options2.allowHtmlFormatCore;
                this.allowExcelFormatCore = options2.allowExcelFormatCore;
                this.allowCsvFormatCore = options2.allowCsvFormatCore;
                this.allowTxtFormatCore = options2.allowTxtFormatCore;
                this.clipboardModeCore = options2.clipboardModeCore;
                this.showProgressCore = options2.showProgressCore;
                this.copyCollapsedDataCore = options2.copyCollapsedDataCore;
            }
        }

        [DefaultValue(0), XtraSerializableProperty, Description("Gets or sets whether data is copied to the Clipboard as plain text or along with its format settings (in RTF, HTML, XLS (Biff8), CSV, UnicodeText and Text formats).")]
        public DevExpress.Export.ClipboardMode ClipboardMode
        {
            get => 
                this.allowFormattedMode ? this.clipboardModeCore : DevExpress.Export.ClipboardMode.Default;
            set => 
                this.clipboardModeCore = value;
        }

        [DefaultValue(0), XtraSerializableProperty, Description("Gets or sets whether a progress bar that indicates a data copy operation's progress is enabled.")]
        public ProgressMode ShowProgress
        {
            get => 
                this.showProgressCore;
            set => 
                this.showProgressCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether column and band headers are to be copied along with data cells.")]
        public DefaultBoolean CopyColumnHeaders
        {
            get => 
                this.copyColumnHeadersCore;
            set => 
                this.copyColumnHeadersCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether copying data in Text and UnicodeText formats is enabled.")]
        public DefaultBoolean AllowTxtFormat
        {
            get => 
                this.allowTxtFormatCore;
            set => 
                this.allowTxtFormatCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether copying data in RTF format is enabled.")]
        public DefaultBoolean AllowRtfFormat
        {
            get => 
                this.allowRtfFormatCore;
            set => 
                this.allowRtfFormatCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether copying data in HTML format is enabled.")]
        public DefaultBoolean AllowHtmlFormat
        {
            get => 
                this.allowHtmlFormatCore;
            set => 
                this.allowHtmlFormatCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether copying data in XLS (Biff8) format is enabled.")]
        public DefaultBoolean AllowExcelFormat
        {
            get => 
                this.allowExcelFormatCore;
            set => 
                this.allowExcelFormatCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether copying data in CSV format is enabled.")]
        public DefaultBoolean AllowCsvFormat
        {
            get => 
                this.allowCsvFormatCore;
            set => 
                this.allowCsvFormatCore = value;
        }

        [DefaultValue(2), XtraSerializableProperty, Description("Gets or sets whether formatted data is copied from both expanded and collapsed rows/nodes.")]
        public DefaultBoolean CopyCollapsedData
        {
            get => 
                this.copyCollapsedDataCore;
            set => 
                this.copyCollapsedDataCore = value;
        }

        [DefaultValue(0), XtraSerializableProperty, Description("Gets or sets data pasting mode.")]
        public DevExpress.Export.PasteMode PasteMode
        {
            get => 
                this.pasteModeCore;
            set => 
                this.pasteModeCore = value;
        }
    }
}

