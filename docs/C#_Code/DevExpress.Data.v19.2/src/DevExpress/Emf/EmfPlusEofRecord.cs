namespace DevExpress.Emf
{
    using System;

    public class EmfPlusEofRecord : EmfPlusRecord
    {
        public EmfPlusEofRecord(short flags) : base(flags)
        {
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusEndOfFile;
    }
}

