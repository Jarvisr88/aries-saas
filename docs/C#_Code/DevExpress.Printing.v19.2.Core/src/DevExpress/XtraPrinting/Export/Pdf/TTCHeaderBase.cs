namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTCHeaderBase
    {
        private byte[] tag;
        private uint version;
        private uint numFonts;
        private uint[] offsetTable;

        public static TTCHeaderBase CreateTCCHeader(TTFStream ttfStream)
        {
            ttfStream.Seek(4);
            return ((ttfStream.ReadULong() == 0x10000) ? new TTCHeaderVer1() : new TTCHeaderVer2());
        }

        public virtual void Read(TTFStream ttfStream)
        {
            ttfStream.Seek(0);
            this.tag = ttfStream.ReadBytes(4);
            this.version = ttfStream.ReadULong();
            this.numFonts = ttfStream.ReadULong();
            this.offsetTable = new uint[this.numFonts];
            for (int i = 0; i < this.numFonts; i++)
            {
                this.offsetTable[i] = ttfStream.ReadULong();
            }
        }

        public uint NumFonts =>
            this.numFonts;

        public uint[] OffsetTable =>
            this.offsetTable;
    }
}

