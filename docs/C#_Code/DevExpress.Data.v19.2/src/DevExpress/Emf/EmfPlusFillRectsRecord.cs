namespace DevExpress.Emf
{
    using System;

    public class EmfPlusFillRectsRecord : EmfPlusFillBase
    {
        private readonly DXRectangleF[] rectangles;

        public EmfPlusFillRectsRecord(ARGBColor color, DXRectangleF[] rectangles) : base(color)
        {
            this.rectangles = rectangles;
        }

        public EmfPlusFillRectsRecord(byte brushId, DXRectangleF[] rectangles) : base(brushId)
        {
            this.rectangles = rectangles;
        }

        public EmfPlusFillRectsRecord(short flags, EmfPlusReader reader) : base(flags, reader)
        {
            bool compressed = (flags & 0x4000) != 0;
            int num = reader.ReadInt32();
            this.rectangles = new DXRectangleF[num];
            for (int i = 0; i < num; i++)
            {
                this.rectangles[i] = reader.ReadDXRectangleF(compressed);
            }
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.rectangles.Length);
            foreach (DXRectangleF ef in this.rectangles)
            {
                writer.Write(ef);
            }
        }

        public DXRectangleF[] Rectangles =>
            this.rectangles;

        protected override int DataSize =>
            (this.rectangles.Length * 0x10) + 8;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusFillRects;
    }
}

