namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;

    public static class PredefinedThemePalettes
    {
        public static readonly PredefinedThemePalette CobaltBlue = new PredefinedThemePalette("CobaltBlue");
        public static readonly PredefinedThemePalette SpruceLeaves = new PredefinedThemePalette("SpruceLeaves");
        public static readonly PredefinedThemePalette Brickwork = new PredefinedThemePalette("Brickwork");
        public static readonly PredefinedThemePalette RedWine = new PredefinedThemePalette("RedWine");
        public static readonly PredefinedThemePalette Turquoise = new PredefinedThemePalette("Turquoise");
        public static readonly PredefinedThemePalette DarkLilac = new PredefinedThemePalette("DarkLilac");
        private static readonly List<PredefinedThemePalette> predefinedThemePalettes = new List<PredefinedThemePalette>();

        internal static void RegisterPredefinedThemePalette(PredefinedThemePalette palette)
        {
            predefinedThemePalettes.Add(palette);
        }

        internal static IEnumerable<PredefinedThemePalette> StandardPalettes =>
            predefinedThemePalettes.AsReadOnly();
    }
}

