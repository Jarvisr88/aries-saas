namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct TTFHMtxEntry
    {
        public ushort AdvanceWidth;
        public short LeftSideBearing;
        public static int SizeOf =>
            4;
    }
}

