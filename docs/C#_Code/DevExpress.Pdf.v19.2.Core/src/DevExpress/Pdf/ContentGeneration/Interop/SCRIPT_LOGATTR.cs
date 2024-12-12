namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_LOGATTR
    {
        private byte value;
        public bool SoftBreak =>
            (this.value & 1) != 0;
        public bool WhiteSpace =>
            (this.value & 2) != 0;
    }
}

