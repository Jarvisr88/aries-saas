namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public abstract class NamedMasks
    {
        public static CultureInfo DefaultCulture;

        static NamedMasks();
        protected NamedMasks();
        public static string Escape(string input);
        public static string Escape(string[] inputStrings, bool ignoreZeros);
        public static string GetAbbreviatedDayNames(CultureInfo culture);
        public static string GetAbbreviatedMonthNames(CultureInfo culture);
        public static string GetAMDesignator(CultureInfo culture);
        public static string GetCurrencyDecimalSeparator(CultureInfo culture);
        public static string GetCurrencyPattern(CultureInfo culture);
        public static string GetCurrencySymbol(CultureInfo culture);
        public static string GetDateSeparator(CultureInfo culture);
        public static string GetDayNames(CultureInfo culture);
        public static string GetMonthNames(CultureInfo culture);
        public static string GetNamedMask(string maskName);
        public static string GetNamedMask(string maskName, CultureInfo culture);
        public static string GetNumberDecimalSeparator(CultureInfo culture);
        public static string GetNumberPattern(CultureInfo culture);
        public static string GetPMDesignator(CultureInfo culture);
        public static string GetTimeSeparator(CultureInfo culture);

        public static string DateSeparator { get; }

        public static string TimeSeparator { get; }

        public static string AbbreviatedDayNames { get; }

        public static string AbbreviatedMonthNames { get; }

        public static string AMDesignator { get; }

        public static string DayNames { get; }

        public static string MonthNames { get; }

        public static string PMDesignator { get; }

        public static string NumberDecimalSeparator { get; }

        public static string CurrencyDecimalSeparator { get; }

        public static string CurrencySymbol { get; }

        public static string NumberPattern { get; }

        public static string CurrencyPattern { get; }
    }
}

