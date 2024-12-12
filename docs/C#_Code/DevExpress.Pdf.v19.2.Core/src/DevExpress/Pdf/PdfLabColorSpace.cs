namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLabColorSpace : PdfCIEBasedColorSpace
    {
        internal const string TypeName = "Lab";
        private const string rangeDictionaryKey = "Range";
        private const double min = -100.0;
        private const double max = 100.0;
        private const double sixDivTwentyNine = 0.20689655172413793;
        private const double fourDivTwentyNine = 0.13793103448275862;
        private const double oneHundredEightDivEightHundredFortyOne = 0.12841854934601665;
        private readonly PdfRange rangeA;
        private readonly PdfRange rangeB;

        internal PdfLabColorSpace(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Range");
            if (array == null)
            {
                this.rangeA = new PdfRange(-100.0, 100.0);
                this.rangeB = new PdfRange(-100.0, 100.0);
            }
            else
            {
                if (array.Count != 4)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                double min = PdfDocumentReader.ConvertToDouble(array[0]);
                double max = PdfDocumentReader.ConvertToDouble(array[1]);
                double num3 = PdfDocumentReader.ConvertToDouble(array[2]);
                double num4 = PdfDocumentReader.ConvertToDouble(array[3]);
                if ((max < min) || (num4 < num3))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.rangeA = new PdfRange(min, max);
                this.rangeB = new PdfRange(num3, num4);
            }
        }

        private static double CorrectRange(PdfRange range, double value)
        {
            double min = range.Min;
            return ((((value - min) / (range.Max - min)) * 200.0) - 100.0);
        }

        internal static PdfLabColorSpace Create(PdfObjectCollection collection, IList<object> array) => 
            new PdfLabColorSpace(ResolveColorSpaceDictionary(collection, array));

        protected internal override PdfRange[] CreateDefaultDecodeArray(int bitsPerComponent) => 
            new PdfRange[] { new PdfRange(0.0, 100.0), new PdfRange(this.rangeA.Min, this.rangeA.Max), new PdfRange(this.rangeB.Min, this.rangeB.Max) };

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            double min = this.rangeA.Min;
            double max = this.rangeA.Max;
            double num3 = this.rangeB.Min;
            double num4 = this.rangeB.Max;
            if ((min != -100.0) || ((max != 100.0) || ((num3 != -100.0) || (num4 != 100.0))))
            {
                double[] numArray1 = new double[] { min, max, num3, num4 };
                dictionary.Add("Range", numArray1);
            }
            return dictionary;
        }

        private static double GammaFunction(double x) => 
            (x >= 0.20689655172413793) ? ((x * x) * x) : ((x - 0.13793103448275862) * 0.12841854934601665);

        protected override IPdfImageScanlineSource GetDecodedImageScanlineSource(IPdfImageScanlineSource decoratingSource, PdfImage image, int width) => 
            new PdfLabColorSpaceImageScanlineSource(decoratingSource, this, image.Decode, width);

        protected internal override PdfColor Transform(PdfColor color)
        {
            double[] components = color.Components;
            double x = (components[0] + 16.0) / 116.0;
            PdfCIEColor whitePoint = base.WhitePoint;
            double z = whitePoint.Z;
            PdfCIEColor blackPoint = base.BlackPoint;
            double num3 = blackPoint.X;
            double y = blackPoint.Y;
            double num5 = blackPoint.Z;
            return PdfColor.FromXYZ(num3 + ((whitePoint.X - num3) * GammaFunction(x + (CorrectRange(this.RangeA, components[1]) / 500.0))), y + ((whitePoint.Y - y) * GammaFunction(x)), num5 + ((z - num5) * GammaFunction(x - (CorrectRange(this.RangeB, components[2]) / 200.0))), z);
        }

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            new PdfScanlineTransformationResult(data, PdfPixelFormat.Argb24bpp);

        public PdfRange RangeA =>
            this.rangeA;

        public PdfRange RangeB =>
            this.rangeB;

        public override int ComponentsCount =>
            3;

        protected override string Name =>
            "Lab";
    }
}

