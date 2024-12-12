namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class Pdf3dBackground : PdfObject
    {
        private readonly string subtype;
        private readonly PdfColorSpace colorSpace;
        private readonly IList<object> colorArray;
        private readonly bool entireAnnotation;

        public Pdf3dBackground(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.subtype = dictionary.GetName("Subtype");
            this.colorSpace = dictionary.GetColorSpace("CS");
            this.colorArray = dictionary.GetArray("C");
            bool? boolean = dictionary.GetBoolean("EA");
            this.entireAnnotation = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DBG");
            dictionary.AddName("Subtype", this.subtype);
            if (this.colorSpace != null)
            {
                dictionary.Add("CS", this.colorSpace.Write(objects));
            }
            dictionary.AddIfPresent("C", this.colorArray);
            dictionary.Add("EA", this.entireAnnotation, false);
            return dictionary;
        }

        public string Subtype =>
            this.subtype;

        public object ColorSpace =>
            this.colorSpace;

        public IList<object> ColorArray =>
            this.colorArray;

        public bool EntireAnnotation =>
            this.entireAnnotation;
    }
}

