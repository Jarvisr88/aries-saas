namespace DevExpress.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class IListExtensions
    {
        public static int FindIndex<T>(this IList<T> list, Predicate<T> predicate);
        public static int GetValidIndex<T>(this IList<T> array, int index);
        public static bool IsValidIndex<T>(this IList<T> array, int index);
        public static bool TryGetValue<T>(this IList<T> array, int index, out T value);
    }
}

