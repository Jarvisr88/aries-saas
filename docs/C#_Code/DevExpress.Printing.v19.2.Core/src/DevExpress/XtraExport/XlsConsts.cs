namespace DevExpress.XtraExport
{
    using System;

    [CLSCompliant(false)]
    public class XlsConsts
    {
        public const ushort MaxColumn = 0xff;
        public const ushort MaxRow = 0xffff;
        public const ushort GeneralFormat = 0;
        public const ushort NoneDecimalFormat = 1;
        public const ushort DecimalFormat = 2;
        public const ushort DigitNoneDecimalFormat = 3;
        public const ushort DigitDecimalFormat = 4;
        public const ushort CurrencyNoneDecimalFormat = 5;
        public const ushort CurrencyDecimalFormat = 7;
        public const ushort PercentNoneDecimalFormat = 9;
        public const ushort PercentDecimalFormat = 10;
        public const ushort ExponentialDecimalFormat = 11;
        public const ushort ExponentialDecimalOneFormat = 0x30;
        public const ushort DateFormat = 14;
        public const ushort DayMonthYearFormat = 15;
        public const ushort DayMonthFormat = 0x10;
        public const ushort MonthYearFormat = 0x11;
        public const ushort HourMinuteAMPMFormat = 0x12;
        public const ushort HourMinuteSecondAMPMFormat = 0x13;
        public const ushort HourMinuteFormat = 20;
        public const ushort HourMinuteSecondFormat = 0x15;
        public const ushort DateTimeFormat = 0x16;
        public const ushort AccountFormat = 0x25;
        public const ushort AccountDecimalFormat = 0x27;
        public const ushort MinuteSecondFormat = 0x2d;
        public const ushort AbsoluteHourTimeFormat = 0x2e;
        public const ushort MinuteSecondMilFormat = 0x2f;
        public const ushort RealFormat = 0x31;

        private XlsConsts()
        {
        }
    }
}

