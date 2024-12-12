namespace DevExpress.Data.Mask
{
    using System;

    public class DateTimeNumericRangeElementEditor : DateTimeElementEditor
    {
        private int fMinValue;
        private int fMaxValue;
        private int fMinDigits;
        private int fMaxDigits;
        private int fCurrentValue;
        protected int digitsEntered;

        public DateTimeNumericRangeElementEditor(int minValue, int maxValue, int minDigits, int maxDigits);
        public DateTimeNumericRangeElementEditor(int initialValue, int minValue, int maxValue, int minDigits, int maxDigits);
        public override bool Delete();
        public override int GetResult();
        public override bool Insert(string inserted);
        protected void SetUntouchedValue(int newValue);
        public override bool SpinDown();
        public override bool SpinUp();

        protected bool Touched { get; }

        public int MinValue { get; }

        public int MaxValue { get; }

        public virtual int MinDigits { get; }

        public int MaxDigits { get; }

        public int CurrentValue { get; }

        public override string DisplayText { get; }

        public override bool FinalOperatorInsert { get; }
    }
}

