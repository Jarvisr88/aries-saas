namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1FontCIDGlyphGroupSelector
    {
        protected PdfType1FontCIDGlyphGroupSelector()
        {
        }

        public static PdfType1FontCIDGlyphGroupSelector Parse(PdfBinaryStream stream, int cidCount)
        {
            byte num = stream.ReadByte();
            if (num == 0)
            {
                return new PdfType1FontCIDGlyphGroupArraySelector(stream, cidCount);
            }
            if (num == 3)
            {
                return new PdfType1FontCIDGlyphGroupRangeSelector(stream, cidCount);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public abstract void Write(PdfBinaryStream stream);

        public abstract byte[] GlyphGroupIndices { get; }

        public abstract int DataLength { get; }
    }
}

