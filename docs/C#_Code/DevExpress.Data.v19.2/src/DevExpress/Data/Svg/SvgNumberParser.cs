namespace DevExpress.Data.Svg
{
    using System;
    using System.Globalization;

    public static class SvgNumberParser
    {
        private static readonly System.Globalization.NumberStyles NumberStyles;
        public static readonly IFormatProvider Culture;

        static SvgNumberParser();
        public static double ParseDouble(string value);
        public static double ParseDouble(string value, IFormatProvider provider);
        public static float ParseFloat(string value);
        public static float ParseFloat(string value, IFormatProvider provider);
    }
}

