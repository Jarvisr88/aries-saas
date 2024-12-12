namespace DevExpress.Emf
{
    using System;

    public class EmfPlusResetClipRecord : EmfPlusRecord
    {
        public EmfPlusResetClipRecord() : base(0)
        {
        }

        public EmfPlusResetClipRecord(short flags, EmfPlusReader reader) : base(flags)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusResetClip;
    }
}

