namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class Pdf3dCommentMarkup : Pdf3dMeasurement
    {
        private readonly string anchor1ModelName;
        private readonly IList<object> textBoxPosition;

        public Pdf3dCommentMarkup(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.anchor1ModelName = dictionary.GetString("N1");
            this.textBoxPosition = dictionary.GetArray("TB");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddIfPresent("N1", this.anchor1ModelName);
            dictionary.AddIfPresent("TB", this.textBoxPosition);
            return dictionary;
        }

        public override Pdf3dMeasurementType Type =>
            Pdf3dMeasurementType.Comment;
    }
}

