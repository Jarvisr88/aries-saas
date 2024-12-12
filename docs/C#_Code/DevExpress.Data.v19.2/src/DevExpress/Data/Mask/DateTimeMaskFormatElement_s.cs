namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_s : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_s(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
    }
}

