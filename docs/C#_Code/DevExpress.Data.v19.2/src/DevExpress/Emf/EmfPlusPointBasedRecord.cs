namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusPointBasedRecord : EmfPlusPenDrawingRecord
    {
        private readonly DXPointF[] points;

        protected EmfPlusPointBasedRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.points = reader.ReadPoints(reader.ReadInt32(), flags);
        }

        public EmfPlusPointBasedRecord(short flags, DXPointF[] points) : base(flags)
        {
            this.points = points;
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.points.Length);
            writer.Write(this.points);
        }

        public DXPointF[] Points =>
            this.points;

        protected override int DataSize =>
            4 + (this.points.Length * 8);
    }
}

