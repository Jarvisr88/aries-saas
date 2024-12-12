namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class ScaleQualifier : IUriQualifier, IBaseUriQualifier
    {
        private const string nameValue = "scale";

        public event EventHandler ActiveValueChanged;

        public int GetAltitude(DependencyObject context, string value, IEnumerable<string> values, out int maxAltitude);
        private static int GetClosestValue(IEnumerable<int> integerValues, int checkValue);
        private static int GetIndex(IEnumerable<int> integerValues, int checkValue);
        public bool IsValidValue(string value);

        public string Name { get; }

        public string DefaultValue { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScaleQualifier.<>c <>9;
            public static Func<int, int> <>9__8_0;
            public static Func<Tuple<int, int>, int> <>9__10_1;
            public static Func<Tuple<int, int>, int> <>9__10_2;
            public static Func<int> <>9__10_3;

            static <>c();
            internal int <GetAltitude>b__8_0(int x);
            internal int <GetClosestValue>b__10_1(Tuple<int, int> x);
            internal int <GetClosestValue>b__10_2(Tuple<int, int> x);
            internal int <GetClosestValue>b__10_3();
        }
    }
}

