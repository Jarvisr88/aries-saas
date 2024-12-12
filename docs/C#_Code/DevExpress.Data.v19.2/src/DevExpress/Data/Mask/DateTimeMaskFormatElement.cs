namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public abstract class DateTimeMaskFormatElement
    {
        public readonly DevExpress.Data.Mask.DateTimePart DateTimePart;
        protected readonly System.Globalization.DateTimeFormatInfo DateTimeFormatInfo;

        protected DateTimeMaskFormatElement(System.Globalization.DateTimeFormatInfo dateTimeFormatInfo, DevExpress.Data.Mask.DateTimePart dateTimePart);
        public abstract string Format(DateTime formattedDateTime);

        public bool Editable { get; }
    }
}

