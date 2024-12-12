namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXComponent
    {
        private readonly int bitsPerComponent;
        private readonly int horizontalSeparation;
        private readonly int verticalSeparation;
        private readonly bool isSigned;
        public int BitsPerComponent =>
            this.bitsPerComponent;
        public bool IsSigned =>
            this.isSigned;
        public int HorizontalSeparation =>
            this.horizontalSeparation;
        public int VerticalSeparation =>
            this.verticalSeparation;
        public JPXComponent(int bitsPerComponent, int horizontalSeparation, int verticalSeparation)
        {
            this.isSigned = (bitsPerComponent & 0x80) != 0;
            this.bitsPerComponent = (bitsPerComponent & 0x7f) + 1;
            this.horizontalSeparation = horizontalSeparation;
            this.verticalSeparation = verticalSeparation;
        }
    }
}

