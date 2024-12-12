namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public abstract class DateTimeNumericRangeFormatElementEditable : DateTimeMaskFormatElementEditable
    {
        protected DateTimeNumericRangeFormatElementEditable(string mask, DateTimeFormatInfo dateTimeFormatInfo, DateTimePart dateTimePart);
        protected static DateTime ToLeapYear(DateTime dateTime);
    }
}

