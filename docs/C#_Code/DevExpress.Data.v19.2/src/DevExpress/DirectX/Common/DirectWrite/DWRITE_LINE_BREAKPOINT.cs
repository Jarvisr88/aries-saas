namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_LINE_BREAKPOINT
    {
        private byte vector;
        public DWRITE_BREAK_CONDITION BreakConditionBefore =>
            ((DWRITE_BREAK_CONDITION) this.vector) & DWRITE_BREAK_CONDITION.MUST_BREAK;
        public DWRITE_BREAK_CONDITION BreakConditionAfter =>
            ((DWRITE_BREAK_CONDITION) (this.vector >> 2)) & DWRITE_BREAK_CONDITION.MUST_BREAK;
        public bool IsWhitespace =>
            ((this.vector >> 4) & 1) != 0;
        public bool IsSoftHyphen =>
            ((this.vector >> 5) & 1) != 0;
    }
}

