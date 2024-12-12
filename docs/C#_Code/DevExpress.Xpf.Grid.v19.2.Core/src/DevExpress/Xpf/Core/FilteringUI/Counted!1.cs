namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), DebuggerDisplay("{Value}: {Count}")]
    public struct Counted<TValue>
    {
        public Counted(TValue value, int count)
        {
            this.<Value>k__BackingField = value;
            this.<Count>k__BackingField = count;
        }

        public TValue Value { get; }
        public int Count { get; }
    }
}

