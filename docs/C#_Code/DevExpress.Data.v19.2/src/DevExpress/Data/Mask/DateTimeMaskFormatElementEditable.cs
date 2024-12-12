namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public abstract class DateTimeMaskFormatElementEditable : DateTimeMaskFormatElementNonEditable
    {
        protected DateTimeMaskFormatElementEditable(string mask, DateTimeFormatInfo dateTimeFormatInfo, DateTimePart dateTimePart);
        public abstract DateTime ApplyElement(int result, DateTime editedDateTime);
        public abstract DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);
    }
}

