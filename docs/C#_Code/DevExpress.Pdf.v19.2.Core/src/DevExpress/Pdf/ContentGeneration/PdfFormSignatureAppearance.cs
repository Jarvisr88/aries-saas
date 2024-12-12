namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFormSignatureAppearance : PdfSignatureAppearance
    {
        private readonly PdfForm form;

        public PdfFormSignatureAppearance(PdfForm form, PdfRectangle signatureBounds, int pageIndex) : base(signatureBounds, pageIndex)
        {
            this.form = form;
        }

        protected override void DrawFormContent(PdfFormCommandConstructor formConstructor)
        {
            formConstructor.DrawForm(formConstructor.DocumentCatalog.Objects.AddResolvedObject(this.form), new PdfTransformationMatrix());
        }

        public PdfForm Form =>
            this.form;
    }
}

