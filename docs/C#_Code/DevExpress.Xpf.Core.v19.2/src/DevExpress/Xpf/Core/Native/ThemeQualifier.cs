namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class ThemeQualifier : IBindableUriQualifier, IBaseUriQualifier
    {
        private const string nameValue = "theme";
        private List<Tuple<string, int, Theme>> themes;
        private int cachedAltitude;

        public int GetAltitude(DependencyObject context, string value, IEnumerable<string> values, out int maxAltitude);
        public Binding GetBinding(RelativeSource source);
        public Binding GetBinding(DependencyObject source);
        private string[] GetThemeValues(Theme theme);
        private void Initialize(List<Tuple<string, int, Theme>> x);
        public bool IsValidValue(string value);

        private List<Tuple<string, int, Theme>> Themes { get; }

        public string Name { get; }

        public string DefaultValue { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeQualifier.<>c <>9;
            public static Func<Tuple<string, int, Theme>, int> <>9__11_0;
            public static Func<Tuple<string, int, Theme>, string> <>9__12_0;
            public static Func<string, bool> <>9__14_0;

            static <>c();
            internal int <GetAltitude>b__11_0(Tuple<string, int, Theme> x);
            internal bool <GetThemeValues>b__14_0(string x);
            internal string <IsValidValue>b__12_0(Tuple<string, int, Theme> x);
        }
    }
}

