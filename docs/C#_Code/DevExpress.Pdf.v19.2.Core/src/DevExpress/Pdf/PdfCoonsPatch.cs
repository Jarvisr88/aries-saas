namespace DevExpress.Pdf
{
    using System;

    public class PdfCoonsPatch
    {
        private readonly PdfBezierCurve left;
        private readonly PdfBezierCurve top;
        private readonly PdfBezierCurve right;
        private readonly PdfBezierCurve bottom;

        internal PdfCoonsPatch(PdfBezierCurve left, PdfBezierCurve top, PdfBezierCurve right, PdfBezierCurve bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public PdfBezierCurve Left =>
            this.left;

        public PdfBezierCurve Top =>
            this.top;

        public PdfBezierCurve Right =>
            this.right;

        public PdfBezierCurve Bottom =>
            this.bottom;
    }
}

