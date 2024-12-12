namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_ITEM
    {
        private int iCharPos;
        private SCRIPT_ANALYSIS a;
        public int ICharPos =>
            this.iCharPos;
        public SCRIPT_ANALYSIS Analysis =>
            this.a;
    }
}

