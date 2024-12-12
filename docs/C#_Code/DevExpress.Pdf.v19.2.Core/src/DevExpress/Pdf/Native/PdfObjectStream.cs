namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfObjectStream : PdfDocumentItem
    {
        private readonly List<object> objects;

        public PdfObjectStream(PdfReaderStream stream) : base(stream.Dictionary.Number, stream.Dictionary.Generation)
        {
            PdfReaderDictionary dictionary = stream.Dictionary;
            int? integer = dictionary.GetInteger("N");
            int? nullable2 = dictionary.GetInteger("First");
            if ((integer == null) || (nullable2 == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int capacity = integer.Value;
            int num2 = nullable2.Value;
            if ((capacity <= 0) || (num2 < 0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
            using (PdfDataStream stream2 = new PdfArrayDataStream(stream.UncompressedData))
            {
                PdfDocumentParser parser = new PdfDocumentParser(dictionary.Objects, base.ObjectNumber, base.ObjectGeneration, stream2, 0);
                int num3 = 0;
                while (true)
                {
                    if (num3 >= capacity)
                    {
                        this.objects = new List<object>(capacity);
                        foreach (KeyValuePair<int, int> pair in dictionary2)
                        {
                            this.objects.Add(PdfDocumentParser.ParseObject(dictionary.Objects, pair.Key, 0, stream2, num2 + pair.Value));
                        }
                        break;
                    }
                    dictionary2.Add(parser.ReadInteger(), parser.ReadInteger());
                    num3++;
                }
            }
        }

        public IList<object> Objects =>
            this.objects;
    }
}

