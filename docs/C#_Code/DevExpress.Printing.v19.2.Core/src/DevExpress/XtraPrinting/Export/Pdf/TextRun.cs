namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;

    public class TextRun
    {
        public string Text;
        internal ushort[] Glyphs;
        internal Dictionary<ushort, char[]> CharMap;

        public byte[] GetGlyphs()
        {
            if (!this.HasGlyphs)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[this.Glyphs.Length * 2];
            for (int i = 0; i < this.Glyphs.Length; i++)
            {
                ushort num2 = this.Glyphs[i];
                buffer[i * 2] = Convert.ToByte((int) ((num2 >> 8) % 0x100));
                buffer[(i * 2) + 1] = Convert.ToByte((int) (num2 % 0x100));
            }
            return buffer;
        }

        internal bool HasGlyphs =>
            this.Glyphs != null;
    }
}

