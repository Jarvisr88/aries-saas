namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [TypeConverter(typeof(TimeSpanRangeCollection.TimeSpanRangeCollectionConverter))]
    public sealed class TimeSpanRangeCollection : ImmutableCollectionCore<TimeSpanRange, TimeSpanRangeCollection>
    {
        public TimeSpanRangeCollection()
        {
        }

        public TimeSpanRangeCollection(IEnumerable<TimeSpanRange> values) : base(values)
        {
        }

        protected override TimeSpanRangeCollection Create(IEnumerable<TimeSpanRange> values) => 
            new TimeSpanRangeCollection(values);

        public static TimeSpanRangeCollection Parse(string input, CultureInfo culture)
        {
            TimeSpanRangeCollection ranges;
            if (!TryParse(input, culture, out ranges))
            {
                throw new FormatException();
            }
            return ranges;
        }

        public override string ToString()
        {
            Func<TimeSpanRange, string> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<TimeSpanRange, string> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = x => x.ToString();
            }
            return string.Join(",", this.Select<TimeSpanRange, string>(selector));
        }

        public string ToString(IFormatProvider provider) => 
            (provider == null) ? this.ToString() : this.ToStringCore(TimeSpanRange.DefaultTimeSpanFormat, provider);

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = TimeSpanRange.DefaultTimeSpanFormat;
            }
            return this.ToStringCore(format, provider);
        }

        private string ToStringCore(string format, IFormatProvider provider)
        {
            ICustomFormatter formatter = provider.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter;
            return ((formatter == null) ? string.Join(",", (IEnumerable<string>) (from x in this select x.ToString(format, provider))) : formatter.Format(format, this, provider));
        }

        public static bool TryParse(string input, CultureInfo culture, out TimeSpanRangeCollection result)
        {
            char[] separator = new char[] { ',' };
            Func<bool, bool> stop = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<bool, bool> local1 = <>c.<>9__9_1;
                stop = <>c.<>9__9_1 = ok => !ok;
            }
            Stated<bool, TimeSpanRangeCollection> stated = input.Split(separator).AsEnumerable<string>().WithState<IEnumerable<string>, bool>(true).SelectUntil<string, TimeSpanRange, TimeSpanRangeCollection, bool>(delegate (string part, bool ok) {
                TimeSpanRange range;
                ok &= TimeSpanRange.TryParse(part.Trim(), culture, out range);
                return range.WithState<TimeSpanRange, bool>(ok);
            }, stop, <>c.<>9__9_2 ??= x => new TimeSpanRangeCollection(x));
            if (!stated.State)
            {
                result = null;
                return false;
            }
            result = stated.Value;
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanRangeCollection.<>c <>9 = new TimeSpanRangeCollection.<>c();
            public static Func<TimeSpanRange, string> <>9__4_0;
            public static Func<bool, bool> <>9__9_1;
            public static Func<IEnumerable<TimeSpanRange>, TimeSpanRangeCollection> <>9__9_2;

            internal string <ToString>b__4_0(TimeSpanRange x) => 
                x.ToString();

            internal bool <TryParse>b__9_1(bool ok) => 
                !ok;

            internal TimeSpanRangeCollection <TryParse>b__9_2(IEnumerable<TimeSpanRange> x) => 
                new TimeSpanRangeCollection(x);
        }

        private sealed class TimeSpanRangeCollectionConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
                (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
                (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                string input = value as string;
                return ((input == null) ? base.ConvertFrom(context, culture, value) : TimeSpanRangeCollection.Parse(input, culture));
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
                (!(destinationType == typeof(string)) || (value == null)) ? base.ConvertTo(context, culture, value, destinationType) : ((TimeSpanRangeCollection) value).ToString(culture);
        }
    }
}

