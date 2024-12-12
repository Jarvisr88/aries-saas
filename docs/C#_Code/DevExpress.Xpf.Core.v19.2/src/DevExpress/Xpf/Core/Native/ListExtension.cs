namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class ListExtension
    {
        public static bool SafeListsEqual<T>(this IList<T> list1, IList<T> list2);
    }
}

