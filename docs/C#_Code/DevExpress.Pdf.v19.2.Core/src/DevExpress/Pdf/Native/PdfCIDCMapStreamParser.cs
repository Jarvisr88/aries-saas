namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCIDCMapStreamParser : PdfCMapStreamParser<short>
    {
        private PdfCIDCMapStreamParser(byte[] data) : base(data)
        {
        }

        protected override short GetCIDFromArray(byte[] bytes)
        {
            if ((bytes == null) || (bytes.Length == 0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return ((bytes.Length != 1) ? ((short) ((bytes[0] << 8) + bytes[1])) : bytes[0]);
        }

        protected override short GetCIDFromValue(int code) => 
            (short) code;

        protected override short Increment(short cid)
        {
            short num1 = cid;
            cid = (short) (num1 + 1);
            return num1;
        }

        public static IDictionary<byte[], short> Parse(byte[] data) => 
            new PdfCIDCMapStreamParser(data).Parse();
    }
}

