namespace DevExpress.Pdf.Native
{
    using System;

    public enum PdfMarkupExternalDataType
    {
        [PdfFieldName("Markup3D")]
        Comment3D = 0,
        [PdfFieldName("3DM")]
        Measurement3D = 1,
        [PdfFieldName("MarkupGeo")]
        Geospatial3D = 2
    }
}

