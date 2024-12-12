namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfConvolutionWindowInfo
    {
        private readonly PdfFixedPointNumber[] weights;
        private readonly int startPosition;
        public PdfFixedPointNumber[] Weights =>
            this.weights;
        public int StartPosition =>
            this.startPosition;
        public PdfConvolutionWindowInfo(PdfFixedPointNumber[] weight, int startPosition)
        {
            this.weights = weight;
            this.startPosition = startPosition;
        }
    }
}

