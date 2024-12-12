namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDeviceColorSpace : PdfColorSpace
    {
        internal const string GrayName = "DeviceGray";
        internal const string RGBName = "DeviceRGB";
        internal const string CMYKName = "DeviceCMYK";
        private readonly PdfDeviceColorSpaceKind kind;

        public PdfDeviceColorSpace(PdfDeviceColorSpaceKind kind)
        {
            this.kind = kind;
        }

        protected internal override PdfColor AlternateTransform(PdfColor color) => 
            (this.kind != PdfDeviceColorSpaceKind.RGB) ? TransformToRGB(color) : color;

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.Write(collection);

        protected internal override PdfColor Transform(PdfColor color)
        {
            double[] components = color.Components;
            foreach (double num2 in components)
            {
                if ((num2 < 0.0) || (num2 > 1.0))
                {
                    int length = components.Length;
                    double[] numArray3 = new double[length];
                    for (int i = 0; i < length; i++)
                    {
                        double num5 = components[i];
                        numArray3[i] = (num5 >= 0.0) ? ((num5 <= 1.0) ? num5 : 1.0) : 0.0;
                    }
                    return new PdfColor(numArray3);
                }
            }
            return color;
        }

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width)
        {
            switch (this.kind)
            {
                case PdfDeviceColorSpaceKind.Gray:
                    return new PdfScanlineTransformationResult(data, PdfPixelFormat.Gray8bit);

                case PdfDeviceColorSpaceKind.RGB:
                    return new PdfScanlineTransformationResult(data, PdfPixelFormat.Argb24bpp);

                case PdfDeviceColorSpaceKind.CMYK:
                    return new PdfScanlineTransformationResult(new PdfCMYKToRGBImageScanlineSource(data, width), PdfPixelFormat.Argb24bpp);
            }
            return null;
        }

        public static PdfColor TransformToRGB(PdfColor color)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            if (color != null)
            {
                double[] numArray = color.Components;
                switch (numArray.Length)
                {
                    case 1:
                    {
                        double num5 = numArray[0];
                        num = num5;
                        num2 = num5;
                        num3 = num5;
                        break;
                    }
                    case 3:
                        num = numArray[0];
                        num2 = numArray[1];
                        num3 = numArray[2];
                        break;

                    case 4:
                    {
                        double num6 = numArray[0];
                        double num7 = numArray[1];
                        double num8 = numArray[2];
                        double num9 = numArray[3];
                        if (num9 < 1.0)
                        {
                            double num10 = 1.0 - num6;
                            double num11 = 1.0 - num7;
                            double num12 = 1.0 - num8;
                            double num13 = 1.0 - num9;
                            double num14 = num9 / num13;
                            double num15 = ((num10 * num11) * num12) * num13;
                            num15 *= num14;
                            num15 = ((num10 * num11) * num8) * num13;
                            num15 *= num14;
                            num15 = ((num10 * num7) * num12) * num13;
                            num15 = ((num10 * num7) * num8) * num13;
                            num15 = ((num6 * num11) * num12) * num13;
                            num15 *= num14;
                            num15 = ((num6 * num11) * num8) * num13;
                            num15 = ((num6 * num7) * num12) * num13;
                            num15 = ((num6 * num7) * num8) * num13;
                            num = PdfColor.ClipColorComponent(((((((((num15 + (0.1373 * num15)) + num15) + (0.1098 * num15)) + (0.9255 * num15)) + (0.1412 * (num15 * num14))) + (0.9294 * num15)) + (0.1333 * (num15 * num14))) + (0.1804 * num15)) + (0.2118 * num15));
                            num2 = PdfColor.ClipColorComponent((((((((((num15 + (0.1216 * num15)) + (0.949 * num15)) + (0.102 * num15)) + (0.1098 * num15)) + (0.6784 * num15)) + (0.0588 * num15)) + (0.651 * num15)) + (0.0745 * (((num6 * num11) * num8) * num9))) + (0.1922 * num15)) + (0.2119 * num15));
                            num3 = PdfColor.ClipColorComponent(((((((((num15 + (0.1255 * num15)) + (0.549 * num15)) + (0.1412 * num15)) + (0.9373 * num15)) + (0.1412 * num15)) + (0.3137 * num15)) + (0.5725 * num15)) + (0.0078 * (num15 * num14))) + (0.2235 * num15));
                        }
                        break;
                    }
                    default:
                        break;
                }
            }
            double[] components = new double[] { num, num2, num3 };
            return new PdfColor(components);
        }

        protected internal override object Write(PdfObjectCollection collection) => 
            new PdfName(PdfEnumToStringConverter.Convert<PdfDeviceColorSpaceKind>(this.kind, true));

        public PdfDeviceColorSpaceKind Kind =>
            this.kind;

        public override int ComponentsCount
        {
            get
            {
                PdfDeviceColorSpaceKind kind = this.kind;
                return ((kind == PdfDeviceColorSpaceKind.Gray) ? 1 : ((kind == PdfDeviceColorSpaceKind.CMYK) ? 4 : 3));
            }
        }
    }
}

