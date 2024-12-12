namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_Month : DateTimeNumericRangeFormatElementEditable
    {
        private readonly DateTimeMaskFormatElementContext context;
        private DateTimeMaskFormatGlobalContext monthNamesDeterminator;
        private string[] _MonthNames;

        public DateTimeMaskFormatElement_Month(string mask, DateTimeFormatInfo dateTimeFormatInfo, DateTimeMaskFormatGlobalContext globalContext);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
        public override string Format(DateTime formattedDateTime);

        protected string[] MonthNames { get; }
    }
}

