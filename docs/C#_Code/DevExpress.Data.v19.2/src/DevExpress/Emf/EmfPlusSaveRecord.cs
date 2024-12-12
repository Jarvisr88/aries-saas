namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSaveRecord : EmfPlusStateControlRecord
    {
        public EmfPlusSaveRecord(int stateIndex) : base(stateIndex)
        {
        }

        public EmfPlusSaveRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusSave;
    }
}

