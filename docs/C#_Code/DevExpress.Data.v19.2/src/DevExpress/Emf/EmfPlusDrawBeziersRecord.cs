namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawBeziersRecord : EmfPlusPointBasedRecord
    {
        public EmfPlusDrawBeziersRecord(byte penId, DXPointF[] points) : base(penId, points)
        {
        }

        public EmfPlusDrawBeziersRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawBeziers;
    }
}

