namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeYearElementEditor : DateTimeNumericRangeElementEditor
    {
        private readonly int maskLength;
        private readonly DateTimeFormatInfo dateTimeFormatInfo;

        public DateTimeYearElementEditor(int initialYear, int maskLength, DateTimeFormatInfo dateTimeFormatInfo);
        public override int GetResult();
        private static int GetYearShift(Calendar calendar);

        public override int MinDigits { get; }
    }
}

