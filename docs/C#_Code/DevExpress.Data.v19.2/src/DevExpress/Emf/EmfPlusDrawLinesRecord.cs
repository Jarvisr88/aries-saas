namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawLinesRecord : EmfPlusPointBasedRecord
    {
        private const short polygonFlagMask = 0x2000;

        public EmfPlusDrawLinesRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
        }

        public EmfPlusDrawLinesRecord(byte penId, DXPointF[] points, bool isPolygon) : this((short) ((isPolygon ? 0x2000 : 0) | penId), points)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsPolygon =>
            (base.Flags & 0x2000) != 0;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawLines;
    }
}

