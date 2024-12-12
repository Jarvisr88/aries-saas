namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Internal;
    using DevExpress.Office.Model;
    using System;

    public class OfficeThemeBase<TFormat> : DocumentModelBase<TFormat>, IDocumentModelPart, IOfficeTheme, IDisposable
    {
        private readonly ThemeDrawingColorCollection colors;
        private readonly ThemeFontScheme fontScheme;
        private readonly ThemeFormatScheme formatScheme;
        private string name;

        public OfficeThemeBase()
        {
            this.name = string.Empty;
            this.SwitchToEmptyHistory(true);
            this.colors = new ThemeDrawingColorCollection(this);
            this.fontScheme = new ThemeFontScheme(this);
            this.formatScheme = new ThemeFormatScheme();
        }

        public void Clear()
        {
            this.name = string.Empty;
            this.colors.Clear();
            this.fontScheme.Clear();
            this.formatScheme.Clear();
        }

        public IOfficeTheme Clone()
        {
            OfficeThemeBase<TFormat> base2 = new OfficeThemeBase<TFormat>();
            base2.CopyFrom(this);
            return base2;
        }

        public void CopyFrom(IOfficeTheme sourceObj)
        {
            this.name = sourceObj.Name;
            this.colors.CopyFrom(sourceObj.Colors);
            this.fontScheme.CopyFrom(sourceObj.FontScheme);
            this.formatScheme.CopyFrom(this, sourceObj);
        }

        public override ExportHelper<TFormat, bool> CreateDocumentExportHelper(TFormat documentFormat) => 
            null;

        protected internal override ImportHelper<TFormat, bool> CreateDocumentImportHelper() => 
            null;

        protected override void CreateOfficeTheme()
        {
        }

        protected internal override IExportManagerService<TFormat, bool> GetExportManagerService() => 
            null;

        protected internal override IImportManagerService<TFormat, bool> GetImportManagerService() => 
            null;

        public override void OnBeginUpdate()
        {
        }

        public override void OnCancelUpdate()
        {
        }

        public override void OnEndUpdate()
        {
        }

        public override void OnFirstBeginUpdate()
        {
        }

        protected internal override void OnHistoryModifiedChanged(object sender, EventArgs e)
        {
        }

        protected internal override void OnHistoryOperationCompleted(object sender, EventArgs e)
        {
        }

        public override void OnLastCancelUpdate()
        {
        }

        public override void OnLastEndUpdate()
        {
        }

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public ThemeDrawingColorCollection Colors =>
            this.colors;

        public ThemeFontScheme FontScheme =>
            this.fontScheme;

        public ThemeFormatScheme FormatScheme =>
            this.formatScheme;

        public override IOfficeTheme OfficeTheme
        {
            get => 
                this;
            set
            {
            }
        }

        public bool IsValidate =>
            !string.IsNullOrEmpty(this.name) && (this.colors.IsValidate && (this.fontScheme.IsValidate && this.formatScheme.IsValidate));

        IDocumentModel IDocumentModelPart.DocumentModel =>
            this;

        public override IDocumentModelPart MainPart =>
            this;
    }
}

