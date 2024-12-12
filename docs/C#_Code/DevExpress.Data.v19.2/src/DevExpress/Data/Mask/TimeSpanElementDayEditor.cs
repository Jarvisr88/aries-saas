namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanElementDayEditor : TimeSpanElementEditor
    {
        private long value;
        private string mask;
        protected int digitsEntered;

        public TimeSpanElementDayEditor(string mask, long value = 0L);
        public override bool Delete();
        public override long GetResult();
        public override bool Insert(string inserted);
        public override bool SpinDown(out bool spinNext);
        public override bool SpinUp(out bool spinNext);

        protected int MaxValue { get; }

        protected bool Touched { get; }

        public override string DisplayText { get; }

        public override bool FinalOperatorInsert { get; }
    }
}

