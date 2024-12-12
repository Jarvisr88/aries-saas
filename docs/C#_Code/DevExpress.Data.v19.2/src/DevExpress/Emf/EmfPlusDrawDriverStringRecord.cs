namespace DevExpress.Emf
{
    using System;

    public class EmfPlusDrawDriverStringRecord : EmfPlusRecord
    {
        private readonly int brushId;
        private readonly ARGBColor? brushColor;
        private readonly char[] glyphs;
        private readonly DXPointF[] positions;
        private readonly EmfPlusDriverStringOptions stringOptions;

        public EmfPlusDrawDriverStringRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            if ((base.Flags & 0x8000) != 0)
            {
                this.brushColor = new ARGBColor?(reader.ReadArgbColor());
            }
            else
            {
                this.brushId = reader.ReadInt32();
            }
            this.stringOptions = (EmfPlusDriverStringOptions) reader.ReadInt32();
            bool flag = reader.ReadInt32() != 0;
            int num = reader.ReadInt32();
            this.glyphs = new char[num];
            for (int i = 0; i < num; i++)
            {
                this.glyphs[i] = (char) reader.ReadUInt16();
            }
            this.positions = new DXPointF[num];
            for (int j = 0; j < num; j++)
            {
                this.positions[j] = reader.ReadDxPointF();
            }
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int FontId =>
            base.ObjectId;

        public int BrushId =>
            this.brushId;

        public ARGBColor? BrushColor =>
            this.brushColor;

        public char[] Glyphs =>
            this.glyphs;

        public DXPointF[] Positions =>
            this.positions;

        public EmfPlusDriverStringOptions StringOptions =>
            this.stringOptions;
    }
}

