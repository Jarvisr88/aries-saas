namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFOS2 : TTFTable
    {
        private short fsType;
        private TTFPanose panose;

        public TTFOS2(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            throw new TTFFileException("Not supported");
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            ttfStream.Move(8);
            this.fsType = ttfStream.ReadShort();
            ttfStream.Move(0x16);
            this.panose = ttfStream.ReadPanose();
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            throw new TTFFileException("Not supported");
        }

        public static int SizeOf =>
            0x56;

        public TTFPanose Panose =>
            this.panose;

        public short FsType =>
            this.fsType;

        public override int Length =>
            SizeOf;

        protected internal override string Tag =>
            "OS/2";
    }
}

