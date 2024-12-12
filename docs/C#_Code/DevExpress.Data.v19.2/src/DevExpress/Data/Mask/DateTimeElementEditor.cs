namespace DevExpress.Data.Mask
{
    using System;

    public abstract class DateTimeElementEditor
    {
        protected DateTimeElementEditor();
        public abstract bool Delete();
        public abstract int GetResult();
        public abstract bool Insert(string inserted);
        public abstract bool SpinDown();
        public abstract bool SpinUp();

        public abstract string DisplayText { get; }

        public abstract bool FinalOperatorInsert { get; }
    }
}

