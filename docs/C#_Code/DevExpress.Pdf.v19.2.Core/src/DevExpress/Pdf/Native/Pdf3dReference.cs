namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf3dReference : Pdf3dData
    {
        private readonly Pdf3dStream stream;

        public Pdf3dReference(PdfReaderDictionary dictionary, PdfPage page) : base(dictionary)
        {
            PdfReaderStream stream = dictionary.GetStream("3DD");
            if (stream != null)
            {
                this.stream = new Pdf3dStream(stream, page);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("3DD", this.stream);
            return dictionary;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        public override Pdf3dDataType Type =>
            Pdf3dDataType.Reference;

        public Pdf3dStream Stream =>
            this.stream;
    }
}

