namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfIndexDescription
    {
        private readonly int startValue;
        private readonly int count;
        public int StartValue =>
            this.startValue;
        public int Count =>
            this.count;
        public PdfIndexDescription(int startValue, int count)
        {
            this.startValue = startValue;
            this.count = count;
        }
    }
}

