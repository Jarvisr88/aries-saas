namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFGlyphDescription
    {
        private byte[] data;

        public virtual void Read(TTFStream ttfStream, int size)
        {
            this.data = ttfStream.ReadBytes(size);
        }

        public void Write(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.data);
        }

        public byte[] Data =>
            this.data;
    }
}

