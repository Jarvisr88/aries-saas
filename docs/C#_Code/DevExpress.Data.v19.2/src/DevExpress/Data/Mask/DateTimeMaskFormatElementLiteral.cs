namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElementLiteral : DateTimeMaskFormatElement
    {
        protected string fLiteral;

        public DateTimeMaskFormatElementLiteral(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public override string Format(DateTime formattedDateTime);

        public string Literal { get; }
    }
}

