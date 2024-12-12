namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using System;

    public static class ThemePaletteHelper
    {
        public static Theme GetBaseTheme(Theme theme) => 
            theme.BaseTheme;

        public static string GetPaletteName(Theme theme) => 
            theme.PaletteName;
    }
}

