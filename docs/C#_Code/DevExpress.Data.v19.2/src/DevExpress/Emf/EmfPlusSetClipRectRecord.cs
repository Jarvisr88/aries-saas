namespace DevExpress.Emf
{
    using System;

    public class EmfPlusSetClipRectRecord : EmfPlusSetClipRecord
    {
        private readonly DXRectangleF rectangle;

        public EmfPlusSetClipRectRecord(DXRectangleF rectangle, EmfPlusCombineMode combineMode) : base(combineMode)
        {
            this.rectangle = rectangle;
        }

        public EmfPlusSetClipRectRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.rectangle = reader.ReadDXRectangleF(false);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.rectangle);
        }

        public DXRectangleF Rectangle =>
            this.rectangle;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusSetClipRect;

        protected override int DataSize =>
            0x10;
    }
}

