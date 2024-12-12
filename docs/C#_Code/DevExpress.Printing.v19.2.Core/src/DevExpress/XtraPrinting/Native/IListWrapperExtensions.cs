namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class IListWrapperExtensions
    {
        public static T GetLast<T>(this IListWrapper<T> listWrapper);
        public static bool IsValidIndex<T>(this IListWrapper<T> listWrapper, int index);
        public static void Remove<T>(this IListWrapper<T> listWrapper, T item);
    }
}

