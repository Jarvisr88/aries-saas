namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfFormCommandConstructor : PdfCommandConstructor
    {
        private readonly PdfRectangle contentSquare;
        private readonly PdfForm form;

        public PdfFormCommandConstructor(PdfForm form) : base(form.Resources)
        {
            this.form = form;
            PdfRectangle bBox = form.BBox;
            double num = PdfMathUtils.Min(bBox.Width, bBox.Height) / 2.0;
            double num2 = (bBox.Left + bBox.Right) / 2.0;
            double num3 = (bBox.Bottom + bBox.Top) / 2.0;
            this.contentSquare = new PdfRectangle(num2 - num, num3 - num, num2 + num, num3 + num);
        }

        public PdfRectangle BoundingBox =>
            this.form.BBox;

        public PdfRectangle ContentSquare =>
            this.contentSquare;

        public PdfForm Form =>
            this.form;
    }
}

