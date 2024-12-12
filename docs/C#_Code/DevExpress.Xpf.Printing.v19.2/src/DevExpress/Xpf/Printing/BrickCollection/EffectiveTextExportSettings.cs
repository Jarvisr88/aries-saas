namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal class EffectiveTextExportSettings : EffectiveExportSettings, ITextExportSettings, IExportSettings
    {
        public EffectiveTextExportSettings(DependencyObject source) : base(source)
        {
        }

        private T GetEffectiveTextExportValue<T>(DependencyProperty property, Func<ITextExportSettings, T> getValue) => 
            base.GetEffectiveValue<T, ITextExportSettings>(property, getValue);

        public System.Windows.Media.FontFamily FontFamily
        {
            get
            {
                Func<ITextExportSettings, System.Windows.Media.FontFamily> getValue = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.Media.FontFamily> local1 = <>c.<>9__2_0;
                    getValue = <>c.<>9__2_0 = o => o.FontFamily;
                }
                return this.GetEffectiveTextExportValue<System.Windows.Media.FontFamily>(TextExportSettings.FontFamilyProperty, getValue);
            }
        }

        public double FontSize
        {
            get
            {
                Func<ITextExportSettings, double> getValue = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<ITextExportSettings, double> local1 = <>c.<>9__4_0;
                    getValue = <>c.<>9__4_0 = o => o.FontSize;
                }
                return this.GetEffectiveTextExportValue<double>(TextExportSettings.FontSizeProperty, getValue);
            }
        }

        public System.Windows.FontStyle FontStyle
        {
            get
            {
                Func<ITextExportSettings, System.Windows.FontStyle> getValue = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.FontStyle> local1 = <>c.<>9__6_0;
                    getValue = <>c.<>9__6_0 = o => o.FontStyle;
                }
                return this.GetEffectiveTextExportValue<System.Windows.FontStyle>(TextExportSettings.FontStyleProperty, getValue);
            }
        }

        public System.Windows.FontWeight FontWeight
        {
            get
            {
                Func<ITextExportSettings, System.Windows.FontWeight> getValue = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.FontWeight> local1 = <>c.<>9__8_0;
                    getValue = <>c.<>9__8_0 = o => o.FontWeight;
                }
                return this.GetEffectiveTextExportValue<System.Windows.FontWeight>(TextExportSettings.FontWeightProperty, getValue);
            }
        }

        public System.Windows.HorizontalAlignment HorizontalAlignment
        {
            get
            {
                Func<ITextExportSettings, System.Windows.HorizontalAlignment> getValue = <>c.<>9__10_0;
                if (<>c.<>9__10_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.HorizontalAlignment> local1 = <>c.<>9__10_0;
                    getValue = <>c.<>9__10_0 = o => o.HorizontalAlignment;
                }
                return this.GetEffectiveTextExportValue<System.Windows.HorizontalAlignment>(TextExportSettings.HorizontalAlignmentProperty, getValue);
            }
        }

        public bool NoTextExport
        {
            get
            {
                Func<ITextExportSettings, bool> getValue = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<ITextExportSettings, bool> local1 = <>c.<>9__12_0;
                    getValue = <>c.<>9__12_0 = o => o.NoTextExport;
                }
                return this.GetEffectiveTextExportValue<bool>(TextExportSettings.NoTextExportProperty, getValue);
            }
        }

        public Thickness Padding
        {
            get
            {
                Func<ITextExportSettings, Thickness> getValue = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<ITextExportSettings, Thickness> local1 = <>c.<>9__14_0;
                    getValue = <>c.<>9__14_0 = o => o.Padding;
                }
                return this.GetEffectiveTextExportValue<Thickness>(TextExportSettings.PaddingProperty, getValue);
            }
        }

        public string Text
        {
            get
            {
                Func<ITextExportSettings, string> getValue = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<ITextExportSettings, string> local1 = <>c.<>9__16_0;
                    getValue = <>c.<>9__16_0 = o => o.Text;
                }
                return this.GetEffectiveTextExportValue<string>(TextExportSettings.TextProperty, getValue);
            }
        }

        public object TextValue
        {
            get
            {
                Func<ITextExportSettings, object> getValue = <>c.<>9__18_0;
                if (<>c.<>9__18_0 == null)
                {
                    Func<ITextExportSettings, object> local1 = <>c.<>9__18_0;
                    getValue = <>c.<>9__18_0 = o => o.TextValue;
                }
                return this.GetEffectiveTextExportValue<object>(TextExportSettings.TextValueProperty, getValue);
            }
        }

        public string TextValueFormatString
        {
            get
            {
                Func<ITextExportSettings, string> getValue = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Func<ITextExportSettings, string> local1 = <>c.<>9__20_0;
                    getValue = <>c.<>9__20_0 = o => o.TextValueFormatString;
                }
                return this.GetEffectiveTextExportValue<string>(TextExportSettings.TextValueFormatStringProperty, getValue);
            }
        }

        public System.Windows.TextWrapping TextWrapping
        {
            get
            {
                Func<ITextExportSettings, System.Windows.TextWrapping> getValue = <>c.<>9__22_0;
                if (<>c.<>9__22_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.TextWrapping> local1 = <>c.<>9__22_0;
                    getValue = <>c.<>9__22_0 = o => o.TextWrapping;
                }
                return this.GetEffectiveTextExportValue<System.Windows.TextWrapping>(TextExportSettings.TextWrappingProperty, getValue);
            }
        }

        public System.Windows.VerticalAlignment VerticalAlignment
        {
            get
            {
                Func<ITextExportSettings, System.Windows.VerticalAlignment> getValue = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.VerticalAlignment> local1 = <>c.<>9__24_0;
                    getValue = <>c.<>9__24_0 = o => o.VerticalAlignment;
                }
                return this.GetEffectiveTextExportValue<System.Windows.VerticalAlignment>(TextExportSettings.VerticalAlignmentProperty, getValue);
            }
        }

        public bool? XlsExportNativeFormat
        {
            get
            {
                Func<ITextExportSettings, bool?> getValue = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<ITextExportSettings, bool?> local1 = <>c.<>9__26_0;
                    getValue = <>c.<>9__26_0 = o => o.XlsExportNativeFormat;
                }
                return this.GetEffectiveTextExportValue<bool?>(TextExportSettings.XlsExportNativeFormatProperty, getValue);
            }
        }

        public string XlsxFormatString
        {
            get
            {
                Func<ITextExportSettings, string> getValue = <>c.<>9__28_0;
                if (<>c.<>9__28_0 == null)
                {
                    Func<ITextExportSettings, string> local1 = <>c.<>9__28_0;
                    getValue = <>c.<>9__28_0 = o => o.XlsxFormatString;
                }
                return this.GetEffectiveTextExportValue<string>(TextExportSettings.XlsxFormatStringProperty, getValue);
            }
        }

        public TextDecorationCollection TextDecorations
        {
            get
            {
                Func<ITextExportSettings, TextDecorationCollection> getValue = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<ITextExportSettings, TextDecorationCollection> local1 = <>c.<>9__30_0;
                    getValue = <>c.<>9__30_0 = o => o.TextDecorations;
                }
                return this.GetEffectiveTextExportValue<TextDecorationCollection>(TextExportSettings.TextDecorationsProperty, getValue);
            }
        }

        public System.Windows.TextTrimming TextTrimming
        {
            get
            {
                Func<ITextExportSettings, System.Windows.TextTrimming> getValue = <>c.<>9__32_0;
                if (<>c.<>9__32_0 == null)
                {
                    Func<ITextExportSettings, System.Windows.TextTrimming> local1 = <>c.<>9__32_0;
                    getValue = <>c.<>9__32_0 = o => o.TextTrimming;
                }
                return this.GetEffectiveTextExportValue<System.Windows.TextTrimming>(TextExportSettings.TextTrimmingProperty, getValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EffectiveTextExportSettings.<>c <>9 = new EffectiveTextExportSettings.<>c();
            public static Func<ITextExportSettings, FontFamily> <>9__2_0;
            public static Func<ITextExportSettings, double> <>9__4_0;
            public static Func<ITextExportSettings, FontStyle> <>9__6_0;
            public static Func<ITextExportSettings, FontWeight> <>9__8_0;
            public static Func<ITextExportSettings, HorizontalAlignment> <>9__10_0;
            public static Func<ITextExportSettings, bool> <>9__12_0;
            public static Func<ITextExportSettings, Thickness> <>9__14_0;
            public static Func<ITextExportSettings, string> <>9__16_0;
            public static Func<ITextExportSettings, object> <>9__18_0;
            public static Func<ITextExportSettings, string> <>9__20_0;
            public static Func<ITextExportSettings, TextWrapping> <>9__22_0;
            public static Func<ITextExportSettings, VerticalAlignment> <>9__24_0;
            public static Func<ITextExportSettings, bool?> <>9__26_0;
            public static Func<ITextExportSettings, string> <>9__28_0;
            public static Func<ITextExportSettings, TextDecorationCollection> <>9__30_0;
            public static Func<ITextExportSettings, TextTrimming> <>9__32_0;

            internal FontFamily <get_FontFamily>b__2_0(ITextExportSettings o) => 
                o.FontFamily;

            internal double <get_FontSize>b__4_0(ITextExportSettings o) => 
                o.FontSize;

            internal FontStyle <get_FontStyle>b__6_0(ITextExportSettings o) => 
                o.FontStyle;

            internal FontWeight <get_FontWeight>b__8_0(ITextExportSettings o) => 
                o.FontWeight;

            internal HorizontalAlignment <get_HorizontalAlignment>b__10_0(ITextExportSettings o) => 
                o.HorizontalAlignment;

            internal bool <get_NoTextExport>b__12_0(ITextExportSettings o) => 
                o.NoTextExport;

            internal Thickness <get_Padding>b__14_0(ITextExportSettings o) => 
                o.Padding;

            internal string <get_Text>b__16_0(ITextExportSettings o) => 
                o.Text;

            internal TextDecorationCollection <get_TextDecorations>b__30_0(ITextExportSettings o) => 
                o.TextDecorations;

            internal TextTrimming <get_TextTrimming>b__32_0(ITextExportSettings o) => 
                o.TextTrimming;

            internal object <get_TextValue>b__18_0(ITextExportSettings o) => 
                o.TextValue;

            internal string <get_TextValueFormatString>b__20_0(ITextExportSettings o) => 
                o.TextValueFormatString;

            internal TextWrapping <get_TextWrapping>b__22_0(ITextExportSettings o) => 
                o.TextWrapping;

            internal VerticalAlignment <get_VerticalAlignment>b__24_0(ITextExportSettings o) => 
                o.VerticalAlignment;

            internal bool? <get_XlsExportNativeFormat>b__26_0(ITextExportSettings o) => 
                o.XlsExportNativeFormat;

            internal string <get_XlsxFormatString>b__28_0(ITextExportSettings o) => 
                o.XlsxFormatString;
        }
    }
}

