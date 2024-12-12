namespace System.Collections
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class ArrayListExtensions
    {
        public static void AddRange<T>(this ArrayList arrayList, IListWrapper<T> items)
        {
            arrayList.Capacity = Math.Max(arrayList.Capacity, arrayList.Count + items.Count);
            foreach (object obj2 in items)
            {
                arrayList.Add(obj2);
            }
        }

        public static ArrayList CreateInstance<T>(IListWrapper<T> items)
        {
            ArrayList arrayList = new ArrayList(items.Count);
            arrayList.AddRange<T>(items);
            return arrayList;
        }
    }
}

