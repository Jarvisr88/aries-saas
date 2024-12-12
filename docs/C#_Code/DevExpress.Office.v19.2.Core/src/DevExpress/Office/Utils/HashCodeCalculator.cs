namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections;

    public static class HashCodeCalculator
    {
        public static int CalcHashCode32(IEnumerable enumerable)
        {
            CombinedHashCode code = new CombinedHashCode(0x1505L);
            IEnumerator enumerator = enumerable.GetEnumerator();
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    object current = enumerator.Current;
                    if (current != null)
                    {
                        code.AddObject(current);
                    }
                }
            }
            return code.CombinedHash32;
        }

        public static int CalcHashCode32(int h1, int h2) => 
            ((h1 << 5) + h1) ^ h2;

        public static int CalcHashCode32(int h1, int h2, int h3) => 
            CalcHashCode32(CalcHashCode32(h1, h2), h3);

        public static int CalcHashCode32(int h1, int h2, int h3, int h4) => 
            CalcHashCode32(CalcHashCode32(h1, h2), CalcHashCode32(h3, h4));

        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4), h5);

        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5, int h6) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4, h5), h6);

        public static int CalcHashCode32(int h1, int h2, int h3, int h4, int h5, int h6, int h7) => 
            CalcHashCode32(CalcHashCode32(h1, h2, h3, h4, h5, h6), h7);
    }
}

