namespace DevExpress.Emf
{
    using System;

    public class EmfPlusFillPolygonRecord : EmfPlusFillBase
    {
        private readonly DXPointF[] points;

        public EmfPlusFillPolygonRecord(DXPointF[] points, ARGBColor color) : base(color)
        {
            this.points = points;
        }

        public EmfPlusFillPolygonRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
            this.points = reader.ReadPoints(reader.ReadInt32(), flags);
        }

        public EmfPlusFillPolygonRecord(DXPointF[] points, byte brushId) : base(brushId)
        {
            this.points = points;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.points.Length);
            writer.Write(this.points);
        }

        public DXPointF[] Points =>
            this.points;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusFillPolygon;

        protected override int DataSize =>
            8 + (this.points.Length * 8);
    }
}

