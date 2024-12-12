namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TEXTRANGE_PROPERTIES
    {
        private readonly IntPtr potfRecords;
        private readonly int cotfRecords;
        public TEXTRANGE_PROPERTIES(IntPtr potfRecords, int cotfRecords)
        {
            this.potfRecords = potfRecords;
            this.cotfRecords = cotfRecords;
        }
    }
}

