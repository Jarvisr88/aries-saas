namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfASCII85DecodeFilter : PdfFilter
    {
        internal const string Name = "ASCII85Decode";
        internal const string ShortName = "A85";
        private const int bufferSize = 5;
        private const long maxAllowedValue = 0xffffffffL;
        private const long multiplier1 = 0x55L;
        private const long multiplier2 = 0x1c39L;
        private const long multiplier3 = 0x95eedL;
        private const long multiplier4 = 0x31c84b1L;
        private const byte endBracket = 0x3e;
        private static readonly byte[] endToken = new byte[] { 0x7e, 0x3e };

        internal PdfASCII85DecodeFilter()
        {
        }

        protected internal override byte[] Decode(byte[] data)
        {
            int length = data.Length;
            if (length == 0)
            {
                return data;
            }
            byte[] buffer = new byte[5];
            List<byte> result = new List<byte>();
            int num2 = 0;
            int index = 0;
            bool flag = false;
            while (true)
            {
                if (num2 < length)
                {
                    byte symbol = data[num2++];
                    if (IsSpaceSymbol(symbol))
                    {
                        continue;
                    }
                    if (symbol != 0x7e)
                    {
                        if (symbol == 0x7a)
                        {
                            if (index != 0)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                result.Add(0);
                            }
                            continue;
                        }
                        if (((symbol < 0x21) || (symbol > 0x75)) | flag)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        buffer[index] = symbol;
                        if (++index == 5)
                        {
                            DecodeBuffer(result, buffer, 4);
                            index = 0;
                        }
                        continue;
                    }
                    while (num2 < length)
                    {
                        symbol = data[num2++];
                        if (symbol == 0x3e)
                        {
                            break;
                        }
                        if (!IsSpaceSymbol(symbol))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
                if (index > 0)
                {
                    int num6 = index;
                    while (true)
                    {
                        if (num6 >= 5)
                        {
                            DecodeBuffer(result, buffer, index - 1);
                            break;
                        }
                        buffer[num6] = 0x75;
                        num6++;
                    }
                }
                return result.ToArray();
            }
        }

        private static void DecodeBuffer(List<byte> result, byte[] buffer, int count)
        {
            long num = ((((((buffer[0] - 0x21) * 0x31c84b1L) + ((buffer[1] - 0x21) * 0x95eedL)) + ((buffer[2] - 0x21) * 0x1c39L)) + ((buffer[3] - 0x21) * 0x55)) + buffer[4]) - 0x21;
            if (num > 0xffffffffUL)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            result.Add((byte) ((num & 0xff000000UL) >> 0x18));
            if (--count > 0)
            {
                result.Add((byte) ((num & 0xff0000L) >> 0x10));
                if (--count > 0)
                {
                    result.Add((byte) ((num & 0xff00L) >> 8));
                    if (--count > 0)
                    {
                        result.Add((byte) (num & 0xffL));
                    }
                }
            }
        }

        private static bool IsSpaceSymbol(byte symbol) => 
            (symbol == 10) || ((symbol == 13) || ((symbol == 0) || ((symbol == 9) || ((symbol == 12) || (symbol == 0x20)))));

        protected internal override string FilterName =>
            "ASCII85Decode";

        internal override byte[] EodToken =>
            endToken;
    }
}

