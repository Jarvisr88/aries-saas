namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Media;

    public static class ThemePaletteSvgPaletteProvider
    {
        private static readonly Dictionary<string, WpfSvgPalette> PalettesCache = new Dictionary<string, WpfSvgPalette>();
        internal static readonly Dictionary<string, Color> PaletteKeyAccentColors;
        internal static readonly Dictionary<string, Color> SelectionColors;
        internal static readonly Dictionary<string, Color> ControlBackColors;
        internal static readonly Dictionary<string, Color> WindowBackColors;
        private static readonly string BlackColor = "BlackColor";
        private static readonly string RedColor = "RedColor";
        private static readonly string GreenColor = "GreenColor";
        private static readonly string BlueColor = "BlueColor";
        private static readonly Regex ThemeNameRegex = new Regex("(?<name>([A-Z]|[a-z])+[0-9]+)(?<tone>[A-Z][a-z]+)", RegexOptions.Compiled);

        static ThemePaletteSvgPaletteProvider()
        {
            Dictionary<string, Color> dictionary1 = new Dictionary<string, Color>();
            dictionary1.Add(PredefinedThemePalettes.CobaltBlue.Name, (Color) ColorConverter.ConvertFromString("#FF2B579A"));
            dictionary1.Add(PredefinedThemePalettes.SpruceLeaves.Name, (Color) ColorConverter.ConvertFromString("#FF227447"));
            dictionary1.Add(PredefinedThemePalettes.Brickwork.Name, (Color) ColorConverter.ConvertFromString("#FFB7472A"));
            dictionary1.Add(PredefinedThemePalettes.RedWine.Name, (Color) ColorConverter.ConvertFromString("#FFA4373A"));
            dictionary1.Add(PredefinedThemePalettes.Turquoise.Name, (Color) ColorConverter.ConvertFromString("#FF077568"));
            dictionary1.Add(PredefinedThemePalettes.DarkLilac.Name, (Color) ColorConverter.ConvertFromString("#FF80397B"));
            PaletteKeyAccentColors = dictionary1;
            Dictionary<string, Color> dictionary2 = new Dictionary<string, Color>();
            dictionary2.Add("Colorful", (Color) ColorConverter.ConvertFromString("#FFAEAEAE"));
            dictionary2.Add("Black", (Color) ColorConverter.ConvertFromString("#FF6A6A6A"));
            dictionary2.Add("Dark", (Color) ColorConverter.ConvertFromString("#FF444444"));
            SelectionColors = dictionary2;
            Dictionary<string, Color> dictionary3 = new Dictionary<string, Color>();
            dictionary3.Add("Colorful", (Color) ColorConverter.ConvertFromString("#FFE6E6E6"));
            dictionary3.Add("White", (Color) ColorConverter.ConvertFromString("#FFD4D4D4"));
            dictionary3.Add("Black", (Color) ColorConverter.ConvertFromString("#FF262626"));
            dictionary3.Add("Dark", (Color) ColorConverter.ConvertFromString("#FF666666"));
            dictionary3.Add("Blue", (Color) ColorConverter.ConvertFromString("#FFD6DBE9"));
            dictionary3.Add("Light", (Color) ColorConverter.ConvertFromString("#FFEEEEF2"));
            dictionary3.Add("VS2017Dark", (Color) ColorConverter.ConvertFromString("#FF2D2D30"));
            WindowBackColors = dictionary3;
            Dictionary<string, Color> dictionary4 = new Dictionary<string, Color>();
            dictionary4.Add("Colorful", (Color) ColorConverter.ConvertFromString("#FFF0F0F0"));
            dictionary4.Add("White", (Color) ColorConverter.ConvertFromString("#FFFFFFFF"));
            dictionary4.Add("Black", (Color) ColorConverter.ConvertFromString("#FF2D2D2D"));
            dictionary4.Add("Dark", (Color) ColorConverter.ConvertFromString("#FF595959"));
            dictionary4.Add("Blue", (Color) ColorConverter.ConvertFromString("#FFCFD6E5"));
            dictionary4.Add("Light", (Color) ColorConverter.ConvertFromString("#FFF5F5F7"));
            dictionary4.Add("VS2017Dark", (Color) ColorConverter.ConvertFromString("#FF3E3E40"));
            ControlBackColors = dictionary4;
            PalettesCache.Add("Office2016ColorfulSE", GetSvgPaletteFromColors("#FF0173C7", "#FFAEAEAE", "#FFE6E6E6", "#FFF0F0F0"));
            PalettesCache.Add("Office2016WhiteSE", GetSvgPaletteFromColors("#FF0072C6", "#FF92C0E0", "#FFD4D4D4", "#FFFFFFFF"));
            PalettesCache.Add("Office2016BlackSE", GetSvgPaletteFromColors("#FF0A0A0A", "#FF6A6A6A", "#FF262626", "#FF2D2D2D"));
            PalettesCache.Add("Office2016DarkGraySE", GetSvgPaletteFromColors("#FF303030", "#FF444444", "#FF666666", "#FF595959"));
            PalettesCache.Add("Office2019Colorful", GetSvgPaletteFromColors("#FF0173C7", "#FFCDE6F7", "#FFE6E6E6", "#FFF0F0F0"));
            PalettesCache.Add("Office2019White", GetSvgPaletteFromColors("#FF0072C6", "#FF92C0E0", "#FFD4D4D4", "#FFFFFFFF"));
            PalettesCache.Add("Office2019Black", GetSvgPaletteFromColors("#FF0A0A0A", "#FF6A6A6A", "#FF262626", "#FF2D2D2D"));
            PalettesCache.Add("Office2019DarkGray", GetSvgPaletteFromColors("#FF303030", "#FF444444", "#FF666666", "#FF595959"));
            PalettesCache.Add("VS2017Blue", GetSvgPaletteFromColors("#FF35496A", "#FFFFF29D", "#FFD6DBE9", "#FFCFD6E5"));
            PalettesCache.Add("VS2017Dark", GetSvgPaletteFromColors("#FF007ACC", "#FF118EDA", "#FF2D2D30", "#FF3E3E40"));
            PalettesCache.Add("VS2017Light", GetSvgPaletteFromColors("#FF007ACC", "#FF118EDA", "#FFEEEEF2", "#FFF5F5F7"));
        }

        public static void AddSvgPalette(string themeName, string baseThemeName, ThemePaletteBase palette)
        {
            WpfSvgPalette palette2 = GetSvgPalette(themeName, baseThemeName, palette);
            if (!PalettesCache.ContainsKey(themeName))
            {
                PalettesCache.Add(themeName, palette2);
            }
            else
            {
                PalettesCache[themeName] = palette2;
            }
        }

        private static Color CalcSelectionColor(Color accentColor)
        {
            double num = 0.35294117647058826;
            return Color.FromArgb(0xff, (byte) ((accentColor.R * num) + 165.0), (byte) ((accentColor.G * num) + 165.0), (byte) ((accentColor.B * num) + 165.0));
        }

        public static WpfSvgPalette GetPalette(string themeName) => 
            PalettesCache.ContainsKey(themeName) ? PalettesCache[themeName] : null;

        internal static WpfSvgPalette GetSvgPalette(string themeName, string baseThemeName, ThemePaletteBase palette) => 
            (palette is PredefinedThemePalette) ? GetSvgPaletteForPredefinedPalette(palette.Name, baseThemeName, null) : GetSvgPaletteForCustomTheme(themeName, baseThemeName, palette);

        private static WpfSvgPalette GetSvgPaletteForCustomTheme(string themeName, string baseThemeName, ThemePaletteBase palette)
        {
            Color color;
            Color color2;
            Color color3;
            Color color4;
            WpfSvgPalette palette2 = new WpfSvgPalette();
            Dictionary<string, Color> dictionary = palette.GetColors(null, themeName, baseThemeName);
            if (dictionary.TryGetValue("Backstage.Window.Background", out color))
            {
                palette2[BlackColor] = new SolidColorBrush(color);
            }
            if (dictionary.TryGetValue("SelectionBackground", out color2))
            {
                palette2[RedColor] = new SolidColorBrush(color2);
            }
            if (dictionary.TryGetValue("Border", out color3))
            {
                palette2[GreenColor] = new SolidColorBrush(color3);
            }
            if (dictionary.TryGetValue("Window.Background", out color4))
            {
                palette2[BlueColor] = new SolidColorBrush(color4);
            }
            ThemePalette palette3 = (ThemePalette) palette;
            return ((palette3.BasePalette == null) ? palette2 : GetSvgPaletteForPredefinedPalette(palette3.BasePalette.Name, baseThemeName, palette2));
        }

        internal static WpfSvgPalette GetSvgPaletteForPredefinedPalette(string paletteName, string baseThemeName, WpfSvgPalette palette = null)
        {
            Color color;
            Color color2;
            Color color3;
            Color color4;
            if (!PaletteKeyAccentColors.TryGetValue(paletteName, out color))
            {
                return null;
            }
            Match match = ThemeNameRegex.Match(baseThemeName);
            if (!match.Success)
            {
                return null;
            }
            string str = match.Groups["name"].Value;
            string key = match.Groups["tone"].Value;
            if (baseThemeName == "VS2017Dark")
            {
                key = "VS2017Dark";
            }
            if (!key.StartsWith("White") && (!str.StartsWith("VS") && (!key.StartsWith("Colorful") || !str.StartsWith("Office2019"))))
            {
                SelectionColors.TryGetValue(key, out color2);
            }
            else
            {
                color2 = CalcSelectionColor(color);
            }
            ControlBackColors.TryGetValue(key, out color3);
            WindowBackColors.TryGetValue(key, out color4);
            WpfSvgPalette palette2 = palette ?? new WpfSvgPalette();
            if (!palette2.ContainsKey(BlackColor))
            {
                palette2[BlackColor] = new SolidColorBrush(color);
            }
            if (!palette2.ContainsKey(RedColor))
            {
                palette2[RedColor] = new SolidColorBrush(color2);
            }
            if (!palette2.ContainsKey(GreenColor))
            {
                palette2[GreenColor] = new SolidColorBrush(color4);
            }
            if (!palette2.ContainsKey(BlueColor))
            {
                palette2[BlueColor] = new SolidColorBrush(color3);
            }
            return palette2;
        }

        private static WpfSvgPalette GetSvgPaletteFromColors(string black, string red, string green, string blue)
        {
            WpfSvgPalette palette1 = new WpfSvgPalette();
            palette1.Add(BlackColor, new SolidColorBrush((Color) ColorConverter.ConvertFromString(black)));
            palette1.Add(RedColor, new SolidColorBrush((Color) ColorConverter.ConvertFromString(red)));
            palette1.Add(GreenColor, new SolidColorBrush((Color) ColorConverter.ConvertFromString(green)));
            palette1.Add(BlueColor, new SolidColorBrush((Color) ColorConverter.ConvertFromString(blue)));
            return palette1;
        }
    }
}

