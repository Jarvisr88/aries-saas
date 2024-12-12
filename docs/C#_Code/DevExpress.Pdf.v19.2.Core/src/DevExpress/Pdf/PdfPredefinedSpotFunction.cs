namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPredefinedSpotFunction : PdfFunction
    {
        private const double doublePi = 6.2831853071795862;
        private readonly PdfPredefinedSpotFunctionKind kind;

        public PdfPredefinedSpotFunction(PdfPredefinedSpotFunctionKind kind)
        {
            this.kind = kind;
        }

        internal PdfPredefinedSpotFunction(string name)
        {
            this.kind = PdfEnumToStringConverter.Parse<PdfPredefinedSpotFunctionKind>(name, false);
        }

        protected internal override bool IsSame(PdfFunction function)
        {
            PdfPredefinedSpotFunction function2 = function as PdfPredefinedSpotFunction;
            return ((function2 != null) && (this.kind == function2.kind));
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.Write(objects);

        protected internal override double[] Transform(double[] arguments)
        {
            double num = 0.0;
            if (arguments.Length >= 2)
            {
                double num2 = arguments[0];
                double num3 = arguments[1];
                switch (this.kind)
                {
                    case PdfPredefinedSpotFunctionKind.SimpleDot:
                        num = 1.0 - ((num2 * num2) + (num3 * num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.InvertedSimpleDot:
                        num = ((num2 * num2) + (num3 * num3)) - 1.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.DoubleDot:
                        num = (Math.Sin(6.2831853071795862 * num2) + Math.Sin(6.2831853071795862 * num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.InvertedDoubleDot:
                        num = -(Math.Sin(6.2831853071795862 * num2) + Math.Sin(6.2831853071795862 * num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.CosineDot:
                        num = (Math.Cos(3.1415926535897931 * num2) + Math.Cos(3.1415926535897931 * num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.Double:
                        num = (Math.Sin(3.1415926535897931 * num2) + Math.Sin(6.2831853071795862 * num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.InvertedDouble:
                        num = -(Math.Sin(3.1415926535897931 * num2) + Math.Sin(6.2831853071795862 * num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.Line:
                        num = -Math.Abs(num3);
                        break;

                    case PdfPredefinedSpotFunctionKind.LineX:
                        num = num2;
                        break;

                    case PdfPredefinedSpotFunctionKind.LineY:
                        num = num3;
                        break;

                    case PdfPredefinedSpotFunctionKind.Round:
                        num2 = Math.Abs(num2);
                        num3 = Math.Abs(num3);
                        if ((num2 + num3) <= 1.0)
                        {
                            num = 1.0 - ((num2 * num2) + (num3 * num3));
                        }
                        else
                        {
                            num2--;
                            num3--;
                            num = ((num2 * num2) + (num3 * num3)) - 1.0;
                        }
                        break;

                    case PdfPredefinedSpotFunctionKind.Ellipse:
                        num2 = Math.Abs(num2);
                        num3 = Math.Abs(num3);
                        num = ((3.0 * num2) + (4.0 * num3)) - 3.0;
                        if (num >= 0.0)
                        {
                            num = (num <= 1.0) ? (0.5 - num) : ((((1.0 - (num2 * num2)) + ((1.0 - (num3 * num3)) / 0.5625)) / 4.0) - 1.0);
                        }
                        else
                        {
                            num3 /= 0.75;
                            num = 1.0 - (((num2 * num2) + (num3 * num3)) / 4.0);
                        }
                        break;

                    case PdfPredefinedSpotFunctionKind.EllipseA:
                        num = 1.0 - ((num2 * num2) + ((0.9 * num3) * num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.InvertedEllipseA:
                        num = ((num2 * num2) + ((0.9 * num3) * num3)) - 1.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.EllipseB:
                        num = 1.0 - Math.Sqrt((num2 * num2) + ((0.625 * num3) * num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.EllipseC:
                        num = 1.0 - (((0.9 * num2) * num2) + (num3 * num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.InvertedEllipseC:
                        num = (((0.9 * num2) * num2) + (num3 * num3)) - 1.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.Square:
                        num = -Math.Max(Math.Abs(num2), Math.Abs(num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.Cross:
                        num = -Math.Min(Math.Abs(num2), Math.Abs(num3));
                        break;

                    case PdfPredefinedSpotFunctionKind.Rhomboid:
                        num = ((0.9 * Math.Abs(num2)) + Math.Abs(num3)) / 2.0;
                        break;

                    case PdfPredefinedSpotFunctionKind.Diamond:
                        num2 = Math.Abs(num2);
                        num3 = Math.Abs(num3);
                        num = num2 + num3;
                        if (num <= 0.75)
                        {
                            num = 1.0 - ((num2 * num2) + (num3 * num3));
                        }
                        else if (num <= 1.23)
                        {
                            num = 1.0 - ((0.85 * num2) + num3);
                        }
                        else
                        {
                            num2--;
                            num3--;
                            num = ((num2 * num2) + (num3 * num3)) - 1.0;
                        }
                        break;

                    default:
                        break;
                }
            }
            return new double[] { num };
        }

        protected internal override object Write(PdfObjectCollection objects) => 
            new PdfName(PdfEnumToStringConverter.Convert<PdfPredefinedSpotFunctionKind>(this.kind, true));

        public PdfPredefinedSpotFunctionKind Kind =>
            this.kind;
    }
}

