namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawEllipseRecord : EmfPlusPenDrawingRecord
    {
        private readonly DXRectangleF bounds;

        public EmfPlusDrawEllipseRecord(byte penId, DXRectangleF bounds) : base(penId)
        {
            this.bounds = bounds;
        }

        public EmfPlusDrawEllipseRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.bounds = reader.ReadDXRectangleF((flags & 0x4000) != 0);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.bounds);
        }

        public DXRectangleF Bounds =>
            this.bounds;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawEllipse;

        protected override int DataSize =>
            0x10;
    }
}

