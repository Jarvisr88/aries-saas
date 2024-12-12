namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ImportExportHelper
    {
        private readonly IDocumentModel documentModel;
        private CommonDialogProviderBase dialogProviderService;

        protected ImportExportHelper(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.dialogProviderService = this.DocumentModel.GetService<CommonDialogProviderBase>();
        }

        protected void ApplyFileDialogSettings(string fileName, int filterIndex)
        {
            this.FileName = fileName;
            this.FilterIndex = filterIndex;
        }

        protected string CreateFilterString(FileDialogFilterCollection filters) => 
            filters.CreateFilterString();

        public abstract void ThrowUnsupportedFormatException();

        public IDocumentModel DocumentModel =>
            this.documentModel;

        protected CommonDialogProviderBase DialogProviderService =>
            this.dialogProviderService;

        protected string FileName { get; set; }

        protected int FilterIndex { get; set; }
    }
}

