namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class SecondIntervalFactory : DateTimeIntervalFactory
    {
        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddSeconds(1.0);
        }

        protected override DateTime SnapInternal(DateTime value) => 
            new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);

        protected override string DefaultShortTextFormat =>
            "{0:%s}";

        protected override string DefaultTextFormat =>
            "{0:ss}";

        protected override string DefaultLongTextFormat =>
            "{0:ss}";

        protected override string DefaultLongestTextFormat =>
            "{0:hh}:{0:mm}:{0:ss} {0:%d} {0:MMMM}, {0:yyyy}";

        public override string LabelTextFormat =>
            "{0:hh}:{0:mm}:{0:ss}";
    }
}

