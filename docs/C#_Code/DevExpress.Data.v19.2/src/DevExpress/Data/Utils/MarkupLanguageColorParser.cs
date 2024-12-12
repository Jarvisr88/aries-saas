namespace DevExpress.Data.Utils
{
    using System;
    using System.Drawing;

    public static class MarkupLanguageColorParser
    {
        private static int GetColor(string colorName, int startIndex);
        private static Color GetColorByArgb(string colorName);
        private static Color GetColorByName(string value);
        private static Color GetColorByRgb(string colorName);
        public static Color ParseColor(string value);
        private static Color ParseRGB(string value);
    }
}

