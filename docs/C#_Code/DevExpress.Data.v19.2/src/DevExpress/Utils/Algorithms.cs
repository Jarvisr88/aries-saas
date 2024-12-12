namespace DevExpress.Utils
{
    using DevExpress.Utils.Implementation;
    using System;
    using System.Collections.Generic;

    public static class Algorithms
    {
        public static int BinarySearch<T>(IVector<T> list, IComparable<T> predicate) => 
            BinarySearch<T>(list, predicate, 0, list.Count - 1);

        public static int BinarySearch<T>(IList<T> list, IComparable<T> predicate) => 
            BinarySearch<T>(list, predicate, 0, list.Count - 1);

        public static int BinarySearch(int count, Func<int, int> compareWithItemAtIndex) => 
            BinarySearch(compareWithItemAtIndex, 0, count - 1);

        public static int BinarySearch<T>(IList<T> list, T value, IComparer<T> comparer) => 
            BinarySearch<T>(list, 0, list.Count, value, comparer);

        public static int BinarySearch(Func<int, int> compareWithItemAtIndex, int startIndex, int endIndex)
        {
            int num = startIndex;
            int num2 = endIndex;
            while (num <= num2)
            {
                int arg = num + ((num2 - num) >> 1);
                int num4 = compareWithItemAtIndex(arg);
                if (num4 == 0)
                {
                    return arg;
                }
                if (num4 > 0)
                {
                    num = arg + 1;
                    continue;
                }
                num2 = arg - 1;
            }
            return ~num;
        }

        public static int BinarySearch<T>(IVector<T> list, IComparable<T> predicate, int startIndex, int endIndex)
        {
            int num = startIndex;
            int num2 = endIndex;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = predicate.CompareTo(list[num3]);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 > 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return ~num;
        }

        public static int BinarySearch<T>(IList<T> list, IComparable<T> predicate, int startIndex, int endIndex)
        {
            int num = startIndex;
            int num2 = endIndex;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = predicate.CompareTo(list[num3]);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 > 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return ~num;
        }

        public static int BinarySearch<T>(IList<T> list, int index, int length, T value, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return BinarySearchCore<T>(list, index, length, value, comparer);
        }

        private static int BinarySearchCore<T>(IList<T> list, int index, int length, T value, IComparer<T> comparer)
        {
            int num = index;
            int num2 = (index + length) - 1;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = comparer.Compare(list[num3], value);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 < 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return ~num;
        }

        public static int BinarySearchReverseOrder<T>(IVector<T> list, IComparable<T> predicate) => 
            BinarySearchReverseOrder<T>(list, predicate, 0, list.Count - 1);

        public static int BinarySearchReverseOrder<T>(IList<T> list, IComparable<T> predicate) => 
            BinarySearchReverseOrder<T>(list, predicate, 0, list.Count - 1);

        public static int BinarySearchReverseOrder<T>(IVector<T> list, IComparable<T> predicate, int startIndex, int endIndex)
        {
            int num = startIndex;
            int num2 = endIndex;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = predicate.CompareTo(list[num3]);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 < 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return ~num;
        }

        public static int BinarySearchReverseOrder<T>(IList<T> list, IComparable<T> predicate, int startIndex, int endIndex)
        {
            int num = startIndex;
            int num2 = endIndex;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = predicate.CompareTo(list[num3]);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 < 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return ~num;
        }

        public static void InvertElementsOrder<T>(IVector<T> list)
        {
            int num = list.Count - 1;
            if (num >= 0)
            {
                int num2 = num / 2;
                for (int i = 0; i <= num2; i++)
                {
                    SwapElements<T>(list, i, num - i);
                }
            }
        }

        public static void InvertElementsOrder<T>(IList<T> list)
        {
            int num = list.Count - 1;
            if (num >= 0)
            {
                int num2 = num / 2;
                for (int i = 0; i <= num2; i++)
                {
                    SwapElements<T>(list, i, num - i);
                }
            }
        }

        public static T Max<T>(T index1, T index2) where T: IComparable<T> => 
            (index1.CompareTo(index2) >= 0) ? index1 : index2;

        public static T Min<T>(T index1, T index2) where T: IComparable<T> => 
            (index1.CompareTo(index2) >= 0) ? index2 : index1;

        public static void SwapElements<T>(IVector<T> list, int index1, int index2)
        {
            T local = list[index1];
            list[index1] = list[index2];
            list[index2] = local;
        }

        public static void SwapElements<T>(IList<T> list, int index1, int index2)
        {
            T local = list[index1];
            list[index1] = list[index2];
            list[index2] = local;
        }

        public static IList<T> TopologicalSort<T>(IList<T> sourceObjects, IComparer<T> comparer) => 
            new TopologicalSorter<T>().Sort(sourceObjects, comparer);

        public static class HashHelpers
        {
            public static readonly int[] primes = new int[] { 
                3, 7, 11, 0x11, 0x17, 0x1d, 0x25, 0x2f, 0x3b, 0x47, 0x59, 0x6b, 0x83, 0xa3, 0xc5, 0xef,
                0x125, 0x161, 0x1af, 0x209, 0x277, 0x2f9, 0x397, 0x44f, 0x52f, 0x63d, 0x78b, 0x91d, 0xaf1, 0xd2b, 0xfd1, 0x12fd,
                0x16cf, 0x1b65, 0x20e3, 0x2777, 0x2f6f, 0x38ff, 0x446f, 0x521f, 0x628d, 0x7655, 0x8e01, 0xaa6b, 0xcc89, 0xf583, 0x126a7, 0x1619b,
                0x1a857, 0x1fd3b, 0x26315, 0x2dd67, 0x3701b, 0x42023, 0x4f361, 0x5f0ed, 0x72125, 0x88e31, 0xa443b, 0xc51eb, 0xec8c1, 0x11bdbf, 0x154a3f, 0x198c4f,
                0x1ea867, 0x24ca19, 0x2c25c1, 0x34fa1b, 0x3f928f, 0x4c4987, 0x5b8b6f, 0x6dda89
            };
            private const int MaxPrimeArrayLength = 0x7feffffd;

            public static int ExpandPrime(int oldSize)
            {
                int min = 2 * oldSize;
                return (((min <= 0x7feffffd) || (0x7feffffd <= oldSize)) ? GetPrime(min) : 0x7feffffd);
            }

            public static int GetPrime(int min)
            {
                if (min < 0)
                {
                    throw new ArgumentException("min < 0");
                }
                for (int i = 0; i < primes.Length; i++)
                {
                    int num2 = primes[i];
                    if (num2 >= min)
                    {
                        return num2;
                    }
                }
                for (int j = min | 1; j < 0x7fffffff; j += 2)
                {
                    if (IsPrime(j) && (((j - 1) % 0x65) != 0))
                    {
                        return j;
                    }
                }
                return min;
            }

            public static bool IsPrime(int candidate)
            {
                if ((candidate & 1) == 0)
                {
                    return (candidate == 2);
                }
                int num = (int) Math.Sqrt((double) candidate);
                for (int i = 3; i <= num; i += 2)
                {
                    if ((candidate % i) == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}

