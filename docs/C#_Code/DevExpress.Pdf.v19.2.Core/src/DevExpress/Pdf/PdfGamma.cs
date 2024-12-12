namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfGamma
    {
        internal const double Default = 1.0;
        private readonly double red;
        private readonly double green;
        private readonly double blue;

        internal PdfGamma()
        {
            this.red = 1.0;
            this.green = 1.0;
            this.blue = 1.0;
        }

        internal PdfGamma(IList<object> array)
        {
            this.red = 1.0;
            this.green = 1.0;
            this.blue = 1.0;
            if (array.Count != 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.red = PdfDocumentReader.ConvertToDouble(array[0]);
            this.green = PdfDocumentReader.ConvertToDouble(array[1]);
            this.blue = PdfDocumentReader.ConvertToDouble(array[2]);
        }

        internal double[] ToArray() => 
            new double[] { this.red, this.green, this.blue };

        public double Red =>
            this.red;

        public double Green =>
            this.green;

        public double Blue =>
            this.blue;

        internal bool IsDefault =>
            (this.red == 1.0) && ((this.green == 1.0) && (this.blue == 1.0));
    }
}

