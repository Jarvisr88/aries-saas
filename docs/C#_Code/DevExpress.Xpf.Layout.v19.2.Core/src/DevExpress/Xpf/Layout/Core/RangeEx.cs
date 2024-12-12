namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class RangeEx
    {
        public static bool IsBetween<T>(this T item, T start, T end) => 
            (Comparer<T>.Default.Compare(item, start) >= 0) && (Comparer<T>.Default.Compare(item, end) <= 0);
    }
}

