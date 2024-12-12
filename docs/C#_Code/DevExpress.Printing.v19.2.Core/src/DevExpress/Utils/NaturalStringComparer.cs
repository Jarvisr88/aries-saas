namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class NaturalStringComparer : IComparer<string>, IDisposable
    {
        private Regex regex;
        private Dictionary<string, Info> cache;

        public NaturalStringComparer()
        {
            this.regex = new Regex(@"(\d+)");
            this.cache = new Dictionary<string, Info>();
        }

        public NaturalStringComparer(int cacheSize)
        {
            this.regex = new Regex(@"(\d+)");
            this.cache = new Dictionary<string, Info>(cacheSize);
        }

        public int Compare(string x, string y)
        {
            if (x == y)
            {
                return 0;
            }
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            Info info = this.Split(x);
            Info info2 = this.Split(y);
            int num = Math.Min(info.Length, info2.Length);
            for (int i = 0; i < num; i++)
            {
                if (!Info.Eq(info, info2, i))
                {
                    return this.CompareStringPart(info, info2, i);
                }
            }
            return IntCompare(info.Length, info2.Length);
        }

        private int CompareStringPart(Info x, Info y, int i)
        {
            if ((i % 2) != 1)
            {
                return this.StringCompare(x.strs[i], y.strs[i]);
            }
            int index = i / 2;
            return IntCompare(x.ints[index], y.ints[index]);
        }

        public void Dispose()
        {
            this.cache.Clear();
            this.cache = null;
        }

        private static int IntCompare(int x, int y) => 
            (x == y) ? 0 : ((x > y) ? 1 : -1);

        private Info Split(string x)
        {
            Info info;
            if (!this.cache.TryGetValue(x, out info))
            {
                info.strs = this.regex.Split(x);
                info.ints = new int[info.strs.Length / 2];
                int index = 1;
                int num3 = 0;
                while (true)
                {
                    int num;
                    if (index >= info.strs.Length)
                    {
                        this.cache[x] = info;
                        break;
                    }
                    int.TryParse(info.strs[index], out num);
                    info.ints[num3] = num;
                    index += 2;
                    num3++;
                }
            }
            return info;
        }

        protected virtual int StringCompare(string x, string y) => 
            string.Compare(x, y, StringComparison.CurrentCulture);

        [StructLayout(LayoutKind.Sequential)]
        private struct Info
        {
            public string[] strs;
            public int[] ints;
            public int Length =>
                this.strs.Length;
            public static bool Eq(NaturalStringComparer.Info x, NaturalStringComparer.Info y, int i) => 
                x.strs[i] == y.strs[i];
        }
    }
}

