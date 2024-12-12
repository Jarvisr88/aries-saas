namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class EffectivePageNumberExportSettings : EffectiveTextExportSettings, IPageNumberExportSettings, ITextExportSettings, IExportSettings
    {
        public EffectivePageNumberExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectivePageNumberExportValue<T>(DependencyProperty property, Func<IPageNumberExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, IPageNumberExportSettings>(property, getValue);

        public string Format
        {
            get
            {
                Func<IPageNumberExportSettings, string> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<IPageNumberExportSettings, string> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = o => o.Format;
                }
                return this.GetEffectivePageNumberExportValue<string>(PageNumberExportSettings.FormatProperty, getValue);
            }
        }

        public PageNumberKind Kind
        {
            get
            {
                Func<IPageNumberExportSettings, PageNumberKind> getValue = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<IPageNumberExportSettings, PageNumberKind> local1 = <>c.<>9__4_0;
                    getValue = <>c.<>9__4_0 = o => o.Kind;
                }
                return this.GetEffectivePageNumberExportValue<PageNumberKind>(PageNumberExportSettings.KindProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectivePageNumberExportSettings.<>c <>9 = new EffectivePageNumberExportSettings.<>c();
            public static Func<IPageNumberExportSettings, string> <>9__2_0;
            public static Func<IPageNumberExportSettings, PageNumberKind> <>9__4_0;

            internal string <get_Format>b__2_0(IPageNumberExportSettings o) => 
                o.Format;

            internal PageNumberKind <get_Kind>b__4_0(IPageNumberExportSettings o) => 
                o.Kind;
        }
    }
}

