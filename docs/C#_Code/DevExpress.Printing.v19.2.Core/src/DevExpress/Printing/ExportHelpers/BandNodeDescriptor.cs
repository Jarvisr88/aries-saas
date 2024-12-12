namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct BandNodeDescriptor
    {
        public IColumn Column;
        public int Depth;
        public int Leaves;
        public int RowIndex;
        public int RowCount;
        public int ColIndex;
    }
}

