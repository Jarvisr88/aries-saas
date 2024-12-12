namespace DevExpress.Emf
{
    using System;
    using System.Text;

    public class EmfPlusDrawStringRecord : EmfPlusRecord
    {
        private readonly int brushId;
        private readonly ARGBColor? brushColor;
        private readonly int formatId;
        private readonly DXRectangleF layoutRect;
        private readonly string text;

        public EmfPlusDrawStringRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            if ((flags & -32768) != 0)
            {
                this.brushColor = new ARGBColor?(reader.ReadArgbColor());
            }
            else
            {
                this.brushId = reader.ReadInt32();
            }
            this.formatId = reader.ReadInt32();
            int num = reader.ReadInt32();
            this.layoutRect = reader.ReadDXRectangleF(false);
            this.text = Encoding.Unicode.GetString(reader.ReadBytes(num * 2));
        }

        public EmfPlusDrawStringRecord(string text, DXRectangleF layoutRect, ARGBColor color, byte fontId, byte formatId) : base((short) (-32768 | fontId))
        {
            this.text = text;
            this.layoutRect = layoutRect;
            this.formatId = formatId;
            this.brushColor = new ARGBColor?(color);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.brushColor.Value);
            writer.Write(this.formatId);
            writer.Write(this.text.Length);
            writer.Write(this.layoutRect);
            writer.Write(Encoding.Unicode.GetBytes(this.text));
            int paddingSize = this.PaddingSize;
            if (paddingSize != 0)
            {
                writer.Write(new byte[paddingSize]);
            }
        }

        public int BrushId =>
            this.brushId;

        public ARGBColor? BrushColor =>
            this.brushColor;

        public int FormatId =>
            this.formatId;

        public DXRectangleF LayoutRect =>
            this.layoutRect;

        public string Text =>
            this.text;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusDrawString;

        protected override int DataSize =>
            (0x1c + (this.text.Length * 2)) + this.PaddingSize;

        private int PaddingSize =>
            GetPadding(this.text.Length * 2);
    }
}

