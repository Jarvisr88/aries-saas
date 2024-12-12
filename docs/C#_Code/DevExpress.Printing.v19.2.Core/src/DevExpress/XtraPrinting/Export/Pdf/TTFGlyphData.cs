namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFGlyphData
    {
        private const int headerSize = 10;
        private short numberOfContours;
        private short xMin;
        private short yMin;
        private short xMax;
        private short yMax;
        private TTFGlyphDescription description;
        private int size;

        public TTFGlyphData(int size)
        {
            this.size = size;
        }

        public void Read(TTFStream ttfStream)
        {
            this.numberOfContours = ttfStream.ReadShort();
            this.xMin = ttfStream.ReadFWord();
            this.yMin = ttfStream.ReadFWord();
            this.xMax = ttfStream.ReadFWord();
            this.yMax = ttfStream.ReadFWord();
            this.ReadDescription(ttfStream);
        }

        private void ReadDescription(TTFStream ttfStream)
        {
            this.description = (this.numberOfContours < 0) ? new TTFCompositeGlyphDescription() : new TTFGlyphDescription();
            this.description.Read(ttfStream, this.size - 10);
        }

        public void Write(TTFStream ttfStream)
        {
            ttfStream.WriteShort(this.numberOfContours);
            ttfStream.WriteFWord(this.xMin);
            ttfStream.WriteFWord(this.yMin);
            ttfStream.WriteFWord(this.xMax);
            ttfStream.WriteFWord(this.yMax);
            if (this.description != null)
            {
                this.description.Write(ttfStream);
            }
        }

        public TTFGlyphDescription Description =>
            this.description;

        public int Size =>
            this.size;

        public short XMin =>
            this.xMin;

        public short YMin =>
            this.yMin;

        public short XMax =>
            this.xMax;

        public short YMax =>
            this.yMax;
    }
}

