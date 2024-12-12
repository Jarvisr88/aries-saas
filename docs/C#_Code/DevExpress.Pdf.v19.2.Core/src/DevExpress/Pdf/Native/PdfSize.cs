namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfSize
    {
        public double Width { get; }
        public double Height { get; }
        public PdfSize(double width, double height)
        {
            this.<Width>k__BackingField = width;
            this.<Height>k__BackingField = height;
        }
    }
}

