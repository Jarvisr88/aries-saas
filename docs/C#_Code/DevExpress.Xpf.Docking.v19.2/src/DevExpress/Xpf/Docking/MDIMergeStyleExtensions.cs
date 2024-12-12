namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    internal static class MDIMergeStyleExtensions
    {
        internal static bool IsDefault(this MDIMergeStyle value) => 
            (value == MDIMergeStyle.Default) || (value == MDIMergeStyle.WhenChildActivated);
    }
}

