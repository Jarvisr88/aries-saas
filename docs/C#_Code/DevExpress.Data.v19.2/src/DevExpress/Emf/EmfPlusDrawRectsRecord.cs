namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawRectsRecord : EmfPlusPenDrawingRecord
    {
        private readonly DXRectangleF[] rectangles;

        public EmfPlusDrawRectsRecord(byte id, DXRectangleF rectangle) : base(id)
        {
            this.rectangles = new DXRectangleF[] { rectangle };
        }

        public EmfPlusDrawRectsRecord(short flags, EmfPlusReader reader) : base(flags)
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

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawRects;

        protected override int DataSize =>
            4 + (0x10 * this.rectangles.Length);
    }
}

