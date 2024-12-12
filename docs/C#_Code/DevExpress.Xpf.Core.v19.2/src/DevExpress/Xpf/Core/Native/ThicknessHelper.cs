namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ThicknessHelper
    {
        internal const char NumericListSeparator = ',';

        public static Thickness ChangeValue(this Thickness thickness, Side side, double value);
        public static double GetValue(this Thickness thickness, Side side);
        public static void Inc(ref Thickness value, Thickness by);
        public static Thickness Multiply(this Thickness thickness, double value);
        public static Thickness Parse(string s);
        public static void SetValue(ref Thickness thickness, Side side, double value);
        public static string ToString(Thickness thickness);
    }
}

