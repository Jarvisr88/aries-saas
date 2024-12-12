namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusFillBase : EmfPlusRecord
    {
        private readonly int brushId;
        private readonly ARGBColor? color;

        protected EmfPlusFillBase(ARGBColor color) : base(-32768)
        {
            this.color = new ARGBColor?(color);
        }

        protected EmfPlusFillBase(byte brushId) : this(brushId, 0)
        {
        }

        protected EmfPlusFillBase(ARGBColor color, byte id) : base((short) (-32768 | id))
        {
            this.color = new ARGBColor?(color);
        }

        protected EmfPlusFillBase(byte brushId, byte objectId) : base(objectId)
        {
            this.brushId = brushId;
        }

        protected EmfPlusFillBase(short flags, EmfPlusReader reader) : base(flags)
        {
            if ((base.Flags & -32768) != 0)
            {
                this.color = new ARGBColor?(reader.ReadArgbColor());
            }
            else
            {
                this.brushId = reader.ReadInt32();
            }
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write((this.color != null) ? this.color.Value.ToArgb() : this.brushId);
        }

        public int BrushId =>
            this.brushId;

        public ARGBColor? Color =>
            this.color;
    }
}

