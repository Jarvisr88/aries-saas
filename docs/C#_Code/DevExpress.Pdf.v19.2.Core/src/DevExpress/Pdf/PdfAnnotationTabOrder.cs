namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfAnnotationTabOrder
    {
        [PdfFieldName("R")]
        RowOrder = 0,
        [PdfFieldName("C")]
        ColumnOrder = 1,
        [PdfFieldName("S")]
        StructureOrder = 2,
        [PdfFieldName("A")]
        ArrayOrder = 3,
        [PdfFieldName("W")]
        WidgetOrder = 4
    }
}

