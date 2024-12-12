namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfMarkupExternalData : PdfObject
    {
        protected PdfMarkupExternalData(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "ExData");
            dictionary.AddEnumName<PdfMarkupExternalDataType>("Subtype", this.Type);
            return dictionary;
        }

        public static PdfMarkupExternalData Parse(PdfPage page, PdfAnnotation parent, PdfReaderDictionary dictionary)
        {
            if (dictionary != null)
            {
                switch (dictionary.GetEnumName<PdfMarkupExternalDataType>("Subtype"))
                {
                    case PdfMarkupExternalDataType.Comment3D:
                        return new Pdf3dMarkupExternalData(page, dictionary);

                    case PdfMarkupExternalDataType.Measurement3D:
                        return new Pdf3dMeasurementExternalData(page, parent, dictionary);

                    case PdfMarkupExternalDataType.Geospatial3D:
                        return new Pdf3dMarkupGeospatialExternalData(dictionary);
                }
            }
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        public abstract PdfMarkupExternalDataType Type { get; }
    }
}

