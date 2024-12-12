namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfGradientShading : PdfShading
    {
        protected const string CoordsDictionaryKey = "Coords";
        private const string domainDictionaryKey = "Domain";
        private const string extendDictionaryKey = "Extend";
        private readonly PdfRange domain;
        private readonly bool extendX;
        private readonly bool extendY;

        protected PdfGradientShading(PdfObjectList<PdfCustomFunction> blendFunctions) : base(blendFunctions)
        {
            this.domain = new PdfRange(0.0, 1.0);
        }

        protected PdfGradientShading(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Domain");
            if (array == null)
            {
                this.domain = new PdfRange(0.0, 1.0);
            }
            else
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.domain = PdfDocumentReader.CreateDomain(array, 0);
            }
            IList<object> list2 = dictionary.GetArray("Extend");
            if (list2 != null)
            {
                if (list2.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                object obj2 = list2[0];
                object obj3 = list2[1];
                if (!(obj2 as bool) || !(obj3 as bool))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.extendX = (bool) obj2;
                this.extendY = (bool) obj3;
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            if ((this.domain.Min != 0.0) || (this.domain.Max != 1.0))
            {
                object[] objArray1 = new object[] { this.domain.Min, this.domain.Max };
                dictionary.Add("Domain", objArray1);
            }
            if (this.extendX || this.extendY)
            {
                object[] objArray2 = new object[] { this.extendX, this.extendY };
                dictionary.Add("Extend", objArray2);
            }
            return dictionary;
        }

        public PdfRange Domain =>
            this.domain;

        public bool ExtendX =>
            this.extendX;

        public bool ExtendY =>
            this.extendY;
    }
}

