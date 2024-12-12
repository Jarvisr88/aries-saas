namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TTFCompositeGlyphDescription : TTFGlyphDescription
    {
        private const ushort ARG_1_AND_2_ARE_WORDS = 1;
        private const ushort WE_HAVE_A_SCALE = 8;
        private const ushort MORE_COMPONENTS = 0x20;
        private const ushort WE_HAVE_AN_X_AND_Y_SCALE = 0x40;
        private const ushort WE_HAVE_A_TWO_BY_TWO = 0x80;
        private List<ushort> glyphIndexList = new List<ushort>();

        private void CreateGlyphIndexList(TTFStream ttfStream)
        {
            ushort num = 0;
            while (true)
            {
                num = ttfStream.ReadUShort();
                this.glyphIndexList.Add(ttfStream.ReadUShort());
                if ((num & 1) != 0)
                {
                    ttfStream.Move(4);
                }
                else
                {
                    ttfStream.Move(2);
                }
                if ((num & 8) != 0)
                {
                    ttfStream.Move(2);
                }
                else if ((num & 0x40) != 0)
                {
                    ttfStream.Move(4);
                }
                else if ((num & 0x80) != 0)
                {
                    ttfStream.Move(8);
                }
                if ((num & 0x20) == 0)
                {
                    return;
                }
            }
        }

        public override void Read(TTFStream ttfStream, int size)
        {
            int position = ttfStream.Position;
            this.CreateGlyphIndexList(ttfStream);
            ttfStream.Seek(position);
            base.Read(ttfStream, size);
        }

        public int Count =>
            this.glyphIndexList.Count;

        public ushort this[int index] =>
            this.glyphIndexList[index];
    }
}

