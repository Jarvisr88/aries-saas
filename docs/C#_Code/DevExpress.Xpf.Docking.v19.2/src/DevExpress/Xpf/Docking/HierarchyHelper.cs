namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class HierarchyHelper
    {
        public static T FindAmongParents<T>(this T item, Func<T, bool> condition, Func<T, T> parentEvaluator) where T: class
        {
            while ((item != null) && !condition(item))
            {
                item = parentEvaluator(item);
            }
            return item;
        }

        public static int FindAncestorIndex<T>(this T item, T ancestor, Func<T, T> parentEvaluator) where T: class
        {
            int num = 0;
            while (item != ancestor)
            {
                num++;
                item = parentEvaluator(item);
                if (item == null)
                {
                    return -1;
                }
            }
            return num;
        }

        public static T FindCommonAncestor<T>(this T obj1, T obj2, Func<T, T> parentEvaluator) where T: class
        {
            if ((obj1 != null) && (obj2 != null))
            {
                HashSet<T> set = new HashSet<T>();
                while (obj1 != null)
                {
                    set.Add(obj1);
                    obj1 = parentEvaluator(obj1);
                }
                while (obj2 != null)
                {
                    if (set.Contains(obj2))
                    {
                        return obj2;
                    }
                    obj2 = parentEvaluator(obj2);
                }
            }
            return default(T);
        }
    }
}

