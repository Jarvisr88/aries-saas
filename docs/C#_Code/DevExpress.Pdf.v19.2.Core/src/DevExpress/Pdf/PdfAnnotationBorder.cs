namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAnnotationBorder
    {
        private const double defaultHorizontalCornerRadius = 0.0;
        private const double defaultVerticalCornerRadius = 0.0;
        private const double defaultLineWidth = 1.0;
        private readonly double horizontalCornerRadius;
        private readonly double verticalCornerRadius;
        private readonly double lineWidth;
        private readonly PdfLineStyle lineStyle;

        public PdfAnnotationBorder() : this((double) 1.0)
        {
        }

        internal PdfAnnotationBorder(IList<object> values)
        {
            int count = values.Count;
            if (count != 3)
            {
                if (count != 4)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return;
                }
                else
                {
                    object obj2 = values[3];
                    IList<object> array = obj2 as IList<object>;
                    if (array == null)
                    {
                        if (!(obj2 is int) && !(obj2 is double))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        object[] objArray1 = new object[] { obj2 };
                        array = objArray1;
                    }
                    this.lineStyle = PdfAnnotationBorderStyle.ParseLineStyle(array);
                }
            }
            this.horizontalCornerRadius = PdfDocumentReader.ConvertToDouble(values[0]);
            this.verticalCornerRadius = PdfDocumentReader.ConvertToDouble(values[1]);
            this.lineWidth = PdfDocumentReader.ConvertToDouble(values[2]);
            if ((this.horizontalCornerRadius < 0.0) || ((this.verticalCornerRadius < 0.0) || (this.lineWidth < 0.0)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (this.lineStyle == null)
            {
                this.lineStyle = PdfLineStyle.CreateSolid();
            }
        }

        internal PdfAnnotationBorder(double width)
        {
            this.horizontalCornerRadius = 0.0;
            this.verticalCornerRadius = 0.0;
            this.lineWidth = width;
            this.lineStyle = PdfLineStyle.CreateSolid();
        }

        protected internal object ToWritableObject()
        {
            if (!this.lineStyle.IsDashed)
            {
                double[] numArray1 = new double[] { this.horizontalCornerRadius, this.verticalCornerRadius, this.lineWidth };
                return new PdfWritableDoubleArray(numArray1);
            }
            return new object[] { this.horizontalCornerRadius, this.verticalCornerRadius, this.lineWidth, this.lineStyle.Data[0] };
        }

        public double HorizontalCornerRadius =>
            this.horizontalCornerRadius;

        public double VerticalCornerRadius =>
            this.verticalCornerRadius;

        public double LineWidth =>
            this.lineWidth;

        public PdfLineStyle LineStyle =>
            this.lineStyle;

        public bool IsDefault =>
            (this.horizontalCornerRadius == 0.0) && ((this.lineWidth == 1.0) && ((this.verticalCornerRadius == 0.0) && !this.lineStyle.IsDashed));
    }
}

