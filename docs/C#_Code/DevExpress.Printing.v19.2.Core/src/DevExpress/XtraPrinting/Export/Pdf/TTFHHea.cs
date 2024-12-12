namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFHHea : TTFTable
    {
        private byte[] tableVersion;
        private short ascender;
        private short descender;
        private short lineGap;
        private ushort advanceWidthMax;
        private short minLeftSideBearing;
        private short minRightSideBearing;
        private short xMaxExtent;
        private short caretSlopeRise;
        private short caretSlopeRun;
        private byte[] reserved;
        private short metricDataFormat;
        private ushort numberOfHMetrics;

        public TTFHHea(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFHHea hea = pattern as TTFHHea;
            this.tableVersion = new byte[hea.tableVersion.Length];
            hea.tableVersion.CopyTo(this.tableVersion, 0);
            this.ascender = hea.ascender;
            this.descender = hea.descender;
            this.lineGap = hea.lineGap;
            this.advanceWidthMax = hea.advanceWidthMax;
            this.minLeftSideBearing = hea.minLeftSideBearing;
            this.minRightSideBearing = hea.minRightSideBearing;
            this.xMaxExtent = hea.xMaxExtent;
            this.caretSlopeRise = hea.caretSlopeRise;
            this.caretSlopeRun = hea.caretSlopeRun;
            this.reserved = new byte[hea.reserved.Length];
            hea.reserved.CopyTo(this.reserved, 0);
            this.metricDataFormat = hea.metricDataFormat;
            this.numberOfHMetrics = hea.numberOfHMetrics;
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.tableVersion = ttfStream.ReadBytes(4);
            this.ascender = ttfStream.ReadFWord();
            this.descender = ttfStream.ReadFWord();
            this.lineGap = ttfStream.ReadFWord();
            this.advanceWidthMax = ttfStream.ReadUFWord();
            this.minLeftSideBearing = ttfStream.ReadFWord();
            this.minRightSideBearing = ttfStream.ReadFWord();
            this.xMaxExtent = ttfStream.ReadFWord();
            this.caretSlopeRise = ttfStream.ReadShort();
            this.caretSlopeRun = ttfStream.ReadShort();
            this.reserved = ttfStream.ReadBytes(10);
            this.metricDataFormat = ttfStream.ReadShort();
            this.numberOfHMetrics = ttfStream.ReadUShort();
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.tableVersion);
            ttfStream.WriteFWord(this.ascender);
            ttfStream.WriteFWord(this.descender);
            ttfStream.WriteFWord(this.lineGap);
            ttfStream.WriteUFWord(this.advanceWidthMax);
            ttfStream.WriteFWord(this.minLeftSideBearing);
            ttfStream.WriteFWord(this.minRightSideBearing);
            ttfStream.WriteFWord(this.xMaxExtent);
            ttfStream.WriteShort(this.caretSlopeRise);
            ttfStream.WriteShort(this.caretSlopeRun);
            ttfStream.WriteBytes(this.reserved);
            ttfStream.WriteShort(this.metricDataFormat);
            ttfStream.WriteUShort(this.numberOfHMetrics);
        }

        protected internal override string Tag =>
            "hhea";

        public short Ascender =>
            this.ascender;

        public short Descender =>
            this.descender;

        public int NumberOfHMetrics =>
            this.numberOfHMetrics;

        public override int Length =>
            SizeOf;

        public static int SizeOf =>
            0x24;
    }
}

