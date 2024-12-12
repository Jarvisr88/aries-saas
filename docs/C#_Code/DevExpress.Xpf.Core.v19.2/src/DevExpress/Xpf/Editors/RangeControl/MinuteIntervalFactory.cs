namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class MinuteIntervalFactory : DateTimeIntervalFactory
    {
        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddMinutes(1.0);
        }

        protected override DateTime SnapInternal(DateTime value) => 
            new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);

        protected override string DefaultShortTextFormat =>
            "{0:%m}";

        protected override string DefaultTextFormat =>
            "{0:mm}";

        protected override string DefaultLongTextFormat =>
            "{0:mm}";

        protected override string DefaultLongestTextFormat =>
            "{0:hh}:{0:mm} {0:%d} {0:MMMM}, {0:yyyy}";

        public override string LabelTextFormat =>
            "{0:hh}:{0:mm} {0:%d}";
    }
}

