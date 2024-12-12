namespace DevExpress.Pdf.Native
{
    using System;

    public class Pdf3dMarkupGeospatialExternalData : PdfMarkupExternalData
    {
        public Pdf3dMarkupGeospatialExternalData(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        public override PdfMarkupExternalDataType Type =>
            PdfMarkupExternalDataType.Geospatial3D;
    }
}

