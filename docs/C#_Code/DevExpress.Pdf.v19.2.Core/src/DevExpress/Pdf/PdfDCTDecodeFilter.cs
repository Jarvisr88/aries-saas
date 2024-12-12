namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDCTDecodeFilter : PdfFilter
    {
        internal const string Name = "DCTDecode";
        internal const string ShortName = "DCT";
        private const string colorTransformDictionaryKey = "ColorTransform";
        private readonly bool colorTransform;

        internal PdfDCTDecodeFilter(PdfReaderDictionary parameters)
        {
            if (parameters != null)
            {
                int? integer = parameters.GetInteger("ColorTransform");
                if (integer != null)
                {
                    int num = integer.Value;
                    if (num == 0)
                    {
                        this.colorTransform = false;
                    }
                    else if (num == 1)
                    {
                        this.colorTransform = true;
                    }
                    else
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
            }
        }

        protected internal override PdfScanlineTransformationResult CreateScanlineSource(PdfImage image, int componentsCount, byte[] data) => 
            new PdfScanlineTransformationResult(PdfDCTDecoder.CreateScanlineSource(RemoveLeadingSpaces(data), image, componentsCount));

        protected internal override byte[] Decode(byte[] data)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        private static byte[] RemoveLeadingSpaces(byte[] data)
        {
            int length = data.Length;
            int index = 0;
            while (true)
            {
                if (index < length)
                {
                    if (PdfObjectParser.IsSpaceSymbol(data[index]))
                    {
                        index++;
                        continue;
                    }
                    if (index != 0)
                    {
                        int num3 = length - index;
                        byte[] destinationArray = new byte[num3];
                        Array.Copy(data, index, destinationArray, 0, num3);
                        return destinationArray;
                    }
                }
                return data;
            }
        }

        protected internal override PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            if (!this.colorTransform)
            {
                return null;
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("ColorTransform", 1);
            return dictionary;
        }

        public bool ColorTransform =>
            this.colorTransform;

        protected internal override string FilterName =>
            "DCTDecode";
    }
}

