namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class ArrayHelper
    {
        public static bool ArraysEqual<T>(T[] first, T[] second) => 
            ArraysEqual<T>(first, second, 0, EqualityComparer<T>.Default);

        public static bool ArraysEqual<T>(T[] first, T[] second, IEqualityComparer<T> comparer) => 
            ArraysEqual<T>(first, second, 0, comparer);

        public static bool ArraysEqual<T>(T[] first, T[] second, int startIndex, IEqualityComparer<T> comparer)
        {
            if (first != second)
            {
                if ((first == null) ^ (second == null))
                {
                    return false;
                }
                if (first == null)
                {
                    return true;
                }
                if (first.Length != second.Length)
                {
                    return false;
                }
                for (int i = startIndex; i < first.Length; i++)
                {
                    if (!comparer.Equals(first[i], second[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static T[] Clone<T>(T[] source) where T: ICloneable
        {
            Func<T, T> cloneItem = <>c__14<T>.<>9__14_0;
            if (<>c__14<T>.<>9__14_0 == null)
            {
                Func<T, T> local1 = <>c__14<T>.<>9__14_0;
                cloneItem = <>c__14<T>.<>9__14_0 = delegate (T x) {
                    if (x != null)
                    {
                        return (T) x.Clone();
                    }
                    return default(T);
                };
            }
            return CloneInternal<T>(source, cloneItem);
        }

        private static T[] CloneInternal<T>(T[] source, Func<T, T> cloneItem)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (cloneItem == null)
            {
                throw new ArgumentNullException("cloneItem");
            }
            T[] localArray = new T[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                localArray[i] = cloneItem(source[i]);
            }
            return localArray;
        }

        public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter) => 
            Array.ConvertAll<TInput, TOutput>(array, converter);

        public static bool EndsWith<T>(T[] array, T[] sample) => 
            (array.Length >= sample.Length) && MatchBack<T>(array, sample, sample.Length);

        public static bool EndsWith<T>(T[] array, T[] sample, int count) => 
            (array.Length >= sample.Length) && MatchBack<T>(array, sample, count);

        public static T[] Filter<T>(T[] items, Predicate<T> match)
        {
            List<T> list = new List<T>(items.Length);
            foreach (T local in items)
            {
                if (match(local))
                {
                    list.Add(local);
                }
            }
            return list.ToArray();
        }

        public static T Find<T>(T[] array, Predicate<T> match) => 
            Array.Find<T>(array, match);

        public static int FindSubset<T>(T[] array, T[] sample)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (Match<T>(array, sample, i))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void ForEach<T>(IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T local in enumerable)
            {
                action(local);
            }
        }

        public static T[] InsertItem<T>(T[] items, T item, int index)
        {
            T[] localArray = new T[items.Length + 1];
            localArray[index] = item;
            for (int i = 0; i < items.Length; i++)
            {
                localArray[(i < index) ? i : (i + 1)] = items[i];
            }
            return localArray;
        }

        private static bool Match<T>(T[] array, T[] sample, int index)
        {
            int num = 0;
            while ((num < sample.Length) && ((index + num) < array.Length))
            {
                if (!Equals(sample[num], array[index + num]))
                {
                    return false;
                }
                num++;
            }
            return (num == sample.Length);
        }

        private static bool MatchBack<T>(T[] array, T[] sample, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                if (!Equals(array[array.Length - i], sample[sample.Length - i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool StartsWith<T>(T[] array, T[] sample) => 
            Match<T>(array, sample, 0);

        [Serializable, CompilerGenerated]
        private sealed class <>c__14<T> where T: ICloneable
        {
            public static readonly ArrayHelper.<>c__14<T> <>9;
            public static Func<T, T> <>9__14_0;

            static <>c__14()
            {
                ArrayHelper.<>c__14<T>.<>9 = new ArrayHelper.<>c__14<T>();
            }

            internal T <Clone>b__14_0(T x)
            {
                if (x != null)
                {
                    return (T) x.Clone();
                }
                return default(T);
            }
        }
    }
}

