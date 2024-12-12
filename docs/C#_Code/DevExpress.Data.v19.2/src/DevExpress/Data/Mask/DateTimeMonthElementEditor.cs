namespace DevExpress.Data.Mask
{
    using System;

    public class DateTimeMonthElementEditor : DateTimeNumericRangeElementEditor
    {
        private readonly string[] monthsKeys;

        public DateTimeMonthElementEditor(int initialValue, int minDigits, string[] monthsNames);
        public override bool Insert(string inserted);

        public override string DisplayText { get; }
    }
}

