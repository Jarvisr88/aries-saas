namespace DevExpress.Text.Fonts
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXLineBreakpoint
    {
        private const char softHyphen = '\x00ad';
        public DXBreakCondition BreakConditionAfter { get; }
        public bool IsWhitespace { get; }
        public bool IsSoftHyphen { get; }
        public DXLineBreakpoint(DWRITE_LINE_BREAKPOINT breakpoint)
        {
            this.<BreakConditionAfter>k__BackingField = (DXBreakCondition) breakpoint.BreakConditionAfter;
            this.<IsWhitespace>k__BackingField = breakpoint.IsWhitespace;
            this.<IsSoftHyphen>k__BackingField = breakpoint.IsSoftHyphen;
        }

        public DXLineBreakpoint(DXBreakCondition breakConditionAfter, char c) : this(breakConditionAfter, char.IsWhiteSpace(c), c == '\x00ad')
        {
        }

        public DXLineBreakpoint(DXBreakCondition breakConditionAfter, bool isWhitespace, bool isSoftHyphen)
        {
            this.<BreakConditionAfter>k__BackingField = breakConditionAfter;
            this.<IsWhitespace>k__BackingField = isWhitespace;
            this.<IsSoftHyphen>k__BackingField = isSoftHyphen;
        }
    }
}

