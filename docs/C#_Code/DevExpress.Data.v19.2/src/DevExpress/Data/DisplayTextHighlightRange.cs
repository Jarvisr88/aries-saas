namespace DevExpress.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayTextHighlightRange
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public DisplayTextHighlightRange(int start, int length);
        internal void SetStart(int value);
        internal void SetLength(int value);
    }
}

