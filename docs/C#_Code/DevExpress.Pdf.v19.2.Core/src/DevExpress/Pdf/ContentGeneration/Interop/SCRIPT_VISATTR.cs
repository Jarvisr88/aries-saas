namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_VISATTR
    {
        private short value;
        public bool IsClusterStart =>
            (this.value & 0x10) != 0;
    }
}

