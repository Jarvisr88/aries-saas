namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfColor
    {
        private readonly PdfPattern pattern;
        private readonly double[] components;

        internal PdfColor(IList<double> components)
        {
            this.components = new double[components.Count];
            components.CopyTo(this.components, 0);
        }

        public PdfColor(params double[] components) : this(null, components)
        {
        }

        internal PdfColor(PdfRGBColor rgbColor)
        {
            double[] numArray2;
            if (rgbColor == null)
            {
                numArray2 = new double[3];
            }
            else
            {
                numArray2 = new double[] { rgbColor.R, rgbColor.G, rgbColor.B };
            }
            this.components = numArray2;
        }

        public PdfColor(PdfPattern pattern, params double[] components)
        {
            if ((pattern == null) && (components.Length == 0))
            {
                throw new ArgumentOutOfRangeException("components", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgZeroColorComponentsCount));
            }
            this.pattern = pattern;
            this.components = components;
        }

        internal static double ClipColorComponent(double component) => 
            PdfMathUtils.Min(1.0, PdfMathUtils.Max(0.0, component));

        private static double ColorComponentTransferFunction(double component)
        {
            component = ClipColorComponent(component);
            return ClipColorComponent((component > 0.0031308) ? ((Math.Pow(component, 0.41666666666666669) * 1.055) - 0.055) : (component * 12.92));
        }

        internal static PdfColor FromXYZ(double x, double y, double z, double whitePointZ)
        {
            double num;
            double num2;
            double num3;
            if (whitePointZ < 1.0)
            {
                num = ((x * 3.1339) + (y * -1.617)) + (z * -0.4906);
                num2 = ((x * -0.9785) + (y * 1.916)) + (z * 0.0333);
                num3 = ((x * 0.072) + (y * -0.229)) + (z * 1.4057);
            }
            else
            {
                num = ((x * 3.2406) + (y * -1.5372)) + (z * -0.4986);
                num2 = ((x * -0.9689) + (y * 1.8758)) + (z * 0.0415);
                num3 = ((x * 0.0557) + (y * -0.204)) + (z * 1.057);
            }
            double[] components = new double[] { ColorComponentTransferFunction(num), ColorComponentTransferFunction(num2), ColorComponentTransferFunction(num3) };
            return new PdfColor(components);
        }

        internal object ToWritableObject() => 
            new PdfWritableDoubleArray(this.components);

        public PdfPattern Pattern =>
            this.pattern;

        public double[] Components =>
            this.components;
    }
}

