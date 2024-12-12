namespace DevExpress.Pdf.Native
{
    using System;

    public enum Pdf3dMeasurementType
    {
        [PdfFieldName("LD3")]
        LinearDimension = 0,
        [PdfFieldName("PD3")]
        PerpendicularDimension = 1,
        [PdfFieldName("AD3")]
        AngularDimension = 2,
        [PdfFieldName("RD3")]
        RadialDimension = 3,
        [PdfFieldName("3DC")]
        Comment = 4
    }
}

