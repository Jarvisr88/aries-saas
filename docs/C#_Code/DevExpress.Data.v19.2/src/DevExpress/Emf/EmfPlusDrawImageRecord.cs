namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawImageRecord : EmfPlusRecord
    {
        private readonly DXRectangleF boundingBox;
        private readonly DXRectangleF srcRectangle;

        public EmfPlusDrawImageRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            reader.ReadInt32();
            reader.ReadInt32();
            this.srcRectangle = reader.ReadDXRectangleF(false);
            this.boundingBox = reader.ReadDXRectangleF((flags & 0x4000) != 0);
        }

        public EmfPlusDrawImageRecord(byte imageId, DXRectangleF boundingBox, DXRectangleF srcRectangle) : base(imageId)
        {
            this.boundingBox = boundingBox;
            this.srcRectangle = srcRectangle;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(0);
            writer.Write(0);
            writer.Write(this.srcRectangle);
            writer.Write(this.boundingBox);
        }

        public DXRectangleF BoundingBox =>
            this.boundingBox;

        public DXRectangleF SrcRectangle =>
            this.srcRectangle;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawImage;

        protected override int DataSize =>
            40;
    }
}

