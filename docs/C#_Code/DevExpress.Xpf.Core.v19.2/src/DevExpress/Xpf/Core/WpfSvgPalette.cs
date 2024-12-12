namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class WpfSvgPalette : Dictionary<string, Brush>, ISupportInitialize
    {
        public static readonly DependencyProperty PaletteProperty = DependencyProperty.RegisterAttached("Palette", typeof(WpfSvgPalette), typeof(WpfSvgPalette), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        private static readonly HashSet<string> DefaultColorKeys = new HashSet<string>(DefaultColors.Values, StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, string> DefaultColors = DefaultSvgPalette.Colors.ToDictionary<KeyValuePair<string, string>, string, string>(x => x.Key, x => (x.Value + "Color"), StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, string> DefaultStyles = DefaultSvgPalette.Colors.ToDictionary<KeyValuePair<string, string>, string, string>(x => x.Value, x => x.Key, StringComparer.OrdinalIgnoreCase);
        private bool isSealed;

        public WpfSvgPalette() : base(StringComparer.OrdinalIgnoreCase)
        {
            this.States = new Dictionary<string, WpfSvgPalette>(StringComparer.OrdinalIgnoreCase);
            this.OverridesThemeColors = false;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            this.Seal();
        }

        private WpfSvgPalette GetActualState(string actualState)
        {
            WpfSvgPalette palette;
            actualState ??= "Normal";
            if (!this.States.TryGetValue(actualState, out palette))
            {
                palette = this;
            }
            return palette;
        }

        public static WpfSvgPalette GetPalette(DependencyObject element) => 
            (WpfSvgPalette) element.GetValue(PaletteProperty);

        private static string GetShortDefaultKey(string defaultKey) => 
            defaultKey.Remove(defaultKey.LastIndexOf("color", StringComparison.OrdinalIgnoreCase));

        protected internal static string GetStringColorFromStyle(string styleName, string defaultColor)
        {
            string str;
            return ((string.IsNullOrEmpty(styleName) || (defaultColor?.ToLower() == "transparent")) ? defaultColor : (DefaultStyles.TryGetValue(styleName, out str) ? str : defaultColor));
        }

        private StylesList GetStylesList(string styleName, string color)
        {
            string str;
            bool flag = DefaultColors.TryGetValue(color, out str);
            if (string.IsNullOrEmpty(styleName) && !flag)
            {
                styleName = color;
            }
            StylesList list1 = new StylesList();
            list1.StyleName = styleName;
            list1.DefaultStyleName = str;
            return list1;
        }

        public virtual bool ReplaceBrush(string styleName, string actualState, string color, out Brush result) => 
            this.TryGetBrushValue(actualState, this.GetStylesList(styleName, color), color, out result);

        public virtual bool ReplaceColor(string styleName, string actualState, string color, out Color result)
        {
            Brush brush;
            if (this.TryGetBrushValue(actualState, this.GetStylesList(styleName, color), color, out brush))
            {
                result = ((SolidColorBrush) brush).Color;
                return true;
            }
            result = Colors.Black;
            return false;
        }

        private void Seal()
        {
            if (!this.isSealed)
            {
                this.SealOverride();
                this.isSealed = true;
            }
        }

        protected virtual void SealOverride()
        {
            foreach (string str in base.Keys.ToArray<string>())
            {
                base[str] = base[str].TryGetAsFrozen<Brush>(false);
            }
            foreach (WpfSvgPalette palette in this.States.Values)
            {
                palette.Seal();
            }
        }

        public static void SetPalette(DependencyObject element, WpfSvgPalette value)
        {
            element.SetValue(PaletteProperty, value);
        }

        private bool TryGetBrushValue(string actualState, StylesList stylesList, string color, out Brush result) => 
            this.GetActualState(actualState).TryGetValueExtended(stylesList.StyleName, stylesList.DefaultStyleName, color, out result);

        private bool TryGetValueExtended(string styleKey, string defaultStyleKey, string color, out Brush result)
        {
            result = null;
            return (!this.TryGetValueFromPalette(styleKey, out result) ? (!this.TryGetValueFromPalette(defaultStyleKey, out result) ? this.TryGetValueFromPalette(color, out result) : true) : true);
        }

        private bool TryGetValueFromPalette(string key, out Brush result)
        {
            result = null;
            return (!string.IsNullOrEmpty(key) ? (!base.TryGetValue(key, out result) ? (!DefaultColorKeys.Contains(key + "color") ? (DefaultColorKeys.Contains(key) && base.TryGetValue(GetShortDefaultKey(key), out result)) : base.TryGetValue(key + "color", out result)) : true) : false);
        }

        public bool OverridesThemeColors { get; set; }

        public Dictionary<string, WpfSvgPalette> States { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WpfSvgPalette.<>c <>9 = new WpfSvgPalette.<>c();

            internal string <.cctor>b__10_0(KeyValuePair<string, string> x) => 
                x.Key;

            internal string <.cctor>b__10_1(KeyValuePair<string, string> x) => 
                x.Value + "Color";

            internal string <.cctor>b__10_2(KeyValuePair<string, string> x) => 
                x.Value;

            internal string <.cctor>b__10_3(KeyValuePair<string, string> x) => 
                x.Key;
        }
    }
}

