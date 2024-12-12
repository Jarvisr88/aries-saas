namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRadialShading : PdfGradientShading
    {
        internal const int Type = 3;
        private readonly PdfPoint startingCircleCenter;
        private readonly double startingCircleRadius;
        private readonly PdfPoint endingCircleCenter;
        private readonly double endingCircleRadius;

        internal PdfRadialShading(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Coords");
            if ((array == null) || (array.Count != 6))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.startingCircleCenter = PdfDocumentReader.CreatePoint(array, 0);
            this.startingCircleRadius = PdfDocumentReader.ConvertToDouble(array[2]);
            this.endingCircleCenter = PdfDocumentReader.CreatePoint(array, 3);
            this.endingCircleRadius = PdfDocumentReader.ConvertToDouble(array[5]);
            if ((this.startingCircleRadius < 0.0) || (this.EndingCircleRadius < 0.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            object[] objArray1 = new object[] { this.startingCircleCenter.X, this.startingCircleCenter.Y, this.startingCircleRadius, this.endingCircleCenter.X, this.endingCircleCenter.Y, this.endingCircleRadius };
            dictionary.Add("Coords", objArray1);
            return dictionary;
        }

        public PdfPoint StartingCircleCenter =>
            this.startingCircleCenter;

        public double StartingCircleRadius =>
            this.startingCircleRadius;

        public PdfPoint EndingCircleCenter =>
            this.endingCircleCenter;

        public double EndingCircleRadius =>
            this.endingCircleRadius;

        protected override int ShadingType =>
            3;
    }
}

