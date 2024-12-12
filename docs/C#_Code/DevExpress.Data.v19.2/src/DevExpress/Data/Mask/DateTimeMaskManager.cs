namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class DateTimeMaskManager : MaskManagerSelectAllEnhancer<DateTimeMaskManagerCore>
    {
        public static bool DoNotClearValueOnInsertAfterSelectAll;

        public DateTimeMaskManager(DateTimeMaskManagerCore coreManager);
        public DateTimeMaskManager(string mask, bool isOperatorMask, CultureInfo culture, bool allowNull);
        public override bool Backspace();
        public override bool Delete();
        public static DateTimeFormatInfo GetGoodCalendarDateTimeFormatInfo(CultureInfo inputCulture);
        private static bool IsGoodCalendar(Calendar calendar);
        protected override bool MakeChange(Func<bool> changeWithTrueWhenSuccessfull);
        protected override bool MakeCursorOp(Func<bool> cursorOpWithTrueWhenSuccessfull);

        protected override bool IsNestedCanSelectAll { get; }
    }
}

