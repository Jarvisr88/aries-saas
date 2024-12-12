namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class NumericFormatter
    {
        private readonly int[] groupSizes;
        private readonly string groupSeparator;
        private readonly string decimalSeparator;
        private readonly List<string> formatMask;
        private readonly int maxDigitsBeforeDecimalSeparator;
        private readonly int maxDigitsAfterDecimalSeparator;
        private readonly int minDigitsBeforeDecimalSeparator;
        private readonly int minDigitsAfterDecimalSeparator;
        public readonly bool Is100Multiplied;
        public static bool? ForceCanRemoveSignByDeleteAndBackspace;

        public NumericFormatter(string formatString, CultureInfo formattingCulture);
        private static string CreateCurrencyFormat(int precision, CultureInfo culture);
        private static string CreateDecimalFormat(int precision, CultureInfo culture);
        private static string CreateFixedPointFormat(int precision, CultureInfo culture);
        private static string CreateFullNumberFormatFromPositiveFormat(string numberFormat, CultureInfo culture);
        private static string CreateNumberFormat(int precision, CultureInfo culture);
        private static string CreatePercentFormat(int precision, CultureInfo culture, string percentSymbol);
        public static string Expand(string formatString, CultureInfo culture);
        public string Format(string source);
        private string Format(string source, int sourcePositionForTerminate);
        private static string GetNegativeSymbolPattern(CultureInfo culture);
        public int GetPositionFormatted(string source, int sourcePosition);
        public int GetPositionSource(string source, int formattedPosition);
        private string GetSeparator(int positionFromDecimalSeparator);
        public static void GuessCanRemoveSignByDeleteAndBackspace(NumericFormatter a, NumericFormatter b, out bool canRemoveSignByDelete, out bool canRemoveSignByBackspace);

        public int MaxDigitsBeforeDecimalSeparator { get; }

        public int MinDigitsBeforeDecimalSeparator { get; }

        public int MaxDigitsAfterDecimalSeparator { get; }

        public int MinDigitsAfterDecimalSeparator { get; }

        private enum NumericMaskSubType
        {
            public const NumericFormatter.NumericMaskSubType Number = NumericFormatter.NumericMaskSubType.Number;,
            public const NumericFormatter.NumericMaskSubType Currency = NumericFormatter.NumericMaskSubType.Currency;,
            public const NumericFormatter.NumericMaskSubType Percent = NumericFormatter.NumericMaskSubType.Percent;,
            public const NumericFormatter.NumericMaskSubType PercentPercent = NumericFormatter.NumericMaskSubType.PercentPercent;
        }
    }
}

