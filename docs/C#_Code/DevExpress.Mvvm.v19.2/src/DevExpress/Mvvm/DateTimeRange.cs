namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(DateTimeRangeTypeConverter))]
    public struct DateTimeRange : IFormattable, IEquatable<DateTimeRange>
    {
        private static readonly string[] DefaultDateTimeDelimeter;
        internal static readonly string DefaultFormat;
        private static readonly DateTimeRange empty;
        private DateTime start;
        private DateTime end;
        public static DateTimeRange Empty =>
            empty;
        public static DateTimeRange Today =>
            new DateTimeRange(DateTime.Now.Date, TimeSpan.FromDays(1.0));
        public static bool operator ==(DateTimeRange x, DateTimeRange y) => 
            x.Equals(y);

        public static bool operator !=(DateTimeRange x, DateTimeRange y) => 
            !x.Equals(y);

        public static DateTimeRange Union(DateTimeRange x, DateTimeRange y)
        {
            long ticks = Math.Max(x.end.Ticks, y.end.Ticks);
            return new DateTimeRange(new DateTime(Math.Min(x.start.Ticks, y.start.Ticks)), new DateTime(ticks));
        }

        public static DateTimeRange Intersect(DateTimeRange x, DateTimeRange y)
        {
            long ticks = Math.Max(x.start.Ticks, y.start.Ticks);
            long num2 = Math.Min(x.end.Ticks, y.end.Ticks);
            return ((num2 >= ticks) ? new DateTimeRange(new DateTime(ticks), new DateTime(num2)) : Empty);
        }

        public static bool TryParse(string input, CultureInfo culture, out DateTimeRange result)
        {
            DateTime time;
            DateTime time2;
            string[] strArray = input.Split(DefaultDateTimeDelimeter, StringSplitOptions.None);
            if ((strArray.Length == 2) && (TryParse(strArray[0].Replace("(", ""), culture, out time) && TryParse(strArray[1].Replace(")", ""), culture, out time2)))
            {
                result = new DateTimeRange(time, time2);
                return true;
            }
            result = Empty;
            return false;
        }

        private static bool TryParse(string value, CultureInfo culture, out DateTime result)
        {
            try
            {
                result = Convert.ToDateTime(value, culture);
                return true;
            }
            catch
            {
                result = DateTime.Today;
                return false;
            }
        }

        public DateTimeRange(DateTime start, TimeSpan duration)
        {
            this.start = start;
            if (duration.Ticks >= 0L)
            {
                this.end = (duration.Ticks > (DateTime.MaxValue.Ticks - start.Ticks)) ? DateTime.MaxValue : start.Add(duration);
            }
            else if ((duration != TimeSpan.MinValue) && (duration.Negate().Ticks <= start.Ticks))
            {
                this.end = start.Add(duration);
            }
            else
            {
                this.end = DateTime.MinValue;
            }
        }

        public DateTimeRange(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public DateTime Start =>
            this.start;
        public DateTime End =>
            this.end;
        public bool IsEmpty =>
            this.Equals(empty);
        public bool IsValid =>
            this.start <= this.end;
        public TimeSpan Duration =>
            (TimeSpan) (this.end - this.start);
        public DateTimeRange Union(DateTimeRange x) => 
            Union(this, x);

        public DateTimeRange Intersect(DateTimeRange x) => 
            Intersect(this, x);

        public bool Contains(DateTime date) => 
            !(this.start == this.end) ? ((date >= this.start) && (date < this.end)) : (date == this.start);

        public bool Contains(DateTimeRange x) => 
            (!(this.start == this.end) || !(x.start == x.end)) ? ((x.start < this.end) ? ((x.start >= this.start) && (x.end <= this.end)) : false) : (this.start == x.start);

        public override bool Equals(object obj) => 
            (obj is DateTimeRange) ? this.Equals((DateTimeRange) obj) : false;

        public bool Equals(DateTimeRange other) => 
            (this.start == other.start) && (this.end == other.end);

        public override int GetHashCode()
        {
            int hashCode = this.start.GetHashCode();
            return ((((((hashCode >> 0x18) & 0xff) | ((hashCode >> 8) & 0xff00)) | ((hashCode << 8) & 0xff0000)) | ((hashCode << 0x18) & 0xff000000UL)) ^ this.end.GetHashCode());
        }

        public override string ToString() => 
            string.Format(DefaultFormat, this.start, this.end);

        public string ToString(IFormatProvider provider) => 
            (provider == null) ? this.ToString() : this.ToStringCore(DefaultFormat, provider);

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = DefaultFormat;
            }
            return ((provider != null) ? this.ToStringCore(format, provider) : string.Format(format, this.start, this.end));
        }

        private string ToStringCore(string format, IFormatProvider provider)
        {
            ICustomFormatter formatter = provider.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter;
            return ((formatter != null) ? formatter.Format(format, this, provider) : string.Format(format, this.start.ToString(provider), this.end.ToString(provider)));
        }

        static DateTimeRange()
        {
            DefaultDateTimeDelimeter = new string[] { ")-(" };
            DefaultFormat = "({0})-({1})";
            empty = new DateTimeRange(DateTime.MinValue, DateTime.MinValue);
        }
    }
}

