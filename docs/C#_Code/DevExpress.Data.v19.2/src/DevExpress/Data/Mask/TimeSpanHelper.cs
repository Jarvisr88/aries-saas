namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class TimeSpanHelper
    {
        public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> predicate);
        private static TimeSpan Replace(TimeSpan timeSpan, long newValue, Func<TimeSpan, long> getValue, Func<TimeSpan, double> getTotalValue, long valueTicks);
        public static TimeSpan ReplaceBackDaysWithDayDuration(this TimeSpan timeSpan, TimeSpan dayDuration);
        public static TimeSpan ReplaceDays(this TimeSpan timeSpan, long newValue);
        public static TimeSpan ReplaceDaysWithDayDuration(this TimeSpan timeSpan, TimeSpan dayDuration);
        public static TimeSpan ReplaceFractional(this TimeSpan timeSpan, long newValue);
        public static TimeSpan ReplaceHours(this TimeSpan timeSpan, long newValue);
        public static TimeSpan ReplaceMilliseconds(this TimeSpan timeSpan, long newValue);
        public static TimeSpan ReplaceMinutes(this TimeSpan timeSpan, long newValue);
        public static TimeSpan ReplaceSeconds(this TimeSpan timeSpan, long newValue);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanHelper.<>c <>9;
            public static Func<TimeSpan, long> <>9__1_0;
            public static Func<TimeSpan, double> <>9__1_1;
            public static Func<TimeSpan, long> <>9__2_0;
            public static Func<TimeSpan, double> <>9__2_1;
            public static Func<TimeSpan, long> <>9__3_0;
            public static Func<TimeSpan, double> <>9__3_1;
            public static Func<TimeSpan, long> <>9__4_0;
            public static Func<TimeSpan, double> <>9__4_1;
            public static Func<TimeSpan, long> <>9__5_0;
            public static Func<TimeSpan, double> <>9__5_1;

            static <>c();
            internal long <ReplaceDays>b__1_0(TimeSpan x);
            internal double <ReplaceDays>b__1_1(TimeSpan x);
            internal long <ReplaceHours>b__2_0(TimeSpan x);
            internal double <ReplaceHours>b__2_1(TimeSpan x);
            internal long <ReplaceMilliseconds>b__5_0(TimeSpan x);
            internal double <ReplaceMilliseconds>b__5_1(TimeSpan x);
            internal long <ReplaceMinutes>b__3_0(TimeSpan x);
            internal double <ReplaceMinutes>b__3_1(TimeSpan x);
            internal long <ReplaceSeconds>b__4_0(TimeSpan x);
            internal double <ReplaceSeconds>b__4_1(TimeSpan x);
        }
    }
}

