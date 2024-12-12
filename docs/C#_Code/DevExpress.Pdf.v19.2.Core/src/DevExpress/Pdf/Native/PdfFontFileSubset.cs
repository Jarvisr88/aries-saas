namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfFontFileSubset
    {
        public PdfFontFileSubsetType Type { get; }
        public byte[] Data { get; }
        public PdfFontFileSubset(PdfFontFileSubsetType type, byte[] data)
        {
            this.<Type>k__BackingField = type;
            this.<Data>k__BackingField = data;
        }
    }
}

