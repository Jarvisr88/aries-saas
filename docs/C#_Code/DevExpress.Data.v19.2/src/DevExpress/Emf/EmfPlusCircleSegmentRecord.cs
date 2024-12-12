namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusCircleSegmentRecord : EmfPlusPenDrawingRecord
    {
        private readonly float startAngle;
        private readonly float sweepAngle;
        private readonly DXRectangleF bounds;

        protected EmfPlusCircleSegmentRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.startAngle = reader.ReadSingle();
            this.sweepAngle = reader.ReadSingle();
            this.bounds = reader.ReadDXRectangleF((flags & 0x4000) != 0);
        }

        protected EmfPlusCircleSegmentRecord(byte penId, DXRectangleF bounds, float startAngle, float sweepAngle) : base(penId)
        {
            this.bounds = bounds;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.startAngle);
            writer.Write(this.sweepAngle);
            writer.Write(this.bounds);
        }

        public float StartAngle =>
            this.startAngle;

        public float SweepAngle =>
            this.sweepAngle;

        public DXRectangleF Bounds =>
            this.bounds;

        protected override int DataSize =>
            0x18;
    }
}

