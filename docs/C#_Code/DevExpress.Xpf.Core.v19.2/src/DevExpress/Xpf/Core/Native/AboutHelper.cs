namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.About;
    using DevExpress.Xpf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class AboutHelper
    {
        public static void ShowAbout(IEnumerable<ProductKind> productKind, string productName, Window owner);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AboutHelper.<>c <>9;
            public static Func<ProductKind, AboutInfo> <>9__0_0;
            public static Func<LicenseState, int> <>9__0_1;
            public static Func<string, bool> <>9__0_2;

            static <>c();
            internal AboutInfo <ShowAbout>b__0_0(ProductKind kind);
            internal int <ShowAbout>b__0_1(LicenseState ls);
            internal bool <ShowAbout>b__0_2(string s);
        }
    }
}

