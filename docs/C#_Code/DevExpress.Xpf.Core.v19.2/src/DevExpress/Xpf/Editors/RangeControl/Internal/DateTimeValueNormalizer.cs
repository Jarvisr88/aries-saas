namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;

    public class DateTimeValueNormalizer : ValueNormalizerBase
    {
        private const long TicksPerMillisecond = 0x2710L;
        private const long TicksPerSecond = 0x989680L;
        private const long TicksPerMinute = 0x23c34600L;
        private const long TicksPerHour = 0x861c46800L;
        private const long TicksPerDay = 0xc92a69c000L;
        private const long DoubleDateOffset = 0x85103c0cb83c000L;
        private const int DaysPerYear = 0x16d;
        private const int DaysPer4Years = 0x5b5;
        private const int DaysPer100Years = 0x8eac;
        private const int DaysPer400Years = 0x23ab1;
        private const int DaysTo1899 = 0xa9559;
        private const int DaysTo10000 = 0x37b9db;
        private const int MillisPerSecond = 0x3e8;
        private const int MillisPerMinute = 0xea60;
        private const int MillisPerHour = 0x36ee80;
        private const int MillisPerDay = 0x5265c00;
        private const long OADateMinAsTicks = 0x6efdddaec64000L;
        private const double OADateMinAsDouble = -657435.0;
        private const double OADateMaxAsDouble = 2958466.0;
        private const long MaxMillis = 0x11efae44cb400L;

        internal static long DoubleDateToTicks(double value)
        {
            if ((value >= 2958466.0) || (value <= -657435.0))
            {
                throw new ArgumentException("Arg_OleAutDateInvalid");
            }
            long num = (long) ((value * 86400000.0) + ((value >= 0.0) ? 0.5 : -0.5));
            if (num < 0L)
            {
                num -= (num % 0x5265c00L) * 2L;
            }
            num += 0x3680b5e1fc00L;
            if ((num < 0L) || (num >= 0x11efae44cb400L))
            {
                throw new ArgumentException("Arg_OleAutDateScale");
            }
            return (num * 0x2710L);
        }

        private static DateTime FromOADate(double d) => 
            new DateTime(DoubleDateToTicks(d), DateTimeKind.Unspecified);

        private static TimeSpan FromOATimeSpan(double d) => 
            new TimeSpan(DoubleDateToTicks(d));

        public double GetComparableDuration(TimeSpan duration) => 
            TicksToOADate(duration.Ticks);

        public override double GetComparableValue(object realValue) => 
            ((realValue == null) || (realValue.GetType() != typeof(DateTime))) ? 0.0 : this.ToOADate(Convert.ToDateTime(realValue));

        public TimeSpan GetRealDuration(double duration) => 
            FromOATimeSpan(duration);

        public override object GetRealValue(double comparable) => 
            FromOADate(comparable);

        private static double TicksToOADate(long value)
        {
            if (value == 0)
            {
                return 0.0;
            }
            if (value < 0xc92a69c000L)
            {
                value += 0x85103c0cb83c000L;
            }
            long num = (value - 0x85103c0cb83c000L) / 0x2710L;
            if (num < 0L)
            {
                long num2 = num % 0x5265c00L;
                if (num2 != 0)
                {
                    num -= (0x5265c00L + num2) * 2L;
                }
            }
            return (((double) num) / 86400000.0);
        }

        private double ToOADate(DateTime dt) => 
            TicksToOADate(dt.Ticks);
    }
}

