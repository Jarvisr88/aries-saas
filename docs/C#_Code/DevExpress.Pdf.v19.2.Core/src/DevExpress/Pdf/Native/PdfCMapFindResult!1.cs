namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfCMapFindResult<T>
    {
        private readonly T value;
        private readonly int codeLength;
        public T Value =>
            this.value;
        public int CodeLength =>
            this.codeLength;
        public PdfCMapFindResult(T value, int codeLength)
        {
            this.value = value;
            this.codeLength = codeLength;
        }
    }
}

