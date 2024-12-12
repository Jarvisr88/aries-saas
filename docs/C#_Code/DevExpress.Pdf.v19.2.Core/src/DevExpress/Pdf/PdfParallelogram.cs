namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfParallelogram
    {
        private readonly double lowerLeftX;
        private readonly double lowerLeftY;
        private readonly double upperLeftX;
        private readonly double upperLeftY;
        private readonly double upperRightX;
        private readonly double upperRightY;
        private readonly double lowerRightX;
        private readonly double lowerRightY;

        internal PdfParallelogram(IList<object> array)
        {
            if (array.Count != 8)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.lowerLeftX = PdfDocumentReader.ConvertToDouble(array[0]);
            this.lowerLeftY = PdfDocumentReader.ConvertToDouble(array[1]);
            this.upperLeftX = PdfDocumentReader.ConvertToDouble(array[2]);
            this.upperLeftY = PdfDocumentReader.ConvertToDouble(array[3]);
            this.upperRightX = PdfDocumentReader.ConvertToDouble(array[4]);
            this.upperRightY = PdfDocumentReader.ConvertToDouble(array[5]);
            this.lowerRightX = PdfDocumentReader.ConvertToDouble(array[6]);
            this.lowerRightY = PdfDocumentReader.ConvertToDouble(array[7]);
        }

        internal double[] ToWriteableObject() => 
            new double[] { this.lowerLeftX, this.lowerLeftY, this.upperLeftX, this.upperLeftY, this.upperRightX, this.upperRightY, this.lowerRightX, this.lowerRightY };

        public double LowerLeftX =>
            this.lowerLeftX;

        public double LowerLeftY =>
            this.lowerLeftY;

        public double UpperLeftX =>
            this.upperLeftX;

        public double UpperLeftY =>
            this.upperLeftY;

        public double UpperRightX =>
            this.upperRightX;

        public double UpperRightY =>
            this.upperRightY;

        public double LowerRightX =>
            this.lowerRightX;

        public double LowerRightY =>
            this.lowerRightY;
    }
}

