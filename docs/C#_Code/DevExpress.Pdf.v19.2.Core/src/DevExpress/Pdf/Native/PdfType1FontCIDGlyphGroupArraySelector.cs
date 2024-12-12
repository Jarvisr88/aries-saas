namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1FontCIDGlyphGroupArraySelector : PdfType1FontCIDGlyphGroupSelector
    {
        internal const int Format = 0;
        private readonly byte[] glyphGroupIndices;

        internal PdfType1FontCIDGlyphGroupArraySelector(PdfBinaryStream stream, int cidCount)
        {
            this.glyphGroupIndices = stream.ReadArray(cidCount);
        }

        public override void Write(PdfBinaryStream stream)
        {
            stream.WriteByte(0);
            stream.WriteArray(this.glyphGroupIndices);
        }

        public override byte[] GlyphGroupIndices =>
            this.glyphGroupIndices;

        public override int DataLength =>
            this.glyphGroupIndices.Length + 1;
    }
}

