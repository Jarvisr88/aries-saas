namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;

    public class PdfType1FontFileData
    {
        private const string fontFileDictionaryKey = "FontFile";
        private const string length1DictionaryKey = "Length1";
        private const string length2DictionaryKey = "Length2";
        private const string length3DictionaryKey = "Length3";
        private readonly byte[] data;
        private readonly int plainTextLength;
        private readonly int cipherTextLength;
        private readonly int nullSegmentLength;

        public PdfType1FontFileData(byte[] data, int plainTextLength, int cipherTextLength, int nullSegmentLength)
        {
            this.data = data;
            if (data.Length < ((plainTextLength + cipherTextLength) + nullSegmentLength))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (IsPfbData(data))
            {
                plainTextLength = ((data[2] + (data[3] << 8)) + (data[4] << 0x10)) + (data[5] << 0x18);
                int num2 = plainTextLength + 8;
                cipherTextLength = ((data[num2] + (data[num2 + 1] << 8)) + (data[num2 + 2] << 0x10)) + (data[num2 + 3] << 0x18);
                num2 += cipherTextLength + 6;
                int destinationIndex = plainTextLength + cipherTextLength;
                nullSegmentLength = Math.Min((int) (((data[num2] + (data[num2 + 1] << 8)) + (data[num2 + 2] << 0x10)) + (data[num2 + 3] << 0x18)), (int) ((data.Length - num2) - 4));
                byte[] destinationArray = new byte[destinationIndex + nullSegmentLength];
                Array.Copy(data, 6, destinationArray, 0, plainTextLength);
                Array.Copy(data, plainTextLength + 12, destinationArray, plainTextLength, cipherTextLength);
                Array.Copy(data, destinationIndex + 0x12, destinationArray, destinationIndex, nullSegmentLength);
                this.data = destinationArray;
            }
            string str = Encoding.ASCII.GetString(this.data);
            int index = str.IndexOf("eexec");
            if (index >= 0)
            {
                int num4 = (index + "eexec".Length) + 1;
                int num5 = num4 - plainTextLength;
                if (num5 != 0)
                {
                    cipherTextLength -= num5;
                    plainTextLength = num4;
                }
                if (nullSegmentLength == 0)
                {
                    int num6 = str.LastIndexOf("cleartomark");
                    if (num6 >= 0)
                    {
                        int num7 = num6 - 1;
                        nullSegmentLength = 12;
                        while (true)
                        {
                            if (num7 >= 0)
                            {
                                char ch = str[num7];
                                if ((ch == '0') || PdfObjectParser.IsSpaceSymbol((byte) ch))
                                {
                                    num7--;
                                    nullSegmentLength++;
                                    continue;
                                }
                            }
                            nullSegmentLength--;
                            cipherTextLength -= nullSegmentLength;
                            break;
                        }
                    }
                }
            }
            this.plainTextLength = plainTextLength;
            this.cipherTextLength = cipherTextLength;
            this.nullSegmentLength = nullSegmentLength;
        }

        public static bool IsPfbData(byte[] data) => 
            (data.Length >= 2) && ((data[0] == 0x80) && (data[1] == 1));

        public static PdfType1FontFileData Parse(PdfReaderDictionary dictionary)
        {
            PdfReaderStream stream = dictionary.GetStream("FontFile");
            if (stream == null)
            {
                return null;
            }
            PdfReaderDictionary dictionary2 = stream.Dictionary;
            int? integer = dictionary2.GetInteger("Length1");
            int? nullable2 = dictionary2.GetInteger("Length2");
            int? nullable3 = dictionary2.GetInteger("Length3");
            if ((integer == null) || ((nullable2 == null) || (nullable3 == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new PdfType1FontFileData(stream.UncompressedData, integer.Value, nullable2.Value, nullable3.Value);
        }

        public void Write(PdfWriterDictionary dictionary)
        {
            PdfObjectCollection objects = dictionary.Objects;
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.Add("Length1", this.plainTextLength);
            dictionary2.Add("Length2", this.cipherTextLength);
            dictionary2.Add("Length3", this.nullSegmentLength);
            dictionary.Add("FontFile", objects.AddStream(dictionary2, this.data));
        }

        public byte[] Data =>
            this.data;

        public int PlainTextLength =>
            this.plainTextLength;

        public int CipherTextLength =>
            this.cipherTextLength;

        public int NullSegmentLength =>
            this.nullSegmentLength;
    }
}

