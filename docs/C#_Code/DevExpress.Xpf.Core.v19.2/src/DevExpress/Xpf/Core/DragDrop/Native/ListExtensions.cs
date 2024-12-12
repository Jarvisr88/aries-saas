namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ListExtensions
    {
        public static void InsertMultiple(this IList list, IEnumerable<object> objects, int index)
        {
            foreach (object obj2 in objects.Reverse<object>())
            {
                list.Insert(index, obj2);
            }
        }

        public static void RemoveMultiple(this IList list, int[] listIndexes)
        {
            Func<int, int> keySelector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<int, int> local1 = <>c.<>9__0_0;
                keySelector = <>c.<>9__0_0 = x => x;
            }
            foreach (int num in listIndexes.OrderByDescending<int, int>(keySelector))
            {
                list.RemoveAt(num);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListExtensions.<>c <>9 = new ListExtensions.<>c();
            public static Func<int, int> <>9__0_0;

            internal int <RemoveMultiple>b__0_0(int x) => 
                x;
        }
    }
}

