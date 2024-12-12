namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class YearIntervalFactory : DateTimeIntervalFactory
    {
        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddYears(1);
        }

        protected override DateTime SnapInternal(DateTime value) => 
            new DateTime(value.Year, 1, 1, 0, 0, 0);

        protected override string DefaultShortTextFormat =>
            "{0:yyyy}";

        protected override string DefaultTextFormat =>
            "{0:yyyy}";

        protected override string DefaultLongTextFormat =>
            "{0:yyyy}";

        protected override string DefaultLongestTextFormat =>
            "{0:yyyy}";

        public override string LabelTextFormat =>
            "{0:yyyy}";
    }
}

