namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfToUnicodeCMapStreamParser : PdfCMapStreamParser<string>
    {
        private PdfToUnicodeCMapStreamParser(byte[] data) : base(data)
        {
        }

        protected override string GetCIDFromArray(byte[] bytes) => 
            GetUnicodeValues(bytes);

        protected override string GetCIDFromValue(int code) => 
            new string((char) code, 1);

        private static string GetUnicodeValues(byte[] bytes)
        {
            if (bytes == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int length = bytes.Length;
            bool flag = (length % 2) != 0;
            length = flag ? ((length + 1) / 2) : (length / 2);
            char[] chArray = new char[length];
            int index = 0;
            int num3 = 0;
            while (index < length)
            {
                chArray[index] = (!flag || (index != 0)) ? ((char) ((bytes[num3++] << 8) + bytes[num3++])) : ((char) bytes[num3++]);
                index++;
            }
            return new string(chArray);
        }

        protected override unsafe string Increment(string cid)
        {
            char[] chArray = cid.ToCharArray();
            char* chPtr1 = &(chArray[chArray.Length - 1]);
            chPtr1[0] = (char) (chPtr1[0] + '\x0001');
            return new string(chArray);
        }

        public static IDictionary<byte[], string> Parse(byte[] data) => 
            new PdfToUnicodeCMapStreamParser(data).Parse();
    }
}

