namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GOFFSET
    {
        private int du;
        private int dv;
        public int HorizontalOffset =>
            this.du;
        public int VerticalOffset =>
            this.dv;
    }
}

