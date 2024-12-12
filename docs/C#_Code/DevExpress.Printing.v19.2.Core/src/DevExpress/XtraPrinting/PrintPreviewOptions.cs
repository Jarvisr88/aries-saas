namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions")]
    public class PrintPreviewOptions
    {
        [Browsable(false)]
        public const string DefaultFileNameDefault = "Document";
        private const bool DefaultShowOptionsBeforeExport = true;
        private const DevExpress.XtraPrinting.ActionAfterExport DefaultActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.AskUser;
        private bool showOptionsBeforeExport = true;
        private string defaultFileName = "Document";
        private DevExpress.XtraPrinting.ActionAfterExport actionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.AskUser;
        private DevExpress.XtraPrinting.SaveMode saveMode = DevExpress.XtraPrinting.SaveMode.UsingSaveFileDialog;
        private string defaultDirectory = string.Empty;
        private PrintingSystemCommand defaultExportFormat = PrintingSystemCommand.ExportPdf;
        private PrintingSystemCommand defaultSendFormat = PrintingSystemCommand.SendPdf;

        internal void Assign(PrintPreviewOptions source)
        {
            this.showOptionsBeforeExport = source.ShowOptionsBeforeExport;
            this.defaultFileName = source.DefaultFileName;
            this.actionAfterExport = source.ActionAfterExport;
            this.saveMode = source.SaveMode;
            this.defaultDirectory = source.DefaultDirectory;
            this.defaultExportFormat = source.DefaultExportFormat;
            this.defaultSendFormat = source.DefaultSendFormat;
        }

        internal bool ShouldSerialize() => 
            !this.showOptionsBeforeExport || ((this.defaultFileName != "Document") || ((this.actionAfterExport != DevExpress.XtraPrinting.ActionAfterExport.AskUser) || ((this.saveMode != DevExpress.XtraPrinting.SaveMode.UsingSaveFileDialog) || ((this.defaultDirectory != "") || ((this.defaultExportFormat != PrintingSystemCommand.ExportPdf) || (this.defaultSendFormat != PrintingSystemCommand.SendPdf))))));

        [Description("Gets or sets a value which indicates whether an Export Options window should be shown when an end-user exports a document from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.ShowOptionsBeforeExport"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty]
        public bool ShowOptionsBeforeExport
        {
            get => 
                this.showOptionsBeforeExport;
            set => 
                this.showOptionsBeforeExport = value;
        }

        [Description("Gets or sets the file name to which, by default, a document is exported from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.DefaultFileName"), DefaultValue("Document"), XtraSerializableProperty]
        public string DefaultFileName
        {
            get => 
                this.defaultFileName;
            set => 
                this.defaultFileName = value;
        }

        [Description("Gets or sets a value which indicates whether the resulting file should be automatically opened after exporting a document from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.ActionAfterExport"), DefaultValue(2), XtraSerializableProperty]
        public DevExpress.XtraPrinting.ActionAfterExport ActionAfterExport
        {
            get => 
                this.actionAfterExport;
            set => 
                this.actionAfterExport = value;
        }

        [Description("Gets or sets a value which specifies how the file path to export a document is obtained when exporting a document from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.SaveMode"), DefaultValue(1), XtraSerializableProperty]
        public DevExpress.XtraPrinting.SaveMode SaveMode
        {
            get => 
                this.saveMode;
            set => 
                this.saveMode = value;
        }

        [Description("Gets or sets the file path to which, by default, a document is exported from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.DefaultDirectory"), DefaultValue(""), XtraSerializableProperty]
        public string DefaultDirectory
        {
            get => 
                this.defaultDirectory;
            set => 
                this.defaultDirectory = value;
        }

        [Description("Specifies the default format to which a document is exported from the Print Preview form."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.DefaultExportFormat"), DefaultValue(30), XtraSerializableProperty, TypeConverter(typeof(PSCommandsExportTypeConverter))]
        public PrintingSystemCommand DefaultExportFormat
        {
            get => 
                this.defaultExportFormat;
            set
            {
                if (PSCommandHelper.IsExportCommand(value))
                {
                    this.defaultExportFormat = value;
                }
            }
        }

        [Description("Specifies the default format to which a document is converted, when the PrintingSystemCommand.SendFile command is executed."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrintPreviewOptions.DefaultSendFormat"), DefaultValue(0x29), XtraSerializableProperty, TypeConverter(typeof(PSCommandsSendTypeConverter))]
        public PrintingSystemCommand DefaultSendFormat
        {
            get => 
                this.defaultSendFormat;
            set
            {
                if (PSCommandHelper.IsSendCommand(value))
                {
                    this.defaultSendFormat = value;
                }
            }
        }
    }
}

