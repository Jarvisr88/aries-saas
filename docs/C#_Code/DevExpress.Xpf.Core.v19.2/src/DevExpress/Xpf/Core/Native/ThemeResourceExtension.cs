namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.DXBinding;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public sealed class ThemeResourceExtension : DXMarkupExtensionBase
    {
        [ThreadStatic]
        private static ThemeResourceConverter resourceConverter;
        private MultiBinding resourceBinding;

        static ThemeResourceExtension();
        public ThemeResourceExtension();
        public ThemeResourceExtension(ThemeKeyExtensionGeneric themeKey);
        private MultiBinding CreateBinding();
        protected override object ProvideValueCore();

        private static ThemeResourceConverter ResourceConverter { get; }

        private MultiBinding ResourceBinding { get; }

        public ThemeKeyExtensionGeneric ThemeKey { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeResourceExtension.<>c <>9;
            public static Func<IProvideValueTarget, object> <>9__12_0;
            public static Func<IAttachableObject, IAttachableObject> <>9__12_1;
            public static Func<IAttachableObject, bool> <>9__12_2;
            public static Func<int, string> <>9__12_3;

            static <>c();
            internal object <CreateBinding>b__12_0(IProvideValueTarget x);
            internal IAttachableObject <CreateBinding>b__12_1(IAttachableObject x);
            internal bool <CreateBinding>b__12_2(IAttachableObject x);
            internal string <CreateBinding>b__12_3(int _);
        }
    }
}

