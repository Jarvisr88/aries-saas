namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAxialShading : PdfGradientShading
    {
        internal const int Type = 2;
        private readonly PdfPoint axisStart;
        private readonly PdfPoint axisEnd;

        internal PdfAxialShading(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Coords");
            if ((array == null) || (array.Count != 4))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.axisStart = PdfDocumentReader.CreatePoint(array, 0);
            this.axisEnd = PdfDocumentReader.CreatePoint(array, 2);
        }

        internal PdfAxialShading(PdfPoint axisStart, PdfPoint axisEnd, PdfObjectList<PdfCustomFunction> blendFunctions) : base(blendFunctions)
        {
            this.axisStart = axisStart;
            this.axisEnd = axisEnd;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            object[] objArray1 = new object[] { this.axisStart.X, this.axisStart.Y, this.axisEnd.X, this.axisEnd.Y };
            dictionary.Add("Coords", objArray1);
            return dictionary;
        }

        public PdfPoint AxisStart =>
            this.axisStart;

        public PdfPoint AxisEnd =>
            this.axisEnd;

        protected override int ShadingType =>
            2;
    }
}

