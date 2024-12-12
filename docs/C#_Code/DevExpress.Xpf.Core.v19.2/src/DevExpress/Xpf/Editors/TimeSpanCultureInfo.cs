namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Mask;
    using System;

    public class TimeSpanCultureInfo : TimeSpanCultureInfoBase
    {
        public TimeSpanCultureInfo() : base(CultureInfo.CurrentCulture.Name)
        {
        }

        public TimeSpanCultureInfo(int culture) : base(culture)
        {
        }

        public TimeSpanCultureInfo(string name) : base(name)
        {
        }

        public TimeSpanCultureInfo(int culture, bool useUserOverride) : base(culture, useUserOverride)
        {
        }

        public TimeSpanCultureInfo(string name, bool useUserOverride) : base(name, useUserOverride)
        {
        }

        public override string GetStringLiteral(TimeSpanStringLiteralType type, long value)
        {
            switch (type)
            {
                case TimeSpanStringLiteralType.Days:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanDaysPlural : EditorStringId.TimeSpanDays);

                case TimeSpanStringLiteralType.DaysShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanDaysShort);

                case TimeSpanStringLiteralType.Hours:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanHoursPlural : EditorStringId.TimeSpanHours);

                case TimeSpanStringLiteralType.HoursShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanHoursShort);

                case TimeSpanStringLiteralType.Minutes:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanMinutesPlural : EditorStringId.TimeSpanMinutes);

                case TimeSpanStringLiteralType.MinutesShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanMinutesShort);

                case TimeSpanStringLiteralType.Seconds:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanSecondsPlural : EditorStringId.TimeSpanSeconds);

                case TimeSpanStringLiteralType.SecondsShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanSecondsShort);

                case TimeSpanStringLiteralType.Fractional:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanFractionalSecondsPlural : EditorStringId.TimeSpanFractionalSeconds);

                case TimeSpanStringLiteralType.FractionalShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanFractionalSecondsShort);

                case TimeSpanStringLiteralType.Milliseconds:
                    return EditorLocalizer.GetString(this.IsPlural(value) ? EditorStringId.TimeSpanMillisecondsPlural : EditorStringId.TimeSpanMilliseconds);

                case TimeSpanStringLiteralType.MillisecondsShort:
                    return EditorLocalizer.GetString(EditorStringId.TimeSpanMillisecondsShort);
            }
            throw new ArgumentOutOfRangeException("type", type, null);
        }

        protected virtual bool IsPlural(long value)
        {
            long num = Math.Abs(value) % ((long) 100);
            long num2 = num % ((long) 10);
            return (((num <= 10) || (num >= 20)) ? (((num2 > 1L) && (num2 < 5L)) || (num2 != 1L)) : true);
        }
    }
}

