namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public static class PredefinedMasks
    {
        public static class DateTime
        {
            public const string ShortDate = "d";
            public const string LongDate = "D";
            public const string ShortTime = "t";
            public const string LongTime = "T";
            public const string FullDateShortTime = "f";
            public const string FullDateLongTime = "F";
            public const string GeneralDateShortTime = "g";
            public const string GeneralDateLongTime = "G";
            public const string MonthDay = "m";
            public const string RFC1123 = "r";
            public const string SortableDateTime = "s";
            public const string UniversalSortableDateTime = "u";
            public const string YearMonth = "y";
        }

        public static class Numeric
        {
            public const string Currency = "c";
            public const string Decimal = "d";
            public const string FixedPoint = "f";
            public const string Number = "n";
            public const string Percent = "P";
            public const string PercentFractional = "p";
        }
    }
}

