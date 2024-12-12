namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class DayIntervalFactory : DateTimeIntervalFactory
    {
        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddDays(1.0);
        }

        protected override DateTime SnapInternal(DateTime value) => 
            new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);

        protected override string DefaultShortTextFormat =>
            "{0:%d}";

        protected override string DefaultTextFormat =>
            "{0:ddd}, {0:%d}";

        protected override string DefaultLongTextFormat =>
            "{0:dddd}, {0:%d}";

        protected override string DefaultLongestTextFormat =>
            "{0:%d} {0:MMMM}, {0:yyyy}";

        public override string LabelTextFormat =>
            "{0:%d} {0:MMMM}";
    }
}

