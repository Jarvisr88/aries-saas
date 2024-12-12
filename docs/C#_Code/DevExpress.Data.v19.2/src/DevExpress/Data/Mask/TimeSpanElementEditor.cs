namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class TimeSpanElementEditor
    {
        protected TimeSpanElementEditor();
        public abstract bool Delete();
        public abstract long GetResult();
        public abstract bool Insert(string inserted);
        public abstract bool SpinDown(out bool spinNext);
        public abstract bool SpinUp(out bool spinNext);

        public abstract string DisplayText { get; }

        public abstract bool FinalOperatorInsert { get; }
    }
}

