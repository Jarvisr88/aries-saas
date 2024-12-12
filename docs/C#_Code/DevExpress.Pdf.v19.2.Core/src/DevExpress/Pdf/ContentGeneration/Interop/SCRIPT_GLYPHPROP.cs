namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_GLYPHPROP
    {
        private SCRIPT_VISATTR sva;
        private short reserved;
        public bool IsClusterStart =>
            this.sva.IsClusterStart;
    }
}

