namespace DevExpress.Pdf
{
    using System;

    public class PdfTensorProductPatch
    {
        private readonly PdfPoint[,] controlPoints;
        private readonly PdfColor[] colors;

        internal PdfTensorProductPatch(PdfPoint[,] controlPoints, params PdfColor[] colors)
        {
            this.controlPoints = controlPoints;
            this.colors = colors;
        }

        public PdfPoint[,] ControlPoints =>
            this.controlPoints;

        public PdfColor[] Colors =>
            this.colors;
    }
}

