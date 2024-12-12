namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_d : DateTimeNumericRangeFormatElementEditable
    {
        private DateTimeMaskFormatElementContext context;
        public static bool EnforceStrictMonthPriority;

        public DateTimeMaskFormatElement_d(string mask, DateTimeFormatInfo dateTimeFormatInfo, DateTimeMaskFormatElementContext context);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
    }
}

