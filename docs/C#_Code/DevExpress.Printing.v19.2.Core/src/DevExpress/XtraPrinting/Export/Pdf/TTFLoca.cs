namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Reflection;

    internal class TTFLoca : TTFTable
    {
        private uint[] offsets;

        public TTFLoca(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFLoca loca = pattern as TTFLoca;
            this.offsets = new uint[loca.Count];
            uint num = 0;
            for (int i = 0; i < base.Owner.Glyf.Glyphs.Length; i++)
            {
                this.offsets[i] = num;
                if (base.Owner.Glyf.Glyphs[i] != null)
                {
                    num += (uint) base.Owner.Glyf.Glyphs[i].Size;
                }
            }
            this.offsets[base.Owner.Glyf.Glyphs.Length] = num;
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.offsets = new uint[base.Owner.MaxP.NumGlyphs + 1];
            for (int i = 0; i < (base.Owner.MaxP.NumGlyphs + 1); i++)
            {
                this.offsets[i] = (base.Owner.Head.IndexToLocFormat == 1) ? ttfStream.ReadULong() : (Convert.ToUInt32(ttfStream.ReadUShort()) << 1);
            }
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            for (int i = 0; i < (base.Owner.MaxP.NumGlyphs + 1); i++)
            {
                if (base.Owner.Head.IndexToLocFormat == 1)
                {
                    ttfStream.WriteULong(this.offsets[i]);
                }
                else
                {
                    ttfStream.WriteUShort(Convert.ToUInt16((uint) (this.offsets[i] >> 1)));
                }
            }
        }

        protected internal override string Tag =>
            "loca";

        public int Count =>
            this.offsets.Length;

        public uint this[ushort index] =>
            this.offsets[index];

        public override int Length =>
            ((base.Owner.Head.IndexToLocFormat == 1) ? 4 : 2) * this.Count;
    }
}

