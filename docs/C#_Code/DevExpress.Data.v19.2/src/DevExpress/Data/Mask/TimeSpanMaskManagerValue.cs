namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;

    public class TimeSpanMaskManagerValue
    {
        public TimeSpanMaskManagerValue(long ticks);
        public TimeSpanMaskManagerValue(System.TimeSpan timeSpan);
        public TimeSpanMaskManagerValue(long ticks, System.TimeSpan dayDuration);
        public TimeSpanMaskManagerValue(System.TimeSpan timeSpan, System.TimeSpan dayDuration);
        protected bool Equals(TimeSpanMaskManagerValue other);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public object GetValue();
        public TimeSpanMaskManagerValue Negate();
        public TimeSpanMaskManagerValue ReplaceDays(long value);
        public TimeSpanMaskManagerValue ReplaceFractional(long value);
        public TimeSpanMaskManagerValue ReplaceHours(long value);
        public TimeSpanMaskManagerValue ReplaceMilliseconds(long value);
        public TimeSpanMaskManagerValue ReplaceMinutes(long value);
        public TimeSpanMaskManagerValue ReplaceSeconds(long value);
        public override string ToString();
        public string ToString(string mask);

        internal System.TimeSpan TimeSpan { get; set; }

        private TimeSpanMaskManagerValue.TimeSpanValueType ValueType { get; set; }

        internal System.TimeSpan DayDuration { get; private set; }

        public long Ticks { get; }

        public long Days { get; }

        public long Hours { get; }

        public long Minutes { get; }

        public long Seconds { get; }

        public long Milliseconds { get; }

        public double TotalHours { get; }

        public double TotalMinutes { get; }

        public double TotalSeconds { get; }

        public double TotalMilliseconds { get; }

        public bool IsNegative { get; }

        private enum TimeSpanValueType
        {
            public const TimeSpanMaskManagerValue.TimeSpanValueType Long = TimeSpanMaskManagerValue.TimeSpanValueType.Long;,
            public const TimeSpanMaskManagerValue.TimeSpanValueType TimeSpan = TimeSpanMaskManagerValue.TimeSpanValueType.TimeSpan;
        }
    }
}

