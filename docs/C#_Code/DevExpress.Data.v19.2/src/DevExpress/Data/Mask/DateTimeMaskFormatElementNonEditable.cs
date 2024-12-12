namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElementNonEditable : DateTimeMaskFormatElement
    {
        private string fMask;

        public DateTimeMaskFormatElementNonEditable(string mask, DateTimeFormatInfo dateTimeFormatInfo, DateTimePart dateTimePart);
        public override string Format(DateTime formattedDateTime);

        public string Mask { get; }
    }
}

