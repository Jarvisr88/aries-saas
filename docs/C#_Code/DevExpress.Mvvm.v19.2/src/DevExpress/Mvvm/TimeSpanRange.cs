namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(TimeSpanRangeTypeConverter))]
    public struct TimeSpanRange : IFormattable, IEquatable<TimeSpanRange>
    {
        internal static readonly string DefaultTimeSpanFormat;
        private static readonly char DefaultTimeSpanDelimeter;
        private static readonly string DefaultFormat;
        private static readonly TimeSpanRange zero;
        private static readonly TimeSpanRange day;
        private readonly TimeSpan start;
        private readonly TimeSpan end;
        public static TimeSpanRange Zero =>
            zero;
        public static TimeSpanRange Day =>
            day;
        public static bool operator ==(TimeSpanRange x, TimeSpanRange y) => 
            x.Equals(y);

        public static bool operator !=(TimeSpanRange x, TimeSpanRange y) => 
            !x.Equals(y);

        public static TimeSpanRange Union(TimeSpanRange x, TimeSpanRange y) => 
            new TimeSpanRange((x.start < y.start) ? x.start : y.start, (x.end > y.end) ? x.end : y.end);

        public static TimeSpanRange Intersect(TimeSpanRange x, TimeSpanRange y)
        {
            TimeSpan start = (x.start > y.start) ? x.start : y.start;
            TimeSpan end = (x.end < y.end) ? x.end : y.end;
            return ((end < start) ? Zero : new TimeSpanRange(start, end));
        }

        public static TimeSpanRange Parse(string input, CultureInfo culture)
        {
            TimeSpanRange range;
            if (!TryParse(input, culture, out range))
            {
                throw new FormatException();
            }
            return range;
        }

        public static bool TryParse(string input, CultureInfo culture, out TimeSpanRange result)
        {
            TimeSpan span;
            TimeSpan span2;
            char[] separator = new char[] { DefaultTimeSpanDelimeter };
            string[] strArray = input.Split(separator);
            if ((strArray.Length == 2) && (TimeSpan.TryParse(strArray[0], culture, out span) && TimeSpan.TryParse(strArray[1], culture, out span2)))
            {
                result = new TimeSpanRange(span, span2);
                return true;
            }
            result = zero;
            return false;
        }

        public TimeSpanRange(TimeSpan start, TimeSpan end)
        {
            this.start = start;
            this.end = end;
        }

        public TimeSpan Start =>
            this.start;
        public TimeSpan End =>
            this.end;
        public TimeSpan Duration =>
            this.end - this.start;
        public bool IsZero =>
            this.Equals(zero);
        public bool IsDay =>
            this.Equals(day);
        public bool IsValid =>
            this.start <= this.end;
        public TimeSpanRange Union(TimeSpanRange x) => 
            Union(this, x);

        public TimeSpanRange Intersect(TimeSpanRange x) => 
            Intersect(this, x);

        public override bool Equals(object obj) => 
            (obj is TimeSpanRange) ? this.Equals((TimeSpanRange) obj) : false;

        public bool Equals(TimeSpanRange other) => 
            (this.start == other.start) && (this.end == other.end);

        public override int GetHashCode()
        {
            int hashCode = this.start.GetHashCode();
            return ((((((hashCode >> 0x18) & 0xff) | ((hashCode >> 8) & 0xff00)) | ((hashCode << 8) & 0xff0000)) | ((hashCode << 0x18) & 0xff000000UL)) ^ this.end.GetHashCode());
        }

        public override string ToString() => 
            string.Format(DefaultFormat, this.start.ToString(DefaultTimeSpanFormat), this.end.ToString(DefaultTimeSpanFormat));

        public string ToString(IFormatProvider provider) => 
            (provider == null) ? this.ToString() : this.ToStringCore(DefaultFormat, provider);

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = DefaultFormat;
            }
            return ((provider != null) ? this.ToStringCore(format, provider) : string.Format(format, this.start.ToString(DefaultTimeSpanFormat), this.end.ToString(DefaultTimeSpanFormat)));
        }

        private string ToStringCore(string format, IFormatProvider provider)
        {
            ICustomFormatter formatter = provider.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter;
            return ((formatter != null) ? formatter.Format(format, this, provider) : string.Format(format, this.start.ToString(DefaultTimeSpanFormat, provider), this.end.ToString(DefaultTimeSpanFormat, provider)));
        }

        static TimeSpanRange()
        {
            DefaultTimeSpanFormat = @"d\.hh\:mm\:ss\.fff";
            DefaultTimeSpanDelimeter = '-';
            DefaultFormat = "{0}-{1}";
            zero = new TimeSpanRange(TimeSpan.Zero, TimeSpan.Zero);
            day = new TimeSpanRange(TimeSpan.Zero, TimeSpan.FromDays(1.0));
        }
    }
}

