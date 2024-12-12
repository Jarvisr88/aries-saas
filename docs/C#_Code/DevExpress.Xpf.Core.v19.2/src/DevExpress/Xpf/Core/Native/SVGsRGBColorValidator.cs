namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Text.RegularExpressions;

    internal static class SVGsRGBColorValidator
    {
        private static readonly Regex uriExpression;

        static SVGsRGBColorValidator();
        public static bool IsValid(string color);
    }
}

