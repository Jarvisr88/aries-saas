namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskFormatElement_Millisecond : DateTimeNumericRangeFormatElementEditable
    {
        public DateTimeMaskFormatElement_Millisecond(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public override DateTime ApplyElement(int result, DateTime editedDateTime);
        public override DateTimeElementEditor CreateElementEditor(DateTime editedDateTime);

        private int EditorMaxValue { get; }

        private int EditorCoeff { get; }
    }
}

