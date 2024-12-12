namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_h12 : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_h12(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
    }
}

