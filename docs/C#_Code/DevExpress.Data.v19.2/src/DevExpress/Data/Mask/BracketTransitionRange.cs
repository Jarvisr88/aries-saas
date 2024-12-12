namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct BracketTransitionRange
    {
        public readonly char From;
        public readonly char To;
        public BracketTransitionRange(char from, char to);
    }
}

