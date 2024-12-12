namespace DevExpress.Emf
{
    using System;

    public class EmfPlusRestoreRecord : EmfPlusStateControlRecord
    {
        public EmfPlusRestoreRecord(int stateIndex) : base(stateIndex)
        {
        }

        public EmfPlusRestoreRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusRestore;
    }
}

