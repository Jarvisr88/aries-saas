namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFunctionBasedShading : PdfShading
    {
        internal const int Type = 1;
        private const string domainDictionaryKey = "Domain";
        private const string matrixDictionaryKey = "Matrix";
        private readonly PdfRange domainX;
        private readonly PdfRange domainY;
        private readonly PdfTransformationMatrix matrix;

        internal PdfFunctionBasedShading(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.matrix = new PdfTransformationMatrix();
            IList<object> array = dictionary.GetArray("Domain");
            if (array == null)
            {
                this.domainX = new PdfRange(0.0, 1.0);
                this.domainY = new PdfRange(0.0, 1.0);
            }
            else
            {
                if (array.Count != 4)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.domainX = PdfDocumentReader.CreateDomain(array, 0);
                this.domainY = PdfDocumentReader.CreateDomain(array, 2);
            }
            IList<object> list2 = dictionary.GetArray("Matrix");
            this.matrix = (list2 == null) ? new PdfTransformationMatrix() : new PdfTransformationMatrix(list2);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            object[] objArray1 = new object[] { this.domainX.Min, this.domainX.Max, this.domainY.Min, this.domainY.Max };
            dictionary.Add("Domain", objArray1);
            dictionary.Add("Matrix", this.matrix.Data);
            return dictionary;
        }

        public PdfRange DomainX =>
            this.domainX;

        public PdfRange DomainY =>
            this.domainY;

        public PdfTransformationMatrix Matrix =>
            this.matrix;

        protected override int ShadingType =>
            1;

        protected override int FunctionDomainDimension =>
            2;
    }
}

