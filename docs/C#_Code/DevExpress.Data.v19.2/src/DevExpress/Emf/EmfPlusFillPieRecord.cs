namespace DevExpress.Emf
{
    using System;

    public class EmfPlusFillPieRecord : EmfPlusFillBase
    {
        private readonly DXRectangleF bounds;
        private readonly float startAngle;
        private readonly float sweepAngle;

        public EmfPlusFillPieRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
            this.startAngle = reader.ReadSingle();
            this.sweepAngle = reader.ReadSingle();
            this.bounds = reader.ReadDXRectangleF((flags & 0x4000) != 0);
        }

        public EmfPlusFillPieRecord(DXRectangleF bounds, float startAngle, float sweepAngle, ARGBColor color) : base(color)
        {
            this.bounds = bounds;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        public EmfPlusFillPieRecord(DXRectangleF bounds, float startAngle, float sweepAngle, byte brushId) : base(brushId)
        {
            this.bounds = bounds;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.startAngle);
            writer.Write(this.sweepAngle);
            writer.Write(this.bounds);
        }

        public DXRectangleF Bounds =>
            this.bounds;

        public float StartAngle =>
            this.startAngle;

        public float SweepAngle =>
            this.sweepAngle;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusFillPie;

        protected override int DataSize =>
            0x1c;
    }
}

