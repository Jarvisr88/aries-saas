namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfType1FontCharset
    {
        protected PdfType1FontCharset()
        {
        }

        public static PdfType1FontCharset Parse(PdfBinaryStream stream, int size)
        {
            switch (stream.ReadByte())
            {
                case 0:
                    return new PdfType1FontArrayCharset(stream, size);

                case 1:
                    return new PdfType1FontByteRangeCharset(stream, size);

                case 2:
                    return new PdfType1FontWordRangeCharset(stream, size);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public abstract void Write(PdfBinaryStream stream);

        public virtual bool IsDefault =>
            false;

        public virtual int Offset =>
            0;

        public abstract IDictionary<short, short> SidToGidMapping { get; }

        public abstract int DataLength { get; }
    }
}

