namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanElementTimeEditor : TimeSpanElementEditor
    {
        private long value;
        private readonly long maxValue;
        private readonly long minValue;
        private readonly string mask;
        private readonly bool allowCustomInput;
        protected int digitsEntered;

        public TimeSpanElementTimeEditor(string mask, long maxValue, long minValue, long value = 0L, bool allowCustomInput = false);
        public override bool Delete();
        public override long GetResult();
        public override bool Insert(string inserted);
        protected void SetUntouchedValue(long newValue);
        public override bool SpinDown(out bool spinNext);
        public override bool SpinUp(out bool spinNext);

        protected int MaxDigits { get; }

        protected bool Touched { get; }

        public override string DisplayText { get; }

        public override bool FinalOperatorInsert { get; }
    }
}

