namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public abstract class DocumentModelImporter
    {
        private readonly IDocumentModel documentModel;
        private readonly DevExpress.Office.Utils.ProgressIndication progressIndication;
        private readonly DevExpress.Office.Utils.CancellationProvider cancellationProvider;

        protected DocumentModelImporter(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.progressIndication = new DevExpress.Office.Utils.ProgressIndication(documentModel);
            this.cancellationProvider = new DevExpress.Office.Utils.CancellationProvider(documentModel);
        }

        public abstract void ThrowInvalidFile();

        public IDocumentModel DocumentModel =>
            this.documentModel;

        protected internal DevExpress.Office.Utils.ProgressIndication ProgressIndication =>
            this.progressIndication;

        public DevExpress.Office.Utils.CancellationProvider CancellationProvider =>
            this.cancellationProvider;
    }
}

