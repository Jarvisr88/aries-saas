namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.NativeFormatOptions")]
    public class NativeFormatOptions : ExportOptionsBase
    {
        private const bool CompressedDefaultValue = true;
        private const bool ShowOptionsBeforeSaveDefaultValue = false;
        private bool compressed;
        private bool showOptionsBeforeSave;

        public NativeFormatOptions()
        {
            this.compressed = true;
        }

        private NativeFormatOptions(NativeFormatOptions source) : base(source)
        {
            this.compressed = true;
        }

        public override void Assign(ExportOptionsBase source)
        {
            NativeFormatOptions options = (NativeFormatOptions) source;
            this.Compressed = options.Compressed;
            this.ShowOptionsBeforeSave = options.ShowOptionsBeforeSave;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new NativeFormatOptions(this);

        protected internal override bool GetShowOptionsBeforeExport(bool defaultValue) => 
            this.ShowOptionsBeforeSave;

        protected internal override bool ShouldSerialize() => 
            !this.compressed || this.showOptionsBeforeSave;

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase =>
            DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

        [Description("Gets or sets a value indicating whether the resulting PRNX file should be compressed."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.NativeFormatOptions.Compressed"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty]
        public bool Compressed
        {
            get => 
                this.compressed;
            set => 
                this.compressed = value;
        }

        [Description("Gets or sets a value which indicates whether a Native Format Options window should be shown when an end-user saves a document from the Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.NativeFormatOptions.ShowOptionsBeforeSave"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(false), XtraSerializableProperty]
        public bool ShowOptionsBeforeSave
        {
            get => 
                this.showOptionsBeforeSave;
            set => 
                this.showOptionsBeforeSave = value;
        }

        protected internal override bool UseActionAfterExportAndSaveModeValue =>
            false;
    }
}

