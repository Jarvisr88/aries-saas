namespace DevExpress.Office.Utils
{
    using System;

    public static class OfficeAlgorithms
    {
        public static U BinarySearch<T, U>(List<T, U> list, IComparable<T> predicate) where U: struct, IConvertToInt<U>
        {
            U local = default(U);
            int num = 0;
            int num2 = list.Count - 1;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = predicate.CompareTo(list.InnerList[num3]);
                if (num4 == 0)
                {
                    return local.FromInt(num3);
                }
                if (num4 < 0)
                {
                    num = num3 + 1;
                    continue;
                }
                num2 = num3 - 1;
            }
            return local.FromInt(~num);
        }
    }
}

