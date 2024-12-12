namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskManager : MaskManagerSelectAllEnhancer<TimeSpanMaskManagerCore>
    {
        public TimeSpanMaskManager(TimeSpanMaskManagerCore nested);
        public TimeSpanMaskManager(string mask, bool isOperatorMask, TimeSpanCultureInfoBase cultureInfo, bool allowNull = true, bool moveValueWithNavigation = false, bool spinNextPartOnCycling = true, TimeSpanMaskInputMode inputMode = 2, TimeSpanMaskPart defaultPart = 1, bool allowNegativeValue = true, TimeSpan dayDuration = new TimeSpan());
        public override bool Backspace();
        public override bool Delete();
        public void GotFocus();
        public override bool Insert(string insertion);
        public void LostFocus();

        public static bool AlwaysZeroOnClearSelectAll { get; set; }
    }
}

