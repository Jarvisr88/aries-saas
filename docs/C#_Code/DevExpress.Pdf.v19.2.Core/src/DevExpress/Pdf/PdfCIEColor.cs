namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCIEColor
    {
        private readonly double x;
        private readonly double y;
        private readonly double z;

        internal PdfCIEColor()
        {
        }

        internal PdfCIEColor(IList<object> array)
        {
            if (array.Count != 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.x = PdfDocumentReader.ConvertToDouble(array[0]);
            this.y = PdfDocumentReader.ConvertToDouble(array[1]);
            this.z = PdfDocumentReader.ConvertToDouble(array[2]);
            if ((this.x < 0.0) || ((this.y < 0.0) || (this.z < 0.0)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        internal double[] ToArray() => 
            new double[] { this.x, this.y, this.z };

        public double X =>
            this.x;

        public double Y =>
            this.y;

        public double Z =>
            this.z;

        internal bool IsEmpty =>
            (this.x == 0.0) && ((this.y == 0.0) && (this.z == 0.0));
    }
}

