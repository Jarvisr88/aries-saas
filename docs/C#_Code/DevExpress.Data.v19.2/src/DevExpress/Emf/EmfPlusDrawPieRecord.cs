namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawPieRecord : EmfPlusCircleSegmentRecord
    {
        public EmfPlusDrawPieRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public EmfPlusDrawPieRecord(byte penId, DXRectangleF bounds, float startAngle, float sweepAngle) : base(penId, bounds, startAngle, sweepAngle)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawPie;
    }
}

