namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1FontEexecASCIICipher : PdfType1FontEexecCipher
    {
        public PdfType1FontEexecASCIICipher(byte[] data, int startPosition, int dataLength, int lenIV) : base(data, startPosition, dataLength, lenIV)
        {
        }

        private short ActualNextByte()
        {
            while (true)
            {
                short num = base.NextByte();
                if ((num < 0) || !PdfObjectParser.IsSpaceSymbol((byte) num))
                {
                    return num;
                }
            }
        }

        protected override short NextChar()
        {
            short num = this.ActualNextByte();
            short num2 = this.ActualNextByte();
            return (((num < 0) || (num2 < 0)) ? -1 : ((short) ((PdfObjectParser.ConvertToHexadecimalDigit((byte) num) << 4) + PdfObjectParser.ConvertToHexadecimalDigit((byte) num2))));
        }

        protected override int BytesPerResultByte =>
            2;
    }
}

