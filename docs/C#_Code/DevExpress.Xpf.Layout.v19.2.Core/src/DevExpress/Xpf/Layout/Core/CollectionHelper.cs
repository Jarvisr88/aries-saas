namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class CollectionHelper
    {
        public static void Accept<T>(this ICollection<T> collection, VisitDelegate<T> visit) where T: class
        {
            foreach (T local in collection)
            {
                visit(local);
            }
        }

        public static bool IsValidIndex<T>(this ICollection<T> collection, int index) => 
            (index >= 0) && (index < collection.Count);
    }
}

