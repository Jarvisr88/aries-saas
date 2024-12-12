namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class MonthIntervalFactory : DateTimeIntervalFactory
    {
        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddMonths(1);
        }

        protected override DateTime SnapInternal(DateTime value) => 
            new DateTime(value.Year, value.Month, 1, 0, 0, 0);

        protected override double ShortTextMaxLength =>
            5.0 * this.MinLength;

        protected override string DefaultShortTextFormat =>
            "{0:MMM}";

        protected override string DefaultTextFormat =>
            "{0:MMMM}";

        protected override string DefaultLongTextFormat =>
            "{0:MMMM}, {0:yyyy}";

        protected override string DefaultLongestTextFormat =>
            "{0:MMMM}, {0:yyyy}";

        public override string LabelTextFormat =>
            "{0:%d} {0:MMM} {0:yyyy}";
    }
}

