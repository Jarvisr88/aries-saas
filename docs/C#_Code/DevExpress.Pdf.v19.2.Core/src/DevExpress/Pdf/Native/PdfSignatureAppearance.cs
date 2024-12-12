namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.IO;

    public abstract class PdfSignatureAppearance
    {
        private readonly PdfRectangle signatureBounds;
        private readonly int pageIndex;

        protected PdfSignatureAppearance(PdfRectangle signatureBounds, int pageIndex)
        {
            this.pageIndex = pageIndex;
            this.signatureBounds = signatureBounds;
        }

        public static PdfSignatureAppearance Create(byte[] data, PdfOrientedRectangle signatureBounds, int pageIndex) => 
            new PdfImageSignatureAppearance(data, signatureBounds, pageIndex);

        public static PdfSignatureAppearance Create(Stream data, PdfOrientedRectangle signatureBounds, int pageIndex) => 
            new PdfImageSignatureAppearance(data, signatureBounds, pageIndex);

        public virtual void CreateAppearance(PdfForm appearanceForm)
        {
            using (PdfCommandConstructor constructor = new PdfCommandConstructor(appearanceForm.Resources))
            {
                PdfDocumentCatalog documentCatalog = appearanceForm.DocumentCatalog;
                PdfForm form = new PdfForm(documentCatalog, appearanceForm.BBox);
                using (PdfFormCommandConstructor constructor2 = new PdfFormCommandConstructor(form))
                {
                    this.DrawFormContent(constructor2);
                    form.ReplaceCommands(constructor2.Commands);
                }
                constructor.DrawForm(documentCatalog.Objects.AddResolvedObject(form), new PdfTransformationMatrix());
                appearanceForm.ReplaceCommands(constructor.Commands);
            }
        }

        protected abstract void DrawFormContent(PdfFormCommandConstructor formConstructor);

        public PdfRectangle SignatureBounds =>
            this.signatureBounds;

        public int PageIndex =>
            this.pageIndex;
    }
}

