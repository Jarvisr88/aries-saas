namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_PROPERTIES
    {
        private int value;
        private int value2;
        public bool Control =>
            (this.value & 0x10000000) != 0;
    }
}

