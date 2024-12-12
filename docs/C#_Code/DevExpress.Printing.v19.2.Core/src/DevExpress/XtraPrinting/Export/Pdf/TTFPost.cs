namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFPost : TTFTable
    {
        private byte[] formatType;
        private byte[] italicAngle;

        public TTFPost(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            throw new TTFFileException("Not supported");
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.formatType = ttfStream.ReadBytes(4);
            this.italicAngle = ttfStream.ReadBytes(4);
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            throw new TTFFileException("Not supported");
        }

        public float ItalicAngle =>
            TTFStream.FixedToFloat(this.italicAngle);

        public float FormatType =>
            TTFStream.FixedToFloat(this.formatType);

        public override int Length
        {
            get
            {
                throw new TTFFileException("Not supported");
            }
        }

        protected internal override string Tag =>
            "post";
    }
}

