namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfIndexedColorSpace : PdfCustomColorSpace
    {
        internal const string TypeName = "Indexed";
        private readonly PdfColorSpace baseColorSpace;
        private readonly int maxIndex;
        private readonly byte[] lookupTable;

        internal PdfIndexedColorSpace(PdfObjectCollection objects, IList<object> array)
        {
            if (array.Count != 4)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.baseColorSpace = (objects == null) ? Parse(null, array[1]) : objects.GetColorSpace(array[1]);
            object obj2 = array[2];
            if (objects != null)
            {
                obj2 = objects.TryResolve(obj2, null);
            }
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.maxIndex = (int) obj2;
            if ((this.maxIndex < 0) || (this.maxIndex > 0xff))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            obj2 = array[3];
            if (objects != null)
            {
                obj2 = objects.TryResolve(obj2, null);
            }
            this.lookupTable = obj2 as byte[];
            if (this.lookupTable == null)
            {
                PdfReaderStream stream = obj2 as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.lookupTable = stream.UncompressedData;
                if (this.lookupTable == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            int newSize = this.baseColorSpace.ComponentsCount * (this.maxIndex + 1);
            if (this.lookupTable.Length != newSize)
            {
                Array.Resize<byte>(ref this.lookupTable, newSize);
            }
        }

        internal PdfIndexedColorSpace(PdfDeviceColorSpace baseColorSpace, int maxIndex, byte[] lookupTable)
        {
            this.baseColorSpace = baseColorSpace;
            this.maxIndex = maxIndex;
            this.lookupTable = lookupTable;
        }

        protected internal override PdfRange[] CreateDefaultDecodeArray(int bitsPerComponent) => 
            new PdfRange[] { new PdfRange(0.0, (double) ((1 << (bitsPerComponent & 0x1f)) - 1)) };

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            new object[] { new PdfName("Indexed"), this.baseColorSpace.Write(collection), this.maxIndex, this.lookupTable };

        protected internal override PdfColor Transform(PdfColor color)
        {
            double[] components = color.Components;
            if (components.Length != 1)
            {
                return color;
            }
            int componentsCount = this.baseColorSpace.ComponentsCount;
            double[] numArray2 = new double[componentsCount];
            int index = 0;
            for (int i = PdfMathUtils.ToInt32(components[0]) * componentsCount; index < componentsCount; i++)
            {
                numArray2[index] = ((double) this.lookupTable[i]) / 255.0;
                index++;
            }
            return this.baseColorSpace.Transform(new PdfColor(color.Pattern, numArray2));
        }

        protected internal override PdfScanlineTransformationResult Transform(PdfImage image, IPdfImageScanlineSource data, PdfImageParameters parameters)
        {
            IPdfImageScanlineSource interpolatedScanlineSource = PdfImageScanlineSourceFactory.CreateIndexedScanlineSource(this.GetDecodedImageScanlineSource(data, image, image.Width), image.Width, image.Height, image.BitsPerComponent, this.lookupTable, this.baseColorSpace.ComponentsCount);
            interpolatedScanlineSource = image.GetInterpolatedScanlineSource(interpolatedScanlineSource, parameters);
            return this.baseColorSpace.Transform(interpolatedScanlineSource, parameters.Width);
        }

        public PdfColorSpace BaseColorSpace =>
            this.baseColorSpace;

        public int MaxIndex =>
            this.maxIndex;

        public byte[] LookupTable =>
            this.lookupTable;

        public override int ComponentsCount =>
            1;
    }
}

