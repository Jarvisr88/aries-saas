namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1FontEexecCipher : PdfType1FontCipher
    {
        private const int kindBytesCount = 4;
        private readonly int len4;

        protected PdfType1FontEexecCipher(byte[] data) : base(data)
        {
            this.len4 = 4;
        }

        protected PdfType1FontEexecCipher(byte[] data, int startPosition, int dataLength, int len4) : base(data, startPosition, dataLength)
        {
            this.len4 = len4;
        }

        public static PdfType1FontEexecCipher Create(byte[] data, int startPosition, int dataLength) => 
            Create(data, startPosition, dataLength, 4);

        public static PdfType1FontEexecCipher Create(byte[] data, int startPosition, int dataLength, int lenIV) => 
            (dataLength >= lenIV) ? (IsASCIICipher(data, startPosition) ? ((PdfType1FontEexecCipher) new PdfType1FontEexecASCIICipher(data, startPosition, dataLength, lenIV)) : ((PdfType1FontEexecCipher) new PdfType1FontEexecBinaryCipher(data, startPosition, dataLength, lenIV))) : null;

        public static byte[] DecryptCharstring(byte[] charstring, int lenIV)
        {
            PdfType1FontEexecCipher cipher = Create(charstring, 0, charstring.Length, lenIV);
            if (cipher == null)
            {
                return null;
            }
            cipher.R = 0x10ea;
            return cipher.Decrypt();
        }

        public static bool IsASCIICipher(byte[] data, int startPosition)
        {
            if (!IsASCIISymbol(data[startPosition]))
            {
                return false;
            }
            int num = 1;
            int num2 = startPosition + 1;
            while (num < 4)
            {
                if (!IsASCIISymbol(data[num2++]))
                {
                    return false;
                }
                num++;
            }
            return true;
        }

        protected static bool IsASCIISymbol(byte c) => 
            PdfObjectParser.IsSpaceSymbol(c) || PdfObjectParser.IsHexadecimalDigitSymbol(c);

        protected override int SkipBytesCount =>
            this.len4;

        protected override int InitialR =>
            0xd971;
    }
}

