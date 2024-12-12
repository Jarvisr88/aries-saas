namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_H24 : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_H24(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
    }
}

