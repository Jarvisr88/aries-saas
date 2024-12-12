namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfMeshShading : PdfShading
    {
        private const string bitsPerCoordinateDictionaryKey = "BitsPerCoordinate";
        private const string bitsPerComponentDictionaryKey = "BitsPerComponent";
        private const string bitsPerFlagDictionaryKey = "BitsPerFlag";
        private const string decodeDictionaryKey = "Decode";
        private static readonly List<int> validBitsPerCoordinate;
        private static readonly List<int> validBitsPerComponent;
        private static readonly List<int> validBitsPerFlag;
        private readonly int bitsPerFlag;
        private readonly int bitsPerCoordinate;
        private readonly int bitsPerComponent;
        private readonly PdfDecodeRange decodeX;
        private readonly PdfDecodeRange decodeY;
        private readonly PdfDecodeRange[] decodeC;
        private readonly byte[] data;

        static PdfMeshShading()
        {
            List<int> list1 = new List<int>();
            list1.Add(1);
            list1.Add(2);
            list1.Add(4);
            list1.Add(8);
            list1.Add(12);
            list1.Add(0x10);
            list1.Add(0x18);
            list1.Add(0x20);
            validBitsPerCoordinate = list1;
            List<int> list2 = new List<int>();
            list2.Add(1);
            list2.Add(2);
            list2.Add(4);
            list2.Add(8);
            list2.Add(12);
            list2.Add(0x10);
            validBitsPerComponent = list2;
            List<int> list3 = new List<int>();
            list3.Add(2);
            list3.Add(4);
            list3.Add(8);
            validBitsPerFlag = list3;
        }

        protected PdfMeshShading(PdfReaderStream stream) : base(stream.Dictionary)
        {
            if (this.HasBitsPerFlag)
            {
                int? nullable3 = stream.Dictionary.GetInteger("BitsPerFlag");
                if (nullable3 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.bitsPerFlag = nullable3.Value;
                if (!validBitsPerFlag.Contains(this.bitsPerFlag))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            int num = (base.Function == null) ? base.ColorSpace.ComponentsCount : 1;
            PdfReaderDictionary dictionary = stream.Dictionary;
            int? integer = dictionary.GetInteger("BitsPerCoordinate");
            int? nullable2 = dictionary.GetInteger("BitsPerComponent");
            IList<object> array = dictionary.GetArray("Decode");
            if ((integer == null) || ((nullable2 == null) || ((array == null) || (array.Count != ((num * 2) + 4)))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.bitsPerCoordinate = integer.Value;
            this.bitsPerComponent = nullable2.Value;
            if (!validBitsPerCoordinate.Contains(this.bitsPerCoordinate) || !validBitsPerComponent.Contains(this.bitsPerComponent))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.decodeX = new PdfDecodeRange(PdfDocumentReader.ConvertToDouble(array[0]), PdfDocumentReader.ConvertToDouble(array[1]), this.bitsPerCoordinate);
            this.decodeY = new PdfDecodeRange(PdfDocumentReader.ConvertToDouble(array[2]), PdfDocumentReader.ConvertToDouble(array[3]), this.bitsPerCoordinate);
            this.decodeC = new PdfDecodeRange[num];
            int index = 0;
            int num3 = 4;
            while (index < num)
            {
                this.decodeC[index] = new PdfDecodeRange(PdfDocumentReader.ConvertToDouble(array[num3++]), PdfDocumentReader.ConvertToDouble(array[num3++]), this.bitsPerComponent);
                index++;
            }
            this.data = stream.UncompressedData;
        }

        protected PdfMeshShading(PdfObjectList<PdfCustomFunction> functions, int bitsPerFlag, int bitsPerCoordinate, int bitsPerComponent, PdfDecodeRange decodeX, PdfDecodeRange decodeY, PdfDecodeRange[] decodeC) : base(functions)
        {
            this.bitsPerFlag = bitsPerFlag;
            this.bitsPerCoordinate = bitsPerCoordinate;
            this.bitsPerComponent = bitsPerComponent;
            this.decodeX = decodeX;
            this.decodeY = decodeY;
            this.decodeC = decodeC;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("BitsPerCoordinate", this.bitsPerCoordinate);
            dictionary.Add("BitsPerComponent", this.bitsPerComponent);
            if (this.HasBitsPerFlag)
            {
                dictionary.Add("BitsPerFlag", this.bitsPerFlag);
            }
            int length = this.decodeC.Length;
            object[] objArray = new object[] { this.decodeX.Min, this.decodeX.Max, this.decodeY.Min, this.decodeY.Max };
            int index = 0;
            int num3 = 4;
            while (index < length)
            {
                PdfDecodeRange range = this.decodeC[index];
                objArray[num3++] = range.Min;
                objArray[num3++] = range.Max;
                index++;
            }
            dictionary.Add("Decode", objArray);
            return dictionary;
        }

        protected PdfIntegerStreamReader CreateIntegerStreamReader() => 
            new PdfIntegerStreamReader(this.bitsPerFlag, this.bitsPerCoordinate, this.bitsPerComponent, this.decodeX, this.decodeY, this.decodeC, this.data);

        protected virtual byte[] GetData() => 
            this.data;

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(this.CreateDictionary(objects), this.GetData());

        internal int BitsPerFlag =>
            this.bitsPerFlag;

        internal int BitsPerCoordinate =>
            this.bitsPerCoordinate;

        internal int BitsPerComponent =>
            this.bitsPerComponent;

        internal PdfDecodeRange DecodeX =>
            this.decodeX;

        internal PdfDecodeRange DecodeY =>
            this.decodeY;

        internal PdfDecodeRange[] DecodeC =>
            this.decodeC;

        protected byte[] Data =>
            this.data;

        protected override bool IsFunctionRequired =>
            false;

        protected virtual bool HasBitsPerFlag =>
            false;
    }
}

