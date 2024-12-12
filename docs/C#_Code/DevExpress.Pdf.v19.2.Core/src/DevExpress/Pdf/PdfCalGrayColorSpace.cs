namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCalGrayColorSpace : PdfCIEBasedColorSpace
    {
        internal const string TypeName = "CalGray";
        private const double oneThird = 0.33333333333333331;
        private readonly double gamma;

        private PdfCalGrayColorSpace(PdfReaderDictionary dictionary) : base(dictionary)
        {
            double? number = dictionary.GetNumber("Gamma");
            this.gamma = (number != null) ? number.GetValueOrDefault() : 1.0;
            if (this.gamma <= 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        internal static PdfCalGrayColorSpace Create(PdfObjectCollection collection, IList<object> array) => 
            new PdfCalGrayColorSpace(ResolveColorSpaceDictionary(collection, array));

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("Gamma", this.gamma, 1.0);
            return dictionary;
        }

        protected internal override PdfColor Transform(PdfColor color)
        {
            double y = base.BlackPoint.Y;
            double num2 = PdfMathUtils.Max((116.0 * Math.Pow(y + ((base.WhitePoint.Y - y) * Math.Pow(color.Components[0], this.gamma)), 0.33333333333333331)) - 16.0, 0.0) / 100.0;
            double[] components = new double[] { num2, num2, num2 };
            return new PdfColor(components);
        }

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            new PdfScanlineTransformationResult(new PdfCIEBasedImageScanlineSource(data, this, width, 1), PdfPixelFormat.Argb24bpp);

        public double Gamma =>
            this.gamma;

        public override int ComponentsCount =>
            1;

        protected override string Name =>
            "CalGray";
    }
}

