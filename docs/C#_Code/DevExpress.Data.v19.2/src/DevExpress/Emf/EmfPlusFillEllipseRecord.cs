namespace DevExpress.Emf
{
    using System;

    public class EmfPlusFillEllipseRecord : EmfPlusFillBase
    {
        private readonly DXRectangleF bounds;

        public EmfPlusFillEllipseRecord(ARGBColor color, DXRectangleF bounds) : base(color)
        {
            this.bounds = bounds;
        }

        public EmfPlusFillEllipseRecord(byte brushId, DXRectangleF bounds) : base(brushId)
        {
            this.bounds = bounds;
        }

        public EmfPlusFillEllipseRecord(short flags, EmfPlusReader reader) : base(flags, reader)
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
            EmfPlusRecordType.EmfPlusFillEllipse;

        protected override int DataSize =>
            20;
    }
}

