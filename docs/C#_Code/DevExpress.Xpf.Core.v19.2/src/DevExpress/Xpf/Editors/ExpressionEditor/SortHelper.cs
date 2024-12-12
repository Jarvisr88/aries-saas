namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    internal static class SortHelper
    {
        public static object[] GetSortedItems(object[] items, ColumnSortOrder sortOrder)
        {
            if (sortOrder == ColumnSortOrder.None)
            {
                return items;
            }
            List<object> list = new List<object>(items);
            IComparer<object> comparer = (sortOrder == ColumnSortOrder.Ascending) ? ((IComparer<object>) Comparer<object>.Default) : ((IComparer<object>) new ReverseComparer(Comparer<object>.Default));
            list.Sort(comparer);
            return list.ToArray();
        }

        private class ReverseComparer : IComparer<object>
        {
            private readonly IComparer<object> comparer;

            public ReverseComparer(IComparer<object> comparer)
            {
                this.comparer = comparer;
            }

            int IComparer<object>.Compare(object x, object y) => 
                this.comparer.Compare(y, x);
        }
    }
}

