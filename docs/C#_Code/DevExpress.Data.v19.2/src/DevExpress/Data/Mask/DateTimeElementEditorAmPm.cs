namespace DevExpress.Data.Mask
{
    using System;

    public class DateTimeElementEditorAmPm : DateTimeElementEditor
    {
        public const int AMValue = 0;
        public const int PMValue = 1;
        protected readonly string AMDesignator;
        protected readonly string PMDesignator;
        protected readonly string AMShort;
        protected readonly string PMShort;
        private int fResult;
        private string fMask;

        public DateTimeElementEditorAmPm(string mask, int initialValue, string am, string pm);
        public override bool Delete();
        public override int GetResult();
        public override bool Insert(string inserted);
        public override bool SpinDown();
        public override bool SpinUp();

        public override string DisplayText { get; }

        public override bool FinalOperatorInsert { get; }
    }
}

