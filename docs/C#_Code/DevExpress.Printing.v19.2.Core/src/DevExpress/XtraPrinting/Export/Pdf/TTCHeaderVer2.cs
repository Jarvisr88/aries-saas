namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTCHeaderVer2 : TTCHeaderVer1
    {
        private uint ulDsigTag;
        private uint ulDsigLength;
        private uint ulDsigOffset;

        public override void Read(TTFStream ttfStream)
        {
            base.Read(ttfStream);
            this.ulDsigTag = ttfStream.ReadULong();
            this.ulDsigLength = ttfStream.ReadULong();
            this.ulDsigOffset = ttfStream.ReadULong();
        }
    }
}

