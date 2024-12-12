namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class ValueDataRange
    {
        public readonly ValueData From;
        public readonly ValueData To;

        public ValueDataRange(ValueData from, ValueData to);
        public ValueDataRange UpdateFrom(Func<ValueData, ValueData> update);
        public ValueDataRange UpdateTo(Func<ValueData, ValueData> update);
    }
}

