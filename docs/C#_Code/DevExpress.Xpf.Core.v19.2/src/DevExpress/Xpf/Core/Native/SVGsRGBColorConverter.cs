namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Text.RegularExpressions;

    internal static class SVGsRGBColorConverter
    {
        private static readonly Regex threeDigitHexExpression;
        private static readonly Regex sixDigitHexExpression;
        private static readonly Regex integerFunctionalExpression;
        private static readonly Regex floatFunctionalExpression;
        private static readonly Regex rgbFunctionalExpression;

        static SVGsRGBColorConverter();
        public static string ConvertColorToSixDigitHex(string color);
        private static int GetInt32(string str);
    }
}

