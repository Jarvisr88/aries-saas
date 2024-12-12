namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ThemeProperties
    {
        private const string StandardCategory = "Standard";
        private const string MetropolisCategory = "Metropolis";
        private const string MetropolisTouchCategory = "Metropolis Touch";
        private const string Office2007Category = "Office 2007";
        private const string Office2010Category = "Office 2010";
        private const string Office2013Category = "Office 2013";
        private const string Office2013TouchCategory = "Office 2013 Touch";
        private const string Office2016Category = "Office 2016";
        private const string Office2016TouchCategory = "Office 2016 Touch";
        private const string Office2019Category = "Office 2019";
        private const string Office2019TouchCategory = "Office 2019 Touch";
        private const string VisualStudioCategory = "Visual Studio";
        private const string LegacyCategory = "Legacy";
        private const string LegacyTouchCategory = "Legacy Touch";
        private const string BaseCategory = "Base";
        private static readonly string[] categories;
        private static readonly Dictionary<string, string> themes;

        static ThemeProperties();
        public static string Category(string themeName);
        public static int CategoryPriority(string category);
        public static string Combine(string themeName, bool touchUI);
        public static string DisplayName(Theme theme);
        public static string GetFromAlias(string themeName, bool allowTouchline);
        public static IEnumerable<Theme> GetThemesCollection(bool showTouchThemes, bool useLegacyCategories = true, bool showPaletteThemes = false);
        public static bool IsLegacy(string category);
        public static bool IsTouch(string themeName);
        public static bool IsTouchlineTheme(string themeName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeProperties.<>c <>9;
            public static Func<string, bool> <>9__0_0;
            public static Func<string, int, <>f__AnonymousType10<int, string>> <>9__24_0;
            public static Func<<>f__AnonymousType10<int, string>, int> <>9__24_2;
            public static Func<IEnumerable<Theme>, IEnumerable<Theme>> <>9__25_0;
            public static Func<Theme, string> <>9__25_2;
            public static Func<IGrouping<string, Theme>, int> <>9__25_3;
            public static Func<Theme, string> <>9__25_5;
            public static Func<IGrouping<string, Theme>, IEnumerable<Theme>> <>9__25_4;
            public static Func<IEnumerable<Theme>, IEnumerable<Theme>> <>9__25_1;

            static <>c();
            internal <>f__AnonymousType10<int, string> <CategoryPriority>b__24_0(string x, int i);
            internal int <CategoryPriority>b__24_2(<>f__AnonymousType10<int, string> x);
            internal IEnumerable<Theme> <GetThemesCollection>b__25_0(IEnumerable<Theme> x);
            internal IEnumerable<Theme> <GetThemesCollection>b__25_1(IEnumerable<Theme> c);
            internal string <GetThemesCollection>b__25_2(Theme t);
            internal int <GetThemesCollection>b__25_3(IGrouping<string, Theme> t);
            internal IEnumerable<Theme> <GetThemesCollection>b__25_4(IGrouping<string, Theme> g);
            internal string <GetThemesCollection>b__25_5(Theme x);
            internal bool <IsTouch>b__0_0(string token);
        }
    }
}

