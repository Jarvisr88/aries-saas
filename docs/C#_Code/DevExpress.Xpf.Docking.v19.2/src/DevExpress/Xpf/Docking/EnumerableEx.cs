namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class EnumerableEx
    {
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, T defaultValue)
        {
            T local2;
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (!predicate(current))
                        {
                            continue;
                        }
                        local2 = current;
                    }
                    else
                    {
                        return defaultValue;
                    }
                    break;
                }
            }
            return local2;
        }
    }
}

