namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfAnnotationStatePaintData
    {
        private readonly PdfForm form;
        private readonly PdfTransformationMatrix matrix;

        public PdfAnnotationStatePaintData(PdfForm form, PdfTransformationMatrix matrix)
        {
            this.form = form;
            this.matrix = matrix;
        }

        public PdfForm Form =>
            this.form;

        public PdfTransformationMatrix Matrix =>
            this.matrix;
    }
}

