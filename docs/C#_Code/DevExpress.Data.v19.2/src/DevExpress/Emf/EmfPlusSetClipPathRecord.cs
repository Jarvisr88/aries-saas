namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSetClipPathRecord : EmfPlusSetClipRecord
    {
        public EmfPlusSetClipPathRecord(short flags) : base(flags)
        {
        }

        public EmfPlusSetClipPathRecord(byte pathId, EmfPlusCombineMode mode) : base(pathId, mode)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusSetClipPath;
    }
}

