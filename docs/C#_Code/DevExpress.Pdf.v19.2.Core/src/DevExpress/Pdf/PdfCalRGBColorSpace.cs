namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCalRGBColorSpace : PdfCIEBasedColorSpace
    {
        internal const string TypeName = "CalRGB";
        private const string matrixDictionaryKey = "Matrix";
        private readonly PdfGamma gamma;
        private readonly PdfColorSpaceMatrix matrix;

        private PdfCalRGBColorSpace(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Gamma");
            this.gamma = (array == null) ? new PdfGamma() : new PdfGamma(array);
            IList<object> list2 = dictionary.GetArray("Matrix");
            this.matrix = (list2 == null) ? new PdfColorSpaceMatrix() : new PdfColorSpaceMatrix(list2);
        }

        private static double ColorComponentTransferFunction(double component) => 
            (component > 0.04045) ? Math.Pow((component + 0.055) / 1.055, 2.4) : (component / 12.92);

        internal static PdfCalRGBColorSpace Create(PdfObjectCollection collection, IList<object> array) => 
            new PdfCalRGBColorSpace(ResolveColorSpaceDictionary(collection, array));

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if (!this.gamma.IsDefault)
            {
                dictionary.Add("Gamma", this.gamma.ToArray());
            }
            if (!this.matrix.IsIdentity)
            {
                dictionary.Add("Matrix", this.matrix.ToArray());
            }
            return dictionary;
        }

        protected internal override PdfColor Transform(PdfColor color)
        {
            double num5;
            double num6;
            double num7;
            double[] components = color.Components;
            double component = Math.Pow(components[0], this.gamma.Red);
            double num2 = Math.Pow(components[1], this.gamma.Green);
            double num3 = Math.Pow(components[2], this.gamma.Blue);
            PdfCIEColor whitePoint = base.WhitePoint;
            double z = whitePoint.Z;
            if (!this.matrix.IsIdentity)
            {
                num5 = ((this.matrix.Xa * component) + (this.matrix.Xb * num2)) + (this.matrix.Xc * num3);
                num6 = ((this.matrix.Ya * component) + (this.matrix.Yb * num2)) + (this.matrix.Yc * num3);
                num7 = ((this.matrix.Za * component) + (this.matrix.Zb * num2)) + (this.matrix.Zc * num3);
            }
            else
            {
                component = ColorComponentTransferFunction(component);
                num2 = ColorComponentTransferFunction(num2);
                num3 = ColorComponentTransferFunction(num3);
                PdfCIEColor blackPoint = base.BlackPoint;
                double x = blackPoint.X;
                double y = blackPoint.Y;
                double num10 = blackPoint.Z;
                num5 = PdfColor.ClipColorComponent(((((component * 0.4124) + (num2 * 0.3576)) + (num3 * 0.1805)) - x) / (whitePoint.X - x));
                num6 = PdfColor.ClipColorComponent(((((component * 0.2126) + (num2 * 0.7152)) + (num3 * 0.0722)) - y) / (whitePoint.Y - y));
                num7 = PdfColor.ClipColorComponent(((((component * 0.0193) + (num2 * 0.1192)) + (num3 * 0.9505)) - num10) / (z - num10));
            }
            return PdfColor.FromXYZ(num5, num6, num7, z);
        }

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            new PdfScanlineTransformationResult(new PdfCIEBasedImageScanlineSource(data, this, width, 3), PdfPixelFormat.Argb24bpp);

        public PdfGamma Gamma =>
            this.gamma;

        public PdfColorSpaceMatrix Matrix =>
            this.matrix;

        public override int ComponentsCount =>
            3;

        protected override string Name =>
            "CalRGB";
    }
}

