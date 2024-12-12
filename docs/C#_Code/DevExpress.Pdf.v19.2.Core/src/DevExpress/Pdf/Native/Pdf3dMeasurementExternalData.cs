namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf3dMeasurementExternalData : PdfMarkupExternalData
    {
        private readonly Pdf3dMeasurement measurement;

        public Pdf3dMeasurementExternalData(PdfPage page, PdfAnnotation parent, PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            if (dictionary.TryGetValue("M3DREF", out obj2))
            {
                this.measurement = objects.GetObject<Pdf3dMeasurement>(obj2, dict => Pdf3dMeasurement.Parse(page, dict));
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("M3DREF", this.measurement);
            return dictionary;
        }

        public override PdfMarkupExternalDataType Type =>
            PdfMarkupExternalDataType.Measurement3D;
    }
}

