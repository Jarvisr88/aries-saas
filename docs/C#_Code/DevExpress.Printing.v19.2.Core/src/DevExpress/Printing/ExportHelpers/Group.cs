namespace DevExpress.Printing.ExportHelpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Group
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int GroupLevel { get; set; }
    }
}

