namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JBIG2HuffmanTableLine
    {
        public int? Code { get; }
        public int Preflen { get; }
        public int RangeLen { get; }
        public int RangeLow { get; }
        public JBIG2HuffmanTableLine(int? code, int preflen, int rangeLen, int rangeLow)
        {
            this.<Code>k__BackingField = code;
            this.<Preflen>k__BackingField = preflen;
            this.<RangeLen>k__BackingField = rangeLen;
            this.<RangeLow>k__BackingField = rangeLow;
        }
    }
}

